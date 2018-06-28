using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dao
{
    public class Perfil
    {

        public List<dto.Perfil> ListarPerfilGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Perfil> lstPerfil = new List<dto.Perfil>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Perfiles_Grilla");
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
                        lstPerfil.Add(new dto.Perfil()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Administrador = ds.Tables[0].Rows[i]["Administrador"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPerfil;
        }

        public List<dto.Perfil> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Perfil> lstPerfil = new List<dto.Perfil>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Perfiles_Grilla");
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
                        lstPerfil.Add(new dto.Perfil()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Administrador = ds.Tables[0].Rows[i]["Administrador"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPerfil;
        }

        public static int ListaPerfilGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Perfiles_Grilla_Count");
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

        public void InsertarPerfil(dto.Perfil objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Perfiles");
                sp.AgregarParametro("prf_codemp", codemp);
                sp.AgregarParametro("prf_nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("prf_administrador", objAccion.Administrador.ToUpper() == "ON" || objAccion.Administrador.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarPerfil(dto.Perfil objAccion, int codemp, int id,int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Perfiles");
                sp.AgregarParametro("prf_codemp", codemp);
                sp.AgregarParametro("prf_prfid", id);
                sp.AgregarParametro("prf_nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("prf_administrador", objAccion.Administrador.ToUpper() == "ON" || objAccion.Administrador.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarPerfil(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Perfiles");
                sp.AgregarParametro("prf_codemp", codemp);
                sp.AgregarParametro("prf_prfid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
