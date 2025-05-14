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
            tabControl = new TabControl();
            tabPage1 = new TabPage();
            ctrl_schedule_info = new Label();
            tabPage2 = new TabPage();
            ctrl_mainSсheduler = new CheckBox();
            ctrl_AllTimeT = new DateTimePicker();
            label6 = new Label();
            ctrl_AllTimeF = new DateTimePicker();
            groupBoxVisualBoom.SuspendLayout();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // ctrl_LST
            // 
            ctrl_LST.FormattingEnabled = true;
            ctrl_LST.ItemHeight = 15;
            ctrl_LST.Location = new Point(6, 21);
            ctrl_LST.Name = "ctrl_LST";
            ctrl_LST.ScrollAlwaysVisible = true;
            ctrl_LST.Size = new Size(318, 49);
            ctrl_LST.TabIndex = 1;
            ctrl_LST.SelectedIndexChanged += ctrl_LST_SelectedIndexChanged;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(329, 120);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(98, 23);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start/Stop";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // ctrl_Speed
            // 
            ctrl_Speed.Location = new Point(328, 61);
            ctrl_Speed.Name = "ctrl_Speed";
            ctrl_Speed.Size = new Size(82, 23);
            ctrl_Speed.TabIndex = 3;
            ctrl_Speed.Text = "-";
            ctrl_Speed.TextAlign = HorizontalAlignment.Center;
            // 
            // label_Speed
            // 
            label_Speed.AutoSize = true;
            label_Speed.Location = new Point(328, 43);
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
            groupBoxVisualBoom.Location = new Point(6, 176);
            groupBoxVisualBoom.Name = "groupBoxVisualBoom";
            groupBoxVisualBoom.Size = new Size(239, 239);
            groupBoxVisualBoom.TabIndex = 6;
            groupBoxVisualBoom.TabStop = false;
            groupBoxVisualBoom.Text = "Визуальный план BOOM";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(416, 43);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 8;
            label1.Text = "коэфф заполн %";
            // 
            // ctrl_FillRatio
            // 
            ctrl_FillRatio.Location = new Point(416, 62);
            ctrl_FillRatio.Name = "ctrl_FillRatio";
            ctrl_FillRatio.Size = new Size(99, 23);
            ctrl_FillRatio.TabIndex = 7;
            ctrl_FillRatio.Text = "-";
            ctrl_FillRatio.TextAlign = HorizontalAlignment.Center;
            // 
            // ctrlLog
            // 
            ctrlLog.Location = new Point(251, 176);
            ctrlLog.Name = "ctrlLog";
            ctrlLog.Size = new Size(395, 239);
            ctrlLog.TabIndex = 9;
            ctrlLog.Text = "";
            // 
            // btnRecalcParams
            // 
            btnRecalcParams.Location = new Point(328, 91);
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
            ctrl_FilesInLST.Location = new Point(6, 91);
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
            label2.Location = new Point(6, 73);
            label2.Name = "label2";
            label2.Size = new Size(301, 15);
            label2.TabIndex = 12;
            label2.Text = "Файлы в LST. 1клик-выбор. 2клик для проигрывания.";
            // 
            // ctrl_RecalcVisualBoom
            // 
            ctrl_RecalcVisualBoom.AutoSize = true;
            ctrl_RecalcVisualBoom.Location = new Point(330, 151);
            ctrl_RecalcVisualBoom.Name = "ctrl_RecalcVisualBoom";
            ctrl_RecalcVisualBoom.Size = new Size(271, 19);
            ctrl_RecalcVisualBoom.TabIndex = 13;
            ctrl_RecalcVisualBoom.Text = "Перестроение BOOM плана, после прохода.";
            ctrl_RecalcVisualBoom.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 3);
            label3.Name = "label3";
            label3.Size = new Size(91, 15);
            label3.TabIndex = 14;
            label3.Text = "Плейлисты LST";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.Highlight;
            label4.Location = new Point(331, 1);
            label4.Name = "label4";
            label4.Size = new Size(243, 15);
            label4.TabIndex = 15;
            label4.Text = "Письмо разработчику: dda_dream@mail.ru";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.Highlight;
            label5.Location = new Point(331, 15);
            label5.Name = "label5";
            label5.Size = new Size(131, 15);
            label5.TabIndex = 16;
            label5.Text = "Помочь разработчику";
            label5.Click += label5_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Location = new Point(12, 12);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(659, 446);
            tabControl.TabIndex = 17;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(ctrl_schedule_info);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(ctrl_LST);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(btnStart);
            tabPage1.Controls.Add(ctrl_Speed);
            tabPage1.Controls.Add(ctrl_RecalcVisualBoom);
            tabPage1.Controls.Add(label_Speed);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(groupBoxVisualBoom);
            tabPage1.Controls.Add(ctrl_FilesInLST);
            tabPage1.Controls.Add(ctrl_FillRatio);
            tabPage1.Controls.Add(btnRecalcParams);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(ctrlLog);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(651, 418);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Основная";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // ctrl_schedule_info
            // 
            ctrl_schedule_info.AutoSize = true;
            ctrl_schedule_info.Location = new Point(433, 124);
            ctrl_schedule_info.Name = "ctrl_schedule_info";
            ctrl_schedule_info.Size = new Size(12, 15);
            ctrl_schedule_info.TabIndex = 17;
            ctrl_schedule_info.Text = "-";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(ctrl_mainSсheduler);
            tabPage2.Controls.Add(ctrl_AllTimeT);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(ctrl_AllTimeF);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(651, 418);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Планировщик";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // ctrl_mainSсheduler
            // 
            ctrl_mainSсheduler.AutoSize = true;
            ctrl_mainSсheduler.Location = new Point(270, 6);
            ctrl_mainSсheduler.Name = "ctrl_mainSсheduler";
            ctrl_mainSсheduler.Size = new Size(112, 19);
            ctrl_mainSсheduler.TabIndex = 3;
            ctrl_mainSсheduler.Text = "Основной план";
            ctrl_mainSсheduler.UseVisualStyleBackColor = true;
            // 
            // ctrl_AllTimeT
            // 
            ctrl_AllTimeT.Format = DateTimePickerFormat.Time;
            ctrl_AllTimeT.Location = new Point(196, 3);
            ctrl_AllTimeT.Name = "ctrl_AllTimeT";
            ctrl_AllTimeT.Size = new Size(68, 23);
            ctrl_AllTimeT.TabIndex = 2;
            ctrl_AllTimeT.Value = new DateTime(2025, 5, 14, 21, 0, 0, 0);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 7);
            label6.Name = "label6";
            label6.Size = new Size(110, 15);
            label6.TabIndex = 1;
            label6.Text = "Каждый день с-по:";
            // 
            // ctrl_AllTimeF
            // 
            ctrl_AllTimeF.Format = DateTimePickerFormat.Time;
            ctrl_AllTimeF.Location = new Point(122, 3);
            ctrl_AllTimeF.Name = "ctrl_AllTimeF";
            ctrl_AllTimeF.Size = new Size(68, 23);
            ctrl_AllTimeF.TabIndex = 0;
            ctrl_AllTimeF.Value = new DateTime(2025, 5, 14, 8, 0, 0, 0);
            // 
            // BoomSosed_MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 470);
            Controls.Add(tabControl);
            Name = "BoomSosed_MainForm";
            Text = "My BoomSosed .NET";
            FormClosing += BoomSosed_MainForm_FormClosing;
            Shown += BoomSosed_MainForm_Shown;
            groupBoxVisualBoom.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox ctrl_LST;
        private Button btnStart;
        [SaveToConfigFileAttribute]
        private  TextBox ctrl_Speed;
        private Label label_Speed;
        private TableLayoutPanel ctrlVisualBoom;
        private GroupBox groupBoxVisualBoom;
        private Label label1;
        [SaveToConfigFileAttribute]
        private TextBox ctrl_FillRatio;
        private RichTextBox ctrlLog;
        private Button btnRecalcParams;
        private ListBox ctrl_FilesInLST;
        private Label label2;
        [SaveToConfigFileAttribute]
        private CheckBox ctrl_RecalcVisualBoom;
        private Label label3;
        private Label label4;
        private Label label5;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label6;
        [SaveToConfigFileAttribute]
        private DateTimePicker ctrl_AllTimeF;
        [SaveToConfigFileAttribute]
        private DateTimePicker ctrl_AllTimeT;
        private CheckBox ctrl_mainSсheduler;
        private Label ctrl_schedule_info;
    }
}
