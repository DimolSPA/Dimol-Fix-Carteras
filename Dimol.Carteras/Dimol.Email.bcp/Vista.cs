using Dimol.dto;
using System.Collections.Generic;

namespace Dimol.Email.bcp
{
    public class Vista
    {
        public static List<Combobox> ListarEstados(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return Dimol.Email.dao.Vista.ListarEstados(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public static IEnumerable<Combobox> ListarEstadosCliente(int codemp, int pclid,  int idioma, int inicio, int limite)
        {
            return dao.EnvioEmail.GetEstadosByCliente(codemp, pclid, idioma, inicio, limite);
        }

        public static int ListarEstadosClienteCount(int codemp, int pclid, int idioma)
        {
            return dao.Vista.ListarEstadosClienteCount(codemp, pclid, idioma);
        }

        public static int ListarEstadosCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return Dimol.Email.dao.Vista.ListarEstadosCount(codemp, idioma, where, sidx, sord, inicio, limite);
        }

        public static List<Combobox> ListarGestores(int codemp, int sucid, int tipoCartera, int gestor, int grupo, string where, string sidx, string sord, int inicio, int limite)
        {
            return Dimol.Email.dao.Vista.ListarGestores(codemp, sucid, tipoCartera, gestor, grupo, where, sidx, sord, inicio, limite);
        }

        public static List<Combobox> ListarGestoresEmailMasivo(int codemp, int sucid, int tipoCartera, string sidx, string sord, int inicio, int limite)
        {
            return dao.Vista.ListarGestoresEmailMasivo(codemp, sucid, tipoCartera, sidx, sord, inicio, limite);
        }

        public static int ListarGestoresCount(int codemp, int sucid, int tipoCartera, int gestor, int grupo, string where, string sidx, string sord, int inicio, int limite)
        {
            return Dimol.Email.dao.Vista.ListarGestoresCount(codemp,  sucid, tipoCartera, gestor, grupo,  where, sidx,  sord, inicio,  limite);
        }

        public static int ListarGestoresEmailMasivoCount(int codemp, int sucid, int tipoCartera, string sidx, string sord, int inicio, int limite)
        {
            return Dimol.Email.dao.Vista.ListarGestoresEmailMasivoCount(codemp, sucid, tipoCartera, sidx, sord, inicio, limite);
        }

        public static List<Combobox> ListarGrupoCobranza(int codemp, int sucdid, string first)
        {
            return Dimol.Email.dao.Vista.ListarGrupoCobranza( codemp,sucdid, first);
        }
    }
}
