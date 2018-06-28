using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class BoletaHonorarioSalida
    {
        public string TipoDeDocumento { get; set; }
        public string BoletaDirector { get; set; } 
        public string BoletaAnulada { get; set; } 
        public string TipoDeBoleta { get; set; } 
        public string FechaDelDocumento { get; set; } 
        public string FechaLibro { get; set; } 
        public string NumeroDelDocumento { get; set; } 
        public string FechaDeContabilizacion { get; set; } 
        public string IdentificadorPrestadorEmisor { get; set; } 
        public string Comentario { get; set; } 
        public string TipoDeHonorario { get; set; } 
        public string IdentificadorCentroDeNegocios { get; set; } 
        public string MontoBruto { get; set; } 
        public string AnalisisTasaDeCambioBruto { get; set; } 
        public string AnalisisPorMonedaValorBruto { get; set; } 
        public string AnalisisPorFichaValorBruto { get; set; } 
        public string AnalisisPorClasificadorN1ValorBruto { get; set; } 
        public string AnalisisPorClasificadorN2ValorBruto { get; set; } 
        public string TipoRetencion { get; set; } 
        public string MontoRetencion { get; set; } 
        public string AnalisisTasaDeCambioRetencion { get; set; } 
        public string AnalisisPorMonedaValorRetencion { get; set; } 
        public string AnalisisPorFichaValorRetencion { get; set; } 
        public string AnalisisPorClasificadorN1ValorRetencion { get; set; } 
        public string AnalisisPorClasificadorN2ValorRetencion { get; set; } 
        public string Total { get; set; } 
        public string AnalisisTasaDeCambioTotal { get; set; } 
        public string AnalisisPorMonedaValorTotal { get; set; } 
        public string AnalisisPorFichaValorTotal{ get; set; } 
        public string AnalisisPorClasificadorN1ValorTotal { get; set; } 
        public string AnalisisPorClasificadorN2ValorTotal { get; set; } 
        public string CodigoLegal { get; set; } 
        public string NombrePersona{ get; set; } 
        public string Ciudad { get; set; } 
        public string DireccionPersona { get; set; } 

    }
}
