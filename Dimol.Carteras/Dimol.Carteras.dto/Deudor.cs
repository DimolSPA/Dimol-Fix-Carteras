using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class Deudor
    {
        public int CodigoEmpresa { get; set; }
        public int CodigoDeudor { get; set; }
        public string Rut { get; set; }
        public int Numero { get; set; }
        public string Dv { get; set; }
        public string Nombre { set; get; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string NombreFantasia { set; get; }
        public int IdComuna { get; set; }
        public string Direccion { get; set; }
        public string ParticularEmpresa { set; get; }
        public DateTime FechaIngreso { get; set; }
        public int IdSociedad { get; set; }
        public string Sociedad { get; set; }
        public int EstadoDireccion { get; set; }
        public string Quiebra { get; set; }
        public string NacionalExtranjero { get; set; }
        public int IdRegion { get; set; }
        public int IdCiudad { get; set; }
        public int IdPais { get; set; }
        public string PreQuiebra { get; set; }
        public string Categoria { get; set; }
        public string SolicitaQuiebra { get; set; }
    }

    public class Telefono
    {
        public int Codemp { get; set; }
        public string TipoContacto { get; set; }
        public string NombreContacto { get; set; }
        public long Numero { get; set; }
        public string TipoTelefono { get; set; }
        public string IdEstado { get; set; }
        public string DescEstado { get; set; }
        public int Ticid { get; set; }
        public int Ddcid { get; set; }
        public int EstadoDireccion { get; set; }
        public int Comuna { get; set; }
        public int Ctcid { get; set; }
        public string CodigoArea { get; set; }
        public string  Direccion { get; set; }
        public int Ciudad { get; set; }
        public int Region { get; set; }
        public int Pais { get; set; }
    }

    public class TelefonoDeudor
    {
        public int Codemp { get; set; }
        public int Ctcid { get; set; }
        public long Numero { get; set; }
        public string IdTipoTelefono { get; set; }
        public string TipoTelefono { get; set; }
        public string IdEstadoTelefono { get; set; }
        public string EstadoTelefono { get; set; }
    }

    public class Email
    {
        public int Codemp { get; set; }
        public int Ctcid { get; set; }
        public string TipoContacto { get; set; }
        public string NombreContacto { get; set; }
        public string Mail { get; set; }
        public string Masivo { get; set; }
        public string DescTipo { get; set; }
        public string TipoEmail { get; set; }
        public string IdEstado { get; set; }
        public int Ddcid { get; set; }
        public int EstadoDireccion { get; set; }
        public int Comuna { get; set; }
        public int Ticid { get; set; }
        public string Direccion { get; set; }
        public int Ciudad { get; set; }
        public int Region { get; set; }
        public int Pais { get; set; }
        public string Pclid { get; set; }
        public int UserId { get; set; }
        public DateTime FechaCreacion { get; set; }

    }

    public class EmailDeudor
    {
        public int Codemp { get; set; }
        public int Ctcid { get; set; }
        public string Mail { get; set; }
        public string IdTipoMail { get; set; }
        public string TipoMail { get; set; }
        public string Masivo { get; set; }   
    }

    public class Historial
    {
        public string Tipo { get; set; }
        public DateTime Fecha { set; get; }
        public string Comentario { get; set; }
        public string NombreUsuario { get; set; }
        public string TipoContacto { get; set; }
        public string NombreContacto { get; set; }
        public string Accion { get; set; }
        public string Estado { get; set; }
        public string Agrupa { get; set; }
        public string Utiliza { get; set; }
        public string Ticid { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
    }

    public class Documento
    {
        public int Ctcid { get; set; }
        public int Dcdid { get; set; }
        public string TipoDocumento { get; set; }
        public string  UrlArchivo { get; set; }
        public string NombreArchivo { get; set; }
    }

    public class Rol
    {
        public int Rolid { get; set; }
        public string Cliente { get; set; }
        public string Deudor { get; set; }
        public string NumeroRol { get; set; }
        public string Causa { get; set; }
        public string Tribunal { set; get; }
        public string Materia { get; set; }
        public string Estado { get; set; }
        public string Bloqueo { get; set; }
        public string EstAdm { get; set; }
        public string FechaAccion { get; set; }
        public string AccionJudicial { get; set; }
    }

    public class DocumentoRol
    {
        public int Ccbid { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public DateTime FechaVencimiento { set; get; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
    }

    public class EstadosRol
    {
        public string IdEstado { get; set; }
        public string Materia { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public string Usuario { get; set; }
    }

    public class ArchivoRol
    {
        public int Ctcid { get; set; }
        public int Dcdid { get; set; }
        public string TipoDocumento { get; set; }
        public string UrlArchivo { get; set; }
        public string NombreArchivo { get; set; }
    }

    public class Contacto
    {
        public int Codemp { get; set; }
        public int Ctcid { get; set; }
        public int Ddcid { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string EstadoContacto { get; set; }
        public string Estado { get; set; }
        public string Comuna { set; get; }
        public string Direccion { set; get; }
    }

    public class VisitaTerrenoDetalle
    {
        public int IdVisita { get; set; }
        public int IdVisitaDetalle { get; set; }
        public int Ctcid { get; set; }
        public string EstadoVisita { get; set; }
        public string Visita { get; set; }
        public string Comentarios { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Comuna { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string DireccionEnvio { get; set; }
        public string PosicionEnvio { get; set; }
    }

    public class VisitaTerrenoGPS
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int Altitud { get; set; }
        public string Direccion { get; set; }
        public string Comuna { get; set; }
        public string Ciudad { get; set; }
        
    }
    public class VisitaTerrenoTelefono
    {
        public int Numero { get; set; }
        
    }
}
