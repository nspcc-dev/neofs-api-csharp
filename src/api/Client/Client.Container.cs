using NeoFS.API.v2.Acl;
using NeoFS.API.v2.Container;
using NeoFS.API.v2.Crypto;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using System.Collections.Generic;
using System.Linq;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        public Container.Container GetContainer(ContainerID cid)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);

            var req = new GetRequest
            {
                Body = new GetRequest.Types.Body
                {
                    ContainerId = cid
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = container_client.Get(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container get response");
            return resp.Body.Container;
        }

        public ContainerID PutContainer(Container.Container container, Signature sig)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);

            var req = new PutRequest
            {
                Body = new PutRequest.Types.Body
                {
                    Container = container,
                    Signature = sig,
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = container_client.Put(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container put response");
            return resp.Body.ContainerId;
        }

        public void DeleteContainer(ContainerID cid, Signature sig)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);

            var req = new DeleteRequest
            {
                Body = new DeleteRequest.Types.Body
                {
                    ContainerId = cid,
                    Signature = sig
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = container_client.Delete(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container put response");
        }

        public List<ContainerID> ListContainers(OwnerID owner)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);

            var req = new ListRequest
            {
                Body = new ListRequest.Types.Body
                {
                    OwnerId = owner
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = container_client.List(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container put response");
            return resp.Body.ContainerIds.ToList();
        }

        public List<ContainerID> ListSelfContainers()
        {
            var w = key.ToOwnerID();
            return ListContainers(w);
        }

        public EACLTable GetExtendedACL(ContainerID cid)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);

            var req = new GetExtendedACLRequest
            {
                Body = new GetExtendedACLRequest.Types.Body
                {
                    ContainerId = cid
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = container_client.GetExtendedACL(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container put response");
            var eacl = resp.Body.Eacl;
            var sig = resp.Body.Signature;
            //TODO: verify signature
            return eacl;
        }

        public void SetExtendedACL(EACLTable eacl, Signature sig)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);

            var req = new SetExtendedACLRequest
            {
                Body = new SetExtendedACLRequest.Types.Body
                {
                    Eacl = eacl,
                    Signature = sig,
                }
            };
            req.MetaHeader = RequestMetaHeader.Default;
            req.SignRequest(key);

            var resp = container_client.SetExtendedACL(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container put response");
        }
    }
}
