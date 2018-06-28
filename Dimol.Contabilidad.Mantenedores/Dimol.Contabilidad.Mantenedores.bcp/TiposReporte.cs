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
    public class TiposReporte : dto.TiposReporte
    {
        dao.TiposReporte daoAccion = new dao.TiposReporte();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            Debug.WriteLine("IDDDDDDDDDDDDDDDDDDDDD " + id);
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar((dto.TiposReporte)this, objsession.CodigoEmpresa);

                    break;
                case "edit":

                    this.IdTiposReporte = (int)id;
                    daoAccion.Editar((dto.TiposReporte)this, objsession.CodigoEmpresa, (int)id, objsession.Idioma);
                    break;
                case "del":
                    daoAccion.Borrar(objsession.CodigoEmpresa, (int)id, objsession.Idioma);
                    break;
            }
        }


        public List<dto.TiposReporte> ListarGrilla(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposReporte> lstAcciones = new List<dto.TiposReporte>();
            lstAcciones = daoAccion.ListarGrilla(codemp, idid, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.TiposReporte> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposReporte> lstAcciones = new List<dto.TiposReporte>();
            lstAcciones = daoAccion.ExportarExcel(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }


        public int ListarTiposReporteCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarTiposReporteCount(codemp, idid, where, sidx, sord, inicio, limite);
            return total;
        }

        public static List<dto.TiposComprobante> ListarTiposComprobante(int codemp, int idid)
        {
            List<dto.TiposComprobante> lst = new List<dto.TiposComprobante>();
            dao.TiposReporte daoAccion = new dao.TiposReporte();
            lst = daoAccion.ListarTiposComprobante(codemp, idid);
            
            return lst;
        }

        public static string ListarTiposComprobante2(int codemp, int idid)
        {
            string salida = "";
            dao.TiposReporte daoAccion = new dao.TiposReporte();
            salida = daoAccion.ListarTiposComprobante3(codemp, idid);

            return salida;
        }

        public static string ListarReportePadre(int codemp, int idid)
        {
            string salida = "";
            dao.TiposReporte daoAccion = new dao.TiposReporte();
            salida = daoAccion.ListarReportePadre(codemp, idid);
            
            return salida;
        }
        /*
        public static string ListarGrupoAcciones(int idioma)
        {
            dao.PeriodoContable daoAccion = new dao.PeriodoContable();
            return daoAccion.ListarGrupoAcciones(idioma);
        }*/

    }
}
