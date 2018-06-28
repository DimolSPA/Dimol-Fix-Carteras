using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class EmpresaConfiguracion:dto.EmpresaConfiguracion
    {

        dao.EmpresaConfiguracion daoEmpresaConfiguracion = new dao.EmpresaConfiguracion();
        public List<dto.EmpresaConfiguracion> ListarEmpresaConfiguracionGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EmpresaConfiguracion> lstEmpresaConfiguracion = new List<dto.EmpresaConfiguracion>();
            lstEmpresaConfiguracion = daoEmpresaConfiguracion.ListarEmpresaConfiguracionGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstEmpresaConfiguracion;
        }

        public List<dto.EmpresaConfiguracion> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EmpresaConfiguracion> lstEmpresaConfiguracion = new List<dto.EmpresaConfiguracion>();
            lstEmpresaConfiguracion = daoEmpresaConfiguracion.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstEmpresaConfiguracion;
        }

        public static int ListarEmpresaConfiguracionGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.EmpresaConfiguracion.ListarEmpresaEmpresaConfiguracionCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperEmpresaConfiguracion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoEmpresaConfiguracion.InsertarEmpresaConfiguracion((dto.EmpresaConfiguracion)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoEmpresaConfiguracion.EditarEmpresaConfiguracion((dto.EmpresaConfiguracion)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoEmpresaConfiguracion.BorrarEmpresaConfiguracion(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
