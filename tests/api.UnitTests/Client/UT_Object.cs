using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo;
using Neo.Cryptography;
using NeoFS.API.v2.Client;
using NeoFS.API.v2.Cryptography;
using NeoFS.API.v2.Refs;
using V2Object = NeoFS.API.v2.Object;
using System;
using System.Text;
using System.Linq;

namespace NeoFS.API.v2.UnitTests.FSClient
{
    [TestClass]
    public class UT_Object
    {
        [TestMethod]
        public void TestObjectPut()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var cid = ContainerID.FromBase58String("Bun3sfMBpnjKc3Tx7SdwrMxyNi8ha8JT3dhuFGvYBRTz");
            var payload = Encoding.ASCII.GetBytes("hello");
            var obj = new V2Object.Object
            {
                Header = new V2Object.Header
                {
                    OwnerId = key.ToOwnerID(),
                    ContainerId = cid,
                },
                Payload = ByteString.CopyFrom(payload),
            };
            var client = new Client.Client(host, key);
            var session = client.CreateSession(ulong.MaxValue);
            var o = client.PutObject(obj, new CallOptions { Ttl = 2, Session = session }).Result;
            Console.WriteLine(o.ToBase58String());
            Assert.AreNotEqual("", o.ToBase58String());
        }

        [TestMethod]
        public void TestObjectGet()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var cid = ContainerID.FromBase58String("Bun3sfMBpnjKc3Tx7SdwrMxyNi8ha8JT3dhuFGvYBRTz");
            var oid = ObjectID.FromBase58String("vWt34r4ddnq61jcPec4rVaXHg7Y7GiEYFmcTB2Qwhtx");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var session = client.CreateSession(ulong.MaxValue);
            var o = client.GetObject(address, new CallOptions { Ttl = 2, Session = session }).Result;
            Assert.AreEqual(oid, o.ObjectId);
        }

        [TestMethod]
        public void TestObjectDelete()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var cid = ContainerID.FromBase58String("Bun3sfMBpnjKc3Tx7SdwrMxyNi8ha8JT3dhuFGvYBRTz");
            var oid = ObjectID.FromBase58String("CnBNgUmXiA5KJeGvMDgUEGiKrbZctjwxT5v3sBYjnmf1");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var session = client.CreateSession(ulong.MaxValue);
            var o = client.DeleteObject(address, new CallOptions { Ttl = 2, Session = session });
            Assert.IsTrue(o);
        }

        [TestMethod]
        public void TestObjectHeaderGet()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var cid = ContainerID.FromBase58String("Bun3sfMBpnjKc3Tx7SdwrMxyNi8ha8JT3dhuFGvYBRTz");
            var oid = ObjectID.FromBase58String("vWt34r4ddnq61jcPec4rVaXHg7Y7GiEYFmcTB2Qwhtx");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var session = client.CreateSession(ulong.MaxValue);
            var o = client.GetObjectHeader(address, false, new CallOptions { Ttl = 2, Session = session });
            Assert.AreEqual(oid, o.ObjectId);
        }

        [TestMethod]
        public void TestObjectGetRange()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var cid = ContainerID.FromBase58String("Bun3sfMBpnjKc3Tx7SdwrMxyNi8ha8JT3dhuFGvYBRTz");
            var oid = ObjectID.FromBase58String("vWt34r4ddnq61jcPec4rVaXHg7Y7GiEYFmcTB2Qwhtx");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var session = client.CreateSession(ulong.MaxValue);
            var o = client.GetObjectPayloadRangeData(address, new V2Object.Range { Offset = 0, Length = 3 }, new CallOptions { Ttl = 2, Session = session }).Result;
            Assert.AreEqual("hel", Encoding.ASCII.GetString(o));
        }

        [TestMethod]
        public void TestObjectGetRangeHash()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var cid = ContainerID.FromBase58String("Bun3sfMBpnjKc3Tx7SdwrMxyNi8ha8JT3dhuFGvYBRTz");
            var oid = ObjectID.FromBase58String("vWt34r4ddnq61jcPec4rVaXHg7Y7GiEYFmcTB2Qwhtx");
            var address = new Address(cid, oid);
            var client = new Client.Client(host, key);
            var session = client.CreateSession(ulong.MaxValue);
            var o = client.GetObjectPayloadRangeHash(address, new V2Object.Range[] { new V2Object.Range { Offset = 0, Length = 3 } }, new byte[] { 0x00 }, ChecksumType.Sha256, new CallOptions { Ttl = 2, Session = session });
            Assert.AreEqual(1, o.Count);
            Assert.AreEqual(Encoding.ASCII.GetBytes("hello")[..3].Sha256().ToHexString(), o[0].ToHexString());
        }

        [TestMethod]
        public void TestObjectSearch()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var cid = ContainerID.FromBase58String("Bun3sfMBpnjKc3Tx7SdwrMxyNi8ha8JT3dhuFGvYBRTz");
            var client = new Client.Client(host, key);
            var session = client.CreateSession(ulong.MaxValue);
            var o = client.SearchObject(cid, new V2Object.SearchRequest.Types.Body.Types.Filter[0], new CallOptions { Ttl = 2, Session = session }).Result;
            Console.WriteLine(o.Count);
            Assert.IsTrue(o.Select(p => p.ToBase58String()).ToList().Contains("vWt34r4ddnq61jcPec4rVaXHg7Y7GiEYFmcTB2Qwhtx"));
        }
    }
}
