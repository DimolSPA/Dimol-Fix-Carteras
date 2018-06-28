

namespace Dimol.Carteras.dto
{
    public class BienPropiedad
    {
        public int BienesRaicesId { get; set; }
        public string Conservador { get; set; }
        public string Rol { get; set; }
        public string Foja { get; set; }
        public int Anio { get; set; }
        public string Direccion { get; set; }
        public bool Propietario { get; set; }
        public bool EvaluoFiscal { get; set; }
        public bool Verificado { get; set; }
        public bool Hipotecado { get; set; }
        public bool Embargo { get; set; }
        public int ConservadorId { get; set; }
        public string ArchivoCertificado { get; set; }
    }
    public class BienDetalle
    {
        public string Observacion { get; set; }
    }
}
