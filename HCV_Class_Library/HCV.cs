using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCV_Class_Library
{
    class HCV
    {
        /*****************************/
        /*           Class           */
        /*****************************/
        // Flow of control for ADDING to the database
        // 1. Accept new connection
        // 2. Accept Health Card Number(s)
        // 3. Scan the database for duplicate entries
        // 4. 

        // Flow of control for CHECKING the database
        // 1: Accept new connection
        // 2: Accept Health Card Number(s)
        // 3. Scan the database for Health Card Number(s)
        // 4. Return result for each Health Card Number 
        // 4.(a) Return values must be either: VALID, VCODE, PUNKO
        //      VALID - Success             - There’s a correct match on the three elements that HCV need to check.
        //      VCODE - Invalid Entry       - There was a mismatch between version codes AND/OR postal codes
        //      PUNKO - Patient Billable    - Should not pass requests for payment for PUNKO patients to the MoH

        /*****************************/
        /*          Database         */
        /*****************************/
        // HCV Database should contain:
        //      Health card
        //      VCode
        //      Postal Code
        // HCV Database should NEVER contain:
        //      Invalid health cards
        //      Out of date version codes
        //      Unknown patients
    }
}
