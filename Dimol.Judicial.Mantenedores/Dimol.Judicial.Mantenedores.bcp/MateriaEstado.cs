using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class MateriaEstado:dto.MateriaEstado
    {
        dao.MateriaEstado daoMateriaEstado = new dao.MateriaEstado();

        public static string ListarEstados(int codemp,int idioma)
        {
            dao.MateriaEstado daoMateriaEstado = new dao.MateriaEstado();
            return daoMateriaEstado.ListarEstados(codemp,idioma);
        }

        public static string ListarMaterias(int codemp, int idioma)
        {
            dao.MateriaEstado daoMateriaEstado = new dao.MateriaEstado();
            return daoMateriaEstado.ListarMaterias(codemp, idioma);
        }
        public static List<Dimol.dto.Combobox> ListarEstadosCombo(int codemp, int idioma, int esjid)
        {
            dao.MateriaEstado daoMateriaEstado = new dao.MateriaEstado();
            return daoMateriaEstado.ListarEstadosCombo(codemp, idioma, esjid);
        }

        public static List<Dimol.dto.Combobox> ListarMateriasCombo(int codemp, int idioma)
        {
            dao.MateriaEstado daoMateriaEstado = new dao.MateriaEstado();
            return daoMateriaEstado.ListarMateriasCombo(codemp, idioma);
        }

        public List<dto.MateriaEstado> ListarMateriaEstadoGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MateriaEstado> lstMateriaEstado = new List<dto.MateriaEstado>();
            lstMateriaEstado = daoMateriaEstado.ListarMateriaEstadoGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMateriaEstado;
        }

        public List<dto.MateriaEstado> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MateriaEstado> lstMateriaEstado = new List<dto.MateriaEstado>();
            lstMateriaEstado = daoMateriaEstado.ListarMateriaEstadoGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            return lstMateriaEstado;
        }

        public static int ListarMateriaEstadoGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.MateriaEstado.ListarMateriaEstadoGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite);
       }


        public void OperMateriaEstado(string oper, string id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoMateriaEstado.InsertarMateriaEstado((dto.MateriaEstado)this, objsession.CodigoEmpresa);
                    break;
                case "edit":
                    //daoMateriaJudicial.EditarMateriaJudicial((dto.MateriaJudicial)this, objsession.CodigoEmpresa, objsession.Idioma);
                    break;
                case "del":
                     string[] split = id.ToString().Split(new Char[] {'|'});
                     int idEstado = string.IsNullOrEmpty(split[0].ToString()) ? 0 : int.Parse(split[0].ToString());
                     int idMateria = string.IsNullOrEmpty(split[1].ToString()) ? 0 : int.Parse(split[1].ToString());
                    daoMateriaEstado.BorrarMateriaEstado(objsession.CodigoEmpresa,  idEstado, idMateria);
                    break;
            }
        }
    }
}
