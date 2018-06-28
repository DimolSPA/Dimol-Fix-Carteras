using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;

namespace Dimol.Finanzas.bcp
{
    public class Clausula : dto.Clausula
    {
        dao.Clausula daoAccion = new dao.Clausula();

        public void OperAccion(string oper, int id, UserSession objsession, int idCCT)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar(objsession.CodigoEmpresa, id, idCCT);
                    break;

                case "edit":
                    this.id = id;
                    //daoAccion.Editar((dto.Clausula)this, objsession.CodigoEmpresa, id);
                    break;

                case "del":
                    daoAccion.Borrar(objsession.CodigoEmpresa, id, idCCT);
                    break;
            }
        }
        
        public List<dto.Clausula> ListarClausulasGrilla(int codemp, int idioma, int id)
        {
            //List<dto.Insumo> lst = new List<dto.Insumo>();
            return dao.Clausula.ListarClausulasGrilla(codemp, idioma, id);
            //return lst;
        }

        public int ListarClausulasGrillaCount(int codemp, int idioma, int id, string where, string sidx, string sord)
        {
            //int total = 0;
            return dao.Clausula.ListarClausulasGrillaCount(codemp, idioma, id, where, sidx, sord);
            //return total;
        }

        public static string ListarClausulasTodas(int codemp, int idid)
        {
            return dao.Clausula.ListarClausulasTodas(codemp, idid);

        }

        public static List<Combobox> ListarClausulasTodas2(int codemp, int idioma, string first)
        {
            return dao.Clausula.ListarClausulasTodas2(codemp, idioma, first);
        }

       
    }
}
