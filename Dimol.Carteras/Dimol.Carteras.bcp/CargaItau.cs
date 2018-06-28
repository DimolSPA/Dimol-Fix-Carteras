using System;
using System.Collections.Generic;
using System.Linq;
using Dimol.Carteras.dto;
using System.IO;
using System.Configuration;
using System.Globalization;
using Dimol.dto;
using Dimol.bcp;

namespace Dimol.Carteras.bcp
{
    public class CargaItau
    {
        public static void ProcesoCargaItau(SitrelCarga obj, UserSession objSession)
        {
            dao.CargaItau.InsertarCarga(obj);

            foreach (SitrelArchivo archivo in obj.Archivos)
            {
                archivo.Codemp = obj.Codemp;
                archivo.Pclid = obj.Pclid;
                archivo.IdCarga = obj.IdCarga;
            }
            LeerArchivosSitrel(obj);
            GrabarArchivosSitrel(obj);
        }

        public static void LeerArchivosSitrel(SitrelCarga obj)
        {
            try
            {
                LeerArchivoSitrelDeudor(obj);
                LeerArchivoSitrelDeudorDireccion(obj);
                LeerArchivoSitrelDeudorTelefono(obj);
                LeerArchivoSitrelDeudorEmail(obj);
                LeerArchivoSitrelOperacion(obj);
                LeerArchivoSitrelCuota(obj);
                LeerArchivoSitrelPago(obj);
            }
            catch (Exception ex)
            {
                obj.Error = obj.Error + "|" + ex.Message;
            }
        }

        public static void GrabarArchivosSitrel(SitrelCarga obj)
        {
            try
            {
                foreach (SitrelArchivo archivo in obj.Archivos)
                {
                    dao.CargaItau.InsertarCargaArchivo(archivo);
                }

                foreach (SitrelDeudor deudor in obj.Deudores)
                {
                    dao.CargaItau.InsertarDeudor(deudor);
                }

                foreach (SitrelDeudorDireccion direccion in obj.Direcciones)
                {
                    dao.CargaItau.InsertarDeudorDireccion(direccion);
                }

                foreach (SitrelDeudorTelefono telefono in obj.Telefonos)
                {
                    dao.CargaItau.InsertarDeudorTelefono(telefono);
                }

                foreach (SitrelDeudorEmail email in obj.Email)
                {
                    dao.CargaItau.InsertarDeudorEmail(email);
                }

                foreach (SitrelOperacion operacion in obj.Operaciones)
                {
                    dao.CargaItau.InsertarOperacion(operacion);
                }

                foreach (SitrelCuota cuota in obj.Cuotas)
                {
                    dao.CargaItau.InsertarCouta(cuota);
                }

                foreach (SitrelPago pago in obj.Pagos)
                {
                    dao.CargaItau.InsertarPago(pago);
                }
            }
            catch (Exception ex)
            {
                obj.Error = obj.Error + "|" + ex.Message;
            }
        }


        public static void CargarDocumentoSitrel(SitrelCarga obj, UserSession objSession)
        {
            dao.CargaItau.CargarDocumentoSitrel(obj, objSession);
        }

