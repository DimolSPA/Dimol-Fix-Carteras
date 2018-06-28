using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dto
{
    public class Documento
    {
        public int DocumentoId { get; set; }
        public string pclid { get; set; }
        public string ctcid { get; set; }
        public string sbcid { get; set; }
        public string NumeroDocumento { get; set; }
        public string RutCliente { get; set; }
        public string Cliente { get; set; }
        public string RutDedor { get; set; }
        public string Deudor { get; set; }
        public string RutAsegurado { get; set; }
        public string Asegurado { get; set; }
        public DateTime? FecIngreso { get; set; }
        public string Moneda { get; set; }
        public decimal MontoIngreso { get; set; }//Monto Calculado
        public string Estatus { get; set; }
        public string EstatusId { get; set; }
        public string CriterioId { get; set; }
        public string Criterio { get; set; }
        public string Observaciones { get; set; }
        public decimal? MontoFacturar { get; set; }
        public decimal ValorIngreso { get; set; }
        public string Codmon { get; set; }
        public DateTime? FecStatusProceso { get; set; }
        public int StatusProceso { get; set; }
        public int Row { get; set; }
    }
    public class DocumentoCriterio
    {
        public decimal MontoFacturar { get; set; }
        public string Observaciones { get; set; }
        public string IsEditable { get; set; }
    }

    public class DocumentoExcelFinanza
    {
        public string NumeroDocumento { get; set; }
        public string RutCliente { get; set; }
        public string Cliente { get; set; }
        public string RutDedor { get; set; }
        public string Deudor { get; set; }
        public string RutAsegurado { get; set; }
        public string Asegurado { get; set; }
        public DateTime? FecIngreso { get; set; }
        public string Moneda { get; set; }
        public decimal ValorIngreso { get; set; }
        //public decimal MontoIngreso { get; set; }//Monto Calculado
        public string Criterio { get; set; }
        public string Observaciones { get; set; }
        public decimal? MontoFacturar { get; set; }
        
      
       
    }
    public class DocumentoExcelControlGestion
    {
        public string RUTCLIENTE { get; set; }
        public string RUTNUM { get; set; }
        public string RUTDV { get; set; }
        public string NOMBRE { get; set; }
        public string APEPAT { get; set; }
        public string APEMAT { get; set; }
        public string COMUNA { get; set; }
        public string DIRECCION1 { get; set; }
        public string DIRECCION2 { get; set; }
        public string TELEFONO1 { get; set; }
        public string TELEFONO2 { get; set; }
        public string TELEFONO3 { get; set; }
        public string TELEFONO4 { get; set; }
        public string TELEFONO5 { get; set; }
        public string CELULAR1 { get; set; }
        public string CELULAR2 { get; set; }
        public string CELULAR3 { get; set; }
        public string CELULAR4 { get; set; }
        public string CELULAR5 { get; set; }
        public string FAX { get; set; }
        public string MAIL1 { get; set; }
        public string MAIL2 { get; set; }
        public string MAIL3 { get; set; }
        public string TIPODOCUMENTO { get; set; }
        public string NUMERO { get; set; }
        public DateTime? FECDOC { get; set; }
        public DateTime? FECVENC { get; set; }
        public string MOTIVOCOBRANZA { get; set; }
        public string CODIGOCARGA { get; set; }
        public string MONEDA { get; set; }
        public decimal TIPOCAMBIO { get; set; }
        public decimal MONTOASIGNADO { get; set; }
        public decimal CAPITAL { get; set; }
        public decimal SALDO { get; set; }
        public decimal GASTOJUD { get; set; }
        public decimal GASTOPRE { get; set; }
        public string BANCO { get; set; }
        public string RUTGIRADOR { get; set; }
        public string NOMBREGIRADOR { get; set; }
        public string NEGOCIO { get; set; }
        public string NUMEROAGRUPAR { get; set; }
        public string RUTASEGURADO { get; set; }
        public string NOMBREASEGURADO { get; set; }
        public string DOCORI { get; set; }
        public string DOCANT { get; set; }
        public string COMENTARIO { get; set; }
        
        public string RUTTERCERO { get; set; }
        public string NOMBRETERCERO { get; set; }

        public string IDCUENTA { get; set; }
        public string DESC_CUENTA { get; set; }
    }
}
