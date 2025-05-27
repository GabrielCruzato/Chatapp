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

        public event Action? connectedEvent;
        public event Action<string, string>? userConnectedEvent;
        public event Action<string>? msgReceivedEvent;
        public event Action? userDisconnectedEvent;

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
                            case 1: 
                                var username = packetReader?.ReadMessage();
                                var uid = packetReader?.ReadMessage();
                                if (username != null && uid != null)
                                    userConnectedEvent?.Invoke(username, uid);
                                break;

                            case 5: 
                                var receivedMessage = packetReader?.ReadMessage();
                                if (receivedMessage != null)
                                    msgReceivedEvent?.Invoke(receivedMessage);
                                break;

                            case 10:
                                var disconnectMessage = packetReader?.ReadMessage();
                                userDisconnectedEvent?.Invoke();
                                Console.WriteLine($"User disconnected: {disconnectMessage}");
                                break;

                            default:
                                Console.WriteLine("Unknown opcode received: " + opcode);
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

        public void SendMessageToServer(string message)
        {
            if (client.Connected)
            {
                var packet = new PacketBuilder();
                packet.WriteOpCode(5);
                packet.WriteString(message);
                client.Client.Send(packet.GetPacket());
            }
            else
            {
                Console.WriteLine("Not connected to server");
            }
        }
    }
}
