using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Contabilidad.Mantenedores.dto;
using System.Data;
using Dimol.dao;
using System.Data.SqlClient;
using System.Diagnostics;


namespace Dimol.Contabilidad.Mantenedores.dao
{
    public class ClasificacionDocumentos
    {
        public List<dto.ClasificacionDocumentos> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ClasificacionDocumentos> lstPeriodos = new List<dto.ClasificacionDocumentos>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ClasificacionDocumentos_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idid", idid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                //Debug.WriteLine("PARAMETROS SP " + codemp + " " + where + " " + sidx + " " + sord + " " + inicio + " " + limite);
                ds = sp.EjecutarProcedimiento();
                //

                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.ClasificacionDocumentos()
                        {

                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["Id"].ToString()),
                            Codigo = ds.Tables[1].Rows[i]["Codigo"].ToString(),
                            Tipo = ds.Tables[1].Rows[i]["Tipo"].ToString(),
                            TipoComprobante = ds.Tables[1].Rows[i]["TipoComprobante"].ToString(),
                            TipoProducto = ds.Tables[1].Rows[i]["TipoProducto"].ToString(),
                            CostosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["CostosSN"].ToString()),
                            SeleccionOtroComprobanteSN = convertirTrueFalse(ds.Tables[1].Rows[i]["SeleccionOtroComprobanteSN"].ToString()),
                            CarteraClientesSN = convertirTrueFalse(ds.Tables[1].Rows[i]["CarteraClientesSN"].ToString()),
                            ContableSN = convertirTrueFalse(ds.Tables[1].Rows[i]["ContableSN"].ToString()),
                            SeleccionaPagosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["SeleccionaPagosSN"].ToString()),
                            AplicaPagosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["AplicaPagosSN"].ToString()),
                            Concepto = devulveConceptoCompleto(ds.Tables[1].Rows[i]["Concepto"].ToString()),
                            FinalizaDeudaSN = convertirTrueFalse(ds.Tables[1].Rows[i]["FinalizaDeudaSN"].ToString()),
                            CancelaSN = convertirTrueFalse(ds.Tables[1].Rows[i]["CancelaSN"].ToString()),
                            TipoLibro = ds.Tables[1].Rows[i]["TipoLibro"].ToString(),
                            CambiaDocumentoSN = convertirTrueFalse(ds.Tables[1].Rows[i]["CambiaDocumentoSN"].ToString()),
                            RemesaSN = convertirTrueFalse(ds.Tables[1].Rows[i]["RemesaSN"].ToString()),
                            TipoDocSeleccionar = ds.Tables[1].Rows[i]["TipoDocSeleccionar"].ToString(),
                            AnulaImpuestoSN = convertirTrueFalse(ds.Tables[1].Rows[i]["AnulaImpuestoSN"].ToString()),
                            FormaPagoSN = convertirTrueFalse(ds.Tables[1].Rows[i]["FormaPagoSN"].ToString()),
                            OrdenCompraSN = convertirTrueFalse(ds.Tables[1].Rows[i]["OrdenCompraSN"].ToString()),
                            Movimiento = ds.Tables[1].Rows[i]["Movimiento"].ToString(),
                            MostrarEnLibrosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["MostrarEnLibrosSN"].ToString()),
                            HonorariosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["HonorariosSN"].ToString()),
                            Cuenta = ds.Tables[1].Rows[i]["Cuenta"].ToString(),
                            Stock = Int16.Parse(validaNULL(ds.Tables[1].Rows[i]["Stock"].ToString())),
                            SaldosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["SaldosSN"].ToString()),
                            ReservaSN = convertirTrueFalse(ds.Tables[1].Rows[i]["ReservaSN"].ToString()),
                            TransitoSN = convertirTrueFalse(ds.Tables[1].Rows[i]["SaldosSN"].ToString())

                        });
                    }
                }
                Debug.WriteLine("lstPeriodos" + lstPeriodos);
                return lstPeriodos;
            }
            catch (Exception ex)
            {
                return lstPeriodos;
            }

        }

        public bool convertirTrueFalse(string val)
        {
            bool returnval = true;
            if (val.Equals("S") && val != null)
            {
                returnval = true;
            }
            else
            {
                returnval = false;
            }
            return returnval;
        }

        public string convertirTrueFalse(bool val)
        {
            string returnval = "S";
            if (val == true)
            {
                returnval = "S";
            }
            else
            {
                returnval = "N";
            }
            return returnval;
        }

        public string validaNULL(string val)
        {

            if (val != null && val != "")
            {
                return val;
            }
            else
            {
                return "0";
            }
        }

        public List<dto.ClasificacionDocumentos> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ClasificacionDocumentos> lstPeriodos = new List<dto.ClasificacionDocumentos>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ClasificacionDocumentos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstPeriodos.Add(new dto.ClasificacionDocumentos()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["Id"].ToString()),
                            Codigo = ds.Tables[1].Rows[i]["Codigo"].ToString(),
                            Tipo = ds.Tables[1].Rows[i]["Tipo"].ToString(),
                            TipoComprobante = ds.Tables[1].Rows[i]["TipoComprobante"].ToString(),
                            TipoProducto = ds.Tables[1].Rows[i]["TipoProducto"].ToString(),
                            CostosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["CostosSN"].ToString()),
                            SeleccionOtroComprobanteSN = convertirTrueFalse(ds.Tables[1].Rows[i]["SeleccionOtroComprobanteSN"].ToString()),
                            CarteraClientesSN = convertirTrueFalse(ds.Tables[1].Rows[i]["CarteraClientesSN"].ToString()),
                            ContableSN = convertirTrueFalse(ds.Tables[1].Rows[i]["ContableSN"].ToString()),
                            SeleccionaPagosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["SeleccionaPagosSN"].ToString()),
                            AplicaPagosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["AplicaPagosSN"].ToString()),
                            Concepto = ds.Tables[1].Rows[i]["Concepto"].ToString(),
                            FinalizaDeudaSN = convertirTrueFalse(ds.Tables[1].Rows[i]["FinalizaDeudaSN"].ToString()),
                            CancelaSN = convertirTrueFalse(ds.Tables[1].Rows[i]["CancelaSN"].ToString()),
                            TipoLibro = ds.Tables[1].Rows[i]["TipoLibro"].ToString(),
                            CambiaDocumentoSN = convertirTrueFalse(ds.Tables[1].Rows[i]["CambiaDocumentoSN"].ToString()),
                            RemesaSN = convertirTrueFalse(ds.Tables[1].Rows[i]["RemesaSN"].ToString()),
                            TipoDocSeleccionar = ds.Tables[1].Rows[i]["TipoDocSeleccionar"].ToString(),
                            AnulaImpuestoSN = convertirTrueFalse(ds.Tables[1].Rows[i]["AnulaImpuestoSN"].ToString()),
                            FormaPagoSN = convertirTrueFalse(ds.Tables[1].Rows[i]["FormaPagoSN"].ToString()),
                            OrdenCompraSN = convertirTrueFalse(ds.Tables[1].Rows[i]["OrdenCompraSN"].ToString()),
                            Movimiento = ds.Tables[1].Rows[i]["Movimiento"].ToString(),
                            MostrarEnLibrosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["MostrarEnLibrosSN"].ToString()),
                            HonorariosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["HonorariosSN"].ToString()),
                            Cuenta = ds.Tables[1].Rows[i]["Cuenta"].ToString(),
                            Stock = Int16.Parse(ds.Tables[1].Rows[i]["Stock"].ToString()),
                            SaldosSN = convertirTrueFalse(ds.Tables[1].Rows[i]["SaldosSN"].ToString()),
                            ReservaSN = convertirTrueFalse(ds.Tables[1].Rows[i]["ReservaSN"].ToString()),
                            TransitoSN = convertirTrueFalse(ds.Tables[1].Rows[i]["SaldosSN"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPeriodos;
        }


        public void Insertar(dto.ClasificacionDocumentos objAccion, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Clasificacion_CpbtDoc");

                sp.AgregarParametro("clb_codemp", codemp);
                sp.AgregarParametro("clb_codigo", objAccion.Codigo);
                string tipoCpbt = "";
                if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 1)
                {
                    tipoCpbt = "C";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 2) 
                {
                    tipoCpbt = "V";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 3)
                {
                    tipoCpbt = "T";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 4)
                {
                    tipoCpbt = "A";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 5)
                {
                    tipoCpbt = "D";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 6)
                {
                    tipoCpbt = "X";
                }
                sp.AgregarParametro("clb_tipcpbtdoc", tipoCpbt);
                sp.AgregarParametro("clb_tipprod", objAccion.TipoProducto);
                sp.AgregarParametro("clb_costos", convertirTrueFalse(objAccion.CostosSN));
                sp.AgregarParametro("clb_selcpbt", convertirTrueFalse(objAccion.SeleccionOtroComprobanteSN));
                sp.AgregarParametro("clb_cartcli", convertirTrueFalse(objAccion.CarteraClientesSN));
                sp.AgregarParametro("clb_contable", convertirTrueFalse(objAccion.ContableSN));
                sp.AgregarParametro("clb_selapl", convertirTrueFalse(objAccion.SeleccionaPagosSN));
                sp.AgregarParametro("clb_aplica", convertirTrueFalse(objAccion.AplicaPagosSN));
                string concepto = "";
                if (Int16.Parse(objAccion.Concepto.ToString()) == 2)
                {
                    concepto = "I";
                }
                else if (Int16.Parse(objAccion.Concepto.ToString()) == 3)
                {
                    concepto = "E";
                }
                else if (Int16.Parse(objAccion.Concepto.ToString()) == 4)
                {
                    concepto = "T";
                }
                sp.AgregarParametro("clb_cptoctbl", concepto);
                sp.AgregarParametro("clb_findeuda", convertirTrueFalse(objAccion.FinalizaDeudaSN));
                sp.AgregarParametro("clb_cancela", convertirTrueFalse(objAccion.CancelaSN));
                sp.AgregarParametro("clb_libcompra", 0);
                sp.AgregarParametro("clb_cambiodoc", convertirTrueFalse(objAccion.CambiaDocumentoSN));
                sp.AgregarParametro("clb_remesa", convertirTrueFalse(objAccion.RemesaSN));
                sp.AgregarParametro("clb_tipsel", 2);
                sp.AgregarParametro("clb_sinimp", convertirTrueFalse(objAccion.AnulaImpuestoSN));
                sp.AgregarParametro("clb_forpag", convertirTrueFalse(objAccion.FormaPagoSN));
                sp.AgregarParametro("clb_ordcomp", convertirTrueFalse(objAccion.OrdenCompraSN));

                sp.AgregarParametro("cct_debhab", objAccion.Movimiento);
                sp.AgregarParametro("cct_libcomven", convertirTrueFalse(objAccion.MostrarEnLibrosSN));
                sp.AgregarParametro("cct_honorarios", convertirTrueFalse(objAccion.HonorariosSN));
                sp.AgregarParametro("cct_pctid", objAccion.Cuenta);
                sp.AgregarParametro("cct_pctid2", objAccion.Cuenta);

                sp.AgregarParametro("ccs_stock", objAccion.Stock);
                sp.AgregarParametro("ccs_saldos", convertirTrueFalse(objAccion.SaldosSN));
                sp.AgregarParametro("ccs_reserva", convertirTrueFalse(objAccion.ReservaSN));
                sp.AgregarParametro("ccs_transito", convertirTrueFalse(objAccion.TransitoSN));

                Debug.WriteLine("DATOS A INGRESAR " + codemp + objAccion.Codigo + tipoCpbt + objAccion.TipoProducto + objAccion.CostosSN + objAccion.SeleccionOtroComprobanteSN
                    + objAccion.CarteraClientesSN + objAccion.ContableSN + objAccion.SeleccionaPagosSN + objAccion.AplicaPagosSN
                    + concepto + objAccion.FinalizaDeudaSN + objAccion.CancelaSN + "TIPO LIBRO :" + objAccion.TipoLibro
                    + objAccion.CambiaDocumentoSN + objAccion.RemesaSN + objAccion.TipoDocSeleccionar + objAccion.AnulaImpuestoSN + objAccion.FormaPagoSN
                    + objAccion.OrdenCompraSN + "MOVIMIENTO " + objAccion.Movimiento + objAccion.MostrarEnLibrosSN + "CUENTA " + objAccion.Cuenta
                    + "STOCK " +objAccion.Stock);

                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Borrar(int codemp, int? id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Clasificacion_CpbtDoc");
                sp.AgregarParametro("clb_codemp", codemp);
                sp.AgregarParametro("clb_clbid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string devulveConceptoCompleto(string val)
        {
            string salida = "";
            if (val.Equals("I"))
            {
                salida = "Ingreso";
            }
            else if (val.Equals("E"))
            {
                salida = "Egreso";
            }
            else if (val.Equals("T"))
            {
                salida = "Traspaso";
            }
            return salida;

        }


        public void Editar(dto.ClasificacionDocumentos objAccion, int codemp, int id)
        {
            try
            {
                 Debug.WriteLine("DATOS A INGRESAR " + "codemp:" +codemp + " id:" + id +  " codigo:" + objAccion.Codigo + " tipocomprobante:"+ objAccion.TipoComprobante + " tipoproducto:" + objAccion.TipoProducto + 
                     " costos:" + objAccion.CostosSN + " selcomprobante:" + objAccion.SeleccionOtroComprobanteSN
                    + " carteracliente:" + objAccion.CarteraClientesSN + " contable:" + objAccion.ContableSN + " seleccionPagos:" + objAccion.SeleccionaPagosSN 
                    + " aplicapagos:" + objAccion.AplicaPagosSN
                    + " concepto:" + objAccion.Concepto + " finalizadeuda:" + objAccion.FinalizaDeudaSN + " cancela:" + objAccion.CancelaSN + " TIPO LIBRO:" + objAccion.TipoLibro
                    + " cambiadoc:" + objAccion.CambiaDocumentoSN + " remesa:" + objAccion.RemesaSN + " tipodocsel:" + objAccion.TipoDocSeleccionar +
                    " anulaimp:" + objAccion.AnulaImpuestoSN + " formapago:" + objAccion.FormaPagoSN
                    + " ordencompra:" + objAccion.OrdenCompraSN);
                StoredProcedure sp = new StoredProcedure("Update_Clasificacion_CpbtDoc");
                sp.AgregarParametro("clb_codemp", codemp);
                sp.AgregarParametro("clb_clbid", id);
                sp.AgregarParametro("clb_codigo", objAccion.Codigo);
                string tipoCpbt = "";
                if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 1)
                {
                    tipoCpbt = "C";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 2)
                {
                    tipoCpbt = "V";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 3)
                {
                    tipoCpbt = "T";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 4)
                {
                    tipoCpbt = "A";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 5)
                {
                    tipoCpbt = "D";
                }
                else if (Int16.Parse(objAccion.TipoComprobante.ToString()) == 6)
                {
                    tipoCpbt = "X";
                }
                sp.AgregarParametro("clb_tipcpbtdoc", tipoCpbt);
                sp.AgregarParametro("clb_tipprod", objAccion.TipoProducto);
                sp.AgregarParametro("clb_costos", convertirTrueFalse(objAccion.CostosSN));
                sp.AgregarParametro("clb_selcpbt", convertirTrueFalse(objAccion.SeleccionOtroComprobanteSN));
                sp.AgregarParametro("clb_cartcli", convertirTrueFalse(objAccion.CarteraClientesSN));
                sp.AgregarParametro("clb_contable", convertirTrueFalse(objAccion.ContableSN));
                sp.AgregarParametro("clb_selapl", convertirTrueFalse(objAccion.SeleccionaPagosSN));
                sp.AgregarParametro("clb_aplica", convertirTrueFalse(objAccion.AplicaPagosSN));
                string concepto = "";
                if (Int16.Parse(objAccion.Concepto.ToString()) == 2)
                {
                    concepto = "I";
                }
                else if (Int16.Parse(objAccion.Concepto.ToString()) == 3)
                {
                    concepto = "E";
                }
                else if (Int16.Parse(objAccion.Concepto.ToString()) == 4)
                {
                    concepto = "T";
                }
                sp.AgregarParametro("clb_cptoctbl", concepto);
                sp.AgregarParametro("clb_findeuda", convertirTrueFalse(objAccion.FinalizaDeudaSN));
                sp.AgregarParametro("clb_cancela", convertirTrueFalse(objAccion.CancelaSN));
                sp.AgregarParametro("clb_libcompra", 0);
                sp.AgregarParametro("clb_cambiodoc", convertirTrueFalse(objAccion.CambiaDocumentoSN));
                sp.AgregarParametro("clb_remesa", convertirTrueFalse(objAccion.RemesaSN));
                sp.AgregarParametro("clb_tipsel", 2);
                sp.AgregarParametro("clb_sinimp", convertirTrueFalse(objAccion.AnulaImpuestoSN));
                sp.AgregarParametro("clb_forpag", convertirTrueFalse(objAccion.FormaPagoSN));
                sp.AgregarParametro("clb_ordcomp", convertirTrueFalse(objAccion.OrdenCompraSN));

                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ListarClasificacionDocumentosCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ClasificacionDocumentos_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idid", idid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                //sp.AgregarParametro("inicio", inicio);
                //sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("NRO DATOS COUNT" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {

                    return Int32.Parse(ds.Tables[1].Rows[0]["count"].ToString());

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0; ;
            }
        }


        public string ListarTipoComprobante(int codemp, int idid)
        {
            string salida = "";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 6; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");

                    sp.AgregarParametro("codigo", "TipCpbt" + i);
                    sp.AgregarParametro("idioma", idid);
                    ds = sp.EjecutarProcedimiento();
                    Debug.WriteLine("TAMAÑO REPORTE PADRE " + ds.Tables.Count + "-" + idid);
                    if (i == 1)
                    {
                        salida += i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }

                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string ListarTipoProducto(int codemp, int idid)
        {
            string salida = "";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");

                    sp.AgregarParametro("codigo", "TipProd" + i);
                    sp.AgregarParametro("idioma", idid);
                    ds = sp.EjecutarProcedimiento();
                    Debug.WriteLine("TAMAÑO REPORTE PADRE " + ds.Tables.Count + "-" + idid);
                    if (i == 1)
                    {
                        salida += i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }

                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string ListarConcepto(int codemp, int idid)
        {
            string salida = ":" + "Seleccione";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 2; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");

                    sp.AgregarParametro("codigo", "TipAsi" + i);
                    sp.AgregarParametro("idioma", idid);
                    ds = sp.EjecutarProcedimiento();
                    Debug.WriteLine("TAMAÑO REPORTE PADRE " + ds.Tables.Count + "-" + idid);
                    if (i == 1)
                    {
                        salida += i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }

                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string ListarMovimiento(int codemp, int idid)
        {
            string salida = ":" + "Seleccione";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "Debe");
                    sp.AgregarParametro("idioma", idid);
                    ds = sp.EjecutarProcedimiento();
                    salida += ";" + "1" + ":" + ds.Tables[0].Rows[0][0].ToString();

                    StoredProcedure sp2 = new StoredProcedure("Trae_Etiquetas");
                    sp2.AgregarParametro("codigo", "Haber");
                    sp2.AgregarParametro("idioma", idid);
                    ds = sp2.EjecutarProcedimiento();
                    salida += ";" + "-1" + ":" + ds.Tables[0].Rows[0][0].ToString();
                
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string ListarCuentas(int codemp)
        {
            string salida = ":" + "Seleccione";
            try
            {

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Lista_Cuentas");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS" + ds.Tables.Count);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StoredProcedure sp2 = new StoredProcedure("_Trae_Nombre_Cuentas");

                    sp2.AgregarParametro("codemp", codemp);
                    sp2.AgregarParametro("id", ds.Tables[0].Rows[i][0].ToString());
                    ds2 = sp2.EjecutarProcedimiento();

                    salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds2.Tables[0].Rows[0][0].ToString();

                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }

        }

        public string ListarStock()
        {
            string salida = ":" + "Seleccione";

            try
            {
                for (int i = 1; i < 100; i++)
                {
                    salida += ";" + "i" + ":" + i.ToString();
                }

                


                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

    }
}


