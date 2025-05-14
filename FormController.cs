using NAudio.Wave;
using System;
using System.Reflection;

namespace My_BoomSosed_NET
{
    class FormController
    {
        BoomSosed_MainForm form;
        Config config;
        Logger logger;



        public FormController(Control form, RichTextBox logControl)
        {
            this.form = (BoomSosed_MainForm)form;
            logger = new Logger(logControl);
            config = new Config(logger);

            var a0 = typeof(BoomSosed_MainForm);

            var a1 = a0.GetFields();
            var a2 = a0.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            var serializableFields = typeof(BoomSosed_MainForm)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute<SaveToConfigFileAttribute>() != null);
            foreach (var field in serializableFields)
            {
                var attribute = field.GetCustomAttribute<SaveToConfigFileAttribute>();
                string fieldName = attribute.Name ?? field.Name;
                logger.Add($"SaveToConfigFileAttribute:  {fieldName}");
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
            var serializableFields = typeof(BoomSosed_MainForm)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute<SaveToConfigFileAttribute>() != null);
            foreach (var field in serializableFields)
            {
                var attribute = field.GetCustomAttribute<SaveToConfigFileAttribute>();
                string fieldName = attribute.Name ?? field.Name;

                var control = form.Controls.Find(fieldName, true).FirstOrDefault();
                if (control is CheckBox checkBox)
                {
                    checkBox.Checked = config.Get(fieldName).ToString().ToLower() == "true";
                }
                else
                {
                    control.Text = config.Get(fieldName);
                }
            }
        }
        public void SafeToConfig()
        {      
            var serializableFields = typeof(BoomSosed_MainForm)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute<SaveToConfigFileAttribute>() != null);
            foreach (var field in serializableFields)
            {
                var attribute = field.GetCustomAttribute<SaveToConfigFileAttribute>();
                string fieldName = attribute.Name ?? field.Name;

                var control = form.Controls.Find(fieldName, true).FirstOrDefault();
                if (control is CheckBox checkBox)
                {
                    config.Add(fieldName, checkBox.Checked.ToString().ToLower());
                }
                else
                {
                    config.Add(fieldName, control.Text);
                }
            }

            config.Save();
        }
    }
}
