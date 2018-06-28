using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dao
{
    public class EmpresaSucursal
    {

        public string ListaComunas()
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_comunas");
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

        public List<dto.EmpresaSucursal> ListarEmpresaSucursalGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EmpresaSucursal> lstEmpresaSucursal = new List<dto.EmpresaSucursal>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Empresa_Sucursal_Grilla");
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
                        lstEmpresaSucursal.Add(new dto.EmpresaSucursal()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            IdComuna = Int16.Parse(ds.Tables[0].Rows[i]["IdComuna"].ToString()),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                            Telefono =  ds.Tables[0].Rows[i]["Telefono"].ToString(),
                            Fax  =  ds.Tables[0].Rows[i]["Fax"].ToString(),
                            Email =  ds.Tables[0].Rows[i]["Email"].ToString(),
                            Css = ds.Tables[0].Rows[i]["Css"].ToString(),
                            Matriz = ds.Tables[0].Rows[i]["Matriz"].ToString().ToUpper() == "S" ? "ON" : "OFF"

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEmpresaSucursal;
        }

        public List<dto.EmpresaSucursal> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EmpresaSucursal> lstEmpresaSucursal = new List<dto.EmpresaSucursal>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Empresa_Sucursal_Grilla");
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
                        lstEmpresaSucursal.Add(new dto.EmpresaSucursal()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            IdComuna = Int16.Parse(ds.Tables[0].Rows[i]["IdComuna"].ToString()),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                            Telefono = ds.Tables[0].Rows[i]["Telefono"].ToString(),
                            Fax = ds.Tables[0].Rows[i]["Fax"].ToString(),
                            Email = ds.Tables[0].Rows[i]["Email"].ToString(),
                            Css = ds.Tables[0].Rows[i]["Css"].ToString(),
                            Matriz = ds.Tables[0].Rows[i]["Matriz"].ToString().ToUpper() == "S" ? "ON" : "OFF"

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEmpresaSucursal;
        }


        public static int ListarEmpresaSucursalGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Empresa_Sucursal_Grilla_Count");
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

        public void InsertarEmpresaSucursal(dto.EmpresaSucursal objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Empresa_Sucursal");
                sp.AgregarParametro("esu_codemp",codemp);
                sp.AgregarParametro("esu_nombre",  (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("esu_comid", (object)objAccion.Comuna ?? DBNull.Value);
                sp.AgregarParametro("esu_direccion", (object)objAccion.Direccion ?? DBNull.Value);
                sp.AgregarParametro("esu_telefono", (object)objAccion.Telefono ?? DBNull.Value);
                sp.AgregarParametro("esu_fax", (object)objAccion.Fax ?? DBNull.Value);
                sp.AgregarParametro("esu_mail", (object)objAccion.Email ?? DBNull.Value);
                sp.AgregarParametro("esu_css", (object)objAccion.Css ?? DBNull.Value);
                sp.AgregarParametro("esu_matriz", objAccion.Matriz.ToUpper() == "ON" || objAccion.Matriz.ToUpper() == "YES" ? "S" : "N");

                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarEmpresaSucursal(dto.EmpresaSucursal objAccion, int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Empresa_Sucursal");
                sp.AgregarParametro("esu_codemp", codemp);
                sp.AgregarParametro("esu_sucid", id);
                sp.AgregarParametro("esu_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("esu_comid", (object)objAccion.Comuna ?? DBNull.Value);
                sp.AgregarParametro("esu_direccion", (object)objAccion.Direccion ?? DBNull.Value);
                sp.AgregarParametro("esu_telefono", (object)objAccion.Telefono ?? DBNull.Value);
                sp.AgregarParametro("esu_fax", (object)objAccion.Fax ?? DBNull.Value);
                sp.AgregarParametro("esu_mail", (object)objAccion.Email ?? DBNull.Value);
                sp.AgregarParametro("esu_css", (object)objAccion.Css ?? DBNull.Value);
                sp.AgregarParametro("esu_matriz", objAccion.Matriz.ToUpper() == "ON" || objAccion.Matriz.ToUpper() == "YES" ? "S" : "N");
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarEmpresaSucursal(int codemp,int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Empresa_Sucursal");
                sp.AgregarParametro("esu_codemp", codemp);
                sp.AgregarParametro("esu_sucid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
