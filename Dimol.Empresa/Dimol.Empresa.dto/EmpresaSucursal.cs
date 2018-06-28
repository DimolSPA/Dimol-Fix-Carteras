using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dto
{
    public class EmpresaSucursal
    {
        public int CodEmp { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdComuna { get; set; }
        public string Comuna { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Css { get; set; }
        public string Matriz { get; set; }

    }
}
