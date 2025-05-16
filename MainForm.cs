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
        const string _VERSION_ = "Initial: 08-05-2025 Last: 14-05-2025";

        int MaxColSizeVisualBoom = 10;
        int MaxRowSizeVisualBoom = 10;
        Random random = new Random();
        System.Windows.Forms.Timer timer_boom;
        FormController formController;

        #region form Delegates
        public delegate void StartStopDelegate(string command);
        public void _StartStop(string command)
        {
            formController.LoggerAdd($"start/stop: {command}");
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
            arr = new int[MaxRowSizeVisualBoom, MaxColSizeVisualBoom];
            timer_boom = new System.Windows.Forms.Timer();

            StartStopDelegate startStopDelegate = _StartStop;
            PlaySoundDelegate playSoundDelegate = _PlaySound;
            formController = new FormController(this, ctrlLog, startStopDelegate, playSoundDelegate);

            ctrl_Speed.Text = "1";
            ctrl_FillRatio.Text = "5";

            formController.LoggerAdd(_VERSION_);
            InitDesign();
            formController.InitFormConfig();
            CalcArray();
            UpdateDesign();
        }
        bool firstTimeTimer = true;
        bool timerEnabled = false;
        string selectedLST = "";
        string selectedFile = "";
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!timer_boom.Enabled)
            {
                if (!ValidBeforeStartTimer())
                {
                    formController.LoggerAdd("Timer is NOT started.");
                    return;
                }

                UpdateDesign();
                curRowSizeVisualBoom = 0;
                curColSizeVisualBoom = -1;
                Int32 val = 0;
                Int32.TryParse(ctrl_Speed.Text, null, out val);
                timer_boom.Interval = val * 1000;
                timerEnabled = true;
                if (firstTimeTimer)
                {
                    timer_boom.Tick += Timer_boom_Tick;
                    firstTimeTimer = false;
                }
                //Зафиксировать выбранный плейлист и файл, что бы во время обработки по шедулеру помнить это.
                if (ctrl_LST.SelectedItem is String)
                    selectedLST = (String)ctrl_LST.SelectedItem;
                if (ctrl_FilesInLST.SelectedItem is String)
                    selectedFile = (String)ctrl_FilesInLST.SelectedItem;
                formController.LoggerAdd($"selected playlist: {selectedLST}");
                if (!string.IsNullOrEmpty(selectedFile))
                    formController.LoggerAdd($"selected file: {selectedFile}");
                else
                    formController.LoggerAdd($"selected file: RANDOM");

                formController.LoggerAdd("Timer started.");
                btnStart.Text = "Stop";
                timer_boom.Start();
            }
            else
            {
                timer_boom.Stop();
                timerEnabled = false;
                formController.LoggerAdd("Timer stopped.");
                btnStart.Text = "Start";
                ctrl_schedule_info.Text = "-";
                ctrl_schedule_info.BackColor = Color.Black;
            }
        }
        private void Timer_boom_Tick(object? sender, EventArgs e)
        {
            if (timerEnabled)
            {
                var aa = ctrl_AllTimeF.Text;
                if (ctrl_mainSсheduler.Checked)
                {
                    if (DateTime.Now.TimeOfDay < ctrl_AllTimeF.Value.TimeOfDay || DateTime.Now.TimeOfDay > ctrl_AllTimeT.Value.TimeOfDay)
                    {
                        ctrl_schedule_info.ForeColor = Color.Red;
                        ctrl_schedule_info.Text = "ВЫКЛ по планировщику";
                        return;
                    }
                    else
                    {
                        ctrl_schedule_info.ForeColor = Color.Green;
                        ctrl_schedule_info.Text = "ВКЛ по планировщику";
                    }
                }
                timerEnabled = false;
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
            MessageBox.Show("Перевод на карту Сбер:");
        }
        private void ctrl_FilesInLST_DoubleClick(object sender, EventArgs e)
        {
            //if (timer_boom.Enabled == false)
            //{
                var selected = ctrl_FilesInLST.SelectedItem;
                if (selected is String && selected != null)
                {
                    PlayMp3(".\\sounds\\" + selected);
                }
            //}
            //else
            //{
            //    formController.LoggerAdd("Проигрывание файла невозможно, так как запущено проигрывание по плану.");
            //}
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
            Int32 val = 0;
            Int32.TryParse(ctrl_Speed.Text, null, out val);
            if (val > 60 * 60/*час*/ || val <= 0)
            {
                ctrl_Speed.Text = "5";
                val = 5;
            }
            //--------------------//-------------------- 3
            InitVisualBoomGrid();
            //--------------------//-------------------- 4 
        }
        public int[,] FillArrayWithRandomValues(int fillPercentage = 10, int rows = 10, int columns = 10)
        {
            int[,] array = new int[rows, columns];
            Random random = new Random();

            int onesCount = rows * columns * fillPercentage / 100;
            var indices = new System.Collections.Generic.List<(int row, int col)>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    indices.Add((i, j));
                }
            }

            for (int i = indices.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (indices[i], indices[j]) = (indices[j], indices[i]);
            }

            for (int i = 0; i < onesCount; i++)
            {
                var (row, col) = indices[i];
                array[row, col] = 1;
            }
            return array;
        }
        int[,] arr;

        public void CalcArray()
        {
            Int32 val = 0;
            Int32.TryParse(ctrl_FillRatio.Text, null, out val);
            if (val > 99 || val < 1)
            {
                ctrl_FillRatio.Text = "5";
                val = 10;
            }

            arr = FillArrayWithRandomValues(val, MaxRowSizeVisualBoom, MaxColSizeVisualBoom);
        }
        public void InitVisualBoomGrid()
        {
            ctrlVisualBoom.Visible = false;
            ctrlVisualBoom.ColumnStyles.Clear();
            ctrlVisualBoom.RowStyles.Clear();
            ctrlVisualBoom.Controls.Clear();
            ctrlVisualBoom.RowCount = 0;
            ctrlVisualBoom.ColumnCount = 0;

            ctrlVisualBoom.RowCount = MaxRowSizeVisualBoom;
            ctrlVisualBoom.ColumnCount = MaxColSizeVisualBoom;
            ctrlVisualBoom.Dock = DockStyle.Fill;
            ctrlVisualBoom.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < 10; i++)
            {
                ctrlVisualBoom.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 10));
                ctrlVisualBoom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 10));
            }

            for (int row = 0; row < MaxRowSizeVisualBoom; row++)
            {
                for (int col = 0; col < MaxColSizeVisualBoom; col++)
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
                    int fileSelectedNum = random.Next(lines.Count());
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

            formController.LoggerAdd($"Boom! {filePath}");
            using (var audioFile = new AudioFileReader(audioFilePath))
            {
                if (outputDevice.PlaybackState != PlaybackState.Playing)
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
                }
                else
                {
                    if (timer_boom.Enabled)
                        timerEnabled = true;
                }
            }
        }
        private void OutputDevice_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            if (timer_boom.Enabled)
                timerEnabled = true;
        }

        int curRowSizeVisualBoom;
        int curColSizeVisualBoom;
        public void StartBoom()
        {
            if (curColSizeVisualBoom < MaxColSizeVisualBoom)
                curColSizeVisualBoom++;
            if (curColSizeVisualBoom >= MaxColSizeVisualBoom)
            {
                curColSizeVisualBoom = 0;
                curRowSizeVisualBoom++;
            }
            if (curRowSizeVisualBoom >= MaxRowSizeVisualBoom)
            {
                curColSizeVisualBoom = -1;
                curRowSizeVisualBoom = 0;
                if (ctrl_RecalcVisualBoom.Checked)
                {
                    CalcArray();
                }
                InitVisualBoomGrid();
                if (timer_boom.Enabled)
                    timerEnabled = true;
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
                    else
                    {
                        if (timer_boom.Enabled)
                            timerEnabled = true;
                    }
                }
            }
            else
            {
                if (timer_boom.Enabled)
                    timerEnabled = true;
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
