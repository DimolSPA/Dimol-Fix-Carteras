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
    public class Tesoreria
    {
        public static List<dto.CuentaBancaria> ListarTesoreriaCuentasBancariasGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.CuentaBancaria> lst = new List<dto.CuentaBancaria>();
           
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tesoreria_Cuentas_Bancarias_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.CuentaBancaria()
                        {
                            CuentaId = Int32.Parse(ds.Tables[0].Rows[i]["CuentaId"].ToString()),
                            NumCuenta = ds.Tables[0].Rows[i]["NumCuenta"].ToString(),
                            TipoCuenta = ds.Tables[0].Rows[i]["TipoCuenta"].ToString(),
                            Banco = ds.Tables[0].Rows[i]["Banco"].ToString(),
                            MontoConciliar = Decimal.Parse(ds.Tables[0].Rows[i]["MontoConciliar"].ToString()),
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
        public static List<dto.CartolaMovimiento> ListarCartolaMovimientosGrilla(int codemp, string numCuenta, string where, string sidx, string sord)
        {
            List<dto.CartolaMovimiento> lst = new List<dto.CartolaMovimiento>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Cartola_Movimientos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", numCuenta);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.CartolaMovimiento()
                        {
                            MovimientoId = Int32.Parse(ds.Tables[0].Rows[i]["MovimientoId"].ToString()),
                            NumCuenta = ds.Tables[0].Rows[i]["NumCuenta"].ToString(),
                            FecMovimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FecMovimiento"].ToString()),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Sucursal = ds.Tables[0].Rows[i]["Sucursal"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            Movimiento = ds.Tables[0].Rows[i]["Movimiento"].ToString(),
                            Motivo = ds.Tables[0].Rows[i]["Motivo"].ToString(),
                            MotivoSistema = ds.Tables[0].Rows[i]["MotivoSistema"].ToString(),
                            MotivoSistemaId = ds.Tables[0].Rows[i]["MotivoSistemaId"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoId = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            Observacion = ds.Tables[0].Rows[i]["OBSERVACION"].ToString(),
                            CuentaId = Int32.Parse(ds.Tables[0].Rows[i]["cuentaId"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCartolaMovimientosGrilla", 0);
                return lst;
            }
        }

        public static int InsertarCartolaBancoCarga(string archivo, int userId)
        {
            int result = -1;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Cartola_Banco_Excel_Carga");
                sp.AgregarParametro("archivo", archivo);
                sp.AgregarParametro("userId", userId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["IDCARGA"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarCartolaBancoCarga", userId);
                return -1;
            }
            return result;
        }
        public static int InsertarCartolaBancoArchivo(string NumCuenta, string fechaMovimiento, string monto, string descripcion,
                                                        string sucursal, string numComprobante, 
                                                        int idCarga, int userId)
        {
            int result = -1;
            try
            {
                DateTime fecMovimiento = new DateTime();
                fecMovimiento = new DateTime();
                DateTime.TryParse(fechaMovimiento, out fecMovimiento);
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Cartola_Banco_Archivo");
                sp.AgregarParametro("numCuenta", NumCuenta);
                sp.AgregarParametro("fecMovimiento", fecMovimiento);
                sp.AgregarParametro("monto", string.IsNullOrEmpty(monto) ? 0 : (object)Decimal.Parse(monto));
                sp.AgregarParametro("descripcion", descripcion);
                sp.AgregarParametro("sucursal", sucursal);
                sp.AgregarParametro("numComprobante", numComprobante);
                sp.AgregarParametro("idCarga", idCarga);
                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.CAja.dao.Tesoreria.InsertarCartolaBancoArchivo", userId);
                return -1;
            }
            return result;
        }

        public static List<dto.DatosCargaCartola> ListarCartolaBancoArchivo(int idCarga)
        {
            List<dto.DatosCargaCartola> lst = new List<dto.DatosCargaCartola>();
            try
            {
                DateTime fechaMovimiento = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Cartola_Banco_Archivo");
                sp.AgregarParametro("idCarga", idCarga);
              
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaMovimiento = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecMovimiento"].ToString(), out fechaMovimiento);
                        lst.Add(new dto.DatosCargaCartola()
                        {
                            Id = Int32.Parse(ds.Tables[0].Rows[i]["id"].ToString()),
                            FecMovimiento = fechaMovimiento,
                            NumCuenta = ds.Tables[0].Rows[i]["NumCuenta"].ToString(),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Motivo = ds.Tables[0].Rows[i]["Motivo"].ToString(),
                            Sucursal = ds.Tables[0].Rows[i]["SUCURSAL"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.CAja.dao.Tesoreria.ListarCartolaBancoArchivo", 0);
                return lst;
            }
        }
        public static List<Combobox> ListarTipoMovimientoBanco()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Movimiento_Banco");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            Value = ds.Tables[0].Rows[i]["TIPO_MOVIMIENTO_BANCO_ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarTipoMovimientoBanco", 0);
                return lst;
            }
        }
        public static List<Combobox> ListarMotivoBanco()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Motivo_Banco");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            Value = ds.Tables[0].Rows[i]["TIPO_MOTIVO_BANCO_ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarMotivoBanco", 0);
                return lst;
            }
        }
        public static List<Combobox> ListarEstadoBanco()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Estado_Banco");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            Value = ds.Tables[0].Rows[i]["TIPO_ESTADO_BANCO_ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarEstadoBanco", 0);
                return lst;
            }
        }

        public static List<Combobox> ListarTipoConciliacionMovimiento()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Conciliacion_Movimiento");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            Value = ds.Tables[0].Rows[i]["CONCILIACION_TIPO_ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarTipoConciliacionMovimiento", 0);
                return lst;
            }
        }

        public static int InsertarCartolaMovimiento(int codemp, string NumCuenta, string fechaMovimiento, string monto, 
                                                        string sucursal, string numComprobante,
                                                        int tipoMovimientoId, int tipoMotivoId, int tipoEstadoId, int archivoRowId, int userId)
        {
            int result = -1;
            try
            {
                DateTime fecMovimiento = new DateTime();
                fecMovimiento = new DateTime();
                DateTime.TryParse(fechaMovimiento, out fecMovimiento);
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Cartola_Movimiento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", NumCuenta);
                sp.AgregarParametro("fecMovimiento", fecMovimiento);
                sp.AgregarParametro("monto", string.IsNullOrEmpty(monto) ? 0 : (object)Decimal.Parse(monto));
                sp.AgregarParametro("sucursal", sucursal);
                sp.AgregarParametro("numComprobante", numComprobante);
                sp.AgregarParametro("tipoMovimientoId", tipoMovimientoId);
                sp.AgregarParametro("tipoMotivoId", tipoMotivoId);
                sp.AgregarParametro("tipoEstadoId", tipoEstadoId);
                sp.AgregarParametro("archivoRowId", archivoRowId);
                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarCartolaMovimiento", userId);
                return -1;
            }
            return result;
        }

        public static List<Autocomplete> ListarNombreGestor(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Nombre_Gestor");
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarNombreGestor", 0);
            }
            return lst;
        }

        public static int ActualizarEstadoMovimientoCartola(int codemp, int movimientoId, int cuentaId, int tipoEstadoId, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Actualizar_Cartola_Movimiento_TipoEstado");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("movimientoId", movimientoId);
                sp.AgregarParametro("cuentaId", cuentaId);
                sp.AgregarParametro("estadoId", tipoEstadoId);
                sp.AgregarParametro("userId", user);
                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ActualizarEstadoMovimientoCartola", 0);

                return id;
            }
            return id;
        }
        public static int ExistConciliacioncomprobante(string numComprobante)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Exist_Conciliacion_NumComprobante");
                sp.AgregarParametro("numComprobante", string.IsNullOrEmpty(numComprobante) ? 0 : (object)Int32.Parse(numComprobante));

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["countComprobante"].ToString());
                    }


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ExistConciliacioncomprobante", 0);

                return id;
            }
            return id;
        }
        public static int InsertarConciliacionMovimiento(int codemp, int movimientoId, string numComprobante, string custodiaId, 
                                                        int pclid, int ctcid, int gestorId, int tipoConciliacion, string numCuenta, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Conciliacion_Movimiento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("movimientoId", movimientoId);
                sp.AgregarParametro("numComprobante", string.IsNullOrEmpty(numComprobante)? DBNull.Value: (object)Int32.Parse(numComprobante));
                sp.AgregarParametro("custodiaId", string.IsNullOrEmpty(custodiaId) ? DBNull.Value : (object)Int32.Parse(custodiaId));
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("gestorId", gestorId);
                sp.AgregarParametro("tipoConciliacionId", tipoConciliacion);
                sp.AgregarParametro("num_cuenta", numCuenta);
                sp.AgregarParametro("userId", user);
                
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["ConciliacionId"].ToString());
                    }
              
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarConciliacionMovimiento", user);

                return id;
            }
            return id;
        }

        public static int InsertarConciliacionMovimientoArchivo(int conciliacionId, int movimientoId,  
                                                        int pclid, int ctcid, string rutaArchivo, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Insertar_Conciliacion_Archivo_Comprobante");
                sp.AgregarParametro("conciliacionId", conciliacionId);
                sp.AgregarParametro("movimientoId", movimientoId);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("rutaArchivo", rutaArchivo);
                sp.AgregarParametro("userId", user);

                id = sp.EjecutarProcedimientoTrans();
               

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarConciliacionMovimientoArchivo", user);

                return id;
            }
            return id;
        }

        public static int ProcesarCartolaBancoRowId(int archivoRowId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Procesar_Cartola_Banco_ArchivoRowId");
                
                sp.AgregarParametro("archivoRowId", archivoRowId);
               
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ProcesarCartolaBancoRowId", 0);
                return -1;
            }
            return result;
        }

        public static List<dto.CartolaMovimientoExcel> ListarCartolaMovimientosExcel(int codemp, string numCuenta)
        {
            List<dto.CartolaMovimientoExcel> lst = new List<dto.CartolaMovimientoExcel>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Cartola_Movimientos_Excel");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", numCuenta);
                

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.CartolaMovimientoExcel()
                        {
                            NumCuenta = ds.Tables[0].Rows[i]["NumCuenta"].ToString(),
                            FecMovimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FecMovimiento"].ToString()),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Motivo = ds.Tables[0].Rows[i]["Motivo"].ToString(),
                            Sucursal = ds.Tables[0].Rows[i]["Sucursal"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            Movimiento = ds.Tables[0].Rows[i]["Movimiento"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            Observacion = ds.Tables[0].Rows[i]["OBSERVACION"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCartolaMovimientosGrilla", 0);
                return lst;
            }
        }

        public static List<Combobox> ListarGestorConciliacion(int codemp, int pclid, int ctcid)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Gestor_Conciliacion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["GES_NOMBRE"].ToString(),
                            Value = ds.Tables[0].Rows[i]["GES_GESID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarGestorConciliacion", 0);
                return lst;
            }
        }

        public static List<Combobox> ListarTipoCuentaTesoreria()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Tesoreria_Cuenta");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            Value = ds.Tables[0].Rows[i]["TIPO_CUENTA_ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarTipoConciliacionMovimiento", 0);
                return lst;
            }
        }

        public static int ExistCuentaBancaria(string numCuenta)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Exist_NumCuenta");
                sp.AgregarParametro("numCuenta", numCuenta);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["countCuenta"].ToString());
                    }


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ExistCuentaBancaria", 0);

                return id;
            }
            return id;
        }

        public static int InsertarCuentaBancaria(int codemp, string numCuenta, int bancoId, int tipoCuentaId,int user)
        {
            int id = -1;

            try
            {
                DataSet ds = new DataSet();
                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Insertar_Cuenta_Bancaria_Tesoreria");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", numCuenta);
                sp.AgregarParametro("bancoId", bancoId);
                sp.AgregarParametro("tipoCuentaId", tipoCuentaId);
                sp.AgregarParametro("userId", user);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["cuentaId"].ToString());
                    }
               

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarCuentaBancaria", user);

                return id;
            }
            return id;
        }
        public static List<Combobox> ListarBancos(int codemp)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Bancos");
                sp.AgregarParametro("codemp", codemp);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarBancos", 0);

            }
            return lst;
        }

        public static List<dto.DocumentoCustodia> ListarDocumentosCustodiaGrilla(int codemp, string numCuenta, string where, string sidx, string sord)
        {
            List<dto.DocumentoCustodia> lst = new List<dto.DocumentoCustodia>();
            DateTime fechaDoc = new DateTime();
            DateTime fechaProrroga = new DateTime();
            decimal montoDoc = new decimal();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Custodia_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", numCuenta);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaDoc = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecDoc"].ToString(), out fechaDoc);
                        fechaProrroga = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecProrroga"].ToString(), out fechaProrroga);
                        decimal.TryParse(ds.Tables[0].Rows[i]["Monto"].ToString(), out montoDoc);
                        lst.Add(new dto.DocumentoCustodia()
                        {
                            CustodiaId = Int32.Parse(ds.Tables[0].Rows[i]["CustodiaId"].ToString()),
                            FecDoc = fechaDoc == new DateTime() ? (DateTime?)null : fechaDoc,
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Monto = montoDoc == new decimal() ? (decimal)0 : montoDoc,
                            Gestor = ds.Tables[0].Rows[i]["Gestor"].ToString(),
                            GiradoA = ds.Tables[0].Rows[i]["GiradoA"].ToString(),
                            TipoBanco = ds.Tables[0].Rows[i]["TipoBanco"].ToString(),
                            NumDocumento = ds.Tables[0].Rows[i]["NumDocumento"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FecProrroga = fechaProrroga == new DateTime() ? (DateTime?) null : fechaProrroga,
                            
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            GestorId = ds.Tables[0].Rows[i]["GestorId"].ToString(),

                            BancoId = ds.Tables[0].Rows[i]["BancoId"].ToString(),
                            EstadoId = ds.Tables[0].Rows[i]["EstadoId"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarDocumentosCustodiaGrilla", 0);
                return lst;
            }
        }
        public static int InsertarDocumentoCustodia(int codemp, string NumCuenta, int pclid, int ctcid, int gestorId, string recibe, string bancoId,
                                                    string numDocumento, string fechaDocumento, string monto, int userId)
        {
            int result = -1;
            try
            {
                DateTime fecDocumento = new DateTime();
                fecDocumento = new DateTime();
                DateTime.TryParse(fechaDocumento, out fecDocumento);
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Documento_Custodia");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", NumCuenta);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("gestorId", gestorId);
                sp.AgregarParametro("numDocumento", numDocumento);
                sp.AgregarParametro("recibe", recibe);
                sp.AgregarParametro("bancoId", Int32.Parse(bancoId));
                sp.AgregarParametro("fecDoc", fecDocumento);
                sp.AgregarParametro("monto", string.IsNullOrEmpty(monto) ? 0 : (object)Decimal.Parse(monto));

                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarDocumentoCustodia", userId);
                return -1;
            }
            return result;
        }

        public static List<dto.CartolaMovimiento> ListarCartolaMovimientosPendienteGrilla(int codemp, string numCuenta, string fechaDocumento, string montoDocumento, string where, string sidx, string sord)
        {
            List<dto.CartolaMovimiento> lst = new List<dto.CartolaMovimiento>();

            try
            {
                DateTime fecDocumento = new DateTime();
                fecDocumento = new DateTime();
                DateTime.TryParse(fechaDocumento, out fecDocumento);
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Cartola_Movimientos_Pendiente_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", numCuenta);
                sp.AgregarParametro("montoBuscar", string.IsNullOrEmpty(montoDocumento) ? 0 : (object)Decimal.Parse(montoDocumento));
                sp.AgregarParametro("fechaBuscar", fechaDocumento);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.CartolaMovimiento()
                        {
                            MovimientoId = Int32.Parse(ds.Tables[0].Rows[i]["MovimientoId"].ToString()),
                            NumCuenta = ds.Tables[0].Rows[i]["NumCuenta"].ToString(),
                            FecMovimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FecMovimiento"].ToString()),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Sucursal = ds.Tables[0].Rows[i]["Sucursal"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            Movimiento = ds.Tables[0].Rows[i]["Movimiento"].ToString(),
                            Motivo = ds.Tables[0].Rows[i]["Motivo"].ToString(),
                            MotivoSistema = ds.Tables[0].Rows[i]["MotivoSistema"].ToString(),
                            MotivoSistemaId = ds.Tables[0].Rows[i]["MotivoSistemaId"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoId = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCartolaMovimientosGrilla", 0);
                return lst;
            }
        }

        public static int ActualizarEstadoDocumentoCustodia(int codemp, int custodiaId, int tipoEstadoId, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Actualizar_Documento_Custodia_TipoEstado");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("custodiaId", custodiaId);
                sp.AgregarParametro("estadoId", tipoEstadoId);
                sp.AgregarParametro("userId", user);
                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ActualizarEstadoDocumentoCustodia", 0);

                return id;
            }
            return id;
        }

        public static List<dto.CartolaMovimiento> ListarMovimientosProtestadosGrilla(int codemp, string numCuenta, string where, string sidx, string sord)
        {
            List<dto.CartolaMovimiento> lst = new List<dto.CartolaMovimiento>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Movimientos_Protestados_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", numCuenta);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.CartolaMovimiento()
                        {
                            MovimientoId = Int32.Parse(ds.Tables[0].Rows[i]["MovimientoId"].ToString()),
                            NumCuenta = ds.Tables[0].Rows[i]["NumCuenta"].ToString(),
                            FecMovimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FecMovimiento"].ToString()),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Sucursal = ds.Tables[0].Rows[i]["Sucursal"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            Movimiento = ds.Tables[0].Rows[i]["Movimiento"].ToString(),
                            Motivo = ds.Tables[0].Rows[i]["Motivo"].ToString(),
                            MotivoSistema = ds.Tables[0].Rows[i]["MotivoSistema"].ToString(),
                            MotivoSistemaId = ds.Tables[0].Rows[i]["MotivoSistemaId"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoId = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCartolaMovimientosGrilla", 0);
                return lst;
            }
        }
        public static List<dto.MovimientoConciliado> ListarMovimientosConciliadoGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.MovimientoConciliado> lst = new List<dto.MovimientoConciliado>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Movimientos_Conciliados_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.MovimientoConciliado()
                        {
                            ConciliacionId = Int32.Parse(ds.Tables[0].Rows[i]["ConciliacionId"].ToString()),
                            MovimientoId = Int32.Parse(ds.Tables[0].Rows[i]["MovimientoId"].ToString()),
                            CustodiaId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["CustodiaId"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["CustodiaId"].ToString()),
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            GestorId = ds.Tables[0].Rows[i]["GestorId"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            MotivoSistema = ds.Tables[0].Rows[i]["MotivoSistema"].ToString(),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = Decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Tipoconciliacion = ds.Tables[0].Rows[i]["Tipoconciliacion"].ToString(),
                            EstadoLiquidacion = ds.Tables[0].Rows[i]["EstadoLiquidacion"].ToString(),
                            FechaConciliacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaConciliacion"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarMovimientosConciliadoGrilla", 0);
                return lst;
            }
        }

        public static dto.FormLiquidacion ListarFormLiquidacion(int codemp, string conciliacionId, string pclid, string ctcid)
        {
            dto.FormLiquidacion obj = new dto.FormLiquidacion();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("ObtenerDatosFormularioLiquidacion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", Int32.Parse(conciliacionId));
                sp.AgregarParametro("pclid", Int32.Parse(pclid));
                sp.AgregarParametro("ctcid", Int32.Parse(ctcid));

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString();
                        obj.RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString();
                        obj.Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString();
                        obj.RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString();
                        obj.Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString();
                        obj.Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString());
                        obj.Capital = Decimal.Parse(ds.Tables[0].Rows[i]["Capital"].ToString());
                        obj.Honorario = Decimal.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString());
                        obj.Interes = Decimal.Parse(ds.Tables[0].Rows[i]["Interes"].ToString());
                        obj.CapitalPor = Int32.Parse(ds.Tables[0].Rows[i]["CapitalPor"].ToString());
                        obj.InteresPor = Int32.Parse(ds.Tables[0].Rows[i]["InteresPor"].ToString());
                        obj.HonorarioPor = Int32.Parse(ds.Tables[0].Rows[i]["HonorarioPor"].ToString());
                        obj.GastoPre = Decimal.Parse(ds.Tables[0].Rows[i]["GastoPre"].ToString());
                        obj.GastoJud = Decimal.Parse(ds.Tables[0].Rows[i]["GastoJud"].ToString());
                        obj.MontoRebajado = Decimal.Parse(ds.Tables[0].Rows[i]["MontoRebajado"].ToString());
                        obj.EstadoLiquidacionId = ds.Tables[0].Rows[i]["EstadoLiquidacionId"].ToString();
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarMovimientosConciliadoGrilla", 0);
                return obj;
            }
        }

        public static List<dto.DocumentoDeudor> ListarLiquidacionDocumentosDeudorGrilla(int codemp, int pclid, int ctcid, string where, string sidx, string sord)
        {
            List<dto.DocumentoDeudor> lst = new List<dto.DocumentoDeudor>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Liquidacion_Documentos_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.DocumentoDeudor()
                        {
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            Ccbid = ds.Tables[0].Rows[i]["Ccbid"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = Decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Intereses = Decimal.Parse(ds.Tables[0].Rows[i]["Intereses"].ToString()),
                            Honorarios = Decimal.Parse(ds.Tables[0].Rows[i]["Honorarios"].ToString()),
                            GastoJudicial = Decimal.Parse(ds.Tables[0].Rows[i]["GastoJudicial"].ToString()),
                            GastoPrejudicial = Decimal.Parse(ds.Tables[0].Rows[i]["GastoPrejudicial"].ToString()),
                            TotalDeuda = Decimal.Parse(ds.Tables[0].Rows[i]["TotalDeuda"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarLiquidacionDocumentosDeudorGrilla", 0);
                return lst;
            }
        }

        public static List<dto.DocumentoImputado> ListarImputacionDocumentosDeudorGrilla(int codemp, int pclid, int ctcid, string docs, string docsFinalizar,
                                                                                         string montoHonorario, string montoInteres, string montoCapital,
                                                                                         string montoGastoPre, string montoGastoJud)
        {
            List<dto.DocumentoImputado> lst = new List<dto.DocumentoImputado>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Imputacion_Documentos_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("docs", docs);
                sp.AgregarParametro("docsFinalizar", string.IsNullOrEmpty(docsFinalizar) ? "0" : (object)docsFinalizar);
                sp.AgregarParametro("montoHonorario",  string.IsNullOrEmpty(montoHonorario) ? 0 : (object)Decimal.Parse(montoHonorario));
                sp.AgregarParametro("montoInteres", string.IsNullOrEmpty(montoInteres) ? 0 : (object)Decimal.Parse(montoInteres));
                sp.AgregarParametro("montoCapital", string.IsNullOrEmpty(montoCapital) ? 0 : (object)Decimal.Parse(montoCapital));
                sp.AgregarParametro("montoGastoPre", string.IsNullOrEmpty(montoGastoPre) ? 0 : (object)Decimal.Parse(montoGastoPre));
                sp.AgregarParametro("montoGastoJud", string.IsNullOrEmpty(montoGastoJud) ? 0 : (object)Decimal.Parse(montoGastoJud));

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.DocumentoImputado()
                        {
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            Ccbid = ds.Tables[0].Rows[i]["Ccbid"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = Decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            CapitalDebitado = ds.Tables[0].Rows[i]["CapitalDebitado"].ToString(),
                            Intereses = Decimal.Parse(ds.Tables[0].Rows[i]["Intereses"].ToString()),
                            InteresDebitado = ds.Tables[0].Rows[i]["InteresDebitado"].ToString(),
                            Honorarios = Decimal.Parse(ds.Tables[0].Rows[i]["Honorarios"].ToString()),
                            HonorarioDebitado = ds.Tables[0].Rows[i]["HonorarioDebitado"].ToString(),
                            GastoJudicial = Decimal.Parse(ds.Tables[0].Rows[i]["GastoJudicial"].ToString()),
                            PagoJudDebitado = ds.Tables[0].Rows[i]["PagoJudDebitado"].ToString(),
                            GastoPrejudicial = Decimal.Parse(ds.Tables[0].Rows[i]["GastoPrejudicial"].ToString()),
                            PagoPreDebitado = ds.Tables[0].Rows[i]["PagoPreDebitado"].ToString(),
                            TotalDeuda = Decimal.Parse(ds.Tables[0].Rows[i]["TotalDeuda"].ToString()),
                            IndicaImputado = ds.Tables[0].Rows[i]["IndicaImputado"].ToString()

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarLiquidacionDocumentosDeudorGrilla", 0);
                return lst;
            }
        }

        public static int InsertarDocumentoImputado(int codemp, int conciliacionId,
                                                    string Ccbid, string estado, string CapitalDebitado, string InteresDebitado, 
                                                    string HonorarioDebitado, string PagoJudDebitado, 
                                                    string PagoPreDebitado, int userId)
        {
            int result = -1;
            try
            {
               
                StoredProcedure sp = new StoredProcedure("_Insertar_Conciliacion_Documento_Imputado");
                sp.AgregarParametro("codemp", codemp);
               
                sp.AgregarParametro("conciliacionId", conciliacionId);
                sp.AgregarParametro("ccbid", Int32.Parse(Ccbid));
                sp.AgregarParametro("estado", estado.Substring(0, 1));
                sp.AgregarParametro("montoCapital", Decimal.Parse(CapitalDebitado));
                sp.AgregarParametro("montoInteres", Decimal.Parse(InteresDebitado));
                sp.AgregarParametro("montoHonorario", Decimal.Parse(HonorarioDebitado));
                sp.AgregarParametro("montoGastoPre", Decimal.Parse(PagoPreDebitado));
                sp.AgregarParametro("montoGastoJud", Decimal.Parse(PagoJudDebitado));
                
                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarDocumentoImputado", userId);
                return -1;
            }
            return result;
        }

        public static int RebajarDocumentoImputado(int codemp, int pclid, int ctcid,
                                                    string Ccbid, string CapitalDebitado, string InteresDebitado,
                                                    string HonorarioDebitado, string PagoJudDebitado,
                                                    string PagoPreDebitado, int userId)
        {
            int result = -1;
            try
            {

                StoredProcedure sp = new StoredProcedure("_Rebajar_Conciliacion_Documento_Cartera");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", Int32.Parse(Ccbid));
                sp.AgregarParametro("montoCapital", Decimal.Parse(CapitalDebitado));
                sp.AgregarParametro("montoInteres", Decimal.Parse(InteresDebitado));
                sp.AgregarParametro("montoHonorario", Decimal.Parse(HonorarioDebitado));
                sp.AgregarParametro("montoGastoPre", Decimal.Parse(PagoPreDebitado));
                sp.AgregarParametro("montoGastoJud", Decimal.Parse(PagoJudDebitado));

                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarDocumentoImputado", userId);
                return -1;
            }
            return result;
        }
        public static string GrabarAccion(int codemp, int pclid, int ctcid,
                                                    string Ccbid)
        {
            string result = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Caja_Accion_Documento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", Int32.Parse(Ccbid));

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = ds.Tables[0].Rows[0]["numero"].ToString();
                    }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.GrabarAccion", 0);
                return "";
            }
            return result;
        }
        public static int AprobarConciliacionMovimiento(int codemp, int conciliacionId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Aprobar_Conciliacion_Movimiento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", conciliacionId);
                
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["proceso"].ToString());
                    }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.AprobarConciliacionMovimiento", 0);
                return -1;
            }
            return result;
        }
        public static int FinalizarDocumento(int codemp, int pclid, int ctcid, int ccbid, string estcpbt, string comentario)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Tesoreria_Finalizar_Documento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("estcpbt", "V");
                sp.AgregarParametro("nuevo_estcpbt", "F");
                sp.AgregarParametro("comentario", comentario);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.FinalizarDocumento", 0);
                throw ex;
            }
        }

        public static int InsertarPagoManual(int codemp, int pclid, int ctcid, string fechaMovimiento, string monto, int tipoConciliacion, int user)
        {
            int id = -1;

            try
            {
                DateTime fecMovimiento = new DateTime();
                fecMovimiento = new DateTime();
                DateTime.TryParse(fechaMovimiento, out fecMovimiento);
            
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Pago_Manual");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("fecMovimiento", fecMovimiento);
                sp.AgregarParametro("monto", string.IsNullOrEmpty(monto) ? 0 : (object)Decimal.Parse(monto));
                sp.AgregarParametro("tipoConciliacionId", tipoConciliacion);
                sp.AgregarParametro("userId", user);
                
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["ConciliacionId"].ToString());
                    }
              
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarPagoManual", user);

                return id;
            }

            return id;
          
         }

        public static List<dto.MovimientoConciliadoAprobado> ListarMovimientosConciliadoAprobadoGrilla(int codemp, string pclid, string where, string sidx, string sord)
        {
            List<dto.MovimientoConciliadoAprobado> lst = new List<dto.MovimientoConciliadoAprobado>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Movimientos_Conciliados_Aprobados_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", string.IsNullOrEmpty(pclid) ? DBNull.Value : (object)Int32.Parse(pclid));
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.MovimientoConciliadoAprobado()
                        {
                            ConciliacionId = Int32.Parse(ds.Tables[0].Rows[i]["ConciliacionId"].ToString()),
                            MovimientoId = Int32.Parse(ds.Tables[0].Rows[i]["MovimientoId"].ToString()),
                            CustodiaId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["CustodiaId"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["CustodiaId"].ToString()),
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            GestorId = ds.Tables[0].Rows[i]["GestorId"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Capital = Decimal.Parse(ds.Tables[0].Rows[i]["Capital"].ToString()),
                            Interes = Decimal.Parse(ds.Tables[0].Rows[i]["Interes"].ToString()),
                            Honorarios = Decimal.Parse(ds.Tables[0].Rows[i]["Honorarios"].ToString()),
                            OtrosGastos = Decimal.Parse(ds.Tables[0].Rows[i]["OtrosGastos"].ToString()),
                            MontoRecuperado = Decimal.Parse(ds.Tables[0].Rows[i]["MontoRecuperado"].ToString()),
                            FechaConciliacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaConciliacion"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarMovimientosConciliadoGrilla", 0);
                return lst;
            }
        }

        public static List<dto.ComprobanteRemesa> ListarRemesaGananciaGrilla(string idsDocs)
        {
            List<dto.ComprobanteRemesa> lst = new List<dto.ComprobanteRemesa>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Calculo_Remesa");
                sp.AgregarParametro("idsConciliacion", idsDocs);
               
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.ComprobanteRemesa()
                        {
                            ImputacionId = int.Parse(ds.Tables[0].Rows[i]["ImputacionId"].ToString()),
                            ConciliacionId = int.Parse(ds.Tables[0].Rows[i]["ConciliacionId"].ToString()),
                            Ccbid = ds.Tables[0].Rows[i]["Ccbid"].ToString(),
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            NumDocumento = ds.Tables[0].Rows[i]["NumDocumento"].ToString(),
                            Capital = Decimal.Parse(ds.Tables[0].Rows[i]["Capital"].ToString()),
                            Interes = Decimal.Parse(ds.Tables[0].Rows[i]["Interes"].ToString()),
                            Honorario = Decimal.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString()),
                            RecuperadoGasto = Decimal.Parse(ds.Tables[0].Rows[i]["RecuperadoGasto"].ToString()),
                            TotalRecuperado = Decimal.Parse(ds.Tables[0].Rows[i]["TotalRecuperado"].ToString()),
                            PorCapital = ds.Tables[0].Rows[i]["PorCapital"].ToString(),
                            GananciaCapital = Decimal.Parse(ds.Tables[0].Rows[i]["GananciaCapital"].ToString()),
                            PorInteres = ds.Tables[0].Rows[i]["PorInteres"].ToString(),
                            GananciaInteres = Decimal.Parse(ds.Tables[0].Rows[i]["GananciaInteres"].ToString()),
                            PorHonorario = ds.Tables[0].Rows[i]["PorHonorario"].ToString(),
                            GananciaHonorario = Decimal.Parse(ds.Tables[0].Rows[i]["GananciaHonorario"].ToString()),
                            TotalGanancia = Decimal.Parse(ds.Tables[0].Rows[i]["TotalGanancia"].ToString()),
                            TotalCliente = Decimal.Parse(ds.Tables[0].Rows[i]["TotalCliente"].ToString()),
                            Anticipo = Decimal.Parse(ds.Tables[0].Rows[i]["TotalAnticipo"].ToString()),
                            DocumentoId = int.Parse(ds.Tables[0].Rows[i]["DocumentoId"].ToString()),
                            ConciliacionTipoId = int.Parse(ds.Tables[0].Rows[i]["ConciliacionTipoId"].ToString()),
                            ConciliacionTipo = ds.Tables[0].Rows[i]["ConciliacionTipo"].ToString(),
                            AnticipoDebitado = Decimal.Parse(ds.Tables[0].Rows[i]["AnticipoDebitado"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarRemesaGananciaGrilla", 0);
                return lst;
            }
        }

        public static List<dto.Remesa> ListarRemesasGeneradasGrilla(string where, string sidx, string sord)
        {
            List<dto.Remesa> lst = new List<dto.Remesa>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Remesas_Generadas_Grilla");
               sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.Remesa()
                        {
                            Id = Int32.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            CapitalRecuperado = Decimal.Parse(ds.Tables[0].Rows[i]["CapitalRecuperado"].ToString()),
                            InteresRecuperado = Decimal.Parse(ds.Tables[0].Rows[i]["InteresRecuperado"].ToString()),
                            HonorarioRecuperado = Decimal.Parse(ds.Tables[0].Rows[i]["HonorarioRecuperado"].ToString()),
                            Capital = Decimal.Parse(ds.Tables[0].Rows[i]["Capital"].ToString()),
                            Interes = Decimal.Parse(ds.Tables[0].Rows[i]["Interes"].ToString()),
                            Honorario = Decimal.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString()),
                            TotalFactura = Decimal.Parse(ds.Tables[0].Rows[i]["TotalFactura"].ToString()),
                            TotalDimol = Decimal.Parse(ds.Tables[0].Rows[i]["TotalDimol"].ToString()),
                            FechaRemesa = DateTime.Parse(ds.Tables[0].Rows[i]["FechaRemesa"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarMovimientosConciliadoGrilla", 0);
                return lst;
            }
        }

        public static int InsertarRemesaCabecera(int userId)
        {
            int result = -1;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Remesa_Cabecera");
                sp.AgregarParametro("userId", userId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["IDREMESA"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarRemesaCabecera", userId);
                return -1;
            }
            return result;
        }

        public static int InsertarRemesaDetalle(int remesaId, int imputacionId, int conciliacionId, int codemp,
                                                       string ccbid, string pclid,
                                                       string ctcid, string numcomprobante,
                                                    string capitalrecuperado, string interesrecuperado, string honorariorecuperado, 
                                                    string porcapital, string porinteres, string porhonorario, 
                                                    string capital, string interes, string honorario, int userId)
        {
            int result = -1;
            try
            {
               
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Remesa_Detalle");
                sp.AgregarParametro("remesaId", remesaId);
                sp.AgregarParametro("imputacionId", imputacionId);
                sp.AgregarParametro("conciliacionId", conciliacionId);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ccbid", int.Parse(ccbid));
                sp.AgregarParametro("pclid", int.Parse(pclid));
                sp.AgregarParametro("ctcid", int.Parse(ctcid));
                sp.AgregarParametro("numcomprobante", int.Parse(numcomprobante));
                sp.AgregarParametro("capitalrecuperado",  Decimal.Parse(capitalrecuperado));
                sp.AgregarParametro("interesrecuperado", Decimal.Parse(interesrecuperado));
                sp.AgregarParametro("honorariorecuperado", Decimal.Parse(honorariorecuperado));
                sp.AgregarParametro("porcapital", int.Parse(porcapital));
                sp.AgregarParametro("porinteres", int.Parse(porinteres));
                sp.AgregarParametro("porhonorario", int.Parse(porhonorario));
                sp.AgregarParametro("capital", Decimal.Parse(capital));
                sp.AgregarParametro("interes", Decimal.Parse(interes));
                sp.AgregarParametro("honorario", Decimal.Parse(honorario));
                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.CAja.dao.Tesoreria.InsertarRemesaDetalle", userId);
                return -1;
            }
            return result;
        }

        public static int InsertarRemesaAnticipo(int remesaId, int codemp,
                                                      string pclid, string ctcid, string anticipo,
                                                   string anticipodebitado, string documentoid,int userId)
        {
            int result = -1;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Remesa_Anticipo");
                sp.AgregarParametro("remesaId", remesaId);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", int.Parse(pclid));
                sp.AgregarParametro("ctcid", int.Parse(ctcid));
                sp.AgregarParametro("anticipo", Decimal.Parse(anticipo));
                sp.AgregarParametro("anticipodebitado", Decimal.Parse(anticipodebitado));
                sp.AgregarParametro("documentoid", int.Parse(documentoid));
              
                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.CAja.dao.Tesoreria.InsertarRemesaAnticipo", userId);
                return -1;
            }
            return result;
        }

        public static int ActualizarSaldoDocumentoCaja(int remesaId, string documentoid, int userId)
        {
            int result = -1;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Actualizar_Saldo_DocumentoCaja");
                sp.AgregarParametro("remesaId", remesaId);
                sp.AgregarParametro("documentoid", int.Parse(documentoid));

                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.CAja.dao.Tesoreria.ActualizarSaldoDocumentoCaja", userId);
                return -1;
            }
            return result;
        }

        public static List<dto.EfectivoCustodia> ListarEfectivoCustodiaGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.EfectivoCustodia> lst = new List<dto.EfectivoCustodia>();
            DateTime fechaDoc = new DateTime();
            DateTime fechaProrroga = new DateTime();
            decimal montoDoc = new decimal();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Custodia_Efectivo_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaDoc = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecDoc"].ToString(), out fechaDoc);
                        fechaProrroga = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecProrroga"].ToString(), out fechaProrroga);
                        decimal.TryParse(ds.Tables[0].Rows[i]["Monto"].ToString(), out montoDoc);
                        lst.Add(new dto.EfectivoCustodia()
                        {
                            CustodiaId = Int32.Parse(ds.Tables[0].Rows[i]["CustodiaId"].ToString()),
                            FecDoc = fechaDoc == new DateTime() ? (DateTime?)null : fechaDoc,
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Monto = montoDoc == new decimal() ? (decimal)0 : montoDoc,
                            Gestor = ds.Tables[0].Rows[i]["Gestor"].ToString(),
                            GiradoA = ds.Tables[0].Rows[i]["GiradoA"].ToString(),
                            TipoBanco = ds.Tables[0].Rows[i]["TipoBanco"].ToString(),
                            NumDocumento = ds.Tables[0].Rows[i]["NumDocumento"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FecProrroga = fechaProrroga == new DateTime() ? (DateTime?)null : fechaProrroga,

                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            GestorId = ds.Tables[0].Rows[i]["GestorId"].ToString(),

                            BancoId = ds.Tables[0].Rows[i]["BancoId"].ToString(),
                            EstadoId = ds.Tables[0].Rows[i]["EstadoId"].ToString(),
                            ConciliacionId = ds.Tables[0].Rows[i]["ConciliacionId"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            MotivoSistema = ds.Tables[0].Rows[i]["MotivoSistema"].ToString(),
                            Saldo = Decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Tipoconciliacion = ds.Tables[0].Rows[i]["Tipoconciliacion"].ToString(),
                            EstadoLiquidacion = ds.Tables[0].Rows[i]["EstadoLiquidacion"].ToString(),
                            FechaConciliacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaConciliacion"].ToString()),
                            EstadoConciliacionId = ds.Tables[0].Rows[i]["EstadoConciliacionId"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarDocumentosCustodiaGrilla", 0);
                return lst;
            }
        }
        public static int InsertarEfectivoCustodia(int codemp,int pclid, int ctcid, int gestorId, string recibe, string bancoId,
                                                    string fechaDocumento, string monto, int userId)
        {
            int result = -1;
            try
            {
                DateTime fecDocumento = new DateTime();
                fecDocumento = new DateTime();
                DateTime.TryParse(fechaDocumento, out fecDocumento);
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Efectivo_Custodia");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("gestorId", gestorId);
                sp.AgregarParametro("recibe", recibe);
                sp.AgregarParametro("bancoId", Int32.Parse(bancoId));
                sp.AgregarParametro("fecDoc", fecDocumento);
                sp.AgregarParametro("monto", string.IsNullOrEmpty(monto) ? 0 : (object)Decimal.Parse(monto));

                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.InsertarEfectivoCustodia", userId);
                return -1;
            }
            return result;
        }
        public static dto.FormLiquidacion ListarFormLiquidacionEfectivo(int codemp, string conciliacionId, string pclid, string ctcid)
        {
            dto.FormLiquidacion obj = new dto.FormLiquidacion();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_ObtenerDatosFormularioLiquidacionEfectivo");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", Int32.Parse(conciliacionId));
                sp.AgregarParametro("pclid", Int32.Parse(pclid));
                sp.AgregarParametro("ctcid", Int32.Parse(ctcid));

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString();
                        obj.RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString();
                        obj.Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString();
                        obj.RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString();
                        obj.Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString();
                        obj.Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString());
                        obj.Capital = Decimal.Parse(ds.Tables[0].Rows[i]["Capital"].ToString());
                        obj.Honorario = Decimal.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString());
                        obj.Interes = Decimal.Parse(ds.Tables[0].Rows[i]["Interes"].ToString());
                        obj.CapitalPor = Int32.Parse(ds.Tables[0].Rows[i]["CapitalPor"].ToString());
                        obj.InteresPor = Int32.Parse(ds.Tables[0].Rows[i]["InteresPor"].ToString());
                        obj.HonorarioPor = Int32.Parse(ds.Tables[0].Rows[i]["HonorarioPor"].ToString());
                        obj.GastoPre = Decimal.Parse(ds.Tables[0].Rows[i]["GastoPre"].ToString());
                        obj.GastoJud = Decimal.Parse(ds.Tables[0].Rows[i]["GastoJud"].ToString());
                        obj.MontoRebajado = Decimal.Parse(ds.Tables[0].Rows[i]["MontoRebajado"].ToString());
                        obj.EstadoLiquidacionId = ds.Tables[0].Rows[i]["EstadoLiquidacionId"].ToString();
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarMovimientosConciliadoGrilla", 0);
                return obj;
            }
        }
        public static int AprobarConciliacionSinMovimiento(int codemp, int conciliacionId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Aprobar_Conciliacion_SinMovimiento_Efectivo");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", conciliacionId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["proceso"].ToString());
                    }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.AprobarConciliacionMovimiento", 0);
                return -1;
            }
            return result;
        }

        public static List<dto.DocumentoCustodiaProtestado> ListarDocumentoCustodiaProtestadosGrilla(int codemp, string numCuenta, string where, string sidx, string sord)
        {
            List<dto.DocumentoCustodiaProtestado> lst = new List<dto.DocumentoCustodiaProtestado>();
            DateTime fechaDoc = new DateTime();
            DateTime fechaProrroga = new DateTime();
            decimal montoDoc = new decimal();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_DocumentosCustodia_Protestados_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", numCuenta);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaDoc = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecDoc"].ToString(), out fechaDoc);
                        fechaProrroga = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FecProrroga"].ToString(), out fechaProrroga);
                        decimal.TryParse(ds.Tables[0].Rows[i]["Monto"].ToString(), out montoDoc);
                        lst.Add(new dto.DocumentoCustodiaProtestado()
                        {
                            CustodiaId = Int32.Parse(ds.Tables[0].Rows[i]["CustodiaId"].ToString()),
                            FecDoc = fechaDoc == new DateTime() ? (DateTime?)null : fechaDoc,
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Monto = montoDoc == new decimal() ? (decimal)0 : montoDoc,
                            Gestor = ds.Tables[0].Rows[i]["Gestor"].ToString(),
                            GiradoA = ds.Tables[0].Rows[i]["GiradoA"].ToString(),
                            TipoBanco = ds.Tables[0].Rows[i]["TipoBanco"].ToString(),
                            NumDocumento = ds.Tables[0].Rows[i]["NumDocumento"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FecProrroga = fechaProrroga == new DateTime() ? (DateTime?)null : fechaProrroga,

                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            GestorId = ds.Tables[0].Rows[i]["GestorId"].ToString(),

                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarDocumentoCustodiaProtestadosGrilla", 0);
                return lst;
            }
        }

        public static List<dto.CartolaMovimiento> ListarCartolaMovimientosLiberadosGrilla(int codemp, string numCuenta, string fechaDocumento, string montoDocumento, string where, string sidx, string sord)
        {
            List<dto.CartolaMovimiento> lst = new List<dto.CartolaMovimiento>();

            try
            {
                DateTime fecDocumento = new DateTime();
                fecDocumento = new DateTime();
                DateTime.TryParse(fechaDocumento, out fecDocumento);
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Cartola_Movimientos_Liberados_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("numCuenta", string.IsNullOrEmpty(numCuenta) ? DBNull.Value : (object)numCuenta);
                sp.AgregarParametro("montoBuscar", string.IsNullOrEmpty(montoDocumento) ? 0 : (object)Decimal.Parse(montoDocumento));
                sp.AgregarParametro("fechaBuscar", fechaDocumento);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.CartolaMovimiento()
                        {
                            MovimientoId = Int32.Parse(ds.Tables[0].Rows[i]["MovimientoId"].ToString()),
                            NumCuenta = ds.Tables[0].Rows[i]["NumCuenta"].ToString(),
                            FecMovimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FecMovimiento"].ToString()),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Sucursal = ds.Tables[0].Rows[i]["Sucursal"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            Movimiento = ds.Tables[0].Rows[i]["Movimiento"].ToString(),
                            Motivo = ds.Tables[0].Rows[i]["Motivo"].ToString(),
                            MotivoSistema = ds.Tables[0].Rows[i]["MotivoSistema"].ToString(),
                            MotivoSistemaId = ds.Tables[0].Rows[i]["MotivoSistemaId"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoId = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarCartolaMovimientosGrilla", 0);
                return lst;
            }
        }
        public static int ActualizarMovimientoConciliacionCustodia(int codemp, int custodiaId, int movimientoId, int conciliacionId, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Actualizar_Efectivo_Custodia_Movimiento_Conciliacion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("movimientoId", movimientoId);
                sp.AgregarParametro("custodiaId", custodiaId);
                sp.AgregarParametro("conciliacionId", conciliacionId);
                sp.AgregarParametro("userId", user);
                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ActualizarEstadoDocumentoCustodia", 0);

                return id;
            }
            return id;
        }

        public static List<Combobox> ListarCuentas(int codemp)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Cuentas_Tesoreria");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    lst.Add(new Combobox()
                    {
                        Text = string.Empty,
                        Value = string.Empty
                    });
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarCuentas", 0);

            }
            return lst;
        }
        public static int ActualizarObservacionMovimientoCartola(int codemp, int movimientoId, int cuentaId, int tipoEstadoId, string observacion, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Actualizar_Cartola_Movimiento_Observacion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("movimientoId", movimientoId);
                sp.AgregarParametro("cuentaId", cuentaId);
                sp.AgregarParametro("estadoId", tipoEstadoId);
                sp.AgregarParametro("observacion", observacion);
                sp.AgregarParametro("userId", user);
                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ActualizarEstadoMovimientoCartola", 0);

                return id;
            }
            return id;
        }

        public static List<dto.MovimientoConciliado> ListarMovimientosConciliadoAprobadosGrilla(int codemp, DateTime? fechaConciliacion, string pclid, string ctcid, string numcomprobante, string where, string sidx, string sord)
        {
            List<dto.MovimientoConciliado> lst = new List<dto.MovimientoConciliado>();

            try
            {
                DateTime fecConciliacion = new DateTime();
                DateTime.TryParse(fechaConciliacion.ToString(), out fecConciliacion);
             
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Movimientos_Conciliados_Apr_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("fechaConciliacion", fecConciliacion == new DateTime() ? DBNull.Value : (object)fecConciliacion.ToString());
                sp.AgregarParametro("pclid", string.IsNullOrEmpty(pclid) ? DBNull.Value : (object)int.Parse(pclid));
                sp.AgregarParametro("ctcid", string.IsNullOrEmpty(ctcid) ? DBNull.Value : (object)int.Parse(ctcid));
                sp.AgregarParametro("numcomprobante", string.IsNullOrEmpty(numcomprobante) ? DBNull.Value : (object)int.Parse(numcomprobante));
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.MovimientoConciliado()
                        {
                            ConciliacionId = Int32.Parse(ds.Tables[0].Rows[i]["ConciliacionId"].ToString()),
                            MovimientoId = Int32.Parse(ds.Tables[0].Rows[i]["MovimientoId"].ToString()),
                            CustodiaId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["CustodiaId"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["CustodiaId"].ToString()),
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            GestorId = ds.Tables[0].Rows[i]["GestorId"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            MotivoSistema = ds.Tables[0].Rows[i]["MotivoSistema"].ToString(),
                            Monto = Decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = Decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Tipoconciliacion = ds.Tables[0].Rows[i]["Tipoconciliacion"].ToString(),
                            EstadoLiquidacion = ds.Tables[0].Rows[i]["EstadoLiquidacion"].ToString(),
                            FechaConciliacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaConciliacion"].ToString()),
                            Remesa = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["Remesa"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["Remesa"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ListarMovimientosConciliadoGrilla", 0);
                return lst;
            }
        }

        public static int ObtConciliacionNumComprobante()
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Obt_Ult_Conciliacion_NumComprobante");
               
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = int.Parse(ds.Tables[0].Rows[0]["NumComprobante"].ToString());
                    }


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ObtConciliacionNumComprobante", 0);

                return id;
            }
            return id;
        }

        public static int ReversarImputacionDocumento(int codemp, int conciliacionId, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Reversar_Imputacion_Documento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", conciliacionId);
                sp.AgregarParametro("userId", user);
                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ReversarImputacionDocumento", user);

                return id;
            }
            return id;
        }
        public static int ReversarImputacionDocumentoEstado(int codemp, int conciliacionId, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Reversar_Imputacion_Documento_Estado");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", conciliacionId);
                sp.AgregarParametro("userId", user);
                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ReversarImputacionDocumentoEstado", user);

                return id;
            }
            return id;
        }
        public static int ReversarConciliacionDocumentoImputado(int codemp, int conciliacionId, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Reversar_Conciliacion_Documento_Imputado");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("conciliacionId", conciliacionId);
                sp.AgregarParametro("userId", user);
                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ReversarConciliacionDocumentoImputado", user);

                return id;
            }
            return id;
        }

        public static List<Combobox> ListarTipoCondicionFacturacion()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Condicion_Facturacion");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            Value = ds.Tables[0].Rows[i]["CONDICION_ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarTipoCondicionFacturaciono", 0);
                return lst;
            }
        }
        public static List<Combobox> ListarTipoCondicionFacturacionRemesa()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_RemesaTipo_Condicion_Facturacion");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            Value = ds.Tables[0].Rows[i]["CONDICION_ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarTipoCondicionFacturacionRemesa", 0);
                return lst;
            }
        }

        public static List<Combobox> ListarCajaTipoCambioTodos()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Todos_Caja_Criterio_Simbolo");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            Value = ds.Tables[0].Rows[i]["SIMBOLO_ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarCajaTipoCambioTodos", 0);
                return lst;
            }
        }

        public static List<Combobox> ListarCajaTipoCambioRemesa()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Remesa_Caja_Criterio_Simbolo");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            Value = ds.Tables[0].Rows[i]["SIMBOLO_ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarCajaTipoCambioRemesa", 0);
                return lst;
            }
        }

        public static List<Combobox> ListarTipoRegionRemesa()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                lst.Add(new Dimol.dto.Combobox()
                {
                    Text = "Otra Región",
                    Value = ""
                });
                lst.Add(new Dimol.dto.Combobox()
                {
                    Text = "Región Metropolitana",
                    Value = "6"
                });
               
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.Tesoreria.ListarCajaCriterioSimboloRemesa", 0);
                return lst;
            }
        }

        public static List<dto.Pago> ConsultaDePagos(int codemp, DateTime? fechaCancelacion, string pclid, string ctcid, string numcomprobante, string where, string sidx, string sord)
        {
            List<dto.Pago> lst = new List<dto.Pago>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tesoreria_Pagos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("fechaCancelacion", fechaCancelacion == new DateTime() ? DBNull.Value : (object)fechaCancelacion.ToString());
                sp.AgregarParametro("pclid", string.IsNullOrEmpty(pclid) ? DBNull.Value : (object)int.Parse(pclid));
                sp.AgregarParametro("ctcid", string.IsNullOrEmpty(ctcid) ? DBNull.Value : (object)int.Parse(ctcid));
                sp.AgregarParametro("numcomprobante", string.IsNullOrEmpty(numcomprobante) ? DBNull.Value : (object)int.Parse(numcomprobante));
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(new dto.Pago()
                        {
                            ConciliacionId = Int32.Parse(ds.Tables[0].Rows[i]["ConciliacionId"].ToString()),
                            MovimientoId = Int32.Parse(ds.Tables[0].Rows[i]["MovimientoId"].ToString()),
                            CustodiaId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["CustodiaId"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["CustodiaId"].ToString()),
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            GestorId = ds.Tables[0].Rows[i]["GestorId"].ToString(),
                            NumComprobante = ds.Tables[0].Rows[i]["NumComprobante"].ToString(),
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Ccbid = ds.Tables[0].Rows[i]["Ccbid"].ToString(),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            TipoCambio = ds.Tables[0].Rows[i]["TipoCambio"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Asignado = Decimal.Parse(ds.Tables[0].Rows[i]["Asignado"].ToString()),
                            Capital = Decimal.Parse(ds.Tables[0].Rows[i]["Capital"].ToString()),
                            Interes = Decimal.Parse(ds.Tables[0].Rows[i]["Interes"].ToString()),
                            Honorario = Decimal.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString()),
                            GastoPre = Decimal.Parse(ds.Tables[0].Rows[i]["GastoPre"].ToString()),
                            GastoJud = Decimal.Parse(ds.Tables[0].Rows[i]["GastoJud"].ToString()),
                            RutAsegurado = ds.Tables[0].Rows[i]["RutAsegurado"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            FecCancela = DateTime.Parse(ds.Tables[0].Rows[i]["FecCancela"].ToString()),
                            Gestor = ds.Tables[0].Rows[i]["Gestor"].ToString(),
                            TipoConciliacion = ds.Tables[0].Rows[i]["TipoConciliacion"].ToString(),
                            FechaAsignado = DateTime.Parse(ds.Tables[0].Rows[i]["FechaAsignado"].ToString()),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dto.ConsultaDePagos", 0);
                return lst;
            }
        }
    }
}
