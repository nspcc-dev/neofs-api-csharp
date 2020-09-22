using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Crypto;
using System.Security.Cryptography;
using Neo.Wallets;
using System;

namespace NeoFS.API.v2.UnitTests.Refs
{
    [TestClass]
    public class UT_Refs
    {
        [TestMethod]
        public void TestObjectID()
        {
            var id = ObjectID.FromByteArray("a9aa4468861473c86e3d2db9d426e37e5858e015a678f7e6a94a12da3569c8b0".FromHex());
            Console.WriteLine(id);
        }
    }
}