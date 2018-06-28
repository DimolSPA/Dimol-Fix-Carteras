using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItinerarioServicios.dto;
using Dimol.dao;
using System.Data;

namespace ItinerarioServicios.dao
{
    public class ProcesoItinerario
    {
        public static List<Proceso> ListarProcesosItinerario()
        {
             List<Proceso> lst = new List<Proceso>();
             try
             {
                 DataSet ds = new DataSet();
                 StoredProcedure sp = new StoredProcedure("_Listar_Proceso_Itinerarios");
                 ds = sp.EjecutarProcedimiento();
                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 {
                     try
                     {
                         lst.Add(new Proceso()
                         {
                             Codemp = Int32.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                             ProcesoId = Int32.Parse(ds.Tables[0].Rows[i]["PROCESO"].ToString()),
                             DiaSemana = ds.Tables[0].Rows[i]["DIASEMANA"].ToString(),
                             Dia = Int32.Parse(ds.Tables[0].Rows[i]["DIA"].ToString()),
                             Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                             Servidor = ds.Tables[0].Rows[i]["SERVIDOR"].ToString(),
                             Status = ds.Tables[0].Rows[i]["ESTATUS"].ToString()
                         });
                     }
                     catch (Exception e)
                     {
                         Dimol.dao.Funciones.InsertarError("Proceso Id incorrecto", "Proceso: " + ds.Tables[0].Rows[i]["PROCESO"].ToString(), "Bot Itinerarios de procesos", 0);
                     }
                 }

                 return lst;
             }
             catch (Exception ex)
             {
                 Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Lista Procesos Itinerario", 0);
                 return lst;
             }
        }
    }
}