        public static void CargarSGD(dto.SitrelCarga obj, UserSession objSession)
        {
            bool[] error = {false,false};
            int ctcid=0;
            int idTipoDocumento = 0;
            ErrorCarga objError = new ErrorCarga();
            Funciones objFunc = new Funciones();
            bool[] salida = {false,false};
            List<Combobox> lstTipoDocumento = dao.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma);
            Combobox tipoDocumento = new Combobox();
            int idComuna = 112;
            string direccion = ".";
            int existe = 0;
            //Creamos los deudores
            foreach (SitrelDeudor deudor in obj.Deudores)
            {
                if (!error[0])
                {
                    ctcid = dao.Deudor.BuscarIdDeudor(deudor.Rut + deudor.DigitoVerificador, objSession.CodigoEmpresa);
                    if (ctcid == 0)
                    {
                        SitrelDeudorDireccion dir = obj.Direcciones.FirstOrDefault(x => x.Rut == deudor.Rut);
                        if (dir != null)
                        {
                            idComuna = dao.CargaItau.ConversorComunas(objSession.CodigoEmpresa, obj.Pclid, Int32.Parse( dir.Comuna)) ;
                            direccion = dir.Direccion;
                        }
                        if(idComuna == 0)
                        {
                            idComuna = 112;
                        }
                        
                        ctcid = dao.Deudor.GuardarDeudor(objSession.CodigoEmpresa, 0, deudor.Nombres, deudor.ApellidoPaterno, deudor.ApellidoMaterno, deudor.Rut + deudor.DigitoVerificador, deudor.NombreCompleto, idComuna, "P", direccion, "", "N", "N",deudor.CuentaCorriente == "S"? 1: 0, false);
                        deudor.Ctcid = ctcid;
                    }
                    //else //actualiza datos deudor coopeuch
                    //{
                    //    deudor.Ctcid = ctcid;
                    //    ctcid = dao.Deudor.EditarDeudorParcial(objSession.CodigoEmpresa, ctcid, deudor.Nombres, deudor.ApellidoPaterno, deudor.ApellidoMaterno, deudor.NombreCompleto, idComuna, direccion, deudor.CuentaCorriente == "S" ? 1 : 0);
                    //}
                    if (ctcid == 0)
                    {
                        obj.ListaErrores.Add(new ErrorCarga
                                            {
                                                TipoError = "ERROR AL CREAR DEUDOR",
                                                Rut = deudor.Rut,
                                                Dv = deudor.DigitoVerificador,
                                                Nombre = deudor.NombreCompleto,
                                                TipoDocumento = deudor.TipoPersona,
                                                Numero =""
                                            });
                        //error = true;
                    }
                }
            }

            //agregamos las direcciones
            foreach (SitrelDeudorDireccion direcc in obj.Direcciones)
            {
                if (!error[0])
                {
                    ctcid = 0;
                    SitrelDeudor deudor = obj.Deudores.FirstOrDefault(x => x.Rut == direcc.Rut);
                    if (deudor != null)
                    {
                        ctcid = deudor.Ctcid;
                    }
                    else
                    {
                        ctcid = dao.Deudor.BuscarIdDeudor(direcc.Rut + Funciones.Digito(Int32.Parse( direcc.Rut)), objSession.CodigoEmpresa);
                    }
                    if (ctcid < 0)
                    {
                        obj.ListaErrores.Add(new ErrorCarga
                                            {
                                                TipoError = "ERROR AL CREAR DIRECCION, NO EXISTE DEUDOR",
                                                Rut = direcc.Rut,
                                                Dv = "",
                                                Nombre = "",
                                                TipoDocumento = direcc.Ciudad,
                                                Numero = direcc.Direccion
                                            });
                        //error = true;
                    }
                    else
                    {
                        direcc.Codemp = obj.Codemp;
                        direcc.Origen = "C";
                        direcc.Enviado = "N";
                        direcc.Ctcid = ctcid;
                        dao.CargaItau.InsertarDeudorDireccionSitrel(direcc);
                    }
                }
            }

