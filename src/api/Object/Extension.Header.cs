namespace NeoFS.API.v2.Object
{
    public sealed partial class Header
    {
        public static partial class Types
        {
            public sealed partial class Attribute
            {
                // SysAttributePrefix is a prefix of key to system attribute.
                public const string SysAttributePrefix = "__NEOFS__";

                // SysAttributeUploadID marks smaller parts of a split bigger object.
                public const string SysAttributeUploadID = SysAttributePrefix + "UPLOAD_ID";

                // SysAttributeExpEpoch tells GC to delete object after that epoch.
                public const string SysAttributeExpEpoch = SysAttributePrefix + "EXPIRATION_EPOCH";
            }
        }
    }
}
