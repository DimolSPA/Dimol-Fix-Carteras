using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.bcp
{
    public class CriterioRemesa
    {
        public static List<dto.CriterioRemesa> ListarCriterioRemesaClienteGrilla(int codemp, int pclid, string where, string sidx, string sord)
        {
            return dao.CriterioRemesa.ListarCriterioRemesaClienteGrilla(codemp, pclid, where, sidx, sord);
        }

        public static int InsertUpdateCriterioRemesa(string criterioId, int codemp, int pclid, string desdeDiasVencido, string hastaDiasVencido, string codigoRegion,
                                            string codigoCarga, string capital, string interes, string honorario,
                                            string tipoCambioCapital, int simboloId, int tipoConciliacion, string condicionId, int user)
        {
            int result = 0;
            int existDefinicionCriterio = dao.CriterioRemesa.ExisteDefinicionCriterio(codemp, pclid, desdeDiasVencido, hastaDiasVencido, codigoRegion,
                                                            codigoCarga, tipoConciliacion, condicionId);
            int idCriterio = dao.CriterioRemesa.ExisteCriterio(string.IsNullOrEmpty(criterioId) ? 0 : int.Parse(criterioId));
            if (idCriterio > 0)//Actualizar, existe
            {
                result = dao.CriterioRemesa.ActualizarCriterio(int.Parse(criterioId), codemp, pclid, desdeDiasVencido, hastaDiasVencido, codigoRegion,
                                                            codigoCarga, capital, interes, honorario, tipoCambioCapital, simboloId, tipoConciliacion, condicionId, user);
            }
            else
            {
                if (existDefinicionCriterio == 0)//Si no existe definicion
                    result = dao.CriterioRemesa.InsertarCriterio(codemp, pclid, desdeDiasVencido, hastaDiasVencido, codigoRegion,
                                                            codigoCarga, capital, interes, honorario, tipoCambioCapital, simboloId, tipoConciliacion, condicionId, user);
            }
                
            return result;
        }

        public static string EmpresaFactura(int codemp, int pclid)
        {
            return dao.CriterioRemesa.EmpresaFactura(codemp, pclid);
        }
    }
}
