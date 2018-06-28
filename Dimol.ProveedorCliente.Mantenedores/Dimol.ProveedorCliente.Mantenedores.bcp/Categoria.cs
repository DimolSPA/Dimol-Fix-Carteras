using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Dimol.ProveedorCliente.Mantenedores.bcp
{
    public class Categoria : dto.Categoria
    {
        dao.Categoria dao = new dao.Categoria();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    dao.Insertar((dto.Categoria)this, objsession.CodigoEmpresa, objsession.Idioma);

                    break;
                case "edit":

                    this.Id = (int)id;
                    dao.Editar((dto.Categoria)this, objsession.CodigoEmpresa, objsession.Idioma, (int)id);
                    break;
                case "del":
                    dao.Borrar(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.Categoria> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Categoria> lst = new List<dto.Categoria>();
            lst = dao.ListarGrilla(codemp, where, sidx, sord, inicio, limite);
            return lst;
        }

        public List<dto.Categoria> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Categoria> lst = new List<dto.Categoria>();
            lst = dao.ExportarExcel(codemp, where, sidx, sord, inicio, limite);
            return lst;
        }


        public int ListarCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            int total = 0;
            total = dao.ListarCount(codemp, idid, where, sidx, sord, inicio, limite);
            return total;
        }

        public static string ListarUtilizacion(int idioma)
        {
            string salida = "";
            dao.Categoria dao = new dao.Categoria();
            salida = dao.ListarUtilizacion(idioma);
            return salida;
        }

       
       

    }
}
