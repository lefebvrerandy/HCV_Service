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
    class ConnectionModule
    {
        // Class constants
        const string kIPAddress = "127.0.0.1";
        const int kPort = 13000;

        TcpListener server = null;
        TcpClient client = null;
        NetworkStream stream = null;
        String data = null;

        ConnectionModule(string IpAddress = kIPAddress, int Port = kPort)
        {
            try
            {
                Int32 port = kPort;
                IPAddress localAddr = IPAddress.Parse(kIPAddress);

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();


                //Waiting for connection
                Logger.Log("Waiting for connecion.");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                client = server.AcceptTcpClient();
                Logger.Log("Connected");
            }
            catch (SocketException e)
            {
                Logger.Log("SocketException: " + e);
            }
        }


        String Read()
        {
            data = null;
            // Buffer for reading data
            Byte[] bytes = new Byte[256];

            // Get a stream object for reading and writing
            stream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                // Process the data sent by the client.
                data = data.ToUpper();
            }
            return data;
        }

        void Write(String message)
        {
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);

            // Send back a response.
            stream.Write(msg, 0, msg.Length);
        }

        void Close()
        {
            // Shutdown and end connection
            client.Close();
            // Stop listening for new clients.
            server.Stop();
        }
    }
    // Client
    //static void Connect(String server, String message)
    //{
    //    try
    //    {
    //        // Create a TcpClient.
    //        // Note, for this client to work you need to have a TcpServer 
    //        // connected to the same address as specified by the server, port
    //        // combination.
    //        Int32 port = 13000;
    //        TcpClient client = new TcpClient(server, port);

    //        // Translate the passed message into ASCII and store it as a Byte array.
    //        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

    //        // Get a client stream for reading and writing.
    //        //  Stream stream = client.GetStream();

    //        NetworkStream stream = client.GetStream();

    //        // Send the message to the connected TcpServer. 
    //        stream.Write(data, 0, data.Length);

    //        Console.WriteLine("Sent: {0}", message);

    //        // Receive the TcpServer.response.

    //        // Buffer to store the response bytes.
    //        data = new Byte[256];

    //        // String to store the response ASCII representation.
    //        String responseData = String.Empty;

    //        // Read the first batch of the TcpServer response bytes.
    //        Int32 bytes = stream.Read(data, 0, data.Length);
    //        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
    //        Console.WriteLine("Received: {0}", responseData);

    //        // Close everything.
    //        stream.Close();
    //        client.Close();
    //    }
    //    catch (ArgumentNullException e)
    //    {
    //        Console.WriteLine("ArgumentNullException: {0}", e);
    //    }
    //    catch (SocketException e)
    //    {
    //        Console.WriteLine("SocketException: {0}", e);
    //    }

    //    Console.WriteLine("\n Press Enter to continue...");
    //    Console.Read();
    //}
}
