using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace NeoFS.Crypto
{
    public static class KeyExtension
    {
        public static byte[] CheckSum(this IEnumerable<byte> data)
        {
            return data.ToArray().CheckSum();
        }

        public static byte[] CheckSum(this byte[] data)
        {
            return SHA256
                .Create()
                .ComputeHash(SHA256.Create().ComputeHash(data))
                .Take(4)
                .ToArray();
        }

        public static byte[] Address(this ECDsa key)
        {
            byte[] address = new byte[25];

            address[0] = 0x17;
            byte[] hash = SHA256.Create().ComputeHash(key.VerificationScript());

            RIPEMD160
                .Create()
                .ComputeHash(hash)
                .CopyTo(address, 1);

            // copy checksum to 
            address.Take(21).CheckSum().CopyTo(address, 21);

            return address;
        }

        public static string ToAddress(this ECDsa key)
        {
            return Base58.Encode(key.Address());
        }

        public static byte[] VerificationScript(this ECDsa key)
        {
            byte[] owner = new byte[35]; // version? + key + checksig

            owner[0] = 0x21; // version?
            owner[34] = 0xac; // checksig
            key.Peer().CopyTo(owner, 1);

            return owner;
        }

        public static byte[] Peer(this ECDsa key)
        {
            var param = key.ExportParameters(false);
            var pubkey = new byte[33];
            var pos = 33 - param.Q.X.Length;
            var x = new BigInteger(param.Q.Y);

            param.Q.X.CopyTo(pubkey, pos);

            if ((param.Q.Y[0] & 1) == 0)
            {
                pubkey[0] = 0x3;
            }
            else
            {
                pubkey[0] = 0x2;
            }

            return pubkey;
        }

        public static ECDsa LoadKey(this byte[] priv)
        {
            ECCurve curve = ECCurve.NamedCurves.nistP256;

            ECDsa key = ECDsa.Create(curve);

            key.ImportECPrivateKey(priv, out _);
            //new ECParameters
            //{
            //    Curve = curve,
            //    D = priv,
            //    Q = new ECPoint
            //    {
            //        X = null,
            //        Y = null,
            //    },
            //});

            //key.ImportECPrivateKey(priv, out _);

            return key;
        }
    }
}
