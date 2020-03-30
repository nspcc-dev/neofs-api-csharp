using System;
using NeoFS.Utils;

namespace NeoFS.Crypto
{
    public static class UUIDExtension
    {
        public static byte[] Bytes(this Guid id)
        {
            if (id == null)
                return null;

            return id.ToString().Replace("-", String.Empty).FromHex();
        }
    }
}