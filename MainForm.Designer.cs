namespace My_BoomSosed_NET
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
            ctrl_SoundFolders = new ListBox();
            btnStart = new Button();
            ctrl_Speed = new TextBox();
            label_Speed = new Label();
            ctrlVisualBoom = new TableLayoutPanel();
            groupBoxVisualBoom = new GroupBox();
            label1 = new Label();
            ctrl_FillRatio = new TextBox();
            ctrlLog = new RichTextBox();
            btnRecalcParams = new Button();
            ctrl_SoundFiles = new ListBox();
            label2 = new Label();
            ctrl_RecalcVisualBoom = new CheckBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            tabControl = new TabControl();
            tabPage1 = new TabPage();
            ctrl_SaveConfig = new Button();
            ctrl_RandomVolume = new CheckBox();
            ctrl_RandomTime = new CheckBox();
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
            // ctrl_SoundFolders
            // 
            ctrl_SoundFolders.FormattingEnabled = true;
            ctrl_SoundFolders.Location = new Point(6, 21);
            ctrl_SoundFolders.Name = "ctrl_SoundFolders";
            ctrl_SoundFolders.ScrollAlwaysVisible = true;
            ctrl_SoundFolders.Size = new Size(145, 184);
            ctrl_SoundFolders.TabIndex = 1;
            ctrl_SoundFolders.SelectedIndexChanged += ctrl_LST_SelectedIndexChanged;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(329, 148);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(77, 23);
            btnStart.TabIndex = 2;
            btnStart.Text = "Start/Stop";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // ctrl_Speed
            // 
            ctrl_Speed.Location = new Point(328, 41);
            ctrl_Speed.Name = "ctrl_Speed";
            ctrl_Speed.Size = new Size(82, 23);
            ctrl_Speed.TabIndex = 3;
            ctrl_Speed.Text = "-";
            ctrl_Speed.TextAlign = HorizontalAlignment.Center;
            // 
            // label_Speed
            // 
            label_Speed.AutoSize = true;
            label_Speed.Location = new Point(328, 26);
            label_Speed.Name = "label_Speed";
            label_Speed.Size = new Size(78, 15);
            label_Speed.TabIndex = 4;
            label_Speed.Text = "сек. на 1 шаг";
            // 
            // ctrlVisualBoom
            // 
            ctrlVisualBoom.ColumnCount = 1;
            ctrlVisualBoom.ColumnStyles.Add(new ColumnStyle());
            ctrlVisualBoom.Location = new Point(31, 167);
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
            groupBoxVisualBoom.Location = new Point(6, 311);
            groupBoxVisualBoom.Name = "groupBoxVisualBoom";
            groupBoxVisualBoom.Size = new Size(239, 239);
            groupBoxVisualBoom.TabIndex = 6;
            groupBoxVisualBoom.TabStop = false;
            groupBoxVisualBoom.Text = "Визуальный план BOOM";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(416, 26);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 8;
            label1.Text = "коэфф заполн %";
            // 
            // ctrl_FillRatio
            // 
            ctrl_FillRatio.Location = new Point(416, 41);
            ctrl_FillRatio.Name = "ctrl_FillRatio";
            ctrl_FillRatio.Size = new Size(99, 23);
            ctrl_FillRatio.TabIndex = 7;
            ctrl_FillRatio.Text = "-";
            ctrl_FillRatio.TextAlign = HorizontalAlignment.Center;
            // 
            // ctrlLog
            // 
            ctrlLog.Location = new Point(251, 311);
            ctrlLog.Name = "ctrlLog";
            ctrlLog.Size = new Size(395, 239);
            ctrlLog.TabIndex = 9;
            ctrlLog.Text = "";
            // 
            // btnRecalcParams
            // 
            btnRecalcParams.Location = new Point(412, 148);
            btnRecalcParams.Name = "btnRecalcParams";
            btnRecalcParams.Size = new Size(99, 23);
            btnRecalcParams.TabIndex = 10;
            btnRecalcParams.Text = "Обновить план";
            btnRecalcParams.UseVisualStyleBackColor = true;
            btnRecalcParams.Click += btnRecalcParams_Click;
            // 
            // ctrl_SoundFiles
            // 
            ctrl_SoundFiles.FormattingEnabled = true;
            ctrl_SoundFiles.Location = new Point(157, 21);
            ctrl_SoundFiles.Name = "ctrl_SoundFiles";
            ctrl_SoundFiles.ScrollAlwaysVisible = true;
            ctrl_SoundFiles.Size = new Size(167, 184);
            ctrl_SoundFiles.TabIndex = 11;
            ctrl_SoundFiles.SelectedIndexChanged += ctrl_FilesInLST_SelectedIndexChanged;
            ctrl_SoundFiles.DoubleClick += ctrl_FilesInLST_DoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(54, 3);
            label2.Name = "label2";
            label2.Size = new Size(271, 15);
            label2.TabIndex = 12;
            label2.Text = "Файлы. 1клик-выбор. 2клик для проигрывания.";
            // 
            // ctrl_RecalcVisualBoom
            // 
            ctrl_RecalcVisualBoom.AutoSize = true;
            ctrl_RecalcVisualBoom.Location = new Point(328, 83);
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
            label3.Size = new Size(42, 15);
            label3.TabIndex = 14;
            label3.Text = "Папки";
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
            tabControl.Size = new Size(659, 581);
            tabControl.TabIndex = 17;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(ctrl_SaveConfig);
            tabPage1.Controls.Add(ctrl_RandomVolume);
            tabPage1.Controls.Add(ctrl_RandomTime);
            tabPage1.Controls.Add(ctrl_schedule_info);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(ctrl_SoundFolders);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(btnStart);
            tabPage1.Controls.Add(ctrl_Speed);
            tabPage1.Controls.Add(ctrl_RecalcVisualBoom);
            tabPage1.Controls.Add(label_Speed);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(groupBoxVisualBoom);
            tabPage1.Controls.Add(ctrl_SoundFiles);
            tabPage1.Controls.Add(ctrl_FillRatio);
            tabPage1.Controls.Add(btnRecalcParams);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(ctrlLog);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(651, 553);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Основная";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // ctrl_SaveConfig
            // 
            ctrl_SaveConfig.Location = new Point(537, 147);
            ctrl_SaveConfig.Name = "ctrl_SaveConfig";
            ctrl_SaveConfig.Size = new Size(108, 23);
            ctrl_SaveConfig.TabIndex = 20;
            ctrl_SaveConfig.Text = "Сохр.настройки";
            ctrl_SaveConfig.UseVisualStyleBackColor = true;
            ctrl_SaveConfig.Click += ctrl_SaveConfig_Click;
            // 
            // ctrl_RandomVolume
            // 
            ctrl_RandomVolume.AutoSize = true;
            ctrl_RandomVolume.Location = new Point(450, 66);
            ctrl_RandomVolume.Name = "ctrl_RandomVolume";
            ctrl_RandomVolume.Size = new Size(147, 19);
            ctrl_RandomVolume.TabIndex = 19;
            ctrl_RandomVolume.Text = "Случайная громкость";
            ctrl_RandomVolume.UseVisualStyleBackColor = true;
            // 
            // ctrl_RandomTime
            // 
            ctrl_RandomTime.AutoSize = true;
            ctrl_RandomTime.Location = new Point(328, 66);
            ctrl_RandomTime.Name = "ctrl_RandomTime";
            ctrl_RandomTime.Size = new Size(124, 19);
            ctrl_RandomTime.TabIndex = 18;
            ctrl_RandomTime.Text = "Случайное время";
            ctrl_RandomTime.UseVisualStyleBackColor = true;
            // 
            // ctrl_schedule_info
            // 
            ctrl_schedule_info.AutoSize = true;
            ctrl_schedule_info.Location = new Point(517, 124);
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
            tabPage2.Size = new Size(651, 553);
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 605);
            Controls.Add(tabControl);
            MaximizeBox = false;
            Name = "MainForm";
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

        [SaveToConfigAttribute]
        private ListBox ctrl_SoundFolders;
        private Button btnStart;
        [SaveToConfigAttribute]
        private  TextBox ctrl_Speed;
        private Label label_Speed;
        private TableLayoutPanel ctrlVisualBoom;
        private GroupBox groupBoxVisualBoom;
        private Label label1;
        [SaveToConfigAttribute]
        private TextBox ctrl_FillRatio;
        private RichTextBox ctrlLog;
        private Button btnRecalcParams;
        [SaveToConfigAttribute]
        private ListBox ctrl_SoundFiles;
        private Label label2;
        [SaveToConfigAttribute]
        private CheckBox ctrl_RecalcVisualBoom;
        private Label label3;
        private Label label4;
        private Label label5;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label6;
        [SaveToConfigAttribute]
        private DateTimePicker ctrl_AllTimeF;
        [SaveToConfigAttribute]
        private DateTimePicker ctrl_AllTimeT;
        [SaveToConfigAttribute]
        private CheckBox ctrl_mainSсheduler;
        private Label ctrl_schedule_info;
        [SaveToConfigAttribute]
        private CheckBox ctrl_RandomTime;
        [SaveToConfigAttribute]
        private CheckBox ctrl_RandomVolume;
        private Button ctrl_SaveConfig;
    }
}
