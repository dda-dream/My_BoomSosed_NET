using System.Windows.Forms.VisualStyles;

namespace My_BoomSosed_NET
{
    class VisualBoom
    {
        TableLayoutPanel ctrlVisualBoom;
        FormController formController;
        GroupBox groupBoxVisualBoom;
        TextBox ctrl_FillRatio;
        TextBox ctrl_RepeatQty;
        CheckBox ctrl_RecalcVisualBoom;
        SoundPlayer soundPlayer;
        CheckBox ctrl_RepeatRandom;
        int[,] arr;
        int curRowSizeVisualBoom;
        int curColSizeVisualBoom;
        const int maxColSizeVisualBoom = 10;
        const int maxRowSizeVisualBoom = 10;
        public VisualBoom(TableLayoutPanel ctrlVisualBoom, FormController formController, GroupBox groupBoxVisualBoom, 
                          TextBox ctrl_FillRatio, CheckBox ctrl_RecalcVisualBoom, SoundPlayer soundPlayer,
                          TextBox ctrl_RepeatQty, CheckBox ctrl_RepeatRandom)
        {
            if (formController == null)
                throw new ArgumentNullException("FormController is null");
            if (soundPlayer == null) 
                throw new ArgumentNullException("SoundPlayer is null");

            this.ctrlVisualBoom = ctrlVisualBoom;
            this.formController = formController;
            arr = new int[maxRowSizeVisualBoom, maxColSizeVisualBoom];
            this.groupBoxVisualBoom = groupBoxVisualBoom;
            this.ctrl_FillRatio = ctrl_FillRatio;
            this.ctrl_RecalcVisualBoom = ctrl_RecalcVisualBoom;
            this.soundPlayer = soundPlayer;
            this.ctrl_RepeatQty = ctrl_RepeatQty;
            this.ctrl_RepeatRandom = ctrl_RepeatRandom;
        }
        public void ResetCurPos()
        {
            curRowSizeVisualBoom = 0;
            curColSizeVisualBoom = -1;
        }
 
        public void CalcArray()
        {
            Int32.TryParse(ctrl_FillRatio.Text, null, out Int32 val);
            if (val > 100 || val < 1)
            {
                ctrl_FillRatio.Text = "5";
                val = 10;
            }

            arr = FillArrayWithRandomValues(val, maxRowSizeVisualBoom, maxColSizeVisualBoom);
        }
        public void InitVisualBoomGrid()
        {
            ctrlVisualBoom.Visible = false;
            ctrlVisualBoom.ColumnStyles.Clear();
            ctrlVisualBoom.RowStyles.Clear();
            ctrlVisualBoom.Controls.Clear();
            ctrlVisualBoom.RowCount = 0;
            ctrlVisualBoom.ColumnCount = 0;

            ctrlVisualBoom.RowCount = maxRowSizeVisualBoom;
            ctrlVisualBoom.ColumnCount = maxColSizeVisualBoom;
            ctrlVisualBoom.Dock = DockStyle.Fill;
            ctrlVisualBoom.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < 10; i++)
            {
                ctrlVisualBoom.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 10));
                ctrlVisualBoom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 10));
            }

            for (int row = 0; row < maxRowSizeVisualBoom; row++)
            {
                for (int col = 0; col < maxColSizeVisualBoom; col++)
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

        public void StartBoom(string selectedLST, string selectedFile, ListBox ctrl_SoundFolders, ListBox ctrl_SoundFiles)
        {
            if (curColSizeVisualBoom < maxColSizeVisualBoom)
                curColSizeVisualBoom++;
            if (curColSizeVisualBoom >= maxColSizeVisualBoom)
            {
                curColSizeVisualBoom = 0;
                curRowSizeVisualBoom++;
            }
            if (curRowSizeVisualBoom >= maxRowSizeVisualBoom)
            {
                curColSizeVisualBoom = -1;
                curRowSizeVisualBoom = 0;
                if (ctrl_RecalcVisualBoom.Checked)
                {
                    CalcArray();
                }
                InitVisualBoomGrid();

                return;
            }
            var panel = (Panel?)ctrlVisualBoom.GetControlFromPosition(curColSizeVisualBoom, curRowSizeVisualBoom);
            if (panel is Panel)
                panel.BackColor = Color.Yellow;

            if (panel != null && panel.Controls.Count > 0)
            {
                if (panel.Controls[0] is Label label)
                {
                    if (label != null && label.Text == "*!*")
                    {
                        PlayRandomSoundFromList( selectedLST, selectedFile, ctrl_SoundFolders, ctrl_SoundFiles);
                    }
                }
            }
        }
        public void PlayRandomSoundFromList(string selectedLST, string selectedFile, ListBox ctrl_SoundFolders, ListBox ctrl_SoundFiles)
        {
            int repeatQty;

            if(int.TryParse(ctrl_RepeatQty.Text, out repeatQty))
            {
                if(ctrl_RepeatRandom.Checked)
                {
                    repeatQty = Random.Shared.Next(1, repeatQty);
                }
            }
            else
            {
                repeatQty = 1;
            }

            if (!string.IsNullOrEmpty(selectedLST))
            {
                if (!string.IsNullOrEmpty(selectedFile))
                {
                    string selectedFld = (string)ctrl_SoundFolders.SelectedItem;
                    string selectedFileLocal = (string)ctrl_SoundFiles.SelectedItem;
                    selectedFileLocal = selectedFileLocal.Split(" | ")[0];
                    if (selectedFileLocal != null && selectedFld != null)
                    {
                        soundPlayer.PlaySound(".\\sounds\\" + (string)selectedFld + "\\" + (string)selectedFileLocal, repeatQty);
                    }
                }
                else
                {
                    string randomFile = (string)ctrl_SoundFiles.Items[Random.Shared.Next(0, ctrl_SoundFiles.Items.Count)];
                    randomFile = randomFile.Split(" | ")[0];

                    string selectedFld = (string)ctrl_SoundFolders.SelectedItem;
                    if (randomFile != null && selectedFld != null)
                    {
                        soundPlayer.PlaySound(".\\sounds\\" + (string)selectedFld + "\\" + (string)randomFile, repeatQty);
                    }
                }
            }
        }
        public int[,] FillArrayWithRandomValues(int fillPercentage = 10, int rows = 10, int columns = 10)
        {
            int[,] array = new int[rows, columns];

            int onesCount = rows * columns * fillPercentage / 100;
            var indices = new List<(int row, int col)>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    indices.Add((i, j));
                }
            }

            for (int i = indices.Count - 1; i > 0; i--)
            {
                int j = Random.Shared.Next(i + 1);
                (indices[i], indices[j]) = (indices[j], indices[i]);
            }

            for (int i = 0; i < onesCount; i++)
            {
                var (row, col) = indices[i];
                array[row, col] = 1;
            }
            return array;
        }
    
    }
}
