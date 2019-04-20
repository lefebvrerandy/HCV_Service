using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCV_Class_Library
{
    public interface IHCVDAL
    {
        List<HCVPatient> GetHCVPatient(string[] HCN);
        HCVPatient GetHCVPatient(string HCN);
        bool AddHCVPatient(HCVPatient newHCVpatient);
        bool UpdateHCVPatient(string HCN, HCVPatient newHCVpatient);
    }
}
