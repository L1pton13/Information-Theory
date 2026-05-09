namespace DSA
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        // Controls — Params panel
        private System.Windows.Forms.Panel panelParams;
        private System.Windows.Forms.Label lblParamsTitle;
        private System.Windows.Forms.Label lblQ, lblP, lblH, lblX, lblH0, lblK;
        private System.Windows.Forms.TextBox txtQ, txtP, txtH, txtX, txtH0, txtK;
        private System.Windows.Forms.Button btnComputeG, btnComputeY;
        private System.Windows.Forms.Label lblGValue, lblYValue, lblKeysInfo;

        // Controls — Sign panel
        private System.Windows.Forms.Panel panelSign;
        private System.Windows.Forms.Label lblSignTitle;
        private System.Windows.Forms.Button btnChooseFile, btnPreviewHash, btnSign, btnSave;
        private System.Windows.Forms.Label lblFilePath, lblHashLabel, lblHashValue;
        private System.Windows.Forms.TextBox txtFileContent, txtSignResult, txtSignedFileContent;

        // Controls — Verify panel
        private System.Windows.Forms.Panel panelVerify;
        private System.Windows.Forms.Label lblVerifyTitle;
        private System.Windows.Forms.Button btnChooseVerifyFile, btnVerify;
        private System.Windows.Forms.Label lblVerifyFilePath;
        private System.Windows.Forms.TextBox txtVerifyContent, txtVerifyResult;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Text = "ЭЦП на основе DSA — Лабораторная работа 4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(1280, 820);
            this.Size = new System.Drawing.Size(1380, 900);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            // ===============================================================
            // COLORS / STYLE HELPERS
            // ===============================================================
            var headerFont = new System.Drawing.Font("Segoe UI Semibold", 11f);
            var labelFont = new System.Drawing.Font("Segoe UI", 9.5f);
            var monoFont = new System.Drawing.Font("Consolas", 9f);
            var headerBg = System.Drawing.Color.FromArgb(40, 60, 100);
            var panelBg = System.Drawing.Color.FromArgb(250, 251, 253);
            var borderColor = System.Drawing.Color.FromArgb(200, 210, 225);
            var btnBlue = System.Drawing.Color.FromArgb(30, 100, 200);
            var btnPurple = System.Drawing.Color.FromArgb(110, 50, 180);
            var btnGreen = System.Drawing.Color.FromArgb(30, 140, 80);
            var btnDark = System.Drawing.Color.FromArgb(50, 60, 80);

            System.Action<System.Windows.Forms.Button, System.Drawing.Color> styleBtn = (b, c) =>
            {
                b.BackColor = c;
                b.ForeColor = System.Drawing.Color.White;
                b.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5f);
                b.Cursor = System.Windows.Forms.Cursors.Hand;
                b.Height = 34;
            };

            System.Action<System.Windows.Forms.TextBox> styleTxt = (t) =>
            {
                t.Font = labelFont;
                t.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                t.Height = 26;
            };

            System.Action<System.Windows.Forms.TextBox> styleMultiTxt = (t) =>
            {
                t.Font = monoFont;
                t.Multiline = true;
                t.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                t.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                t.ReadOnly = true;
                t.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
            };

            // ===============================================================
            // MAIN LAYOUT — 3-column TableLayoutPanel
            // ===============================================================
            var mainLayout = new System.Windows.Forms.TableLayoutPanel();
            mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayout.ColumnCount = 3;
            mainLayout.RowCount = 1;
            mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 290));
            mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50));
            mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50));
            mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100));
            mainLayout.Padding = new System.Windows.Forms.Padding(10);
            mainLayout.BackColor = System.Drawing.Color.FromArgb(230, 233, 240);
            this.Controls.Add(mainLayout);

            // ===============================================================
            // PANEL: ПАРАМЕТРЫ (left)
            // ===============================================================
            panelParams = new System.Windows.Forms.Panel();
            panelParams.Dock = System.Windows.Forms.DockStyle.Fill;
            panelParams.BackColor = panelBg;
            panelParams.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            mainLayout.Controls.Add(panelParams, 0, 0);

            var paramFlow = new System.Windows.Forms.FlowLayoutPanel();
            paramFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            paramFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            paramFlow.WrapContents = false;
            paramFlow.AutoScroll = true;
            paramFlow.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            paramFlow.Width = 290;
            panelParams.Controls.Add(paramFlow);

            // Header
            var hdrParams = new System.Windows.Forms.Panel();
            hdrParams.BackColor = headerBg;
            hdrParams.Height = 38;
            hdrParams.Width = 290;
            hdrParams.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            var hdrLblP = new System.Windows.Forms.Label { Text = "  Параметры DSA", ForeColor = System.Drawing.Color.White, Font = headerFont, Dock = System.Windows.Forms.DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
            hdrParams.Controls.Add(hdrLblP);
            paramFlow.Controls.Add(hdrParams);

            System.Action<string, System.Windows.Forms.Control, System.Windows.Forms.FlowLayoutPanel> addRow = (labelText, ctrl, parent) =>
            {
                var lbl = new System.Windows.Forms.Label { Text = labelText, Font = labelFont, Width = 256, Height = 20, Margin = new System.Windows.Forms.Padding(0, 6, 0, 2), ForeColor = System.Drawing.Color.FromArgb(50, 60, 80) };
                ctrl.Width = 256;
                ctrl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
                parent.Controls.Add(lbl);
                parent.Controls.Add(ctrl);
            };

            // q
            txtQ = new System.Windows.Forms.TextBox(); styleTxt(txtQ);
            txtQ.Leave += txtQ_Leave;
            addRow("q (простое число):", txtQ, paramFlow);

            // p
            txtP = new System.Windows.Forms.TextBox(); styleTxt(txtP);
            txtP.Leave += txtP_Leave;
            addRow("p (простое, (p−1) mod q = 0):", txtP, paramFlow);

            // h
            txtH = new System.Windows.Forms.TextBox(); styleTxt(txtH);
            txtH.Leave += txtH_Leave;
            addRow("h (целое, 1 < h < p−1):", txtH, paramFlow);

            // Button: вычислить g
            btnComputeG = new System.Windows.Forms.Button { Text = "Вычислить g", Width = 256, Margin = new System.Windows.Forms.Padding(0, 6, 0, 2) };
            styleBtn(btnComputeG, btnBlue);
            btnComputeG.Click += btnComputeG_Click;
            paramFlow.Controls.Add(btnComputeG);

            // g value label
            lblGValue = new System.Windows.Forms.Label { Text = "g = ...", Width = 256, Height = 36, Font = monoFont, ForeColor = System.Drawing.Color.Gray, Margin = new System.Windows.Forms.Padding(0, 2, 0, 8), AutoSize = false };
            paramFlow.Controls.Add(lblGValue);

            // Separator
            var sep1 = new System.Windows.Forms.Label { Width = 256, Height = 1, BackColor = borderColor, Margin = new System.Windows.Forms.Padding(0, 4, 0, 10) };
            paramFlow.Controls.Add(sep1);

            // x
            txtX = new System.Windows.Forms.TextBox(); styleTxt(txtX);
            txtX.Leave += txtX_Leave;
            addRow("x (закрытый ключ, 0 < x < q):", txtX, paramFlow);

            // Button: вычислить y
            btnComputeY = new System.Windows.Forms.Button { Text = "Вычислить открытый ключ y", Width = 256, Margin = new System.Windows.Forms.Padding(0, 6, 0, 2) };
            styleBtn(btnComputeY, btnPurple);
            btnComputeY.Click += btnComputeY_Click;
            paramFlow.Controls.Add(btnComputeY);

            // y value label
            lblYValue = new System.Windows.Forms.Label { Text = "y = ...", Width = 256, Height = 36, Font = monoFont, ForeColor = System.Drawing.Color.Gray, Margin = new System.Windows.Forms.Padding(0, 2, 0, 4), AutoSize = false };
            paramFlow.Controls.Add(lblYValue);

            // keys info
            lblKeysInfo = new System.Windows.Forms.Label { Text = "", Width = 256, Height = 60, Font = new System.Drawing.Font("Segoe UI", 8.5f), ForeColor = System.Drawing.Color.Gray, Margin = new System.Windows.Forms.Padding(0, 0, 0, 8), AutoSize = false };
            paramFlow.Controls.Add(lblKeysInfo);

            // Separator
            var sep2 = new System.Windows.Forms.Label { Width = 256, Height = 1, BackColor = borderColor, Margin = new System.Windows.Forms.Padding(0, 4, 0, 10) };
            paramFlow.Controls.Add(sep2);

            // H0
            txtH0 = new System.Windows.Forms.TextBox { Text = "100" }; styleTxt(txtH0);
            txtH0.Leave += txtH0_Leave;
            SetFieldOk(txtH0);
            addRow("H₀ (начальное значение хэша):", txtH0, paramFlow);

            // k
            txtK = new System.Windows.Forms.TextBox(); styleTxt(txtK);
            txtK.Leave += txtK_Leave;
            addRow("k (случайное, 0 < k < q):", txtK, paramFlow);

            // ===============================================================
            // PANEL: ПОДПИСАНИЕ (middle)
            // ===============================================================
            panelSign = new System.Windows.Forms.Panel();
            panelSign.Dock = System.Windows.Forms.DockStyle.Fill;
            panelSign.BackColor = panelBg;
            panelSign.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            mainLayout.Controls.Add(panelSign, 1, 0);

            var signLayout = new System.Windows.Forms.TableLayoutPanel();
            signLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            signLayout.ColumnCount = 1;
            signLayout.RowCount = 10;
            signLayout.Padding = new System.Windows.Forms.Padding(0);

            // rows: header, btn choose, lbl path, txt file content, btn preview hash, hash label, btn sign, txt sign result, txt signed content, btn save
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38)); // header
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38)); // btn choose
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24)); // lbl path
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15));  // file content
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38)); // btn preview
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28)); // hash label
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38)); // btn sign
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42));  // sign result
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30));  // signed content
            signLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42)); // save btn

            panelSign.Controls.Add(signLayout);

            // Header
            var hdrSign = new System.Windows.Forms.Panel { BackColor = headerBg, Dock = System.Windows.Forms.DockStyle.Fill };
            var hdrSignLbl = new System.Windows.Forms.Label { Text = "  Подписание файла", ForeColor = System.Drawing.Color.White, Font = headerFont, Dock = System.Windows.Forms.DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
            hdrSign.Controls.Add(hdrSignLbl);
            signLayout.Controls.Add(hdrSign, 0, 0);

            // btn choose file
            btnChooseFile = new System.Windows.Forms.Button { Text = "📂  Выбрать файл для подписи", Dock = System.Windows.Forms.DockStyle.Fill, Margin = new System.Windows.Forms.Padding(8, 4, 8, 2) };
            styleBtn(btnChooseFile, btnDark);
            btnChooseFile.Click += btnChooseFile_Click;
            signLayout.Controls.Add(btnChooseFile, 0, 1);

            // file path label
            lblFilePath = new System.Windows.Forms.Label { Text = "...", Dock = System.Windows.Forms.DockStyle.Fill, Font = new System.Drawing.Font("Segoe UI", 8.5f), ForeColor = System.Drawing.Color.Gray, Padding = new System.Windows.Forms.Padding(10, 2, 10, 0), AutoEllipsis = true };
            signLayout.Controls.Add(lblFilePath, 0, 2);

            // file content preview
            txtFileContent = new System.Windows.Forms.TextBox(); styleMultiTxt(txtFileContent);
            txtFileContent.Dock = System.Windows.Forms.DockStyle.Fill;
            txtFileContent.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
            signLayout.Controls.Add(txtFileContent, 0, 3);

            // btn preview hash
            btnPreviewHash = new System.Windows.Forms.Button { Text = "Предпросмотр хэша из файла", Dock = System.Windows.Forms.DockStyle.Fill, Margin = new System.Windows.Forms.Padding(8, 4, 8, 2) };
            styleBtn(btnPreviewHash, System.Drawing.Color.FromArgb(0, 130, 160));
            btnPreviewHash.Click += btnPreviewHash_Click;
            signLayout.Controls.Add(btnPreviewHash, 0, 4);

            // hash value label
            var hashPanel = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10, 2, 10, 0) };
            var hashPrefixLbl = new System.Windows.Forms.Label { Text = "H = ", Font = new System.Drawing.Font("Segoe UI", 9.5f), ForeColor = System.Drawing.Color.FromArgb(50, 60, 80), AutoSize = true, Location = new System.Drawing.Point(0, 4) };
            lblHashValue = new System.Windows.Forms.Label { Text = "", Font = new System.Drawing.Font("Consolas", 10f, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(200, 80, 0), AutoSize = true, Location = new System.Drawing.Point(30, 4) };
            hashPanel.Controls.Add(hashPrefixLbl);
            hashPanel.Controls.Add(lblHashValue);
            signLayout.Controls.Add(hashPanel, 0, 5);

            // btn sign
            btnSign = new System.Windows.Forms.Button { Text = "✒  Подписать файл", Dock = System.Windows.Forms.DockStyle.Fill, Margin = new System.Windows.Forms.Padding(8, 2, 8, 2) };
            styleBtn(btnSign, btnGreen);
            btnSign.Click += btnSign_Click;
            signLayout.Controls.Add(btnSign, 0, 6);

            // sign result (steps)
            txtSignResult = new System.Windows.Forms.TextBox(); styleMultiTxt(txtSignResult);
            txtSignResult.Dock = System.Windows.Forms.DockStyle.Fill;
            txtSignResult.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
            signLayout.Controls.Add(txtSignResult, 0, 7);

            // signed file content
            txtSignedFileContent = new System.Windows.Forms.TextBox(); styleMultiTxt(txtSignedFileContent);
            txtSignedFileContent.Dock = System.Windows.Forms.DockStyle.Fill;
            txtSignedFileContent.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
            txtSignedFileContent.BackColor = System.Drawing.Color.FromArgb(240, 248, 240);
            signLayout.Controls.Add(txtSignedFileContent, 0, 8);

            // save button
            btnSave = new System.Windows.Forms.Button { Text = "💾  Сохранить подписанный файл", Dock = System.Windows.Forms.DockStyle.Fill, Margin = new System.Windows.Forms.Padding(8, 4, 8, 8) };
            styleBtn(btnSave, System.Drawing.Color.FromArgb(140, 90, 20));
            btnSave.Click += btnSave_Click;
            signLayout.Controls.Add(btnSave, 0, 9);

            // ===============================================================
            // PANEL: ПРОВЕРКА (right)
            // ===============================================================
            panelVerify = new System.Windows.Forms.Panel();
            panelVerify.Dock = System.Windows.Forms.DockStyle.Fill;
            panelVerify.BackColor = panelBg;
            panelVerify.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            mainLayout.Controls.Add(panelVerify, 2, 0);

            var verifyLayout = new System.Windows.Forms.TableLayoutPanel();
            verifyLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            verifyLayout.ColumnCount = 1;
            verifyLayout.RowCount = 6;
            verifyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38)); // header
            verifyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38)); // btn choose
            verifyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24)); // lbl path
            verifyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25));  // file content
            verifyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38)); // btn verify
            verifyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75));  // verify result
            panelVerify.Controls.Add(verifyLayout);

            // Header
            var hdrVerify = new System.Windows.Forms.Panel { BackColor = System.Drawing.Color.FromArgb(30, 100, 60), Dock = System.Windows.Forms.DockStyle.Fill };
            var hdrVerifyLbl = new System.Windows.Forms.Label { Text = "  Проверка ЭЦП", ForeColor = System.Drawing.Color.White, Font = headerFont, Dock = System.Windows.Forms.DockStyle.Fill, TextAlign = System.Drawing.ContentAlignment.MiddleLeft };
            hdrVerify.Controls.Add(hdrVerifyLbl);
            verifyLayout.Controls.Add(hdrVerify, 0, 0);

            // btn choose verify file
            btnChooseVerifyFile = new System.Windows.Forms.Button { Text = "📂  Выбрать файл для проверки", Dock = System.Windows.Forms.DockStyle.Fill, Margin = new System.Windows.Forms.Padding(8, 4, 8, 2) };
            styleBtn(btnChooseVerifyFile, btnDark);
            btnChooseVerifyFile.Click += btnChooseVerifyFile_Click;
            verifyLayout.Controls.Add(btnChooseVerifyFile, 0, 1);

            // verify file path
            lblVerifyFilePath = new System.Windows.Forms.Label { Text = "...", Dock = System.Windows.Forms.DockStyle.Fill, Font = new System.Drawing.Font("Segoe UI", 8.5f), ForeColor = System.Drawing.Color.Gray, Padding = new System.Windows.Forms.Padding(10, 2, 10, 0), AutoEllipsis = true };
            verifyLayout.Controls.Add(lblVerifyFilePath, 0, 2);

            // verify file content
            txtVerifyContent = new System.Windows.Forms.TextBox(); styleMultiTxt(txtVerifyContent);
            txtVerifyContent.Dock = System.Windows.Forms.DockStyle.Fill;
            txtVerifyContent.Margin = new System.Windows.Forms.Padding(8, 2, 8, 2);
            verifyLayout.Controls.Add(txtVerifyContent, 0, 3);

            // btn verify
            btnVerify = new System.Windows.Forms.Button { Text = "🔍  ПРОВЕРИТЬ ПОДПИСЬ", Dock = System.Windows.Forms.DockStyle.Fill, Margin = new System.Windows.Forms.Padding(8, 4, 8, 2) };
            styleBtn(btnVerify, System.Drawing.Color.FromArgb(30, 100, 60));
            btnVerify.Font = new System.Drawing.Font("Segoe UI Semibold", 10f);
            btnVerify.Click += btnVerify_Click;
            verifyLayout.Controls.Add(btnVerify, 0, 4);

            // verify result
            txtVerifyResult = new System.Windows.Forms.TextBox(); styleMultiTxt(txtVerifyResult);
            txtVerifyResult.Dock = System.Windows.Forms.DockStyle.Fill;
            txtVerifyResult.Margin = new System.Windows.Forms.Padding(8, 2, 8, 8);
            verifyLayout.Controls.Add(txtVerifyResult, 0, 5);

            this.ResumeLayout(false);
        }
    }
}
