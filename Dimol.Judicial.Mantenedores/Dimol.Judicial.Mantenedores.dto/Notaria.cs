using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class Notaria
    {
        public int Codemp { get; set; }
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string NombreNotaria { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public int IdComuna { get; set; }
        public string Direccion { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Fax { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
      
    }
}
