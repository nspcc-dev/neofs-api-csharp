using System;
using System.Security.Cryptography;
using Google.Protobuf;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.Crypto
{
    public static class SignerExtension
    {
        public static byte[] SignData(this byte[] data, ECDsa key)
        {
            var hash = new byte[65];
            hash[0] = 0x4;
            key
                .SignHash(SHA512.Create().ComputeHash(data))
                .CopyTo(hash, 1);

            return hash;
        }

        public static Signature SignMessagePart(this IMessage data, ECDsa key)
        {
            var sig = new Signature
            {
                Key = ByteString.CopyFrom(key.PublicKey()),
                Sign = ByteString.CopyFrom(data.ToByteArray().SignData(key)),
            };
            return sig;
        }

        public static void SignRequest(this IMessage message, ECDsa key)
        {
            if (message is IRequest to_sign)
            {
                if (to_sign.MetaHeader is null)
                    to_sign.MetaHeader = new RequestMetaHeader();
                var verify_origin = to_sign.VerifyHeader;
                var meta_header = to_sign.MetaHeader;
                var verify_header = new RequestVerificationHeader();

                if (verify_origin is null)
                {
                    verify_header.BodySignature = to_sign.GetBody().SignMessagePart(key);
                }
                verify_header.MetaSignature = meta_header.SignMessagePart(key);
                if (verify_origin != null)
                    verify_header.OriginSignature = verify_origin.SignMessagePart(key);
                verify_header.Origin = verify_origin;
                to_sign.VerifyHeader = verify_header;
            }
            else
            {
                throw new System.InvalidOperationException("can't sign message");
            }
        }

        public static void SignResponse(this IMessage message, ECDsa key)
        {
            if (message is IResponse to_sign)
            {
                if (to_sign.MetaHeader is null)
                    to_sign.MetaHeader = new ResponseMetaHeader();
                var verify_origin = to_sign.VerifyHeader;
                var meta_header = to_sign.MetaHeader;
                var verify_header = new ResponseVerificationHeader();

                if (verify_origin is null)
                {
                    verify_header.BodySignature = to_sign.GetBody().SignMessagePart(key);
                }
                else
                {
                    verify_header.OriginSignature = verify_origin.SignMessagePart(key);
                }
                verify_header.MetaSignature = meta_header.SignMessagePart(key);
                verify_header.Origin = verify_origin;
                to_sign.VerifyHeader = verify_header;
            }
            else
            {
                throw new System.InvalidOperationException("can't sign message");
            }
        }
    }
}
