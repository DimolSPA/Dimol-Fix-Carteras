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
    public class ClasificacionDocumentos : dto.ClasificacionDocumentos
    {
        dao.ClasificacionDocumentos daoAccion = new dao.ClasificacionDocumentos();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar((dto.ClasificacionDocumentos)this, objsession.CodigoEmpresa);

                    break;
                case "edit":

                    this.Id = (int)id;
                    daoAccion.Editar((dto.ClasificacionDocumentos)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoAccion.Borrar(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.ClasificacionDocumentos> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ClasificacionDocumentos> lstAcciones = new List<dto.ClasificacionDocumentos>();
            lstAcciones = daoAccion.ListarGrilla(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.ClasificacionDocumentos> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ClasificacionDocumentos> lstAcciones = new List<dto.ClasificacionDocumentos>();
            lstAcciones = daoAccion.ExportarExcel(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }


        public int ListarClasificacionDocumentosCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarClasificacionDocumentosCount(codemp, where, sidx, sord, inicio, limite);
            return total;
        }

        public static string ListarTipoComprobante(int codemp, int idid)
        {
            string salida = "";
            dao.ClasificacionDocumentos daoAccion = new dao.ClasificacionDocumentos();
            salida = daoAccion.ListarTipoComprobante(codemp, idid);

            return salida;
        }

        public static string ListarTipoProducto(int codemp, int idid)
        {
            string salida = "";
            dao.ClasificacionDocumentos daoAccion = new dao.ClasificacionDocumentos();
            salida = daoAccion.ListarTipoProducto(codemp, idid);

            return salida;
        }

        public static string ListarConcepto(int codemp, int idid)
        {
            string salida = "";
            dao.ClasificacionDocumentos daoAccion = new dao.ClasificacionDocumentos();
            salida = daoAccion.ListarConcepto(codemp, idid);

            return salida;
        }

        public static string ListarMovimiento(int codemp, int idid)
        {
            string salida = "";
            dao.ClasificacionDocumentos daoAccion = new dao.ClasificacionDocumentos();
            salida = daoAccion.ListarMovimiento(codemp, idid);

            return salida;
        }

        public static string ListarCuentas(int codemp)
        {
            string salida = "";
            dao.ClasificacionDocumentos daoAccion = new dao.ClasificacionDocumentos();
            salida = daoAccion.ListarCuentas(codemp);

            return salida;
        }

        public static string ListarStock()
        {
            string salida = "";
            dao.ClasificacionDocumentos daoAccion = new dao.ClasificacionDocumentos();
            salida = daoAccion.ListarStock();

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
