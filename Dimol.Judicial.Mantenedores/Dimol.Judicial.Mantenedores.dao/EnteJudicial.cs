using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class EnteJudicial
    {
        public string ListarProvcli(int codemp, int idioma)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_provcli");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();

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

        public string ListarEmpleados(int codemp, int idioma)
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
                    //salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();

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

        public List<dto.EnteJudicial> ListarEnteJudicialGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EnteJudicial> lstEnteJudicial = new List<dto.EnteJudicial>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_EnteJudicial_Grilla");
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
                        lstEnteJudicial.Add(new dto.EnteJudicial()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            IdCliente = Int16.Parse(ds.Tables[0].Rows[i]["IdCliente"].ToString()),
                            IdEmpleado = Int16.Parse(ds.Tables[0].Rows[i]["IdEmpleado"].ToString()),
                            Sindico = ds.Tables[0].Rows[i]["Sindico"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Abogado = ds.Tables[0].Rows[i]["Abogado"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Procurador = ds.Tables[0].Rows[i]["Procurador"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Receptor = ds.Tables[0].Rows[i]["Receptor"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            NombreEmpleado = ds.Tables[0].Rows[i]["NombreEmpleado"].ToString(),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEnteJudicial;
        }

        public List<dto.EnteJudicial> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EnteJudicial> lstEnteJudicial = new List<dto.EnteJudicial>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_EnteJudicial_Grilla");
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
                        lstEnteJudicial.Add(new dto.EnteJudicial()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            IdCliente = Int16.Parse(ds.Tables[0].Rows[i]["IdCliente"].ToString()),
                            IdEmpleado = Int16.Parse(ds.Tables[0].Rows[i]["IdEmpleado"].ToString()),
                            Sindico = ds.Tables[0].Rows[i]["Sindico"].ToString().ToUpper(),
                            Abogado = ds.Tables[0].Rows[i]["Abogado"].ToString().ToUpper(),
                            Procurador = ds.Tables[0].Rows[i]["Procurador"].ToString().ToUpper() ,
                            Receptor = ds.Tables[0].Rows[i]["Receptor"].ToString().ToUpper(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            NombreEmpleado = ds.Tables[0].Rows[i]["NombreEmpleado"].ToString(),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEnteJudicial;
        }


        public static int ListarEnteJudicialGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_EnteJudicial_Grilla_Count");
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

        public void InsertarEnteJudicial(dto.EnteJudicial objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Entes_Judicial");
                sp.AgregarParametro("etj_codemp", codemp);
                sp.AgregarParametro("etj_pclid", objAccion.Nombre != "0" ? (object)objAccion.Nombre : DBNull.Value);
                sp.AgregarParametro("etj_emplid", objAccion.NombreEmpleado != "0" ? (object)objAccion.NombreEmpleado : DBNull.Value);
                sp.AgregarParametro("etj_sindico", objAccion.Sindico.ToUpper() == "ON" || objAccion.Sindico.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("etj_abogado", objAccion.Abogado.ToUpper() == "ON" || objAccion.Abogado.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("etj_procurador", objAccion.Procurador.ToUpper() == "ON" || objAccion.Procurador.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("etj_receptor", objAccion.Receptor.ToUpper() == "ON" || objAccion.Receptor.ToUpper() == "YES" ? "S" : "N");
                
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarEnteJudicial(dto.EnteJudicial objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Entes_Judicial");
                sp.AgregarParametro("etj_codemp", codemp);
                sp.AgregarParametro("etj_etjid", objAccion.Id);
                sp.AgregarParametro("etj_pclid", objAccion.Nombre != "0" ? (object)objAccion.Nombre : DBNull.Value);
                sp.AgregarParametro("etj_emplid", objAccion.NombreEmpleado != "0" ? (object)objAccion.NombreEmpleado : DBNull.Value);
                sp.AgregarParametro("etj_sindico", objAccion.Sindico.ToUpper() == "ON" || objAccion.Sindico.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("etj_abogado", objAccion.Abogado.ToUpper() == "ON" || objAccion.Abogado.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("etj_procurador", objAccion.Procurador.ToUpper() == "ON" || objAccion.Procurador.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("etj_receptor", objAccion.Receptor.ToUpper() == "ON" || objAccion.Receptor.ToUpper() == "YES" ? "S" : "N");
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarEnteJudicial(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Entes_Judicial");
                sp.AgregarParametro("etj_codemp", codemp);
                sp.AgregarParametro("etj_etjid", id);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region "Rol Entes"
        public static List<dto.EnteJudicial> ListarRolEnteJudicialGrilla(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EnteJudicial> lstEnteJudicial = new List<dto.EnteJudicial>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Entes_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
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
                        lstEnteJudicial.Add(new dto.EnteJudicial()
                        {
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Etjid"].ToString()),
                            Sindico = ds.Tables[0].Rows[i]["Sindico"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Abogado = ds.Tables[0].Rows[i]["Abogado"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Procurador = ds.Tables[0].Rows[i]["Procurador"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Receptor = ds.Tables[0].Rows[i]["Receptor"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEnteJudicial;
        }

        public static int ListarRolEnteJudicialGrillaCount(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Entes_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
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

        public static List<Combobox> ListarEntes(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Entes");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString(),
                            Value = ds.Tables[0].Rows[i]["etj_etjid"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static int InsertarEnteJudicialRol( int codemp, int etjid, int rolid)
        {
            int error = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_EnteJud_Rol");
                sp.AgregarParametro("ejr_codemp", codemp);
                sp.AgregarParametro("ejr_etjid",etjid);
                sp.AgregarParametro("ejr_rolid", rolid);

                error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return error;
            }
            return error;
        }

        public static int EliminarEnteJudicialRol(int codemp, int etjid, int rolid)
        {
            int error = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_EnteJud_Rol");
                sp.AgregarParametro("ejr_codemp", codemp);
                sp.AgregarParametro("ejr_etjid", etjid);
                sp.AgregarParametro("ejr_rolid", rolid);

                error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return error;
            }
            return error;
        }
        #endregion

        
    }
}
