using System;
using System.Collections.Generic;
using System.Linq;

namespace NeoFS.API.v2.Netmap
{
    public partial class Context
    {
        public void ProcessSelectors(PlacementPolicy policy)
        {
            foreach (var selector in policy.Selectors)
            {
                if (selector is null)
                    throw new ArgumentNullException(nameof(ProcessSelectors));
                if (selector.Filter != MainFilterName && !Filters.ContainsKey(selector.Filter))
                    throw new ArgumentNullException(nameof(ProcessSelectors) + " filter not found");
                Selectors[selector.Name] = selector;
                var results = GetSelection(policy, selector);
                Selections[selector.Name] = results;
            }
        }

        public List<List<Node>> GetSelection(PlacementPolicy policy, Selector sel)
        {
            int bucket_count = sel.GetBucketCount();
            int node_in_bucket = sel.GetNodesInBucket(policy);
            var buckets = GetSelectionBase(sel).ToList();
            if (buckets.Count() < bucket_count)
                throw new InvalidOperationException(nameof(GetSelection) + " not enough nodes");
            if (pivot is null)
            {
                if (sel.Attribute == "")
                    buckets.Sort((b1, b2) => b1.Item2[0].ID.CompareTo(b2.Item2[0].ID));
                else
                    buckets.Sort((b1, b2) => b1.Item1.CompareTo(b2.Item1));
            }
            var nodes = new List<List<Node>>();
            foreach (var it in buckets)
            {
                if (node_in_bucket <= it.Item2.Count)
                {
                    nodes.Add(it.Item2.Take(node_in_bucket).ToList());
                }
            }
            if (nodes.Count() < bucket_count)
                throw new InvalidOperationException(nameof(GetSelection) + " not enough nodes");
            if (pivot != null)
            {
                var list = nodes.Select(p =>
                {
                    var agg = newAggregator();
                    foreach (var n in p)
                        agg.Add(weightFunc(n));
                    return (agg.Compute(), p);
                }).ToList();
                list.Sort((i1, i2) => i1.Item1.CompareTo(i2.Item1));
                list.Reverse();
                return list.Take(bucket_count).Select(p => p.p).ToList();
            }
            return nodes.GetRange(0, bucket_count);
        }

        public IEnumerable<(string, List<Node>)> GetSelectionBase(Selector sel)
        {
            Filters.TryGetValue(sel.Filter, out Filter filter);
            if (sel.Attribute == "")
            {
                foreach (var node in Map.Nodes.Where(p => sel.Filter == MainFilterName || Match(filter, p)))
                    yield return ("", new List<Node> { node });
            }
            else
            {
                foreach (var group in Map.Nodes.Where(p => sel.Filter == MainFilterName || Match(filter, p)).GroupBy(p => p.Attributes[sel.Attribute]))
                {
                    if (pivot is null)
                        yield return (group.Key, group.ToList());
                    else
                    {
                        var list = group.Select(p =>
                        {
                            p.Weight = weightFunc(p);
                            p.Distance = p.Hash.Distance(pivotHash);
                            return p;
                        }).ToList();
                        list.Sort((n1, n2) =>
                        {
                            var w1 = (~0u - n1.Distance) * n1.Weight;
                            var w2 = (~0u - n2.Distance) * n2.Weight;
                            return w1.CompareTo(w2);
                        });
                        list.Reverse();
                        yield return (group.Key, list.ToList());
                    }

                }
            }
        }
    }
}
