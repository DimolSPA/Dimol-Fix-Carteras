using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class EnteJudicial
    {
        public int Codemp { get; set; }
        public int Id { get; set; }
        public string Rut { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public string Sindico { get; set; }
        public string Abogado { get; set; }
        public string Procurador { get; set; }
        public string Receptor { get; set; }
        public string Nombre { get; set; }
        public string NombreEmpleado { get; set; }
    }
}
