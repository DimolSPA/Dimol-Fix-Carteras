using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.bcp
{
    public class MotivoCastigo : dto.MotivoCastigo
    {

        dao.MotivoCastigo daoMotivoCastigo = new dao.MotivoCastigo();
        public List<dto.MotivoCastigo> ListarMotivoCastigoGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MotivoCastigo> lstMotivoCastigo = new List<dto.MotivoCastigo>();
            lstMotivoCastigo = daoMotivoCastigo.ListarMotivoCastigoGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMotivoCastigo;
        }

        public List<dto.MotivoCastigo> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MotivoCastigo> lstMotivoCastigo = new List<dto.MotivoCastigo>();
            lstMotivoCastigo = daoMotivoCastigo.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMotivoCastigo;
        }

        public static int ListarMotivoCastigoGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.MotivoCastigo.ListarMotivoCastigoGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperMotivoCastigo(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoMotivoCastigo.InsertarMotivoCastigo((dto.MotivoCastigo)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoMotivoCastigo.EditarMotivoCastigo((dto.MotivoCastigo)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoMotivoCastigo.BorrarMotivoCastigo(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
