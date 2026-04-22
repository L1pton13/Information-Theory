using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RabinCipher
{
    public static class MathHelper
    {
        public static BigInteger FastPower(BigInteger a, BigInteger z, BigInteger n)
        {
            BigInteger a1 = a;
            BigInteger z1 = z;
            BigInteger x = 1;

            while (z1 != 0)
            {
                while (z1 % 2 == 0)
                {
                    z1 = z1 / 2;
                    a1 = (a1 * a1) % n;
                }

                z1-=1;
                x = (x * a1) % n; 
            }

            return x;
        }

        public static (BigInteger x, BigInteger y, BigInteger d) ExtendedEuclid (BigInteger a, BigInteger b)
        {
            BigInteger d0 = a, d1 = b;
            BigInteger x0 = 1, x1 = 0;
            BigInteger y0 = 0, y1 = 1;

            while (d1 > 1)
            {
                BigInteger q = d0 / d1;
                BigInteger d2 =  d0 % d1;
                BigInteger x2 = x0 - q * x1;
                BigInteger y2 = y0 - q * y1;

                d0 = d1; d1 = d2;
                x0 = x1; x1 = x2;
                y0 = y1; y1 = y2;
            }

            return (x1, y1, d1);
        }

        public static bool IsPrime(BigInteger n)
        {
            if (n<2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0 || n % 3 == 0) return false;

            for (BigInteger i = 5; i*i <= n; i+=6)
            {
                if (n % i == 0 || n % (i + 2) == 0) return false;
            }
            return true;
        }

        public static bool IsValidRabinPrime(BigInteger n)
        {
            return IsPrime(n) && (n % 4 == 3);
        }
    }
}
