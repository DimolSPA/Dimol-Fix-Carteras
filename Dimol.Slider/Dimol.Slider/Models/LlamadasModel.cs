using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Xml.Serialization;

namespace Dimol.Slider.Models
{
    public class LlamadasModel
    {
        public string NombreGestor { get; set; }
        public string NumeroGestor { get; set; }
        public int Efectivas { get; set; }
        public int Totales { get; set; }
        public string Cartera { get; set; }

        [XmlIgnore]
        public decimal Promedio { get; set; }
        [XmlElement("Promedio")]
        public string PromedioStr
        {
            get { Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL"); return this.Promedio.ToString("N0"); }
            set { this.Promedio = decimal.Parse(value); }
        }
    }
}