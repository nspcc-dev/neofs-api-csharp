using NeoFS.API.v2.Cryptography;
using System;
using System.Collections.Generic;

namespace NeoFS.API.v2.Netmap
{
    public class Node : IEquatable<Node>, IComparable<Node>
    {
        public const string CapacityAttribute = "Capacity";
        public const string PriceAttribute = "Price";
        public UInt64 ID;
        public UInt64 Capacity;
        public UInt64 Price;
        public int Index;
        public NodeInfo Info;
        public Dictionary<string, string> Attributes = new Dictionary<string, string>();
        public double Weight;
        public UInt64 Distance;

        public UInt64 Hash => ID;
        public string NetworkAddress => Info.Address;
        public byte[] PublicKey => Info.PublicKey.ToByteArray();

        public Node(int index, NodeInfo ni)
        {
            ID = ni.PublicKey.ToByteArray().Murmur64(0);
            Index = index;
            Info = ni;
            foreach (var attr in ni.Attributes)
            {
                if (attr.Key == CapacityAttribute)
                    Capacity = UInt64.Parse(attr.Value);
                else if (attr.Key == PriceAttribute)
                    Price = UInt64.Parse(attr.Value);
                Attributes.Add(attr.Key, attr.Value);
            }
        }

        public bool Equals(Node n)
        {
            return ID == n.ID;
        }

        public int CompareTo(Node n)
        {
            if (n == null)
                return 1;
            else
                return ID.CompareTo(n.ID);
        }
    }
}
