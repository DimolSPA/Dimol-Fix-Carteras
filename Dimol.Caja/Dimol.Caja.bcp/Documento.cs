using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dimol.dto;
namespace Dimol.Caja.bcp
{
    public class Documento
    {
        public static List<dto.Documento> ListarCajaIngresoDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.Documento.ListarCajaIngresoDocumentosGrilla(codemp, where, sidx, sord);
        }
        public static int InsertUpdateDocumentoCaja(string documentoId, int codemp, string numeroDocumento, int pclid, int ctcid, string sbcid,
                                                    int codmon, string mtoIngreso, int estatus, int user)
        {
            int result = -1;
            int id = -1;
            int criterioId = -1;

            if (!string.IsNullOrEmpty(documentoId))//Se está modificando
            {
                result = dao.Documento.ValidaIngresoCriterioDocumento(Int32.Parse(documentoId));
                if (result > 0)//tiene Criterio Definido
                {
                    //Valido si se ha modificado el monto de ingreso
                    result = dao.Documento.ValidaModificaMontoDocumento(Int32.Parse(documentoId), mtoIngreso);
                    if (result == 1)//Se Modificó
                    {
                        criterioId = dao.Documento.CriterioPorDefecto(codemp, pclid, Int32.Parse(documentoId));
                        if (criterioId > 0)
                        {//Tiene criterio que aplicar por defecto
                            List<dto.DocumentoCriterio> lst = dao.Documento.TraeCajaRecepcionDocumentosCriterio(Int32.Parse(documentoId), criterioId);
                            foreach (dto.DocumentoCriterio p in lst)
                            {
                                if (p.IsEditable != "S")
                                    result = dao.Documento.GuardarCajaRecepcionDocumentosCriterio(Int32.Parse(documentoId), criterioId, p.MontoFacturar.ToString("N2"), p.Observaciones, 1);
                            }
                        }
                    }
                }
            }

            id = dao.Documento.InsertUpdateDocumentoCaja(documentoId, codemp, numeroDocumento, pclid, ctcid, sbcid, codmon, mtoIngreso, estatus, user);

            if (id > 0)
                result = dao.Documento.ValidaIngresoCriterioDocumento(id);
            if (result == 0) { //No tiene Criterio Definido
                criterioId = dao.Documento.CriterioPorDefecto(codemp, pclid, id);
                if (criterioId > 0)
                {//Tiene criterio que aplicar por defecto
                    List<dto.DocumentoCriterio> lst = dao.Documento.TraeCajaRecepcionDocumentosCriterio(id, criterioId);
                    foreach (dto.DocumentoCriterio p in lst)
                    {
                        if (p.IsEditable != "S")
                            result = dao.Documento.GuardarCajaRecepcionDocumentosCriterio(id, criterioId, p.MontoFacturar.ToString("N2"), p.Observaciones, 1);
                    }
                }
            }
            
            return id;
        }
        public static List<dto.Documento> ListarCajaTraspasoDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.Documento.ListarCajaTraspasoDocumentosGrilla(codemp, where, sidx, sord);
        }
        public static string TraspasoComercial(List<string> lst, int codemp, int user)
        {
            string salida = "";
            string[] id;
            
            bool error = false;
            foreach (string s in lst)
            {
                id = s.Split('|');
                if (id[1] == "1" || id[2] == "0") //Es recepcionado o no ha sido traspasado
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //Se pasa a En proceso
                        error = dao.Documento.TraspasarComercial(id[0], codemp, 2, user) <= 0;

                        if (!error)
                        {
                            scope.Complete();
                        }
                    }
                }
              
            }
            return salida;
        }
        public static string TraspasoComercialIngresado(List<string> lst, int codemp, int user)
        {
            string salida = "";
            string[] id;

            bool error = false;
            foreach (string s in lst)
            {
                id = s.Split('|');
                if (id[1] == "2" || id[2] == "1") //Es en Proceso o ya fue traspasado
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //Se pasa a En proceso
                        error = dao.Documento.TraspasarComercial(id[0], codemp, 3, user) <= 0;

                        if (!error)
                        {
                            scope.Complete();
                        }
                    }
                }

            }
            return salida;
        }

        public static string TraspasoFinanzas(List<string> lst, int codemp, int user)
        {
            string salida = "";
            string[] id;
            string aplicaAprobacion = "N";
            string yaSeFacturo = "N";
            bool error = false;
            int poseeCriterioFacturacion = 0;
            foreach (string s in lst)
            {
                id = s.Split('|');
                //if (id[1] == "2") //Es en Proceso
                //{
                poseeCriterioFacturacion = dao.Documento.TieneCriterioFActuracionCliente(Int32.Parse(id[0]));
                if (id[2] != "")
                {
                    aplicaAprobacion = dao.Documento.RequiereAprobacion(Int32.Parse(id[2]));
                    yaSeFacturo = dao.Documento.YaSeFacturo(Int32.Parse(id[2]));
                    if ((yaSeFacturo == "N") && (aplicaAprobacion == "N"))//Si es N
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            //Se pasa a Finanzas
                            error = dao.Documento.TraspasarComercial(id[0], codemp, 4, user) <= 0;

                            if (!error)
                            {
                                scope.Complete();
                            }
                        }
                    }
                    else
                    {
                        if (aplicaAprobacion == "S")
                        {
                            //Se pasa a Panel de Aprobacion
                            using (TransactionScope scope = new TransactionScope())
                            {
                                //Se pasa a Panel de Aprobacion
                                error = dao.Documento.TraspasarComercial(id[0], codemp, 5, user) <= 0;

                                if (!error)
                                {
                                    scope.Complete();
                                }
                            }
                        }
                        if (yaSeFacturo == "S")
                        {
                            //Se pasa a Panel de Aprobacion
                            using (TransactionScope scope = new TransactionScope())
                            {
                                //Se pasa a Estatus No corresponde
                                error = dao.Documento.TraspasarComercial(id[0], codemp, 7, user) <= 0;

                                if (!error)
                                {
                                    scope.Complete();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (id[2] == "" && poseeCriterioFacturacion == 1)
                        salida = "Existe (n) Documento(s) seleccionado(s) sin criterio definido";
                    else
                    {
                        if (id[2] == "" && poseeCriterioFacturacion == 0)
                        {
                            //Se pasa a Estatus No Corresponde
                            using (TransactionScope scope = new TransactionScope())
                            {
                                //Se pasa a Estatus No Corresponde
                                error = dao.Documento.TraspasarComercial(id[0], codemp, 7, user) <= 0;

                                if (!error)
                                {
                                    scope.Complete();
                                }
                            }
                        }
                    }
                }
            }
            return salida;
        }

        public static List<dto.Documento> ListarCajaTraspasoComercialDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.Documento> lstDocumentos = dao.Documento.ListarCajaTraspasoComercialDocumentosGrilla(codemp, where, sidx, sord);

            bool SeAplicaCriterio = false;

            foreach (dto.Documento doc in lstDocumentos)
            {
                int TieneCriterio = -1;
                int criterioId = -1;
                TieneCriterio = dao.Documento.ValidaIngresoCriterioDocumento(doc.DocumentoId);
                if (TieneCriterio == 0)//No tiene Criterio Definido
                {
                    criterioId = dao.Documento.CriterioPorDefecto(codemp, Int32.Parse(doc.pclid), doc.DocumentoId);
                    if (criterioId > 0)
                    {//Tiene criterio que aplicar por defecto
                        List<dto.DocumentoCriterio> lst = dao.Documento.TraeCajaRecepcionDocumentosCriterio(doc.DocumentoId, criterioId);
                        foreach (dto.DocumentoCriterio p in lst)
                        {
                            if (p.IsEditable != "S")
                            {
                                SeAplicaCriterio = true;
                                bcp.Documento.GuardarCajaRecepcionDocumentosCriterio(doc.DocumentoId, criterioId, p.MontoFacturar.ToString("N2"), p.Observaciones, 1);
                            }
                        }
                    }
                }
            }

            if (SeAplicaCriterio)
                return dao.Documento.ListarCajaTraspasoComercialDocumentosGrilla(codemp, where, sidx, sord);
            else return lstDocumentos;
            
        }
        public static string ListarCajaCriterioFacturacion(int codemp, int pclid)
        {
            return dao.Documento.ListarCajaCriterioFacturacion(codemp, pclid);
        }
        public static List<Combobox> ListarCajaCriterioFacturacionCombo(int codemp, int pclid)
        {
            return dao.Documento.ListarCajaCriterioFacturacionCombo(codemp, pclid);
        }
        public static int SiAplicaCriterio(int documentoId, int criterioId)
        {
            return dao.Documento.SiAplicaCriterio(documentoId, criterioId);
        }
        public static List<dto.DocumentoCriterio> TraeCajaRecepcionDocumentosCriterio(int documentoId, int criterioId)
        {
            return dao.Documento.TraeCajaRecepcionDocumentosCriterio(documentoId, criterioId);
        }
        public static int GuardarCajaRecepcionDocumentosCriterio(int documentoId, int criterioId, string montoFacturar, string observaciones, int user)
        {
            return dao.Documento.GuardarCajaRecepcionDocumentosCriterio(documentoId, criterioId, montoFacturar, observaciones, user);
        }
        public static List<dto.Documento> ListarCajaTraspasoFinanzasDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.Documento.ListarCajaTraspasoFinanzasDocumentosGrilla(codemp, where, sidx, sord);
        }

        public static string TraspasoFacturacion(List<string> lst, int codemp, string numFact, string observaciones, int user)
        {
            string salida = "";
            string[] id;

            bool error = false;
            foreach (string s in lst)
            {
                id = s.Split('|');
                //if (id[1] == "2") //Es en Proceso
                //{
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //Se pasa a Facturar
                        error = dao.Documento.GuardarCajaRecepcionDocumentosFactura(id[0], numFact, observaciones, user) <= 0;
                        if (!error)
                        {
                            error = dao.Documento.TraspasarComercial(id[0], codemp, 8, user) <= 0;
                        }
                      
                        if (!error)
                        {
                            scope.Complete();
                        }
                    }
                //}

            }
            return salida;
        }

        public static int CriterioPorDefecto(int codemp, int pclid, int documentoId)
        {
            return dao.Documento.CriterioPorDefecto(codemp, pclid, documentoId);
        }

        public static List<Autocomplete> ListarRutNombreDeudor(string nombre)
        {
            return dao.Documento.ListarRutNombreDeudor(nombre);
        }
        public static int TieneCriterioFActuracionCliente(int documentoId)
        {
            return dao.Documento.TieneCriterioFActuracionCliente(documentoId);
        }
        public static List<dto.DocumentoExcelFinanza> ListarCajaTraspasoFinanzasDocumentosExcel(int codemp)
        {
            return dao.Documento.ListarCajaTraspasoFinanzasDocumentosExcel(codemp);
        }

        public static List<dto.DocumentoExcelControlGestion> ListarCajaTraspasoDocumentosExcel(int codemp, string where, string sidx, string sord)
        {
            return dao.Documento.ListarCajaTraspasoDocumentosExcel(codemp, where, sidx, sord);
        }
        public static List<dto.Documento> ListarCajaPanelAprobacionDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.Documento.ListarCajaPanelAprobacionDocumentosGrilla(codemp, where, sidx, sord);
        }
        public static string AprobacionTraspasoFinanzas(List<string> lst, int codemp, int user)
        {
            string salida = "";
            string[] id;
            bool error = false;
            
            foreach (string s in lst)
            {
                id = s.Split('|');

                using (TransactionScope scope = new TransactionScope())
                {
                    //Se pasa a Finanzas
                    error = dao.Documento.TraspasarComercial(id[0], codemp, 4, user) <= 0;

                    if (!error)
                    {
                        scope.Complete();
                    }
                }
               
            }
            return salida;
        }
        public static int obtieneEstatusDocumento(int documentoId)
        {
            return dao.Documento.obtieneEstatusDocumento(documentoId);
        } 
    }
}
