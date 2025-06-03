using NAudio.Wave;
namespace My_BoomSosed_NET
{

    
    public partial class MainForm : Form
    {
        const string _VERSION_ = "Initial release: 08-05-2025 Latest release: 23-05-2025";

        System.Windows.Forms.Timer timer_boom;
        FormController formController;
        int curRowSizeVisualBoom;
        int curColSizeVisualBoom;
        Int32 speedCounter = 0;
        float soundVolume;
        bool scheduleEnabled = false;
        bool schedulePaused = false;
        string selectedLST = "";
        string selectedFile = "";
        int[,] arr;

        #region FORM Delegates
        public delegate void StartStopDelegate(string command);
        public void _StartStop(string command)
        {
            formController.LoggerAdd($"start/stop: {command}");
            if (command.Trim().Contains("start"))
                btnStart_Click(this, null);

            if (command.Trim().Contains("stop"))
                btnStart_Click(this, null);

        }
        public delegate void PlaySoundDelegate();
        public void _PlaySound()
        {
            formController.LoggerAdd("play selectedFile sound");
            PlayMp3(".\\sounds\\Boom\\Boom.mp3");
        }
        #endregion

        public MainForm()
        {
            InitializeComponent();
            timer_boom = new System.Windows.Forms.Timer();

            StartStopDelegate startStopDelegate = _StartStop;
            PlaySoundDelegate playSoundDelegate = _PlaySound;
            formController = new FormController(this, ctrlLog, startStopDelegate, playSoundDelegate);
            arr = new int[formController.MaxRowSizeVisualBoom, formController.MaxColSizeVisualBoom];

            ctrl_Speed.Text = "1";
            ctrl_FillRatio.Text = "5";

            formController.LoggerAdd(_VERSION_);
            InitDesign();
            formController.InitFormConfig();
            formController.LoggerAdd("Config loaded from config.cfg");
            CalcArray();
            UpdateDesign();

            timer_boom.Interval = 1000;
            timer_boom.Tick += Timer_boom_Tick;
            timer_boom.Start();
        }

        void StartScheduler()
        {
            if (!ValidBeforeStartTimer())
            {
                formController.LoggerAdd("Scheduler is NOT started.");
                return;
            }

            UpdateDesign();
            curRowSizeVisualBoom = 0;
            curColSizeVisualBoom = -1;
            scheduleEnabled = true;
            schedulePaused = false;
            speedCounter = 0;
<<<<<<< HEAD
            //Зафиксировать выбранный плейлист и файл, что бы во время обработки по шедулеру помнить.
=======
            //������������� ��������� �������� � ����, ��� �� �� ����� ��������� �� �������� �������.
>>>>>>> 96cf949302687175c90effc97db1a41d1e2f7957
            if (ctrl_SoundFolders.SelectedItem is String)
                selectedLST = (String)ctrl_SoundFolders.SelectedItem;
            if (ctrl_SoundFiles.SelectedItem is String)
                selectedFile = (String)ctrl_SoundFiles.SelectedItem;
            formController.LoggerAdd($"selectedFile playlist: {selectedLST}");
            if (!string.IsNullOrEmpty(selectedFile))
                formController.LoggerAdd($"selectedFile file: {selectedFile}");
            else
                formController.LoggerAdd($"selectedFile file: RANDOM");

            formController.LoggerAdd("Scheduler started.");
            btnStart.Text = "Stop";
        }
        void StopScheduler()
        {
            scheduleEnabled = false;
            formController.LoggerAdd("Scheduler stopped.");
            btnStart.Text = "Start";
            ctrl_schedule_info.Text = "-";
            ctrl_schedule_info.BackColor = Color.Black;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!scheduleEnabled)
                StartScheduler();
            else
                StopScheduler();
        }
        void FormCaptionInfo()
        {
            if (scheduleEnabled)
                this.Text = $"My BoomSosed .NET {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToLongTimeString()} " +
                            $"schEn={scheduleEnabled} schPa={schedulePaused} speedCnt={this.speedCounter}";
            else
                this.Text = $"My BoomSosed .NET {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToLongTimeString()} ";
        }

