using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dimol.Usuario.Models
{
    public class GridModel
    {
        public string  GridSelect { get; set; }
        public string  GridData { get; set; }
    }

    public class UsuarioModel : dto.Usuario
    {
        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Usuario")]
        public string Usuario { get; set; }

        [DisplayName("Clave")]
        public string Clave { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Tipo Pregunta Secreta")]
        public string TipoPreguntaSecreta { get; set; }

        [DisplayName("Respuesta")]
        public string Respuesta { get; set; }

        [DisplayName("Perfil")]
        public string Perfil { get; set; }

        [DisplayName("Permisos")]
        public string Permisos { get; set; }

        [DisplayName("CambiaPassword")]
        public bool CambiaPassword { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Cambio Password")]
        public new string FechaCambioPassword { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Ingreso")]
        public new string FechaIngreso { get; set; }

        [DisplayName("Ingresos OK")]
        public string IngresosOK { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Ultimo Ingreso")]
        public new string FechaUltimoIngreso { get; set; }

        [DisplayName("Ingresos Malos")]
        public string IngresosMalos { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Fecha Bloqueo")]
        public new string FechaBloqueo { get; set; }

        public UsuarioModel(dto.Usuario objUsuarioDto)
        {
            Rut = objUsuarioDto.Rut;
            Nombre = objUsuarioDto.Nombre;
            Usuario = objUsuarioDto.Login;
            Clave = objUsuarioDto.Clave;
            Email = objUsuarioDto.Mail;
            TipoPreguntaSecreta = objUsuarioDto.TipoPregunta;
            Respuesta = objUsuarioDto.Respuesta;
            Perfil = objUsuarioDto.Perfil;
            Permisos = objUsuarioDto.Permiso;
            CambiaPassword = objUsuarioDto.CambiPassword;
            FechaCambioPassword = objUsuarioDto.FechaCambioPassword;
            FechaIngreso = objUsuarioDto.FechaIngreso;
            IngresosOK = objUsuarioDto.IngresosOK.ToString();
            FechaUltimoIngreso = objUsuarioDto.FechaUltimoIngreso;
            IngresosMalos = objUsuarioDto.IngresosMalos.ToString();
            FechaBloqueo = objUsuarioDto.FechaBloqueo;
            Estado = objUsuarioDto.Estado;
            IdUsuario = objUsuarioDto.IdUsuario;
        }
    }
}