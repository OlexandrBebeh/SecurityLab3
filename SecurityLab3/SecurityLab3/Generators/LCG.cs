namespace SecurityLab3.Generators
{
    public class Lcg
    {
        public const long M = 4294967296; // 2^32
        private long A;
        private long C;
        private long last;
        
        public Lcg(long a, long c, long seed)
        {
            A = a;
            C = c;
            last = seed;
        }
        
        public long Next()
        {
            last = (A * last + C) % M;

            return last;
        }
    }
}