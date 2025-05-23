using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Networking
{

     public class Server
    {
        TcpClient client;

        public Server(string ip, int port)
        {
            client = new TcpClient(ip, port);
        }

        public void ConnectToServer()
        {
            if (!client.Connected)
            {   
                client.Connect("127.0.0.1", 7891);
                Console.WriteLine("Connected to server");
            }
            else
            {
                Console.WriteLine("Failed to connect to server");
            }
        }
    }
}
