using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dao;
using Dimol.dto;
using Dimol.Carteras.dto;
using System.Data;
namespace Dimol.Carteras.dao
{
    public class VisitaTerreno
    {
        #region "Visita Terreno"
        public static List<dto.VisitaTerrenoDetalle> ListarVisitaTerreno(int ctcid)
        {
            List<dto.VisitaTerrenoDetalle> lst = new List<dto.VisitaTerrenoDetalle>();
            try
            {
                DateTime fechaEnvio = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Visita_Terreno");
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();


                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaEnvio = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FECHAENVIO"].ToString(), out fechaEnvio);
                        lst.Add(new dto.VisitaTerrenoDetalle()
                        {
                            IdVisita = Int32.Parse(ds.Tables[0].Rows[i]["ID_VISITA"].ToString()),
                            IdVisitaDetalle = Int32.Parse(ds.Tables[0].Rows[i]["ID_VISITA_DETALLE"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["CTCID"].ToString()),
                            EstadoVisita = ds.Tables[0].Rows[i]["ESTADO_VISITA"].ToString().Trim(),
                            Visita = ds.Tables[0].Rows[i]["VISITA"].ToString().Trim(),
                            Comentarios = ds.Tables[0].Rows[i]["COMENTARIOS"].ToString().Trim(),
                            Direccion = ds.Tables[0].Rows[i]["DIRECCION"].ToString().Trim(),
                            Latitud = Convert.ToDouble(ds.Tables[0].Rows[i]["LATITUD"]),
                            Longitud = Convert.ToDouble(ds.Tables[0].Rows[i]["LONGITUD"]),
                            Comuna = ds.Tables[0].Rows[i]["COMUNA"].ToString().Trim(),
                            FechaEnvio = fechaEnvio,
                            DireccionEnvio = ds.Tables[0].Rows[i]["DIRECCIONENVIO"].ToString().Trim(),
                            PosicionEnvio = ds.Tables[0].Rows[i]["POSICIONENVIO"].ToString().Trim()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerreno", 0);
                return lst;
            }
        }

        public static List<dto.VisitaTerrenoGPS> ListarVisitaTerrenoGPS(int idVisita, int idVisitaDetalle)
        {
            List<dto.VisitaTerrenoGPS> lst = new List<dto.VisitaTerrenoGPS>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Visita_Terreno_Detalle_GPS");
                sp.AgregarParametro("idVisita", idVisita);
                sp.AgregarParametro("idVisitaDetalle", idVisitaDetalle);
                ds = sp.EjecutarProcedimiento();


                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.VisitaTerrenoGPS()
                        {
                            Latitud = Convert.ToDouble(ds.Tables[0].Rows[i]["LATITUD"]),
                            Longitud = Convert.ToDouble(ds.Tables[0].Rows[i]["LONGITUD"]),
                            Altitud = Int32.Parse(ds.Tables[0].Rows[i]["ALTITUD"].ToString()),
                            Direccion = ds.Tables[0].Rows[i]["DIRECCION"].ToString().Trim(),
                            Comuna = ds.Tables[0].Rows[i]["COMUNA"].ToString().Trim(),
                            Ciudad = ds.Tables[0].Rows[i]["CIUDAD"].ToString().Trim()

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoGPS", 0);
                return lst;
            }
        }

        public static List<Dimol.dto.Combobox> ListarVisitasTerrenoFotos(int idVisita, int idVisitaTerreno)
        {
            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Visita_Terreno_Detalle_Fotos");
                sp.AgregarParametro("idVisita", idVisita);
                sp.AgregarParametro("idVisitaDetalle", idVisitaTerreno);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["rutaArchivo"].ToString(),
                            Text = ds.Tables[0].Rows[i]["texto"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitasTerrenoFotos", 0);
                return lst;
            }
        }

        public static List<dto.VisitaTerrenoTelefono> ListarVisitaTerrenoTelefonos(int idVisita, int idVisitaDetalle)
        {
            List<dto.VisitaTerrenoTelefono> lst = new List<dto.VisitaTerrenoTelefono>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Visita_Terreno_Detalle_Telefonos");
                sp.AgregarParametro("idVisita", idVisita);
                sp.AgregarParametro("idVisitaDetalle", idVisitaDetalle);
                ds = sp.EjecutarProcedimiento();


                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.VisitaTerrenoTelefono()
                        {
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString()),


                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoTelefonos", 0);
                return lst;
            }
        }

        public static int InsertarVisitaTerrenoSolicitud(int codemp, int ctcid, string direccion,
                                                        int idRegion, int idCiudad, int idComuna,
                                                        string comuna, int userId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Solicitud");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("direccion", direccion);
                sp.AgregarParametro("idRegion", idRegion);
                sp.AgregarParametro("idCiudad", idCiudad);
                sp.AgregarParametro("idComuna", idComuna);
                sp.AgregarParametro("comuna", comuna);
                sp.AgregarParametro("userId", userId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["solicitud"].ToString());
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoSolicitud", userId);
                return -1;
            }
            return result;
        }

        public static int InsertarVisitaTerrenoSolicitudStatus(int solicitudId, int userId, int estado)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Solicitud_status");
                sp.AgregarParametro("solicitudId", solicitudId);
                sp.AgregarParametro("userId", userId);
                sp.AgregarParametro("estado", estado);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["solicitud"].ToString());
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudStatus", userId);
                return -1;
            }
            return result;
        }

        public static int InsertarVisitaTerrenoSolicitudGestor(int solicitudId, int gesId, string gestor,
                                                                string telefonoImei, string telefonNum, int userId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Solicitud_gestor");
                sp.AgregarParametro("solicitudId", solicitudId);
                sp.AgregarParametro("gestorId", gesId);
                sp.AgregarParametro("gestor", gestor);
                sp.AgregarParametro("telefonoImei", telefonoImei);
                sp.AgregarParametro("telefonoNum", telefonNum);
                sp.AgregarParametro("userId", userId);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["solicitud"].ToString());
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudSGestor", userId);
                return -1;
            }
            return result;
        }

        public static int InsertarVisitaTerreno(int solicitudId, int ctcid, int userId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno");
                sp.AgregarParametro("solicitudId", solicitudId);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("userId", userId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["solicitud"].ToString());
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerreno", userId);
                return -1;
            }
            return result;
        }

        public static int InsertarVisitaTerrenoSolicitudCoordenadas(int solicitudId, string altitud, string longitud)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Actualizar_Visita_Terreno_Solicitud_Coordenadas");
                sp.AgregarParametro("solicitudId", solicitudId);
                sp.AgregarParametro("latitud", altitud);
                sp.AgregarParametro("longitud", longitud);


                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudCoordenadas", solicitudId);
                return -1;
            }
            return result;
        }
        public static int ActualizarVisitaTerrenoSolicitud(int solicitudId, int estado, int userId, string visitada)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Actualizar_Visita_Terreno_Solicitud");
                sp.AgregarParametro("solicitudId", solicitudId);
                sp.AgregarParametro("estado", estado);
                sp.AgregarParametro("user", userId);
                sp.AgregarParametro("visitada", visitada);

                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ActualizarVisitaTerrenoSolicitud", userId);
                return -1;
            }
            return result;
        }

        public static int ListarVisitaTerrenoSolicitudAprobarCount(int codemp, int pclid, int regionId, int ciudadId, int comunaId, decimal monto,
                                                                    bool enQuiebra, bool enSolicitud, bool enPreQuiebra, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_VisitaTerrenoSolicitudAprobar_Count");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid == 0 ? DBNull.Value : (object)pclid);
                sp.AgregarParametro("idRegion", regionId == 0 ? DBNull.Value : (object)regionId);
                sp.AgregarParametro("idCiudad", ciudadId == 0 ? DBNull.Value : (object)ciudadId);
                sp.AgregarParametro("idComuna", comunaId == 0 ? DBNull.Value : (object)comunaId);
                sp.AgregarParametro("monto", monto == 0 ? DBNull.Value : (object)monto);
                sp.AgregarParametro("quiebra", (enQuiebra) ? "S" : "N");
                sp.AgregarParametro("solitada", (enSolicitud) ? "S" : "N");
                sp.AgregarParametro("preQuiebra", (enPreQuiebra) ? "S" : "N");
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                    }
                
                return count;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoSolicitudAprobarCount", 0);
                return count;
            }
        }
        public static List<dto.VisitaTerrenoSolicitudAceptar> ListarVisitaTerrenoSolicitudAprobar(int codemp, int pclid, int regionId, int ciudadId, int comunaId, decimal monto,
                                                                                                bool enQuiebra, bool enSolicitud, bool enPreQuiebra, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.VisitaTerrenoSolicitudAceptar> lst = new List<dto.VisitaTerrenoSolicitudAceptar>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_VisitaTerrenoSolicitudAprobar");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid == 0 ? DBNull.Value : (object)pclid);
                sp.AgregarParametro("idRegion", regionId == 0 ? DBNull.Value : (object)regionId);
                sp.AgregarParametro("idCiudad", ciudadId == 0 ? DBNull.Value : (object)ciudadId);
                sp.AgregarParametro("idComuna", comunaId == 0 ? DBNull.Value : (object)comunaId);
                sp.AgregarParametro("monto", monto == 0 ? DBNull.Value : (object)monto);
                sp.AgregarParametro("quiebra", (enQuiebra) ? "S" : "N");
                sp.AgregarParametro("solitada", (enSolicitud) ? "S" : "N");
                sp.AgregarParametro("preQuiebra", (enPreQuiebra) ? "S" : "N");
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.VisitaTerrenoSolicitudAceptar()
                        {
                            pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            ctcid = Int32.Parse(ds.Tables[0].Rows[i]["CTCID"].ToString()),
                            solicitudId = Int32.Parse(ds.Tables[0].Rows[i]["SOLICITUDID"].ToString()),
                            rutDeudor = ds.Tables[0].Rows[i]["RUT_DEUDOR"].ToString(),
                            deudor = ds.Tables[0].Rows[i]["DEUDOR"].ToString(),
                            quiebra = ds.Tables[0].Rows[i]["QUIEBRA"].ToString(),
                            direccion = ds.Tables[0].Rows[i]["DIRECCION"].ToString(),
                            comId = Int32.Parse(ds.Tables[0].Rows[i]["COMID"].ToString()),
                            comuna = ds.Tables[0].Rows[i]["COMUNA"].ToString(),
                            ciuId = Int32.Parse(ds.Tables[0].Rows[i]["CIUID"].ToString()),
                            ciudad = ds.Tables[0].Rows[i]["CIUDAD"].ToString(),
                            regId = Int32.Parse(ds.Tables[0].Rows[i]["REGID"].ToString()),
                            region = ds.Tables[0].Rows[i]["REGION"].ToString(),
                            deuda = decimal.Parse(ds.Tables[0].Rows[i]["DEUDA"].ToString()),
                            cliente = ds.Tables[0].Rows[i]["CLIENTE"].ToString(),
                            gestor = ds.Tables[0].Rows[i]["GESTOR"].ToString(),
                            ultimaGestion = ds.Tables[0].Rows[i]["ULTMAVISITA"].ToString(),
                            fila = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString()),
                            Solicitante = ds.Tables[0].Rows[i]["Solicitante"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoSolicitudAprobar", 0);
                return lst;
            }
        }
        public static int ListarVisitaTerrenoGenerarCount(int gesId, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_VisitaTerrenoGenerarCount");
                sp.AgregarParametro("gesid", gesId);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                    }
                
                return count;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoGenerarCount", 0);
                return count;
            }
        }
        public static List<dto.VisitaTerrenoGenerar> ListarVisitaTerrenoGenerar(int gesId, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.VisitaTerrenoGenerar> lst = new List<dto.VisitaTerrenoGenerar>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_VisitaTerrenoGenerar");
                sp.AgregarParametro("gesid", gesId);
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
                        lst.Add(new dto.VisitaTerrenoGenerar()
                        {

                            SolicitudId = Int32.Parse(ds.Tables[0].Rows[i]["SOLICITUD_ID"].ToString()),
                            RutDeudor = ds.Tables[0].Rows[i]["RUT"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["DEUDOR"].ToString(),
                            Direccion = ds.Tables[0].Rows[i]["DIRECCION"].ToString(),
                            Comuna = ds.Tables[0].Rows[i]["COMUNA"].ToString(),
                            Ciudad = ds.Tables[0].Rows[i]["CIUDAD"].ToString(),
                            Latitud = Convert.ToDouble(ds.Tables[0].Rows[i]["LATITUD"]),
                            Longitud = Convert.ToDouble(ds.Tables[0].Rows[i]["LONGITUD"]),
                            Deuda = decimal.Parse(ds.Tables[0].Rows[i]["DEUDA"].ToString()),
                            Gestor = ds.Tables[0].Rows[i]["GESTOR"].ToString()

                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoGenerar", 0);
                return lst;
            }
        }
        public static List<String> NotificarTerrenoColor(int ctcid)
        {

            List<string> resultados = new List<string>();


            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_VisitaTerrenoPestanaColor");

                sp.AgregarParametro("ctcid", ctcid);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        resultados.Add(ds.Tables[0].Rows[0]["COLOR"].ToString());
                        resultados.Add(ds.Tables[0].Rows[0]["CANT"].ToString());
                    }
                
                return resultados;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.NotificarTerrenoColor", 0);
                return resultados;
            }

        }

        public static int InsertarVisitaTerrenoClientePointGestor(string clientepointId,int solicitudId,
                                                                int gestorId, string telefonoImei, int userId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_ClientePoint_gestor");
                sp.AgregarParametro("clientePointId", clientepointId);
                sp.AgregarParametro("solicitudId", solicitudId);
                sp.AgregarParametro("gestorId", gestorId);
                sp.AgregarParametro("telefonoImei", telefonoImei);
                sp.AgregarParametro("userId", userId);
                
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["pointId"].ToString());
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoClientePointGestor", userId);
                return -1;
            }
            return result;
        }
        public static int ListarVisitaTerrenoCarteraGeoGestionGestorCount(string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_VisitaTerreno_CarteraGestores_Grilla_Count");
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                    }
                
                return count;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoGenerarCount", 0);
                return count;
            }
        }
        public static List<dto.VisitaTerrenoCarteraGestorGeoGestion> ListarVisitaTerrenoCarteraGeoGestionGestor(string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.VisitaTerrenoCarteraGestorGeoGestion> lst = new List<dto.VisitaTerrenoCarteraGestorGeoGestion>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_VisitaTerreno_CarteraGestores_Grilla");
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
                        lst.Add(new dto.VisitaTerrenoCarteraGestorGeoGestion()
                        {

                            CarteraId = ds.Tables[0].Rows[i]["CARTERA_ID"].ToString(),
                            Cartera_Nombre = ds.Tables[0].Rows[i]["CARTERA_NOMBRE"].ToString(),
                            Descripcion = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString(),
                            GesId = ds.Tables[0].Rows[i]["GES_GESID"].ToString(),
                            Ges_Nombre = ds.Tables[0].Rows[i]["GES_NOMBRE"].ToString(),
                            TelefonoTerreno = ds.Tables[0].Rows[i]["GES_TELEFONO_TERRENO"].ToString(),
                            TelefonoImei = ds.Tables[0].Rows[i]["GES_IMEI"].ToString()

                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoGenerar", 0);
                return lst;
            }
        }

        public static int InsertarVisitaTerrenoCarteraGestor(string carteraId, int gestorId, string carteraNombre, 
                                                            string carteraDescripcion, int userId)
        {
            int result = -1;
            try
            {
            
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Cartera_Gestor");
                sp.AgregarParametro("carteraId", carteraId);
                sp.AgregarParametro("gestorId", gestorId);
                sp.AgregarParametro("carteraNombre", carteraNombre);
                sp.AgregarParametro("carteraDescripcion", carteraDescripcion);
                sp.AgregarParametro("userId", userId);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["cartera"].ToString());
                    }
                
               
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoCarteraGestor", userId);
                return -1;
            }
            return result;
        }

        public static int CountVisitaTerrenoCarteraGestor(int gestorId)
        {
            int result = -1;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Visita_Terreno_Cartera_Gestor_Count");
                sp.AgregarParametro("gestorId", gestorId);
               
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["countCartera"].ToString());
                    }
                

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.CountVisitaTerrenoCarteraGestor", 0);
                return -1;
            }
            return result;
        }

        public static string getPassGeoGestion()
        {
            string pass = "0";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Visita_Terreno_Credenciales_GeoGestion");
               
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        pass = ds.Tables[0].Rows[0]["PASSGEO"].ToString();
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.getPassGeoGestion", 0);
                return "0";
            }
            return pass;
        }
        public static string getUserGeoGestion()
        {
            string userGeo = "0";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Visita_Terreno_Credenciales_GeoGestion");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        userGeo = ds.Tables[0].Rows[0]["USERGEO"].ToString();
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.getUserGeoGestion", 0);
                return "0";
            }
            return userGeo;
        }

        public static int InsertarVisitaTerrenoFormulario(string fechaFormulario, string formularioNombre, string gestor,
                                                        string cliente, string posicion, string direccion,
                                                        string foto1, string foto2, string foto3, string foto4,
                                                        string estado, string visita, string direccionActual, string comentarios,
                                                        string direccion1, string comuna1, string direccion2, string comuna2, 
                                                        int userId, int idCarga, string formId)
        {
            int result = -1;
            try
            {
                DateTime fechaEnvio = new DateTime();
                fechaEnvio = new DateTime();
                DateTime.TryParse(fechaFormulario, out fechaEnvio);
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Formulario");
                sp.AgregarParametro("fecEnvio", fechaEnvio);
                sp.AgregarParametro("formularioNombre", formularioNombre);
                sp.AgregarParametro("gestor", gestor);
                sp.AgregarParametro("cliente", cliente);
                sp.AgregarParametro("position", posicion);
                sp.AgregarParametro("direccion", direccion);
                sp.AgregarParametro("foto1", foto1);
                sp.AgregarParametro("foto2", foto2);
                sp.AgregarParametro("foto3", foto3);
                sp.AgregarParametro("foto4", foto4);
                sp.AgregarParametro("estado", estado);
                sp.AgregarParametro("visita", visita);
                sp.AgregarParametro("direccionActual", direccionActual);
                sp.AgregarParametro("comentarios", comentarios);
                sp.AgregarParametro("direccion1", direccion1);
                sp.AgregarParametro("comuna1", comuna1);
                sp.AgregarParametro("direccion2", direccion2);
                sp.AgregarParametro("comuna2", comuna2);
                sp.AgregarParametro("userId", userId);
                sp.AgregarParametro("idCarga", idCarga);
                sp.AgregarParametro("formId", formId);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["formularioId"].ToString());
                    }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoFormulario", userId);
                return -1;
            }
            return result;
        }

        public static int InsertarVisitaTerrenoCarga(string archivo, int userId)
        {
            int result = -1;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Carga");
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoCarga", userId);
                return -1;
            }
            return result;
        }
        public static List<dto.DatosCargaVisitaTerreno> ListarVisitaTerrenoFormulariosCarga(int idCarga, string procesado)
        {
            List<dto.DatosCargaVisitaTerreno> lst = new List<dto.DatosCargaVisitaTerreno>();
            try
            {
                DateTime fechaVisita = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Visita_Terreno_Formularios");
                sp.AgregarParametro("idCarga", idCarga);
                sp.AgregarParametro("procesado", procesado);
               
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaVisita = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["fechavisita"].ToString(), out fechaVisita);
                        lst.Add(new dto.DatosCargaVisitaTerreno()
                        {

                            FechaVisita = fechaVisita,
                            Gestor = ds.Tables[0].Rows[i]["gestor"].ToString(),
                            FormularioId = Int32.Parse(ds.Tables[0].Rows[i]["formularioid"].ToString()),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            NombreFormulario = ds.Tables[0].Rows[i]["nombreformulario"].ToString(),
                            Posicion = ds.Tables[0].Rows[i]["posicion"].ToString(),
                            Direccion = ds.Tables[0].Rows[i]["direccion"].ToString(),
                            Foto1 = ds.Tables[0].Rows[i]["foto1"].ToString(),
                            Foto2 = ds.Tables[0].Rows[i]["foto2"].ToString(),
                            Foto3 = ds.Tables[0].Rows[i]["foto3"].ToString(),
                            Foto4 = ds.Tables[0].Rows[i]["foto4"].ToString(),
                            EstadoVisita = ds.Tables[0].Rows[i]["estadovisita"].ToString(),
                            Visita = ds.Tables[0].Rows[i]["visita"].ToString(),
                            DireccionActual = ds.Tables[0].Rows[i]["direccionactual"].ToString(),
                            Comentarios = ds.Tables[0].Rows[i]["comentarios"].ToString(),
                            Direccion1 = ds.Tables[0].Rows[i]["direccion1"].ToString(),
                            Comuna1 = ds.Tables[0].Rows[i]["comuna1"].ToString(),
                            Direccion2 = ds.Tables[0].Rows[i]["direccion2"].ToString(),
                            Comuna2 = ds.Tables[0].Rows[i]["comuna2"].ToString(),
                            Procesado = ds.Tables[0].Rows[i]["procesado"].ToString()

                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoFormulariosCarga", 0);
                return lst;
            }
        }

        public static int InsertarVisitaTerrenoDetalle(int solicitudId, int estadoVisita, 
                                                    string visita, string comentarios, string direccionActual,
                                                    DateTime fecEnvio, string direccionEnvio, string posicionEnvio)
        {
            int result = -1;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Detalle");
                sp.AgregarParametro("solicitudId", solicitudId);
                sp.AgregarParametro("estadoVisita", estadoVisita);
                sp.AgregarParametro("visita ", visita);
                sp.AgregarParametro("comentarios", comentarios);
                sp.AgregarParametro("direccionActual", direccionActual);
                sp.AgregarParametro("fecEnvio", fecEnvio);
                sp.AgregarParametro("direccionEnvio", direccionEnvio);
                sp.AgregarParametro("posicionEnvio", posicionEnvio);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["idvisitadetalle"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoCarga", 0);
                return -1;
            }
            return result;
        }

        public static List<Combobox> ListarVisitaTerrenoTipoEstadoVisita()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Visita_Terreno_Tipo_Estado_Visita");
               
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["ESTADO_VISITA"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID_TIPO_ESTADO_VISITA"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoTipoEstadoVisita", 0);
                return lst;
            }
        }

        public static int InsertarVisitaTerrenoNuevasDirecciones(int solicitudId, int visitaDetalleId, string direccion,
                                                        string comuna, string ciudad, string latitud, string longitud)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Detalle_Nuevas_Direcciones");
                sp.AgregarParametro("solicitudId ", solicitudId);
                sp.AgregarParametro("idvisitadetalle", visitaDetalleId);
                sp.AgregarParametro("direccion", direccion);
                sp.AgregarParametro("comuna", comuna);
                sp.AgregarParametro("ciudad ", ciudad);
                sp.AgregarParametro("latitud ", latitud);
                sp.AgregarParametro("longitud ", longitud);
                result = sp.EjecutarProcedimientoTrans();
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoNuevasDirecciones", 0);
                return -1;
            }
            return result;
        }
        public static int procesarFormulario(int formularioId, string procesado)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Actualizar_Visita_Terreno_Formulario_Procesado");
                sp.AgregarParametro("formularioId", formularioId);
                sp.AgregarParametro("procesado", procesado);

                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.procesarFormulario", 0);
                return -1;
            }
            return result;
        }
        public static int InsertarFormularioSolicitud(int formularioId, int solicitudId)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Formulario_Solicitud");
                sp.AgregarParametro("formularioId", formularioId);
                sp.AgregarParametro("solicitudId", solicitudId);

                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.insertarFormularioSolicitud", 0);
                return -1;
            }
            return result;
        }

        public static string InsertarVisitaTerrenoFormularioFormId(string formId)
        {
            string result = "-1";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Formulario_FormId");
                sp.AgregarParametro("formularioId", formId);
                
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = ds.Tables[0].Rows[0]["formId"].ToString();
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoFormularioFormId", 777);
                return "-1";
            }
            return result;
        }
        public static int InsertarVisitaTerrenoFormularioFotos(int solicitudId, int visitaDetalleId, string rutaImageGeo, string pathImage)
        {
            int result = -1;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Visita_Terreno_Detalle_Fotos");
                sp.AgregarParametro("solicitudId", solicitudId);
                sp.AgregarParametro("idvisitadetalle", visitaDetalleId);
                sp.AgregarParametro("rutaImageGeo", rutaImageGeo);
                sp.AgregarParametro("pathImage", pathImage);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["IDFOTO"].ToString());
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoFormularioFotos", 777);
                return -1;
            }
            return result;
        }

        public static int ExisteSolicitud(int solicitudId)
        {
            int result = -1;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Existe_Visita_Terreno_Solicitud");
                sp.AgregarParametro("solicitudId", solicitudId);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = Int32.Parse(ds.Tables[0].Rows[0]["solicitud"].ToString());
                    }
                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ExisteSolicitud", 777);
                return -1;
            }
            return result;
        }

        public static List<dto.DatosCargaVisitaTerreno> ListarVisitaTerrenoFormulariosById(string formId)
        {
            List<dto.DatosCargaVisitaTerreno> lst = new List<dto.DatosCargaVisitaTerreno>();
            try
            {
                DateTime fechaVisita = new DateTime();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Visita_Terreno_Formulario_ById");
                sp.AgregarParametro("formId", formId);
               
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaVisita = new DateTime();
                        DateTime.TryParse(ds.Tables[0].Rows[i]["fechavisita"].ToString(), out fechaVisita);
                        lst.Add(new dto.DatosCargaVisitaTerreno()
                        {

                            FechaVisita = fechaVisita,
                            Gestor = ds.Tables[0].Rows[i]["gestor"].ToString(),
                            FormularioId = Int32.Parse(ds.Tables[0].Rows[i]["formularioid"].ToString()),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            NombreFormulario = ds.Tables[0].Rows[i]["nombreformulario"].ToString(),
                            Posicion = ds.Tables[0].Rows[i]["posicion"].ToString(),
                            Direccion = ds.Tables[0].Rows[i]["direccion"].ToString(),
                            Foto1 = ds.Tables[0].Rows[i]["foto1"].ToString(),
                            Foto2 = ds.Tables[0].Rows[i]["foto2"].ToString(),
                            Foto3 = ds.Tables[0].Rows[i]["foto3"].ToString(),
                            Foto4 = ds.Tables[0].Rows[i]["foto4"].ToString(),
                            EstadoVisita = ds.Tables[0].Rows[i]["estadovisita"].ToString(),
                            Visita = ds.Tables[0].Rows[i]["visita"].ToString(),
                            DireccionActual = ds.Tables[0].Rows[i]["direccionactual"].ToString(),
                            Comentarios = ds.Tables[0].Rows[i]["comentarios"].ToString(),
                            Direccion1 = ds.Tables[0].Rows[i]["direccion1"].ToString(),
                            Comuna1 = ds.Tables[0].Rows[i]["comuna1"].ToString(),
                            Direccion2 = ds.Tables[0].Rows[i]["direccion2"].ToString(),
                            Comuna2 = ds.Tables[0].Rows[i]["comuna2"].ToString(),
                            Procesado = ds.Tables[0].Rows[i]["procesado"].ToString()

                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoFormulariosById", 0);
                return lst;
            }
        }

        public static int actualizarFormulario(string formId)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Actualizar_Visita_Terreno_Formulario");
                sp.AgregarParametro("formId", formId);
               
                result = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.actualizarFormulario", 0);
                return -1;
            }
            return result;
        }
        public static List<int> ListarClientesDeudor(int codemp, int ctcid)
        {
            List<int> lst = new List<int>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Cliente_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()));
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarRutaEstampesDeudor", 0);
                return lst;
            }

        }

        public static List<Tuple<int, int>> ListarClientesDeudorVisita(int codemp, int solicitudId)
        {
            List<Tuple<int, int>> list = new List<Tuple<int, int>>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Cliente_Deudor_Visita");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("solicitudId", solicitudId);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        list.Add(new Tuple<int, int>(Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()), Int32.Parse(ds.Tables[0].Rows[i]["CTCID"].ToString())));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.VisitaTerreno.ListarClientesDeudorVisita", 0);
                return list;
            }

        }
        #endregion
    }
}
