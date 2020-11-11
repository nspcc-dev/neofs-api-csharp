using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Netmap;

namespace NeoFS.API.v2.UnitTests.TestNetmap
{
    [TestClass]
    public class UT_Selector
    {
        [TestMethod]
        public void TestUnspecifiedClause()
        {
            var p = new PlacementPolicy(1, new Replica[] { new Replica(1, "X") }, new Selector[] { new Selector("X", "", Clause.Distinct, 4, "*") }, null);
            var nodes = new Node[]
            {
                Helper.GenerateTestNode(0, ("ID", "1"), ("Country", "RU"), ("City", "St.Petersburg"), ("SSD", "0")),
                Helper.GenerateTestNode(1, ("ID", "2"), ("Country", "RU"), ("City", "St.Petersburg"), ("SSD", "1")),
                Helper.GenerateTestNode(2, ("ID", "3"), ("Country", "RU"), ("City", "Moscow"), ("SSD", "1")),
                Helper.GenerateTestNode(3, ("ID", "4"), ("Country", "RU"), ("City", "Moscow"), ("SSD", "1")),
            };
            var nm = new NetMap(nodes);
            var result = nm.GetContainerNodes(p, null);
            Assert.AreEqual(4, result.Count);
        }
    }
}
