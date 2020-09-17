using System;
using System.Collections.Generic;
using System.Linq;
using NeoFS.API.v2.Refs;
using System.Security.Cryptography;
using Google.Protobuf;
using Neo.SmartContract;
using Neo.Wallets;
using Neo.Cryptography;

namespace NeoFS.API.v2.Crypto
{
    public static class KeyExtension
    {
        public static string ToAddress(this ECDsa key)
        {
            return key.PublicKey().PublicKeyToAddress();
        }

        public static OwnerID ToOwnerID(this ECDsa key)
        {
            return key.PublicKey().PublicKeyToOwnerID();
        }

        public static string OwnerIDToAddress(this OwnerID owner)
        {
            return Neo.Cryptography.Base58.Encode(owner.Value.ToByteArray());
        }

        public static string PublicKeyToAddress(this byte[] public_key)
        {
            Neo.Cryptography.ECC.ECCurve curve = Neo.Cryptography.ECC.ECCurve.Secp256r1;
            var public_key_point = Neo.Cryptography.ECC.ECPoint.DecodePoint(public_key, curve);
            var contract = Contract.CreateSignatureContract(public_key_point);
            return contract.ScriptHash.ToAddress();
        }

        public static OwnerID PublicKeyToOwnerID(this byte[] public_key)
        {
            var bytes = Neo.Cryptography.Base58.Decode(public_key.PublicKeyToAddress());
            return new OwnerID
            {
                Value = ByteString.CopyFrom(bytes),
            };
        }

        // encode point
        public static byte[] PublicKey(this ECDsa key)
        {
            var param = key.ExportParameters(false);
            var pubkey = new byte[33];
            var pos = 33 - param.Q.X.Length;

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

        private static byte[] DecodePublicKey(this byte[] public_key)
        {
            return Neo.Cryptography.ECC.ECPoint.DecodePoint(public_key, Neo.Cryptography.ECC.ECCurve.Secp256r1).EncodePoint(false).AsSpan(1).ToArray();
        }

        public static ECDsa LoadPrivateKey(this byte[] priv)
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

        public static ECDsa LoadPublicKey(this byte[] public_key)
        {
            var public_key_full = public_key.DecodePublicKey();
            var key = ECDsa.Create(new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                Q = new ECPoint
                {
                    X = public_key[..32],
                    Y = public_key[32..]
                }
            });
            return key;
        }
    }
}
