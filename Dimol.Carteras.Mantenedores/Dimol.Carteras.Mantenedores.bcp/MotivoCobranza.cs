using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;

namespace Dimol.Carteras.Mantenedores.bcp
{
    public class MotivoCobranza: dto.MotivoCobranza
    {

        dao.MotivoCobranza daoMotivoCobranza = new dao.MotivoCobranza();
        public void OperMotivoCobranza(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoMotivoCobranza.InsertarMotivoCobranza((dto.MotivoCobranza)this, objsession.CodigoEmpresa);
                    break;
                case "edit":
                    daoMotivoCobranza.EditarMotivoCobranza((dto.MotivoCobranza)this, objsession.CodigoEmpresa);
                    break;
                case "del":
                    daoMotivoCobranza.BorrarMotivoCobranza(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.MotivoCobranza> ListarMotivoCobranzaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MotivoCobranza> lstMotivoCobranza = new List<dto.MotivoCobranza>();
            lstMotivoCobranza = daoMotivoCobranza.ListarMotivoCobranzaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMotivoCobranza;
        }

        public static int ListarMotivoCobranzaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
           return  dao.MotivoCobranza.ListarMotivoCobranzaGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }


        public List<dto.MotivoCobranza> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MotivoCobranza> lstMotivoCobranza = new List<dto.MotivoCobranza>();
            lstMotivoCobranza = daoMotivoCobranza.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMotivoCobranza;
        }
    }
}
