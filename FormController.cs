using System.Reflection;

namespace My_BoomSosed_NET
{
    public class FormController
    {
        MainForm form;
        Config config;
        public Logger Logger { get; }
        Delegate startStop;
        Delegate playSelectedSound;
        ListBox ctrl_SoundFolders;
        TextBox ctrl_Speed;

        public FormController( Control form, RichTextBox logControl, Delegate startStop, Delegate playSelectedSound,
            ListBox ctrl_SoundFolders, TextBox ctrl_Speed)
        {
            Logger = new Logger(logControl);
            config = new Config(Logger);

            this.form = (MainForm)form;
            this.startStop = startStop;
            this.playSelectedSound = playSelectedSound;
            this.ctrl_SoundFolders = ctrl_SoundFolders;
            this.ctrl_Speed = ctrl_Speed;

            var serializableFields = 
                typeof(MainForm).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                .Where(f => f.GetCustomAttribute<SaveToConfigAttribute>() != null);

            foreach (var field in serializableFields)
            {
                var attribute = field.GetCustomAttribute<SaveToConfigAttribute>();
                string fieldName = /*attribute.Name ??*/ field.Name;
                Logger.Add($"SaveToConfigFile:  {fieldName}");
            }
        }


        public bool ValidBeforeStartTimer()
        {
            bool retVal = true;

            var selected = ctrl_SoundFolders.SelectedItem;
            if (selected == null)
            {
                Logger.Add("Choose PlayList!");
                MessageBox.Show("Choose PlayList!");
                retVal = false;
            }
            return retVal;
        }


        public void ReadSoundFolders()
        {
            var soundsDir = Directory.EnumerateDirectories(".\\sounds\\");

            foreach (var folder in soundsDir)
            {
                ctrl_SoundFolders.Items.Add(folder.Replace(".\\sounds\\", ""));
            }
        }
        public void UpdateDesign()
        {
            Int32.TryParse(ctrl_Speed.Text, null, out Int32 val);
            if (val > 60 * 60/*час*/ || val <= 0)
            {
                ctrl_Speed.Text = "5";
            }
        }

        public static List<Control> GetAllControls(Control parent)
        {
            List<Control> controlList = new List<Control>();
            controlList.Add(parent);

            foreach (Control child in parent.Controls)
            {
                controlList.AddRange(GetAllControls(child));
            }

            return controlList;
        }
        public void InitFormConfig()
        {
            var attributedFields = typeof(MainForm)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute<SaveToConfigAttribute>() != null);

            foreach (var field in attributedFields)
            {
                if (config.Get(field.Name).Trim() == "")
                    continue;

                var control = form.Controls.Find(field.Name, true).FirstOrDefault();

                if (control == null)
                    continue;

                if (control is CheckBox checkBox)
                {
                    checkBox.Checked = config.Get(field.Name).ToString().ToLower() == "true";
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Text = config.Get(field.Name);
                } 
                else
                {
                    control.Text = config.Get(field.Name);
                }
            }
        }
        public void SafeToConfig()
        {      
            var attributedFields = typeof(MainForm)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute<SaveToConfigAttribute>() != null);
            foreach (var field in attributedFields)
            {
                var control = form.Controls.Find(field.Name, true).FirstOrDefault();
                if (control == null)
                    continue;

                if (control is CheckBox checkBox)
                {
                    config.Add(field.Name, checkBox.Checked.ToString().ToLower());
                }
                else if (control is DateTimePicker dateTimePicker)
                { 
                    config.Add(field.Name, dateTimePicker.Value.ToLongTimeString());
                } 
                else
                {
                    config.Add(field.Name, control.Text);
                }
            }
            config.Save();
        }
        /// <summary>
        /// Commands supported: 
        ///     play_sound - play once choosen file(or random) from selected playlist
        ///     start - start scheduler
        ///     stop - stop scheduler
        /// </summary>
        /// <param name="command"></param>
        public void ProcessCommand(string command)
        {
            if (command.Trim().Contains("play_sound"))
            {
                playSelectedSound.Method.Invoke(form, null);
            }
            else if (command.Trim().Contains("start") || command.Trim().Contains("stop"))
            {
                string[] _command = { command };
                startStop.Method.Invoke(form, _command);
            }
            else
            {
                Logger.Add("Command not supported.");
            }
        }
        public async void StartCommandServer()
        {
            while (true)
            {
                await Task.Delay(1000);
                string command = await Task<string>.Run(StartCommandServerAsync);
                if (command != "")
                {
                    Logger.Add("ProcessCommand: " + command);
                    ProcessCommand(command);
                }
            }
        }
        public string StartCommandServerAsync()
        {
            TCPCommandServer tcpServer;

            Logger.Add("Starting command server at port: 60006");

            tcpServer = new TCPCommandServer(Logger);
            string command = tcpServer.StartAndWaitCommand();
        
            return command;
        }
    }
}
