//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;
//using HCV_Class_Library;

//namespace ConnectionModule
//{
//    static public class ServerManager
//    {
//        static TcpClient[] _clientList = new TcpClient[1000];
//        static Server _server;


//        static public void LookForClients()
//        {
//            _server = new Server();
//            for (int i = 0; i < 1000; i++)
//            {
//                _server.Connect(_clientList[i]);
//                if (i >= 999)
//                {
//                    CleanupList();
//                }
//            }
//        }

//        static private void CleanupList()
//        {
//            Logger.Log("Cleaning up Client List.");
//        }

//        static public void ReadFromClients()
//        {
//            foreach (TcpClient client in _clientList)
//            {
//                if ((_server.RecievedMessage != "") && (_server.RecievedMessage != null))
//                {
//                    Console.WriteLine("Server Read: {0}", _server.RecievedMessage);
//                }
//            }
//        }
//    }
//}
