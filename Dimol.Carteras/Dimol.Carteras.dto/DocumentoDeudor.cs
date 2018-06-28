using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class DocumentoDeudor
    {
        public int Codemp { get; set; }
        public string Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Dcdid { get; set; }
        public string TipoDocumento { get; set; }
        public string UrlArchivo { get; set; }
        public string Archivo { get; set; }
        public string NombreCliente { get; set; }
        public string RutDeudor { get; set; }
        public string TipoTipoDocumento { get; set; }
    }
}
