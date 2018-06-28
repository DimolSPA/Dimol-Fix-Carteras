using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.bcp
{
    public class CriterioImputacion
    {
        public static List<dto.CriterioImputacion> ListarCriteriosImputacionGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.CriterioImputacion.ListarCriteriosImputacionGrilla(codemp, where, sidx, sord);
        }
    }
}
