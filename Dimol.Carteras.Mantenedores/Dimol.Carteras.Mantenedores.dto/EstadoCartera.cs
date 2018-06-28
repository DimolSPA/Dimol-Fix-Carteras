using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.dto
{
    public class EstadoCartera
    {

        public int Codemp { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdAgrupa { get; set; }
        public string Agrupa { get; set; }
        public string Utiliza { get; set; }
        public string PredJu { get; set; }
        public string SolFecha { get; set; }
        public string GenRet { get; set; }
        public string Compromiso { get; set; }
    }
}
