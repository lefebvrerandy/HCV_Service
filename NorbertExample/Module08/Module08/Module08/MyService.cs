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

namespace Module08
{
    public partial class MyService : ServiceBase
    {
        Thread t;
        FileWatcher fw;
        public MyService()
        {
            InitializeComponent();
            CanPauseAndContinue = true;
            fw = new FileWatcher();
            t = new Thread(new ThreadStart(fw.Run));
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("Service Started");
            t.Start();
        }

        protected override void OnStop()
        {
            Logger.Log("Service Stopped");
            fw.Done = true;
            t.Join();
            Logger.Log("Stopped");
        }

        protected override void OnContinue()
        {
            Logger.Log("Service Continued");
            t.Resume();
        }

        protected override void OnPause()
        {
            Logger.Log("Service Paused");
            t.Suspend();
        }
    }
}
