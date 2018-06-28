using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Slider.dto
{
    public class Email
    {
        public int Codemp { get; set; }
        public string From { get; set; }
        public MailAddressCollection To { get; set; }
        public MailAddressCollection Cc { get; set; }
        public MailAddressCollection Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Html { get; set; }
        public MailPriority Priority { get; set; }
        public List<Attachment> Attachments { get; set; }
        public bool SSL { get; set; }
        public string Host { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Sender { get; set; }

        public Email()
        {
            To = new MailAddressCollection();
            Bcc = new MailAddressCollection();
            Cc = new MailAddressCollection();
            Priority = MailPriority.Normal;
            Attachments = new List<Attachment>();
        }
    }
}
