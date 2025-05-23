using NAudio.SoundFont;
using NAudio.Wave;
using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.ComponentModel.Design.ObjectSelectorEditor;
namespace My_BoomSosed_NET
{
    public partial class MainForm : Form
    {
        const string _VERSION_ = "Initial release: 08-05-2025 Latest release: 21-05-2025";

        System.Windows.Forms.Timer timer_boom;
        FormController formController;

        #region FORM Delegates
        public delegate void StartStopDelegate(string command);
        public void _StartStop(string command)
        {
            formController.LoggerAdd($"start/stop: {command}");
            if (command.Trim().Contains("start"))
                btnStart_Click(this, null);

            if( command.Trim().Contains("stop"))
                btnStart_Click(this, null);
            
        }

        public delegate void PlaySoundDelegate();
        public void _PlaySound()
        { 
            formController.LoggerAdd("play selected sound");
            PlayMp3(".\\sounds\\mp3\\Boom.mp3");
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
            CalcArray();
            UpdateDesign();

            timer_boom.Interval = 1000;
            timer_boom.Tick += Timer_boom_Tick;
            timer_boom.Start();
        }
        bool scheduleEnabled = false;
        bool schedulePaused = false;
        string selectedLST = "";
        string selectedFile = "";

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
            speedCounter=0;
            //������������� ��������� �������� � ����, ��� �� �� ����� ��������� �� �������� �������.
            if (ctrl_LST.SelectedItem is String)
                selectedLST = (String)ctrl_LST.SelectedItem;
            if (ctrl_FilesInLST.SelectedItem is String)
                selectedFile = (String)ctrl_FilesInLST.SelectedItem;
            formController.LoggerAdd($"selected playlist: {selectedLST}");
            if (!string.IsNullOrEmpty(selectedFile))
                formController.LoggerAdd($"selected file: {selectedFile}");
            else
                formController.LoggerAdd($"selected file: RANDOM");

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

        Int32 speedCounter=0;
        float soundVolume;
        private void Timer_boom_Tick(object? sender, EventArgs e)
        {
            this.Text = $"My BoomSosed .NET {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToLongTimeString()} " +
                        $"schEn={scheduleEnabled} schPa={schedulePaused} speedCnt={this.speedCounter}"                        
                        ;

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
                if (ctrl_mainS�heduler.Checked)
                {
                    if (DateTime.Now.TimeOfDay < ctrl_AllTimeF.Value.TimeOfDay || DateTime.Now.TimeOfDay > ctrl_AllTimeT.Value.TimeOfDay)
                    {
                        ctrl_schedule_info.ForeColor = Color.Red;
                        ctrl_schedule_info.Text = "���� �� ������������";
                        return;
                    }
                    else
                    {
                        ctrl_schedule_info.ForeColor = Color.Green;
                        ctrl_schedule_info.Text = "��� �� ������������";
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
            if (ctrl_LST.Items.Count != 0)
            {
                ctrl_FilesInLST.Items.Clear();
                var selected = ctrl_LST.SelectedItem;
                if (selected is String && selected != null)
                {
                    var lines = File.ReadAllLines((string)selected);
                    foreach (var line in lines)
                    {
                        if (line.Trim() != "")
                            ctrl_FilesInLST.Items.Add(line);
                    }
                    ctrl_FilesInLST.Sorted = true;
                }
            }
        }
        private void ctrl_FilesInLST_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("������� �� ����� ����:");
        }
        private void ctrl_FilesInLST_DoubleClick(object sender, EventArgs e)
        {
            var selected = ctrl_FilesInLST.SelectedItem;
            if (selected is String && selected != null)
            {
                PlayMp3(".\\sounds\\" + selected);
            }
        }
        private void BoomSosed_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            formController.SafeToConfig();
        }
        void InitDesign()
        {
            if (ctrl_LST.Items.Count == 0)
            {
                var soundsDir = Directory.EnumerateFiles(".\\sounds\\");

                foreach (var file in soundsDir)
                {
                    if (Path.GetExtension(file).ToLower() == ".lst")
                        ctrl_LST.Items.Add(file);
                }
            }
        }
        void UpdateDesign()
        {
            //--------------------//-------------------- 1
            //--------------------//-------------------- 2
            Int32.TryParse(ctrl_Speed.Text, null, out Int32 val);
            if (val > 60 * 60/*���*/ || val <= 0)
            {
                ctrl_Speed.Text = "5";
            }
            //--------------------//-------------------- 3
            InitVisualBoomGrid();
            //--------------------//-------------------- 4 
        }
        int[,] arr;

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
                    PlayMp3(".\\sounds\\" + selectedFile);
                }
                else
                {
                    var lines = File.ReadAllLines((string)selectedLST);
                    int fileSelectedNum = Random.Shared.Next(lines.Count());
                    PlayMp3(".\\sounds\\" + lines[fileSelectedNum]);
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

            if (ctrl_RandomVolume.Checked)
                soundVolume = (float)Random.Shared.NextDouble();
            else
                soundVolume = 1;

            formController.LoggerAdd($"Boom! {filePath} soundVolume: {soundVolume}");
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

        int curRowSizeVisualBoom;
        int curColSizeVisualBoom;
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
                var isThislabel = panel.Controls[0];
                if (isThislabel is Label)
                {
                    var label = (Label)isThislabel;
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

            var selected = ctrl_LST.SelectedItem;
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
    }
}
