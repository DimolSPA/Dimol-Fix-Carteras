using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Transactions;
using System.Configuration;
using System.Drawing;
using System.IO;
using Dimol.bcp;
using Dimol.dto;
using System.Data.OleDb;
namespace Dimol.Caja.bcp
{
    public class Tesoreria
    {
        public static List<dto.CuentaBancaria> ListarTesoreriaCuentasBancariasGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarTesoreriaCuentasBancariasGrilla(codemp, where, sidx, sord);
        }
        public static List<dto.CartolaMovimiento> ListarCartolaMovimientosGrilla(int codemp, string numCuenta, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarCartolaMovimientosGrilla(codemp,numCuenta, where, sidx, sord);
        }
        public static List<dto.CartolaMovimiento> ListarCartolaMovimientosPendienteGrilla(int codemp, string numCuenta, string fechaDocumento, string montoDocumento, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarCartolaMovimientosPendienteGrilla(codemp, numCuenta, fechaDocumento, montoDocumento, where, sidx, sord);
        }
        public static DataSet CargarExcel(string rutaArchivo, string path)
        {
            string[] archivo = rutaArchivo.Split('.');
           
            string conStr = "";
            if (archivo[archivo.Length - 1].ToLower() == "xls")
            {
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='"
                    + path + "\\" + rutaArchivo + "'"
                    + "; Extended Properties ='Excel 8.0;IMEX=1;HDR=No'";
            }
            else if (archivo[archivo.Length - 1].ToLower() == "xlsx")
            {
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source="
                    + path + "\\" + rutaArchivo
                    + "; Extended Properties ='Excel 8.0;IMEX=1;HDR=No'";
            }
            conStr = string.Format(conStr, rutaArchivo);
            OleDbConnection con = new OleDbConnection(conStr);


            try
            {
                con.Open();
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetname = dt.Rows[0]["TABLE_NAME"].ToString();
                OleDbDataAdapter adp = new OleDbDataAdapter("Select * from [" + sheetname + "]", con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }
        public static List<dto.DatosCargaCartola> CargarDatosCartola(string nombreArchivo, string NumCuenta, int user)
        {
            List<dto.DatosCargaCartola> lst = new List<dto.DatosCargaCartola>();
            try
            {
                DataSet ds = CargarExcel(nombreArchivo, ConfigurationManager.AppSettings["RutaArchivosCartolaBanco"]);
                if (ds.Tables.Count > 0)
                {
                    ds.AcceptChanges();
                    DataRow drn = ds.Tables[0].Rows[0];
                    for (int i = 0; i < drn.Table.Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = drn[i].ToString();
                    }
                    //Insertar todo el contenido del excel
                    int idcarga = 0;
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        idcarga = dao.Tesoreria.InsertarCartolaBancoCarga(nombreArchivo, user);
                    }
                    if (idcarga > 0)
                    {
                        for (int j = 1; j < ds.Tables[0].Rows.Count; j++)
                        {
                            DataRow dr = ds.Tables[0].Rows[j];


                            dao.Tesoreria.InsertarCartolaBancoArchivo(dr["Cuenta"].ToString(), dr["Fecha"].ToString(), dr["Monto"].ToString(), dr["Motivo"].ToString(),
                                                                            dr["Sucursal"].ToString(), string.Empty, idcarga, user);

                        }

                        lst = dao.Tesoreria.ListarCartolaBancoArchivo(idcarga);
                    }

                }
                //lst = dao.Tesoreria.ListarCartolaBancoArchivo(1);
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception(ex.Message);
            }
            return lst;
        }

        public static List<dto.DatosCargaCartola> ProcesoCargaCartolaBanco(List<dto.DatosCargaCartola> lst, string NumCuenta, int codemp, int user)
        {
            List<dto.DatosCargaCartola> lstError = new List<dto.DatosCargaCartola>();
            try
            {
                
                if (lst.Count > 0)
                {
                    foreach (dto.DatosCargaCartola archivo in lst)
                    {
                        //Valido que la cuenta del archivo sea igual a la cuenta en conciliacion
                        if (archivo.NumCuenta != NumCuenta)
                            lstError.Add(new dto.DatosCargaCartola()
                            {
                                NumCuenta = archivo.NumCuenta,
                                FecMovimiento = archivo.FecMovimiento,
                                Monto = archivo.Monto,
                                Motivo = archivo.Motivo,
                                Sucursal = archivo.Sucursal,
                                Mensaje ="El número de cuenta del archivo, no coincide con el conciliado"

                            });
                        else
                        {
                            int salida = 0;

                            //Tipo de Movimiento
                            int idMovimiento = 0;
                            List<Combobox> lstTipoMovimientoBanco = dao.Tesoreria.ListarTipoMovimientoBanco();
                            Combobox tipoMovimientoBanco = new Combobox();
                            //Evaluar si es Egreso o Ingreso, se evalua el monto
                            if (archivo.Monto < 0)
                                tipoMovimientoBanco = lstTipoMovimientoBanco.Find(x => x.Text == "Egreso");
                            else
                                tipoMovimientoBanco = lstTipoMovimientoBanco.Find(x => x.Text == "Ingreso");
                            idMovimiento = Int32.Parse(tipoMovimientoBanco.Value);

                            //Motivo Sistema
                            int idMotivo = 0;
                            List<Combobox> lstTipoMotivoBanco = dao.Tesoreria.ListarMotivoBanco();
                            Combobox tipoMotivoBanco = new Combobox();

                            List<string> subStrings = new List<string> { "TRANSFERENCIA DE DIMOL", "TRANSFER DE DIMOL", "TRANSFERENCIA A","TRANSFER A","TRANSF A",
                                                                    "TRANSFE DE", "TRANSFER DE", "TRANSFERENCIA DE","TRANSFER","TRANSFERENCIA","TRANS DE",
                                                                    "DEPOSITO CHEQUE","DEPOSITO CON CHEQUE", "DEPOSITO DE CHEQUE","DEP CHEQUE","DEP CHEQ","DEP.CHEQ",
                                                                    "DEPOSIT EN EFECTIVO","DEPOSITO EN EFECTIVO","DEPOSITO EFECTIVO",
                                                                    "DEPOSITO", "CARGO","ABONO","DEVOLUCION","SERVIPAG","PAGO","TRASPASO"};
                            switch (subStrings.FirstOrDefault(archivo.Motivo.Contains))
                            {
                                case "TRANSFERENCIA DE DIMOL":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencias Clientes");
                                    break;
                                case "TRANSFER DE DIMOL":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencias Clientes");
                                    break;
                                case "TRANSFERENCIA A":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencias Clientes");
                                    break;
                                case "TRANSFER A":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencias Clientes");
                                    break;
                                case "TRANSF A":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencias Clientes");
                                    break;
                                case "TRANSFE DE":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencia");
                                    break;
                                case "TRANSFER DE":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencia");
                                    break;
                                case "TRANSFERENCIA DE":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencia");
                                    break;
                                case "TRANSFER":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencia");
                                    break;
                                case "TRANSFERENCIA":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencia");
                                    break;
                                case "TRANS DE":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Transferencia");
                                    break;
                                case "DEPOSITO CHEQUE":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito cheque");
                                    break;
                                case "DEPOSITO CON CHEQUE":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito cheque");
                                    break;
                                case "DEPOSITO DE CHEQUE":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito cheque");
                                    break;
                                case "DEP CHEQUE":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito cheque");
                                    break;
                                case "DEP CHEQ":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito cheque");
                                    break;
                                case "DEP.CHEQ":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito cheque");
                                    break;
                                case "DEPOSITO":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito");
                                    break;
                                case "DEPOSIT EN EFECTIVO":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito efectivo");
                                    break;
                                case "DEPOSITO EN EFECTIVO":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito efectivo");
                                    break;
                                case "DEPOSITO EFECTIVO":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Deposito efectivo");
                                    break;
                                case "ABONO":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Abono");
                                    break;
                                case "CARGO":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Cargo por transferencia");
                                    break;
                                case "DEVOLUCION":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Devolución cheque protestado");
                                    break;
                                case "SERVIPAG":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Servipag");
                                    break;
                                case "PAGO":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Pago de créditos");
                                    break;
                                case "TRASPASO":
                                    tipoMotivoBanco = lstTipoMotivoBanco.Find(x => x.Text == "Traspaso");
                                    break;
                                default:
                                    tipoMotivoBanco = null;
                                    break;
                            }

                            idMotivo = tipoMotivoBanco == null ? 8 : Int32.Parse(tipoMotivoBanco.Value);

                            //Estado
                            int idEstado = 0;
                            List<Combobox> lstTipoEstadoBanco = dao.Tesoreria.ListarEstadoBanco();
                            Combobox tipoEstadoBanco = new Combobox();
                            if (idMotivo >= 1 && idMotivo <= 5)
                                tipoEstadoBanco = lstTipoEstadoBanco.Find(x => x.Text == "LIBERADO");
                            else
                            {
                                if (idMotivo >= 6 && idMotivo <= 8)
                                    tipoEstadoBanco = lstTipoEstadoBanco.Find(x => x.Text == "PENDIENTE");
                                else
                                {
                                    if (idMotivo >= 9 && idMotivo <= 11)
                                        tipoEstadoBanco = lstTipoEstadoBanco.Find(x => x.Text == "NO APLICA");
                                    else
                                        if (idMotivo >= 12)
                                            tipoEstadoBanco = lstTipoEstadoBanco.Find(x => x.Text == "PENDIENTE");
                                }
                            }
                            idEstado = Int32.Parse(tipoEstadoBanco.Value);


                            using (TransactionScope scope = new TransactionScope())
                            {

                                salida = dao.Tesoreria.InsertarCartolaMovimiento(codemp, NumCuenta, archivo.FecMovimiento.ToString(), archivo.Monto.ToString(),
                                                                                archivo.Sucursal, archivo.NumComprobante, idMovimiento, idMotivo, idEstado, archivo.Id, user);
                                if (salida > 0)
                                {
                                    dao.Tesoreria.ProcesarCartolaBancoRowId(archivo.Id);
                                    scope.Complete();

                                }
                            }

                        }
                    }//End foreach
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception(ex.Message);
            }
            return lstError;

        }

        public static List<Combobox> ListarMotivoBanco()
        {
            return dao.Tesoreria.ListarMotivoBanco();
        }
        public static List<Combobox> ListarEstadoBanco()
        {
            return dao.Tesoreria.ListarEstadoBanco();
        }
        public static List<Combobox> ListarTipoConciliacionMovimiento()
        {
            return dao.Tesoreria.ListarTipoConciliacionMovimiento();
        }
        public static List<Autocomplete> ListarNombreGestor(string nombre)
        {
            return dao.Tesoreria.ListarNombreGestor(nombre);
        }
        public static int ActualizarEstadoMovimientoCartola(int codemp, int movimientoId, int cuentaId, int tipoEstadoId, int user)
        {
            return dao.Tesoreria.ActualizarEstadoMovimientoCartola(codemp, movimientoId, cuentaId, tipoEstadoId, user);
        }
        public static int ActualizarObservacionMovimientoCartola(int codemp, int movimientoId, int cuentaId, int tipoEstadoId, string observacion, int user)
        {
            return dao.Tesoreria.ActualizarObservacionMovimientoCartola(codemp, movimientoId, cuentaId, tipoEstadoId, observacion, user);
        }
        public static int ExistConciliacioncomprobante(string numComprobante)
        {
            return dao.Tesoreria.ExistConciliacioncomprobante(numComprobante);
        }
        public static int InsertarConciliacionMovimiento(int codemp, int movimientoId, string numComprobante, string custodiaId,
                                                        int pclid, int ctcid, int gestorId, int tipoConciliacion, string pathArchivo, string numCuenta, int user)
        {
            int result = 0;

            result = dao.Tesoreria.InsertarConciliacionMovimiento(codemp, movimientoId, numComprobante, custodiaId, pclid, ctcid, gestorId, tipoConciliacion, numCuenta, user);

            if (result > 0)
                result = dao.Tesoreria.InsertarConciliacionMovimientoArchivo(result, movimientoId, pclid, ctcid, pathArchivo, user);

            return result;
        }

        public static string TraspasoPanelProtestado(List<string> lst, int cuentaId, int codemp, int user)
        {
            string salida = "";
            
            bool error = false;
            foreach (string movimientoId in lst)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //Se pasa a En proceso
                    error = dao.Tesoreria.ActualizarEstadoMovimientoCartola(codemp, Int32.Parse(movimientoId), cuentaId, 3, user) <= 0;

                    if (!error)
                    {
                        scope.Complete();
                    }
                }

            }
            return salida;
        }
        public static List<dto.CartolaMovimientoExcel> ListarCartolaMovimientosExcel(int codemp, string numCuenta)
        {
            return dao.Tesoreria.ListarCartolaMovimientosExcel(codemp, numCuenta);
        }
        public static List<Combobox> ListarGestorConciliacion(int codemp, int pclid, int ctcid)
        {
            return dao.Tesoreria.ListarGestorConciliacion(codemp, pclid, ctcid);
        }
        public static List<Combobox> ListarTipoCuentaTesoreria()
        {
            return dao.Tesoreria.ListarTipoCuentaTesoreria();
        }
        public static int ExistCuentaBancaria(string numCuenta)
        {
            return dao.Tesoreria.ExistCuentaBancaria(numCuenta);
        }
        public static int InsertarCuentaBancaria(int codemp, string numCuenta, int bancoId, int tipoCuentaId, int user)
        {
            int result = 0;

            result = dao.Tesoreria.InsertarCuentaBancaria(codemp, numCuenta, bancoId, tipoCuentaId,  user);

            return result;
        }
        public static List<Combobox> ListarBancos(int codemp)
        {
            return dao.Tesoreria.ListarBancos(codemp);
        }

