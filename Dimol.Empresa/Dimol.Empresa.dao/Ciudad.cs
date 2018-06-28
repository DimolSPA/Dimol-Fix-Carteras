using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dao
{
    public class Ciudad
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

        public string ListarRegiones(int idPais)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Regiones");
                sp.AgregarParametro("idPais", idPais);
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

        public string ListarRegiones()
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Regiones");
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

        public List<dto.Ciudad> ListarCiudadGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Ciudad> lstCiudad = new List<dto.Ciudad>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Ciudades_Grilla");
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
                        lstCiudad.Add(new dto.Ciudad()
                        {
                            IdPais = Int16.Parse(ds.Tables[0].Rows[i]["IdPais"].ToString()),
                            NombrePais = ds.Tables[0].Rows[i]["NombrePais"].ToString(),
                            IdRegion = Int16.Parse(ds.Tables[0].Rows[i]["IdRegion"].ToString()),
                            NombreRegion = ds.Tables[0].Rows[i]["NombreRegion"].ToString(),
                            IdCiudad = Int16.Parse(ds.Tables[0].Rows[i]["IdCiudad"].ToString()),
                            NombreCiudad = ds.Tables[0].Rows[i]["NombreCiudad"].ToString(),
                            CodigoArea = Int16.Parse(ds.Tables[0].Rows[i]["CodigoArea"].ToString()),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstCiudad;
        }

        public List<dto.Ciudad> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Ciudad> lstCiudad = new List<dto.Ciudad>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Ciudades_Grilla");
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
                        lstCiudad.Add(new dto.Ciudad()
                        {
                            IdPais = Int16.Parse(ds.Tables[0].Rows[i]["IdPais"].ToString()),
                            NombrePais = ds.Tables[0].Rows[i]["NombrePais"].ToString(),
                            IdRegion = Int16.Parse(ds.Tables[0].Rows[i]["IdRegion"].ToString()),
                            NombreRegion = ds.Tables[0].Rows[i]["NombreRegion"].ToString(),
                            IdCiudad = Int16.Parse(ds.Tables[0].Rows[i]["IdCiudad"].ToString()),
                            NombreCiudad = ds.Tables[0].Rows[i]["NombreCiudad"].ToString(),
                            CodigoArea = Int16.Parse(ds.Tables[0].Rows[i]["CodigoArea"].ToString()),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstCiudad;
        }

        public static int ListarCiudadGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Ciudades_Grilla_Count");
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

        public void InsertarCiudad(dto.Ciudad objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Ciudad");
                sp.AgregarParametro("ciu_regid", objAccion.NombreRegion);
                sp.AgregarParametro("ciu_nombre", objAccion.NombreCiudad.ToUpper());
                sp.AgregarParametro("ciu_codarea", objAccion.CodigoArea);

                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarCiudad(dto.Ciudad objAccion, int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Ciudad");
                sp.AgregarParametro("ciu_regid", objAccion.NombreRegion);
                sp.AgregarParametro("ciu_ciuid", id);
                sp.AgregarParametro("ciu_nombre", objAccion.NombreCiudad.ToUpper());
                sp.AgregarParametro("ciu_codarea", objAccion.CodigoArea);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarCiudad(int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Ciudad");
                sp.AgregarParametro("ciu_ciuid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
