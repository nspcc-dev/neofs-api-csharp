using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo;
using NeoFS.API.v2.Cryptography;
using System;

namespace NeoFS.API.v2.UnitTests.FSClient
{
    [TestClass]
    public class UT_Session
    {
        [TestMethod]
        public void TestSessionCreate()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var client = new Client.Client(host, key);
            var token = client.CreateSession(ulong.MaxValue);
            Assert.AreEqual(key.ToOwnerID(), token.Body.OwnerId);
            Console.WriteLine($"id={token.Body.Id.ToUUID()}, key={token.Body.SessionKey.ToByteArray().ToHexString()}");
        }
    }
}
