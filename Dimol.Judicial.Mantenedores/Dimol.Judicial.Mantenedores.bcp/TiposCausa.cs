using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class TiposCausa : dto.TiposCausa
    {
        dao.TiposCausa daoTiposCausa = new dao.TiposCausa();

        public List<dto.TiposCausa> ListarTiposCausaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposCausa> lstTiposCausa = new List<dto.TiposCausa>();
            lstTiposCausa = daoTiposCausa.ListarTiposCausaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTiposCausa;
        }

        public List<dto.TiposCausa> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposCausa> lstTiposCausa = new List<dto.TiposCausa>();
            lstTiposCausa = daoTiposCausa.ListarTiposCausaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTiposCausa;
        }

        public static int ListarTiposCausaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TiposCausa.ListarTiposCausaGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperTiposCausa(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoTiposCausa.InsertarTiposCausa((dto.TiposCausa)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoTiposCausa.EditarTiposCausa((dto.TiposCausa)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoTiposCausa.BorrarTiposCausa(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
