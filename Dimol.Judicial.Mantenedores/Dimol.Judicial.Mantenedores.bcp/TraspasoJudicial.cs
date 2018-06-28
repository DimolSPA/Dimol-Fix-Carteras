using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class TraspasoJudicial
    {
        public static List<dto.TraspasoJudicialCandidato> ListarTraspasosGrilla(int codemp, int idioma, int perfil, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosGrilla(codemp, idioma, perfil, where, sidx, sord, inicio, limite);
        }
        public static List<dto.TraspasoJudicialCandidato> ListarTraspasosGrillaPrevisional(int codemp, int idioma, int perfil, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosGrillaPrevisional(codemp, idioma, perfil, where, sidx, sord, inicio, limite);
        }

        public static int ListarTraspasosGrillaCount(int codemp, int idioma, int perfil, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosGrillaCount(codemp, idioma, perfil, where, sidx, sord, inicio, limite);
        }
        public static int ListarTraspasosGrillaCountPrevisional(int codemp, int idioma, int perfil, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosGrillaCountPrevisional(codemp, idioma, perfil, where, sidx, sord, inicio, limite);
        }

        public static List<dto.TraspasoJudicialPendiente> ListarTraspasosPendientesGrilla(int codemp, int codsuc, int idioma, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosPendientesGrilla(codemp,  codsuc,  idioma,  fechaDesde,  fechaHasta, where, sidx, sord, inicio, limite);
        }

        public static int ListarTraspasosPendientesGrillaCount(int codemp, int codsuc, int idioma, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosPendientesGrillaCount(codemp, codsuc, idioma, fechaDesde, fechaHasta, where, sidx, sord, inicio, limite);
        }

        public static List<dto.TraspasoJudicialHecho> ListarTraspasosHechosGrilla(int codemp, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosHechosGrilla(codemp,  fechaDesde, fechaHasta, where, sidx, sord, inicio, limite);
        }
        public static List<dto.TraspasoJudicialHecho> ListarTraspasosHechosGrillaPrevisional(int codemp, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosHechosGrillaPrevisional(codemp, fechaDesde, fechaHasta, where, sidx, sord, inicio, limite);
        }

        public static List<dto.TraspasoJudicialHecho> ListarTraspasosHechosDeudorGrilla(int codemp, int ctcid, string where, string sidx, string sord)
        {
            return dao.TraspasoJudicial.ListarTraspasosHechosDeudorGrilla(codemp, ctcid, where, sidx, sord);
        }

        public static int ListarTraspasosHechosGrillaCount(int codemp,  DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosHechosGrillaCount(codemp, fechaDesde, fechaHasta, where, sidx, sord, inicio, limite);
        }
        public static int ListarTraspasosHechosGrillaCountPrevisional(int codemp, DateTime fechaDesde, DateTime fechaHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarTraspasosHechosGrillaCountPrevisional(codemp, fechaDesde, fechaHasta, where, sidx, sord, inicio, limite);
        }

        public static string TraspasoDeudores(List<string> lst, Dimol.dto.UserSession objSesion)
        {
            string salida = "";
            int pclid;
            int ctcid;
            bool error = false;
            Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();
            int estid = objFunc.ConfiguracionEmpNum(objSesion.CodigoEmpresa, 68);

            List<string> Cabeceralist = new List<string>();
            foreach (string s in lst)
                Cabeceralist.Add(s.Split('|')[0] + "|" + s.Split('|')[1]);
            Cabeceralist = Cabeceralist.Distinct().ToList(); //Obtenemos la cabecera a partir de la nueva lista

            using (TransactionScope scope = new TransactionScope())
            {
                foreach (string cabecera in Cabeceralist)
                {
                    pclid = Int32.Parse(cabecera.Split('|')[0]);
                    ctcid = Int32.Parse(cabecera.Split('|')[1]);
                    List<string> Documentoslist = new List<string>();

                    //Buscamos los documentos correspondientes a la cabecera
                    foreach (string s in lst)
                        if (Int32.Parse(s.Split('|')[0]) == pclid && Int32.Parse(s.Split('|')[1]) == ctcid)
                            Documentoslist.Add(s);
                    
                    //Insertar panel Demanda
                    error = bcp.PanelDemanda.InsertarPanelDemanda(objSesion.CodigoEmpresa, objSesion.Idioma, pclid, ctcid, objSesion.UserId, Documentoslist) <= 0;
                    foreach (string doc in Documentoslist)
                    {
                        if (!error)
                            error = dao.TraspasoJudicial.InsertarCarteraClientesEstadosHistorialEspecial(objSesion.CodigoEmpresa, pclid, ctcid, Int32.Parse(doc.Split('|')[2]), DateTime.Now, estid, objSesion.CodigoSucursal, objSesion.Gestor, objSesion.IpRed, objSesion.IpPc, "DOCUMENTO INGRESA A JUDICIAL", decimal.Parse(doc.Split('|')[3]), decimal.Parse(doc.Split('|')[4]), objSesion.UserId) <= 0;

                        if (!error)
                            error = dao.TraspasoJudicial.ActualizarCarteraClientesEstadosTraspasoJudicial(objSesion.CodigoEmpresa, pclid, ctcid, Int32.Parse(doc.Split('|')[2]), estid, objSesion.UserId) <= 0;
                    }
                }

                if (!error)
                {
                    scope.Complete();
                }
            }
            
            return salida;
        }
        public static string TraspasoDeudoresPrevisional(List<string> lst, Dimol.dto.UserSession objSesion)
        {
            string salida = "";
            int pclid;
            int ctcid;
            bool error = false;
            Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();
            int estid = objFunc.ConfiguracionEmpNum(objSesion.CodigoEmpresa, 68);

            List<string> Cabeceralist = new List<string>();
            foreach (string s in lst)
                Cabeceralist.Add(s.Split('|')[0] + "|" + s.Split('|')[1]);
            Cabeceralist = Cabeceralist.Distinct().ToList(); //Obtenemos la cabecera a partir de la nueva lista

            using (TransactionScope scope = new TransactionScope())
            {
                foreach (string cabecera in Cabeceralist)
                {
                    pclid = Int32.Parse(cabecera.Split('|')[0]);
                    ctcid = Int32.Parse(cabecera.Split('|')[1]);
                    List<string> ListaResoluciones = new List<string>();

                    //Buscamos los documentos correspondientes a la cabecera
                    foreach (string s in lst)
                        if (Int32.Parse(s.Split('|')[0]) == pclid && Int32.Parse(s.Split('|')[1]) == ctcid)
                            ListaResoluciones.Add(s.Split('|')[2]);

                    //Insertar panel Demanda
                    error = bcp.PanelDemanda.InsertarPanelDemandaPrevisional(objSesion.CodigoEmpresa, objSesion.Idioma, pclid, ctcid, objSesion.UserId, ListaResoluciones) <= 0;

                    //foreach (string doc in ListaResoluciones)
                    //{
                    //    if (!error)
                    //        error = dao.TraspasoJudicial.InsertarCarteraClientesEstadosHistorialEspecial(objSesion.CodigoEmpresa, pclid, ctcid, Int32.Parse(doc.Split('|')[2]), DateTime.Now, estid, objSesion.CodigoSucursal, objSesion.Gestor, objSesion.IpRed, objSesion.IpPc, "DOCUMENTO INGRESA A JUDICIAL", decimal.Parse(doc.Split('|')[3]), decimal.Parse(doc.Split('|')[4]), objSesion.UserId) <= 0;

                    //    if (!error)
                    //        error = dao.TraspasoJudicial.ActualizarCarteraClientesEstadosTraspasoJudicial(objSesion.CodigoEmpresa, pclid, ctcid, Int32.Parse(doc.Split('|')[2]), estid, objSesion.UserId) <= 0;
                    //}
                    foreach (var Resolucion in ListaResoluciones)
                    {
                        List<dto.DocumentoRol> DocumentosPorResolucíon = dao.Rol.ListarDocumentosPorNumeroResolucion(Resolucion);

                        foreach (var Doc in DocumentosPorResolucíon)
                        {
                            if (!error)
                                error = dao.TraspasoJudicial.InsertarCarteraClientesEstadosHistorialEspecial(objSesion.CodigoEmpresa, pclid, ctcid, Doc.Ccbid, DateTime.Now, estid, objSesion.CodigoSucursal, objSesion.Gestor, objSesion.IpRed, objSesion.IpPc, "DOCUMENTO INGRESA A JUDICIAL", Doc.Monto, Doc.Saldo, objSesion.UserId) <= 0;

                            if (!error)
                                error = dao.TraspasoJudicial.ActualizarCarteraClientesEstadosTraspasoJudicial(objSesion.CodigoEmpresa, pclid, ctcid, Doc.Ccbid, estid, objSesion.UserId) <= 0;
                        }
                    }
                }

                if (!error)
                {
                    scope.Complete();
                }
            }

            return salida;
        }

        public static string TraspasoDeudoresOLD(List<string> lst, Dimol.dto.UserSession objSesion)
        {
            string salida = "";
            string[] id;
            int pclid;
            int ctcid;
            //int primerNumero = 0;
            bool error = false;
            Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();
            int estid = objFunc.ConfiguracionEmpNum(objSesion.CodigoEmpresa, 68);
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (string s in lst)
                {
                    id = s.Split('|');
                    pclid = Int32.Parse(id[0]);
                    ctcid = Int32.Parse(id[1]);
                    //primerNumero = dao.TraspasoJudicial.NuevoNumeroComprobante(objSesion.CodigoEmpresa, objSesion.CodigoSucursal, tpcid);
                    // Insertar panel Demanda
                    error = bcp.PanelDemanda.InsertarPanelDemandaOLD(objSesion.CodigoEmpresa, objSesion.Idioma, pclid, ctcid, objSesion.UserId) <= 0;
                    List<dto.DocumentoTraspasar> lstDocumentos = dao.TraspasoJudicial.ListarDocumentosTraspasar(objSesion.CodigoEmpresa, objSesion.Idioma, pclid, ctcid);
                    foreach (dto.DocumentoTraspasar t in lstDocumentos)
                    {
                        if (!error)
                        {
                            error = dao.TraspasoJudicial.InsertarCarteraClientesEstadosHistorialEspecial(objSesion.CodigoEmpresa, pclid, ctcid, t.Ccbid, DateTime.Now, estid, objSesion.CodigoSucursal, objSesion.Gestor, objSesion.IpRed, objSesion.IpPc, "DOCUMENTO INGRESA A JUDICIAL", t.Monto, t.Saldo, objSesion.UserId) <= 0;
                        }
                        if (!error)
                        {
                            error = dao.TraspasoJudicial.ActualizarCarteraClientesEstadosTraspasoJudicial(objSesion.CodigoEmpresa, pclid, ctcid, t.Ccbid, estid, objSesion.UserId) <= 0;
                        }


                    }

                }
                if (!error)
                {
                    scope.Complete();
                }
            }
            return salida;
        }
        public static List<dto.DocumentoReversar> ListarDocumentosReversaGrilla(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarDocumentosReversaGrilla(codemp, pclid, ctcid, where, sidx,  sord, inicio, limite);
        }

        public static int ListarDocumentosReversaGrillaCount(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.TraspasoJudicial.ListarDocumentosReversaGrillaCount(codemp, pclid, ctcid, where, sidx, sord, inicio, limite);
        }

        public static string ReversaDocumentoDeudores(List<string> lst, int estid, string comentario, Dimol.dto.UserSession objSesion)
        {
            string salida = "";
            string[] id;
            int pclid, ctcid, ccbid;
            decimal monto, saldo;
            //int primerNumero = 0;
            bool error = false;
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (string s in lst)
                {
                    id = s.Split('|');
                    pclid = Int32.Parse(id[0]);
                    ctcid = Int32.Parse(id[1]);
                    ccbid = Int32.Parse(id[2]);
                    monto = decimal.Parse(id[3]);
                    saldo = decimal.Parse(id[4]);
                    if (saldo == 0)
                    {
                        saldo = dao.TraspasoJudicial.TraeUltimoSaldoReversa(objSesion.CodigoEmpresa, pclid, ctcid, ccbid);
                    }
                    error = dao.TraspasoJudicial.InsertarCarteraClientesEstadosHistorialEspecial(objSesion.CodigoEmpresa, pclid, ctcid, ccbid, DateTime.Now, estid, objSesion.CodigoSucursal, objSesion.Gestor, objSesion.IpRed, objSesion.IpPc, comentario.ToUpper() , monto, saldo, objSesion.UserId) <= 0;
                    if (!error)
                    {
                        error = dao.TraspasoJudicial.ReversaEstadoTraspasoJudicial(objSesion.CodigoEmpresa, pclid, ctcid, ccbid, estid, objSesion.UserId, saldo) <= 0;

                    }

                }
                if (!error)
                {
                    scope.Complete();
                }
            }
            return salida;
        }

        public static List<Combobox> ListarEstadosReversa(int codemp, int idioma, string first)
        {
            return dao.TraspasoJudicial.ListarEstadosReversa(codemp, idioma, first);
        }

        public static string DocumentosNoDemandables(List<string> lst, Dimol.dto.UserSession objSesion, string comentario)
        {
            string salida = "";
            int pclid;
            int ctcid;
            bool error = false;
            Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();
            int estid = objFunc.ConfiguracionEmpNum(objSesion.CodigoEmpresa, 68);//Ingresa a Judicial

            List<string> Cabeceralist = new List<string>();
            foreach (string s in lst)
                Cabeceralist.Add(s.Split('|')[0] + "|" + s.Split('|')[1]);
            Cabeceralist = Cabeceralist.Distinct().ToList();//obtemos la cabecera a partir de la nueva lista

            using (TransactionScope scope = new TransactionScope())
            {
                foreach (string cabecera in Cabeceralist)
                {
                    pclid = Int32.Parse(cabecera.Split('|')[0]);
                    ctcid = Int32.Parse(cabecera.Split('|')[1]);
                    List<string> numDocList = new List<string>();
                    List<string> Documentoslist = new List<string>();
                    //Buscamos los documentos correspondientes al la cabecera
                    foreach (string s in lst)
                        if (Int32.Parse(s.Split('|')[0]) == pclid && Int32.Parse(s.Split('|')[1]) == ctcid)
                            Documentoslist.Add(s);
                    
                    //Buscamos los numeros documentos en la lista filtrada
                    foreach (string numDoc in Documentoslist)
                        numDocList.Add(numDoc.Split('|')[5]);
                            
                    //Grabo la accion como Gestion Interna del deudor, involucrando los distintos documentos
                    string numDocumentos = string.Join(",", numDocList.ToArray());
                    string detalleComentario = "Documento " + numDocumentos + ", no demandado por: " + comentario;
                    error = Dimol.Carteras.bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(objSesion.CodigoEmpresa, pclid, ctcid, 11, objSesion.CodigoSucursal, 0, "N", objSesion.IpRed, objSesion.IpPc, objSesion.UserId, detalleComentario, 0, Int64.Parse("0")) <= 0;
                       
                    foreach (string doc in Documentoslist)
                    {
                        if (!error)
                            error = dao.TraspasoJudicial.InsertarCarteraClientesEstadosHistorialEspecial(objSesion.CodigoEmpresa, pclid, ctcid, Int32.Parse(doc.Split('|')[2]), DateTime.Now, estid, objSesion.CodigoSucursal, objSesion.Gestor, objSesion.IpRed, objSesion.IpPc, "DOCUMENTO INGRESA A JUDICIAL", decimal.Parse(doc.Split('|')[3]), decimal.Parse(doc.Split('|')[4]), objSesion.UserId) <= 0;

                        if (!error)
                            error = dao.TraspasoJudicial.ActualizarCarteraClientesEstadosTraspasoJudicial(objSesion.CodigoEmpresa, pclid, ctcid, Int32.Parse(doc.Split('|')[2]), estid, objSesion.UserId) <= 0;
                        
                        if (!error)
                            error = dao.TraspasoJudicial.ActualizarCarteraClientesComentario(objSesion.CodigoEmpresa, pclid, ctcid, Int32.Parse(doc.Split('|')[2]), comentario, objSesion.UserId) <= 0;
                    }
                }
                if (!error)
                {
                    scope.Complete();
                }
            }

            return salida;
        }
    }
}
