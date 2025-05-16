namespace My_BoomSosed_NET
{
    public class Logger
    {
        RichTextBox loggerControl;
        const string loggerFile = "log.txt";
        public Logger(RichTextBox loggerControl)
        {
            this.loggerControl = loggerControl;
        }
        public void Add(string s, bool toFileOnly = false)
        {
            try
            {
                bool endContainNL = s.Substring(s.Length - 1, 1).Contains("\n");
                s = s + (endContainNL ? "" : "\n");
                s = $"{DateTime.Now.ToLongTimeString()} : {s}";
                File.AppendAllText(loggerFile, s);
                if (toFileOnly)
                    return;

                loggerControl.Text += s;
                loggerControl.SelectionStart = loggerControl.Text.Length;
                loggerControl.SelectionLength = 0;
                loggerControl.ScrollToCaret();
                loggerControl.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Clear()
        {
            loggerControl.Text = "";
        }
    }
}
