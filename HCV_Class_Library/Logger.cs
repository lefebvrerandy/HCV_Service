using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HCV_Class_Library
{
    public static class Logger
    {
        public static void Log(string message)
        {
            try
            {
                EventLog serviceEventLog = new EventLog();
                if (!EventLog.Exists("HCVLog"))
                {
                    EventLog.CreateEventSource("HCVSource", "HCVLog");
                }
                serviceEventLog.Source = "HCVSource";
                serviceEventLog.Log = "HCVLog";
                serviceEventLog.WriteEntry(message);
            }
            catch(Exception e)
            {
                Debug.Write(e);
            }
        }
    }
}
