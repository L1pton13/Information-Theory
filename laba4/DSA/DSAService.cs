using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace DSA
{
    public static class DSAService
    {
        // Получить ASCII-коды символов файла
        public static int[] GetAsciiCodes(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            return bytes.Select(b => (int)b).ToArray();
        }

        // Получить ASCII-коды из строки (для пустого файла вернёт пустой массив)
        public static int[] GetAsciiCodesFromBytes(byte[] bytes)
        {
            return bytes.Select(b => (int)b).ToArray();
        }

        // Отобразить содержимое файла как коды ASCII (десятичные), сгруппированные
        public static string GetFileDisplayText(byte[] bytes)
        {
            if (bytes.Length == 0) return "[Пустой файл]";
            var sb = new StringBuilder();
            // Показываем исходный текст
            sb.AppendLine("=== Содержимое файла (текст) ===");
            try
            {
                sb.AppendLine(Encoding.UTF8.GetString(bytes));
            }
            catch
            {
                sb.AppendLine("[Бинарный файл]");
            }
            sb.AppendLine();
            sb.AppendLine("=== ASCII-коды символов (десятичная) ===");
            sb.AppendLine(string.Join(" ", bytes.Select(b => b.ToString())));
            return sb.ToString();
        }

        // Формирует шаги вычисления хэша
        public static string ComputeHashWithSteps(int[] codes, BigInteger h0, BigInteger mod, out BigInteger hashResult)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"=== ХЕШИРОВАНИЕ (формула 3.2) ===");
            sb.AppendLine($"Hᵢ = (Hᵢ₋₁ + Mᵢ)² mod q,  q = {mod}");
            sb.AppendLine();
            sb.AppendLine($"H₀ = {h0}");

            if (codes.Length == 0)
            {
                sb.AppendLine();
                sb.AppendLine("Файл пустой — хеш равен начальному значению H₀.");
                hashResult = h0;
                sb.AppendLine($"H = {hashResult}");
                return sb.ToString();
            }

            BigInteger H = h0;
            for (int i = 0; i < codes.Length; i++)
            {
                BigInteger prev = H;
                BigInteger sum = prev + codes[i];
                BigInteger sq = sum * sum;
                H = sq % mod;
                sb.AppendLine($"H{i + 1} = ({prev} + {codes[i]})² mod {mod} = {sq} mod {mod} = {H}");
            }

            hashResult = H;
            sb.AppendLine();
            sb.AppendLine($"Хеш-образ сообщения: H = H{codes.Length} = {hashResult}");
            return sb.ToString();
        }

        // Формирует шаги вычисления подписи
        public static string ComputeSignatureWithSteps(
            BigInteger g, BigInteger p, BigInteger q,
            BigInteger x, BigInteger k, BigInteger hashM,
            out BigInteger r, out BigInteger s)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== ВЫЧИСЛЕНИЕ ЭЦП (DSA) ===");
            sb.AppendLine();

            BigInteger gk_mod_p = DSAMath.FastModPow(g, k, p);
            r = gk_mod_p % q;
            sb.AppendLine($"r = (g^k mod p) mod q");
            sb.AppendLine($"r = ({g}^{k} mod {p}) mod {q}");
            sb.AppendLine($"r = {gk_mod_p} mod {q} = {r}");
            sb.AppendLine();

            BigInteger kInv = DSAMath.ModInverse(k, q);
            BigInteger inner = (hashM + x * r) % q;
            s = (kInv * (hashM + x * r)) % q;
            sb.AppendLine($"k⁻¹ mod q = k^(q-2) mod q = {k}^{q - 2} mod {q} = {kInv}");
            sb.AppendLine($"s = k⁻¹ * (h(M) + x*r) mod q");
            sb.AppendLine($"s = {kInv} * ({hashM} + {x}*{r}) mod {q}");
            sb.AppendLine($"s = {kInv} * {hashM + x * r} mod {q} = {s}");

            return sb.ToString();
        }

        // Формирует результат верификации
        public static string VerifyWithSteps(
            BigInteger g, BigInteger p, BigInteger q,
            BigInteger y, BigInteger r, BigInteger s,
            BigInteger hashM, out bool isValid)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== ПРОВЕРКА ЭЦП (DSA) ===");
            sb.AppendLine();

            var (w, u1, u2, v) = DSAMath.VerifySignature(g, p, q, y, r, s, hashM);

            sb.AppendLine($"w = s⁻¹ mod q = s^(q-2) mod q = {s}^{q - 2} mod {q} = {w}");
            sb.AppendLine();
            sb.AppendLine($"u₁ = h(M) * w mod q = {hashM} * {w} mod {q} = {u1}");
            sb.AppendLine();
            sb.AppendLine($"u₂ = r * w mod q = {r} * {w} mod {q} = {u2}");
            sb.AppendLine();
            sb.AppendLine($"v = (g^u₁ * y^u₂ mod p) mod q");
            BigInteger gu1 = DSAMath.FastModPow(g, u1, p);
            BigInteger yu2 = DSAMath.FastModPow(y, u2, p);
            sb.AppendLine($"  g^u₁ mod p = {g}^{u1} mod {p} = {gu1}");
            sb.AppendLine($"  y^u₂ mod p = {y}^{u2} mod {p} = {yu2}");
            sb.AppendLine($"  ({gu1} * {yu2} mod {p}) mod {q} = {v}");
            sb.AppendLine();

            isValid = (v == r);
            if (isValid)
            {
                sb.AppendLine($"✔ v = r ({v} = {r}) — подпись ВЕРНА");
            }
            else
            {
                sb.AppendLine($"✘ v ≠ r ({v} ≠ {r}) — подпись НЕВЕРНА");
                sb.AppendLine($"  Хеш из подписи r={r}, вычисленный хеш сообщения привёл к v={v}");
            }

            return sb.ToString();
        }

        // Проверяет, похож ли файл на текстовый (нет нулевых байтов, большинство — ASCII/UTF8)
        public static bool IsLikelyText(byte[] bytes)
        {
            if (bytes.Length == 0) return true;
            int checkLen = Math.Min(bytes.Length, 512);
            int nonText = 0;
            for (int i = 0; i < checkLen; i++)
            {
                if (bytes[i] == 0) return false; // нулевой байт — точно бинарный
                if (bytes[i] < 9 || (bytes[i] > 13 && bytes[i] < 32)) nonText++;
            }
            return nonText < checkLen * 0.1;
        }

        // Создать подписанный файл как байты: оригинальные байты + ASCII-суффикс " r s"
        public static byte[] BuildSignedBytes(byte[] originalBytes, BigInteger r, BigInteger s)
        {
            byte[] suffix = Encoding.ASCII.GetBytes(" " + r.ToString() + " " + s.ToString());
            byte[] result = new byte[originalBytes.Length + suffix.Length];
            Array.Copy(originalBytes, result, originalBytes.Length);
            Array.Copy(suffix, 0, result, originalBytes.Length, suffix.Length);
            return result;
        }

        // Читает подпись из подписанного файла (работает с байтами)
        // Ищем " r s" в конце — два числа через пробел, добавленных нами
        public static bool TryParseSignedBytes(byte[] fileBytes, out byte[] originalBytes, out BigInteger r, out BigInteger s)
        {
            r = 0; s = 0; originalBytes = new byte[0];

            // Читаем с конца: ищем последние два числа через пробел
            // Конвертируем хвост файла в строку для парсинга (подпись всегда ASCII)
            int tailLen = Math.Min(fileBytes.Length, 100); // подпись не может быть длиннее ~100 байт
            string tail = Encoding.ASCII.GetString(fileBytes, fileBytes.Length - tailLen, tailLen);

            // Парсим с конца: последний токен — s, предпоследний — r
            string trimmedTail = tail.TrimEnd('\r', '\n', ' ');
            int lastSpace = trimmedTail.LastIndexOf(' ');
            if (lastSpace < 0) return false;

            string sPart = trimmedTail.Substring(lastSpace + 1);
            if (!BigInteger.TryParse(sPart, out s)) return false;

            string rest = trimmedTail.Substring(0, lastSpace);
            int prevSpace = rest.LastIndexOf(' ');
            if (prevSpace < 0) return false;

            string rPart = rest.Substring(prevSpace + 1);
            if (!BigInteger.TryParse(rPart, out r)) return false;

            // Длина суффикса " r s" в байтах
            string suffix = " " + r.ToString() + " " + s.ToString();
            int suffixLen = Encoding.ASCII.GetByteCount(suffix);
            if (suffixLen > fileBytes.Length) return false;

            originalBytes = new byte[fileBytes.Length - suffixLen];
            Array.Copy(fileBytes, originalBytes, originalBytes.Length);
            return true;
        }

        // Совместимость — текстовый парсинг для отображения в verify panel
        public static bool TryParseSignedFile(string content, out string originalText, out BigInteger r, out BigInteger s)
        {
            r = 0; s = 0; originalText = "";
            string trimmed = content.TrimEnd('\r', '\n');
            int lastSpace = trimmed.LastIndexOf(' ');
            if (lastSpace < 0) return false;
            string sPart = trimmed.Substring(lastSpace + 1).Trim();
            if (!BigInteger.TryParse(sPart, out s)) return false;
            string rest = trimmed.Substring(0, lastSpace).TrimEnd('\r', '\n');
            int prevSpace = rest.LastIndexOf(' ');
            if (prevSpace < 0) return false;
            string rPart = rest.Substring(prevSpace + 1).Trim();
            if (!BigInteger.TryParse(rPart, out r)) return false;
            originalText = rest.Substring(0, prevSpace);
            return true;
        }
    }
}
