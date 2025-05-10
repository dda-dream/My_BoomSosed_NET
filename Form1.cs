using System.Text;

namespace My_BoomSosed_NET
{
    public partial class BoomSosed_MainForm : Form
    {
        Logger logger;
        public BoomSosed_MainForm()
        {
            InitializeComponent();
            logger = new Logger(ctrlLog);
            logger.Add("ver 08-05-2025 v0.1");
            ctrl_Speed.Text = "1";
            ctrl_FillRatio.Text = "5";
            UpdateDesign();

        }
        bool firstTimeTimer = true;
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
                timer_boom.Start();
                if (firstTimeTimer)
                {
                    timer_boom.Tick += Timer_boom_Tick;
                    firstTimeTimer = false;
                }
                timer_boom.Enabled = true;
                logger.Add("Timer started.");
            }
            else
            {
                timer_boom.Enabled = false;
                logger.Add("Timer stopped.");
            }
        }

        private void Timer_boom_Tick(object? sender, EventArgs e)
        {
            StartBoom();
        }

        private void btnRecalcParams_Click(object sender, EventArgs e)
        {
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
                }
            }
        }
        private void ctrl_FilesInLST_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = ctrl_FilesInLST.SelectedItem;
            if (selected is String && selected != null)
            {
                PlayMp3(".\\sounds\\" + selected);
            }
        }
    }
}
