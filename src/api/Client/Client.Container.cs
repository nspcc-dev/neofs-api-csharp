using Google.Protobuf;
using Neo;
using NeoFS.API.v2.Acl;
using NeoFS.API.v2.Container;
using NeoFS.API.v2.Cryptography;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Session;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoFS.API.v2.Client
{
    public partial class Client
    {
        public Container.Container GetContainer(ContainerID cid, CallOptions options = null)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new GetRequest
            {
                Body = new GetRequest.Types.Body
                {
                    ContainerId = cid
                }
            };
            req.MetaHeader = opts.GetRequestMetaHeader();
            req.SignRequest(key);

            var resp = container_client.Get(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container get response");
            return resp.Body.Container;
        }

        public ContainerID PutContainer(Container.Container container, CallOptions options = null)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);
            var opts = ApplyCustomOptions(options);

            if (container.OwnerId is null) container.OwnerId = key.ToOwnerID();
            var req = new PutRequest
            {
                Body = new PutRequest.Types.Body
                {
                    Container = container,
                    Signature = container.SignMessagePart(key),
                }
            };
            req.MetaHeader = opts.GetRequestMetaHeader();
            req.SignRequest(key);
            var resp = container_client.Put(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container put response");
            return resp.Body.ContainerId;
        }

        public void DeleteContainer(ContainerID cid, CallOptions options = null)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new DeleteRequest
            {
                Body = new DeleteRequest.Types.Body
                {
                    ContainerId = cid,
                }
            };
            req.Body.Signature = req.Body.SignMessagePart(key);
            req.MetaHeader = opts.GetRequestMetaHeader();
            req.SignRequest(key);

            var resp = container_client.Delete(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container put response");
        }

        public List<ContainerID> ListContainers(OwnerID owner, CallOptions options = null)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new ListRequest
            {
                Body = new ListRequest.Types.Body
                {
                    OwnerId = owner
                }
            };
            req.MetaHeader = opts.GetRequestMetaHeader();
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

        public EACLTable GetExtendedACL(ContainerID cid, CallOptions options = null)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new GetExtendedACLRequest
            {
                Body = new GetExtendedACLRequest.Types.Body
                {
                    ContainerId = cid
                }
            };
            req.MetaHeader = opts.GetRequestMetaHeader();
            req.SignRequest(key);

            var resp = container_client.GetExtendedACL(req);
            if (!resp.VerifyResponse())
                throw new InvalidOperationException("invalid container put response");
            var eacl = resp.Body.Eacl;
            var sig = resp.Body.Signature;
            if (!eacl.VerifyMessagePart(sig)) throw new InvalidOperationException(nameof(GetExtendedACL) + " invalid eacl");
            return eacl;
        }

        public void SetExtendedACL(EACLTable eacl, CallOptions options = null)
        {
            var container_client = new ContainerService.ContainerServiceClient(channel);
            var opts = ApplyCustomOptions(options);
            var req = new SetExtendedACLRequest
            {
                Body = new SetExtendedACLRequest.Types.Body
                {
                    Eacl = eacl,
                    Signature = eacl.SignMessagePart(key),
                }
            };
            req.MetaHeader = opts.GetRequestMetaHeader();
            req.SignRequest(key);

            var resp = container_client.SetExtendedACL(req);
            if (!resp.VerifyResponse())
                throw new System.InvalidOperationException("invalid container put response");
        }
    }
}
