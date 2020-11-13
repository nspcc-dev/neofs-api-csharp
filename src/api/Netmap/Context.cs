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
        public Dictionary<Filter, UInt64> NumCache = new Dictionary<Filter, ulong>();
        private byte[] pivot;
        private UInt64 pivotHash;
        private Func<IAggregator> newAggregator;
        private Func<Node, double> weightFunc;

        public Context(NetMap map)
        {
            Map = map;
            newAggregator = () => new MeanIQRAgg(0);
            weightFunc = map.Nodes.GenarateWeightFunc();
        }

        public void SetPivot(byte[] pivot)
        {
            if (pivot is null || pivot.Length == 0) return;
            this.pivot = pivot;
            pivotHash = pivot.Murmur64(0);
        }

        public void SetWeightFunc(Func<Node, double> f)
        {
            weightFunc = f;
        }

        public void SetAggregator(Func<IAggregator> agg)
        {
            newAggregator = agg;
        }
    }
}
