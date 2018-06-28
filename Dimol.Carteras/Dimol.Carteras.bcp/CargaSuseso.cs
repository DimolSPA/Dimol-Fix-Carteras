using Dimol.bcp;
using Dimol.Carteras.dto;
using Dimol.dto;
using System;
using System.Globalization;
using System.IO;

namespace Dimol.Carteras.bcp
{
    public class CargaSuseso
    {
        public static void ProcesoCargaSuseso(dto.CargaSuseso obj, UserSession objSession)
        {
            LeerArchivoInteresesReajuste(obj);

            if (!obj.Error)
            {
                GrabarArchivosInteresesReajuste(obj);
            }
        }

        public static void LeerArchivoInteresesReajuste(dto.CargaSuseso obj)
        {
            try
            {
                Funciones func = new Funciones();

                for (int i = 0; i < obj.ListaArchivos.Count; i++)
                {
                    var rutaArchivo = obj.RutaDirectorio + obj.ListaArchivos[i];
                    var nombreArchivo = obj.ListaArchivos[i];

                    using (StreamReader sr = new StreamReader(rutaArchivo))
                    {
                        string strLinea;
                        string[] filas;

                        var contLineas = 0;
                        bool esReajuste = false;
                        while ((strLinea = sr.ReadLine()) != null)
                        {
                            contLineas++;

                            //Descarta la primera linea
                            if (contLineas == 1)
                            {
                                //Verifica si se trata de un archivo de intereses o de reajuste
                                if (strLinea.Contains("Reajuste"))
                                {
                                    esReajuste = true;
                                }

                                continue;
                            }

                            filas = strLinea.Split(';');
                            if (esReajuste)
                            {
                                SusesoArchivoReajusteModel item = MapearDatosArchivoReajuste(filas, obj, nombreArchivo, contLineas);

                                obj.ListaSusesoArchivoReajuste.Add(item);
                            }
                            else
                            {
                                SusesoArchivoInteresModel item = MapearDatosArchivoIntereses(filas, obj, nombreArchivo, contLineas);

                                obj.ListaSusesoArchivoInteres.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception /*ex*/)
            {
                //obj.Archivos.Find(x => x.NombreArchivo.Contains("DEUDOR")).Error = ex.Message;
            }
        }

        public static void GrabarArchivosInteresesReajuste(dto.CargaSuseso obj)
        {
            try {
                #region Archivos de intereses
                //Elimina los registros por FechaPago
                if (obj.ListaSusesoArchivoInteres.Count > 0)
                {
                    dao.CargaSuseso.EliminarInteresesPorFechaPago(obj.ListaSusesoArchivoInteres[0]);
                }

                //Inserta los nuevos registros
                foreach (SusesoArchivoInteresModel ArchivoInteres in obj.ListaSusesoArchivoInteres)
                {
                    dao.CargaSuseso.InsertarCargaArchivoInteres(ArchivoInteres);
                }
                #endregion

                #region Archivos de reajuste
                //Elimina los registros por FechaPago
                if (obj.ListaSusesoArchivoReajuste.Count > 0)
                {
                    dao.CargaSuseso.EliminarReajustePorFechaPago(obj.ListaSusesoArchivoReajuste[0]);
                }

                //Inserta los nuevos registros
                foreach (SusesoArchivoReajusteModel ArchivoReajuste in obj.ListaSusesoArchivoReajuste)
                {
                    dao.CargaSuseso.InsertarCargaArchivoReajuste(ArchivoReajuste);
                }
                #endregion
            } catch (Exception ex) {
                obj.ErrorMensaje = obj.ErrorMensaje + "|" + ex.Message;
            }
        }

        protected static SusesoArchivoInteresModel MapearDatosArchivoIntereses(string[] filas, dto.CargaSuseso obj, string nombreArchivo, int contLineas) {
            SusesoArchivoInteresModel item = new SusesoArchivoInteresModel();

            //VALIDACION DE QUE LOS DATOS VACIOS/INVALIDOS
            try
            {
                //FechaPago
                string stringFechaPago = filas[0] + "-" + filas[1].ToString().PadLeft(2, '0') + "-" + filas[2].ToString().PadLeft(2, '0') + " 00:00:00,000";
                item.FechaPago = DateTime.ParseExact(stringFechaPago, "yyyy-MM-dd HH:mm:ss,fff", CultureInfo.InvariantCulture);

                //FechaDocumento
                string stringFechaDocumento = filas[3] + "-" + filas[4].ToString().PadLeft(2, '0') + "-01 00:00:00,000";
                item.FechaDocumento = DateTime.ParseExact(stringFechaDocumento, "yyyy-MM-dd HH:mm:ss,fff", CultureInfo.InvariantCulture);

                //TasaInteres
                item.TasaInteres = decimal.Parse(filas[5]);
            }
            catch (Exception)
            {
                item.Error = "Datos inválidos:<br>&nbsp;&nbsp;-&nbsp;Archivo: " + nombreArchivo + "<br>&nbsp;&nbsp;-&nbsp;Linea: " + contLineas;
                obj.Error = true;
            }

            return item;
        }

        protected static SusesoArchivoReajusteModel MapearDatosArchivoReajuste(string[] filas, dto.CargaSuseso obj, string nombreArchivo, int contLineas)
        {
            SusesoArchivoReajusteModel item = new SusesoArchivoReajusteModel();

            //VALIDACION DE QUE LOS DATOS VACIOS/INVALIDOS
            try
            {
                //Fecha de Pago
                string stringFechaPago = filas[0] + "-" + filas[1].ToString().PadLeft(2, '0') + "-01 00:00:00,000";
                item.FechaPago = DateTime.ParseExact(stringFechaPago, "yyyy-MM-dd HH:mm:ss,fff", CultureInfo.InvariantCulture);

                //Periodo
                item.Periodo = filas[2];

                //Fecha Inicial
                string stringFechaInicial = filas[3] + " 00:00:00,000";
                item.FechaInicial = DateTime.ParseExact(stringFechaInicial, "dd-MM-yyyy HH:mm:ss,fff", CultureInfo.InvariantCulture);

                //Fecha Final
                string stringFechaFinal = filas[4] + " 00:00:00,000";
                item.FechaFinal = DateTime.ParseExact(stringFechaFinal, "dd-MM-yyyy HH:mm:ss,fff", CultureInfo.InvariantCulture);

                //Reajuste
                item.Reajuste = decimal.Parse(filas[5]);
            }
            catch (Exception)
            {
                item.Error = "Datos inválidos:<br>&nbsp;&nbsp;-&nbsp;Archivo: " + nombreArchivo + "<br>&nbsp;&nbsp;-&nbsp;Linea: " + contLineas;
                obj.Error = true;
            }

            return item;
        }

        //public static void CargarDocumentoSitrel(dto.SitrelCarga obj, UserSession objSession)
        //{
        //    dao.CargaSusesos.CargarDocumentoSitrel(obj, objSession);
        //}

        //public static void CargarSGD(dto.SitrelCarga obj, UserSession objSession)
        //{
        //    bool[] error = {false,false};
        //    int ctcid=0;
        //    int idTipoDocumento = 0;
        //    ErrorCarga objError = new ErrorCarga();
        //    Funciones objFunc = new Funciones();
        //    bool[] salida = {false,false};
        //    List<Combobox> lstTipoDocumento = dao.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma);
        //    Combobox tipoDocumento = new Combobox();
        //    int idComuna = 112;
        //    string direccion = ".";
        //    int existe = 0;
        //    //Creamos los deudores
        //    foreach (SitrelDeudor deudor in obj.Deudores)
        //    {
        //        if (!error[0])
        //        {
        //            ctcid = dao.Deudor.BuscarIdDeudor(deudor.Rut + deudor.DigitoVerificador, objSession.CodigoEmpresa);
        //            if (ctcid == 0)
        //            {
        //                SitrelDeudorDireccion dir = obj.Direcciones.FirstOrDefault(x => x.Rut == deudor.Rut);
        //                if (dir != null)
        //                {
        //                    idComuna = dao.CargaSusesos.ConversorComunas(objSession.CodigoEmpresa, obj.Pclid, Int32.Parse( dir.Comuna)) ;
        //                    direccion = dir.Direccion;
        //                }
        //                if(idComuna == 0)
        //                {
        //                    idComuna = 112;
        //                }

        //                ctcid = dao.Deudor.GuardarDeudor(objSession.CodigoEmpresa, 0, deudor.Nombres, deudor.ApellidoPaterno, deudor.ApellidoMaterno, deudor.Rut + deudor.DigitoVerificador, deudor.NombreCompleto, idComuna, "P", direccion, "", "N", "N",deudor.CuentaCorriente == "S"? 1: 0, false);
        //                deudor.Ctcid = ctcid;
        //            }
        //            //else //actualiza datos deudor coopeuch
        //            //{
        //            //    deudor.Ctcid = ctcid;
        //            //    ctcid = dao.Deudor.EditarDeudorParcial(objSession.CodigoEmpresa, ctcid, deudor.Nombres, deudor.ApellidoPaterno, deudor.ApellidoMaterno, deudor.NombreCompleto, idComuna, direccion, deudor.CuentaCorriente == "S" ? 1 : 0);
        //            //}
        //            if (ctcid == 0)
        //            {
        //                obj.ListaErrores.Add(new ErrorCarga
        //                                    {
        //                                        TipoError = "ERROR AL CREAR DEUDOR",
        //                                        Rut = deudor.Rut,
        //                                        Dv = deudor.DigitoVerificador,
        //                                        Nombre = deudor.NombreCompleto,
        //                                        TipoDocumento = deudor.TipoPersona,
        //                                        Numero =""
        //                                    });
        //                //error = true;
        //            }
        //        }
        //    }

        //    //agregamos las direcciones
        //    foreach (SitrelDeudorDireccion direcc in obj.Direcciones)
        //    {
        //        if (!error[0])
        //        {
        //            ctcid = 0;
        //            SitrelDeudor deudor = obj.Deudores.FirstOrDefault(x => x.Rut == direcc.Rut);
        //            if (deudor != null)
        //            {
        //                ctcid = deudor.Ctcid;
        //            }
        //            else
        //            {
        //                ctcid = dao.Deudor.BuscarIdDeudor(direcc.Rut + Funciones.Digito(Int32.Parse( direcc.Rut)), objSession.CodigoEmpresa);
        //            }
        //            if (ctcid < 0)
        //            {
        //                obj.ListaErrores.Add(new ErrorCarga
        //                                    {
        //                                        TipoError = "ERROR AL CREAR DIRECCION, NO EXISTE DEUDOR",
        //                                        Rut = direcc.Rut,
        //                                        Dv = "",
        //                                        Nombre = "",
        //                                        TipoDocumento = direcc.Ciudad,
        //                                        Numero = direcc.Direccion
        //                                    });
        //                //error = true;
        //            }
        //            else
        //            {
        //                direcc.Codemp = obj.Codemp;
        //                direcc.Origen = "C";
        //                direcc.Enviado = "N";
        //                direcc.Ctcid = ctcid;
        //                dao.CargaSusesos.InsertarDeudorDireccionSitrel(direcc);
        //            }
        //        }
        //    }

        //    //agregamos los telefonos
        //    foreach (SitrelDeudorTelefono telefono in obj.Telefonos )
        //    {
        //        if (!error[0])
        //        {
        //            ctcid = 0;
        //            SitrelDeudor deudor = obj.Deudores.FirstOrDefault(x => x.Rut == telefono.Rut);
        //            if (deudor != null)
        //            {
        //                ctcid = deudor.Ctcid;
        //            }
        //            else
        //            {
        //                ctcid = dao.Deudor.BuscarIdDeudor(telefono.Rut + Funciones.Digito(Int32.Parse(telefono.Rut)), objSession.CodigoEmpresa);
        //            }
        //            if (ctcid < 0)
        //            {
        //                obj.ListaErrores.Add(new ErrorCarga
        //                                    {
        //                                        TipoError = "ERROR AL CREAR TELEFONO, NO EXISTE DEUDOR",
        //                                        Rut = telefono.Rut,
        //                                        Dv = "",
        //                                        Nombre = "",
        //                                        TipoDocumento = telefono.TipoTelefono,
        //                                        Numero = telefono.Numero
        //                                    });
        //                //error = true;
        //            }
        //            else
        //            {
        //                telefono.Codemp = obj.Codemp;
        //                telefono.Origen = "C";
        //                telefono.Enviado = "N";
        //                telefono.Ctcid = ctcid;
        //                dao.CargaSusesos.InsertarDeudorTelefonoSitrel(telefono);
        //            }    
        //        }
        //    }

        //    //agregamos los email
        //    foreach (SitrelDeudorEmail email in obj.Email)
        //    {
        //        if (!error[0])
        //        {
        //            ctcid = 0;
        //            SitrelDeudor deudor = obj.Deudores.FirstOrDefault(x => x.Rut == email.Rut);
        //            if (deudor != null)
        //            {
        //                ctcid = deudor.Ctcid;
        //            }
        //            else
        //            {
        //                ctcid = dao.Deudor.BuscarIdDeudor(email.Rut + Funciones.Digito(Int32.Parse(email.Rut)), objSession.CodigoEmpresa);
        //            }
        //            if (ctcid < 0)
        //            {
        //                obj.ListaErrores.Add(new ErrorCarga
        //                                    {
        //                                        TipoError = "ERROR AL CREAR EMAIL, NO EXISTE DEUDOR",
        //                                        Rut = email.Rut,
        //                                        Dv = "",
        //                                        Nombre = "",
        //                                        TipoDocumento = "",
        //                                        Numero = email.Email
        //                                    });
        //                //error = true;
        //            }
        //            else
        //            {
        //                email.Codemp = obj.Codemp;
        //                email.Origen = "C";
        //                email.Enviado = "N";
        //                email.Ctcid = ctcid;
        //                dao.CargaSusesos.InsertarDeudorEmailSitrel(email);
        //            }
        //        }
        //    }

        //    //grabamos los documentos
        //    foreach (SitrelCuota cuota in obj.Cuotas)
        //    {

        //        SitrelOperacion oper = obj.Operaciones.FirstOrDefault(x => x.NumeroOperacion == cuota.NumeroOperacion);
        //        if (oper == null) 
        //        {
        //            obj.ListaErrores.Add(new ErrorCarga
        //            {
        //                TipoError = "CUOTA NO TIENE OPERACION ASOCIADA",
        //                Rut = "",
        //                Dv = "",
        //                Nombre = "",
        //                TipoDocumento = cuota.Producto,
        //                Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
        //            });
        //            //error[0] = true;
        //            break;
        //        }
        //        SitrelDeudor deudor = obj.Deudores.FirstOrDefault(x => x.Rut == oper.Rut);
        //        if (deudor == null)
        //        {
        //            obj.ListaErrores.Add(new ErrorCarga
        //            {
        //                TipoError = "CUOTA NO TIENE DEUDOR ASOCIADO",
        //                Rut = "",
        //                Dv = "",
        //                Nombre = "",
        //                TipoDocumento = cuota.Producto,
        //                Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
        //            });
        //            //error[0] = true;
        //            break;
        //        }
        //        existe = dao.Comprobante.BuscarCarteraCliente(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid);
        //        if (existe == 0)
        //        {
        //            if (dao.Comprobante.InsertarCarteraCliente(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid) < 0)
        //            {
        //                obj.ListaErrores.Add(new ErrorCarga
        //                {
        //                    TipoError = "ERROR AL INSERTAR CARTERA CLIENTE",
        //                    Rut = deudor.Rut + "-" + deudor.DigitoVerificador,
        //                    Dv = deudor.DigitoVerificador,
        //                    Nombre = deudor.NombreCompleto,
        //                    TipoDocumento = oper.NombreProducto,
        //                    Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
        //                });
        //                error[0] = true;
        //            }
        //        }
        //        tipoDocumento = lstTipoDocumento.Find(x => x.Value == cuota.Producto);
        //        if (tipoDocumento == null)
        //        {
        //            tipoDocumento = lstTipoDocumento.Find(x => x.Text == oper.NombreProducto.Replace("FACTURA", "FACT."));
        //        }
        //        if (tipoDocumento == null)
        //        {
        //            idTipoDocumento = -1;
        //        }
        //        else
        //        {
        //            idTipoDocumento = Int32.Parse(tipoDocumento.Value);
        //        }

        //        //Comienzo a Grabar los Documentos-------------------
        //        dto.Comprobante dcto = new dto.Comprobante();
        //        int resultado = 0;
        //        int estid = 0;
        //        DateTime fecha = DateTime.ParseExact(cuota.FechaVencimiento.ToString(), "yyyyMMdd", null);
        //        //Busco si esta asignado a la cartera
        //        existe = dao.Comprobante.BuscarCarteraCliente(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid);
        //        if (existe == 0)
        //        {
        //            if (dao.Comprobante.InsertarCarteraCliente(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid) < 0)
        //            {
        //                obj.ListaErrores.Add(new ErrorCarga
        //                {
        //                    TipoError = "ERROR AL INSERTAR CARTERA CLIENTE",
        //                    Rut = deudor.Rut + "-" + deudor.DigitoVerificador,
        //                    Dv = deudor.DigitoVerificador,
        //                    Nombre = deudor.NombreCompleto,
        //                    TipoDocumento = oper.NombreProducto,
        //                    Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
        //                });
        //                error[0] = true;
        //            }
        //        }
        //        //Grabo el Documento
        //        if (!error[0])
        //        {
        //            dcto.Pclid = obj.Pclid;
        //            dcto.Ctcid = deudor.Ctcid;
        //            dcto.Ccbid = 0;
        //            dcto.TipoDocumento = idTipoDocumento.ToString();
        //            dcto.TipoCartera = Int32.Parse(obj.TipoCartera);
        //            dcto.NumeroCpbt = cuota.NumeroOperacion + "/" + cuota.NumeroCuota;
        //            dcto.FechaVencimiento = fecha;
        //            dcto.FechaDocumento = fecha;
        //            if (obj.TipoCartera == "1")
        //            {
        //                if (fecha > DateTime.Parse(objFunc.FechaServer()))
        //                {
        //                    estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 100);
        //                }
        //                else
        //                {
        //                    estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 99);
        //                }
        //            }
        //            else
        //            {
        //                if (obj.Pclid == 424)
        //                {
        //                    estid = 228;//APROBACION AREA COMERCIAL
        //                }
        //                else
        //                {
        //                    estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 17);
        //                }
        //            }
        //            dcto.EstadoCartera = estid.ToString();
        //            dcto.EstadoCpbt = "V";
        //            List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);

        //            dcto.CodigoMoneda = Int32.Parse(lstMoneda.Find(x => x.Text == oper.Moneda.ToUpper()).Value);
        //            dcto.TipoCambio = 1;  // ajustar despues
        //            dcto.MontoAsignado = cuota.MontoDetalle == 0 ? cuota.Capital + cuota.Intereses + cuota.Gastos : cuota.MontoDetalle;
        //            dcto.Monto = cuota.Capital;
        //            dcto.Saldo = cuota.Capital;
        //            dcto.Intereses = cuota.Intereses;
        //            dcto.GastoOtros = cuota.Gastos;
        //            dcto.NombreBanco = null;
        //            dcto.RutGirador = null;
        //            dcto.NombreGirador = null;
        //            dcto.MotivoCobranza = "26";//dao.Comprobante.BuscarMotivoCobranza(objSession.CodigoEmpresa, objSession.Idioma, datos.MotivoCobranza).ToString();
        //            if (string.IsNullOrEmpty(oper.Glosa))
        //            {
        //                dcto.Comentario = "";
        //            }
        //            else
        //            {
        //                dcto.Comentario = oper.Glosa;
        //            }
        //            dcto.CodigoCarga = obj.CodigoCarga;
        //            if (!string.IsNullOrEmpty(oper.NombreSucursal))
        //            {
        //                dcto.NumeroEspecial = oper.NombreSucursal;
        //            }
        //            else
        //            {
        //                dcto.NumeroEspecial = null;
        //            }
        //            dcto.NumeroAgrupa = null;
        //            dcto.Contrato = Int32.Parse(obj.Contrato);
        //            dcto.SubcarteraRut = null;
        //            dcto.Antecedentes = "S";
        //            dcto.Originales = "N";

        //            if (!error[0])
        //            {
        //                resultado = dao.CargaSusesos.GrabarDocumentoSitrel(dcto, objSession.CodigoEmpresa);
        //            }
        //            if (resultado <= 0)
        //            {
        //                //error[0] = true;
        //                obj.ListaErrores.Add(new ErrorCarga
        //                {
        //                    TipoError = "ERROR AL GRABAR DOCUMENTO",
        //                    Rut = deudor.Rut + "-" + deudor.DigitoVerificador,
        //                    Dv = deudor.DigitoVerificador,
        //                    Nombre = deudor.NombreCompleto,
        //                    TipoDocumento = oper.NombreProducto,
        //                    Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
        //                });
        //                //return error;
        //            }
        //            else
        //            {
        //                resultado = dao.Comprobante.InsertarHistorialCarga(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid, resultado, estid, objSession.CodigoSucursal, null, "", "", "", cuota.MontoDetalle, cuota.MontoDetalle, objSession.UserId, fecha);
        //                if (resultado < 0)
        //                {
        //                    //error[0] = true;
        //                    obj.ListaErrores.Add(new ErrorCarga
        //                    {
        //                        TipoError = "ERROR AL INSERTAR HISTORIAL",
        //                        Rut = deudor.Rut + "-" + deudor.DigitoVerificador,
        //                        Dv = deudor.DigitoVerificador,
        //                        Nombre = deudor.NombreCompleto,
        //                        TipoDocumento = oper.NombreProducto,
        //                        Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
        //                    });
        //                }
        //                //return error;
        //            }

        //        }

        //    }

        //}

        //public static int InsertarGestion(int codemp, int pclid, int ctcid, DateTime fecha, int accid, string codigoMoneda, string codigoEmpresa, string codigoAccion, string codigoContacto, string codigoRespuesta, string glosaGestion, string fechaCompromiso, decimal montoCompromiso,
        //    decimal montoGestion, string nombreContacto, string programacionLlamada, string telefonoContacto)
        //{
        //    return dao.CargaSusesos.InsertarGestion( codemp, pclid, ctcid, fecha,accid,  codigoMoneda,  codigoEmpresa, codigoAccion,  codigoContacto, codigoRespuesta,  glosaGestion,  fechaCompromiso,  montoCompromiso, montoGestion, nombreContacto,  programacionLlamada, telefonoContacto);
        //}



        //public static List<Combobox> ListarAcciones(int codemp, int pclid, string first)
        //{
        //    return dao.CargaSusesos.ListarAcciones(codemp, pclid, first);
        //}

        //public static List<Combobox> ListarContactos(int codemp, int pclid, string accion, string first)
        //{
        //    return dao.CargaSusesos.ListarContactos(codemp, pclid, accion, first);
        //}

        //public static List<Combobox> ListarRespuestas(int codemp, int pclid, string accion, string contacto, string first)
        //{
        //    return dao.CargaSusesos.ListarRespuestas(codemp, pclid, accion, contacto, first);
        //}

        //public static List<Combobox> ListarTipoDireccion(int codemp, int pclid, string first)
        //{
        //    return dao.CargaSusesos.ListarTipoDireccion(codemp, pclid, first);
        //}

        //public static int InsertarDireccion(SitrelDeudorDireccion obj)
        //{
        //    return dao.CargaSusesos.InsertarDeudorDireccionSitrel(obj);
        //}

        //public static int InsertarTelefono(SitrelDeudorTelefono obj)
        //{
        //    return dao.CargaSusesos.InsertarDeudorTelefonoSitrel(obj);
        //}

        //public static int InsertarEmail(SitrelDeudorEmail obj)
        //{
        //    return dao.CargaSusesos.InsertarDeudorEmailSitrel(obj);
        //}

        //public static List<dto.Direccion> ListarDireccion(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        //{
        //   return dao.CargaSusesos.ListarDireccion(codemp, ctcid, where, sidx, sord, inicio, limite);
        //}

        //public static int ListarDireccionCount(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        //{
        //    return dao.CargaSusesos.ListarDireccionCount(codemp, ctcid, where, sidx, sord, inicio, limite);
        //}

        //#region "Archivos Salida Susesos"

        //public static List<string> ListaSalidaGestiones(int codemp, int pclid, DateTime desde, DateTime hasta)
        //{
        //    return dao.CargaSusesos.ListaSalidaGestiones( codemp, pclid, desde, hasta);
        //}

        //public static List<string> ListaSalidaDirecciones(int codemp, int pclid, DateTime desde, DateTime hasta)
        //{
        //    return dao.CargaSusesos.ListaSalidaDirecciones(codemp, pclid, desde, hasta);
        //}

        //public static List<string> ListaSalidaTelefonos(int codemp, int pclid, DateTime desde, DateTime hasta)
        //{
        //    return dao.CargaSusesos.ListaSalidaTelefonos(codemp, pclid, desde, hasta);
        //}

        //public static List<string> ListaSalidaEmail(int codemp, int pclid, DateTime desde, DateTime hasta)
        //{
        //    return dao.CargaSusesos.ListaSalidaEmail(codemp, pclid, desde, hasta);
        //}

        //public static string NombreArchivoSitrel(int codemp, int tipoArchivo, string tipo)
        //{
        //    return dao.CargaSusesos.NombreArchivoSitrel(codemp, tipoArchivo, tipo);
        //}
        //#endregion
    }
}