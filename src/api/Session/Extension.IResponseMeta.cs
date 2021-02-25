using NeoFS.API.v2.Acl;

namespace NeoFS.API.v2.Session
{
    public static partial class SessionExtension
    {
        public static void SetVersion(this IResponseMeta meta, ulong epoch)
        {
            meta.MetaHeader.Epoch = epoch;
        }

        public static void SetTTL(this IResponseMeta meta, uint ttl)
        {
            meta.MetaHeader.Ttl = ttl;
        }

        public static void SetEpoch(this IResponseMeta meta, ulong epoch)
        {
            meta.MetaHeader.Epoch = epoch;
        }

        public static void AddXHeaders(this IResponseMeta meta, XHeader[] xHeaders)
        {
            meta.MetaHeader.XHeaders.AddRange(xHeaders);
        }
    }
}