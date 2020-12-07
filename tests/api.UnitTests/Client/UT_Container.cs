using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Acl;
using NeoFS.API.v2.Cryptography;
using NeoFS.API.v2.Refs;
using System;

namespace NeoFS.API.v2.UnitTests.FSClient
{
    [TestClass]
    public class UT_Container
    {
        [TestMethod]
        public void TestPutContainer()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var client = new Client.Client(host, key);
            var container = new Container.Container
            {
                Version = Refs.Version.SDKVersion(),
                OwnerId = key.ToOwnerID(),
                Nonce = new Guid().ToByteString(),
                BasicAcl = 0,
                PlacementPolicy = new Netmap.PlacementPolicy(),
            };
            var cid = client.PutContainer(container);
            Assert.AreEqual(container.CalCulateAndGetID, cid);
        }

        [TestMethod]
        public void TestGetContainer()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var client = new Client.Client(host, key);
            var cid = ContainerID.FromBase58String("GEoxHRfL3Hp5KgVQAVQpSHckPUMweQq8Rxh1PupzmB76");
            var container = client.GetContainer(cid);
            Assert.AreEqual(cid, container.CalCulateAndGetID);
        }

        [TestMethod]
        public void TestDeleteContainer()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var client = new Client.Client(host, key);
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            client.DeleteContainer(cid);
        }

        [TestMethod]
        public void TestListContainer()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var client = new Client.Client(host, key);
            var cids = client.ListContainers(key.ToOwnerID());
            Assert.AreEqual(0, cids.Count);
        }

        [TestMethod]
        public void TestGetExtendedACL()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            var client = new Client.Client(host, key);
            var eacl = client.GetExtendedACL(cid);
            Console.WriteLine(eacl);
        }

        [TestMethod]
        public void TestSetExtendedACL()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            var client = new Client.Client(host, key);
            var eacl = new EACLTable
            {
                ContainerId = cid,
            };
            client.SetExtendedACL(eacl);
        }
    }
}
