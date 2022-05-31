using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace DbConsole
{
     class Program
    {
        static void Main(string[] args)
        {
            RecordModel.Upload(); 
            return; 

            var exitcode = HostFactory.Run(x =>
                {
                    x.Service<RecordModel>(s =>
                      {
                      s.ConstructUsing(sendtoLIS => new RecordModel());
                          s.WhenStarted(sendtoLIS =>  sendtoLIS.Start());
                          s.WhenStopped(sendtoLIS =>  sendtoLIS.Stop());
                      });

                    x.RunAsLocalSystem();

                    x.SetServiceName("FetchDBService");
                    x.SetDisplayName("FetchDBService");
                    x.SetDescription("This is a service for fetching column from a DB.");
             });

            int exitCodeValue = (int)Convert.ChangeType(exitcode, exitcode.GetTypeCode());
             Environment.ExitCode = exitCodeValue;
        }
    }
}
