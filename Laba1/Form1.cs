using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ciphers;

namespace Ciphers
{
    public partial class        Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _generatedProgressiveKey = "";

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, что выбран метод "Железнодорожная изгородь"
                if (rbRailFence.Checked)
                {
                    // Получаем данные с формы
                    string inputText = txtInput.Text;
                    int rails = (int)numRails.Value; // берем значение из NumericUpDown

                    // Вызываем метод шифрования из твоего класса
                    string encryptedText = RailFenceCipher.Encrypt(inputText, rails);

                    // Выводим результат
                    txtOutput.Text = encryptedText;
                }
                else if (rbVigenere.Checked)
                {
                    // Шифрование Виженером
                    string inputText = txtInput.Text;
                    string key = txtKey.Text;

                    if (string.IsNullOrWhiteSpace(key))
                    {
                        MessageBox.Show("Введите ключевое слово!", "Предупреждение",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Шифруем
                    string encryptedText = VigenereCipher.Encrypt(inputText, key);
                    txtOutput.Text = encryptedText;

                    // Получаем прогрессивный ключ для отображения
                    _generatedProgressiveKey = VigenereCipher.GetProgressiveKeyString(inputText, key);
                    txtKey.Text = _generatedProgressiveKey;
                    // Можно также показать ключ в отдельном текстбоксе, если есть
                    // txtProgressiveKey.Text = _generatedProgressiveKey;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при шифровании: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, что выбран метод "Железнодорожная изгородь"
                if (rbRailFence.Checked)
                {
                    // Получаем данные с формы
                    string inputText = txtInput.Text;
                    int rails = (int)numRails.Value;

                    // Проверяем, что есть что расшифровывать
                    if (string.IsNullOrWhiteSpace(inputText))
                    {
                        MessageBox.Show("Введите текст для расшифровки",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Вызываем метод дешифрования
                    string decryptedText = RailFenceCipher.Decrypt(inputText, rails);

                    // Выводим результат
                    txtOutput.Text = decryptedText;

                }
                else if  (rbVigenere.Checked)
                {
                    // Дешифрование Виженера
                    string inputText = txtInput.Text;
                    string key = txtKey.Text;

                    if (string.IsNullOrWhiteSpace(key))
                    {
                        MessageBox.Show("Введите ключевое слово!", "Предупреждение",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string decryptedText = VigenereCipher.Decrypt(inputText, key);
                    txtOutput.Text = decryptedText;

                    _generatedProgressiveKey = VigenereCipher.GetProgressiveKeyString(inputText, key);
                    txtKey.Text = _generatedProgressiveKey;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расшифровке: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            txtOutput.Clear();
            txtKey.Clear();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстовые файлы (*.txt)|*.txt";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string content = File.ReadAllText(openDialog.FileName, Encoding.UTF8);
                txtInput.Text = content;
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOutput.Text))
            {
                MessageBox.Show("Нет результата для сохранения");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveDialog.FileName, txtOutput.Text, Encoding.UTF8);
                MessageBox.Show("Файл сохранен");
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOutput.Text))
            {
                Clipboard.SetText(txtOutput.Text);
            }
        }

        private void btnClearInput_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
        }

        private void rbRailFence_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRailFence.Checked)
            {
                lblRails.Visible = true;
                numRails.Visible = true;

                lblKey.Visible = false;
                txtKey.Visible = false;
            }
        }

        private void rbVigenere_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVigenere.Checked)
            {
                lblRails.Visible = false;
                numRails.Visible = false;

                lblKey.Visible = true;
                txtKey.Visible = true;
            }
        }
    }
}
