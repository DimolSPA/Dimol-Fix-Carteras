using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.ArchivoCoopeuch
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
                CrearArchivoCoopeuch service1 = new CrearArchivoCoopeuch();
                service1.TestStartupAndStop(args);
            }
            else
            {
                // Put the body of your old Main method here.
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                                                { 
                                                    new CrearArchivoCoopeuch() 
                                                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
