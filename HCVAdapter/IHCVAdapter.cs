using System;
using System.Collections.Generic;
using System.Text;

namespace EMS
{
    public interface IHCVAdapter
    {
        string GetRequest(string HCVIPAddress, string HCN, string vCode, string postalCode);
        /*-------------------------------------------------------------------------------------------------
                    Currently waiting on patient model to contain a VCode and a PostalCode field
        -------------------------------------------------------------------------------------------------*/
        //string GetRequest(string HCVIPAddress, Patient patient);
        //string GetRequest(string HCVIPAddress, List<Patient> patients);
        string SetRequest(string HCVIPAddress, string HCN, string vCode, string postalCode);

    }
}
