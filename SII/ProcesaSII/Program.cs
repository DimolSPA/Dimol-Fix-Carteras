using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SII.bcp;
using SII.dto;

namespace ProcesaSII
{
    class Program
    {
        static void Main(string[] args)
        {
            string estado = "1";
            if(args.Length >0){
                estado = args[0];
            }

            List<SII.dto.Status> lst = SII.bcp.Procesar.ListarRutporProcesar(estado);
            List<SII.dto.Combobox> lstTipoActividad =  SII.bcp.Status.ListarActividadEconomica();
            List<SII.dto.Combobox> lstTipoDocumento = SII.bcp.Status.ListarTipoDocumento();

            do
            {
                foreach (SII.dto.Status s in lst)
                {
                    SII.bcp.Procesar.ProcesarRutHTML(s,lstTipoActividad, lstTipoDocumento);
                }
                lst = SII.bcp.Procesar.ListarRutporProcesar(estado);
            } while (lst.Count > 0);
        }
    }
}
