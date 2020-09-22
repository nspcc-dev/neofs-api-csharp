
namespace NeoFS.API.v2.Crypto
{
    public static class Base58
    {
        public static byte[] Decode(string addr)
        {
            return Neo.Cryptography.Base58.Decode(addr);
        }

        public static string Encode(byte[] bytes)
        {
            return Neo.Cryptography.Base58.Encode(bytes);
        }
    }
}