using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class TiposTribunal : dto.TiposTribunal
    {
        dao.TiposTribunal daoTiposTribunal = new dao.TiposTribunal();

        public List<dto.TiposTribunal> ListarTiposTribunalGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposTribunal> lstTiposTribunal = new List<dto.TiposTribunal>();
            lstTiposTribunal = daoTiposTribunal.ListarTiposTribunalGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTiposTribunal;
        }

        public List<dto.TiposTribunal> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposTribunal> lstTiposTribunal = new List<dto.TiposTribunal>();
            lstTiposTribunal = daoTiposTribunal.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTiposTribunal;
        }

        public static int ListarTiposTribunalGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TiposTribunal.ListarTiposTribunalGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperTiposTribunal(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoTiposTribunal.InsertarTiposTribunal((dto.TiposTribunal)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoTiposTribunal.EditarTiposTribunal((dto.TiposTribunal)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoTiposTribunal.BorrarTiposTribunal(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
