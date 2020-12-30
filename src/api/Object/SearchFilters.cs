using NeoFS.API.v2.Refs;
using static NeoFS.API.v2.Object.SearchRequest.Types.Body.Types;
using System.Collections.Generic;
using System.Linq;

namespace NeoFS.API.v2.Object
{
    public class SearchFilters
    {
        private readonly List<Filter> filters = new List<Filter>();

        public Filter[] Filters => filters.ToArray();

        public SearchFilters() { }

        public SearchFilters(IEnumerable<Filter> fs)
        {
            filters = fs.ToList();
        }

        public void AddFilter(string name, string value, MatchType op)
        {
            filters.Add(new Filter
            {
                Key = name,
                Value = value,
                MatchType = op,
            });
        }

        public void AddObjectVersionFilter(Version v, MatchType op)
        {
            AddFilter(Filter.FilterHeaderVersion, v.Major + "." + v.Minor, op);
        }

        public void AddObjectContainerIDFilter(ContainerID cid, MatchType op)
        {
            AddFilter(Filter.FilterHeaderContainerID, cid.ToBase58String(), op);
        }

        public void AddObjectOwnerIDFilter(OwnerID oid, MatchType op)
        {
            AddFilter(Filter.FilterHeaderOwnerID, oid.ToBase58String(), op);
        }

        public void AddRootFilter()
        {
            AddFilter(Filter.FilterPropertyRoot, "", MatchType.Unspecified);
        }

        public void AddPhyFilter()
        {
            AddFilter(Filter.FilterPropertyPhy, "", MatchType.Unspecified);
        }

        public void AddParentIDFilter(ObjectID oid, MatchType op)
        {
            AddFilter(Filter.FilterHeaderParent, oid.ToBase58String(), op);
        }

        public void AddObjectIDFilter(ObjectID oid, MatchType op)
        {
            AddFilter(Filter.FilterHeaderObjectID, oid.ToBase58String(), op);
        }

        public void AddSplitIDFilter(SplitID sid, MatchType op)
        {
            AddFilter(Filter.FilterHeaderSplitID, sid.ToString(), op);
        }

        public void AddTypeFilter(byte typ, MatchType op)
        {
            AddFilter(Filter.FilterHeaderObjectType, typ.ToString(), op);
        }
    }
}
