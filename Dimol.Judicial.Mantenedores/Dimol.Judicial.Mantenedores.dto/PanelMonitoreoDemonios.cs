using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    class PanelMonitoreoDemonios
    {
    }
    public class MonitoreoExternoCabecera
    {
        public string Recolecto { get; set; }
        public int CantCausas { get; set; }
        public int CantMesActual { get; set; }
    }
    public class MonitoreoExternoDemanda
    {
        public int Pclid { get; set; }
        public string Cliente { get; set; }
        public decimal SaldoCartera { get; set; }
        public decimal SaldoSinDemanda { get; set; }
        public string PorSaldoSinDemanda { get; set; }
        public decimal SaldoDemandado { get; set; }
        public string PorSaldoDemandado { get; set; }
        public decimal SaldoDemandadoDosAnios { get; set; }
        public string PorSaldoDemandadoDosAnios { get; set; }
        public int Row { get; set; }
    }
    public class MonitoreoExternoRolBuscado
    {
        public int TribunalId { get; set; }
        public string Tribunal { get; set; }
        public int Anio { get; set; }
        public int MinRol { get; set; }
        public int MaxRol { get; set; }
        public int Encontrados { get; set; }
        public int NoEncontrados { get; set; }
        public string Porcentaje { get; set; }
        public int Row { get; set; }
    }

    public class MonitoreoSiiCabecera
    {
        public string Recolecto { get; set; }
        public int CantRut { get; set; }
        public int CantMesActual { get; set; }
        public int Acumulativas { get; set; }
        public DateTime FecUtimaActualizacion { get; set; }
        public DateTime FecProximaActualizacion { get; set; }
    }
    public class MonitoreoSiiCliente
    {
        public int Pclid { get; set; }
        public string Cliente { get; set; }
        public decimal SaldoCartera { get; set; }
        public decimal SaldoVerde { get; set; }
        public string PorSaldoVerde { get; set; }
        public decimal SaldoAmarillo { get; set; }
        public string PorSaldoAmarillo { get; set; }
        public decimal SaldoRojo { get; set; }
        public string PorSaldoRojo { get; set; }
        public int Row { get; set; }
    }

    public class MonitoreoInternoCabecera
    {
        public int CantCausasDiaAnterior { get; set; }
        public int CantDeudoresDiaAnterior { get; set; }
        public decimal SaldoDiaAnterior { get; set; }

        public int CantDeudoresJudicializado { get; set; }
        public int CantCausasJudicializadas { get; set; }
        public decimal SaldoJudicializado { get; set; }

        public int CantDeudoresCausasActivas { get; set; }
        public int CantCausasActivas { get; set; }
        public decimal SaldoCausaActiva { get; set; }

        public int CantDeudoresCausasArchivadas { get; set; }
        public int CantCausasArchivadas { get; set; }
        public decimal SaldoCausaArchivada { get; set; }

        public int CantDeudoresCausasArchivadas7dias { get; set; }
        public int CantCausaArchivada7Dias { get; set; }
        public decimal SaldoCausaArchivada7Dias { get; set; }

        public DateTime FecUtimaActualizacion { get; set; }
       
    }
    public class MonitoreoInternoCliente
    {
        public int Pclid { get; set; }
        public string Cliente { get; set; }
        public int TotalCausas { get; set; }
        public int ACount { get; set; }
        public int BCount { get; set; }
        public int CCount { get; set; }
        public int DCount { get; set; }
        public int ActualizadasCount { get; set; }
        public int NoActualizadasCount { get; set; }
        public string Porcentaje { get; set; }
        public int Row { get; set; }
    }
}
