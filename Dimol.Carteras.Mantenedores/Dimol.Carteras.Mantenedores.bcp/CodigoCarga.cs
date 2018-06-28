using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.bcp
{
    public class CodigoCarga: dto.CodigoCarga
    {

        dao.CodigoCarga daoCodigoCarga = new dao.CodigoCarga();
        public void OperCodigoCarga(string oper, string id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoCodigoCarga.InsertarCodigoCarga((dto.CodigoCarga)this, objsession.CodigoEmpresa);
                    break;
                case "edit":
                    daoCodigoCarga.EditarCodigoCarga((dto.CodigoCarga)this, objsession.CodigoEmpresa);
                    break;
                case "del":
                    {
                        string[] split = id.ToString().Split(new Char[] {'|'});
                        int pcliId = string.IsNullOrEmpty (split[1].ToString())? 0: int.Parse (split[1].ToString());
                        int codId=string.IsNullOrEmpty (split[2].ToString())? 0: int.Parse (split[2].ToString());
                        daoCodigoCarga.BorrarCodigoCarga(objsession.CodigoEmpresa, pcliId, codId);
                    }
                    break;
            }
        }

        public static string ListarClientesCodigoCarga(int codemp)
        {
            dao.CodigoCarga daoCodigoCarga = new dao.CodigoCarga();
            return daoCodigoCarga.ListarClientesCodigoCarga(codemp);
        }

        public List<dto.CodigoCarga> ListarCodigoCargaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            dao.CodigoCarga daoCodigoCarga = new dao.CodigoCarga();
            List<dto.CodigoCarga> lstCodigoCarga = new List<dto.CodigoCarga>();
            lstCodigoCarga = daoCodigoCarga.ListarCodigoCargaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstCodigoCarga;
        }

        public List<dto.CodigoCarga> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            dao.CodigoCarga daoCodigoCarga = new dao.CodigoCarga();
            List<dto.CodigoCarga> lstCodigoCarga = new List<dto.CodigoCarga>();
            lstCodigoCarga = daoCodigoCarga.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstCodigoCarga;
        }

        public static int ListarCodigoCargaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.CodigoCarga.ListarCodigoCargaGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }
    }
}
