using System.Collections.Generic;
using System;
using NeoFS.API.v2.Netmap.Normalize;
using NeoFS.API.v2.Netmap.Aggregator;

namespace NeoFS.API.v2.Netmap
{
    public static class Helper
    {
        private static Func<Node, double> WeightFunc(SigmoidNorm s, ReverseMinNorm r)
        {
            return n =>
            {
                return s.Normalize(n.ID) * r.Normalize(n.ID);
            };
        }

        public static Func<Node, double> GenarateWeightFunc(this List<Node> ns)
        {
            var mean = new MeanAgg();
            var min = new MinAgg();
            for (int i = 0; i < ns.Count; i++)
            {
                mean.Add(ns[i].Capacity);
                min.Add(ns[i].Price);
            }
            return WeightFunc(new SigmoidNorm(mean.Compute()), new ReverseMinNorm(min.Compute()));
        }

        public static ulong Distance(this UInt64 x, UInt64 y)
        {
            var acc = x ^ y;
            acc ^= acc >> 33;
            acc = acc * 0xff51afd7ed558ccd;
            acc ^= acc >> 33;
            acc = acc * 0xc4ceb9fe1a85ec53;
            acc ^= acc >> 33;
            return acc;
        }
    }
}
