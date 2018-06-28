using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class DeudorQuiebra
    {
        public static int ListarDeudoresQuiebraCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.DeudorQuiebra.ListarDeudoresQuiebraCount(codemp, where, sidx, sord, inicio, limite);
        }
        public static List<dto.DeudorQuiebra> ListarDeudoresQuiebra(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.DeudorQuiebra.ListarDeudoresQuiebra(codemp, where, sidx, sord, inicio, limite);
        }

        public static int InsertarDeudorQuiebra(int codemp, string rut, string nombre, string rolNumero, int tribumalId, string tipoCausa, string materia, int userId)
        {
            int result = -1;
            result = dao.DeudorQuiebra.InsertarDeudorQuiebra(codemp, rut, nombre, rolNumero, tribumalId, tipoCausa, materia, userId);
            return result;
        }
        public static List<dto.DeudorQuiebra> ListarDeudorQuiebra(int codemp, string rut)
        {
            return dao.DeudorQuiebra.ListarDeudorQuiebra(codemp, rut);
        }
        public static int ActualizarDeudorQuiebra(int codemp, string rut, string tipoCausaId, string materiaId)
        {
            return dao.DeudorQuiebra.ActualizarDeudorQuiebra(codemp, rut, tipoCausaId, materiaId);
        }
        public static string TraeDeudorQuiebraRolNumero(int codemp, string rut)
        {
            return dao.DeudorQuiebra.TraeDeudorQuiebraRolNumero(codemp, rut);
        }
        public static int TraeDeudorQuiebraIdRol(int codemp, int pclid, int ctcid, string rolNumero)
        {
            return dao.DeudorQuiebra.TraeDeudorQuiebraIdRol(codemp, pclid, ctcid, rolNumero);
        }
    }
}
