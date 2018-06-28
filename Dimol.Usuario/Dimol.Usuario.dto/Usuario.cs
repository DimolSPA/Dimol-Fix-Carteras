namespace Dimol.Usuario.dto
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IngresosOK { get; set; }
        public int IngresosMalos { get; set; }
        //public string Usuario { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaUltimoIngreso { get; set; }
        public string FechaBloqueo { get; set; }
        public string FechaCambioPassword { get; set; }
        public string Login { get; set; }
        public string Clave { get; set; }
        public string Mail { get; set; }
        public string TipoPregunta { get; set; }
        public string Respuesta { get; set; }
        public string Perfil { get; set; }
        public string Permiso { get; set; }
        public bool CambiPassword { get; set; }
    }
}