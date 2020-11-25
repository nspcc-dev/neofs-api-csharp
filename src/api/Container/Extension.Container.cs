using Google.Protobuf;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Cryptography;

namespace NeoFS.API.v2.Container
{
    public partial class Container
    {
        private ContainerID _id;
        public ContainerID CalCulateAndGetID
        {
            get
            {
                if (_id is null)
                    _id = new ContainerID
                    {
                        Value = this.Sha256()
                    };
                return _id;
            }
        }
    }
}