

namespace NeoFS.API.v2.Netmap.Normalize
{
    public class ReverseMinNorm : INormalizer
    {
        private double min;

        public ReverseMinNorm(double min)
        {
            this.min = min;
        }

        public double Normalize(double w)
        {
            if (w == 0) return 0;
            return min / w;
        }
    }
}
