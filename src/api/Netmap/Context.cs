using NeoFS.API.v2.Netmap.Aggregator;
using NeoFS.API.v2.Cryptography;
using System;
using System.Collections.Generic;

namespace NeoFS.API.v2.Netmap
{
    public partial class Context
    {
        public NetMap Map;
        public Dictionary<string, Filter> Filters = new Dictionary<string, Filter>();
        public Dictionary<string, Selector> Selectors = new Dictionary<string, Selector>();
        public Dictionary<string, List<Node[]>> Selections = new Dictionary<string, List<Node[]>>();
        private Dictionary<Filter, UInt64> numCache = new Dictionary<Filter, ulong>();
        private byte[] pivot;
        private UInt64 pivotHash;
        private IAggregator aggregator;
        private Func<Node, double> weightFunc;

        public Context(NetMap map)
        {
            Map = map;
            aggregator = new MeanIQRAgg(0);
            weightFunc = map.Nodes.GenarateWeightFunc();
        }
        public void SetPivot(byte[] pivot)
        {
            if (pivot is null || pivot.Length == 0) return;
            this.pivot = pivot;
            pivotHash = pivot.Murmur64(0);
        }
    }
}
