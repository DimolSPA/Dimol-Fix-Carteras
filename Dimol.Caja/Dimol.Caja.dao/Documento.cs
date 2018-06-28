using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Dimol.Caja.dao
{
    public class Documento
    {
        public static List<dto.Documento> ListarCajaIngresoDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.Documento> lst = new List<dto.Documento>();
            DateTime fechaIngreso = new DateTime();
            decimal valorIngreso = new decimal();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Caja_Ingreso_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecIngreso"].ToString(), out fechaIngreso);
                        decimal.TryParse(ds.Tables[0].Rows[i]["ValorIngreso"].ToString(), out valorIngreso);
                        lst.Add(new dto.Documento()
                        {
                            DocumentoId = Int32.Parse(ds.Tables[0].Rows[i]["DocumentoId"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            NumeroDocumento = ds.Tables[0].Rows[i]["NumeroDocumento"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDedor = ds.Tables[0].Rows[i]["RutDedor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutAsegurado = ds.Tables[0].Rows[i]["RutAsegurado"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            FecIngreso = fechaIngreso == new DateTime() ? (DateTime?)null : fechaIngreso,
                            MontoIngreso = decimal.Parse(ds.Tables[0].Rows[i]["MontoIngreso"].ToString()),
                            pclid = ds.Tables[0].Rows[i]["pclid"].ToString(),
                            ctcid = ds.Tables[0].Rows[i]["ctcid"].ToString(),
                            sbcid = ds.Tables[0].Rows[i]["sbcid"].ToString(),
                            ValorIngreso = valorIngreso == new decimal() ? (decimal) 0 : valorIngreso,
                            Codmon = ds.Tables[0].Rows[i]["codmon"].ToString(),
                            EstatusId = ds.Tables[0].Rows[i]["EstatusId"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.PanelAvenimiento.ListarCajaIngresoDocumentosGrilla", 0);
                return lst;
            }
        }

        public static int InsertUpdateDocumentoCaja(string documentoId, int codemp, string numeroDocumento, int pclid, int ctcid, string sbcid,
                                                    int codmon, string mtoIngreso, int estatus, int user)
        {
            int id = -1;

            try
            {
                DataSet ds = new DataSet();
                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Guardar_Caja_Ingreso_Documento");
                sp.AgregarParametro("documentoId", string.IsNullOrEmpty(documentoId) ? 0 : (object)Int32.Parse(documentoId));
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rec", numeroDocumento);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("sbcid", (object)sbcid ?? DBNull.Value);
                sp.AgregarParametro("codmon", codmon);
                sp.AgregarParametro("valorIngreso", string.IsNullOrEmpty(mtoIngreso) ? 0 : (object)Decimal.Parse(mtoIngreso));
                sp.AgregarParametro("estatus", estatus);
                sp.AgregarParametro("userId", user);
                
                
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["documentoId"].ToString());
                    }
               
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.InsertUpdateDocumentoCaja", user);
                
                return id;
            }
            return id;
        }

        public static List<dto.Documento> ListarCajaTraspasoDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.Documento> lst = new List<dto.Documento>();
            DateTime fechaIngreso = new DateTime();
            DateTime fechaStatusProceso = new DateTime();
            decimal valorIngreso = new decimal();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Caja_Traspaso_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecIngreso"].ToString(), out fechaIngreso);
                        decimal.TryParse(ds.Tables[0].Rows[i]["ValorIngreso"].ToString(), out valorIngreso);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecStatusProceso"].ToString(), out fechaStatusProceso);
                        lst.Add(new dto.Documento()
                        {
                            DocumentoId = Int32.Parse(ds.Tables[0].Rows[i]["DocumentoId"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            NumeroDocumento = ds.Tables[0].Rows[i]["NumeroDocumento"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDedor = ds.Tables[0].Rows[i]["RutDedor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutAsegurado = ds.Tables[0].Rows[i]["RutAsegurado"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            FecIngreso = fechaIngreso == new DateTime() ? (DateTime?)null : fechaIngreso,
                            MontoIngreso = decimal.Parse(ds.Tables[0].Rows[i]["MontoIngreso"].ToString()),
                            pclid = ds.Tables[0].Rows[i]["pclid"].ToString(),
                            ctcid = ds.Tables[0].Rows[i]["ctcid"].ToString(),
                            sbcid = ds.Tables[0].Rows[i]["sbcid"].ToString(),
                            Estatus = ds.Tables[0].Rows[i]["Estatus"].ToString(),
                            EstatusId = ds.Tables[0].Rows[i]["EstatusId"].ToString(),
                            ValorIngreso = valorIngreso == new decimal() ? (decimal) 0 : valorIngreso,
                            Codmon = ds.Tables[0].Rows[i]["codmon"].ToString(),
                            FecStatusProceso = fechaStatusProceso == new DateTime() ? (DateTime?)null : fechaStatusProceso,
                            StatusProceso = Int32.Parse(ds.Tables[0].Rows[i]["StatusProceso"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCajaTraspasoDocumentosGrilla", 0);
                return lst;
            }
        }

        public static int TraspasarComercial(string documentoId, int codemp, int estatus, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Traspasar_Comercial_Caja_Documento");
                sp.AgregarParametro("documentoId", string.IsNullOrEmpty(documentoId) ? 0 : (object)Int32.Parse(documentoId));
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estatus", estatus);
                sp.AgregarParametro("userId", user);

                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.TraspasarComercial", user);

                return id;
            }
            return id;
        }

        public static List<dto.Documento> ListarCajaTraspasoComercialDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.Documento> lst = new List<dto.Documento>();
            DateTime fechaIngreso = new DateTime();
            decimal montoFacturar = new decimal();
            decimal valorIngreso = new decimal();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Caja_Traspaso_Comercial_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecIngreso"].ToString(), out fechaIngreso);
                        decimal.TryParse(ds.Tables[0].Rows[i]["MontoFacturar"].ToString(), out montoFacturar);
                        decimal.TryParse(ds.Tables[0].Rows[i]["ValorIngreso"].ToString(), out valorIngreso);
                        lst.Add(new dto.Documento()
                        {
                            DocumentoId = Int32.Parse(ds.Tables[0].Rows[i]["DocumentoId"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            NumeroDocumento = ds.Tables[0].Rows[i]["NumeroDocumento"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDedor = ds.Tables[0].Rows[i]["RutDedor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutAsegurado = ds.Tables[0].Rows[i]["RutAsegurado"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            FecIngreso = fechaIngreso == new DateTime() ? (DateTime?)null : fechaIngreso,
                            MontoIngreso = decimal.Parse(ds.Tables[0].Rows[i]["MontoIngreso"].ToString()),
                            pclid = ds.Tables[0].Rows[i]["pclid"].ToString(),
                            ctcid = ds.Tables[0].Rows[i]["ctcid"].ToString(),
                            sbcid = ds.Tables[0].Rows[i]["sbcid"].ToString(),
                            EstatusId = ds.Tables[0].Rows[i]["EstatusId"].ToString(),
                            CriterioId = ds.Tables[0].Rows[i]["CriterioId"].ToString(),
                            Observaciones = ds.Tables[0].Rows[i]["OBSERVACIONES"].ToString(),
                            MontoFacturar = montoFacturar == new decimal() ? (decimal?)null : montoFacturar,
                            ValorIngreso = valorIngreso == new decimal() ? (decimal) 0 : valorIngreso,
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCajaTraspasoDocumentosGrilla", 0);
                return lst;
            }
        }

        public static string ListarCajaCriterioFacturacion(int codemp, int pclid)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Caja_Criterio_Facturacion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCajaCriterioFacturacion", 0);
                return "";
            }


        }
        public static List<Combobox> ListarCajaCriterioFacturacionCombo(int codemp, int pclid)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Caja_Criterio_Facturacion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][1].ToString(),
                            Value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCajaCriterioFacturacionCombo", 0);
            }
            return lst;
        }

        public static int SiAplicaCriterio(int documentoId, int criterioId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Si_Aplica_Criterio");
                sp.AgregarParametro("documentoId", documentoId);
                sp.AgregarParametro("criterioId", criterioId);
               

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["result"].ToString());
                    }


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.SiAplicaCriterio", 0);
                return -1;
            }
            return result;
        }
        public static List<dto.DocumentoCriterio> TraeCajaRecepcionDocumentosCriterio(int documentoId, int criterioId)
        {
            List<dto.DocumentoCriterio> lst = new List<dto.DocumentoCriterio>();
           
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Caja_Recepcion_Documentos_Criterio");
                sp.AgregarParametro("documentoId", documentoId);
                sp.AgregarParametro("criterioId", criterioId);
           
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DocumentoCriterio()
                        {
                            MontoFacturar = decimal.Parse(ds.Tables[0].Rows[i]["MontoFacturar"].ToString()),
                            Observaciones = ds.Tables[0].Rows[i]["Observaciones"].ToString(),
                            IsEditable = ds.Tables[0].Rows[i]["Editable"].ToString()

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.Documento.TraeCajaRecepcionDocumentosCriterio", 0);
                return lst;
            }
        }
        public static int GuardarCajaRecepcionDocumentosCriterio(int documentoId, int criterioId, string montoFacturar, string observaciones, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Guardar_Caja_Recepcion_Documentos_Criterio");
                sp.AgregarParametro("documentoId", documentoId);
                sp.AgregarParametro("criterioId", criterioId);
                sp.AgregarParametro("montoFacturar", Decimal.Parse(montoFacturar));
                sp.AgregarParametro("observaciones", observaciones);
                sp.AgregarParametro("userId", user);
                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.GuardarCajaRecepcionDocumentosCriterio", 0);

                return id;
            }
            return id;
        }
        public static string RequiereAprobacion(int criterioId)
        {
            string result = "N";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Indica_RequiereAprueba_Caja_Factura");
                sp.AgregarParametro("criterioId", criterioId);


                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = ds.Tables[0].Rows[0]["result"].ToString();
                    }


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.RequiereAprobacion", 0);
                return "N";
            }
            return result;
        }
        public static string YaSeFacturo(int criterioId)
        {
            string result = "N";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Indica_NoCorresponde_Caja_Factura");
                sp.AgregarParametro("criterioId", criterioId);


                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = ds.Tables[0].Rows[0]["result"].ToString();
                    }


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.YaSeFacturo", 0);
                return "N";
            }
            return result;
        }

        public static List<dto.Documento> ListarCajaTraspasoFinanzasDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.Documento> lst = new List<dto.Documento>();
            DateTime fechaIngreso = new DateTime();
            decimal montoFacturar = new decimal();
            decimal valorIngreso = new decimal();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Caja_Traspaso_Finanzas_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecIngreso"].ToString(), out fechaIngreso);
                        decimal.TryParse(ds.Tables[0].Rows[i]["MontoFacturar"].ToString(), out montoFacturar);
                        decimal.TryParse(ds.Tables[0].Rows[i]["ValorIngreso"].ToString(), out valorIngreso);
                        lst.Add(new dto.Documento()
                        {
                            DocumentoId = Int32.Parse(ds.Tables[0].Rows[i]["DocumentoId"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            NumeroDocumento = ds.Tables[0].Rows[i]["NumeroDocumento"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDedor = ds.Tables[0].Rows[i]["RutDedor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutAsegurado = ds.Tables[0].Rows[i]["RutAsegurado"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            FecIngreso = fechaIngreso == new DateTime() ? (DateTime?)null : fechaIngreso,
                            MontoIngreso = decimal.Parse(ds.Tables[0].Rows[i]["MontoIngreso"].ToString()),
                            pclid = ds.Tables[0].Rows[i]["pclid"].ToString(),
                            ctcid = ds.Tables[0].Rows[i]["ctcid"].ToString(),
                            sbcid = ds.Tables[0].Rows[i]["sbcid"].ToString(),
                            EstatusId = ds.Tables[0].Rows[i]["EstatusId"].ToString(),
                            CriterioId = ds.Tables[0].Rows[i]["CriterioId"].ToString(),
                            Observaciones = ds.Tables[0].Rows[i]["OBSERVACIONES"].ToString(),
                            MontoFacturar = montoFacturar == new decimal() ? (decimal?)null : montoFacturar,
                            ValorIngreso = valorIngreso == new decimal() ? (decimal) 0 : valorIngreso,
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCajaTraspasoFinanzasDocumentosGrilla", 0);
                return lst;
            }
        }

        public static int GuardarCajaRecepcionDocumentosFactura(string documentoId, string numFact, string observaciones, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Guardar_Caja_Recepcion_Documentos_Factura");
                sp.AgregarParametro("documentoId", string.IsNullOrEmpty(documentoId) ? 0 : (object)Int32.Parse(documentoId));
                sp.AgregarParametro("numFact", numFact);
                sp.AgregarParametro("observaciones", observaciones);
                sp.AgregarParametro("userId", user);
                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.GuardarCajaRecepcionDocumentosFactura", 0);

                return id;
            }
            return id;
        }

        public static int CriterioPorDefecto(int codemp, int pclid, int documentoId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Aplica_Criterio_Por_Defecto");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("documentoId", documentoId);
               
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                    }


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.CriterioPorDefecto", 0);
                return -1;
            }
            return result;
        }

        public static int ValidaIngresoCriterioDocumento(int documentoId)
        {
            int id = -1;

            try
            {
                DataSet ds = new DataSet();
                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Valida_Ingreso_Criterio_Documento");
                sp.AgregarParametro("documentoId", documentoId);
               


                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["CriterioId"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.ValidaIngresoCriterioDocumento", 0);

                return id;
            }
            return id;
        }

        public static int ValidaModificaMontoDocumento(int documentoId, string montoIngreso)
        {
            int id = -1;

            try
            {
                DataSet ds = new DataSet();
                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Valida_Monto_Ingreso_Documento");
                sp.AgregarParametro("documentoId", documentoId);
                sp.AgregarParametro("montoIngreso", string.IsNullOrEmpty(montoIngreso) ? 0 : (object)Decimal.Parse(montoIngreso));


                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["Actualizar"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.ValidaModificaMontoDocumento", 0);

                return id;
            }
            return id;
        }

        public static List<Autocomplete> ListarRutNombreDeudor(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Nombre_Deudor_Caja");
                sp.AgregarParametro("texto", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.ListarRutNombreDeudor", 0);
            }
            return lst;
        }

        public static int TieneCriterioFActuracionCliente(int documentoId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Tiene_CriterioFacturacion");
                sp.AgregarParametro("documentoId", documentoId);
             
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["CountCriterio"].ToString());
                    }


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.TieneCriterioFActuracionCliente", 0);
                return -1;
            }
            return result;
        }

        public static List<dto.DocumentoExcelFinanza> ListarCajaTraspasoFinanzasDocumentosExcel(int codemp)
        {
            List<dto.DocumentoExcelFinanza> lst = new List<dto.DocumentoExcelFinanza>();
            DateTime fechaIngreso = new DateTime();
            decimal montoFacturar = new decimal();
            decimal valorIngreso = new decimal();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Caja_Traspaso_Finanzas_Documentos_Excel");
                sp.AgregarParametro("codemp", codemp);
                

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecIngreso"].ToString(), out fechaIngreso);
                        decimal.TryParse(ds.Tables[0].Rows[i]["MontoFacturar"].ToString(), out montoFacturar);
                        decimal.TryParse(ds.Tables[0].Rows[i]["ValorIngreso"].ToString(), out valorIngreso);
                        lst.Add(new dto.DocumentoExcelFinanza()
                        {
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            NumeroDocumento = ds.Tables[0].Rows[i]["NumeroDocumento"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDedor = ds.Tables[0].Rows[i]["RutDedor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutAsegurado = ds.Tables[0].Rows[i]["RutAsegurado"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            ValorIngreso = valorIngreso == new decimal() ? (decimal) 0 : valorIngreso,
                            FecIngreso = fechaIngreso == new DateTime() ? (DateTime?)null : fechaIngreso,
                            //MontoIngreso = decimal.Parse(ds.Tables[0].Rows[i]["MontoIngreso"].ToString()),
                            Criterio = ds.Tables[0].Rows[i]["Criterio"].ToString(),
                            Observaciones = ds.Tables[0].Rows[i]["OBSERVACIONES"].ToString(),
                            MontoFacturar = montoFacturar == new decimal() ? (decimal?)null : montoFacturar
                            

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCajaTraspasoFinanzasDocumentosGrilla", 0);
                return lst;
            }
        }

        public static List<dto.DocumentoExcelControlGestion> ListarCajaTraspasoDocumentosExcel(int codemp, string where, string sidx, string sord)
        {
            List<dto.DocumentoExcelControlGestion> lst = new List<dto.DocumentoExcelControlGestion>();
            DateTime fechaDocumento = new DateTime();
            DateTime fechaVencimiento = new DateTime();
           
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Caja_Traspaso_Documentos_Excel");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecDoc"].ToString(), out fechaDocumento);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecVenc"].ToString(), out fechaVencimiento);
                        lst.Add(new dto.DocumentoExcelControlGestion()
                        {
                            RUTCLIENTE = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            RUTNUM = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            RUTDV = ds.Tables[0].Rows[i]["dvDeudor"].ToString(),
                            NOMBRE = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            APEPAT = ds.Tables[0].Rows[i]["APEPAT"].ToString(),
                            APEMAT = ds.Tables[0].Rows[i]["APEMAT"].ToString(),
                            COMUNA = ds.Tables[0].Rows[i]["COMUNA"].ToString(),
                            DIRECCION1 = ds.Tables[0].Rows[i]["DIRECCION1"].ToString(),
                            DIRECCION2 = ds.Tables[0].Rows[i]["DIRECCION2"].ToString(),
                            TELEFONO1 = ds.Tables[0].Rows[i]["TELEFONO1"].ToString(),
                            TELEFONO2 = ds.Tables[0].Rows[i]["TELEFONO2"].ToString(),
                            TELEFONO3 = ds.Tables[0].Rows[i]["TELEFONO3"].ToString(),
                            TELEFONO4 = ds.Tables[0].Rows[i]["TELEFONO4"].ToString(),
                            TELEFONO5 = ds.Tables[0].Rows[i]["TELEFONO5"].ToString(),
                            CELULAR1 = ds.Tables[0].Rows[i]["CELULAR1"].ToString(),
                            CELULAR2 = ds.Tables[0].Rows[i]["CELULAR2"].ToString(),
                            CELULAR3 = ds.Tables[0].Rows[i]["CELULAR3"].ToString(),
                            CELULAR4 = ds.Tables[0].Rows[i]["CELULAR4"].ToString(),
                            CELULAR5 = ds.Tables[0].Rows[i]["CELULAR5"].ToString(),
                            FAX = ds.Tables[0].Rows[i]["FAX"].ToString(),
                            MAIL1 = ds.Tables[0].Rows[i]["MAIL1"].ToString(),
                            MAIL2 = ds.Tables[0].Rows[i]["MAIL2"].ToString(),
                            MAIL3 = ds.Tables[0].Rows[i]["MAIL3"].ToString(),
                            TIPODOCUMENTO = ds.Tables[0].Rows[i]["TIPODOCUMENTO"].ToString(),
                            NUMERO = ds.Tables[0].Rows[i]["NUMERO"].ToString(),
                            FECDOC = fechaDocumento == new DateTime() ? (DateTime?)null : fechaDocumento,
                            FECVENC = fechaVencimiento == new DateTime() ? (DateTime?)null : fechaVencimiento,
                            MOTIVOCOBRANZA = ds.Tables[0].Rows[i]["MOTIVOCOBRANZA"].ToString(),
                            CODIGOCARGA = ds.Tables[0].Rows[i]["CODIGOCARGA"].ToString(),
                            MONEDA = ds.Tables[0].Rows[i]["MONEDA"].ToString(),
                            TIPOCAMBIO = Decimal.Parse(ds.Tables[0].Rows[i]["TIPOCAMBIO"].ToString()),
                            MONTOASIGNADO = Decimal.Parse(ds.Tables[0].Rows[i]["MONTOASIGNADO"].ToString()),
                            CAPITAL = Decimal.Parse(ds.Tables[0].Rows[i]["CAPITAL"].ToString()),
                            SALDO = Decimal.Parse(ds.Tables[0].Rows[i]["SALDO"].ToString()),
                            GASTOJUD = Decimal.Parse(ds.Tables[0].Rows[i]["GASTOJUD"].ToString()),
                            GASTOPRE = Decimal.Parse(ds.Tables[0].Rows[i]["GASTOPRE"].ToString()),
                            BANCO = ds.Tables[0].Rows[i]["BANCO"].ToString(),
                            RUTGIRADOR = ds.Tables[0].Rows[i]["RUTGIRADOR"].ToString(),
                            NOMBREGIRADOR = ds.Tables[0].Rows[i]["NOMBREGIRADOR"].ToString(),
                            NEGOCIO = ds.Tables[0].Rows[i]["NEGOCIO"].ToString(),
                            NUMEROAGRUPAR = ds.Tables[0].Rows[i]["NUMEROAGRUPAR"].ToString(),
                            RUTASEGURADO = ds.Tables[0].Rows[i]["RUTASEGURADO"].ToString(),
                            NOMBREASEGURADO = ds.Tables[0].Rows[i]["NOMBREASEGURADO"].ToString(),
                            DOCORI = ds.Tables[0].Rows[i]["DOCORI"].ToString(),
                            DOCANT = ds.Tables[0].Rows[i]["DOCANT"].ToString(),
                            COMENTARIO = ds.Tables[0].Rows[i]["COMENTARIO"].ToString(),
                            RUTTERCERO = ds.Tables[0].Rows[i]["RUTTERCERO"].ToString(),
                            NOMBRETERCERO = ds.Tables[0].Rows[i]["NOMBRETERCERO"].ToString(),
                            IDCUENTA = ds.Tables[0].Rows[i]["IDCUENTA"].ToString(),
                            DESC_CUENTA = ds.Tables[0].Rows[i]["DESC_CUENTA"].ToString()

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCajaTraspasoDocumentosExcel", 0);
                return lst;
            }
        }

        public static List<dto.Documento> ListarCajaPanelAprobacionDocumentosGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.Documento> lst = new List<dto.Documento>();
            DateTime fechaIngreso = new DateTime();
            decimal montoFacturar = new decimal();
            decimal valorIngreso = new decimal();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Caja_Panel_Aprobacion_Documentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecIngreso"].ToString(), out fechaIngreso);
                        decimal.TryParse(ds.Tables[0].Rows[i]["MontoFacturar"].ToString(), out montoFacturar);
                        decimal.TryParse(ds.Tables[0].Rows[i]["ValorIngreso"].ToString(), out valorIngreso);
                        lst.Add(new dto.Documento()
                        {
                            DocumentoId = Int32.Parse(ds.Tables[0].Rows[i]["DocumentoId"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            NumeroDocumento = ds.Tables[0].Rows[i]["NumeroDocumento"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDedor = ds.Tables[0].Rows[i]["RutDedor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutAsegurado = ds.Tables[0].Rows[i]["RutAsegurado"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            FecIngreso = fechaIngreso == new DateTime() ? (DateTime?)null : fechaIngreso,
                            MontoIngreso = decimal.Parse(ds.Tables[0].Rows[i]["MontoIngreso"].ToString()),
                            pclid = ds.Tables[0].Rows[i]["pclid"].ToString(),
                            ctcid = ds.Tables[0].Rows[i]["ctcid"].ToString(),
                            sbcid = ds.Tables[0].Rows[i]["sbcid"].ToString(),
                            EstatusId = ds.Tables[0].Rows[i]["EstatusId"].ToString(),
                            CriterioId = ds.Tables[0].Rows[i]["CriterioId"].ToString(),
                            Criterio = ds.Tables[0].Rows[i]["Criterio"].ToString(),
                            Observaciones = ds.Tables[0].Rows[i]["OBSERVACIONES"].ToString(),
                            MontoFacturar = montoFacturar == new decimal() ? (decimal?)null : montoFacturar,
                            ValorIngreso = valorIngreso == new decimal() ? (decimal)0 : valorIngreso,
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCajaPanelAprobacionDocumentosGrilla", 0);
                return lst;
            }
        }

        public static int obtieneEstatusDocumento(int documentoId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Obtener_Estatus_Documento");
                sp.AgregarParametro("documentoId", documentoId);
                

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["EstatusId"].ToString());
                    }


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Documento.obtieneEstatusDocumento", 0);
                return -1;
            }
            return result;
        }
    }
}
