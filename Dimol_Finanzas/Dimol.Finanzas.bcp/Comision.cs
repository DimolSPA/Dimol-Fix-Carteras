using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;

namespace Dimol.Finanzas.bcp
{
    public class Comision
    {
        public static List<Combobox> ListarAniosComision(int codemp, int codsuc, string first)
        {
            return dao.Comision.ListarAniosComision(codemp, codsuc, first);
        }

        public static List<Combobox> ListarMesesComision(int idioma, string first)
        {
            return dao.Comision.ListarMesesComision(idioma, first);
        }

        public List<dto.Comision> ListarComisionesGrilla(int codemp, int codsuc, int anio, int mes, string where, string sidx, string sord, int inicio, int limite)
        {
            //List<dto.Insumo> lst = new List<dto.Insumo>();
            return dao.Comision.ListarComisionesGrilla(codemp, codsuc, anio, mes, where, sidx, sord, inicio, limite);
            //return lst;
        }

        public int ListarComisionesGrillaCount(int codemp, int codsuc, int anio, int mes, string where, string sidx, string sord)
        {
            //int total = 0;
            return dao.Comision.ListarComisionesGrillaCount(codemp, codsuc, anio, mes, where, sidx, sord);
            //return total;
        }

        public static int GrabarComision(int codemp, int idioma, int codsuc, string desde, string hasta)
        {
            int val = 0;
            val = dao.Comision.GrabarComision(codemp, idioma, codsuc, desde, hasta);
            return val;
        }

    }
}
