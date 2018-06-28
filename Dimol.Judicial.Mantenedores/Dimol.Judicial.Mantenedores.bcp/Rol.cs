using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class Rol
    {
        public List<dto.Rol> ListarRolesGrilla(int codemp, int idioma, int idCompetencia, string rol_numero, int? rol_trbid, int? rol_tcaid, int? rol_pclid, string ctc_rut, string ctc_nomfant, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarRolesGrilla(codemp, idioma, idCompetencia, rol_numero, rol_trbid, rol_tcaid, rol_pclid, ctc_rut, ctc_nomfant, where, sidx, sord, inicio, limite);
        }

        public int ListarRolesGrillaCount(int codemp, int idioma, int idCompetencia, string rol_numero, int? rol_trbid, int? rol_tcaid, int? rol_pclid, string ctc_rut, string ctc_nomfant, string where, string sidx, string sord)
        {
            return dao.Rol.ListarRolesGrillaCount(codemp, idioma, idCompetencia, rol_numero, rol_trbid, rol_tcaid, rol_pclid, ctc_rut, ctc_nomfant, where, sidx, sord);
        }

        public static List<Combobox> ListarTiposCausa(int codemp, int idioma, string first)
        {
            return dao.Rol.ListarTiposCausa(codemp, idioma, first);
        }

        public static List<Combobox> ListarTribunales(int codemp, int idCompetencia, string first)
        {
            return dao.Rol.ListarTribunales(codemp, idCompetencia, first);
        }

        public static List<Combobox> ListarTribunales(int codemp, string first)
        {
            return dao.Rol.ListarTribunales(codemp, first);
        }

        public static List<Combobox> ListarClientes(int codemp, int idioma, string first)
        {
            return dao.Rol.ListarClientes(codemp, idioma, first);
        }

        public static List<Combobox> ListarProvCli(int codemp, string first)
        {
            return dao.Rol.ListarProvCli(codemp, first);
        }

        public static List<Combobox> ListarDeudores(int codemp, string first)
        {
            return dao.Rol.ListarDeudores(codemp, first);
        }

        public static int InsertarRol(dto.Rol objAccion, Dimol.dto.UserSession objSession)
        {
            return dao.Rol.InsertarRol(objAccion, objSession);
        }

        public static int EditarRol(dto.Rol objRol, Dimol.dto.UserSession objSession)
        {
           return dao.Rol.EditarRol( objRol, objSession);
        }

        public static int ActualilzarQuiebraDeudor(int codemp, int ctcid, string quiebra)
        {
            return dao.Rol. ActualilzarQuiebraDeudor(  codemp, ctcid,quiebra);
        }

        public static List<Autocomplete> ListarRutNombreDeudor(string nombre)
        {
            return dao.Rol.ListarRutNombreDeudor(nombre);
        }

        public static dto.RolCarga BuscarRol(int codemp, int rolid, int idioma)
        {
            return dao.Rol.BuscarRol(codemp, rolid, idioma);
        }

        public static void BuscarRolDemandaAvenimiento(int codemp, int rolid, dto.RolCarga rol)
        {
            dao.Rol.BuscarRolDemandaAvenimiento(codemp, rolid, rol.Demanda, rol.Avenimiento);
        }

        public static List<dto.DocumentoRol> ListarDocumentosAsignadosGrilla(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosAsignadosGrilla(codemp, idioma, rolid, where, sidx, sord, inicio, limite);
        }

        public static List<dto.DocumentoRol> ListarDocumentosPorNumeroResolucion(string NumeroResolucion)
        {
            return dao.Rol.ListarDocumentosPorNumeroResolucion(NumeroResolucion);
        }

        public static List<dto.DocumentoRol> ListarDocumentosAsignadosGrillaPrevisional(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosAsignadosGrillaPrevisional(codemp, idioma, rolid, where, sidx, sord, inicio, limite);
        }
        public static List<dto.DocumentoEstampe> ListarDocumentosEstampesGrilla(int codemp, int pclid, int ctcid, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosEstampesGrilla(codemp, pclid, ctcid, rolid, where, sidx, sord, inicio, limite);
        }

        public static  int ListarDocumentosAsignadosGrillaCount(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosAsignadosGrillaCount(codemp, idioma, rolid, where, sidx, sord, inicio, limite);
        }

        public static int ListarDocumentosAsignadosGrillaCountPrevisional(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosAsignadosGrillaCountPrevisional(codemp, idioma, rolid, where, sidx, sord, inicio, limite);
        }

        public static int ListarDocumentosEstampesGrillaCount(int codemp, int pclid, int ctcid, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosEstampesGrillaCount(codemp, pclid, ctcid, rolid, where, sidx, sord, inicio, limite);
        }

        public static List<dto.DocumentoRol> ListarDocumentosPorAsignarGrilla(int codemp, int idioma, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosPorAsignarGrilla(codemp, idioma, pclid, ctcid, where, sidx, sord, inicio, limite);
        }

        public static List<dto.DocumentoRol> ListarDocumentosPorAsignarGrillaPrevisonal(int codemp, int idioma, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosPorAsignarGrillaPrevisonal(codemp, idioma, pclid, ctcid, where, sidx, sord, inicio, limite);
        }

        public static int ListarDocumentosPorAsignarGrillaCount(int codemp, int idioma, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosPorAsignarGrillaCount(codemp, idioma, pclid, ctcid, where, sidx, sord, inicio, limite);
        }

        public static int ListarDocumentosPorAsignarGrillaCountPrevisional(int codemp, int idioma, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarDocumentosPorAsignarGrillaCountPrevisional(codemp, idioma, pclid, ctcid, where, sidx, sord, inicio, limite);
        }

        public static List<dto.EstadosRol> ListarEstadosRolGrilla(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarEstadosRolGrilla(codemp, idioma, rolid, where, sidx, sord, inicio, limite);
        }

        public static int ListarEstadosRolGrillaCount(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarEstadosRolGrillaCount(codemp, idioma, rolid, where, sidx, sord, inicio, limite);
        }
        public static int ActualizarParaPoderJudicial(int codemp, int rolid, bool  enPoderJudicial)
        {
            return dao.Rol.ActualizarParaPoderJudicial(codemp, rolid, enPoderJudicial);
        }
        public static int ActualizarParaPoderJudicialHistorial(int codemp, int rolid, bool enPoderJudicial, int userId)
        {
            return dao.Rol.ActualizarParaPoderJudicialHistorial(codemp, rolid, enPoderJudicial, userId);
        }
        public static bool TraeActualizarPoderJudicial(int codemp, int rolid)
        {
            return dao.Rol.TraeActualizarPoderJudicial(codemp, rolid);
        }
        public static int GuardarDocumentoEstampe(int codemp, int pclid, int ctcid, int rolid, string path, string nombre, string ext, string fecjud, int usrid)
        {
            return dao.Rol.GuardarDocumentoEstampe(codemp, pclid, ctcid, rolid, path, nombre, ext, fecjud, usrid);
        }
        #region "Rol Estados"

        public static int InsertarEstadoRol(int codemp, int rolid, int estid, int esjid, int usrid, string ipred, string ipmaquina, string comentario, DateTime fecjud, int codsuc, int gesid)
        {
            int error = 0;
            error = dao.Rol.InsertarEstadoRol(codemp, rolid, estid, esjid, usrid, ipred, ipmaquina, comentario, fecjud, codsuc, gesid);
            if (esjid == 3 || esjid == 9 || esjid == 10)
            {
                if (error > 0)
                {
                    error = bcp.PanelQuiebra.InsertarPanelQuiebra(codemp, rolid, usrid);
                }
            }
            return error;
        }

        public static int EliminarEstadoRol(int codemp, int rolid, int estid, int esjid, DateTime fecha)
        {
           return dao.Rol.EliminarEstadoRol(codemp, rolid, estid, esjid, fecha);
        }

        public static int EliminarEstampe(int codemp, string[] ids)
        {
            return dao.Rol.EliminarEstampe(codemp, ids);
        }

        public static void OperRol(string oper, string id, UserSession objSession)
        {
            string[] ids = id.Split('|');
            switch (oper)
            {
                case "add":
                    //in.InsertarContacto(obj);
                    break;
                case "edit":
                    EliminarEstadoRol(objSession.CodigoEmpresa,Int32.Parse( ids[1]),Int32.Parse(ids[2]),Int32.Parse(ids[3]),DateTime.Parse( ids[4]));
                    break;
                case "del":
                    EliminarEstadoRol(objSession.CodigoEmpresa, Int32.Parse(ids[0]), Int32.Parse(ids[1]), Int32.Parse(ids[2]), DateTime.ParseExact(ids[3], "MMddyyyyHHmmssfff", CultureInfo.InvariantCulture));
                    break;
            }
        }

        public static List<dto.Autocomplete> ListarEstadoJudicial(int codemp, int idioma,string nombre, int esjid)
        { 
            return dao.Rol.ListarEstadoJudicial(codemp, idioma, nombre, esjid);
        }

        #endregion

        #region "Rol Demandados"
        public static List<dto.Demandado> ListarRolDemandadosGrilla(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarRolDemandadosGrilla( codemp, rolid, where, sidx, sord, inicio, limite);
        }

        public static int ListarRolDemandadosGrillaCount(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarRolDemandadosGrillaCount(codemp, rolid, where, sidx, sord, inicio, limite);
        }

        public static int InsertarDemandadoRol(int codemp, int rolid, string rut, string nombre, bool repLegal)
        {
       
            return dao.Rol.InsertarDemandadoRol(codemp, rolid, rut, nombre, repLegal);
        }

        public static int EliminarDemandadoRol(int codemp, int rolid, string rut)
        {
            return dao.Rol.EliminarDemandadoRol(codemp, rolid, rut);
        }
        #endregion

        #region "Rol Montos"

        public static int GrabarMontosDemanda(int codemp, int rolid, dto.DemandaAvenimiento demanda, dto.DemandaAvenimiento avenimiento)
        {
            int existe = dao.Rol.ExisteDemandaAvenimientoRol(codemp,rolid);
            int salida = 0;
            if (existe == 0)
            {
                salida = dao.Rol.InsertarDemandaAvenimientoRol(codemp, rolid, demanda, avenimiento);
            }
            else
            {
                salida = dao.Rol.ActualizarDemandaAvenimientoRol(codemp, rolid, demanda, avenimiento);
            }
            return salida;
        }

        #endregion

        #region "Asociados"

        public static List<dto.Asociados> ListarAsociados(int codemp, int ctcid)
        {
            return dao.Rol.ListarAsociados(codemp, ctcid);
        }

        #endregion

        #region "Rol Documentos"

        public static int InsertarDocumentosRol(int codemp, int rolid, int pclid, int ctcid, int ccbid, decimal monto, decimal saldo)
        {
            return dao.Rol.InsertarDocumentosRol(codemp, rolid,  pclid, ctcid,  ccbid,  monto,  saldo);
        }

        public static int EliminarDocumentosRol(int codemp, int rolid, int pclid, int ctcid, int ccbid)
        {
            return dao.Rol.EliminarDocumentosRol(codemp, rolid, pclid, ctcid, ccbid);
        }

        #endregion

        #region "Borradores"
        public static Dimol.dto.Combobox HistoriaBorrador(int codemp, int rolid, int idBorrador)
        {
            return dao.Rol.HistoriaBorrador(codemp, rolid, idBorrador);
        }
        #endregion

        #region "Rol Demandados"
        public static List<dto.Asegurado> ListarRolAseguradosGrilla(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarRolAseguradosGrilla(codemp, rolid, where, sidx, sord, inicio, limite);
        }

        public static int ListarRolAseguradosGrillaCount(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Rol.ListarRolAseguradosGrillaCount(codemp, rolid, where, sidx, sord, inicio, limite);
        }
        #endregion

        #region "Bloqueo Rol"
        public static string TraeEstAdmPoderJudicial(int rolid)
        {
            return dao.Rol.TraeEstAdmPoderJudicial(rolid);
        }

        public static int BloquearRol(int codemp, int rolId, string bloqueo, int usrid)
        {
            return dao.Rol.BloquearRol(codemp, rolId, bloqueo, usrid);
        }

        #endregion
    }
}
