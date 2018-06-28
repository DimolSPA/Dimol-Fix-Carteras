using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class MateriaJudicial:dto.MateriaJudicial
    {
        dao.MateriaJudicial daoMateriaJudicial = new dao.MateriaJudicial();
        
        public List<dto.MateriaJudicial> ListarMateriaJudicialGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MateriaJudicial> lstMateriaJudicial = new List<dto.MateriaJudicial>();
            lstMateriaJudicial = daoMateriaJudicial.ListarMateriaJudicialGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMateriaJudicial;
        }


        public List<dto.MateriaJudicial> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MateriaJudicial> lstMateriaJudicial = new List<dto.MateriaJudicial>();
            lstMateriaJudicial = daoMateriaJudicial.ListarMateriaJudicialGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMateriaJudicial;
        }

       public static int ListarMateriaJudicialGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
       {
            return dao.MateriaJudicial.ListarMateriaJudicialGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
       }

       public void OperMateriaJudicial(string oper, int? id, UserSession objsession)
       {
            switch (oper)
            {
                case "add":
                    daoMateriaJudicial.InsertarMateriaJudicial((dto.MateriaJudicial)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoMateriaJudicial.EditarMateriaJudicial((dto.MateriaJudicial)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoMateriaJudicial.BorrarMateriaJudicial(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
