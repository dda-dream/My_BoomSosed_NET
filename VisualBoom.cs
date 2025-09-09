using System.Windows.Forms.VisualStyles;

namespace My_BoomSosed_NET
{
    class VisualBoom
    {
        TableLayoutPanel ctrlVisualBoom;
        FormController formController;
        GroupBox groupBoxVisualBoom;
        TextBox ctrl_FillRatio;
        CheckBox ctrl_RecalcVisualBoom;
        SoundPlayer soundPlayer;
        int[,] arr;
        int curRowSizeVisualBoom;
        int curColSizeVisualBoom;
        public VisualBoom(TableLayoutPanel ctrlVisualBoom, FormController formController, GroupBox groupBoxVisualBoom, 
                          TextBox ctrl_FillRatio, CheckBox ctrl_RecalcVisualBoom, SoundPlayer soundPlayer)
        {
            this.ctrlVisualBoom = ctrlVisualBoom;
            this.formController = formController;
            arr = new int[formController.MaxRowSizeVisualBoom, formController.MaxColSizeVisualBoom];
            this.groupBoxVisualBoom = groupBoxVisualBoom;
            this.ctrl_FillRatio = ctrl_FillRatio;
            this.ctrl_RecalcVisualBoom = ctrl_RecalcVisualBoom;
            this.soundPlayer = soundPlayer;
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

            arr = formController.FillArrayWithRandomValues(val, formController.MaxRowSizeVisualBoom, formController.MaxColSizeVisualBoom);
        }
        public void InitVisualBoomGrid()
        {
            ctrlVisualBoom.Visible = false;
            ctrlVisualBoom.ColumnStyles.Clear();
            ctrlVisualBoom.RowStyles.Clear();
            ctrlVisualBoom.Controls.Clear();
            ctrlVisualBoom.RowCount = 0;
            ctrlVisualBoom.ColumnCount = 0;

            ctrlVisualBoom.RowCount = formController.MaxRowSizeVisualBoom;
            ctrlVisualBoom.ColumnCount = formController.MaxColSizeVisualBoom;
            ctrlVisualBoom.Dock = DockStyle.Fill;
            ctrlVisualBoom.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            for (int i = 0; i < 10; i++)
            {
                ctrlVisualBoom.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 10));
                ctrlVisualBoom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 10));
            }

            for (int row = 0; row < formController.MaxRowSizeVisualBoom; row++)
            {
                for (int col = 0; col < formController.MaxColSizeVisualBoom; col++)
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
            if (curColSizeVisualBoom < formController.MaxColSizeVisualBoom)
                curColSizeVisualBoom++;
            if (curColSizeVisualBoom >= formController.MaxColSizeVisualBoom)
            {
                curColSizeVisualBoom = 0;
                curRowSizeVisualBoom++;
            }
            if (curRowSizeVisualBoom >= formController.MaxRowSizeVisualBoom)
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
            if (!string.IsNullOrEmpty(selectedLST))
            {
                if (!string.IsNullOrEmpty(selectedFile))
                {
                    string selectedFld = (string)ctrl_SoundFolders.SelectedItem;
                    string selectedFileLocal = (string)ctrl_SoundFiles.SelectedItem;
                    if (selectedFileLocal != null && selectedFld != null)
                    {
                        soundPlayer.PlaySound(".\\sounds\\" + (string)selectedFld + "\\" + (string)selectedFileLocal);
                    }
                }
                else
                {
                    string randomFile = (string)ctrl_SoundFiles.Items[Random.Shared.Next(0, ctrl_SoundFiles.Items.Count)];
                    string selectedFld = (string)ctrl_SoundFolders.SelectedItem;
                    if (randomFile != null && selectedFld != null)
                    {
                        soundPlayer.PlaySound(".\\sounds\\" + (string)selectedFld + "\\" + (string)randomFile);
                    }
                }
            }
        }
    
    }
}
