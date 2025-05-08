using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace My_BoomSosed_NET
{
    public partial class BoomSosed_MainForm : Form
    {
        int colSizeVisualBoom = 10;
        int rowSizeVisualBoom = 10;
        Random random = new Random();
        void UpdateDesign()
        {
            //--------------------//-------------------- 1
            ctrl_LST.Items.Clear();
            var soundsDir = Directory.EnumerateFiles(".\\sounds\\");

            foreach (var file in soundsDir)
            {
                if (Path.GetExtension(file).ToLower() == ".lst")
                    ctrl_LST.Items.Add(file);
            }
            //--------------------//-------------------- 2
            Int32 val = 0;
            Int32.TryParse(ctrl_Speed.Text, null, out val);
            if (val > 60 * 60/*час*/ || val <= 0)
            {
                ctrl_Speed.Text = "5";
                val = 5;
            }
            label_Speed.Text = String.Format("1 шаг за {0} секунд.", ctrl_Speed.Text);
            //--------------------//-------------------- 3
            FillVisualBoomGrid();
            //--------------------//-------------------- 4 
        }
        private void FillVisualBoomGrid()
        {
            ctrlVisualBoom.Visible = false;
            ctrlVisualBoom.ColumnStyles.Clear();
            ctrlVisualBoom.RowStyles.Clear();
            ctrlVisualBoom.Controls.Clear();
            ctrlVisualBoom.RowCount = 0;
            ctrlVisualBoom.ColumnCount = 0;

            ctrlVisualBoom.RowCount = rowSizeVisualBoom;
            ctrlVisualBoom.ColumnCount = colSizeVisualBoom;
            ctrlVisualBoom.Dock = DockStyle.Fill;
            ctrlVisualBoom.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < 10; i++)
            {
                ctrlVisualBoom.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 10));
                ctrlVisualBoom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 10));
            }

            Int32 val = 0;
            Int32.TryParse(ctrl_FillRatio.Text, null, out val);
            if (val > 99 || val < 1)
            {
                ctrl_FillRatio.Text = "10";
                val = 10;
            }
            double fillRatio = (double)val / 100;
            for (int row = 0; row < rowSizeVisualBoom; row++)
            {
                for (int col = 0; col < colSizeVisualBoom; col++)
                {
                    Panel panel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.White 
                    };
                    if (random.NextDouble() < fillRatio)
                    {
                        panel.BackColor = Color.Green;
                        panel.Text = "*";
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
        void StartBoom()
        {

            for (int row = 0; row < rowSizeVisualBoom; row++)
            {
                for (int col = 0; col < colSizeVisualBoom; col++)
                {
                    var panel = (Panel?)ctrlVisualBoom.GetControlFromPosition(col, row);
                    if(panel != null && panel.BackColor != Color.White)
                    {
                        var isThislabel = panel.Controls[0];
                        if (isThislabel is Label)
                        {
                            var label = (Label)isThislabel;
                            if (label != null && label.Text == "*!*")
                            {
                                //PlayRandomSoundFromList();
                            }
                        }
                    }
                }
            }
        }
    }
}