        public static List<dto.DocumentoCustodia> ListarDocumentosCustodiaGrilla(int codemp, string numCuenta, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarDocumentosCustodiaGrilla(codemp, numCuenta, where, sidx, sord);
        }

        public static int InsertarDocumentoCustodia(int codemp, string NumCuenta, int pclid, int ctcid, int gestorId, string recibe, string bancoId,
                                                    List <dto.DocumentoCustodiaGrid> docs, int userId)
        {
            int result = 0;

            foreach (dto.DocumentoCustodiaGrid doc in docs)
            {
                if (!string.IsNullOrEmpty(doc.NumDoc) && !string.IsNullOrEmpty(doc.MontoDoc) && !string.IsNullOrEmpty(doc.FechaDoc))
                {
                    dao.Tesoreria.InsertarDocumentoCustodia(codemp, NumCuenta, pclid, ctcid, gestorId, recibe, bancoId,
                                                    doc.NumDoc, doc.FechaDoc, doc.MontoDoc, userId);
                }
            }
            result = 1;

            
            return result;
        }

        public static int InsertarConciliacionCustodia(int codemp, int movimientoId, string numComprobante, string custodiaId,
                                                       int pclid, int ctcid, int gestorId, int tipoConciliacion, string numCuenta, int user)
        {
            int result = 0;

            result = dao.Tesoreria.InsertarConciliacionMovimiento(codemp, movimientoId, numComprobante, custodiaId, pclid, ctcid, gestorId, tipoConciliacion, numCuenta, user);

           return result;
        }
        public static int ActualizarEstadoDocumentoCustodia(int codemp, int custodiaId, int tipoEstadoId, int user)
        {
            return dao.Tesoreria.ActualizarEstadoDocumentoCustodia(codemp, custodiaId, tipoEstadoId, user);
        }
        public static List<dto.CartolaMovimiento> ListarMovimientosProtestadosGrilla(int codemp, string numCuenta, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarMovimientosProtestadosGrilla(codemp, numCuenta, where, sidx, sord);
        }
        public static List<dto.MovimientoConciliado> ListarMovimientosConciliadoGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarMovimientosConciliadoGrilla(codemp, where, sidx, sord);
        }
        public static dto.FormLiquidacion ListarFormLiquidacion(int codemp, string conciliacionId, string pclid, string ctcid)
        {
            return dao.Tesoreria.ListarFormLiquidacion(codemp, conciliacionId, pclid, ctcid);
        }
        public static List<dto.DocumentoDeudor> ListarLiquidacionDocumentosDeudorGrilla(int codemp, int pclid, int ctcid, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarLiquidacionDocumentosDeudorGrilla(codemp, pclid, ctcid, where, sidx, sord);
        }
        public static List<dto.DocumentoImputado> ListarImputacionDocumentosDeudorGrilla(int codemp, int pclid, int ctcid, string docs, string docsFinalizar, string montoHonorario, string montoInteres, string montoCapital,
                                                                                         string montoGastoPre, string montoGastoJud)
        {
            return dao.Tesoreria.ListarImputacionDocumentosDeudorGrilla(codemp, pclid, ctcid, docs, docsFinalizar, montoHonorario, montoInteres, montoCapital, montoGastoPre, montoGastoJud);
        }

        public static int InsertarDocumentoImputado(int codemp, int conciliacionId, int pclid, int ctcid, 
                                                   List<dto.DocumentoPorImputar> docs,
                                                   List<dto.DocumentoPorFinalizar> dofinalizar, int userId)
        {
            int result = 0;

            foreach (dto.DocumentoPorImputar doc in docs)
            {
                if (doc.IndicaImputado == "1")
                {
                    bool error = false;
                     
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //Se pasa a En proceso
                        error = dao.Tesoreria.InsertarDocumentoImputado(codemp, conciliacionId, doc.Ccbid, doc.Estado, doc.CapitalDebitado, doc.InteresDebitado, doc.HonorarioDebitado,
                                                    doc.PagoJudDebitado, doc.PagoPreDebitado, userId) <= 0;

                        if (!error)
                        {
                            //Rebajar documento.
                            error = dao.Tesoreria.RebajarDocumentoImputado(codemp, pclid, ctcid, doc.Ccbid, doc.CapitalDebitado, doc.InteresDebitado, doc.HonorarioDebitado,
                                                     doc.PagoJudDebitado, doc.PagoPreDebitado, userId) <= 0;
                            //string numdocumento = dao.Tesoreria.GrabarAccion(codemp, pclid, ctcid, doc.Ccbid);
                            //int resultAccion = Carteras.bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(codemp, pclid, ctcid, 7, 1, 0, "N", "10.0.1.15", "10.0.1.219", userId, "Documento rebajado: " + doc.Numero + "por: Capital: " + decimal.Parse(doc.CapitalDebitado).ToString("N") + " Intereses: " + decimal.Parse(doc.InteresDebitado).ToString("N") + " Honorarios: " + decimal.Parse(doc.HonorarioDebitado).ToString("N") + " Gastos Judiciales y PreJudiciales: " + decimal.Parse(doc.PagoJudDebitado).ToString("N") + ", " + decimal.Parse(doc.PagoPreDebitado).ToString("N"), 0, Int64.Parse("0"));
                        }
                        
                        if (!error)
                        {
                            scope.Complete();
                        }
                    }
                }
            }
            //Aprobar la conciliacion
            dao.Tesoreria.AprobarConciliacionMovimiento(codemp, conciliacionId);
            //Finalizar documentos
            foreach (dto.DocumentoPorFinalizar doc in dofinalizar)
            {
                if (doc.Saldo == "0")
                {
                    bool error = false;

                    error = dao.Tesoreria.FinalizarDocumento(codemp, pclid, ctcid, Int32.Parse(doc.Ccbid), doc.Estado, "Se cancela Deuda") <= 0;

                    if (!error)
                    {
                        result = 1;
                    }

                }
            }
           
            result = 1;


            return result;
        }
        public static int FinalizarDocumentos(int codemp, int pclid, int ctcid,
                                                  List<dto.DocumentoPorFinalizar> dofinalizar, int userId)
        {
            int result = 0;

            foreach (dto.DocumentoPorFinalizar doc in dofinalizar)
            {
                if (doc.Saldo == "1")
                {
                    bool error = false;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        error = dao.Tesoreria.FinalizarDocumento(codemp, pclid, ctcid, Int32.Parse(doc.Ccbid), doc.Estado,"Se cancela Deuda") <= 0;

                        if (!error)
                        {
                            //Grabo la accion
                            //string numdocumento = dao.Tesoreria.GrabarAccion(codemp, pclid, ctcid, doc.Ccbid);
                            int resultAccion = Carteras.bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(codemp, pclid, ctcid, 7, 1, 0, "N", "10.0.1.15", "10.0.1.219", userId, "Se cancela Deuda id documento " + doc.Numero, 0, Int64.Parse("0"));
                            
                        }
                        if (!error)
                            scope.Complete();
                    }
                }
            }
           
            result = 1;
            return result;
        }

        public static int InsertarPagoManual(int codemp, int pclid, int ctcid, string fecha, string monto, int tipoConciliacion, int user)
        {
            int result = 0;

            result = dao.Tesoreria.InsertarPagoManual(codemp, pclid, ctcid, fecha, monto, tipoConciliacion, user);

            return result;
        }

        public static List<dto.MovimientoConciliadoAprobado> ListarMovimientosConciliadoAprobadoGrilla(int codemp, string pclid, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarMovimientosConciliadoAprobadoGrilla(codemp, pclid, where, sidx, sord);
        }

        public static List<dto.ComprobanteRemesa> ListarRemesaGananciaGrilla(string docs)
        {
            return dao.Tesoreria.ListarRemesaGananciaGrilla(docs);
        }

        public static List<dto.Remesa> ListarRemesasGeneradasGrilla(string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarRemesasGeneradasGrilla(where, sidx, sord);
        }

        public static int InsertarRemesa(int codemp, List<dto.ComprobanteRemesa> docs, int userId)
        {
            int result = 0;
            bool error = false;
            List<string> Detallelist = new List<string>();
            List<int> DoctoCajalist = new List<int>();
            using (TransactionScope scope = new TransactionScope())
            {
                //Se ingresa remesa
                result = dao.Tesoreria.InsertarRemesaCabecera(userId);
                error = result <= 0;

                if (!error)
                {
                    //Ingresar Detalle.
                    foreach (dto.ComprobanteRemesa doc in docs)
                    {
                        if (doc.DocumentoId > 0){
                            Detallelist.Add(doc.Pclid + "|" + doc.Ctcid + "|" + doc.Anticipo + "|" + doc.DocumentoId  + "|" + doc.AnticipoDebitado);
                            DoctoCajalist.Add(doc.DocumentoId);
                        }
                          
                        error = dao.Tesoreria.InsertarRemesaDetalle(result, doc.ImputacionId, doc.ConciliacionId, codemp, doc.Ccbid, doc.Pclid,
                                                doc.Ctcid, doc.NumComprobante, doc.Capital.ToString(), doc.Interes.ToString(), doc.Honorario.ToString(),
                                                doc.PorCapital, doc.PorInteres, doc.PorHonorario, doc.GananciaCapital.ToString(), doc.GananciaInteres.ToString(),
                                                doc.GananciaHonorario.ToString(),userId) <= 0;
                    }
                }
                //Ingresar detalle anticipo
                if (!error)
                {
                    if (Detallelist.Count > 0){
                        foreach (string anticipo in Detallelist){
                            error = dao.Tesoreria.InsertarRemesaAnticipo(result, codemp, anticipo.Split('|')[0], anticipo.Split('|')[1], anticipo.Split('|')[2],
                                                                            anticipo.Split('|')[4], anticipo.Split('|')[3], userId) <= 0;
                        }
                             
                    }
                       
                }
                if (!error)
                {
                    DoctoCajalist = DoctoCajalist.Distinct().ToList();//obtemos los id de los documentos
                    if (DoctoCajalist.Count > 0){
                        foreach (int documento in DoctoCajalist) {
                            error = dao.Tesoreria.ActualizarSaldoDocumentoCaja(result, documento.ToString(), userId) <= 0;
                        }
                    }
                }
                if (!error)
                {
                    result = 1;
                    scope.Complete();
                }
                else
                {
                    result = 0;
                }
            }
           
           return result;
        }

        public static List<dto.EfectivoCustodia> ListarEfectivoCustodiaGrilla(int codemp, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarEfectivoCustodiaGrilla(codemp, where, sidx, sord);
        }
        public static int InsertarEfectivoCustodia(int codemp, int pclid, int ctcid, int gestorId, string recibe, string bancoId,
                                                   string FechaDoc, string MontoDoc, int userId)
        {
            int result = 0;

            dao.Tesoreria.InsertarEfectivoCustodia(codemp, pclid, ctcid, gestorId, recibe, bancoId,
                                                   FechaDoc, MontoDoc, userId);
            result = 1;


            return result;
        }
        public static dto.FormLiquidacion ListarFormLiquidacionEfectivo(int codemp, string conciliacionId, string pclid, string ctcid)
        {
            return dao.Tesoreria.ListarFormLiquidacionEfectivo(codemp, conciliacionId, pclid, ctcid);
        }
        public static int InsertarDocumentoImputadoSinMovimiento(int codemp, int conciliacionId, int pclid, int ctcid,
                                                  List<dto.DocumentoPorImputar> docs,
                                                  List<dto.DocumentoPorFinalizar> dofinalizar, int userId)
        {
            int result = 0;
            bool error = false;
            foreach (dto.DocumentoPorImputar doc in docs)
            {
                if (doc.IndicaImputado == "1")
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //Se pasa a En proceso
                        error = dao.Tesoreria.InsertarDocumentoImputado(codemp, conciliacionId, doc.Ccbid, doc.Estado, doc.CapitalDebitado, doc.InteresDebitado, doc.HonorarioDebitado,
                                                    doc.PagoJudDebitado, doc.PagoPreDebitado, userId) <= 0;

                        if (!error)
                        {
                            //Rebajar documento.
                            error = dao.Tesoreria.RebajarDocumentoImputado(codemp, pclid, ctcid, doc.Ccbid, doc.CapitalDebitado, doc.InteresDebitado, doc.HonorarioDebitado,
                                                     doc.PagoJudDebitado, doc.PagoPreDebitado, userId) <= 0;
                            //string numdocumento = dao.Tesoreria.GrabarAccion(codemp, pclid, ctcid, doc.Ccbid);
                            //int resultAccion = Carteras.bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(codemp, pclid, ctcid, 7, 1, 0, "N", "10.0.1.15", "10.0.1.219", userId, "Documento rebajado: " + doc.Numero + "por: Capital: " + decimal.Parse(doc.CapitalDebitado).ToString("N") + " Intereses: " + decimal.Parse(doc.InteresDebitado).ToString("N") + " Honorarios: " + decimal.Parse(doc.HonorarioDebitado).ToString("N") + " Gastos Judiciales y PreJudiciales: " + decimal.Parse(doc.PagoJudDebitado).ToString("N") + ", " + decimal.Parse(doc.PagoPreDebitado).ToString("N"), 0, Int64.Parse("0"));
                        }

                        if (!error)
                        {
                            scope.Complete();
                        }
                    }
                }
            }
            if (!error)
            {
                //Aprobar la conciliacion
                dao.Tesoreria.AprobarConciliacionSinMovimiento(codemp, conciliacionId);
            }
            
            //Finalizar documentos
            foreach (dto.DocumentoPorFinalizar doc in dofinalizar)
            {
                if (doc.Saldo == "0")
                {
                    error = false;

                    error = dao.Tesoreria.FinalizarDocumento(codemp, pclid, ctcid, Int32.Parse(doc.Ccbid), doc.Estado, "Se cancela Deuda") <= 0;

                    if (!error)
                    {
                        result = 1;
                    }

                }
            }

            result = 1;


            return result;
        }

        public static List<dto.DocumentoCustodiaProtestado> ListarDocumentoCustodiaProtestadosGrilla(int codemp, string numCuenta, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarDocumentoCustodiaProtestadosGrilla(codemp, numCuenta, where, sidx, sord);
        }
        public static List<dto.CartolaMovimiento> ListarCartolaMovimientosLiberadosGrilla(int codemp, string numCuenta, string fechaDocumento, string montoDocumento, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarCartolaMovimientosLiberadosGrilla(codemp, numCuenta, fechaDocumento, montoDocumento, where, sidx, sord);
        }
        public static int ActualizarMovimientoConciliacionCustodia(int codemp, int custodiaId, int movimientoId, int conciliacionId,int user)
        {
            return dao.Tesoreria.ActualizarMovimientoConciliacionCustodia(codemp, custodiaId, movimientoId, conciliacionId, user);
        }
        public static List<Combobox> ListarCuentas(int codemp)
        {
            return dao.Tesoreria.ListarCuentas(codemp);
        }
        public static List<dto.MovimientoConciliado> ListarMovimientosConciliadoAprobadosGrilla(int codemp, DateTime? fechaConciliacion, string pclid, string ctcid, string numcomprobante, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ListarMovimientosConciliadoAprobadosGrilla(codemp, fechaConciliacion, pclid, ctcid, numcomprobante, where, sidx, sord);
        }

        public static int ObtConciliacionNumComprobante()
        {
            return dao.Tesoreria.ObtConciliacionNumComprobante();
        }

        public static int ReversarImputacion(int codemp, int conciliacionId, int user)
        {
            int result = 0;
            bool error = false;
            using (TransactionScope scope = new TransactionScope())
            {
                //Se reversa la cartera de documento
                error = dao.Tesoreria.ReversarImputacionDocumento(codemp, conciliacionId, user) <= 0;

                if (!error)
                {
                    //Cambiar estado de la conciliacion
                    error = dao.Tesoreria.ReversarImputacionDocumentoEstado(codemp, conciliacionId, user) <= 0;
                   
                }
                if (!error)
                {
                    //Eliminar imputacion
                    error = dao.Tesoreria.ReversarConciliacionDocumentoImputado(codemp, conciliacionId, user) <= 0;

                }
                if (!error)
                {
                    result = 1;
                    scope.Complete();
                }
            }

            return result;
        }

        public static List<Combobox> ListarTipoCondicionFacturacion()
        {
            return dao.Tesoreria.ListarTipoCondicionFacturacion();
        }
        public static List<Combobox> ListarTipoCondicionFacturacionRemesa()
        {
            return dao.Tesoreria.ListarTipoCondicionFacturacionRemesa();
        }
        public static List<Combobox> ListarCajaTipoCambioTodos()
        {
            return dao.Tesoreria.ListarCajaTipoCambioTodos();
        }
        public static List<Combobox> ListarCajaTipoCambioRemesa()
        {
            return dao.Tesoreria.ListarCajaTipoCambioRemesa();
        }
        public static List<Combobox> ListarTipoRegionRemesa()
        {
            return dao.Tesoreria.ListarTipoRegionRemesa();
        }
        public static List<dto.Pago> ConsultaDePagos(int codemp, DateTime? fechaCancelacion, string pclid, string ctcid, string numcomprobante, string where, string sidx, string sord)
        {
            return dao.Tesoreria.ConsultaDePagos(codemp, fechaCancelacion, pclid, ctcid, numcomprobante,  where, sidx, sord);
        }
    }
}
