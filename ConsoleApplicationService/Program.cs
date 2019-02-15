using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Configuration;
using LogService;

namespace ConsoleApplicationService
{
    class Program
    {
        static void Main(string[] args)
        {           
            var service = new LogService.ServiceFR();
            ServiceBase[] serviceToRun = new ServiceBase[] { service };
            //if(Environment.UserInteractive)
            //{
                //Console.CancelKeyPress += (x, y) => service.Stop();
                service.Start();
            //}
            //else
            //{
           //     ServiceBase.Run(serviceToRun);
            //}
        }
    }
}
