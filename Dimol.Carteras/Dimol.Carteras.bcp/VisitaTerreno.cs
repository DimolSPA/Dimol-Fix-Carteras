using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Carteras.dto;
using Dimol.dto;
using System.Transactions;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Net.Http.Headers;
using System.Globalization;
using System.Threading;

namespace Dimol.Carteras.bcp
{
    public class VisitaTerreno
    {
        #region "Visita Terreno"
        public static List<dto.VisitaTerrenoDetalle> ListarVisitaTerreno(int ctcid)
        {
            return dao.VisitaTerreno.ListarVisitaTerreno(ctcid);
        }

        public static List<dto.VisitaTerrenoGPS> ListarVisitaTerrenoGPS(int idVisita, int idVisitaDetalle)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoGPS(idVisita, idVisitaDetalle);
        }

        public static string ListarVisitasTerrenoFotos(int idVisita, int idVisitaDetalle)
        {
            string salida = "";
            string[] nombreArchivo;
            List<Combobox> lstFotos = dao.VisitaTerreno.ListarVisitasTerrenoFotos(idVisita, idVisitaDetalle);
            if (lstFotos.Count > 0)
            {
                foreach (Combobox obj in lstFotos)
                {
                    nombreArchivo = obj.Value.Split('\\');
                    salida += "<li><a href=\"#\"><img src=\"" + System.Configuration.ConfigurationManager.AppSettings["UrlImagenesTerreno"] + nombreArchivo[nombreArchivo.Length - 1] + "\" data-large=\"" + System.Configuration.ConfigurationManager.AppSettings["UrlImagenesTerreno"] + "/" + nombreArchivo[nombreArchivo.Length - 1] + "\" alt=\"" + obj.Text + "\" title=\"" + obj.Text + "\" data-description=\"" + obj.Text + "\" onContextMenu=\"return false;\" /></a></li>";
                }
            }
            else
            {
                salida = "";
            }


            return salida;
        }
        public static List<dto.VisitaTerrenoTelefono> ListarVisitaTerrenoTelefonos(int idVisita, int idVisitaDetalle)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoTelefonos(idVisita, idVisitaDetalle);
        }

        public static int InsertarVisitaTerrenoSolicitud(int codemp, int ctcid, string direccion,
                                                        int idRegion, int idCiudad, int idComuna,
                                                        string comuna, int userId, UserSession objSesion)
        {
            int salidaSolicitud = 0;
            int gestion = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoSolicitud(codemp, ctcid, direccion, idRegion, idCiudad, idComuna, comuna, userId);

                if (salidaSolicitud >= 0)
                {
                    salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudStatus(salidaSolicitud, userId, 1);
                }

                if (salidaSolicitud >= 0)
                {
                    salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerreno(salidaSolicitud, ctcid, userId);
                }

                if (salidaSolicitud >= 0)
                {
                    List<int> Clientelist = new List<int>();
                    Clientelist = dao.VisitaTerreno.ListarClientesDeudor(codemp, ctcid);
                    foreach (int cliente in Clientelist)
                    {
                        //Grabo la accion
                        gestion = bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(codemp, cliente, ctcid, 3, objSesion.CodigoSucursal, 0, "N", objSesion.IpRed, objSesion.IpPc, objSesion.UserId, "Se Solicita Visita Terreno a " + direccion, 0, Int64.Parse("0"));
                    }
                }

                if (salidaSolicitud >= 0)
                {
                    scope.Complete();
                }
            }

            return salidaSolicitud;
        }

        public static int InsertarVisitaTerrenoSolicitudCoordenadas(int solicitudId, string altitud, string longitud)
        {
            int salidaSolicitud = 0;

            salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudCoordenadas(solicitudId, altitud, longitud);

            return salidaSolicitud;
        }

        public static List<dto.VisitaTerrenoSolicitudAceptar> ListarVisitaTerrenoSolicitudAprobar(int codemp, int pclid, int regionId, int ciudadId, int comunaId,
                                                                            decimal monto, bool enQuiebra, bool enSolicitud, bool enPreQuiebra, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoSolicitudAprobar(codemp, pclid, regionId, ciudadId, comunaId, monto, enQuiebra, enSolicitud, enPreQuiebra, where, sidx, sord, inicio, limite);
        }

        public static int ListarVisitaTerrenoSolicitudCount(int codemp, int pclid, int regionId, int ciudadId, int comunaId,
                                                                           decimal monto, bool enQuiebra, bool enSolicitud, bool enPreQuiebra, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoSolicitudAprobarCount(codemp, pclid, regionId, ciudadId, comunaId, monto, enQuiebra, enSolicitud, enPreQuiebra, where, sidx, sord, inicio, limite);
        }

        public static List<dto.VisitaTerrenoSolicitudAceptar> GrabarAceptarVisitas(List<dto.VisitaTerrenoSolicitudAceptar> lst, int codemp, int userId, int estado,
                                                                                    int gestorId, string gestor, string telefonoImei,
                                                                                   string telefonoNum, string visitada, UserSession objSesion)
        {
            List<dto.VisitaTerrenoSolicitudAceptar> salida = new List<dto.VisitaTerrenoSolicitudAceptar>();
            int salidaSolicitud = 0;
            int gestion = 0;
            foreach (dto.VisitaTerrenoSolicitudAceptar c in lst)
            {
                //Si es 0 es una nueva solicitud de visita
                if (c.solicitudId == 0)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //Crea la solicitud

                        salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoSolicitud(codemp, c.ctcid, c.direccion, c.regId, c.ciuId, c.comId, c.comuna, userId);

                        if (salidaSolicitud >= 0)
                        {
                            //asigna gestor de terreno
                            salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudGestor(salidaSolicitud, gestorId, gestor, telefonoImei, telefonoNum, userId);
                        }

                        if (salidaSolicitud >= 0)
                        {
                            //crea historial
                            salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudStatus(salidaSolicitud, userId, 1);
                        }

                        if (salidaSolicitud >= 0)
                        {
                            salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerreno(salidaSolicitud, c.ctcid, userId);
                        }

                        if (salidaSolicitud >= 0)
                        {
                            List<int> Clientelist = new List<int>();
                            Clientelist = dao.VisitaTerreno.ListarClientesDeudor(codemp, c.ctcid);
                            foreach (int cliente in Clientelist)
                            {
                                //Grabo la accion
                                gestion = bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(codemp, cliente, c.ctcid, 3, objSesion.CodigoSucursal, 0, "N", objSesion.IpRed, objSesion.IpPc, objSesion.UserId, "Se Solicita Visita Terreno a " + c.direccion, 0, Int64.Parse("0"));
                            }
                        
                        }

                        if (salidaSolicitud >= 0)
                        {
                            scope.Complete();
                            salida.Add(new dto.VisitaTerrenoSolicitudAceptar() { solicitudId = salidaSolicitud, direccion = c.direccion, comuna = c.comuna });
                        }


                    }

                    //Actualiza la solicitud a estatus aprobada
                    if (salidaSolicitud >= 0)
                    {
                        //crea historial
                        salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudStatus(salidaSolicitud, userId, estado);
                        //actualiza estatus
                        salidaSolicitud = dao.VisitaTerreno.ActualizarVisitaTerrenoSolicitud(salidaSolicitud, estado, userId, visitada);
                    }
                }
                else
                {
                    //crea historial
                    dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudStatus(c.solicitudId, userId, estado);
                    //asigna gestor de terreno
                    dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudGestor(c.solicitudId, gestorId, gestor, telefonoImei, telefonoNum, userId);
                    //actualiza estatus
                    dao.VisitaTerreno.ActualizarVisitaTerrenoSolicitud(c.solicitudId, estado, userId, visitada);

                    salida.Add(new dto.VisitaTerrenoSolicitudAceptar() { solicitudId = c.solicitudId, direccion = "", comuna = "" });
                }


            }

            return salida;
        }

        public static List<Combobox> GrabarRechazarVisitas(List<dto.VisitaTerrenoSolicitudAceptar> lst, int codemp, int userId, int estado, string visitada)
        {
            List<Combobox> salida = new List<Combobox>();

            foreach (dto.VisitaTerrenoSolicitudAceptar c in lst)
            {
                //Si es 0 no tiene solicitud de visita
                if (c.solicitudId != 0)
                {
                    //crea historial
                    dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudStatus(c.solicitudId, userId, estado);
                    //actualiza estatus
                    dao.VisitaTerreno.ActualizarVisitaTerrenoSolicitud(c.solicitudId, estado, userId, visitada);
                }


            }

            return salida;
        }

        public static List<dto.VisitaTerrenoGenerar> ListarVisitaTerrenoGenerar(int gesId, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoGenerar(gesId,where, sidx, sord, inicio, limite);
        }

        public static int ListarVisitaTerrenoGenerarCount(int gesId, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoGenerarCount(gesId,where, sidx, sord, inicio, limite);
        }

        public static int VisitaTerrenoSolicitudGenerar(int solicitudId, int estado, int userId, string clientepointId,
                                                        int gestorId, string telefonoImei, string visitada)
        {
            int salidaSolicitud = -1;

            //crea historial
            salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoSolicitudStatus(solicitudId, userId, estado);
            //actualiza estatus
            if (salidaSolicitud > 0)
            {
                salidaSolicitud = dao.VisitaTerreno.ActualizarVisitaTerrenoSolicitud(solicitudId, estado, userId, visitada);
            }
            //crea punto id cliente en Geogestion
            if ((salidaSolicitud > 0) && (!string.IsNullOrEmpty(clientepointId)))
            {
                salidaSolicitud = dao.VisitaTerreno.InsertarVisitaTerrenoClientePointGestor(clientepointId, solicitudId, gestorId, telefonoImei, userId);
            }

            return salidaSolicitud;
        }
        public static List<String> NotificarTerrenoColor(int ctcid)
        {
            List<string> resultados = new List<string>();

            resultados = dao.VisitaTerreno.NotificarTerrenoColor(ctcid);

            return resultados;
        }

        public static List<dto.VisitaTerrenoCarteraGestorGeoGestion> ListarVisitaTerrenoCarteraGeoGestionGestor(string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoCarteraGeoGestionGestor(where, sidx, sord, inicio, limite);
        }

        public static int ListarVisitaTerrenoCarteraGeoGestionGestorCount(string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoCarteraGeoGestionGestorCount(where, sidx, sord, inicio, limite);
        }
        public static int InsertarVisitaTerrenoCarteraGestor(string carteraId, int gestorId, string carteraNombre,
                                                            string carteraDescripcion, int userId)
        {
            int result = -1;

            result = dao.VisitaTerreno.InsertarVisitaTerrenoCarteraGestor(carteraId, gestorId, carteraNombre, carteraDescripcion, userId);

            return result;
        }
        public static int CountVisitaTerrenoCarteraGestor(int gestorId)
        {
            int result = -1;

            result = dao.VisitaTerreno.CountVisitaTerrenoCarteraGestor(gestorId);

            return result;
        }
        public static string getPassGeoGestion()
        {
            string result = "0";

            result = dao.VisitaTerreno.getPassGeoGestion();

            return result;
        }
        public static string getUserGeoGestion()
        {
            string result = "0";

            result = dao.VisitaTerreno.getUserGeoGestion();

            return result;
        }
        public static int InsertarVisitaTerrenoFormulario(string fechaFormulario, string formularioNombre, string gestor,
                                                        string cliente, string posicion, string direccion,
                                                        string foto1, string foto2, string foto3, string foto4,
                                                        string estado, string visita, string direccionActual, string comentarios,
                                                        string direccion1, string comuna1, string direccion2, string comuna2,
                                                        int userId, int idCarga, string formId)
        {
            int result = -1;

            result = dao.VisitaTerreno.InsertarVisitaTerrenoFormulario(fechaFormulario, formularioNombre, gestor, cliente, posicion,
                                                                        direccion, foto1, foto2, foto3, foto4, estado, visita, direccionActual,
                                                                        comentarios, direccion1, comuna1, direccion2, comuna2, userId, idCarga, formId);

            return result;
        }
        public static int InsertarVisitaTerrenoCarga(string archivo, int userId)
        {
            int result = -1;

            result = dao.VisitaTerreno.InsertarVisitaTerrenoCarga(archivo, userId);

            return result;
        }
        public static List<dto.DatosCargaVisitaTerreno> ListarVisitaTerrenoFormulariosCarga(int idCarga, string procesado)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoFormulariosCarga(idCarga, procesado);
        }

        public static string InsertarVisitaTerrenoFormularioFormId(string formId)
        {
            string result = "-1";

            result = dao.VisitaTerreno.InsertarVisitaTerrenoFormularioFormId(formId);

            return result;
        }
        public static int InsertarVisitaTerrenoDetalle(int solicitudId, int estadoVisita,
                                                   string visita, string comentarios, string direccionActual,
                                                    DateTime fecEnvia, string direccionEnvia, string posicionEnvio)
        {
            int result = -1;
            int resultGestion = -1;
            result = dao.VisitaTerreno.InsertarVisitaTerrenoDetalle(solicitudId, estadoVisita, visita, comentarios, direccionActual, fecEnvia, direccionEnvia, posicionEnvio);

            if (result >= 0)
            {
                List<Tuple<int, int>> Clientelist = new List<Tuple<int, int>>();
                Clientelist = dao.VisitaTerreno.ListarClientesDeudorVisita(1, solicitudId);
                foreach (var cliente in Clientelist)
                {
                    //Grabo la accion
                    resultGestion = bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(1, cliente.Item1, cliente.Item2, 3, 1, 0, "N", "10.0.1.15", "10.0.1.219", 269, comentarios, 0, Int64.Parse("0"));
                }
            }
            
            return result;
        }
        public static int InsertarVisitaTerrenoNuevasDirecciones(int solicitudId, int visitaDetalleId, string direccion,
                                                       string comuna, string ciudad, string altitud, string longitud)
        {
            int result = -1;
            result = dao.VisitaTerreno.InsertarVisitaTerrenoNuevasDirecciones(solicitudId, visitaDetalleId, direccion, comuna, ciudad, altitud, longitud);
            return result;
        }

        public static int InsertarVisitaTerrenoFormularioFotos(int solicitudId, int visitaDetalleId, string rutaImageGeo, string pathImage)
        {
            int result = -1;
            result = dao.VisitaTerreno.InsertarVisitaTerrenoFormularioFotos(solicitudId, visitaDetalleId, rutaImageGeo, pathImage);
            return result;
        }
        public static int InsertarFormularioSolicitud(int formularioId, int solicitudId)
        {
            int result = -1;
            result = dao.VisitaTerreno.InsertarFormularioSolicitud(formularioId, solicitudId);
            return result;
        }
        public static int procesarFormulario(int formularioId, string procesado)
        {
            int result = -1;
            result = dao.VisitaTerreno.procesarFormulario(formularioId, procesado);
            return result;
        }
        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Image image;
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                image = Image.FromStream(ms, true);
            }

            return image;
        }
        public static GoogleGeoCodeResponse obtenerGeolocalizacion(string address)
        {
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false", Uri.EscapeDataString(address));
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(requestUri).Result;
            var jsonData = response.Content.ReadAsStringAsync().Result;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
           
            GoogleGeoCodeResponse result = serializer.Deserialize<GoogleGeoCodeResponse>(jsonData);
            return result;
        }
        public static Tuple<string, string> CrearEnviarVisitasTerreno(List<String> ids, string user, string pass, string gestorId, string gestorNombre, int userId)
        {
            string error = string.Empty;
            string msgsuccessEnviarTerreno = string.Empty;

            int gesId = Int32.Parse(gestorId.Split('|')[0]);
            string imei = gestorId.Split('|')[1];
            string gestor = gestorNombre.Split('-')[0];
            string telf = gestorNombre.Split('-')[1];
            string carteraId = gestorId.Split('|')[2];
            string[] splitted;
            for (var i = 0; i < ids.Count; i++)
            {
                splitted = ids[i].Split('|');
                if (splitted[1] != "0")
                {
                    int solicitudId = Int32.Parse(splitted[4]);
                    dto.ClientPoi clientPoi = new dto.ClientPoi();
                    clientPoi.user = user; //Usuario para acceder al servicio web 
                    clientPoi.pass = pass; //Contraseña del usuario para acceder al servicio web 
                    clientPoi.name = splitted[4] + "-" + splitted[0]; //Nombre del cliente 
                    clientPoi.address = splitted[3];//Dirección del cliente 
                    clientPoi.phone = string.Empty; //Teléfono del cliente 
                    clientPoi.mail = string.Empty; //Correo del cliente 
                    clientPoi.description = string.Empty; //Descripción del cliente 
                    clientPoi.radius = 100; //Radio de influencia 
                    clientPoi.imei = imei; //Identificación del asesor 
                    clientPoi.layer_id = carteraId; //Identificador de la cartera de cliente 
                    clientPoi.latitude = Double.Parse(splitted[1].Replace(",", "."), CultureInfo.InvariantCulture); //Latitud donde se encuentra el cliente 
                    clientPoi.longitude = Double.Parse(splitted[2].Replace(",", "."), CultureInfo.InvariantCulture); //Longitud donde se encuentra el cliente
                    
                    //asignamos el deudor al gestor, creando como cliente en GeoGestion
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var toBeSend = serializer.Serialize(clientPoi);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(toBeSend);
                    var content = new ByteArrayContent(messageBytes);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    HttpResponseMessage responseMessage = client.PostAsync("https://apigeogestion.erictelm2m.com:7443/newClientPoi", content).Result;
                    Thread.Sleep(1000);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Thread.Sleep(1000);
                        var jsonData = responseMessage.Content.ReadAsStringAsync().Result;
                        dto.ClientPoiID clientPoiID = serializer.Deserialize<dto.ClientPoiID>(jsonData);
                        //Guardamos el id generado por GeoGestion en VISITA_TERRENO_CLIENTEPOINT_GESTOR
                        int salida = bcp.VisitaTerreno.VisitaTerrenoSolicitudGenerar(solicitudId, 4, userId,
                                                                        clientPoiID.id, gesId, imei, "N");
                        if (salida != -1)
                        {
                            msgsuccessEnviarTerreno = "Informacion Enviada con exito";
                        }
                    }
                    else
                    {
                        if (responseMessage.StatusCode == System.Net.HttpStatusCode.Forbidden)
                        {
                            msgsuccessEnviarTerreno = "Ha ocurrido un problema interno, el imei o cartera no corresponden";
                        }
                        else
                        {
                            Dimol.dao.Funciones.InsertarError(responseMessage.Content.ReadAsStringAsync().Result, "Cartera/CrearEnviarVisitasTerreno", "Cartera/CrearEnviarVisitasTerreno", 120);
                            msgsuccessEnviarTerreno = "Ha ocurrido un problema interno, no se puso generar la visita en GeoGestión";
                        }
                       
                    }
                }
                else
                {
                    error = "Verifique la latitud y longitud de direcciones que intenta generar";
                }
            }
            return Tuple.Create(error, msgsuccessEnviarTerreno);
        }
        public static string crearClientLayerCarteraTerrenoVisitaGestor(int gestorId, string imei, string carteraNombre, string carteraDescripcion, string userGeo, string passGeo, int userId){
            string message = string.Empty;
            int salida = -1;
            int result = -1;
            salida = CountVisitaTerrenoCarteraGestor(gestorId);//Validamos que el gestor tenga una sola caretera creada
            if (salida == -1)
            {
                message = "Ha ocurrido un problema al consultar las carteras";
            }
            else
            {
                if (salida > 0)
                {
                    message = "La Cartera no puede ser creada, El Gestor ya tiene una cartera";
                }
                else
                {
                    if (salida == 0)
                    {
                        string idCartera = string.Empty;
                        dto.ClientLayer clientLayer = new dto.ClientLayer();
                        clientLayer.user = userGeo;
                        clientLayer.pass = passGeo;
                        clientLayer.imei = imei;
                        clientLayer.name = carteraNombre;
                        clientLayer.description = carteraDescripcion;
                        //Creamos la cartera en GeoGestion
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.BaseAddress = new Uri("https://apigeogestion.erictelm2m.com:7443/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage responseMessage = client.PostAsJsonAsync("newClientLayer", clientLayer).Result;
                        Thread.Sleep(1000);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            Thread.Sleep(1000);
                            //Se obtienen las carteras creadas
                            dto.PostClientLayersGestor postClientLayersGestor = new dto.PostClientLayersGestor();
                            postClientLayersGestor.user = userGeo;
                            postClientLayersGestor.pass = passGeo;
                            postClientLayersGestor.imei = imei;
                            HttpResponseMessage response = client.PostAsJsonAsync("getClientLayers", postClientLayersGestor).Result;
                            Thread.Sleep(1000);
                            if (response.IsSuccessStatusCode)
                            {
                                Thread.Sleep(1000);
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                var jsonData = response.Content.ReadAsStringAsync().Result;
                                List<ClientLayersGestor> listCarteras = serializer.Deserialize<List<ClientLayersGestor>>(jsonData);
                                for (var i = 0; i < listCarteras.Count; i++)
                                {
                                    //Obtenemos la primera cartera creada
                                    if (i == 0)
                                    {
                                        idCartera = listCarteras[i].id;
                                        break;
                                    }
                                }
                                //Guardar cartera en VISITA_TERRENO_CARTERA_GESTOR
                                result = InsertarVisitaTerrenoCarteraGestor(idCartera, gestorId, carteraNombre, carteraDescripcion, userId);
                                if (result > 0)
                                {
                                    message = "Cartera guardada con exito";

                                }
                                if (result == -2)
                                {
                                   message = "La Cartera no puede ser creada, El Gestor ya posee una cartera";
                                }
                                else
                                {
                                    if (result == -1)
                                    {
                                        message = "Error al guardar la cartera";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Forbidden)
                            {
                                message = "Ha ocurrido un problema interno, el imei no corresponde";
                            }
                            else
                            {
                                Dimol.dao.Funciones.InsertarError(responseMessage.Content.ReadAsStringAsync().Result, "Cartera.bcp.VisitaTerreno/crearClientLayerCarteraTerrenoVisitaGestor", "Cartera.bcp.VisitaTerreno/crearClientLayerCarteraTerrenoVisitaGestor", 0);
                                message = "Ha ocurrido un problema interno, no se puso crear la cartera en GeoGestión";
                            }
                            
                        }
                    }
                }
            }
            return message;
        }
        public static int ExisteSolicitud(int solicitudId)
        {
            int result = -1;
            result = dao.VisitaTerreno.ExisteSolicitud(solicitudId);
            return result;
        }
        public static List<dto.DatosCargaVisitaTerreno> ListarVisitaTerrenoFormulariosById(string formId)
        {
            return dao.VisitaTerreno.ListarVisitaTerrenoFormulariosById(formId);
        }
        public static int actualizarFormulario(string formId)
        {
            int result = -1;
            result = dao.VisitaTerreno.actualizarFormulario(formId);
            return result;
        }
        #endregion
    }
}
