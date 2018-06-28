using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dimol.dao;



namespace Dimol.ArchivoCoopeuch.dao
{
    public class ArchivoCoopeuch
    {
        public static List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch> ListarGestionesCobralex(int pclid)
        {
            List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch> lst = new List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Cobralex_Gestiones");
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        lst.Add(new Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch()
                        {
                            RutDeudor = ds.Tables[0].Rows[i]["RUT_DEUDOR"].ToString(),
                            CodigoTribunal = ds.Tables[0].Rows[i]["CODIGO_TRIBUNAL"].ToString(),
                            RolTribunal = ds.Tables[0].Rows[i]["ROL_TRIBUNAL"].ToString(),
                            FechaGestion = ds.Tables[0].Rows[i]["FECHA_GESTION"].ToString(),
                            CodigoGestion = ds.Tables[0].Rows[i]["CODIGO_GESTION"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["COMENTARIO"].ToString()
                        });
                    }
                    catch (Exception e)
                    {
                        Dimol.dao.Funciones.InsertarError("ListarGestionesCobralex " + e.Message, e.StackTrace, "Bot ListarGestionesCobralex", 0);
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarGestionesCobralex", 0);
                return lst;
            }
        }

        public static List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch> ListarGestionesCobralexPoderJudicial(int pclid)
        {
            List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch> lst = new List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Cobralex_Gestiones_PoderJudicial");
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        lst.Add(new Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch()
                        {
                            RutDeudor = ds.Tables[0].Rows[i]["RUT_DEUDOR"].ToString(),
                            CodigoTribunal = ds.Tables[0].Rows[i]["CODIGO_TRIBUNAL"].ToString(),
                            RolTribunal = ds.Tables[0].Rows[i]["ROL_TRIBUNAL"].ToString(),
                            FechaGestion = ds.Tables[0].Rows[i]["FECHA_GESTION"].ToString(),
                            CodigoGestion = ds.Tables[0].Rows[i]["CODIGO_GESTION"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["COMENTARIO"].ToString()
                        });
                    }
                    catch (Exception e)
                    {
                        Dimol.dao.Funciones.InsertarError("ListarGestionesCobralexPoderJudicial " + e.Message, e.StackTrace, "Bot ListarGestionesCobralexPoderJudicial", 0);
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarGestionesCobralexPoderJudicial", 0);
                return lst;
            }
        }
    }
}
