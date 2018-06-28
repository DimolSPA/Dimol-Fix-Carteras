using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{

    public class Accion : dto.Accion
    {
        dao.Accion daoAccion = new dao.Accion();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.InsertarAccion((dto.Accion)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    this.IdAccion = (int)id;
                    daoAccion.EditarAccion((dto.Accion)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                    daoAccion.BorrarAccion(objsession.CodigoEmpresa,  (int)id);
                    break;
            }
        }

        public List<dto.Accion> ListarAccionesGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Accion> lstAcciones = new List<dto.Accion>();
            lstAcciones = daoAccion.ListarAccionesGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public List<dto.Accion> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Accion> lstAcciones = new List<dto.Accion>();
            lstAcciones = daoAccion.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstAcciones;
        }

        public static string ListarGrupoAcciones(int idioma)
        {
            dao.Accion daoAccion = new dao.Accion();
            return daoAccion.ListarGrupoAcciones(idioma);
        }

    }

}
