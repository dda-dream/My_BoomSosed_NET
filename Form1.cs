using System.Text;

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
                UpdateDesign();
                curRowSizeVisualBoom = 0;
                curColSizeVisualBoom = -1;
                Int32 val = 0;
                Int32.TryParse(ctrl_Speed.Text, null, out val);
                timer_boom.Interval = val * 1000;
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
        private void ctrl_LST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ctrl_LST.Items.Count != 0)
            {
                ctrl_FilesInLST.Items.Clear();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var selected = ctrl_LST.SelectedItem;
                if (selected is String && selected != null)
                {
                    var lines = File.ReadAllLines((string)selected, Encoding.GetEncoding(1251));
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
                AddLog("//PlayFile();");
                PlayMp3(".\\sounds\\" + selected);
            }
        }
    }
}
