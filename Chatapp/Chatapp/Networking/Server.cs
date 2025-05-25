using ChatClient.Networking.IO;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatClient.Networking
{
    public class Server
    {
        TcpClient client;
        PacketReader? packetReader;

        public event Action connectedEvent;
        public event Action<string, string> userConnectedEvent; 

        public Server()
        {
            client = new TcpClient();
        }

        public void ConnectToServer(string Username)
        {
            if (!client.Connected)
            {
                client.Connect("127.0.0.1", 50922);
                packetReader = new PacketReader(client.GetStream());

                if (!string.IsNullOrEmpty(Username))
                {
                    connectedEvent?.Invoke();

                    var connectPacket = new PacketBuilder();
                    connectPacket.WriteOpCode(0);
                    connectPacket.WriteString(Username);
                    client.Client.Send(connectPacket.GetPacket());

                    Console.WriteLine("Connected to server");
                }

                ReadPacket();
            }
            else
            {
                Console.WriteLine("Already connected");
            }
        }

        private void ReadPacket()
        {
            Task.Run(() =>
            {
                while (client.Connected)
                {
                    try
                    {
                        var opcode = packetReader?.ReadByte();

                        switch (opcode)
                        {
                            case 1: // usuário conectado
                                var username = packetReader.ReadMessage();
                                var uid = packetReader.ReadMessage();
                                userConnectedEvent?.Invoke(username, uid);
                                break;
                            case 2:
                                Console.WriteLine("Received message");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro no ReadPacket: " + ex.Message);
                        client.Close();
                    }
                }
            });
        }
    }
}
