using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;
using System.Transactions;
using Dimol.Reportes.bcp;
using Dimol.Reportes.dto;
using System.Net.Mail;
using System.Net.Mime;

namespace Dimol.Email.bcp
{
    public class EnvioEmailCoopeuch
    {
        public static List<int> ListarCarteraEmail(int codemp, int pclid, string estid, int tipoCartera, int gestor, int sucid, string listaGestores)
        {
          return dao.EnvioEmail.ListarCarteraEmail( codemp, pclid,  estid, tipoCartera, gestor,  sucid,  listaGestores);
        }

        public static List<dto.DatosDeudor> ListarContactosEmailDeudor(int codemp, int ctcid, int todo, int contacto)
        { 
            return dao.EnvioEmail.ListarContactosEmailDeudor( codemp, ctcid, todo,  contacto);
        }

        public static bool Enviar(int pclid, int ctcid, UserSession objSession, string path, string emailDestino, dto.DocumentoCocha docEmailCocha, string TipoReporte)
        {
            int exitoInsertarHistorial = 0;
            
            bool error = false;
            Dimol.dao.Funciones objFunc = new Dimol.dao.Funciones();

            Dimol.Email.dto.Gestor DatosGestor = new Dimol.Email.dto.Gestor();
            DatosGestor = Dimol.Email.dao.EnvioEmailCocha.TraeGestor(objSession.Email);

            if (!string.IsNullOrEmpty(DatosGestor.Telefono)) { 

                docEmailCocha.Numero = DatosGestor.Telefono; // Numero de teléfono del gestor
                docEmailCocha.Cuenta = objSession.Email; //DatosGestor.Email; // Email del gestor
                docEmailCocha.Banco = objSession.Nombre;//DatosGestor.Nombre; // Nombre del gestor

                Transformador objTransformador = new Transformador();

                dto.EmailBodyCocha mailCocha = Dimol.Email.dao.EnvioEmailCocha.TraeMailCocha(Int32.Parse(TipoReporte), Int32.Parse(TipoReporte));
                string salida = objTransformador.TransformXSLToHTML(docEmailCocha, mailCocha.Body);
            
                exitoInsertarHistorial = 0;
                  
                        try
                        {                                                                                         

                                if (!error)
                                {
                                    
                                    Email emailSender = new Email();
                                    dto.Email email = new dto.Email();
                             
                                        email = new dto.Email();
                                        email.From = objSession.Email; //DatosGestor.Email; // objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);
                                        email.Codemp = objSession.CodigoEmpresa;
                                        email.From = objSession.Email; //DatosGestor.Email; //objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);
                                        email.Sender = objSession.Email; //DatosGestor.Email; //objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);

                                        foreach (string mail in emailDestino.Split(','))
                                        {
                                            email.To.Add(mail);
                                        }                                                                 
                                        
                                        email.Subject = mailCocha.Subject;
                                        email.Body = salida;
                                        email.Html = true;                                        
                                        
                                        error = emailSender.EnviarEmailMutual(email, pclid) ? false : true;

                                        if (error)
                                        {
                                            Dimol.dao.Funciones.InsertarError("Correo no enviado, CTCID: " + ctcid + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailCoopeuch.Enviar", objSession.UserId);
                                            
                                        }
                                        else
                                        {
                                            
                                            exitoInsertarHistorial = Dimol.Email.dao.EnvioEmailCocha.InsertarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, pclid, ctcid, objSession.Gestor, objSession.CodigoSucursal, "S", objSession.IpRed, objSession.IpPc, objSession.UserId, docEmailCocha.Comentario);

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
                        

                        }
                        catch (Exception ex)
                        {
                            Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmailCoopeuch.Enviar, CTCID: " + ctcid + ", PCLID: " + pclid, objSession.UserId);
                        }
            }
            else
            {
                Dimol.dao.Funciones.InsertarError("El mail " + objSession.Email + " no se encuentra en la tabla Gestor o Usuario, o bien el mail no coincide en ambas tablas. Codigo de Gestor: " + objSession.Gestor.ToString(), "", "Dimol.Email.bcp.EnvioEmailCoopeuch.Enviar, CTCID: " + ctcid + ", PCLID: " + pclid, objSession.UserId);
                error = false;
            }
            return error;
        }
        

    }

     
}
