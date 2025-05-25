using ChatServer.Network.IO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        static List<Client> users = new List<Client>();

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 50922);
            listener.Start();

            Console.WriteLine("Server started. Waiting for connections...");

            while (true)
            {
                var tcpClient = listener.AcceptTcpClient();
                Console.WriteLine("Client connecting...");

                var client = new Client(tcpClient);

                // Agora usando OnUserConnected
                Task.Run(() =>
                {
                    client.Listen(() => OnUserConnected(client));
                });
            }

        }

        static void OnUserConnected(Client client)
        {
            lock (users)
            {
                users.Add(client);
            }

            BroadcastConnection(client);
        }

        static void BroadcastConnection(Client newUser)
        {
            foreach (var user in users)
            {
                if (user.UID != newUser.UID)
                {
                    var packetToOldUsers = new PacketBuilder();
                    packetToOldUsers.WriteOpCode(1);
                    packetToOldUsers.WriteString(newUser.Username);
                    packetToOldUsers.WriteString(newUser.UID.ToString());
                    user.ClientSocket.Client.Send(packetToOldUsers.GetPacket());
                }

                var packetToNewUser = new PacketBuilder();
                packetToNewUser.WriteOpCode(1);
                packetToNewUser.WriteString(user.Username);
                packetToNewUser.WriteString(user.UID.ToString());
                newUser.ClientSocket.Client.Send(packetToNewUser.GetPacket());
            }
        }
    
    }
}
