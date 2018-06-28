using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.bcp
{
    public class TipoRetiroEntrega : dto.TipoRetiroEntrega 
    {

        dao.TipoRetiroEntrega daoTipoRetiroEntrega = new dao.TipoRetiroEntrega();

        public List<dto.TipoRetiroEntrega> ListarTipoRetiroEntregaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {

            List<dto.TipoRetiroEntrega> lstTipoRetiroEntrega = new List<dto.TipoRetiroEntrega>();
            lstTipoRetiroEntrega = daoTipoRetiroEntrega.ListarTipoRetiroEntregaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTipoRetiroEntrega;
        }


        public static int ListarTipoRetiroEntregaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TipoRetiroEntrega.ListarTipoRetiroEntregaGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperTipoRetiroEntrega(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoTipoRetiroEntrega.InsertarTipoRetiroEntrega((dto.TipoRetiroEntrega)this, objsession.CodigoEmpresa,objsession.Idioma );
                    break;
                case "edit":
                    daoTipoRetiroEntrega.EditarTipoRetiroEntrega((dto.TipoRetiroEntrega)this, objsession.CodigoEmpresa,objsession.Idioma);
                    break;
                case "del":
                    daoTipoRetiroEntrega.BorrarTipoRetiroEntrega(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }

        public List<dto.TipoRetiroEntrega> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoRetiroEntrega> lstTipoRetiroEntrega = new List<dto.TipoRetiroEntrega>();
            lstTipoRetiroEntrega = daoTipoRetiroEntrega.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstTipoRetiroEntrega;
        }

    }
}
