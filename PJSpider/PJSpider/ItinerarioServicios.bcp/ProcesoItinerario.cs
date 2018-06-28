using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItinerarioServicios.dto;
using Dimol.dao;
using System.Data;
using System.ServiceProcess;
using Dimol.WindowsService.bcp;

namespace ItinerarioServicios.bcp
{
    public class ProcesoItinerario
    {
        public static void ListarProcesosItinerario()
        {
            
            try
            {
                List<Proceso> lstProcesos = new List<Proceso>();
                lstProcesos = dao.ProcesoItinerario.ListarProcesosItinerario();
                
                foreach (Proceso p in lstProcesos)
                {
                    if (p.Servidor != "N/A")
                    {
                        ImpersonateUser iu = new ImpersonateUser();
                        iu.Impersonate();
                        Dimol.WindowsService.bcp.Service s = new Dimol.WindowsService.bcp.Service(p.Nombre, p.Servidor);
                        iu.Undo();

                        if (p.Status == "Stop")
                        {
                            if (s.Running)
                            {
                                s.StopService();
                                Dimol.dao.Funciones.InsertarError("Deteniendo Servicio", "Proceso: " + p.Nombre + " Servidor: " + p.Servidor + " ProcesoID: " + p.ProcesoId.ToString(), "Detiene el servicio", 0);
                            }
                          
                        }
                        else
                        {
                            if (!s.Running)
                            {
                                s.StartService();
                                Dimol.dao.Funciones.InsertarError("Iniciando Servicio", "Proceso: " + p.Nombre + " Servidor: " + p.Servidor + " ProcesoID: " + p.ProcesoId.ToString(), "Detiene el servicio", 0);
                            }
                        }
                    }
                    
                    //ServiceController service = new ServiceController(p.Nombre, p.Servidor);
                    //if ((service.Status == ServiceControllerStatus.Stopped) || (service.Status == ServiceControllerStatus.StopPending))
                    //{
                    //    service.Start();
                    //}

                }
                
            }
            catch (Exception ex)
            {

                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarProcesosItinerario", 0);
               
            }
          
            
           
        }
    }
}
