using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class Pais:dto.Pais
    {
        dao.Pais daoPais = new dao.Pais();
        public List<dto.Pais> ListarPaisGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Pais> lstPais = new List<dto.Pais>();
            lstPais = daoPais.ListarPaisGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstPais;
        }

        public List<dto.Pais> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Pais> lstPais = new List<dto.Pais>();
            lstPais = daoPais.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstPais;
        }

        public static int ListarPaisGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Pais.ListarPaisGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }


        public void OperPais(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoPais.InsertarPais((dto.Pais)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoPais.EditarPais((dto.Pais)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoPais.BorrarPais((int)id);
                    break;
            }
        }
    }
}
