using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class Mandatos:dto.Mandatos
    {
        dao.Mandatos daoMandatos = new dao.Mandatos();

        public static string ListarNotarias(int codemp)
        {
            dao.Mandatos daoMandatos = new dao.Mandatos();
            return daoMandatos.ListarNotarias(codemp);
        }

        public static string ListarClientes(int codemp)
        {
            dao.Mandatos daoMandatos = new dao.Mandatos();
            return daoMandatos.ListarClientes(codemp);
        }
        

        public List<dto.Mandatos> ListarMandatosGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Mandatos> lstMandatos = new List<dto.Mandatos>();
            lstMandatos = daoMandatos.ListarMandatosGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMandatos;
        }

        public List<dto.Mandatos> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Mandatos> lstMandatos = new List<dto.Mandatos>();
            lstMandatos = daoMandatos.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMandatos;
        }

        public static int ListarMandatosGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Mandatos.ListarMandatosGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperMandatos(string oper, string id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoMandatos.InsertarMandatos((dto.Mandatos)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoMandatos.EditarMandatos((dto.Mandatos)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                     string[] split = id.ToString().Split(new Char[] {'|'});
                     int idCliente = string.IsNullOrEmpty(split[0].ToString()) ? 0 : int.Parse(split[0].ToString());
                     int idNotaria = string.IsNullOrEmpty(split[1].ToString()) ? 0 : int.Parse(split[1].ToString());
                     string numRep = string.IsNullOrEmpty(split[2].ToString()) ? string.Empty :split[2].ToString();
                     daoMandatos.BorrarMandatos(objsession.CodigoEmpresa, idCliente,idNotaria,numRep);
                    break;
            }
        }
    }
}
