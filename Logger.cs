using System.Reflection;
using System.Runtime.CompilerServices;

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
        //public void __Add(string message)
        //{
        //    loggerControl.Text += message;
        //    loggerControl.SelectionStart = loggerControl.Text.Length;
        //    loggerControl.SelectionLength = 0;
        //    loggerControl.ScrollToCaret();
        //    loggerControl.Update();
        //}
        public void Add(string message)
        {
            try
            {
                bool endContainNL = message.Substring(message.Length - 1, 1).Contains("\n");
                message = message + (endContainNL ? "" : "\n");
                message = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()} : {message}";
                File.AppendAllText(loggerFile, message);

                var _add = new Action<string>((string type) => {
                    loggerControl.Text += type + message;
                    loggerControl.SelectionStart = loggerControl.Text.Length;
                    loggerControl.SelectionLength = 0;
                    loggerControl.ScrollToCaret();
                    loggerControl.Update();
                });

                if (loggerControl.InvokeRequired)
                {
                    loggerControl.Invoke(_add, "i: ");
                }
                else
                {
                    _add("");
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
