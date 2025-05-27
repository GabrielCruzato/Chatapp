using System.Net.Sockets;
using System.Text;

namespace ChatServer.Network.IO
{
    public class PacketReader : BinaryReader
    {
        private NetworkStream _stream;

        public PacketReader(NetworkStream stream) : base(stream)
        {
            _stream = stream;
        }

        private void ReadExactly(byte[] buffer, int offset, int count)
        {
            int readBytes = 0;
            while (readBytes < count)
            {
                int currentRead = _stream.Read(buffer, offset + readBytes, count - readBytes);
                if (currentRead == 0)
                    throw new IOException("Connection closed while reading.");
                readBytes += currentRead;
            }
        }

        public string ReadMessage()
        {
            var lengthBuffer = new byte[sizeof(int)];
            ReadExactly(lengthBuffer, 0, lengthBuffer.Length);
            var length = BitConverter.ToInt32(lengthBuffer, 0);

            var messageBuffer = new byte[length];
            ReadExactly(messageBuffer, 0, length);
            return Encoding.UTF8.GetString(messageBuffer);
        }
    }
}
