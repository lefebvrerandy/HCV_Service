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

namespace MyServiceExample
{
    public partial class MyService : ServiceBase
    {
        // The thread and server instance are class level, so multiple methods 
        // (namely OnStart and OnStop) can access them

        Thread tServer = null;
        Server instanceServer = null;
        public MyService()
        {
            InitializeComponent();
            // Set up the instances in the constructor to reduce
            instanceServer = new Server();
            ThreadStart ts = new ThreadStart(instanceServer.RunServer);
            tServer = new Thread(ts);
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("Server Started");
            tServer.Start();
        }

        protected override void OnStop()
        {
            Logger.Log("Server Stopping");
            instanceServer.Run = false;
            // Wait for the server thread to finish
            tServer.Join();
            Logger.Log("Server Stopped");
        }
    }
}
