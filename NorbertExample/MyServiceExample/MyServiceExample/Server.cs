using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyServiceExample
{
    public class Server
    {
        public bool Run { set; get; } // used to control server running

        public Server()
        {
            Run = true;
        }
        public void RunServer()
        {
            // The server will continue to run until the Run flag will be set to false
            while (Run)
            {
                // Main Server code loop. Add server code here.
                Thread.Sleep(1); // Should have this at the end of the loop.
            }
        }

        public void StopServer()
        {
            Run = false;
        }
    }
}
