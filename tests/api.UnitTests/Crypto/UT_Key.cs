using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Crypto;

namespace NeoFS.API.v2.UnitTests.Crypto
{
    [TestClass]
    public class UT_Key
    {
        [TestMethod]
        public void TestImportKey()
        {
            var private_key = "30770201010420347bc2bd9eb7b9f41a217a26dc5a3d2a3c25ece1c8bff1d5a146aaf4156e3436a00a06082a8648ce3d030107a14403420004b3622bf4017bdfe317c58aed5f4c753f206b7db896046fa7d774bbc4bf7f8dc2af9c7b29759df7f3d92052a5b9bc545bcd31c6a7a3463e90c768a6c3e45b1036";
            var key = private_key.FromHex().LoadKey();

            var private_key_exported = key.ExportECPrivateKey();

            Assert.AreEqual(private_key, private_key_exported.ToHex());
        }
    }
}