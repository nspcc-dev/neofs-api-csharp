using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Cryptography;
using System;

namespace NeoFS.API.v2.UnitTests.FSClient
{
    [TestClass]
    public class UT_Netmap
    {
        [TestMethod]
        public void TestLocalNodeInfo()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var client = new Client.Client(host, key);
            var ni = client.LocalNodeInfo();
            Console.WriteLine(ni.ToString());
        }

        [TestMethod]
        public void TestEpoch()
        {
            var host = "localhost:8080";
            var key = "KxDgvEKzgSBPPfuVfw67oPQBSjidEiqTHURKSDL1R7yGaGYAeYnr".LoadWif();
            var client = new Client.Client(host, key);
            var epoch = client.Epoch();
            Console.WriteLine(epoch);
        }
    }
}
