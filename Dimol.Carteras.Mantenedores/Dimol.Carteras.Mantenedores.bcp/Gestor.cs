using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.bcp
{
    public class Gestor : dto.Gestor
    {

        dao.Gestor daoGestor = new dao.Gestor();
        public static string ListarTipoCartera(int idioma)
        {
            dao.Gestor daoGestor = new dao.Gestor();
            return daoGestor.ListarTipoCartera(idioma);
        }

        public static string ListarGrupos(int codemp,int sucursal)
        {
            dao.Gestor daoGestor = new dao.Gestor();
            return daoGestor.ListarGrupos(codemp,sucursal);
        }

        public static string ListarEmpleados(int codemp)
        {
            dao.Gestor daoGestor = new dao.Gestor();
            return daoGestor.ListarEmpleados(codemp);
        }

        public List<dto.Gestor> ListarGestorGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite,int codsucursal)
        {

            dao.Gestor daoGestor = new dao.Gestor();
            List<dto.Gestor> lstGestor = new List<dto.Gestor>();
            lstGestor = daoGestor.ListarGestorGrilla(codemp, idioma, where, sidx, sord, inicio, limite,codsucursal);
            return lstGestor;
        }

        public List<dto.Gestor> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int codsucursal)
        {
            dao.Gestor daoGestor = new dao.Gestor();
            List<dto.Gestor> lstGestor = new List<dto.Gestor>();
            lstGestor = daoGestor.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite, codsucursal);
            return lstGestor;
        }

        public static int ListarGestorGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int codsucursal)
        {
         return dao.Gestor.ListarGestorGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite,  codsucursal);
        }

        public void OperGestor(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoGestor.InsertarGestor((dto.Gestor)this, objsession.CodigoEmpresa, objsession.CodigoSucursal);
                    break;
                case "edit":
                    daoGestor.EditarGestor((dto.Gestor)this, objsession.CodigoEmpresa, objsession.CodigoSucursal);
                    break;
                case "del":
                    daoGestor.BorrarGestor(objsession.CodigoEmpresa, id, objsession.CodigoSucursal);
                    break;
            }
        }
    }
}
