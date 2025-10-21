using NAudio.Wave;
using System.Globalization;
using System.Media;
//https://freesound.org/

namespace My_BoomSosed_NET
{           
    public partial class MainForm : Form
    {
        const string _VERSION_ = "Initial release: 08-05-2025 Latest release: 08-10-2025";

        System.Windows.Forms.Timer timer_boom;
        Int32 speedCounter = 0;
        bool scheduleEnabled = false;
        string selectedLST = "";
        string selectedFile = "";
        FormController formController;
        SoundPlayer soundPlayer;
        VisualBoom visualBoom;
        int secondsToStop;

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
            soundPlayer.PlaySound(".\\sounds\\Boom\\Boom.mp3");
        }
        #endregion

        public MainForm()
        {
            InitializeComponent();
            timer_boom = new System.Windows.Forms.Timer();

            StartStopDelegate startStopDelegate = _StartStop;
            PlaySoundDelegate playSoundDelegate = _PlaySound;
            formController = new FormController(this, ctrlLog, startStopDelegate, playSoundDelegate);
            soundPlayer = new SoundPlayer(formController, ctrl_RandomVolume, ctrl_VolumeAmplifier);
            visualBoom = new VisualBoom(ctrlVisualBoom, formController, groupBoxVisualBoom, ctrl_FillRatio, 
                                        ctrl_RecalcVisualBoom, soundPlayer, ctrl_RepeatQty, ctrl_RepeatRandom);


            ctrl_Speed.Text = "1";
            ctrl_FillRatio.Text = "5";
            ctrl_RepeatQty.Text = "1";

            formController.LoggerAdd(_VERSION_);
            ReadSoundFolders();
            formController.InitFormConfig();
            formController.LoggerAdd("Config loaded from config.cfg");
            visualBoom.CalcArray();
            UpdateDesign();

            timer_boom.Interval = 1000;
            timer_boom.Tick += Timer_boom_Tick;
            timer_boom.Start();
            
            ToolTip toolTip0 = new ToolTip();
            toolTip0.SetToolTip(ctrl_SecondsToStop, "таймер выкл, сек");
            
        }

        void StartScheduler()
        {
            if (!ValidBeforeStartTimer())
            {
                formController.LoggerAdd("Scheduler is NOT started.");
                return;
            }

            formController.ClearLog();
            UpdateDesign();
            visualBoom.ResetCurPos();

            scheduleEnabled = true;
            speedCounter = 0;
            //Зафиксировать выбранный плейлист и файл, что бы во время обработки по шедулеру помнить.
            if (ctrl_SoundFolders.SelectedItem is String)
                selectedLST = (String)ctrl_SoundFolders.SelectedItem;
            if (ctrl_SoundFiles.SelectedItem is String)
                selectedFile = (String)ctrl_SoundFiles.SelectedItem;
            selectedFile = selectedFile.Split(" | ")[0];

            Int32.TryParse(ctrl_SecondsToStop.Text, out secondsToStop);
                

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

            if (Int32.TryParse(ctrl_SecondsToStop.Text, null, out Int32 checkSecondsToStop))
            {
                secondsToStop--;
                if (secondsToStop < 0)
                {
                    formController.LoggerAdd("Таймер истек. Остановка шедулера.");
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

                visualBoom.StartBoom(selectedLST, selectedFile, ctrl_SoundFolders, ctrl_SoundFiles);
            }
        }
        private void btnRecalcParams_Click(object sender, EventArgs e)
        {
            visualBoom.CalcArray();
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
            string selectedFld = (string)ctrl_SoundFolders.SelectedItem;
            string selectedFile = (string)ctrl_SoundFiles.SelectedItem;
            selectedFile = selectedFile.Split(" | ")[0];

            if ( selectedFile != null && selectedFld != null)
            {
                soundPlayer.PlaySound(".\\sounds\\" + (string)selectedFld +"\\"+ (string)selectedFile);
            }
        }
        private void BoomSosed_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        void ReadSoundFolders()
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
            if (val > 60 * 60/*час*/ || val <= 0)
            {
                ctrl_Speed.Text = "5";
            }
            //--------------------//-------------------- 3
            visualBoom.InitVisualBoomGrid();
            //--------------------//-------------------- 4 
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
