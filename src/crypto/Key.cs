using System;
using System.Numerics;
using System.Security.Cryptography;

namespace NeoFS.Crypto
{
    public static class KeyExtension
    {
        public static string ToHex(this byte[] data)
        {
            return BitConverter.
                ToString(data).
                Replace("-", String.Empty).
                ToLower();
        }

        public static byte[] FromHex(this string str)
        {
            byte[] data = new byte[str.Length / 2];
            for (int i = 0; i < str.Length; i += 2)
            {
                string hex = str.Substring(i, 2);
                data[i / 2] = (byte) Int32.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            }

            return data;
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
