using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.bcp
{
    public class PeriodoContableMes : dto.PeriodoContableMes
    {
        dao.PeriodoContableMes daoAccion = new dao.PeriodoContableMes();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    //daoAccion.InsertarPeriodo((dto.PeriodoContableMes)this, objsession.CodigoEmpresa);

                    break;
                case "edit":

                    this.IdPeriodoMensual = (int)id;
                    daoAccion.EditarPeriodo((dto.PeriodoContableMes)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoAccion.BorrarPeriodo(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.PeriodoContableMes> ListarPeriodosGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PeriodoContableMes> lstAcciones = new List<dto.PeriodoContableMes>();
            lstAcciones = daoAccion.ListarPeriodosGrilla(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.PeriodoContableMes> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PeriodoContableMes> lstAcciones = new List<dto.PeriodoContableMes>();
            lstAcciones = daoAccion.ExportarExcel(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public int ListarPeriodoContableMesCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarPeriodoContableMesCount(codemp, where, sidx, sord, inicio, limite);
            return total;
        }

        /*
        public static string ListarGrupoAcciones(int idioma)
        {
            dao.PeriodoContable daoAccion = new dao.PeriodoContable();
            return daoAccion.ListarGrupoAcciones(idioma);
        }*/

    }
}
