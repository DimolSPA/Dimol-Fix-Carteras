using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class CastigoMasivo
    {
        public int Pclid { get; set; }
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        public string TipoCartera { get; set; }
        public string CodigoCarga { get; set; }
        public string Contrato { get; set; }
        public string Archivo { get; set; }
        public int Codemp { get; set; }
        public int Tpcid { get; set; }
        public int Sucid { get; set; }
        public int Numero { get; set; }
        public int Pagina { get; set; }
        public int IdReporte { get; set; }

        public List<ErrorCarga> ListaErrores = new List<ErrorCarga>();
    }
}
