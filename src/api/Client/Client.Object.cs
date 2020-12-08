using Google.Protobuf;
using Grpc.Core;
using NeoFS.API.v2.Cryptography;
using NeoFS.API.v2.Object;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        public async Task<Object.Object> GetObject(Address object_address, CallOptions options = null)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new GetRequest
            {
                Body = new GetRequest.Types.Body
                {
                    Address = object_address,
                }
            };
            var meta = opts.GetRequestMetaHeader();
            AttachObjectSessionToken(options, meta, object_address, ObjectSessionContext.Types.Verb.Get);
            req.MetaHeader = meta;
            req.SignRequest(key);

            var stream = object_client.Get(req).ResponseStream;
            var obj = new Object.Object();
            var payload = new byte[] { };
            int offset = 0;
            while (await stream.MoveNext())
            {
                var resp = stream.Current;
                if (!resp.VerifyResponse())
                    throw new InvalidOperationException("invalid object get response");
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
                        throw new FormatException("malformed object get reponse");
                }
            }
            obj.Payload = ByteString.CopyFrom(payload);
            return obj;
        }

        public async Task<ObjectID> PutObject(Object.Object obj, CallOptions options = null)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var call = object_client.Put();
            var opts = ApplyCustomOptions(options);
            var req_stream = call.RequestStream;

            var req = new PutRequest();
            var body = new PutRequest.Types.Body();
            req.Body = body;
            var address = new Address
            {
                ContainerId = obj.Header.ContainerId,
                ObjectId = obj.ObjectId,
            };
            var meta = opts.GetRequestMetaHeader();
            AttachObjectSessionToken(options, meta, address, ObjectSessionContext.Types.Verb.Put);
            req.MetaHeader = meta;
            var init = new PutRequest.Types.Body.Types.Init
            {
                ObjectId = obj.ObjectId,
                Signature = obj.Signature,
                Header = obj.Header,
            };
            req.Body.Init = init;
            req.SignRequest(key);

            await req_stream.WriteAsync(req);

            int offset = 0;
            while (offset < obj.Payload.Length)
            {
                var end = offset + Object.Object.ChunkSize > obj.Payload.Length ? obj.Payload.Length : offset + Object.Object.ChunkSize;
                var chunk = ByteString.CopyFrom(obj.Payload.ToByteArray()[offset..end]);
                var chunk_body = new PutRequest.Types.Body
                {
                    Chunk = chunk,
                };
                req.Body = chunk_body;
                req.VerifyHeader = null;
                req.SignRequest(key);
                await req_stream.WriteAsync(req);
                offset = end;
            }
            await req_stream.CompleteAsync();
            var resp = await call.ResponseAsync;
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid object put response");
            return resp.Body.ObjectId;
        }

        public bool DeleteObject(Address object_address, CallOptions options = null)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new DeleteRequest
            {
                Body = new DeleteRequest.Types.Body
                {
                    Address = object_address,
                }
            };
            var meta = opts.GetRequestMetaHeader();
            AttachObjectSessionToken(options, meta, object_address, ObjectSessionContext.Types.Verb.Delete);
            req.MetaHeader = meta;
            req.SignRequest(key);

            var resp = object_client.Delete(req);
            if (!resp.VerifyResponse())
                throw new InvalidOperationException("invalid object delete response");
            return true;
        }

        public Object.Object GetObjectHeader(Address object_address, bool minimal, CallOptions options = null)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new HeadRequest
            {
                Body = new HeadRequest.Types.Body
                {
                    Address = object_address,
                    MainOnly = minimal,
                }
            };
            var meta = opts.GetRequestMetaHeader();
            AttachObjectSessionToken(options, meta, object_address, ObjectSessionContext.Types.Verb.Head);
            req.MetaHeader = meta;
            req.SignRequest(key);

            var resp = object_client.Head(req);
            if (!resp.VerifyResponse())
                throw new InvalidOperationException("invalid object get header response");
            var header = new Header();
            var sig = new Signature();
            switch (resp.Body.HeadCase)
            {
                case HeadResponse.Types.Body.HeadOneofCase.ShortHeader:
                    {
                        if (!minimal) throw new FormatException("expect full header received short");
                        var short_header = resp.Body.ShortHeader;
                        if (short_header is null)
                            throw new FormatException("malformed object header");
                        header.PayloadLength = short_header.PayloadLength;
                        header.Version = short_header.Version;
                        header.OwnerId = short_header.OwnerId;
                        header.ObjectType = short_header.ObjectType;
                        header.CreationEpoch = short_header.CreationEpoch;
                        break;
                    }
                case HeadResponse.Types.Body.HeadOneofCase.Header:
                    {
                        if (minimal) throw new FormatException("expect short header received full");
                        var full_header = resp.Body.Header;
                        if (full_header is null)
                            throw new FormatException("malformed object header");
                        header = full_header.Header;
                        sig = full_header.Signature;
                        if (!object_address.ObjectId.VerifyMessagePart(sig))
                        {
                            throw new InvalidOperationException(nameof(GetObjectHeader) + " invalid signature");
                        }
                        break;
                    }
                default:
                    throw new FormatException("malformed object header response");
            }
            var obj = new Object.Object
            {
                ObjectId = object_address.ObjectId,
                Header = header,
                Signature = sig,
            };
            return obj;
        }

        public async Task<byte[]> GetObjectPayloadRangeData(Address object_address, Object.Range range, CallOptions options = null)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new GetRangeRequest
            {
                Body = new GetRangeRequest.Types.Body
                {
                    Address = object_address,
                    Range = range,
                }
            };
            var meta = opts.GetRequestMetaHeader();
            AttachObjectSessionToken(options, meta, object_address, ObjectSessionContext.Types.Verb.Range);
            req.SignRequest(key);

            var stream = object_client.GetRange(req).ResponseStream;
            var payload = new byte[range.Length];
            var offset = 0;
            while (await stream.MoveNext())
            {
                var resp = stream.Current;
                if (!resp.VerifyResponse())
                    throw new FormatException("invalid object range response");
                var chunk = resp.Body.Chunk;
                chunk.CopyTo(payload, offset);
                offset += chunk.Length;
            }
            return payload;
        }

        public List<byte[]> GetObjectPayloadRangeHash(Address object_address, Object.Range[] range, byte[] salt, ChecksumType check_sum_type, CallOptions options = null)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var opts = ApplyCustomOptions(options);
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
            var meta = opts.GetRequestMetaHeader();
            AttachObjectSessionToken(options, meta, object_address, ObjectSessionContext.Types.Verb.Rangehash);
            req.MetaHeader = meta;
            req.SignRequest(key);

            var resp = object_client.GetRangeHash(req);
            if (!resp.VerifyResponse())
                throw new FormatException("invalid object range hash response");
            return resp.Body.HashList.Select(p => p.ToByteArray()).ToList();
        }

        public async Task<List<ObjectID>> SearchObject(ContainerID cid, SearchRequest.Types.Body.Types.Filter[] filters, CallOptions options = null)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new SearchRequest
            {
                Body = new SearchRequest.Types.Body
                {
                    ContainerId = cid,
                    Version = SearchObjectVersion,
                }
            };
            req.Body.Filters.AddRange(filters);
            var meta = opts.GetRequestMetaHeader();
            AttachObjectSessionToken(options, meta, new Address { ContainerId = cid }, ObjectSessionContext.Types.Verb.Search);
            req.MetaHeader = meta;
            req.SignRequest(key);

            var stream = object_client.Search(req).ResponseStream;
            var result = new List<ObjectID>();
            while (await stream.MoveNext())
            {
                var resp = stream.Current;
                if (!resp.VerifyResponse())
                    throw new FormatException("invalid object search response");
                result = result.Concat(resp.Body.IdList).ToList();
            }
            return result;
        }

        private void AttachObjectSessionToken(CallOptions options, RequestMetaHeader meta, Address address, ObjectSessionContext.Types.Verb verb,
            ulong exp = 0, ulong nbf = 0, ulong iat = 0)
        {
            if (options.Session is null) return;
            if (options.Session.Signature != null)
            {
                meta.SessionToken = options.Session;
                return;
            }

            var token = new SessionToken
            {
                Body = options.Session.Body
            };

            var ctx = new ObjectSessionContext
            {
                Address = address,
                Verb = verb,
            };

            var lt = new SessionToken.Types.Body.Types.TokenLifetime
            {
                Iat = iat,
                Exp = exp,
                Nbf = nbf,
            };

            token.Body.Object = ctx;
            token.Body.Lifetime = lt;
            token.Signature = token.Body.SignMessagePart(key);

            meta.SessionToken = token;
        }
    }
}
