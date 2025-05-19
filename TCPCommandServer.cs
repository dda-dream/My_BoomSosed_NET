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
        }
        public string StartAndWaitCommand()
        {
            string text="";
            try
            {
                using TcpListener listener = new TcpListener(IPAddress.Any, this.Port);
                listener.Start();

                using TcpClient client = listener.AcceptTcpClient();
                logger.Add("Client accepted: " + client.Client.RemoteEndPoint);

                using StreamWriter welcomeWriter = new StreamWriter(client.GetStream());
                welcomeWriter.WriteLine("Welcome! This is BoomSosed interface! 60 sec timeout.");
                welcomeWriter.WriteLine("Commands supported:play_sound, scheduler_start, scheduler_stop");
                welcomeWriter.Flush();

                client.ReceiveTimeout = 60 * 1000;
                using StreamReader reader = new StreamReader(client.GetStream());

                text = reader.ReadToEnd();

                logger.Add($"StartAndWaitCommand: text: ({text})");
            }
            catch (IOException ex) when (ex.InnerException is SocketException socketEx)
            {
                logger.Add($"TCPCommandServer:System.IO.IOException - ошибка:{socketEx.SocketErrorCode.ToString()}");
            }
            catch (SocketException ex)
            {
                logger.Add($"TCPCommandServer:StartAndWaitCommand - ошибка. \n{ex.ToString()}\n");
            }
            finally
            {
            }
            
            return text;
        }
    }
}
