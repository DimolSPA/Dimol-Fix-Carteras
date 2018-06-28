using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.dto
{
    public class ClasificacionDocumentos
    {
        public int Codemp { get; set; }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public string TipoComprobante { get; set; }
        public string TipoProducto { get; set; }
        public bool CostosSN { get; set; }
        public bool SeleccionOtroComprobanteSN { get; set; }
        public bool CarteraClientesSN { get; set; }
        public bool ContableSN { get; set; }
        public bool SeleccionaPagosSN { get; set; }
        public bool AplicaPagosSN { get; set; }
        public string Concepto { get; set; }
        public bool FinalizaDeudaSN { get; set; }
        public bool CancelaSN { get; set; }
        public string TipoLibro { get; set; }
        public bool CambiaDocumentoSN { get; set; }
        public bool RemesaSN { get; set; }
        public string TipoDocSeleccionar { get; set; }
        public bool AnulaImpuestoSN { get; set; }
        public bool FormaPagoSN { get; set; }
        public bool OrdenCompraSN { get; set; }
        public string Movimiento { get; set; }
        public bool MostrarEnLibrosSN { get; set; }
        public bool HonorariosSN { get; set; }
        public string Cuenta { get; set; }
        public int Stock { get; set; }
        public bool SaldosSN { get; set; }
        public bool ReservaSN { get; set; }
        public bool TransitoSN { get; set; }
    }
}
