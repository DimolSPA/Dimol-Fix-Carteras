using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.bcp
{
    public class GrupoCobranza : dto.GrupoCobranza
    {
        dao.GrupoCobranza daoGrupoCobranza = new dao.GrupoCobranza();

        public static string ListarEmpleadosGrupoCobranza(int codemp)
        {
            dao.GrupoCobranza daoGrupoCobranza = new dao.GrupoCobranza();
            return daoGrupoCobranza.ListarEmpleadosGrupoCobranza(codemp);
        }

        public List<dto.GrupoCobranza> ListaGrupoCobranzaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int idsuc)
        {
            dao.GrupoCobranza daoGrupoCobranza = new dao.GrupoCobranza();
            List<dto.GrupoCobranza> lstGrupoCobranza = new List<dto.GrupoCobranza>();
            lstGrupoCobranza = daoGrupoCobranza.ListarGrupoCobranzaGrilla(codemp, idioma, where, sidx, sord, inicio, limite, idsuc);
            return lstGrupoCobranza;
        }
        public List<dto.GrupoCobranza> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int idsuc)
        {
            dao.GrupoCobranza daoCodigoCarga = new dao.GrupoCobranza();
            List<dto.GrupoCobranza> lstGrupoCobranza = new List<dto.GrupoCobranza>();
            lstGrupoCobranza = daoGrupoCobranza.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite, idsuc);
            return lstGrupoCobranza;
        }


        public static int ListarGrupoCobranzaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, int idsuc)
        {
            return dao.GrupoCobranza.ListarGrupoCobranzaGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite, idsuc);
        }

        public void OperGrupoCobranza(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoGrupoCobranza.InsertarGrupoCobranza((dto.GrupoCobranza)this, objsession.CodigoEmpresa, objsession.CodigoSucursal);
                    break;
                case "edit":
                    daoGrupoCobranza.EditarGrupoCobranza((dto.GrupoCobranza)this, objsession.CodigoEmpresa, objsession.CodigoSucursal);
                    break;
                case "del":
                    daoGrupoCobranza.BorrarGrupoCobranza(objsession.CodigoEmpresa, id, objsession.CodigoSucursal);
                    break;
            }
        }
    }
}
