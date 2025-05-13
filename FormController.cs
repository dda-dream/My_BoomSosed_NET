namespace My_BoomSosed_NET
{
    class FormController
    {
        //BoomSosed_MainForm form;
        List<Control> controls;
        Config config;
        Logger logger;
        public FormController(Control form)
        {
            //this.form = form;
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
    }
}
