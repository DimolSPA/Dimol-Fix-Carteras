using Dimol.Carteras.dto;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dimol.Reportes.bcp;
using Dimol.Reportes.dto;
namespace Dimol.Carteras.bcp
{
    public class Comprobante
    {
        public List<dto.Comprobante> ListarCarteraClienteComprobante(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.TraeCarteraClienteComprobante(codemp, pclid, ctcid, estadoCPBT, idioma, where, sidx, sord, inicio, limite);
        }

        public static List<dto.Comprobante> TraeCarteraClienteComprobanteTotal(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma)
        {
            return dao.Comprobante.TraeCarteraClienteComprobanteTotal(codemp, pclid, ctcid, estadoCPBT, idioma);
        }

        public static List<dto.Comprobante> TraeCarteraClienteComprobanteTotalMoneda(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma)
        {
            return dao.Comprobante.TraeCarteraClienteComprobanteTotalMoneda(codemp, pclid, ctcid, estadoCPBT, idioma);
        }

        public dto.Comprobante TraeCpbt(int codemp, int pclid, int ctcid, int ccbid)
        {
            return dao.Comprobante.TraeCpbt(codemp, pclid, ctcid, ccbid);
        }

        public int TraeCarteraClienteComprobanteCount(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.TraeCarteraClienteComprobanteCount(codemp, pclid, ctcid, estadoCPBT, idioma, where, sidx, sord, inicio, limite);
        }

        public List<Combobox> ListarGrupoCpbt(int codemp, int pclid, int ctcid, string estadoCPBT, int idioma)
        {
            return dao.Comprobante.ListarGrupoCpbt(codemp, pclid, ctcid, estadoCPBT, idioma);
        }

        public int TraeTipoCartera(int codemp, int pclid, int ctcid, string estadoCPBT)
        {
            return dao.Comprobante.TraeTipoCartera(codemp, pclid, ctcid, estadoCPBT);
        }

        public List<Combobox> ListarTipoCartera(int idioma)
        {
            return dao.Comprobante.ListarTipoCartera(idioma);
        }

        public List<Combobox> ListarEstadosCartera(int codemp, int idioma)
        {
            return dao.Comprobante.ListarEstadosCartera(codemp, idioma);
        }

        public List<Combobox> ListarMonedas(int codemp)
        {
            return dao.Comprobante.ListarMonedas(codemp);
        }

        public List<Combobox> ListarMotivoCobranza(int codemp, int idioma, string first)
        {
            return dao.Comprobante.ListarMotivoCobranza(codemp, idioma, first);
        }

        public List<Combobox> ListarAsociadoSubcartera(int codemp, string first)
        {
            return dao.Comprobante.ListarAsociadoSubcartera(codemp, first);
        }

        public List<Combobox> ListarSituacionCartera(int idioma)
        {
            return dao.Comprobante.ListarSituacionCartera(idioma);
        }

        public List<Combobox> ListarCodigoCarga(int codemp, int pclid, string first)
        {
            return dao.Comprobante.ListarCodigoCarga(codemp, pclid, first);
        }

        public List<Combobox> ListarContrato(int codemp, int pclid, int tipoCartera, string first)
        {
            return dao.Comprobante.ListarContrato(codemp, pclid, tipoCartera, first);
        }

        public int GrabarDocumento(dto.Comprobante obj, int codemp)
        {
            return dao.Comprobante.GrabarDocumento(obj, codemp);
        }

        public int EliminarDocumento(dto.Comprobante obj, int codemp)
        {
            return dao.Comprobante.EliminarDocumento(obj, codemp);
        }

        public static int EliminarSubcartera(int id, int codemp)
        {
            return dao.Comprobante.EliminarSubcartera(id, codemp);
        }

        public int DescartarDocumento(dto.Comprobante obj, int codemp)
        {
            return dao.Comprobante.DescartarDocumento(obj, codemp);
        }

        public static List<Combobox> ListarTipoDocumento(int codemp, int idioma)
        {
            return dao.Comprobante.ListarTipoDocumento(codemp, idioma);
        }

        public static string ListarImagenesCpbt(int codemp, int pclid, int ctcid)
        {
            string salida = "";
            string[] nombreArchivo;
            List<Combobox> lstImagenes = dao.Comprobante.ListarImagenesCpbt(codemp, pclid, ctcid);
            if (lstImagenes.Count > 0)
            {
                foreach (Combobox obj in lstImagenes)
                {
                    nombreArchivo = obj.Value.Split('\\');
                    salida += "<li><a href=\"#\"><img src=\"" + ConfigurationManager.AppSettings["UrlImagenes"] + nombreArchivo[nombreArchivo.Length - 1] + "\" data-large=\"" + ConfigurationManager.AppSettings["UrlImagenes"] + "/" + nombreArchivo[nombreArchivo.Length - 1] + "\" alt=\"" + obj.Text + "\" title=\"" + obj.Text + "\" data-description=\"" + obj.Text + "\" onContextMenu=\"return false;\" /></a></li>";
                }
            }
            else
            {
                salida = "";
            }


            return salida;
        }

        public static int DemandaPendienteCpbt(int codemp, int pclid, int ctcid, int ccbid, int userid, bool operacion)
        {
            int salida = 0;
            if (operacion)
            {
                //insertar
                salida = dao.Comprobante.InsertarCarteraDemandaPendiente(codemp, pclid, ctcid, ccbid, userid);
            }
            else
            {
                //delete
                salida = dao.Comprobante.EliminarCarteraDemandaPendiente(codemp, pclid, ctcid, ccbid);
            }
            return salida;
        }

        #region "Comprobante"
        public static List<Combobox> ListarTipoDocumento(int codemp, int idioma, int perfil, string tipo, string first)
        {
            return dao.Comprobante.ListarTipoDocumento(codemp, idioma, perfil, tipo, first);
        }

        public static List<Combobox> ListarEstadosComprobante(int idioma, string first)
        {
            return dao.Comprobante.ListarEstadosComprobante(idioma, first);
        }

        public static List<Combobox> ListarMonedas(int codemp, string first)
        {
            return dao.Comprobante.ListarMonedas(codemp, first);
        }

        public static List<dto.BuscarComprobante> ListarComprobantes(int codemp, int idioma, int codsuc, int tipo, int numero, int pclid, string emisionDesde, string emisionHasta, DateTime? vencimientoDesde, DateTime? vencimientoHasta, int montoDesde, int montoHasta, string rut, string nombreFantasia, string telefono, string email, string direccion, string estado, int ctcid, int trbid, string rol, string moneda, string numeroInterno, string producto, string comentario, string where, string sidx, string sord, int inicio, int limite, string tipoComprobante)
        {
            switch (tipoComprobante)
            {
                case "CC":
                    return dao.Comprobante.ListarComprobanteCartera(codemp, idioma, codsuc, tipo, numero, pclid, emisionDesde, emisionHasta, vencimientoDesde, vencimientoHasta, montoDesde, montoHasta, rut, nombreFantasia, telefono, email, direccion, where, sidx, sord, inicio, limite);

                case "C":
                    return dao.Comprobante.ListarComprobanteCompra(codemp, idioma, codsuc, tipo, numero, pclid, emisionDesde, emisionHasta, estado, ctcid, trbid, rol, moneda, where, sidx, sord, inicio, limite);

                case "V":
                    return dao.Comprobante.ListarComprobanteVenta(codemp, idioma, codsuc, tipo, numero, pclid, emisionDesde, emisionHasta, vencimientoDesde, vencimientoHasta, montoDesde, montoHasta, estado, moneda, numeroInterno, producto, comentario, where, sidx, sord, inicio, limite);

                default:
                    return new List<dto.BuscarComprobante>();

            }

        }

        public static int ListarComprobantesCount(int codemp, int idioma, int codsuc, int tipoDocumento, int numero, int pclid, string emisionDesde, string emisionHasta, DateTime? vencimientoDesde, DateTime? vencimientoHasta, int montoDesde, int montoHasta, string rut, string nombreFantasia, string telefono, string email, string direccion, string estado, int ctcid, int trbid, string rol, string moneda, string numeroInterno, string producto, string comentario, string where, string sidx, string sord, int inicio, int limite, string tipo)
        {
            switch (tipo)
            {
                case "CC":
                    return dao.Comprobante.ListarComprobanteCarteraCount(codemp, idioma, codsuc, tipoDocumento, numero, pclid, emisionDesde, emisionHasta, vencimientoDesde, vencimientoHasta, montoDesde, montoHasta, rut, nombreFantasia, telefono, email, direccion, where, sidx, sord, inicio, limite);

                case "C":
                    return dao.Comprobante.ListarComprobanteCompraCount(codemp, idioma, codsuc, tipoDocumento, numero, pclid, emisionDesde, emisionHasta, estado, ctcid, trbid, rol, moneda, where, sidx, sord, inicio, limite);

                case "V":
                    return dao.Comprobante.ListarComprobanteVentaCount(codemp, idioma, codsuc, tipoDocumento, numero, pclid, emisionDesde, emisionHasta, vencimientoDesde, vencimientoHasta, montoDesde, montoHasta, estado, moneda, numeroInterno, producto, comentario, where, sidx, sord, inicio, limite);

                default:
                    return 0;

            }

        }

        public static List<dto.BuscarAceptarComprobante> ListarAceptarComprobantes(int codemp, int idioma, int codsuc, string tipo, string estado, string cartera,  string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarAceptarComprobante(codemp, idioma, codsuc, tipo, estado, cartera,  where, sidx, sord, inicio, limite);
        }

        public static int ListarAceptarComprobantesCount(int codemp, int idioma, int codsuc, string tipo, string estado, string cartera, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarAceptarComprobanteCount(codemp, idioma, codsuc, tipo, estado, cartera, where, sidx, sord, inicio, limite);
        }

