using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestSocketIoTClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Socket IoT Client ...");

            var socketServer = "localhost";
            var socketServerPort = 12000;
            var deviceId = "d1";

            using var client = new TcpClient();
            client.Connect(socketServer, socketServerPort);
            using var networkStream = client.GetStream();
            using var reader = new StreamReader(networkStream, Encoding.UTF8);

            int i = 1;
            var clientTime = TimeSpan.FromSeconds(10);
            var start = DateTime.Now;
            while (true)
            {
                if (DateTime.Now.Subtract(start) <= clientTime)
                {
                    var message = $"tenant1~{DateTime.UtcNow}~{deviceId}~testdata{i++}\r\n";
                    var bytes = Encoding.UTF8.GetBytes(message);
                    networkStream.Write(bytes, 0, bytes.Length);
                    Task.Delay(1000).Wait();
                }
                else
                {
                    break;
                }
            }

            client.Close();

           
        }
    }
}
