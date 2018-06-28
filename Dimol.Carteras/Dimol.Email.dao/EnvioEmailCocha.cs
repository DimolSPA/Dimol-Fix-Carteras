using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;
using System.Data;
using Dimol.dao;
using Dimol.Email.dto;

namespace Dimol.Email.dao
{
    public class EnvioEmailCocha
    {
        public static dto.CochaCpbt TraeDocumento(string[] lstDocs)
        {
            dto.CochaCpbt doc = new CochaCpbt();
            
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Cartera_Documento_Cpbt_Json");
                sp.AgregarParametro("ccb_pclid", Int32.Parse(lstDocs[0]));
                sp.AgregarParametro("ccb_ctcid", Int32.Parse(lstDocs[1]));
                sp.AgregarParametro("ccb_ccbid", Int32.Parse(lstDocs[2]));
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    doc.Numero = ds.Tables[0].Rows[0]["ccb_numero"].ToString();
                    doc.TipoMoneda = ds.Tables[0].Rows[0]["ccb_codmon"].ToString(); 
                    doc.Saldo = decimal.Parse(ds.Tables[0].Rows[0]["ccb_saldo"].ToString());
                    doc.CodigoCarga = Int32.Parse(ds.Tables[0].Rows[0]["ccb_codid"].ToString());                 
                }

