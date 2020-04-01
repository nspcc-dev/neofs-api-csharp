using System;
using System.Linq;

namespace Netmap
{
    public static class SelectorExtension
    {
        public static string Stringify(this PlacementRule rule)
        {
            return string.Format(
                "RF {0} {1}",
                rule.ReplFactor,
                string.Join(
                    ", ",
                    rule.SFGroups
                        .Select(s => s.Stringify())));
        }

        public static string Stringify(this SFGroup group)
        {
            string[] items = new string[group.Filters.Count + group.Selectors.Count];

            for (var i = 0; i < group.Selectors.Count; i++)
            {
                var s = group.Selectors[i];
                items[i] = string.Format("SELECT {0} {1}", s.Count, s.Key);
            }

            var off = group.Selectors.Count;

            for (var i = 0; i < group.Filters.Count; i++)
            {
                var f = group.Filters[i];
                items[i + off] = string.Format("FILTER {0} {1} {2}",
                    f.Key,
                    f.F.Op.ToString(),
                    f.F.Value);
            }

            return string.Join(", ", items);
        }
    }
}
