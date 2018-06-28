using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;

namespace Dimol.Finanzas.bcp
{
    public class MaximaConvencional : dto.MaximaConvencional
    {
        dao.MaximaConvencional daoAccion = new dao.MaximaConvencional();

        public void OperAccion(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    daoAccion.Insertar((dto.MaximaConvencional)this, objsession.CodigoEmpresa);
                    break;
                
                case "edit":
                    this.MXC_MXCID = (int)id;
                    daoAccion.Editar((dto.MaximaConvencional)this, objsession.CodigoEmpresa, (int)id);
                    break;

                case "del":
                    daoAccion.Borrar(objsession.CodigoEmpresa, (int)id);
                    break;
            }
        }

        public List<dto.MaximaConvencional> ListarMaximaConvencionalGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            //List<dto.Insumo> lst = new List<dto.Insumo>();
            return dao.MaximaConvencional.ListarMaximaConvencionalGrilla(codemp, idioma, where, sidx, sord, inicio, limite);
            //return lst;
        }

        public int ListarMaximaConvencionalGrillaCount(int codemp, int idioma, string where, string sidx, string sord)
        {
            //int total = 0;
            return dao.MaximaConvencional.ListarMaximaConvencionalGrillaCount(codemp, idioma, where, sidx, sord);
            //return total;
        }

        public static List<Combobox> ListarTiposDocumentos(int codemp, int idioma, string first)
        {

            return dao.MaximaConvencional.ListarTiposDocumentos(codemp, idioma, first);
        }

        public static string ListarTiposDocumentos(int codemp, int idid)
        {
            return dao.MaximaConvencional.ListarTiposDocumentos(codemp, idid);

        }

        public static string ListarTipos(int codemp, int idid)
        {
            return dao.MaximaConvencional.ListarTipos(codemp, idid);

        }

        public static string ListarMonedas(int codemp)
        {
            return dao.MaximaConvencional.ListarMonedas(codemp);

        }

        public static string ListarAplica(int codemp, int idid)
        {
            return dao.MaximaConvencional.ListarAplica(codemp, idid);

        }
    }
}
