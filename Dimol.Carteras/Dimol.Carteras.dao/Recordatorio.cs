using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dao
{
    public class Recordatorio
    {
        public static int GrabarSMS(dto.Recordatorio obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Pre_Insertar_SMS_Deudores");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("numero", obj.Telefono);
                sp.AgregarParametro("fecha_envio", obj.FechaEnvio);
                sp.AgregarParametro("usuario", obj.Usrid);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Recordatorio.GrabarSMS", obj.Usrid );
                throw ex;
            }
        }

        public static int GrabarEmail(dto.Recordatorio obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Pre_Insertar_Email_Deudores");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("email", obj.Email);
                sp.AgregarParametro("fecha_envio", obj.FechaEnvio);
                sp.AgregarParametro("usuario", obj.Usrid);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Recordatorio.GrabarEmail", obj.Usrid);
                throw ex;
            }
        }

        public static int EliminarSMS(dto.Recordatorio obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Pre_Eliminar_SMS_Deudores");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("numero", obj.Telefono);
                sp.AgregarParametro("usuario", obj.Usrid);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Recordatorio.EliminarSMS", obj.Usrid);
                throw ex;
            }
        }

        public static int EliminarEmail(dto.Recordatorio obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Pre_Eliminar_Email_Deudores");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("email", obj.Email);
                sp.AgregarParametro("usuario", obj.Usrid);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Recordatorio.EliminarEmail", obj.Usrid);
                throw ex;
            }
        }

        public static List<dto.Recordatorio> ListarSMSPreDeudor(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Recordatorio> lst = new List<dto.Recordatorio>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Pre_SMS_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new dto.Recordatorio()
                        {
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Telefono = ds.Tables[0].Rows[i]["Telefono"].ToString(),
                            FechaEnvio = DateTime.Parse(  ds.Tables[0].Rows[i]["FechaEnvio"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaModificacion = DateTime.Parse(  ds.Tables[0].Rows[i]["FechaModificacion"].ToString()),
                            UsuarioModificacion = ds.Tables[0].Rows[i]["UsuarioModificacion"].ToString()
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

        public static int ListarSMSPreDeudorCount(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Pre_SMS_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0; ;
            }
        }

        public static List<dto.Recordatorio> ListarEmailPreDeudor(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Recordatorio> lst = new List<dto.Recordatorio>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Pre_Email_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new dto.Recordatorio()
                        {
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Email = ds.Tables[0].Rows[i]["Email"].ToString(),
                            FechaEnvio = DateTime.Parse(ds.Tables[0].Rows[i]["FechaEnvio"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaModificacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaModificacion"].ToString()),
                            UsuarioModificacion = ds.Tables[0].Rows[i]["UsuarioModificacion"].ToString()
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

        public static int ListarEmailPreDeudorCount(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Pre_Email_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0; ;
            }
        }
    }
}
