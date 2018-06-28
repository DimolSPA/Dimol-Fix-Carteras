using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using Dimol.dto;
namespace Dimol.Carteras.dao
{
    public class BienVehiculo
    {
        public static List<dto.BienVehiculo> ListarBienesVehiculosGrilla(int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BienVehiculo> lst = new List<dto.BienVehiculo>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Bienes_Vehiculos_Grilla");
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
                        lst.Add(new dto.BienVehiculo()
                        {
                            VehiculoId = int.Parse(ds.Tables[0].Rows[i]["VEHICULOID"].ToString()),
                            Patente = ds.Tables[0].Rows[i]["PATENTE"].ToString(),
                            Marca = ds.Tables[0].Rows[i]["MARCA"].ToString(),
                            Modelo = ds.Tables[0].Rows[i]["MODELO"].ToString(),
                            Anio = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["ANIO"].ToString()) ? new int() : int.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString()),
                            Propietario = ds.Tables[0].Rows[i]["PROPIETARIO"].ToString() == "S" ? true : false,
                            Prenda = ds.Tables[0].Rows[i]["PRENDA"].ToString() == "S" ? true : false,
                            Embargo = ds.Tables[0].Rows[i]["EMBARGO"].ToString() == "S" ? true : false,
                            MarcaId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MARCAID"].ToString()) ? new int(): int.Parse(ds.Tables[0].Rows[i]["MARCAID"].ToString()),
                            ModeloId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MODELOID"].ToString()) ? new int() : int.Parse(ds.Tables[0].Rows[i]["MODELOID"].ToString()),
                            ArchivoComprobante = ds.Tables[0].Rows[i]["ArchivoComprobante"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienVehiculo.ListarBienesVehiculosGrilla", 0);

                return lst;
            }
        }

        public static int ListarBienesVehiculosGrillaCount(int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Bienes_Vehiculos_Grilla_Count");
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = int.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienVehiculo.ListarBienesVehiculosGrillaCount", 0);
                return count;
            }
        }
        public static List<Combobox> ListarMarcasVehiculo()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Marcas_Vehiculo");
                
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Descripcion"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienVehiculo.ListarMarcasVehiculo", 0);
                return lst;
            }
        }
        public static List<Combobox> ListarModelosVehiculo(int marcaId)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Modelos_Vehiculo");
                sp.AgregarParametro("marcaId", marcaId);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Descripcion"].ToString(),
                            Value = ds.Tables[0].Rows[i]["Id"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienVehiculo.ListarModelosVehiculo", 0);
                return lst;
            }
        }

        public static int InsertarBienVehiculo(dto.BienVehiculo model,int ctcid, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_BienVehiculo");
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("patente", model.Patente);
                sp.AgregarParametro("marcaId", model.MarcaId == null ? DBNull.Value: (object)model.MarcaId);
                sp.AgregarParametro("modeloId", model.ModeloId == null ? DBNull.Value : (object)model.ModeloId);
                sp.AgregarParametro("anio", model.Anio == null ? DBNull.Value : (object)model.Anio);
                sp.AgregarParametro("propietario", model.Propietario ? "S" : "N");
                sp.AgregarParametro("prenda", model.Prenda ? "S" : "N");
                sp.AgregarParametro("embargo", model.Embargo ? "S" : "N");
                sp.AgregarParametro("userId", user);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienVehiculo.InsertarBienVehiculo", user);

                return id;
            }
            return id;
        }

        public static int ActualizarBienVehiculo(dto.BienVehiculo model, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Actualizar_BienVehiculo");
                sp.AgregarParametro("Id", model.VehiculoId);
                sp.AgregarParametro("patente", model.Patente);
                sp.AgregarParametro("marcaId", model.MarcaId == null ? DBNull.Value : (object)model.MarcaId);
                sp.AgregarParametro("modeloId", model.ModeloId == null ? DBNull.Value : (object)model.ModeloId);
                sp.AgregarParametro("anio", model.Anio == null ? DBNull.Value : (object)model.Anio);
                sp.AgregarParametro("propietario", model.Propietario ? "S" : "N");
                sp.AgregarParametro("prenda", model.Prenda ? "S" : "N");
                sp.AgregarParametro("embargo", model.Embargo ? "S" : "N");
                sp.AgregarParametro("userId", user);

                id = sp.EjecutarProcedimientoTrans();


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienVehiculo.ActualizarBienVehiculo", user);

                return id;
            }
            return id;
        }

        public static int ExisteRegistro(int vehiculoId)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Exist_BienVehiculo");
                sp.AgregarParametro("Id", vehiculoId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["vehiculoId"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienVehiculo.ExisteRegistro", 0);

                return id;
            }
            return id;
        }

        public static int InsertarVehiculoArchivo(int vehiculoId, string rutaArchivo, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Insertar_Bien_Vehiculo_Certificado");
                sp.AgregarParametro("vehiculoId", vehiculoId);
                sp.AgregarParametro("rutaArchivo", rutaArchivo);
                sp.AgregarParametro("userId", user);

                id = sp.EjecutarProcedimientoTrans();


            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienVehiculo.InsertarVehiculoArchivo", user);

                return id;
            }
            return id;
        }
    }
}
