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
            StartBoom();
        }

        private void ctrl_Speed_TextChanged(object sender, EventArgs e)
        {
            UpdateDesign();
        }

        private void ctrl_FillRatio_TextChanged(object sender, EventArgs e)
        {
            UpdateDesign();
        }
    }
}
