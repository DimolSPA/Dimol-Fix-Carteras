using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ActualizaRolInternoService
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                ActualizaRolInternoService service1 = new ActualizaRolInternoService();
                service1.TestStartupAndStop(args);
            }
            else
            {
                // Put the body of your old Main method here.
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                                                { 
                                                    new ActualizaRolInternoService() 
                                                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
