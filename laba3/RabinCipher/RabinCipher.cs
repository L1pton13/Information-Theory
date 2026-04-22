using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RabinCipher
{
    public class RabinCipher
    {
        private List<long> BytesToBlock(byte[] bytes)
        {
            List<long> numbs = new List<long>();
            int bytesInBlock = 4;

            for (int i = 0; i < bytes.Length; i += bytesInBlock)
            {
                // Проверка: если вдруг в файле "хвост" меньше 4 байт, не падаем
                if (i + bytesInBlock <= bytes.Length)
                {
                    // Собираем Int32 из 4 байт и приводим к Long
                    int val = BitConverter.ToInt32(bytes, i);
                    numbs.Add((long)(uint)val);
                }
            }
            return numbs;
        }
        public byte[] Encrypt(byte[] data, BigInteger n, BigInteger b)
        {
            List<byte> result = new List<byte>();

            foreach (byte mbyte in data)
            {
                BigInteger m = new BigInteger(mbyte);
                BigInteger c = (m * (m + b)) % n;

                int cipherValue = (int)c;

                //Записываем результат как 4 байта (int32)
                byte[] bytes = BitConverter.GetBytes(cipherValue);

                result.AddRange(bytes);
            }
            return result.ToArray();
        }

        public byte[] Decrypt(byte[] encryptedData, BigInteger p, BigInteger q, BigInteger b, BigInteger n)
        {
            List<byte> result = new List<byte>();
            List<long> numbs = BytesToBlock(encryptedData);

            var euclid = MathHelper.ExtendedEuclid(p, q);
            BigInteger yP = euclid.x;
            BigInteger yQ = euclid.y;

            foreach (long cLong in numbs)
            {
                BigInteger c = new BigInteger(cLong);
                BigInteger D = (b * b + 4 * c) % n;

                // Извлечение квадратных корней
                BigInteger mp = BigInteger.ModPow(D, (p + 1) / 4, p);
                BigInteger mq = BigInteger.ModPow(D, (q + 1) / 4, q);

                // Вычисляем 4 корня по Китайской теореме об остатках
                BigInteger d1 = (yP * p * mq + yQ * q * mp) % n;
                if (d1 < 0) d1 += n;

                BigInteger d2 = n - d1;

                BigInteger d3 = BigInteger.Abs(yP * p * mq - yQ * q * mp) % n;
                if (d3 < 0) d3 += n;

                BigInteger d4 = n - d3;

                // Список корней
                BigInteger[] rootsD = { d1, d4, d3, d2 };

                long finalM = 0;
                foreach (BigInteger d in rootsD)
                {
                    BigInteger diff = (d - b) % n;
                    BigInteger positiveDiff = (diff < 0) ? diff + n : diff;

                    BigInteger m;
                    if (positiveDiff % 2 == 0)
                    {
                        m = (positiveDiff / 2) % n;
                    }
                    else
                    {
                        m = (positiveDiff + n) / 2 % n;
                    }

                    if (m < 256) // Если нашли подходящий байт
                    {
                        finalM = (long)m;
                        break;
                    }
                }
                result.Add((byte)finalM);
            }

            return result.ToArray();
        }
    }
}
