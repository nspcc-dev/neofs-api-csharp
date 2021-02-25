using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo;
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
            Assert.AreEqual(25, ownerId.Value.Length);
            Assert.AreEqual(27, ownerId.ToByteArray().Length);
        }

        [TestMethod]
        public void TestPublicKey()
        {
            var key = "02f0476c51a3bfb67e0452618aaa220fa17113d66b457662056a154d32e333b377".HexToBytes().LoadPublicKey();
            Assert.AreEqual("02f0476c51a3bfb67e0452618aaa220fa17113d66b457662056a154d32e333b377", key.PublicKey().ToHexString());
        }

        [TestMethod]
        public void TestWif()
        {
            var key = "L4kWTNckyaWn2QdUrACCJR1qJNgFFGhTCy63ERk7ZK3NvBoXap6t".LoadWif();
            var address = key.ToAddress();
            Assert.AreEqual("NZtdBM2WNB7sWoeesKSjAeFPJea64HUmyd", address);
            key = "L1pBKpw4tR6CogySzye3GUcVPz5pAeemXbyupoWUEVrtfstBfDiN".LoadWif();
            Assert.AreEqual("Nix7r8QFw2MEzR9HSWnJcGPt6ZSj4gAS3V", key.ToAddress());
        }
    }
}
