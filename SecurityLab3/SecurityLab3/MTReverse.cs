using System.Collections.Generic;

namespace SecurityLab3
{
    public class MtReverse
    {
        private const int W = 32;
        private const int N = 624;
        private const int M = 397;
        private const int R = 31;
        private const int U = 11;
        private const int S = 7;
        private const int T = 15;
        private const int L = 18;
        private const long A = 0x9908B0DF;
        private const long B = 0x9D2C5680;
        private const long C = 0xEFC60000;
        
        List<long> used = new();
        
        private long ToUpper(long x)
        {
            return x & ((1L << W) - (1 << R));
        }

        private long ToLower(long x)
        {
            return x & ((1L << R) - 1);
        }

        private long TimesA(long x)
        {
            if ((x & 1) == 1)
            {
                return (L >> 1) ^ A;
            }
            else
            {
                return x >> 1;
            }
        }
        private long UndoXorRightShift(long x, int shift)
        {
            var res = x;

            for (var i = shift; i < W; i += shift)
            {
                res ^= x >> i;
            }

            return res;
        }

        private long UndoXorLeftShiftMask(long x, int shift, long mask)
        {
            var window = (1 << shift) - 1;
            var res = x;

            for (var i = 0; i < W / shift; i++)
            {
                res ^= ((window & res) << shift) & mask;
            }

            return res;
        }

        private long DecreaseInt(long x)
        {
            long res = x;
            res ^= res >> U;
            res ^= (res << S) & B;
            res ^= (res << T) & C;
            res ^= res >> L;

            return res;
        }

        private long IncreaseInt(long x)
        {
            x = UndoXorRightShift(x, L);
            x = UndoXorLeftShiftMask(x, T, C);
            x = UndoXorLeftShiftMask(x, S, B);
            x = UndoXorRightShift(x, U);

            return x;
        }

        public void Init(long[] lst)
        {
            used.Clear();
            foreach (var t in lst)
            {
                used.Add(IncreaseInt(t));
            }
        }

        public long Predict()
        {
            var nextVal = used[N - M] ^ TimesA(
                ToUpper(used[N - 1]) | ToLower(used[N - 2]));
            used.Add(nextVal);
            var predicted = IncreaseInt(nextVal);

            return predicted;
        }
    }
}