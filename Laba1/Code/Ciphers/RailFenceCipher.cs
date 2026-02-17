using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers
{
    public static class RailFenceCipher
    {
        // Русский алфавит (33 буквы)
        private const string RussianAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        /// <summary>
        /// Проверяет, является ли символ русской буквой
        /// </summary>
        private static bool IsRussianLetter(char c)
        {
            char upper = char.ToUpper(c);
            return RussianAlphabet.IndexOf(upper) >= 0;
        }

        /// <summary>
        /// Очищает текст от не-русских символов и приводит к верхнему регистру
        /// </summary>
        private static string CleanText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            StringBuilder result = new StringBuilder();
            foreach (char c in text)
            {
                if (IsRussianLetter(c))
                {
                    result.Append(char.ToUpper(c));
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Шифрование методом железнодорожной изгороди
        /// </summary>
        public static string Encrypt(string text, int rails)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (rails <= 0)
                throw new ArgumentException("Высота изгороди должна быть положительным числом");

            string cleanText = CleanText(text);
            if (string.IsNullOrEmpty(cleanText))
                return string.Empty;

            if (rails == 1)
                return cleanText;

            int length = cleanText.Length;

            // Создаем матрицу для размещения символов
            char[,] fence = new char[rails, length];

            // Заполняем матрицу пустыми символами
            for (int i = 0; i < rails; i++)
                for (int j = 0; j < length; j++)
                    fence[i, j] = '\0';

            // Размещаем символы зигзагом
            int currentRail = 0;
            int direction = 1; // 1 - вниз, -1 - вверх

            for (int i = 0; i < length; i++)
            {
                fence[currentRail, i] = cleanText[i];

                currentRail += direction;

                if (currentRail == rails - 1 || currentRail == 0)
                    direction *= -1;
            }

            // Собираем результат построчно (пропуская пустые места)
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (fence[i, j] != '\0')
                        result.Append(fence[i, j]);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Дешифрование методом железнодорожной изгороди
        /// </summary>
        public static string Decrypt(string text, int rails)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            if (rails <= 0)
                throw new ArgumentException("Высота изгороди должна быть положительным числом");

            string cleanText = CleanText(text);

            if (string.IsNullOrEmpty(cleanText))
                return string.Empty;

            int length = cleanText.Length;

            if (rails == 1)
                return cleanText;

            // Шаг 1: Создаем матрицу и определяем позиции символов
            char[,] fence = new char[rails, length];
            int[] railLengths = new int[rails];

            // Определяем, сколько символов попадет на каждую рельсу
            int currentRail = 0;
            int direction = 1;

            for (int i = 0; i < length; i++)
            {
                railLengths[currentRail]++;

                currentRail += direction;

                if (currentRail == rails - 1 || currentRail == 0)
                    direction *= -1;
            }

            // Шаг 2: Заполняем матрицу символами построчно
            int index = 0;
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    // Определяем, должен ли здесь быть символ
                    int rail = 0;
                    int dir = 1;
                    bool hasChar = false;

                    for (int k = 0; k <= j; k++)
                    {
                        if (k == j && rail == i)
                        {
                            hasChar = true;
                            break;
                        }

                        rail += dir;
                        if (rail == rails - 1 || rail == 0)
                            dir *= -1;
                    }

                    if (hasChar && index < length)
                    {
                        fence[i, j] = cleanText[index];
                        index++;
                    }
                    else
                    {
                        fence[i, j] = '\0';
                    }
                }
            }

            // Шаг 3: Читаем зигзагом
            StringBuilder result = new StringBuilder();
            currentRail = 0;
            direction = 1;

            for (int i = 0; i < length; i++)
            {
                result.Append(fence[currentRail, i]);

                currentRail += direction;

                if (currentRail == rails - 1 || currentRail == 0)
                    direction *= -1;
            }

            return result.ToString();
        }
    }
}
