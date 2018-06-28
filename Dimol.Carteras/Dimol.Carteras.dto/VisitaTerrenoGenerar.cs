using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class VisitaTerrenoGenerar
    {
        
        public int SolicitudId { get; set; }
        public string RutDeudor { get; set; }
        public string Deudor { get; set; }
        public string Direccion { get; set; }
        public string Comuna { get; set; }
        public string Ciudad { get; set; }
        public decimal Deuda { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Gestor { get; set; }
    }
}
