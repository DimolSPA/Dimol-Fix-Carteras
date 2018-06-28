using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Contabilidad.Mantenedores.bcp
{
    public class Impuesto : dto.Impuesto
    {
        dao.Impuesto daoAccion = new dao.Impuesto();

        public void OperAccion(string oper, int? id, UserSession objsession, int idCC)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar((dto.Impuesto)this, objsession.CodigoEmpresa);

                    break;
                case "edit":

                    this.Id = (int)id;
                    daoAccion.Editar((dto.Impuesto)this, objsession.CodigoEmpresa, (int)id, idCC);
                    break;
                case "del":
                    daoAccion.Borrar(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.Impuesto> ListarGrilla(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Impuesto> lstAcciones = new List<dto.Impuesto>();
            lstAcciones = daoAccion.ListarGrilla(codemp, idid, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public int ListarCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = daoAccion.ListarCount(codemp, idid, where, sidx, sord, inicio, limite);
            return total;
        }

        public List<dto.Impuesto> ExportarExcel(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Impuesto> lstAcciones = new List<dto.Impuesto>();
            lstAcciones = daoAccion.ExportarExcel(codemp, idid, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }


        public static string ListarCuentasContables(int idioma)
        {
            dao.Impuesto daoImpuesto = new dao.Impuesto();
            string salida = "";
            //string salida = "";
            salida = daoImpuesto.ListarCuentasContables(idioma);
            return salida;
        }

    }
}
