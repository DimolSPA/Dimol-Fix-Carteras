using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class PanelMonitoreoDemonio
    {
        public static List<dto.MonitoreoExternoCabecera> ListarPanelMonitoreoExternoCabecera()
        {
            return dao.PanelMonitoreoDemonio.ListarPanelMonitoreoExternoCabecera();
        }
        public static List<dto.MonitoreoExternoDemanda> ListarPanelMonitoreoExternoDemandas(string where, string sidx, string sord)
        {
            return dao.PanelMonitoreoDemonio.ListarPanelMonitoreoExternoDemandas(where, sidx, sord);
        }
        public static List<dto.MonitoreoExternoRolBuscado> ListarPanelMonitoreoExternoRol(int codemp, int zonaId, string where, string sidx, string sord)
        {
            return dao.PanelMonitoreoDemonio.ListarPanelMonitoreoExternoRol(codemp, zonaId, where, sidx, sord);
        }
        public static List<Combobox> ListarZonasTribunales(int codemp, string first)
        {
            return dao.PanelMonitoreoDemonio.ListarZonasTribunales(codemp, first);
        }
        public static List<dto.MonitoreoSiiCabecera> ListarPanelMonitoreoSiiCabecera()
        {
            return dao.PanelMonitoreoDemonio.ListarPanelMonitoreoSiiCabecera();
        }
        public static List<dto.MonitoreoSiiCliente> ListarPanelMonitoreoSiiClientes(string where, string sidx, string sord)
        {
            return dao.PanelMonitoreoDemonio.ListarPanelMonitoreoSiiClientes(where, sidx, sord);
        }

        public static List<dto.MonitoreoInternoCabecera> ListarPanelMonitoreoInternoCabecera()
        {
            return dao.PanelMonitoreoDemonio.ListarPanelMonitoreoInternoCabecera();
        }
        public static List<dto.MonitoreoInternoCliente> ListarPanelMonitoreoInternoClientes(string where, string sidx, string sord)
        {
            return dao.PanelMonitoreoDemonio.ListarPanelMonitoreoInternoClientes(where, sidx, sord);
        }
    }
}
