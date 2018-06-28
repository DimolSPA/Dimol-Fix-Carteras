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
    public class SuperCategoria : dto.SuperCategoria
    {
        dao.SuperCategoria dao = new dao.SuperCategoria();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    dao.Insertar((dto.SuperCategoria)this, objsession.CodigoEmpresa, objsession.Idioma);

                    break;
                case "edit":

                    this.Id = (int)id;
                    dao.Editar((dto.SuperCategoria)this, objsession.CodigoEmpresa, objsession.Idioma, (int)id);
                    break;
                case "del":
                    dao.Borrar(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }


        public List<dto.SuperCategoria> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.SuperCategoria> lst = new List<dto.SuperCategoria>();
            lst = dao.ListarGrilla(codemp, where, sidx, sord, inicio, limite);
            return lst;
        }

        public List<dto.SuperCategoria> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.SuperCategoria> lst = new List<dto.SuperCategoria>();
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
            dao.SuperCategoria dao = new dao.SuperCategoria();
            salida = dao.ListarUtilizacion(idioma);
            return salida;
        }




    }
}
