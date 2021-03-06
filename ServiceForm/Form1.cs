﻿using System;
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
        Thread _serverThread;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            _serverThread = new Thread(new ThreadStart(ServerMethod));
            Logger.Log("Server Started");
            _serverThread.Start();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
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
