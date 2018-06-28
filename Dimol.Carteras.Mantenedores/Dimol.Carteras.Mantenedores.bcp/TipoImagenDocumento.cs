using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.bcp
{
    public class TipoImagenDocumento:dto.TipoImagenDocumento
    {

        dao.TipoImagenDocumento daoTipoImagenDocumento = new dao.TipoImagenDocumento();

        public List<dto.TipoImagenDocumento> ListarTipoImagenDocumentoGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoImagenDocumento> lstTipoImagenDocumento = new List<dto.TipoImagenDocumento>();
            lstTipoImagenDocumento = daoTipoImagenDocumento.ListarTipoImagenDocumentoGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTipoImagenDocumento;
        }

        public List<dto.TipoImagenDocumento> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoImagenDocumento> lstTipoImagenDocumento = new List<dto.TipoImagenDocumento>();
            lstTipoImagenDocumento = daoTipoImagenDocumento.ListarTipoImagenDocumentoGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTipoImagenDocumento;
        }

        public static int ListarTipoImagenDocumentoGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TipoImagenDocumento.ListarTipoImagenDocumentoGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperTipoImagenDocumento(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoTipoImagenDocumento.InsertarTipoImagenDocumento((dto.TipoImagenDocumento)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoTipoImagenDocumento.EditarTipoImagenDocumento((dto.TipoImagenDocumento)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoTipoImagenDocumento.BorrarTipoImagenDocumento(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
