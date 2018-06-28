using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class Perfil : dto.Perfil
    {
        dao.Perfil daoPerfil = new dao.Perfil();
        public List<dto.Perfil> ListarPerfilGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Perfil> lstPerfil = new List<dto.Perfil>();
            lstPerfil = daoPerfil.ListarPerfilGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstPerfil;
        }

        public List<dto.Perfil> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Perfil> lstPerfil = new List<dto.Perfil>();
            lstPerfil = daoPerfil.ExportarExcel(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstPerfil;
        }

        public static int ListarPerfilGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Perfil.ListaPerfilGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public void OperPerfil(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoPerfil.InsertarPerfil((dto.Perfil)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "edit":
                    daoPerfil.EditarPerfil((dto.Perfil)this, objsession.CodigoEmpresa, (int)id, objsession.Idioma);
                    break;
                case "del":
                    daoPerfil.BorrarPerfil(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }
    }
}
