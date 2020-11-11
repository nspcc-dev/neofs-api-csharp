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

        public List<Node[]> GetSelection(PlacementPolicy policy, Selector sel)
        {
            int bucket_count = sel.Clause == Clause.Same ? 1 : (int)sel.Count;
            int node_in_bucket = sel.Clause == Clause.Same ? (int)(policy.ContainerBackupFactor * sel.Count) : (int)policy.ContainerBackupFactor;
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
            var nodes = new List<Node[]>();
            foreach (var it in buckets)
            {
                if (node_in_bucket < it.Item2.Length)
                {
                    nodes.Add(it.Item2[..node_in_bucket]);
                }
            }
            if (nodes.Count() < bucket_count)
                throw new InvalidOperationException(nameof(GetSelection) + " not enough nodes");
            if (pivot != null)
            {
                var list = nodes.Select(p =>
                {
                    foreach (var n in p)
                        aggregator.Add(weightFunc(n));
                    return (aggregator.Compute(), p);
                }).ToList();
                list.Sort((i1, i2) => i1.CompareTo(i2));
                return list.Take(bucket_count).Select(p => p.p).ToList();
            }
            return nodes.Take(bucket_count).ToList();
        }

        public IEnumerable<(string, Node[])> GetSelectionBase(Selector sel)
        {
            var filter = Filters[sel.Filter];
            if (sel.Attribute == "")
            {
                foreach (var node in Map.Nodes.Where(p => sel.Filter == MainFilterName || Match(filter, p)))
                    yield return ("", new Node[] { node });
            }
            else
            {
                foreach (var group in Map.Nodes.Where(p => sel.Filter == MainFilterName || Match(filter, p)).GroupBy(p => p.Attributes[sel.Attribute]))
                {
                    if (pivot is null)
                        yield return (group.Key, group.ToArray());
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
                            var w1 = (double)(~((ulong)0) - n1.Distance) * n1.Weight;
                            var w2 = (double)(~((ulong)0) - n2.Distance) * n2.Weight;
                            return w1.CompareTo(w2);
                        });
                        yield return (group.Key, list.ToArray());
                    }

                }
            }
        }
    }
}
