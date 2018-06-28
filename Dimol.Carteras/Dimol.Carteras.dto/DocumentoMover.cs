using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class DocumentoMover
    {
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string UltimoEstado { get; set; }
        public string Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
