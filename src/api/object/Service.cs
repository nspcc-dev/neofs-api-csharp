using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Google.Protobuf;
using NeoFS.API.Service;
using NeoFS.API.Session;
using NeoFS.Crypto;

namespace NeoFS.API.Object
{
    public sealed partial class Object
    {
        public const string KeyRootObject = "ROOT_OBJECT";
        public const string KeyStorageGroup = "STORAGE_GROUP";

        public void SetPayload(byte[] payload)
        {
            Payload = ByteString.CopyFrom(payload);
            SystemHeader.PayloadLength = (ulong)payload.Length;
        }

        public void SetPluginHeaders(ulong expired, string filename)
        {
            expired = expired * 60 + (ulong) DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            if (Headers == null)
            {
                //headers_ = new Google.Protobuf.Collections.RepeatedField<Header>();
                //Headers = new Google.Protobuf.Collections.RepeatedField<Header>();
            }

            Headers.Add(
                new Header
                {
                    UserHeader = new UserHeader
                    {
                        Key = "plugin",
                        Value = "sendneofs",
                    },
                });

            Headers.Add(
                new Header
                {
                    UserHeader = new UserHeader
                    {
                        Key = "expired",
                        Value = Convert.ToString(expired),
                    },
                });

            Headers.Add(
                new Header
                {
                    UserHeader = new UserHeader
                    {
                        Key = "filename",
                        Value = filename,
                    },
                });
        }

        public static Object Prepare(byte[] cid, byte[] oid, ulong size, ECDsa key)
        {
            byte[] owner = key.Address();

            return new Object
            {
                SystemHeader = new SystemHeader
                {
                    ID = ByteString.CopyFrom(oid),
                    CID = ByteString.CopyFrom(cid),
                    OwnerID = ByteString.CopyFrom(owner),
                    PayloadLength = size,
                    CreatedAt = new CreationPoint
                    {
                        UnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    }
                },
            };
        }
    }

    public static class RequestExtension
    {
        public static PutRequest PrepareHeader(this Object obj, uint ttl, Token tkn, ECDsa key, bool debug = false)
        {
            var req = new PutRequest
            {
                Header = new PutRequest.Types.PutHeader
                {
                    Object = obj,
                    Token = tkn,
                }
            };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

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
                Chunk = ByteString.CopyFrom(chunk),
            };

            req.SetTTL(ttl);
            req.SignHeader(key, false);

            return req;
        }
    }
    public sealed partial class PutRequest : IMeta, IVerify { }
    public sealed partial class GetRequest : IMeta, IVerify { }
    public sealed partial class HeadRequest : IMeta, IVerify { }
    public sealed partial class SearchRequest : IMeta, IVerify { }
}
