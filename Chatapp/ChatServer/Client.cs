using ChatServer.Network.IO;
using System;
using System.Net.Sockets;

namespace ChatServer
{
    public class Client
    {
        public string? Username { get; set; }
        public Guid UID { get; set; }
        public TcpClient ClientSocket { get; set; }

        private PacketReader packetReader;

        public Client(TcpClient client)
        {
            ClientSocket = client;
            UID = Guid.NewGuid();
            packetReader = new PacketReader(ClientSocket.GetStream());
        }

        public void Listen(Action onUserConnected)
        {
            try
            {
                var opcode = packetReader.ReadByte();
                if (opcode == 0)
                {
                    Username = packetReader.ReadMessage();
                    Console.WriteLine($"[{DateTime.Now}]: {Username} connected.");

                    onUserConnected.Invoke(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
        }
    }
}
    