            //agregamos los telefonos
            foreach (SitrelDeudorTelefono telefono in obj.Telefonos )
            {
                if (!error[0])
                {
                    ctcid = 0;
                    SitrelDeudor deudor = obj.Deudores.FirstOrDefault(x => x.Rut == telefono.Rut);
                    if (deudor != null)
                    {
                        ctcid = deudor.Ctcid;
                    }
                    else
                    {
                        ctcid = dao.Deudor.BuscarIdDeudor(telefono.Rut + Funciones.Digito(Int32.Parse(telefono.Rut)), objSession.CodigoEmpresa);
                    }
                    if (ctcid < 0)
                    {
                        obj.ListaErrores.Add(new ErrorCarga
                                            {
                                                TipoError = "ERROR AL CREAR TELEFONO, NO EXISTE DEUDOR",
                                                Rut = telefono.Rut,
                                                Dv = "",
                                                Nombre = "",
                                                TipoDocumento = telefono.TipoTelefono,
                                                Numero = telefono.Numero
                                            });
                        //error = true;
                    }
                    else
                    {
                        telefono.Codemp = obj.Codemp;
                        telefono.Origen = "C";
                        telefono.Enviado = "N";
                        telefono.Ctcid = ctcid;
                        dao.CargaItau.InsertarDeudorTelefonoSitrel(telefono);
                    }    
                }
            }

            //agregamos los email
            foreach (SitrelDeudorEmail email in obj.Email)
            {
                if (!error[0])
                {
                    ctcid = 0;
                    SitrelDeudor deudor = obj.Deudores.FirstOrDefault(x => x.Rut == email.Rut);
                    if (deudor != null)
                    {
                        ctcid = deudor.Ctcid;
                    }
                    else
                    {
                        ctcid = dao.Deudor.BuscarIdDeudor(email.Rut + Funciones.Digito(Int32.Parse(email.Rut)), objSession.CodigoEmpresa);
                    }
                    if (ctcid < 0)
                    {
                        obj.ListaErrores.Add(new ErrorCarga
                                            {
                                                TipoError = "ERROR AL CREAR EMAIL, NO EXISTE DEUDOR",
                                                Rut = email.Rut,
                                                Dv = "",
                                                Nombre = "",
                                                TipoDocumento = "",
                                                Numero = email.Email
                                            });
                        //error = true;
                    }
                    else
                    {
                        email.Codemp = obj.Codemp;
                        email.Origen = "C";
                        email.Enviado = "N";
                        email.Ctcid = ctcid;
                        dao.CargaItau.InsertarDeudorEmailSitrel(email);
                    }
                }
            }

