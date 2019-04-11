using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module08;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Log("Test Method");
            DAL dal = new DAL();
            FileDataEntity fde = new FileDataEntity();
            fde.PathName = "some path name";
            fde.OldPathName = "some old path name";
            fde.Action = "Create";
            fde.TimeAffected = DateTime.UtcNow;

            dal.AddFileData(fde);

            List<FileDataEntity> flist = dal.GetFDEList();

            foreach (FileDataEntity fdeItem in flist)
            {
                Console.WriteLine(fdeItem.Action + ", " + fdeItem.PathName + ", " + fdeItem.TimeAffected.ToString());
            }

            Console.WriteLine("Enter to Continue...");
            Console.ReadLine();

        }
    }
}
