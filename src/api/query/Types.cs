using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;

namespace NeoFS.API.Query
{
    public sealed partial class Query
    {
        public static Query Parse(IEnumerable<string> query, bool sg = false, bool root = false, bool debug = false)
        {
            var q = new Query();

            if (root)
            {
                if (debug)
                {
                    Console.WriteLine("\nSearch only for user's objects\n");
                }

                q.Filters.Add(new Filter
                {
                    Type = Filter.Types.Type.Exact,
                    Name = Object.Object.KeyRootObject,
                });
            }

            if (sg)
            {
                if (debug)
                {
                    Console.WriteLine("\nSearch only for StorageGroup objects\n");
                }

                q.Filters.Add(new Filter
                {
                    Type = Filter.Types.Type.Exact,
                    Name = Object.Object.KeyStorageGroup,
                });
            }

            if (query.Count() > 0)
            {
                if (debug)
                {
                    Console.WriteLine("\nSearch by queries:\n{0}", string.Join("\n", query));
                }

                foreach (var item in query)
                {
                    var kv = item.Split("=");

                    if (kv.Length != 2)
                    {
                        throw new Exception(
                            string.Format("Expect query 'key=value', received '{0}'", item));
                    }

                    q.Filters.Add(new Filter
                    {
                        Type = Filter.Types.Type.Regex,
                        Name = kv[0],
                        Value = kv[1],
                    });
                }
            }


            return q;
        }
    }
}
