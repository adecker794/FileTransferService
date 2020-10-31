using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;

namespace FileTransferService
{
    class FileTransfer
    {
        private readonly System.Timers.Timer aTimer;
        public static string time = ConfigurationManager.AppSettings["timer"];

        private void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            TransferringFile();
        }
        public FileTransfer()
        {
            int time1 = Int32.Parse(time);
            aTimer = new System.Timers.Timer(time1) { AutoReset = true };
            aTimer.Enabled = true;
            aTimer.Elapsed += OnTimedEvent;
        }
        public void Start()
        {
            TransferringFile();
            aTimer.Start();
        }

        public void Stop()
        {
            aTimer.Stop();
        }

        public void TransferringFile()
        {
            string fileName = ConfigurationManager.AppSettings["fileName"];
            string sourcePath = ConfigurationManager.AppSettings["sourcePath"];
            string targetPath = ConfigurationManager.AppSettings["targetPath"];
            Console.WriteLine(sourcePath);
            Console.WriteLine(targetPath);
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            System.IO.File.Copy(sourceFile, destFile, true);
        }


    }
}
