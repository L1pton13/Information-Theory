namespace RabinCipher
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tbP = new TextBox();
            tbQ = new TextBox();
            lblP = new Label();
            lblQ = new Label();
            lblN = new Label();
            lblB = new Label();
            tbN = new TextBox();
            tbB = new TextBox();
            tableLayoutPanelInput = new TableLayoutPanel();
            rtbInput = new RichTextBox();
            tableLayoutPanelEncDec = new TableLayoutPanel();
            btnEncrypt = new Button();
            btnDecrypt = new Button();
            rbOneByte = new RadioButton();
            rbFourBytes = new RadioButton();
            tableLayoutPanelOutput = new TableLayoutPanel();
            rtbOutput = new RichTextBox();
            tableLayoutPanelSaveTo = new TableLayoutPanel();
            lblSaveToFile = new Label();
            btnSaveTo = new Button();
            buttonClear = new Button();
            tableLayoutPanelOpenFile = new TableLayoutPanel();
            lblReadFile = new Label();
            btnOpenFile = new Button();
            errorProvider = new ErrorProvider(components);
            tableLayoutPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanelInput.SuspendLayout();
            tableLayoutPanelEncDec.SuspendLayout();
            tableLayoutPanelOutput.SuspendLayout();
            tableLayoutPanelSaveTo.SuspendLayout();
            tableLayoutPanelOpenFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel.Controls.Add(tableLayoutPanelInput, 0, 1);
            tableLayoutPanel.Controls.Add(tableLayoutPanelOutput, 1, 1);
            tableLayoutPanel.Controls.Add(tableLayoutPanelOpenFile, 1, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            tableLayoutPanel.Size = new Size(800, 450);
            tableLayoutPanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.Controls.Add(tbP, 0, 0);
            tableLayoutPanel1.Controls.Add(tbQ, 0, 1);
            tableLayoutPanel1.Controls.Add(lblP, 1, 0);
            tableLayoutPanel1.Controls.Add(lblQ, 1, 1);
            tableLayoutPanel1.Controls.Add(lblN, 1, 2);
            tableLayoutPanel1.Controls.Add(lblB, 1, 3);
            tableLayoutPanel1.Controls.Add(tbN, 0, 2);
            tableLayoutPanel1.Controls.Add(tbB, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(394, 129);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tbP
            // 
            tbP.Dock = DockStyle.Fill;
            tbP.Location = new Point(3, 3);
            tbP.Name = "tbP";
            tbP.Size = new Size(112, 31);
            tbP.TabIndex = 0;
            // 
            // tbQ
            // 
            tbQ.Dock = DockStyle.Fill;
            tbQ.Location = new Point(3, 35);
            tbQ.Name = "tbQ";
            tbQ.Size = new Size(112, 31);
            tbQ.TabIndex = 1;
            // 
            // lblP
            // 
            lblP.AutoSize = true;
            lblP.Dock = DockStyle.Fill;
            lblP.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblP.Location = new Point(121, 0);
            lblP.Name = "lblP";
            lblP.Size = new Size(270, 32);
            lblP.TabIndex = 2;
            lblP.Text = "    Введите простое p";
            // 
            // lblQ
            // 
            lblQ.AutoSize = true;
            lblQ.Dock = DockStyle.Fill;
            lblQ.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblQ.Location = new Point(121, 32);
            lblQ.Name = "lblQ";
            lblQ.Size = new Size(270, 32);
            lblQ.TabIndex = 3;
            lblQ.Text = "    Введите простое q";
            // 
            // lblN
            // 
            lblN.AutoSize = true;
            lblN.Dock = DockStyle.Fill;
            lblN.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblN.Location = new Point(121, 64);
            lblN.Name = "lblN";
            lblN.Size = new Size(270, 32);
            lblN.TabIndex = 4;
            lblN.Text = "    Полученное n=p*q";
            // 
            // lblB
            // 
            lblB.AutoSize = true;
            lblB.Dock = DockStyle.Fill;
            lblB.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblB.Location = new Point(121, 96);
            lblB.Name = "lblB";
            lblB.Size = new Size(270, 33);
            lblB.TabIndex = 5;
            lblB.Text = "    Введите b<n";
            // 
            // tbN
            // 
            tbN.Dock = DockStyle.Fill;
            tbN.Location = new Point(3, 67);
            tbN.Name = "tbN";
            tbN.ReadOnly = true;
            tbN.Size = new Size(112, 31);
            tbN.TabIndex = 6;
            // 
            // tbB
            // 
            tbB.Dock = DockStyle.Fill;
            tbB.Enabled = false;
            tbB.Location = new Point(3, 99);
            tbB.Name = "tbB";
            tbB.Size = new Size(112, 31);
            tbB.TabIndex = 7;
            // 
            // tableLayoutPanelInput
            // 
            tableLayoutPanelInput.ColumnCount = 1;
            tableLayoutPanelInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelInput.Controls.Add(rtbInput, 0, 0);
            tableLayoutPanelInput.Controls.Add(tableLayoutPanelEncDec, 0, 1);
            tableLayoutPanelInput.Dock = DockStyle.Fill;
            tableLayoutPanelInput.Location = new Point(3, 138);
            tableLayoutPanelInput.Name = "tableLayoutPanelInput";
            tableLayoutPanelInput.RowCount = 2;
            tableLayoutPanelInput.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelInput.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelInput.Size = new Size(394, 309);
            tableLayoutPanelInput.TabIndex = 1;
            // 
            // rtbInput
            // 
            rtbInput.Dock = DockStyle.Fill;
            rtbInput.Location = new Point(3, 3);
            rtbInput.Name = "rtbInput";
            rtbInput.ReadOnly = true;
            rtbInput.Size = new Size(388, 148);
            rtbInput.TabIndex = 0;
            rtbInput.Text = "";
            // 
            // tableLayoutPanelEncDec
            // 
            tableLayoutPanelEncDec.ColumnCount = 2;
            tableLayoutPanelEncDec.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelEncDec.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelEncDec.Controls.Add(btnEncrypt, 0, 0);
            tableLayoutPanelEncDec.Controls.Add(btnDecrypt, 1, 0);
            tableLayoutPanelEncDec.Controls.Add(rbOneByte, 0, 1);
            tableLayoutPanelEncDec.Controls.Add(rbFourBytes, 1, 1);
            tableLayoutPanelEncDec.Dock = DockStyle.Fill;
            tableLayoutPanelEncDec.Location = new Point(3, 157);
            tableLayoutPanelEncDec.Name = "tableLayoutPanelEncDec";
            tableLayoutPanelEncDec.RowCount = 2;
            tableLayoutPanelEncDec.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelEncDec.RowStyles.Add(new RowStyle(SizeType.Absolute, 63F));
            tableLayoutPanelEncDec.Size = new Size(388, 149);
            tableLayoutPanelEncDec.TabIndex = 1;
            // 
            // btnEncrypt
            // 
            btnEncrypt.Dock = DockStyle.Top;
            btnEncrypt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnEncrypt.Location = new Point(3, 3);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(188, 34);
            btnEncrypt.TabIndex = 0;
            btnEncrypt.Text = "Шифровать";
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Click += btnEncrypt_Click;
            // 
            // btnDecrypt
            // 
            btnDecrypt.Dock = DockStyle.Top;
            btnDecrypt.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnDecrypt.Location = new Point(197, 3);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(188, 34);
            btnDecrypt.TabIndex = 1;
            btnDecrypt.Text = "Дешифровать";
            btnDecrypt.UseVisualStyleBackColor = true;
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // rbOneByte
            // 
            rbOneByte.AutoSize = true;
            rbOneByte.Checked = true;
            rbOneByte.Dock = DockStyle.Top;
            rbOneByte.Location = new Point(3, 89);
            rbOneByte.Name = "rbOneByte";
            rbOneByte.Size = new Size(188, 29);
            rbOneByte.TabIndex = 2;
            rbOneByte.TabStop = true;
            rbOneByte.Text = "по 1 байту";
            rbOneByte.UseVisualStyleBackColor = true;
            rbOneByte.CheckedChanged += rbOneByte_CheckedChanged;
            // 
            // rbFourBytes
            // 
            rbFourBytes.AutoSize = true;
            rbFourBytes.Dock = DockStyle.Top;
            rbFourBytes.Location = new Point(197, 89);
            rbFourBytes.Name = "rbFourBytes";
            rbFourBytes.Size = new Size(188, 29);
            rbFourBytes.TabIndex = 3;
            rbFourBytes.TabStop = true;
            rbFourBytes.Text = "по 4 байта";
            rbFourBytes.UseVisualStyleBackColor = true;
            rbFourBytes.CheckedChanged += rbFourBytes_CheckedChanged;
            // 
            // tableLayoutPanelOutput
            // 
            tableLayoutPanelOutput.ColumnCount = 1;
            tableLayoutPanelOutput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelOutput.Controls.Add(rtbOutput, 0, 0);
            tableLayoutPanelOutput.Controls.Add(tableLayoutPanelSaveTo, 0, 1);
            tableLayoutPanelOutput.Dock = DockStyle.Fill;
            tableLayoutPanelOutput.Location = new Point(403, 138);
            tableLayoutPanelOutput.Name = "tableLayoutPanelOutput";
            tableLayoutPanelOutput.RowCount = 2;
            tableLayoutPanelOutput.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelOutput.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelOutput.Size = new Size(394, 309);
            tableLayoutPanelOutput.TabIndex = 2;
            // 
            // rtbOutput
            // 
            rtbOutput.Dock = DockStyle.Fill;
            rtbOutput.Location = new Point(3, 3);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(388, 148);
            rtbOutput.TabIndex = 0;
            rtbOutput.Text = "";
            // 
            // tableLayoutPanelSaveTo
            // 
            tableLayoutPanelSaveTo.ColumnCount = 2;
            tableLayoutPanelSaveTo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelSaveTo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelSaveTo.Controls.Add(lblSaveToFile, 0, 0);
            tableLayoutPanelSaveTo.Controls.Add(btnSaveTo, 1, 0);
            tableLayoutPanelSaveTo.Controls.Add(buttonClear, 0, 1);
            tableLayoutPanelSaveTo.Dock = DockStyle.Fill;
            tableLayoutPanelSaveTo.Location = new Point(3, 157);
            tableLayoutPanelSaveTo.Name = "tableLayoutPanelSaveTo";
            tableLayoutPanelSaveTo.RowCount = 2;
            tableLayoutPanelSaveTo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelSaveTo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelSaveTo.Size = new Size(388, 149);
            tableLayoutPanelSaveTo.TabIndex = 1;
            // 
            // lblSaveToFile
            // 
            lblSaveToFile.AutoSize = true;
            lblSaveToFile.Dock = DockStyle.Top;
            lblSaveToFile.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblSaveToFile.Location = new Point(3, 0);
            lblSaveToFile.Name = "lblSaveToFile";
            lblSaveToFile.Size = new Size(188, 64);
            lblSaveToFile.TabIndex = 0;
            lblSaveToFile.Text = "Выберите файл для сохранения";
            // 
            // btnSaveTo
            // 
            btnSaveTo.Dock = DockStyle.Top;
            btnSaveTo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnSaveTo.Location = new Point(197, 3);
            btnSaveTo.Name = "btnSaveTo";
            btnSaveTo.Size = new Size(188, 34);
            btnSaveTo.TabIndex = 1;
            btnSaveTo.Text = "Сохранить";
            btnSaveTo.UseVisualStyleBackColor = true;
            btnSaveTo.Click += btnSaveTo_Click;
            // 
            // buttonClear
            // 
            buttonClear.Dock = DockStyle.Top;
            buttonClear.Location = new Point(3, 77);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(188, 34);
            buttonClear.TabIndex = 2;
            buttonClear.Text = "Очистить";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // tableLayoutPanelOpenFile
            // 
            tableLayoutPanelOpenFile.ColumnCount = 2;
            tableLayoutPanelOpenFile.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanelOpenFile.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanelOpenFile.Controls.Add(lblReadFile, 0, 0);
            tableLayoutPanelOpenFile.Controls.Add(btnOpenFile, 1, 0);
            tableLayoutPanelOpenFile.Dock = DockStyle.Fill;
            tableLayoutPanelOpenFile.Location = new Point(403, 3);
            tableLayoutPanelOpenFile.Name = "tableLayoutPanelOpenFile";
            tableLayoutPanelOpenFile.RowCount = 1;
            tableLayoutPanelOpenFile.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelOpenFile.Size = new Size(394, 129);
            tableLayoutPanelOpenFile.TabIndex = 3;
            // 
            // lblReadFile
            // 
            lblReadFile.AutoSize = true;
            lblReadFile.Dock = DockStyle.Top;
            lblReadFile.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblReadFile.Location = new Point(3, 0);
            lblReadFile.Name = "lblReadFile";
            lblReadFile.Size = new Size(250, 64);
            lblReadFile.TabIndex = 0;
            lblReadFile.Text = "Выберите файл для открытия";
            // 
            // btnOpenFile
            // 
            btnOpenFile.Dock = DockStyle.Top;
            btnOpenFile.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnOpenFile.Location = new Point(259, 3);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(132, 34);
            btnOpenFile.TabIndex = 1;
            btnOpenFile.Text = "Открыть";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel);
            MinimumSize = new Size(822, 506);
            Name = "MainForm";
            Text = "Rabin Cipher";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanelInput.ResumeLayout(false);
            tableLayoutPanelEncDec.ResumeLayout(false);
            tableLayoutPanelEncDec.PerformLayout();
            tableLayoutPanelOutput.ResumeLayout(false);
            tableLayoutPanelSaveTo.ResumeLayout(false);
            tableLayoutPanelSaveTo.PerformLayout();
            tableLayoutPanelOpenFile.ResumeLayout(false);
            tableLayoutPanelOpenFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox tbP;
        private TextBox tbQ;
        private Label lblP;
        private Label lblQ;
        private Label lblN;
        private Label lblB;
        private TextBox tbN;
        private TextBox tbB;
        private TableLayoutPanel tableLayoutPanelInput;
        private RichTextBox rtbInput;
        private TableLayoutPanel tableLayoutPanelOutput;
        private RichTextBox rtbOutput;
        private TableLayoutPanel tableLayoutPanelEncDec;
        private TableLayoutPanel tableLayoutPanelOpenFile;
        private Label lblReadFile;
        private Button btnOpenFile;
        private Button btnEncrypt;
        private Button btnDecrypt;
        private TableLayoutPanel tableLayoutPanelSaveTo;
        private Label lblSaveToFile;
        private Button btnSaveTo;
        private ErrorProvider errorProvider;
        private RadioButton rbOneByte;
        private RadioButton rbFourBytes;
        private Button buttonClear;
    }
}
