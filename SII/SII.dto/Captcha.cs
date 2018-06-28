using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SII.dto
{
    public class Captcha
    {
        public int codigorespuesta { get; set; }
        public string glosarespuesta { get; set; }
        public string imgCaptcha { get; set; }
        public string txtCaptcha { get; set; }
        public string largoCaptcha { get; set; }
        public bool validez { get; set; }
        public string Codigo { get; set; }
        public string Html { get; set; }

    }
}
