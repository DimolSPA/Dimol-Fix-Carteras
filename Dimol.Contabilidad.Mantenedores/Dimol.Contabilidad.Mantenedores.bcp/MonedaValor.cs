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
    public class MonedaValor : dto.MonedaValor
    {
        dao.MonedaValor daoAccion = new dao.MonedaValor();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar((dto.MonedaValor)this, objsession.CodigoEmpresa);

                    break;
                case "edit":

                    this.Id = (int)id;
                    daoAccion.Editar((dto.MonedaValor)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoAccion.Borrar((dto.MonedaValor)this, objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.MonedaValor> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MonedaValor> lstAcciones = new List<dto.MonedaValor>();
            lstAcciones = daoAccion.ListarGrilla(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.MonedaValor> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MonedaValor> lstAcciones = new List<dto.MonedaValor>();
            lstAcciones = daoAccion.ExportarExcel(codemp, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }


        public int ListarMonedasValoresCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarMonedasValoresCount(codemp, where, sidx, sord, inicio, limite);
            return total;
        }

        public static string ListarMonedas(int codemp)
        {
            string salida = "";
            dao.MonedaValor daoAccion = new dao.MonedaValor();
            salida = daoAccion.ListarMonedas(codemp);

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
