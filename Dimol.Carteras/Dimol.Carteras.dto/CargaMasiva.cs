using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class CargaMasiva
    {
        public int Pclid { get; set; }
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        public string TipoCartera { get; set; }
        public string CodigoCarga { get; set; }
        public string Contrato { get; set; }
        public string Archivo { get; set; }
        public bool ArchivoJudicial { get; set; }
        public bool ArchivoQuiebra { get; set; }

        public List<ErrorCarga> ListaErrores = new List<ErrorCarga>();
    }

    public class DocumentoAnterior
    {
        public int Ccbid { get; set; }
        public string Numero { get; set; }
        public string EstadoCpbt { get; set; }
        public string CodigoCarga { get; set; }
        public int EstadoId { get; set; }
    }

    public class AnularCargaMasiva
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string  Usuario { get; set; }
    }


    public class AprobarCargaMasiva
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
        public string RutCliente { set; get; }
        public string NombreCliente { get; set; }
        public string RutDeudor { set; get; }
        public string NombreDeudor { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoAsignado { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaIngreso { get; set; }
    }

}
