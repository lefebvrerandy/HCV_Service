using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HCV_Class_Library;

namespace ServiceForm
{
    public partial class Form1 : Form
    {
        Thread _thread;
        public Form1()
        {
            InitializeComponent();
            //CanPauseAndContinue = true;   // This will be used in the service
            //_thread = new Thread(new ThreadStart(METHOD_TO_RUN));
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Logger.Log("Server Started");
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            Logger.Log("Server Stopping");
            ////instanceServer.Run = false;
            // Wait for the server thread to finish
            //_thread.Join();
            Logger.Log("Server Stopped");
        }

        private void btn_Continue_Click(object sender, EventArgs e)
        {
            Logger.Log("Service Continued");
            //_thread.Resume();
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            Logger.Log("Service Paused");
            //_thread.Suspend();
        }
    }
}
