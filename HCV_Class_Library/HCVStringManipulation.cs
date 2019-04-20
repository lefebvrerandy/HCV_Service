using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCV_Class_Library
{
    public class HCVStringManipulation
    {
        public string[] ConstructCompareString(List<HCVPatient> patient)
        {
            string[] finalString = new string[patient.Count];
            if (patient != null)
            {
                for (int i = 0; i < patient.Count; i++)
                {
                    if (patient[i] != null)
                    {
                        string hcn = patient[i].HealthCardNumber;
                        string vcode = patient[i].VCode;
                        string postalcode = patient[i].PostalCode;
                        finalString[i] = hcn + "," + vcode + "," + postalcode;
                        if (finalString[i].Length > 20)
                        {
                            finalString[i] = finalString[i].Remove(20);        // Remove any extra white space
                        }
                    }
                    else
                    {
                        finalString[i] = "null";
                    }

                }
            }

            return finalString;
        }

        public string TearOffBeginningAndEnd(string content)
        {
            char[] trimStart = { 'G', 'E', 'T', ' ', 'S', 'E', 'T' };
            string temp = content.TrimStart(trimStart);
            string LookingForPatient = "";
            if (temp.Contains("<EOF>"))
            {
                LookingForPatient = temp.Replace("<EOF>", "");
            }

            return LookingForPatient;
        }

        public string TearOffVCodeAndPostal(string lookingForPatient)
        {
            string HCN = lookingForPatient.Remove(10);               // Remove VCode and Postal code
            return HCN;
        }

        public string ConstructVCode(string lookingForPatient)
        {
            char[] healthCardNumber = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' };
            string vcode = lookingForPatient.Trim(healthCardNumber);
            vcode = vcode.Remove(2);
            return vcode;
        }

        public string ConstructPostalCode(string lookingForPatient)
        {
            string[] temp = lookingForPatient.Split(',');
            string postalcode = temp[2];

            return postalcode;
        }

        public string[] RearrangeStringArray(string[] returningString)
        {
            string[] finalStringArray = new string[returningString.Length];

            for(int i = 0; i < returningString.Length; i++)
            {
                finalStringArray[i] = returningString[returningString.Length - i - 1];
            }

            return finalStringArray;
        }
    }
}
