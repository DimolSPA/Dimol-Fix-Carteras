using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dimol.Reportes.dto
{
    [Serializable]
    public class ResumenGestiones
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Sucid { get; set; }
        public int IdReporte { get; set; }
        public int Pagina { get; set; }
        public int Idioma { get; set; }
        public string PathArchivo { get; set; }
        
        public CabeceraReporte Encabezado = new CabeceraReporte();
        public TituloReporte Titulo = new TituloReporte();
        public List<ResumenGestionesDetalle> lstDocumentos = new List<ResumenGestionesDetalle>();
        
        [XmlIgnore]
        public DateTime FechaReporte { get; set; }

        [XmlElement("FechaReporte")]
        public string FechaEmision
        {
            get { return this.FechaReporte.ToString("dd/MM/yyyy HH:mm:ss"); }
            set { this.FechaReporte = DateTime.Parse(value); }
        }

    }
    [Serializable]
    public class ResumenGestionesDetalle
    {
        [XmlIgnore]
        public DateTime Fecha { get; set; }

        [XmlElement("Fecha")]
        public string FechaGestion
        {
            get { return this.Fecha.ToString("dd/MM/yyyy"); }
            set { this.Fecha = DateTime.Parse(value); }
        }
        public string Comentario { get; set; }
        public string Usuario { get; set; }
        public string Tipo { get; set; }
    }
}
