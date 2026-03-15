namespace LfsrCipher;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        this.lblTitle = new Label();
        this.lblSubtitle = new Label();
        this.lblSeedLabel = new Label();
        this.tbSeed = new TextBox();
        this.lblSeedHint = new Label();
        this.lblSeedLen = new Label();
        this.lblInFile = new Label();
        this.lblInputFile = new Label();
        this.lblInputSize = new Label();
        this.btnOpenFile = new Button();
        this.lblOutFile = new Label();
        this.lblOutputFile = new Label();
        this.btnSaveFile = new Button();
        this.btnEncrypt = new Button();
        this.btnDecrypt = new Button();
        this.btnShowTacts = new Button();
        this.grpKeystream = new GroupBox();
        this.tbKeystreamBinary = new RichTextBox();
        this.grpInputBin = new GroupBox();
        this.tbInputBinary = new RichTextBox();
        this.grpOutputBin = new GroupBox();
        this.tbOutputBinary = new RichTextBox();
        this.grpTacts = new GroupBox();
        this.tbTacts = new RichTextBox();
        this.lblStatus = new Label();
        this.tlpMain = new TableLayoutPanel();
        this.pnlSeed = new Panel();
        this.tlpFiles = new TableLayoutPanel();
        this.tlpButtons = new FlowLayoutPanel();
        this.tlpPanels = new TableLayoutPanel();

        this.SuspendLayout();

        // ── Form ──────────────────────────────────────────────────────────────
        this.Text = "LFSR Stream Cipher — Вариант 8 (степень 30)";
        this.Size = new Size(1000, 820);
        this.MinimumSize = new Size(1000, 820);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Font = new Font("Segoe UI", 9.5f);

        // ── Status bar ────────────────────────────────────────────────────────
        this.lblStatus.Dock = DockStyle.Bottom;
        this.lblStatus.Height = 24;
        this.lblStatus.BorderStyle = BorderStyle.Fixed3D;
        this.lblStatus.ForeColor = Color.DimGray;
        this.lblStatus.Padding = new Padding(6, 0, 0, 0);
        this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;

        // ── Title ─────────────────────────────────────────────────────────────
        this.lblTitle.Text = "Потоковый шифр на основе LFSR";
        this.lblTitle.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
        this.lblTitle.Dock = DockStyle.Fill;
        this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;

        this.lblSubtitle.Text = "P(x) = x³⁰ + x¹⁶ + x¹⁵ + x + 1   |   Вариант 8   |   Степень: 30";
        this.lblSubtitle.ForeColor = Color.Gray;
        this.lblSubtitle.Dock = DockStyle.Fill;
        this.lblSubtitle.TextAlign = ContentAlignment.MiddleLeft;

        // ── Seed row — всё абсолютное, ничего не сжимается ───────────────────
        // label(200) + textbox(340 — ровно под 30 символов Courier 11pt) + gap(12) + hint(100) + counter(80)
        this.lblSeedLabel.Text = "Начальное состояние (30 бит):";
        this.lblSeedLabel.AutoSize = false;
        this.lblSeedLabel.Size = new Size(300, 32);
        this.lblSeedLabel.TextAlign = ContentAlignment.MiddleLeft;
        this.lblSeedLabel.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        this.lblSeedLabel.Location = new Point(0, 4);

        this.tbSeed.Font = new Font("Courier New", 9f);
        this.tbSeed.Size = new Size(340, 28);
        this.tbSeed.MaxLength = 30;
        this.tbSeed.Location = new Point(310, 5);
        this.tbSeed.TextChanged += tbSeed_TextChanged;

        this.lblSeedHint.Text = "Только 0 и 1";
        this.lblSeedHint.AutoSize = false;
        this.lblSeedHint.Size = new Size(130, 32);
        this.lblSeedHint.ForeColor = Color.Gray;
        this.lblSeedHint.TextAlign = ContentAlignment.MiddleCenter;
        this.lblSeedHint.Location = new Point(660, 4);

        this.lblSeedLen.Text = "0 / 30";
        this.lblSeedLen.AutoSize = false;
        this.lblSeedLen.Size = new Size(100, 32);
        this.lblSeedLen.ForeColor = Color.Red;
        this.lblSeedLen.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
        this.lblSeedLen.TextAlign = ContentAlignment.MiddleCenter;
        this.lblSeedLen.Location = new Point(780, 4);

        this.pnlSeed.Dock = DockStyle.Fill;
        this.pnlSeed.Controls.AddRange(new Control[]
            { lblSeedLabel, tbSeed, lblSeedHint, lblSeedLen });

        // ── Files rows ────────────────────────────────────────────────────────
        this.lblInFile.Text = "Входной файл:";
        this.lblInFile.Dock = DockStyle.Fill;
        this.lblInFile.TextAlign = ContentAlignment.MiddleLeft;
        this.lblInFile.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

        this.lblInputFile.Text = "не выбран";
        this.lblInputFile.ForeColor = Color.Gray;
        this.lblInputFile.Dock = DockStyle.Fill;
        this.lblInputFile.TextAlign = ContentAlignment.MiddleLeft;
        this.lblInputFile.AutoEllipsis = true;

        this.lblInputSize.Text = "";
        this.lblInputSize.ForeColor = Color.Gray;
        this.lblInputSize.Dock = DockStyle.Fill;
        this.lblInputSize.TextAlign = ContentAlignment.MiddleLeft;

        this.btnOpenFile.Text = "Выбрать файл...";
        this.btnOpenFile.Dock = DockStyle.Fill;
        this.btnOpenFile.Margin = new Padding(4, 5, 4, 3);
        this.btnOpenFile.Click += btnOpenFile_Click;

        this.lblOutFile.Text = "Выходной файл:";
        this.lblOutFile.Dock = DockStyle.Fill;
        this.lblOutFile.TextAlign = ContentAlignment.MiddleLeft;
        this.lblOutFile.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

        this.lblOutputFile.Text = "не выбран";
        this.lblOutputFile.ForeColor = Color.Gray;
        this.lblOutputFile.Dock = DockStyle.Fill;
        this.lblOutputFile.TextAlign = ContentAlignment.MiddleLeft;
        this.lblOutputFile.AutoEllipsis = true;

        this.btnSaveFile.Text = "Сохранить как...";
        this.btnSaveFile.Dock = DockStyle.Fill;
        this.btnSaveFile.Margin = new Padding(4, 3, 4, 5);
        this.btnSaveFile.Click += btnSaveFile_Click;

        this.tlpFiles.Dock = DockStyle.Fill;
        this.tlpFiles.ColumnCount = 4;
        this.tlpFiles.RowCount = 2;
        this.tlpFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
        this.tlpFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        this.tlpFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
        this.tlpFiles.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
        this.tlpFiles.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        this.tlpFiles.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        this.tlpFiles.Controls.Add(lblInFile, 0, 0);
        this.tlpFiles.Controls.Add(lblInputFile, 1, 0);
        this.tlpFiles.Controls.Add(lblInputSize, 2, 0);
        this.tlpFiles.Controls.Add(btnOpenFile, 3, 0);
        this.tlpFiles.Controls.Add(lblOutFile, 0, 1);
        this.tlpFiles.Controls.Add(lblOutputFile, 1, 1);
        this.tlpFiles.Controls.Add(btnSaveFile, 3, 1);

        // ── Action buttons ────────────────────────────────────────────────────
        this.btnEncrypt.Text = "Зашифровать";
        this.btnEncrypt.AutoSize = false;
        this.btnEncrypt.Size = new Size(200, 38);
        this.btnEncrypt.Margin = new Padding(0, 5, 12, 5);
        this.btnEncrypt.BackColor = Color.FromArgb(39, 174, 96);
        this.btnEncrypt.ForeColor = Color.White;
        this.btnEncrypt.FlatStyle = FlatStyle.Flat;
        this.btnEncrypt.FlatAppearance.BorderSize = 0;
        this.btnEncrypt.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
        this.btnEncrypt.Click += btnEncrypt_Click;

        this.btnDecrypt.Text = "Расшифровать";
        this.btnDecrypt.AutoSize = false;
        this.btnDecrypt.Size = new Size(200, 38);
        this.btnDecrypt.Margin = new Padding(0, 5, 12, 5);
        this.btnDecrypt.BackColor = Color.FromArgb(230, 126, 34);
        this.btnDecrypt.ForeColor = Color.White;
        this.btnDecrypt.FlatStyle = FlatStyle.Flat;
        this.btnDecrypt.FlatAppearance.BorderSize = 0;
        this.btnDecrypt.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
        this.btnDecrypt.Click += btnDecrypt_Click;

        this.btnShowTacts.Text = "Показать такты LFSR";
        this.btnShowTacts.AutoSize = false;
        this.btnShowTacts.Size = new Size(250, 38);
        this.btnShowTacts.Margin = new Padding(0, 5, 0, 5);
        this.btnShowTacts.BackColor = Color.FromArgb(142, 68, 173);
        this.btnShowTacts.ForeColor = Color.White;
        this.btnShowTacts.FlatStyle = FlatStyle.Flat;
        this.btnShowTacts.FlatAppearance.BorderSize = 0;
        this.btnShowTacts.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
        this.btnShowTacts.Click += btnShowTacts_Click;

        this.tlpButtons.Dock = DockStyle.Fill;
        this.tlpButtons.FlowDirection = FlowDirection.LeftToRight;
        this.tlpButtons.WrapContents = false;
        this.tlpButtons.Padding = new Padding(0);
        this.tlpButtons.Controls.Add(btnEncrypt);
        this.tlpButtons.Controls.Add(btnDecrypt);
        this.tlpButtons.Controls.Add(btnShowTacts);

        // ── 4 output panels 2×2 ───────────────────────────────────────────────
        var rtbFont = new Font("Courier New", 9f);

        void Setup(RichTextBox rtb, GroupBox grp, string title, bool wrap = true)
        {
            rtb.Dock = DockStyle.Fill; rtb.Font = rtbFont;
            rtb.ReadOnly = true; rtb.BackColor = SystemColors.Window;
            rtb.WordWrap = wrap;
            rtb.ScrollBars = wrap ? RichTextBoxScrollBars.Vertical : RichTextBoxScrollBars.Both;
            grp.Text = title; grp.Dock = DockStyle.Fill; grp.Padding = new Padding(4);
            grp.Controls.Add(rtb);
        }

        Setup(tbKeystreamBinary, grpKeystream, "Ключевой поток (первые 8 байт)");
        Setup(tbInputBinary, grpInputBin, "Входной файл — двоичный вид (первые 8 байт)");
        Setup(tbOutputBinary, grpOutputBin, "Выходной файл — двоичный вид (первые 8 байт)");
        Setup(tbTacts, grpTacts, "Такты LFSR", false);

        this.tlpPanels.Dock = DockStyle.Fill;
        this.tlpPanels.ColumnCount = 2;
        this.tlpPanels.RowCount = 2;
        this.tlpPanels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        this.tlpPanels.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        this.tlpPanels.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        this.tlpPanels.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        this.tlpPanels.Controls.Add(grpKeystream, 0, 0);
        this.tlpPanels.Controls.Add(grpInputBin, 1, 0);
        this.tlpPanels.Controls.Add(grpOutputBin, 0, 1);
        this.tlpPanels.Controls.Add(grpTacts, 1, 1);

        // ── Main layout ───────────────────────────────────────────────────────
        this.tlpMain.Dock = DockStyle.Fill;
        this.tlpMain.ColumnCount = 1;
        this.tlpMain.RowCount = 6;
        this.tlpMain.Padding = new Padding(10, 8, 10, 4);
        this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
        this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
        this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
        this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
        this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        this.tlpMain.Controls.Add(lblTitle, 0, 0);
        this.tlpMain.Controls.Add(lblSubtitle, 0, 1);
        this.tlpMain.Controls.Add(pnlSeed, 0, 2);
        this.tlpMain.Controls.Add(tlpFiles, 0, 3);
        this.tlpMain.Controls.Add(tlpButtons, 0, 4);
        this.tlpMain.Controls.Add(tlpPanels, 0, 5);

        this.Controls.Add(tlpMain);
        this.Controls.Add(lblStatus);
        this.ResumeLayout(false);
    }

    private Label lblTitle;
    private Label lblSubtitle;
    private Label lblSeedLabel;
    private TextBox tbSeed;
    private Label lblSeedHint;
    private Label lblSeedLen;
    private Label lblInFile;
    private Label lblInputFile;
    private Label lblInputSize;
    private Button btnOpenFile;
    private Label lblOutFile;
    private Label lblOutputFile;
    private Button btnSaveFile;
    private Button btnEncrypt;
    private Button btnDecrypt;
    private Button btnShowTacts;
    private GroupBox grpKeystream;
    private RichTextBox tbKeystreamBinary;
    private GroupBox grpInputBin;
    private RichTextBox tbInputBinary;
    private GroupBox grpOutputBin;
    private RichTextBox tbOutputBinary;
    private GroupBox grpTacts;
    private RichTextBox tbTacts;
    private Label lblStatus;
    private TableLayoutPanel tlpMain;
    private Panel pnlSeed;
    private TableLayoutPanel tlpFiles;
    private FlowLayoutPanel tlpButtons;
    private TableLayoutPanel tlpPanels;
}