using System;
using System.ComponentModel;

namespace Dimol.Email.dto.MailModels
{
    public class EmailEntity
    {
        [DisplayName("Cliente")]
        public string NombreRutCliente { get; set; }

        [DisplayName("pclid")]
        public string Pclid { get; set; }

        [DisplayName("Deudor")]
        public string NombreRutDeudor { get; set; }

        [DisplayName("ctcid")]
        public string Ctcid { get; set; }

        [DisplayName("Reporte")]
        public string Reporte { get; set; }

        [DisplayName("Tipo Cartera")]
        public string TipoCartera { get; set; }

        [DisplayName("Codigo Carga")]
        public string CodigoCarga { get; set; }

        [DisplayName("Codigo Carga")]
        public string Grupo { get; set; }

        [DisplayName("Fecha Baja")]
        public DateTime? FechaBaja { get; set; }

        [DisplayName("Gestor")]
        public string Gestor { get; set; }

        [DisplayName("Fecha Recuperación")]
        public DateTime? FechaDesde { get; set; }

        [DisplayName("Hasta")]
        public DateTime? FechaHasta { get; set; }

        [DisplayName("Grupo Cobranza")]
        public string GrupoCobranza { get; set; }

        [DisplayName("Estado")]
        public string Estados { get; set; }

        public string Gestores { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Enviar Mail a Todos")]
        public bool EmailTodos { get; set; }

        [DisplayName("Enviar Mail Contactos")]
        public bool EmailContacto { get; set; }

        [DisplayName("dcdid")]
        public string Dcdid { get; set; }

        [DisplayName("Tipo Documento")]
        public string TipoDocumento { get; set; }

        [DisplayName("Archivo")]
        public string Archivo { get; set; }

        [DisplayName("URL")]
        public string Ruta { get; set; }

        public int Codemp { get; set; }
    }
}
