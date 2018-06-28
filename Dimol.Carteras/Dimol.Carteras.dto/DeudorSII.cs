using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dimol.Carteras.dto
{
    public class DeudorSII
    {
        public int Ctcid { get; set; }
        public string RutContribuyente { get; set; }
        public string RazonSocial { get; set; }
        
        [XmlIgnore]
        public DateTime FechaConsulta { get; set; }

        [XmlElement("Fecha de Consulta")]
        public string FechaConsultaStr
        {
            get { return this.FechaConsulta.ToString("dd-MM-yyyy HH:mm"); }
            set { this.FechaConsulta = DateTime.Parse(value); }
        }

        public string InicioActividades { get; set; }
        
        [XmlIgnore]
        public DateTime FechaInicioAct { get; set; }

        [XmlElement("Fecha de Inicio de Actividades")]
        public string FechaInicioActStr
        {
            get { return this.FechaInicioAct.ToString("dd-MM-yyyy"); }
            set { this.FechaInicioAct = DateTime.Parse(value); }
        }

        public string ContribuyenteAutorizado { get; set; }
        public string ContribuyenteProPyme { get; set; }

        public List<DeudorActividades> lstActividades = new List<DeudorActividades>();

        public string Comentario { get; set; }

        public List<DeudorDocTimbrados> lstTimbrados = new List<DeudorDocTimbrados>();

        public string Observacion { get; set; }
        public string Registrado { get; set; }

    }

    public class DeudorActividades
    {
        public int Codigo { get; set; }
        public string Actividades { get; set; }
        public string Categoria { get; set; }
        public string AfectaIVA { get; set; }
    }

    public class DeudorDocTimbrados
    {
        public string Documento { get; set; }
        public int Anio { get; set; }
    }
}
