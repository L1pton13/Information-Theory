using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;

namespace RabinCipher
{
    public partial class MainForm : Form
    {
        private string currentDirectory = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\Information - Theory\\laba3\\testFiles";
        private byte[] rawData;      // Исходные байты файла
        private byte[] processedData; // Результат (зашифрованный или расшифрованный)
        private RabinCipher cipher = new RabinCipher();

        public MainForm()
        {
            InitializeComponent();
            tbP.Leave += (s, e) => ValidateP();
            tbQ.Leave += (s, e) => ValidateQ();
            tbQ.Leave += (s, e) => ValidateN();
            tbB.Leave += (s, e) => ValidateB();
        }

        private bool ValidateParameters()
        {
            if (string.IsNullOrWhiteSpace(tbP.Text) || string.IsNullOrWhiteSpace(tbQ.Text) || string.IsNullOrWhiteSpace(tbB.Text))
                return false;

            BigInteger p, q, b, n;

            try
            {
                p = BigInteger.Parse(tbP.Text);
                q = BigInteger.Parse(tbQ.Text);
                b = BigInteger.Parse(tbB.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("В полях p, q и b должны быть только целые числа!", "Ошибка ввода");
                return false;
            }

            bool HasError = false;

            if (!MathHelper.IsValidRabinPrime(p))
            {
                MessageBox.Show(tbP, "Число p должно быть простым и удовлетворять условию p % 4 = 3");
                HasError = true;
            }

            if (!MathHelper.IsValidRabinPrime(q))
            {
                MessageBox.Show("Число q должно быть простым и удовлетворять условию q % 4 = 3");
                HasError = true;
            }

            if (!HasError)
            {
                n = p * q;
                if (n <= 255)
                {
                    MessageBox.Show("N (p*q) должно быть > 255, чтобы зашифровать байт");
                    HasError = true;
                }
                if (p == q)
                {
                    MessageBox.Show("Числа p и q должны быть разными");
                    HasError = true;
                }
            }
            if (HasError)
                return false;
            else
                return true;
        }

        private bool ValidateP()
        {
            if (string.IsNullOrWhiteSpace(tbP.Text))
                return false;

            BigInteger p;
            try
            {
                p = BigInteger.Parse(tbP.Text);
            }
            catch (FormatException)
            {
                tbP.BackColor = Color.Maroon;
                tbP.ForeColor = Color.White;
                MessageBox.Show("P должно быть только целым числом!", "Ошибка ввода");
                return false;
            }

            if (!MathHelper.IsValidRabinPrime(p))
            {
                tbP.BackColor = Color.Maroon;
                tbP.ForeColor = Color.White;
                MessageBox.Show("Число p должно быть простым и удовлетворять условию p % 4 = 3");
                return false;
            }
            tbP.BackColor = Color.White;
            tbP.ForeColor = Color.Black;
            return true;
        }

        private bool ValidateQ()
        {
            if (string.IsNullOrWhiteSpace(tbQ.Text))
                return false;

            BigInteger q;
            try
            {
                q = BigInteger.Parse(tbQ.Text);
            }
            catch (FormatException)
            {
                tbQ.BackColor = Color.Maroon;
                tbQ.ForeColor = Color.White;
                MessageBox.Show("Q должно быть только целым числом!", "Ошибка ввода");
                return false;
            }

            if (!MathHelper.IsValidRabinPrime(q))
            {
                tbQ.BackColor = Color.Maroon;
                tbQ.ForeColor = Color.White;
                MessageBox.Show("Число q должно быть простым и удовлетворять условию q % 4 = 3");
                return false;
            }
            if (BigInteger.Parse(tbP.Text) == q)
            {
                MessageBox.Show("Числа p и q должны быть разными");
                return false;
            }
            tbQ.BackColor = Color.White;
            tbQ.ForeColor = Color.Black;
            return true;
        }

        private void ValidateN()
        {
            if (!ValidateP() || !ValidateQ())
                return;

            BigInteger p = BigInteger.Parse(tbP.Text);
            BigInteger q = BigInteger.Parse(tbQ.Text);
            BigInteger n = p * q;
            if (n <= 255)
                MessageBox.Show("N (p*q) должно быть > 255, чтобы зашифровать байт");
            else
            {
                tbN.Text = n.ToString();
                tbN.Enabled = false;
                tbB.Enabled = true;
            }
        }

        private void ValidateB()
        {
            if (string.IsNullOrWhiteSpace(tbB.Text))
                return;

            BigInteger b;
            try
            {
                b = BigInteger.Parse(tbB.Text);
            }
            catch (FormatException)
            {
                tbB.BackColor = Color.Maroon;
                MessageBox.Show("B должно быть только целым числом!", "Ошибка ввода");
                return;
            }

            if (b >= BigInteger.Parse(tbN.Text))
            {
                tbB.BackColor = Color.Maroon;
                MessageBox.Show("Число b должно удовлетворять условию b < n");
                return;
            }
            tbB.BackColor = Color.White;
        }

        public void UpdateInputDisplay()
        {
            if (rawData == null) return;

            var displayData = rawData.Take(1024).ToArray();
            StringBuilder sb = new StringBuilder();

            if (rbFourBytes.Checked)
            {
                for (int i = 0; i < displayData.Length; i += 4)
                {
                    if (i + 4 <= displayData.Length)
                    {
                        int val = BitConverter.ToInt32(displayData, i);
                        sb.Append(val).Append(" ");
                    }
                    else
                    {
                        sb.Append(displayData[i]).Append(" ");
                    }
                }
            }
            else
            {
                sb.Append(string.Join(" ", displayData));
            }

            if (rawData.Length > 1024)
                sb.Append("... [данные обрезаны]");

            rtbInput.Text = sb.ToString();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (ValidateParameters())
            {
                using OpenFileDialog openFileDialog = new OpenFileDialog();
                {
                    openFileDialog.InitialDirectory = currentDirectory;
                    openFileDialog.Filter = "All files (*.*)|*.*";
                    openFileDialog.RestoreDirectory = true;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                    rawData = File.ReadAllBytes(openFileDialog.FileName);
                    UpdateInputDisplay();
                }
            }
            else
                MessageBox.Show("Введите все исходные значения перед началом работы");

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (ValidateParameters() && rawData != null)
            {
                BigInteger p = BigInteger.Parse(tbP.Text);
                BigInteger q = BigInteger.Parse(tbQ.Text);
                BigInteger b = BigInteger.Parse(tbB.Text);
                BigInteger n = BigInteger.Parse(tbN.Text);

                processedData = cipher.Encrypt(rawData, n, b);

                StringBuilder sb = new StringBuilder();

                int limit = Math.Min(processedData.Length, 1024);

                for (int i = 0; i < limit; i += 4)
                {
                    int val = BitConverter.ToInt32(processedData, i);
                    sb.Append(val).Append(" ");
                }

                if (processedData.Length > 1024)
                    sb.Append("... [данные обрезаны]");

                rtbOutput.Text = sb.ToString();
            }
            else if (!ValidateParameters())
            {
                MessageBox.Show("Введите все необходимые данные");
                return;
            }
            else if (rawData == null)
            {
                MessageBox.Show("Сначала выберите файл для шифрования");
                return;
            }

        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (ValidateParameters() && rawData != null)
            {
                BigInteger p = BigInteger.Parse(tbP.Text);
                BigInteger q = BigInteger.Parse(tbQ.Text);
                BigInteger b = BigInteger.Parse(tbB.Text);
                BigInteger n = BigInteger.Parse(tbN.Text);

                processedData = cipher.Decrypt(rawData, p, q, b, n);

                var displayData = processedData.Take(1024);
                rtbOutput.Text = string.Join(" ", displayData);

                if (processedData.Length > 1024)
                    rtbOutput.Text += " ... [данные обрезаны]";
            }
            else if (!ValidateParameters())
            {
                MessageBox.Show("Введите все исходные значения перед началом работы");
            }
            else if (rawData == null)
            {
                MessageBox.Show("Сначала выберите файл для дешифрования");
                return;
            }

        }

        private void btnSaveTo_Click(object sender, EventArgs e)
        {
            if (processedData == null)
            {
                MessageBox.Show("Нет данных для сохранения. Сначала выполните шифрование или дешифрование.");
                return;
            }

            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            {
                saveFileDialog.InitialDirectory = currentDirectory;
                saveFileDialog.Filter = "All files (*.*)|*.*";
                saveFileDialog.RestoreDirectory = true;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                File.WriteAllBytes(saveFileDialog.FileName, processedData);
                MessageBox.Show("Файл успешно сохранён!");

            }
        }

        private void rbOneByte_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInputDisplay();
        }

        private void rbFourBytes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInputDisplay();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            rawData = null;
            processedData = null;

            rtbInput.Clear();
            rtbOutput.Clear();
        }
    }
}
