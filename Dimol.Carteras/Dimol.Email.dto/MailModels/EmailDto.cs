using System.Collections.Generic;
using System.Net.Mail;

namespace Dimol.Email.dto.MailModels
{
    public class EmailDto
    {
        public EmailDto()
        {
            this.LogoId = 139;
            this.To = new MailAddressCollection();
            this.Bcc = new MailAddressCollection();
            this.Cc = new MailAddressCollection();
            this.Priority = MailPriority.Normal;
            this.Attachments = new List<string>();
        }

        public string From { get; set; }
        public string FromName { get; set; }
        public string Sender { get; set; }
        public string ToName { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public int Pclid { get; set; }
        public string TemplateName { get; set; }
        public List<string> Attachments { get; set; }
        public MailAddressCollection To { get; set; }
        public MailAddressCollection Cc { get; set; }
        public MailAddressCollection Bcc { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public MailPriority Priority { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHtml { get; set; }
        public int LogoId { get; set; }
        public string LogoPath { get; set; }

        
    }

   
}
