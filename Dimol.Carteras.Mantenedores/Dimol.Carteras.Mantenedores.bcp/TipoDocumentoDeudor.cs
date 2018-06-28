using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.bcp
{
    public class TipoDocumentoDeudor : dto.TipoDocumentoDeudor
    {
        dao.TipoDocumentoDeudor daoTipoDocumentoDeudor = new dao.TipoDocumentoDeudor();

        public List<dto.TipoDocumentoDeudor> ListarTipoDocumentoDeudorGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoDocumentoDeudor> lstTipoDocumentoDeudor = new List<dto.TipoDocumentoDeudor>();
            lstTipoDocumentoDeudor = daoTipoDocumentoDeudor.ListarTipoDocumentoDeudorGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTipoDocumentoDeudor;
        }

        public List<dto.TipoDocumentoDeudor> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoDocumentoDeudor> lstTipoDocumentoDeudor = new List<dto.TipoDocumentoDeudor>();
            lstTipoDocumentoDeudor = daoTipoDocumentoDeudor.ListarTipoDocumentoDeudorGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTipoDocumentoDeudor;
        }

        public static int ListarTipoDocumentoDeudorGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TipoDocumentoDeudor.ListarTipoDocumentoDeudorGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperTipoDocumentoDeudor(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoTipoDocumentoDeudor.InsertarTipoDocumentoDeudor((dto.TipoDocumentoDeudor)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoTipoDocumentoDeudor.EditarTipoDocumentoDeudor((dto.TipoDocumentoDeudor)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoTipoDocumentoDeudor.BorrarTipoDocumentoDeudor(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