                return doc;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Email.dao.EnvioEmailCocha", Int32.Parse(lstDocs[0]));
                return doc;
            }
        }

        public static string TraeNombreCliente(int codemp, int pclid)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Nombre_Cliente");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        salida = ds.Tables[0].Rows[0]["PCL_NOMFANT"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return salida;
        }

        public static string TraeNombreDeudor(int codemp, int ctcid)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Nombre_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        salida = ds.Tables[0].Rows[0]["CTC_NOMFANT"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return salida;
        }

        public static string TraeRutDeudor(int codemp, int ctcid)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Rut_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        salida = ds.Tables[0].Rows[0]["RUT"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return salida;
        }

        public static List<Combobox> ListarTipoReporte(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipo_Mail");
               
                ds = sp.EjecutarProcedimiento();
                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Descripcion"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id_Email"].ToString()
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

        public static string ListarMonedas(int codemp)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Monedas");
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

        public static dto.EmailBodyCocha TraeMailCocha(int idMail, int idTemplate)
        {
            dto.EmailBodyCocha obj = new dto.EmailBodyCocha();
            
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Mail_Cocha");
                sp.AgregarParametro("idmail", idMail);
                sp.AgregarParametro("idtemplate", idTemplate);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {

                        obj = new EmailBodyCocha
                        {
                            Header = ds.Tables[0].Rows[0]["Header"].ToString(),
                            Subject = ds.Tables[0].Rows[0]["Asunto"].ToString(),
                            Body = ds.Tables[0].Rows[0]["Body"].ToString()
                        };
                    
                   
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static int InsertarCarteraClientesEstadosAcciones(int codemp, int pclid, int ctcid, int accion, int codsuc, int gestor, string ipRed, string ipMaquina, string comentario, int usuario)
        {            
            try
            {
                StoredProcedure spAc = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Acciones");
                spAc.AgregarParametro("cea_codemp", codemp);
                spAc.AgregarParametro("cea_pclid", pclid);
                spAc.AgregarParametro("cea_ctcid", ctcid);
                spAc.AgregarParametro("cea_accid", accion);
                spAc.AgregarParametro("cea_sucid", codsuc);
                spAc.AgregarParametro("cea_gesid", gestor == 0 ? DBNull.Value : (object)gestor);
                spAc.AgregarParametro("cea_contacto", "N");
                spAc.AgregarParametro("cea_ipred", ipRed);
                spAc.AgregarParametro("cea_ipmaquina", ipMaquina);
                spAc.AgregarParametro("cea_comentario", string.IsNullOrEmpty(comentario) ? " " : comentario);
                spAc.AgregarParametro("cea_estado", "S");
                spAc.AgregarParametro("cea_usrid", usuario);
                spAc.AgregarParametro("cea_ddcid", DBNull.Value);
                spAc.AgregarParametro("cea_telefono", DBNull.Value);

                int error = spAc.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.EnvioEmailCocha.InsertarCarteraClientesEstadosAcciones", usuario);
                throw ex;
            }

        }

        public static int InsertarCarteraClientesEstadosHistorialEspecial(int codemp, int pclid, int ctcid, int ccbid, DateTime fecha, int estid, int codsuc, int gesid, string ipRed, string ipMaquina, string comentario, decimal monto, decimal saldo, int usuario)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial_Especial");
                sp.AgregarParametro("ceh_codemp", codemp);
                sp.AgregarParametro("ceh_pclid", pclid);
                sp.AgregarParametro("ceh_ctcid", ctcid);
                sp.AgregarParametro("ceh_ccbid", ccbid);
                sp.AgregarParametro("ceh_fecha", fecha);
                sp.AgregarParametro("ceh_estid", estid);
                sp.AgregarParametro("ceh_sucid", codsuc);
                sp.AgregarParametro("ceh_gesid", gesid == 0 ? DBNull.Value : (object)gesid);
                sp.AgregarParametro("ceh_ipred", ipRed);
                sp.AgregarParametro("ceh_ipmaquina", ipMaquina);
                sp.AgregarParametro("ceh_comentario", string.IsNullOrEmpty(comentario) ? " " : comentario);
                sp.AgregarParametro("ceh_monto", monto);
                sp.AgregarParametro("ceh_saldo", saldo);
                sp.AgregarParametro("ceh_usrid", usuario);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.InsertarCarteraClientesEstadosHistorialEspecial", usuario);
                throw ex;
            }

        }

        public static int InsertarCarteraClientesEstadosAcciones(int codemp, int pclid, int ctcid, int gesid, int codsuc, string contacto, string ipRed, string ipMaquina, int usuario, string comentario)
        {
            Funciones func = new Funciones();
            try
            {
                StoredProcedure spAc = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Acciones");
                spAc.AgregarParametro("cea_codemp", codemp);
                spAc.AgregarParametro("cea_pclid", pclid);
                spAc.AgregarParametro("cea_ctcid", ctcid);
                spAc.AgregarParametro("cea_accid", func.ConfiguracionEmpNum(codemp, 65));
                spAc.AgregarParametro("cea_sucid", codsuc);
                spAc.AgregarParametro("cea_gesid", gesid == 0 ? DBNull.Value : (object)gesid);
                spAc.AgregarParametro("cea_contacto", contacto);
                spAc.AgregarParametro("cea_ipred", ipRed);
                spAc.AgregarParametro("cea_ipmaquina", ipMaquina);
                spAc.AgregarParametro("cea_comentario", comentario);
                spAc.AgregarParametro("cea_estado", "S");
                spAc.AgregarParametro("cea_usrid", usuario);
                spAc.AgregarParametro("cea_ddcid", DBNull.Value);
                spAc.AgregarParametro("cea_telefono", DBNull.Value);

                int error = spAc.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.InsertarCarteraClientesEstadosAcciones", usuario);
                throw ex;
            }

        }

        public static dto.Gestor TraeGestor(string mail)
        {
            dto.Gestor obj = new dto.Gestor();
            Dimol.dao.Funciones func = new Funciones();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Datos_Gestor");
                sp.AgregarParametro("mail", mail);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj = new Gestor
                        {
                            Email = ds.Tables[0].Rows[0]["Email"].ToString(),
                            Nombre = ds.Tables[0].Rows[0]["Nombre"].ToString(),
                            Telefono = ds.Tables[0].Rows[0]["Telefono"].ToString(),
                            Password = func.ConfiguracionEmpStr(1, 63)
                        };
                    }
                    else
                    {
                        obj = new Gestor
                        {
                            Email = func.ConfiguracionEmpStr(1, 61),
                            Nombre = "",
                            Telefono = "",
                            Password = func.ConfiguracionEmpStr(1, 62)
                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

    }
}
