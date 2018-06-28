using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;
using System.Transactions;
using Dimol.Reportes.bcp;
using Dimol.Reportes.dto;

namespace Dimol.Email.bcp
{
    public class EnvioEmailMutualPagos
    {

        public static void TraeCuentaBancoEjecutivos(Dimol.Email.dto.DocumentoMutualPagos docEmailMutualPagos)
        {
            dao.EnvioEmailMutualPagos.TraeCuentaBancoEjecutivos(docEmailMutualPagos);
        }

        public static void InsertarBajasCpbtDoc(int pclid, int ctcid, int ccbid, dto.CochaCpbt doc, DateTime fechaPago, int usrid, int cuenta, int banco, string obs, int estid, DateTime fechaActual)
        {
            int exitoInsertaBajas = dao.EnvioEmailMutualPagos.InsertarBajasCpbtDoc(pclid, ctcid, ccbid, doc, fechaPago, usrid, cuenta, banco, obs, estid, fechaActual);

        }

        public static void ActualizarHistorialBajasCpbtDoc(int pclid, int ctcid, int ccbid, string historial, int usrid)
        {
            int exitoActualizaBajas = dao.EnvioEmailMutualPagos.ActualizarHistorialBajasCpbtDoc(pclid, ctcid, ccbid, historial, usrid);

        }

        public static bool Enviar(int pclid, int ctcid, UserSession objSession, dto.DocumentoMutualPagos docEmailMutualPagos, List<string[]> Documentos, string TipoReporte, string comentarioHistorial, string Estid)
        {
            int exitoInsertarHistorial = 0;
            string pathArchivo = "";
            bool error = false;
            Dimol.dao.Funciones objFunc = new Dimol.dao.Funciones();

            Dimol.Email.dto.Gestor DatosGestor = new Dimol.Email.dto.Gestor();
            DatosGestor = Dimol.Email.dao.EnvioEmailCocha.TraeGestor(objSession.Email);

            if (!string.IsNullOrEmpty(DatosGestor.Telefono))
            {

                docEmailMutualPagos.Telefono = DatosGestor.Telefono; // Numero de teléfono del gestor
                docEmailMutualPagos.Email = objSession.Email; //DatosGestor.Email; // Email del gestor
                docEmailMutualPagos.NombreGestor = objSession.Nombre;//DatosGestor.Nombre; // Nombre del gestor

                Transformador objTransformador = new Transformador();

                dto.EmailBodyCocha mailCocha = Dimol.Email.dao.EnvioEmailCocha.TraeMailCocha(Int32.Parse(TipoReporte), Int32.Parse(TipoReporte));
                string salida = objTransformador.TransformXSLToHTML(docEmailMutualPagos, mailCocha.Body);

                exitoInsertarHistorial = 0;

                try
                {                                    
                        
                        Email emailSender = new Email();
                        dto.Email email = new dto.Email();

                        email = new dto.Email();
                        email.Codemp = objSession.CodigoEmpresa;
                        email.From = DatosGestor.Email; //DatosGestor.Email; //objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);
                        email.Sender = objSession.Email; //DatosGestor.Email; //objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);
                    
                        string emails = (Estid == "51") ? objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 141) : docEmailMutualPagos.Numero;

                        foreach (string mail in emails.Split(','))
                        {
                            email.To.Add(mail);
                        }

                        email.Subject = mailCocha.Subject + " " + docEmailMutualPagos.RutDeudor + " " + docEmailMutualPagos.Deudor; 
                        email.Body = salida;
                        email.Html = true;

                        error = emailSender.EnviarEmailMutualPagos(email, pclid) ? false : true;
                        if (error)
                        {
                            Dimol.dao.Funciones.InsertarError("Correo no enviado, CTCID: " + ctcid + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailMutualPagos.Enviar", objSession.UserId);
                            //dao.EnvioEmail.ReversarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, pclid, ctcid);
                        }
                        else
                        {
                            foreach (var item in Documentos)
                            {
                                Dimol.dao.Utilidades util = new Dimol.dao.Utilidades(objSession.CodigoEmpresa, 1, 0, "", "");
                                exitoInsertarHistorial = util.ActualizaCarteraEstados(objSession.CodigoEmpresa, Int32.Parse(item[0]), Int32.Parse(item[1]), Int32.Parse(item[2]), Int32.Parse(Estid), "V");
                            }
                            //exitoInsertarHistorial = Dimol.Email.dao.EnvioEmailCocha.InsertarCarteraClientesEstadosHistorialEspecial(objSession.CodigoEmpresa, pclid, ctcid, Int32.Parse(Documentos.FirstOrDefault()[2]), DateTime.Now, objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 141), objSession.CodigoSucursal, objSession.Gestor, objSession.IpRed, objSession.IpPc, docEmailCocha.Comentario, 0, docEmailCocha.Saldo, objSession.UserId);
                            exitoInsertarHistorial = Dimol.Email.dao.EnvioEmailCocha.InsertarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, pclid, ctcid, objSession.Gestor, objSession.CodigoSucursal, "S", objSession.IpRed, objSession.IpPc, objSession.UserId, comentarioHistorial);

                            if (exitoInsertarHistorial > 0)
                            {
                                error = true;
                            }
                            else
                            {
                                error = false;
                            }
                        }



                }
                catch (Exception ex)
                {
                    Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmailMutualPagos.Enviar, CTCID: " + ctcid + ", PCLID: " + pclid, objSession.UserId);
                }
            }
            else
            {
                Dimol.dao.Funciones.InsertarError("El mail " + objSession.Email + " no se encuentra en la tabla Gestor o Usuario, o bien el mail no coincide en ambas tablas. Codigo de Gestor: " + objSession.Gestor.ToString(), "", "Dimol.Email.bcp.EnvioEmailMutualPagos.Enviar, CTCID: " + ctcid + ", PCLID: " + pclid, objSession.UserId);
                error = false;
            }
            return error;
        }


    }

     
}
