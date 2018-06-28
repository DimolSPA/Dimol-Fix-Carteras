using Dimol.dao;
using System;
using System.Net.Mail;
using System.Net.Mime;

namespace Dimol.Email.bcp
{
    public class Email
    {
        public bool EnviarEmail(dto.Email objEmail)
        {
            MailMessage correo = new MailMessage();
            SmtpClient Smtp = new SmtpClient();            
            string Host ="" ,PasMail ="",From ="";
            Funciones objFunc = new Funciones();


            //----------Busco los datos con cual se envia el mail-------
            Host = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 60);
            From = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 64);
            correo.From = new MailAddress(objEmail.From);
            PasMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 63);
            try{
                foreach(MailAddress m in objEmail.To){
                    correo.To.Add(m);
                }
                foreach(MailAddress b in objEmail.Bcc){
                    correo.Bcc.Add(b);
                }
                foreach(MailAddress c in objEmail.Cc){
                    correo.Bcc.Add(c);
                }
                correo.Subject = objEmail.Subject;
                correo.Body = objEmail.Body;
                correo.IsBodyHtml = objEmail.Html;
                correo.Priority = objEmail.Priority;
                //correo.Sender = new MailAddress( objEmail.Sender);
                correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                if (objEmail.Attachments !=null)
                {
                    foreach (Attachment a in objEmail.Attachments)
                    {
                        correo.Attachments.Add(a);
                    }
                }
                


                Smtp.Host = Host;
                Smtp.UseDefaultCredentials = false;
                Smtp.Credentials = new System.Net.NetworkCredential(objEmail.From, PasMail);
                Smtp.EnableSsl = objEmail.SSL;

            }catch(Exception ex){
                return false;
            }

            try {
                Smtp.Send(correo);
                return true;
            }catch(Exception ex){
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.Email.EnviarEmail, To: " + objEmail.To.ToString() + ", Usuario: " + objEmail.Usuario, 0);
                return false;
            }
        }

        public bool EnviarEmailMutual(dto.Email objEmail, int pclid)
        {
            MailMessage correo = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            string Host = "", CtaMail = "", PasMail = "", NombreImagen = "";
            Funciones objFunc = new Funciones();

            if (pclid == 609) {
                NombreImagen = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 142);
            }
            else {
                NombreImagen = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 139);
            }

            LinkedResource LinkedImage = new LinkedResource(NombreImagen);
            LinkedImage.ContentId = "MiLogo";
            LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(objEmail.Body, null, "text/html");
            htmlView.LinkedResources.Add(LinkedImage);

            if(pclid == 318)
            {
                LinkedResource LinkedImageMutual = new LinkedResource(objFunc.ConfiguracionEmpStr(objEmail.Codemp, 140));
                LinkedImageMutual.ContentId = "MiLogoMutual";
                LinkedImageMutual.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                htmlView.LinkedResources.Add(LinkedImageMutual);
            }
            
            correo.AlternateViews.Add(htmlView);

            //----------Busco los datos con cual se envia el mail-------
            Host = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 60);
            //PasMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 62);
            //From = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 200);
            //CtaMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 61);
            //if(objEmail.From == null){
            //    CtaMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 61);
            //}
            //else
            //{
            //    CtaMail = objEmail.From;
            //}
            //objEmail.Sender = "jean.robles@dimol.cl";
            CtaMail = objEmail.From;
            //correo.From = new MailAddress(objEmail.From);
            correo.From = new MailAddress(objEmail.From, objEmail.From);
            PasMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 63);
            try
            {
                foreach (MailAddress m in objEmail.To)
                {
                    correo.To.Add(m);
                }
                foreach (MailAddress b in objEmail.Bcc)
                {
                    correo.Bcc.Add(b);
                }
                foreach (MailAddress c in objEmail.Cc)
                {
                    correo.CC.Add(c);
                }
                correo.Subject = objEmail.Subject;
                //correo.Body = objEmail.Body;
                correo.IsBodyHtml = objEmail.Html;
                correo.Priority = objEmail.Priority;
                //correo.Sender = new MailAddress(objEmail.Sender);
                correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                if (objEmail.Attachments != null)
                {
                    foreach (Attachment a in objEmail.Attachments)
                    {
                        correo.Attachments.Add(a);
                    }
                }

                Smtp.Host = Host;
                Smtp.UseDefaultCredentials = false;
                Smtp.Credentials = new System.Net.NetworkCredential(objEmail.From, PasMail);
                Smtp.EnableSsl = objEmail.SSL;

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.Email.EnviarEmail, To: " + objEmail.To.ToString() + ", Usuario: " + objEmail.Usuario, 0);
                return false;
            }

            try
            {
                Smtp.Send(correo);
                Dimol.dao.Funciones.InsertarError("Emial enviado desde: " + objEmail.From + " " + objEmail.Password, "", "Dimol.Email.bcp.Email.EnviarEmail, To: " + objEmail.To.ToString() + ", Usuario: " + objEmail.From + ", Pass: " + objEmail.Password, 0);
                return true;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.Email.EnviarEmail, To: " + objEmail.To.ToString() + ", Usuario: " + objEmail.Usuario, 0);
                return false;
            }
        }

        public bool EnviarEmailMutualPagos(dto.Email objEmail, int pclid)
        {
            MailMessage correo = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            string Host = "", CtaMail = "", PasMail = "", From = "";
            Funciones objFunc = new Funciones();

            LinkedResource LinkedImage = new LinkedResource(objFunc.ConfiguracionEmpStr(objEmail.Codemp, 139));
            LinkedImage.ContentId = "MiLogo";
            LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(objEmail.Body, null, "text/html");
            htmlView.LinkedResources.Add(LinkedImage);
            
            correo.AlternateViews.Add(htmlView);

            //----------Busco los datos con cual se envia el mail-------
            Host = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 60);
            //PasMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 62);
            From = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 64);
            //CtaMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 61);
            //if(objEmail.From == null){
            //    CtaMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 61);
            //}
            //else
            //{
            //    CtaMail = objEmail.From;
            //}
            objEmail.Sender = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 200);
            CtaMail = objEmail.Sender;
            correo.From = new MailAddress(objEmail.From);
            PasMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 63);
            try
            {
                foreach (MailAddress m in objEmail.To)
                {
                    correo.To.Add(m);
                }
                foreach (MailAddress b in objEmail.Bcc)
                {
                    correo.Bcc.Add(b);
                }
                foreach (MailAddress c in objEmail.Cc)
                {
                    correo.CC.Add(c);
                }
                correo.Subject = objEmail.Subject;
                //correo.Body = objEmail.Body;
                correo.IsBodyHtml = objEmail.Html;
                correo.Priority = objEmail.Priority;
                correo.Sender = new MailAddress(objEmail.Sender);
                
                correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                if (objEmail.Attachments != null)
                {
                    foreach (Attachment a in objEmail.Attachments)
                    {
                        correo.Attachments.Add(a);
                    }
                }



                Smtp.Host = Host;
                Smtp.UseDefaultCredentials = false;
                Smtp.Credentials = new System.Net.NetworkCredential(CtaMail, PasMail);
                Smtp.EnableSsl = objEmail.SSL;
            }
            catch (Exception ex)
            {
                return false;
            }

            try
            {
                Smtp.Send(correo);
                return true;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.Email.EnviarEmail, To: " + objEmail.To.ToString() + ", Usuario: " + objEmail.Usuario, 0);
                return false;
            }
        }
    }
}
