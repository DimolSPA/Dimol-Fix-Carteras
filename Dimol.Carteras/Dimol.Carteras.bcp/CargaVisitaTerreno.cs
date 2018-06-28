using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Carteras.dto;
using System.Data;
using Dimol.bcp;
using Dimol.dto;
using System.Transactions;
using System.Configuration;
using System.Drawing;
using System.IO;
namespace Dimol.Carteras.bcp
{
    public class CargaVisitaTerreno
    {
        #region "Cargar Datos Visita Terreno"


        public static List<DatosCargaVisitaTerreno> CargarDatosVisitaTerreno(string nombreArchivo, int user)
        {
            List<DatosCargaVisitaTerreno> lst = new List<DatosCargaVisitaTerreno>();
            try
            {
                DataSet ds = Funciones.CargarExcel(nombreArchivo, System.Configuration.ConfigurationManager.AppSettings["RutaArchivosTerreno"]);
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
                        idcarga = bcp.VisitaTerreno.InsertarVisitaTerrenoCarga(nombreArchivo, user);
                    }
                    if (idcarga > 0)
                    {
                        for (int j = 1; j < ds.Tables[0].Rows.Count; j++)
                        {
                            DataRow dr = ds.Tables[0].Rows[j];
                            //int solicitudId = 0;
                            //string rut = string.Empty;
                            //string[] ids = dr["Cliente"].ToString().Split('-'); // 49-761711547-CENTRO ESTRUCTURAL AL SERVICIO DE LA MINERIA LTDA
                            //solicitudId = Int32.Parse(ids[0]);
                            //rut = ids[1];
                            //string fileName = solicitudId + "_" + rut + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_") + ".jpg";
                            //string path = ConfigurationManager.AppSettings["RutaImagenes"];

                            //Funciones objFunc = new Funciones();
                            //objFunc.CreaCarpetas(path);

                            //string foto1 = dr["Foto 1"].ToString();
                            //Image image = bcp.VisitaTerreno.Base64ToImage(foto1);
                            //image.Save(path + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            //var address = dr["Nueva Dirección 1"].ToString() + ", " + dr["Comuna 1"].ToString() + ", " + "Chile";
                            //GoogleGeoCodeResponse geolocalizacion = new GoogleGeoCodeResponse();
                            //geolocalizacion = bcp.VisitaTerreno.obtenerGeolocalizacion(address);

                            //string latitud = geolocalizacion.results[0].geometry.location.lat;
                            //string longitud = geolocalizacion.results[0].geometry.location.lng;
                            //string ciudad = "Santiago";
                            //foreach (var addressComponent in geolocalizacion.results[0].address_components)
                            //{
                            //    if (addressComponent.types[0] == "administrative_area_level_2")
                            //    {
                            //        ciudad = addressComponent.long_name;
                            //    }
                            //}

                            bcp.VisitaTerreno.InsertarVisitaTerrenoFormulario(dr["Fecha"].ToString(), dr["Formulario"].ToString(), dr["Comercial"].ToString(),
                                                                            dr["Cliente"].ToString(), dr["Posición"].ToString(), dr["Dirección"].ToString(),
                                                                            dr["Foto 1"].ToString(), dr["Foto 2"].ToString(), dr["Foto 3"].ToString(),
                                                                            dr["Foto 4"].ToString(), dr["Estado"].ToString(), dr["Visita"].ToString(),
                                                                            dr["Dirección Actual"].ToString(), dr["Comentarios"].ToString(), dr["Nueva Dirección 1"].ToString(),
                                                                            dr["Comuna 1"].ToString(), dr["Nueva Dirección 2"].ToString(), dr["Comuna 2"].ToString(), user, idcarga, string.Empty);

                        }

                        lst = bcp.VisitaTerreno.ListarVisitaTerrenoFormulariosCarga(idcarga, "N");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception(ex.Message);
            }
            return lst;
        }
        public static List<DatosCargaVisitaTerreno> ProcesoCargaTerreno(List<dto.DatosCargaVisitaTerreno> lst, UserSession objSession)
        {
           
            try
            {
                if (lst.Count > 0)
                {
                    foreach (dto.DatosCargaVisitaTerreno formulario in lst)
                    {
                        string msgError = string.Empty;
                        int salidaSolicitud = 0;
                        int solicitudId = 0;
                        string rut = string.Empty;
                        string[] ids = formulario.Deudor.Split('-'); // 49-761711547-CENTRO ESTRUCTURAL AL SERVICIO DE LA MINERIA LTDA
                        solicitudId = Int32.Parse(ids[0]);
                        rut = ids[1];
                        int idEstadoVisita = 0;
                        List<Combobox> lstTipoEstadoVisita = dao.VisitaTerreno.ListarVisitaTerrenoTipoEstadoVisita();
                        Combobox tipoEstadoVisita = new Combobox();
                        tipoEstadoVisita = lstTipoEstadoVisita.Find(x => x.Text == formulario.EstadoVisita);
                        idEstadoVisita = tipoEstadoVisita == null ? 1 : Int32.Parse(tipoEstadoVisita.Value);
                        string visita = "P";
                        visita = formulario.Visita.Contains("Positiva") ? "P": "N";
                        string direccionActual = "S";
                        direccionActual = formulario.DireccionActual.Contains("Si") ? "S" : "N";
                        using (TransactionScope scope = new TransactionScope())
                        {
                            //Actualiza la solicitud a estatus recibida
                            //crea historial
                            if (solicitudId > 0)
                            {
                                salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudStatus(solicitudId, objSession.UserId, 5);
                                //actualiza estatus
                                if (salidaSolicitud > 0)
                                {
                                    salidaSolicitud = dao.VisitaTerreno.ActualizarVisitaTerrenoSolicitud(solicitudId, 5, objSession.UserId, "S");
                                }
                                else
                                {
                                    msgError = msgError + "Error al actualizar el estatus. ";
                                }
                                //Inserta detalle de la visita
                                if (salidaSolicitud > 0)
                                {
                                    salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoDetalle(solicitudId, idEstadoVisita, visita, formulario.Comentarios, direccionActual, formulario.FechaVisita, formulario.Direccion, formulario.Posicion);
                                }
                                else
                                {
                                    msgError = msgError + "Error al crear el detalle de la visita. ";
                                }
                                //Inserta detalle de la visita en Nuevas direcciones
                                if (salidaSolicitud > 0)
                                {
                                    if (!String.IsNullOrEmpty(formulario.Direccion1) && !String.IsNullOrEmpty(formulario.Comuna1))
                                    {
                                        dao.VisitaTerreno.InsertarVisitaTerrenoNuevasDirecciones(solicitudId, salidaSolicitud, formulario.Direccion1, formulario.Comuna1, "Santiago", "0", "0" );
                                    }
                                    if (!String.IsNullOrEmpty(formulario.Direccion2) && !String.IsNullOrEmpty(formulario.Comuna2))
                                    {
                                        dao.VisitaTerreno.InsertarVisitaTerrenoNuevasDirecciones(solicitudId, salidaSolicitud, formulario.Direccion2, formulario.Comuna2, "Santiago", "0", "0");
                                    }

                                }
                                if (salidaSolicitud > 0)
                                {
                                    salidaSolicitud = dao.VisitaTerreno.InsertarFormularioSolicitud(formulario.FormularioId, solicitudId);
                                }
                                if (salidaSolicitud > 0)
                                {
                                    dao.VisitaTerreno.procesarFormulario(formulario.FormularioId, "S");
                                    scope.Complete();
                                    formulario.Procesado = "S";
                                    formulario.mensaje = "EL registro fue registrado con exito";
                                }
                                else
                                {
                                    formulario.mensaje = "Ha ocurrido un error al procesar el registro";
                                }
                            }
                            else
                            {
                                msgError = msgError + "El Formulario no tiene Identificador de solicitud. ";
                            }

                            if (!(string.IsNullOrEmpty(msgError)))
                            {
                                formulario.mensaje = msgError;
                            }

                        }
                                            
                    }
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
    }
}
