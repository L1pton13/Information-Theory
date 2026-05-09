using System.Numerics;

namespace DSA
{
    public static class DSAMath
    {
        // Быстрое возведение в степень по модулю
        public static BigInteger FastModPow(BigInteger b, BigInteger e, BigInteger m)
        {
            BigInteger res = 1;
            b %= m;
            while (e > 0)
            {
                if (e % 2 == 1) res = (res * b) % m;
                b = (b * b) % m;
                e /= 2;
            }
            return res;
        }

        // Обратный элемент по малой теореме Ферма: a^(q-2) mod q
        public static BigInteger ModInverse(BigInteger n, BigInteger mod)
        {
            return FastModPow(n, mod - 2, mod);
        }

        // Проверка числа на простоту
        public static bool IsPrime(BigInteger n)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0) return false;
            for (BigInteger i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        // Вычисление хеш-образа сообщения по формуле 3.2 (по модулю q)
        // H_i = (H_{i-1} + M_i)^2 mod q
        public static BigInteger ComputeHash(int[] asciiCodes, BigInteger h0, BigInteger mod)
        {
            BigInteger H = h0;
            foreach (var code in asciiCodes)
            {
                H = BigInteger.Pow(H + code, 2) % mod;
            }
            return H;
        }

        // Вычисление g = h^((p-1)/q) mod p
        public static BigInteger ComputeG(BigInteger h, BigInteger p, BigInteger q)
        {
            BigInteger exp = (p - 1) / q;
            return FastModPow(h, exp, p);
        }

        // Вычисление y = g^x mod p (открытый ключ)
        public static BigInteger ComputeY(BigInteger g, BigInteger x, BigInteger p)
        {
            return FastModPow(g, x, p);
        }

        // Вычисление подписи DSA: r и s
        // r = (g^k mod p) mod q
        // s = k^(-1) * (h(M) + x*r) mod q
        public static (BigInteger r, BigInteger s) ComputeSignature(
            BigInteger g, BigInteger p, BigInteger q,
            BigInteger x, BigInteger k, BigInteger hashM)
        {
            BigInteger r = FastModPow(g, k, p) % q;
            BigInteger kInv = ModInverse(k, q); // k^(q-2) mod q
            BigInteger s = (kInv * (hashM + x * r)) % q;
            return (r, s);
        }

        // Проверка подписи DSA
        // w = s^(-1) mod q
        // u1 = h(M)*w mod q
        // u2 = r*w mod q
        // v = (g^u1 * y^u2 mod p) mod q
        // подпись верна если v == r
        public static (BigInteger w, BigInteger u1, BigInteger u2, BigInteger v) VerifySignature(
            BigInteger g, BigInteger p, BigInteger q,
            BigInteger y, BigInteger r, BigInteger s, BigInteger hashM)
        {
            BigInteger w = ModInverse(s, q);
            BigInteger u1 = (hashM * w) % q;
            BigInteger u2 = (r * w) % q;
            BigInteger gu1 = FastModPow(g, u1, p);
            BigInteger yu2 = FastModPow(y, u2, p);
            BigInteger v = ((gu1 * yu2) % p) % q;
            return (w, u1, u2, v);
        }
    }
}
