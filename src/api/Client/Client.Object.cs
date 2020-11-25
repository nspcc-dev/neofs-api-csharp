using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.v2.Cryptography;
using NeoFS.API.v2.Object;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        public async Task<Object.Object> GetObject(Address object_address)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);

            var req = new GetRequest
            {
                Body = new GetRequest.Types.Body
                {
                    Address = object_address,
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var stream = object_client.Get(req).ResponseStream;
            var obj = new Object.Object();
            var payload = new byte[] { };
            int offset = 0;
            while (await stream.MoveNext())
            {
                var resp = stream.Current;
                if (!resp.VerifyResponse())
                    throw new System.InvalidOperationException("invalid object get response");
                switch (resp.Body.ObjectPartCase)
                {
                    case GetResponse.Types.Body.ObjectPartOneofCase.Init:
                        {
                            obj.ObjectId = resp.Body.Init.ObjectId;
                            obj.Signature = resp.Body.Init.Signature;
                            obj.Header = resp.Body.Init.Header;
                            payload = new byte[obj.Header.PayloadLength];
                            break;
                        }
                    case GetResponse.Types.Body.ObjectPartOneofCase.Chunk:
                        {
                            resp.Body.Chunk.CopyTo(payload, offset);
                            offset += resp.Body.Chunk.Length;
                            break;
                        }
                    default:
                        throw new System.FormatException("malformed object get reponse");
                }
            }
            obj.Payload = ByteString.CopyFrom(payload);
            return obj;
        }

        public async Task<ObjectID> PutObject(Object.Object obj, uint copy = 3)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var call = object_client.Put();
            var req_stream = call.RequestStream;

            var init_req = new PutRequest
            {
                Body = new PutRequest.Types.Body
                {
                    Init = new PutRequest.Types.Body.Types.Init
                    {
                        ObjectId = obj.ObjectId,
                        Signature = obj.Signature,
                        Header = obj.Header,
                        CopiesNumber = copy,
                    }
                }
            };
            init_req.MetaHeader = RequestMetaHeader.Default;
            init_req.SignRequest(key);

            await req_stream.WriteAsync(init_req);

            int offset = 0;
            while (offset < obj.Payload.Length)
            {
                var end = offset + Object.Object.ChunkSize > obj.Payload.Length ? obj.Payload.Length : offset + Object.Object.ChunkSize;
                var chunk = ByteString.CopyFrom(obj.Payload.ToByteArray()[offset..end]);
                var chunk_req = new PutRequest
                {
                    Body = new PutRequest.Types.Body
                    {
                        Chunk = chunk,
                    }
                };
                await req_stream.WriteAsync(chunk_req);
            }
            await req_stream.CompleteAsync();
            var resp = await call.ResponseAsync;
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid object put response");
            return resp.Body.ObjectId;
        }

        public bool DeleteObject(Address object_address)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);

            var req = new DeleteRequest
            {
                Body = new DeleteRequest.Types.Body
                {
                    Address = object_address,
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = object_client.Delete(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid object delete response");
            return true;
        }

        public Object.Object GetObjectHeader(Address object_address, bool minimal)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);

            var req = new HeadRequest
            {
                Body = new HeadRequest.Types.Body
                {
                    Address = object_address,
                    MainOnly = minimal,
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = object_client.Head(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid object get header response");
            var header = new Header();
            switch (resp.Body.HeadCase)
            {
                case HeadResponse.Types.Body.HeadOneofCase.Header:
                    {
                        var short_header = resp.Body.ShortHeader;
                        if (short_header is null)
                            throw new System.FormatException("malformed object header");
                        header.PayloadLength = short_header.PayloadLength;
                        header.Version = short_header.Version;
                        header.OwnerId = short_header.OwnerId;
                        header.ObjectType = short_header.ObjectType;
                        header.CreationEpoch = short_header.CreationEpoch;
                        break;
                    }
                case HeadResponse.Types.Body.HeadOneofCase.ShortHeader:
                    {
                        var full_header = resp.Body.Header;
                        if (full_header is null)
                            throw new System.FormatException("malformed object header");
                        header = full_header.Header;
                        //TODO: check signature
                        break;
                    }
                default:
                    throw new System.FormatException("malformed object header response");
            }
            var obj = new Object.Object();
            obj.Header = header;
            return obj;
        }

        public async Task<byte[]> GetObjectPayloadRangeData(Address object_address, Range range)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var req = new GetRangeRequest
            {
                Body = new GetRangeRequest.Types.Body
                {
                    Address = object_address,
                    Range = range,
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var stream = object_client.GetRange(req).ResponseStream;
            var payload = new byte[range.Length];
            var offset = 0;
            while (await stream.MoveNext())
            {
                var resp = stream.Current;
                if (!resp.VerifyResponse())
                    throw new System.FormatException("invalid object range response");
                var chunk = resp.Body.Chunk;
                chunk.CopyTo(payload, offset);
                offset += chunk.Length;
            }
            return payload;
        }

        public List<byte[]> GetObjectPayloadRangeSHA256(Address object_address, Range[] range, byte[] salt, ChecksumType check_sum_type)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var req = new GetRangeHashRequest
            {
                Body = new GetRangeHashRequest.Types.Body
                {
                    Address = object_address,
                    Salt = ByteString.CopyFrom(salt),
                    Type = check_sum_type,
                }
            };
            req.Body.Ranges.AddRange(range);
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = object_client.GetRangeHash(req);
            if (!resp.VerifyResponse())
                throw new System.FormatException("invalid object range hash response");
            return resp.Body.HashList.Select(p => p.ToByteArray()).ToList();
        }

        public async Task<List<ObjectID>> SearchObject(ContainerID cid, SearchRequest.Types.Body.Types.Filter[] filters, uint query_version)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var req = new SearchRequest
            {
                Body = new SearchRequest.Types.Body
                {
                    ContainerId = cid,
                    Version = query_version,
                }
            };
            req.Body.Filters.AddRange(filters);
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var stream = object_client.Search(req).ResponseStream;
            var result = new List<ObjectID>();
            while (await stream.MoveNext())
            {
                var resp = stream.Current;
                if (!resp.VerifyResponse())
                    throw new System.FormatException("invalid object search response");
                result.Concat(resp.Body.IdList);
            }

            return result;
        }
    }
}
