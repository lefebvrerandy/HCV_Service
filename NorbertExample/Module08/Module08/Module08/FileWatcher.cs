using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Module08
{
    class FileWatcher
    {
        FileSystemWatcher fsw;
        DAL dal;
        public bool Done { get; set; }

        public FileWatcher()
        {
            dal = new DAL();

            fsw = new FileSystemWatcher(@"c:\nmtemp");
            fsw.Created += new FileSystemEventHandler(fsw_Created);
            fsw.Deleted += new FileSystemEventHandler(fsw_Deleted);
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.Renamed += new RenamedEventHandler(fsw_Renamed);
            fsw.EnableRaisingEvents = true;
            Done = false;
        }

        void fsw_Renamed(object sender, RenamedEventArgs e)
        {
            FileDataEntity fde = new FileDataEntity();
            fde.Action = "Rename";
            fde.PathName = e.FullPath;
            fde.OldPathName = e.OldFullPath;
            fde.TimeAffected = DateTime.UtcNow;

            dal.AddFileData(fde);
        }

        void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            FileDataEntity fde = new FileDataEntity();
            fde.Action = "Change";
            fde.PathName = e.FullPath;
            fde.OldPathName = "";
            fde.TimeAffected = DateTime.UtcNow;

            dal.AddFileData(fde);
        }

        void fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            FileDataEntity fde = new FileDataEntity();
            fde.Action = "Delete";
            fde.PathName = e.FullPath;
            fde.OldPathName = "";
            fde.TimeAffected = DateTime.UtcNow;

            dal.AddFileData(fde);
        }

        void fsw_Created(object sender, FileSystemEventArgs e)
        {
            FileDataEntity fde = new FileDataEntity();
            fde.Action = "Create";
            fde.PathName = e.FullPath;
            fde.OldPathName = "";
            fde.TimeAffected = DateTime.UtcNow;

            dal.AddFileData(fde);
        }

        public void Run()
        {
            while (!Done)
            {
                System.Threading.Thread.Sleep(1);
            }
        }
    }
}
