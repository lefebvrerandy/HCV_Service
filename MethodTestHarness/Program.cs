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
        static int numberOfErrors1 = 0;
        static int numberOfErrors2 = 0;
        static void Main(string[] args)
        {
            Thread thServer = new Thread(new ThreadStart(ServerMethod));
            Thread thClient = new Thread(new ThreadStart(ClientMethod));
            Thread thClient2 = new Thread(new ThreadStart(ClientMethod2));
            thServer.Start();
            Thread.Sleep(2000);

            // Client Threads
            int totalThreadsToRun = 100;
            int min = 0;
            int max = 100;
            Random random1 = new Random();

            //-----------------------------------------------------------------
            //      Testing for 2 clients with random connection times
            //-----------------------------------------------------------------
            //Thread[] clients = new Thread[totalThreadsToRun];
            //for (int i = 0; i <= totalThreadsToRun-1; i++)
            //{
            //    clients[i] = new Thread(() => ClientMethod());
            //    clients[i+1] = new Thread(() => ClientMethod2());
            //    i++;
            //}
            //for (int i = 0; i <= totalThreadsToRun-1; i++)
            //{
            //    // Start clients 1 and 2
            //    clients[i].Start();
            //    Thread.Sleep(random1.Next(min, max));
            //    clients[i+1].Start();
            //    Thread.Sleep(random1.Next(min, max));

            //    // join clients 1 and 2
            //    clients[i].Join();
            //    //Thread.Sleep(random1.Next(min, max));
            //    clients[i+1].Join();
            //    //Thread.Sleep(random1.Next(min, max));
            //    i++;
            //}

            //-----------------------------------------------------------------
            //      Testing alot of clients all at once
            //-----------------------------------------------------------------
            // NOTES:
            //  Currently can not handle no sleep between spawning client. Must be around 1/10 a second between
            Thread[] clientAlot = new Thread[totalThreadsToRun];
            for (int i = 0; i <= totalThreadsToRun - 1; i++)
            {
                clientAlot[i] = new Thread(() => ClientMethod());
            }
            for (int i = 0; i <= totalThreadsToRun - 1; i++)
            {
                // Start clients 1 and 2
                clientAlot[i].Start();
                Thread.Sleep(random1.Next(min, max));
            }
            for (int i = 0; i <= totalThreadsToRun - 1; i++)
            {
                // Start clients 1 and 2
                clientAlot[i].Join();
                Thread.Sleep(random1.Next(min, max));
            }

            Console.WriteLine("\n\n\n-------------------------");
            Console.WriteLine("ERROR COUNT: 1: " + numberOfErrors1.ToString() + "    2: " + numberOfErrors2.ToString());
            Console.WriteLine("-------------------------\n\n\n");
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
            if (client1Response != "Test One<EOF>")
            {
                numberOfErrors1++;
            }
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
            if (client2Response != "Client Two<EOF>")
            {
                numberOfErrors2++;
            }
        }
    }
}