        public static List<dto.BuscarEstadoComprobante> ListarEstadoComprobantes(int codemp, int idioma, int codsuc, string tipo, string estado, int perfil, DateTime? desde, DateTime? hasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarEstadoComprobante(codemp, idioma, codsuc, tipo, estado,desde, hasta, where, sidx, sord, inicio, limite);
        }

        public static List<string> ListarLinkRutasEstampes(int codemp, int pclid, int ctcid, int rolid, int tpcid, int numero)
        {
            return dao.Comprobante.ListarLinkRutasEstampes(codemp, pclid, ctcid, rolid, tpcid, numero);
        }

        public static int ListarEstadoComprobantesCount(int codemp, int idioma, int codsuc, string tipo, string estado, int perfil, DateTime? desde, DateTime? hasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarEstadoComprobanteCount(codemp, idioma, codsuc, tipo, estado,  desde, hasta, where, sidx, sord, inicio, limite);
        }

        public static List<Combobox> AceptarComprobantes(List<dto.AceptarComprobante> lst, UserSession objSesion)
        {
            List<Combobox> salida = new List<Combobox>();
            int salidaIndividual = 0;
            CabeceraComprobante obj =  new CabeceraComprobante();
            foreach (dto.AceptarComprobante c in lst)
            {
                //salidaIndividual = dao.Comprobante.AceptarComprobante(c, objSesion);
                //if (salidaIndividual < 0)
                //{
                //    salida.Add(new Combobox { Text = c.Numero.ToString(), Value = salidaIndividual.ToString() });
                //}
                using (TransactionScope scope = new TransactionScope())
                {
                    obj =  new CabeceraComprobante();
                    obj.Codemp = objSesion.CodigoEmpresa;
                    obj.Codsuc = objSesion.CodigoSucursal;
                    obj.TipoComprobante = c.IdTipoDocumento;
                    obj.CabeceraId = c.Numero;
                    obj.Estado = "A";
                    salidaIndividual = dao.Comprobante.InsertarCabeceraEstados(obj, objSesion);
                    if(salidaIndividual >=0 ){
                        salidaIndividual = dao.Comprobante.ModificarComprobanteEstado(obj, objSesion);
                    }
                    if(salidaIndividual >=0 ){
                        salidaIndividual = dao.Comprobante.InsertarCabeceraOP(obj);
                    }
                    if (salidaIndividual >= 0)
                    {
                        scope.Complete();
                    }
                }

            }

            return salida;
        }

        public static List<Combobox> ContabilizarComprobantes(List<dto.AceptarComprobante> lst, UserSession objSesion)
        {
            List<Combobox> salida = new List<Combobox>();
            int salidaIndividual = 0;
            CabeceraComprobante obj = new CabeceraComprobante();
            foreach (dto.AceptarComprobante c in lst)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    obj = new CabeceraComprobante();
                    obj.Codemp = objSesion.CodigoEmpresa;
                    obj.Codsuc = objSesion.CodigoSucursal;
                    obj.TipoComprobante = c.IdTipoDocumento;
                    obj.CabeceraId = c.Numero;
                    obj.Estado = "C";
                    salidaIndividual = dao.Comprobante.InsertarCabeceraEstados(obj, objSesion);
                    if (salidaIndividual >= 0)
                    {
                        salidaIndividual = dao.Comprobante.ModificarComprobanteEstado(obj, objSesion);
                    }
                    if (salidaIndividual >= 0)
                    {
                        salidaIndividual = dao.Comprobante.InsertarCabeceraOP(obj);
                    }
                    if (salidaIndividual >= 0)
                    {
                        scope.Complete();
                    }
                }
            }

