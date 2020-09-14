using System;
using System.Security.Cryptography;
using Google.Protobuf;
using Grpc.Core;
using Google.Protobuf;
using NeoFS.API.v2.Object;
using NeoFS.Crypto;

namespace NeoFS.API.v2.Object
{
    public partial class Client : ObjectService.ObjectServiceClient
    {
        private Channel channel;
        Client(Channel chan) : base(chan)
        {
            channel = chan;
        }

        public async Task<DeleteResponse> ObjectDelete(byte[] cid, Guid oid, uint ttl, ECDsa key, bool debug = false)
        {
            var token = await channel.EstablishSession(oid, ttl, key, debug);

            var req = new DeleteRequest
            {
                Token = token,
                OwnerID = ByteString.CopyFrom(key.Address()),
                Address = new Refs.Address
                {
                    CID = ByteString.CopyFrom(cid),
                    ObjectID = ByteString.CopyFrom(oid.Bytes()),
                },
            };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            return Delete(req);
        }

        public AsyncServerStreamingCall<SearchResponse> ObjectSearch(uint ttl, ECDsa key, IEnumerable<string> query, byte[] cid, bool sg = false, bool root = false, bool debug = true)
        {
            MemoryStream buf = new MemoryStream();
            Query.Query.Parse(query, sg, root, debug).WriteTo(buf);

            var req = new SearchRequest
            {
                ContainerID = ByteString.CopyFrom(cid),
                Query = ByteString.CopyFrom(buf.ToArray()),
            };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            return Search(req);
        }

        public PutRequest PrepareHeader(uint ttl, Token tkn, ECDsa key, bool debug = false)
        {
            var req = new PutRequest
            {
                Header = new PutRequest.Types.PutHeader
                {
                    Object = obj,
                    Token = tkn,
                    CopiesNumber = 0,
                }
            };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            return req;
        }

        public PutRequest PrepareChunk(this IEnumerable<byte> chunk, uint ttl, ECDsa key)
        {
            return chunk.ToArray().PrepareChunk(ttl, key);
        }

        public PutRequest PrepareChunk(this byte[] chunk, uint ttl, ECDsa key)
        {
            var req = new PutRequest
            {
                Chunk = ByteString.CopyFrom(chunk),
            };

            req.SetTTL(ttl);
            req.SignHeader(key, false);

            return req;
        }
    }
}
