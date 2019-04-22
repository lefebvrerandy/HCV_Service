using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HCV_Class_Library;

namespace HCVService
{
    public partial class Service1 : ServiceBase
    {
        Thread _serverThread;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _serverThread = new Thread(new ThreadStart(ServerMethod));
            Logger.Log("Server Started");
            _serverThread.Start();
        }

        protected override void OnStop()
        {
            Logger.Log("Server Stopping");
            AsynchronousSocketListener.Disconnect();
            _serverThread.Join();
            _serverThread = null;
            Logger.Log("Server Stopped");
        }

        private static void ServerMethod()
        {
            AsynchronousSocketListener server = new AsynchronousSocketListener();
            server.StartListening();
        }
    }
}
