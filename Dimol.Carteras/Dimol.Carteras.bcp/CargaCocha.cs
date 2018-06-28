using Dimol.bcp;
using Dimol.Carteras.dto;
using Dimol.Carteras.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
{
    public class CargaCocha
    {
        #region "Cargar Datos" 
        public static List<dto.CargaCocha> CargarDatosCocha(string nombreArchivo)
        {
            List<dto.CargaCocha> lst = new List<dto.CargaCocha>();
            try
            {
                DataSet ds = Funciones.CargarExcel(nombreArchivo, System.Configuration.ConfigurationManager.AppSettings["RutaArchivos"] + "\\Download\\");
                if (ds.Tables.Count > 0)
                {
                    DataColumnCollection columns = ds.Tables[0].Columns;
                    ds.AcceptChanges();
                    DataRow drn = ds.Tables[0].Rows[0];
                    for (int i = 0; i < drn.Table.Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = drn[i].ToString();
                    }

                    for (int j = 1; j < ds.Tables[0].Rows.Count; j++)
                    {
                        DataRow dr = ds.Tables[0].Rows[j];
                        lst.Add(new dto.CargaCocha
                        {
                            OrigenCobranza = dr["Origen_Cobranza"].ToString(),
                            SubTipoDeuda1 = dr["SubTipoDeuda1"].ToString(),
                            SubTipoDeuda2 = dr["SubTipoDeuda2"].ToString(),
                            IdDeuda = Int32.Parse(dr["Id_Deuda"].ToString()),
                            IdDeudaX = dr["Id_Deuda_X"].ToString(),
                            IdDeudaAux = Int32.Parse(dr["Id_Deuda_aux"].ToString()),
                            AntiguedadDeuda = dr["Antiguedad_Deuda"].ToString(),
                            Cobrador = dr["Cobrador"].ToString(),
                            CodCCO = Int32.Parse(dr["CodCCo"].ToString()),
                            CodVen = dr["CodVen"].ToString(),
                            Comentario = dr["Comentario"].ToString(),
                            CtdPaxNeg = Int32.Parse(dr["CtdPaxNeg"].ToString()),
                            Cuit = Int32.Parse(dr["Cuit"].ToString()),
                            DiasAntiguedad = Int32.Parse(dr["Dias_Antiguedad"].ToString()),
                            DV = dr["DV"].ToString(),
                            EjecCtasComer = dr["Ejec_Ctas_Comer"].ToString(),
                            Empresa = dr["Empresa"].ToString(),
                            FecUltLlamada = DateTime.Parse(dr["Fec_Ult_Llamada"].ToString()),
                            FechaDoc = DateTime.Parse(dr["Fecha_doc"].ToString()),
                            FecEmi = DateTime.Parse(dr["FecEmi"].ToString()),
                            FecHoy = DateTime.Parse(dr["FecHoy"].ToString()),
                            FecUltSolRetPago = DateTime.Parse(dr["FecUltSolRetPago"].ToString()),
                            FecVen = DateTime.Parse(dr["FecVen"].ToString()),
                            Frec = dr["Frec"].ToString(),
                            GlosaFactura = dr["Glosa_Factura"].ToString(),
                            Holding = dr["Holding"].ToString(),
                            Inacti = Int32.Parse(dr["Inacti"].ToString()),
                            IndDescFee = dr["Ind_Desc_Fee"].ToString(),
                            IndTarjetaTesorero = dr["Ind_Tarjeta_Tesorero"].ToString(),
                            Merc = dr["Merc"].ToString(),
                            Moneda = dr["Moneda"].ToString(),
                            Negocio = dr["Negocio"].ToString(),
                            Nemote = dr["Nemote"].ToString(),
                            NomAnalista = dr["NomAnalista"].ToString(),
                            NombreDeudor = dr["Nombre_Deudor"].ToString(),
                            NombrePax1 = dr["Nombre_Pax1"].ToString(),
                            NomCCO = dr["NomCCo"].ToString(),
                            NomVen = dr["NomVen"].ToString(),
                            NumFact = dr["NumFac"].ToString(),
                            NumRut = dr["NumRut"].ToString().Length > 8 ? Int32.Parse(dr["NumRut"].ToString().Substring(1,8)) : Int32.Parse(dr["NumRut"].ToString()), // remuevo el prefijo en los ruts q usa cocha
                            OpTelCob = Int32.Parse(dr["OpTelCob"].ToString()),
                            PlazoPago = Int32.Parse(dr["PlazoPago"].ToString()),
                            PromPago = decimal.Parse(dr["PromPago"].ToString()),
                            RazonSocial = dr["RazonSoc"].ToString(),
                            SaldoCLP = decimal.Parse(dr["Saldo_CLP"].ToString()),
                            SaldoUSD = decimal.Parse(dr["Saldo_USD"].ToString()),
                            StatusDev = dr["Status_Dev"].ToString(),
                            TasaCambio = decimal.Parse(dr["TasaCambio"].ToString()),
                            TipCli = dr["TipCli"].ToString(),
                            TipoCobranza = dr["Tipo_Cobranza"].ToString(),
                            TipoFactura = dr["Tipo_Factura"].ToString(),
                            UltLlamada = DateTime.Parse(dr["UltLlamada"].ToString()),
                            UsuMensaje = dr["usu_mensaje"].ToString(),
                            NacInt = dr["Nac_int"].ToString(),
                            Proveedor = dr["Proveedor"].ToString(),
                            TipoMov = dr["Tipo_mov"].ToString(),
                            Inbduc = dr["Indus"].ToString(),
                            Limcre = long.Parse(dr["Limcre"].ToString()),
                            LimCreUSD = Int32.Parse(dr["Limcreusd"].ToString()),
                            CodFacturador = Int32.Parse(dr["CodFacturador"].ToString())
                        });
                    }

                    //ds.Tables[0].Rows[0].Delete();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception(ex.Message);
            }
            return lst;
        }

        #endregion

        #region "Proceso Carga Cocha"

        public static bool ProcesoCarga(List<dto.CargaCocha> lst, dto.CargaMasiva objCarga, UserSession objSession)
        {
            bool error = false;
            int ctcid = 0;
            int idTipoDocumento = 0;
            ErrorCarga objError = new ErrorCarga();
            Funciones objFunc = new Funciones();
            bool[] salida = { false, false };
            List<Combobox> lstTipoDocumento = dao.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma);
            Combobox tipoDocumento = new Combobox();
            string arrCtcid = "", arrFacturas = "";
            int[] arrCod = {2,7,10 };
            int idComuna = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa,4);//Direccion.BuscarComuna(datos.Comuna);
            List<dto.CargaCocha> lstDocsXRut = new List<dto.CargaCocha>();
            dto.Comprobante objCPBT = new dto.Comprobante();

            //Recorremos los registros del archivo
            if (lst.Count > 0)
            {
                //var deudores = lst.Select(o => new { o.NumRut, o.DV, o.NombreDeudor}).Distinct().ToList();

                foreach (var datos in lst)
                {
                    //Grabando los deudores
                    if (datos.Ctcid == 0)
                    {
                        if (Funciones.ValidaRut(datos.NumRut + datos.DV))
                        {
                            ctcid = dao.Deudor.BuscarIdDeudor(datos.NumRut + datos.DV, objSession.CodigoEmpresa);
                            if (ctcid <= 0)
                            {
                                //salida = bcp.CargaCocha.CrearDeudoresCarga(datos.NombreDeudor,datos.NumRut, datos.DV, objCarga, objSession, idComuna);
                            }
                            lstDocsXRut = lst.FindAll(x => x.NumRut == datos.NumRut);
                            lstDocsXRut.Select(c => { c.Ctcid = ctcid; return c; }).ToList();
                        }
                        else
                        {
                            lstDocsXRut = lst.FindAll(x => x.NumRut == datos.NumRut);
                            lstDocsXRut.Select(c => { c.Error = true; return c; }).ToList();
                            objCarga.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "RUT NO VALIDO",
                                Rut = datos.NumRut + "-" + datos.DV,
                                Dv = datos.DV,
                                Nombre = datos.NombreDeudor,
                                TipoDocumento = "Factura",// datos.TipoDocumento,
                                Numero = ""
                            });
                            error = true;
                        }
                    }
                    arrCtcid = arrCtcid + ctcid + ",";
                    arrFacturas = arrFacturas + "'" + datos.NumFact.ToString() + "',";

                }
                if (!salida[0] && !error)
                {
                    tipoDocumento = lstTipoDocumento.Find(x => x.Value == "1");
                    if (tipoDocumento == null)
                    {
                        idTipoDocumento = -1;
                    }
                    else
                    {
                        idTipoDocumento = Int32.Parse(tipoDocumento.Value);
                    }
                    List<dto.CargaCocha> lstPermitidos = lst.Where(s => arrCod.Contains(s.OpTelCob)).ToList<dto.CargaCocha>();
                    List<Combobox> lstCodigoCarga = dao.Comprobante.ListarCodigoCarga(objSession.CodigoEmpresa, objCarga.Pclid, "");
                    List<dto.Comprobante> lstFinalizado = dao.CargaCocha.ListarDocumentosCocha(objSession.CodigoEmpresa,objCarga.Pclid,arrCtcid,arrFacturas, Int32.Parse(objCarga.TipoCartera), "F");
                    List<dto.Comprobante> lstVigente = dao.CargaCocha.ListarDocumentosCocha(objSession.CodigoEmpresa, objCarga.Pclid, "", "", Int32.Parse(objCarga.TipoCartera),"V" );

                    foreach (dto.CargaCocha datos in lst)
                    {

                        datos.CodigoCarga = Int32.Parse(lstCodigoCarga.FirstOrDefault(x => x.Text.Contains(datos.OpTelCob + " -")).Value);
                        if (dao.Comprobante.ExisteCpbtNumeroTipo(objSession.CodigoEmpresa, objCarga.Pclid, datos.Ctcid, idTipoDocumento, datos.NumFact)==0)
                        {
                            //Comienzo a Grabar los Documentos
                            if (!salida[0] && !salida[1] && !datos.Error)
                            {
                                objCPBT = dao.Comprobante.TraeCpbtNumeroTipo(objSession.CodigoEmpresa, objCarga.Pclid, datos.Ctcid, datos.NumFact, idTipoDocumento);
                                if (objCPBT.Ccbid <= 0)
                                {
                                    //salida = GrabarDocumentos(datos, objCarga, objSession, idTipoDocumento);
                                }


                            }
                            if (salida[0])
                            {
                                objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "ERROR AL GRABAR DOCUMENTO",
                                    Rut = datos.NumRut + "-" + datos.DV,
                                    Dv = datos.DV,
                                    Nombre = datos.NombreDeudor,
                                    TipoDocumento = "Factura",// datos.TipoDocumento,
                                    Numero = datos.NumFact.ToString()
                                });
                                error = true;
                            }
                        }
                    }
                    //Update de documentos
                    foreach (dto.Comprobante datos in lstVigente)
                    {
                        int gesid = 0;

                        Dimol.dao.Utilidades util = new Dimol.dao.Utilidades(1, 1, 1, "", "");

                        gesid = util.TraeGestorCartera(objSession.CodigoEmpresa, objSession.CodigoSucursal, datos.Ctcid);

                        dto.Comprobante dcto = new dto.Comprobante();
                        List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);
                        if (datos.Moneda == "CLP")
                        {
                            dcto.CodigoMoneda = Int32.Parse(lstMoneda.Find(x => x.Text == "PESOS").Value);
                            dcto.TipoCambio = 1;
                            dcto.MontoAsignado = datos.SaldoCLP;
                            dcto.Monto = datos.SaldoCLP;
                            dcto.Saldo = datos.SaldoCLP;
                        }
                        else if (datos.Moneda == "USD")
                        {
                            dcto.CodigoMoneda = Int32.Parse(lstMoneda.Find(x => x.Text == "DOLAR").Value);
                            dcto.TipoCambio = 1;// datos.TasaCambio; se mantiernre la tasa de cambio en 1 pq se usa la primera del mes.
                            dcto.MontoAsignado = datos.SaldoUSD;
                            dcto.Monto = datos.SaldoUSD;
                            dcto.Saldo = datos.SaldoUSD;
                        }


                    }

                    


                }
                if (!salida[0] && !error)
                {
                    error = false;
                }
                else
                {
                    error = true;
                }

            }
            return error;
        }

        public static bool[] CrearDeudoresCarga(string nombreDeudor,int numeroRut, string DV, dto.CargaMasiva objCarga, UserSession objSession, int idComuna)
        {
            bool error = false;
            bool repetido = false;
            Funciones objFunc = new Funciones();
            List<Contacto> lstContactos = new List<Contacto>();
            int ctcid = 0;

            if (!error)
            {
                string[] arrNom = nombreDeudor.Split(' ');
                ctcid = dao.Deudor.GuardarDeudor(objSession.CodigoEmpresa, 0, nombreDeudor, arrNom.Length > 1 ? arrNom[1] : "", arrNom.Length > 2 ? arrNom[2] : "", numeroRut + DV, nombreDeudor, idComuna, "P", "", "0", "N", "N", 1, false);
                if (ctcid <= 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL CREAR DEUDOR",
                        Rut = numeroRut + "-" + DV,
                        Dv = DV,
                        Nombre = nombreDeudor,
                        TipoDocumento = "Factura",// datos.TipoDocumento,
                        Numero = ""
                    });
                    error = true;
                }
            }

            bool[] salida = { error, repetido };
            return salida;
        }

        public static bool[] GrabarDocumentos(dto.CargaCocha datos, dto.CargaMasiva objCarga, UserSession objSession, int idTipoDocumento)
        {
            //Comienzo a Grabar los Documentos-------------------
            dto.Comprobante dcto = new dto.Comprobante();
            int existe = 0;
            int resultado = 0;
            bool[] error = { false, false };
            int estid = 0;
            Funciones objFunc = new Funciones();
            //Busco si esta asignado a la cartera
            existe = dao.Comprobante.BuscarCarteraCliente(objSession.CodigoEmpresa, objCarga.Pclid, datos.Ctcid);
            if (existe == 0)
            {
                if (dao.Comprobante.InsertarCarteraCliente(objSession.CodigoEmpresa, objCarga.Pclid, datos.Ctcid) < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL INSERTAR CARTERA CLIENTE",
                        Rut = datos.NumRut + "-" + datos.DV,
                        Dv = datos.DV,
                        Nombre = datos.NombreDeudor,
                        TipoDocumento = "Factura",// datos.TipoDocumento,
                        Numero = datos.NumFact.ToString()
                    });
                    error[0] = true;
                }
            }
            //Grabo el Documento
            if (!error[0])
            {
                dcto.Pclid = objCarga.Pclid;
                dcto.Ctcid = datos.Ctcid;
                dcto.Ccbid = 0;
                dcto.TipoDocumento = idTipoDocumento.ToString();
                dcto.TipoCartera = Int32.Parse(objCarga.TipoCartera);
                dcto.NumeroCpbt = datos.NumFact.ToString();
                dcto.FechaVencimiento = datos.FecVen;
                dcto.FechaDocumento = datos.FecEmi;
                if (objCarga.TipoCartera == "1")
                {
                    if (datos.FecVen > DateTime.Parse(objFunc.FechaServer()))
                    {
                        estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 100);
                    }
                    else
                    {
                        estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 99);
                    }
                }
                else
                {
                    estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 17);
                }
                dcto.EstadoCartera = estid.ToString();
                dcto.EstadoCpbt = "V";
                List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);
                if (datos.Moneda == "CLP")
                {
                    dcto.CodigoMoneda = Int32.Parse(lstMoneda.Find(x => x.Text == "PESOS").Value);
                    dcto.TipoCambio = 1;
                    dcto.MontoAsignado = datos.SaldoCLP;
                    dcto.Monto = datos.SaldoCLP;
                    dcto.Saldo = datos.SaldoCLP;
                }
                else if (datos.Moneda == "USD")
                {
                    dcto.CodigoMoneda = Int32.Parse(lstMoneda.Find(x => x.Text == "DOLAR").Value);
                    dcto.TipoCambio = 1;// datos.TasaCambio; se mantiernre la tasa de cambio en 1 pq se usa la primera del mes.
                    dcto.MontoAsignado = datos.SaldoUSD;
                    dcto.Monto = datos.SaldoUSD;
                    dcto.Saldo = datos.SaldoUSD;
                }

                //dcto.CodigoMoneda = Int32.Parse(lstMoneda.Find(x => x.Text == datos.Moneda.ToUpper()).Value);
                //dcto.TipoCambio = datos.TipoCambio;
                //dcto.MontoAsignado = datos.MontoAsignado;
                //dcto.Monto = datos.Capital;
                //dcto.Saldo = datos.Saldo;
                dcto.GastoJudicial = 0;
                dcto.GastoOtros = 0;

                dcto.NombreBanco = null;
                dcto.RutGirador = null;
                dcto.NombreGirador = null;

                dcto.MotivoCobranza = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa,28).ToString();
                if (string.IsNullOrEmpty(datos.Comentario))
                {
                    dcto.Comentario = "";
                }
                else
                {
                    dcto.Comentario = datos.Comentario;
                }
                
                if (!string.IsNullOrEmpty(objCarga.CodigoCarga))
                {
                    dcto.CodigoCarga = objCarga.CodigoCarga;
                }
                else
                {
                    dcto.CodigoCarga = datos.CodigoCarga.ToString();
                }
                if (datos.CodigoCarga == 0)
                {
                    int codigoCarga = dao.Comprobante.TraeCodigoCarga(objSession.CodigoEmpresa, objCarga.Pclid, datos.OpTelCob.ToString());
                    if (codigoCarga > 0)
                    {
                        dcto.CodigoCarga = codigoCarga.ToString();
                    }
                    else
                    {
                        if (objCarga.Pclid == 424)
                        {
                            dcto.CodigoCarga = objCarga.CodigoCarga;
                        }
                        else
                        {
                            dcto.CodigoCarga = null;
                        }
                    }
                }
                else
                {
                    dcto.CodigoCarga = null;
                }
                if (!string.IsNullOrEmpty(datos.Negocio))
                {
                    dcto.NumeroEspecial = datos.Negocio.ToLower();
                }
                else
                {
                    dcto.NumeroEspecial = null;
                }

                    dcto.NumeroAgrupa = null;
                
                dcto.Contrato = Int32.Parse(objCarga.Contrato);


                    dcto.SubcarteraRut = null;
                    dcto.Sbcid = 0;
          
                //Tercero
                dcto.TerceroId = 0;

                //Cuenta
                dcto.IdCuenta = "";
                dcto.DescripcionCuenta = "";

                dcto.Antecedentes = "N";
                dcto.Originales = "S";

                if (!error[0])
                {
                    resultado = dao.Comprobante.GrabarDocumento(dcto, objSession.CodigoEmpresa);
                }
                if (resultado <= 0)
                {
                    error[0] = true;
                    return error;
                }
                else
                {
                    resultado = dao.Comprobante.InsertarHistorialCarga(objSession.CodigoEmpresa, objCarga.Pclid, datos.Ctcid, resultado, estid, objSession.CodigoSucursal, null, objSession.IpRed, objSession.IpPc, "", dcto.MontoAsignado, dcto.Saldo, objSession.UserId, datos.FecVen);
                    if (resultado < 0)
                    {
                        error[0] = true;
                    }
                    return error;
                }

            }

            if (resultado < 0)
            {
                error[0] = true;
                return error;
            }
            else
            {
                return error;
            }

        }
        #endregion

    }
}
