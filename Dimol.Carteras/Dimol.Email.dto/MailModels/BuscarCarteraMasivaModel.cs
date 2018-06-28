using System;

namespace Dimol.Email.dto
{
    public class BuscarCarteraMasivaModel
    {
        public string Estados { get; set; }
	    public int Ctcid { get; set; }
	    public int Codemp { get; set; }
	    public int Pclid { get; set; }
	    public int TipoCartera { get; set; }
	    public DateTime? FechaVencimiento { get; set; }
        public int FechaTipo { get; set; }
        public Decimal MontoDesde { get; set; }
        public Decimal MontoHasta { get; set; }
        public bool Liquidacion { get; set; }
        public int LiquidacionTipo { get; set; }
        public string Path { get; set; }
        public string Template { get; set; }
        public string Gestores { get; set; }
    }
}
