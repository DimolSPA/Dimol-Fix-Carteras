using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp 

{
    public class Idioma :dto.Idioma
    {
        dao.Idioma daoIdioma = new dao.Idioma();
        public List<dto.Idioma> ListarIdiomaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Idioma> lstIdiomas = new List<dto.Idioma>();
            lstIdiomas = daoIdioma.ListarIdiomaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstIdiomas;
        }

        public List<dto.Idioma> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Idioma> lstIdiomas = new List<dto.Idioma>();
            lstIdiomas = daoIdioma.ListarIdiomaGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstIdiomas;
        }

        public static int ListarIdiomaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Idioma.ListarIdiomaGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
       
        }


        public void OperIdioma(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoIdioma.InsertarIdioma((dto.Idioma)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    //this.IdAccion = (int)id;
                    daoIdioma.EditarIdioma((dto.Idioma)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoIdioma.BorrarIdioma((int)id);
                    break;
            }
        }
    }
}

