using PJSpider.bcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PJSpider
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[] {"1","5"};
            }
            int particion = 1;
            int cantidad = 5;
            try
            {
                particion = Int32.Parse( args[0]);
                cantidad = Int32.Parse(args[1]);
                Causa.ActualizarPoderJudicialParticion(1, 1, "-1", particion, cantidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el proceso. Particion: " + particion + "Mensaje: " + ex.Message);
            }

            //if (Environment.UserInteractive)
            //{
            //    PJSpiderService service1 = new PJSpiderService();
            //    service1.TestStartupAndStop(args);
            //}
            //else
            //{
            //    // Put the body of your old Main method here.
            //    ServiceBase[] ServicesToRun;
            //    ServicesToRun = new ServiceBase[] 
            //                                { 
            //                                    new PJSpiderService() 
            //                                };
            //    ServiceBase.Run(ServicesToRun);
            //}
            
        }
    }
}
