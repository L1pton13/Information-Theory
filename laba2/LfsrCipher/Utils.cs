namespace LfsrCipher;

/// <summary>
/// Вспомогательные методы для отображения данных.
/// </summary>
public static class Utils
{
    /// <summary>
    /// Преобразует массив байт в строку двоичного представления.
    /// Пример: {0x41} -> "01000001"
    /// </summary>
    public static string ToBinaryString(byte[] data, int count = -1)
    {
        if (count < 0) count = data.Length;
        count = Math.Min(count, data.Length);
        return string.Join(" ", data.Take(count).Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
    }

    /// <summary>
    /// Фильтрует строку — оставляет только символы '0' и '1', не более maxLen.
    /// </summary>
    public static string FilterBinaryInput(string input, int maxLen)
    {
        var filtered = new System.Text.StringBuilder();
        foreach (char c in input)
        {
            if (filtered.Length >= maxLen) break;
            if (c == '0' || c == '1') filtered.Append(c);
        }
        return filtered.ToString();
    }

    /// <summary>
    /// Форматирует размер файла в читаемый вид.
    /// </summary>
    public static string FormatFileSize(long bytes)
    {
        if (bytes < 1024) return $"{bytes} Б";
        if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} КБ";
        return $"{bytes / (1024.0 * 1024):F1} МБ";
    }
}
