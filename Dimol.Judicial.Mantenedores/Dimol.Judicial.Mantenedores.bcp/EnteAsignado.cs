using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class EnteAsignado:dto.EnteAsignado
    {
        dao.EnteAsignado daoEnteAsignado = new dao.EnteAsignado();

        public List<dto.EnteAsignado> ListarEnteAsignadoGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, string tipo)
        {
            List<dto.EnteAsignado> lstEnteAsignado = new List<dto.EnteAsignado>();
            lstEnteAsignado = daoEnteAsignado.ListarEnteAsignadoGrilla(codemp, idioma, where, sidx, sord, inicio, limite,tipo);
            return lstEnteAsignado;
        }

        public static int ListarEnteAsignadoGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, string tipo)
        {
            return dao.EnteAsignado.ListarEnteAsignadoGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite,tipo );
        }


        public List<dto.EnteAsignado> ListarEnteParaAsignarGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, string tipo)
        {
            List<dto.EnteAsignado> lstEnteAsignado = new List<dto.EnteAsignado>();
            lstEnteAsignado = daoEnteAsignado.ListarEnteParaAsignarGrilla(codemp, idioma, where, sidx, sord, inicio, limite,tipo);
            return lstEnteAsignado;
        }

        public static int ListarEnteParaAsignarGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, string tipo)
        {
            return dao.EnteAsignado.ListarEnteParaAsignarGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite, tipo);
        }

        public static List<Combobox> ListarTiposEnte(int idioma)
        {
            return dao.EnteAsignado.ListarTiposEnte(idioma);
        }


        public static void GrabarEnte(int codemp, List<String> lst, int rolId)
        {
            foreach (string ids in lst)
            {
                string[] id = ids.Split('|');
                string[] arrayAsignado = id[0].Split(',');
                string[] arrayParaAsignar = id[1].Split(',');
                for (int I = 0; I < arrayAsignado.Length; I++)
                {
                    for (int J = 0; J < arrayParaAsignar.Length; J++)
                        dao.EnteAsignado.GrabarEnte(codemp, int.Parse(arrayAsignado[I].ToString()), int.Parse(arrayParaAsignar[J].ToString()));
                }
                
               
            }
        
        }

    }
}
