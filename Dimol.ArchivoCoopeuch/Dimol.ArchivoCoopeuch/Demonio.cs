using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.ArchivoCoopeuch.bcp;

namespace Dimol.ArchivoCoopeuch
{
    public class Demonio
    {
        private int Inicio { get; set; }
        private int Termino { get; set; }
        public Demonio(string inicio, string termino)
        {
            this.Termino = string.IsNullOrEmpty(termino) ? 0 : Int32.Parse(termino);
            this.Inicio = string.IsNullOrEmpty(inicio) ? 0 : Int32.Parse(inicio);
        }
        public void Start()
        {
            bcp.ArchivoCoopeuch.ListarGestionesCobralex();
        }
    }
}
