using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.bcp
{
    public class CriterioFacturacion
    {
        public static List<dto.CriterioFacturacion> ListarCriteriosFacturacionGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.CriterioFacturacion.ListarCriteriosFacturacionGrilla(codemp, where, sidx, sord);
        }
    }
}
