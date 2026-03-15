namespace LfsrCipher;

/// <summary>
/// Потоковый шифр на основе LFSR.
/// Шифрование и дешифрование — одна операция (XOR симметричен).
/// </summary>
public static class StreamCipher
{
    private const int BufferSize = 4096;

    /// <summary>
    /// Шифрует или дешифрует файл inPath -> outPath используя LFSR с seed.
    /// Возвращает превью: первые previewBytes байт ключевого потока и выходного файла.
    /// </summary>
    public static CipherResult ProcessFile(
        int[] seed,
        string inPath,
        string outPath,
        int previewBytes = 8)
    {
        var keystreamPreview = new List<byte>();
        var outputPreview = new List<byte>();

        // Клонируем регистр — не портим оригинал
        var reg = (int[])seed.Clone();
        int previewCount = 0;

        using var inFile  = File.OpenRead(inPath);
        using var outFile = File.Create(outPath);

        byte[] inBuf  = new byte[BufferSize];
        byte[] outBuf = new byte[BufferSize];
        int read;

        while ((read = inFile.Read(inBuf, 0, BufferSize)) > 0)
        {
            for (int i = 0; i < read; i++)
            {
                // Генерируем 8 бит ключа для одного байта
                byte keyByte = 0;
                for (int bit = 7; bit >= 0; bit--)
                {
                    int k = Lfsr.Tick(reg);
                    keyByte |= (byte)(k << bit);
                }

                outBuf[i] = (byte)(inBuf[i] ^ keyByte);

                if (previewCount < previewBytes)
                {
                    keystreamPreview.Add(keyByte);
                    outputPreview.Add(outBuf[i]);
                    previewCount++;
                }
            }
            outFile.Write(outBuf, 0, read);
        }

        return new CipherResult(
            keystreamPreview.ToArray(),
            outputPreview.ToArray());
    }

    /// <summary>
    /// Читает первые maxBytes байт файла для отображения.
    /// </summary>
    public static byte[] ReadPreview(string path, int maxBytes = 8)
    {
        using var f = File.OpenRead(path);
        var buf = new byte[maxBytes];
        int n = f.Read(buf, 0, maxBytes);
        return buf[..n];
    }
}

/// <summary>
/// Результат шифрования: превью ключевого потока и выходного файла.
/// </summary>
public record CipherResult(byte[] KeystreamPreview, byte[] OutputPreview);
