using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using NAudio.Wave;

namespace My_BoomSosed_NET
{
    public partial class BoomSosed_MainForm : Form
    {
        int MaxColSizeVisualBoom = 10;
        int MaxRowSizeVisualBoom = 10;
        Random random = new Random();
        void UpdateDesign()
        {
            //--------------------//-------------------- 1
            if (ctrl_LST.Items.Count == 0)
            {
                var soundsDir = Directory.EnumerateFiles(".\\sounds\\");

                foreach (var file in soundsDir)
                {
                    if (Path.GetExtension(file).ToLower() == ".lst")
                        ctrl_LST.Items.Add(file);
                }
            }
            //--------------------//-------------------- 2
            Int32 val = 0;
            Int32.TryParse(ctrl_Speed.Text, null, out val);
            if (val > 60 * 60/*час*/ || val <= 0)
            {
                ctrl_Speed.Text = "5";
                val = 5;
            }
            label_Speed.Text = $"1 шаг за {ctrl_Speed.Text} секунд.";
            //--------------------//-------------------- 3
            FillVisualBoomGrid();
            //--------------------//-------------------- 4 
        }
        private void AddLog(string s)
        {
            ctrlLog.Text += $"{DateTime.Now.ToLongTimeString()} : {s}\n";
            ctrlLog.SelectionStart = ctrlLog.Text.Length;
            ctrlLog.SelectionLength = 0;
            ctrlLog.ScrollToCaret();
            ctrlLog.Update();
        }
        private void FillVisualBoomGrid()
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

            Int32 val = 0;
            Int32.TryParse(ctrl_FillRatio.Text, null, out val);
            if (val > 99 || val < 1)
            {
                ctrl_FillRatio.Text = "10";
                val = 10;
            }
            double fillRatio = (double)val / 100;
            AddLog($"Start filling.");
            for (int row = 0; row < MaxRowSizeVisualBoom; row++)
            {
                for (int col = 0; col < MaxColSizeVisualBoom; col++)
                {
                    Panel panel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.White 
                    };
                    if (random.NextDouble() < fillRatio)
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
        void PlayRandomSoundFromList()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var selected = ctrl_LST.SelectedItem;
            if (selected is String && selected != null)
            {
                var lines = File.ReadAllLines((string)selected, Encoding.GetEncoding(1251));
                int fileSelectedNum = random.Next(lines.Count());
                PlayMp3(".\\sounds\\"+lines[fileSelectedNum]);
            }
        }
        public void PlayMp3(string filePath)
        {
            AddLog($"Boom! {filePath}");
            var audioFilePath = Path.GetDirectoryName(Application.ExecutablePath) + filePath;
            using (var audioFile = new AudioFileReader(audioFilePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.PlaybackStopped += (sender, e) =>
                {
                    AddLog("Воспроизведение завершено.");
                };
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
        int curRowSizeVisualBoom;
        int curColSizeVisualBoom;
        void StartBoom()
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
                FillVisualBoomGrid();
                return;
            }
            var panel = (Panel?)ctrlVisualBoom.GetControlFromPosition(curColSizeVisualBoom, curRowSizeVisualBoom);
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
    }
}
