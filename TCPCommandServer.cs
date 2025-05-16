using System.IO;
using System.Net;
using System.Net.Sockets;

namespace My_BoomSosed_NET
{
    /// <summary>
    /// TCPCommandServer - TCP server for Boom command. 
    /// </summary>
    public class TCPCommandServer
    { 
        int Port { get; set; }
        TcpListener listener;
        Logger logger;
        public TCPCommandServer(Logger logger, int Port = 60006)
        {
            this.logger = logger;
            this.Port = Port;
            listener = new TcpListener(IPAddress.Any, this.Port);
            listener.Start();
        }
        public string StartAndWaitCommand()
        {
            string text="";
            try
            {
                TcpClient client = listener.AcceptTcpClient();
                logger.Add("StartAndWaitCommand: Timeout 60 sec. Client accepted: " + client.Client.RemoteEndPoint, true);

                var welcomeWriter = new StreamWriter(client.GetStream());
                welcomeWriter.WriteLine("Welcome! This is BoomSosed interface! 60 sec timeout.");
                welcomeWriter.Flush();

                client.ReceiveTimeout = 60 * 1000;
                using (StreamReader reader = new StreamReader(client.GetStream()))
                    text = reader.ReadToEnd();
                
                listener.Stop();
                logger.Add($"StartAndWaitCommand: text: ({text})", true);
            }
            catch (Exception ex)
            {
                listener.Stop();
                //logger.Add(ex.ToString(), true);
                logger.Add("TCPCommandServer:StartAndWaitCommand - ошибка.", true);
            }
            
            return text;
        }
    }
}
