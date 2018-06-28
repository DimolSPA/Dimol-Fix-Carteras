using Dimol.dao;
using SII.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SII.dao
{
    public class Status
    {
        public static int InsertarCaptcha(dto.Status obj)
        {
            int codigo = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Captcha");
                sp.AgregarParametro("captcha", obj.CodigoCaptcha);
                sp.AgregarParametro("codigo", obj.ValorCaptcha);
                sp.AgregarParametro("estado", "V");
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        codigo = Int32.Parse( ds.Tables[0].Rows[0][0].ToString());
                    }
                }

                return codigo;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Insertar_Captcha", obj.Rut);
                return codigo;
            }
        }

        public static int InsertarRutCaptcha(dto.Status obj)
        {
            int codigo = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Rut_Captcha");
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("dv", obj.DigitoVerificador);
                sp.AgregarParametro("captcha", obj.CodigoCaptcha);
                sp.AgregarParametro("estado", "V");
                sp.AgregarParametro("html", obj.Html); 
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        codigo = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                }

                return codigo;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Insertar_Rut_Captcha", obj.Rut);
                return codigo;
            }
        }

        public static int InsertarRutCaptchaEstado(dto.Status obj)
        {
            int codigo = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Rut_Captcha");
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("dv", obj.DigitoVerificador);
                sp.AgregarParametro("captcha", obj.CodigoCaptcha);
                sp.AgregarParametro("estado", obj.Estado);
                sp.AgregarParametro("html", obj.Html);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        codigo = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                }

                return codigo;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Insertar_Rut_Captcha", obj.Rut);
                return codigo;
            }
        }

        public static List<dto.Status> ListarRutporEstado(string estado)
        {
            List<dto.Status> lst = new List<dto.Status>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Rut_Captcha");
                sp.AgregarParametro("estado", estado);
                sp.AgregarParametro("fecha", DateTime.Now);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Status()
                        {
                            IdRut = Int32.Parse(ds.Tables[0].Rows[i]["id_rut"].ToString()),
                            Rut = Int32.Parse(ds.Tables[0].Rows[i]["rut"].ToString()),
                            DigitoVerificador = ds.Tables[0].Rows[i]["dv"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Listar_Rut_Captcha: " + estado, 0);
                return lst;
            }
        }

        public static List<dto.Status> ListarRutporDemonio(string estado)
        {
            List<dto.Status> lst = new List<dto.Status>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Rut_Captcha_Demonio");
                sp.AgregarParametro("estado", estado);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Status()
                        {
                            IdRut = Int32.Parse(ds.Tables[0].Rows[i]["id_rut"].ToString()),
                            Rut = Int32.Parse(ds.Tables[0].Rows[i]["rut"].ToString()),
                            DigitoVerificador = ds.Tables[0].Rows[i]["dv"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Listar_Rut_Captcha_Demonio: " + estado, 0);
                return lst;
            }
        }

        public static List<dto.Status> ListarRutporDemonioNew(string estado)
        {
            List<dto.Status> lst = new List<dto.Status>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Rut_Captcha_Demonio");
                sp.AgregarParametro("estado", estado);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Status()
                        {
                            IdRut = Int32.Parse(ds.Tables[0].Rows[i]["id_rut"].ToString()),
                            Rut = Int32.Parse(ds.Tables[0].Rows[i]["rut"].ToString()),
                            DigitoVerificador = ds.Tables[0].Rows[i]["dv"].ToString(),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Listar_Rut_Captcha_Demonio: " + estado, 0);
                return lst;
            }
        }

        public static string BuscarCaptcha(string captcha)
        {
            string codigo = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Buscar_Captcha");
                sp.AgregarParametro("captcha", captcha);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        codigo =  ds.Tables[0].Rows[0][0].ToString();
                    }
                }

                return codigo;
            }
            catch (Exception ex)
            {
                return codigo;
            }
        }

        public static int DetenerRutporDemonio(string estado)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Detener_Rut_Captcha_Demonio");
                sp.AgregarParametro("estado", estado);

                error = sp.EjecutarProcedimientoTrans();

                //ds = sp.EjecutarProcedimiento();

                //if (ds.Tables.Count > 0)
                //{
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        lst.Add(new dto.Status()
                //        {
                //            IdRut = Int32.Parse(ds.Tables[0].Rows[i]["id_rut"].ToString()),
                //            Rut = Int32.Parse(ds.Tables[0].Rows[i]["rut"].ToString()),
                //            DigitoVerificador = ds.Tables[0].Rows[i]["dv"].ToString()
                //        });
                //    }
                //}

                //return lst;
            }
            catch (Exception ex)
            {

            } 
            return error;
        }

        #region "Procesar RUT"

        public static List<dto.Status> ListarRutporProcesar(string estado)
        {
            List<dto.Status> lst = new List<dto.Status>();
            DateTime fecha = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Rut_Procesa_Demonio");
                sp.AgregarParametro("estado", estado);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Status()
                        {
                            IdRut = Int32.Parse(ds.Tables[0].Rows[i]["id_rut"].ToString()),
                            Rut = Int32.Parse(ds.Tables[0].Rows[i]["rut"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Html = ds.Tables[0].Rows[i]["Html"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Listar_Rut_Procesa_Demonio: " + estado, 0);
                return lst;
            }
        }

        public static int InsertarTipoDocumento(string documento)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("INSERTA_DOCUMENTO");
                sp.AgregarParametro("documento", documento);

                error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {

            }
            return error;
        }

        public static int InsertarTimbraje(int ctcid, int tipoDocumento, int anio, DateTime fecha)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("dbo.INSERTA_TIMBRAJE");
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("tipo_documento", tipoDocumento);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("fecha", fecha);

                error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {

            }
            return error;
        }

        public static List<SII.dto.Combobox> ListarTipoDocumento()
        {
            List<SII.dto.Combobox> lst = new List<SII.dto.Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("LISTAR_TIPO_DOCUMENTO");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new SII.dto.Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["TIPO_DOCUMENTO"].ToString(),
                            Text = ds.Tables[0].Rows[i]["DOCUMENTO"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarTipoDocumento: ", 0);
                return lst;
            }
        }

        public static int MarcarRutLeido(int idRut, string estado)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("MARCA_RUT_LEIDO");
                sp.AgregarParametro("ID_RUT", idRut);
                sp.AgregarParametro("ESTADO", estado);

                error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {

            }
            return error;
        }

        public static int InsertarCabecera(Cabecera obj )
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("INSERTA_CABECERA");
                sp.AgregarParametro("id_rut", obj.IdRut);
                sp.AgregarParametro("rut", obj.Rut);
                sp.AgregarParametro("dv", obj.DV);
                sp.AgregarParametro("nombre_razon_social", (object)obj.NombreRazonSocial ?? DBNull.Value);
                sp.AgregarParametro("fecha_consulta", obj.FechaConsulta == new DateTime() ? DBNull.Value : (object)obj.FechaConsulta);
                sp.AgregarParametro("inicio_actividades", (object)obj.InicioActividades ?? DBNull.Value);
                sp.AgregarParametro("fecha_inicio_actividades", obj.FechaInicioActividades == new DateTime() ? DBNull.Value : (object)obj.FechaInicioActividades);
                sp.AgregarParametro("impuesto_moneda_extranjera", (object)obj.ImpuestoMonedaExtranjera ?? DBNull.Value);
                sp.AgregarParametro("menor_pro_pyme", (object)obj.MenorProPyme ?? DBNull.Value);
                sp.AgregarParametro("registrado", (object)obj.Registrado ?? DBNull.Value);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("emision", (object)obj.Emision?? DBNull.Value);
                sp.AgregarParametro("observacion", (object)obj.Observacion ?? DBNull.Value);
                sp.AgregarParametro("fecha", obj.Fecha);

                error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {

            }
            return error;
        }

        public static int InsertarActividadEconomica(ActividadEconomica obj)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("INSERTA_ACTIVIDAD_RUT");
                sp.AgregarParametro("id_rut", obj.IdRut);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("codigo_actividad", obj.CodigoActividad);
                sp.AgregarParametro("fecha", obj.FechaConsulta);

                error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {

            }
            return error;
        }

        public static List<SII.dto.Combobox> ListarActividadEconomica()
        {
            List<SII.dto.Combobox> lst = new List<SII.dto.Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("LISTAR_ACTIVIDAD");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new SII.dto.Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["CODIGO_ACTIVIDAD"].ToString(),
                            Text = ds.Tables[0].Rows[i]["ACTIVIDAD"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarTipoDocumento: ", 0);
                return lst;
            }
        }

        public static int InsertarTipoActividadEconomica(string codigo, string actividad, string iva, int categoria, string disponibleInternet)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("INSERTA_ACTIVIDAD_ECONOMICA");
                sp.AgregarParametro("CODIGO_ACTIVIDAD", codigo);
                sp.AgregarParametro("ACTIVIDAD", actividad);
                sp.AgregarParametro("AFECTO_IVA", iva);
                sp.AgregarParametro("CATEGORIA_TRIBUTARIA", categoria);
                sp.AgregarParametro("DISPONIBLE_INTERNET", disponibleInternet);

                error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {

            }
            return error;
        }

        #endregion
    }
}
