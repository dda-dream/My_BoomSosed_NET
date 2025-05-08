namespace My_BoomSosed_NET
{
    public partial class BoomSosed_MainForm : Form
    {
        public BoomSosed_MainForm()
        {
            InitializeComponent();

            UpdateDesign();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!timer_boom.Enabled)
            {
                Int32 val = 0;
                Int32.TryParse(ctrl_Speed.Text, null, out val);
                timer_boom.Interval = val*1000;
                timer_boom.Start();
                timer_boom.Tick += Timer_boom_Tick;
                timer_boom.Enabled = true;
                AddLog("Timer start.");
            }
            else
            {
                timer_boom.Enabled = false;
                AddLog("Timer end.");
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
    }
}
