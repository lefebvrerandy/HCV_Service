using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionModuleServer;
using ConnectionModuleClient;
using HCV_Class_Library;
using System.Threading;

namespace MethodTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thServer = new Thread(new ThreadStart(ServerMethod));
            Thread thClient = new Thread(new ThreadStart(ClientMethod));
            Thread thClient2 = new Thread(new ThreadStart(ClientMethod2));
            thServer.Start();
            Thread.Sleep(4000);
            thClient.Start();
            //Thread.Sleep(100);
            thClient2.Start();
            Thread.Sleep(4000);


            thClient.Join();
            thClient2.Join();
            AsynchronousSocketListener.Disconnect();
            thServer.Join();
            Console.ReadKey();

        }

        private static void ServerMethod()
        {
            AsynchronousSocketListener.StartListening();
        }

        private static void ClientMethod()
        {
            AsynchronousClient client1 = new AsynchronousClient("client1");
            client1.StartClient("127.0.0.1");
            client1.SendMessage("Test One");
            string client1Response = client1.ReceiveMessage();
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("\nClient 1 received: {0}\n", client1Response);
            Console.WriteLine("--------------------------------------------\n");
        }
        private static void ClientMethod2()
        {
            AsynchronousClient client2 = new AsynchronousClient("client2");
            client2.StartClient("127.0.0.1");
            client2.SendMessage("Client Two");
            string client2Response = client2.ReceiveMessage();
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("\nClient 2 received: {0}\n", client2Response);
            Console.WriteLine("--------------------------------------------\n");
        }
    }
}
