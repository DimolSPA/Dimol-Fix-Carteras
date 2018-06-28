using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class BuscarDeudor
    {
        public string RutCliente { get; set; }
        public string Pclid { get; set; }
        public string NombreCliente { get; set; }
        public string Rut { get; set; }
        public string Ctcid { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreFantasia { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Rol { get; set; }
        public string SituacionCartera { get; set; }
        public string NumeroCPBT { get; set; }
        public string Gestor { get; set; }
        public string Gesid { get; set; }
        public int Id { set; get; }
        public string TipoCliente { get; set; }

    }
}
