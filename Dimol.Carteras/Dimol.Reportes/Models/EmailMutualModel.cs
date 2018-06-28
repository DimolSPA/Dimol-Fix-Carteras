using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.Reportes.Models
{
    public class EmailMutualModel
    {
        [DisplayName("Cliente")]
        [StringLength(200)]
        public string NombreRutCliente { get; set; }
        [DisplayName("pclid")]
        [StringLength(20)]
        public string Pclid { get; set; }
        [DisplayName("Deudor")]
        [StringLength(1024)]
        public string NombreRutDeudor { get; set; }
        [DisplayName("ctcid")]
        [StringLength(20)]
        public string Ctcid { get; set; }
        [DisplayName("Tipo Reporte")]
        [StringLength(200)]
        public string TipoReporte { get; set; }
        [DisplayName("Tipo Cartera")]
        [StringLength(20)]
        public string TipoCartera { get; set; }
        [DisplayName("Codigo Carga")]
        [StringLength(20)]
        public string CodigoCarga { get; set; }
        [DisplayName("Codigo Carga")]
        [StringLength(20)]
        public string Grupo { get; set; }
        [DisplayName("Fecha Baja")]
        public DateTime? FechaBaja { get; set; }
        [DisplayName("Gestor")]
        [StringLength(20)]
        public string Gestor { get; set; }
        [DisplayName("Fecha Recuperación")]
        public DateTime? FechaDesde { get; set; }
        [DisplayName("Hasta")]
        public DateTime? FechaHasta { get; set; }
    
        [DisplayName("Estado")]
        [StringLength(20)]
        public string Estados { get; set; }
        public string Gestores { get; set; }
        public string Documentos { get; set; }

        [DisplayName("Email")]       
        public string Email { get; set; }
        [DisplayName("dcdid")]
        [StringLength(20)]
        public string Dcdid { get; set; }
        [DisplayName("Tipo Documento")]
        [StringLength(20)]
        public string TipoDocumento { get; set; }
        [DisplayName("Archivo")]
        [StringLength(1024)]
        public string Archivo { get; set; }
        [DisplayName("URL")]
        [StringLength(1024)]
        public string Ruta { get; set; }

        [DisplayName("Número")]
        [StringLength(1024)]
        public string Numero { get; set; }
        [DisplayName("Cuenta")]
        [StringLength(1024)]
        public string Cuenta { get; set; }
        [DisplayName("Monto en Pesos")]
        public decimal Monto { get; set; }
        [DisplayName("Monto en Dólares")]
        public decimal MontoDolar { get; set; }
        [DisplayName("Saldo Enviado")]
        public decimal Saldo { get; set; }
        [DisplayName("Saldo en Dólares")]
        public decimal SaldoDolar { get; set; }
        [DisplayName("Banco")]
        [StringLength(1024)]
        public string Banco { get; set; }

        public int Codemp { get; set; }

        [DisplayName("Fecha")]
        public string FechaMail { get; set; }

        [DisplayName("Comentario")]
        public string ComentarioMail { get; set; }

        [DisplayName("N/C CLP")]
        public bool CheckNotaCredito { get; set; }

        public bool ValidaNotaCredito { get; set; }

        [DisplayName("Valor en Pesos")]
        public decimal NotaCredito { get; set; }
    }
}
