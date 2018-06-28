using Dimol.dto;
using Dimol.Reportes.bcp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dimol.Email.bcp
{
    public class EnvioEmailCocha
    {
        public static dto.CochaCpbt TraeDocumento(string[] lstDocs)
        {
          return dao.EnvioEmailCocha.TraeDocumento(lstDocs);
        }

        public static string TraeNombreCliente(int codemp, int pclid)
        {
            return dao.EnvioEmailCocha.TraeNombreCliente(codemp, pclid);
        }

        public static string TraeNombreDeudor(int codemp, int ctcid)
        {
            return dao.EnvioEmailCocha.TraeNombreDeudor(codemp, ctcid);
        }

        public static string TraeRutDeudor(int codemp, int ctcid)
        {
            return dao.EnvioEmailCocha.TraeRutDeudor(codemp, ctcid);
        }

        public static bool Enviar(dto.DocumentoCocha docEmailCocha, int pclid, int ctcid, UserSession objSession, List<string[]> Documentos, string TipoReporte)
        {
            Dimol.dao.Funciones.InsertarError("Inicio envio mail cocha: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailCocha.Enviar", objSession.UserId);
            
            bool error = false;
            string[] conCopia;
            int respuesta;
            string cuadroPeso = "", cuadroDolar = "";

            Transformador objTransformador = new Transformador();
                                 
            dto.EmailBodyCocha mailCocha = Dimol.Email.dao.EnvioEmailCocha.TraeMailCocha(Int32.Parse(TipoReporte), 1);
            string salida = objTransformador.TransformXSLToHTML(docEmailCocha, mailCocha.Body);

            Dimol.dao.Funciones objFunc = new Dimol.dao.Funciones();                      
                              
            Dimol.dao.Funciones.InsertarError("Comienzo proceso envio email: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailCocha.Enviar", objSession.UserId);

            Email emailSender = new Email();
            dto.Email email = new dto.Email();
                        
            email = new dto.Email();
            email.From = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133); 
            email.Html = true;
            email.Codemp = objSession.CodigoEmpresa;
            email.Sender = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);
            email.Subject = docEmailCocha.Deudor; //mailCocha.Subject;
            email.Body = salida;

            foreach(string mail in objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 134).Split(','))
            {
                email.To.Add(mail);
            }                        

            if (docEmailCocha.CodigoCarga == 7)
            {
                conCopia = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 135).Split(',');
            }
            else
            {
                conCopia = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 136).Split(',');
            }

            foreach (string mailCc in conCopia)
            {
                email.Cc.Add(mailCc);
            }

            error = emailSender.EnviarEmail(email) ? false : true;

            if (error)
            {
                Dimol.dao.Funciones.InsertarError("Correo no enviado, , PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailCocha.Enviar", objSession.UserId);
            }
            else
            {

                if (docEmailCocha.DocPesos.Count() != 0) cuadroPeso = "FACTURA" + Convert.ToChar(9) + "MONEDA" + Convert.ToChar(9) + "MONTO" + Convert.ToChar(13);

                foreach (var item in docEmailCocha.DocPesos)
                {
                    cuadroPeso += item.Numero + Convert.ToChar(9) + item.TipoMoneda + Convert.ToChar(9) + item.Saldo + Convert.ToChar(13);
                }

                if (docEmailCocha.DocPesos.Count() != 0) cuadroPeso += "TOTAL" + Convert.ToChar(9) + "CLP" + Convert.ToChar(9) + docEmailCocha.Saldo + Convert.ToChar(13);


                if (docEmailCocha.DocDolares.Count() != 0) cuadroDolar = "FACTURA" + Convert.ToChar(9) + "MONEDA" + Convert.ToChar(9) + "MONTO" + Convert.ToChar(13);

                foreach (var item in docEmailCocha.DocDolares)
                {
                    cuadroDolar += item.Numero + Convert.ToChar(9) + item.TipoMoneda + Convert.ToChar(9) + item.Saldo + Convert.ToChar(13);
                }

                if (docEmailCocha.DocDolares.Count() != 0) cuadroDolar += "TOTAL" + Convert.ToChar(9) + "DOLAR" + Convert.ToChar(9) + docEmailCocha.SaldoDolar + Convert.ToChar(13);


                docEmailCocha.Comentario = "FECHA     " + docEmailCocha.FechaMail + Convert.ToChar(13) + "CUENTA     " + docEmailCocha.Cuenta + Convert.ToChar(13) + "BANCO     " + docEmailCocha.Banco + Convert.ToChar(13) + cuadroPeso + Convert.ToChar(13) + cuadroDolar + Convert.ToChar(13) + docEmailCocha.Comentario;

                //respuesta = Dimol.Email.dao.EnvioEmailCocha.InsertarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, pclid, ctcid, 7, objSession.CodigoSucursal, objSession.Gestor, objSession.IpRed, objSession.IpPc, docEmailCocha.Comentario, objSession.UserId);

                /* if (respuesta > 0)
                 {
                     //Dimol.dao.Funciones.InsertarError("Registro en Historial Exitoso: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailCocha.Enviar", objSession.UserId);
                 }
                 else
                 {
                     Dimol.dao.Funciones.InsertarError("Registro en Historial Fallido: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailCocha.Enviar", objSession.UserId);
                 }*/

                foreach (var item in Documentos)
                {
                    Dimol.dao.Utilidades util = new Dimol.dao.Utilidades(objSession.CodigoEmpresa, 1, 0, "", "");
                    respuesta = util.ActualizaCarteraEstados(objSession.CodigoEmpresa, Int32.Parse(item[0]), Int32.Parse(item[1]), Int32.Parse(item[2]), objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 137), "V");                   
                }

                respuesta = Dimol.Email.dao.EnvioEmailCocha.InsertarCarteraClientesEstadosHistorialEspecial(objSession.CodigoEmpresa, pclid, ctcid, Int32.Parse(Documentos.FirstOrDefault()[2]), DateTime.Now, objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 137), objSession.CodigoSucursal, objSession.Gestor, objSession.IpRed, objSession.IpPc, docEmailCocha.Comentario, docEmailCocha.Saldo, 0, objSession.UserId);

                if (respuesta < 1)
                {
                    Dimol.dao.Funciones.InsertarError("Registro en Historial Fallido: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailCocha.Enviar", objSession.UserId);
                }
            }
            
            Dimol.dao.Funciones.InsertarError("Fin proceso email cocha: Usuario" + objSession.Nombre + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailCocha.Enviar", objSession.UserId);
            return error;
        }
        
        public static List<Combobox> ListarTipoReporte(int codemp, string first)
        {
            return dao.EnvioEmailCocha.ListarTipoReporte(codemp, first);
        }

        public static string ListarMonedas(int codemp)
        {            
            return dao.EnvioEmailCocha.ListarMonedas(codemp);
        }
    }

     
}
