using Dimol.dto;
using Dimol.Tesoreria.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Tesoreria.bcp
{
    public class Caja
    {
        public static List<Combobox> ListarTipo(int idioma, string first)
        {
            return dao.Caja.ListarTipo(idioma, first);
        }

        public static List<Combobox> ListarEmpleados(int codemp, string first)
        {
            return dao.Caja.ListarEmpleados(codemp, first);
        }

        public static List<Combobox> ListarTipoCpbt(int codemp, string tipoCpbtDoc, int perfil, int idioma, string carteraCliente, string tipo, string first)
        {
            return dao.Caja.ListarTipoCpbt(codemp, tipoCpbtDoc, perfil, idioma, carteraCliente, tipo, first);
        }

        public static List<BuscarDocumentoCaja> ListarDocumentosCaja(int codemp, int idioma, string pclid, string ctcid, string tipoMovimiento, string tipoDocumento, string emplid, string numeroCuenta, string montoDesde, string montoHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Caja.ListarDocumentosCaja(codemp, idioma, pclid, ctcid,  tipoMovimiento, tipoDocumento,  emplid, numeroCuenta,  montoDesde,  montoHasta,  where, sidx,  sord,  inicio, limite);
        }

        public static int ListarDocumentosCajaCount(int codemp, int idioma, string pclid, string ctcid, string tipoMovimiento, string tipoDocumento, string emplid, string numeroCuenta, string montoDesde, string montoHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Caja.ListarDocumentosCajaCount(codemp, idioma, pclid, ctcid, tipoMovimiento, tipoDocumento, emplid, numeroCuenta, montoDesde, montoHasta, where, sidx, sord, inicio, limite);
        }

        public static int ListarAnularPagoCount(int codemp, int sucid, string pclid, string ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Caja.ListarAnularPagoCount(codemp, sucid, pclid, ctcid,idioma, where, sidx, sord, inicio, limite);
        }

        public static List<BuscarAnularPago> ListarAnularPago(int codemp, int sucid, string pclid, string ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Caja.ListarAnularPago(codemp, sucid, pclid, ctcid, idioma, where, sidx, sord, inicio, limite);
        }

        public static List<Combobox> ListarEstadosCaja(int codemp, int idioma, string first)
        {
            return dao.Caja.ListarEstadosCaja(codemp, idioma, first);
        }

        public static List<Combobox> ListarNegociacionesCaja(int codemp, int ctcid, string first)
        {
            return dao.Caja.ListarNegociacionesCaja(codemp, ctcid, first);
        }
    }
}
