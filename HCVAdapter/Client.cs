using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace EMS
{
    // State object for receiving data from remote device.  
    public class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();

    }

    public class AsynchronousClient
    {
        // The port number for the remote device.  
        private const int port = 11000;

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        Socket client;

        public static bool connected;

        // The response from the remote device.  
        private static String response = String.Empty;

        private string _hostName;

        public AsynchronousClient(string hostName = "")
        {
            _hostName = hostName;
        }

        public void StartClient(string IPAddress)
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // The name of the   
                // remote device is "host.contoso.com".  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(IPAddress);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.  
                client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendMessage(string message)
        {
            Thread.Sleep(200);
            // Send test data to the remote device.  
            string SendingMessage = message + "<EOF>";
            Send(client, SendingMessage);
            sendDone.WaitOne();
            sendDone.Reset();
            Thread.Sleep(200);
        }

        public string ReceiveMessage()
        {
            Thread.Sleep(200);
            // Receive the response from the remote device.
            StateObject so = new StateObject();
            so = Receive(client);
            receiveDone.WaitOne();
            receiveDone.Reset();
            Thread.Sleep(200);
            return so.sb.ToString();
        }

        public void Disconnect()
        {
            // Release the socket.  
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Server is currently offline");
                connected = false;
            }

            // Signal that the connection has been made.  
            connectDone.Set();
        }

        private static StateObject Receive(Socket client)
        {
            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = client;
            try
            {
                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return state;
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    receiveDone.Set();
                }
                else
                {
                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal bool IsConnected()
        {
            return connected;
        }
    }
}