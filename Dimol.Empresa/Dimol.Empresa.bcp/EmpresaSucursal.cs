using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class EmpresaSucursal:dto.EmpresaSucursal
    {
        dao.EmpresaSucursal daoEmpresaSucursal = new dao.EmpresaSucursal();

        public static string ListaComunas()
        {
            dao.EmpresaSucursal daoEmpresaSucursal = new dao.EmpresaSucursal();
            return daoEmpresaSucursal.ListaComunas();
        }

        public static int ListarEmpresaSucursalGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.EmpresaSucursal.ListarEmpresaSucursalGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public List<dto.EmpresaSucursal> ListarEmpresaSucursalGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EmpresaSucursal> lstEmpresaSucursal = new List<dto.EmpresaSucursal>();
            lstEmpresaSucursal = daoEmpresaSucursal.ListarEmpresaSucursalGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstEmpresaSucursal;
        }

        public List<dto.EmpresaSucursal> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EmpresaSucursal> lstEmpresaSucursal = new List<dto.EmpresaSucursal>();
            lstEmpresaSucursal = daoEmpresaSucursal.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstEmpresaSucursal;
        }

        public void OperEmpresaSucursal(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoEmpresaSucursal.InsertarEmpresaSucursal((dto.EmpresaSucursal)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoEmpresaSucursal.EditarEmpresaSucursal((dto.EmpresaSucursal)this, objsession.CodigoEmpresa, (int)id);
                    break;
                case "del":
                    daoEmpresaSucursal.BorrarEmpresaSucursal(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
