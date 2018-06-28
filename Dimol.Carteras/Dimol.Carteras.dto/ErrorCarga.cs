using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class ResumenErrorCarga
    {
        public bool Error { get; set; }
        public List<ErrorCarga> ListaErrores { set; get; }
    }

    public class ErrorCarga
    {
        public string TipoError { get; set; }
        public string Rut { get; set; }
        public string Dv { get; set; }
        public string Nombre { get; set; }
        public string TipoDocumento { set; get; }
        public string Numero { get; set; }
    }
}
