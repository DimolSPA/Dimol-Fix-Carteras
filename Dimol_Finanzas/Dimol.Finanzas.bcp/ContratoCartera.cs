using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;

namespace Dimol.Finanzas.bcp
{
    public class ContratoCartera
    {
        public List<dto.ContratoCartera> ListarContratoCarteraGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            //List<dto.Insumo> lst = new List<dto.Insumo>();
            return dao.ContratoCartera.ListarContratoCarteraGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            //return lst;
        }

        public int ListarContratoCarteraGrillaCount(int codemp, int idioma, string where, string sidx, string sord)
        {
            //int total = 0;
            return dao.ContratoCartera.ListarContratoCarteraGrillaCount(codemp, idioma, where, sidx, sord);
            //return total;
        }

        public static List<Combobox> ListarTipos(int idioma, string first)
        {
            return dao.ContratoCartera.ListarTipos(idioma, first);
        }

        public void BorrarContratoCartera(int codemp, int id)
        {
            dao.ContratoCartera.BorrarContratoCartera(codemp, id);

        }

        public void GuardarTodoClausulas(int codemp, string nom, string tipo)
        {
            dao.ContratoCartera.GuardarTodoClausulas(codemp, nom, tipo);

        }

        /*
        public List<dto.Clausula> ListarClausulasGrilla(int codemp, int idioma, int id)
        {
            //List<dto.Insumo> lst = new List<dto.Insumo>();
            return dao.ContratoCartera.ListarClausulasGrilla(codemp, idioma, id);
            //return lst;
        }

        public int ListarClausulasGrillaCount(int codemp, int idioma, int id, string where, string sidx, string sord)
        {
            //int total = 0;
            return dao.ContratoCartera.ListarClausulasGrillaCount(codemp, idioma, id, where, sidx, sord);
            //return total;
        }
         */
    }
}
