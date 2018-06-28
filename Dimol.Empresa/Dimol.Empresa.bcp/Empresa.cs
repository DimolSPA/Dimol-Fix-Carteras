using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class Empresa:dto.Empresa
    {

        dao.Empresa daoEmpresa = new dao.Empresa();

        public List<dto.Empresa> ListarEmpresas(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Empresa> lstEmpresa = new List<dto.Empresa>();
            lstEmpresa = daoEmpresa.ListarEmpresa(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstEmpresa;
        }
        
       public static dto.Empresa DatosEmpresa(int codemp)
       {
           return dao.Empresa.DatosEmpresa(codemp);
       }

       public static void EditarDatosEmpresa(dto.Empresa objAccion, int codemp)
       {
           dao.Empresa.EditarDatosEmpresa(objAccion, codemp);
       }
    }
}
