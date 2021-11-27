using System;
using SecurityLab3.Generators;
using Org.BouncyCastle.Math;

namespace SecurityLab3
{
    public class GeneratorsHacker
    {
        public static Lcg FindLcg(long[] seq)
        {
            if (seq.Length < 3)
            {
                throw new InvalidOperationException("Logic error");
            }

            bool n = false;
            var x0 = seq[0];
            var x1 = seq[1];
            var x2 = seq[2];
            var v1 = x0 - x1;
            if (v1 < 0)
            {
                v1 = -v1;
                n = true;
            }

            var v2 = new BigInteger($"{v1}").ModInverse(new BigInteger($"{Lcg.M}")).LongValue;
            var a = (x1 - x2) * v2 % Lcg.M;
            if (n)
            {
                a = -a;
            }
            
            var b = (x1 - a * x0) % Lcg.M;
            
            return new Lcg(a, b, x2);
        }
    }
}