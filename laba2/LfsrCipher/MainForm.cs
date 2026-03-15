namespace LfsrCipher;

public partial class MainForm : Form
{
    private string? _inputFilePath;
    private string? _outputFilePath;

    public MainForm()
    {
        InitializeComponent();
    }

    private void btnOpenFile_Click(object sender, EventArgs e)
    {
        using var dlg = new OpenFileDialog
        {
            Title = "Выберите файл для шифрования / дешифрования",
            Filter = "Все файлы (*.*)|*.*"
        };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        _inputFilePath = dlg.FileName;
        lblInputFile.Text = Path.GetFileName(_inputFilePath);
        lblInputFile.ForeColor = Color.Black;
        lblInputSize.Text = Utils.FormatFileSize(new FileInfo(_inputFilePath).Length);

        byte[] preview = StreamCipher.ReadPreview(_inputFilePath, 8);
        tbInputBinary.Text = Utils.ToBinaryString(preview);

        tbKeystreamBinary.Clear();
        tbOutputBinary.Clear();
        lblOutputFile.Text = "не выбран";
        lblOutputFile.ForeColor = Color.Gray;
        _outputFilePath = null;

        SetStatus("Файл выбран. Введите начальное состояние и нажмите «Зашифровать» или «Расшифровать».");
    }

    private void btnSaveFile_Click(object sender, EventArgs e)
    {
        using var dlg = new SaveFileDialog
        {
            Title = "Сохранить результат как...",
            Filter = "Все файлы (*.*)|*.*",
            FileName = _inputFilePath != null
                ? Path.GetFileNameWithoutExtension(_inputFilePath) + "_out" + Path.GetExtension(_inputFilePath)
                : "output.bin"
        };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        _outputFilePath = dlg.FileName;
        lblOutputFile.Text = Path.GetFileName(_outputFilePath);
        lblOutputFile.ForeColor = Color.Black;
        SetStatus("Путь для сохранения выбран.");
    }

    private void btnEncrypt_Click(object sender, EventArgs e) => RunCipher("Зашифровать");
    private void btnDecrypt_Click(object sender, EventArgs e) => RunCipher("Расшифровать");

    private void RunCipher(string action)
    {
        if (!ValidateInputs()) return;

        int[]? seed = Lfsr.ParseSeed(tbSeed.Text);
        if (seed == null || !Lfsr.IsValidSeed(seed))
        {
            MessageBox.Show($"Введите ровно {Lfsr.Degree} бит (символы 0 и 1). Состояние не может быть нулевым.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_outputFilePath == null) btnSaveFile_Click(this, EventArgs.Empty);
        if (_outputFilePath == null) return;

        try
        {
            SetStatus($"{action}... Пожалуйста, подождите.");
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            CipherResult result = StreamCipher.ProcessFile(seed, _inputFilePath!, _outputFilePath, previewBytes: 8);

            tbKeystreamBinary.Text = Utils.ToBinaryString(result.KeystreamPreview);
            tbOutputBinary.Text = Utils.ToBinaryString(result.OutputPreview);

            long outSize = new FileInfo(_outputFilePath).Length;
            SetStatus($"Готово! Файл сохранён: {Path.GetFileName(_outputFilePath)} ({Utils.FormatFileSize(outSize)})");
            MessageBox.Show($"Файл успешно обработан и сохранён:\n{_outputFilePath}",
                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при обработке файла:\n{ex.Message}",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SetStatus("Ошибка.");
        }
        finally
        {
            Cursor = Cursors.Default;
        }
    }

    private void btnShowTacts_Click(object sender, EventArgs e)
    {
        int[]? seed = Lfsr.ParseSeed(tbSeed.Text);
        if (seed == null || !Lfsr.IsValidSeed(seed))
        {
            MessageBox.Show($"Введите ровно {Lfsr.Degree} бит для отображения тактов.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var rows = Lfsr.GetTactsTable(seed, 65);
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"{"Такт",4}  {"Состояние b30...b1",-32}  {"Выход",5}  {"Обр.связь",9}");
        sb.AppendLine(new string('-', 60));
        foreach (var r in rows)
            sb.AppendLine($"{r.Tact,4}  {r.State,-32}  {r.OutBit,5}  {r.Feedback,9}");

        tbTacts.Text = sb.ToString();
        SetStatus("Отображено 65 тактов LFSR.");
    }

    private void tbSeed_TextChanged(object sender, EventArgs e)
    {
        int caret = tbSeed.SelectionStart;
        string filtered = Utils.FilterBinaryInput(tbSeed.Text, Lfsr.Degree);
        if (tbSeed.Text != filtered)
        {
            tbSeed.Text = filtered;
            tbSeed.SelectionStart = Math.Min(caret, filtered.Length);
        }
        int len = tbSeed.Text.Length;
        lblSeedLen.Text = $"{len} / {Lfsr.Degree}";
        lblSeedLen.ForeColor = len == Lfsr.Degree ? Color.Green : Color.Red;
    }

    private bool ValidateInputs()
    {
        if (_inputFilePath == null)
        {
            MessageBox.Show("Сначала выберите входной файл.", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    private void SetStatus(string text) => lblStatus.Text = text;
}