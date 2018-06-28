using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dto
{
    public class Gestor
    {
        public int Codemp { get; set; }
        public int CodSucursal { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int IdTipoCartera { get; set; }
        public string TipoCartera { get; set; }
        public string ComKi { get; set; }
        public string ComHon { get; set; }
        public int IdEmpleado { get; set; }
        public string Empleado { get; set; }
        public string Remoto { get; set; }
        public string Estado { get; set; }
        public string ComJKi { get; set; }
        public string ComJHon { get; set; }
        public int IdGrupo { get; set; }
        public string Grupo { get; set; }
    }
}
