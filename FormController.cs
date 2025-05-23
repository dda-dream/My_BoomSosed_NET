using System.Reflection;

namespace My_BoomSosed_NET
{
    public class FormController
    {
        MainForm form;
        Config config;
        Logger logger;
        Delegate startStop;
        Delegate playSelectedSound;
        public int MaxColSizeVisualBoom = 10;
        public int MaxRowSizeVisualBoom = 10;

        public FormController(Control form, RichTextBox logControl, Delegate startStop, Delegate playSelectedSound )
        {
            this.form = (MainForm)form;
            logger = new Logger(logControl);
            config = new Config(logger);
            this.startStop = startStop;
            this.playSelectedSound = playSelectedSound;

            var a0 = typeof(MainForm);

            var a1 = a0.GetFields();
            var a2 = a0.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            var serializableFields = 
                typeof(MainForm).GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                .Where(f => f.GetCustomAttribute<SaveToConfigAttribute>() != null);
            foreach (var field in serializableFields)
            {
                var attribute = field.GetCustomAttribute<SaveToConfigAttribute>();
                string fieldName = /*attribute.Name ??*/ field.Name;
                logger.Add($"SaveToConfigFile:  {fieldName}");
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

        public void LoggerAdd(string s)
        {
            logger.Add(s);
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
                logger.Add("Command not supported.");
            }
        }
        public async void StartCommandServer()
        {
            while (true)
            {
                string command = await Task<string>.Run(StartCommandServerAsync);
                if (command != "")
                {
                    logger.Add("ProcessCommand: " + command);
                    ProcessCommand(command);
                }
            }
        }
        public string /*async Task<string>*/ StartCommandServerAsync()
        {
            TCPCommandServer tcpServer;

            logger.Add("Starting command server at port: 60006");

            tcpServer = new TCPCommandServer(logger);
            string command = tcpServer.StartAndWaitCommand();
        
            return command;
        }

    }
}
