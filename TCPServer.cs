using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace My_BoomSosed_NET
{
    /// <summary>
    /// TCPServer - server for Boom command. 
    /// </summary>
    public class TCPServer
    { 
        public int Port { get; set; }
        TcpListener listener;
        public TCPServer(int Port = 60006)
        {
            this.Port = Port;
            listener = new TcpListener(IPAddress.Any, this.Port);
            listener.Start();
        }
        public async void Start()
        {
            using (TcpClient client = listener.AcceptTcpClient())
            {
                using (StreamReader reader = new StreamReader(client.GetStream()))
                {
                    string text = reader.ReadToEnd();
                }
            }
        }
    }
}
