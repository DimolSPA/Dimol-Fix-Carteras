using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.bcp
{
    public class Talonario : dto.Talonario
    {
        public static List<Combobox> ListarTalonarios(int codemp, string first)
        {
            return dao.Talonario.ListarTalonarios(codemp, first);
        }

        public static dto.Talonario getTalonarioPorId(int codemp, int id)
        {
            return dao.Talonario.getTalonarioPorId(codemp, id);
        }

        public static List<dto.Talonario> ListarTalonariosSinAsignar(int codemp, int id, int suc, string where, string sidx, string sord)
        {
            return dao.Talonario.ListarTalonariosSinAsignar(codemp, id, suc, where, sidx, sord);
        }

        public static List<dto.Talonario> ListarTalonariosAsignados(int codemp, int id, int suc, string where, string sidx, string sord)
        {
            return dao.Talonario.ListarTalonariosAsignados(codemp, id, suc, where, sidx, sord);
        }

        public static int Insertar(dto.Talonario objAccion, int codemp, int sucursal)
        {
            return dao.Talonario.Insertar(objAccion, codemp, sucursal);
        }
    }
}
