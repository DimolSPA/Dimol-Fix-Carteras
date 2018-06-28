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
    public class TiposTraslado : dto.TiposTraslado
    {
        dao.TiposTraslado daoAccion = new dao.TiposTraslado();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar((dto.TiposTraslado)this, objsession.CodigoEmpresa, objsession.Idioma);

                    break;
                case "edit":

                    this.Id = (int)id;
                    daoAccion.Editar((dto.TiposTraslado)this, objsession.CodigoEmpresa, (int)id, objsession.Idioma);
                    break;
                case "del":
                    daoAccion.Borrar(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.TiposTraslado> ListarGrilla(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposTraslado> lstAcciones = new List<dto.TiposTraslado>();
            lstAcciones = daoAccion.ListarGrilla(codemp, idid, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.TiposTraslado> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposTraslado> lstAcciones = new List<dto.TiposTraslado>();
            lstAcciones = daoAccion.ExportarExcel(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }


        public int ListarTiposTrasladoCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarTiposTrasladoCount(codemp, idid, where, sidx, sord, inicio, limite);
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
