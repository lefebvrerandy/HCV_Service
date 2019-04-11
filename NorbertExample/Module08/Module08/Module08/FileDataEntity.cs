using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module08
{
    public class FileDataEntity
    {
        public int ID { get; set; }
        public string Action { get; set; }
        public string PathName { get; set; }
        public string OldPathName { get; set; }
        public DateTime TimeAffected { get; set; }


    }
}
