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
            ctrl_FilesInLST = new ListBox();
            label2 = new Label();
            ctrl_RecalcVisualBoom = new CheckBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            groupBoxVisualBoom.SuspendLayout();
            SuspendLayout();
            // 
            // ctrl_LST
            // 
            ctrl_LST.FormattingEnabled = true;
            ctrl_LST.ItemHeight = 15;
            ctrl_LST.Location = new Point(2, 20);
            ctrl_LST.Name = "ctrl_LST";
            ctrl_LST.ScrollAlwaysVisible = true;
            ctrl_LST.Size = new Size(318, 49);
            ctrl_LST.TabIndex = 1;
            ctrl_LST.SelectedIndexChanged += ctrl_LST_SelectedIndexChanged;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(325, 119);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(98, 23);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start/Stop";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // ctrl_Speed
            // 
            ctrl_Speed.Location = new Point(324, 60);
            ctrl_Speed.Name = "ctrl_Speed";
            ctrl_Speed.Size = new Size(82, 23);
            ctrl_Speed.TabIndex = 3;
            ctrl_Speed.Text = "-";
            ctrl_Speed.TextAlign = HorizontalAlignment.Center;
            // 
            // label_Speed
            // 
            label_Speed.AutoSize = true;
            label_Speed.Location = new Point(324, 42);
            label_Speed.Name = "label_Speed";
            label_Speed.Size = new Size(78, 15);
            label_Speed.TabIndex = 4;
            label_Speed.Text = "сек. на 1 шаг";
            // 
            // ctrlVisualBoom
            // 
            ctrlVisualBoom.ColumnCount = 1;
            ctrlVisualBoom.ColumnStyles.Add(new ColumnStyle());
            ctrlVisualBoom.Location = new Point(31, 32);
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
            groupBoxVisualBoom.Location = new Point(2, 175);
            groupBoxVisualBoom.Name = "groupBoxVisualBoom";
            groupBoxVisualBoom.Size = new Size(239, 239);
            groupBoxVisualBoom.TabIndex = 6;
            groupBoxVisualBoom.TabStop = false;
            groupBoxVisualBoom.Text = "Визуальный план BOOM";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(412, 42);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 8;
            label1.Text = "коэфф заполн %";
            // 
            // ctrl_FillRatio
            // 
            ctrl_FillRatio.Location = new Point(412, 61);
            ctrl_FillRatio.Name = "ctrl_FillRatio";
            ctrl_FillRatio.Size = new Size(99, 23);
            ctrl_FillRatio.TabIndex = 7;
            ctrl_FillRatio.Text = "-";
            ctrl_FillRatio.TextAlign = HorizontalAlignment.Center;
            // 
            // ctrlLog
            // 
            ctrlLog.Location = new Point(247, 175);
            ctrlLog.Name = "ctrlLog";
            ctrlLog.Size = new Size(395, 239);
            ctrlLog.TabIndex = 9;
            ctrlLog.Text = "";
            // 
            // btnRecalcParams
            // 
            btnRecalcParams.Location = new Point(324, 90);
            btnRecalcParams.Name = "btnRecalcParams";
            btnRecalcParams.Size = new Size(99, 23);
            btnRecalcParams.TabIndex = 10;
            btnRecalcParams.Text = "Обновить план";
            btnRecalcParams.UseVisualStyleBackColor = true;
            btnRecalcParams.Click += btnRecalcParams_Click;
            // 
            // ctrl_FilesInLST
            // 
            ctrl_FilesInLST.FormattingEnabled = true;
            ctrl_FilesInLST.ItemHeight = 15;
            ctrl_FilesInLST.Location = new Point(2, 90);
            ctrl_FilesInLST.Name = "ctrl_FilesInLST";
            ctrl_FilesInLST.ScrollAlwaysVisible = true;
            ctrl_FilesInLST.Size = new Size(318, 79);
            ctrl_FilesInLST.TabIndex = 11;
            ctrl_FilesInLST.SelectedIndexChanged += ctrl_FilesInLST_SelectedIndexChanged;
            ctrl_FilesInLST.DoubleClick += ctrl_FilesInLST_DoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2, 72);
            label2.Name = "label2";
            label2.Size = new Size(301, 15);
            label2.TabIndex = 12;
            label2.Text = "Файлы в LST. 1клик-выбор. 2клик для проигрывания.";
            // 
            // ctrl_RecalcVisualBoom
            // 
            ctrl_RecalcVisualBoom.AutoSize = true;
            ctrl_RecalcVisualBoom.Location = new Point(326, 150);
            ctrl_RecalcVisualBoom.Name = "ctrl_RecalcVisualBoom";
            ctrl_RecalcVisualBoom.Size = new Size(271, 19);
            ctrl_RecalcVisualBoom.TabIndex = 13;
            ctrl_RecalcVisualBoom.Text = "Перестроение BOOM плана, после прохода.";
            ctrl_RecalcVisualBoom.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(2, 2);
            label3.Name = "label3";
            label3.Size = new Size(91, 15);
            label3.TabIndex = 14;
            label3.Text = "Плейлисты LST";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.Highlight;
            label4.Location = new Point(327, 2);
            label4.Name = "label4";
            label4.Size = new Size(243, 15);
            label4.TabIndex = 15;
            label4.Text = "Письмо разработчику: dda_dream@mail.ru";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.Highlight;
            label5.Location = new Point(327, 20);
            label5.Name = "label5";
            label5.Size = new Size(131, 15);
            label5.TabIndex = 16;
            label5.Text = "Помочь разработчику";
            label5.Click += label5_Click;
            // 
            // BoomSosed_MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(644, 417);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(ctrl_RecalcVisualBoom);
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
        private ListBox ctrl_FilesInLST;
        private Label label2;
        private CheckBox ctrl_RecalcVisualBoom;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}
