using Dimol.dao;
using Dimol.Email.dto.MailModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Dimol.Email.bcp.Services
{
    public class EnvioEmailService : IEnvioEmailService
    {
        public string Render<T>(int pclid, string filename, BaseEmailModel<T> Model)
        {
            if (filename == "GENERAL")//Si es general el cliente es 0
                pclid = 0;

            var Bones = Engine.GetTemplateContent(pclid, filename); //Buscamos el template
            //Finalmente renderizamos
            var Result = Engine.Render(Bones, Model);

            return Result;
        }

        public string Render(int pclid, string filename)
        {
            var Bones = Engine.GetTemplateContent(pclid, filename); //Buscamos el template
            return Bones;
        }

        //Envío de emails a partir de un template
        public async Task<bool> Send<T>(EmailDto Model, BaseEmailModel<T> Data)
        {
            try
            {
                var fromAddress = new MailAddress(Model.From, Model.FromName);
                var toAddress = new MailAddress(Model.To[0].Address, Model.ToName);
                string fromPassword = Model.Password;
                string body = Render(Model.Pclid, Model.TemplateName, Data);

                //Inline logo settings
                Funciones objFunc = new Funciones();
                //Creamos nuestro recurso
                Model.LogoPath = objFunc.ConfiguracionEmpStr(1 , Model.LogoId);
                LinkedResource LinkedImage = new LinkedResource(Model.LogoPath);
                LinkedImage.ContentId = "Logo";
                LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                htmlView.LinkedResources.Add(LinkedImage);


                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = Model.Subject,
                    //Body = body,
                    IsBodyHtml = Model.IsBodyHtml,
                    Sender = new MailAddress(Model.Sender)
                };

                message.AlternateViews.Add(htmlView);

                foreach (MailAddress b in Model.Bcc)
                {
                    message.Bcc.Add(b);
                }

                foreach (MailAddress c in Model.Cc)
                {
                    message.Bcc.Add(c);
                }

                //Add attachments
                if (Model.Attachments.Count > 0)
                {
                    foreach(var a in Model.Attachments)
                    {
                        var Attachment = new Attachment(a, MediaTypeNames.Application.Octet);
                        message.Attachments.Add(Attachment);
                    } 
                }
                

                using (var smtp = new SmtpClient
                {
                    Host = Model.Host,
                    //Port = Model.Port,
                    EnableSsl = Model.EnableSsl,
                    DeliveryMethod = Model.DeliveryMethod,
                    UseDefaultCredentials = Model.UseDefaultCredentials,
                    Credentials = new NetworkCredential(Model.Sender, Model.Password)
                }) {
                    await smtp.SendMailAsync(message);
                };

                foreach(Attachment a in message.Attachments)
                {
                    a.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmail.SendEmailWithTemplate", 0);
                return false;
            }
        }

        public List<string> ListTemplates()
        {
            List<string> Templates = new List<string>();
            DirectoryInfo Directory = new DirectoryInfo(Path.Combine(Engine.GetTemplatesPath(), "Dimol.Email.bcp", "Templates"));
            FileInfo[] Files = Directory.GetFiles("*.cshtml");
            foreach (var F in Files)
            {
                Templates.Add(F.FullName);
            }
            return Templates;
        }

        public IEnumerable<EmailTemplate> ListarEmailTemplatesCliente(int pclid)
        {
            return dao.EnvioEmail.ListarEmailTemplatesByClient(pclid);
        }
    }
}
