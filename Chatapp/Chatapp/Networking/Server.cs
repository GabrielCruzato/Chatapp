using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatClient.Networking.IO;

namespace ChatClient.Networking
{

     public class Server
    {
        TcpClient client;

        public Server()
        {
            client = new TcpClient();
        }

        public void ConnectToServer(string Username)
        {
            if (!client.Connected)
            {   
                client.Connect("127.0.0.1", 50922);
                var connectPacket = new PacketBuilder();
                connectPacket.WriteOpCode(0); 
                connectPacket.WriteString(Username);
                client.Client.Send(connectPacket.GetPacket());

                Console.WriteLine("Connected to server");
            }
            else
            {
                Console.WriteLine("Failed to connect to server");
            }
        }
    }
}
