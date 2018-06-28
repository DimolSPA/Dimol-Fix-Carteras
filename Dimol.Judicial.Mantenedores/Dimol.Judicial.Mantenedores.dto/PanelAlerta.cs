using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class PanelAlerta
    {
        public int TraspasosAprobados { get; set; }
        public int DemandasProceso { get; set; }
        public string PromedioConfeccionDias { get; set; }
        public int CantCorrecciones { get; set; }
        public string PromedioCorrecciones { get; set; }
    }
    public class PanelAlertaAnalisisCliente
    {
        public int Pclid { get; set; }
        public string Cliente { get; set; }
        public int CantDemandas { get; set; }
        public decimal Saldo { get; set; }
        public int Percentage { get; set; }
        public string Porcentaje { get; set; }
    }
    public class PanelAlertaEncargado
    {
        public int UsrId { get; set; }
        public string Encargado { get; set; }
        public int CantDemandas { get; set; }
        public decimal Saldo { get; set; }
        public int CantCorrecciones { get; set; }
        public string TiempoEntrega { get; set; }
    }
    public class PanelAlertaTipo
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public int PromedioDias { get; set; }
        public int CantCasos { get; set; }
        public string Atraso { get; set; }
    }
    public class PanelAlertaTipoReporte
    {
        public int PanelId { get; set; }
        public string Cliente { get; set; }
        public string Deudor { get; set; }
        public string Asegurado { get; set; }
        public Nullable<System.DateTime> FechaAprobacionTraspaso { get; set; }
        public Nullable<System.DateTime> FechaIngresoTribunal { get; set; }
        public Nullable<System.DateTime> IngresoJudicial { get; set; }
        public Nullable<System.DateTime> FechaEnvio { get; set; }
        public Nullable<System.DateTime> FechaEntrega { get; set; }
        public string Encargado { get; set; }
        public string Correcciones { get; set; }
        public int CountCorrecciones { get; set; }
        public string Comentarios { get; set; }
        public int DiasTranscurso { get; set; }
        public int DiasAtraso { get; set; }
    }

    public class PanelAlertaReporteAnalisisCliente
    {
        public int PanelId { get; set; }
        public int Pclid { get; set; }
        public string Cliente { get; set; }
        public string Deudor { get; set; }
        public string Asegurado { get; set; }
        public Nullable<System.DateTime> FechaAprobacionTraspaso { get; set; }
        public Nullable<System.DateTime> FechaIngresoTribunal { get; set; }
        public Nullable<System.DateTime> IngresoJudicial { get; set; }
        public Nullable<System.DateTime> FechaEnvio { get; set; }
        public Nullable<System.DateTime> FechaEntrega { get; set; }
        public string Encargado { get; set; }
        public string Correcciones { get; set; }
        public int CountCorrecciones { get; set; }
        public string Comentarios { get; set; }
        public string TipoDocumento { get; set; }
        public decimal Saldo { get; set; }
    }

}
