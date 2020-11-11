using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Cryptography;
using System;

namespace NeoFS.API.v2.UnitTests.TestCryptography
{
    [TestClass]
    public class UT_Key
    {
        [TestMethod]
        public void TestImportKey()
        {

        }

        [TestMethod]
        public void TestOwnerId()
        {
            var address = "NTfozM1xX7WDKD2LUE5yjtd8FMFYQJoy54";
            var ownerId = address.AddressToOwnerID();
            Assert.AreEqual(25, ownerId.Value.Length);
            var key = "Kwk6k2eC3L3QuPvD8aiaNyoSXgQ2YL1bwS5CP1oKoA9waeAze97s".LoadWif();
            ownerId = key.ToOwnerID();
            Console.WriteLine(ownerId.Value.ToHex());
            Assert.AreEqual(25, ownerId.Value.Length);
            Assert.AreEqual(25, ownerId.ToByteArray().Length);
        }
    }
}