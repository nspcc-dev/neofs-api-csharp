

namespace NeoFS.API.v2.Netmap.Aggregator
{
    public class MaxAgg : IAggregator
    {
        private double max;

        public void Add(double n)
        {
            if (max < n)
                max = n;
        }

        public double Compute()
        {
            return max;
        }

        public void Clear()
        {
            max = 0;
        }
    }
}
