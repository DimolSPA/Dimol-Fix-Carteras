using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Carteras.Mantenedores.dto;
using System.Data;
using Dimol.dao;
using System.Data.SqlClient;

namespace Dimol.Carteras.Mantenedores.dao
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
                    sp.AgregarParametro("codigo", "AgrAc"+i);
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

    }
}
