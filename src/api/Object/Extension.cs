using System.Security.Cryptography;
using Google.Protobuf;
using System.Linq;
using System.Collections.Generic;
using NeoFS.API.v2.Session;
using NeoFS.API.v2.Crypto;

namespace NeoFS.API.v2.Object
{
    public partial class GetRequest : IRequestSignable
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }

    public partial class GetResponse : IResponseVerifiable
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }

    public partial class GetRangeHashRequest : IRequestSignable
    {
        public IMessage GetBody()
        {
            return Body;
        }
    }

    public partial class Object
    {
        public const int ChunkSize = 3 * (1 << 20);
    }
}
