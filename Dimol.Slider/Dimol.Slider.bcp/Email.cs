using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Slider.bcp
{
    public class Email
    {

        public bool EnviarEmail(dto.Email objEmail)
        {
            MailMessage correo = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            string Host = "", CtaMail = "", PasMail = "", From = "";
            dao.Util func = new dao.Util();

            //----------Busco los datos con cual se envia el mail-------
            Host = func.ConfigParamStr(1);
            //PasMail = objFunc.ConfiguracionEmpStr(objEmail.Codemp, 62);
            From = func.ConfigParamStr(2);

            CtaMail = objEmail.Sender;
            correo.From = new MailAddress(objEmail.Sender, objEmail.From);
            PasMail = func.ConfigParamStr(3);

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
                    correo.Bcc.Add(c);
                }
                correo.Subject = objEmail.Subject;
                correo.Body = objEmail.Body;
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
                return false;
            }
        }
    }
}
