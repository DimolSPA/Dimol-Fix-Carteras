using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class EnteJudicial : dto.EnteJudicial
    {
        dao.EnteJudicial daoEnteJudicial = new dao.EnteJudicial();

        public static string ListarProvcli(int codemp, int idioma)
        {
            dao.EnteJudicial daoEnteJudicial = new dao.EnteJudicial();
            return daoEnteJudicial.ListarProvcli(codemp, idioma);
        }

        public static string ListarEmpleados(int codemp, int idioma)
        {
            dao.EnteJudicial daoEnteJudicial = new dao.EnteJudicial();
            return daoEnteJudicial.ListarEmpleados(codemp, idioma);
        }

        public List<dto.EnteJudicial> ListarEnteJudicialGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EnteJudicial> lstEnteJudicial = new List<dto.EnteJudicial>();
            lstEnteJudicial = daoEnteJudicial.ListarEnteJudicialGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstEnteJudicial;
        }

        public List<dto.EnteJudicial> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EnteJudicial> lstEnteJudicial = new List<dto.EnteJudicial>();
            lstEnteJudicial = daoEnteJudicial.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstEnteJudicial;
        }

        public static int ListarEnteJudicialGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.EnteJudicial.ListarEnteJudicialGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperEnteJudicial(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoEnteJudicial.InsertarEnteJudicial((dto.EnteJudicial)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoEnteJudicial.EditarEnteJudicial((dto.EnteJudicial)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoEnteJudicial.BorrarEnteJudicial(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }

        public static List<dto.EnteJudicial> ListarRolEnteJudicialGrilla(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.EnteJudicial.ListarRolEnteJudicialGrilla( codemp, rolid,  where, sidx,  sord, inicio, limite);
        }

        public static int ListarRolEnteJudicialGrillaCount(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.EnteJudicial.ListarRolEnteJudicialGrillaCount(codemp, rolid, where, sidx, sord, inicio, limite);
        }

        public static List<Combobox> ListarEntes(int codemp, string first)
        {
            return dao.EnteJudicial.ListarEntes(codemp, first);
        }

        public static int InsertarEnteJudicialRol( int codemp, int etjid, int rolid)
        {
            return dao.EnteJudicial.InsertarEnteJudicialRol(codemp, etjid, rolid);
        }

        public static int EliminarEnteJudicialRol(int codemp, int etjid, int rolid)
        {
            return dao.EnteJudicial.EliminarEnteJudicialRol(codemp, etjid, rolid);
        }

    }
}
