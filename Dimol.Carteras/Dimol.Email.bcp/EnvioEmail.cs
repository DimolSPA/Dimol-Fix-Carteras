using Dimol.dao;
using Dimol.dto;
using Dimol.Email.bcp.Services;
using Dimol.Email.dto;
using Dimol.Email.dto.MailModels;
using Dimol.Reportes.bcp;
using Dimol.Reportes.dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Dimol.Email.bcp
{
    public class EnvioEmail
    {

        public static List<int> ListarCarteraEmail(int codemp, int pclid, string estid, int tipoCartera, int gestor, int sucid, string listaGestores)
        {
            return dao.EnvioEmail.ListarCarteraEmail(codemp, pclid, estid, tipoCartera, gestor, sucid, listaGestores);
        }

        public static List<dto.DatosDeudor> ListarContactosEmailDeudor(int codemp, int ctcid, int todo, int contacto)
        {
            return dao.EnvioEmail.ListarContactosEmailDeudor(codemp, ctcid, todo, contacto);
        }

        public static bool Enviar(int pclid, string lstEstados, int tipoCartera, string lstGestores, bool todos, bool contacto, UserSession objSession, string path, string emailDestino)
        {
            Dimol.dao.Funciones.InsertarError("Inicio correo masivo: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
            int exitoInsertarHistorial = 0;
            int cantidadCorreo = 0;
            bool error = false;
            Dimol.dao.Funciones objFunc = new Dimol.dao.Funciones();
            List<dto.EmailBody> lstFull = new List<dto.EmailBody>();
            List<dto.EmailBody> lstFullOrdenada = new List<dto.EmailBody>();
            List<Dimol.Email.dto.DatosDeudor> lstDatosDeudor = new List<dto.DatosDeudor>();
            Dimol.dao.Funciones.InsertarError("Buscando deudores: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
            Dimol.dao.Funciones.InsertarError("Dimol.Email.dao.EnvioEmail.ListarCarteraEmail(" + objSession.CodigoEmpresa + "," + pclid + ", " + lstEstados + "," + tipoCartera + "," + objSession.Gestor + "," + objSession.CodigoSucursal + "," + lstGestores + ")", "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
            List<int> lstDeudores = Dimol.Email.dao.EnvioEmail.ListarCarteraEmail(objSession.CodigoEmpresa, pclid, lstEstados, tipoCartera, objSession.Gestor, objSession.CodigoSucursal, lstGestores);
            Dimol.dao.Funciones.InsertarError("Cargando datos contacto deudores: Deudores: " + lstDeudores.Count + ", Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
            for (int i = 0; i < lstDeudores.Count; i++)
            {
                lstFull.Add(new dto.EmailBody
                {
                    Ctcid = lstDeudores[i],
                    NombreEmpresa = objSession.NombreEmpresa,
                    MensajePersonalizado = null,
                    Pclid = pclid,
                    TipoCartera = tipoCartera,
                    NombreCliente = Dimol.Email.dao.EnvioEmail.TraeNombreCliente(objSession.CodigoEmpresa, pclid),
                    ListaDatosDeudor = Dimol.Email.dao.EnvioEmail.ListarContactosEmailDeudor(objSession.CodigoEmpresa, lstDeudores[i], todos ? 1 : 0, contacto ? 1 : 0),
                    DatosGestor = Dimol.Email.dao.EnvioEmail.TraeGestor(objSession.CodigoEmpresa, lstDeudores[i], objSession.CodigoSucursal, pclid)
                });
                //lstDatosDeudor.AddRange(Dimol.Email.dao.EnvioEmail.ListarContactosEmailDeudor(objSession.CodigoEmpresa, lstDeudores[i], todos ? 1 : 0, contacto ? 1 : 0));

            }

            lstFull.Sort((x, y) => string.Compare(x.DatosGestor.Nombre, y.DatosGestor.Nombre));
            var distintosGestores = lstFull.GroupBy(test => test.DatosGestor.Nombre)
                                               .Select(grp => grp.First())
                                               .ToList();
            foreach (dto.EmailBody b in distintosGestores)
            {
                lstFullOrdenada.AddRange(lstFull.Where(x => x.DatosGestor.Nombre == b.DatosGestor.Nombre).ToList().Take(150));
                var setToRemove = new HashSet<dto.EmailBody>(lstFullOrdenada);
                lstFull.RemoveAll(x => setToRemove.Contains(x));
            }

            lstFullOrdenada.AddRange(lstFull);
            Dimol.dao.Funciones.InsertarError("Comienzo proceso: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
            foreach (dto.EmailBody datos in lstFullOrdenada)
            {

                exitoInsertarHistorial = 0;
                //buscar cartera?
                string tipoContacto = "S";
                if (datos.ListaDatosDeudor.Count > 0)
                {
                    tipoContacto = datos.ListaDatosDeudor[0].TipoContacto;
                    try
                    {
                        if (string.IsNullOrEmpty(emailDestino))
                        {
                            exitoInsertarHistorial = dao.EnvioEmail.InsertarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, pclid, datos.Ctcid, objSession.CodigoSucursal, tipoContacto, objSession.IpRed, objSession.IpPc, objSession.UserId);
                        }
                        else
                        {
                            exitoInsertarHistorial = 1;
                        }
                        if (exitoInsertarHistorial > 0)
                        {
                            Liquidacion obj = new Liquidacion();
                            obj.Codemp = objSession.CodigoEmpresa;
                            obj.Pclid = pclid;
                            obj.Ctcid = datos.Ctcid;// 1202065;//7598;
                            obj.TipoCartera = tipoCartera;
                            obj.EstadoCpbt = "V";
                            obj.Idioma = objSession.Idioma;
                            obj.Sucid = objSession.CodigoSucursal;
                            obj.FechaReporte = DateTime.Now;
                            obj.NombreArchivo = "Liquidacion_Cliente_" + datos.ListaDatosDeudor[0].Rut;
                            obj.PathArchivo = path + obj.NombreArchivo + ".pdf";
                            obj.Pagina = 355;
                            obj.IdReporte = 3;
                            error = GeneraReporteCochaMutual(obj) ? false : true;

                            if (!error)
                            {
                                datos.PathReporte = obj.PathArchivo.Replace("\\\\", "\\");
                                Email emailSender = new Email();
                                dto.Email email = new dto.Email();
                                foreach (DatosDeudor deudor in datos.ListaDatosDeudor)
                                {
                                    email = new dto.Email();
                                    email.From = datos.DatosGestor.Email;
                                    email.Codemp = objSession.CodigoEmpresa;
                                    email.From = datos.DatosGestor.Nombre;//objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 61);
                                    email.Sender = datos.DatosGestor.Email;//
                                    //if (cantidadCorreo < 151)
                                    //{
                                    //    email.From = datos.DatosGestor.Nombre;//objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 61);
                                    //    email.Sender = datos.DatosGestor.Email;//
                                    //}
                                    //else if (cantidadCorreo >= 151 && cantidadCorreo < 301)
                                    //{
                                    //    email.Sender = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 131);
                                    //}
                                    //else if (cantidadCorreo >= 301 && cantidadCorreo < 451)
                                    //{
                                    //    email.Sender = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 132);
                                    //}
                                    //else if (cantidadCorreo >= 451)
                                    //{
                                    //    email.Sender = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 61);
                                    //    cantidadCorreo = 0;
                                    //}
                                    if (!string.IsNullOrEmpty(emailDestino))
                                    {
                                        email.To.Add(emailDestino);
                                    }
                                    else
                                    {
                                        email.To.Add(deudor.Mail);
                                    }
                                    email.Bcc.Add(objSession.Email);
                                    email.Subject = "Cuenta Corriente " + datos.NombreCliente;
                                    email.Body = datos.GenerarEmailBody(deudor);
                                    email.Html = true;
                                    System.Net.Mail.Attachment objAtt = new System.Net.Mail.Attachment(obj.PathArchivo);
                                    email.Attachments.Add(objAtt);
                                    error = emailSender.EnviarEmail(email) ? false : true;
                                    if (error)
                                    {
                                        Dimol.dao.Funciones.InsertarError("Correo no enviado, CTCID: " + datos.Ctcid + ", PCLID: " + datos.Pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
                                        dao.EnvioEmail.ReversarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, pclid, datos.Ctcid);
                                    }

                                    cantidadCorreo++;
                                }
                                System.IO.File.Delete(obj.PathArchivo + ".fo");
                            }
                        }
                        else
                        {
                            error = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmail.Enviar, CTCID: " + datos.Ctcid + ", PCLID: "
                            + datos.Pclid, objSession.UserId);
                    }
                }
                else
                {
                    Dimol.dao.Funciones.InsertarError("Deudor: " + datos.Ctcid + ", no tiene datos de contacto", "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
                }
            }
            Dimol.dao.Funciones.InsertarError("Fin proceso email masivo: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
            return error;
        }

        public static bool GeneraReporteCochaMutual(Liquidacion obj)
        {
            try
            {
                bool result = Cartera.TraeLiquidacionCochaMutual(obj);
                File.Delete(obj.PathArchivo + ".fo");
                return result;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmail.GeneraReporteCochaMutual", 0);
                return false;
            }
        }

        public static List<EmailBody> ListarCarteraEmailMasivo(BuscarCarteraMasivaModel Model, UserSession objSession)
        {
            Dimol.dao.Funciones.InsertarError("Buscando deudores", null, null, 0);
            List<EmailBody> lstFull = new List<EmailBody>();
            List<EmailBody> lstFullOrdenada = new List<EmailBody>();
            List<EmailBody> Deudores = dao.EnvioEmail.ListarCarteraEmailMasivo(Model);
            foreach (var d in Deudores)
            {
                lstFull.Add(new EmailBody
                {
                    Ctcid = d.Ctcid,
                    NombreEmpresa = objSession.NombreEmpresa,
                    MensajePersonalizado = null,
                    Pclid = Model.Pclid,
                    Rut = d.Rut,
                    Contactos = d.Contactos,
                    TipoCartera = Model.TipoCartera,
                    NombreCliente = d.NombreCliente,
                    NombreDeudor = d.NombreDeudor,
                    ListaDatosDeudor = dao.EnvioEmail.ListarContactosEmailDeudor(objSession.CodigoEmpresa, d.Ctcid, 0, 1),
                    DatosGestor = new dto.Gestor()
                    {
                        Email = d.DatosGestor.Email,
                        Nombre = d.DatosGestor.Nombre,
                        Telefono = d.DatosGestor.Telefono,
                        Password = d.DatosGestor.Password
                    },
                });
            }

            lstFull.Sort((x, y) => string.Compare(x.DatosGestor.Nombre, y.DatosGestor.Nombre));
            var distintosGestores = lstFull
                .GroupBy(test => test.DatosGestor.Nombre)
                .Select(grp => grp.First())
                .ToList();

            foreach (EmailBody b in distintosGestores)
            {
                lstFullOrdenada.AddRange(lstFull.Where(x => x.DatosGestor.Nombre == b.DatosGestor.Nombre).ToList().Take(150));
                var setToRemove = new HashSet<EmailBody>(lstFullOrdenada);
                lstFull.RemoveAll(x => setToRemove.Contains(x));
            }

            lstFullOrdenada.AddRange(lstFull);
            Dimol.dao.Funciones.InsertarError("Comienzo proceso: Usuario" + objSession.Nombre + ", PCLID: " + Model.Pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
            //Pasar a lista de VM para el preview jqgrid
            return lstFullOrdenada;
        }

        /// <summary>
        /// Realizar envío masivo de emails "asincrono"
        /// </summary>
        /// <param name="Model">BuscarCarteraMasivaModel</param>
        /// <param name="objSession">UserSession</param>
        /// <param name="Test">bool</param>
        /// <returns>int</returns>
        public static async Task<int> EnvioMasivo(BuscarCarteraMasivaModel Model, UserSession objSession, bool Test)
        {
            int enviados = 0;
            List<string> Enviados = new List<string>();

            List<EmailBody> Targets = new List<EmailBody>();
            if (Test)
            {
                Targets = ListarCarteraEmailMasivo(Model, objSession)
                .Skip(0)
                .Take(10)
                .ToList();
            }
            else
            {
                Targets = ListarCarteraEmailMasivo(Model, objSession);
            }
            Funciones objFunc = new Funciones();
            var Host = objFunc.ConfiguracionEmpStr(Model.Codemp, 60);

            string Attachment = "";
            if (Targets.Count > 0)
            {
                int exitoInsertarHistorial = 0;
                string tipoContacto = "S";
                Funciones.InsertarError("Comienzo proceso: Usuario" + objSession.Nombre + ", PCLID: " + Model.Pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
                var Senders = GenerarSenders(Model);
                NetworkCredential SenderData = new NetworkCredential();
                foreach (EmailBody datos in Targets)
                {
                    //Definimos el indice de la cuenta desde la que va a salir el mail
                    if (enviados <= 179)
                    {
                        SenderData.Password = datos.DatosGestor.Password;
                        SenderData.UserName = datos.DatosGestor.Email;
                    }
                    else if (enviados > 180 && enviados <= 360)
                    {
                        SenderData.Password = Senders[0].Password;
                        SenderData.UserName = Senders[0].UserName;
                    }
                    else if (enviados > 361 && enviados <= 540)
                    {
                        SenderData.Password = Senders[1].Password;
                        SenderData.UserName = Senders[1].UserName;
                    }
                    else if (enviados > 541)
                    {
                        SenderData.Password = Senders[2].Password;
                        SenderData.UserName = Senders[2].UserName;
                    }

                    exitoInsertarHistorial = 0;
                    tipoContacto = "S";
                    if (datos.ListaDatosDeudor.Count > 0)
                    {
                        tipoContacto = datos.ListaDatosDeudor[0].TipoContacto;
                        try
                        {
                            if (!string.IsNullOrEmpty(datos.ListaDatosDeudor[0].Mail))
                            {
                                exitoInsertarHistorial = dao.EnvioEmail.InsertarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, Model.Pclid, datos.Ctcid, objSession.CodigoSucursal, tipoContacto, objSession.IpRed, objSession.IpPc, objSession.UserId);
                            }
                            if (exitoInsertarHistorial > 0)
                            {
                                IEnvioEmailService _service = new EnvioEmailService();
                                foreach (DatosDeudor deudor in datos.ListaDatosDeudor)
                                {
                                    var x = Enviados.Where(t => t == deudor.Mail).FirstOrDefault();
                                    if(x == null)
                                    {
                                        Enviados.Add(deudor.Mail);
                                        EmailDto EmailObj = new EmailDto()
                                        {
                                            From = datos.DatosGestor.Email,
                                            FromName = datos.DatosGestor.Nombre,
                                            ToName = deudor.Nombre,
                                            Password = SenderData.Password,
                                            Pclid = datos.Pclid,
                                            TemplateName = Model.Template,
                                            Host = Host,
                                            EnableSsl = false,
                                            DeliveryMethod = SmtpDeliveryMethod.Network,
                                            UseDefaultCredentials = false,
                                            Subject = "Mensaje de prueba debe ir a: " + deudor.Mail,
                                            IsBodyHtml = true,
                                            Sender = SenderData.UserName,
                                            Attachments = new List<string>()
                                        };

                                        if (!Test)
                                        {
                                            EmailObj.Bcc.Add(objSession.Email);
                                            EmailObj.To.Add(new MailAddress(deudor.Mail, deudor.Nombre));
                                        }
                                        else
                                        {
                                            EmailObj.To.Add(new MailAddress("jeanr.robles@gmail.com", "Jean Robles"));
                                            //EmailObj.To.Add(new MailAddress(datos.DatosGestor.Email, datos.DatosGestor.Nombre));
                                        }

                                        Attachment = GenerarLiquidaciones(Model, datos, objSession);

                                        if (!String.IsNullOrEmpty(Attachment) && Attachment != "")
                                            EmailObj.Attachments.Add(Attachment.Replace("\\\\", "\\"));

                                        enviados = await EnvioConTemplate(Model, objSession, enviados, datos, _service, deudor, EmailObj);
                                    }
                                    x = null;
                                }
                                if (!String.IsNullOrEmpty(Attachment) && Attachment != "")
                                    File.Delete(Attachment + ".fo");
                            }
                        }
                        catch (Exception ex)
                        {
                            Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmail.Enviar, CTCID: " + datos.Ctcid + ", PCLID: "
                                + datos.Pclid, objSession.UserId);
                        }
                    }
                }
            }
            return enviados;
        }

        /// <summary>
        /// Crear listado de cuentas para senders de envíos masivos
        /// </summary>
        /// <param name="Model">BuscarCarteraMasivaModel</param>
        /// <returns>List<NetworkCredential></returns>
        private static List<NetworkCredential> GenerarSenders(BuscarCarteraMasivaModel Model)
        {
            List<NetworkCredential> Addresses = new List<NetworkCredential>();
            Funciones objFunc = new Funciones();

            Addresses.Add(new NetworkCredential(
                objFunc.ConfiguracionEmpStr(Model.Codemp, 131), "wgVqKuuJ"));

            //Cuentas alternativas
            Addresses.Add(new NetworkCredential(
               objFunc.ConfiguracionEmpStr(Model.Codemp, 132), "wgVqKuuJ"));

            Addresses.Add(new NetworkCredential(
               objFunc.ConfiguracionEmpStr(Model.Codemp, 61),
               objFunc.ConfiguracionEmpStr(Model.Codemp, 62)
               )
            );
            return Addresses;
        }

        /// <summary>
        /// Renderizar email template con razor engine
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="objSession"></param>
        /// <param name="enviados"></param>
        /// <param name="datos"></param>
        /// <param name="_service"></param>
        /// <param name="deudor"></param>
        /// <param name="EmailObj"></param>
        /// <returns></returns>
        private static async Task<int> EnvioConTemplate(BuscarCarteraMasivaModel Model, UserSession objSession, int enviados, EmailBody datos, IEnvioEmailService _service, DatosDeudor deudor, EmailDto EmailObj)
        {
            if (Model.Template.ToUpper() == "GENERAL") //No se seleccionó ninguna plantilla y enviaremos con la plantilla general
            {
                BaseEmailModel<EmailGeneral> MailData = new BaseEmailModel<EmailGeneral>
                {
                    Props = new EmailGeneral
                    {
                        Cliente = new ClienteData()
                        {
                            Nombre = datos.NombreCliente
                        },
                        Deudor = new DeudorData()
                        {
                            Nombre = deudor.Nombre
                        },
                        Gestor = new GestorData()
                        {
                            Email = datos.DatosGestor.Email,
                            Nombre = datos.DatosGestor.Nombre,
                            Telefono = datos.DatosGestor.Telefono
                        },
                        FechaVencimiento = (Model.FechaVencimiento.Value.Year == 1 ? null : Model.FechaVencimiento)
                    }
                };

                bool envio = await _service.Send(EmailObj, MailData);
                if (envio)
                    enviados++;
                else
                {
                    Dimol.dao.Funciones.InsertarError("Correo no enviado, CTCID: " + datos.Ctcid + ", PCLID: " + datos.Pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
                    dao.EnvioEmail.ReversarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, Model.Pclid, datos.Ctcid);
                }
                return enviados;
            }
            if (datos.Pclid == 279) //Coopeuch templates
            {
                if (Model.Template.ToUpper() == "EMAILVENCIDAS")
                {
                    BaseEmailModel<EmailVencidasCoopeuch> MailData = new BaseEmailModel<EmailVencidasCoopeuch>
                    {
                        Props = new EmailVencidasCoopeuch()
                        {
                            Cliente = new ClienteData()
                            {
                                Nombre = datos.NombreCliente
                            },
                            Deudor = new DeudorData()
                            {
                                Nombre = deudor.Nombre
                            },
                            Gestor = new GestorData()
                            {
                                Email = datos.DatosGestor.Email,
                                Nombre = datos.DatosGestor.Nombre,
                                Telefono = datos.DatosGestor.Telefono
                            },
                            FechaVencimiento = (Model.FechaVencimiento.Value.Year == 1 ? null : Model.FechaVencimiento)

                        }
                    };
                    bool envio = await _service.Send(EmailObj, MailData);
                    if (envio)
                        enviados++;
                    else
                    {
                        Dimol.dao.Funciones.InsertarError("Correo no enviado, CTCID: " + datos.Ctcid + ", PCLID: " + datos.Pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
                        dao.EnvioEmail.ReversarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, Model.Pclid, datos.Ctcid);
                    }
                }
                else if (Model.Template.ToUpper() == "EMAILSINVENCER")
                {
                    BaseEmailModel<EmailSinVencerCoopeuch> MailData = new BaseEmailModel<EmailSinVencerCoopeuch>
                    {
                        Props = new EmailSinVencerCoopeuch()
                        {
                            Cliente = new ClienteData()
                            {
                                Nombre = datos.NombreCliente
                            },
                            Deudor = new DeudorData()
                            {
                                Nombre = deudor.Nombre
                            },
                            Gestor = new GestorData()
                            {
                                Email = datos.DatosGestor.Email,
                                Nombre = datos.DatosGestor.Nombre,
                                Telefono = datos.DatosGestor.Telefono
                            },
                            FechaVencimiento = (Model.FechaVencimiento.Value.Year == 1 ? null : Model.FechaVencimiento)
                        }
                    };
                    bool envio = await _service.Send(EmailObj, MailData);
                    if (envio)
                        enviados++;
                    else
                    {
                        Dimol.dao.Funciones.InsertarError("Correo no enviado, CTCID: " + datos.Ctcid + ", PCLID: " + datos.Pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
                        dao.EnvioEmail.ReversarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, Model.Pclid, datos.Ctcid);
                    }
                }
            }
            else if (datos.Pclid == 609)
            {
                if (Model.Template.ToUpper() == "SIEMENSGENERAL")
                {
                    BaseEmailModel<SiemensGeneral> MailData = new BaseEmailModel<SiemensGeneral>
                    {
                        Props = new SiemensGeneral
                        {
                            Banco = "Some bank name here",
                            Gestor = new GestorData
                            {
                                Telefono = datos.DatosGestor.Telefono,
                                Email = datos.DatosGestor.Email
                            }
                        }
                    };
                    //Email configuration (Con este id buscamos la url del logo en la db para el cuerpo del email)
                    EmailObj.LogoId = 142;

                    bool envio = await _service.Send(EmailObj, MailData);
                    if (envio)
                        enviados++;
                    else
                    {
                        Dimol.dao.Funciones.InsertarError("Correo no enviado, CTCID: " + datos.Ctcid + ", PCLID: " + datos.Pclid, "", "Dimol.Email.bcp.EnvioEmail.Enviar", objSession.UserId);
                        dao.EnvioEmail.ReversarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, Model.Pclid, datos.Ctcid);
                    }
                }
            }

            return enviados;
        }

        private static string GenerarLiquidaciones(BuscarCarteraMasivaModel model, EmailBody datos, UserSession objSession)
        {
            string Result = "";
            Liquidacion obj = new Liquidacion();
            LiquidacionDura liqD = new LiquidacionDura();
            switch (model.LiquidacionTipo)
            {
                case 1: //Masiva 2
                    obj.Codemp = objSession.CodigoEmpresa;
                    obj.Pclid = model.Pclid;
                    obj.Ctcid = datos.Ctcid;// 1202065;//7598;
                    obj.TipoCartera = 1;
                    obj.EstadoCpbt = "V";
                    obj.Idioma = objSession.Idioma;
                    obj.Sucid = objSession.CodigoSucursal;
                    obj.FechaReporte = DateTime.Now;
                    obj.NombreArchivo = "Liquidacion_Cliente_" + datos.ListaDatosDeudor[0].Rut;
                    obj.PathArchivo = model.Path + obj.NombreArchivo + ".pdf";
                    obj.Pagina = 355;
                    obj.IdReporte = 1;
                    try
                    {
                        //bool result = Cartera.TraeLiquidacionCochaMutual(obj);
                        Result = Cartera.TraeLiquidacionMasiva(obj) ? obj.PathArchivo : "";
                    }
                    catch (Exception ex)
                    {
                        Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmail.GenerarLiquidaciones", 0);
                    }

                    break;
                case 2: //Dura
                    liqD.Codemp = objSession.CodigoEmpresa;
                    liqD.Pclid = model.Pclid;
                    liqD.Ctcid = datos.Ctcid;
                    liqD.TipoCartera = 2;
                    liqD.EstadoCpbt = "V";
                    liqD.Idioma = 1;
                    liqD.Sucid = objSession.CodigoSucursal;
                    liqD.Pagina = 355;
                    liqD.IdReporte = 2;
                    liqD.FechaReporte = DateTime.Now;
                    liqD.NombreArchivo = "Liquidacion_Cliente_" + datos.ListaDatosDeudor[0].Rut;
                    liqD.PathArchivo = model.Path + obj.NombreArchivo + ".pdf";

                    try
                    {
                        //bool result = Cartera.TraeLiquidacionCochaMutual(obj);
                        Result = Cartera.TraeLiquidacionDura(liqD) ? liqD.PathArchivo : "";
                    }
                    catch (Exception ex)
                    {
                        Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmail.GenerarLiquidaciones", 0);
                    }

                    break;
                case 3:  //Cocha - Mutual

                    obj.Codemp = objSession.CodigoEmpresa;
                    obj.Pclid = model.Pclid;
                    obj.Ctcid = datos.Ctcid;// 1202065;//7598;
                    obj.TipoCartera = 1;
                    obj.EstadoCpbt = "V";
                    obj.Idioma = objSession.Idioma;
                    obj.Sucid = objSession.CodigoSucursal;
                    obj.FechaReporte = DateTime.Now;
                    obj.NombreArchivo = "Liquidacion_Cliente_" + datos.ListaDatosDeudor[0].Rut;
                    obj.PathArchivo = model.Path + obj.NombreArchivo + ".pdf";
                    obj.Pagina = 355;
                    obj.IdReporte = 3;
                    try
                    {
                        Result = Cartera.TraeLiquidacionCochaMutual(obj) ? obj.PathArchivo : "";
                    }
                    catch (Exception ex)
                    {
                        Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmail.GenerarLiquidaciones", 0);
                    }
                    break;
                case 4: //Resumen gestiones
                    ResumenGestiones resumen = new ResumenGestiones();
                    resumen.Codemp = objSession.CodigoEmpresa;
                    resumen.Idioma = 1;
                    resumen.Sucid = objSession.CodigoSucursal;
                    resumen.Pclid = model.Pclid;
                    resumen.Ctcid = model.Pclid;
                    resumen.Pagina = 355;
                    resumen.FechaReporte = DateTime.Now;
                    resumen.IdReporte = 4;
                    string Filename = "Resumen_Gestiones_" + model.Codemp + "_" + model.Ctcid + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                    resumen.PathArchivo = model.Path + Filename;
                    try
                    {
                        Result = Cartera.TraeResumenGestiones(resumen) ? resumen.PathArchivo : "";
                    }
                    catch (Exception ex)
                    {
                        Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmail.GenerarLiquidaciones", 0);
                    }

                    break;
                case 5: //Mutual Ley
                    liqD.Codemp = objSession.CodigoEmpresa;
                    liqD.Pclid = model.Pclid;
                    liqD.Ctcid = datos.Ctcid;// 1202065;//7598;
                    liqD.TipoCartera = 1;
                    liqD.EstadoCpbt = "V";
                    liqD.Idioma = 1;
                    liqD.Sucid = objSession.CodigoSucursal;
                    liqD.Pagina = 355;
                    liqD.IdReporte = 5;
                    liqD.FechaReporte = DateTime.Now;
                    liqD.NombreArchivo = "Liquidacion_Cliente_" + datos.ListaDatosDeudor[0].Rut;
                    liqD.PathArchivo = model.Path + obj.NombreArchivo + ".pdf";

                    try
                    {
                        //bool result = Cartera.TraeLiquidacionCochaMutual(obj);
                        Result = Cartera.TraeLiquidacionMutualLey(liqD) ? liqD.PathArchivo : "";
                    }
                    catch (Exception ex)
                    {
                        Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmail.GenerarLiquidaciones", 0);
                    }
                    break;
                default:
                    Result = "";
                    break;
            }
            return Result;
        }

        public static List<MailTarget> GeneratePreview(BuscarCarteraMasivaModel Model, UserSession objSession)
        {
            var Result = new List<MailTarget>();
            var Lst = ListarCarteraEmailMasivo(Model, objSession);
            if (Lst != null)
                Result = GetPreviewTargets(Lst);

            return Result;
        }

        private static List<MailTarget> GetPreviewTargets(List<EmailBody> lstFullOrdenada)
        {
            var EmailTargets = new List<MailTarget>();
            foreach (var DocDeudores in lstFullOrdenada)
            {
                foreach (var el in DocDeudores.ListaDatosDeudor)
                {
                    EmailTargets.Add(new MailTarget()
                    {
                        Id = el.Ctcid,
                        Deudor = el.NombreFantasia,
                        Email = el.Mail,
                        Gestor = DocDeudores.DatosGestor.Nombre,
                        DocNumber = DocDeudores.Ctcid.ToString()
                    });
                }
            }
            return EmailTargets;
        }

        public static IEnumerable<LiquidacionModel> GetLiquidaciones()
        {
            return dao.EnvioEmail.GetLiquidaciones();
        }
    }
}
