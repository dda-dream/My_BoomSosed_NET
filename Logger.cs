namespace My_BoomSosed_NET
{
    public class Logger
    {
        RichTextBox loggerControl;
        const string loggerFile = "log.txt";
        enum ChannelType { Control, File, Registry };
        List<ChannelType> channels = new List<ChannelType>();
        public Logger(RichTextBox loggerControl)
        {
            this.loggerControl = loggerControl;
            channels.Add(ChannelType.Control); 
            channels.Add(ChannelType.File); 
        }
        public delegate void _Add(string message);
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
                message = $"{DateTime.Now.ToLongTimeString()} : {message}";
                File.AppendAllText(loggerFile, message);
                //if (toFileOnly)
                //    return;

                if (loggerControl.InvokeRequired)
                {
                    _Add _Add = __Add;
                    loggerControl.Invoke(_Add, message);
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

        public IAsyncResult BeginInvoke(Delegate method, object?[]? args)
        {
            throw new NotImplementedException();
        }

        public object? EndInvoke(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public object? Invoke(Delegate method, object?[]? args)
        {
            throw new NotImplementedException();
        }
    }
}
