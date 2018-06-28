using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
{
    public class DeudorSII : Carteras.dto.DeudorSII
    {
        public void ListarDatosSII(int ctcid, dto.DeudorSII d)
        {
            dao.DeudorSII.ListarDatosSII(ctcid, d);
        }
    }
}
