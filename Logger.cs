namespace My_BoomSosed_NET
{
    enum ChannelType { Control, File, Registry };
    public class Logger
    {
        RichTextBox loggerControl;
        const string loggerFile = "log.txt";
        List<ChannelType> channels = new List<ChannelType>();
        public Logger(RichTextBox loggerControl)
        {
            this.loggerControl = loggerControl;
            channels.Add(ChannelType.Control); 
            channels.Add(ChannelType.File); 
        }
        public void __Add(string message)
        {
            loggerControl.Text += message;
            loggerControl.SelectionStart = loggerControl.Text.Length;
            loggerControl.SelectionLength = 0;
            loggerControl.ScrollToCaret();
            loggerControl.Update();
        }
        public void Add(string message)
        {
            try
            {
                bool endContainNL = message.Substring(message.Length - 1, 1).Contains("\n");
                message = message + (endContainNL ? "" : "\n");
                message = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()} : {message}";
                File.AppendAllText(loggerFile, message);

                if (loggerControl.InvokeRequired)
                {
                    loggerControl.Invoke((string m) => { __Add(m); }, message);
                }
                else
                {
                    loggerControl.Text += message;
                    loggerControl.SelectionStart = loggerControl.Text.Length;
                    loggerControl.SelectionLength = 0;
                    loggerControl.ScrollToCaret();
                    loggerControl.Update();
                }
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
