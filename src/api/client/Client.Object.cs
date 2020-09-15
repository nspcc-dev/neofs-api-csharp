using NeoFS.API.v2.Object;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Crypto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;

namespace NeoFS.API
{
    public partial class Client
    {
        public async Task<Object> GetObject(Address object_address)
        {
            var object_client = new ObjectService.ObjectServiceClient(channel);
            var req = new GetRequest
            {
                Body = new GetRequest.Types.Body
                {
                    Address = object_address,
                }
            };
            req.MetaHeader = new RequestMetaHeader();
            req.SignRequest(key);
            var stream = object_client.Get(req).ResponseStream;
            var obj = new Object();
            var payload = new byte[] { };
            int offset = 0;
            while (await stream.MoveNext())
            {
                var resp = stream.Current;
                if (!resp.VerifyResponse())
                    throw new System.InvalidOperationException("response verification failed");
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
                        throw new System.FormatException("invalid object reponse");
                }
            }
            obj.Payload = ByteString.CopyFrom(payload);
            return obj;
        }

        public ObjectID PutObject(Object obj)
        {
            return new ObjectID();
        }

        public void DeleteObject(Address object_address)
        {

        }

        public Object GetObjectHeader(Address object_address)
        {
            return new Object();
        }

        public byte[] GetObjectPayloadRangeData(Address object_address, Range range)
        {
            return new byte[] { };
        }

        public List<byte[]> GetObjectPayloadRangeSHA256(Address object_address, Range range)
        {
            return new List<byte[]>();
        }
    }
}