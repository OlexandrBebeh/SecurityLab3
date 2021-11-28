using System;
using System.Collections.Generic;
using System.Reflection;

namespace SecurityLab3
{
    public class MtState
    {
        public static readonly int N = 624;

        private const int W = 32;
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
        
        private static long ToUpper(long x)
        {
            return x & ((1L << W) - (1L << R));
        }

        private static long ToLower(long x)
        {
            return x & ((1L << R) - 1);
        }

        private static long TimesA(long x)
        {
            if ((x & 1) == 1)
            {
                return (x >> 1) ^ A;
            }
            
            return x >> 1;
        }
        private static long UndoXorRightShift(long x, int shift)
        {
            var res = x;

            for (var i = shift; i < W; i += shift)
            {
                res ^= x >> i;
            }

            return res;
        }

        private static long UndoXorLeftShiftMask(long x, int shift, long mask)
        {
            var window = (1 << shift) - 1;
            var res = x;

            for (var i = 0; i < W / shift; i++)
            {
                res ^= ((window & res) << shift) & mask;
                window <<= shift;
            }

            return res;
        }

        private static long IncreaseLong(long x)
        {
            long res = x;
            res ^= res >> U;
            res ^= (res << S) & B;
            res ^= (res << T) & C;
            res ^= res >> L;

            return res;
        }

        private static long DecreaseLong(long x)
        {
            x = UndoXorRightShift(x, L);
            x = UndoXorLeftShiftMask(x, T, C);
            x = UndoXorLeftShiftMask(x, S, B);
            x = UndoXorRightShift(x, U);

            return x;
        }

        public void Init(long[] lst)
        {
            if (lst.Length < N)
            {
                new InvalidOperationException("Logic error");
            }
            used.Clear();
            for (var i = 0; i < N; i++)
            {
                used.Add(DecreaseLong(lst[i]));
            }
        }

        public long Predict()
        {
            var nextVal = used[M] ^ TimesA(ToUpper(used[0]) | ToLower(used[1]));
            used.Add(nextVal);
            var predicted = IncreaseLong(nextVal);
            used.Remove(used[0]);

            return predicted;
        }
    }
}