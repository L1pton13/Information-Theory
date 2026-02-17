using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers
{
    public static class VigenereCipher
    {
        // Русский алфавит (33 буквы)
        private static readonly char[] RussianAlphabet = new char[]
        {
            'А','Б','В','Г','Д','Е','Ё','Ж','З','И','Й','К','Л','М','Н','О','П',
            'Р','С','Т','У','Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я'
        };

        // Таблица Виженера 33x33
        private static char[,] _vigenereTable;

        // Для быстрого поиска индекса буквы
        private static Dictionary<char, int> _alphabetIndex;

        // Статический конструктор - создаем таблицу один раз при загрузке класса
        static VigenereCipher()
        {
            CreateVigenereTable();
        }

        /// <summary>
        /// Создает таблицу Виженера 33x33
        /// </summary>
        private static void CreateVigenereTable()
        {
            int size = RussianAlphabet.Length;
            _vigenereTable = new char[size, size];
            _alphabetIndex = new Dictionary<char, int>();

            // Заполняем словарь индексов
            for (int i = 0; i < size; i++)
            {
                _alphabetIndex[RussianAlphabet[i]] = i;
            }

            // Заполняем таблицу Виженера
            for (int row = 0; row < size; row++) // row - индекс буквы ключа (сдвиг)
            {
                for (int col = 0; col < size; col++) // col - индекс буквы текста
                {
                    // Сдвиг: шифрованная буква = (col + row) % size
                    int encryptedIndex = (col + row) % size;
                    _vigenereTable[row, col] = RussianAlphabet[encryptedIndex];
                }
            }
        }

        /// <summary>
        /// Проверяет, является ли символ русской буквой
        /// </summary>
        private static bool IsRussianLetter(char c)
        {
            char upper = char.ToUpper(c);
            return _alphabetIndex.ContainsKey(upper);
        }

        /// <summary>
        /// Очищает ключ от не-русских символов и приводит к верхнему регистру
        /// </summary>
        private static string CleanKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            StringBuilder result = new StringBuilder();
            foreach (char c in key)
            {
                char upper = char.ToUpper(c);
                if (_alphabetIndex.ContainsKey(upper))
                {
                    result.Append(upper);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Генерирует прогрессивный ключ нужной длины
        /// </summary>
        /// <param name="baseKey">Базовое ключевое слово (очищенное)</param>
        /// <param name="textLength">Длина исходного текста</param>
        /// <param name="applyKey">Массив булевых значений - где применять ключ</param>
        /// <returns>Прогрессивный ключ</returns>
        private static char[] GenerateProgressiveKey(string baseKey, int textLength, bool[] applyKey)
        {
            char[] result = new char[textLength];

            int keyLength = baseKey.Length;
            int keyIndex = 0; // позиция в текущем проходе ключа
            int shift = 0;    // сдвиг для текущего прохода

            for (int i = 0; i < textLength; i++)
            {
                if (applyKey[i])
                {
                    // Определяем, какой это проход ключа
                    // Первые keyLength букв - проход 0, следующие keyLength - проход 1, и т.д.
                    int passage = keyIndex / keyLength;
                    shift = passage; // сдвиг равен номеру прохода

                    // Берем букву из базового ключа
                    char baseChar = baseKey[keyIndex % keyLength];
                    int baseIdx = _alphabetIndex[baseChar];

                    // Применяем сдвиг от прохода
                    int shiftedIdx = (baseIdx + shift) % RussianAlphabet.Length;
                    result[i] = RussianAlphabet[shiftedIdx];

                    keyIndex++;
                }
                else
                {
                    result[i] = '\0'; // заглушка для не-букв
                }
            }

            return result;
        }

        /// <summary>
        /// Шифрование методом Виженера с прогрессивным ключом
        /// </summary>
        public static string Encrypt(string text, string baseKey)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // Очищаем ключ
            string cleanBaseKey = CleanKey(baseKey);
            if (string.IsNullOrEmpty(cleanBaseKey))
                throw new ArgumentException("Ключ должен содержать хотя бы одну русскую букву");

            int textLength = text.Length;

            // Определяем, к каким символам применяем шифрование
            bool[] applyKey = new bool[textLength];
            for (int i = 0; i < textLength; i++)
            {
                applyKey[i] = IsRussianLetter(text[i]);
            }

            // Генерируем прогрессивный ключ
            char[] progressiveKey = GenerateProgressiveKey(cleanBaseKey, textLength, applyKey);

            // Шифруем с использованием таблицы
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < textLength; i++)
            {
                char currentChar = text[i];

                if (applyKey[i])
                {
                    // Шифруем русскую букву
                    char upperChar = char.ToUpper(currentChar);
                    int textIdx = _alphabetIndex[upperChar];
                    int keyIdx = _alphabetIndex[progressiveKey[i]];

                    // Берем из таблицы: строка = ключ, столбец = текст
                    char encryptedChar = _vigenereTable[keyIdx, textIdx];

                    result.Append(encryptedChar);
                }
                else
                {
                    // Не русская буква - оставляем как есть
                    result.Append(currentChar);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Дешифрование методом Виженера с прогрессивным ключом
        /// </summary>
        public static string Decrypt(string encryptedText, string baseKey)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return string.Empty;

            // Очищаем ключ
            string cleanBaseKey = CleanKey(baseKey);
            if (string.IsNullOrEmpty(cleanBaseKey))
                throw new ArgumentException("Ключ должен содержать хотя бы одну русскую букву");

            int textLength = encryptedText.Length;

            // Определяем, какие символы были зашифрованы (русские буквы)
            bool[] wasEncrypted = new bool[textLength];
            for (int i = 0; i < textLength; i++)
            {
                wasEncrypted[i] = IsRussianLetter(encryptedText[i]);
            }

            // Генерируем прогрессивный ключ (такой же, как при шифровании)
            char[] progressiveKey = GenerateProgressiveKey(cleanBaseKey, textLength, wasEncrypted);
            

            // Расшифровываем с использованием таблицы
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < textLength; i++)
            {
                char currentChar = encryptedText[i];

                if (wasEncrypted[i])
                {
                    // Расшифровываем русскую букву
                    int keyIdx = _alphabetIndex[progressiveKey[i]];

                    // Ищем в строке ключа (keyIdx) столбец, где находится currentChar
                    int textIdx = -1;
                    for (int col = 0; col < RussianAlphabet.Length; col++)
                    {
                        if (_vigenereTable[keyIdx, col] == currentChar)
                        {
                            textIdx = col;
                            break;
                        }
                    }

                    if (textIdx != -1)
                    {
                        result.Append(RussianAlphabet[textIdx]);
                    }
                    else
                    {
                        // Если не нашли (не должно происходить), оставляем как есть
                        result.Append(currentChar);
                    }
                }
                else
                {
                    // Не русская буква - оставляем как есть
                    result.Append(currentChar);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Возвращает сгенерированный прогрессивный ключ в виде строки
        /// </summary>
        public static string GetProgressiveKeyString(string text, string baseKey)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(baseKey))
                return string.Empty;

            string cleanBaseKey = CleanKey(baseKey);
            if (string.IsNullOrEmpty(cleanBaseKey))
                return string.Empty;

            int textLength = text.Length;
            bool[] applyKey = new bool[textLength];
            for (int i = 0; i < textLength; i++)
            {
                applyKey[i] = IsRussianLetter(text[i]);
            }

            char[] progressiveKey = GenerateProgressiveKey(cleanBaseKey, textLength, applyKey);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < textLength; i++)
            {
                if (applyKey[i])
                    result.Append(progressiveKey[i]);
                else
                    result.Append(' '); // пробел для не-букв
            }

            return result.ToString();
        }
    }
}