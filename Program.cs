using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;


namespace FileTransferService
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.SetServiceName("FileTransferService");
                x.SetDisplayName("File Transfer Service");
                x.SetDescription("This is a service that transfers a file at a set interval");
                x.RunAsLocalSystem();
                x.StartAutomaticallyDelayed();

            x.Service<FileTransfer>(s =>
                {
                    s.ConstructUsing(filetransfer => new FileTransfer());
                    s.WhenStarted(filetransfer => filetransfer.Start());
                    s.WhenStopped(filetransfer => filetransfer.Stop());
                });

                x.EnableServiceRecovery(recoveryOption =>
                {
                    //Console.WriteLine("In the service recovery section");
                    recoveryOption.RestartService(1);   // first failure
                    //log.Error("Restarting service after first failure");
                    recoveryOption.RestartService(5);   // second failure
                    //log.Error("Restarting service after second failure");
                    recoveryOption.RestartService(5);   // third failure
                    //log.Error("Restarting service after third failure");

                });
            });
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
