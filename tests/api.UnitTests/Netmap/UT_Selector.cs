using System;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Netmap;
using NeoFS.API.v2.Netmap.Aggregator;
using NeoFS.API.v2.Netmap.Normalize;

namespace NeoFS.API.v2.UnitTests.TestNetmap
{
    public class SelectorTestCase
    {
        public string Name;
        public PlacementPolicy Policy;
        public Type E;
    }

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
            Assert.AreEqual(4, result.Flatten().Length);
        }

        [TestMethod]
        public void TestGetPlacementVectors()
        {
            var p = new PlacementPolicy(2,
                new Replica[]{
                    new Replica(1, "SPB"),
                    new Replica(2, "Americas"),
                },
                new Selector[]{
                    new Selector("SPB", "City", Clause.Same, 1, "SPBSSD"),
                    new Selector("Americas", "City", Clause.Distinct, 2, "Americas"),
                },
                new Filter[]{
                    new Filter("SPBSSD", "", "", Operation.And,
                        new Filter("", "Country", "RU", Operation.Eq),
                        new Filter("", "City", "St.Petersburg", Operation.Eq),
                        new Filter("", "SSD", "1", Operation.Eq)),
                    new Filter("Americas", "", "", Operation.Or,
                        new Filter("", "Continent", "NA", Operation.Eq),
                        new Filter("", "Continent", "SA", Operation.Eq)),
                });
            var nodes = new Node[] {
                Helper.GenerateTestNode(0, ("ID", "1"), ("Country", "RU"), ("City", "St.Petersburg"), ("SSD", "0")),
                Helper.GenerateTestNode(1, ("ID", "2"), ("Country", "RU"), ("City", "St.Petersburg"), ("SSD", "1")),
                Helper.GenerateTestNode(2, ("ID", "3"), ("Country", "RU"), ("City", "Moscow"), ("SSD", "1")),
                Helper.GenerateTestNode(3, ("ID", "4"), ("Country", "RU"), ("City", "Moscow"), ("SSD", "1")),
                Helper.GenerateTestNode(4, ("ID", "5"), ("Country", "RU"), ("City", "St.Petersburg"), ("SSD", "1")),
                Helper.GenerateTestNode(5, ("ID", "6"), ("Continent", "NA"), ("City", "NewYork")),
                Helper.GenerateTestNode(6, ("ID", "7"), ("Continent", "AF"), ("City", "Cairo")),
                Helper.GenerateTestNode(7, ("ID", "8"), ("Continent", "AF"), ("City", "Cairo")),
                Helper.GenerateTestNode(8, ("ID", "9"), ("Continent", "SA"), ("City", "Lima")),
                Helper.GenerateTestNode(9, ("ID", "10"), ("Continent", "AF"), ("City", "Cairo")),
                Helper.GenerateTestNode(10, ("ID", "11"), ("Continent", "NA"), ("City", "NewYork")),
                Helper.GenerateTestNode(11, ("ID", "12"), ("Continent", "NA"), ("City", "LosAngeles")),
                Helper.GenerateTestNode(12, ("ID", "13"), ("Continent", "SA"), ("City", "Lima")),
            };
            var nm = new NetMap(nodes);
            var result = nm.GetContainerNodes(p, null);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(6, result.Flatten().Length);
            Assert.AreEqual(2, result[0].Length);
            foreach (var ni in result[0])
            {
                Assert.AreEqual("RU", ni.Attributes["Country"]);
                Assert.AreEqual("St.Petersburg", ni.Attributes["City"]);
                Assert.AreEqual("1", ni.Attributes["SSD"]);
            }
            Assert.AreEqual(4, result[1].Length);
            foreach (var ni in result[1])
            {
                ni.Attributes["Continent"].Should().BeOneOf("NA", "SA");
            }
        }

        [TestMethod]
        public void TestProcessSelectors()
        {
            var p = new PlacementPolicy(2, null,
                new Selector[]{
                    new Selector("SameRU", "City", Clause.Same, 2, "FromRU"),
                    new Selector("DistinctRU", "City", Clause.Distinct, 2, "FromRU"),
                    new Selector("Good", "Country", Clause.Distinct, 2, "Good"),
                    new Selector("Main", "Country", Clause.Distinct, 3, "*"),
                },
                new Filter[]{
                    new Filter("FromRU", "Country", "Russia", Operation.Eq),
                    new Filter("Good", "Rating", "4", Operation.Ge),
                }
            );
            var nodes = new Node[]{
                Helper.GenerateTestNode(0, ("Country", "Russia"), ("Rating", "1"), ("City", "SPB")),
                Helper.GenerateTestNode(1, ("Country", "Germany"), ("Rating", "5"), ("City", "Berlin")),
                Helper.GenerateTestNode(2, ("Country", "Russia"), ("Rating", "6"), ("City", "Moscow")),
                Helper.GenerateTestNode(3, ("Country", "France"), ("Rating", "4"), ("City", "Paris")),
                Helper.GenerateTestNode(4, ("Country", "France"), ("Rating", "1"), ("City", "Lyon")),
                Helper.GenerateTestNode(5, ("Country", "Russia"), ("Rating", "5"), ("City", "SPB")),
                Helper.GenerateTestNode(6, ("Country", "Russia"), ("Rating", "7"), ("City", "Moscow")),
                Helper.GenerateTestNode(7, ("Country", "Germany"), ("Rating", "3"), ("City", "Darmstadt")),
                Helper.GenerateTestNode(8, ("Country", "Germany"), ("Rating", "7"), ("City", "Frankfurt")),
                Helper.GenerateTestNode(9, ("Country", "Russia"), ("Rating", "9"), ("City", "SPB")),
                Helper.GenerateTestNode(10, ("Country", "Russia"), ("Rating", "9"), ("City", "SPB")),
            };
            var nm = new NetMap(nodes);
            var c = new Context(nm);
            c.ProcessFilters(p);
            c.ProcessSelectors(p);
            foreach (var s in p.Selectors)
            {
                var ns = c.Selections[s.Name];
                var sel = c.Selectors[s.Name];
                var bc = sel.GetBucketCount();
                var nib = sel.GetNodesInBucket(p);
                Assert.AreEqual(bc, ns.Count);
                foreach (var res in ns)
                {
                    Assert.AreEqual(nib, res.Length);
                    foreach (var j in res)
                        Assert.IsTrue(c.ApplyFilter(sel.Filter, j));
                }
            }
        }

        [TestMethod]
        public void TestProcessSelectorsHRW()
        {
            var p = new PlacementPolicy(1, null,
                new Selector[]{
                    new Selector("Main", "Country", Clause.Distinct, 3, "*"),
                },
                null
            );
            var nodes = new Node[]{
                Helper.GenerateTestNode(0,("Country", "Germany"), (Node.PriceAttribute, "2"), (Node.CapacityAttribute, "10000")),
                Helper.GenerateTestNode(1,("Country", "Germany"), (Node.PriceAttribute, "4"), (Node.CapacityAttribute, "1")),
                Helper.GenerateTestNode(2,("Country", "France"), (Node.PriceAttribute, "3"), (Node.CapacityAttribute, "10")),
                Helper.GenerateTestNode(3,("Country", "Russia"), (Node.PriceAttribute, "2"), (Node.CapacityAttribute, "10000")),
                Helper.GenerateTestNode(4,("Country", "Russia"), (Node.PriceAttribute, "1"), (Node.CapacityAttribute, "10000")),
                Helper.GenerateTestNode(5,("Country", "Russia"), (Node.CapacityAttribute, "10000")),
                Helper.GenerateTestNode(6,("Country", "France"), (Node.PriceAttribute, "100"), (Node.CapacityAttribute, "1")),
                Helper.GenerateTestNode(7,("Country", "France"), (Node.PriceAttribute, "7"), (Node.CapacityAttribute, "10000")),
                Helper.GenerateTestNode(8,("Country", "Russia"), (Node.PriceAttribute, "2"), (Node.CapacityAttribute, "1")),

            };
            var nm = new NetMap(nodes);
            var c = new Context(nm);
            c.SetPivot(Encoding.ASCII.GetBytes("containerID"));
            c.SetWeightFunc(Netmap.Helper.WeightFunc(new MaxNorm(10000), new ReverseMinNorm(1)));
            c.SetAggregator(() => new MaxAgg());
            c.ProcessFilters(p);
            c.ProcessSelectors(p);

            var result = c.Selections["Main"];
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(4, result[0][0].Index);
            Assert.AreEqual(0, result[1][0].Index);
            Assert.AreEqual(7, result[2][0].Index);
        }

        [TestMethod]
        public void TestProcessSelectorsInvalid()
        {
            var test_cases = new SelectorTestCase[]
            {
                new SelectorTestCase
                {
                    Name = "InvalidFilterReference",
                    Policy = new PlacementPolicy(1, null,
                        new Selector[]{ new Selector("MyStore", "Country", Clause.Distinct, 1, "FromNL") },
                        new Filter[]{ new Filter("FromRU", "Country", "Russia", Operation.Eq) }
                    ),
                    E = typeof(ArgumentNullException),
                },
                new SelectorTestCase
                {
                    Name = "NotEnoughNodes (backup factor)",
                    Policy = new PlacementPolicy(2, null,
                        new Selector[]{ new Selector("MyStore", "Country", Clause.Distinct, 1, "FromRU") },
                        new Filter[]{ new Filter("FromRU", "Country", "Russia", Operation.Eq) }
                    ),
                    E = typeof(InvalidOperationException),
                },
                new SelectorTestCase
                {
                    Name = "NotEnoughNodes (buckets)",
                    Policy = new PlacementPolicy(1, null,
                        new Selector[]{ new Selector("MyStore", "Country", Clause.Distinct, 2, "FromRU") },
                        new Filter[]{ new Filter("FromRU", "Country", "Russia", Operation.Eq) }
                    ),
                    E = typeof(InvalidOperationException),
                }
            };
            var nodes = new Node[]
            {
                Helper.GenerateTestNode(0, ("Country", "Russia")),
                Helper.GenerateTestNode(0, ("Country", "Germany")),
                Helper.GenerateTestNode(0),
            };
            foreach (var t in test_cases)
            {
                var nm = new NetMap(nodes);
                var c = new Context(nm);
                c.ProcessFilters(t.Policy);
                try
                {
                    c.ProcessSelectors(t.Policy);
                }
                catch (Exception e)
                {
                    Assert.IsInstanceOfType(e, t.E);
                }
            }
        }
    }
}
