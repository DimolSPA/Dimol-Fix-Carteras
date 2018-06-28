using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dto
{
    public class Empleado
    {
        public int CodEmp { get; set; }
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Foto { get; set; }
        public int Estado { get; set; }
        public string DescripcionEstado { get; set; }
        
    }
}
