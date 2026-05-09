using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace DSA
{
    public partial class Form1 : Form
    {
        // --- State ---
        private BigInteger _q, _p, _h, _x, _k, _g, _y;
        private bool _gComputed = false;
        private bool _yComputed = false;
        private byte[] _fileBytes = null;
        private string _filePath = null;
        private BigInteger _currentHash;
        private BigInteger _currentR, _currentS;
        private BigInteger _h0 = 100;
        private byte[] _signedFileBytes = null;

        // Colors
        private static readonly Color ColorOk = Color.FromArgb(198, 239, 206);
        private static readonly Color ColorErr = Color.FromArgb(220, 50, 50);
        private static readonly Color ColorErrText = Color.White;
        private static readonly Color ColorNeutral = Color.White;
        private static readonly Color ColorNeutralText = Color.Black;

        public Form1()
        {
            InitializeComponent();
        }

        // =====================================================================
        // VALIDATION HELPERS
        // =====================================================================

        private void SetFieldOk(TextBox tb)
        {
            tb.BackColor = ColorOk;
            tb.ForeColor = ColorNeutralText;
        }

        private void SetFieldErr(TextBox tb)
        {
            tb.BackColor = ColorErr;
            tb.ForeColor = ColorErrText;
        }

        private void SetFieldNeutral(TextBox tb)
        {
            tb.BackColor = ColorNeutral;
            tb.ForeColor = ColorNeutralText;
        }

        private bool ValidateQ()
        {
            if (!BigInteger.TryParse(txtQ.Text.Trim(), out BigInteger val) || !DSAMath.IsPrime(val) || val < 2)
            {
                SetFieldErr(txtQ);
                MessageBox.Show("Параметр q должен быть простым числом (целым, > 1).\nПример: 107", "Ошибка ввода q", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _q = val;
            SetFieldOk(txtQ);
            return true;
        }

        private bool ValidateP()
        {
            if (!BigInteger.TryParse(txtP.Text.Trim(), out BigInteger val))
            {
                SetFieldErr(txtP);
                MessageBox.Show("Параметр p должен быть простым числом, при котором (p−1) mod q = 0.\nПример: 643 (при q=107)", "Ошибка ввода p", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            bool isPrime = DSAMath.IsPrime(val);
            bool divisible = (val - 1) % _q == 0;

            if (!isPrime && !divisible)
            {
                SetFieldErr(txtP);
                MessageBox.Show("p должно быть простым числом И (p−1) mod q = 0.\nПример: 643 (при q=107)", "Ошибка ввода p", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!isPrime)
            {
                SetFieldErr(txtP);
                MessageBox.Show("p должно быть простым числом.\nПример: 643", "Ошибка ввода p", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!divisible)
            {
                SetFieldErr(txtP);
                MessageBox.Show($"Условие (p−1) mod q = 0 не выполнено.\n({val - 1}) mod {_q} = {(val - 1) % _q} ≠ 0.\nПример: 643 (при q=107)", "Ошибка ввода p", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _p = val;
            SetFieldOk(txtP);
            return true;
        }

        private bool ValidateH()
        {
            if (!BigInteger.TryParse(txtH.Text.Trim(), out BigInteger val) || val <= 1 || val >= _p - 1)
            {
                SetFieldErr(txtH);
                MessageBox.Show($"Параметр h должен быть целым числом из интервала (1, p−1) = (1, {_p - 1}).\nПример: 2", "Ошибка ввода h", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _h = val;
            SetFieldOk(txtH);
            return true;
        }

        private bool ValidateX()
        {
            if (!BigInteger.TryParse(txtX.Text.Trim(), out BigInteger val) || val <= 0 || val >= _q)
            {
                SetFieldErr(txtX);
                MessageBox.Show($"Закрытый ключ x должен быть целым числом из интервала (0, q) = (0, {_q}).\nПример: 45", "Ошибка ввода x", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _x = val;
            SetFieldOk(txtX);
            return true;
        }

        private bool ValidateH0()
        {
            if (!BigInteger.TryParse(txtH0.Text.Trim(), out BigInteger val) || val < 0)
            {
                SetFieldErr(txtH0);
                MessageBox.Show("H₀ должен быть неотрицательным целым числом.\nПо умолчанию: 100", "Ошибка ввода H₀", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _h0 = val;
            SetFieldOk(txtH0);
            return true;
        }

        private bool ValidateK()
        {
            if (!BigInteger.TryParse(txtK.Text.Trim(), out BigInteger val) || val <= 0 || val >= _q)
            {
                SetFieldErr(txtK);
                MessageBox.Show($"Параметр k должен быть целым числом из интервала (0, q) = (0, {_q}).\nПример: 31", "Ошибка ввода k", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _k = val;
            SetFieldOk(txtK);
            return true;
        }

        // =====================================================================
        // EVENTS — PARAMETER INPUTS (validate on Leave)
        // =====================================================================

        private void txtQ_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQ.Text)) { SetFieldNeutral(txtQ); return; }
            ValidateQ();
        }

        private void txtP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtP.Text)) { SetFieldNeutral(txtP); return; }
            if (_q == 0) { MessageBox.Show("Сначала введите корректный q.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ValidateP();
        }

        private void txtH_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtH.Text)) { SetFieldNeutral(txtH); return; }
            if (_p == 0) { MessageBox.Show("Сначала введите корректный p.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ValidateH();
        }

        private void txtX_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtX.Text)) { SetFieldNeutral(txtX); return; }
            if (_q == 0) { MessageBox.Show("Сначала введите корректный q.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ValidateX();
        }

        private void txtH0_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtH0.Text)) { SetFieldNeutral(txtH0); return; }
            ValidateH0();
        }

        private void txtK_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtK.Text)) { SetFieldNeutral(txtK); return; }
            if (_q == 0) { MessageBox.Show("Сначала введите корректный q.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ValidateK();
        }

        // =====================================================================
        // BUTTON: Вычислить g
        // =====================================================================
        private void btnComputeG_Click(object sender, EventArgs e)
        {
            if (!ValidateQ()) return;
            if (!ValidateP()) return;
            if (!ValidateH()) return;

            _g = DSAMath.ComputeG(_h, _p, _q);
            if (_g <= 1)
            {
                MessageBox.Show($"Вычисленное g = {_g} ≤ 1. Выберите другие параметры (h, p, q).", "Ошибка g", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblGValue.Text = $"g = {_g}  ⚠ g должен быть > 1";
                lblGValue.ForeColor = Color.Red;
                _gComputed = false;
                return;
            }
            lblGValue.Text = $"g = h^((p−1)/q) mod p = {_h}^{(_p - 1) / _q} mod {_p} = {_g}";
            lblGValue.ForeColor = Color.FromArgb(0, 120, 60);
            _gComputed = true;
        }

        // =====================================================================
        // BUTTON: Вычислить y (открытый ключ)
        // =====================================================================
        private void btnComputeY_Click(object sender, EventArgs e)
        {
            if (!_gComputed) { MessageBox.Show("Сначала вычислите g.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (!ValidateX()) return;

            _y = DSAMath.ComputeY(_g, _x, _p);
            lblYValue.Text = $"y = g^x mod p = {_g}^{_x} mod {_p} = {_y}";
            lblYValue.ForeColor = Color.FromArgb(0, 120, 60);
            lblKeysInfo.Text = $"✔ ЭЦП вычислена успешно.  Открытый ключ (y, p, q, g) = ({_y}, {_p}, {_q}, {_g})   Закрытый ключ x = {_x}";
            lblKeysInfo.ForeColor = Color.FromArgb(0, 120, 60);
            _yComputed = true;
        }

        // =====================================================================
        // BUTTON: Выбрать файл (для подписи)
        // =====================================================================
        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Выберите файл для подписи";
                dlg.Filter = "Все файлы (*.*)|*.*";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                _filePath = dlg.FileName;
                _fileBytes = File.ReadAllBytes(_filePath);
                lblFilePath.Text = _filePath;
                txtFileContent.Text = DSAService.GetFileDisplayText(_fileBytes);
                // Clear previous results
                txtSignResult.Clear();
                txtSignedFileContent.Clear();
                lblHashValue.Text = "";
                _signedFileBytes = null;
            }
        }

        // =====================================================================
        // BUTTON: Предпросмотр хэша
        // =====================================================================
        private void btnPreviewHash_Click(object sender, EventArgs e)
        {
            if (!ValidateH0()) return;
            if (!ValidateQ()) return;
            if (_fileBytes == null) { MessageBox.Show("Сначала выберите файл.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            int[] codes = DSAService.GetAsciiCodesFromBytes(_fileBytes);
            string steps = DSAService.ComputeHashWithSteps(codes, _h0, _q, out _currentHash);
            txtSignResult.Text = steps;
            lblHashValue.Text = $"H = {_currentHash}";
        }

        // =====================================================================
        // BUTTON: Подписать файл
        // =====================================================================
        private void btnSign_Click(object sender, EventArgs e)
        {
            if (!_yComputed) { MessageBox.Show("Сначала вычислите ключи (g и y).", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (_fileBytes == null) { MessageBox.Show("Сначала выберите файл.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (!ValidateH0()) return;
            if (!ValidateK()) return;

            int[] codes = DSAService.GetAsciiCodesFromBytes(_fileBytes);

            // Compute hash
            string hashSteps = DSAService.ComputeHashWithSteps(codes, _h0, _q, out _currentHash);

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("=== ПАРАМЕТРЫ DSA ===");
            sb.AppendLine($"q = {_q}");
            sb.AppendLine($"p = {_p}");
            sb.AppendLine($"h = {_h}");
            sb.AppendLine($"g = h^((p-1)/q) mod p = {_g}");
            sb.AppendLine($"x = {_x}   (закрытый ключ)");
            sb.AppendLine($"y = g^x mod p = {_y}   (открытый ключ)");
            sb.AppendLine($"k = {_k}");
            sb.AppendLine();
            sb.AppendLine(hashSteps);
            lblHashValue.Text = $"H = {_currentHash}";

            // Sign — retry if r or s is 0
            string sigSteps = DSAService.ComputeSignatureWithSteps(_g, _p, _q, _x, _k, _currentHash, out _currentR, out _currentS);

            if (_currentR == 0 || _currentS == 0)
            {
                sb.AppendLine(sigSteps);
                sb.AppendLine();
                sb.AppendLine($"⚠ r={_currentR} или s={_currentS} равно 0. Введите другое значение k и нажмите «Подписать файл» снова.");
                txtSignResult.Text = sb.ToString();
                MessageBox.Show($"Получено r={_currentR} или s={_currentS} = 0.\nПожалуйста, введите другое значение k и повторите подписание.", "Повторите ввод k", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetFieldErr(txtK);
                return;
            }

            sb.AppendLine(sigSteps);
            sb.AppendLine();
            sb.AppendLine($"✔ ЭЦП = (r, s) = ({_currentR}, {_currentS})");
            txtSignResult.Text = sb.ToString();

            // Build signed file bytes: original bytes + ASCII signature suffix " r s"
            _signedFileBytes = DSAService.BuildSignedBytes(_fileBytes, _currentR, _currentS);

            // Show preview in text box
            bool isText = DSAService.IsLikelyText(_fileBytes);
            var previewSb = new System.Text.StringBuilder();
            if (isText)
            {
                previewSb.AppendLine(Encoding.UTF8.GetString(_fileBytes));
            }
            else
            {
                previewSb.AppendLine("[Бинарный файл — содержимое не отображается как текст]");
            }
            previewSb.Append($" {_currentR} {_currentS}");
            txtSignedFileContent.Text = previewSb.ToString();
        }

        // =====================================================================
        // BUTTON: Сохранить подписанный файл
        // =====================================================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_signedFileBytes == null)
            {
                MessageBox.Show("Сначала подпишите файл.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string initialDir = _filePath != null ? Path.GetDirectoryName(_filePath) : Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string baseName = _filePath != null ? Path.GetFileNameWithoutExtension(_filePath) : "document";
            string ext = _filePath != null ? Path.GetExtension(_filePath) : ".txt";

            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Сохранить подписанный файл";
                dlg.InitialDirectory = initialDir;
                dlg.FileName = baseName + "_signed" + ext;
                dlg.Filter = "Все файлы (*.*)|*.*";

                if (dlg.ShowDialog() != DialogResult.OK) return;

                File.WriteAllBytes(dlg.FileName, _signedFileBytes);
                MessageBox.Show($"Файл сохранён:\n{dlg.FileName}", "Сохранено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // =====================================================================
        // BUTTON: Выбрать файл для проверки
        // =====================================================================
        private string _verifyFilePath = null;
        private byte[] _verifyFileBytes = null;

        private void btnChooseVerifyFile_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Выберите подписанный файл для проверки";
                dlg.Filter = "Все файлы (*.*)|*.*";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                _verifyFilePath = dlg.FileName;
                _verifyFileBytes = File.ReadAllBytes(_verifyFilePath);
                lblVerifyFilePath.Text = _verifyFilePath;

                // Пытаемся распарсить подпись из байтов
                if (DSAService.TryParseSignedBytes(_verifyFileBytes, out byte[] originalBytes, out BigInteger vr, out BigInteger vs))
                {
                    var sb = new System.Text.StringBuilder();
                    bool isText = DSAService.IsLikelyText(originalBytes);
                    sb.AppendLine("=== Содержимое файла (без подписи) ===");
                    if (isText)
                        sb.AppendLine(Encoding.UTF8.GetString(originalBytes));
                    else
                        sb.AppendLine("[Бинарный файл — содержимое не отображается как текст]");
                    sb.AppendLine();
                    sb.AppendLine("=== ASCII-коды байтов оригинала (десятичная) ===");
                    sb.AppendLine(string.Join(" ", originalBytes.Select(b => b.ToString())));
                    sb.AppendLine();
                    sb.AppendLine($"=== Подпись в файле: r = {vr}, s = {vs} ===");
                    txtVerifyContent.Text = sb.ToString();
                }
                else
                {
                    txtVerifyContent.Text = DSAService.GetFileDisplayText(_verifyFileBytes);
                }

                txtVerifyResult.Clear();
                txtVerifyResult.BackColor = Color.White;
            }
        }

        // =====================================================================
        // BUTTON: Проверить подпись
        // =====================================================================
        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (!_yComputed) { MessageBox.Show("Для проверки необходимы ключи. Убедитесь, что вычислены g и y.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (_verifyFileBytes == null) { MessageBox.Show("Сначала выберите файл для проверки.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (!DSAService.TryParseSignedBytes(_verifyFileBytes, out byte[] originalBytes, out BigInteger r, out BigInteger s))
            {
                txtVerifyResult.BackColor = Color.FromArgb(255, 200, 200);
                txtVerifyResult.Text = "✘ Ошибка: не удалось извлечь подпись из файла. Возможно, файл не подписан или повреждён.";
                return;
            }

            int[] codes = DSAService.GetAsciiCodesFromBytes(originalBytes);

            if (!ValidateH0()) return;

            string hashSteps = DSAService.ComputeHashWithSteps(codes, _h0, _q, out BigInteger hashM);
            string verifySteps = DSAService.VerifyWithSteps(_g, _p, _q, _y, r, s, hashM, out bool isValid);

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("=== ПАРАМЕТРЫ ПРОВЕРКИ ===");
            sb.AppendLine($"q = {_q}, p = {_p}, g = {_g}");
            sb.AppendLine($"y (открытый ключ) = {_y}");
            sb.AppendLine($"r = {r}, s = {s}  (из подписи)");
            sb.AppendLine();
            sb.AppendLine(hashSteps);
            sb.AppendLine(verifySteps);

            txtVerifyResult.Text = sb.ToString();
            txtVerifyResult.BackColor = isValid
                ? Color.FromArgb(198, 239, 206)
                : Color.FromArgb(255, 200, 200);
        }
    }
}
