//using HCV_Class_Library;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//namespace ConnectionModuleServer
//{
//    // State object for reading client data asynchronously  
//    public class StateObject
//    {
//        // Client  socket.  
//        public Socket workSocket = null;
//        // Size of receive buffer.  
//        public const int BufferSize = 1024;
//        // Receive buffer.  
//        public byte[] buffer = new byte[BufferSize];
//        // Received data string.  
//        public StringBuilder sb = new StringBuilder();
//    }

//    public class AsynchronousSocketListener
//    {
//        // Thread signal.  
//        public static ManualResetEvent allDone = new ManualResetEvent(false);
//        public static bool closeServer = false;
//        private static Socket listener;

//        public AsynchronousSocketListener()
//        {
//        }

//        public void StartListening()
//        {
//            // Establish the local endpoint for the socket.  
//            // The DNS name of the computer  
//            // running the listener is "host.contoso.com".  
//            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
//            IPAddress ipAddress = ipHostInfo.AddressList[0];
//            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

//            // Create a TCP/IP socket.  
//            listener = new Socket(ipAddress.AddressFamily,
//                SocketType.Stream, ProtocolType.Tcp);

//            // Bind the socket to the local endpoint and listen for incoming connections.  
//            try
//            {
//                listener.Bind(localEndPoint);
//                listener.Listen(100);

//                while ((true) && (!closeServer))
//                {
//                    // Set the event to nonsignaled state.  
//                    allDone.Reset();

//                    // Start an asynchronous socket to listen for connections.  
//                    //Console.WriteLine("Waiting for a connection...");
//                    listener.BeginAccept(
//                        new AsyncCallback(AcceptCallback),
//                        listener);

//                    // Wait until a connection is made before continuing.  
//                    allDone.WaitOne();
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }

//        public static void AcceptCallback(IAsyncResult ar)
//        {
//            // Signal the main thread to continue.  
//            allDone.Set();

//            // Get the socket that handles the client request.  
//            Socket listener = (Socket)ar.AsyncState;
//            Socket handler = listener.EndAccept(ar);

//            // Create the state object.  
//            StateObject state = new StateObject();
//            state.workSocket = handler;
//            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                new AsyncCallback(ReadCallback), state);
//        }

//        public static void ReadCallback(IAsyncResult ar)
//        {
//            String content = String.Empty;

//            // Retrieve the state object and the handler socket  
//            // from the asynchronous state object.  
//            StateObject state = (StateObject)ar.AsyncState;
//            Socket handler = state.workSocket;

//            // Read data from the client socket.   
//            int bytesRead = handler.EndReceive(ar);

//            if (bytesRead > 0)
//            {
//                // There  might be more data, so store the data received so far.  
//                state.sb.Append(Encoding.ASCII.GetString(
//                    state.buffer, 0, bytesRead));

//                // Check for end-of-file tag. If it is not there, read   
//                // more data.  
//                content = state.sb.ToString();
//                if (content.IndexOf("<EOF>") > -1)
//                {
//                    // Must be in one of three options
//                    // RUSS' EXPLANATION ON THE THREE
//                    //      VALID                       - Valid health card,
//                    //      VCODE                       - Incorrect two-character version code (thus will be declined by MoH during billing)
//                    //      PUNKO                       - Patient unknown error (also will not be paid by MoH).

//                    string[] newString = new string[100];
//                    string returningString = "PUNKO";

//                    // Check what the call is and what the client is trying to do
//                    if (content.IndexOf("GET") > -1)    // Get a single entry
//                    {
//                        HCV_Class_Library.HCVStringManipulation adapter = new HCV_Class_Library.HCVStringManipulation();
//                        newString[0] = "PUNKO";
//                        // Send the recieved data into the Get method and return the results into a string array
//                        newString = Get(content);
//                        returningString = adapter.ConcatReturn(newString);

//                        // Send the newly created string with the list of results
//                        Send(handler, returningString);
//                    }
//                    else if (content.IndexOf("SET") > -1)   // Update a single entry
//                    {
//                        returningString = Set(content);

