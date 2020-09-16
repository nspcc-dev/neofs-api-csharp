using System;
using Google.Protobuf;

namespace NeoFS.API.v2.Crypto
{
    public static class HexExtension
    {
        public static string ToHex(this ByteString data)
        {
            return data.ToByteArray().ToHex();
        }

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
                data[i / 2] = (byte)Int32.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            }

            return data;
        }
    }
}
