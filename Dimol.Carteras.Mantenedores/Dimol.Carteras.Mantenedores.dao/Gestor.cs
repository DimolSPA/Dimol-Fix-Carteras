using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dao
{
    public class Gestor
    {
       
        public string ListarTipoCartera(int idioma)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_TipoCartera");
                sp.AgregarParametro("idioma", idioma);
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

        public string ListarGrupos(int codemp,int sucursal)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_GrupoCobranza");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucursal", sucursal);
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

        public string ListarEmpleados(int codemp)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Empleados");
                sp.AgregarParametro("codemp", codemp);
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

        public List<dto.Gestor> ListarGestorGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int codsucursal)
        {
            List<dto.Gestor> lstEstadoCartera = new List<dto.Gestor>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("sucursal", codsucursal);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstEstadoCartera.Add(new dto.Gestor()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            CodSucursal = Int16.Parse(ds.Tables[0].Rows[i]["CODSUC"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            Telefono = ds.Tables[0].Rows[i]["TELEFONO"].ToString(),
                            Email = ds.Tables[0].Rows[i]["EMAIL"].ToString(),
                            IdTipoCartera = Int16.Parse(ds.Tables[0].Rows[i]["TIPOCARTID"].ToString()),
                            TipoCartera =ds.Tables[0].Rows[i]["TIPOCART"].ToString(),
                            ComKi = ds.Tables[0].Rows[i]["COMKI"].ToString(),
                            ComHon = ds.Tables[0].Rows[i]["COMHON"].ToString(),
                            //IdEmpleado = Int16.Parse(ds.Tables[0].Rows[i]["IDEMPLEADO"].ToString()),
                            IdEmpleado = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["IDEMPLEADO"].ToString()) ? 0 : Int16.Parse(ds.Tables[0].Rows[i]["IDEMPLEADO"].ToString()),
                            Empleado=ds.Tables[0].Rows[i]["EMPLEADO"].ToString(),
                            //Remoto = ds.Tables[0].Rows[i]["REMOTO"].ToString(),
                            Remoto = ds.Tables[0].Rows[i]["REMOTO"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            //Estado = ds.Tables[0].Rows[i]["ESTADO"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["ESTADO"].ToString().ToUpper() == "A" ? "ON" : "OFF",
                            ComJKi = ds.Tables[0].Rows[i]["COMJKI"].ToString(),
                            ComJHon =ds.Tables[0].Rows[i]["COMHJON"].ToString(),
                            IdGrupo = Int16.Parse(ds.Tables[0].Rows[i]["GRUPOID"].ToString()),
                            Grupo = ds.Tables[0].Rows[i]["GRUPO"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEstadoCartera;
        }

        public List<dto.Gestor> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int codsucursal)
        {
            List<dto.Gestor> lstEstadoCartera = new List<dto.Gestor>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("sucursal", codsucursal);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstEstadoCartera.Add(new dto.Gestor()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            CodSucursal = Int16.Parse(ds.Tables[0].Rows[i]["CODSUC"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            Telefono = ds.Tables[0].Rows[i]["TELEFONO"].ToString(),
                            Email = ds.Tables[0].Rows[i]["EMAIL"].ToString(),
                            IdTipoCartera = Int16.Parse(ds.Tables[0].Rows[i]["TIPOCARTID"].ToString()),
                            TipoCartera = ds.Tables[0].Rows[i]["TIPOCART"].ToString(),
                            ComKi = ds.Tables[0].Rows[i]["COMKI"].ToString(),
                            ComHon = ds.Tables[0].Rows[i]["COMHON"].ToString(),
                            //IdEmpleado = Int16.Parse(ds.Tables[0].Rows[i]["IDEMPLEADO"].ToString()),
                            IdEmpleado = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["IDEMPLEADO"].ToString()) ? 0 : Int16.Parse(ds.Tables[0].Rows[i]["IDEMPLEADO"].ToString()),
                            Remoto = ds.Tables[0].Rows[i]["REMOTO"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["ESTADO"].ToString(),
                            ComJKi = ds.Tables[0].Rows[i]["COMJKI"].ToString(),
                            ComJHon = ds.Tables[0].Rows[i]["COMHJON"].ToString(),
                            IdGrupo = Int16.Parse(ds.Tables[0].Rows[i]["GRUPOID"].ToString()),
                            Grupo = ds.Tables[0].Rows[i]["GRUPO"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEstadoCartera;
        }

        public static int ListarGestorGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int codsucursal)
        {
            int count = 0;
            List<dto.Gestor> lstEstadoCartera = new List<dto.Gestor>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("sucursal", codsucursal);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables.Count > 0)
                    {
                        count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return count;
        }

        public void InsertarGestor(dto.Gestor objAccion, int codemp, int idsuc)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Gestor");
                sp.AgregarParametro("ges_codemp", (object)codemp ?? DBNull.Value);
                sp.AgregarParametro("ges_sucid", (object)idsuc ?? DBNull.Value);
                sp.AgregarParametro("ges_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("ges_telefono", (object)objAccion.Telefono ?? DBNull.Value);
                sp.AgregarParametro("ges_email", (object)objAccion.Email.ToLower() ?? DBNull.Value);
                sp.AgregarParametro("ges_tipcart", (object)objAccion.TipoCartera ?? DBNull.Value);
                sp.AgregarParametro("ges_comki", string.IsNullOrEmpty(objAccion.ComKi) ? "0" : objAccion.ComKi.Replace(",", "."));
                sp.AgregarParametro("ges_comhon", string.IsNullOrEmpty(objAccion.ComHon) ? "0" : objAccion.ComHon.Replace(",", "."));
                sp.AgregarParametro("ges_emplid", (object)objAccion.Empleado ?? DBNull.Value);
                sp.AgregarParametro("ges_remoto", objAccion.Remoto.ToUpper() == "ON" || objAccion.Remoto.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("ges_comjki", string.IsNullOrEmpty(objAccion.ComJKi) ? "0" : objAccion.ComJKi.Replace(",", "."));
                sp.AgregarParametro("ges_comjhon", string.IsNullOrEmpty(objAccion.ComJHon) ? "0" : objAccion.ComJHon.Replace(",", "."));
                sp.AgregarParametro("ges_grupoid", (object)objAccion.Grupo ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarGestor(dto.Gestor objAccion, int codemp, int idsuc)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Gestor");
                sp.AgregarParametro("ges_codemp", (object)codemp ?? DBNull.Value);
                sp.AgregarParametro("ges_sucid", (object)idsuc ?? DBNull.Value);
                sp.AgregarParametro("ges_gesid", (object)objAccion.Id ?? DBNull.Value);
                sp.AgregarParametro("ges_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("ges_telefono", (object)objAccion.Telefono ?? DBNull.Value);
                sp.AgregarParametro("ges_email", (object)objAccion.Email.ToLower() ?? DBNull.Value);
                sp.AgregarParametro("ges_tipcart", (object)objAccion.TipoCartera ?? DBNull.Value);
                sp.AgregarParametro("ges_comki",string.IsNullOrEmpty(objAccion.ComKi) ? "0" :objAccion.ComKi.Replace(",", "."));
                sp.AgregarParametro("ges_comhon",string.IsNullOrEmpty(objAccion.ComHon) ? "0" :objAccion.ComHon.Replace(",", "."));
                sp.AgregarParametro("ges_emplid", (object)objAccion.Empleado ?? DBNull.Value);
                sp.AgregarParametro("ges_remoto", objAccion.Remoto.ToUpper() == "ON" || objAccion.Remoto.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("ges_estado", objAccion.Estado.ToUpper() == "ON" || objAccion.Estado.ToUpper() == "YES" ? "A" : "N");
                sp.AgregarParametro("ges_comjki", string.IsNullOrEmpty(objAccion.ComJKi) ? "0" : objAccion.ComJKi.Replace(",", "."));
                sp.AgregarParametro("ges_comjhon", string.IsNullOrEmpty(objAccion.ComJHon) ? "0" : objAccion.ComJHon.Replace(",", "."));
                sp.AgregarParametro("ges_grupoid", (object)objAccion.Grupo ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarGestor(int codemp, int? id, int idsuc)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Delete_Gestor");
                sp.AgregarParametro("ges_codemp", codemp);
                sp.AgregarParametro("ges_sucid", idsuc);
                sp.AgregarParametro("ges_gesid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