            return salida;
        }

        public static List<Combobox> FacturarComprobantes(List<dto.AceptarComprobante> lst, UserSession objSesion)
        {
            List<Combobox> salida = new List<Combobox>();
            int salidaIndividual = 0;
            CabeceraComprobante obj = new CabeceraComprobante();
            foreach (dto.AceptarComprobante c in lst)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    obj = new CabeceraComprobante();
                    obj.Codemp = objSesion.CodigoEmpresa;
                    obj.Codsuc = objSesion.CodigoSucursal;
                    obj.TipoComprobante = c.IdTipoDocumento;
                    obj.CabeceraId = c.Numero;
                    obj.Estado = "F";
                    salidaIndividual = dao.Comprobante.InsertarCabeceraEstados(obj, objSesion);
                    if (salidaIndividual >= 0)
                    {
                        salidaIndividual = dao.Comprobante.ModificarComprobanteEstado(obj, objSesion);
                    }
                    if (salidaIndividual >= 0)
                    {
                        salidaIndividual = dao.Comprobante.InsertarCabeceraOP(obj);
                    }
                    if (salidaIndividual >= 0)
                    {
                        List<dto.Comprobante> docs = new List<dto.Comprobante>();

                        docs = dao.Comprobante.TraeListaCpbt(obj);

                        foreach (var item in docs)
                        {
                            salidaIndividual = dao.Comprobante.ModificarGastoJudicial(item, objSesion, docs.Count());
                        }

                    }
                    if (salidaIndividual >= 0)
                    {
                        scope.Complete();
                    }
                }
            }

            return salida;
        }

        public static List<dto.BoletaHonorarioSalida> ListarBHContabilizadas(int codemp, DateTime desde, DateTime hasta)
        {
            return dao.Comprobante.ListarBHContabilizadas(codemp, desde, hasta);
        }

        public static List<dto.BoletaHonorarioSalida> ListarBHFacturadas(int codemp, DateTime desde, DateTime hasta)
        {
            return dao.Comprobante.ListarBHFacturadas(codemp, desde,  hasta);
        }
        #endregion

        #region "Sub Cartera"

        public static List<dto.BuscarSubCartera> ListarSubCarteras(int codemp, string nombre, string rut, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarSubCarteras(codemp, nombre, rut, where, sidx, sord, inicio, limite);
        }

        public static int ListarSubCarterasCount(int codemp, string nombre, string rut, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarSubCarterasCount(codemp, nombre, rut, where, sidx, sord, inicio, limite);
        }

        public static dto.SubCartera TraeSubCartera(int codemp, int sbcid)
        {
            return dao.Comprobante.TraeSubCartera(codemp, sbcid);
        }

        public static int InsertarSubcartera(dto.SubCartera obj)
        {
            return dao.Comprobante.InsertarSubcartera(obj);
        }

        public static int ModificarSubcartera(dto.SubCartera obj)
        {
            return dao.Comprobante.ModificarSubcartera(obj);
        }

        public List<Autocomplete> ListarRutNombreAsegurado(string nombre)
        {
            return dao.Comprobante.ListarRutNombreAsegurado(nombre);
        }        

        #endregion

        #region "Agregar Historial"

        public static List<Combobox> ListarEstadosHistorial(int codemp, int grupo, int idioma, string tipo, string estadoXDoc, int perfil)
        {
            return dao.Comprobante.ListarEstadosHistorial(codemp, grupo, idioma, tipo, estadoXDoc, perfil);
        }

        public static List<Combobox> ListarEstadosCobranzaClientePerfil(int codemp, int grupo, int idioma, int pclid, string estadoXDoc, int perfil)
        {
            return dao.Comprobante.ListarEstadosCobranzaClientePerfil(codemp, grupo, idioma, pclid, estadoXDoc, perfil);
        }

        public static List<dto.Comprobante> ListarDocumentosHistorial(int codemp, int pclid, int ctcid, string estadoCPBT, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarDocumentosHistorial(codemp, pclid, ctcid, estadoCPBT, where, sidx, sord, inicio, limite);
        }

        public static int ListarDocumentosHistorialCount(int codemp, int pclid, int ctcid, string estadoCPBT, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarDocumentosHistorialCount(codemp, pclid, ctcid, estadoCPBT, where, sidx, sord, inicio, limite);
        }

        public static dto.DetalleEstados TraeDetalleEstado(int codemp, int estid)
        {
            return dao.Comprobante.TraeDetalleEstado(codemp, estid);
        }

        public static int InsertarCarteraClientesEstadosAcciones(int codemp, int pclid, int ctcid, int accid, int codsuc, int gesid, string contacto, string ipRed, string ipMaquina, int usuario, string comentario, int ddcid, long telefono)
        {
            return dao.Comprobante.InsertarCarteraClientesEstadosAcciones(codemp, pclid, ctcid, accid, codsuc, gesid, contacto, ipRed, ipMaquina, usuario, comentario, ddcid, telefono);
        }

        public static int ActualizarCarteraClientesCompromiso(int codemp, int pclid, int ctcid, int ccbid, decimal compromiso, DateTime? fecha)
        {
            return dao.Comprobante.ActualizarCarteraClientesCompromiso(codemp, pclid, ctcid, ccbid, compromiso, fecha);
        }

        public static int InsertarCarteraClientesEstadosHistorialEspecial(int codemp, int pclid, int ctcid, int ccbid, DateTime fecha, int estid, int codsuc, int gesid, string ipRed, string ipMaquina, string comentario, decimal monto, decimal saldo, int usuario)
        {
            return dao.Comprobante.InsertarCarteraClientesEstadosHistorialEspecial(codemp, pclid, ctcid, ccbid, fecha, estid, codsuc, gesid, ipRed, ipMaquina, comentario, monto, saldo, usuario);
        }

        public static int ActualizaCarteraEstados(int codemp, int pclid, int ctcid, int ccbid, int estid, string estcpbt)
        {
            Dimol.dao.Utilidades util = new Dimol.dao.Utilidades(codemp, 1, 0, "", "");
            return util.ActualizaCarteraEstados(codemp, pclid, ctcid, ccbid, estid, estcpbt);
        }

        public static int ActualizarCarteraClientesUltimaGestion(int codemp, int pclid, int ctcid, string estCpbt)
        {
            return dao.Comprobante.ActualizarCarteraClientesUltimaGestion(codemp, pclid, ctcid, estCpbt);
        }

        public static int ActualizarEstadoCarteraClientesTodos(int codemp, int pclid, int ctcid, int estid, string estCpbt)
        {
            return dao.Comprobante.ActualizarEstadoCarteraClientesTodos(codemp, pclid, ctcid, estid, estCpbt);
        }


        #endregion

        #region "Comprobantes"
        public static List<Combobox> ListarFormasPago(int codemp, int idioma, string first)
        {
            return dao.Comprobante.ListarFormasPago(codemp, idioma, first);
        }

        public static List<Combobox> ListarSucursales(int codemp, int pclid, string first)
        {
            return dao.Comprobante.ListarSucursales(codemp, pclid, first);
        }

        public static ClasificacionComprobante TraeClasificacionComprobante(int codemp, int tpcid)
        {
            return dao.Comprobante.TraeClasificacionComprobante(codemp, tpcid);
        }

        public static List<Combobox> ListarGastosComprobante(int idioma)
        {
            return dao.Comprobante.ListarGastosComprobante(idioma);
        }

        public static string[] GrabarComprobante(CabeceraComprobante obj, UserSession objSession)
        {
            string mensaje = "", asegurado="";
            int repetido = 0, ultimoId = 0, error = 0;
            List<Combobox> lstSubcartera = new List<Combobox>();

            repetido = dao.Comprobante.TraeNumeroComprobante(obj.Codemp, obj.Codsuc, obj.TipoComprobante, obj.Pclid, obj.Numero, obj.CabeceraId);

            switch (repetido)
            {
                case -1:
                    ultimoId = -1;
                    mensaje = "Hubo un problema al verificar el documento en el sistema.";
                    break;
                case 0:
                    ultimoId = dao.Comprobante.TraeUltimoNumeroComprobante(obj.Codemp, obj.Codsuc, obj.TipoComprobante);
                    if (obj.CabeceraId == 0)
                    {
                        obj.CabeceraId = ultimoId;
                        if (obj.Rolid != 0)
                        {
                            lstSubcartera = dao.Comprobante.TraeSubCarteraComprobante(obj.Codemp, obj.Rolid);
                            if (lstSubcartera.Count != 0)
                            {
                                asegurado = lstSubcartera[0].Value + " - " + lstSubcartera[0].Text;
                                if (!string.IsNullOrEmpty(lstSubcartera[1].Value) && !string.IsNullOrEmpty(lstSubcartera[1].Text ))
                                {
                                    asegurado = asegurado+", A: " + lstSubcartera[1].Value + " - " + lstSubcartera[1].Text;
                                }
                            }
                        }
                        obj.Glosa = (string.IsNullOrEmpty(obj.Glosa) ? "" : obj.Glosa + ", ") + (!string.IsNullOrEmpty(asegurado) ? asegurado + ", " : "") + obj.TipoComprobanteDesc + ", " + "Nº" + ":" + obj.Numero + ", " + obj.DetalleGlosa; //"Detalle" + ":" + obj.DetalleC[0].Nombre.Split('-')[1];
                        if (obj.Pclid == 89 && !string.IsNullOrEmpty(lstSubcartera[2].Value))
                        {
                            obj.Glosa = obj.Glosa + ", C: " + lstSubcartera[2].Value; //Codigo de Carga
                        }
                        if (obj.Pclid == 424 && !string.IsNullOrEmpty(lstSubcartera[2].Text))
                        {
                            obj.Glosa = obj.Glosa + ", O: " + lstSubcartera[2].Text; //Numero de Documento
                        }
                        error = dao.Comprobante.InsertarCabecera(obj);
                        obj.Estado = "E";
                        if (error > 0)
                        {
                            error = dao.Comprobante.InsertarCabeceraOP(obj);
                            if (error > 0)
                            {
                                error = dao.Comprobante.InsertarCabeceraEstados(obj, objSession);
                                if (error > 0)
                                {
                                    if (obj.Estado == "E")
                                    {
                                        switch (obj.Tipo)
                                        {
                                            case "CC":
                                                GrabarDetalleCarteraCliente(obj);
                                                break;
                                            case "C":
                                                GrabarDetalleCpbtDoc(obj);
                                                break;
                                            case "V":
                                                GrabarDetalleCpbtDoc(obj);
                                                break;
                                        }
                                        //    Recalcular_Cpbt()
                                    }


                                    //Cargar()
                                }
                                else
                                {
                                    ultimoId = -1;
                                    mensaje = "Hubo un error al crear el estado de la cabecera del comprobante.";
                                    break;
                                }
                            }
                            else
                            {
                                ultimoId = -1;
                                mensaje = "Hubo un error al crear la cabecera OP del comprobante.";
                                break;
                            }
                        }
                        else
                        {
                            ultimoId = -1;
                            mensaje = "Hubo un error al crear la cabecera del comprobante.";
                            break;
                        }
                    }
                    else
                    {
                        if (obj.Estado == "E")
                        {
                            error = dao.Comprobante.ActualizarCabecera(obj);
                            if (error >= 0)
                            {
                                error = dao.Comprobante.InsertarCabeceraOP(obj);
                            }
                            else
                            {
                                ultimoId = -1;
                                mensaje = "Hubo un error al actualizar la cabecera OP del comprobante.";
                                break;
                            }
                        }
                    }

                    break;
                default:   
                                                           
                        ultimoId = obj.CabeceraId;

                        if (obj.Rolid != 0)
                        {
                            lstSubcartera = dao.Comprobante.TraeSubCarteraComprobante(obj.Codemp, obj.Rolid);
                            if (lstSubcartera.Count != 0)
                            {
                                asegurado = lstSubcartera[0].Value + " - " + lstSubcartera[0].Text;
                                if (!string.IsNullOrEmpty(lstSubcartera[1].Value) && !string.IsNullOrEmpty(lstSubcartera[1].Text))
                                {
                                    asegurado = asegurado + ", A: " + lstSubcartera[1].Value + " - " + lstSubcartera[1].Text;
                                }
                            }
                        }

                        obj.Glosa = (!string.IsNullOrEmpty(asegurado) ? asegurado + ", " : "") + obj.TipoComprobanteDesc + ", " + "Nº" + ":" + obj.Numero + ", " + obj.DetalleGlosa;

                        if ((obj.Pclid == 89 || obj.Pclid == 598) && !string.IsNullOrEmpty(lstSubcartera[2].Value))
                        {
                            obj.Glosa = obj.Glosa + ", C: " + lstSubcartera[2].Value; //Codigo de Carga
                        }

                        if (obj.Pclid == 424 && !string.IsNullOrEmpty(lstSubcartera[2].Text))
                        {
                            obj.Glosa = obj.Glosa + ", O: " + lstSubcartera[2].Text; //Numero de Documento
                        }

                        error = dao.Comprobante.ActualizarGlosaCabecera(obj);

                        if(error <= 0)
                        {
                            ultimoId = -1;
                            mensaje = "Hubo un error al actualizar la glosa del comprobante.";
                            break;
                        }
                    

                    mensaje = "Documento ya se encuentra ingresado en el sistema.";
                    break;
            }

            return new string[] { ultimoId.ToString(), mensaje };
        }

        public static string GrabarDetalleCpbtDoc(CabeceraComprobante obj)
        {
            string salida = "";
            int error = 0;

            foreach (DetalleCabeceraComprobante detalle in obj.DetalleCC)
            {
                error = dao.Comprobante.EliminarDetalleCabecera(detalle);
            }
            foreach (DetalleCabeceraComprobante detalle in obj.DetalleC)
            {
                error = dao.Comprobante.EliminarDetalleCabecera(detalle);
            }
            foreach (DetalleCabeceraComprobante detalle in obj.DetalleV)
            {
                error = dao.Comprobante.EliminarDetalleCabecera(detalle);
            }

            if (error > 0)
            {
                error = dao.Comprobante.InsertarCabeceraOP(obj);
            }
            else
            {
                salida = "Error al eliminar detalle del comprobante.";
            }

            return salida;
        }

        public static string GrabarDetalleCarteraCliente(CabeceraComprobante obj)
        {
            string salida = "";
            int error = 0;
            decimal monto = 0;

            foreach (DetalleCabeceraComprobante detalle in obj.DetalleCC)
            {
                error = dao.Comprobante.EliminarDetalleCabecera(detalle);
            }

            monto = dao.Comprobante.TraeMontoCabeceraTotales(obj);

            if (monto >= 0)
            {
                error = dao.Comprobante.ActualizarCabeceraTotales(obj);
            }

            if (error >= 0)
            {
                error = dao.Comprobante.InsertarCabeceraOP(obj);
            }

            if (error < 0)
            {
                salida = "Error al eliminar detalle del comprobante.";
            }

            return salida;
        }

        public static List<Dimol.dto.Autocomplete> ListarItemComprobante(string nombre, int codemp, int tipprod, int pclid, string gasto)
        {
            return dao.Comprobante.ListarItemComprobante(nombre, codemp, tipprod, pclid, gasto);
        }

        public static string GrabarDetalleComprobante(CabeceraComprobante obj, UserSession objSession)
        {
            string mensaje = "";
            ClasificacionInsumo objInsumo;
            ImpuestoComprobante objImpuesto;
            int error = 0, item = 0;
            //string tipoImpuesto = "";
            //if(obj.Libcompra == 2){
            //    tipoImpuesto = "R";
            //}
            decimal impuesto = 0, impExe = 0, total = 0;
            string Retenido = "S";


            foreach (DetalleCabeceraComprobante d in obj.DetalleC)
            {
                objInsumo = dao.Comprobante.TraeClasificacionInsumo(obj.Codemp, d.Insid);
                if (obj.Sinimp == "N")
                {
                    objImpuesto = dao.Comprobante.TraeImpuestoComprobante(obj.Codemp, objSession.CodigoSucursal, obj.TipoComprobante, obj.CabeceraId, obj.Libcompra == 2 ? "S" : "N");
                }
                else
                {
                    objImpuesto = new ImpuestoComprobante()
                    {
                        Porcentaje = 0,
                        Retenido = "N"
                    };
                }
                if (d.Retenido == "S")
                {
                    objImpuesto = new ImpuestoComprobante()
                    {
                        Porcentaje = 10,
                        Retenido = "S"
                    };
                    objInsumo.Exento = "N";
                    Retenido = "S";
                }
                if (d.Item == 0 && obj.Modo == "add")
                {
                    d.Codemp = obj.Codemp;
                    d.Codsuc = obj.Codsuc;
                    d.TipoComprobante = obj.TipoComprobante;
                    d.CabeceraId = obj.CabeceraId;
                    d.Item = dao.Comprobante.BuscarUltimoItemDetalle(obj.Codemp, obj.Codsuc, obj.TipoComprobante, obj.CabeceraId);
                    //d.Insid = d.Insid;
                    d.Prodid = 0;//null
                    d.Pclid = 0;//null
                    d.Ctcid = 0;//null
                    d.Ccbid = 0;//null
                    d.Precio = d.Precio - (d.Precio * obj.Descuento);
                    //d.PrecioReal = d.Precio;

                    if (objInsumo.Exento == "S")
                    {
                        impuesto = 0;
                        Retenido = "N";
                        impExe = (d.PrecioReal * d.Cantidad);
                    }
                    else
                    {
                        impuesto = (d.PrecioReal * d.Cantidad) * (objImpuesto.Porcentaje / 100);
                        impExe = 0;
                    }
                    if (objImpuesto.Retenido == "S")
                    {
                        total = (d.PrecioReal * d.Cantidad) - impuesto;
                    }
                    else
                    {
                        total = (d.PrecioReal * d.Cantidad) + impuesto;
                    }

                    if (d.Retenido == "S")
                    {
                        objImpuesto.Porcentaje = 10;
                        impuesto = (d.PrecioReal * d.Cantidad) * (objImpuesto.Porcentaje / 100);
                        Retenido = "S";
                        impExe = 0;
                        total = (d.PrecioReal * d.Cantidad) - impuesto;
                    }
                    else
                    {
                        impuesto = 0;
                        Retenido = "N";
                        impExe = (d.PrecioReal * d.Cantidad);
                        total = (d.PrecioReal * d.Cantidad) + impuesto;
                    }

                    //d.Cantidad =
                    d.Saldo = d.Cantidad;
                    d.Neto = d.PrecioReal * d.Cantidad;
                    d.Impuesto = impuesto;
                    d.Retenido = Retenido;
                    d.Total = total;
                    d.Interes = 0;
                    d.Honorario = 0;
                    d.GastoPrejudicial = 0;
                    d.GastoJudicial = 0;
                    d.PorcFact = 0;
                    d.PorcHono = 0;
                    d.Bodid = 0;
                    d.Bdsid = 0;
                    d.Posicion = 0;

                    d.Tpcidpad = 0;
                    d.Numeropad = 0;
                    d.Itempad = 0;
                    d.Bodiddes = 0;
                    d.Bsciddes = "";
                    d.Posiciondes = 0;
                    d.NumeroSerie = 0;
                    d.NumeroSerieProv = "";
                    d.Cantebj = 0;
                    d.Ltpid = null;
                    d.Bscid = null;
                    d.Bsciddes = null;
                    d.Anio = null;
                    d.NumApl = null;
                    d.ItemApl = null;
                    d.ValRem = null;
                    d.Comentario = "";
                    d.Subitem = "";
                    d.Exento = impExe;
                    d.Rolid = obj.Rolid;

                    error = dao.Comprobante.InsertarDetalleCabecera(d);
                    if (error > 0)
                    {
                        if (error > 0 && obj.Rolid != 0)
                        {
                            error = dao.Comprobante.InsertarDetalleComprobanteRol(d);
                        }
                        if (error > 0 && objInsumo.ImputableCliente == "S")
                        {
                            error = dao.Comprobante.InsertarDetalleComprobanteProvCli(d);
                        }
                    }
                }
                else
                {
                    d.Codemp = obj.Codemp;
                    d.Codsuc = obj.Codsuc;
                    d.TipoComprobante = obj.TipoComprobante;
                    d.CabeceraId = obj.CabeceraId;
                    error = dao.Comprobante.TraePadreDetalleComprobante(d);
                    if (error > 0)
                    {
                        //d.Item = d.Item;
                        //d.Insid = d.Insid;
                        d.Prodid = 0;//null
                        d.Pclid = 0;//null
                        d.Ctcid = 0;//null
                        d.Ccbid = 0;//null
                        d.Precio = d.Precio - (d.Precio * obj.Descuento);
                        //d.PrecioReal = d.Precio;

                        if (objInsumo.Exento == "S")
                        {
                            impuesto = 0;
                            Retenido = "N";
                            impExe = (d.PrecioReal * d.Cantidad);
                        }
                        else
                        {
                            impuesto = (d.PrecioReal * d.Cantidad) * (objImpuesto.Porcentaje / 100);
                            impExe = 0;
                        }
                        if (objImpuesto.Retenido == "S")
                        {
                            total = (d.PrecioReal * d.Cantidad) - impuesto;
                        }
                        else
                        {
                            total = (d.PrecioReal * d.Cantidad) + impuesto;
                        }

                        if (d.Retenido == "S")
                        {
                            objImpuesto.Porcentaje = 10;
                            impuesto = (d.PrecioReal * d.Cantidad) * (objImpuesto.Porcentaje / 100);
                            Retenido = "S";
                            impExe = 0;
                            total = (d.PrecioReal * d.Cantidad) - impuesto;
                        }
                        else
                        {
                            impuesto = 0;
                            Retenido = "N";
                            impExe = (d.PrecioReal * d.Cantidad);
                            total = (d.PrecioReal * d.Cantidad) + impuesto;
                        }

                        //d.Cantidad =
                        d.Saldo = d.Cantidad;
                        d.Neto = d.PrecioReal * d.Cantidad;
                        d.Impuesto = impuesto;
                        d.Retenido = Retenido;
                        d.Total = total;
                        d.Interes = 0;
                        d.Honorario = 0;
                        d.GastoPrejudicial = 0;
                        d.GastoJudicial = 0;
                        d.PorcFact = 0;
                        d.PorcHono = 0;
                        d.Bodid = 0;
                        d.Bdsid = 0;
                        d.Posicion = 0;
                        d.Tpcidpad = 0;
                        d.Numeropad = 0;
                        d.Itempad = 0;
                        d.Bodiddes = 0;
                        d.Bsciddes = "";
                        d.Posiciondes = 0;
                        d.NumeroSerie = 0;
                        d.NumeroSerieProv = "";
                        d.Cantebj = 0;
                        d.Ltpid = null;
                        d.Bscid = null;
                        d.Bsciddes = null;
                        d.Anio = null;
                        d.NumApl = null;
                        d.ItemApl = null;
                        d.ValRem = null;
                        d.Comentario = "";
                        d.Subitem = null;
                        d.Exento = impExe;
                        d.Rolid = obj.Rolid;
                        error = dao.Comprobante.ActualizarDetalleCabecera(d);
                    }
                }
            }

            if (error > 0)
            {
                error = dao.Comprobante.CuadrarDetalleCabeceraComprobante(obj);
            }
            return mensaje;
        }

        public static CabeceraComprobante TraeRolCabeceraComprobante(CabeceraComprobante obj)
        {
            return dao.Comprobante.TraeRolCabeceraComprobante(obj);
        }

        public static List<dto.DetalleComprobanteCompra> ListarDetalleComprobanteCompra(int codemp, int sucid, int tpcid, int numero, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarDetalleComprobanteCompra(codemp, sucid, tpcid, numero, where, sidx, sord, inicio, limite);
        }

        public static int ListarDetalleComprobanteCompraCount(int codemp, int sucid, int tpcid, int numero, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarDetalleComprobanteCompraCount(codemp, sucid, tpcid, numero, where, sidx, sord, inicio, limite);
        }

        public static CabeceraComprobante BuscarComprobante(int codemp, int sucid, int tpcid, int numero)
        {
            return dao.Comprobante.BuscarComprobante(codemp, sucid, tpcid, numero);
        }

        public static string AnularComprobante(CabeceraComprobante obj)
        {
            return dao.Comprobante.AnularComprobante(obj);
        }

        public static string EliminarComprobante(CabeceraComprobante obj)
        {
            return dao.Comprobante.EliminarComprobante(obj);
        }

        public static List<dto.Comprobante> TraeListaCpbt(CabeceraComprobante obj)
        {
            return dao.Comprobante.TraeListaCpbt(obj);
        }

        public static string TraeCabeceraComprobanteEstado(CabeceraComprobante obj)
        {
            return dao.Comprobante.TraeCabeceraComprobanteEstado(obj);
        }

        public static int ModificarGastoJudicial(dto.Comprobante obj, UserSession objSesion, int total)
        {
            return dao.Comprobante.ModificarGastoJudicial(obj, objSesion, total);
        }        

        public static List<Autocomplete> ListarTribunalAuto(string nombre)
        {
            return dao.Comprobante.ListarTribunalAuto(nombre);
        }
        public static Tuple<bool, string> GrabarCastigoDevolucion(CabeceraComprobante obj, List<string> lst, UserSession objSession, List<string> lstMotivos)
        {
            decimal sumSaldoDetalleComprobante = 0;
            bool error = false;
            string salida = "", estadoDocumento = "";
            int folio = 0, ccbid = 0, rolidDocumento = 0;
            decimal monto = 0, saldo = 0, asignado = 0;
            string[] splitted;
            //32 Castigo Judicial documentos en J, 31 Castigo Prejudical documentos en V
            for (int i = 0; i < lst.Count; i++)
            {
                splitted = lst[i].Split('|');
                estadoDocumento = splitted[11];
                rolidDocumento = Int32.Parse(splitted[16]);
                if (obj.TipoComprobante == 32 && rolidDocumento == 0)
                {
                    error = true;
                    salida = "<b>Existen documentos, que no poseen rol</b>";
                }
                if (!error)
                {
                    if ((obj.TipoComprobante == 32 && estadoDocumento == "J") || (obj.TipoComprobante == 31 && estadoDocumento == "V") || (obj.TipoComprobante != 31 && obj.TipoComprobante != 32))
                    {
                        ccbid = Int32.Parse(splitted[2]);
                        monto = decimal.Parse(splitted[3]);
                        saldo = decimal.Parse(splitted[4]);
                        asignado = decimal.Parse(splitted[10]);
                        obj.DetalleC.Add(new dto.DetalleCabeceraComprobante
                        {
                            Pclid = Int32.Parse(splitted[0]),
                            Ctcid = Int32.Parse(splitted[1]),
                            Ccbid = ccbid,
                            Item = 0,
                            Insid = 0,
                            Precio = saldo == 0 ? asignado : saldo,
                            PrecioReal = monto,
                            Cantidad = 1,
                            Saldo = saldo == 0 ? asignado : saldo,
                            Neto = saldo == 0 ? asignado : saldo,
                            Impuesto = 0,
                            Retenido = "N",
                            Total = saldo == 0 ? asignado : saldo,
                            EstadoCpbt = estadoDocumento
                        });
                    }
                }
            }
            if (!error)
            {
                //Si existen detalles, se crea el comprobante
                if (obj.DetalleC.Count > 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        error = InsertarCabeceraComprobante(objSession, obj) <= 0;
                        if (!error)
                        {
                            error = InsertarCabeceraComprobanteDetalle(objSession, obj) <= 0;
                        }
                        //Update en los totales de la cabecera
                        sumSaldoDetalleComprobante = dao.Comprobante.TraeMontoCabeceraTotales(obj);

                        if (sumSaldoDetalleComprobante >= 0)
                        {
                            error = dao.Comprobante.ActualizarCabeceraTotales(obj, sumSaldoDetalleComprobante) <= 0;
                        }

                        //Grabar motivos

                        if (lstMotivos.Count > 0)
                        {
                            List<string> Deudoreslist = new List<string>();
                            foreach (string s in lst)
                                Deudoreslist.Add(s.Split('|')[1]);
                            Deudoreslist = Deudoreslist.Distinct().ToList();//obtemos los deudores a partir de la nueva lista

                            error = InsertarMotivoComprobante(objSession, obj, lstMotivos, Deudoreslist) <= 0;
                        }

                        if (!error)
                        {
                            folio = obj.CabeceraId;
                            salida = "Registro grabado con exito, Folio: <b>" + folio + "</b>";
                            scope.Complete();
                        }
                    }
                }
                else
                {
                    error = true;
                    salida = "<b>El Estado de los documentos, no corresponde con el tipo de comprobante</b>";
                }
            }
            
            return Tuple.Create((!error ? true : false), salida);
        }

        public static int InsertarCabeceraComprobante(UserSession objSession, CabeceraComprobante obj)
        {
            int cabeceraId = -1;
            int insert = -1;
            //cabecera
            cabeceraId = dao.Comprobante.TraeUltimoNumeroComprobante(objSession.CodigoEmpresa, objSession.CodigoSucursal, obj.TipoComprobante);
            if (obj.CabeceraId == 0)
            {
                obj.CabeceraId = cabeceraId;
                obj.Moneda = "1";
                obj.FormaPago = "1";
                obj.FechaDocumento = System.DateTime.Now;
                obj.FechaVencimiento = System.DateTime.Now;
                insert = dao.Comprobante.InsertarCabecera(obj);
                obj.Estado = "E";
                if (insert >= 0)
                {
                    insert = dao.Comprobante.InsertarCabeceraOP(obj);
                }
                if (insert >= 0)
                {
                    insert = dao.Comprobante.InsertarCabeceraEstados(obj, objSession);
                }
            }
          
            return cabeceraId;
        }
        public static int InsertarCabeceraComprobanteDetalle(UserSession objSession, CabeceraComprobante obj)
        {
            int procesoDetalle = -1;
            foreach (DetalleCabeceraComprobante detalle in obj.DetalleC)
            {
                ClasificacionInsumo objInsumo;
                ImpuestoComprobante objImpuesto;
                decimal impuesto = 0, impExe = 0, total = 0;
                string Retenido = "S";

                objInsumo = dao.Comprobante.TraeClasificacionInsumo(obj.Codemp, detalle.Insid);
                if (obj.Sinimp == "N")
                {
                    objImpuesto = dao.Comprobante.TraeImpuestoComprobante(obj.Codemp, objSession.CodigoSucursal, obj.TipoComprobante, obj.CabeceraId, obj.Libcompra == 2 ? "S" : "N");
                }
                else
                {
                    objImpuesto = new ImpuestoComprobante()
                    {
                        Porcentaje = 0,
                        Retenido = "N"
                    };
                }
                if (detalle.Retenido == "S")
                {
                    objImpuesto = new ImpuestoComprobante()
                    {
                        Porcentaje = 10,
                        Retenido = "S"
                    };
                    objInsumo.Exento = "N";
                    Retenido = "S";
                }
                if (detalle.Item == 0)
                {
                    detalle.Codemp = obj.Codemp;
                    detalle.Codsuc = obj.Codsuc;
                    detalle.TipoComprobante = obj.TipoComprobante;
                    detalle.CabeceraId = obj.CabeceraId;
                    detalle.Item = dao.Comprobante.BuscarUltimoItemDetalle(obj.Codemp, obj.Codsuc, obj.TipoComprobante, obj.CabeceraId);
                    //d.Insid = d.Insid;
                    detalle.Prodid = 0;
                    
                    detalle.Precio = detalle.Precio - (detalle.Precio * obj.Descuento);
                    //d.PrecioReal = d.Precio;

                    if (objInsumo.Exento == "S")
                    {
                        impuesto = 0;
                        Retenido = "N";
                        impExe = (detalle.PrecioReal * detalle.Cantidad);
                    }
                    else
                    {
                        impuesto = (detalle.PrecioReal * detalle.Cantidad) * (objImpuesto.Porcentaje / 100);
                        impExe = 0;
                    }
                    if (objImpuesto.Retenido == "S")
                    {
                        total = (detalle.PrecioReal * detalle.Cantidad) - impuesto;
                    }
                    else
                    {
                        total = (detalle.PrecioReal * detalle.Cantidad) + impuesto;
                    }
                    
                    detalle.Saldo = detalle.Saldo;
                    detalle.Neto = detalle.PrecioReal * detalle.Cantidad;
                    detalle.Impuesto = impuesto;
                    detalle.Retenido = Retenido;
                    detalle.Total = total;
                    detalle.Interes = 0;
                    detalle.Honorario = 0;
                    detalle.GastoPrejudicial = 0;
                    detalle.GastoJudicial = 0;
                    detalle.PorcFact = 0;
                    detalle.PorcHono = 0;
                    detalle.Bodid = 0;
                    detalle.Bdsid = 0;
                    detalle.Posicion = 0;

                    detalle.Tpcidpad = 0;
                    detalle.Numeropad = 0;
                    detalle.Itempad = 0;
                    detalle.Bodiddes = 0;
                    detalle.Bsciddes = "";
                    detalle.Posiciondes = 0;
                    detalle.NumeroSerie = 0;
                    detalle.NumeroSerieProv = "";
                    detalle.Cantebj = 0;
                    detalle.Ltpid = null;
                    detalle.Bscid = null;
                    detalle.Bsciddes = null;
                    detalle.Anio = null;
                    detalle.NumApl = null;
                    detalle.ItemApl = null;
                    detalle.ValRem = null;
                    detalle.Comentario = "";
                    detalle.Subitem = "";
                    detalle.Exento = impExe;
                    detalle.Rolid = obj.Rolid;
                    
                    procesoDetalle = dao.Comprobante.InsertarDetalleCabecera(detalle);
                    if (procesoDetalle > 0)
                    {
                        if (procesoDetalle > 0 && obj.Rolid != 0)
                        {
                            procesoDetalle = dao.Comprobante.InsertarDetalleComprobanteRol(detalle);
                        }
                        if (procesoDetalle > 0 && objInsumo.ImputableCliente == "S")
                        {
                            procesoDetalle = dao.Comprobante.InsertarDetalleComprobanteProvCli(detalle);
                        }
                    }
                }
                
            }
            return procesoDetalle;
        }
        public static int InsertarMotivoComprobante(UserSession objSession, CabeceraComprobante obj, List<string> lstMotivos, List<string> lstDeudores)
        {
            int procesoMotivo = -1;
            int ctcid = 0;
            for (int i = 0; i < lstDeudores.Count; i++)
            {
                ctcid = Int32.Parse(lstDeudores[i]);
                foreach (string motivo in lstMotivos){
                    procesoMotivo = dao.Comprobante.GrabarMotivoComprobante(objSession.CodigoEmpresa, objSession.CodigoSucursal, obj.TipoComprobante, obj.CabeceraId, Int32.Parse(motivo), ctcid);
                }
            }
            return procesoMotivo;
        }
        public static string[] GrabarCastigoDevolucion(CabeceraComprobante obj, UserSession objSession)
        {
            string mensaje = "", asegurado = "";
            int repetido = 0, ultimoId = 0, error = 0;
            List<Combobox> lstSubcartera = new List<Combobox>();

            repetido = dao.Comprobante.TraeNumeroComprobante(obj.Codemp, obj.Codsuc, obj.TipoComprobante, obj.Pclid, obj.Numero, obj.CabeceraId);

            switch (repetido)
            {
                case -1:
                    mensaje = "Hubo un problema al verificar el documento en el sistema.";
                    break;
                case 0:
                    ultimoId = dao.Comprobante.TraeUltimoNumeroComprobante(obj.Codemp, obj.Codsuc, obj.TipoComprobante);
                    if (obj.CabeceraId == 0)
                    {
                        obj.CabeceraId = ultimoId;
                        //if (obj.Rolid != 0)
                        //{
                        //    lstSubcartera = dao.Comprobante.TraeSubCarteraComprobante(obj.Codemp, obj.Rolid);
                        //    if (lstSubcartera.Count != 0)
                        //    {
                        //        asegurado = lstSubcartera[0].Value + " - " + lstSubcartera[0].Text;
                        //        if (!string.IsNullOrEmpty(lstSubcartera[1].Value) && !string.IsNullOrEmpty(lstSubcartera[1].Text))
                        //        {
                        //            asegurado = asegurado + ", ASEGURADO: " + lstSubcartera[1].Value + " - " + lstSubcartera[1].Text;
                        //        }
                        //    }
                        //}
                        //obj.Glosa = (string.IsNullOrEmpty(obj.Glosa) ? "" : obj.Glosa + ", ") + (!string.IsNullOrEmpty(asegurado) ? asegurado + ", " : "") + obj.TipoComprobanteDesc + ", " + "Numero" + ":" + obj.Numero + ", " + "Detalle" + ":" + obj.DetalleC[0].Nombre.Split('-')[1];
                        //if (obj.Pclid == 89 && !string.IsNullOrEmpty(lstSubcartera[2].Value))
                        //{
                        //    obj.Glosa = obj.Glosa + ", CODIGO CARGA: " + lstSubcartera[2].Value;
                        //}
                        //if (obj.Pclid == 424 && !string.IsNullOrEmpty(lstSubcartera[2].Text))
                        //{
                        //    obj.Glosa = obj.Glosa + ", NUMERO DOCUMENTO: " + lstSubcartera[2].Text;
                        //}
                        obj.Moneda = "1";
                        obj.FormaPago = "1";
                        obj.FechaDocumento = System.DateTime.Now;
                        obj.FechaVencimiento = System.DateTime.Now;
                        error = dao.Comprobante.InsertarCabecera(obj);
                        obj.Estado = "E";
                        if (error > 0)
                        {
                            error = dao.Comprobante.InsertarCabeceraOP(obj);
                            if (error > 0)
                            {
                                error = dao.Comprobante.InsertarCabeceraEstados(obj, objSession);
                                if (error > 0)
                                {
                                    if (obj.Estado == "E")
                                    {
                                        switch (obj.Tipo)
                                        {
                                            case "CC":
                                                GrabarDetalleCarteraCliente(obj);
                                                break;
                                            case "C":
                                                GrabarDetalleCpbtDoc(obj);
                                                break;
                                            case "V":
                                                GrabarDetalleCpbtDoc(obj);
                                                break;
                                        }
                                        //    Recalcular_Cpbt()
                                    }


                                    //Cargar()
                                }
                                else
                                {
                                    mensaje = "Hubo un error al crear el estado de la cabecera del comprobante.";
                                    break;
                                }
                            }
                            else
                            {
                                mensaje = "Hubo un error al crear la cabecera OP del comprobante.";
                                break;
                            }
                        }
                        else
                        {
                            mensaje = "Hubo un error al crear la cabecera del comprobante.";
                            break;
                        }
                    }
                    else
                    {
                        if (obj.Estado == "E")
                        {
                            error = dao.Comprobante.ActualizarCabecera(obj);
                            if (error >= 0)
                            {
                                error = dao.Comprobante.InsertarCabeceraOP(obj);
                            }
                            else
                            {
                                mensaje = "Hubo un error al actualizar la cabecera OP del comprobante.";
                                break;
                            }
                        }
                    }

                    break;
                default:
                    mensaje = "Documento ya se encuentra ingresado en el sistema.";
                    break;
            }

            return new string[] { ultimoId.ToString(), mensaje };
        }

        public static string[] GrabarDetalleCastigo(CabeceraComprobante obj, UserSession objSession)
        {
            string mensaje = "ERROR";
            ClasificacionInsumo objInsumo;
            ImpuestoComprobante objImpuesto;
            int error = 0, item = 0;
            //string tipoImpuesto = "";
            //if(obj.Libcompra == 2){
            //    tipoImpuesto = "R";
            //}
            decimal impuesto = 0, impExe = 0, total = 0;
            string Retenido = "S";


            foreach (DetalleCabeceraComprobante d in obj.DetalleC)
            {
                objInsumo = dao.Comprobante.TraeClasificacionInsumo(obj.Codemp, d.Insid);
                if (obj.Sinimp == "N")
                {
                    objImpuesto = dao.Comprobante.TraeImpuestoComprobante(obj.Codemp, objSession.CodigoSucursal, obj.TipoComprobante, obj.CabeceraId, obj.Libcompra == 2 ? "S" : "N");
                }
                else
                {
                    objImpuesto = new ImpuestoComprobante()
                    {
                        Porcentaje = 0,
                        Retenido = "N"
                    };
                }
                if (d.Retenido == "S")
                {
                    objImpuesto = new ImpuestoComprobante()
                    {
                        Porcentaje = 10,
                        Retenido = "S"
                    };
                    objInsumo.Exento = "N";
                    Retenido = "S";
                }
                if (d.Item == 0 && obj.Modo == "add")
                {
                    d.Codemp = obj.Codemp;
                    d.Codsuc = obj.Codsuc;
                    d.TipoComprobante = obj.TipoComprobante;
                    d.CabeceraId = obj.CabeceraId;
                    d.Item = dao.Comprobante.BuscarUltimoItemDetalle(obj.Codemp, obj.Codsuc, obj.TipoComprobante, obj.CabeceraId);
                    //d.Insid = d.Insid;
                    d.Prodid = 0;//null
                    d.Pclid = 0;//null
                    d.Ctcid = 0;//null
                    d.Ccbid = 0;//null
                    d.Precio = d.Precio - (d.Precio * obj.Descuento);
                    //d.PrecioReal = d.Precio;

                    if (objInsumo.Exento == "S")
                    {
                        impuesto = 0;
                        Retenido = "N";
                        impExe = (d.PrecioReal * d.Cantidad);
                    }
                    else
                    {
                        impuesto = (d.PrecioReal * d.Cantidad) * (objImpuesto.Porcentaje / 100);
                        impExe = 0;
                    }
                    if (objImpuesto.Retenido == "S")
                    {
                        total = (d.PrecioReal * d.Cantidad) - impuesto;
                    }
                    else
                    {
                        total = (d.PrecioReal * d.Cantidad) + impuesto;
                    }
                    //d.Cantidad =
                    d.Saldo = d.Cantidad;
                    d.Neto = d.PrecioReal * d.Cantidad;
                    d.Impuesto = impuesto;
                    d.Retenido = Retenido;
                    d.Total = total;
                    d.Interes = 0;
                    d.Honorario = 0;
                    d.GastoPrejudicial = 0;
                    d.GastoJudicial = 0;
                    d.PorcFact = 0;
                    d.PorcHono = 0;
                    d.Bodid = 0;
                    d.Bdsid = 0;
                    d.Posicion = 0;

                    d.Tpcidpad = 0;
                    d.Numeropad = 0;
                    d.Itempad = 0;
                    d.Bodiddes = 0;
                    d.Bsciddes = "";
                    d.Posiciondes = 0;
                    d.NumeroSerie = 0;
                    d.NumeroSerieProv = "";
                    d.Cantebj = 0;
                    d.Ltpid = null;
                    d.Bscid = null;
                    d.Bsciddes = null;
                    d.Anio = null;
                    d.NumApl = null;
                    d.ItemApl = null;
                    d.ValRem = null;
                    d.Comentario = "";
                    d.Subitem = "";
                    d.Exento = impExe;
                    d.Rolid = obj.Rolid;

                    error = dao.Comprobante.InsertarDetalleCabecera(d);
                    if (error > 0)
                    {
                        if (error > 0 && obj.Rolid != 0)
                        {
                            error = dao.Comprobante.InsertarDetalleComprobanteRol(d);
                        }
                        if (error > 0 && objInsumo.ImputableCliente == "S")
                        {
                            error = dao.Comprobante.InsertarDetalleComprobanteProvCli(d);
                        }
                    }
                }
                else
                {
                    d.Codemp = obj.Codemp;
                    d.Codsuc = obj.Codsuc;
                    d.TipoComprobante = obj.TipoComprobante;
                    d.CabeceraId = obj.CabeceraId;
                    error = dao.Comprobante.TraePadreDetalleComprobante(d);
                    if (error > 0)
                    {
                        //d.Item = d.Item;
                        //d.Insid = d.Insid;
                        d.Prodid = 0;//null
                        d.Pclid = 0;//null
                        d.Ctcid = 0;//null
                        d.Ccbid = 0;//null
                        d.Precio = d.Precio - (d.Precio * obj.Descuento);
                        //d.PrecioReal = d.Precio;

                        if (objInsumo.Exento == "S")
                        {
                            impuesto = 0;
                            Retenido = "N";
                            impExe = (d.PrecioReal * d.Cantidad);
                        }
                        else
                        {
                            impuesto = (d.PrecioReal * d.Cantidad) * (objImpuesto.Porcentaje / 100);
                            impExe = 0;
                        }
                        if (objImpuesto.Retenido == "S")
                        {
                            total = (d.PrecioReal * d.Cantidad) - impuesto;
                        }
                        else
                        {
                            total = (d.PrecioReal * d.Cantidad) + impuesto;
                        }
                        //d.Cantidad =
                        d.Saldo = d.Cantidad;
                        d.Neto = d.PrecioReal * d.Cantidad;
                        d.Impuesto = impuesto;
                        d.Retenido = Retenido;
                        d.Total = total;
                        d.Interes = 0;
                        d.Honorario = 0;
                        d.GastoPrejudicial = 0;
                        d.GastoJudicial = 0;
                        d.PorcFact = 0;
                        d.PorcHono = 0;
                        d.Bodid = 0;
                        d.Bdsid = 0;
                        d.Posicion = 0;
                        d.Tpcidpad = 0;
                        d.Numeropad = 0;
                        d.Itempad = 0;
                        d.Bodiddes = 0;
                        d.Bsciddes = "";
                        d.Posiciondes = 0;
                        d.NumeroSerie = 0;
                        d.NumeroSerieProv = "";
                        d.Cantebj = 0;
                        d.Ltpid = null;
                        d.Bscid = null;
                        d.Bsciddes = null;
                        d.Anio = null;
                        d.NumApl = null;
                        d.ItemApl = null;
                        d.ValRem = null;
                        d.Comentario = "";
                        d.Subitem = null;
                        d.Exento = impExe;
                        d.Rolid = obj.Rolid;
                        error = dao.Comprobante.ActualizarDetalleCabecera(d);
                    }
                }
            }

            if (error > 0)
            {
                error = dao.Comprobante.CuadrarDetalleCabeceraComprobante(obj);
            }
            if (error > -1)
            {
                mensaje = "OK";
            }
            //return mensaje;
            return new string[] { obj.CabeceraId.ToString(), mensaje };
        }


        #endregion

        #region "Castigo y Devolucion"

        public static int ActualizarEstadoCarteraClientesCastigoDevolucion(int codemp, int pclid, int ctcid, int estid, string estCpbt, string comentario)
        {
            return dao.Comprobante.ActualizarEstadoCarteraClientesCastigoDevolucion(codemp,  pclid, ctcid, estid, estCpbt, comentario);
        }
        public static int RechazarCastigoDevolucion(int codemp, int folio, int comprobanteId, int pclid, string motivo, int user)
        {
            int procesoRechazo = -1;
            procesoRechazo = dao.Comprobante.AprobarRechazarCastigoDevolucionComprobante(codemp, folio, comprobanteId, pclid, "X");
            if (procesoRechazo > 0)
            {
                procesoRechazo = dao.Comprobante.InsertCastigoDevolucionMotivoRechazo(codemp, folio, comprobanteId, pclid, motivo, user);
            }
            return procesoRechazo;
        }
        public static int AprobarCastigoDevolucion(List<dto.ComprobanteCabeceraDetalle> lst, int codemp, int folio, int comprobanteId, int pclid, string nombreComprobante, int user, string ipRed, string iPc)
        {
            int procesoAprueba = -1;
            //31 Castigo Prejudicial estado 21 CASTIGO PREJUDICIAL
            //32 CASTIGO JUDICIAL estado 71 CASTIGO JUDICIAL
            //33	DEVOLUCION DOC. DEUDOR estado 22 DEVOLUCION DE DOCUMENTO
            //34	DEVOLUCION DOC. CLIENTE estado 174 DEVOLUCION DE DOCUMENTOS AL CLIENTE
            int newEstado = 0;
            if (comprobanteId == 31)
            {
                newEstado = 21;
            }
            if (comprobanteId == 32)
            {
                newEstado = 71;
            }
            if (comprobanteId == 33)
            {
                newEstado = 22;
            }
            if (comprobanteId == 34)
            {
                newEstado = 174;
            }
            foreach (dto.ComprobanteCabeceraDetalle dato in lst)
            {
                procesoAprueba = dao.Comprobante.AprobarCastigoDevolucionComprobante(codemp, dato.Pclid, dato.Ctcid, dato.Ccbid, dato.Monto, dato.CbtEstado, newEstado, nombreComprobante, user, ipRed, iPc);
            }
            if (procesoAprueba > 0)
            {
                procesoAprueba = dao.Comprobante.AprobarRechazarCastigoDevolucionComprobante(codemp, folio, comprobanteId, pclid, "A");
            }
            if (procesoAprueba > 0)
            {
                List<int> Roleslist = new List<int>();
                foreach (dto.ComprobanteCabeceraDetalle s in lst)
                    Roleslist.Add(s.RolId);
                Roleslist = Roleslist.Distinct().ToList();//obtemos la cabecera a partir de la nueva lista
                if (Roleslist.Count > 0)
                    foreach (int rol in Roleslist)
                        if (rol != 0)
                            procesoAprueba = dao.Comprobante.AprobarCastigoDevolucionComprobanteRol(codemp, rol, newEstado, nombreComprobante, user, ipRed, iPc);
            }
        
           
            return procesoAprueba;
        }
        public static List<string> AprobarCastigoDevolucionReporte(List<dto.ComprobanteCabeceraDetalle> lst, List<string> lstReportes, int codemp, int sucursal, int idioma, int comprobanteId, int folio, int pclid)
        {
            Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();
            List<string> Deudoreslist = new List<string>();
            List<string> ziplist = new List<string>();
            foreach (dto.ComprobanteCabeceraDetalle dato in lst)
                Deudoreslist.Add(dato.Pclid + "|" + dato.Ctcid + "|" + dato.RutDeudor);
            Deudoreslist = Deudoreslist.Distinct().ToList();//obtemos la cabecera a partir de la nueva lista

            foreach (string reporte in lstReportes)
            {
                if (reporte == "1")
                {
                    if (Deudoreslist.Count > 0)
                        foreach (string deudor in Deudoreslist)
                        {
                            Dimol.Reportes.dto.ResumenGestiones obj = new Dimol.Reportes.dto.ResumenGestiones();
                            obj.Codemp = codemp;
                            obj.Idioma = idioma;
                            obj.Sucid = sucursal;
                            obj.Pclid = Int32.Parse(deudor.Split('|')[0]);
                            obj.Ctcid = Int32.Parse(deudor.Split('|')[1]);
                            obj.Pagina = 355;
                            obj.FechaReporte = DateTime.Now;
                            obj.IdReporte = 4;
                            string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];
                            string fileName = "resumen_gestiones_" + codemp + "_" + obj.Ctcid + "_" + DateTime.Now.ToString("yyyyMMddhhmmss")+ ".pdf";
                            string path = ubicacion + "Documentos\\" + codemp + "\\" + deudor.Split('|')[2] + "\\";
                            objFunc.CreaCarpetas(path);
                            obj.PathArchivo = path + fileName;
                            
                            bool ruta = Dimol.Reportes.bcp.Cartera.TraeResumenGestiones(obj);
                            int dcdid = -1;
                            if (ruta)
                            {
                                dcdid = bcp.Deudor.GuardarDocumentoDeudor(codemp, Int32.Parse(deudor.Split('|')[1]), 1, fileName, deudor.Split('|')[0]);
                                ziplist.Add(ConfigurationManager.AppSettings["UrlArchivosCastigo"] +  codemp + "/" + deudor.Split('|')[2] + "/" + fileName);
                            }
                        }
                }
                if (reporte == "2")//Certificado de incobrabilidad
                {
                    if (Deudoreslist.Count > 0)
                        foreach (string deudor in Deudoreslist)
                        {
                            if (comprobanteId == 31) //Prejudicial
                            {
                                Dimol.Reportes.dto.CastigoPrejudicialCliente objCliente = new Dimol.Reportes.dto.CastigoPrejudicialCliente();
                                objCliente.Codemp = codemp;
                                objCliente.Codsuc = sucursal;
                                objCliente.Tpcid = comprobanteId;
                                objCliente.Cbcnumero = folio;
                                objCliente.Pclid = Int32.Parse(deudor.Split('|')[0]);
                                objCliente.Ctcid = Int32.Parse(deudor.Split('|')[1]);
                                objCliente.Idioma = idioma;
                                objCliente.Empresa = "DIMOL SpA";
                                objCliente.FechaReporte = DateTime.Now;
                                objCliente.Pagina = 359;
                                objCliente.IdReporte = 1;
                                string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];
                                string fileName = "certificado_Incobrabilidad_" + codemp + "_" + Int32.Parse(deudor.Split('|')[1]) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                                string path = ubicacion + "Documentos\\" + codemp + "\\" + deudor.Split('|')[2] + "\\";
                                objFunc.CreaCarpetas(path);
                                objCliente.PathArchivo = path + fileName;
                                bool ruta = Dimol.Reportes.bcp.Cartera.TraeCastigoPrejudicialClienteNormal(objCliente);
                                int dcdid = -1;
                                if (ruta)
                                {
                                    dcdid = bcp.Deudor.GuardarDocumentoDeudor(codemp, Int32.Parse(deudor.Split('|')[1]), 1, fileName, deudor.Split('|')[0]);
                                    ziplist.Add(ConfigurationManager.AppSettings["UrlArchivosCastigo"] + codemp + "/" + deudor.Split('|')[2] + "/" + fileName);
                                }
                                System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                            }
                            if (comprobanteId == 32)//judicial 
                            {
                                Dimol.Reportes.dto.CastigoJudicialCliente objCliente = new Dimol.Reportes.dto.CastigoJudicialCliente();
                                objCliente.Codemp = codemp;
                                objCliente.Codsuc = sucursal;
                                objCliente.Tpcid = comprobanteId;
                                objCliente.CbcDesde = folio;
                                objCliente.Pclid = Int32.Parse(deudor.Split('|')[0]);
                                objCliente.Ctcid = Int32.Parse(deudor.Split('|')[1]);
                                objCliente.CbcHasta = folio;
                                objCliente.Idioma = idioma;
                                objCliente.Empresa = "DIMOL SpA";
                                objCliente.FechaReporte = DateTime.Now;
                                objCliente.Pagina = 359;
                                objCliente.IdReporte = 4;
                                objCliente.RutAbogado = "138315541";
                                objCliente.NombreAbogado = "MAY GUTIERREZ OTTO";
                                string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];
                                string fileName = "certificado_Incobrabilidad_Judicial_" + codemp + "_" + Int32.Parse(deudor.Split('|')[1]) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                                string path = ubicacion + "Documentos\\" + codemp + "\\" + deudor.Split('|')[2] + "\\";
                                objFunc.CreaCarpetas(path);
                                objCliente.PathArchivo = path + fileName;
                                bool ruta = Dimol.Reportes.bcp.Cartera.TraeCastigoJudicialNormal(objCliente);
                                int dcdid = -1;
                                if (ruta)
                                {
                                    dcdid = bcp.Deudor.GuardarDocumentoDeudor(codemp, Int32.Parse(deudor.Split('|')[1]), 1, fileName, deudor.Split('|')[0]);
                                    ziplist.Add(ConfigurationManager.AppSettings["UrlArchivosCastigo"] + codemp + "/" + deudor.Split('|')[2] + "/" + fileName);
                                }
                                System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                            }
                            if (comprobanteId == 33 || comprobanteId == 34)
                            {
                                Dimol.Reportes.dto.DevolucionDocumentosCliente objCliente = new Dimol.Reportes.dto.DevolucionDocumentosCliente();
                                objCliente.Codemp = codemp;
                                objCliente.Codsuc = sucursal;
                                objCliente.Tpcid = comprobanteId;
                                objCliente.Cbcnumero= folio;
                                objCliente.Pclid = Int32.Parse(deudor.Split('|')[0]);
                                objCliente.Ctcid = Int32.Parse(deudor.Split('|')[1]);
                                objCliente.Idioma = idioma;
                                objCliente.FechaReporte = DateTime.Now;
                                objCliente.Pagina = 359;
                                objCliente.IdReporte = 5;
                                string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];
                                string fileName = "devolucion_documentos_" + codemp + "_" + Int32.Parse(deudor.Split('|')[1]) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                                string path = ubicacion + "Documentos\\" + codemp + "\\" + deudor.Split('|')[2] + "\\";
                                objFunc.CreaCarpetas(path);
                                objCliente.PathArchivo = path + fileName;
                                bool ruta = Dimol.Reportes.bcp.Cartera.TraeDevolucionDocumentosNormal(objCliente);
                                int dcdid = -1;
                                if (ruta)
                                {
                                    dcdid = bcp.Deudor.GuardarDocumentoDeudor(codemp, Int32.Parse(deudor.Split('|')[1]), 1, fileName, deudor.Split('|')[0]);
                                    ziplist.Add(ConfigurationManager.AppSettings["UrlArchivosCastigo"] + codemp + "/" + deudor.Split('|')[2] + "/" + fileName);
                                }
                                System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                            }
                        }
                }
                if (reporte == "3")//HOJA TRAMITE
                {
                    if (Deudoreslist.Count > 0)
                        foreach (string deudor in Deudoreslist)
                        {
                            //string[] cbcidList = new string[] { };
                            List<int> cbcidList = new  List<int>();
                            foreach (dto.ComprobanteCabeceraDetalle dato in lst)
                            {
                                if (dato.Pclid == Int32.Parse(deudor.Split('|')[0]) && dato.Ctcid == Int32.Parse(deudor.Split('|')[1]))
                                {
                                    cbcidList.Add(dato.Ccbid);
                                }
                            }

                            Dimol.Reportes.dto.HojaTramite objCliente = new Dimol.Reportes.dto.HojaTramite();
                            objCliente.Ccbid = string.Join(",", cbcidList.ToArray());
                            objCliente.Codemp = codemp;
                            objCliente.Pclid = Int32.Parse(deudor.Split('|')[0]);
                            objCliente.Ctcid = Int32.Parse(deudor.Split('|')[1]);
                            objCliente.Idioma = idioma;
                            objCliente.FechaReporte = DateTime.Now;
                            objCliente.Pagina = 358;
                            objCliente.IdReporte = 9;
                            string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];
                            string fileName = "hoja_Tramite_" + codemp + "_" + Int32.Parse(deudor.Split('|')[1]) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                            string path = ubicacion + "Documentos\\" + codemp + "\\" + deudor.Split('|')[2] + "\\";
                            objFunc.CreaCarpetas(path);
                            objCliente.PathArchivo = path + fileName;
                            bool ruta = Dimol.Reportes.bcp.Judicial.TraeHojaTramiteClienteParcial(objCliente);
                            int dcdid = -1;
                            if (ruta)
                            {
                                dcdid = bcp.Deudor.GuardarDocumentoDeudor(codemp, Int32.Parse(deudor.Split('|')[1]), 1, fileName, deudor.Split('|')[0]);
                                ziplist.Add(ConfigurationManager.AppSettings["UrlArchivosCastigo"] + codemp + "/" + deudor.Split('|')[2] + "/" + fileName);
                            }
                            System.IO.File.Delete(objCliente.PathArchivo + ".fo");
                        }
                }
                if (reporte == "4") //Sii
                { 
                    if (Deudoreslist.Count > 0)
                        foreach (string deudor in Deudoreslist){
                            string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];
                            string fileName = "certificado_SII_" + codemp + "_" + Int32.Parse(deudor.Split('|')[1]) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                            string path = ubicacion + "Documentos\\" + codemp + "\\" + deudor.Split('|')[2] + "\\";
                            objFunc.CreaCarpetas(path);

                            Dimol.Reportes.bcp.Cartera.GeneraPDFSII(Int32.Parse(deudor.Split('|')[1]), path, fileName);
                            int dcdid = bcp.Deudor.GuardarDocumentoDeudor(codemp, Int32.Parse(deudor.Split('|')[1]), 1, fileName, deudor.Split('|')[0]);
                            ziplist.Add(ConfigurationManager.AppSettings["UrlArchivosCastigo"] + codemp + "/" + deudor.Split('|')[2] + "/" + fileName);
                            System.IO.File.Delete(path + fileName + ".fo");
                        }
                   
                }
                if (reporte == "5") //Estampes
                {
                    foreach (string deudor in Deudoreslist){
                        List<string> Estampelist = new List<string>();
                        Estampelist = bcp.Deudor.ListarRutaEstampesDeudor(codemp, Int32.Parse(deudor.Split('|')[0]), Int32.Parse(deudor.Split('|')[1]));
                        foreach (string estampe in Estampelist)
                        {
                            //.substr(imgUrl.indexOf('/', 7) + 1)
                            ziplist.Add(ConfigurationManager.AppSettings["UrlArchivosCastigo"] + "/" + estampe);
                        }
                    }
                    
                }
             }
        
           
            return ziplist;
        }
        #endregion

        public KeyValuePair<string, string> TraeTerceroNombreRut(int codemp, int terceroId)
        {
            return dao.Deudor.BuscarTerceroRutNombre(codemp, terceroId);
        }
        public int BuscarTercero(int codemp, string rutTercero)
        {
            return dao.Deudor.BuscarTercero(codemp, rutTercero);
        }
        public int GuardarTercero(int codemp, string rutTercero, string nombreTercero)
        {
            return dao.Deudor.GuardarTercero(codemp, rutTercero, nombreTercero);
        }
        public static List<dto.TerceroDocumento> ListarTercerosDocumentos(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarTercerosDocumentos(codemp, pclid, ctcid, where, sidx, sord, inicio, limite);
        }
        public static List<dto.MotivoCastigoDevolucion> ListarMotivoCastigoDevolucion(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarMotivoCastigoDevolucion(codemp, idioma, where, sidx, sord, inicio, limite);
        }
        public static List<dto.ComprobanteCabecera> ListarComprobantesAprobarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarComprobantesAprobarGrilla(codemp, where, sidx, sord, inicio, limite);
        }
        public static List<dto.ComprobanteCabeceraDetalle> ListarComprobantesAprobarDetalleGrilla(int codemp, int tipoComprobante, int folio, int pclid,
                                                                                                string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Comprobante.ListarComprobantesAprobarDetalleGrilla(codemp, tipoComprobante,folio, pclid, where, sidx, sord, inicio, limite);
        }
        public static int AprobarAvenimiento(int codemp)
        {
            int proceso = -1;
            //obtengo la secuencia de nuevos documentos
            List<dto.Comprobante> lstDocumentos = dao.Comprobante.ListarPanelAvenimientoNuevosDocumentos(codemp, 86, 1223905,1, "5999347", "28/10/2017", Int32.Parse("10"), "60.270,00");
            bcp.Comprobante objComprobante = new bcp.Comprobante();
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (Dimol.Carteras.dto.Comprobante documento in lstDocumentos)
                {
                    //Grabar nuevos documentos
                    proceso = objComprobante.GrabarDocumento(documento, codemp);


                }
            }
            
           return proceso;
        }
    }
}
