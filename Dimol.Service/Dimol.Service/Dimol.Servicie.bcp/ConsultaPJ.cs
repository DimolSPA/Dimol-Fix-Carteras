using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Service.dao;

namespace Dimol.Service.bcp
{
    public class ConsultaPJ
    {
        public static List<Dimol.Service.dto.ConsultaPJ> ConsultarPorRut(string rut)
        {
            return dao.ConsultaPJ.ConsultarPorRut(rut);
        }
    }
}
