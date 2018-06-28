using System;

namespace Dimol.Reportes.dto
{
    [Serializable]
    public class TituloReporte
    {
        public string Cliente { get; set; }
        public string RutCliente { get; set; }
        public string Deudor { get; set; }
        public string RutDeudor { get; set; }

        public string TasaInteresPesos { get; set; }
        public string TasaInteresUF { get; set; }
        public string TasaInteresDolar { get; set; }
    }
}