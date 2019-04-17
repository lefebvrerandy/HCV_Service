using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionModule;
using HCV_Class_Library;
using System.Threading;

namespace MethodTestHarness
{
    class Program
    {
        static String sendingMessage = "Hello From Client";
        static String recievedMessage = String.Empty;

        static void Main(string[] args)
        {
            //Thread thread[2] = new Thread();
            Thread thServer = new Thread(new ThreadStart(ServerMethod));
            Thread thClient = new Thread(new ThreadStart(ClientMethod));
            thServer.Start();
            thClient.Start();

            thServer.Join();
            thClient.Join();
            Console.WriteLine("Client Sent: {0}", sendingMessage);
            Console.WriteLine("Server Read: {0}", recievedMessage);
            Console.ReadKey();

        }

        private static void ServerMethod()
        {
            Server server = new Server();   // Default params = local
            server.Connect();
            recievedMessage = server.Read();
            server.Close();
        }

        private static void ClientMethod()
        {
            Client client = new Client();   // Default params = local
            client.Connect();
            client.Write(sendingMessage);
            client.Close();
        }
    }
}
