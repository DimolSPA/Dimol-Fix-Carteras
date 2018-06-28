using Dimol.dao;
using Dimol.dto;
using Dimol.Email.dto;
using Dimol.Email.dto.MailModels;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dimol.Email.dao
{
    public class EnvioEmail
    {
        public static List<int> ListarCarteraEmail(int codemp, int pclid, string estid, int tipoCartera, int gestor, int sucid, string listaGestores)
        {
            List<int> lst = new List<int>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Cartera_Email");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("tipo_cartera", tipoCartera);
                sp.AgregarParametro("gestor", gestor);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("lista_gestores", listaGestores);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(Int32.Parse(ds.Tables[0].Rows[i]["ccb_ctcid"].ToString()));
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Email.dao.ListarCarteraEmail", pclid);
                return lst;
            }
        }

        public static List<dto.DatosDeudor> ListarContactosEmailDeudor(int codemp, int ctcid, int todo, int contacto)
        {
            List<dto.DatosDeudor> lst = new List<dto.DatosDeudor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Cartera_Email_Contactos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("todo", todo);
                sp.AgregarParametro("contacto", contacto);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DatosDeudor()
                        {
                            Ctcid = ctcid,
                            Mail = ds.Tables[0].Rows[i]["ddm_mail"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["ctc_nombre"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString(),
                            Rut = ds.Tables[0].Rows[i]["ctc_rut"].ToString(),
                            TipoContacto = ds.Tables[0].Rows[i]["Contacto"].ToString(),
                            TipoEmail = ds.Tables[0].Rows[i]["ddm_tipo"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Email.dao.ListarContactosEmailDeudor", ctcid);
                return lst;
            }
        }

        public static int InsertarCarteraClientesEstadosAcciones(int codemp, int pclid, int ctcid, int codsuc, string contacto, string ipRed, string ipMaquina, int usuario)
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
                spAc.AgregarParametro("cea_gesid", DBNull.Value);
                spAc.AgregarParametro("cea_contacto", contacto);
                spAc.AgregarParametro("cea_ipred", ipRed);
                spAc.AgregarParametro("cea_ipmaquina", ipMaquina);
                spAc.AgregarParametro("cea_comentario", "EMAIL");
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

        public static int ReversarCarteraClientesEstadosAcciones(int codemp, int pclid, int ctcid)
        {
            Funciones func = new Funciones();
            try
            {
                StoredProcedure spAc = new StoredProcedure("_Delete_Cartera_Clientes_Estados_Acciones");
                spAc.AgregarParametro("codemp", codemp);
                spAc.AgregarParametro("pclid", pclid);
                spAc.AgregarParametro("ctcid", ctcid);
                spAc.AgregarParametro("accid", func.ConfiguracionEmpNum(codemp, 65));
                int error = spAc.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.ReversarCarteraClientesEstadosAcciones: " + pclid, ctcid);
                throw ex;
            }

        }


        public static dto.Gestor TraeGestor(int codemp, int ctcid, int codsuc, int pclid)
        {
            dto.Gestor obj = new dto.Gestor();
            Dimol.dao.Funciones func = new Funciones();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Cartera_Email_Gestor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("pclid", pclid);
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
                            Password = func.ConfiguracionEmpStr(codemp, 63)
                        };
                    }
                    else
                    {
                        obj = new Gestor
                        {
                            Email = func.ConfiguracionEmpStr(codemp, 61),
                            Nombre = "",
                            Telefono = "",
                            Password = func.ConfiguracionEmpStr(codemp, 62)
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

        public static IEnumerable<EmailTemplate> ListarEmailTemplatesByClient(int pclid)
        {
            var Templates = new List<EmailTemplate>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Email_Templates_Cliente");
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Templates.Add(new EmailTemplate()
                        {
                            TemplateId = int.Parse(ds.Tables[0].Rows[i]["TEMPLATEID"].ToString()),
                            Cliente = int.Parse(ds.Tables[0].Rows[i]["CLIENTE"].ToString()),
                            Alias = ds.Tables[0].Rows[i]["ALIAS"].ToString(),
                            Filename = ds.Tables[0].Rows[i]["FILENAME"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Reportes.bcp.Cartera.ListarEmailTemplatesByClient: " + pclid, 0);
                throw ex; ;
            }
            return Templates;
        }

        public static IEnumerable<Combobox> GetEstadosByCliente(int Codemp, int Pclid, int Idioma, int Inicio, int Limite)
        {
            List<Combobox> Estados = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Email_Cliente_Grilla");
                sp.AgregarParametro("codemp", Codemp);
                sp.AgregarParametro("pclid", Pclid);
                sp.AgregarParametro("idid", Idioma);
                sp.AgregarParametro("inicio", Inicio);
                sp.AgregarParametro("limite", Limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Estados.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["ID"].ToString(),
                            Text = ds.Tables[0].Rows[i]["ESTADO"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarEstados", 0);
            }
            return Estados;
        }

        public static List<EmailBody> ListarCarteraEmailMasivo(BuscarCarteraMasivaModel Model)
        {
            List<EmailBody> Result = new List<EmailBody>();
            Funciones func = new Funciones();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_BuscarCarteraEmailMasivo");
                sp.AgregarParametro("EstadosIn", Model.Estados);
                sp.AgregarParametro("Codemp", Model.Codemp);
                sp.AgregarParametro("Pclid", Model.Pclid);
                sp.AgregarParametro("TipoCartera", Model.TipoCartera);
                if (Model.FechaVencimiento == null || Model.FechaVencimiento.Value.Year == 1)
                { //Pasar nula la fecha si no se selccionó nada
                    sp.AgregarParametro("FechaOperador", DBNull.Value);
                    sp.AgregarParametro("FechaVencimiento", DBNull.Value);
                }
                else
                {
                    sp.AgregarParametro("FechaOperador", Model.FechaTipo);
                    sp.AgregarParametro("FechaVencimiento", Model.FechaVencimiento);
                }
                if (String.IsNullOrEmpty(Model.Gestores))
                {
                    sp.AgregarParametro("GestoresIn", DBNull.Value);
                }
                else
                {
                    sp.AgregarParametro("GestoresIn", Model.Gestores);
                }
                sp.AgregarParametro("MontoDesde", Model.MontoDesde);
                sp.AgregarParametro("MontoHasta", Model.MontoHasta);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Gestor Gestor = new Gestor();
                        if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["GESTOR_EMAIL"].ToString()))
                        {
                            Gestor.Email = ds.Tables[0].Rows[i]["GESTOR_EMAIL"].ToString();
                            Gestor.Nombre = ds.Tables[0].Rows[i]["GESTOR"].ToString();
                            Gestor.Telefono = ds.Tables[0].Rows[i]["GESTOR_TELEFONO"].ToString();
                            Gestor.Password = func.ConfiguracionEmpStr(Model.Codemp, 63);
                        }
                        else
                        {
                            Gestor.Email = func.ConfiguracionEmpStr(Model.Codemp, 61);
                            Gestor.Nombre = "";
                            Gestor.Telefono = "";
                            Gestor.Password = func.ConfiguracionEmpStr(Model.Codemp, 62);
                        }
                            Result.Add(new EmailBody()
                        {
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["CCB_CTCID"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["RUT"].ToString(),
                            NombreDeudor = ds.Tables[0].Rows[i]["DEUDOR"].ToString(),
                            Contactos = Int32.Parse(ds.Tables[0].Rows[i]["CONTACTOS"].ToString()),
                            DatosGestor = Gestor,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarCarteraEmailMasivo", 0);
            }
            return Result;
        }


        public static IEnumerable<LiquidacionModel> GetLiquidaciones()
        {
            List<LiquidacionModel> Estados = new List<LiquidacionModel>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Liquidaciones_Email_Masivo");
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Estados.Add(new LiquidacionModel()
                        {
                            Id = int.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarEstados", 0);
            }
            return Estados;
        }

    }
}
