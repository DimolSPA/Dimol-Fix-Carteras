using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Carteras.dto;
using System.Data;
using Dimol.dao;
using System.Data.SqlClient;

namespace Dimol.Carteras.dao
{
    public class Accion
    {
        public List<dto.Accion> ListarAccionesGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Accion> lstAcciones = new List<dto.Accion>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Acciones_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where",where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstAcciones.Add(new dto.Accion()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            IdAccion = Int16.Parse(ds.Tables[0].Rows[i]["idAccion"].ToString()),
                            Idioma = Int16.Parse(ds.Tables[0].Rows[i]["Idioma"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Agrupa = ds.Tables[0].Rows[i]["Agrupa"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstAcciones;
        }

        public List<dto.Accion> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Accion> lstAcciones = new List<dto.Accion>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Acciones_Grilla");
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
                        lstAcciones.Add(new dto.Accion()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            IdAccion = Int16.Parse(ds.Tables[0].Rows[i]["idAccion"].ToString()),
                            Idioma = Int16.Parse(ds.Tables[0].Rows[i]["Idioma"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Agrupa = ds.Tables[0].Rows[i]["Agrupa"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstAcciones;
        }

        public string ListarGrupoAcciones(int idioma)
        {
            string salida="";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 6; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipAgru" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    if (i == 1)
                    {
                        salida += i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }
            
        }

        public void InsertarAccion(dto.Accion objAccion,int codemp,int idioma)
        {
            try
            {                
                StoredProcedure sp = new StoredProcedure("_Insertar_Acciones");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("agrupa", objAccion.Agrupa);
                sp.AgregarParametro("idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void BorrarAccion(int codemp, int idAccion)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Acciones");
                sp.AgregarParametro("acc_codemp", codemp);
                sp.AgregarParametro("acc_accid", idAccion);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarAccion(dto.Accion objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Acciones");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("accid", objAccion.IdAccion);
                sp.AgregarParametro("nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("agrupa", objAccion.Agrupa);
                sp.AgregarParametro("idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Dimol.dto.Combobox> ListarTipoAgrupa(int perfil, int idioma)
        {
            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();
            int ultimoAgrupa = 0;
            if (perfil < 5)
            {
                ultimoAgrupa = 4;
            }
            else
            {
                ultimoAgrupa = 5;
            }
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 2; i <= ultimoAgrupa; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipAgru" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lst.Add(new Dimol.dto.Combobox()
                            {
                                Value = i.ToString(),
                                Text = ds.Tables[0].Rows[0][0].ToString()
                            });
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<Dimol.dto.Combobox> ListarAcciones(int codemp, int idioma, string first)
        {
            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Acciones");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();
                lst.Add(new Dimol.dto.Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach(DataRow row in ds.Tables[0].Rows){
                            lst.Add(new Dimol.dto.Combobox()
                        {
                            Value =row[0].ToString(),
                            Text = row[1].ToString()
                        });
                        }
                        
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static int BuscarAccionesAgrupa(int codemp, int accid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Acciones_Agrupa");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("accid", accid);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            return Int32.Parse(row[0].ToString());
                        }

                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static DateTime BuscarUltimaFechaAcciones(int codemp, int pclid, int ctcid, int accid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Ultima_Accion_Fecha");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("accid", accid);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            return DateTime.Parse(row[0].ToString());
                        }

                    }
                }
                return new DateTime();
            }
            catch (Exception ex)
            {
                return new DateTime();
            }

        }

    }
}
