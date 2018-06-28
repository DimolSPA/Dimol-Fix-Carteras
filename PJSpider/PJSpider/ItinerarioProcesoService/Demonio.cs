using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItinerarioProcesoService
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
            while (1 == 1)
            {
                ItinerarioServicios.bcp.ProcesoItinerario.ListarProcesosItinerario();
                //agregar timer 10 min 
                //Thread.Sleep(1000 * 60 * 10);  // 600,000 ms = 600 sec = 10 min
                System.Threading.Thread.Sleep(600000); 
            }
            
        }
    }
}
