using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Container;

namespace NeoFS.API
{
    public partial class Client
    {
        public Container GetContainer(ContainerID cid)
        {
            return new Container();
        }

        public ContainerID PutContainer(Container container)
        {
            return new ContainerID();
        }

        public void DeleteContainer(ContainerID cid)
        {

        }

        public ContainerID[] ListContainers(OwnerID owner)
        {
            return new ContainerID[] { };
        }

        public ContainerID[] ListSelfContainers()
        {
            return new ContainerID[] { };
        }
    }
}