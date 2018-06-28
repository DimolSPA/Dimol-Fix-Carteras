using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescargarRolExternoService
{
    public class Demonio
    {
        private int Inicio { get; set; }
        private int Termino { get; set; }
        private int Anio { get; set; }
        public Demonio(string inicio, string termino, string anio)
        {
            this.Termino = string.IsNullOrEmpty(termino) ? 0 : Int32.Parse(termino);
            this.Inicio = string.IsNullOrEmpty(inicio) ? 0 : Int32.Parse(inicio);
            this.Anio = string.IsNullOrEmpty(anio) ? 0 : Int32.Parse(anio);
        }
        public void Start()
        {
            PJSpider.bcp.Externo.DescargarRolesExternos (Inicio, Termino, Anio);
        }
    }

}
