using System;
using System.Collections.Generic;
using System.Linq;
using NeoFS.API.v2.Cryptography;

namespace NeoFS.API.v2.Netmap
{
    public class NetMap
    {
        public List<Node> Nodes = new List<Node>();

        public NetMap(Node[] ns)
        {
            if (ns is null) return;
            Nodes.AddRange(ns);
        }

        public List<List<Node>> GetPlacementVectors(List<List<Node>> ns, byte[] pivot)
        {
            var h = pivot.Murmur64(0);
            var weightFunc = Nodes.GenarateWeightFunc();
            var results = new List<List<Node>>();
            ns.ForEach(ns =>
            {
                var list = ns.Select(p =>
                {
                    p.Weight = weightFunc(p);
                    p.Distance = p.Hash.Distance(h);
                    return p;
                }).ToList();
                list.Sort((n1, n2) =>
                {
                    var w1 = (double)(~((ulong)0) - n1.Distance) * n1.Weight;
                    var w2 = (double)(~((ulong)0) - n2.Distance) * n2.Weight;
                    return w1.CompareTo(w2);
                });
                results.Add(list);
            });
            return results;
        }

        public List<List<Node>> GetContainerNodes(PlacementPolicy policy, byte[] pivot)
        {
            var context = new Context(this);
            context.SetPivot(pivot);
            context.ProcessFilters(policy);
            context.ProcessSelectors(policy);
            var result = new List<List<Node>>();
            foreach (var replica in policy.Replicas)
            {
                var r = new List<Node>();
                if (replica is null)
                    throw new ArgumentNullException(nameof(GetContainerNodes) + " missing Replicas");
                if (replica.Selector == "")
                    foreach (var selector in policy.Selectors)
                        r = r.Concat(context.Selections[selector.Name].Flatten()).ToList();
                else if (context.Selections.TryGetValue(replica.Selector, out List<List<Node>> ns))
                    r = r.Concat(ns.Flatten()).ToList();
                else
                    throw new InvalidOperationException(nameof(GetContainerNodes) + " selection not found");
                result.Add(r);
            }
            return result;
        }
    }
}
