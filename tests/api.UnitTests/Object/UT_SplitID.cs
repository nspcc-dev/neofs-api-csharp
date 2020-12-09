

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Object;
using System;

namespace NeoFS.API.v2.UnitTests.TestObject
{
    [TestClass]
    public class UT_SplitID
    {
        [TestMethod]
        public void TestParse()
        {
            var sid = new SplitID();
            var str = sid.ToString();
            var sid1 = new SplitID();
            sid1.Parse(str);
            Assert.AreEqual(sid.ToString(), sid1.ToString());
        }

        [TestMethod]
        public void TestGuid()
        {
            var g = Guid.NewGuid();
            var sid = new SplitID();
            sid.SetGuid(g);
            Assert.AreEqual(g.ToString(), sid.ToString());
        }

        [TestMethod]
        public void TestNull()
        {
            var sid = new SplitID();
            Assert.AreEqual("", sid.ToString());
            Assert.AreEqual(0, sid.ToByteString().Length);
        }
    }
}
