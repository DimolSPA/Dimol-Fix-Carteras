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
    public class FormasDePago : dto.FormasDePago
    {
        dao.FormasDePago daoAccion = new dao.FormasDePago();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar((dto.FormasDePago)this, objsession.CodigoEmpresa, objsession.Idioma);

                    break;
                case "edit":

                    this.IdFP = (int)id;
                    daoAccion.Editar((dto.FormasDePago)this, objsession.CodigoEmpresa, (int)id, objsession.Idioma);
                    break;
                case "del":
                    daoAccion.Borrar(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.FormasDePago> ListarGrilla(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.FormasDePago> lstAcciones = new List<dto.FormasDePago>();
            lstAcciones = daoAccion.ListarGrilla(codemp, idid, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.FormasDePago> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.FormasDePago> lstAcciones = new List<dto.FormasDePago>();
            lstAcciones = daoAccion.ExportarExcel(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }


        public int ListarFormasDePagoCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarFormasDePagoCount(codemp, idid, where, sidx, sord, inicio, limite);
            return total;
        }

        public static List<string> ListarTiposDocCaja(int codemp)
        {
            List<string> salida = new List<string>();
            dao.FormasDePago daoAccion = new dao.FormasDePago();
            salida = daoAccion.ListarTiposDocCaja(codemp);
            return salida;
        }

        public static string ListarTiposDocCaja2(int codemp)
        {
            string salida = "";
            dao.FormasDePago daoAccion = new dao.FormasDePago();
            salida = daoAccion.ListarTiposDocCaja2(codemp);
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
