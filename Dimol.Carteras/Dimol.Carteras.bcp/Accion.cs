using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
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

        public static List<Dimol.dto.Combobox> ListarTipoAgrupa(int perfil, int idioma)
        {
            return dao.Accion.ListarTipoAgrupa(perfil, idioma);
        }

        public static List<Dimol.dto.Combobox> ListarAcciones(int codemp, int idioma, string first)
        {
            return dao.Accion.ListarAcciones(codemp, idioma, first);
        }

        public static int BuscarAccionesAgrupa(int codemp, int accid)
        {
            return dao.Accion.BuscarAccionesAgrupa(codemp, accid);
        }

        public static DateTime BuscarUltimaFechaAcciones(int codemp, int pclid, int ctcid, int accid)
        {
            return dao.Accion.BuscarUltimaFechaAcciones(codemp, pclid, ctcid, accid);
        }

    }

}
