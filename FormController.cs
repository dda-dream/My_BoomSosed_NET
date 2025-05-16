using NAudio.MediaFoundation;
using NAudio.Wave;
using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices.ObjectiveC;

namespace My_BoomSosed_NET
{
    public class FormController
    {
        MainForm form;
        Config config;
        Logger logger;
        Delegate startStop;
        Delegate playSelectedSound;

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

            var serializableFields = typeof(MainForm)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute<SaveToConfigAttribute>() != null);
            foreach (var field in serializableFields)
            {
                var attribute = field.GetCustomAttribute<SaveToConfigAttribute>();
                string fieldName = attribute.Name ?? field.Name;
                logger.Add($"SaveToConfigFile:  {fieldName}");
            }
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
        ///     scheduler_start - start scheduler
        ///     scheduler_stop - stop scheduler
        /// </summary>
        /// <param name="command"></param>
        public void ProcessCommand(string command)
        {
            if (command.Trim().Contains("play_sound"))
            {
                playSelectedSound.Method.Invoke(form, null);
            }
            else if (command.Trim().Contains("scheduler_start") || command.Trim().Contains("scheduler_stop"))
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
                    logger.Add("ProcessCommand: " + command, true);
                    ProcessCommand(command);
                }
            }
        }
        public string /*async Task<string>*/ StartCommandServerAsync()
        {
            TCPCommandServer tcpServer;

            logger.Add("Starting command server at port: 60006", true);
            logger.Add("Commands supported:play_sound, scheduler_start, scheduler_stop");

            tcpServer = new TCPCommandServer(logger);
            string command = tcpServer.StartAndWaitCommand();
        
            return command;
        }

    }
}
