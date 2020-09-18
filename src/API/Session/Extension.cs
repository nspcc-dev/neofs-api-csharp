using NeoFS.API.v2.Refs;

namespace NeoFS.API.v2.Session
{
    public sealed partial class RequestVerificationHeader
    {
        public void AddHeaderSignature(Signature sig)
        {
            MetaSignature = sig;
        }

        public void AddBodySignature(Signature sig)
        {
            BodySignature = sig;
        }

        public void AddOriginSignature(Signature sig)
        {
            OriginSignature = sig;
        }
    }

    public sealed partial class ResponseVerificationHeader
    {
        public void AddHeaderSignature(Signature sig)
        {
            MetaSignature = sig;
        }

        public void AddBodySignature(Signature sig)
        {
            BodySignature = sig;
        }

        public void AddOriginSignature(Signature sig)
        {
            OriginSignature = sig;
        }
    }

}
