using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class Ciudad:dto.Ciudad
    {

        dao.Ciudad daoCiudad = new dao.Ciudad();

        public static string ListarPaises()
        {
            dao.Ciudad daoCiudad = new dao.Ciudad();
            return daoCiudad.ListarPaises();
        }


        public static string ListarRegiones(int idPais)
        {
            dao.Ciudad daoCiudad = new dao.Ciudad();
            return daoCiudad.ListarRegiones(idPais);
        }

        public static string ListarRegiones()
        {
            dao.Ciudad daoCiudad = new dao.Ciudad();
            return daoCiudad.ListarRegiones();
        }

        public List<dto.Ciudad> ListarCiudadGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Ciudad> lstCiudad = new List<dto.Ciudad>();
            lstCiudad = daoCiudad.ListarCiudadGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstCiudad;
        }

        public List<dto.Ciudad> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Ciudad> lstCiudad = new List<dto.Ciudad>();
            lstCiudad = daoCiudad.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstCiudad;
        }

        public static int ListarCiudadGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Ciudad.ListarCiudadGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperCiudad(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoCiudad.InsertarCiudad((dto.Ciudad)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoCiudad.EditarCiudad((dto.Ciudad)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoCiudad.BorrarCiudad((int)id);
                    break;
            }
        }
    }
}
