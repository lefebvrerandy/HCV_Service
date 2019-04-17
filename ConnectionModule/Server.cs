using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using HCV_Class_Library;

namespace ConnectionModule
{
    public class Server
    {
        // Class constants
        const string kIPAddress = "127.0.0.1";
        const int kPort = 13000;

        // Private variables
        private TcpListener server = null;
        private TcpClient client = null;
        private NetworkStream clientStream = null;

        // Public variables
        public String RecievedMessage = null;

        public Server(string IpAddress = kIPAddress, int port = kPort)
        {
            IPAddress localAddr = IPAddress.Parse(kIPAddress);

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();
        }

        public void Connect()
        {
            try
            {
                //Waiting for connection
                Logger.Log("SERVER: Waiting for connecion.");

                // Perform a blocking call to accept requests.
                client = server.AcceptTcpClient();
                Logger.Log("SERVER: Connected to Client " + client);

            }
            catch (SocketException e)
            {
                Logger.Log("SERVER: SocketException: " + e);
            }
        }


        public String Read()
        {
            RecievedMessage = null;
            // Buffer for reading data
            Byte[] bytes = new Byte[256];

            // Get a stream object for reading and writing
            clientStream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = clientStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                RecievedMessage = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                // Process the data sent by the client.
                //RecievedMessage = RecievedMessage.ToUpper();
            }
            return RecievedMessage;
        }

        public void Write(String message)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);

            // Send back a response.
            clientStream.Write(msg, 0, msg.Length);
        }

        public void Close()
        {
            // Shutdown and end connection
            client.Close();
            // Stop listening for new clients.
            server.Stop();
        }
    }
}
