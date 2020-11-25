using System;

namespace NeoFS.API.v2.Netmap
{
    public partial class Context
    {
        private const string MainFilterName = "*";

        public bool ApplyFilter(string name, Node n)
        {
            return name == MainFilterName || Match(Filters[name], n);
        }

        public void ProcessFilters(PlacementPolicy policy)
        {
            foreach (var filter in policy.Filters)
            {
                ProcessFilter(filter, true);
            }
        }

        private void ProcessFilter(Filter filter, bool top)
        {
            if (filter is null) throw new ArgumentNullException(nameof(ProcessFilter));
            if (filter.Name == MainFilterName) throw new ArgumentException(nameof(ProcessFilter) + " '*' is reversed");
            if (top && filter.Name == "") throw new ArgumentException(nameof(ProcessFilter) + " filters on top level must be named");
            if (!top && filter.Name != "" && !Filters.ContainsKey(filter.Name))
                throw new ArgumentException(nameof(ProcessFilter) + " filter not found");
            switch (filter.Op)
            {
                case Operation.And:
                case Operation.Or:
                    {
                        foreach (var fl in filter.Filters)
                        {
                            ProcessFilter(fl, false);
                        }
                        break;
                    }
                default:
                    {
                        if (0 < filter.Filters.Count) throw new ArgumentException(nameof(ProcessFilter) + " simple filter must not contain sub filters");
                        if (!top && filter.Name != "") return;
                        switch (filter.Op)
                        {
                            case Operation.Eq:
                            case Operation.Ne:
                                break;
                            case Operation.Gt:
                            case Operation.Ge:
                            case Operation.Lt:
                            case Operation.Le:
                                {
                                    if (!UInt64.TryParse(filter.Value, out UInt64 n))
                                        throw new ArgumentException(nameof(ProcessFilter) + " invalid number");
                                    NumCache[filter] = n;
                                    break;
                                }
                            default:
                                throw new InvalidOperationException(nameof(ProcessFilter));
                        }
                        break;
                    }
            }
            if (top)
                Filters[filter.Name] = filter;
        }

        private bool MatchKeyValue(Filter filter, Node n)
        {
            switch (filter.Op)
            {
                case Operation.Eq:
                    if (n.Attributes.TryGetValue(filter.Key, out string value))
                    {
                        return value == filter.Value;
                    }
                    return false;
                case Operation.Ne:
                    if (n.Attributes.TryGetValue(filter.Key, out value))
                    {
                        return value != filter.Value;
                    }
                    return true;
                default:
                    {
                        UInt64 attribute;
                        switch (filter.Key)
                        {
                            case Node.PriceAttribute:
                                attribute = n.Price;
                                break;
                            case Node.CapacityAttribute:
                                attribute = n.Capacity;
                                break;
                            default:
                                {
                                    if (!n.Attributes.ContainsKey(filter.Key))
                                        return false;
                                    if (!UInt64.TryParse(n.Attributes[filter.Key], out attribute))
                                        return false;
                                    break;
                                }
                        }
                        switch (filter.Op)
                        {
                            case Operation.Gt:
                                return NumCache[filter] < attribute;
                            case Operation.Ge:
                                return NumCache[filter] <= attribute;
                            case Operation.Lt:
                                return attribute < NumCache[filter];
                            case Operation.Le:
                                return attribute <= NumCache[filter];
                        }
                        break;
                    }
            }
            return true;
        }

        public bool Match(Filter filter, Node n)
        {
            switch (filter.Op)
            {
                case Operation.And:
                case Operation.Or:
                    {
                        foreach (var fl in filter.Filters)
                        {
                            Filter f = fl;
                            if (fl.Name != "")
                                f = Filters[fl.Name];
                            var r = Match(f, n);
                            if (r == (filter.Op == Operation.Or))
                                return r;
                        }
                        return filter.Op == Operation.And;
                    }
                default:
                    return MatchKeyValue(filter, n);
            }
        }
    }
}
