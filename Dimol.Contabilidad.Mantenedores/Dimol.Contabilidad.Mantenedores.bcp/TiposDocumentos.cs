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
    public class TiposDocumentos : dto.TiposDocumentos
    {
        dao.TiposDocumentos daoAccion = new dao.TiposDocumentos();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar((dto.TiposDocumentos)this, objsession.CodigoEmpresa, objsession.Idioma);

                    break;
                case "edit":

                    this.Id = (int)id;
                    daoAccion.Editar((dto.TiposDocumentos)this, objsession.CodigoEmpresa, objsession.Idioma, (int)id);
                    break;
                case "del":
                    daoAccion.Borrar(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.TiposDocumentos> ListarGrilla(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposDocumentos> lstAcciones = new List<dto.TiposDocumentos>();
            lstAcciones = daoAccion.ListarGrilla(codemp, idid, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.TiposDocumentos> ExportarExcel(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposDocumentos> lstAcciones = new List<dto.TiposDocumentos>();
            lstAcciones = daoAccion.ExportarExcel(codemp, idid, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }


        public int ListarTiposDocumentosCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarTiposDocumentosCount(codemp, idid, where, sidx, sord, inicio, limite);
            return total;
        }

        public static string ListarTipoComprobante(int codemp)
        {
            string salida = "";
            dao.TiposDocumentos daoAccion = new dao.TiposDocumentos();
            salida = daoAccion.ListarTipoComprobante(codemp);

            return salida;
        }

        public static string ListarTipoDocCaja(int codemp, int idid)
        {
            string salida = "";
            dao.TiposDocumentos daoAccion = new dao.TiposDocumentos();
            salida = daoAccion.ListarTipoDocCaja(codemp, idid);

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
