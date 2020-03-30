using System;
using System.Numerics;
using System.Security.Cryptography;

namespace NeoFS.Crypto
{
    public static class KeyExtension
    {
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
