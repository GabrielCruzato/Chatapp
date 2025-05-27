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
        static readonly List<Client> users = new();

        static void Main()
        {
            TcpListener listener = new(IPAddress.Any, 50922);
            listener.Start();
            Console.WriteLine("Server started. Waiting for connections...");

            while (true)
            {
                TcpClient tcpClient = listener.AcceptTcpClient();
                Console.WriteLine("Client connecting...");
                var client = new Client(tcpClient);

                Task.Run(async () =>
                {
                    await client.ListenAsync(() => OnUserConnected(client));
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
                    var packet = new PacketBuilder();
                    packet.WriteOpCode(1);
                    packet.WriteString(newUser.Username!);
                    packet.WriteString(newUser.UID.ToString());
                    user.ClientSocket.Client.Send(packet.GetPacket());
                }

                var packetToNew = new PacketBuilder();
                packetToNew.WriteOpCode(1);
                packetToNew.WriteString(user.Username!);
                packetToNew.WriteString(user.UID.ToString());
                newUser.ClientSocket.Client.Send(packetToNew.GetPacket());
            }
        }

        public static void BroadcastMessage(string message, Guid senderUid)
        {
            foreach (var user in users)
            {
                if (user.UID != senderUid)
                {
                    var packet = new PacketBuilder();
                    packet.WriteOpCode(5);
                    packet.WriteString(message);
                    user.ClientSocket.Client.Send(packet.GetPacket());
                }
            }
        }

        public static void BroadcastDisconnectMessage(string message, Guid senderUid)
        {
            foreach (var user in users)
            {
                if (user.UID != senderUid)
                {
                    var packet = new PacketBuilder();
                    packet.WriteOpCode(2);
                    packet.WriteString(message);
                    user.ClientSocket.Client.Send(packet.GetPacket());
                }
            }
        }
    }
}
