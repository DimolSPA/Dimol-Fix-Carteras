using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class Moneda:dto.Moneda
    {
        dao.Moneda daoMoneda = new dao.Moneda();
        public List<dto.Moneda> ListarMonedasGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Moneda> lstMoneda = new List<dto.Moneda>();
            lstMoneda = daoMoneda.ListarMonedaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMoneda;
        }

        public List<dto.Moneda> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Moneda> lstMoneda = new List<dto.Moneda>();
            lstMoneda = daoMoneda.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMoneda;
        }

        public static int ListarMonedaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Moneda.ListarMonedaGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperMoneda(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoMoneda.InsertarMoneda((dto.Moneda)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoMoneda.EditarMoneda((dto.Moneda)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoMoneda.BorrarMoneda(objsession.CodigoEmpresa,(int)id);
                    break;
            }
        }
    }
}
