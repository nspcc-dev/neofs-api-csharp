using FluentAssertions;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Client;
using NeoFS.API.v2.Cryptography;
using NeoFS.API.v2.Refs;
using V2Object = NeoFS.API.v2.Object;
using Neo.Wallets;
using System;
using System.IO;

namespace NeoFS.API.v2.UnitTests.FSClient
{
    [TestClass]
    public class UT_Object
    {
        [TestMethod]
        public void TestObjectPut()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            var payload = new byte[] { 0x01 };
            var obj = new V2Object.Object
            {
                Header = new V2Object.Header
                {
                    Version = Refs.Version.SDKVersion(),
                    ContainerId = cid,
                    PayloadLength = 1,
                    PayloadHash = ByteString.CopyFrom(payload).Sha256Checksum(),
                    ObjectType = V2Object.ObjectType.Regular,
                },
                Payload = ByteString.CopyFrom(payload),
            };
            Console.WriteLine(obj.CalculateAndGetID.ToBase58String());
            var option = new CallOption
            {
                Session = new Session.SessionToken
                {
                    Body = new Session.SessionToken.Types.Body
                    {
                        Id = ByteString.CopyFrom(new Guid().ToByteArray()),
                    }
                },
            };
            var client = new Client.Client(host, key);
            var o = client.PutObject(obj, option).Result;
        }

        [TestMethod]
        public void TestObjectGet()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            var oid = ObjectID.FromBase58String("EUt5jj8B7gjaodazoQgqm5kMS2Yd6gmfcphF4A6Jsdym");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var o = client.GetObject(address).Result;
        }

        [TestMethod]
        public void TestObjectDelete()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            var oid = ObjectID.FromBase58String("EUt5jj8B7gjaodazoQgqm5kMS2Yd6gmfcphF4A6Jsdym");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var o = client.DeleteObject(address);
        }

        [TestMethod]
        public void TestObjectHeaderGet()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            var oid = ObjectID.FromBase58String("EUt5jj8B7gjaodazoQgqm5kMS2Yd6gmfcphF4A6Jsdym");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var o = client.GetObjectHeader(address, false);
        }

        [TestMethod]
        public void TestObjectGetRange()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            var oid = ObjectID.FromBase58String("EUt5jj8B7gjaodazoQgqm5kMS2Yd6gmfcphF4A6Jsdym");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var o = client.GetObjectPayloadRangeData(address, new V2Object.Range { Offset = 0, Length = 1 }).Result;
        }

        [TestMethod]
        public void TestObjectGetRangeHash()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            var oid = ObjectID.FromBase58String("EUt5jj8B7gjaodazoQgqm5kMS2Yd6gmfcphF4A6Jsdym");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var o = client.GetObjectPayloadRangeHash(address, new V2Object.Range[] { new V2Object.Range { Offset = 0, Length = 1 } }, new byte[] { 0x00 }, ChecksumType.Sha256);
        }

        [TestMethod]
        public void TestObjectSearch()
        {
            var host = "localhost:8080";
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var cid = ContainerID.FromBase58String("3qnq7CnrhwnJeEVDmp9MyVG7iRamrZENvZJScp5PDzgC");
            var client = new Client.Client(host, key);
            var o = client.SearchObject(cid, new V2Object.SearchRequest.Types.Body.Types.Filter[0], 0).Result;
        }
    }
}
