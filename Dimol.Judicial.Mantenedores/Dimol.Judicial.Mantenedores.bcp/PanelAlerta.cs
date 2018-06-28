using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class PanelAlerta
    {
        public static int ListarPanelAlertaAnalisisClienteCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelAlerta.ListarPanelAlertaAnalisisClienteCount(codemp, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PanelAlertaAnalisisCliente> ListarPanelAlertaAnalisisCliente(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelAlerta.ListarPanelAlertaAnalisisCliente(codemp, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PanelAlerta> ListarPanelAlerta(int codemp)
        {
            return dao.PanelAlerta.ListarPanelAlerta(codemp);
        }

        public static List<dto.PanelAlerta> ListarPanelAlertaMasiva(int codemp)
        {
            return dao.PanelAlerta.ListarPanelAlertaMasiva(codemp);
        }
        public static int ListarPanelAlertaEncargadoCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelAlerta.ListarPanelAlertaEncargadoCount(codemp, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PanelAlertaEncargado> ListarPanelAlertaEncargado(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelAlerta.ListarPanelAlertaEncargado(codemp, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PanelAlertaTipo> ListarPanelAlertaTipo(int codemp)
        {
            return dao.PanelAlerta.ListarPanelAlertaTipo(codemp);
        }

        public static List<dto.PanelAlertaTipo> ListarPanelAlertaTipoMasiva(int codemp)
        {
            return dao.PanelAlerta.ListarPanelAlertaTipoMasiva(codemp);
        }
        public static List<dto.PanelAlertaTipoReporte> ListarPanelAlertaTipoReporte(int codemp, int tipoReporte, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelAlerta.ListarPanelAlertaTipoReporte(codemp, tipoReporte,where, sidx, sord, inicio, limite);
        }
        public static List<dto.PanelAlertaReporteAnalisisCliente> ListarPanelAlertaReporteAnalisisCliente(int codemp, int pclid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.PanelAlerta.ListarPanelAlertaReporteAnalisisCliente(codemp, pclid, where, sidx, sord, inicio, limite);
        }
    }
}
