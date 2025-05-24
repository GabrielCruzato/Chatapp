using System.Net.Sockets;
using System.Text;

namespace ChatServer.Network.IO
{
    internal class PacketReader : BinaryReader
    {
        private NetworkStream? ns;
        public PacketReader(Stream input) : base(input)
        {
            ns = input as NetworkStream;

            if (ns is null)
            {
                throw new ArgumentException("Input stream is not a NetworkStream");
            }
        }

        public new string ReadString()
        {
            int length = ReadInt32();
            byte[] bytes = ReadBytes(length);
            if (ns is null)
            {
                throw new InvalidOperationException("NetworkStream is null");
            }
            ns.Read(bytes, 0, length);

            var message = Encoding.UTF8.GetString(bytes);
            return message;
        }

        public Guid ReadGuid()
        {
            byte[] bytes = ReadBytes(16);
            return new Guid(bytes);
        }
    }
}
