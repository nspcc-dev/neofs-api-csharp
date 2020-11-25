using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Client;
using NeoFS.API.v2.Cryptography;
using System.Security.Cryptography;
using Neo.Wallets;
using System;
using System.IO;

namespace NeoFS.API.v2.UnitTests.Client
{
    [TestClass]
    public class UT_Object
    {
        [TestMethod]
        public async void TestObjectPut1()
        {
            var file = File.Open("object.txt", FileMode.Create);
            var client = new NeoFS.API.v2.Client.Client(null, null);
            var result = await client.PutObject(null, 0);
        }
    }
}