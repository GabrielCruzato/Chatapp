using System.IO;
using System.Text;

namespace ChatClient.Networking.IO 
{
    public class PacketBuilder
    {
        MemoryStream ms;
        public PacketBuilder()
        {
            ms = new MemoryStream();
        }

        public void WriteOpCode(byte opcode)
        {
            ms.WriteByte(opcode);
        }

        public void WriteString(string msg)
        {
            var msgLength = Encoding.UTF8.GetByteCount(msg);
            ms.Write(BitConverter.GetBytes(msgLength), 0, sizeof(int));
            ms.Write(Encoding.UTF8.GetBytes(msg), 0, msgLength);
        }

        public byte[] GetPacket()
        {
            return ms.ToArray();
        }
    }
}
