using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Crypto;
using System.Security.Cryptography;
using Neo.Wallets;
using System;

namespace NeoFS.API.v2.UnitTests.Crypto
{
    [TestClass]
    public class UT_Key
    {
        [TestMethod]
        public void TestImportKey()
        {
            var private_key = "Kwk6k2eC3L3QuPvD8aiaNyoSXgQ2YL1bwS5CP1oKoA9waeAze97s";
            var key = private_key.LoadWif();
            Console.WriteLine($"{key.ExportECPrivateKey().ToHex()}");
            var kp = new KeyPair(key.ExportECPrivateKey());
            Assert.AreEqual(kp.PublicKey.EncodePoint(true).ToHex(), key.PublicKey().ToHex());
        }
    }
}