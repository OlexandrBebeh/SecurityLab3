namespace SecurityLab3.Generators
{
    public class Mt
    {
        private const int W = 32;
        private const int N = 624;
        private const int M = 397;

        private const int U = 11;
        private const int S = 7;
        private const int T = 15;
        private const int L = 18;
        private const int F = 1812433253;

        private const long A = 0x9908B0DF;
        private const long B = 0x9D2C5680;
        private const long C = 0xEFC60000;
        private const long D = 0xFFFFFFFF;

        private const long LowerBound = 0x7FFFFFFF;
        private const long UpperBound = 0x80000000;

        private long[] sequence;
        private int index;

        private long seed;

        public void MersenneTwisterRandomizer(long[] array)
        {
            index = 0;
            sequence = array;
            Twist();
        }

        public void MersenneTwisterRandomizer(long s)
        {
            seed = s;
            index = N + 1;
            sequence = new long[N];
            sequence[0] = s;

            for (var i = 1; i < N; i++)
            {
                sequence[i] = (F * (sequence[i - 1] ^ (sequence[i - 1] >> (W - 2))) + i) & 0xffffffff;
            }
        }

        public long Next()
        {
            if (index >= N)
            {
                Twist();
                index = 0;
            }

            var result = sequence[index];
            result ^= (result >> U) & D;
            result ^= (result << S) & B;
            result ^= (result << T) & C;
            result ^= result >> L;

            index++;
            return result;
        }

        private void Twist()
        {
            for (var i = 0; i < N; i++)
            {
                var x = sequence[i] & UpperBound + sequence[(i + 1) % N] & LowerBound;
                var x1 = x >> 1;
                if (x % 2 != 0)
                {
                    x1 ^= A;
                }

                sequence[i] = sequence[(i + M) % N] ^ x1;
            }
        }
    }
}