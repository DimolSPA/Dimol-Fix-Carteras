using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Dimol.Contabilidad.Mantenedores.bcp
{
    public class TiposCausaGuias : dto.TiposCausaGuias
    {
        dao.TiposCausaGuias daoAccion = new dao.TiposCausaGuias();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar((dto.TiposCausaGuias)this, objsession.CodigoEmpresa, objsession.Idioma);

                    break;
                case "edit":

                    this.Id = (int)id;
                    daoAccion.Editar((dto.TiposCausaGuias)this, objsession.CodigoEmpresa, (int)id, objsession.Idioma);
                    break;
                case "del":
                    daoAccion.Borrar(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.TiposCausaGuias> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposCausaGuias> lstAcciones = new List<dto.TiposCausaGuias>();
            lstAcciones = daoAccion.ListarGrilla(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.TiposCausaGuias> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposCausaGuias> lstAcciones = new List<dto.TiposCausaGuias>();
            lstAcciones = daoAccion.ExportarExcel(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }


        public int ListarTiposCausaGuiasCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarTiposCausaGuiasCount(codemp, where, sidx, sord, inicio, limite);
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
