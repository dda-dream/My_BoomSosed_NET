using System.Text;

namespace My_BoomSosed_NET
{    
    public partial class BoomSosed_MainForm : Form
    {
        Logger logger;
        //System.Timers.Timer timer_boom = new System.Timers.Timer();
        System.Windows.Forms.Timer timer_boom = new ();

        public BoomSosed_MainForm()
        {
            InitializeComponent();
            logger = new Logger(ctrlLog);
            logger.Add("ver 08-05-2025 v0.1");
            ctrl_Speed.Text = "1";
            ctrl_FillRatio.Text = "5";
            CalcArray();
            UpdateDesign();
        }
        bool firstTimeTimer = true;
        bool timerEnabled = false;
        string selectedLST="";
        string selectedFile="";
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!timer_boom.Enabled)
            {
                if (!ValidBeforeStartTimer())
                {
                    logger.Add("Timer is NOT started.");
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
                if(ctrl_LST.SelectedItem is String)
                    selectedLST = (String)ctrl_LST.SelectedItem;
                if(ctrl_FilesInLST.SelectedItem is String)
                    selectedFile = (String)ctrl_FilesInLST.SelectedItem;
                logger.Add($"selected playlist: {selectedLST}");
                if(!string.IsNullOrEmpty(selectedFile))
                    logger.Add($"selected file: {selectedFile}");
                else
                    logger.Add($"selected file: RANDOM");
                logger.Add("Timer started.");
                btnStart.Text = "Stop";
                timer_boom.Start();
            }
            else
            {
                timer_boom.Stop();
                timerEnabled = false; 
                logger.Add("Timer stopped.");
                btnStart.Text = "Start";
            }
        }
        private void Timer_boom_Tick(object? sender, EventArgs e)
        {
            if (timerEnabled)
            {
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
            if (ctrl_LST.Items.Count != 0)
            {
                ctrl_FilesInLST.Items.Clear();
                var selected = ctrl_LST.SelectedItem;
                if (selected is String && selected != null)
                {
                    var lines = File.ReadAllLines((string)selected);
                    foreach (var line in lines)
                    {
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
            //logger.Add("Перевод на карту Сбер: ", Color.Blue);
        }

        private void ctrl_FilesInLST_DoubleClick(object sender, EventArgs e)
        {
            if (timer_boom.Enabled == false)
            {
                var selected = ctrl_FilesInLST.SelectedItem;
                if (selected is String && selected != null)
                {
                    PlayMp3(".\\sounds\\" + selected);
                }
            }
        }
    }
}
