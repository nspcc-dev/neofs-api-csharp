using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoFS.API.v2.Refs;
using NeoFS.API.v2.Object;
using static NeoFS.API.v2.Object.SearchRequest.Types.Body.Types;
using System;

namespace NeoFS.API.v2.UnitTests.TestObject
{
    [TestClass]
    public class UT_Search
    {
        [TestMethod]
        public void TestFilter()
        {
            var sf = new SearchFilters();
            sf.AddFilter("header", "value", MatchType.StringEqual);
            Assert.AreEqual(1, sf.Filters.Length);
        }

        [TestMethod]
        public void TestAddRootFilters()
        {
            var sf = new SearchFilters();
            sf.AddRootFilter();
            var f = sf.Filters[0];

            Assert.AreEqual(MatchType.Unspecified, f.MatchType);
            Assert.AreEqual(Filter.FilterPropertyRoot, f.Name);
            Assert.AreEqual("", f.Value);
        }

        [TestMethod]
        public void TestAddPhyFilters()
        {
            var sf = new SearchFilters();
            sf.AddPhyFilter();
            var f = sf.Filters[0];

            Assert.AreEqual(MatchType.Unspecified, f.MatchType);
            Assert.AreEqual(Filter.FilterPropertyPhy, f.Name);
            Assert.AreEqual("", f.Value);
        }

        [TestMethod]
        public void TestAddParentIDFilters()
        {
            var sf = new SearchFilters();
            var oid = ObjectID.FromBase58String("vWt34r4ddnq61jcPec4rVaXHg7Y7GiEYFmcTB2Qwhtx");
            sf.AddParentIDFilter(oid, MatchType.StringEqual);

            Assert.AreEqual(1, sf.Filters.Length);
            var f = sf.Filters[0];

            Assert.AreEqual(MatchType.StringEqual, f.MatchType);
            Assert.AreEqual(Filter.FilterHeaderParent, f.Name);
            Assert.AreEqual("vWt34r4ddnq61jcPec4rVaXHg7Y7GiEYFmcTB2Qwhtx", f.Value);
        }

        [TestMethod]
        public void TestAddObjectIDFilters()
        {
            var sf = new SearchFilters();
            var oid = ObjectID.FromBase58String("vWt34r4ddnq61jcPec4rVaXHg7Y7GiEYFmcTB2Qwhtx");
            sf.AddObjectIDFilter(oid, MatchType.StringEqual);

            Assert.AreEqual(1, sf.Filters.Length);
            var f = sf.Filters[0];

            Assert.AreEqual(MatchType.StringEqual, f.MatchType);
            Assert.AreEqual(Filter.FilterHeaderObjectID, f.Name);
            Assert.AreEqual("vWt34r4ddnq61jcPec4rVaXHg7Y7GiEYFmcTB2Qwhtx", f.Value);
        }

        [TestMethod]
        public void TestAddSplitIDFilters()
        {
            var sf = new SearchFilters();
            var sid = new SplitID();
            sid.Parse("5dee2659-583f-492f-9ae1-2f5766ccab5c");
            sf.AddSplitIDFilter(sid, MatchType.StringEqual);
            Assert.AreEqual(1, sf.Filters.Length);
            var f = sf.Filters[0];

            Assert.AreEqual(MatchType.StringEqual, f.MatchType);
            Assert.AreEqual(Filter.FilterHeaderSplitID, f.Name);
            Assert.AreEqual("5dee2659-583f-492f-9ae1-2f5766ccab5c", f.Value);
        }
    }
}
