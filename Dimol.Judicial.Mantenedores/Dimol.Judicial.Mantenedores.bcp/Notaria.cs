using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class Notaria:dto.Notaria
    {
        dao.Notaria daoNotaria = new dao.Notaria();
        public List<dto.Notaria> ListarNotariaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Notaria> lstNotaria = new List<dto.Notaria>();
            lstNotaria = daoNotaria.ListarNotariaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstNotaria;
        }

        public List<dto.Notaria> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Notaria> lstNotaria = new List<dto.Notaria>();
            lstNotaria = daoNotaria.ListarNotariaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstNotaria;
        }

        public static int ListarNotariaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Notaria.ListarNotariaGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        
        public void OperNotaria(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoNotaria.InsertarNotaria((dto.Notaria)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoNotaria.EditarNotaria((dto.Notaria)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoNotaria.BorrarNotaria(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