        private void Timer_boom_Tick(object? sender, EventArgs e)
        {
            FormCaptionInfo();

            if (!scheduleEnabled || schedulePaused)
                return;

            if (this.speedCounter <= 1)
            {
                Int32.TryParse(ctrl_Speed.Text, null, out Int32 speedCounter);
                if (ctrl_RandomTime.Checked)
                {
                    if (speedCounter < 1)
                    {
                        ctrl_Speed.Text = "1";
                        speedCounter = 1;
                    }
                    this.speedCounter = Random.Shared.Next(1, speedCounter);
                }
                else
                {
                    this.speedCounter = speedCounter;
                }
            }
            else
            {
                this.speedCounter--;
                return;
            }

            if (scheduleEnabled && !schedulePaused)
            {
                var aa = ctrl_AllTimeF.Text;
<<<<<<< HEAD
                if (ctrl_mainSсheduler.Checked)
=======
                if (ctrl_mainS�heduler.Checked)
>>>>>>> 96cf949302687175c90effc97db1a41d1e2f7957
                {
                    if (DateTime.Now.TimeOfDay < ctrl_AllTimeF.Value.TimeOfDay || DateTime.Now.TimeOfDay > ctrl_AllTimeT.Value.TimeOfDay)
                    {
                        ctrl_schedule_info.ForeColor = Color.Red;
<<<<<<< HEAD
                        ctrl_schedule_info.Text = "ВЫКЛ по планировщику";
=======
                        ctrl_schedule_info.Text = "���� �� ������������";
>>>>>>> 96cf949302687175c90effc97db1a41d1e2f7957
                        return;
                    }
                    else
                    {
                        ctrl_schedule_info.ForeColor = Color.Green;
<<<<<<< HEAD
                        ctrl_schedule_info.Text = "ВКЛ по планировщику";
=======
                        ctrl_schedule_info.Text = "��� �� ������������";
>>>>>>> 96cf949302687175c90effc97db1a41d1e2f7957
                    }
                }
                StartBoom();
            }
        }
        private void btnRecalcParams_Click(object sender, EventArgs e)
        {
            CalcArray();
            UpdateDesign();
        }
        private void ctrl_LST_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFile = "";
            var soundsFiles = Directory.EnumerateFiles(".\\sounds\\" + ctrl_SoundFolders.Text);

            ctrl_SoundFiles.Items.Clear();
            ctrl_SoundFiles.ClearSelected();
            foreach (var file in soundsFiles)
            {
                ctrl_SoundFiles.Items.Add(Path.GetFileName(file));
            }
            ctrl_SoundFiles.Sorted = true;
        }
        private void ctrl_FilesInLST_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void label5_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            MessageBox.Show("Перевод на карту Сбер:");
=======
            MessageBox.Show("������� �� ����� ����:");