//                        Send(handler, returningString);
//                    }
//                    else
//                    {
//                        returningString = "ERROR";
//                        Send(handler, returningString);
//                    }

//                }
//                else
//                {
//                    // Not all data received. Get more.  
//                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
//                    new AsyncCallback(ReadCallback), state);
//                }
//            }
//        }

//        private static void Send(Socket handler, String data)
//        {
//            Thread.Sleep(100);
//            // Convert the string data to byte data using ASCII encoding.  
//            byte[] byteData = Encoding.ASCII.GetBytes(data);

//            // Begin sending the data to the remote device.  
//            handler.BeginSend(byteData, 0, byteData.Length, 0,
//                new AsyncCallback(SendCallback), handler);
//        }

//        private static void SendCallback(IAsyncResult ar)
//        {
//            try
//            {
//                // Retrieve the socket from the state object.  
//                Socket handler = (Socket)ar.AsyncState;

//                // Complete sending the data to the remote device.  
//                int bytesSent = handler.EndSend(ar);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }

//        public static void Disconnect()
//        {
//            closeServer = true;
//            allDone.Set();
//        }

//        private static string[] Get(string content)
//        {
//            HCV_Class_Library.HCVDAL dal = new HCV_Class_Library.HCVDAL();
//            HCV_Class_Library.HCV hcv = new HCV_Class_Library.HCV(dal);
//            List<HCV_Class_Library.HCVPatient> patient = new List<HCV_Class_Library.HCVPatient>();
//            HCV_Class_Library.HCVStringManipulation adapter = new HCV_Class_Library.HCVStringManipulation();


//            // Set up the main string by removing the beginning and ending flags
//            // split the main string into a group of patient strings
//            content = adapter.TearOffBeginningAndEnd(content);
//            string[] patientList = content.Split('/');

//            // Create a string of only health card numbers we are looking for
//            // AKA. Trim off vcode and postal code for each string
//            string[] HealthCardOnly = new string[patientList.Length];
//            for (int i = 0; i < patientList.Length; i++)
//            {
//                HealthCardOnly[i] = adapter.TearOffVCodeAndPostal(patientList[i]); 
//            }

//            // Get the list of patients 
//            // Recombine the patient strings for comparing
//            patient = hcv.GetHCVPatient(HealthCardOnly);         
//            string[] FoundPatient = adapter.ConstructCompareString(patient);
//            string[] returningString = new string[patientList.Length];

//            // Compare each string to what we recieved to what we found and create a list
//            // of results
//            for (int i = 0; i < patientList.Length; i++)
//            {
//                if (FoundPatient[i] == patientList[i])
//                {
//                    returningString[i] = "VALID";
//                }
//                else if (FoundPatient[i].IndexOf(HealthCardOnly[i]) > -1)
//                {
//                    returningString[i] = "VCODE";
//                }
//                else
//                {
//                    returningString[i] = "PUNKO";
//                }
//            }
//            return returningString;
//        }

//        private static string Set(string content)
//        {
//            string returningString = "ERROR";
//            HCV_Class_Library.HCVDAL dal = new HCV_Class_Library.HCVDAL();
//            HCV_Class_Library.HCVPatient patient = new HCV_Class_Library.HCVPatient();
//            HCV_Class_Library.HCV hcv = new HCV_Class_Library.HCV(dal);
//            HCV_Class_Library.HCVStringManipulation adapter = new HCV_Class_Library.HCVStringManipulation();

//            // Trim the beginning and ending label
//            // Split the contents into 3 sections: HCN, VCode, PostalCode
//            content = adapter.TearOffBeginningAndEnd(content);
//            string[] patientInfo = content.Split(',');


//            // Find the HCVPatient
//            patient = hcv.GetHCVPatient(patientInfo[0]);
//            if (patient != null)
//            {
//                // Create a new HCVPatient
//                // Update the HCVDatabase with the new patient information
//                patient = hcv.CreateHCVPatient(patientInfo[0], patientInfo[1], patientInfo[2]);
//                if (hcv.UpdateHCVPatient(patientInfo[0], patient))
//                {
//                    returningString = "VALID";
//                }
//            }
//            return returningString;
//        }
//    }
//}