using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dao
{
    public class EmpresaConfiguracion
    {

        public List<dto.EmpresaConfiguracion> ListarEmpresaConfiguracionGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EmpresaConfiguracion> lstEmpresaConfiguracion = new List<dto.EmpresaConfiguracion>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Empresa_Configuracion_Grilla");
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
                        lstEmpresaConfiguracion.Add(new dto.EmpresaConfiguracion()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            ValorNumerico = ds.Tables[0].Rows[i]["ValorNumerico"].ToString(),
                            ValorTexto = ds.Tables[0].Rows[i]["ValorTexto"].ToString()

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEmpresaConfiguracion;
        }

        public List<dto.EmpresaConfiguracion> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EmpresaConfiguracion> lstEmpresaConfiguracion = new List<dto.EmpresaConfiguracion>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Empresa_Configuracion_Grilla");
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
                        lstEmpresaConfiguracion.Add(new dto.EmpresaConfiguracion()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            ValorNumerico = ds.Tables[0].Rows[i]["ValorNumerico"].ToString(),
                            ValorTexto = ds.Tables[0].Rows[i]["ValorTexto"].ToString()

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEmpresaConfiguracion;
        }

        public static int ListarEmpresaEmpresaConfiguracionCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Empresa_Configuracion_Grilla_Count");
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


        public void InsertarEmpresaConfiguracion(dto.EmpresaConfiguracion objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Empresa_Configuracion");
                sp.AgregarParametro("emc_codemp", codemp);
                sp.AgregarParametro("emc_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("emc_valnum", (object)objAccion.ValorNumerico.Replace(",",".") ?? DBNull.Value);
                sp.AgregarParametro("emc_valtxt", (object)objAccion.ValorTexto ?? DBNull.Value);
               
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarEmpresaConfiguracion(dto.EmpresaConfiguracion objAccion, int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Empresa_Configuracion");
                sp.AgregarParametro("emc_codemp", codemp);
                sp.AgregarParametro("emc_emcid", id);
                sp.AgregarParametro("emc_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("emc_valnum", (object)objAccion.ValorNumerico.Replace(",",".") ?? DBNull.Value);
                sp.AgregarParametro("emc_valtxt", (object)objAccion.ValorTexto ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarEmpresaConfiguracion(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Empresa_Configuracion");
                sp.AgregarParametro("emc_codemp", codemp);
                sp.AgregarParametro("emc_emcid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
