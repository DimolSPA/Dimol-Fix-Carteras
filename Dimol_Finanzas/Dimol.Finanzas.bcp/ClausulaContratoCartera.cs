using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;

namespace Dimol.Finanzas.bcp
{
    public class ClausulaContratoCartera
    {

        public List<dto.ClausulaContratoCartera> ListarClausulaContratoCarteraGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            //List<dto.Insumo> lst = new List<dto.Insumo>();
            return dao.ClausulaContratoCartera.ListarClausulaContratoCarteraGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            //return lst;
        }

        public int ListarClausulaContratoCarteraGrillaCount(int codemp, string where, string sidx, string sord)
        {
            //int total = 0;
            return dao.ClausulaContratoCartera.ListarClausulaContratoCarteraGrillaCount(codemp, where, sidx, sord);
            //return total;
        }

        public static List<Combobox> ListarTipos(int idioma, string first)
        {
            return dao.ClausulaContratoCartera.ListarTipos(idioma, first);
        }

        public static List<Combobox> ListarTiposAplicacion(int idioma, string first)
        {
            return dao.ClausulaContratoCartera.ListarTiposAplicacion(idioma, first);
        }

        public static List<Combobox> ListarAreas(int idioma, string first)
        {
            return dao.ClausulaContratoCartera.ListarAreas(idioma, first);
        }

        public static List<Combobox> ListarTiposRango(int idioma, string first)
        {
            return dao.ClausulaContratoCartera.ListarTiposRango(idioma, first);
        }

        public List<dto.ClausulaContratoCartera> ListarClausulaContratoCarteraPorID(int codemp, int idioma, int id)
        {
            //List<dto.Insumo> lst = new List<dto.Insumo>();
            return dao.ClausulaContratoCartera.ListarClausulaContratoCarteraPorID(codemp, idioma, id);
            //return lst;
        }

        public string ListarTipoPorId(int idioma, int id)
        {
            return dao.ClausulaContratoCartera.ListarTipoPorId(idioma, id);
        }

        public static int GrabarClausulaContratoCartera(int codemp, int idioma, dto.ClausulaContratoCartera obj)
        {
            return dao.ClausulaContratoCartera.GrabarClausulaContratoCartera(codemp, idioma, obj);
        }
    }
}
