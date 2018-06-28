using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.ArchivoCopec
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                CrearArchivoCopec service1 = new CrearArchivoCopec();
                service1.TestStartupAndStop(args);
            }
            else { 
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new CrearArchivoCopec()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
