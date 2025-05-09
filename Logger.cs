namespace My_BoomSosed_NET
{
    class Logger
    {
        RichTextBox loggerControl;

        public Logger(RichTextBox loggerControl)
        {
            this.loggerControl = loggerControl;
        }
        public void AddLog(string s)
        {
            loggerControl.Text += $"{DateTime.Now.ToLongTimeString()} : {s}\n";
            loggerControl.SelectionStart = loggerControl.Text.Length;
            loggerControl.SelectionLength = 0;
            loggerControl.ScrollToCaret();
            loggerControl.Update();
        }
    }
}
