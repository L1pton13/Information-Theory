namespace LfsrCipher;

/// <summary>
/// Линейный сдвиговый регистр с обратной связью (LFSR).
/// Вариант 8: P(x) = x^30 + x^16 + x^15 + x + 1
/// Степень: 30. Тапы (1-индексация): 30, 16, 15, 1.
/// Регистр хранится как int[] где index 0 = b1 (LSB), index 29 = b30 (MSB).
/// Выходной бит = b30 (MSB). Feedback -> b1 (LSB). Сдвиг влево.
/// </summary>
public static class Lfsr
{
    public const int Degree = 30;

    // 0-indexed tap positions: b30=idx29, b16=idx15, b15=idx14, b1=idx0
    private static readonly int[] Taps = { 29, 15, 14, 0 };

    /// <summary>
    /// Проверяет корректность начального состояния регистра.
    /// </summary>
    public static bool IsValidSeed(int[] seed)
    {
        if (seed == null || seed.Length != Degree) return false;
        if (seed.Any(b => b != 0 && b != 1)) return false;
        if (seed.All(b => b == 0)) return false;
        return true;
    }

    /// <summary>
    /// Парсит строку из 0 и 1 в массив битов.
    /// </summary>
    public static int[]? ParseSeed(string s)
    {
        if (s.Length != Degree) return null;
        var bits = new int[Degree];
        for (int i = 0; i < Degree; i++)
        {
            if (s[i] != '0' && s[i] != '1') return null;
            // Ввод b30..b1, массив хранит b1..b30 — разворачиваем
            bits[Degree - 1 - i] = s[i] - '0';
        }
        return bits;
    }

    /// <summary>
    /// Выполняет один такт LFSR.
    /// Возвращает выходной бит (b30 до сдвига).
    /// Обновляет регистр на месте.
    /// </summary>
    public static int Tick(int[] register)
    {
        int outBit = register[Degree - 1]; // b30 = MSB = выходной бит

        int fb = 0;
        foreach (int tap in Taps)
            fb ^= register[tap];

        // Сдвиг влево: b2->b1, b3->b2, ..., b30->b29, fb->b1
        for (int i = Degree - 1; i > 0; i--)
            register[i] = register[i - 1];
        register[0] = fb;

        return outBit;
    }

    /// <summary>
    /// Генерирует numBits бит ключевого потока из начального состояния seed.
    /// seed не изменяется.
    /// </summary>
    public static int[] GenerateKeystream(int[] seed, int numBits)
    {
        var reg = (int[])seed.Clone();
        var ks = new int[numBits];
        for (int i = 0; i < numBits; i++)
            ks[i] = Tick(reg);
        return ks;
    }

    /// <summary>
    /// Возвращает таблицу тактов для отчёта.
    /// Каждая строка: (номер такта, состояние b30..b1, выходной бит, бит обр. связи).
    /// </summary>
    public static List<TactRow> GetTactsTable(int[] seed, int numTacts)
    {
        var reg = (int[])seed.Clone();
        var rows = new List<TactRow>();

        for (int i = 0; i < numTacts; i++)
        {
            // Снимаем данные ДО такта
            string stateStr = string.Concat(reg.Reverse().Select(b => b.ToString()));
            int outBit = reg[Degree - 1];
            int fb = 0;
            foreach (int tap in Taps)
                fb ^= reg[tap];

            rows.Add(new TactRow(i, stateStr, outBit, fb));
            Tick(reg);
        }
        return rows;
    }
}

/// <summary>
/// Одна строка таблицы тактов LFSR.
/// </summary>
public record TactRow(int Tact, string State, int OutBit, int Feedback);