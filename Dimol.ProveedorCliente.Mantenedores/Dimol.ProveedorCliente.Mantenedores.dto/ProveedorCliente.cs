using System.ComponentModel;

namespace Dimol.ProveedorCliente.Mantenedores.dto
{
    public class ProveedorCliente
    {
        public int Codemp { get; set; }
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreFantasia { get; set; }
        public string Estados { get; set; }
        public string Tipo { get; set; }
        public string Nacionalidad { get; set; }
        [DisplayName("Es Previsional")]
        public bool EsPrevisional { get; set; }
        public string Giro { get; set; }
        public new string FechaIngreso { get; set; }
        public new string FechaFin { get; set; }
        public string Comentario { get; set; }
        public string RutRepLegal { get; set; }
        public string NombreRepLegal { get; set; }
        public string TipoCartera { get; set; }
        public string CodigoSAP { get; set; }
        public string Usuario { get; set; }
        public bool Transportista { get; set; }
        public bool Naviera { get; set; }
        public bool Mostrar { get; set; }
        public string CodigoSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public bool CasaMatriz { get; set; }
        public string Pais { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public string Comuna { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Correo { get; set; }
        public string Banco { get; set; }
        public string TipoCuenta { get; set; }
        public string Numero { get; set; }
        public string Impuesto { get; set; }
        public string TipoContacto { get; set; }
        public string NombreContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string AnexoContacto { get; set; }
        public string CelularContacto { get; set; }
        public string FaxContacto { get; set; }
        public string CorreoContacto { get; set; }
        public string FormaDePago { get; set; }
        public bool UtilizaCredito { get; set; }
        public string ComentarioCuentaCorriente { get; set; }
        public string ContratoCartera { get; set; }
        public string RutContrato { get; set; }
        public string NombreContrato { get; set; }
        public bool Indefinido { get; set; }
        public bool InteresClientes { get; set; }
        public bool HonorariosClientes { get; set; }
        public string Sucursal { get; set; }
        public string LimiteCredito { get; set; }
        public string CreditoConsumido { get; set; }
        public string EstadoCredito { get; set; }
        public new string FechaInicioContrato { get; set; }
        public new string FechaFinContrato { get; set; }
        public string TipoCliente { get; set; }
    }
}
