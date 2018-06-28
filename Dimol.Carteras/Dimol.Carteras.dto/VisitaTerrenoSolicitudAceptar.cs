using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class VisitaTerrenoSolicitudAceptar
    {
        public int pclid { get; set; }
        public int ctcid { get; set; }
        public int solicitudId { get; set; }
        public string rutDeudor { get; set; }
        public string deudor { get; set; }
        public string quiebra { get; set; }
        public string direccion { get; set; }
        public int comId { get; set; }
        public string comuna { get; set; }
        public int ciuId { get; set; }
        public string ciudad { get; set; }
        public int regId { get; set; }
        public string region { get; set; }
        //public string telefonos { get; set; }
        public decimal deuda { get; set; }
        public string cliente { get; set; }
        public string gestor { get; set; }
        public string ultimaGestion { get; set; }
        public int fila { get; set; }
        public string Solicitante { get; set; }
    }
}
