using System.ComponentModel;

namespace Dimol.Carteras.Models
{
    public class CargaItauModel
    {
        public int Pclid { get; set; }

        [DisplayName("Cliente")]
        public string RutCliente { set; get; }

        public string NombreCliente { get; set; }

        [DisplayName("Tipo Cartera")]
        public string TipoCartera { get; set; }

        [DisplayName("CodigoCarga")]
        public string CodigoCarga { get; set; }

        [DisplayName("Contrato")]
        public string Contrato { get; set; }

        [DisplayName("Archivo Deudor")]
        public string ArchivoDeudor { get; set; }

        [DisplayName("Archivo Dirección")]
        public string ArchivoDireccion { get; set; }

        [DisplayName("Archivo Teléfono")]
        public string ArchivoTelefono { get; set; }

        [DisplayName("Archivo Email")]
        public string ArchivoEmail { get; set; }

        [DisplayName("Archivo Operación")]
        public string ArchivoOperacion { get; set; }

        [DisplayName("Archivo Cuota")]
        public string ArchivoCuota { get; set; }

        [DisplayName("Archivo Pago")]
        public string ArchivoPago { get; set; }

        [DisplayName("Carga Judicial")]
        public bool CargaJudicial { get; set; }

        [DisplayName("Archivo Quiebra")]
        public bool ArchivoQuiebra { get; set; }
    }
}