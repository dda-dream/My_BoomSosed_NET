using NAudio.Wave;
using System.Globalization;
using System.Media;
//https://freesound.org/

namespace My_BoomSosed_NET
{           
    public partial class MainForm : Form
    {
        const string _VERSION_ = "Initial release: 08-05-2025 Latest release: 08-10-2025";

        readonly System.Windows.Forms.Timer timer_boom;
        Int32 speedCounter = 0;
        bool scheduleEnabled = false;
        string selectedLST = "";
        string selectedFile = "";
        readonly FormController formController;
        readonly SoundPlayer soundPlayer;
        readonly VisualBoom visualBoom;
        int secondsToStop;

        #region FORM Delegates
        public delegate void StartStopDelegate(string command);
        public void _StartStop(string command)
        {
            formController.Logger.Add($"start/stop: {command}");
            if (command.Trim().Contains("start"))
                btnStart_Click(this, new EventArgs() { });

            if (command.Trim().Contains("stop"))
                btnStart_Click(this, new EventArgs() { });

        }
        public delegate void PlaySoundDelegate();
        public void _PlaySound()
        {
            formController.Logger.Add("play selectedFile sound");
            soundPlayer.PlaySound(".\\sounds\\Ball\\01 Ball.mp3");
        }
        #endregion

        public MainForm()
        {
            InitializeComponent();
            timer_boom = new System.Windows.Forms.Timer();

            StartStopDelegate startStopDelegate = _StartStop;
            PlaySoundDelegate playSoundDelegate = _PlaySound;
            formController = new FormController(this, ctrlLog, startStopDelegate, playSoundDelegate, ctrl_SoundFolders, ctrl_Speed);
            soundPlayer = new SoundPlayer(formController, ctrl_RandomVolume, ctrl_VolumeAmplifier);
            visualBoom = new VisualBoom(ctrlVisualBoom, formController, groupBoxVisualBoom, ctrl_FillRatio, 
                                        ctrl_RecalcVisualBoom, soundPlayer, ctrl_RepeatQty, ctrl_RepeatRandom);


            ctrl_Speed.Text = "1";
            ctrl_FillRatio.Text = "5";
            ctrl_RepeatQty.Text = "1";

            formController.Logger.Add(_VERSION_);
            formController.ReadSoundFolders();
            formController.InitFormConfig();
            formController.Logger.Add("Config loaded from config.cfg");
            visualBoom.CalcArray();
            formController.UpdateDesign();
            visualBoom.InitVisualBoomGrid();

            timer_boom.Interval = 1000;
            timer_boom.Tick += Timer_boom_Tick;
            timer_boom.Start();
            
            ToolTip toolTip0 = new ToolTip();
            toolTip0.SetToolTip(ctrl_SecondsToStop, "таймер выкл, сек");
            
        }

        void StartScheduler()
        {
            if (!formController.ValidBeforeStartTimer())
            {
                formController.Logger.Add("Scheduler is NOT started.");
                return;
            }

            formController.Logger.Clear();
            formController.UpdateDesign();
            visualBoom.InitVisualBoomGrid();
            visualBoom.ResetCurPos();

            scheduleEnabled = true;
            speedCounter = 0;
            //Зафиксировать выбранный плейлист и файл, что бы во время обработки по шедулеру помнить.
            if (ctrl_SoundFolders.SelectedItem is String s_folder)
                selectedLST = s_folder;
            if (ctrl_SoundFiles.SelectedItem is String s_file)
                selectedFile = s_file;
            selectedFile = selectedFile.Split(" | ")[0];

            Int32.TryParse(ctrl_SecondsToStop.Text, out secondsToStop);
                

            formController.Logger.Add($"selectedFile playlist: {selectedLST}");
            if (!string.IsNullOrEmpty(selectedFile))
                formController.Logger.Add($"selectedFile file: {selectedFile}");
            else
                formController.Logger.Add($"selectedFile file: RANDOM");

            formController.Logger.Add("Scheduler started.");
            btnStart.Text = "Stop";
        }
        void StopScheduler()
        {
            scheduleEnabled = false;
            formController.Logger.Add("Scheduler stopped.");
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
            if (scheduleEnabled || soundPlayer.soundPlaying)
                this.Text = $"My BoomSosed .NET {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToLongTimeString()} " +
                            $"schEn={scheduleEnabled} playing={soundPlayer.soundPlaying} speedCnt={this.speedCounter}";
            else
                this.Text = $"My BoomSosed .NET {DateTime.Now.ToShortDateString()} - {DateTime.Now.ToLongTimeString()} ";
        }

        private void Timer_boom_Tick(object? sender, EventArgs e)
        {
            FormCaptionInfo();

            if (!scheduleEnabled || soundPlayer.soundPlaying)
                return;
            if (Int32.TryParse(ctrl_SecondsToStop.Text, null, out _))
            {
                secondsToStop--;
                if (secondsToStop < 0)
                {
                    formController.Logger.Add("Таймер истек. Остановка шедулера.");
                    StopScheduler();
                }
            }



            if (this.speedCounter < 1)
            {
                if (ctrl_RandomTime.Checked)
                {
                    if (speedCounter <= 1)
                    {
                        Int32.TryParse(ctrl_Speed.Text, null, out Int32 speedCounter);
                        this.speedCounter = speedCounter;
                    }
                    this.speedCounter = Random.Shared.Next(1, speedCounter);
                }
                else
                {
                    Int32.TryParse(ctrl_Speed.Text, null, out Int32 speedCounter);
                    this.speedCounter = speedCounter;
                }
            }
            else
            {
                this.speedCounter--;
                return;
            }

            if (scheduleEnabled && !soundPlayer.soundPlaying)
            {
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

                visualBoom.StartBoom(selectedLST, selectedFile, ctrl_SoundFolders, ctrl_SoundFiles);
            }
        }
        private void btnRecalcParams_Click(object sender, EventArgs e)
        {
            visualBoom.CalcArray();
            formController.UpdateDesign();
            visualBoom.InitVisualBoomGrid();

        }
        private void ctrl_LST_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFile = "";
            var soundsFiles = Directory.EnumerateFiles(".\\sounds\\" + ctrl_SoundFolders.Text);

            ctrl_SoundFiles.Items.Clear();
            ctrl_SoundFiles.ClearSelected();
            foreach (var file in soundsFiles)
            {
                double rounded = Math.Round(soundPlayer.GetSoundLength(file), 1);
                ctrl_SoundFiles.Items.Add($"{Path.GetFileName(file)} | {rounded.ToString("0.0", CultureInfo.InvariantCulture)}");
                //ctrl_SoundFiles.Items.Add($"{Path.GetFileName(file)}");
            }
            ctrl_SoundFiles.Sorted = true;
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
            string selectedFld = ctrl_SoundFolders.SelectedItem as string ?? "";
            string selectedFile = ctrl_SoundFiles.SelectedItem as string ?? ""  ;
            selectedFile = selectedFile.Split(" | ")[0];

            if ( selectedFile != null && selectedFld != null)
            {
                soundPlayer.PlaySound(".\\sounds\\" + (string)selectedFld +"\\"+ (string)selectedFile);
            }
        }
        private void BoomSosed_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }




        private void BoomSosed_MainForm_Shown(object sender, EventArgs e)
        {
            formController.StartCommandServer();
        }

        private void ctrl_SaveConfig_Click(object sender, EventArgs e)
        {
            formController.SafeToConfig();
            formController.Logger.Add("Config saved to config.cfg");
        }
    }
}
