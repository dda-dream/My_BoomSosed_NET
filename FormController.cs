using NAudio.Wave;
using System.Reflection;

namespace My_BoomSosed_NET
{
    class FormController
    {
        BoomSosed_MainForm form;
        List<Control> controls;
        Config config;
        Logger logger;



        public FormController(Control form)
        {
            this.form = (BoomSosed_MainForm)form;
            controls = new List<Control>();
            foreach (Control control in form.Controls)
            {
                controls.AddRange( GetAllControls(control) );
            }
            foreach (var control in controls)
            {
                if (control.Name == "ctrlLog" && control is RichTextBox)
                {
                    logger = new Logger((RichTextBox)control);
                    break;
                }
            }
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
            controls.Find( x => x.Name == "ctrl_Speed").Text = config.Get("ctrl_Speed");
            controls.Find( x => x.Name == "ctrl_FillRatio").Text = config.Get("ctrl_FillRatio");
            ((CheckBox)controls.Find(x => x.Name == "ctrl_RecalcVisualBoom")).Checked = config.Get("ctrl_RecalcVisualBoom").ToLower() == "true";
        }
        public void SafeToConfig()
        {
            

            config.Add("ctrl_Speed", controls.Find( x => x.Name == "ctrl_Speed").Text);
            config.Add("ctrl_FillRatio", controls.Find( x => x.Name == "ctrl_FillRatio").Text);
            config.Add("ctrl_RecalcVisualBoom", ((CheckBox)controls.Find(x => x.Name == "ctrl_RecalcVisualBoom")).Checked.ToString().ToLower());
            config.Add("ctrl_LST", controls.Find( x => x.Name == "ctrl_LST").Text);
            config.Add("ctrl_FilesInLST", controls.Find( x => x.Name == "ctrl_FilesInLST").Text);
            config.Save();
        }

    }
}
