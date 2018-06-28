using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class HojaTramite
    {
        public static void TraeTitulo(dto.HojaTramite obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Historial_Gestiones_Titulo");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("ctcid", DBNull.Value);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.Titulo = new dto.TituloReporte
                        {
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                

        public static void ListarTramitesDetalle(dto.HojaTramite obj)
        {
            try
            {
                List<dto.HojaTramiteBruto> lstBruto = new List<dto.HojaTramiteBruto>();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Hoja_Tramite");
                sp.AgregarParametro("rol_codemp", obj.Codemp);
                sp.AgregarParametro("rol_pclid", obj.Pclid);
                sp.AgregarParametro("idi_idid", obj.Idioma);
                sp.AgregarParametro("ctc_id", obj.Ctcid);
                sp.AgregarParametro("ect_prejud", obj.EstadoCpbt);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    { 
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {                                                      

                            lstBruto.Add(new dto.HojaTramiteBruto
                            {

                                RutDeudor = Dimol.bcp.Funciones.formatearRut(dr["ctc_numero"].ToString() + dr["ctc_digito"].ToString()),
                                NombreDeudor = dr["ctc_nomfant"].ToString(),
                                Causa = dr["tci_nombre"].ToString(),
                                Juzgado = dr["trb_nombre"].ToString(),
                                Monto = decimal.Parse(dr["rol_total"].ToString()),
                                Rol = dr["rol_numero"].ToString(),
                                Direccion = dr["ctc_direccion"].ToString(),
                                Ciudad = dr["ciu_nombre"].ToString(),
                                IdDetalle = Int32.Parse(dr["esj_orden"].ToString()),
                                FechaJudicial = DateTime.Parse(dr["rle_fecjud"].ToString()),
                                Materia = dr["mji_nombre"].ToString(),
                                Estado = dr["eci_nombre"].ToString(),
                                Comentario = dr["comentario"].ToString(),
                                CodigoMoneda = Int32.Parse(dr["ccb_codmon"].ToString()),
                            });                        
                        }                                             

                        List<string> lstRoles = lstBruto.Select(o => o.Rol).Distinct().ToList();

                        Dimol.dto.Indicadores objInd = new Dimol.dto.Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objInd);

                        foreach (string rol in lstRoles)
                        {
                            dto.HojaTramiteCliente objHojaCli = new dto.HojaTramiteCliente();


                            foreach (var item in lstBruto.Where(o =>o.Rol == rol))
                            {
                                objHojaCli.lstHojaDetalle.Add(new dto.HojaTramiteDetalle
                                {
                                    IdDetalle = item.IdDetalle,
                                    FechaJudicial = item.FechaJudicial,
                                    Materia = item.Materia,
                                    Estado = item.Estado,
                                    Comentario = item.Comentario
                                });

                                objHojaCli.RutDeudor = Dimol.bcp.Funciones.formatearRut(item.RutDeudor);
                                objHojaCli.NombreDeudor = item.NombreDeudor;
                                objHojaCli.Causa = item.Causa;
                                objHojaCli.Juzgado = item.Juzgado;
                                
                                objHojaCli.Rol = item.Rol;
                                objHojaCli.Direccion = item.Direccion;
                                objHojaCli.Ciudad = item.Ciudad;

                                switch (item.CodigoMoneda)
                                {
                                    case 1:
                                        objHojaCli.Monto = item.Monto;
                                        break;
                                    case 2:
                                        objHojaCli.Monto = item.Monto * objInd.UF;
                                        break;
                                    case 3:
                                        objHojaCli.Monto = item.Monto * objInd.DolarObservado;
                                        break;
                                    default:
                                        break;
                                }
                            }
                                           
                            obj.lstHojaCliente.Add(objHojaCli);                           

                        }
                        
                    }
                }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
