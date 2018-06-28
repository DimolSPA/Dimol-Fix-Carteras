using Dimol.dto;
using System.Collections.Generic;


namespace Dimol.ProveedorCliente.Mantenedores.bcp
{
    public class ProveedorCliente
    {
        public static List<Combobox> ListarEstados(int idioma, string first)
        {
            return dao.ProveedorCliente.ListarEstados(idioma, first);
        }

        public List<dto.ProveedorCliente> ListarProveedorClienteGrilla(int codemp, string tipo, string nombre, string apellidoPaterno, string apellidoMaterno, string rut, string nombreFantasia, string estado, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.ProveedorCliente.ListarProveedorClienteGrilla(codemp, tipo, nombre, apellidoPaterno, apellidoMaterno, rut, nombreFantasia, estado, where, sidx, sord, inicio, limite);
        }

        public int ListarProveedorClienteGrillaCount(int codemp, string tipo, string nombre, string apellidoPaterno, string apellidoMaterno, string rut, string nombreFantasia, string estado, string where, string sidx, string sord, int inicio, int limite)
        {
            //int total = 0;
            return dao.ProveedorCliente.ListarProveedorClienteGrillaCount(codemp, tipo, nombre, apellidoPaterno, apellidoMaterno, rut, nombreFantasia, estado, where, sidx, sord, inicio, limite);
            //return total;
        }

        public List<dto.ProveedorCliente> ListarReceptoresGrilla(int codemp, string tipo, string nombre, string apellidoPaterno, string apellidoMaterno, string rut, string where, string sidx, string sord, int inicio, int limite)
        {
            
            return dao.ProveedorCliente.ListarReceptoresGrilla(codemp, tipo, nombre, apellidoPaterno, apellidoMaterno, rut, where, sidx, sord, inicio, limite);
            
        }

        public int ListarReceptoresGrillaCount(int codemp, string tipo, string nombre, string apellidoPaterno, string apellidoMaterno, string rut, string where, string sidx, string sord, int inicio, int limite)
        {

            return dao.ProveedorCliente.ListarReceptoresGrillaCount(codemp, tipo, nombre, apellidoPaterno, apellidoMaterno, rut, where, sidx, sord, inicio, limite);
            
        }
        public static List<Combobox> ListarEstado(int idioma, string first)
        {
            return dao.ProveedorCliente.ListarEstado(idioma, first);
        }

        public static List<Combobox> ListarTiposProvCli(int codemp, int idioma, string first)
        {
            return dao.ProveedorCliente.ListarTiposProvCli(codemp, idioma, first);
        }

        public static List<Combobox> ListarNacionalidad(int codemp, int idioma, string first)
        {
            return dao.ProveedorCliente.ListarNacionalidad(codemp, idioma, first);
        }

        public static List<Combobox> ListarGiros(int codemp, int idioma, string first)
        {
            return dao.ProveedorCliente.ListarGiros(codemp, idioma, first);
        }

        public static List<Combobox> ListarTipoCartera(int idioma, string first)
        {
            return dao.ProveedorCliente.ListarTipoCartera(idioma, first);
        }

        public static List<Combobox> ListarUsuarios(int codemp, string first)
        {
            return dao.ProveedorCliente.ListarUsuarios(codemp, first);
        }

        public static List<Combobox> ListarPais()
        {
            return dao.ProveedorCliente.ListarPais();
        }

        public static List<Combobox> ListarCiudad(int region)
        {
            return dao.ProveedorCliente.ListarCiudad(region);
        }

        public static List<Combobox> ListarRegion(int pais)
        {
            return dao.ProveedorCliente.ListarRegion(pais);
        }

        public static List<Combobox> ListarComuna(int ciudad)
        {
            return dao.ProveedorCliente.ListarComuna(ciudad);
        }

        public static List<Combobox> ListarBancos(int codemp, string first)
        {
            return dao.ProveedorCliente.ListarBancos(codemp, first);
        }

        public static List<Combobox> ListarTiposCuenta(int idioma, string first)
        {
            return dao.ProveedorCliente.ListarTiposCuentas(idioma, first);
        }

        public static List<Combobox> ListarImpuestosProvCli(int codemp, string first)
        {
            return dao.ProveedorCliente.ListarImpuestosProvCli(codemp, first);
        }

        public static List<Combobox> ListarTiposContactoProvCli(int codemp, int idioma, string first)
        {
            return dao.ProveedorCliente.ListarTiposContactoProvCli(codemp, idioma,first);
        }

        public static List<Combobox> ListarFormasDePago(int codemp, int idioma, string first)
        {
            return dao.ProveedorCliente.ListarFormasDePago(codemp, idioma, first);
        }

        public static List<Combobox> ListarContratosCartera(int codemp, string first)
        {
            return dao.ProveedorCliente.ListarContratosCartera(codemp, first);
        }

        public static List<Combobox> ListarSucursalesProvCli(int codemp, int suc, string first)
        {
            return dao.ProveedorCliente.ListarSucursalesProvCli(codemp, suc, first);
        }

        public static int GrabarCliente(dto.ProveedorCliente obj, int codemp, int idioma, string tipoCliente)
        {
            return dao.ProveedorCliente.GrabarCliente(obj, codemp, idioma, tipoCliente);
        }

        public static List<Combobox> ListarEstadosCredito(int idioma, string first)
        {
            return dao.ProveedorCliente.ListarEstadosCredito(idioma, first);
        }

        public static int InsertarEnteJudicial(int codemp,int idCliente)
        {
            return dao.ProveedorCliente.InsertarEnteJudicial(codemp, idCliente);
        }

        public static int GrabarClienteSucursal(dto.ProveedorCliente obj, int codemp, int idioma, int idCliente)
        {
            return dao.ProveedorCliente.GrabarClienteSucursal(obj, codemp, idioma, idCliente);
        }

        public static int GrabarClienteImpuesto(dto.ProveedorCliente obj, int codemp, int idCliente)
        {
            return dao.ProveedorCliente.GrabarClienteImpuesto(obj, codemp, idCliente);
        }

        public static int GrabarClienteContacto(dto.ProveedorCliente obj, int codemp, int idCliente)
        {
            return dao.ProveedorCliente.GrabarClienteContacto(obj, codemp, idCliente);
        }

        public static int GrabarClienteCuentaCorriente(dto.ProveedorCliente obj, int codemp, int idCliente)
        {
            return dao.ProveedorCliente.GrabarClienteCuentaCorriente(obj, codemp, idCliente);
        }

        public static int GrabarClienteContrato(dto.ProveedorCliente obj, int codemp, int idCliente)
        {
            return dao.ProveedorCliente.GrabarClienteContrato(obj, codemp, idCliente);
        }
    }
}
