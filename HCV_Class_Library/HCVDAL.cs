using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCV_Class_Library
{
    public class HCVDAL : IHCVDAL
    {
        // HCV Database should contain:
        //      Health card     - 10 Numeric digits
        //      VCode           - 2 Alphabetical Characters
        //      Postal Code     - 6 Character string

        int HCN;
        string VCode;
        string PostalCode;
    }
}
