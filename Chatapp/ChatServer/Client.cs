using ChatServer.Network.IO;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Client
    {
        public string? Username { get; set; }
        public Guid UID { get; set; } = Guid.NewGuid();
        public TcpClient ClientSocket { get; set; }

        private readonly PacketReader packetReader;

        public Client(TcpClient client)
        {
            ClientSocket = client;
            packetReader = new PacketReader(ClientSocket.GetStream());
        }

        public async Task ListenAsync(Action onUserConnected)
        {
            try
            {
                var opcode = packetReader.ReadByte();
                if (opcode == 0)
                {
                    Username = packetReader.ReadMessage();
                    Console.WriteLine($"[{DateTime.Now}]: {Username} connected.");
                    onUserConnected.Invoke();

                    await Task.Run(Process);
                }
                else
                {
                    Console.WriteLine($"Expected opcode 0, but got {opcode}");
                    ClientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Listen error: {ex.Message}");
                ClientSocket.Close();
            }
        }

        public void Process()
        {
            while (true)
            {
                try
                {
                    var opcode = packetReader.ReadByte();
                    switch (opcode)
                    {
                        case 5:
                            var msg = packetReader.ReadMessage();
                            Console.WriteLine($"[{DateTime.Now}]: [{Username}]: {msg}");
                            Program.BroadcastMessage(msg, UID);
                            break;
                        default:
                            Console.WriteLine($"Unknown opcode: {opcode}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Process error: {ex.Message}");
                    Program.BroadcastDisconnectMessage($"User {Username} disconnected.", UID);
                    ClientSocket.Close();
                    break;
                }
            }
        }
    }
}