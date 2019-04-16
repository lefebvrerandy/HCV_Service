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
        // 3. Scan the database for duplicate entrie(s)
        // 4. Insert into database new Health Card Number(s)
        // 5. Return result to client

        // Flow of control for REPLACING to the database
        // 1. Accept new connection
        // 2. Accept Health Card Number(s)
        // 3. Scan the database for entrie(s)
        // 4. Insert into database (WHERE FOUND) new Health Card Number(s)
        // 5. Return result to client

        // Flow of control for CHECKING the database
        // 1: Accept new connection
        // 2: Accept Health Card Number(s)
        // 3. Scan the database for Health Card Number(s)
        // 4. Return result for each Health Card Number 
        // 4.(a) Return values must be either: VALID, VCODE, PUNKO
        //      VALID - Success             - There’s a correct match on the three elements that HCV need to check.
        //      VCODE - Invalid Entry       - There was a mismatch between version codes AND/OR postal codes
        //      PUNKO - Patient Billable    - Should not pass requests for payment for PUNKO patients to the MoH
        //-----------------------------------------------------------------------------------
        // RUSS' EXPLANATION ON THE THREE
        //      VALID                       - Valid health card,
        //      VCODE                       - Incorrect two-character version code (thus will be declined by MoH during billing)
        //      PUNKO                       - Patient unknown error (also will not be paid by MoH).

        /*****************************/
        /*          Database         */
        /*****************************/
        // HCV Database should contain:
        //      Health card     - 10 Numeric digits
        //      VCode           - 2 Alphabetical Characters
        //      Postal Code     - 6 Character string
        // HCV Database should NEVER contain:
        //      Invalid health cards
        //      Out of date version codes
        //      Unknown patients


        /*****************************/
        /*   Notes from Russ on HCV  */
        /*****************************/
        // The HCV application cannot run on the same PC as the EMS-II solution.
        //
        // EMS-II must be adapted to send and receive these requests from the billing 
        //      support component of the GUI. It is anticipated that HCV will be run before
        //      any billing file would be created, thus if VCODE or PUNKO responses are 
        //      returned from HCV, they could be corrected and confirmed before generating 
        //      the monthly billing file.
        //
        // EMS-II patient demographic information must be changed to store the most recent
        //      response code from HCV as a component of the patient information table in the database.
        //
        // Any billing codes assigned for a patient who returns PUNKO from HCV, should be marked 
        //      through the EMS-II GUI as ‘Patient Billable’, and the monthly reporting summary 
        //      should reflect this information. No billing codes that return PUNKO should ever be 
        //      passed to the Ministry of Health through the monthly billing file.


        /*****************************/
        /*      Demographics         */
        /*****************************/
        // You must modify your EMS systems demographics to store the most recent HCV status 
        //      (e.g. VALID, VCODE, PUNKO)

        /*****************************/
        /*        Billing            */
        /*****************************/
        // Your EMS software would be able to check the HCV before you completed the monthly billing file 
        //      was submitted
        // It is anticipated that HCV will be run before any billing file would be created,
        //      thus if VCODE or PUNKO responses are returned from HCV, they could be corrected 
        //      and confirmed before generating the monthly billing file.
        // **No billing codes that return PUNKO should ever be passed to the Ministry of Health through
        //      the monthly billing file.**

        /*****************************/
        /*          GUI              */
        /*****************************/
        // Any billing codes assigned for a patient who returns PUNKO from HCV, should be marked through
        //      the EMS-II GUI as ‘Patient Billable’, and the monthly reporting summary should reflect
        //      this information.


    }
}
