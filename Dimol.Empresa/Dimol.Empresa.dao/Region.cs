using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dao
{
    public class Region
    {
        public string ListarPaises()
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Paises");
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {   
                    if (i == 0)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                }
                salida = salida.Replace("\"", "'");
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public List<dto.Region> ListarRegionGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Region> lstRegion = new List<dto.Region>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Regiones_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstRegion.Add(new dto.Region()
                        {
                            IdPais = Int16.Parse(ds.Tables[0].Rows[i]["IdPais"].ToString()),
                            NombrePais = ds.Tables[0].Rows[i]["NombrePais"].ToString(),
                            IdRegion = Int16.Parse(ds.Tables[0].Rows[i]["IdRegion"].ToString()),
                            NombreRegion = ds.Tables[0].Rows[i]["NombreRegion"].ToString(),
                            Orden = Int16.Parse(ds.Tables[0].Rows[i]["Orden"].ToString()),
                         
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstRegion;
        }

        public List<dto.Region> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Region> lstRegion = new List<dto.Region>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Regiones_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstRegion.Add(new dto.Region()
                        {
                            IdPais = Int16.Parse(ds.Tables[0].Rows[i]["IdPais"].ToString()),
                            NombrePais = ds.Tables[0].Rows[i]["NombrePais"].ToString(),
                            IdRegion = Int16.Parse(ds.Tables[0].Rows[i]["IdRegion"].ToString()),
                            NombreRegion = ds.Tables[0].Rows[i]["NombreRegion"].ToString(),
                            Orden = Int16.Parse(ds.Tables[0].Rows[i]["Orden"].ToString()),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstRegion;
        }

        public static int ListarRegionGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Regiones_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public void InsertarRegion(dto.Region objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Region");
                sp.AgregarParametro("reg_paiid", objAccion.NombrePais);
                sp.AgregarParametro("reg_nombre", objAccion.NombreRegion.ToUpper());
                sp.AgregarParametro("reg_orden", objAccion.Orden);
                
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarRegion(dto.Region objAccion, int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Region");
                sp.AgregarParametro("reg_paiid", objAccion.NombrePais);
                sp.AgregarParametro("reg_regid",id);
                sp.AgregarParametro("reg_nombre", objAccion.NombreRegion.ToUpper());
                sp.AgregarParametro("reg_orden", objAccion.Orden);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarRegion(int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Region");
                sp.AgregarParametro("reg_regid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
