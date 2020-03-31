using System;
using System.IO;
using System.Security.Cryptography;

using Google.Protobuf;

using NeoFS.Crypto;
using NeoFS.Utils;

using static NeoFS.API.Service.RequestVerificationHeader.Types;

namespace NeoFS.API.Service
{
    public interface IMeta
    {
        RequestMetaHeader Meta { get; set; }
    }

    public interface IVerify
    {
        RequestVerificationHeader Verify { get; set; }
    }

    public static class SignerExtention
    {
        public static void SignHeader(this IMessage req, ECDsa key, bool debug = false)
        {
            RequestMetaHeader meta = null;

            { // save old meta header
                if (req is IMeta m)
                {
                    meta = m.Meta;
                    m.Meta = new RequestMetaHeader();
                }
            }

            if (req is IVerify v)
            {

                if (v.Verify == null)
                {
                    v.Verify = new RequestVerificationHeader();
                }

                var data = req.SignMessage(key, debug);

                if (debug)
                {
                    Console.WriteLine("Hash = {0}", data.ToHex());
                }

                var sign = new Signature
                {
                    Sign = new Sign
                    {
                        Peer = ByteString.CopyFrom(key.Peer()),
                        Sign_ = ByteString.CopyFrom(data),
                    }
                };

                v.Verify.AddSignature(sign);
            }


            { // restore meta header
                if (req is IMeta m && meta != null)
                {
                    m.Meta = meta;
                }
            }
        }
        public static void SignHeader(this IMessage req, ECDsa key)
        {
            req.SignHeader(key, true);
        }

        public static byte[] SignMessage(this byte[] data, ECDsa key)
        {
            var hash = new byte[65];
            hash[0] = 0x4;

            key
                .SignHash(SHA512.Create().ComputeHash(data))
                .CopyTo(hash, 1);

            return hash;
        }

        public static byte[] SignMessage(this ByteString data, ECDsa key)
        {
            return data.ToByteArray().SignMessage(key);
        }

        public static byte[] SignMessage(this IMessage req, ECDsa key)
        {
            return req.SignMessage(key, true);
        }

        public static byte[] SignMessage(this IMessage req, ECDsa key, bool debug = false)
        {
            using (MemoryStream buf = new MemoryStream(req.CalculateSize()))
            {
                req.WriteTo(buf);

                if (debug)
                {
                    Console.WriteLine("\nMessage = {0} => {1}",
                    req.GetType().FullName,
                    buf.ToArray().ToHex());
                }

                return buf.ToArray().SignMessage(key);

                //var data = new byte[65];
                //byte[] hash;
                //data[0] = 0x4;


                //using (var sha = SHA512.Create())
                //{
                //    hash = sha.ComputeHash(buf.ToArray());
                //    key.SignHash(hash).CopyTo(data, 1);
                //}

                //return data;
            }
        }
    }

    public static class TTLExtension
    {
        public static void SetTTL(this IMeta req, uint ttl)
        {
            if (req.Meta == null)
            {
                req.Meta = new RequestMetaHeader
                {
                    Epoch = 0,
                    TTL = ttl,
                };
            }
            else
            {
                req.Meta.TTL = ttl;
            }
        }
    }

    public sealed partial class RequestVerificationHeader
    {
        public void AddSignature(Types.Signature sign)
        {
            signatures_.Add(sign);
        }
    }
}
