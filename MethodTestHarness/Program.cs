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
            //-----------------------------------------------------------------
            //      Global Server/Client Test Harness Variables
            //-----------------------------------------------------------------
            Thread thServer = new Thread(new ThreadStart(ServerMethod));
            //Thread thClient = new Thread(new ThreadStart(ClientMethod));
            Thread thClient2 = new Thread(new ThreadStart(ClientMethod2));
            thServer.Start();
            Thread.Sleep(2000);
            //thClient.Start();
            thClient2.Start();

            // Client Threads
            int totalThreadsToRun = 100;
            int min = 0;
            int max = 0;
            Random random1 = new Random();

            //-----------------------------------------------------------------
            //      Testing for 2 clients with random connection times
            //-----------------------------------------------------------------
            //Thread[] clients = new Thread[totalThreadsToRun];
            //for (int i = 0; i <= totalThreadsToRun - 1; i++)
            //{
            //    clients[i] = new Thread(() => ClientMethod());
            //    clients[i + 1] = new Thread(() => ClientMethod2());
            //    i++;
            //}
            //for (int i = 0; i <= totalThreadsToRun - 1; i++)
            //{
            // Start clients 1 and 2
            //clients[i].Start();
            //Thread.Sleep(random1.Next(min, max));
            //clients[i + 1].Start();
            //Thread.Sleep(random1.Next(min, max));

            // join clients 1 and 2
            //clients[i].Join();
            //Thread.Sleep(random1.Next(min, max));
            //clients[i + 1].Join();
            //Thread.Sleep(random1.Next(min, max));
            //    i++;
            //}

            //-----------------------------------------------------------------
            //      Testing alot of clients all at once
            //-----------------------------------------------------------------
            //Thread[] clientAlot = new Thread[totalThreadsToRun];
            //for (int i = 0; i <= totalThreadsToRun - 1; i++)
            //{
            //    clientAlot[i] = new Thread(() => ClientMethod());
            //}
            //for (int i = 0; i <= totalThreadsToRun - 1; i++)
            //{
            //    // Start clients 1 and 2
            //    clientAlot[i].Start();
            //    Thread.Sleep(random1.Next(min, max));
            //}
            //for (int i = 0; i <= totalThreadsToRun - 1; i++)
            //{
            //    // Start clients 1 and 2
            //    clientAlot[i].Join();
            //    Thread.Sleep(random1.Next(min, max));
            //}

            //Console.WriteLine("\n\n\n-------------------------");
            //Console.WriteLine("ERROR COUNT: 1: " + numberOfErrors1.ToString() + "    2: " + numberOfErrors2.ToString());
            //Console.WriteLine("-------------------------\n\n\n");
            //AsynchronousSocketListener.Disconnect();
            thServer.Join();
            //thClient.Join();
            thClient2.Join();
            Console.ReadKey();

            //-----------------------------------------------------------------
            //      Testing Entity DAL
            //-----------------------------------------------------------------
            //Thread dalThread = new Thread(new ThreadStart(GetHCVPatientFromDAL));         // Works
            //Thread dalThread = new Thread(new ThreadStart(AddHCVPatientToDAL));           // Works UpdateHCVPatient
            //Thread dalThread = new Thread(new ThreadStart(UpdateHCVPatientInDAL));        // Works
            //dalThread.Start();
            //dalThread.Join();
            //Console.ReadKey();

        }
        // Works
        private static void UpdateHCVPatientInDAL()
        {
            bool retCode = false;
            HCVDAL HCVDal = new HCVDAL();
            HCV HCVObject = new HCV(HCVDal);
            HCVPatient newHCVPatient = new HCVPatient();
            newHCVPatient = HCVObject.CreateHCVPatient("1231321321", "AZ", "N1N1N1");
            retCode = HCVObject.UpdateHCVPatient("9999999999", newHCVPatient);
            Console.WriteLine(retCode);
        }

        // Works
        private static void AddHCVPatientToDAL()
        {
            bool retCode = false;
            HCVDAL HCVDal = new HCVDAL();
            HCV HCVObject = new HCV(HCVDal);
            HCVPatient newHCVPatient = new HCVPatient();
            newHCVPatient = HCVObject.CreateHCVPatient("1231231231","AZ","N2H4J1");
            retCode = HCVObject.AddHCVPatient(newHCVPatient);
            Console.WriteLine(retCode);
        }
        
        // Works
        private static void GetHCVPatientFromDAL()
        {
            HCVDAL HCVDal = new HCVDAL();
            HCV HCVObject = new HCV(HCVDal);
            List<HCVPatient> HCVpatient = new List<HCVPatient>();
            string[] sendingString = { "1234567981" };
            HCVpatient = HCVObject.GetHCVPatient(sendingString);
            foreach (HCVPatient x in HCVpatient)
            {
                Console.WriteLine("HCN: " + x.HealthCardNumber);
                Console.WriteLine("VCode: " + x.VCode);
                Console.WriteLine("Postal: " + x.PostalCode);
                Console.WriteLine("");
            }
        }

        // Works
        private static void ServerMethod()
        {
            AsynchronousSocketListener.StartListening();
        }

        // Works
        private static void ClientMethod()
        {
            AsynchronousClient client1 = new AsynchronousClient("client1");
            client1.StartClient("127.0.0.1");
            client1.SendMessage("GET 1134567983,BA,N2H4J2/1234567981,AB,N2H4J4/1234567982,AC,N2H3J5");
            string client1Response = client1.ReceiveMessage();
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("\nClient 1 received: {0}\n", client1Response);
            Console.WriteLine("--------------------------------------------\n");
            if (client1Response != "VALID")
            {
                numberOfErrors1++;
            }
        }

        // Works
        private static void ClientMethod2()
        {
            AsynchronousClient client2 = new AsynchronousClient("client2");
            client2.StartClient("127.0.0.1");
            client2.SendMessage("SET 1231321321,ZZ,A1N1N1");
            string client2Response = client2.ReceiveMessage();
            Console.WriteLine("\n--------------------------------------------");
            Console.WriteLine("\nClient 2 received: {0}\n", client2Response);
            Console.WriteLine("--------------------------------------------\n");
            if (client2Response != "VCODE")
            {
                numberOfErrors2++;
            }
        }
    }
}
