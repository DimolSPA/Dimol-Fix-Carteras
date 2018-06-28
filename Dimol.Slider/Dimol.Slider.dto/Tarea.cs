using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Slider.dto
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Observacion { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaTarea { get; set; }
        public int Activa { get; set; }
        public int Completa { get; set; }
        public string FechaCompleta { get; set; }
        public int Lunes { get; set; }
        public int Martes { get; set; }
        public int Miercoles { get; set; }
        public int Jueves { get; set; }
        public int Viernes { get; set; }
    }
}
