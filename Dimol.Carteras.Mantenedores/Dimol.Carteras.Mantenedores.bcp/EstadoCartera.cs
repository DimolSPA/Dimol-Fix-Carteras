using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.Mantenedores.bcp
{
    public  class EstadoCartera: dto.EstadoCartera
    {
        public static string ListarTipoEstadoCartera(int codemp, int permiso)
        {
            dao.EstadoCartera daoEstadoCartera = new dao.EstadoCartera();
            return daoEstadoCartera.ListarTipoEstadoCartera(codemp, permiso);
        }
        public List<dto.EstadoCartera> ListarEstadoCarteraGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {

            dao.EstadoCartera daoGrupoCobranza = new dao.EstadoCartera();
            List<dto.EstadoCartera> lstEstadoCartera = new List<dto.EstadoCartera>();
            lstEstadoCartera = daoGrupoCobranza.ListarEstadoCarteraGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstEstadoCartera;
        }

         public static int ListarEstadoCarteraGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
         {
             return dao.EstadoCartera.ListarEstadoCarteraGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
         }


         public List<dto.EstadoCartera> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
         {
            dao.EstadoCartera daoEstadoCartera = new dao.EstadoCartera();
             List<dto.EstadoCartera> lstEstadoCartera = new List<dto.EstadoCartera>();
             lstEstadoCartera = daoEstadoCartera.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
             return lstEstadoCartera;
         }
 
         public void OperEstadoCartera(string oper, int? id, UserSession objsession)
         {
             dao.EstadoCartera daoEstadoCartera = new dao.EstadoCartera();
            
             switch (oper)
             {
                 case "add":
                     daoEstadoCartera.InsertarEstadoCartera((dto.EstadoCartera)this, objsession.CodigoEmpresa, objsession.Idioma);
                     break;
                 case "edit":
                     daoEstadoCartera.EditarEstadoCartera((dto.EstadoCartera)this, objsession.CodigoEmpresa, objsession.Idioma);
                     break;
                 case "del":
                     daoEstadoCartera.BorrarEstadoCartera(objsession.CodigoEmpresa, Id, objsession.Idioma);
                     break;
             }
         }
    }
}
