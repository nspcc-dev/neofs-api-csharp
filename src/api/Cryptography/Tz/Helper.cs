using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace NeoFS.API.v2.Cryptography.Tz
{
    public static class Helper
    {
        public static ulong NextUlong(this RandomNumberGenerator rng)
        {
            var buff = new byte[8];
            rng.GetBytes(buff);
            return BitConverter.ToUInt64(buff, 0);
        }

        public static int GetLeadingZeros(ulong value)
        {
            int i = 64;
            while (value != 0)
            {
                value >>= 1;
                i--;
            }
            return i;
        }

        public static int GetNonZeroLength(this ulong value)
        {
            return 64 - GetLeadingZeros(value);
        }

        public static string ToHexString(this byte[] value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in value)
                sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        public static byte[] HexToBytes(this string value)
        {
            if (value == null || value.Length == 0)
                return Array.Empty<byte>();
            if (value.Length % 2 == 1)
                throw new FormatException();
            byte[] result = new byte[value.Length / 2];
            for (int i = 0; i < result.Length; i++)
                result[i] = byte.Parse(value.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier);
            return result;
        }
    }
}
