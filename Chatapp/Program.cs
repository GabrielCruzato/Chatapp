using System;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    public class Program
    {
        static TcpListener? listener;

        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
                listener.Start();
                Console.WriteLine("Server started. Waiting for a connection...");

                var client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected!");

                // Handle client communication here (e.g., read/write data)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                listener?.Stop();
                Console.WriteLine("Server stopped.");
            }
        }
    }
}
