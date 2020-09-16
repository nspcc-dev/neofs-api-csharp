using System;
using System.Security.Cryptography;
using Google.Protobuf;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Refs;
using System.Numerics;

namespace NeoFS.API.v2.Crypto
{
    public interface IRequestMeta
    {
        RequestMetaHeader MetaHeader { get; set; }
    }

    public interface IRequestSignable : IRequestMeta
    {
        RequestVerificationHeader VerifyHeader { get; set; }
        IMessage GetBody();
    }

    public interface IResponseMeta
    {
        ResponseMetaHeader MetaHeader { get; set; }
    }

    public interface IResponseVerifiable : IResponseMeta
    {
        ResponseVerificationHeader VerifyHeader { get; set; }
        IMessage GetBody();
    }

    public static class SignerExtension
    {
        public static byte[] SignMessage(this byte[] data, ECDsa key)
        {
            var hash = new byte[65];
            hash[0] = 0x4;

            key
                .SignHash(SHA512.Create().ComputeHash(data))
                .CopyTo(hash, 1);

            return hash;
        }

        public static void SignRequest(this IMessage message, ECDsa key)
        {
            if (message is IRequestSignable to_sign)
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
                SignMessagePart(key, verify_origin.ToByteArray(), verify_header.AddOriginSignature);
                verify_header.Origin = verify_origin;
                to_sign.VerifyHeader = verify_header;
            }
            else
            {
                throw new System.InvalidOperationException("can't sign message");
            }
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

        public static bool VerifyResponse(this IMessage message)
        {
            if (message is IResponseVerifiable to_sign)
            {
                return VerifyMatryoshkaLevel(to_sign.GetBody().ToByteArray(), to_sign.MetaHeader, to_sign.VerifyHeader);
            }
            throw new System.InvalidOperationException("can't verify message");
        }

        private static bool VerifyMatryoshkaLevel(byte[] body, ResponseMetaHeader meta_header, ResponseVerificationHeader verify_header)
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
            return VerifyMatryoshkaLevel(body, meta_header.Origin, origin);
        }

        private static bool VerifyMessagePart(byte[] data, Signature sign)
        {
            //TODO: verify
            return true;
        }
    }

    public static class TTLExtension
    {
        public static void SetTTL(this IRequestMeta req, uint ttl)
        {
            if (req.MetaHeader == null)
            {
                req.MetaHeader = new RequestMetaHeader
                {
                    Epoch = 0,
                    Ttl = ttl,
                };
            }
            else
            {
                req.MetaHeader.Ttl = ttl;
            }
        }
    }
}
