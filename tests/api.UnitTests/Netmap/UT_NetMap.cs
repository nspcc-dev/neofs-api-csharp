using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Netmap;
using System;
using System.Collections.Generic;

namespace NeoFS.API.v2.UnitTests.TestNetmap
{
    [TestClass]
    public class UT_NetMap
    {
        [TestMethod]
        public void TestFlattenNodes()
        {
            var ns1 = new Node[] { Helper.GenerateTestNode(0, ("Raing", "1")) };
            var ns2 = new Node[] { Helper.GenerateTestNode(0, ("Raing", "2")) };
            var list = new List<Node[]>();
            list.Add(ns1);
            list.Add(ns2);
            Assert.AreEqual(2, list.Flatten().Length);
        }
    }
}