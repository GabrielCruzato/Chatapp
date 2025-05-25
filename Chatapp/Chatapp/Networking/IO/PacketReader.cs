using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ChatClient.Networking.IO
{
    internal class PacketReader : BinaryReader
    {
        public PacketReader(Stream input) : base(input)
        {
            if (input is not NetworkStream)
            {
                throw new ArgumentException("Input stream is not a NetworkStream");
            }
        }

        public string ReadMessage()
        {
            int length = ReadInt32();
            byte[] bytes = ReadBytes(length);
            return Encoding.UTF8.GetString(bytes);
        }

        public Guid ReadGuid()
        {
            byte[] bytes = ReadBytes(16);
            return new Guid(bytes);
        }
    }
}