using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MyServiceExample
{
    public static class Logger
    {
        public static void Log(string message)
        {
            EventLog serviceEventLog = new EventLog();
            if (!EventLog.Exists("MyEventLog"))
            {
                EventLog.CreateEventSource("MyEventSource", "MyEventLog");
            }
            serviceEventLog.Source = "MyEventSource";
            serviceEventLog.Log = "MyEventLog";
            serviceEventLog.WriteEntry(message);
        }

    }
}
