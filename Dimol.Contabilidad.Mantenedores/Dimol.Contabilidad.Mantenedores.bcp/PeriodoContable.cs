using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Dimol.Contabilidad.Mantenedores.bcp
{
    public class PeriodoContable : dto.PeriodoContable
    {
        dao.PeriodoContable daoAccion = new dao.PeriodoContable();

        public void OperPeriodoContable(string oper, int? id, UserSession objsession)
         {
             switch (oper)
             {
                 case "add":
                     daoAccion.InsertarPeriodo((dto.PeriodoContable)this, objsession.CodigoEmpresa);

                     break;
                 case "edit":
                     Debug.WriteLine("OPERACION EDIT ");
                     this.IdPeriodo = (int)id;

                     daoAccion.EditarPeriodo((dto.PeriodoContable)this, objsession.CodigoEmpresa, objsession.UserId);
                     break;
                 case "del":
                     daoAccion.BorrarPeriodo(objsession.CodigoEmpresa, id);
                     break;
             }
         }
         

        public List<dto.PeriodoContable> ListarPeriodosGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PeriodoContable> lstAcciones = new List<dto.PeriodoContable>();
            lstAcciones = daoAccion.ListarPeriodosGrilla(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.PeriodoContable> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PeriodoContable> lstAcciones = new List<dto.PeriodoContable>();
            lstAcciones = daoAccion.ExportarExcel(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public int ListarPeriodoContableCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarPeriodoContableCount(codemp, where, sidx, sord, inicio, limite);
            return total;
        }

        

    }
}