>>>>>>> 96cf949302687175c90effc97db1a41d1e2f7957
        }
        private void ctrl_FilesInLST_DoubleClick(object sender, EventArgs e)
        {
            string selectedFld = (string)ctrl_SoundFolders.SelectedItem;
            string selectedFile = (string)ctrl_SoundFiles.SelectedItem;
            if ( selectedFile != null && selectedFld != null)
            {
                PlayMp3(".\\sounds\\" + (string)selectedFld +"\\"+ (string)selectedFile);
            }
        }
        private void BoomSosed_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        void InitDesign()
        {
            var soundsDir = Directory.EnumerateDirectories(".\\sounds\\");

            foreach (var folder in soundsDir)
            {
                ctrl_SoundFolders.Items.Add( folder.Replace(".\\sounds\\", "") );
            }
        }
        void UpdateDesign()
        {
            //--------------------//-------------------- 1
            //--------------------//-------------------- 2
            Int32.TryParse(ctrl_Speed.Text, null, out Int32 val);
<<<<<<< HEAD
            if (val > 60 * 60/*час*/ || val <= 0)
=======
            if (val > 60 * 60/*���*/ || val <= 0)
>>>>>>> 96cf949302687175c90effc97db1a41d1e2f7957
            {
                ctrl_Speed.Text = "5";
            }
            //--------------------//-------------------- 3
            InitVisualBoomGrid();
            //--------------------//-------------------- 4 
        }

        public void CalcArray()
        {
            Int32.TryParse(ctrl_FillRatio.Text, null, out Int32 val);
            if (val > 100 || val < 1)
            {
                ctrl_FillRatio.Text = "5";
                val = 10;
            }

            arr = formController.FillArrayWithRandomValues(val, formController.MaxRowSizeVisualBoom, formController.MaxColSizeVisualBoom);
        }
        public void InitVisualBoomGrid()
        {
            ctrlVisualBoom.Visible = false;
            ctrlVisualBoom.ColumnStyles.Clear();
            ctrlVisualBoom.RowStyles.Clear();
            ctrlVisualBoom.Controls.Clear();
            ctrlVisualBoom.RowCount = 0;
            ctrlVisualBoom.ColumnCount = 0;

            ctrlVisualBoom.RowCount = formController.MaxRowSizeVisualBoom;
            ctrlVisualBoom.ColumnCount = formController.MaxColSizeVisualBoom;
            ctrlVisualBoom.Dock = DockStyle.Fill;
            ctrlVisualBoom.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < 10; i++)
            {
                ctrlVisualBoom.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 10));
                ctrlVisualBoom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 10));
            }

            for (int row = 0; row < formController.MaxRowSizeVisualBoom; row++)
            {
                for (int col = 0; col < formController.MaxColSizeVisualBoom; col++)
                {
                    Panel panel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.White
                    };
                    if (arr[row, col] == 1)
                    {
                        panel.BackColor = Color.Green;
                        Label label = new Label
                        {
                            Text = "*!*",
                        };
                        panel.Controls.Add(label);
                    }
                    ctrlVisualBoom.Controls.Add(panel, col, row);
                }
            }
            groupBoxVisualBoom.Controls.Add(ctrlVisualBoom);
            ctrlVisualBoom.Visible = true;
        }
        public void PlayRandomSoundFromList()
        {
            if (!string.IsNullOrEmpty(selectedLST))
            {
                if (!string.IsNullOrEmpty(selectedFile))
                {
                    string selectedFld = (string)ctrl_SoundFolders.SelectedItem;
                    string selectedFile = (string)ctrl_SoundFiles.SelectedItem;
                    if (selectedFile != null && selectedFld != null)
                    {
                        PlayMp3(".\\sounds\\" + (string)selectedFld + "\\" + (string)selectedFile);
                    }
                }
                else
                {
                    string randomFile = (string)ctrl_SoundFiles.Items[Random.Shared.Next(0, ctrl_SoundFiles.Items.Count)];
                    string selectedFld = (string)ctrl_SoundFolders.SelectedItem;
                    if (randomFile != null && selectedFld != null)
                    {
                        PlayMp3(".\\sounds\\" + (string)selectedFld + "\\" + (string)randomFile);
                    }
                }
            }
        }
        WaveOutEvent outputDevice = new WaveOutEvent();
        public void PlayMp3(string filePath)
        {
            var audioFilePath = Path.GetDirectoryName(Application.ExecutablePath) + filePath;
            if (!File.Exists(audioFilePath))
            {
                formController.LoggerAdd($"PlayMp3: File not exist {filePath}");
                return;
            }

            soundVolume = ctrl_RandomVolume.Checked ? (float)Random.Shared.NextDouble() : 1;

            formController.LoggerAdd($"Boom! {filePath} soundVolume: {(int)(soundVolume * 100)}");
            using (var audioFile = new AudioFileReader(audioFilePath))
            {
                if (outputDevice.PlaybackState != PlaybackState.Playing)
                {
                    schedulePaused = true;
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    outputDevice.Volume = soundVolume;
                    outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
                }
            }
        }
        private void OutputDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            schedulePaused = false;
        }
        public void StartBoom()
        {
            if (curColSizeVisualBoom < formController.MaxColSizeVisualBoom)
                curColSizeVisualBoom++;
            if (curColSizeVisualBoom >= formController.MaxColSizeVisualBoom)
            {
                curColSizeVisualBoom = 0;
                curRowSizeVisualBoom++;
            }
            if (curRowSizeVisualBoom >= formController.MaxRowSizeVisualBoom)
            {
                curColSizeVisualBoom = -1;
                curRowSizeVisualBoom = 0;
                if (ctrl_RecalcVisualBoom.Checked)
                {
                    CalcArray();
                }
                InitVisualBoomGrid();

                return;
            }
            var panel = (Panel?)ctrlVisualBoom.GetControlFromPosition(curColSizeVisualBoom, curRowSizeVisualBoom);
            if (panel is Panel)
                panel.BackColor = Color.Yellow;

            if (panel != null && panel.Controls.Count > 0)
            {
                if (panel.Controls[0] is Label label)
                {
                    if (label != null && label.Text == "*!*")
                    {
                        PlayRandomSoundFromList();
                    }
                }
            }
        }
        bool ValidBeforeStartTimer()
        {
            bool retVal = true;

            var selected = ctrl_SoundFolders.SelectedItem;
            if (selected == null)
            {
                formController.LoggerAdd("Choose PlayList!");
                MessageBox.Show("Choose PlayList!");
                retVal = false;
            }
            return retVal;
        }

        private void BoomSosed_MainForm_Shown(object sender, EventArgs e)
        {
            formController.StartCommandServer();
        }

        private void ctrl_SaveConfig_Click(object sender, EventArgs e)
        {
            formController.SafeToConfig();
            formController.LoggerAdd("Config saved to config.cfg");
        }
    }
}
