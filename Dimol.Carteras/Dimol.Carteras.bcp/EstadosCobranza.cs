using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Carteras.dto;
using Dimol.dto;
namespace Dimol.Carteras.bcp
{
    public class EstadosCobranza
    {
        public List<Combobox> ListarPerfiles(int codemp, int idioma, int perfilId)
        {
            return dao.EstadosCobranza.ListarPerfiles(codemp, idioma, perfilId);
        }
        public List<Combobox> TraeListaTipoEstados(string eticlave, int idioma)
        {
            return dao.EstadosCobranza.TraeListaTipoEstados(eticlave, idioma);
        }
        public static int ListarEstadosCarteraPerfilCount(int codemp, int idioma, int perfilSelected, int agrupaSelected, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.EstadosCobranza.ListarEstadosCarteraPerfilCount(codemp, idioma, perfilSelected,agrupaSelected, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PerfilEstadoCobranza> ListarEstadosCarteraPerfil(int codemp, int idioma, int perfilSelected, int agrupaSelected, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.EstadosCobranza.ListarEstadosCarteraPerfil(codemp, idioma, perfilSelected, agrupaSelected, where, sidx, sord, inicio, limite);
        }
        public static int InsertarPerfilEstado(int codemp, int perfil, int estid, int accion, int user)
        {
            int result = 0;

            result = dao.EstadosCobranza.InsertarPerfilEstado(codemp, perfil, estid);
            if (result > 0)
            {
                result = dao.EstadosCobranza.InsertarPerfilEstadoHistorial(perfil, estid, accion, user);
            }

            return result;
        }
        public static int EliminarPerfilEstado(int codemp, int perfil, int estid, int accion, int user)
        {
            int result = 0;

            result = dao.EstadosCobranza.EliminarPerfilEstado(codemp, perfil, estid);
            if (result > 0)
            {
                result = dao.EstadosCobranza.InsertarPerfilEstadoHistorial(perfil, estid, accion, user);
            }
            return result;
        }

        public static int ListarEstadosCarteraClienteCount(int codemp, int idioma, int clienteSelected, int agrupaSelected, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.EstadosCobranza.ListarEstadosCarteraClienteCount(codemp, idioma, clienteSelected, agrupaSelected, where, sidx, sord, inicio, limite);
        }
        public static List<dto.PerfilEstadoCobranza> ListarEstadosCarteraCliente(int codemp, int idioma, int clienteSelected, int agrupaSelected, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.EstadosCobranza.ListarEstadosCarteraCliente(codemp, idioma, clienteSelected, agrupaSelected, where, sidx, sord, inicio, limite);
        }
        public static int InsertarClienteEstado(int codemp, int pclid, int estid, int accion, int user)
        {
            int result = 0;

            result = dao.EstadosCobranza.InsertarClienteEstado(codemp, pclid, estid);
            if (result > 0)
            {
                result = dao.EstadosCobranza.InsertarClienteEstadoHistorial(pclid, estid, accion, user);
            }

            return result;
        }
        public static int EliminarClienteEstado(int codemp, int pclid, int estid, int accion, int user)
        {
            int result = 0;

            result = dao.EstadosCobranza.EliminarClienteEstado(codemp, pclid, estid);
            if (result > 0)
            {
                result = dao.EstadosCobranza.InsertarClienteEstadoHistorial(pclid, estid, accion, user);
            }
            return result;
        }
    }
}
