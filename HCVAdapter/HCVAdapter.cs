using System;
using System.Collections.Generic;

namespace EMS
{
    public class HCVAdapter : IHCVAdapter
    {
        AsynchronousClient client = new AsynchronousClient();
        string[] result;

        public string GetRequest(string HCVIPAddress, string HCN, string vCode, string postalCode)
        {
            string finalString;
            finalString = BuildClientString("GET", HCN, vCode, postalCode);   // Create the client string
            finalString += "<EOF>";                                         // Add the end of file attachment
            client.StartClient(HCVIPAddress);
            string response = "Server offline";
            if (client.IsConnected())
            {
                client.SendMessage(finalString);
                response = client.ReceiveMessage();
            }

            return response;
        }

        /*-------------------------------------------------------------------------------------------------
                Currently waiting on patient model to contain a VCode and a PostalCode field
        -------------------------------------------------------------------------------------------------*/

        //public string GetRequest(string HCVIPAddress, Patient patient)
        //{
        //    string response = "ERROR";
        //    if (patient != null)
        //    {
        //        string finalString;
        //        finalString = BuildClientString("GET", patient.HCN, patient.vCode, patient.postalCode);
        //        finalString += "<EOF>";     // Add the end of file attachment
        //        client.StartClient(HCVIPAddress);
        //        string response = "Server offline";
        //        if (client.IsConnected())
        //        {
        //            client.SendMessage(finalString);
        //            response = client.ReceiveMessage();
        //        }
        //    }

        //    return response;
        //}

        //public string GetRequest(string HCVIPAddress, List<Patient> patients)
        //{
        //    string response = "ERROR";
        //    if (patients != null)
        //    {
        //        string finalString = "";
        //        foreach (Patient x in patients)
        //        {
        //            if (!finalString.Contains("GET"))
        //                finalString += BuildClientString("GET", x.HCN, x.vCode, x.postalCode);
        //            else
        //                finalString += BuildClientString("", x.HCN, x.vCode, x.postalCode);
        //        }
        //        finalString += "<EOF>";     // Add the end of file attachment
        //        client.StartClient(HCVIPAddress);
        //        string response = "Server offline";
        //        if (client.IsConnected())
        //        {
        //            client.SendMessage(finalString);
        //            response = client.ReceiveMessage();
        //        }
        //    }

        //    return response;
        //}

        public string SetRequest(string HCVIPAddress, string HCN, string vCode, string postalCode)
        {
            string finalString;
            finalString = BuildClientString("SET", HCN, vCode, postalCode);   // Create the client string
            finalString += "<EOF>";                                         // Add the end of file attachment
            client.StartClient(HCVIPAddress);
            string response = "Server offline";
            if (client.IsConnected())
            {
                client.SendMessage(finalString);
                response = client.ReceiveMessage();
            }

            return response;
        }

        private string BuildClientString(string request, string HCN, string vCode, string postalCode)
        {
            string finalString;
            if (request != "")
                finalString = request + " " + HCN + "," + vCode + "," + postalCode;
            else
                finalString = "/" + HCN + "," + vCode + "," + postalCode;

            return finalString;
        }
    }

}
