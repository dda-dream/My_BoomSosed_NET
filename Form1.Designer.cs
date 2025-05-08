namespace My_BoomSosed_NET
{
    partial class BoomSosed_MainForm
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
            ctrl_LST = new ListBox();
            btnStart = new Button();
            ctrl_Speed = new TextBox();
            label_Speed = new Label();
            ctrlVisualBoom = new TableLayoutPanel();
            groupBoxVisualBoom = new GroupBox();
            label1 = new Label();
            ctrl_FillRatio = new TextBox();
            ctrlLog = new RichTextBox();
            btnRecalcParams = new Button();
            timer_boom = new System.Windows.Forms.Timer(components);
            ctrl_FilesInLST = new ListBox();
            label2 = new Label();
            groupBoxVisualBoom.SuspendLayout();
            SuspendLayout();
            // 
            // ctrl_LST
            // 
            ctrl_LST.FormattingEnabled = true;
            ctrl_LST.ItemHeight = 15;
            ctrl_LST.Location = new Point(12, 12);
            ctrl_LST.Name = "ctrl_LST";
            ctrl_LST.Size = new Size(294, 49);
            ctrl_LST.TabIndex = 1;
            ctrl_LST.SelectedIndexChanged += ctrl_LST_SelectedIndexChanged;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(648, 38);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(81, 23);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start/Stop";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // ctrl_Speed
            // 
            ctrl_Speed.Location = new Point(312, 38);
            ctrl_Speed.Name = "ctrl_Speed";
            ctrl_Speed.Size = new Size(100, 23);
            ctrl_Speed.TabIndex = 3;
            ctrl_Speed.Text = "5";
            ctrl_Speed.TextAlign = HorizontalAlignment.Center;
            // 
            // label_Speed
            // 
            label_Speed.AutoSize = true;
            label_Speed.Location = new Point(312, 20);
            label_Speed.Name = "label_Speed";
            label_Speed.Size = new Size(101, 15);
            label_Speed.TabIndex = 4;
            label_Speed.Text = "1 шаг за 5 секунд";
            // 
            // ctrlVisualBoom
            // 
            ctrlVisualBoom.ColumnCount = 1;
            ctrlVisualBoom.ColumnStyles.Add(new ColumnStyle());
            ctrlVisualBoom.Location = new Point(86, 164);
            ctrlVisualBoom.Name = "ctrlVisualBoom";
            ctrlVisualBoom.RowCount = 1;
            ctrlVisualBoom.RowStyles.Add(new RowStyle());
            ctrlVisualBoom.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            ctrlVisualBoom.Size = new Size(106, 84);
            ctrlVisualBoom.TabIndex = 5;
            // 
            // groupBoxVisualBoom
            // 
            groupBoxVisualBoom.Controls.Add(ctrlVisualBoom);
            groupBoxVisualBoom.Location = new Point(12, 77);
            groupBoxVisualBoom.Name = "groupBoxVisualBoom";
            groupBoxVisualBoom.Size = new Size(400, 329);
            groupBoxVisualBoom.TabIndex = 6;
            groupBoxVisualBoom.TabStop = false;
            groupBoxVisualBoom.Text = "Визуальный план BOOM";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(419, 20);
            label1.Name = "label1";
            label1.Size = new Size(125, 15);
            label1.TabIndex = 8;
            label1.Text = "коэфф заполнения %";
            // 
            // ctrl_FillRatio
            // 
            ctrl_FillRatio.Location = new Point(419, 38);
            ctrl_FillRatio.Name = "ctrl_FillRatio";
            ctrl_FillRatio.Size = new Size(125, 23);
            ctrl_FillRatio.TabIndex = 7;
            ctrl_FillRatio.Text = "10";
            ctrl_FillRatio.TextAlign = HorizontalAlignment.Center;
            // 
            // ctrlLog
            // 
            ctrlLog.Location = new Point(419, 77);
            ctrlLog.Name = "ctrlLog";
            ctrlLog.Size = new Size(310, 160);
            ctrlLog.TabIndex = 9;
            ctrlLog.Text = "";
            // 
            // btnRecalcParams
            // 
            btnRecalcParams.Location = new Point(550, 38);
            btnRecalcParams.Name = "btnRecalcParams";
            btnRecalcParams.Size = new Size(92, 23);
            btnRecalcParams.TabIndex = 10;
            btnRecalcParams.Text = "Recalc params";
            btnRecalcParams.UseVisualStyleBackColor = true;
            btnRecalcParams.Click += btnRecalcParams_Click;
            // 
            // ctrl_FilesInLST
            // 
            ctrl_FilesInLST.FormattingEnabled = true;
            ctrl_FilesInLST.ItemHeight = 15;
            ctrl_FilesInLST.Location = new Point(418, 296);
            ctrl_FilesInLST.Name = "ctrl_FilesInLST";
            ctrl_FilesInLST.Size = new Size(311, 109);
            ctrl_FilesInLST.TabIndex = 11;
            ctrl_FilesInLST.SelectedIndexChanged += ctrl_FilesInLST_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(418, 278);
            label2.Name = "label2";
            label2.Size = new Size(285, 15);
            label2.TabIndex = 12;
            label2.Text = "Файлы в LST. Кликни на файле для проигрывания.";
            // 
            // BoomSosed_MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(738, 417);
            Controls.Add(label2);
            Controls.Add(ctrl_FilesInLST);
            Controls.Add(btnRecalcParams);
            Controls.Add(ctrlLog);
            Controls.Add(label1);
            Controls.Add(ctrl_FillRatio);
            Controls.Add(groupBoxVisualBoom);
            Controls.Add(label_Speed);
            Controls.Add(ctrl_Speed);
            Controls.Add(btnStart);
            Controls.Add(ctrl_LST);
            Name = "BoomSosed_MainForm";
            Text = "My BoomSosed .NET";
            groupBoxVisualBoom.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox ctrl_LST;
        private Button btnStart;
        private TextBox ctrl_Speed;
        private Label label_Speed;
        private TableLayoutPanel ctrlVisualBoom;
        private GroupBox groupBoxVisualBoom;
        private Label label1;
        private TextBox ctrl_FillRatio;
        private RichTextBox ctrlLog;
        private Button btnRecalcParams;
        private System.Windows.Forms.Timer timer_boom;
        private ListBox ctrl_FilesInLST;
        private Label label2;
    }
}
