using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Networking.IO
{
    internal class PacketBuilder
    {
        MemoryStream ms;

        public PacketBuilder()
        {
            ms = new MemoryStream();
        }

        public void WriteOpCode(byte opCode)
        {
            ms.WriteByte(opCode);
        }

        public void WriteString(string str)
        {
            var msgLength = Encoding.UTF8.GetByteCount(str);
            ms.Write(BitConverter.GetBytes(msgLength), 0, sizeof(int));
            ms.Write(Encoding.UTF8.GetBytes(str), 0, msgLength);
        }

        public byte[] GetPacket()
        {
            var packet = ms.ToArray();
            ms.Dispose();
            ms = new MemoryStream(); 
            return packet;
        }
    }
}
