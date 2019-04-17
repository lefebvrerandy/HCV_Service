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
        static String sendingMessage = "Hello From Client";
        static String recievedMessage = String.Empty;

        static void Main(string[] args)
        {
            //Thread thread[2] = new Thread();
            Thread thServer = new Thread(new ThreadStart(ServerMethod));
            Thread thClient = new Thread(new ThreadStart(ClientMethod));
            Thread thClient2 = new Thread(new ThreadStart(ClientMethod2));
            thServer.Start();
            thClient.Start();
            Thread.Sleep(100);
            thClient2.Start();
            Thread.Sleep(4000);
            AsynchronousSocketListener.closeServer = true;

            thClient.Join();
            thClient2.Join();
            thServer.Join();
            Console.ReadKey();

        }

        private static void ServerMethod()
        {
            AsynchronousSocketListener.StartListening();
        }

        private static void ClientMethod()
        {
            AsynchronousClient.StartClient("Client One");
        }
        private static void ClientMethod2()
        {
            AsynchronousClient.StartClient("Client Two");
        }
    }
}
