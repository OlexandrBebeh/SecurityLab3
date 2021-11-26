using System;
using SecurityLab3.Generators;

namespace SecurityLab3
{
    public class GeneratorsHacker
    {
        public LCG FindLCG(long[] seq)
        {
            if (seq.Length < 3)
            {
                throw new InvalidOperationException("Logic error");
            }

            var x0 = seq[0];
            var x1 = seq[1];
            var x2 = seq[2];

            var a = ((x1 - x2) / (x0 - x1)) % LCG.M;
            var b = ((x1 - a * x0)) % LCG.M;

            return new LCG(a, b, x2);
        }
    }
}