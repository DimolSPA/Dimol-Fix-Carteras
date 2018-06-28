using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesaRolService
{
    public class Demonio
    {
        private string Anio  { get; set; }
        private string Estado { get; set; }
        public Demonio(string anio, string estado)
        {
            this.Estado = estado;
            this.Anio = anio;
        }
        public void Start()
        {
            PJSpider.bcp.Causa.ProcesarRolHTMLDemonio(Anio, Estado);
        }
        
    }
}
