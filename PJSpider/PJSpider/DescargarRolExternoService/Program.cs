using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DescargarRolExternoService
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
                DescargarRolExternoService service1 = new DescargarRolExternoService();
                service1.TestStartupAndStop(args);
            }
            else
            {
                // Put the body of your old Main method here.
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                                                { 
                                                    new DescargarRolExternoService() 
                                                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
