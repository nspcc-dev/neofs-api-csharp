using System;
using System.Security.Cryptography;
using Google.Protobuf;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.Crypto
{
    public static class VerifyExtension
    {
        public static bool VerifyMessage(this byte[] data, byte[] sig, ECDsa key)
        {
            Console.WriteLine($"key: {key.PublicKey().ToHex()} hash: {SHA512.Create().ComputeHash(data).ToHex()}");
            return key.VerifyHash(SHA512.Create().ComputeHash(data), sig[1..]);
        }

        private static bool VerifyMessagePart(byte[] data, Signature sig)
        {
            Console.WriteLine("verify message part");
            using (var key = sig.Key.ToByteArray().LoadPublicKey())
            {
                return data.VerifyMessage(sig.Sign.ToByteArray(), key);
            }
        }

        private static bool VerifyMatryoshkaLevel1(byte[] body, ResponseMetaHeader meta_header, ResponseVerificationHeader verify_header)
        {
            if (!VerifyMessagePart(meta_header.ToByteArray(), verify_header.MetaSignature))
                return false;
            if (!VerifyMessagePart(verify_header.Origin.ToByteArray(), verify_header.OriginSignature))
                return false;
            var origin = verify_header.Origin;
            if (origin is null)
            {
                if (!VerifyMessagePart(body, verify_header.BodySignature))
                    return false;
                return true;
            }
            if (verify_header.BodySignature is null) return false;
            return VerifyMatryoshkaLevel1(body, meta_header.Origin, origin);
        }

        public static bool VerifyResponse(this IMessage message)
        {
            if (message is IResponse to_sign)
            {
                return VerifyMatryoshkaLevel1(to_sign.GetBody().ToByteArray(), to_sign.MetaHeader, to_sign.VerifyHeader);
            }
            throw new System.InvalidOperationException("can't verify message");
        }

        private static bool VerifyMatryoshkaLevel2(byte[] body, RequestMetaHeader meta_header, RequestVerificationHeader verify_header)
        {
            if (!VerifyMessagePart(meta_header.ToByteArray(), verify_header.MetaSignature))
                return false;
            if (!VerifyMessagePart(verify_header.Origin.ToByteArray(), verify_header.OriginSignature))
                return false;
            var origin = verify_header.Origin;
            if (origin is null)
            {
                if (!VerifyMessagePart(body, verify_header.BodySignature))
                    return false;
                return true;
            }
            if (verify_header.BodySignature is null) return false;
            return VerifyMatryoshkaLevel2(body, meta_header.Origin, origin);
        }

        public static bool VerifyRequest(this IMessage message)
        {
            if (message is IRequest to_sign)
            {
                return VerifyMatryoshkaLevel2(to_sign.GetBody().ToByteArray(), to_sign.MetaHeader, to_sign.VerifyHeader);
            }
            throw new System.InvalidOperationException("can't verify message");
        }
    }
}