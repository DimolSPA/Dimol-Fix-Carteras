using Dimol.ApiServices.Models;
using Dimol.bcp;
using Dimol.Carteras.bcp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;

namespace Dimol.ApiServices.Controllers
{
    public class FormsController : ApiController
    {
        //[HttpPost]
        //public HttpResponseMessage Post([FromBody]string formId)
        [HttpPost]
        [ActionName("EnviarFormId")]
        public async Task<HttpResponseMessage> PostEnviarFormId([FromBody] FormularioID value)
        {
           // Save formId
           
            try
            {
                if (value.formId != null)
                {
                    string result = "-1";
                    result = Dimol.Carteras.bcp.VisitaTerreno.InsertarVisitaTerrenoFormularioFormId(value.formId);
                    if (result != "-1")
                    {
                        //descargar informacion del formulario
                    var resultGetExternal = await GetExternalResponse(value.formId);
                    }
                    
                    
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        
       
        private async Task<string> GetExternalResponse(string formId)
        {
            PostObtenerFormId datos = new PostObtenerFormId();
            datos.user = VisitaTerreno.getUserGeoGestion();
            datos.pass = VisitaTerreno.getPassGeoGestion();
            datos.formId = formId;
            //Newtonsoft.Json.JsonConvert.SerializeObject(datos); //JSONSerializer.Serialize(
            var toBeSend = JsonConvert.SerializeObject(datos,
                               new JsonSerializerSettings());
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(toBeSend);
            var content = new ByteArrayContent(messageBytes);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync("https://apigeogestion.erictelm2m.com:7443/getForm", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                //Encabezado
                FormObject formulario = JsonConvert.DeserializeObject<FormObject>(jsonData);
                string clienteForm = formulario.poiName;
                string gestorForm = formulario.device;
                string nomFormulario = formulario.name;
                double timestamp = Convert.ToDouble(formulario.ts);
                System.DateTime dateFecEnvioForm = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                dateFecEnvioForm = dateFecEnvioForm.AddSeconds(timestamp);
                string direccionForm = string.Empty;
                if (formulario.address != null)
                {
                    direccionForm = formulario.address;
                }
                string posicionForm = string.Empty;
                if (formulario.coordinates != null)
                {
                    posicionForm = formulario.coordinates.lat.ToString().Replace(",",".") + ", " + formulario.coordinates.lon.ToString().Replace(",",".");
                }
                
                string Direccion1Form = string.Empty;
                string Direccion2Form = string.Empty;
                string Comuna1Form = string.Empty;
                string Comuna2Form = string.Empty;
                //Detalles
                string comentarios = string.Empty;
                string estadoVisitaForm = "Con Moradores";
                string visitaForm = "Positiva";
                string direccionActualForm = "Si";
                if (!string.IsNullOrEmpty(formulario.form))
                {
                    FormItemObject formItem = JsonConvert.DeserializeObject<FormItemObject>(formulario.form);
                    for (var i = 0; i < formItem.fields.Count; i++)
                    {
                        if (formItem.fields[i].field_type == "radio")
                        {
                            RadioObject formItemRadio = JsonConvert.DeserializeObject<RadioObject>(formItem.fields[i].field_options.ToString());
                            if (formItem.fields[i].label.Trim() == "Estado")
                            {
                                for (var x = 0; x < formItemRadio.options.Count; x++)
                                {
                                    if (formItemRadio.options[x].@checked == true)
                                    {
                                        estadoVisitaForm = formItemRadio.options[x].label;
                                    }
                                }
                            }
                            if (formItem.fields[i].label.Trim() == "Visita")
                            {
                                for (var x = 0; x < formItemRadio.options.Count; x++)
                                {
                                    if (formItemRadio.options[x].@checked == true)
                                    {
                                        visitaForm = formItemRadio.options[x].label;
                                    }
                                }
                            }
                            if (formItem.fields[i].label.Trim() == "Dirección Actual")
                            {
                                for (var x = 0; x < formItemRadio.options.Count; x++)
                                {
                                    if (formItemRadio.options[x].@checked == true)
                                    {
                                        direccionActualForm = formItemRadio.options[x].label;
                                    }
                                }
                            }
                        }
                        if (formItem.fields[i].field_type == "paragraph")
                        {
                            if (formItem.fields[i].value != null)
                            {
                                comentarios = formItem.fields[i].value.ToString();
                            }
                        }
                        if (formItem.fields[i].field_type == "text")
                        {
                            if (formItem.fields[i].label.Trim() == "Nueva Dirección 1" || formItem.fields[i].cid == "c40")
                            {
                                if (formItem.fields[i].value != null)
                                {
                                    Direccion1Form = formItem.fields[i].value.ToString();
                                }
                            }
                            if (formItem.fields[i].label.Trim() == "Comuna 1" || formItem.fields[i].cid == "c49")
                            {
                                if (formItem.fields[i].value != null)
                                {
                                    Comuna1Form = formItem.fields[i].value.ToString();
                                }
                            }
                            if (formItem.fields[i].label.Trim() == "Nueva Dirección 2" || formItem.fields[i].cid == "c53")
                            {
                                if (formItem.fields[i].value != null)
                                {
                                    Direccion2Form = formItem.fields[i].value.ToString();
                                }
                            }
                            if (formItem.fields[i].label.Trim() == "Comuna 2" || formItem.fields[i].cid == "c57")
                            {
                                if (formItem.fields[i].value != null)
                                {
                                    Comuna2Form = formItem.fields[i].value.ToString();
                                }
                            }
                        }
                    }
                }
                int insertar = Dimol.Carteras.bcp.VisitaTerreno.InsertarVisitaTerrenoFormulario(dateFecEnvioForm.ToString(), nomFormulario, gestorForm, clienteForm, posicionForm,
                                                                                   direccionForm, string.Empty, string.Empty, string.Empty, string.Empty, estadoVisitaForm, visitaForm,
                                                                                   direccionActualForm, comentarios, Direccion1Form, Comuna1Form, Direccion2Form, Comuna2Form,
                                                                                   269, 0, formId);
                if (insertar >= 0)
                {
                    List<Dimol.Carteras.dto.DatosCargaVisitaTerreno> lst = Dimol.Carteras.bcp.VisitaTerreno.ListarVisitaTerrenoFormulariosById(formId);
                    if (lst.Count > 0)
                    {
                        foreach (Dimol.Carteras.dto.DatosCargaVisitaTerreno apiForm in lst)
                        {
                            string msgError = string.Empty;
                            int salidaSolicitud = 0;
                            int solicitudId = 0;
                            string rut = string.Empty;
                            if (!string.IsNullOrEmpty(apiForm.Deudor))
                            {
                                string[] ids = apiForm.Deudor.Split('-');
                                if (ids.Length > 2)
                                {
                                    solicitudId = Int32.Parse(ids[0]);
                                    rut = ids[1];
                                }
                            }
                            int idEstadoVisita = 0;
                            List<Dimol.dto.Combobox> lstTipoEstadoVisita = Dimol.Carteras.dao.VisitaTerreno.ListarVisitaTerrenoTipoEstadoVisita();
                            Dimol.dto.Combobox tipoEstadoVisita = new Dimol.dto.Combobox();
                            tipoEstadoVisita = lstTipoEstadoVisita.Find(x => x.Text == apiForm.EstadoVisita);
                            idEstadoVisita = tipoEstadoVisita == null ? 1 : Int32.Parse(tipoEstadoVisita.Value);
                            string visita = "P";
                            visita = apiForm.Visita.Contains("Positiva") ? "P" : "N";
                            string direccionActual = "S";
                            direccionActual = apiForm.DireccionActual.Contains("Si") ? "S" : "N";
                            solicitudId = Dimol.Carteras.bcp.VisitaTerreno.ExisteSolicitud(solicitudId);
                            using (TransactionScope scope = new TransactionScope())
                            {
                                //Actualiza la solicitud a estatus recibida
                                //crea historial
                                if (solicitudId > 0)
                                {
                                    salidaSolicitud = Dimol.Carteras.dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudStatus(solicitudId, 269, 5);
                                    //actualiza estatus
                                    if (salidaSolicitud > 0)
                                    {
                                        salidaSolicitud = Dimol.Carteras.dao.VisitaTerreno.ActualizarVisitaTerrenoSolicitud(solicitudId, 5, 269, "S");
                                    }
                                    else
                                    {
                                        msgError = msgError + "Error al actualizar el estatus. ";
                                    }
                                    //Inserta detalle de la visita
                                    if (salidaSolicitud > 0)
                                    {
                                        salidaSolicitud = Dimol.Carteras.bcp.VisitaTerreno.InsertarVisitaTerrenoDetalle(solicitudId, idEstadoVisita, visita, apiForm.Comentarios, direccionActual, apiForm.FechaVisita, apiForm.Direccion, apiForm.Posicion);
                                    }
                                    else
                                    {
                                        msgError = msgError + "Error al crear el detalle de la visita. ";
                                    }
                                    //Inserta detalle de la visita en Nuevas direcciones
                                    if (salidaSolicitud > 0)
                                    {
                                        string latitud = "0";
                                        string longitud = "0";
                                        string ciudad = "Santiago";
                                        if (!String.IsNullOrEmpty(apiForm.Direccion1)) //&& !String.IsNullOrEmpty(apiForm.Comuna1))
                                        {
                                            
                                            Dimol.Carteras.dto.GoogleGeoCodeResponse geolocalizacion = new Dimol.Carteras.dto.GoogleGeoCodeResponse();
                                            geolocalizacion = Dimol.Carteras.bcp.VisitaTerreno.obtenerGeolocalizacion(apiForm.Direccion1 + ", " + apiForm.Comuna1 + ", " + "Chile");
                                            if (geolocalizacion.status == "OK")
                                            {
                                                latitud = geolocalizacion.results[0].geometry.location.lat;
                                                longitud = geolocalizacion.results[0].geometry.location.lng;
                                                foreach (var addressComponent in geolocalizacion.results[0].address_components)
                                                {
                                                    if (addressComponent.types[0] == "administrative_area_level_2")
                                                    {
                                                        ciudad = addressComponent.long_name;
                                                    }
                                                }
                                            }
                                            Dimol.Carteras.bcp.VisitaTerreno.InsertarVisitaTerrenoNuevasDirecciones(solicitudId, salidaSolicitud, apiForm.Direccion1, apiForm.Comuna1, ciudad, latitud, longitud);
                                        }
                                        if (!String.IsNullOrEmpty(apiForm.Direccion2)) //&& !String.IsNullOrEmpty(apiForm.Comuna2))
                                        {
                                            Dimol.Carteras.dto.GoogleGeoCodeResponse geolocalizacion = new Dimol.Carteras.dto.GoogleGeoCodeResponse();
                                            geolocalizacion = Dimol.Carteras.bcp.VisitaTerreno.obtenerGeolocalizacion(apiForm.Direccion2 + ", " + apiForm.Comuna2 + ", " + "Chile");
                                            if (geolocalizacion.status == "OK")
                                            {
                                                latitud = geolocalizacion.results[0].geometry.location.lat;
                                                longitud = geolocalizacion.results[0].geometry.location.lng;
                                                foreach (var addressComponent in geolocalizacion.results[0].address_components)
                                                {
                                                    if (addressComponent.types[0] == "administrative_area_level_2")
                                                    {
                                                        ciudad = addressComponent.long_name;
                                                    }
                                                }
                                            }
                                            Dimol.Carteras.bcp.VisitaTerreno.InsertarVisitaTerrenoNuevasDirecciones(solicitudId, salidaSolicitud, apiForm.Direccion2, apiForm.Comuna2, ciudad, latitud, longitud);
                                        }
                                        
                                        if (formulario.photos != null)
                                        {
                                            //Ingresar fotos
                                            string path = ConfigurationManager.AppSettings["RutaImagenesTerreno"];
                                            Funciones objFunc = new Funciones();
                                            objFunc.CreaCarpetas(path);

                                            string foto1 = formulario.photos.c2;
                                            if (!String.IsNullOrEmpty(foto1))
                                            {
                                                string fileName = solicitudId + "_" + rut + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_1") + ".jpg";
                                                Byte[] img = Convert.FromBase64String(foto1);
                                                System.IO.File.WriteAllBytes(path + fileName, img);
                                                Dimol.Carteras.bcp.VisitaTerreno.InsertarVisitaTerrenoFormularioFotos(solicitudId, salidaSolicitud, string.Empty, fileName);
                                            }
                                            string foto2 = formulario.photos.c6;
                                            if (!String.IsNullOrEmpty(foto2))
                                            {
                                                string fileName = solicitudId + "_" + rut + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_2") + ".jpg";
                                                Byte[] img = Convert.FromBase64String(foto2);
                                                System.IO.File.WriteAllBytes(path + fileName, img);
                                                Dimol.Carteras.bcp.VisitaTerreno.InsertarVisitaTerrenoFormularioFotos(solicitudId, salidaSolicitud, string.Empty, fileName);
                                            }
                                            string foto3 = formulario.photos.c12;
                                            if (!String.IsNullOrEmpty(foto3))
                                            {
                                                string fileName = solicitudId + "_" + rut + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_3") + ".jpg";
                                                Byte[] img = Convert.FromBase64String(foto3);
                                                System.IO.File.WriteAllBytes(path + fileName, img);
                                                Dimol.Carteras.bcp.VisitaTerreno.InsertarVisitaTerrenoFormularioFotos(solicitudId, salidaSolicitud, string.Empty, fileName);
                                            }
                                            string foto4 = formulario.photos.c18;
                                            if (!String.IsNullOrEmpty(foto4))
                                            {
                                                string fileName = solicitudId + "_" + rut + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_4") + ".jpg";
                                                Byte[] img = Convert.FromBase64String(foto4);
                                                System.IO.File.WriteAllBytes(path + fileName, img);
                                                Dimol.Carteras.bcp.VisitaTerreno.InsertarVisitaTerrenoFormularioFotos(solicitudId, salidaSolicitud, string.Empty, fileName);
                                            }
                                        }
                                        //End Ingresar fotos
                                    }
                                    if (salidaSolicitud > 0)
                                    {
                                        salidaSolicitud = Dimol.Carteras.bcp.VisitaTerreno.InsertarFormularioSolicitud(apiForm.FormularioId, solicitudId);
                                    }
                                    if (salidaSolicitud > 0)
                                    {
                                        Dimol.Carteras.bcp.VisitaTerreno.actualizarFormulario(formId);
                                        scope.Complete();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //var result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            
            var result =  await response.Content.ReadAsStringAsync();
            return result;
        }
        
        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Image image;
            using ( MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                image = Image.FromStream(ms, true);
            }

            return image;
        }
    }
}
