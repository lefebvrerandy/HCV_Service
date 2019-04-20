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

        private readonly HCVEntities _context;

        public HCVDAL()
        {
            _context = new HCVEntities();
        }

        /// <summary>
        /// This method will be called to retrieve information from the database given the HCN to look up.
        /// </summary>
        /// <param name="HCN">The Health Card number of the HCVpatient</param>
        /// <returns>The patient object</returns>
        public List<HCVPatient> GetHCVPatient(string[] HCN)
        {
            List<HCVPatient> HCVpatient = new List<HCVPatient>();
            for (int i = 0; i < HCN.Length; i++)
            {
                string HCNstring = HCN[i];
                HCVpatient.Add(_context.HCVPatients.FirstOrDefault(x => x.HealthCardNumber == HCNstring));
            }
            return HCVpatient;
        }

        public HCVPatient GetHCVPatient(string HCN)
        {
            HCVPatient HCVpatient = new HCVPatient();
            HCVpatient = _context.HCVPatients.FirstOrDefault(x => x.HealthCardNumber == HCN);

            return HCVpatient;
        }

        /// <summary>
        /// This method will be called to add a patient to the HCV database
        /// </summary>
        /// <param name="newHCVpatient">The new HCVPatient that will be added</param>
        /// <returns>Validation</returns>
        public bool AddHCVPatient(HCVPatient newHCVpatient)
        {
            HCVPatient HCVpatient = _context.HCVPatients.Add(newHCVpatient);
            _context.SaveChanges();
            return HCVpatient == null ? false : true;
        }

        /// <summary>
        /// This method will be used to update the patients information health card information
        /// inside of the HCV database.
        /// </summary>
        /// <param name="HCN">The health card number to look up</param>
        /// <param name="newHCVpatient">The new information that will be replaced with</param>
        /// <returns>Validation</returns>
        public bool UpdateHCVPatient(string HCN, HCVPatient newHCVpatient)
        {
            bool retCode = false;

            var result = _context.HCVPatients.SingleOrDefault(x => x.HealthCardNumber == HCN);
            if (result != null)
            {
                // Update the row with the new information
                // each row will have the three columns (HCN, VCode, PostalCode) updated
                if (newHCVpatient.HealthCardNumber != "" && newHCVpatient.HealthCardNumber != null)
                {
                    result.HealthCardNumber = newHCVpatient.HealthCardNumber;
                }
                if (newHCVpatient.VCode != "" && newHCVpatient.VCode != null)
                {
                    result.VCode = newHCVpatient.VCode;
                }
                if (newHCVpatient.PostalCode != "" && newHCVpatient.PostalCode != null)
                {
                    result.PostalCode = newHCVpatient.PostalCode;
                }

                _context.SaveChanges();
                retCode = true;
            }

            return retCode;
        }
    }
}
