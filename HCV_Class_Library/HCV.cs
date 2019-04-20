using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HCV_Class_Library
{
    public class HCV
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


        private readonly IHCVDAL _dal;

        public HCV(IHCVDAL dal)
        {
            _dal = dal;
        }

        public List<HCVPatient> GetHCVPatient(string[] HCN)
        {
            return _dal.GetHCVPatient(HCN);
        }
        public HCVPatient GetHCVPatient(string HCN)
        {
            return _dal.GetHCVPatient(HCN);
        }

        public bool AddHCVPatient(HCVPatient newHCVpatient)
        {
            return _dal.AddHCVPatient(newHCVpatient);
        }

        public bool UpdateHCVPatient(string HCN, HCVPatient newHCVpatient)
        {
            return _dal.UpdateHCVPatient(HCN, newHCVpatient);
        }

        public HCVPatient CreateHCVPatient(string newHCN, string newVCode, string newpostalCode)
        {
            bool retCode = true;
            // Validate the parameters
            // Validate the new healthcard number
            if (newHCN.Length == 10)
            {
                foreach (char c in newHCN)
                {
                    if (c < '0' || c > '9')
                    {
                        retCode = false;
                    }
                }
            }
            else
            {
                retCode = false;
            }

            // Validate the VCode
            if (newVCode.Length == 2)
            {
                bool FoundMatch = false;
                FoundMatch = Regex.IsMatch(newVCode, @"^[a-zA-Z]+$");
                if (!FoundMatch)
                    retCode = false;
            }
            else
            {
                retCode = false;
            }

            // Validate the Postal Code
            if ((newpostalCode.Length == 6) || (newpostalCode.Length == 7))
            {
                bool FoundMatch = false;
                try
                {
                    FoundMatch = Regex.IsMatch(newpostalCode, "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]");
                    if (!FoundMatch)
                        retCode = false;
                }
                catch (ArgumentException)
                {
                    retCode = false;
                }
            }
            else
            {
                retCode = false;
            }


            HCVPatient newHCVPatient;
            if (retCode)
            {
                newHCVPatient = new HCVPatient
                {
                    HealthCardNumber = newHCN,
                    VCode = newVCode,
                    PostalCode = newpostalCode
                };
            }
            else
            {
                newHCVPatient = null;
            }
            return newHCVPatient;
        }

    }
}
