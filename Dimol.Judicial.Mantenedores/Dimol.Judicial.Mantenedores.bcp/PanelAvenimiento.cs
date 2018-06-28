using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Dimol.Judicial.Mantenedores.bcp
{
    public class PanelAvenimiento
    {
        public static int InsertarPanelAvenimiento(int codemp, int rolId, string rolNumero, int pclid, int ctcid, int tribunalId, int userId)
        {
            return dao.PanelAvenimiento.InsertarPanelAvenimiento(codemp, rolId, rolNumero, pclid, ctcid, tribunalId, userId);
        }
        public static List<dto.PanelAvenimiento> ListarPanelAvenimientoGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.PanelAvenimiento.ListarPanelAvenimientoGrilla(codemp, where, sidx, sord);
        }
        public static int AprobarAvenimiento(int codemp, int idioma, int sucursal, int rolid, int pclid, int ctcid, string fechaAvenimiento, string montoAvenimiento, string cuotasAvenimiento, 
                                                string montoCuotaAvenimiento, string montoUltimaCuotaAvenimiento, string fechaPrimeraCuotaAvenimiento, 
                                                string fechaUltimaCuotaAvenimiento, string interesAvenimiento, int user, string ipRed, string ipPc)
        {
            int procesoDocumento = -1;
            bool noJudicial = false;
            DateTime fechaAccion = new DateTime();
            List<dto.DocumentoRol> lstDocsAsig = bcp.Rol.ListarDocumentosAsignadosGrilla(codemp, idioma, rolid, "", "Numero", "asc", 0, 100000);
            if (lstDocsAsig.Count > 0)
            {
                foreach (dto.DocumentoRol docVerifica in lstDocsAsig)
                {
                    if (docVerifica.Estado != "J")
                    {
                        noJudicial = true; 
                        break;
                    }
                }
                if (noJudicial)
                {
                    procesoDocumento = -3; //No hay documentos judiciales
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //Grabo la accion
                        procesoDocumento = Dimol.Carteras.bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(codemp, pclid, ctcid, 11, sucursal, 0, "N", ipRed, ipPc, user, "", 0, Int64.Parse("0"));
                        
                        if (procesoDocumento >= 0)
                        {
                            //Tomo la fecha de la gestion
                            fechaAccion = Dimol.Carteras.bcp.Accion.BuscarUltimaFechaAcciones(codemp, pclid, ctcid, 11);
                            foreach (dto.DocumentoRol doc in lstDocsAsig)
                            {
                                //obtengo la secuencia de nuevos documentos
                                List<Dimol.Carteras.dto.Comprobante> lstDocumentos = dao.PanelAvenimiento.ListarPanelAvenimientoNuevosDocumentos(codemp, pclid, ctcid, doc.Ccbid, doc.Numero, fechaPrimeraCuotaAvenimiento, Int32.Parse(cuotasAvenimiento), montoCuotaAvenimiento);
                                Dimol.Carteras.bcp.Comprobante objComprobante = new Dimol.Carteras.bcp.Comprobante();
                                foreach (Dimol.Carteras.dto.Comprobante documento in lstDocumentos)
                                {
                                    //Grabar nuevos documentos
                                    procesoDocumento = objComprobante.GrabarDocumento(documento, codemp);
                                    //Agregar gestion al documento como Documento sin vencer
                                    //Actualizo historial de los documentos
                                    if (procesoDocumento >= 0)
                                    {
                                        procesoDocumento = Dimol.Carteras.bcp.Comprobante.InsertarCarteraClientesEstadosHistorialEspecial(codemp, pclid, ctcid, procesoDocumento, fechaAccion, Int32.Parse(documento.EstadoCartera), sucursal, 0, ipRed, ipPc, "", documento.Monto, documento.Saldo, user);
                                    }

                                }
                                if (procesoDocumento >= 0)
                                {
                                    //Finalizar documento Original
                                    procesoDocumento = dao.PanelAvenimiento.FinalizarDocumentoAvenimiento(codemp, pclid, ctcid, doc.Ccbid, doc.Monto, doc.Estado, "Se llegó a un acuerdo");
                                }
                            }
                        }
                        if (procesoDocumento >= 0)
                        {
                            procesoDocumento = dao.PanelAvenimiento.ActualizarEstatusPanelAvenimiento(codemp, rolid, pclid, ctcid, "A");
                        }
                        if (procesoDocumento >= 0)
                        {
                            scope.Complete();
                        }
                    }
                }
            }
            else
            {
                procesoDocumento = -2; //No hay documentos asignados
            }
            return procesoDocumento;
        }
    }
}
