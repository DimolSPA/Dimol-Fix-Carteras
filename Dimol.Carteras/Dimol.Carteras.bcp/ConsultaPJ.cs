using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
{
    public class ConsultaPJ
    {
        public static List<Dimol.Carteras.dto.ConsultaPJ> ConsultarPorRut(string rut)
        {
            return dao.ConsultaPJ.ConsultarPorRut(rut);
        }
    }
}
