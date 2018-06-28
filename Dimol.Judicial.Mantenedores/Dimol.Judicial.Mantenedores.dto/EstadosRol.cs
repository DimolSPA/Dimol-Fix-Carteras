using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class EstadosRol
    {
        public int Id { get; set; }
        public string Materia { get; set; }
        public string Cuaderno { get; set; }
        public string Estado { get; set; }
        public DateTime FechaJudicial { get; set; }
        public string Comentario { get; set; }
        public string Usuario { get; set; }
        public string Archivo { get; set; }

        public int IdEstado { get; set; }
        public int IdMateria { get; set; }
        public DateTime Fecha { get; set; }
        public int Rolid { get; set; }
    }
}
