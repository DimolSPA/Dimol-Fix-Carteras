using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dao
{
    public class Comuna
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

        public string ListarCiudades()
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Ciudades");
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

        public List<dto.Comuna> ListarComunaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Comuna> lstComuna = new List<dto.Comuna>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comunas_Grilla");
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
                        lstComuna.Add(new dto.Comuna()
                        {
                            IdPais = Int16.Parse(ds.Tables[0].Rows[i]["IdPais"].ToString()),
                            NombrePais = ds.Tables[0].Rows[i]["NombrePais"].ToString(),
                            IdRegion = Int16.Parse(ds.Tables[0].Rows[i]["IdRegion"].ToString()),
                            NombreRegion = ds.Tables[0].Rows[i]["NombreRegion"].ToString(),
                            IdCiudad = Int16.Parse(ds.Tables[0].Rows[i]["IdCiudad"].ToString()),
                            NombreCiudad = ds.Tables[0].Rows[i]["NombreCiudad"].ToString(),
                            IdComuna = Int16.Parse(ds.Tables[0].Rows[i]["IdComuna"].ToString()),
                            NombreComuna = ds.Tables[0].Rows[i]["NombreComuna"].ToString(),
                            CodigoPostal = ds.Tables[0].Rows[i]["CodigoPostal"].ToString(),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstComuna;
        }

        public List<dto.Comuna> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Comuna> lstComuna = new List<dto.Comuna>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comunas_Grilla");
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
                        lstComuna.Add(new dto.Comuna()
                        {
                            IdPais = Int16.Parse(ds.Tables[0].Rows[i]["IdPais"].ToString()),
                            NombrePais = ds.Tables[0].Rows[i]["NombrePais"].ToString(),
                            IdRegion = Int16.Parse(ds.Tables[0].Rows[i]["IdRegion"].ToString()),
                            NombreRegion = ds.Tables[0].Rows[i]["NombreRegion"].ToString(),
                            IdCiudad = Int16.Parse(ds.Tables[0].Rows[i]["IdCiudad"].ToString()),
                            NombreCiudad = ds.Tables[0].Rows[i]["NombreCiudad"].ToString(),
                            IdComuna = Int16.Parse(ds.Tables[0].Rows[i]["IdComuna"].ToString()),
                            NombreComuna = ds.Tables[0].Rows[i]["NombreComuna"].ToString(),
                            CodigoPostal = ds.Tables[0].Rows[i]["CodigoPostal"].ToString(),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstComuna;
        }

        public static int ListarComunaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comunas_Grilla_Count");
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

        public void InsertarComuna(dto.Comuna objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Comuna");
                sp.AgregarParametro("com_ciuid", objAccion.NombreCiudad);
                sp.AgregarParametro("com_nombre", objAccion.NombreComuna.ToUpper());
                sp.AgregarParametro("com_codpost", objAccion.CodigoPostal);

                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarComuna(dto.Comuna objAccion, int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Comuna");
                sp.AgregarParametro("com_ciuid", objAccion.NombreCiudad);
                sp.AgregarParametro("com_comid", id);
                sp.AgregarParametro("com_nombre", objAccion.NombreComuna.ToUpper());
                sp.AgregarParametro("com_codpost", objAccion.CodigoPostal);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarComuna(int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Comuna");
                sp.AgregarParametro("com_comid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
    }
}
