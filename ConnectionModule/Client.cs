using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HCV_Class_Library;

namespace ConnectionModule
{
    public class Client
    {
        // Class Constants
        const string kIPAddress = "127.0.0.1";
        const int kPort = 13000;

        private String responseData;
        private NetworkStream stream;
        private TcpClient client;
        private Byte[] data;
        private String _hostname;
        private int _port;

        public Client(String hostname = kIPAddress, int port = kPort)
        {
            responseData = String.Empty;
            stream = null;
            client = null;
            data = null;
            _hostname = hostname;
            _port = port;
        }

        public void Connect()
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                client = new TcpClient(_hostname, _port);

            }
            catch (ArgumentNullException e)
            {
                Logger.Log("CLIENT: ArgumentNullException: " + e);
            }
            catch (SocketException e)
            {
                Logger.Log("CLIENT: SocketException: " + e);
            }
        }

        public String Read()
        {
            // Buffer to store the response bytes.
            data = new Byte[256];

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            return responseData;
        }

        public void Write(String message)
        {
            stream = client.GetStream();
            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Send the message to the connected TcpServer. 
            if (stream != null)
                stream.Write(data, 0, data.Length);
        }

        public void Close()
        {
            // Close everything.
            stream.Close();
            client.Close();
        }
    }
}