            //grabamos los documentos
            foreach (SitrelCuota cuota in obj.Cuotas)
            {
                
                SitrelOperacion oper = obj.Operaciones.FirstOrDefault(x => x.NumeroOperacion == cuota.NumeroOperacion);
                if (oper == null) 
                {
                    obj.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "CUOTA NO TIENE OPERACION ASOCIADA",
                        Rut = "",
                        Dv = "",
                        Nombre = "",
                        TipoDocumento = cuota.Producto,
                        Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
                    });
                    //error[0] = true;
                    break;
                }
                SitrelDeudor deudor = obj.Deudores.FirstOrDefault(x => x.Rut == oper.Rut);
                if (deudor == null)
                {
                    obj.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "CUOTA NO TIENE DEUDOR ASOCIADO",
                        Rut = "",
                        Dv = "",
                        Nombre = "",
                        TipoDocumento = cuota.Producto,
                        Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
                    });
                    //error[0] = true;
                    break;
                }
                existe = dao.Comprobante.BuscarCarteraCliente(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid);
                if (existe == 0)
                {
                    if (dao.Comprobante.InsertarCarteraCliente(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid) < 0)
                    {
                        obj.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL INSERTAR CARTERA CLIENTE",
                            Rut = deudor.Rut + "-" + deudor.DigitoVerificador,
                            Dv = deudor.DigitoVerificador,
                            Nombre = deudor.NombreCompleto,
                            TipoDocumento = oper.NombreProducto,
                            Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
                        });
                        error[0] = true;
                    }
                }
                tipoDocumento = lstTipoDocumento.Find(x => x.Value == cuota.Producto);
                if (tipoDocumento == null)
                {
                    tipoDocumento = lstTipoDocumento.Find(x => x.Text == oper.NombreProducto.Replace("FACTURA", "FACT."));
                }
                if (tipoDocumento == null)
                {
                    idTipoDocumento = -1;
                }
                else
                {
                    idTipoDocumento = Int32.Parse(tipoDocumento.Value);
                }

                //Comienzo a Grabar los Documentos-------------------
                dto.Comprobante dcto = new dto.Comprobante();
                int resultado = 0;
                int estid = 0;
                DateTime fecha = DateTime.ParseExact(cuota.FechaVencimiento.ToString(), "yyyyMMdd", null);
                //Busco si esta asignado a la cartera
                existe = dao.Comprobante.BuscarCarteraCliente(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid);
                if (existe == 0)
                {
                    if (dao.Comprobante.InsertarCarteraCliente(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid) < 0)
                    {
                        obj.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL INSERTAR CARTERA CLIENTE",
                            Rut = deudor.Rut + "-" + deudor.DigitoVerificador,
                            Dv = deudor.DigitoVerificador,
                            Nombre = deudor.NombreCompleto,
                            TipoDocumento = oper.NombreProducto,
                            Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
                        });
                        error[0] = true;
                    }
                }
                //Grabo el Documento
                if (!error[0])
                {
                    dcto.Pclid = obj.Pclid;
                    dcto.Ctcid = deudor.Ctcid;
                    dcto.Ccbid = 0;
                    dcto.TipoDocumento = idTipoDocumento.ToString();
                    dcto.TipoCartera = Int32.Parse(obj.TipoCartera);
                    dcto.NumeroCpbt = cuota.NumeroOperacion + "/" + cuota.NumeroCuota;
                    dcto.FechaVencimiento = fecha;
                    dcto.FechaDocumento = fecha;
                    if (obj.TipoCartera == "1")
                    {
                        if (fecha > DateTime.Parse(objFunc.FechaServer()))
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
                        if (obj.Pclid == 424)
                        {
                            estid = 228;//APROBACION AREA COMERCIAL
                        }
                        else
                        {
                            estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 17);
                        }
                    }
                    dcto.EstadoCartera = estid.ToString();
                    dcto.EstadoCpbt = "V";
                    List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);

                    dcto.CodigoMoneda = Int32.Parse(lstMoneda.Find(x => x.Text == oper.Moneda.ToUpper()).Value);
                    dcto.TipoCambio = 1;  // ajustar despues
                    dcto.MontoAsignado = cuota.MontoDetalle == 0 ? cuota.Capital + cuota.Intereses + cuota.Gastos : cuota.MontoDetalle;
                    dcto.Monto = cuota.Capital;
                    dcto.Saldo = cuota.Capital;
                    dcto.Intereses = cuota.Intereses;
                    dcto.GastoOtros = cuota.Gastos;
                    dcto.NombreBanco = null;
                    dcto.RutGirador = null;
                    dcto.NombreGirador = null;
                    dcto.MotivoCobranza = "26";//dao.Comprobante.BuscarMotivoCobranza(objSession.CodigoEmpresa, objSession.Idioma, datos.MotivoCobranza).ToString();
                    if (string.IsNullOrEmpty(oper.Glosa))
                    {
                        dcto.Comentario = "";
                    }
                    else
                    {
                        dcto.Comentario = oper.Glosa;
                    }
                    dcto.CodigoCarga = obj.CodigoCarga;
                    if (!string.IsNullOrEmpty(oper.NombreSucursal))
                    {
                        dcto.NumeroEspecial = oper.NombreSucursal;
                    }
                    else
                    {
                        dcto.NumeroEspecial = null;
                    }
                    dcto.NumeroAgrupa = null;
                    dcto.Contrato = Int32.Parse(obj.Contrato);
                    dcto.SubcarteraRut = null;
                    dcto.Antecedentes = "S";
                    dcto.Originales = "N";

                    if (!error[0])
                    {
                        resultado = dao.CargaItau.GrabarDocumentoSitrel(dcto, objSession.CodigoEmpresa);
                    }
                    if (resultado <= 0)
                    {
                        //error[0] = true;
                        obj.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL GRABAR DOCUMENTO",
                            Rut = deudor.Rut + "-" + deudor.DigitoVerificador,
                            Dv = deudor.DigitoVerificador,
                            Nombre = deudor.NombreCompleto,
                            TipoDocumento = oper.NombreProducto,
                            Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
                        });
                        //return error;
                    }
                    else
                    {
                        resultado = dao.Comprobante.InsertarHistorialCarga(objSession.CodigoEmpresa, obj.Pclid, deudor.Ctcid, resultado, estid, objSession.CodigoSucursal, null, "", "", "", cuota.MontoDetalle, cuota.MontoDetalle, objSession.UserId, fecha);
                        if (resultado < 0)
                        {
                            //error[0] = true;
                            obj.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "ERROR AL INSERTAR HISTORIAL",
                                Rut = deudor.Rut + "-" + deudor.DigitoVerificador,
                                Dv = deudor.DigitoVerificador,
                                Nombre = deudor.NombreCompleto,
                                TipoDocumento = oper.NombreProducto,
                                Numero = cuota.NumeroOperacion + "|" + cuota.NumeroCuota
                            });
                        }
                        //return error;
                    }

                }

            }

        }

        public static int InsertarGestion(int codemp, int pclid, int ctcid, DateTime fecha, int accid, string codigoMoneda, string codigoEmpresa, string codigoAccion, string codigoContacto, string codigoRespuesta, string glosaGestion, string fechaCompromiso, decimal montoCompromiso,
            decimal montoGestion, string nombreContacto, string programacionLlamada, string telefonoContacto)
        {
            return dao.CargaItau.InsertarGestion( codemp, pclid, ctcid, fecha,accid,  codigoMoneda,  codigoEmpresa, codigoAccion,  codigoContacto, codigoRespuesta,  glosaGestion,  fechaCompromiso,  montoCompromiso, montoGestion, nombreContacto,  programacionLlamada, telefonoContacto);
        }

      

        public static void LeerArchivoSitrelDeudor(dto.SitrelCarga obj)
        {
            try
            {
                Dimol.bcp.Funciones func= new Dimol.bcp.Funciones() ;
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + func.Configuracion_Str(15) + "\\" +obj.Archivos.Find( x=>x.NombreArchivo.Contains("DEUDOR")).NombreArchivo))
                {
                    string strLinea;
                    string[] filas;
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        filas = strLinea.Split(';');
                        obj.Deudores.Add(new SitrelDeudor
                        {
                            Codemp= obj.Codemp,
                            IdCarga=obj.IdCarga,
                            Pclid=obj.Pclid,
                            Rut=filas[0],
                            DigitoVerificador = filas[1],
                            TipoPersona = filas[2],
                            Nombres = filas[3],
                            ApellidoPaterno = filas[4],
                            ApellidoMaterno = filas[5],
                            NombreCompleto = filas[6],
                            RazonSocial = filas[7],
                            NombreFantasia = filas[8],
                            Sexo = filas[9],
                            SegmentoDeudor = filas[10],
                            CuentaCorriente = filas[11]
                        });
                       
                    }
                }

            }
            catch (Exception ex)
            {
                obj.Archivos.Find(x => x.NombreArchivo.Contains("DEUDOR")).Error = ex.Message;
            }
        }

        public static void LeerArchivoSitrelDeudorDireccion(dto.SitrelCarga obj)
        {
            try
            {
                Dimol.bcp.Funciones func = new Dimol.bcp.Funciones();
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + func.Configuracion_Str(15) + "\\" + obj.Archivos.Find(x => x.NombreArchivo.Contains("DIREC")).NombreArchivo))
                {
                    string strLinea;
                    string[] filas;
                    string[] direccion;
                    string tipoPersona = "PERSONA";
                    string calle = "";
                    string ciudad = "";
                    int region = 0;
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        calle = "";
                        ciudad = "";
                        region = 0;
                        filas = strLinea.Split(';');
                        direccion = filas[1].Split(':');
                        if (direccion.Length == 1)
                        {
                            calle = direccion[0].Trim();
                        }
                        else if (direccion.Length == 4)
                        {
                            calle = direccion[1].Replace("CIUDAD", "").Trim();
                            ciudad = direccion[2].Replace("REGION", "").Trim();
                            region = string.IsNullOrEmpty(filas[3]) ? 0 : Int32.Parse(direccion[3]);
                        }
                        if (obj.Deudores.Find(x => x.Rut == filas[0]) != null)
                        {
                            tipoPersona = obj.Deudores.Find(x => x.Rut == filas[0]).TipoPersona;
                        }
                        obj.Direcciones.Add(new SitrelDeudorDireccion
                        {
                            Codemp = obj.Codemp,
                            IdCarga = obj.IdCarga,
                            Pclid = obj.Pclid,
                            Rut = filas[0],
                            Direccion = calle,
                            Ciudad = ciudad,
                            Region = region,
                            Comuna = filas[2],
                            TipoDireccion = filas[3],
                            TipoPersona = tipoPersona
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Archivos.Find(x => x.NombreArchivo.Contains("DIREC")).Error = ex.Message;
            }
        }

        public static void LeerArchivoSitrelDeudorTelefono(dto.SitrelCarga obj)
        {
            try
            {
                Dimol.bcp.Funciones func = new Dimol.bcp.Funciones();
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + func.Configuracion_Str(15) + "\\" +obj.Archivos.Find(x => x.NombreArchivo.Contains("TELEF")).NombreArchivo))
                {
                    string strLinea;
                    string[] filas;
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        filas = strLinea.Split(';');
                        obj.Telefonos.Add(new SitrelDeudorTelefono
                        {
                            Codemp = obj.Codemp,
                            IdCarga = obj.IdCarga,
                            Pclid = obj.Pclid,
                            Rut = filas[0],
                            CodigoArea = filas[1],
                            Numero = filas[2],
                            Anexo = filas[3],
                            TipoTelefono= filas[4]
                        });

                    }
                }

            }
            catch (Exception ex)
            {
                obj.Archivos.Find(x => x.NombreArchivo.Contains("TELEF")).Error = ex.Message;
            }
        }

        public static void LeerArchivoSitrelDeudorEmail(dto.SitrelCarga obj)
        {
            try
            {
                Dimol.bcp.Funciones func = new Dimol.bcp.Funciones();
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + func.Configuracion_Str(15) + "\\" +obj.Archivos.Find(x => x.NombreArchivo.Contains("EMAIL")).NombreArchivo))
                {
                    string strLinea;
                    string[] filas;
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        filas = strLinea.Split(';');
                        obj.Email.Add(new SitrelDeudorEmail
                        {
                            Codemp = obj.Codemp,
                            IdCarga = obj.IdCarga,
                            Pclid = obj.Pclid,
                            Rut = filas[0],
                            Email = filas[1]
                        });

                    }
                }

            }
            catch (Exception ex)
            {
                obj.Archivos.Find(x => x.NombreArchivo.Contains("EMAIL")).Error = ex.Message;
            }
        }

        public static void LeerArchivoSitrelOperacion(dto.SitrelCarga obj)
        {
            try
            {
                Dimol.bcp.Funciones func = new Dimol.bcp.Funciones();
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + func.Configuracion_Str(15) + "\\" +obj.Archivos.Find(x => x.NombreArchivo.Contains("OPER")).NombreArchivo))
                {
                    string strLinea;
                    string[] filas;
                    string tipoPersona = "PERSONA";
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        filas = strLinea.Split(';');
                        if (obj.Deudores.Find(x => x.Rut == filas[0]) != null)
                        {
                            tipoPersona = obj.Deudores.Find(x => x.Rut == filas[0]).TipoPersona;
                        }
                        obj.Operaciones.Add(new SitrelOperacion
                        {
                            Codemp = obj.Codemp,
                            IdCarga = obj.IdCarga,
                            Pclid = obj.Pclid,
                            Rut = filas[0],
                            NumeroOperacion = filas[1],
                            CodigoProducto = filas[2],
                            NombreProducto = filas[3],
                            TipoDeudor = filas[4],
                            Moneda = filas[5],
                            MontoMora = string.IsNullOrEmpty(filas[6]) ? 0 :decimal.Parse(filas[6])/100,
                            SaldoInsoluto = string.IsNullOrEmpty(filas[7]) ? 0 :decimal.Parse(filas[7])/100,
                            MontoOperacion =string.IsNullOrEmpty(filas[8]) ? 0 : decimal.Parse(filas[8])/100,
                            DeudaTotal = string.IsNullOrEmpty(filas[9]) ? 0 : decimal.Parse(filas[9]) / 100,
                            DiasMora = string.IsNullOrEmpty(filas[10]) ? 0 : Int32.Parse(filas[10]),
                            EjecutivoCuenta = filas[11],
                            MontoTotalInteres = string.IsNullOrEmpty(filas[12]) ? 0 : decimal.Parse(filas[12]) / 100,
                            EstadoProducto = filas[13],
                            FechaUltimoPago = string.IsNullOrEmpty(filas[14]) ? 0 : Int32.Parse(filas[14]),
                            Campania = filas[15],
                            Accion = filas[16],
                            Contacto = filas[17],
                            Respuesta = filas[18],
                            Glosa = filas[19],
                            FechaGestion = string.IsNullOrEmpty(filas[20]) ? new DateTime() : DateTime.ParseExact(filas[20], "yyyyMMdd HHmmss", CultureInfo.InvariantCulture),
                            CodigoSucursal = filas[21],
                            NombreSucursal = filas[22],
                            DireccionSucursal = filas[23],
                            TelefonoSucursal= filas[24],
                            FechaVencimiento = string.IsNullOrEmpty(filas[25]) ? 0 : Int32.Parse(filas[25]),
                            NombreEstrategia = filas[26],
                            TipoPersona = tipoPersona
                        });

                    }
                }

            }
            catch (Exception ex)
            {
                obj.Archivos.Find(x => x.NombreArchivo.Contains("OPER")).Error = ex.Message;
            }
        }

        public static void LeerArchivoSitrelCuota(dto.SitrelCarga obj)
        {
            try
            {
                Dimol.bcp.Funciones func = new Dimol.bcp.Funciones();
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + func.Configuracion_Str(15) + "\\" +obj.Archivos.Find(x => x.NombreArchivo.Contains("CUOTA")).NombreArchivo))
                {
                    string strLinea;
                    string[] filas;
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        filas = strLinea.Split(';');
                        string rut="";
                        if (obj.Operaciones.FirstOrDefault(x => x.NumeroOperacion == filas[0]) != null)
                        {
                            rut = obj.Operaciones.FirstOrDefault(x => x.NumeroOperacion == filas[0]).Rut;
                        }
                        obj.Cuotas.Add(new SitrelCuota
                        {
                            Codemp = obj.Codemp,
                            IdCarga = obj.IdCarga,
                            Pclid = obj.Pclid,
                            Rut= rut,
                            NumeroOperacion = filas[0],
                            Producto = filas[1],
                            NumeroCuota = string.IsNullOrEmpty(filas[2]) ? 0 : Int32.Parse(filas[2]),
                            FechaVencimiento = string.IsNullOrEmpty(filas[3]) ? 0 : Int32.Parse(filas[3]),
                            MontoDetalle =decimal.Parse( filas[4]) / 100,
                            Capital = decimal.Parse(filas[5]) / 100,
                            Intereses = decimal.Parse(filas[6]) / 100,
                            Gastos = decimal.Parse(filas[7]) / 100,
                            DiasMora = Int32.Parse(filas[8])
                        });

                    }
                }

            }
            catch (Exception ex)
            {
                obj.Archivos.Find(x => x.NombreArchivo.Contains("CUOTA")).Error = ex.Message;
            }
        }

        public static void LeerArchivoSitrelPago(dto.SitrelCarga obj)
        {
            try
            {
                Dimol.bcp.Funciones func = new Dimol.bcp.Funciones();
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + func.Configuracion_Str(15) + "\\" +obj.Archivos.Find(x => x.NombreArchivo.Contains("PAGO")).NombreArchivo))
                {
                    string strLinea;
                    string[] filas;
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        filas = strLinea.Split(';');
                        obj.Pagos.Add(new SitrelPago
                        {
                            Codemp = obj.Codemp,
                            IdCarga = obj.IdCarga,
                            Pclid = obj.Pclid,
                            Rut = filas[0],
                            NumeroOperacion = filas[1],
                            CodigoProducto =filas[2],
                            FechaPago = Int32.Parse(filas[3]),
                            MontoPago = decimal.Parse(filas[4]) / 100
                        });

                    }
                }

            }
            catch (Exception ex)
            {
                obj.Archivos.Find(x => x.NombreArchivo.Contains("PAGO")).Error = ex.Message;
            }
        }

        public static List<Combobox> ListarAcciones(int codemp, int pclid, string first)
        {
            return dao.CargaItau.ListarAcciones(codemp, pclid, first);
        }

        public static List<Combobox> ListarContactos(int codemp, int pclid, string accion, string first)
        {
            return dao.CargaItau.ListarContactos(codemp, pclid, accion, first);
        }

        public static List<Combobox> ListarRespuestas(int codemp, int pclid, string accion, string contacto, string first)
        {
            return dao.CargaItau.ListarRespuestas(codemp, pclid, accion, contacto, first);
        }

        public static List<Combobox> ListarTipoDireccion(int codemp, int pclid, string first)
        {
            return dao.CargaItau.ListarTipoDireccion(codemp, pclid, first);
        }

        public static int InsertarDireccion(SitrelDeudorDireccion obj)
        {
            return dao.CargaItau.InsertarDeudorDireccionSitrel(obj);
        }

        public static int InsertarTelefono(SitrelDeudorTelefono obj)
        {
            return dao.CargaItau.InsertarDeudorTelefonoSitrel(obj);
        }

        public static int InsertarEmail(SitrelDeudorEmail obj)
        {
            return dao.CargaItau.InsertarDeudorEmailSitrel(obj);
        }

        public static List<dto.Direccion> ListarDireccion(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
           return dao.CargaItau.ListarDireccion(codemp, ctcid, where, sidx, sord, inicio, limite);
        }

        public static int ListarDireccionCount(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.CargaItau.ListarDireccionCount(codemp, ctcid, where, sidx, sord, inicio, limite);
        }

        #region "Archivos Salida Itau"

        public static List<string> ListaSalidaGestiones(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
            return dao.CargaItau.ListaSalidaGestiones( codemp, pclid, desde, hasta);
        }

        public static List<string> ListaSalidaDirecciones(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
            return dao.CargaItau.ListaSalidaDirecciones(codemp, pclid, desde, hasta);
        }

        public static List<string> ListaSalidaTelefonos(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
            return dao.CargaItau.ListaSalidaTelefonos(codemp, pclid, desde, hasta);
        }

        public static List<string> ListaSalidaEmail(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
            return dao.CargaItau.ListaSalidaEmail(codemp, pclid, desde, hasta);
        }

        public static string NombreArchivoSitrel(int codemp, int tipoArchivo, string tipo)
        {
            return dao.CargaItau.NombreArchivoSitrel(codemp, tipoArchivo, tipo);
        }

        #endregion
    }
}
