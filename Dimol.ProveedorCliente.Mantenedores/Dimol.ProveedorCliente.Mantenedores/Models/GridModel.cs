using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Dimol.ProveedorCliente.Mantenedores.Models
{
    public class GridModel
    {
        public string  GridSelect { get; set; }
        public string  GridData { get; set; }
    }

    public class ProveedorClienteModel : dto.ProveedorCliente
    {
        [DisplayName("Rut")]
        public string Rut { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Apellidos")]
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }

        [DisplayName("Nombre Fantasia")]
        public string NombreFantasia { get; set; }

        [DisplayName("Estado")]
        public string Estados { get; set; }

        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        [DisplayName("Nacionalidad")]
        public string Nacionalidad { get; set; }

        [DisplayName("Giro")]
        public string Giro { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Ingreso")]
        public new string FechaIngreso { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Fin")]
        public new string FechaFin { get; set; }

        [DisplayName("Comentario")]
        public string Comentario { get; set; }

        [DisplayName("Rut Rep.Legal")]
        public string RutRepLegal { get; set; }

        [DisplayName("Nombre")]
        public string NombreRepLegal { get; set; }

        [DisplayName("Tipo Cartera")]
        public string TipoCartera { get; set; }

        [DisplayName("Codigo SAP")]
        public string CodigoSAP { get; set; }

        [DisplayName("Usuario")]
        public string Usuario { get; set; }

        [DisplayName("Transportista")]
        public bool Transportista { get; set; }

        [DisplayName("Naviera")]
        public bool Naviera { get; set; }

        [DisplayName("Mostrar en WEB")]
        public bool Mostrar { get; set; }

        [DisplayName("Codigo Sucursal")]
        public string CodigoSucursal { get; set; }

        [DisplayName("Nombre Sucursal")]
        public string NombreSucursal { get; set; }

        [DisplayName("Casa Matriz")]
        public bool CasaMatriz { get; set; }

        [DisplayName("Pais")]
        public string Pais { get; set; }

        [DisplayName("Region")]
        public string Region { get; set; }

        [DisplayName("Ciudad")]
        public string Ciudad { get; set; }

        [DisplayName("Comuna")]
        public string Comuna { get; set; }

        [DisplayName("Direccion")]
        public string Direccion { get; set; }

        [DisplayName("Telefono")]
        public string Telefono { get; set; }

        [DisplayName("Fax")]
        public string Fax { get; set; }

        [DisplayName("Correo")]
        public string Correo { get; set; }

        [DisplayName("Banco")]
        public string Banco { get; set; }

        [DisplayName("Tipo Cuenta")]
        public string TipoCuenta { get; set; }

        [DisplayName("Numero")]
        public string Numero { get; set; }

        [DisplayName("Impuesto")]
        public string Impuesto { get; set; }

        [DisplayName("Tipo Contacto")]
        public string TipoContacto { get; set; }

        [DisplayName("Nombre Contacto")]
        public string NombreContacto { get; set; }

        [DisplayName("Telefono Contacto")]
        public string TelefonoContacto { get; set; }

        [DisplayName("Anexo")]
        public string AnexoContacto { get; set; }

        [DisplayName("Celular Contacto")]
        public string CelularContacto { get; set; }

        [DisplayName("Fax Contacto")]
        public string FaxContacto { get; set; }

        [DisplayName("Correo Contacto")]
        public string CorreoContacto { get; set; }

        [DisplayName("Forma de Pago")]
        public string FormaDePago { get; set; }

        [DisplayName("Utiliza Credito")]
        public bool UtilizaCredito { get; set; }

        [DisplayName("Comentario")]
        public string ComentarioCuentaCorriente { get; set; }

        [DisplayName("Tipo Contrato")]
        public string ContratoCartera { get; set; }

        [DisplayName("Rut")]
        public string RutContrato { get; set; }

        [DisplayName("Nombre")]
        public string NombreContrato { get; set; }

        [DisplayName("Indefinido")]
        public bool Indefinido { get; set; }

        [DisplayName("Intereses Clientes")]
        public bool InteresClientes { get; set; }

        [DisplayName("Honorarios Clientes")]
        public bool HonorariosClientes { get; set; }

        [DisplayName("Sucursal")]
        public string Sucursal { get; set; }

        [DisplayName("Limite Credito")]
        public string LimiteCredito { get; set; }

        [DisplayName("Credito Consumido")]
        public string CreditoConsumido { get; set; }

        [DisplayName("Estado Credito")]
        public string EstadoCredito { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Inicio")]
        public new string FechaInicioContrato { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Fin")]
        public new string FechaFinContrato { get; set; }
    }
}