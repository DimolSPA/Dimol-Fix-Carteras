using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class Comuna:dto.Comuna
    {
        dao.Comuna daoComuna = new dao.Comuna();

        public static string ListarPaises()
        {
            dao.Comuna daoComuna = new dao.Comuna();
            return daoComuna.ListarPaises();
        }

        public static string ListarRegiones()
        {
            dao.Comuna daoComuna = new dao.Comuna();
            return daoComuna.ListarRegiones();
        }

        public static string ListarCiudades()
        {
            dao.Comuna daoComuna = new dao.Comuna();
            return daoComuna.ListarCiudades();
        }

        public List<dto.Comuna> ListarComunaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Comuna> lstComuna = new List<dto.Comuna>();
            lstComuna = daoComuna.ListarComunaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstComuna;
        }

        public List<dto.Comuna> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Comuna> lstComuna = new List<dto.Comuna>();
            lstComuna = daoComuna.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstComuna;
        }

        public static int ListarComunaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comuna.ListarComunaGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperComuna(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoComuna.InsertarComuna((dto.Comuna)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoComuna.EditarComuna((dto.Comuna)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoComuna.BorrarComuna((int)id);
                    break;
            }
        }
    }
}
