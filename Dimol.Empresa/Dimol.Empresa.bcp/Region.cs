using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class Region:dto.Region
    {
        dao.Region daoRegion = new dao.Region();


        public static string ListarPaises()
        {
            dao.Region daoRegion = new dao.Region();
            return daoRegion.ListarPaises();
        }
        public List<dto.Region> ListarRegionGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Region> lstRegion = new List<dto.Region>();
            lstRegion = daoRegion.ListarRegionGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstRegion;
        }

        public List<dto.Region> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Region> lstRegion = new List<dto.Region>();
            lstRegion = daoRegion.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstRegion;
        }

        public static int ListarRegionGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Region.ListarRegionGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperRegion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoRegion.InsertarRegion((dto.Region)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoRegion.EditarRegion((dto.Region)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoRegion.BorrarRegion((int)id);
                    break;
            }
        }
    }
}
