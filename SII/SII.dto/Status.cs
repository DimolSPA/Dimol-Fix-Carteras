using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SII.dto
{
    public class Status
    {
        public int Rut { get; set; }
        public string DigitoVerificador { get; set; }
        public string CodigoCaptcha { get; set; }
        public string URLCaptcha { get; set; }
        public Byte[] ImagenCaptcha { get; set; }
        public string ValorCaptcha { get; set; }
        public string Html { get; set; }
        public int IdRut { get; set; }
        public int Ctcid { get; set; }
        public string Estado { get; set; }

    }
}
