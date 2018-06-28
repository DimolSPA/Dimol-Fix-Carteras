using Dimol.dao;
using Dimol.WindowsService.bcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
{
    public class Proceso
    {
        public static List<dto.Proceso> ListarProceso(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Proceso> lst = new List<dto.Proceso>();
            try
            {
                lst = dao.Proceso.ListarProceso(codemp, where, sidx, sord, inicio, limite);
                ImpersonateUser iu = new ImpersonateUser();
                iu.Impersonate();
                
                foreach (dto.Proceso p in lst)
                {
                    Dimol.WindowsService.bcp.Service s = new Dimol.WindowsService.bcp.Service(p.Nombre, p.Servidor);
                    

                    //if (impersonation.impersonateValidUser(Username, Domain, Password))
                    //{
                    if (s.Running)
                    {
                        p.Running = true;
                        //s.StopService();
                    }
                    else
                    {
                        p.Running = false;
                        //s.StartService();
                    }

                }
                iu.Undo();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.Proceso.ListarProceso", 0);
            }

            return lst;
        }

        public static int ListarProcesoCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
           return dao.Proceso.ListarProcesoCount(codemp, where, sidx,  sord, inicio,  limite);
        }
    }
}
