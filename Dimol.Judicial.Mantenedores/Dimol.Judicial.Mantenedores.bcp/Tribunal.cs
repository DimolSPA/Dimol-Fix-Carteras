using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class Tribunal : dto.Tribunal
    {
        dao.Tribunal daoTribunal = new dao.Tribunal();

        public static string ListarTiposTribunal(int codemp, int idioma)
        {
            dao.Tribunal daoTribunal = new dao.Tribunal();
            return daoTribunal.ListarTiposTribunal(codemp, idioma);
        }

        public static string ListarBancos(int codemp)
        {
            dao.Tribunal daoTribunal = new dao.Tribunal();
            return daoTribunal.ListarBancos(codemp);
        }

        public List<dto.Tribunal> ListarTribunalGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Tribunal> lstTribunal = new List<dto.Tribunal>();
            lstTribunal = daoTribunal.ListarTribunalGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTribunal;
        }

        public List<dto.Tribunal> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Tribunal> lstTribunal = new List<dto.Tribunal>();
            lstTribunal = daoTribunal.ListarTribunalGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTribunal;
        }

        public static int ListarTribunalGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Tribunal.ListarTribunalGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperTribunal(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoTribunal.InsertarTribunal((dto.Tribunal)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoTribunal.EditarTribunal((dto.Tribunal)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoTribunal.BorrarTribunal(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
