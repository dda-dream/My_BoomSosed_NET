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
                    checkBox.Checked = true;
                }
                else
                {
                    control.Text = config.Get(fieldName);
                }
                
            }

            /*
            controls.Find( x => x.Name == "ctrl_Speed").Text = config.Get("ctrl_Speed");
            controls.Find( x => x.Name == "ctrl_FillRatio").Text = config.Get("ctrl_FillRatio");
            ((CheckBox)controls.Find(x => x.Name == "ctrl_RecalcVisualBoom")).Checked = config.Get("ctrl_RecalcVisualBoom").ToLower() == "true";
            */
        }
        public void SafeToConfig()
        {            
            /*
            config.Add("ctrl_Speed", controls.Find( x => x.Name == "ctrl_Speed").Text);
            config.Add("ctrl_FillRatio", controls.Find( x => x.Name == "ctrl_FillRatio").Text);
            config.Add("ctrl_RecalcVisualBoom", ((CheckBox)controls.Find(x => x.Name == "ctrl_RecalcVisualBoom")).Checked.ToString().ToLower());
            config.Add("ctrl_LST", controls.Find( x => x.Name == "ctrl_LST").Text);
            config.Add("ctrl_FilesInLST", controls.Find( x => x.Name == "ctrl_FilesInLST").Text);
            config.Save();
            */
        }

    }
}
