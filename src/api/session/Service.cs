using System;
using System.IO;
using System.Security.Cryptography;
using Google.Protobuf;
using NeoFS.API.Service;

namespace NeoFS.API.Session
{
    public sealed partial class CreateRequest : IMeta, IVerify { }

    public static class ServiceExtension
    {
        public static bool IsSame(this Token tkn, Token old)
        {
            return tkn != null
                && old != null
                && tkn.FirstEpoch == old.FirstEpoch
                && tkn.LastEpoch == old.LastEpoch
                && !tkn.OwnerID.IsEmpty
                && !old.OwnerID.IsEmpty
                && tkn.OwnerID.Equals(old.OwnerID)
                && tkn.PublicKeys.Count > 0
                && tkn.ObjectID.Count > 0
                && old.ObjectID.Count > 0
                && tkn.ObjectID.Equals(old.ObjectID);
        }

        public static byte[] GetBytes(this Token tkn)
        {
            var num = 0;
            if (tkn.ObjectID.Count > 0)
            {
                num = tkn.ObjectID[0].Length;
            }


            var len = 16 * tkn.ObjectID.Count * num;
            var buf = new MemoryStream(len);

            buf.Write(BitConverter.GetBytes(tkn.FirstEpoch));
            buf.Write(BitConverter.GetBytes(tkn.LastEpoch));

            for (int i = 0; i < tkn.ObjectID.Count; i++)
            {
                buf.Write(tkn.ObjectID[i].ToByteArray());
            }

            return buf.ToArray();
        }

        public static void Sign(this Token tkn, ECDsa key)
        {
            { // sign token header
                if (tkn.Header == null)
                {
                    tkn.Header = new VerificationHeader
                    {
                        PublicKey = null,
                        KeySignature = null,
                    };
                }

                var signature = tkn.Header.PublicKey.SignMessage(key);
                tkn.Header.KeySignature = ByteString.CopyFrom(signature);
            }

            // sign message
            tkn.Signature = ByteString.CopyFrom(tkn.GetBytes().SignMessage(key));
        }

        public static CreateRequest PrepareInit(this Token tkn, uint ttl, ECDsa key, bool debug = false)
        {
            var req = new CreateRequest{ Init = tkn };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            return req;
        }

        public static CreateRequest PrepareSigned(this Token tkn, uint ttl, ECDsa key, bool debug = false)
        {
            var req = new CreateRequest { Signed = tkn };

            req.SetTTL(ttl);
            req.SignHeader(key, debug);

            return req;
        }
    }
}
