using System.Security.Cryptography;
using Google.Protobuf;
using System.Linq;
using System.Collections.Generic;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Crypto;

namespace NeoFS.API.v2.Object
{
    public partial class GetRequest : IRequestSignable
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }

    public partial class GetResponse : IResponseVerifiable
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }

    public static class Extension
    {
        public static PutRequest PrepareInit(this Object obj, uint ttl, SessionToken tkn, ECDsa key, bool debug = false)
        {
            var req = new PutRequest
            {
                Body = new PutRequest.Types.Body
                {
                    Init = new PutRequest.Types.Body.Types.Init
                    {
                        ObjectId = obj.ObjectId,
                        Signature = new Refs.Signature
                        {
                            Key = null,
                            Sign = null,
                        },
                        Header = new Header
                        {
                            SessionToken = tkn,
                        },
                        CopiesNumber = 0,
                    }
                }
            };

            // req.SetTTL(ttl);
            // req.SignHeader(key, debug);

            return req;
        }

        public static PutRequest PrepareChunk(this IEnumerable<byte> chunk, uint ttl, ECDsa key)
        {
            return chunk.ToArray().PrepareChunk(ttl, key);
        }

        public static PutRequest PrepareChunk(this byte[] chunk, uint ttl, ECDsa key)
        {
            var req = new PutRequest
            {
                Body = new PutRequest.Types.Body
                {
                    Chunk = ByteString.CopyFrom(chunk),
                }
            };

            // req.SetTTL(ttl);
            // req.SignHeader(key, false);

            return req;
        }
    }
}
