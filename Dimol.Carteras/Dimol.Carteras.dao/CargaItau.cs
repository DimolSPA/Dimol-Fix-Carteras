using Dimol.Carteras.dto;
using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dimol.Carteras.dao
{
    public class CargaItau
    {
        public static void InsertarCarga(SitrelCarga obj)
        {
            try
            {
                DataSet ds = new DataSet();

                StoredProcedure sp = new StoredProcedure("ST_Inserta_Carga");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj.IdCarga = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                obj.Error = ex.Message;
                obj.IdCarga = 0;
            }
        }

        public static int InsertarCargaArchivo(dto.SitrelArchivo obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Carga_Archivo");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("id_carga", obj.IdCarga);
                sp.AgregarParametro("codigo_archivo", obj.CodigoArchivo);
                sp.AgregarParametro("nombre_archivo", obj.NombreArchivo);
                sp.AgregarParametro("estado", (object)obj.Estado ?? DBNull.Value);
                sp.AgregarParametro("error", (object)obj.Error?? DBNull.Value);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                obj.Error = ex.Message;
                return -1;
            }
        }

        public static int InsertarDeudor(dto.SitrelDeudor obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Deudor");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("id_carga", obj.IdCarga);
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("digito_verificador", obj.DigitoVerificador);
                sp.AgregarParametro("tipo_persona", obj.TipoPersona);
                sp.AgregarParametro("nombres", obj.Nombres);
                sp.AgregarParametro("apellido_paterno", obj.ApellidoPaterno);
                sp.AgregarParametro("apellido_materno", obj.ApellidoMaterno);
                sp.AgregarParametro("nombre", obj.NombreCompleto);
                sp.AgregarParametro("razon_social", (object)obj.RazonSocial ?? DBNull.Value);
                sp.AgregarParametro("nombre_fantasia", (object)obj.NombreFantasia ?? DBNull.Value);
                sp.AgregarParametro("sexo", obj.Sexo);
                sp.AgregarParametro("segmento_deudor", (object)obj.SegmentoDeudor ?? DBNull.Value);
                sp.AgregarParametro("cuenta_corriente", obj.CuentaCorriente);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarDeudorDireccion(dto.SitrelDeudorDireccion obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Deudor_Direccion");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("id_carga", obj.IdCarga);
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("direccion", obj.Direccion);
                sp.AgregarParametro("zona_geografica", obj.Comuna);
                sp.AgregarParametro("tipo_direccion", obj.TipoDireccion);
                sp.AgregarParametro("tipo_persona", obj.TipoPersona);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarDeudorDireccionSitrel(dto.SitrelDeudorDireccion obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Deudor_Direccion_Sitrel");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("direccion", obj.Direccion);
                sp.AgregarParametro("comid", obj.Comuna);
                sp.AgregarParametro("tipo_direccion", obj.TipoDireccion);
                sp.AgregarParametro("origen", obj.Origen);
                sp.AgregarParametro("enviado", obj.Enviado);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarDeudorTelefono(dto.SitrelDeudorTelefono obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Deudor_Telefono");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("id_carga", obj.IdCarga);
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("numero", obj.Numero);
                sp.AgregarParametro("codigo_area", (object)obj.CodigoArea ?? DBNull.Value);
                sp.AgregarParametro("anexo", (object)obj.Anexo ?? DBNull.Value);
                sp.AgregarParametro("tipo_telefono", obj.TipoTelefono);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarDeudorTelefonoSitrel(dto.SitrelDeudorTelefono obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Deudor_Telefono_Sitrel");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("numero", obj.Numero);
                sp.AgregarParametro("tipo_telefono", obj.TipoTelefono);
                sp.AgregarParametro("anexo", (object)obj.Anexo ?? DBNull.Value);
                sp.AgregarParametro("codigo_area", (object)obj.CodigoArea ?? DBNull.Value);
                sp.AgregarParametro("origen", obj.Origen);
                sp.AgregarParametro("enviado", obj.Enviado);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarDeudorEmail(dto.SitrelDeudorEmail obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Deudor_Email");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("id_carga", obj.IdCarga);
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("email", obj.Email);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarDeudorEmailSitrel(dto.SitrelDeudorEmail obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Deudor_Email_Sitrel");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("email", obj.Email);
                sp.AgregarParametro("origen", obj.Origen);
                sp.AgregarParametro("enviado", obj.Enviado);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarOperacion(dto.SitrelOperacion obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Operacion");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("id_carga", obj.IdCarga);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("numero_operacion", obj.NumeroOperacion);
                sp.AgregarParametro("codigo_producto", obj.CodigoProducto);
                sp.AgregarParametro("nombre_producto", obj.NombreProducto);
                sp.AgregarParametro("tipo_deudor", obj.TipoDeudor);
                sp.AgregarParametro("moneda", obj.Moneda);
                sp.AgregarParametro("monto_mora", obj.MontoMora);
                sp.AgregarParametro("saldo_insoluto", obj.SaldoInsoluto);
                sp.AgregarParametro("monto_operacion", obj.MontoOperacion);
                sp.AgregarParametro("deuda_total", obj.DeudaTotal);
                sp.AgregarParametro("dias_mora", obj.DiasMora);
                sp.AgregarParametro("ejecutivo_cuenta", (object)obj.EjecutivoCuenta ?? DBNull.Value);
                sp.AgregarParametro("monto_total_interes", (object)obj.MontoTotalInteres ?? DBNull.Value);
                sp.AgregarParametro("estado_producto", (object)obj.EstadoProducto ?? DBNull.Value);
                sp.AgregarParametro("fecha_ultimo_pago", (object)obj.FechaUltimoPago ?? DBNull.Value);
                sp.AgregarParametro("campania", obj.Campania);
                sp.AgregarParametro("accion", (object)obj.Accion ?? DBNull.Value);
                sp.AgregarParametro("contacto", (object)obj.Contacto ?? DBNull.Value);
                sp.AgregarParametro("respuesta", (object)obj.Respuesta ?? DBNull.Value);
                sp.AgregarParametro("glosa", (object)obj.Glosa ?? DBNull.Value);
                sp.AgregarParametro("fecha_gestion", obj.FechaGestion == new DateTime()? DBNull.Value : (object)obj.FechaGestion);
                sp.AgregarParametro("codigo_sucursal", obj.CodigoSucursal);
                sp.AgregarParametro("nombre_sucursal", obj.NombreSucursal);
                sp.AgregarParametro("direccion_sucursal", obj.DireccionSucursal);
                sp.AgregarParametro("telefono_sucursal", obj.TelefonoSucursal);
                sp.AgregarParametro("fecha_vencimiento", obj.FechaVencimiento);
                sp.AgregarParametro("nombre_estrategia", obj.NombreEstrategia);
                sp.AgregarParametro("tipo_persona", obj.TipoPersona);


                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarCouta(dto.SitrelCuota obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Cuota");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("id_carga", obj.IdCarga);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("numero_operacion", obj.NumeroOperacion);
                sp.AgregarParametro("producto", obj.Producto);
                sp.AgregarParametro("numero_cuota", obj.NumeroCuota);
                sp.AgregarParametro("fecha_vencimiento", obj.FechaVencimiento);
                sp.AgregarParametro("monto_detalle", obj.MontoDetalle);
                sp.AgregarParametro("capital", obj.Capital);
                sp.AgregarParametro("intereses", obj.Intereses);
                sp.AgregarParametro("gastos", obj.Gastos);
                sp.AgregarParametro("dias_mora", (object)obj.DiasMora ?? DBNull.Value);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarPago(dto.SitrelPago obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Pago");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("id_carga", obj.IdCarga);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("numero_operacion", obj.NumeroOperacion);
                sp.AgregarParametro("codigo_producto", obj.CodigoProducto);
                sp.AgregarParametro("fecha_pago", obj.FechaPago);
                sp.AgregarParametro("monto_pago", obj.MontoPago);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static List<Combobox> ListarAcciones(int codemp, int pclid, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Listar_Acciones");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["CODIGO"].ToString(),
                            Text = ds.Tables[0].Rows[i]["NOMBRE"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static List<Combobox> ListarTipoDireccion(int codemp, int pclid, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Listar_Tipo_Direccion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                                    {
                                        Value = "",
                                        Text = first
                                    });
                }
                
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["CODIGO"].ToString(),
                            Text = ds.Tables[0].Rows[i]["NOMBRE"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static List<Combobox> ListarContactos(int codemp, int pclid, string accion, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Listar_Contacto_Accion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("accion", accion);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["CODIGO"].ToString(),
                            Text = ds.Tables[0].Rows[i]["NOMBRE"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static List<Combobox> ListarRespuestas(int codemp, int pclid, string accion,string contacto, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Listar_Respuesta_Contacto");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("accion", accion);
                sp.AgregarParametro("contacto", contacto);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["CODIGO"].ToString(),
                            Text = ds.Tables[0].Rows[i]["NOMBRE"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static int CargarDocumentoSitrel(SitrelCarga obj, UserSession objSession)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Guardar_Cartera_Clientes");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("id_carga", obj.IdCarga);
                sp.AgregarParametro("codigo_carga", obj.CodigoCarga);
                sp.AgregarParametro("tipcart", obj.TipoCartera);
                sp.AgregarParametro("ipred", objSession.IpRed);
                sp.AgregarParametro("ipmaquina", objSession.IpPc);
                sp.AgregarParametro("estcpbt", "V");
                sp.AgregarParametro("contrato", obj.Contrato);
                sp.AgregarParametro("sucid", objSession.CodigoSucursal);
                sp.AgregarParametro("usrid", objSession.UserId);
                sp.AgregarParametro("gesid", 22);

                int error = sp.EjecutarProcedimientoTransLargo();

                return error;
            } catch (Exception ex) {
                return -1;
            }
        }

        public static int GrabarDocumentoSitrel(dto.Comprobante obj, int codemp)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Guardar_Cartera_Clientes_Cpbt_Doc");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_ctcid", obj.Ctcid);
                sp.AgregarParametro("ccb_ccbid", obj.Ccbid);


                sp.AgregarParametro("ccb_tpcid", Int32.Parse(obj.TipoDocumento));
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("ccb_numero", obj.NumeroCpbt);

                sp.AgregarParametro("ccb_fecdoc", obj.FechaDocumento);
                sp.AgregarParametro("ccb_fecvenc", obj.FechaVencimiento);
                sp.AgregarParametro("ccb_estid", Int32.Parse(obj.EstadoCartera));
                sp.AgregarParametro("ccb_estcpbt", obj.EstadoCpbt);
                sp.AgregarParametro("ccb_codmon", obj.CodigoMoneda);
                sp.AgregarParametro("ccb_tipcambio", decimal.Parse(obj.TipoCambio.ToString()));
                sp.AgregarParametro("ccb_asignado", decimal.Parse(obj.MontoAsignado.ToString()));
                sp.AgregarParametro("ccb_monto", decimal.Parse(obj.Monto.ToString()));
                sp.AgregarParametro("ccb_saldo", decimal.Parse(obj.Saldo.ToString()));
                sp.AgregarParametro("ccb_gastjud", decimal.Parse(obj.GastoJudicial.ToString()));
                sp.AgregarParametro("ccb_gastotro", decimal.Parse(obj.GastoOtros.ToString()));
                sp.AgregarParametro("ccb_intereses", decimal.Parse(obj.Intereses.ToString()));
                sp.AgregarParametro("ccb_honorarios", decimal.Parse(obj.Honorarios.ToString()));
                sp.AgregarParametro("ccb_bcoid", string.IsNullOrEmpty(obj.NombreBanco) || obj.NombreBanco == "0" ? DBNull.Value : (object)obj.NombreBanco);
                if (obj.RutGirador != null)
                {
                    sp.AgregarParametro("ccb_rutgir", obj.RutGirador);
                    sp.AgregarParametro("ccb_nomgir", obj.NombreGirador);
                }
                else
                {
                    sp.AgregarParametro("ccb_rutgir", DBNull.Value);
                    sp.AgregarParametro("ccb_nomgir", DBNull.Value);
                }
                sp.AgregarParametro("ccb_mtcid", Int32.Parse(obj.MotivoCobranza));
                sp.AgregarParametro("ccb_comentario", string.IsNullOrEmpty(obj.Comentario) ? "" : obj.Comentario);
                sp.AgregarParametro("ccb_retent", DBNull.Value);
                sp.AgregarParametro("ccb_codid", string.IsNullOrEmpty(obj.CodigoCarga) ? DBNull.Value : (object)Int32.Parse(obj.CodigoCarga));
                sp.AgregarParametro("ccb_numesp", string.IsNullOrEmpty(obj.NumeroEspecial) ? "" : obj.NumeroEspecial);
                sp.AgregarParametro("ccb_numagrupa", string.IsNullOrEmpty(obj.NumeroAgrupa) ? "" : obj.NumeroAgrupa);
                sp.AgregarParametro("ccb_cctid", obj.Contrato);
                sp.AgregarParametro("ccb_sbcid", (object)obj.SubcarteraRut ?? DBNull.Value);
                sp.AgregarParametro("ccb_docori", obj.Originales);
                sp.AgregarParametro("ccb_docant", obj.Antecedentes);
                ds = sp.EjecutarProcedimiento();
                //int error = sp.EjecutarProcedimientoTrans();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["ccbid"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ConversorComunas(int codemp, int pclid, int codigo)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Comuna_Sitrel");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("codigo", codigo);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["comid"].ToString());
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

        public static int InsertarGestion(int codemp, int pclid, int ctcid, DateTime fecha, int accid, string codigoMoneda, string codigoEmpresa, string codigoAccion, string codigoContacto, string codigoRespuesta, string glosaGestion, string fechaCompromiso, decimal montoCompromiso,
            decimal montoGestion, string nombreContacto, string programacionLlamada, string telefonoContacto)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Inserta_Gestion_Sitrel");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("fecha", fecha);
                sp.AgregarParametro("accid", accid);
                sp.AgregarParametro("codigo_moneda", (object)codigoMoneda ?? DBNull.Value);
                sp.AgregarParametro("codigo_empresa", codigoEmpresa);
                sp.AgregarParametro("codigo_accion", codigoAccion);
                sp.AgregarParametro("codigo_contacto", codigoContacto);
                sp.AgregarParametro("codigo_respuesta",codigoRespuesta);
                sp.AgregarParametro("glosa_gestion",(object) glosaGestion??DBNull.Value);
                sp.AgregarParametro("fecha_compromiso", (object)fechaCompromiso ?? DBNull.Value);
                sp.AgregarParametro("monto_compromiso", (object)montoCompromiso ?? DBNull.Value);
                sp.AgregarParametro("monto_gestion", (object)montoGestion ?? DBNull.Value);
                sp.AgregarParametro("nombre_contacto", (object)nombreContacto ?? DBNull.Value);
                sp.AgregarParametro("programacion_llamada", (object)programacionLlamada ?? DBNull.Value);
                sp.AgregarParametro("telefono_contacto", (object)telefonoContacto ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Gestion Itau:" + pclid.ToString() + "|" + ctcid.ToString() + "|" + fecha.ToLongDateString() + "|" + accid.ToString() + "|" + codigoMoneda + "|" + codigoEmpresa + "|" + codigoAccion + "|" + codigoContacto + "|" + codigoRespuesta + "|" + glosaGestion + "|" + fechaCompromiso + "|" + montoCompromiso.ToString() + "|" + montoGestion.ToString() + "|" + nombreContacto + "|" + programacionLlamada + "|" + telefonoContacto, 1000);
                

                return -1;
            }
        }

        //old

        public static List<AnularCargaMasiva> ListarCargasAnular(int codemp, int estid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<AnularCargaMasiva> lst = new List<AnularCargaMasiva>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Anular_Carga_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
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
                        lst.Add(new AnularCargaMasiva()
                        {
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString() ?? "",
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString() ?? "",
                            Fecha = DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString()),
                            IdUsuario = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Usuario = ds.Tables[0].Rows[i]["Usuario"].ToString() ?? ""
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static int ListarCargasAnularCount(int codemp, int estid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Anular_Carga_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                 if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
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

        public static int BorrarCarga(string codemp, string pclid, string estid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Cartera_Clientes_CpbtDoc_Cargas");
                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_estid", estid);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static List<AprobarCargaMasiva> ListarCargasAprobar(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<AprobarCargaMasiva> lst = new List<AprobarCargaMasiva>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Aprobar_Carga_Grilla");
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
                        lst.Add(new AprobarCargaMasiva()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString() ?? "",
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString() ?? "",
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString() ?? "",
                            NombreDeudor = ds.Tables[0].Rows[i]["NombreDeudor"].ToString() ?? "",
                            FechaDocumento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            FechaIngreso = DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["MontoAsignado"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString() ?? "",
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString() ?? ""
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static int ListarCargasAprobarCount(int codemp,  string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Aprobar_Carga_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
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

        public static int GrabarCarteraClientesEstadosHistorialEspecial(dto.Comprobante obj, UserSession objSession, int nuevoEstado)
        {
            try
            {
                Funciones objFunc = new Funciones();
                DateTime fecha = DateTime.Parse(objFunc.FechaServer());
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial_Especial");
                sp.AgregarParametro("ceh_codemp", objSession.CodigoEmpresa);
                sp.AgregarParametro("ceh_pclid", obj.Pclid);
                sp.AgregarParametro("ceh_ctcid", obj.Ctcid);
                sp.AgregarParametro("ceh_ccbid", obj.Ccbid);
                sp.AgregarParametro("ceh_fecha", fecha);
                sp.AgregarParametro("ceh_estid", nuevoEstado);
                sp.AgregarParametro("ceh_sucid", objSession.CodigoSucursal);
                sp.AgregarParametro("ceh_gesid", DBNull.Value);
                sp.AgregarParametro("ceh_ipred", objSession.IpRed);
                sp.AgregarParametro("ceh_ipmaquina", objSession.IpPc);
                sp.AgregarParametro("ceh_comentario", "");
                sp.AgregarParametro("ceh_monto", obj.Monto);
                sp.AgregarParametro("ceh_saldo", obj.Saldo);
                sp.AgregarParametro("ceh_usrid", objSession.UserId);


                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ActualizaCarteraEstados(int pclid, int ctcid, int ccbid, int estid, string estcpbt, UserSession objSession)
        {
            Dimol.dao.Utilidades util = new Dimol.dao.Utilidades(objSession.CodigoEmpresa,objSession.CodigoSucursal,objSession.UserId,objSession.IpRed, objSession.IpPc);
            return util.ActualizaCarteraEstados(objSession.CodigoEmpresa, pclid, ctcid, ccbid, estid, estcpbt);
        }

        public static Dimol.dao.Utilidades InstanciaUtilidades(UserSession objSession)
        {
            return new Dimol.dao.Utilidades(objSession.CodigoEmpresa,objSession.CodigoSucursal,objSession.UserId,objSession.IpRed, objSession.IpPc);
        }

        public static string[] TraeCuotaPago(string rutCliente, string rutDeudor, string numero)
        {
            string[] datosCuota= {"","","",""};
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Cuota_Pago_CpbtDoc");
                sp.AgregarParametro("pcl_rut", rutCliente);
                sp.AgregarParametro("ctc_rut", rutDeudor);
                sp.AgregarParametro("ccb_numero", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    datosCuota[0] = ds.Tables[0].Rows[0][0].ToString();
                    datosCuota[1] =ds.Tables[0].Rows[0][1].ToString();
                    datosCuota[2] =ds.Tables[0].Rows[0][2].ToString();
                    datosCuota[3] =ds.Tables[0].Rows[0][3].ToString();
                  
                }
            }
            catch (Exception ex)
            {
                
            }
            return datosCuota;
        }

        public static List<dto.Direccion> ListarDireccion(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Direccion> lst = new List<dto.Direccion>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Listar_Direcciones_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new dto.Direccion()
                        {
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                            IdComuna = ds.Tables[0].Rows[i]["IdComuna"].ToString(),
                            Calle = ds.Tables[0].Rows[i]["Calle"].ToString(),
                            Ciudad = ds.Tables[0].Rows[i]["Ciudad"].ToString(),
                            Region = ds.Tables[0].Rows[i]["Region"].ToString(),
                            Pais =ds.Tables[0].Rows[i]["Pais"].ToString(),
                            TipoDireccion = ds.Tables[0].Rows[i]["TipoDireccion"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static int ListarDireccionCount(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Listar_Direcciones_Deudor_Grilla_count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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
        #region "Archivos Salida Itau"

        public static List< string> ListaSalidaGestiones(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
            List<string> salida = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Salida_Gestiones");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("fecha_inicio", desde);
                sp.AgregarParametro("fecha_termino", hasta);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            salida.Add(ds.Tables[0].Rows[i][0].ToString());
                        }
                    }
                    return salida;
                }
                else
                {
                    return salida;
                }
            }
            catch (Exception ex)
            {
                return salida;
            }
        }

        public static List<string> ListaSalidaDirecciones(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
            List<string> salida = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Salida_Direcciones");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("fecha_inicio", desde);
                sp.AgregarParametro("fecha_termino", hasta);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            salida.Add(ds.Tables[0].Rows[i][0].ToString());
                        }
                    }
                    return salida;
                }
                else
                {
                    return salida;
                }
            }
            catch (Exception ex)
            {
                return salida;
            }
        }

        public static List<string> ListaSalidaTelefonos(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
            List<string> salida = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Salida_Telefonos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("fecha_inicio", desde);
                sp.AgregarParametro("fecha_termino", hasta);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            salida.Add(ds.Tables[0].Rows[i][0].ToString());
                        }
                    }
                    return salida;
                }
                else
                {
                    return salida;
                }
            }
            catch (Exception ex)
            {
                return salida;
            }
        }

        public static List<string> ListaSalidaEmail(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
            List<string> salida = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Salida_Email");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("fecha_inicio", desde);
                sp.AgregarParametro("fecha_termino", hasta);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            salida.Add(ds.Tables[0].Rows[i][0].ToString());
                        }
                    }
                    return salida;
                }
                else
                {
                    return salida;
                }
            }
            catch (Exception ex)
            {
                return salida;
            }
        }

        public static string NombreArchivoSitrel(int codemp, int tipoArchivo, string tipo)
        {
            string salida ="";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ST_Nombre_Archivo_Sitrel");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo_archivo", tipoArchivo);
                sp.AgregarParametro("tipo", tipo);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        salida = ds.Tables[0].Rows[0][0].ToString();

                    }
                    return salida;
                }
                else
                {
                    return salida;
                }
            }
            catch (Exception ex)
            {
                return salida;
            }
        }

        #endregion
    }
}

