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
        public int[,] FillArrayWithRandomValues(int fillPercentage = 10, int rows = 10, int columns = 10)
        {
        // Проверка входных параметров
        if (fillPercentage < 0 || fillPercentage > 100)
            throw new ArgumentException("Коэффициент заполнения должен быть в диапазоне от 0 до 100.");
        if (rows <= 0 || columns <= 0)
            throw new ArgumentException("Размер массива должен быть положительным числом.");

        int[,] array = new int[rows, columns];
        Random random = new Random();

        // Общее количество элементов в массиве
        int totalElements = rows * columns;

        // Количество единиц, которые нужно разместить
        int onesCount = (int)(totalElements * fillPercentage / 100.0);

        // Создаем список индексов для случайного размещения единиц
        var indices = new System.Collections.Generic.List<(int row, int col)>();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                indices.Add((i, j));
            }
        }

        // Перемешиваем индексы
        for (int i = indices.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (indices[i], indices[j]) = (indices[j], indices[i]);
        }

        // Размещаем единицы
        for (int i = 0; i < onesCount; i++)
        {
            var (row, col) = indices[i];
            array[row, col] = 1;
        }

        return array;
    }
        public void FillVisualBoomGrid()
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

            int[,] arr = FillArrayWithRandomValues(val, MaxRowSizeVisualBoom, MaxColSizeVisualBoom);

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
            logger.Add($"Boom! {filePath}");
            var audioFilePath = Path.GetDirectoryName(Application.ExecutablePath) + filePath;
            using (var audioFile = new AudioFileReader(audioFilePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.PlaybackStopped += (sender, e) =>
                {
                    logger.Add("Воспроизведение завершено.");
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
                if(ctrl_RecalcVisualBoom.Enabled)
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
        bool ValidBeforeStartTimer()
        {
            bool retVal = true;

            var selected = ctrl_LST.SelectedItem;
            if (selected == null)
            {
                logger.Add("Choose PlayList!");
                retVal = false;
            }
            return retVal;
        }
    }
}
