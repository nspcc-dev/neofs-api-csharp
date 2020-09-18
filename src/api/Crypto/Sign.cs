using System;
using System.Security.Cryptography;
using Google.Protobuf;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.Crypto
{
    public static class SignerExtension
    {
        public static byte[] SignMessage(this byte[] data, ECDsa key)
        {
            var hash = new byte[65];
            hash[0] = 0x4;
            Console.WriteLine($"key: {key.PublicKey().ToHex()} hash: {SHA512.Create().ComputeHash(data).ToHex()}");
            key
                .SignHash(SHA512.Create().ComputeHash(data))
                .CopyTo(hash, 1);

            return hash;
        }

        private static void SignMessagePart(ECDsa key, byte[] data, Action<Signature> setter)
        {
            var sig = new Signature
            {
                Key = ByteString.CopyFrom(key.PublicKey()),
                Sign = ByteString.CopyFrom(data.SignMessage(key)),
            };
            setter(sig);
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
                    SignMessagePart(key, to_sign.GetBody().ToByteArray(), verify_header.AddBodySignature);
                }
                SignMessagePart(key, meta_header.ToByteArray(), verify_header.AddHeaderSignature);
                if (verify_origin != null)
                    SignMessagePart(key, verify_origin.ToByteArray(), verify_header.AddOriginSignature);
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
                    SignMessagePart(key, to_sign.GetBody().ToByteArray(), verify_header.AddBodySignature);
                }
                SignMessagePart(key, meta_header.ToByteArray(), verify_header.AddHeaderSignature);
                SignMessagePart(key, verify_origin.ToByteArray(), verify_header.AddOriginSignature);
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
