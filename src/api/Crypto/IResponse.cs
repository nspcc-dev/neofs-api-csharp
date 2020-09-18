using NeoFS.API.v2.Session;
using Google.Protobuf;

namespace NeoFS.API.v2.Crypto
{
    public interface IResponseMeta
    {
        ResponseMetaHeader MetaHeader { get; set; }
    }

    public interface IResponse : IResponseMeta
    {
        ResponseVerificationHeader VerifyHeader { get; set; }
        IMessage GetBody();
    }
}