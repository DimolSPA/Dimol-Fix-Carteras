using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.dto
{
    public class TiposDocumentos
    {
        public int Codemp { get; set; }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CodigoNumero { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string TipoComprobante { get; set; }
        public bool Talonario { get; set; }
        public int UltimoNumero { get; set; }
        public int LineasXReporte { get; set; }
        public string TipoDocDigital { get; set; }
    }
}
