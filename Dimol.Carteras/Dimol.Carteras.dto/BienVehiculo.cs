using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Carteras.dto
{
    public class BienVehiculo
    {
        public int VehiculoId { get; set; }
        [DisplayName("Patente")]
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        [DisplayName("Año")]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Debe ingresar un año valido.")]
        public int? Anio { get; set; }
        [DisplayName("Propietario")]
        public bool Propietario { get; set; }
        [DisplayName("Prenda")]
        public bool Prenda { get; set; }
        [DisplayName("Embargo")]
        public bool Embargo { get; set; }
        [DisplayName("Marca")]
        public int? MarcaId { get; set; }
        [DisplayName("Modelo")]
        public int? ModeloId { get; set; }
        public string ArchivoComprobante { get; set; }
    }
}
