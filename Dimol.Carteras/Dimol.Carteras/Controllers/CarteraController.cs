using Dimol.bcp;
using Dimol.Carteras.bcp;
using Dimol.Carteras.Models;
using Dimol.dto;
using Dimol.Reportes.dto;
using Dimol.WindowsService.bcp;
using Mvc.HtmlHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dimol.Carteras.Controllers
{
    public class CarteraController : Dimol.Controllers.BaseController
    {
        // GET: /Carteras/
        Deudor objDeudor = new Deudor();

        #region "Views"
        public ActionResult Index(string tipo, string pag, string r, string cli)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Direccion objDireccion = new Direccion();
            Deudor objDeudor = new Deudor();
            List<Combobox> lstEstadoTodos = objDeudor.ListarEstado(objSession.Idioma);
            List<Combobox> lstEstado = new List<Combobox>();
            ViewBag.ClienteAsociado = objSession.ClienteAsociado;
            ViewBag.Tipo = tipo;
            ViewBag.Pag = pag;
            ViewBag.Cli = cli;

            if (objSession.ClienteAsociado > 0)
            {
                if (!string.IsNullOrEmpty(objSession.EstadosClienteAsociado))
                {
                    lstEstado.Add(new Combobox() { Text = "Seleccione", Value = "" });
                    foreach (string value in objSession.EstadosClienteAsociado.Split(','))
                    {
                        lstEstado.Add(lstEstadoTodos.Find(x => x.Value == value));
                    }
                }
                ViewBag.Pclid = objSession.ClienteAsociado;
                ViewBag.Cliente = objSession.NombreClienteAsociado;
                ViewBag.RutCliente = objSession.RutClienteAsociado;
                ViewBag.NombreClienteBuscar = objSession.NombreClienteAsociado;
            }
            else
            {
                lstEstado = lstEstadoTodos;
            }

            if (objSession.PrfId == 8 && objSession.Permisos == 4)
            {
                ViewBag.CongelarInteres = "S";
            }
            else
            {
                ViewBag.CongelarInteres = "N";
            }

            ViewBag.Pais = new SelectList(objDireccion.ListarPais(), "Value", "Text", objSession.CodPais);
            ViewBag.Region = new SelectList(objDireccion.ListarRegion(objSession.CodPais), "Value", "Text");
            ViewBag.Ciudad = new SelectList(objDireccion.ListarCiudad(0), "Value", "Text");
            ViewBag.Comuna = new SelectList(objDireccion.ListarComuna(0), "Value", "Text");
            ViewBag.EstadoDireccion = new SelectList(objDireccion.ListarEstado(objSession.Idioma), "Value", "Text");
            ViewBag.SituacionCartera = new SelectList(lstEstado, "Value", "Text", "");
            ViewBag.EstadoCPBT = new SelectList(lstEstado, "Value", "Text", tipo);
            ViewBag.GrupoCPBT = new SelectList(objDireccion.ListarEstado(-1), "Value", "Text");
            ViewBag.TipoContacto = objDeudor.ListarTipoContacto(objSession.CodigoEmpresa, objSession.Idioma);

            ViewBag.ListaCpbt = new SelectList(objDireccion.ListarEstado(-1), "Value", "Text");
            ViewBag.ListaTipoImagenesCpbt = new SelectList(objDireccion.ListarEstado(-1), "Value", "Text");

            //Envio Email
            ViewBag.Banco = new SelectList(objDeudor.ListarBancos(objSession.CodigoEmpresa), "Value", "Text");

            List<Combobox> lstGestiones = new List<Combobox>();
            lstGestiones.Add(new Combobox() { Text = "DEUDOR INFORMA PAGADO/ENVIA RESPALDO", Value = "49" });
            lstGestiones.Add(new Combobox() { Text = "RETIRO CHEQUE", Value = "51" });
            lstGestiones.Add(new Combobox() { Text = "INFORMA PAGADO, NO ENVIA RESPALDO", Value = "131" });

            ViewBag.Gestion = new SelectList(lstGestiones, "Value", "Text");
            ViewBag.TipoBanco = new SelectList(objDeudor.ListarTipoBancos(objSession.ClienteAsociado), "Value", "Text");
            ViewBag.CuentaBanco = new SelectList(objDeudor.ListarCuentaTipoBanco(0, objSession.ClienteAsociado), "Value", "Text");

            List<SelectListItem> lstHistorial = new List<SelectListItem>();
            lstHistorial.Add(new SelectListItem
                    {
                        Text = "Ambos",
                        Value = "A",
                        Selected = true
                    });
            lstHistorial.Add(new SelectListItem
                    {
                        Text = "Prejudicial",
                        Value = "P"
                    });
            lstHistorial.Add(new SelectListItem
                    {
                        Text = "Judicial",
                        Value = "J"
                    });

            ViewBag.TipoHistorial = lstHistorial;
            List<Combobox> lstEstadosHistorial = new List<Combobox>();
            lstEstadosHistorial.Add(new Combobox() { Text = "-- Seleccione --", Value = "" });
            //lstEstadosHistorial.AddRange(bcp.Comprobante.ListarEstadosHistorial(objSession.CodigoEmpresa, 3, objSession.Idioma, tipo, "V", objSession.PrfId));
            lstEstadosHistorial.AddRange(bcp.Comprobante.ListarEstadosCobranzaClientePerfil(objSession.CodigoEmpresa, 3, objSession.Idioma, 0, "V", objSession.PrfId));
            ViewBag.TipoGestion = new SelectList(Carteras.bcp.Accion.ListarAcciones(objSession.CodigoEmpresa, objSession.Idioma, "-- Seleccione --"), "Value", "Text");
            ViewBag.Contacto = new SelectList(objDeudor.ListarContactos(objSession.CodigoEmpresa, 0), "Value", "Text");
            ViewBag.Agrupa = new SelectList(Carteras.bcp.Accion.ListarTipoAgrupa(objSession.PrfId, objSession.Idioma), "Value", "Text", 3);
            ViewBag.TipoEstado = new SelectList(lstEstadosHistorial, "Value", "Text");
            ViewBag.TelefonoHistorial = lstHistorial;
            List<SelectListItem> lstResLlamado = new List<SelectListItem>();
            lstResLlamado.Add(new SelectListItem
            {
                Text = "Hablado",
                Value = "1",
                Selected = true
            });
            lstResLlamado.Add(new SelectListItem
            {
                Text = "No Contesta",
                Value = "2"
            });
            lstResLlamado.Add(new SelectListItem
            {
                Text = "Ocupado",
                Value = "3"
            });
            lstResLlamado.Add(new SelectListItem
            {
                Text = "Cortado/Malo",
                Value = "5"
            });

            ViewBag.ResultadoLlamado = lstResLlamado;
            if (string.IsNullOrEmpty(pag))
            {
                pag = "1";
            }
            ViewBag.Reporte = new SelectList(Dimol.Reportes.bcp.Cartera.ListarReportes(Int32.Parse(pag), objSession.Idioma, "Seleccione Reporte"), "Value", "Text", "");

            ViewBag.IdPais = new SelectList(objDireccion.ListarPais(), "Value", "Text", objDeudor.IdPais);
            ViewBag.IdRegion = new SelectList(objDireccion.ListarRegion(objDeudor.IdPais), "Value", "Text", objDeudor.IdRegion);
            ViewBag.IdCiudad = new SelectList(objDireccion.ListarCiudad(objDeudor.IdRegion), "Value", "Text", objDeudor.IdCiudad);
            ViewBag.IdComuna = new SelectList(objDireccion.ListarComuna(objDeudor.IdCiudad), "Value", "Text", objDeudor.IdComuna);
            ViewBag.EstadoDireccion = new SelectList(objDireccion.ListarEstado(objSession.Idioma), "Value", "Text", objDeudor.EstadoDireccion);
            ViewBag.TipoTelefono = new SelectList(objDeudor.ListarTipoTelefonoC(objSession.Idioma), "Value", "Text", "");
            ViewBag.EstadoTelefono = new SelectList(objDeudor.ListarEstadoTelefonoC(objSession.Idioma), "Value", "Text", "");
            List<Combobox> lstTipo = new List<Combobox>();
            lstTipo.Add(new Combobox { Text = "Personal", Value = "P" });
            lstTipo.Add(new Combobox { Text = "Empresa", Value = "E" });
            ViewBag.TipoEmail = new SelectList(lstTipo, "Value", "Text", "P");
            ViewBag.TipoContacto = new SelectList(objDeudor.ListarTipoContactoC(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            List<Combobox> lstEstadoContacto = new List<Combobox>();
            lstEstadoContacto.Add(new Combobox { Text = "Activado", Value = "A" });
            lstEstadoContacto.Add(new Combobox { Text = "Desactivado", Value = "D" });
            ViewBag.EstadoContacto = new SelectList(lstEstadoContacto, "Value", "Text", "A");
            if (!string.IsNullOrEmpty(r))
            {
                ViewBag.Rut = r;
            }

            ViewBag.AccionSitrel = new SelectList(bcp.CargaItau.ListarAcciones(0, 0, "Seleccione"), "Value", "Text");
            ViewBag.ContactoSitrel = new SelectList(bcp.CargaItau.ListarContactos(0, 0, "", "Seleccione"), "Value", "Text");
            ViewBag.RespuestaSitrel = new SelectList(bcp.CargaItau.ListarRespuestas(0, 0, "", "", "Seleccione"), "Value", "Text");
            ViewBag.TelefonoContactoSitrel = new SelectList(bcp.CargaItau.ListarAcciones(0, 0, "Seleccione"), "Value", "Text"); 
            ViewBag.TipoDireccion = new SelectList(bcp.CargaItau.ListarTipoDireccion(0, 0, null), "Value", "Text"); ;
            ViewBag.Prf = objSession.PrfId;
            List<SelectListItem> lstCategoria = new List<SelectListItem>();
            lstCategoria.Add(new SelectListItem
            {
                Text = "Sin Categoria",
                Value = "S",
                Selected = true
            });
            lstCategoria.Add(new SelectListItem
            {
                Text = "Bueno",
                Value = "B"
            });
            lstCategoria.Add(new SelectListItem
            {
                Text = "Regular",
                Value = "R"
            });
            lstCategoria.Add(new SelectListItem
            {
                Text = "Malo",
                Value = "M"
            });
            ViewBag.Categoria = lstCategoria;
            if (objSession.PrfId != 4 && objSession.PrfId != 9 && objSession.PrfId != 6)
            {
                ViewBag.CategoriaEnable = "disabled";
            }
            else
            {
                ViewBag.CategoriaEnable = "";
            }
            if (objSession.UserId == 378 )
            {
                ViewBag.CategoriaEnable = "";
            }
            List<Combobox> lstMarcaVehiculos = bcp.BienVehiculo.ListarMarcasVehiculo();
            List<Combobox> lstModeloVehiculos = bcp.BienVehiculo.ListarModelosVehiculo(0);
            List<Combobox> lstConservadorBienes = bcp.BienPropiedad.ListarConservadorBienes();

            ViewBag.MarcaId = new SelectList(lstMarcaVehiculos, "Value", "Text");
            ViewBag.ModeloId = new SelectList(lstModeloVehiculos, "Value", "Text");
            ViewBag.ConservadorId = new SelectList(lstConservadorBienes, "Value", "Text");

            return View();
        }

        public ActionResult EjecutivosMutual()
        {            
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            Deudor objDeudor = new Deudor();

            ViewBag.Title = "Mantención de Cuentas y Datos de Ejecutivos";

            ViewBag.Ejecutivos = new SelectList(objDeudor.ListarEjecutivosMutual(0), "Value", "Text");
            ViewBag.TipoBanco = new SelectList(objDeudor.ListarTipoBancos(0), "Value", "Text");
            ViewBag.CuentaBanco = new SelectList(objDeudor.ListarCuentaTipoBanco(0, 0), "Value", "Text");


            return View();
        }

        public ActionResult TortaDinamica()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            Comprobante objComprobante = new Comprobante();
            
            ViewBag.Title = "Torta Dinamica";

            Funciones objFunc = new Funciones();
            bool perfilGestor = false;

            foreach (string id in objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 143).Split(','))
            {
                if (objSession.PrfId.ToString() == id) perfilGestor = true;
            }            

            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 1, "Seleccione"), "Value", "Text", "");

            List<Combobox> lstEstadoDocumento = new List<Combobox>();
            lstEstadoDocumento.Add(new Combobox() { Text = "-- Seleccione --", Value = "" });
            lstEstadoDocumento.Add(new Combobox() { Text = "Vigente", Value = "V" });
            lstEstadoDocumento.Add(new Combobox() { Text = "Judicial", Value = "J" });            
            
            ViewBag.EstadoDocumento = new SelectList(lstEstadoDocumento, "Value", "Text", "");
            
            if (perfilGestor)
            {
                ViewBag.Gestor = new SelectList(Dimol.Carteras.bcp.Gestor.ListarGestorCombo(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.Email), "Value", "Text", "");
            }
            else
            {
                ViewBag.Gestor = new SelectList(Dimol.Carteras.bcp.Gestor.ListarGestoresCombo(objSession.CodigoEmpresa, objSession.CodigoSucursal), "Value", "Text", "");
            }
                        
            return View();
        }

        public JsonResult TortaAgrupada(int pclid, int tipcart, string estcpbt, int? codid, int gesid, int? docsvencidos = null)
        {
            bcp.Torta bcpTorta = new bcp.Torta();

            dto.Torta obj = new dto.Torta();

            obj.Pclid = pclid;
            obj.TipoCartera = tipcart;
            obj.EstadoCpbt = estcpbt;
            obj.CodigoCarga = (codid == null ? 0 : (int)codid);
            obj.CodGestor = gesid;
            obj.Codemp = objSession.CodigoEmpresa;
            obj.Idioma = objSession.Idioma;
            obj.DocsVencidos = docsvencidos;

            bcpTorta.ListarTortaAgrupada(obj);
                        
            var jsonData = new
            {
                saldo = obj.Totales.Saldo,
                deudores = obj.Totales.Deudores,
                documentos = obj.Totales.Documentos,

                rows =
                (
                    from dto.TortaCliente item in obj.lstDocumentos
                    select new
                    {
                        item.IdAgrupa,
                        item.Agrupa,
                        Est = (from dto.TortaEstado estado in item.lstEstados 
                        select new
                        {
                            estado.Estado,
                            estado.Deudores,
                            estado.Documentos,
                            estado.Saldo,
                            estado.Regularizado,
                            Deu = (from dto.TortaDeudores deudores in estado.lstDeudores
                                   select new {
                                       deudores.Ctcid,
                                       deudores.Rut,
                                       deudores.Nombre,
                                       deudores.Acciones,
                                       deudores.Historial,
                                       deudores.Saldo
                                   }).ToArray()
                        }).ToArray() 
                    }
                ).ToArray(),
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Deudores(int? idd)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Disable = new int[]{1,2,3};
            ViewBag.Ctcid = 0;

            Direccion objDireccion = new Direccion();
            Deudor objDeudor = new Deudor();
            DeudorModel model = new DeudorModel();

            if (idd != null && idd != 0)
            {
                objDeudor.CodigoDeudor = (int)idd;
                objSession.Ctcid = (int)idd;
                objDeudor.CodigoEmpresa = objSession.CodigoEmpresa;
                objDeudor.BuscarDeudor();
                model.ApellidoMaterno = objDeudor.Materno;
                model.ApellidoPaterno = objDeudor.Paterno;
                model.Ctcid = objDeudor.CodigoDeudor.ToString();
                ViewBag.Ctcid = objDeudor.CodigoDeudor.ToString();
                model.Direccion = objDeudor.Direccion;
                model.EstadoDireccion = objDeudor.EstadoDireccion;
                model.FechaIngreso = objDeudor.FechaIngreso.ToShortDateString();
                model.IdCiudad = objDeudor.IdCiudad;
                model.IdComuna = objDeudor.IdComuna;
                model.IdPais = objDeudor.IdPais;
                model.IdRegion = objDeudor.IdRegion;
                model.IdSociedad = objDeudor.IdSociedad;
                if (objDeudor.NacionalExtranjero == "N")
                {
                    model.NacionalExtranjero = true;
                }
                else
                {
                    model.NacionalExtranjero = false;
                }

                model.Nombre = objDeudor.Nombre;
                model.NombreFantasia = objDeudor.NombreFantasia;
                model.ParticularEmpresa = objDeudor.ParticularEmpresa;
                if (objDeudor.Quiebra == "S")
                {
                    model.Quiebra = true;
                }
                else
                {
                    model.Quiebra = false;
                }
                model.SolicitaQuiebra= objDeudor.SolicitaQuiebra == "S" ? true : false;
                model.Rut = objDeudor.Rut;
                ViewBag.Disable = new int[] { };
            }
            else
            {
                objSession.Ctcid = 0;
            }
            Session["Usuario"] = objSession;
            List<Combobox> lstTipo = new List<Combobox>();
            lstTipo.Add(new Combobox { Text = "Seleccione", Value = " " });
            lstTipo.Add(new Combobox {Text= "Particular", Value = "P" });
            lstTipo.Add(new Combobox { Text = "Empresa", Value = "E" });
            ViewBag.IdPais = new SelectList(objDireccion.ListarPais(), "Value", "Text", objDeudor.IdPais);
            ViewBag.IdRegion = new SelectList(objDireccion.ListarRegion(objDeudor.IdPais), "Value", "Text", objDeudor.IdRegion);
            ViewBag.IdCiudad = new SelectList(objDireccion.ListarCiudad(objDeudor.IdRegion), "Value", "Text", objDeudor.IdCiudad);
            ViewBag.IdComuna = new SelectList(objDireccion.ListarComuna(objDeudor.IdCiudad), "Value", "Text", objDeudor.IdComuna);
            ViewBag.EstadoDireccion = new SelectList(objDireccion.ListarEstado(objSession.Idioma), "Value", "Text",objDeudor.EstadoDireccion);
            ViewBag.ParticularEmpresa = new SelectList(lstTipo, "Value", "Text",objDeudor.ParticularEmpresa);
            ViewBag.Sociedad = objDeudor.Sociedad;
            ViewBag.TipoTelefono = objDeudor.ListarTipoTelefono(objSession.Idioma) ;
            ViewBag.EstadoTelefono = objDeudor.ListarEstadoTelefono(objSession.Idioma);
            ViewBag.TipoMail = objDeudor.ListarTipoMail(objSession.Idioma);
            ViewBag.TipoContacto = objDeudor.ListarTipoContacto(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.ComunaGrilla = objDeudor.ListarComunaGrilla();
            //new SelectList(lstTipo, "Value", "Text", objDeudor.ParticularEmpresa);
            ViewBag.EstadoContacto = new SelectList(lstTipo, "Value", "Text", objDeudor.ParticularEmpresa);
      

            if (idd != null && idd != 0)
            {
                return View(model);
            }
            else
            {
                ViewBag.NacionalExtranjero = true;
                return View();
            }

            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Deudores(DeudorModel model)
        {
            int ctcid = objDeudor.BuscarIdDeudor(model.Rut, objSession.CodigoEmpresa);
            if (ctcid != 0 && ctcid != objDeudor.CodigoDeudor)
            {
                model.CodigoDeudor = ctcid;
            }
            string nacional = "N";
            if (!model.NacionalExtranjero)
            {
                nacional = "E";
            }
            string quiebra = "N";
            if (model.Quiebra)
            {
                quiebra = "S";
            }
            if (string.IsNullOrEmpty(model.ApellidoMaterno)){
                model.ApellidoMaterno = string.Empty;
            }
           
            ctcid = objDeudor.GuardarDeudor(objSession.CodigoEmpresa, model.CodigoDeudor, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.NombreFantasia, model.IdComuna, model.ParticularEmpresa, model.Direccion, model.IdSociedad.ToString(), quiebra, nacional, model.EstadoDireccion, model.SolicitaQuiebra);

            //ctcid = objDeudor.BuscarIdDeudor(model.Rut, objSession.CodigoEmpresa);
            ((UserSession)Session["Usuario"]).Ctcid = ctcid;

            return Json(ctcid);
        }

        public ActionResult BuscarDeudores()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult BuscarCpbtDeudores()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.TipoDocumento = new SelectList(objDeudor.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma,"Todos" ), "Value", "Text", "");
            return View();
        }

        public ActionResult DeudorCpbt(string idd)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            Comprobante objComprobante = new Comprobante();
            ComprobanteModel model = new ComprobanteModel();
            int pclid = 0;
            int ctcid = 0;
            int ccbid= 0;
            //datos combobox
            ViewBag.Contrato = new SelectList(objComprobante.ListarContrato(objSession.CodigoEmpresa, pclid, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, pclid, "Seleccione"), "Value", "Text", "");

            if (idd != null)
            {
                string[] ids = idd.Split('|'); // pclid - ctcid -ccbid
                pclid = Int32.Parse(ids[0]);
                ctcid = Int32.Parse(ids[1]);
                ccbid = Int32.Parse(ids[2]);
                ((UserSession)Session["Usuario"]).PclId = pclid;
                ((UserSession)Session["Usuario"]).Ctcid = ctcid;

                ViewBag.Pclid = pclid;
                ViewBag.Ctcid = ctcid;
                ViewBag.Ccbid = ccbid;
                dto.BuscarDeudor datosDeudor = objDeudor.BuscarDeudorCliente(objSession.CodigoEmpresa, pclid, ctcid);
                ViewBag.RutCliente = datosDeudor.RutCliente;
                ViewBag.NombreRutCliente = datosDeudor.RutCliente + " - " + datosDeudor.NombreCliente;
                ViewBag.NombreCliente = datosDeudor.NombreCliente;
                ViewBag.RutDeudor = datosDeudor.Rut;
                ViewBag.NombreRutDeudor = datosDeudor.Rut + " - " + datosDeudor.NombreFantasia;
                ViewBag.NombreFantasia = datosDeudor.NombreFantasia;
                dto.Comprobante comprobante = objComprobante.TraeCpbt(objSession.CodigoEmpresa, pclid, ctcid, ccbid);
                ViewBag.Contrato = new SelectList(objComprobante.ListarContrato(objSession.CodigoEmpresa, pclid, 1, "Seleccione"), "Value", "Text", comprobante.Contrato);
                ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, pclid, "Seleccione"), "Value", "Text", comprobante.CodigoCarga);
                model.TipoCartera= comprobante.TipoCartera.ToString();
                model.TipoDocumento =comprobante.TipoDocumento;
                model.Numero= comprobante.NumeroCpbt;
                model.FechaIngreso = comprobante.FechaIngreso.ToShortDateString();
                model.FechaDocumento = comprobante.FechaDocumento.ToShortDateString();
                model.FechaVencimiento = comprobante.FechaVencimiento.ToShortDateString();
                model.EstadoCpbt = comprobante.EstadoCpbt;
                model.EstadoCartera = comprobante.EstadoCartera;
                model.CodigoCarga = comprobante.CodigoCarga;
                model.Moneda = comprobante.CodigoMoneda.ToString();
                model.TipoCambio = comprobante.TipoCambio;
                model.NombreRutAsegurado = comprobante.SubcarteraRut;
                //model.Contrato = comprobante.Contrato;
                model.MotivoCobranza = comprobante.MotivoCobranza;
                model.NumeroNegocio = comprobante.NumeroEspecial;
                model.NumeroAgrupaEspecial = comprobante.NumeroAgrupa;
                ViewBag.Sbcid = Int32.Parse(string.IsNullOrEmpty(comprobante.SubcarteraNombre) ? "0" : comprobante.SubcarteraNombre);
                ViewBag.TerceroId = comprobante.TerceroId;
                var pair = objComprobante.TraeTerceroNombreRut(objSession.CodigoEmpresa, comprobante.TerceroId);
                ViewBag.RutTercero = pair.Value;
                ViewBag.NombreTercero = pair.Key;
                model.RutTercero = pair.Value;
                model.NombreTercero = pair.Key;

                if (comprobante.Antecedentes == "S")
                {
                    model.Antecedentes = true;
                }
                else
                {
                    model.Antecedentes = false;
                }

                if (comprobante.Originales == "S")
                {
                    model.Originales = true;
                }
                else
                {
                    model.Originales = false;
                }
                model.Monto = comprobante.MontoAsignado;
                model.MontoDocumento = comprobante.Monto;
                model.Saldo = comprobante.Saldo;
                model.GastoPreJudicial = comprobante.GastoOtros;
                model.GastoJudicial = comprobante.GastoJudicial;
                model.Comentario = comprobante.Comentario;
                model.Ccbid = comprobante.Ccbid;
                model.IdCuenta = comprobante.IdCuenta;
                model.DescripcionCuenta = comprobante.DescripcionCuenta;
            }

            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", model.TipoCartera);
            ViewBag.TipoDocumento = new SelectList(objDeudor.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", model.TipoDocumento);
            ViewBag.EstadoCpbt = new SelectList(objDeudor.ListarEstado(objSession.Idioma), "Value", "Text", model.EstadoCpbt);
            ViewBag.EstadoCartera = new SelectList(objComprobante.ListarEstadosCartera(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", model.EstadoCartera);
            ViewBag.Moneda = new SelectList(objComprobante.ListarMonedas(objSession.CodigoEmpresa), "Value", "Text", model.Moneda);
            ViewBag.Asociado = new SelectList(objComprobante.ListarAsociadoSubcartera(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", model.Asociado);
            ViewBag.MotivoCobranza = new SelectList(objComprobante.ListarMotivoCobranza(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", model.MotivoCobranza);

            Dimol.dao.Funciones func = new dao.Funciones();
            if(model.EstadoCpbt == "V" && model.EstadoCartera != func.ConfiguracionEmpNum(1,17).ToString() && objSession.Permisos <4)
            {
                ViewBag.Save = false;
                ViewBag.Del = false;
            } else
            {
                ViewBag.Save = true;
                ViewBag.Del = true;
            }
         
            if (model.FechaIngreso != null)
            {
                return View(model);
            }
            else
            {
                model.TipoCambio = 1;
                ViewBag.EstadoCpbt = new SelectList(objDeudor.ListarEstado(objSession.Idioma), "Value", "Text", "V");
                model.Monto = 0;
                model.MontoDocumento =0;
                model.Saldo = 0;
                model.GastoPreJudicial = 0;
                model.GastoJudicial = 0;

                return View(model);
            }
        }

        public ActionResult CargaMasiva()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Comprobante objComprobante = new Comprobante();
            ViewBag.Contrato = new SelectList(objComprobante.ListarContrato(objSession.CodigoEmpresa, 0, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 0, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text","");
            return View();
        }

        public ActionResult CargaPanelDemandasMasivas()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Comprobante objComprobante = new Comprobante();
            ViewBag.Contrato = new SelectList(objComprobante.ListarContrato(objSession.CodigoEmpresa, 0, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 0, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "");
            return View();
        }

        public ActionResult CargaMasivaAnular()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            return View(model);
        }

        public ActionResult CargaMasivaAprobar()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            return View(model);
        }

        public ActionResult CargaPago()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult CargaClientes()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Comprobante objComprobante = new Comprobante();
            ViewBag.Contrato = new SelectList(objComprobante.ListarContrato(objSession.CodigoEmpresa, 0, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 0, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "");
            return View();
        }

        public ActionResult Comprobante(string tipo, string pj, int pag, string idd,string id)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            int tipoComprobante=0, numero=0, estado=0;
            CabeceraComprobanteModel model = new CabeceraComprobanteModel();

            if (!string.IsNullOrEmpty(idd))
            {
                try
                {
                    Int32.TryParse(idd, out tipoComprobante);
                    Int32.TryParse(id, out numero);
                    dto.CabeceraComprobante obj = bcp.Comprobante.BuscarComprobante(objSession.CodigoEmpresa, objSession.CodigoSucursal, tipoComprobante, numero);

                    model.RutCliente = obj.RutCliente;
                    model.CabeceraId = obj.CabeceraId;
                    model.Numero = obj.Numero;
                    model.Pclid = obj.Pclid;
                    model.Ctcid = obj.Ctcid;
                    model.TipoComprobante = obj.TipoComprobante;
                    model.FechaDocumento = obj.FechaDocumento;
                    model.FechaVencimiento = obj.FechaVencimiento;
                    model.FechaEntrega = obj.FechaEntrega;
                    model.FechaContabilizacion = obj.FechaContabilizacion;
                    model.FechaOrdenCompra = obj.FechaOrdenCompra;
                    model.FechaIngreso = obj.FechaIngreso;
                    model.Moneda = obj.Moneda;
                    model.TipoCambio = obj.TipoCambio;
                    model.TipoGasto = obj.TipoGasto;
                    model.Glosa = obj.Glosa;

                    switch (obj.Estado)
                    {
                        case "E":
                            estado = 1;
                            break;
                        case "A":
                            estado = 2;
                            break;
                        case "F":
                            estado = 3;
                            break;
                        case "B":
                            estado = 4;
                            break;
                        case "C":
                            estado = 5;
                            break;
                        case "X":
                            estado = 6;
                            break;
                    }

                    model.Estado = estado.ToString();
                    model.FormaPago = obj.FormaPago;

                    model.Subtotal = obj.Neto.ToString("N2");
                    model.Neto = obj.Neto.ToString("N2");
                    model.Descuento = obj.Descuento.ToString("N2");
                    model.Impuestos = obj.Impuestos.ToString("N2");
                    model.Retenido = obj.Retenido.ToString("N2");
                    model.Total = obj.Saldo.ToString("N2");

                    model.TipoRol = obj.TipoRol;
                    model.Tribunal = obj.Tribunal;
                    model.NombreTribunal = obj.NombreTribunal;
                    string[] numRol = obj.NumeroRol.Split('-');
                    int anioRol = 0;

                    switch (numRol.Length)
                    {
                        case 1:
                            model.Rol = numRol[0];
                            break;
                        case 2:
                            model.Rol = numRol[0];
                            model.Anio = Int32.TryParse(numRol[1], out anioRol) ? anioRol : 0;
                            break;

                        case 3:
                            model.Rol = numRol[1];
                            model.Anio = Int32.TryParse(numRol[2], out anioRol) ? anioRol : 0;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.Comprobante", objSession.UserId);
                }

            }
            else
            {
                model.FechaVencimiento = DateTime.Today;
                model.FechaContabilizacion = DateTime.Today;
                model.FechaIngreso = DateTime.Today;
                model.TipoCambio =1 ;
                model.Anio = DateTime.Today.Year;
            }

            Direccion objDireccion = new Direccion();
            Deudor objDeudor = new Deudor();
            Comprobante objComprobante = new Comprobante();
            if (tipo == "C" && pj == "N")
            {
                ViewBag.TipoComprobante = new SelectList(bcp.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId, tipo, "-- Seleccione --").FindAll(x=>x.Text.Contains("BOLETA HONORARIOS")), "Value", "Text", model.TipoComprobante);
                ViewBag.Moneda = new SelectList(objComprobante.ListarMonedas(objSession.CodigoEmpresa).FindAll(x=>x.Text.Contains("PESOS")), "Value", "Text", model.Moneda?? "1");
                ViewBag.TipoGasto = new SelectList(bcp.Comprobante.ListarGastosComprobante(objSession.Idioma), "Value", "Text", model.TipoGasto??"J");
                ViewBag.FormaPago = new SelectList(bcp.Comprobante.ListarFormasPago(objSession.CodigoEmpresa, objSession.Idioma, "-- Seleccione --").FindAll(x=>x.Text.Contains("TRANSFERENCIA")), "Value", "Text", model.FormaPago);
            }
            else
            {
                ViewBag.TipoComprobante = new SelectList(bcp.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId, tipo, "-- Seleccione --").FindAll(x => x.Text.Contains("BOLETA HONORARIOS")), "Value", "Text", model.TipoComprobante);
                ViewBag.Moneda = new SelectList(objComprobante.ListarMonedas(objSession.CodigoEmpresa), "Value", "Text", model.Moneda);
                ViewBag.TipoGasto = new SelectList(bcp.Comprobante.ListarGastosComprobante(objSession.Idioma), "Value", "Text", model.TipoGasto);
                ViewBag.FormaPago = new SelectList(bcp.Comprobante.ListarFormasPago(objSession.CodigoEmpresa, objSession.Idioma, "-- Seleccione --"), "Value", "Text", model.FormaPago);
            }
            
            ViewBag.Estado = new SelectList(bcp.Comprobante.ListarEstadosComprobante(objSession.Idioma,"-- Seleccione --"), "Value", "Text", model.Estado);
            ViewBag.Sucursal = new SelectList(new List<Combobox>());
            ViewBag.SituacionCartera = new SelectList(objDeudor.ListarEstado(objSession.Idioma), "Value", "Text", "");
            ViewBag.FechaDocumento = model.FechaDocumento == new DateTime() ?  "": model.FechaDocumento.ToShortDateString();
            ViewBag.FechaVencimiento = model.FechaVencimiento == new DateTime() ?"": model.FechaVencimiento.ToShortDateString();
            ViewBag.FechaContabilizacion = model.FechaContabilizacion == new DateTime() ?"": model.FechaContabilizacion.ToShortDateString();
            ViewBag.FechaEntrega = model.FechaEntrega == new DateTime() ? "" : model.FechaEntrega.ToShortDateString();
            ViewBag.FechaOrdenCompra = model.FechaOrdenCompra == new DateTime() ? "" : model.FechaOrdenCompra.ToShortDateString();
            ViewBag.FechaOrdenCompra = model.FechaIngreso == new DateTime() ? "" : model.FechaIngreso.ToShortDateString();
            ViewBag.TipoCambio = model.TipoCambio  == 0 ? 1 : model.TipoCambio;

            List<Combobox> lstTipo = new List<Combobox>();
            lstTipo.Add(new Combobox { Text = "C", Value = "C" });
            lstTipo.Add(new Combobox { Text = "V", Value = "V" });
            lstTipo.Add(new Combobox { Text = "E", Value = "E" });
            lstTipo.Add(new Combobox { Text = "A", Value = "A" });
            lstTipo.Add(new Combobox { Text = "F", Value = "F" });
            lstTipo.Add(new Combobox { Text = "I", Value = "I" });
            ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", "C");
            int anio = DateTime.Today.Year;
            List<Combobox> lstAnio = new List<Combobox>();
            lstAnio.Add(new Combobox { Text = "", Value = "0" });
            for ( int a = anio - 10; a < anio + 1; a++)
            {
                lstAnio.Add(new Combobox { Text =  a.ToString(), Value =a.ToString() });
            }
            ViewBag.Anio = new SelectList(lstAnio, "Value", "Text", model.Anio.ToString());

            //Definición de parámetro de idCompetencia
            //int idCompetencia;
            //switch (model.Rol)
            //{
            //    case "P":
            //        idCompetencia = 4;
            //        break;
            //    default:
            //        idCompetencia = 3;
            //        break;
            //}

            //ViewBag.Tribunal = new SelectList(Judicial.Mantenedores.bcp.Rol.ListarTribunales(objSession.CodigoEmpresa, idCompetencia, "Seleccione"), "Value", "Text", model.Tribunal);
            ViewBag.Tipo = tipo;
            ViewBag.PJ = pj;
            ViewBag.Pag = pag;
            ViewBag.Prfid = objSession.PrfId;

            return View(model);
        }

        public ActionResult BuscarComprobantes(string tipo, string prejud)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            BuscarComprobanteModel model = new BuscarComprobanteModel();
            bcp.Comprobante objComprobante = new Comprobante();
            switch (tipo)
            {
                case "CC":
                    ViewBag.Title = "Búsqueda de Boletas de Receptores";
                    break;
                case "C":
                    ViewBag.Title = "Búsqueda de Boletas de Receptores";
                    break;
                case "V":
                    ViewBag.Title = "Búsqueda de Boletas de Receptores";
                    break;
            }
            ViewBag.TipoDocumento = new SelectList(bcp.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId, tipo, "Seleccione"), "Value", "Text", "");

            ViewBag.Estado = new SelectList(bcp.Comprobante.ListarEstadosComprobante(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Moneda = new SelectList(bcp.Comprobante.ListarMonedas(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            model.Cartera = prejud;
            model.Tipo = tipo;
            return View(model);
        }

        public ActionResult AceptarComprobantes(string tipo, string cartera)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            BuscarComprobanteModel model = new BuscarComprobanteModel();
            switch (tipo)
            {
                case "CC":
                    ViewBag.Title = "Aceptar Comprobantes";
                    model.Estado = "E";
                    model.Cartera = "S";
                    model.Tipo = "V";
                    break;
                case "C":
                    ViewBag.Title = "Aprobación Boletas de Receptores";
                    model.Estado = "E";
                    model.Cartera = "N";
                    model.Tipo = "C";
                    break;
                case "V":
                    ViewBag.Title = "Aceptar Comprobantes";
                    model.Estado = "E";
                    model.Cartera = "N";
                    model.Tipo = "V";
                    break;
            }
            return View(model);
        }

        public ActionResult BuscarSubCartera()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult SubCartera(string idd)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Direccion objDireccion = new Direccion();

            if (idd != "" && idd != null)
            {
                dto.SubCartera obj = bcp.Comprobante.TraeSubCartera(objSession.CodigoEmpresa, Int32.Parse(idd));
                ViewBag.Pais = new SelectList(objDireccion.ListarPais(), "Value", "Text", obj.Pais);
                ViewBag.Region = new SelectList(objDireccion.ListarRegion(obj.Pais), "Value", "Text", obj.Region);
                ViewBag.Ciudad = new SelectList(objDireccion.ListarCiudad(obj.Region), "Value", "Text",obj.Ciudad);
                ViewBag.Comuna = new SelectList(objDireccion.ListarComuna(obj.Ciudad), "Value", "Text", obj.Comuna);

                SubCarteraModel model = new SubCarteraModel() { 
                    Ciudad= obj.Ciudad,
                    Comuna= obj.Comuna,
                    Direccion = obj.Direccion,
                    Nombre = obj.Nombre,
                    Pais = obj.Pais,
                    Region = obj.Region,
                    Rut = obj.Rut,
                    Sbcid = obj.Sbcid.ToString(),
                    Telefono = obj.Telefono
                };

                return View(model);
            }
            else
            {
                ViewBag.Pais = new SelectList(objDireccion.ListarPais(), "Value", "Text", objSession.CodPais);
                ViewBag.Region = new SelectList(objDireccion.ListarRegion(objSession.CodPais), "Value", "Text");
                ViewBag.Ciudad = new SelectList(objDireccion.ListarCiudad(0), "Value", "Text");
                ViewBag.Comuna = new SelectList(objDireccion.ListarComuna(0), "Value", "Text");

                return View();
            }            
        }

        public ActionResult AnularRestriccionGestor()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Usuarios = bcp.Gestor.ListarUsuarios(objSession.CodigoEmpresa);
            ViewBag.Gestores = bcp.Gestor.ListarGestores(objSession.CodigoEmpresa, objSession.CodigoSucursal);
            return View();
        }

        public ActionResult DocumentosDeudor()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Codemp = objSession.CodigoEmpresa;
            ViewBag.TipoDocumento = new SelectList(objDeudor.ListarTipoDocumentosDeudor(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            return View();
        }

        public ActionResult MoverCartera()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            List<Combobox> lstEstado = objDeudor.ListarEstado(objSession.Idioma);
            ViewBag.Estado = new SelectList(lstEstado, "Value", "Text", "");
            return View();
        }

        public ActionResult ContabilidadBoleta()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            BuscarComprobanteModel model = new BuscarComprobanteModel();
            ViewBag.Title = "Contabilidad de Boletas de Receptores";
            model.Estado = "A";
            model.Cartera = "N";
            model.Tipo = "C";
            return View(model);
        }

        public ActionResult FacturacionBoleta(string tipo, string cartera)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            BuscarComprobanteModel model = new BuscarComprobanteModel();

            ViewBag.Title = "Facturas de Boletas de Receptores";
            model.Estado = "C";
            model.Cartera = "N";
            model.Tipo = "C";

            return View(model);
        }

        public ActionResult CastigoDevolucion(string tipo, string cartera)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            List<SelectListItem> lstCartera = new List<SelectListItem>();
            lstCartera.Add(new SelectListItem
            {
                Text = "Vigente",
                Value = "V",
                Selected = cartera == "P" ? true : false
            });
            lstCartera.Add(new SelectListItem
            {
                Text = "Judicial",
                Value = "J",
                Selected = cartera == "J" ? true : false
            });
            lstCartera.Add(new SelectListItem
            {
                Text = "Ambos",
                Value = "X",

            });

            ViewBag.Estado = lstCartera;
            List<Combobox> lstTipoComprobante = bcp.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId, "G", "-- Seleccione --");
            lstTipoComprobante.RemoveAt(1);

            CastigoDevolucionModel model = new CastigoDevolucionModel();

            ViewBag.Title = "Castigos y Devoluciones";

            //switch (tipo)
            //{
            //    case "C":
            //        ViewBag.Title = "Castigo de Documentos " + (cartera == "P" ? "PreJudicial" : "Judicial");
            //        //ViewBag.TipoComprobante = new SelectList(bcp.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId, "G", "-- Seleccione --"), "Value", "Text", model.TipoComprobante);
            //        break;
            //    case "D":
            //        ViewBag.Title = "Devolucion de Documentos " + (cartera == "P" ? "PreJudicial" : "Judicial");
            //       // ViewBag.TipoComprobante = new SelectList(bcp.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId, "G", "-- Seleccione --"), "Value", "Text", model.TipoComprobante);
            //        break;
            //}
            ViewBag.TipoComprobante = new SelectList(lstTipoComprobante, "Value", "Text", model.TipoComprobante);
            if (!string.IsNullOrEmpty(tipo)){
                model.Tipo = tipo;
            }
            else
            {
                model.Tipo = "CC";
            }
           
            model.Cartera = cartera;
        
            //if (tipo == "C" && pj == "N")
            //{
            //    ViewBag.TipoComprobante = new SelectList(bcp.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId, tipo, "-- Seleccione --").FindAll(x => x.Text.Contains("BOLETA")), "Value", "Text", model.TipoComprobante);
            //    ViewBag.Moneda = new SelectList(objComprobante.ListarMonedas(objSession.CodigoEmpresa).FindAll(x => x.Text.Contains("PESOS")), "Value", "Text", model.Moneda ?? "1");
            //    ViewBag.TipoGasto = new SelectList(bcp.Comprobante.ListarGastosComprobante(objSession.Idioma), "Value", "Text", model.TipoGasto ?? "J");
            //    ViewBag.FormaPago = new SelectList(bcp.Comprobante.ListarFormasPago(objSession.CodigoEmpresa, objSession.Idioma, "-- Seleccione --").FindAll(x => x.Text.Contains("TRANSFERENCIA")), "Value", "Text", model.FormaPago);
            //}
            //else
            //{
            //    ViewBag.TipoComprobante = new SelectList(bcp.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId, tipo, "-- Seleccione --"), "Value", "Text", model.TipoComprobante);
            //    ViewBag.Moneda = new SelectList(objComprobante.ListarMonedas(objSession.CodigoEmpresa), "Value", "Text", model.Moneda);
            //    ViewBag.TipoGasto = new SelectList(bcp.Comprobante.ListarGastosComprobante(objSession.Idioma), "Value", "Text", model.TipoGasto);
            //    ViewBag.FormaPago = new SelectList(bcp.Comprobante.ListarFormasPago(objSession.CodigoEmpresa, objSession.Idioma, "-- Seleccione --"), "Value", "Text", model.FormaPago);
            //}
            return View(model);
        }

        public ActionResult ControlProcesos()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
           
            return View();
        }

        public ActionResult CastigosMasivos(int pag)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            CastigoMasivoModel model = new CastigoMasivoModel();
            model.Pagina = pag;
            return View(model);
        }

        #endregion

        #region "Funciones Compartidas"
        public ActionResult GetDummy(GridSettings gridSettings)
        {
            // create json data 
            bcp.Deudor bcpDeudor = new Deudor();

            int totalRecords = 0;


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarDeudor> lst = new List<dto.BuscarDeudor>();


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarDeudor item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Pclid,
                            item.NombreCliente,
                            item.Ctcid,
                            item.Rut,
                            item.NombreFantasia,
                            item.Gestor,
                            item.Rol,
                            item.Gesid
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Upload(string tipo, string rut, string Ctcid, string Pclid, string TipoDocumento)
        {
            string fileName = "";
            string archivosItau = "";
            string archivosSuseso = "";
            string path = "";
            int id = 0; 
            Funciones objFunc = new Funciones();
            
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    fileName = "";
                    HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                    int fileSize = file.ContentLength;
                    string mimeType = file.ContentType;
                    Stream fileContent = file.InputStream;

                    switch (tipo)
                    {
                        case "CargaItau":
                            id = 15;
                            fileName = objSession.CodigoEmpresa + "_" + objSession.UserId + "_";
                            path = ConfigurationManager.AppSettings["RutaArchivos"] + objFunc.Configuracion_Str(id) + "\\";
                            
                            fileName = fileName + file.FileName;
                            objFunc.CreaCarpetas(path);

                            file.SaveAs(path + fileName);
                            archivosItau = archivosItau + fileName + ";";

                            break;
                        case "CargaSuseso":
                            id = 15;
                            fileName = objSession.CodigoEmpresa + "_" + objSession.UserId + "_";
                            path = ConfigurationManager.AppSettings["RutaArchivos"] + objFunc.Configuracion_Str(id) + "\\Suseso\\";
                            
                            fileName = fileName + file.FileName;
                            objFunc.CreaCarpetas(path);

                            file.SaveAs(path + fileName);
                            archivosSuseso += fileName + ";";

                            break;
                        case "Carga":
                            id = 15;
                            fileName = objSession.CodigoEmpresa + "_" + objSession.UserId + "_";
                            path = ConfigurationManager.AppSettings["RutaArchivos"] + objFunc.Configuracion_Str(id) + "\\";
                            
                            fileName = fileName + file.FileName;
                            objFunc.CreaCarpetas(path);
                            
                            file.SaveAs(path +  fileName);

                            break;
                        case "Documento":
                            id = 13;
                            string[] rutNombre = rut.Split('-');
                            path = ConfigurationManager.AppSettings["RutaArchivos"] + objFunc.Configuracion_Str(id) + "\\" + objSession.CodigoEmpresa + "\\" + rutNombre[0].Trim() + "\\";
                            fileName = file.FileName;
                            dto.DocumentoDeudor obj = new dto.DocumentoDeudor()
                            {
                                Codemp = objSession.CodigoEmpresa,
                                Ctcid = int.Parse(Ctcid),
                                Pclid = Pclid,
                                Dcdid = 0,
                                Archivo = fileName,
                                TipoDocumento = TipoDocumento,
                                TipoTipoDocumento = bcp.Deudor.TraeTipoTipoDocumento(objSession.CodigoEmpresa, Int32.Parse(TipoDocumento))
                            };
                            if (obj.TipoTipoDocumento == "C" && string.IsNullOrEmpty(obj.Pclid))
                            {
                                return Json(1);//objFunc.TraeError("Cliente", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
                            } else {
                                objFunc.CreaCarpetas(path);
                                
                                file.SaveAs(path + fileName);
                                GuardarDocumentoDeudor(obj);
                            }

                            break;
                        case "Castigo":
                            id = 23;
                            fileName = objSession.CodigoEmpresa + "_" + objSession.UserId + "_"+ DateTime.Now.ToString("yyyyMMddHHmmss_");
                            path = ConfigurationManager.AppSettings["RutaArchivos"] + objFunc.Configuracion_Str(id) + "\\";
                            fileName = fileName + file.FileName;
                            objFunc.CreaCarpetas(path);
                            
                            file.SaveAs(path +  fileName);

                            break;
                        case "CargaImagenCpbtdoc":
                            //en lugar de enviar el valor del rut, se enviara en sustitución de éste el valor ccb_ccbid
                            int cdi_cdid = Deudor.NumCarteraClientesCpbtDocImagenes(objSession.CodigoEmpresa, Pclid, Int32.Parse(Ctcid), Int32.Parse(rut));
                           
                            fileName = objSession.CodigoEmpresa + "_" + Pclid + "_" + Ctcid + "_" + rut + "_" + cdi_cdid + "_";
                            path = ConfigurationManager.AppSettings["RutaImagenes"];
                            fileName = fileName + file.FileName;
                            objFunc.CreaCarpetas(path);

                            file.SaveAs(path + fileName);

                            break;
                        case "DescargaTerreno":
                            fileName = objSession.CodigoEmpresa + "_" + objSession.UserId + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_");
                            path = ConfigurationManager.AppSettings["RutaArchivosTerreno"];
                            fileName = fileName + file.FileName;
                            objFunc.CreaCarpetas(path);

                            file.SaveAs(path + fileName);

                            break;
                        case "Estampe":
                            string ext = "";
                            fileName = "estampe_" + objSession.CodigoEmpresa + "_" + objSession.UserId + "_" + Pclid + "_" + Ctcid + "_" + TipoDocumento.Split('|')[0] + "_";
                            path = ConfigurationManager.AppSettings["RutaArchivos"] + "Documentos\\Estampes\\" + Pclid + "\\" + Ctcid + "\\" + TipoDocumento.Split('|')[0] + "\\";
                            ext = Path.GetExtension(file.FileName).ToLower();
                            
                            if (ext != ".pdf" && ext != ".odf" && ext != ".doc" && ext != ".docx")
                            {
                                return Json(-1);
                            }

                            //Verifica si se ha cargado un nuevo estampe y elimina el o los anteriores asociados a la boleta
                            List<string> lstEstampes = new List<string>();
                            lstEstampes = bcp.Deudor.ListarRutaEstampes(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(Pclid) ? "0" : Pclid), Int32.Parse(string.IsNullOrEmpty(Ctcid) ? "0" : Ctcid), TipoDocumento.Split('|'));

                            if (lstEstampes.Count > 0)
                            {
                                foreach (string ruta in lstEstampes)
                                {
                                    System.IO.File.Delete(ConfigurationManager.AppSettings["RutaArchivos"] + "Documentos\\Estampes\\" + ruta);
                                }

                                int del = Deudor.EliminarDatosEstampes(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(Pclid) ? "0" : Pclid), Int32.Parse(string.IsNullOrEmpty(Ctcid) ? "0" : Ctcid), TipoDocumento.Split('|'));
                            }

                            //Guarda el documento actual (estampe)
                            id = Deudor.GuardarDocumentoEstampe(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(Pclid) ? "0" : Pclid), Int32.Parse(string.IsNullOrEmpty(Ctcid) ? "0" : Ctcid), TipoDocumento.Split('|'), path, fileName, ext, objSession.UserId);

                            if (id > 0) {
                                objFunc.CreaCarpetas(path);
                                fileName = fileName + id + ext;
                                
                                file.SaveAs(path + fileName);
                            } else {
                                return Json("");
                            }

                            break;
                        case "CargaArchivoComprobante":
                            fileName = TipoDocumento + "_"+ objSession.CodigoEmpresa + "_" + objSession.UserId + "_" + DateTime.Now.ToString("yyyyMMddHHmmss_");
                            path = ConfigurationManager.AppSettings["RutaCertificados"];
                            //Use the following properties to get file's name, size and MIMEType
                            fileName = fileName + file.FileName;
                            objFunc.CreaCarpetas(path);

                            //To save file, use SaveAs method
                            file.SaveAs(path + fileName); //File will be saved in application root
                            break;
                    }
                }
            } catch (Exception ex) {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.CarteraController.Carga.Upload", 0);
                fileName = "";
            }

            switch (tipo)
            {
                case "CargaItau":
                    return Json(archivosItau);
                case "CargaSuseso":
                    return Json(archivosSuseso);
                default:
                    return Json(fileName);
            }
        }

        public JsonResult ListarRegion(int pais)
        {
            Direccion objDireccion = new Direccion();
            return Json(new SelectList(objDireccion.ListarRegion(pais), "Value", "Text"));
        }

        public JsonResult GetCuentaProvcliBanco(int Pclid, int Tipo)
        {
            Deudor objDeudor = new Deudor();
            return Json(objDeudor.GetCuentaProvcliBanco(Pclid, Tipo, objSession.CodigoEmpresa));
        }

        public JsonResult GetDeudorCodigoCargaCount(int Pclid, int Ctcid, string EstCpbt, int CodCarga)
        {
            Deudor objDeudor = new Deudor();
            return Json(objDeudor.GetDeudorCodigoCargaCount(Pclid, Ctcid, EstCpbt, CodCarga, objSession.CodigoEmpresa));
        }

        public JsonResult ListarCiudad(int region)
        {
            Direccion objDireccion = new Direccion();
            return Json(new SelectList(objDireccion.ListarCiudad(region), "Value", "Text"));
        }

        public JsonResult ListarComuna(int ciudad)
        {
            Direccion objDireccion = new Direccion();
            return Json(new SelectList(objDireccion.ListarComuna(ciudad), "Value", "Text"));
        }

        public JsonResult ListarGrupoCpbt( int pclid, int ctcid, string estadoCPBT)
        {
            Comprobante objComprobante = new Comprobante();
            return Json(new SelectList(objComprobante.ListarGrupoCpbt(objSession.CodigoEmpresa, pclid, ctcid, estadoCPBT, objSession.Idioma), "Value", "Text"));
        }

        public JsonResult ListarCpbt(int ctcid, string pclid, string estadoCPBT)
        {
            Deudor objDeudor = new Deudor();
            return Json(new SelectList(objDeudor.ListarCpbt(objSession.CodigoEmpresa, ctcid, pclid, estadoCPBT), "Value", "Text"));
        }

        public JsonResult ListarTiposImagenesCpbt()
        {
            Deudor objDeudor = new Deudor();
            return Json(new SelectList(objDeudor.ListarTiposImagenesCpbt(objSession.CodigoEmpresa), "Value", "Text"));
        }

        public JsonResult ListarCuentaTipoBanco(int tipoBanco, int pclid)
        {
            Deudor objDeudor = new Deudor();
            return Json(new SelectList(objDeudor.ListarCuentaTipoBanco(tipoBanco, pclid), "Value", "Text"));
        }

        public JsonResult ListarTipoBancos(int pclid)
        {
            Deudor objDeudor = new Deudor();
            return Json(new SelectList(objDeudor.ListarTipoBancos(pclid), "Value", "Text"));
        }

        public JsonResult ListarBancos()
        {
            Deudor objDeudor = new Deudor();
            return Json(new SelectList(objDeudor.ListarBancos(), "Value", "Text"));
        }

        public JsonResult ListarEjecutivosMutual(int pclid)
        {
            Deudor objDeudor = new Deudor();
            return Json(new SelectList(objDeudor.ListarEjecutivosMutual(pclid), "Value", "Text"));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GrabarImagenesCpbt(string pclid, int ctcid, int ccbid, int tpcid, string rutaImagen)
        {
            int salida = 0;
            int cdi_cdid = bcp.Deudor.NumCarteraClientesCpbtDocImagenes(objSession.CodigoEmpresa, pclid, ctcid, ccbid);
           
            
            //Use the following properties to get file's name, size and MIMEType
            rutaImagen = ConfigurationManager.AppSettings["RutaImagenes"].Replace(@"\\", @"\") + rutaImagen;
            salida = bcp.Deudor.GrabarImagenesCpbt(objSession.CodigoEmpresa, pclid, ctcid, ccbid, cdi_cdid, tpcid, rutaImagen);
            return Json(salida);
        }
        
        public ActionResult Gestionar()
        {
            return View();
        }

        public ActionResult BuscarDeudor()
        {
            return PartialView("_BuscarDeudor");
           // return View("_BuscarDeudor");
        }

        public ActionResult BuscarRutCliente(string term)
        {
            bcp.Cliente objCliente = new Cliente();
            return Json(objCliente.ListarRutCliente(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreCliente(string term)
        {
            bcp.Cliente objCliente = new Cliente();
            return Json(objCliente.ListarRutNombreCliente(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarNombreCliente(string term)
        {
            bcp.Cliente objCliente = new Cliente();
            return Json(objCliente.ListarNombreCliente(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreDeudor(string term)
        {
            bcp.Deudor obj= new bcp.Deudor();
            return Json(obj.ListarRutNombreDeudor(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutDeudor(string term)
        {
            //bcp.Deudor objDeudor = new Deudor();
            return Json(objDeudor.ListarRutDeudor(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreAsegurado(string term)
        {
            bcp.Comprobante obj = new bcp.Comprobante();
            return Json(obj.ListarRutNombreAsegurado(term), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Grilla Buscador Deudores"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDeudores(GridSettings gridSettings, BuscarDeudorModel model)
        {
            // create json data 
            bcp.Deudor bcpDeudor = new Deudor();
            
            int totalRecords = bcpDeudor.ListarDeudoresGrillaCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.UserId, model.Pclid, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.NombreFantasia, model.Telefono, model.Email, model.Direccion, model.Gestor, model.Rol, model.SituacionCartera, model.NumeroCPBT, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize); 


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarDeudor> lst = bcpDeudor.ListarDeudoresGrilla(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.UserId, model.Pclid, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.NombreFantasia, model.Telefono, model.Email, model.Direccion, model.Gestor, model.Rol, model.SituacionCartera, model.NumeroCPBT, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarDeudor item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Pclid,
                            item.NombreCliente,
                            item.Ctcid,
                            item.Rut,
                            item.NombreFantasia,
                            item.Gestor,
                            item.Rol,
                            item.Gesid
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDeudoresCpbt(GridSettings gridSettings, BuscarDeudorModel model)
        {
            // create json data 
            bcp.Deudor bcpDeudor = new Deudor();

            int totalRecords = bcpDeudor.ListarDeudoresCpbtCount(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.NombreFantasia, model.TipoDocumento,model.NumeroCPBT, model.Telefono, model.Email, model.Direccion, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DeudorDocumento> lst = bcpDeudor.ListarDeudoresCpbt(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.NombreFantasia, model.TipoDocumento,model.NumeroCPBT, model.Sbcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DeudorDocumento item in lst
                    select new
                    {
                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid,
                        cell = new object[]
                        {
                            item.Pclid,
                            item.Ctcid,
                            item.Ccbid,
                            item.RutCliente,
                            item.NombreCliente,
                            item.TipoDocumento,
                            item.Numero,
                            item.Monto,
                            item.Saldo,
                            item.Estado,
                            item.EstadoCpbt
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDeudoresMailCochaCpbt(GridSettings gridSettings, EmailCochaModel model)
        {
            // create json data 
            bcp.Deudor bcpDeudor = new bcp.Deudor();

            int totalRecords = bcpDeudor.ListarDeudoresMailCochaCpbtCount(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DeudorDocumento> lst = bcpDeudor.ListarDeudoresMailCochaCpbt(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.Carteras.dto.DeudorDocumento item in lst
                    select new
                    {
                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid,
                        cell = new object[]
                        {
                            item.Pclid,
                            item.Ctcid,
                            item.Ccbid,
                            //item.RutCliente,
                            //item.NombreCliente,
                            item.TipoDocumento,
                            item.Numero,
                            item.Monto,
                            item.Saldo,
                            item.Moneda,
                            item.Estado,
                            item.EstadoCpbt,
                            item.Carga
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDeudoresMailMutualCpbt(GridSettings gridSettings, EmailCochaModel model)
        {
            // create json data 
            bcp.Deudor bcpDeudor = new bcp.Deudor();

            int totalRecords = bcpDeudor.ListarDeudoresMailCochaCpbtCount(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DeudorDocumento> lst = bcpDeudor.ListarDeudoresMailCochaCpbt(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);
            
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.Carteras.dto.DeudorDocumento item in lst
                    select new
                    {
                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid,
                        cell = new object[]
                        {
                            item.Pclid,
                            item.Ctcid,
                            item.Ccbid,
                            item.TipoDocumento,
                            item.Numero,
                            item.FechaVencimiento,
                            item.Saldo,
                            item.Estado,
                            item.Negocio
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDeudoresMailMutualPagosCpbt(GridSettings gridSettings, EmailCochaModel model)
        {
            // create json data 
            bcp.Deudor bcpDeudor = new bcp.Deudor();

            int totalRecords = bcpDeudor.ListarDeudoresMailCochaCpbtCount(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DeudorDocumento> lst = bcpDeudor.ListarDeudoresMailCochaCpbt(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.Carteras.dto.DeudorDocumento item in lst
                    select new
                    {
                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid,
                        cell = new object[]
                        {
                            item.Pclid,
                            item.Ctcid,
                            item.Ccbid,
                            item.TipoDocumento,
                            item.Numero,
                            item.FechaVencimiento,
                            item.Monto,
                            item.Saldo,
                            item.Negocio,
                            item.Estado,
                            item.Carga
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetDeudor(string Rut)
        {
            // create json data 
            bcp.Deudor bcpDeudor = new Deudor();

            List<dto.BuscarDeudor> lst = bcpDeudor.ListarDeudor(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.UserId, Rut);


            var jsonData = new
            {
                rows =
                (
                    from dto.BuscarDeudor item in lst
                    select new
                    {
                        item.Id,
                        item.Pclid,
                        item.NombreCliente,
                        item.Ctcid,
                        item.Rut,
                        item.NombreFantasia,
                        item.Gestor,
                        item.Rol,
                        item.Gesid
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetDeudorCli(string Rut, string Cli)
        {
            // create json data 
            bcp.Deudor bcpDeudor = new Deudor();

            List<dto.BuscarDeudor> lst = bcpDeudor.ListarDeudorCli(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.UserId, Rut, Int32.Parse(Cli));


            var jsonData = new
            {
                rows =
                (
                    from dto.BuscarDeudor item in lst
                    select new
                    {
                        item.Id,
                        item.Pclid,
                        item.NombreCliente,
                        item.Ctcid,
                        item.Rut,
                        item.NombreFantasia,
                        item.Gestor,
                        item.Rol,
                        item.Gesid
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetBuscarDeudores(GridSettings gridSettings, BuscarDeudorModel model)
        {
            // create json data 
            bcp.Deudor bcpDeudor = new Deudor();

            int totalRecords = bcpDeudor.ListarBuscarDeudoresGrillaCount(objSession.CodigoEmpresa,  objSession.UserId, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.NombreFantasia, model.Telefono, model.Email, model.Direccion, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarDeudor> lst = bcpDeudor.ListarBuscarDeudoresGrilla(objSession.CodigoEmpresa, objSession.UserId, model.Nombre, model.ApellidoPaterno, model.ApellidoMaterno, model.Rut, model.NombreFantasia, model.Telefono, model.Email, model.Direccion, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarDeudor item in lst
                    select new
                    {
                        id = item.Ctcid,
                        cell = new object[]
                        {
                            item.Ctcid,
                            item.Rut,
                            item.NombreFantasia,
                            item.Nombre,
                            item.ApellidoPaterno,
                            item.ApellidoMaterno
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Cargar Datos Deudor"
        public ActionResult Buscar(int ctcid)
        {
            objDeudor.CodigoEmpresa = objSession.CodigoEmpresa;
            objDeudor.CodigoDeudor = ctcid;
            objDeudor.BuscarDeudor();
            ((UserSession)Session["Usuario"]).Ctcid = ctcid;

            return Json(objDeudor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTelefonos(GridSettings gridSettings, int ctcid, string telefono)
        {
            // create json data 
            if (telefono == "")
            {
                telefono = null;
            }

            int totalRecords = objDeudor.ListarTelefonosCount(objSession.CodigoEmpresa, ctcid, telefono, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Telefono> lst = objDeudor.ListarTelefonos(objSession.CodigoEmpresa, ctcid, telefono, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Telefono item in lst
                    select new
                    {
                        id = item.Ctcid+"|"+item.Ddcid+"|"+item.Numero,
                        cell = new object[]
                        {
                            item.CodigoArea,
                            item.Numero,
                            item.DescEstado,
                            item.TipoContacto,
                            item.NombreContacto,
                            item.Ctcid,
                            item.TipoTelefono,
                            item.Comuna,
                            item.IdEstado,
                            item.Ticid,
                            item.Ddcid,
                            item.EstadoDireccion,
                            item.Direccion,
                            item.Ciudad,
                            item.Region,
                            item.Pais
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDatosSII(int ctcid)
        {
            bcp.DeudorSII bcpDeudorSii = new DeudorSII();

            dto.DeudorSII d = new dto.DeudorSII();
         
            bcpDeudorSii.ListarDatosSII(ctcid, d);

            int color, anio;

            if(d.lstTimbrados.Where(o => o.Documento.Contains("Factura") || o.Documento.Contains("Boleta")).Select(o => o.Anio).FirstOrDefault() != 0)
            {
                anio = d.lstTimbrados.Where(o => o.Documento.Contains("Factura") || o.Documento.Contains("Boleta")).Select(o => o.Anio).Max();
            }
            else
            {
                anio = 0;
            }

            //if ((DateTime.Now.Month > 0 && DateTime.Now.Month < 5) && anio == DateTime.Now.Year - 1)
            if ((DateTime.Now.Month > 0 && DateTime.Now.Month < 5) && anio == d.FechaConsulta.Year - 1)
            {
                //anio = DateTime.Now.Year;
                anio = d.FechaConsulta.Year;
            }
            
            if (d.lstActividades.Count <= 0 && d.lstTimbrados.Count <= 0)
            {
                color = 0; //Amarillo
            }
            //else if (anio == DateTime.Now.Year || anio == (DateTime.Now.Year - 1))
            //else if (anio == DateTime.Now.Year)
            else if (anio == d.FechaConsulta.Year)
            {
                color = 1; //Verde
            }
            else
            {
                color = 2; //Rojo
            }

            var jsonData = new
            {
                rutContr = d.RutContribuyente,
                razonSocial = d.RazonSocial,
                fecConsulta = d.FechaConsultaStr,
                inicioAct = d.InicioActividades,
                fechaInicio = d.FechaInicioActStr,
                contrAutoriza = d.ContribuyenteAutorizado,
                contrProPyme = d.ContribuyenteProPyme,
                comentario = d.Comentario,
                observacion = d.Observacion,
                color = color,
                registrado = d.Registrado,

                rows =
                (
                    from dto.DeudorDocTimbrados item in d.lstTimbrados
                    select new
                    {
                        item.Documento,
                        item.Anio
                    }
                ).ToArray(),

                rowsAct =
                (
                    from dto.DeudorActividades item in d.lstActividades
                    select new
                    {
                        item.Actividades,
                        item.AfectaIVA,
                        item.Categoria,
                        item.Codigo
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarCuentaEjecutivo(string cuenta, int idEjecutivo, int idBanco)
        {          
            int result = 0;
            result = bcp.Deudor.InsertarCuentaEjecutivo(cuenta, idEjecutivo, idBanco);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarEjecutivoMutual(int pclid, string ejecutivo, string email, string oficina, int idEjecutivo)
        {
            int result = 0;
            result = bcp.Deudor.InsertarEjecutivoMutual(pclid, ejecutivo, email, oficina, idEjecutivo);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EliminarCuentaEjecutivo(int cuenta)
        {
            int result = 0;
            result = bcp.Deudor.EliminarCuentaEjecutivo(cuenta);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EliminarEjecutivoMutual(int idejecutivo)
        {
            int result = 0;
            result = bcp.Deudor.EliminarEjecutivoMutual(idejecutivo);

            return Json(result);
        }

        public JsonResult ListarEjecutivoMutual(int ejecutivo, int cuenta, int pclid)
        {
            bcp.Deudor bcpDeudor = new Deudor();

            List<dto.EjecutivoMutual> lst = bcpDeudor.ListarEjecutivoMutual(ejecutivo, cuenta, pclid);
                        

            var jsonData = new
            {

                rows =
                (
                    from dto.EjecutivoMutual item in lst
                    select new
                    {
                        item.IdTipoBanco,
                        item.NombreBanco,
                        item.IdCuentaEjecutivo,
                        item.Cuenta,
                        item.Email,
                        item.Oficina
                    }
                ).ToArray()
                
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTelefonoDeudor(GridSettings gridSettings, int ctcid)
        {
            // create json data 
            int totalRecords = objDeudor.ListarTelefonosDeudorCount(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TelefonoDeudor> lst = objDeudor.ListarTelefonosDeudor(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TelefonoDeudor item in lst
                    select new
                    {
                        id = item.Codemp + "|" + item.Ctcid + "|" + item.Numero,
                        cell = new object[]
                        {
                            item.Numero,
                            item.TipoTelefono,
                            item.EstadoTelefono,
                            item.Codemp,
                            item.Ctcid
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmail(GridSettings gridSettings, int ctcid, string email)
        {
            // create json data 
            if (email == "")
            {
                email = null;
            }

            int totalRecords = objDeudor.ListarEmailCount(objSession.CodigoEmpresa, ctcid, email, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Email> lst = objDeudor.ListarEmail(objSession.CodigoEmpresa, ctcid, email, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Email item in lst
                    select new
                    {
                        id = item.Ctcid + "|" + item.Ddcid + "|" + item.Mail,
                        cell = new object[]
                        {
                            item.Mail,
                            item.DescTipo,
                            item.TipoContacto,
                            item.NombreContacto,
                            item.Ctcid,
                            item.Ddcid,
                            item.Ticid,
                            item.IdEstado,
                            item.Direccion,
                            item.EstadoDireccion,
                            item.Ciudad,
                            item.Region,
                            item.Pais,
                            item.Comuna,
                            item.Masivo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmailProv(GridSettings gridSettings, int ctcid, string email)
        {
            // create json data 
            if (email == "")
            {
                email = null;
            }

            int totalRecords = objDeudor.ListarEmailCount(objSession.CodigoEmpresa, ctcid, email, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Email> lst = objDeudor.ListarEmailProv(objSession.CodigoEmpresa, ctcid, email, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Email item in lst
                    select new
                    {
                        id = item.Ctcid + "|" + item.Ddcid + "|" + item.Mail,
                        cell = new object[]
                        {
                            item.Mail,
                            item.DescTipo,
                            item.TipoContacto,
                            item.NombreContacto,
                            item.Ctcid,
                            item.Ddcid,
                            item.Ticid,
                            item.IdEstado,
                            item.Direccion,
                            item.EstadoDireccion,
                            item.Ciudad,
                            item.Region,
                            item.Pais,
                            item.Comuna,
                            item.Masivo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmailMutual(GridSettings gridSettings, int ctcid, string email)
        {
            // create json data 
            if (email == "")
            {
                email = null;
            }

            int totalRecords = objDeudor.ListarEmailCount(objSession.CodigoEmpresa, ctcid, email, objSession.Idioma, "", "TipoEmail, Mail", "asc", 1, 100);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Email> lst = objDeudor.ListarEmail(objSession.CodigoEmpresa, ctcid, email, objSession.Idioma, "and t.Masivo = 'S'", "TipoEmail, Mail", "asc", 0, 100);
            //List<dto.Email> lst2 = objDeudor.ListarEmailProv(objSession.CodigoEmpresa, ctcid, email, objSession.Idioma, "and t.Masivo = 'S'", "TipoEmail, Mail", "asc", 0, 100);
            //var lst3 = lst.Concat(lst2).ToList();

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,
                rows =
                (
                    from dto.Email item in lst
                    select new
                    {
                        id = item.Ctcid + "|" + item.Ddcid + "|" + item.Mail,
                        cell = new object[]
                        {
                            item.Ddcid,
                            item.Mail                            
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmailDeudor(GridSettings gridSettings, int ctcid)
        {
            // create json data 
            int totalRecords = objDeudor.ListarEmailDeudorCount(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.EmailDeudor> lst = objDeudor.ListarEmailDeudor(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EmailDeudor item in lst
                    select new
                    {
                        id = item.Codemp + "|" + item.Ctcid + "|" + item.Mail,
                        cell = new object[]
                        {
                            item.Mail,
                            item.TipoMail,
                            item.Masivo,
                            item.Codemp,
                            item.Ctcid
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHistorial(GridSettings gridSettings, int pclid, int ctcid, string tipoHistorial)
        {
            // create json data 
            int totalRecords = objDeudor.ListarHistorialCount(objSession.CodigoEmpresa, pclid, ctcid, objSession.Idioma, tipoHistorial,gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Historial> lst = objDeudor.ListarHistorial(objSession.CodigoEmpresa, pclid, ctcid, objSession.Idioma, tipoHistorial,gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Historial item in lst
                    select new
                    {
                        id = pclid + "|" + ctcid + "|" + item.Fecha ,
                        cell = new object[]
                        {
                            item.Fecha,
                            item.Tipo,
                            item.NombreUsuario,
                            item.Comentario,
                            item.TipoContacto,
                            item.NombreContacto,
                            item.Telefono
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetObservacion(GridSettings gridSettings, int pclid, int ctcid, string tipoHistorial)
        {
            // create json data 
            int totalRecords = objDeudor.ListarObservacionCount(objSession.CodigoEmpresa, pclid, ctcid, objSession.Idioma, tipoHistorial, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Historial> lst = objDeudor.ListarObservacion(objSession.CodigoEmpresa, pclid, ctcid, objSession.Idioma, tipoHistorial, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Historial item in lst
                    select new
                    {
                        id = pclid + "|" + ctcid + "|" + item.Fecha,
                        cell = new object[]
                        {
                            item.Fecha,
                            item.Tipo,
                            item.NombreUsuario,
                            item.Comentario,
                            item.TipoContacto,
                            item.NombreContacto,
                            item.Telefono
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCausaRut(GridSettings gridSettings, string rut)
        {
            // create json data 
            //CausaPorRut.ConsultaRutClient  objWS = new CausaPorRut.ConsultaRutClient();
            //Service.dto.ConsultaPJ[] lstWS = objWS.CausasPorRut(rut);

            List<dto.ConsultaPJ> lstWS = bcp.ConsultaPJ.ConsultarPorRut(rut);

            int totalRecords = lstWS.Count;

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            //List<dto.Historial> lst = objDeudor.ListarObservacion(objSession.CodigoEmpresa, pclid, ctcid, objSession.Idioma, tipoHistorial, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.ConsultaPJ item in lstWS
                    select new
                    {
                        id = item.Numero + "|" + item.FechaIngreso,
                        cell = new object[]
                        {
                            item.Tipo +"-"+item.Numero+"-"+item.Anio,
                            item.FechaIngreso,
                            item.Demandante+" / "+item.Demandado,
                            item.Tribunal,
                            item.RutaDemanda,
                            item.Url
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocCliente(GridSettings gridSettings, int pclid, int ctcid)
        {

            //objSession.Domain = "http://localhost:46811";
            // create json data 
            int totalRecords = objDeudor.ListarDocClienteCount(objSession.CodigoEmpresa, pclid, ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Documento> lst = objDeudor.ListarDocCliente(objSession.CodigoEmpresa, pclid, ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);
            
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Documento item in lst
                    select new
                    {
                        id = ctcid + "|" + item.Dcdid,
                        cell = new object[]
                        {
                            item.Ctcid,
                            item.Dcdid,
                            item.TipoDocumento,
                            item.NombreArchivo,
                            objSession.Domain + item.UrlArchivo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocDeudor(GridSettings gridSettings, int ctcid)
        {
            // create json data 
            int totalRecords = objDeudor.ListarDocDeudorCount(objSession.CodigoEmpresa, ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Documento> lst = objDeudor.ListarDocDeudor(objSession.CodigoEmpresa, ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Documento item in lst
                    select new
                    {
                        id = ctcid + "|" + item.Dcdid,
                        cell = new object[]
                        {
                            item.Ctcid,
                            item.Dcdid,
                            item.TipoDocumento,
                            item.NombreArchivo,
                            objSession.Domain + item.UrlArchivo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRol(GridSettings gridSettings, int ctcid)
        {
            // create json data 
            int totalRecords = objDeudor.ListarRolCount(objSession.CodigoEmpresa, ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Rol> lst = objDeudor.ListarRol(objSession.CodigoEmpresa, ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Rol item in lst
                    select new
                    {
                        id = ctcid + "|" + item.Rolid,
                        cell = new object[]
                        {
                            item.Rolid,
                            item.Cliente,
                            item.Deudor,
                            item.NumeroRol,
                            item.Bloqueo,
                            item.Causa,
                            item.Tribunal,
                            item.Materia,
                            item.Estado,
                            item.EstAdm,
                            item.FechaAccion,
                            item.AccionJudicial
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocRol(GridSettings gridSettings, int rolid)
        {
            // create json data 
            int totalRecords = objDeudor.ListarDocRolCount(objSession.CodigoEmpresa, rolid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.DocumentoRol> lst = objDeudor.ListarDocRol(objSession.CodigoEmpresa, rolid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoRol item in lst
                    select new
                    {
                        id = item.Ccbid,
                        cell = new object[]
                        {
                            item.Tipo,
                            item.Numero,
                            item.Monto,
                            item.Saldo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalDocRol(int rolid)
        {
            List<dto.DocumentoRol> lst = objDeudor.ListarDocRol(objSession.CodigoEmpresa, rolid, objSession.Idioma, "", "Ccbid", "asc", 0, 111111111);
            decimal monto = lst.Sum(x => x.Monto);
            decimal saldo = lst.Sum(x => x.Saldo);

            var jsonData = new
            {
                monto = monto,//.ToString("N2"),
                saldo = saldo//.ToString("N2")
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetEstadoRol(GridSettings gridSettings, int rolid)
        {
            // create json data 
            int totalRecords = objDeudor.ListarEstadoRolCount(objSession.CodigoEmpresa, rolid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.EstadosRol> lst = objDeudor.ListarEstadoRol(objSession.CodigoEmpresa, rolid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EstadosRol item in lst
                    select new
                    {
                        id = item.IdEstado,
                        cell = new object[]
                        {
                            item.Materia,
                            item.Estado,
                            item.Fecha,
                            item.Comentario,
                            item.Usuario
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArchivosRol(GridSettings gridSettings, int rolid, int ctcid)
        {
            // create json data 
            int totalRecords = 0;//objDeudor.ListarEstadoRolCount(objSession.CodigoEmpresa, rolid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.EstadosRol> lst = new List<dto.EstadosRol>();//objDeudor.ListarEstadoRol(objSession.CodigoEmpresa, rolid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EstadosRol item in lst
                    select new
                    {
                        id = item.IdEstado,
                        cell = new object[]
                        {
                            item.Materia,
                            item.Estado,
                            item.Fecha,
                            item.Comentario,
                            item.Usuario
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContactosDeudor(GridSettings gridSettings, int ctcid)
        {
            // create json data 
            int totalRecords = objDeudor.ListarContactosCount(objSession.CodigoEmpresa, ctcid,objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Contacto> lst = objDeudor.ListarContactos(objSession.CodigoEmpresa, ctcid, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Contacto item in lst
                    select new
                    {
                        id = item.Codemp + "|" + item.Ctcid + "|" + item.Ddcid,
                        cell = new object[]
                        {
                            item.Nombre,
                            item.Tipo,
                            item.EstadoContacto,
                            item.Comuna,
                            item.Direccion,
                            item.Codemp,
                            item.Ctcid,
                            item.Ddcid                            
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public JsonResult OperTelefono(dto.Telefono model, string oper, string id, FormCollection form)
        {
            model.Ctcid = ((UserSession)Session["Usuario"]).Ctcid;
            objDeudor.OperTelefono(oper, id, objSession,model);
            return Json("OK");
        }

        public JsonResult OperTelefonoDeudor(dto.TelefonoDeudor model, string oper, string id)
        {
            model.Ctcid = ((UserSession)Session["Usuario"]).Ctcid;
            objDeudor.OperTelefonoDeudor(oper, id, objSession, model);
            return Json("OK");
        }

        public JsonResult OperContacto(dto.Contacto model, string oper, string id)
        {
            model.Ctcid = ((UserSession)Session["Usuario"]).Ctcid;
            model.Codemp = ((UserSession)Session["Usuario"]).CodigoEmpresa;
            objDeudor.OperContacto(oper, id, objSession, model);
            return Json("OK");
        }

        public JsonResult OperEmail(dto.Email model, string oper, int? id)
        {
            model.Ctcid = ((UserSession)Session["Usuario"]).Ctcid;
            objDeudor.OperEmail(oper, id, objSession, model);
            return Json("OK");
        }

        public JsonResult OperEmailDeudor(dto.EmailDeudor model, string oper, string id)
        {
            model.Ctcid = ((UserSession)Session["Usuario"]).Ctcid;
            model.Codemp = ((UserSession)Session["Usuario"]).CodigoEmpresa;
            if (model.Masivo == "on")
            {
                model.Masivo = "S";
            }
            else
            {
                model.Masivo = "N";
            }
            objDeudor.OperEmailDeudor(oper, id, model);
            return Json("OK");
        }

        public JsonResult OperHistorial(dto.Historial model, string oper, int? id)
        {
            //model.Ctcid = ((UserSession)Session["Usuario"]).Ctcid;
            objDeudor.OperHistorial(oper, id, objSession, model);
            return Json("OK");
        }

        #endregion

        #region "Comprobantes y Documentos"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCpbt(GridSettings gridSettings, BuscarDeudorModel model)
        {
            // create json data 
            bcp.Comprobante bcpComprobante = new Comprobante();
            int pclid = 0;
            int ctcid = 0;
            if (model.Pclid != null)
            {
                pclid = Int32.Parse(model.Pclid);
            }

            if (model.Ctcid != null)
            {
                ctcid = Int32.Parse(model.Ctcid);
            }
            int totalRecords = 0;
            if (ctcid != 0)
            {
                totalRecords = bcpComprobante.TraeCarteraClienteComprobanteCount(objSession.CodigoEmpresa, pclid, ctcid, model.SituacionCartera, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, 0, 10000000);
            }

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Comprobante> lst = new List<dto.Comprobante>();
            if (ctcid != 0)
            {
                lst = bcpComprobante.ListarCarteraClienteComprobante(objSession.CodigoEmpresa, pclid, ctcid, model.SituacionCartera, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);
            }

            lst.Where(w => w.FechaPlazo == new DateTime()).ToList().ForEach(s => s.FechaPlazo = null);
            lst.Where(w => w.FechaCalculoInteres == new DateTime()).ToList().ForEach(s => s.FechaCalculoInteres = null);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Comprobante item in lst
                    select new
                    {
                        id = item.Ccbid,
                        cell = new object[]
                        {
                            item.Ccbid,
                            item.TipoCpbtNombre,
                            item.NumeroCpbt,
                            item.NumeroEspecial,
                            item.CodigoCarga,      
                            item.FechaIngreso,
                            item.FechaDocumento,
                            item.FechaVencimiento,
                            item.DiasVencido,     
                            item.FechaPlazo,
                            item.FechaCalculoInteres,
                            item.FechaUltimaGestion,
                            item.EstadoCartera,
                            item.EstadoJudicial,
                            item.Moneda,
                            item.Monto,
                            item.Saldo,
                            item.Compromiso,
                            item.Intereses,
                            item.Honorarios,
                            item.GastoOtros,
                            item.GastoJudicial,
                            item.TotalDeuda,
                            item.SubcarteraNombre,
                            item.Comentario,
                            item.DemandaPendiente
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCpbtTotal(GridSettings gridSettings, BuscarDeudorModel model)
        {
            // create json data 
            bcp.Comprobante bcpComprobante = new Comprobante();
            int pclid = 0;
            int ctcid = 0;
            if (model.Pclid != null)
            {
                pclid = Int32.Parse(model.Pclid);
            }

            if (model.Ctcid != null)
            {
                ctcid = Int32.Parse(model.Ctcid);
            }

            List<dto.Comprobante> lst = bcp.Comprobante.TraeCarteraClienteComprobanteTotal(objSession.CodigoEmpresa, pclid, ctcid, model.SituacionCartera, objSession.Idioma);

            if (lst.Count >0)
            {
                return Json(lst[0]);
            } else {
                return Json( new dto.Comprobante());
            }

            
        }

        public ActionResult GetCpbtTotalMoneda(GridSettings gridSettings, BuscarDeudorModel model)
        {
            // create json data 
            bcp.Comprobante bcpComprobante = new Comprobante();
            int pclid = 0;
            int ctcid = 0;
            if (model.Pclid != null)
            {
                pclid = Int32.Parse(model.Pclid);
            }

            if (model.Ctcid != null)
            {
                ctcid = Int32.Parse(model.Ctcid);
            }

            List<dto.Comprobante> lst = bcp.Comprobante.TraeCarteraClienteComprobanteTotalMoneda(objSession.CodigoEmpresa, pclid, ctcid, model.SituacionCartera, objSession.Idioma);

            if (lst.Count > 0)
            {
                return Json(lst[0]);
            }
            else
            {
                return Json(new dto.Comprobante());
            }
        }

        public JsonResult TipoCarteraGrilla(int pclid, int ctcid, string estadoCPBT)
        {
            Comprobante objComprobante = new Comprobante();
            var jsonData = new
            {
                tipoCartera = objComprobante.TraeTipoCartera(objSession.CodigoEmpresa, pclid, ctcid, estadoCPBT)
            };


            return Json(jsonData,JsonRequestBehavior.AllowGet) ;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GuardarCpbt(ComprobanteModel model)
        {
            int salida = 0;
            try
            {
                Comprobante objComprobante = new Comprobante();
                dto.Comprobante obj = new dto.Comprobante();
                Dimol.bcp.Utilidades utl = new Dimol.bcp.Utilidades(){ Empresa= objSession.CodigoEmpresa,Sucursal= objSession.CodigoSucursal, Usuario = objSession.UserId, IpRed= objSession.IpRed,IpMaquina= objSession.IpPc};

                if (model.Antecedentes)
                {
                    obj.Antecedentes = "S";
                } else {
                    obj.Antecedentes = "N";
                }
                obj.CalculoHonorarios = model.CalculoHonorarios;
                obj.Carta = model.Carta;
                obj.Ccbid = model.Ccbid;
                obj.Cobrable = model.Cobrable;
                obj.CodigoCarga = model.CodigoCarga;
                obj.CodigoMoneda = Int32.Parse( model.Moneda);
                obj.Comentario = model.Comentario;
                obj.Compromiso = model.Compromiso;
                obj.Contrato = Int32.Parse(string.IsNullOrEmpty(model.Contrato) ? "0" : model.Contrato);
                obj.Ctcid = model.Ctcid;
                obj.DiasVencido = model.DiasVencido;

                if (!string.IsNullOrEmpty(model.EstadoCartera))
                { 
                    obj.EstadoCartera = model.EstadoCartera;
                }
                else
                {
                    obj.EstadoCartera = "1";
                }

                if (!string.IsNullOrEmpty(model.EstadoCpbt))
                { 
                    obj.EstadoCpbt = model.EstadoCpbt;
                }
                else
                {
                    obj.EstadoCpbt = "V";
                }

                obj.FechaCalculoInteres = model.FechaCalculoInteres;
                obj.FechaCastigo = model.FechaCastigo;
                obj.FechaDocumento = DateTime.Parse(model.FechaDocumento);

                if (!string.IsNullOrEmpty( model.FechaIngreso))
                {
                    obj.FechaIngreso = DateTime.Parse(model.FechaIngreso);
                }
                else
                {
                    obj.FechaIngreso = DateTime.Now;
                }

                obj.FechaPlazo = model.FechaPlazo;
                obj.FechaUltimaGestion = DateTime.Now;
                obj.FechaVencimiento = DateTime.Parse(model.FechaVencimiento);
                obj.GastoJudicial = model.GastoJudicial;
                obj.GastoOtros = model.GastoPreJudicial;
                obj.Honorarios = model.Honorarios;
                obj.Intereses = model.Intereses;
                obj.Moneda = model.Moneda;
                obj.Monto = model.MontoDocumento;
                obj.MontoAsignado = model.Monto;
                obj.MotivoCobranza = model.MotivoCobranza;
                if (string.IsNullOrEmpty( model.NombreBanco ))
                {
                    obj.NombreBanco = "0";
                }
                else
                {
                    obj.NombreBanco = model.NombreBanco;
                }
                
                obj.NombreCliente = model.NombreCliente;
                obj.NombreGirador = model.NombreGirador;
                obj.NumeroAgrupa = model.NumeroAgrupaEspecial;
                obj.NumeroCpbt = model.Numero;
                obj.NumeroEspecial = model.NumeroNegocio;
                if (model.Originales)
                {
                    obj.Originales = "S";
                }
                else
                {
                    obj.Originales = "N";
                }
                if (model.Pclid == 0)
                {
                    obj.Pclid = ((UserSession)Session["Usuario"]).PclId;
                }
                else
                {
                    obj.Pclid = model.Pclid;
                }
                
                obj.Retent = model.Retent;
                obj.RutGirador = model.RutGirador;
                obj.Saldo = model.Saldo;
                obj.SubcarteraNombre = model.SubcarteraNombre;
                obj.Sbcid = model.Sbcid;
                obj.TipoCambio = model.TipoCambio;
                obj.TipoCartera = Int32.Parse(model.TipoCartera);
                obj.TipoCpbtNombre = model.TipoDocumento;
                obj.TipoDocumento = model.TipoDocumento;
                obj.TotalDeuda = model.TotalDeuda;

                int idtercero = 0;
                if (!string.IsNullOrEmpty(model.RutTercero))
                {
                    idtercero = objComprobante.BuscarTercero(objSession.CodigoEmpresa, model.RutTercero);
                    if (idtercero == 0)
                    {
                        idtercero = objComprobante.GuardarTercero(objSession.CodigoEmpresa, model.RutTercero, model.NombreTercero);
                    }
                }
                obj.TerceroId = idtercero < 0 ? 0: idtercero;

                obj.IdCuenta = model.IdCuenta;
                obj.DescripcionCuenta = model.DescripcionCuenta;

                #region Datos extendidos
                obj.NumeroResolucion = model.NumResolucion;
                obj.RutRepresentante1 = model.RutRepresentante1;
                obj.NombreRepresentante1 = model.NombreRepresentante1;
                obj.RutRepresentante2 = model.RutRepresentante2;
                obj.NombreRepresentante2 = model.NombreRepresentante2;
                obj.RutRepresentante3 = model.RutRepresentante3;
                obj.NombreRepresentante3 = model.NombreRepresentante3;
                #endregion

                salida = objComprobante.GrabarDocumento(obj, objSession.CodigoEmpresa);
                if (salida > 0){
                    //utl.InsertarHistorialCartera(objSession.CodigoEmpresa,objSession.CodigoSucursal,obj.Pclid,obj.Ctcid,obj.Ccbid,Int32.Parse( obj.EstadoCartera),obj.Monto.ToString(),obj.Saldo.ToString(),objSession.UserId,"");
                }
            }
            catch (Exception ex)
            {
            }

            return Json(salida);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EliminarCpbt(ComprobanteModel model)
        {
            Funciones func = new Funciones();
            Comprobante objComprobante = new Comprobante();
            dto.Comprobante obj = new dto.Comprobante();
            Dimol.bcp.Utilidades utl = new Dimol.bcp.Utilidades() { Empresa = objSession.CodigoEmpresa, Sucursal = objSession.CodigoSucursal, Usuario = objSession.UserId, IpRed = objSession.IpRed, IpMaquina = objSession.IpPc };
            int salida = 0;
            obj.Pclid=model.Pclid;
            obj.Ctcid=model.Ctcid;
            obj.Ccbid=model.Ccbid;
            if (model.EstadoCpbt == "V" && model.EstadoCartera == func.ConfiguracionEmpNum(objSession.CodigoEmpresa, 17).ToString())
            {
                salida = objComprobante.EliminarDocumento(obj, objSession.CodigoEmpresa);
                         }
            else if (objSession.Permisos >= 4 && (model.EstadoCpbt == "V" || model.EstadoCpbt == "J"))
            {
                salida = objComprobante.DescartarDocumento(obj, objSession.CodigoEmpresa);
                if (salida > 0) {
                    utl.InsertarHistorialCartera(objSession.CodigoEmpresa, objSession.CodigoSucursal, obj.Pclid, obj.Ctcid, obj.Ccbid, Int32.Parse(obj.EstadoCartera), obj.Monto.ToString(), obj.Saldo.ToString(), objSession.UserId, "");
                }
            }

            return Json(salida);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DemandaPendienteCpbt(ComprobanteModel model)
        {
            int salida = 0;
            if (objSession.PrfId == 6 || objSession.PrfId == 9 || objSession.PrfId == 8 || objSession.PrfId == 1)
            {
                bcp.Comprobante.DemandaPendienteCpbt(objSession.CodigoEmpresa, model.Pclid, model.Ctcid, model.Ccbid, objSession.UserId, model.DemandaPendiente);
            }
            return Json(salida);
        }

        public ActionResult CargarImagenesCpbt(int pclid, int ctcid)
        {
            return Json(bcp.Comprobante.ListarImagenesCpbt(objSession.CodigoEmpresa, pclid, ctcid));
        }
        #endregion

        #region "Carga Masiva"

        public JsonResult ProcesoCargaPanelDemandasMasivas(CargaMasivaModel model, GridSettings gridSettings)
        {
            try
            {
                string[] rut = model.RutCliente.Split('-');
                dto.CargaMasiva objCarga = new dto.CargaMasiva();
                objCarga.Archivo = model.Archivo;
                objCarga.ArchivoQuiebra = model.ArchivoQuiebra;
                objCarga.ArchivoJudicial = model.CargaJudicial;
                objCarga.CodigoCarga = model.CodigoCarga;
                objCarga.Contrato = model.Contrato;
                objCarga.NombreCliente = rut[1].Trim();
                objCarga.Pclid = model.Pclid;
                objCarga.RutCliente = rut[0].Trim();
                objCarga.TipoCartera = model.TipoCartera;

                if (model.Archivo != "")
                {
                    List<dto.CargaJudicial> lst = bcp.CargaMasiva.CargarDatosJudicial(model.Archivo);
                    bcp.CargaMasiva.ProcesoPanelCargaMasiva(lst, objCarga, objSession);
                }

                int totalPages = (int)Math.Ceiling((float)objCarga.ListaErrores.Count / (float)gridSettings.pageSize);

                var jsonData = new
                {
                    total = totalPages,
                    page = gridSettings.pageIndex,
                    records = objCarga.ListaErrores.Count,
                    rows =
                    (
                        from dto.ErrorCarga item in objCarga.ListaErrores
                        select new
                        {
                            id = 1,
                            cell = new object[]
                        {
                            item.Rut+"-"+item.Dv,
                            item.Nombre,
                            item.Numero,
                            item.TipoDocumento,
                            item.TipoError
                        }
                        }
                    ).ToArray()
                };

                return Json(new { success = true, data = objCarga.ListaErrores }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Cartera/CargaPanelCoopeuch", 150);
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult ProcesoCargaMasiva(CargaMasivaModel model, GridSettings gridSettings)
        {
            try
            {
                bool error = false;
                string[] rut = model.RutCliente.Split('-');
                dto.CargaMasiva objCarga = new dto.CargaMasiva();
                objCarga.Archivo = model.Archivo;
                objCarga.ArchivoQuiebra = model.ArchivoQuiebra;
                objCarga.ArchivoJudicial = model.CargaJudicial;
                objCarga.CodigoCarga = model.CodigoCarga;
                objCarga.Contrato = model.Contrato;
                objCarga.NombreCliente = rut[1].Trim();
                objCarga.Pclid = model.Pclid;
                objCarga.RutCliente = rut[0].Trim();
                objCarga.TipoCartera = model.TipoCartera;

                if (model.Archivo != "")
                {
                    if (model.CargaJudicial && !model.ArchivoQuiebra)
                    {
                        List<dto.CargaJudicial> lst = bcp.CargaMasiva.CargarDatosJudicialCsv(model.Archivo);
                        bcp.CargaMasiva.ProcesoCargaJudicial(lst, objCarga, objSession);
                    }

                    if (model.ArchivoQuiebra && !model.CargaJudicial)
                    {
                        List<dto.DatosCarga> lst = bcp.CargaMasiva.CargarDatos(model.Archivo);
                    }

                    if (!model.ArchivoQuiebra && !model.CargaJudicial)
                    {
                        List<dto.DatosCarga> lst;
                        switch (objCarga.Pclid)
                        {
                            case 424:
                                break;
                        }
                        if (objCarga.Pclid == 424)
                        {
                            lst = bcp.CargaMasiva.CargarDatosOriencoop(model.Archivo, objSession.CodigoEmpresa, objCarga.Pclid, Int32.Parse(objCarga.CodigoCarga));
                        }
                        else
                        {
                            lst = bcp.CargaMasiva.CargarDatos(model.Archivo);
                        }
                        switch (objCarga.Pclid)
                        {
                            case 559: //mutual ley previsional
                                error = bcp.CargaMasiva.ProcesoCargaMutualLey(lst, objCarga, objSession);
                                break;
                            default:
                                error = bcp.CargaMasiva.ProcesoCarga(lst, objCarga, objSession);
                                break;
                        }
                    }
                }

                int totalPages = (int)Math.Ceiling((float)objCarga.ListaErrores.Count / (float)gridSettings.pageSize);

                var jsonData = new
                {
                    total = totalPages,
                    page = gridSettings.pageIndex,
                    records = objCarga.ListaErrores.Count,
                    rows =
                    (
                        from dto.ErrorCarga item in objCarga.ListaErrores
                        select new
                        {
                            id = 1,
                            cell = new object[]
                            {
                                item.Rut+"-"+item.Dv,
                                item.Nombre,
                                item.Numero,
                                item.TipoDocumento,
                                item.TipoError
                            }
                        }
                    ).ToArray()
                };

                return Json(new { success = true, data = objCarga.ListaErrores }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Cartera/ProcesoCargaMasiva", 150);
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarCodigoCarga(int codemp, int pclid)
        {
            Comprobante obj = new Comprobante();
            return Json( new SelectList(obj.ListarCodigoCarga(codemp,pclid,"Seleccione"), "Value", "Text"));
        }

        public JsonResult ListarContrato(int codemp, int pclid, int tipoCartera)
        {
            Comprobante obj = new Comprobante();
            return Json(new SelectList(obj.ListarContrato(codemp, pclid,tipoCartera ,"Seleccione"), "Value", "Text"));
        }

        public ActionResult ExportToExcel(GridSettings gridSettings, List<dto.ErrorCarga> lst)
        {
            var products = new System.Data.DataTable("teste");
            products.Columns.Add("col1", typeof(int));
            products.Columns.Add("col2", typeof(string));

            products.Rows.Add(1, "product 1");
            products.Rows.Add(2, "product 2");
            products.Rows.Add(3, "product 3");
            products.Rows.Add(4, "product 4");
            products.Rows.Add(5, "product 5");
            products.Rows.Add(6, "product 6");
            products.Rows.Add(7, "product 7");

            bcp.Accion objAccion = new bcp.Accion();


            var grid = new GridView();
            grid.DataSource = objAccion.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }
        #endregion

        #region "Anular Carga Masiva"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAnularCarga(GridSettings gridSettings)
        {
            // create json data 
            Funciones objFunc = new Funciones();
            int estado = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 17);
            int totalRecords = Dimol.Carteras.bcp.CargaMasiva.ListarCargasAnularCount(objSession.CodigoEmpresa, estado, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.AnularCargaMasiva> lst = Dimol.Carteras.bcp.CargaMasiva.ListarCargasAnular(objSession.CodigoEmpresa, estado, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

          


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.AnularCargaMasiva item in lst
                    select new
                    {
                        id = item.Codemp + "|" + item.Pclid + "|" + estado + "|" + item.Fecha,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Pclid,
                            item.NombreCliente,
                            item.Fecha,
                            item.Usuario,
                            item.IdUsuario
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperAnularCarga( Dimol.Carteras.dto.AnularCargaMasiva model, string oper, string id)
        {
            Dimol.Carteras.bcp.CargaMasiva.OperCargasAnular(oper, id, objSession, model);
            return Json("OK");
        }
        #endregion

        #region "Aprobar Carga Masiva"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAprobarCarga(GridSettings gridSettings)
        {
            // create json data 
            Funciones objFunc = new Funciones();
            int totalRecords = Dimol.Carteras.bcp.CargaMasiva.ListarCargasAprobarCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.AprobarCargaMasiva> lst = Dimol.Carteras.bcp.CargaMasiva.ListarCargasAprobar(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);




            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.AprobarCargaMasiva item in lst
                    select new
                    {
                        id = objSession.CodigoEmpresa + "|" + item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Pclid,
                            item.Ctcid,
                            item.Ccbid,
                            item.NombreCliente,
                            item.RutDeudor,
                            item.NombreDeudor,
                            item.TipoDocumento,
                            item.Numero,
                            item.FechaVencimiento,
                            item.FechaIngreso,
                            item.MontoAsignado,
                            item.Monto,
                            item.Saldo
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AprobarCarga(List<String> ids)
        {
            string salida = Dimol.Carteras.bcp.CargaMasiva.AprobarCarga(ids,objSession);
            return Json(salida);
        }
        #endregion

        #region "Carga Pago"

        public JsonResult ProcesoCargaPago(CargaMasivaModel model, GridSettings gridSettings)
        {
            bool error = false;
            string[] rut = model.RutCliente.Split('-');
            dto.CargaMasiva objCarga = new dto.CargaMasiva();
            objCarga.Archivo = model.Archivo;
            objCarga.ArchivoQuiebra = model.ArchivoQuiebra;
            objCarga.ArchivoJudicial = model.CargaJudicial;
            objCarga.CodigoCarga = model.CodigoCarga;
            objCarga.Contrato = model.Contrato;
            objCarga.NombreCliente = rut[1].Trim();
            objCarga.Pclid = model.Pclid;
            objCarga.RutCliente = rut[0].Trim();
            objCarga.TipoCartera = model.TipoCartera;

            if (model.Archivo != "")
            {
                List<dto.DatosCargaPago> lst;
                if (objCarga.Pclid == 424)
                {
                    lst = bcp.CargaMasiva.CargarDatosPagoOriencoop(model.Archivo, objSession.CodigoEmpresa, objCarga.Pclid);
                }
                else
                {
                    lst = bcp.CargaMasiva.CargarDatosPago(model.Archivo);
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    error = bcp.CargaMasiva.ProcesoCargaPagos(lst, objCarga, objSession);
                    if (!error)
                    {
                        scope.Complete();
                    }
                }
            }

            int totalPages = (int)Math.Ceiling((float)objCarga.ListaErrores.Count / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = objCarga.ListaErrores.Count,
                rows =
                (
                    from dto.ErrorCarga item in objCarga.ListaErrores
                    select new
                    {
                        id = 1,
                        cell = new object[]
                        {
                            item.Rut+"-"+item.Dv,
                            item.Nombre,
                            item.Numero,
                            item.TipoDocumento,
                            item.TipoError
                        }
                    }
                ).ToArray()
            };
            return Json(objCarga.ListaErrores, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Comprobante"

        #region "Buscar Comprobante"
        public ActionResult GetComprobantes(GridSettings gridSettings, BuscarComprobanteModel model)
        {
            // create json data 


            int totalRecords = bcp.Comprobante.ListarComprobantesCount(objSession.CodigoEmpresa, objSession.Idioma, objSession.CodigoSucursal, model.TipoDocumento == null ? 0 : Int32.Parse(model.TipoDocumento), model.Numero == null ? 0 : Int32.Parse(model.Numero), model.Pclid == null ? 0 : Int32.Parse(model.Pclid), model.FechaDesdeEmision, model.FechaHastaEmision, model.FechaVencimientoDesde, model.FechaVencimientoHasta, model.MontoDesde == null ? 0 : Int32.Parse(model.MontoDesde), model.MontoHasta == null ? 0 : Int32.Parse(model.MontoHasta ),model.Rut, model.NombreFantasia, model.Telefono, model.Email,model.Direccion,model.Estado, model.Ctcid == null ? 0 : Int32.Parse(model.Ctcid), model.Tribunal == null ? 0 : Int32.Parse(model.Tribunal), model.Rol, model.Moneda,model.NumeroInterno,model.Producto,model.Comentario, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize, model.Tipo);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarComprobante> lst = bcp.Comprobante.ListarComprobantes(objSession.CodigoEmpresa, objSession.Idioma, objSession.CodigoSucursal, model.TipoDocumento == null ? 0 : Int32.Parse(model.TipoDocumento), model.Numero == null ? 0 : Int32.Parse(model.Numero), model.Pclid == null ? 0 : Int32.Parse(model.Pclid), model.FechaDesdeEmision, model.FechaHastaEmision, model.FechaVencimientoDesde, model.FechaVencimientoHasta, model.MontoDesde == null ? 0 : Int32.Parse(model.MontoDesde), model.MontoHasta == null ? 0 : Int32.Parse(model.MontoHasta), model.Rut, model.NombreFantasia, model.Telefono, model.Email, model.Direccion, model.Estado, model.Ctcid == null ? 0 : Int32.Parse(model.Ctcid), model.Tribunal == null ? 0 : Int32.Parse(model.Tribunal), model.Rol, model.Moneda, model.NumeroInterno, model.Producto, model.Comentario, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow, model.Tipo);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarComprobante item in lst
                    select new
                    {
                        id = item.IdTipoDocumento + "-" + item.Numero,
                        cell = new object[]
                        {
                            //item.Rut,
                            //item.NombreFantasia,
                            //item.TipoDocumento,
                            //item.NumeroCliente,
                            //item.Numero,
                            //item.FechaEmision,
                            //item.FechaVencimiento,
                            //item.Final,
                            //item.Saldo,
                            //item.Estado
                            item.NombreFantasia,
                            item.TipoDocumento,
                            item.NumeroCliente,
                            item.Final,
                            item.Saldo,
                            item.Estado,
                            item.RutDeudor,
                            item.NombreDeudor,
                            item.Rol,
                            item.Tribunal
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Aceptar Comprobantes"

        public ActionResult GetAceptarComprobantes(GridSettings gridSettings, BuscarComprobanteModel model)
        {
            // create json data 
            int totalRecords = bcp.Comprobante.ListarAceptarComprobantesCount(objSession.CodigoEmpresa, objSession.Idioma, objSession.CodigoSucursal, model.Tipo, model.Estado, model.Cartera,  gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarAceptarComprobante> lst = bcp.Comprobante.ListarAceptarComprobantes(objSession.CodigoEmpresa, objSession.Idioma, objSession.CodigoSucursal, model.Tipo, model.Estado, model.Cartera,  gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarAceptarComprobante item in lst
                    select new
                    {
                        id = item.IdTipoDocumento + "|" + item.Numero + "|" + item.Fecha.ToShortDateString()+ "|" + item.FechaContable.ToShortDateString() ,
                        cell = new object[]
                        {
                            item.IdTipoDocumento,
                            item.NombreFantasia,
                            item.TipoDocumento,
                            item.NumeroCliente,
                            item.Fecha,
                            item.Gestion,
                            item.Monto,
                            item.Usuario,
                            item.Deudor,
                            item.Rol,
                            item.Tribunal,
                            item.Asegurado,
                            string.Join(", <br/>", bcp.Comprobante.ListarLinkRutasEstampes(objSession.CodigoEmpresa, item.Pclid, item.Ctcid, item.Rolid, item.IdTipoDocumento, item.Numero).ToArray())
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEstadoComprobantes(GridSettings gridSettings, BuscarComprobanteModel model)
        {
            // create json data 
            int totalRecords = bcp.Comprobante.ListarEstadoComprobantesCount(objSession.CodigoEmpresa, objSession.Idioma, objSession.CodigoSucursal, model.Tipo, model.Estado,  objSession.PrfId, model.FechaEmisionDesde, model.FechaEmisionHasta, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarEstadoComprobante> lst = bcp.Comprobante.ListarEstadoComprobantes(objSession.CodigoEmpresa, objSession.Idioma, objSession.CodigoSucursal, model.Tipo, model.Estado, objSession.PrfId, model.FechaEmisionDesde, model.FechaEmisionHasta, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarEstadoComprobante item in lst
                    select new
                    {
                        id = item.IdTipoDocumento + "|" + item.Numero ,
                        cell = new object[]
                        {
                            Funciones.formatearRut( item.Rut),
                            item.NombreFantasia,
                            item.TipoDocumento,
                            item.NumeroCliente,
                            item.Bruto,
                            item.Retenido,
                            item.Monto,
                            item.Fecha,
                            item.Cliente,
                            item.Usuario,
                            string.Join(", <br/>", bcp.Comprobante.ListarLinkRutasEstampes(objSession.CodigoEmpresa, item.Pclid, item.Ctcid, item.Rolid, item.IdTipoDocumento, item.Numero).ToArray())
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GrabarAceptarComprobantes(List<String> ids)
        {
            //string salida = Dimol.Carteras.bcp.CargaMasiva.AprobarCarga(ids,objSession);
            string[] splitted;
            List<dto.AceptarComprobante> lst = new List<dto.AceptarComprobante>();
            foreach(string s in ids)
            {
                splitted = s.Split('|');
                lst.Add(new dto.AceptarComprobante()
                {
                    IdTipoDocumento = Int32.Parse(splitted[0]),
                    Numero = Int32.Parse(splitted[1]),
                    Fecha = DateTime.Parse(splitted[2]),
                    FechaContable = DateTime.Parse(splitted[3]) == new DateTime() ? "" : splitted[3]
                });

            }

            List<Combobox> salida =  bcp.Comprobante.AceptarComprobantes(lst, objSession);
            //bcp.Contabilidad.GrabarCpbt()

            return Json(salida);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ContabilizaBH(List<String> ids)
        {
            //string salida = Dimol.Carteras.bcp.CargaMasiva.AprobarCarga(ids,objSession);
            string[] splitted;
            List<dto.AceptarComprobante> lst = new List<dto.AceptarComprobante>();
            foreach (string s in ids)
            {
                splitted = s.Split('|');
                lst.Add(new dto.AceptarComprobante()
                {
                    IdTipoDocumento = Int32.Parse(splitted[0]),
                    Numero = Int32.Parse(splitted[1])
                });

            }

            List<Combobox> salida = bcp.Comprobante.ContabilizarComprobantes(lst, objSession);
            //bcp.Contabilidad.GrabarCpbt()

            return Json(salida);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FacturaBH(List<String> ids)
        {
            //string salida = Dimol.Carteras.bcp.CargaMasiva.AprobarCarga(ids,objSession);
            string[] splitted;
            List<dto.AceptarComprobante> lst = new List<dto.AceptarComprobante>();
            foreach (string s in ids)
            {
                splitted = s.Split('|');
                lst.Add(new dto.AceptarComprobante()
                {
                    IdTipoDocumento = Int32.Parse(splitted[0]),
                    Numero = Int32.Parse(splitted[1])
                });

            }

            List<Combobox> salida = bcp.Comprobante.FacturarComprobantes(lst, objSession);
            //bcp.Contabilidad.GrabarCpbt()

            return Json(salida);
        }

        public ActionResult GeneraSalidaBH(string desde, string hasta, string tipo)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            DateTime inicio = new DateTime(), fin = new DateTime();
            if(string.IsNullOrEmpty(desde))
            {
                inicio =  DateTime.Today;
            }
            else
            {
                inicio = DateTime.Parse(desde, currentCulture);
            }
            if(string.IsNullOrEmpty(hasta))
            {
                fin =  DateTime.Today.AddDays(1);
            }
            else
            {
                fin = DateTime.Parse(hasta, currentCulture);
            }


            var grid = new GridView();
            switch (tipo)
            {
                case "A":
                    grid.DataSource = bcp.Comprobante.ListarBHContabilizadas(objSession.CodigoEmpresa, inicio, fin);
                    break;
                case "C":
                    grid.DataSource = bcp.Comprobante.ListarBHFacturadas(objSession.CodigoEmpresa, inicio, fin);
                    break;
            }
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");

        }

        #endregion

        #region "Comprobante"
        public ActionResult ListarSucursal(int pclid)
        {
            return Json(new SelectList(bcp.Comprobante.ListarSucursales(objSession.CodigoEmpresa, pclid, ""), "Value", "Text"));
        }

        public ActionResult TraeClasificacionComprobante(int tpcid)
        {
            return Json(bcp.Comprobante.TraeClasificacionComprobante(objSession.CodigoEmpresa, tpcid));
        }

        public ActionResult GrabarComprobante(CabeceraComprobanteModel model)
        {
            dto.CabeceraComprobante obj = new dto.CabeceraComprobante();
            string[] salida;

            obj.Aplica = model.Aplica;
            obj.CabeceraId = model.CabeceraId;
            obj.Cambiodoc = model.Cambiodoc;
            obj.Cancela = model.Cancela;
            obj.Cartcli = model.Cartcli;
            obj.Contable = model.Contable;
            obj.Costos = model.Costos;
            obj.Cptoctbl = model.Cptoctbl;
            obj.Ctcid = model.Ctcid;
            obj.Ccbid = model.Ccbid;
            obj.Estado = model.Estado;
            obj.FechaContabilizacion = model.FechaContabilizacion;
            obj.FechaDocumento = model.FechaDocumento;
            obj.FechaEntrega = model.FechaEntrega;
            obj.FechaIngreso = model.FechaIngreso;
            obj.FechaOrdenCompra = model.FechaOrdenCompra;
            obj.FechaVencimiento = model.FechaVencimiento;
            obj.Findeuda = model.Findeuda;
            obj.FormaPago = model.FormaPago;
            obj.Forpag = model.Forpag;
            obj.Glosa = model.Glosa;
            obj.Libcompra = model.Libcompra;
            obj.Moneda = model.Moneda;
            obj.MotivoCobranza = model.MotivoCobranza;
            obj.NombreCliente = model.NombreCliente;
            obj.Numero = model.Numero;
            obj.NumeroOC = model.NumeroOC;
            obj.Ordcomp = model.Ordcomp;
            obj.Pclid = model.Pclid;
            obj.Remesa = model.Remesa;
            obj.RutCliente = model.RutCliente;
            obj.Selapl = model.Selapl;
            obj.Selcpbt = model.Selcpbt;
            obj.Sucursal = model.Sucursal=="null"? null: model.Sucursal ;
            obj.Tipcpbtdoc = model.Tipcpbtdoc;
            obj.Tipdig = model.Tipdig;
            obj.TipoCambio = model.TipoCambio;
            obj.TipoComprobante = model.TipoComprobante;
            obj.TipoComprobanteDesc = model.TipoComprobanteDesc;
            obj.TipoGasto = model.TipoGasto;
            obj.Tipprod = model.Tipprod;
            obj.Clbid = model.Clbid;
            obj.Sinimp = model.Sinimp;

            obj.Codemp = objSession.CodigoEmpresa;
            obj.Codsuc = objSession.CodigoSucursal;
            obj.Tipo = model.Tipo;
            obj.PJ = model.PJ;
            obj.Pag = model.Pag;
            obj.Descuento = decimal.Parse( model.Descuento?? "0");

            obj.DetalleGlosa = "Det: ";

            //foreach (var item in JArray.Parse(model.DetalleGlosa).ToObject<string[]>())
            foreach (var item in bcp.Comprobante.ListarDetalleComprobanteCompra(obj.Codemp, obj.Codsuc, obj.TipoComprobante, obj.CabeceraId, "", "Item", "asc", 0, 10000))
            {
                if (!string.IsNullOrEmpty(item.Abreviado))
                {
                    obj.DetalleGlosa = obj.DetalleGlosa + item.Abreviado + " - ";
                }
                
            }

            obj.DetalleGlosa = obj.DetalleGlosa.Substring(0, obj.DetalleGlosa.Length - 3);

            obj.Rolid = model.Rolid;

            obj.DetalleC.Add(new dto.DetalleCabeceraComprobante
            {
                Nombre = model.Nombre
            });

            using (TransactionScope scope = new TransactionScope())
            {
                salida = bcp.Comprobante.GrabarComprobante(obj, objSession);
                if (string.IsNullOrEmpty(salida[1]) || salida[0] != "-1")
                {
                    scope.Complete();
                }
            }
            var jsonData = new
            {
                id = salida[0],
                glosa = obj.Glosa,
                mensaje = salida[1]
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarItemComprobante(string term, int tipprod, int pclid, string gasto)
        {
            return Json(bcp.Comprobante.ListarItemComprobante(term, objSession.CodigoEmpresa, tipprod, pclid, gasto), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GrabarDetalleComprobante(CabeceraComprobanteModel model)
        {
            dto.CabeceraComprobante obj = new dto.CabeceraComprobante();
            string salida = "";

            obj.Aplica = model.Aplica;
            obj.CabeceraId = model.CabeceraId;
            obj.Cambiodoc = model.Cambiodoc;
            obj.Cancela = model.Cancela;
            obj.Cartcli = model.Cartcli;
            obj.Contable = model.Contable;
            obj.Costos = model.Costos;
            obj.Cptoctbl = model.Cptoctbl;
            obj.Ctcid = model.Ctcid;
            obj.Ccbid = model.Ccbid;
            obj.Estado = model.Estado;
            obj.FechaContabilizacion = model.FechaContabilizacion;
            obj.FechaDocumento = model.FechaDocumento;
            obj.FechaEntrega = model.FechaEntrega;
            obj.FechaIngreso = model.FechaIngreso;
            obj.FechaOrdenCompra = model.FechaOrdenCompra;
            obj.FechaVencimiento = model.FechaVencimiento;
            obj.Findeuda = model.Findeuda;
            obj.FormaPago = model.FormaPago;
            obj.Forpag = model.Forpag;
            obj.Glosa = model.Glosa;
            obj.Libcompra = model.Libcompra;
            obj.Moneda = model.Moneda;
            obj.MotivoCobranza = model.MotivoCobranza;
            obj.NombreCliente = model.NombreCliente;
            obj.Numero = model.Numero;
            obj.NumeroOC = model.NumeroOC;
            obj.Ordcomp = model.Ordcomp;
            obj.Pclid = model.Pclid;
            obj.Remesa = model.Remesa;
            obj.RutCliente = model.RutCliente;
            obj.Selapl = model.Selapl;
            obj.Selcpbt = model.Selcpbt;
            obj.Sucursal = model.Sucursal == "null" ? null : model.Sucursal;
            obj.Tipcpbtdoc = model.Tipcpbtdoc;
            obj.Tipdig = model.Tipdig;
            obj.TipoCambio = model.TipoCambio;
            obj.TipoComprobante = model.TipoComprobante;
            obj.TipoComprobanteDesc = model.TipoComprobanteDesc;
            obj.TipoGasto = model.TipoGasto;
            obj.Tipprod = model.Tipprod;
            obj.Clbid = model.Clbid;
            obj.Sinimp = model.Sinimp;

            obj.Codemp = objSession.CodigoEmpresa;
            obj.Codsuc = objSession.CodigoSucursal;
            obj.Tipo = model.Tipo;
            obj.PJ = model.PJ;
            obj.Pag = model.Pag;
            obj.Descuento = decimal.Parse(string.IsNullOrEmpty(model.Descuento) ? "0" : model.Descuento);
            obj.Modo = model.Modo;
            obj.Rolid = model.Rolid;
            obj.DetalleC.Add(new dto.DetalleCabeceraComprobante 
            {
                Item = model.Item,
                Insid = model.Insid,
                Precio = string.IsNullOrEmpty(model.Monto) ? 0 : decimal.Parse( model.Monto.Replace(".","")),
                PrecioReal = string.IsNullOrEmpty(model.Monto) ? 0 : decimal.Parse(model.Monto.Replace(".", "")),
                Cantidad = string.IsNullOrEmpty(model.Cantidad) ? 1 :decimal.Parse(model.Cantidad.Replace(".", "")),
                Retenido = model.ImpuestoRetenido ? "S" : "N"
            });

            using (TransactionScope scope = new TransactionScope())
            {
                salida = bcp.Comprobante.GrabarDetalleComprobante(obj, objSession);
                if (string.IsNullOrEmpty(salida))
                {
                    scope.Complete();
                }
            }




            return Json("");
        }

        public ActionResult TraeRolComprobante(CabeceraComprobanteModel model)
        {
            dto.CabeceraComprobante obj = new dto.CabeceraComprobante();
            obj.Codemp = objSession.CodigoEmpresa;
            obj.TipoRol = model.TipoRol;
            obj.NumeroRol = model.Rol.ToString() + (model.Anio != 0 ? "-" + model.Anio.ToString(): "");
            obj.Tribunal = model.Tribunal;

            return Json(bcp.Comprobante.TraeRolCabeceraComprobante(obj));
        }

        public JsonResult GetDetalleCompra(GridSettings gridSettings, int tcp, int numero)
        {
            // create json data 
            int totalRecords = bcp.Comprobante.ListarDetalleComprobanteCompraCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, tcp, numero, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DetalleComprobanteCompra> lst = bcp.Comprobante.ListarDetalleComprobanteCompra(objSession.CodigoEmpresa, objSession.CodigoSucursal, tcp, numero, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DetalleComprobanteCompra item in lst
                    select new
                    {
                        id = objSession.CodigoEmpresa + "|" + numero + "|" + item.Item + "|" + item.Insid,
                        cell = new object[]
                        {

                            item.Codigo,
                            item.Nombre,
                            item.Precio,
                            item.Cantidad,
                            item.Neto,
                            item.Retenido,
                            item.Impuesto,
                            item.TotalNeto,    
                            item.Abreviado,
                             @"<a href='\Documentos\Estampes\" + item.ArchivoEstampe.Replace("\\\\",@"\") + "' target='_blank'>" + item.ArchivoEstampe.Split('\\')[item.ArchivoEstampe.Split('\\').Length - 1] + "</a>",
                            item.NombreArchivo,
                            item.FecJud
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnularComprobante(CabeceraComprobanteModel model)
        {
            dto.CabeceraComprobante obj = new dto.CabeceraComprobante();
            string salida = "", estadoCpbt = "";
            int error = 0;

            obj.CabeceraId = model.CabeceraId;
            obj.TipoComprobante = model.TipoComprobante;
            obj.TipoCambio = model.TipoCambio;
         
            obj.Codemp = objSession.CodigoEmpresa;
            obj.Codsuc = objSession.CodigoSucursal;
            obj.Tipo = model.Tipo;
            obj.PJ = model.PJ;
            obj.Pag = model.Pag;
            //obj.Descuento = decimal.Parse(model.Descuento);

            using (TransactionScope scope = new TransactionScope())
            {
                estadoCpbt = bcp.Comprobante.TraeCabeceraComprobanteEstado(obj);
                salida = bcp.Comprobante.EliminarComprobante(obj);// AnularComprobante(obj);

              /*  if (string.IsNullOrEmpty(salida))
                {
                    if (estadoCpbt == "F") { 

                        List<dto.Comprobante> docs = new List<dto.Comprobante>();

                        docs = bcp.Comprobante.TraeListaCpbt(obj);

                        foreach (var item in docs)
                        {
                            item.Monto = item.Monto * (-1);
                            error = bcp.Comprobante.ModificarGastoJudicial(item, objSession, docs.Count());
                        }

                        if (error > 0)
                        {
                            salida = "";
                        }
                        else
                        {
                            salida = error.ToString();
                        }
                    }
                }*/

                if (string.IsNullOrEmpty(salida))
                {
                    scope.Complete();
                }
            }

            return Json(salida);
        }

        public ActionResult EliminarBH(List<String> ids)
        {
            //string salida = Dimol.Carteras.bcp.CargaMasiva.AprobarCarga(ids,objSession);
            string[] splitted;
            dto.CabeceraComprobante obj = new dto.CabeceraComprobante();
            string salida = "";
            foreach (string s in ids)
            {
                splitted = s.Split('|');
                obj = new dto.CabeceraComprobante()
                {
                    TipoComprobante = Int32.Parse(splitted[0]),
                    CabeceraId = Int32.Parse(splitted[1])
                };
                obj.Codemp = objSession.CodigoEmpresa;
                obj.Codsuc = objSession.CodigoSucursal;
                using (TransactionScope scope = new TransactionScope())
                {
                    salida = bcp.Comprobante.EliminarComprobante(obj);// AnularComprobante(obj);
                    if (string.IsNullOrEmpty(salida))
                    {
                        scope.Complete();
                    }
                }
            }

            //List<Combobox> salida = bcp.Comprobante.AceptarComprobantes(lst, objSession);

            //dto.CabeceraComprobante obj = new dto.CabeceraComprobante();
            //string salida = "";

            //obj.CabeceraId = model.CabeceraId;
            //obj.TipoComprobante = model.TipoComprobante;
            //obj.TipoCambio = model.TipoCambio;

            //obj.Codemp = objSession.CodigoEmpresa;
            //obj.Codsuc = objSession.CodigoSucursal;
            //obj.Tipo = model.Tipo;
            //obj.PJ = model.PJ;
            //obj.Pag = model.Pag;
            ////obj.Descuento = decimal.Parse(model.Descuento);

            //using (TransactionScope scope = new TransactionScope())
            //{
            //    salida = bcp.Comprobante.EliminarComprobante(obj);// AnularComprobante(obj);
            //    if (string.IsNullOrEmpty(salida))
            //    {
            //        scope.Complete();
            //    }
            //}

            return Json(salida);
        }

        public ActionResult ActualizarDatosComprobante(CabeceraComprobanteModel model)
        {
            int estado = 0;
            dto.CabeceraComprobante obj = bcp.Comprobante.BuscarComprobante(objSession.CodigoEmpresa, objSession.CodigoSucursal, model.TipoComprobante, model.CabeceraId);


            switch (obj.Estado)
            {
                case "E":
                    estado = 1;
                    break;
                case "A":
                    estado = 2;
                    break;
                case "F":
                    estado = 3;
                    break;
                case "B":
                    estado = 4;
                    break;
                case "C":
                    estado = 5;
                    break;
                case "X":
                    estado = 6;
                    break;
            }

            obj.Estado = estado.ToString();

            //model.Subtotal = obj.Neto.ToString("N2");
            //model.Neto = obj.Neto.ToString("N2");
            //model.Descuento = obj.Descuento.ToString("N2");
            //model.Impuestos = obj.Impuestos.ToString("N2");
            //model.Retenido = obj.Retenido.ToString("N2");
            //model.Total = obj.Saldo.ToString("N2");


            var jsonData = new
            {
                Estado=obj.Estado,
                Glosa= obj.Glosa,
                Subtotal=obj.Neto.ToString("N2"),
                Descuento=obj.Descuento.ToString("N2"),
                Neto= obj.Neto.ToString("N2"),
                Impuestos= obj.Impuestos.ToString("N2"),
                Retenido= obj.Retenido.ToString("N2"),
                Total= obj.Saldo.ToString("N2")
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarTribunal(string term)
        {
            return Json(bcp.Comprobante.ListarTribunalAuto(term), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region "Sub Cartera"

        public ActionResult GetSubCartera(GridSettings gridSettings, string nombre, string rut)
        {
            // create json data 
            int totalRecords = bcp.Comprobante.ListarSubCarterasCount(objSession.CodigoEmpresa, nombre, rut, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BuscarSubCartera> lst = bcp.Comprobante.ListarSubCarteras(objSession.CodigoEmpresa, nombre, rut, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BuscarSubCartera item in lst
                    select new
                    {
                        id = item.Sbcid,
                        cell = new object[]
                        {
                            item.Sbcid,
                            item.Rut,
                            item.Nombre
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarSubCartera(SubCarteraModel model)
        {
            int salida = 0;
            try
            {
                dto.SubCartera obj = new dto.SubCartera()
                {
                    Codemp = objSession.CodigoEmpresa,
                    Ciudad = model.Ciudad,
                    Comuna = model.Comuna,
                    Direccion = model.Direccion,
                    Nombre = model.Nombre,
                    Pais = model.Pais,
                    Region = model.Region,
                    Rut = model.Rut,
                    Sbcid = 0,
                    Telefono = model.Telefono

                };
                if (model.Sbcid != "" && model.Sbcid != null)
                {
                    obj.Sbcid = Int32.Parse(model.Sbcid);
                    salida = bcp.Comprobante.ModificarSubcartera(obj);
                }
                else
                {
                    salida = bcp.Comprobante.InsertarSubcartera(obj);
                }
                return Json(salida);
            }
            catch (Exception ex)
            {
                return Json( ex.StackTrace);
            }

        }

        public ActionResult EliminarSubCartera(int id)
        {
            int salida = 0;
            try
            {
                salida = bcp.Comprobante.EliminarSubcartera(id, objSession.CodigoEmpresa);
                return Json(salida);
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }

        }
        #endregion

        #region "Anular Restriccion Gestor"

        public ActionResult GetRestriccionesGestor(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = bcp.Gestor.ListarRestriccionesGestorCount(objSession.CodigoEmpresa,objSession.CodigoSucursal, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.RestriccionGestor> lst = bcp.Gestor.ListarRestriccionesGestor(objSession.CodigoEmpresa, objSession.CodigoSucursal, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.RestriccionGestor item in lst
                    select new
                    {
                        id = item.Usrid+"|" + item.Gesid ,
                        cell = new object[]
                        {
                            item.NombreUsuario,
                            item.NombreGestor,
                            item.FechaDesde,
                            item.FechaHasta
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperAnularRestriccion(dto.RestriccionGestor model, string oper, string id)
        {
            model.Codemp = objSession.CodigoEmpresa;
            model.Sucid = objSession.CodigoSucursal;
            if (oper == "del")
            {
                string[] ids = id.Split('|');
                model.Usrid = Int32.Parse(ids[0]);
                model.Gesid = Int32.Parse(ids[1]);
            }
            bcp.Gestor.OperAnularRestriccion(model, oper, id);
            return Json("OK");
        }

        #endregion

        #region "Documentos Deudor"

        public ActionResult GuardarDocumentoDeudor(DocumentoDeudorModel model)
        {
            int salida = 0;
            string tipoTipoDocumento = "";
            Funciones objFunc = new Funciones();
            try
            {
                dto.DocumentoDeudor obj = new dto.DocumentoDeudor()
                {
                    Codemp = objSession.CodigoEmpresa,
                    Ctcid = Int32.Parse( model.Ctcid),
                    Pclid = model.Pclid,
                    Dcdid = 0,
                    Archivo = model.Archivo,
                    TipoDocumento = model.TipoDocumento
                };
                tipoTipoDocumento=bcp.Deudor.TraeTipoTipoDocumento(obj.Codemp, Int32.Parse( obj.TipoDocumento));
                if (tipoTipoDocumento == "C" && string.IsNullOrEmpty(obj.Pclid))
                {
                    return Json( objFunc.TraeError("Cliente", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
                }
                if (!string.IsNullOrEmpty(obj.Pclid))
                {
                    if (tipoTipoDocumento != "C")
                    {
                        obj.Pclid = null;
                    }
                }
                salida = bcp.Deudor.GuardarDocumentoDeudor(obj.Codemp, obj.Ctcid, Int32.Parse( obj.TipoDocumento), obj.Archivo, model.Pclid);

                if (salida <= 0)
                {
                    return Json(objFunc.TraeError("ErrLog", objSession.Idioma));
                }
                return Json(salida);
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }

        public ActionResult GuardarDocumentoDeudor(dto.DocumentoDeudor obj)
        {
            int salida = 0;
            Funciones objFunc = new Funciones();
            try
            {
                if (obj.TipoTipoDocumento != "C")
                {
                    obj.Pclid = null;

                }
                salida = bcp.Deudor.GuardarDocumentoDeudor(obj.Codemp, obj.Ctcid, Int32.Parse(obj.TipoDocumento), obj.Archivo, obj.Pclid);

                if (salida <= 0)
                {
                    return Json(objFunc.TraeError("ErrLog", objSession.Idioma));
                }
                return Json(salida);
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }

        public ActionResult GetDocumentosDeudor(GridSettings gridSettings, DocumentoDeudorModel model)
        {
            Funciones objFunc = new Funciones();
            // create json data 
            int totalRecords = bcp.Deudor.ListarDocumentosDeudorCount(objSession.CodigoEmpresa, objSession.Idioma, Int32.Parse( model.Ctcid), model.Pclid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoDeudor> lst = bcp.Deudor.ListarDocumentosDeudor( objSession.CodigoEmpresa, objSession.Idioma, Int32.Parse( model.Ctcid), model.Pclid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoDeudor item in lst
                    select new
                    {
                        id = ConfigurationManager.AppSettings["UrlArchivos"] + objFunc.Configuracion_Str(13) + "/" + objSession.CodigoEmpresa + "/"+ item.RutDeudor +"/"+ item.Archivo,
                        cell = new object[]
                        {
                            item.Pclid,
                            item.Ctcid,
                            item.Dcdid,
                            item.NombreCliente,
                            item.TipoDocumento,
                            item.Archivo
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Agregar Gestiones"

        public JsonResult ListarContactos( int ctcid)
        {
            bcp.Deudor obj = new Deudor();
            return Json(new SelectList( obj.ListarContactos(objSession.CodigoEmpresa, ctcid), "Value", "Text"));
        }

        public JsonResult ListarTelefonosContactos(int ctcid)
        {
            return Json(new SelectList(Deudor.ListarTelefonosContactos(objSession.CodigoEmpresa, ctcid), "Value", "Text"));
        }

        public JsonResult ListarEstadosHistorial(int grupo, string tipo, string estadoXDoc)
        {
            return Json(new SelectList(bcp.Comprobante.ListarEstadosHistorial(objSession.CodigoEmpresa, grupo, objSession.Idioma, tipo, estadoXDoc, objSession.PrfId), "Value", "Text"));
        }
        public JsonResult ListarEstadosCobranzaClientePerfil(int grupo, string pclid, string estadoXDoc)
        {
            return Json(new SelectList(bcp.Comprobante.ListarEstadosCobranzaClientePerfil(objSession.CodigoEmpresa, grupo, objSession.Idioma, string.IsNullOrEmpty(pclid) ? 0 : Int32.Parse(pclid), estadoXDoc, objSession.PrfId), "Value", "Text"));
        }

        public JsonResult BuscarAccionesAgrupa(int id)
        {
            return Json(bcp.Accion.BuscarAccionesAgrupa(objSession.CodigoEmpresa, id));
        }

        public JsonResult BuscarDestalleEstado(int id)
        {
            return Json(bcp.Comprobante.TraeDetalleEstado(objSession.CodigoEmpresa, id));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDocumentosHistorial(GridSettings gridSettings, AgregarGestionModel model)
        {
            // create json data 

            int pclid = 0;
            int ctcid = 0;
            if (model.Pclid != null)
            {
                pclid = Int32.Parse(model.Pclid);
            }

            if (model.Ctcid != null)
            {
                ctcid = Int32.Parse(model.Ctcid);
            }

            int totalRecords = bcp.Comprobante.ListarDocumentosHistorialCount(objSession.CodigoEmpresa, pclid, ctcid, model.Tipo,  gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, 0, 10000000);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Comprobante> lst = bcp.Comprobante.ListarDocumentosHistorial(objSession.CodigoEmpresa, pclid, ctcid, model.Tipo, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow); 
           
            lst.Where(w => w.FechaPlazo == new DateTime()).ToList().ForEach(s => s.FechaPlazo = null);
            lst.Where(w => w.FechaCalculoInteres == new DateTime()).ToList().ForEach(s => s.FechaCalculoInteres = null);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Comprobante item in lst
                    select new
                    {
                        id = item.Ccbid,
                        cell = new object[]
                        {
                            item.Ccbid,
                            item.TipoCpbtNombre,
                            item.NumeroCpbt,
                            item.FechaDocumento,
                            item.Monto,
                            item.Saldo,
                            item.Compromiso
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarGestiones(AgregarGestionModel model)
        {
            Funciones objFunc = new Funciones();
            dto.DetalleEstados objDetalleEstado = bcp.Comprobante.TraeDetalleEstado(objSession.CodigoEmpresa, model.TipoEstado);
            DateTime fechaAccion = new DateTime();
            int error = 0;
            var lstFinal= new List<dto.Comprobante>() ;

            if(model.TipoGestion == 0 ){
                return Json(objFunc.TraeEtiqueta("Accion", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            if(string.IsNullOrEmpty( model.Comentario)){
                return Json(objFunc.TraeEtiqueta("AgrAc5", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            if(model.CambiaEstado && model.TipoEstado == 0 ){
                return Json(objFunc.TraeEtiqueta("Estado", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            if(model.CambiaEstado && model.TipoEstado == 0 && model.EstadosXDocumentos ){
                return Json(objFunc.TraeEtiqueta("Docu", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            if ((model.CambiaEstado && model.TipoEstado > 0 && objDetalleEstado.Compromiso == "S" ) && string.IsNullOrEmpty(model.FechaHistorial))
            {
                return Json(objFunc.TraeEtiqueta("Fecha", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<dto.ComprobanteCorto> lstGrilla = serializer.Deserialize<List<dto.ComprobanteCorto>>(model.Documentos);

            using (TransactionScope scope = new TransactionScope())
            {
                //Grabo la accion
                error = bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), model.TipoGestion, objSession.CodigoSucursal, objSession.Gestor, model.Contacto > 0 ? "S" : "N", objSession.IpRed, objSession.IpPc, objSession.UserId, model.Comentario, model.Contacto, string.IsNullOrEmpty(model.TelefonoHistorial) ? 0 : Int64.Parse(model.TelefonoHistorial.Trim()));
                //error = 1;

                if (model.MostrarTelefono && error > 0)
                {
                    string estado = "0";
                    switch (model.ResultadoLlamado)
                    {
                        case 1:
                        case 2:
                        case 3:
                            estado = "A";
                            break;
                        case 5:
                            estado = "M";
                            break;
                    }
                    //Actualizo estado del telefono
                    error = bcp.Deudor.EditarTelefonoPrioridad(objSession.CodigoEmpresa, model.Ctcid, Int64.Parse(model.TelefonoHistorial), estado, model.ResultadoLlamado);
                    // error = 1;
                }

                if (model.CambiaEstado && error > 0 )
                {
                    //Tomo la fecha de la gestion
                    fechaAccion = bcp.Accion.BuscarUltimaFechaAcciones(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), model.TipoGestion);
                    //Genero la lista de documentoa a modificar
                    List<dto.Comprobante> lst = bcp.Comprobante.ListarDocumentosHistorial(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), model.EstadoCpbt, "", "TipoCpbtNombre", "asc", 0, 10000);
                    string[] allowedStatus = { };
                    if (!string.IsNullOrEmpty(  model.Ids)) {
                        serializer = new JavaScriptSerializer();
                        allowedStatus = serializer.Deserialize<string[]>(model.Ids); 
                    } 
                    if (model.EstadosXDocumentos)
                    {
                        var lstFiltrada = from cpbt in lst
                                          where allowedStatus.Contains(cpbt.Ccbid.ToString())
                                          select cpbt;
                        lstFinal = lstFiltrada.ToList();
                    }
                    else
                    {
                        lstFinal = lst;
                    }
                    if (model.EstadosXDocumentos)
                    {
                        foreach (dto.Comprobante cpbt in lstFinal)
                        {
                            //Actualizo fecha y monto de comprommiso
                            if (objDetalleEstado.Compromiso == "S" && model.MostrarFecha)
                            {
                                decimal compromiso = (lstGrilla.Find(x => x.Ccbid == cpbt.Ccbid)).Compromiso;
                                error = bcp.Comprobante.ActualizarCarteraClientesCompromiso(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, compromiso, DateTime.Parse(model.FechaHistorial));
                            }
                            else
                            {
                                error = bcp.Comprobante.ActualizarCarteraClientesCompromiso(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, 0, null);
                            }
                            //Actualizo historial de los documentos
                            if (error > 0)
                            {
                                error = bcp.Comprobante.InsertarCarteraClientesEstadosHistorialEspecial(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, fechaAccion, model.TipoEstado, objSession.CodigoSucursal, objSession.Gestor, objSession.IpRed, objSession.IpPc, "", cpbt.Monto, cpbt.Saldo, objSession.UserId);
                            }
                            //Actualizo estado de los documentos
                            if (error > 0)
                            {
                                error = bcp.Comprobante.ActualizaCarteraEstados(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, model.TipoEstado, model.Tipo);
                            }
                        }
                    }
                    else
                    {
                        //Actualizo estado de los documentos fecha ultima gestion
                        error = bcp.Comprobante.ActualizarEstadoCarteraClientesTodos(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), model.TipoEstado, model.EstadoCpbt);
                    }
                }
                else
                {
                    //Actualizo fecha ultima gestion
                    error = bcp.Comprobante.ActualizarCarteraClientesUltimaGestion(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), model.EstadoCpbt);
                }
                if (error> 0)
                {
                    scope.Complete();
                }
            }

            return Json(error);
        }

        public JsonResult GuardarContacto(ContactoTelefonoMailModel model)
        {
            string salida = "";
            int error = 0;
            switch (model.TipoForm)
            {
                case "T":
                    dto.Telefono obj = new dto.Telefono();
                    obj.Codemp = objSession.CodigoEmpresa;
                    obj.Ctcid = Int32.Parse(model.Ctcid);
                    obj.Numero = Int32.Parse(model.Telefono);
                    obj.TipoTelefono = model.TipoTelefono;
                    obj.DescEstado = model.EstadoTelefono;
                    obj.IdEstado = model.EstadoTelefono;
                    obj.NombreContacto = model.NombreContacto;
                    obj.TipoContacto = model.TipoContacto;
                    obj.Comuna = model.IdComuna;
                    obj.Direccion = model.DireccionContacto;
                    obj.Ddcid = model.Ddcid;
                    obj.EstadoDireccion = model.EstadoDireccion;
                    if (obj.Ddcid == 0)
                    {
                        int id = bcp.Deudor.InsertarContacto(obj);
                        obj.Ddcid = id;
                    }
                    else
                    {
                        error = bcp.Deudor.EditarContacto(obj);
                    }

                    error = bcp.Deudor.InsertarTelefonoContacto(obj);
                    if (model.Pclid == "522")
                    {
                        dto.SitrelDeudorTelefono objST = new dto.SitrelDeudorTelefono();
                        objST.Codemp = obj.Codemp;
                        objST.Ctcid = obj.Ctcid;
                        objST.Numero = obj.Numero.ToString();
                        objST.TipoTelefono = obj.TipoTelefono;
                        objST.Anexo = model.Anexo;
                        objST.CodigoArea = obj.CodigoArea;
                        objST.Origen = "G";
                        objST.Enviado = "N";
                        bcp.CargaItau.InsertarTelefono(objST);
                    }
                    break;
                case "E":
                    dto.Email objEmail = new dto.Email();
                    objEmail.Codemp = objSession.CodigoEmpresa;
                    objEmail.Ctcid = Int32.Parse(model.Ctcid);
                    objEmail.Mail = model.Email;
                    objEmail.TipoEmail = model.TipoEmail;
                    objEmail.Masivo = model.EmailMasivo ? "S" : "N";
                    objEmail.IdEstado = model.EstadoTelefono;
                    objEmail.NombreContacto = model.NombreContacto;
                    objEmail.TipoContacto = model.TipoContacto;
                    objEmail.Comuna = model.IdComuna;
                    objEmail.Direccion = model.DireccionContacto;
                    objEmail.Ddcid = model.Ddcid;
                    objEmail.EstadoDireccion = model.EstadoDireccion;
                    objEmail.Pclid = model.Pclid;
                    objEmail.FechaCreacion = DateTime.Now;
                    objEmail.UserId = objSession.UserId;
                    if (objEmail.Ddcid == 0)
                    {
                        int id = bcp.Deudor.InsertarContacto(objEmail);
                        objEmail.Ddcid = id;
                    }
                    else
                    {
                        error = bcp.Deudor.EditarContacto(objEmail);
                    }
                    error = bcp.Deudor.InsertarEmailContactoProvider(objEmail);
                    if (model.Pclid == "522")
                    {
                        dto.SitrelDeudorEmail objST = new dto.SitrelDeudorEmail();
                        objST.Codemp = objEmail.Codemp;
                        objST.Ctcid = objEmail.Ctcid;
                        objST.Email = objEmail.Mail;
                        objST.Origen = "G";
                        objST.Enviado = "N";
                        bcp.CargaItau.InsertarEmail(objST);
                    }
                    break;
                case "D":
                    dto.Email objDireccion = new dto.Email();
                    objDireccion.Codemp = objSession.CodigoEmpresa;
                    objDireccion.Ctcid = Int32.Parse(model.Ctcid);
                    objDireccion.Mail = model.Email;
                    objDireccion.TipoEmail = model.TipoEmail;
                    objDireccion.Masivo = model.EmailMasivo ? "S" : "N";
                    objDireccion.IdEstado = model.EstadoTelefono;
                    objDireccion.NombreContacto = model.NombreContacto;
                    objDireccion.TipoContacto = model.TipoContacto;
                    objDireccion.Comuna = model.IdComuna;
                    objDireccion.Direccion = model.DireccionContacto;
                    objDireccion.Ddcid = model.Ddcid;
                    objDireccion.EstadoDireccion = model.EstadoDireccion;
                    if (!string.IsNullOrEmpty(model.NombreContacto))
                    {
                        if (objDireccion.Ddcid == 0)
                        {
                            int id = bcp.Deudor.InsertarContacto(objDireccion);
                            objDireccion.Ddcid = id;
                        }
                        else
                        {
                            error = bcp.Deudor.EditarContacto(objDireccion);
                        }
                    }

                    //if (model.Pclid == "522")
                    {
                        dto.SitrelDeudorDireccion objST = new dto.SitrelDeudorDireccion();
                        objST.Codemp = objDireccion.Codemp;
                        objST.Ctcid = objDireccion.Ctcid;
                        objST.Direccion = objDireccion.Direccion;
                        objST.Comuna = objDireccion.Comuna.ToString();
                        objST.TipoDireccion = model.TipoDireccion;
                        objST.Origen = "G";
                        objST.Enviado = "N";
                        bcp.CargaItau.InsertarDireccion(objST);
                    }
                    break;
            }
            //if (model.TipoForm == "T")
            //{
            //    dto.Telefono obj = new dto.Telefono();
            //    obj.Codemp = objSession.CodigoEmpresa;
            //    obj.Ctcid = Int32.Parse( model.Ctcid);
            //    obj.Numero = Int32.Parse( model.Telefono);
            //    obj.TipoTelefono = model.TipoTelefono;
            //    obj.DescEstado = model.EstadoTelefono;
            //    obj.IdEstado = model.EstadoTelefono;
            //    obj.NombreContacto = model.NombreContacto;
            //    obj.TipoContacto = model.TipoContacto;
            //    obj.Comuna = model.IdComuna;
            //    obj.Direccion = model.Direccion;
            //    obj.Ddcid = model.Ddcid;
            //    obj.EstadoDireccion = model.EstadoDireccion;
            //    if (obj.Ddcid == 0)
            //    {
            //        int id = bcp.Deudor.InsertarContacto(obj);
            //        obj.Ddcid = id;
            //    }
            //    else
            //    {
            //        error = bcp.Deudor.EditarContacto(obj);
            //    }
                
            //    error = bcp.Deudor.InsertarTelefonoContacto(obj);
            //}
            //else if (model.TipoForm == "E")
            //{
            //    dto.Email obj = new dto.Email();
            //    obj.Codemp = objSession.CodigoEmpresa;
            //    obj.Ctcid = Int32.Parse(model.Ctcid);
            //    obj.Mail = model.Email;
            //    obj.TipoEmail = model.TipoEmail;
            //    obj.Masivo = model.EmailMasivo? "S" : "N";
            //    obj.IdEstado = model.EstadoTelefono;
            //    obj.NombreContacto = model.NombreContacto;
            //    obj.TipoContacto = model.TipoContacto;
            //    obj.Comuna = model.IdComuna;
            //    obj.Direccion = model.Direccion;
            //    obj.Ddcid = model.Ddcid;
            //    obj.EstadoDireccion = model.EstadoDireccion;
            //    if (obj.Ddcid == 0)
            //    {
            //        int id = bcp.Deudor.InsertarContacto(obj);
            //        obj.Ddcid = id;
            //    }
            //    else
            //    {
            //        error = bcp.Deudor.EditarContacto(obj);
            //    }
            //    error = bcp.Deudor.InsertarEmailContacto(obj);
            //}
            if (error > 0)
            {
                salida = "Contacto grabado correctamente.";
            }
            else
            {
                salida = "El contacto no fue grabado.";
            }

            return Json(salida);
        }

        public JsonResult EliminarContactoTelefono(string id)
        {
            int salida = 0;
            string[] ids =id.Split('|');
            try
            {
                Deudor.EliminarTelefonoTodos(objSession.CodigoEmpresa,ids[0], ids[1]);
                return Json("Telefono eliminado.");
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }

        public JsonResult EliminarContactoEmail(string id)
        {
            string[] ids = id.Split('|');
            try
            {
                Deudor.EliminarEmailTodos(objSession.CodigoEmpresa.ToString(), ids[0], ids[1]);
                return Json("Email eliminado.");
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }

        public JsonResult EliminarContactoEmailProv(string id)
        {
            string[] ids = id.Split('|');
            try
            {
                Deudor.EliminarEmailProv(objSession.CodigoEmpresa.ToString(), ids[0], ids[1]);
                return Json("Email eliminado.");
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }

        #endregion

        #region "Genera Reportes"

        public JsonResult GeneraReporte(int pclid, int ctcid, string tipo, int rep, int pag)
        {
            bool ruta = false;
            Liquidacion objLiq = new Liquidacion();
            string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];

            switch (rep)
            {
                case 1:
                    //bool ruta = false;
                    objLiq.Codemp = objSession.CodigoEmpresa;
                    objLiq.Pclid = pclid;
                    objLiq.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                    objLiq.TipoCartera = 1;
                    objLiq.EstadoCpbt = tipo;
                    objLiq.Idioma = objSession.Idioma;
                    objLiq.Sucid = objSession.CodigoSucursal;
                    objLiq.FechaReporte = DateTime.Now;
                    objLiq.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa +"\\liquidacion_1_" + objSession.CodigoEmpresa + "_" + objLiq.Ctcid + ".pdf";
                    objLiq.IdReporte = rep;
                    objLiq.Pagina = pag;

                    ruta = Reportes.bcp.Cartera.TraeLiquidacionMasiva(objLiq);
                    System.IO.File.Delete(objLiq.PathArchivo + ".fo");
                    if (ruta)
                    {
                        return Json(Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/liquidacion_1_" + +objSession.CodigoEmpresa + "_" + objLiq.Ctcid + ".pdf");
                    }
                    else
                    {
                        return Json("");
                    }
                    break;
                case 2:
                    LiquidacionDura objLiqDura= new LiquidacionDura();
                    objLiqDura.Codemp = objSession.CodigoEmpresa;
                    objLiqDura.Pclid = pclid;
                    objLiqDura.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                    objLiqDura.TipoCartera = 2;
                    objLiqDura.EstadoCpbt = tipo;
                    objLiqDura.Idioma = objSession.Idioma;
                    objLiqDura.Sucid = objSession.CodigoSucursal;
                    objLiqDura.FechaReporte = DateTime.Now;
                    objLiqDura.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\liquidacion_2_" + objSession.CodigoEmpresa + "_" + objLiqDura.Ctcid + ".pdf";
                    objLiqDura.IdReporte = rep;
                    objLiqDura.Pagina = pag;

                    ruta = Dimol.Reportes.bcp.Cartera.TraeLiquidacionDura(objLiqDura);
                    System.IO.File.Delete(objLiqDura.PathArchivo + ".fo");
                    if (ruta)
                    {
                        return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/liquidacion_2_" + +objSession.CodigoEmpresa + "_" + objLiqDura.Ctcid + ".pdf");
                    }
                    else
                    {
                        return Json("");
                    }
                    break;
                case 3:
                    objLiq.Codemp = objSession.CodigoEmpresa;
                    objLiq.Pclid = pclid;
                    objLiq.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                    objLiq.TipoCartera = 1;
                    objLiq.EstadoCpbt = tipo;
                    objLiq.Idioma = objSession.Idioma;
                    objLiq.Sucid = objSession.CodigoSucursal;
                    objLiq.FechaReporte = DateTime.Now;
                    objLiq.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\liquidacion_3_" + objSession.CodigoEmpresa + "_" + objLiq.Ctcid + ".pdf";
                    objLiq.IdReporte = rep;
                    objLiq.Pagina = pag;

                    ruta = Dimol.Reportes.bcp.Cartera.TraeLiquidacionMutual(objLiq);
                    System.IO.File.Delete(objLiq.PathArchivo + ".fo");
                    if (ruta)
                    {
                        return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/"  +objSession.CodigoEmpresa + "/liquidacion_3_" + +objSession.CodigoEmpresa + "_" + objLiq.Ctcid + ".pdf");
                    }
                    else
                    {
                        return Json("");
                    }
                    break;
                case 4:
                    ResumenGestiones obj = new ResumenGestiones();
                    obj.Codemp = objSession.CodigoEmpresa;
                    obj.Sucid = objSession.CodigoSucursal;
                    obj.Idioma = objSession.Idioma;
                    obj.Pclid = pclid;
                    obj.Ctcid = ctcid;
                    obj.FechaReporte = DateTime.Now;
                    obj.IdReporte = rep;
                    obj.Pagina = pag;
                    obj.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\resumen_gestiones_" + objSession.CodigoEmpresa + "_" + obj.Ctcid + ".pdf";

                    //string ruta = bcp.Cartera.TraeResumenGestiones(obj);


                    ruta = Dimol.Reportes.bcp.Cartera.TraeResumenGestiones(obj);
                    System.IO.File.Delete(objLiq.PathArchivo + ".fo");
                    if (ruta)
                    {
                        return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" +objSession.CodigoEmpresa + "/resumen_gestiones_" + +objSession.CodigoEmpresa + "_" + obj.Ctcid + ".pdf");
                    }
                    else
                    {
                        return Json("");
                    }
                    break;
                case 5:
                    LiquidacionDura objLiqMutualLey = new LiquidacionDura();
                    objLiqMutualLey.Codemp = objSession.CodigoEmpresa;
                    objLiqMutualLey.Pclid = pclid;
                    objLiqMutualLey.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                    objLiqMutualLey.TipoCartera = 1;
                    objLiqMutualLey.EstadoCpbt = tipo;
                    objLiqMutualLey.Idioma = objSession.Idioma;
                    objLiqMutualLey.Sucid = objSession.CodigoSucursal;
                    objLiqMutualLey.FechaReporte = DateTime.Now;
                    objLiqMutualLey.PathArchivo = ubicacion + "Documentos\\" + objSession.CodigoEmpresa + "\\liquidacion_5_" + objSession.CodigoEmpresa + "_" + objLiqMutualLey .Ctcid + ".pdf";
                    objLiqMutualLey.IdReporte = rep;
                    objLiqMutualLey.Pagina = pag;



                    ruta = Dimol.Reportes.bcp.Cartera.TraeLiquidacionMutualLey(objLiqMutualLey);
                    System.IO.File.Delete(objLiqMutualLey.PathArchivo + ".fo");
                    if (ruta)
                    {
                        return Json(Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + "/Documentos/" + objSession.CodigoEmpresa + "/liquidacion_5_" + +objSession.CodigoEmpresa + "_" + objLiqMutualLey.Ctcid + ".pdf");
                    }
                    else
                    {
                        return Json("");
                    }
                    break;
                default:
                    return Json("");
                    break;
            }
        }

        public void GeneraReporteXLS(int pclid, int ctcid, string tipo, int rep, int pag)
        {
            string ruta = "";
            Liquidacion objLiq = new Liquidacion();
            string ubicacion = ConfigurationManager.AppSettings["RutaArchivos"];
            switch (rep)
            {
                case 1:
                case 2:
                case 4:
                    break;
                case 3:
                    objLiq.Codemp = objSession.CodigoEmpresa;
                    objLiq.Pclid = pclid;
                    objLiq.Ctcid = ctcid;// 1203973;// 1202065;//7598;
                    objLiq.TipoCartera = 1;
                    objLiq.EstadoCpbt = tipo;
                    objLiq.Idioma = objSession.Idioma;
                    objLiq.Sucid = objSession.CodigoSucursal;
                    objLiq.FechaReporte = DateTime.Now;
                    objLiq.PathArchivo = ubicacion ;
                    objLiq.IdReporte = rep;
                    objLiq.Pagina = pag;

                    ruta = Dimol.Reportes.bcp.Cartera.TraeLiquidacionCochaMutualXLS(objLiq);
                    string filename = @"liquidacion_3_"+ objSession.CodigoEmpresa + "_" + objLiq.Ctcid ;
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
                    Response.ContentType = "application/vnd.xls";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache); // not necessarily required
                    Response.Charset = "";
                    Response.Output.Write(ruta);
                    Response.End();
                    break;
                //default:
                    //return Json("");

            } 
            //String content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta charset=\"utf-8\" /><title></title></head><body><table style=\"font-family:'Times New Roman', Times, serif;font-size:14px;text-align:left\" cellspacing=\"3\"><tr><td colspan=\"12\" style =\"font-family:'Times New Roman', Times, serif;font-size:18px;color:darkblue;text-align:center;font-weight:bold\">INFORME DE LIQUIDACION</td></tr><tr><td colspan=\"12\" style=\"font-family:'Times New Roman', Times, serif;font-size:18px;color:darkblue;text-align:center;font-weight:bold\">TURISMO COCHA S.A.</td></tr><tr><td colspan=\"12\" style=\"font-family:'Times New Roman', Ties, serif;font-size:18px;color:darkblue;text-align:center;font-weight:bold\">RUT: 81.821.100-7</td></tr><tr><td colspan =\"12\" style=\"font-weight:bold;height:25px\"></td></tr><tr style=\"font-weight:bold\"><td style =\"width:20px\"></td>	<td>RUT: </td><td>98.000.100-8</td><td colspan=\"2\">NOMBRE: </td><td>A.F.P. HABITAT S.A.</td><td></td><td></td><td></td><td></td><td></td></tr><tr><td style=\"width:20px\"></td><td>CIUDAD:</td><td>SANTIAGO</td><td style=\"width:20px\"></td><td>COMUNA:</td><td>PROVIDENCIA</td><td style =\"width:20px\"></td><td>REGION:</td><td colspan=\"2\">REGION  METROPOLITANA</td><td></td><td></td></tr><tr><td style=\"width:20px\"></td><td>DIRECCION:</td>	<td>AV. PROVIDENCIA 1909, PISO 6</td><td></td><td>TELEFONO:</td><td></td><td></td><td>COD. POSTAL:</td><td colspan=\"2\">7500000</td><td></td><td></td></tr><tr><td style=\"width:20px\"></td><td>CELULAR:</td><td></td><td></td><td>E-MAIL:</td><td>irodrigu@afphabitat.cl</td><td></td><td>FAX:</td><td colspan=\"2\"></td><td></td><td></td></tr><tr><td style=\"width:20px\"></td><td></td><td></td><td></td><td style =\"font-weight:bold;\">FECHA EMISION:</td><td style=\"font-weight:bold;\">29/09/2015</td><td></td><td style=\"font-weight:bold;\">GESTOR:</td><td style=\"font-weight:bold;\" colspan=\"2\">KATHERINE CUBILLOS</td><td></td><td></td></tr><tr><td colspan=\"12\" style=\"font-weight:bold;height:25px\"></td></tr><tr><td style=\"width:20px\"></td><td colspan=\"11\" style=\"font-weight:bold;color:darkblue;text-align:center\">DETALLE LIQUIDACION</td></tr><tr><td colspan=\"2\" style=\"font-weight:bold;color:darkblue;text-align:right\">PESOS</td><td></td><td></td><td> </td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr><tr style=\"font-weight:bold;text-align:center\"><td style=\"width:20px\"></td><td></td><td>TIPO</td><td colspan=\"2\">NUMERO</td><td>FECHA EMISION</td><td colspan=\"2\">FECHA VENC.</td><td>DIAS  VENC.</td><td>MONTO</td><td>SALDO</td><td>NEGOCIO</td><td></td></tr><tr><td colspan=\"2\"></td><td>FACTURA</td><td style=\"text-align:right\" colspan=\"2\">6246129</td><td style=\"text-align:center\">14/07/2015</td><td style=\"text-align:center\" colspan=\"2\">13/08/2015</td><td style=\"color:red;text-align:right\">47</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">5-C05-382</td><td></td></tr><tr><td colspan=\"2\"></td><td>FACTURA</td><td style=\"text-align:right\" colspan=\"2\">6246129</td><td style=\"text-align:center\">14/07/2015</td><td style=\"text-align:center\" colspan=\"2\">13/08/2015</td><td style=\"color:red;text-align:right\">47</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">5-C05-382</td><td></td></tr><tr><td colspan=\"2\"></td><td>FACTURA</td><td style=\"text-align:right\" colspan=\"2\">6246129</td><td style=\"text-align:center\">14/07/2015</td><td style=\"text-align:center\" colspan=\"2\">13/08/2015</td><td style=\"color:red;text-align:right\">47</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">5-C05-382</td><td></td></tr><tr><td colspan=\"2\"></td><td>FACTURA</td><td style=\"text-align:right\" colspan=\"2\">6246129</td><td style=\"text-align:center\">14/07/2015</td><td style=\"text-align:center\" colspan=\"2\">13/08/2015</td><td style=\"color:red;text-align:right\">47</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">5-C05-382</td><td></td></tr><tr><td colspan=\"2\"></td><td>FACTURA</td><td style=\"text-align:right\" colspan=\"2\">6246129</td><td style=\"text-align:center\">14/07/2015</td><td style=\"text-align:center\" colspan=\"2\">13/08/2015</td><td style=\"color:red;text-align:right\">47</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">5-C05-382</td><td></td></tr><tr><td colspan=\"2\"></td><td>FACTURA</td><td style=\"text-align:right\" colspan=\"2\">6246129</td><td style=\"text-align:center\">14/07/2015</td><td style=\"text-align:center\" colspan=\"2\">13/08/2015</td><td style=\"color:red;text-align:right\">47</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">252,087.00</td><td style=\"text-align:right\">5-C05-382</td><td></td></tr><tr style=\"font-weight:bold;color:darkblue\"><td colspan=\"2\"></td><td></td><td></td><td></td><td colspan=\"3\">SUB-TOTAL PESOS</td><td></td><td style=\"text-align:right\">36,473,448.62</td><td style=\"text-align:right\">36,473,448.62</td><td></td><td></td></tr><tr style=\"font-weight:bold;color:darkblue\"><td colspan=\"2\"></td><td></td><td></td><td></td><td colspan=\"3\">CANTIDAD DOCUMENTOS</td><td></td><td style=\"text-align:right\">54</td><td></td><td></td><td></td></tr></table></body></html>";
            //string filename = @"d:\test1";
            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            //Response.ContentType = "application/vnd.xls";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache); // not necessarily required
            //Response.Charset = "";
            //Response.Output.Write(content);
            //Response.End();

        
        }

        #endregion

        #region "Carga Itau"
        public ActionResult CargaItau()
        {
            //Validación de sesión
            if (!SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            Comprobante objComprobante = new Comprobante();
            ViewBag.Contrato = new SelectList(objComprobante.ListarContrato(objSession.CodigoEmpresa, 0, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 0, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "");

            return View();
        }

        public JsonResult ProcesoCargaItau(CargaItauModel model, GridSettings gridSettings)
        {
            bool error = false;
            string[] rut = model.RutCliente.Split('-');
            dto.SitrelCarga objCarga = new dto.SitrelCarga();
            objCarga.Codemp= objSession.CodigoEmpresa;
            objCarga.CodigoCarga = model.CodigoCarga;
            objCarga.Error = "";
            objCarga.Estado = "LA";
            objCarga.FechaCarga = DateTime.Now;
            objCarga.Pclid = model.Pclid;
            objCarga.IdCarga = 0;
            objCarga.TipoCartera  = model.TipoCartera;
            objCarga.Contrato = model.Contrato;
            objCarga.Archivos.Add(new dto.SitrelArchivo {
                CodigoArchivo = 1,//deudor
                NombreArchivo = model.ArchivoDeudor,
                Error = "",
                Estado = "LA"         
            });
            objCarga.Archivos.Add(new dto.SitrelArchivo
            {
                CodigoArchivo = 2,//operacion
                NombreArchivo = model.ArchivoOperacion,
                Error = "",
                Estado = "LA"
            });
            objCarga.Archivos.Add(new dto.SitrelArchivo {
                CodigoArchivo = 3,//cuota
                NombreArchivo = model.ArchivoCuota,
                Error = "",
                Estado = "LA"           
              });
            objCarga.Archivos.Add(new dto.SitrelArchivo {
                CodigoArchivo = 4,//telefono
                NombreArchivo = model.ArchivoTelefono,
                Error = "",
                Estado = "LA"          
              });
            objCarga.Archivos.Add(new dto.SitrelArchivo {
                CodigoArchivo = 5,//direccion 
                NombreArchivo = model.ArchivoDireccion,
                Error = "",
                Estado = "LA"            
               });
            objCarga.Archivos.Add(new dto.SitrelArchivo {
                CodigoArchivo = 6,//email
                NombreArchivo = model.ArchivoEmail,
                Error = "",
                Estado = "LA"        
              });
            objCarga.Archivos.Add(new dto.SitrelArchivo {
                CodigoArchivo = 7,//pago
                NombreArchivo = model.ArchivoPago,
                Error = "",
                Estado = "LA"
            });

            bcp.CargaItau.ProcesoCargaItau(objCarga, objSession);



            //if (model.Archivo != "")
            //{

            //    if (model.CargaJudicial && !model.ArchivoQuiebra)
            //    {
            //        List<dto.CargaJudicial> lst = bcp.CargaMasiva.CargarDatosJudicial(model.Archivo);
            //        bcp.CargaMasiva.ProcesoCargaJudicial(lst, objCarga, objSession);
            //    }
            //    if (model.ArchivoQuiebra && !model.CargaJudicial)
            //    {
            //        List<dto.DatosCarga> lst = bcp.CargaMasiva.CargarDatos(model.Archivo);
            //        //bcp.CargaMasiva.ProcesoCargaQuiebra(lst, objCarga, objSession);
            //    }
            //    if (!model.ArchivoQuiebra && !model.CargaJudicial)
            //    {
            //        List<dto.DatosCarga> lst;
            //        if (objCarga.Pclid == 424)
            //        {
            //            lst = bcp.CargaMasiva.CargarDatosOriencoop(model.Archivo, objSession.CodigoEmpresa, objCarga.Pclid, Int32.Parse(objCarga.CodigoCarga));
            //        }
            //        else
            //        {
            //            lst = bcp.CargaMasiva.CargarDatos(model.Archivo);
            //        }
            //        error = bcp.CargaMasiva.ProcesoCarga(lst, objCarga, objSession);
            //    }

            //}

            int totalPages = (int)Math.Ceiling((float)objCarga.ListaErrores.Count / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = objCarga.ListaErrores.Count,
                rows =
                (
                    from dto.ErrorCarga item in objCarga.ListaErrores
                    select new
                    {
                        id = 1,
                        cell = new object[]
                        {
                            item.Rut+"-"+item.Dv,
                            item.Nombre,
                            item.Numero,
                            item.TipoDocumento,
                            item.TipoError
                        }
                    }
                ).ToArray()
            };
            return Json(objCarga.ListaErrores, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SPCargaItau(CargaItauModel model, GridSettings gridSettings)
        {
            bool error = false;
            string[] rut = model.RutCliente.Split('-');
            dto.SitrelCarga objCarga = new dto.SitrelCarga();
            objCarga.Codemp = objSession.CodigoEmpresa;
            objCarga.CodigoCarga = model.CodigoCarga;
            objCarga.Error = "";
            objCarga.Estado = "LA";
            objCarga.FechaCarga = DateTime.Now;
            objCarga.Pclid = model.Pclid;
            objCarga.IdCarga = 0;
            objCarga.TipoCartera = model.TipoCartera;
            objCarga.Contrato = model.Contrato;
            objCarga.Archivos.Add(new dto.SitrelArchivo
            {
                CodigoArchivo = 1,//deudor
                NombreArchivo = model.ArchivoDeudor,
                Error = "",
                Estado = "LA"
            });
            objCarga.Archivos.Add(new dto.SitrelArchivo
            {
                CodigoArchivo = 2,//operacion
                NombreArchivo = model.ArchivoOperacion,
                Error = "",
                Estado = "LA"
            });
            objCarga.Archivos.Add(new dto.SitrelArchivo
            {
                CodigoArchivo = 3,//cuota
                NombreArchivo = model.ArchivoCuota,
                Error = "",
                Estado = "LA"
            });
            objCarga.Archivos.Add(new dto.SitrelArchivo
            {
                CodigoArchivo = 4,//telefono
                NombreArchivo = model.ArchivoTelefono,
                Error = "",
                Estado = "LA"
            });
            objCarga.Archivos.Add(new dto.SitrelArchivo
            {
                CodigoArchivo = 5,//direccion 
                NombreArchivo = model.ArchivoDireccion,
                Error = "",
                Estado = "LA"
            });
            objCarga.Archivos.Add(new dto.SitrelArchivo
            {
                CodigoArchivo = 6,//email
                NombreArchivo = model.ArchivoEmail,
                Error = "",
                Estado = "LA"
            });
            objCarga.Archivos.Add(new dto.SitrelArchivo
            {
                CodigoArchivo = 7,//pago
                NombreArchivo = model.ArchivoPago,
                Error = "",
                Estado = "LA"
            });

            bcp.CargaItau.CargarDocumentoSitrel(objCarga, objSession);

            int totalPages = (int)Math.Ceiling((float)objCarga.ListaErrores.Count / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = objCarga.ListaErrores.Count,
                rows =
                (
                    from dto.ErrorCarga item in objCarga.ListaErrores
                    select new
                    {
                        id = 1,
                        cell = new object[]
                        {
                            item.Rut+"-"+item.Dv,
                            item.Nombre,
                            item.Numero,
                            item.TipoDocumento,
                            item.TipoError
                        }
                    }
                ).ToArray()
            };
            return Json(objCarga.ListaErrores, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarAccionesST(int pclid)
        {
            return Json(new SelectList(bcp.CargaItau.ListarAcciones(objSession.CodigoEmpresa, pclid, "Seleccione"), "Value", "Text"));
        }

        public JsonResult ListarContactosST( int pclid, string accion)
        {
            return Json(new SelectList(bcp.CargaItau.ListarContactos(objSession.CodigoEmpresa, pclid, accion, "Seleccione"), "Value", "Text"));
        }

        public JsonResult ListarRespuestasST(int pclid, string accion, string contacto)
        {
            return Json(new SelectList(bcp.CargaItau.ListarRespuestas(objSession.CodigoEmpresa, pclid, accion, contacto, "Seleccione"), "Value", "Text"));
        }

        public JsonResult ListarTipoDireccionST(int pclid)
        {
            return Json(new SelectList(bcp.CargaItau.ListarTipoDireccion(objSession.CodigoEmpresa, pclid, null), "Value", "Text"));
        }

        public ActionResult GuardarGestionesSitrel(AgregarGestionSitrelModel model)
        {
            Funciones objFunc = new Funciones();
            dto.DetalleEstados objDetalleEstado = bcp.Comprobante.TraeDetalleEstado(objSession.CodigoEmpresa, model.TipoEstado);
            DateTime fechaAccion = new DateTime();
            int error = 0;
            var lstFinal = new List<dto.Comprobante>();
            int tipogestion = 1;
            string contacto = "S";
            decimal totalCompromiso = 0;

            model.TipoEstado = Int32.Parse( model.RespuestaSitrel.Split('|')[0]);
            model.RespuestaSitrel = model.RespuestaSitrel.Split('|')[1];
            model.CambiaEstado = true;
            model.EstadosXDocumentos = true;

            if (model.AccionSitrel.Contains("EMAIL"))
            {
                tipogestion = 4;
            }
            else if (model.AccionSitrel.Contains("LLAMA"))
            {
                tipogestion = 1;
            }
            else if (model.AccionSitrel.Contains("VISIT"))
            {
                tipogestion = 3;
            }

            if (model.ContactoSitrel.Contains("DIRECTO"))
            {
                contacto = "N";
            }


            if (tipogestion == 0)
            {
                return Json(objFunc.TraeEtiqueta("Accion", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            if (string.IsNullOrEmpty(model.ComentarioSitrel))
            {
                return Json(objFunc.TraeEtiqueta("AgrAc5", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            if (model.CambiaEstado && model.TipoEstado == 0)
            {
                return Json(objFunc.TraeEtiqueta("Estado", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            if (model.CambiaEstado && model.TipoEstado == 0 && model.EstadosXDocumentos)
            {
                return Json(objFunc.TraeEtiqueta("Docu", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            if ((model.CambiaEstado && model.TipoEstado > 0 && objDetalleEstado.Compromiso == "S") && string.IsNullOrEmpty(model.FechaHistorial))
            {
                return Json(objFunc.TraeEtiqueta("Fecha", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma));
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<dto.ComprobanteCorto> lstGrilla = serializer.Deserialize<List<dto.ComprobanteCorto>>(model.Documentos);

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    //Grabo la accion
                    error = bcp.Comprobante.InsertarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), tipogestion, objSession.CodigoSucursal, objSession.Gestor, contacto, objSession.IpRed, objSession.IpPc, objSession.UserId, model.ComentarioSitrel, 0, string.IsNullOrEmpty(model.TelefonoHistorial) ? 0 : Int64.Parse(model.TelefonoHistorial.Trim()));
                    //error = 1;

                    if (model.MostrarTelefono && error > 0)
                    {
                        string estado = "0";
                        switch (model.ResultadoLlamado)
                        {
                            case 1:
                            case 2:
                            case 3:
                                estado = "A";
                                break;
                            case 5:
                                estado = "M";
                                break;
                        }
                        //Actualizo estado del telefono
                        //error = bcp.Deudor.EditarTelefonoPrioridad(objSession.CodigoEmpresa, model.Ctcid, Int32.Parse(model.TelefonoHistorial), estado, model.ResultadoLlamado);
                        // error = 1;
                    }

                    if (model.CambiaEstado && error > 0)
                    {
                        //Tomo la fecha de la gestion
                        fechaAccion = bcp.Accion.BuscarUltimaFechaAcciones(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), tipogestion);
                        //Genero la lista de documentoa a modificar
                        List<dto.Comprobante> lst = bcp.Comprobante.ListarDocumentosHistorial(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), model.EstadoCpbt, "", "TipoCpbtNombre", "asc", 0, 10000);
                        string[] allowedStatus = { };
                        if (!string.IsNullOrEmpty(model.Ids))
                        {
                            serializer = new JavaScriptSerializer();
                            allowedStatus = serializer.Deserialize<string[]>(model.Ids);
                        }
                        if (model.EstadosXDocumentos && allowedStatus.Length != 0)
                        {
                            var lstFiltrada = from cpbt in lst
                                              where allowedStatus.Contains(cpbt.Ccbid.ToString())
                                              select cpbt;
                            lstFinal = lstFiltrada.ToList();
                        }
                        else
                        {
                            lstFinal = lst;
                        }

                        if (lstFinal.Count > 0)
                        {
                            model.MontoGestionSitrel = 0;
                            //Actualizo fecha y monto de comprommiso
                            if (model.RespuestaSitrel.Contains("COMPAG") && !string.IsNullOrEmpty(model.FechaCompromisoSitrel))
                            {
                                foreach (dto.Comprobante cpbt in lstFinal)
                                {
                                    //Actualizo fecha y monto de comprommiso
                                    decimal compromiso = (lstGrilla.Find(x => x.id == cpbt.Ccbid)).m;
                                    if (compromiso == 0)
                                    {
                                        compromiso = cpbt.Monto;
                                    }
                                    totalCompromiso += compromiso;
                                    model.MontoGestionSitrel += compromiso;
                                    error = bcp.Comprobante.ActualizarCarteraClientesCompromiso(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, compromiso, DateTime.Parse(model.FechaCompromisoSitrel));
                                    //Actualizo estado de los documentos
                                    if (error > 0)
                                    {
                                        error = bcp.Comprobante.ActualizaCarteraEstados(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, model.TipoEstado, model.Tipo);
                                    }
                                    //Actualizo historial de los documentos
                                    if (error > 0)
                                    {
                                        error = bcp.Comprobante.InsertarCarteraClientesEstadosHistorialEspecial(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, fechaAccion, model.TipoEstado, objSession.CodigoSucursal, objSession.Gestor, objSession.IpRed, objSession.IpPc, "", cpbt.Monto, cpbt.Saldo, objSession.UserId);
                                    }
                                }
                            }
                            else
                            {
                                // actualizo estados que no sean compromiso
                                foreach (dto.Comprobante cpbt in lstFinal)
                                {
                                    model.MontoGestionSitrel += cpbt.Monto;
                                    //Actualizo fecha y monto de comprommiso
                                    error = bcp.Comprobante.ActualizarCarteraClientesCompromiso(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, 0, null);
                                    //Actualizo estado de los documentos
                                    if (error > 0)
                                    {
                                        error = bcp.Comprobante.ActualizaCarteraEstados(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, model.TipoEstado, model.Tipo);
                                    }
                                    //Actualizo historial de los documentos
                                    if (error > 0)
                                    {
                                        error = bcp.Comprobante.InsertarCarteraClientesEstadosHistorialEspecial(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), cpbt.Ccbid, fechaAccion, model.TipoEstado, objSession.CodigoSucursal, objSession.Gestor, objSession.IpRed, objSession.IpPc, "", cpbt.Monto, cpbt.Saldo, objSession.UserId);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Actualizo todos los documentos
                            error = bcp.Comprobante.ActualizarEstadoCarteraClientesTodos(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), model.TipoEstado, model.EstadoCpbt);
                        }
                    }
                    else
                    {
                        //Actualizo fecha ultima gestion
                        //error = bcp.Comprobante.ActualizarCarteraClientesUltimaGestiont(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), model.EstadoCpbt);
                        error = bcp.Comprobante.ActualizarEstadoCarteraClientesTodos(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), model.TipoEstado, model.EstadoCpbt);
                    }

                    string programacion = "";
                    string strFechacompromiso = "";
                    if (!string.IsNullOrEmpty(model.FechaProgramadaSitrel))
                    {
                        DateTime fechaProgramacion = DateTime.Parse(model.FechaProgramadaSitrel);
                        programacion = fechaProgramacion.Year.ToString() + fechaProgramacion.Month.ToString().PadLeft(2, '0') + fechaProgramacion.Day.ToString().PadLeft(2, '0') + " " + model.HoraProgramadaSitrel.ToString().PadLeft(2, '0') + model.MinutoProgramadoSitrel.ToString().PadLeft(2, '0') + "00";

                    }

                    if (!string.IsNullOrEmpty(model.FechaCompromisoSitrel))
                    {
                        DateTime fechaCompromiso = DateTime.Parse(model.FechaCompromisoSitrel);
                        strFechacompromiso = fechaCompromiso.Year.ToString() + fechaCompromiso.Month.ToString().PadLeft(2, '0') + fechaCompromiso.Day.ToString().PadLeft(2, '0');

                    }

                    error = bcp.CargaItau.InsertarGestion(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), fechaAccion, tipogestion, "", model.CodigoEmpresa, model.AccionSitrel, model.ContactoSitrel, model.RespuestaSitrel, model.ComentarioSitrel, strFechacompromiso,
                        totalCompromiso, model.MontoGestionSitrel, model.NombreContactoSitrel, programacion, model.TelefonoContactoSitrel);
                }
                catch (Exception ex)
                {
                    Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "GuardarGestionesSitrel", 0);
                }
                if (error > 0)
                {
                    scope.Complete();
                }
            }

            return Json(error);
        }

        public JsonResult GetDireccion(GridSettings gridSettings, int ctcid)
        {
            int totalRecords = bcp.CargaItau.ListarDireccionCount(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<dto.Direccion> lst = bcp.CargaItau.ListarDireccion(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Direccion item in lst
                    select new
                    {
                        id = item.Ctcid + "|" + item.Calle ,
                        cell = new object[]
                        {
                            item.Ctcid,
                            item.TipoDireccion,
                            item.Calle.Replace("#","Nº").Replace(","," - "),
                            item.Comuna,
                            "Visita",
                            item.Region,
                            item.Ciudad,
                            item.IdComuna
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Carga Suseso
        public ActionResult CargaSuseso()
        {
            //Validación de sesión
            if (!SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public JsonResult ProcesoCargaSuseso(CargaSusesoModel ObjCargaSuseso, GridSettings gridSettings)
        {
            dto.CargaSuseso ObjCargaSusesoDto = new dto.CargaSuseso();

            //Ruta del directorio
            Funciones objFunc = new Funciones();
            //ObjCargaSusesoDto.RutaDirectorio = "C:\\Archivos\\Download\\Suseso\\";
            ObjCargaSusesoDto.RutaDirectorio = ConfigurationManager.AppSettings["RutaArchivos"] + objFunc.Configuracion_Str(15) + "\\Suseso\\";

            //Lista de archivos
            if (ObjCargaSuseso.ArchivoT1 != "" && ObjCargaSuseso.ArchivoT1 != null) { ObjCargaSusesoDto.ListaArchivos.Add(ObjCargaSuseso.ArchivoT1); }
            if (ObjCargaSuseso.ArchivoT2 != "" && ObjCargaSuseso.ArchivoT2 != null) { ObjCargaSusesoDto.ListaArchivos.Add(ObjCargaSuseso.ArchivoT2); }
            if (ObjCargaSuseso.ArchivoT3 != "" && ObjCargaSuseso.ArchivoT3 != null) { ObjCargaSusesoDto.ListaArchivos.Add(ObjCargaSuseso.ArchivoT3); }
            if (ObjCargaSuseso.ArchivoT4 != "" && ObjCargaSuseso.ArchivoT4 != null) { ObjCargaSusesoDto.ListaArchivos.Add(ObjCargaSuseso.ArchivoT4); }
            if (ObjCargaSuseso.ArchivoT5 != "" && ObjCargaSuseso.ArchivoT5 != null) { ObjCargaSusesoDto.ListaArchivos.Add(ObjCargaSuseso.ArchivoT5); }
            if (ObjCargaSuseso.ArchivoT6 != "" && ObjCargaSuseso.ArchivoT6 != null) { ObjCargaSusesoDto.ListaArchivos.Add(ObjCargaSuseso.ArchivoT6); }
            if (ObjCargaSuseso.ArchivoT7 != "" && ObjCargaSuseso.ArchivoT7 != null) { ObjCargaSusesoDto.ListaArchivos.Add(ObjCargaSuseso.ArchivoT7); }
            if (ObjCargaSuseso.ArchivoT8 != "" && ObjCargaSuseso.ArchivoT8 != null) { ObjCargaSusesoDto.ListaArchivos.Add(ObjCargaSuseso.ArchivoT8); }

            bcp.CargaSuseso.ProcesoCargaSuseso(ObjCargaSusesoDto, objSession);

            //return Json(objCarga.ListaErrores, JsonRequestBehavior.AllowGet);
            return Json(ObjCargaSusesoDto, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Archivos Salida Itau"

        public ActionResult SalidaItau()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Comprobante objComprobante = new Comprobante();
            ViewBag.FechaDesde = DateTime.Today.ToShortDateString();
            ViewBag.FechaHasta = DateTime.Today.ToShortDateString();
            return View();
        }

        public void ArchivosSalidaItau(SalidaItauModel model)//int codemp, int pclid, DateTime desde, DateTime hasta, int archivo)
        {
            string ruta = bcp.CargaItau.NombreArchivoSitrel(objSession.CodigoEmpresa,model.TipoArchivo,"S");
            ruta = ruta.Replace("AAAAMMDD", DateTime.Today.ToString("yyyyMMdd"));
            List<string> lst = new List<string>();
            DateTime desde = new DateTime(2015, 10, 1);
            DateTime hasta = DateTime.Today.AddDays(1);
            if (!string.IsNullOrEmpty(model.FechaDesde))
            {
                desde = DateTime.Parse(model.FechaDesde);
            }
            if (!string.IsNullOrEmpty(model.FechaHasta))
            {
                hasta = DateTime.Parse(model.FechaHasta).AddDays(1);
            }
            StringBuilder sb = new StringBuilder();
            string text = "";
            string s = "";

            switch (model.TipoArchivo)
            {
                case 8:
                    lst = bcp.CargaItau.ListaSalidaGestiones(objSession.CodigoEmpresa, model.Pclid, desde, hasta);
                    foreach(string linea in lst){
                        s = linea.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                        sb.Append(s);
                        sb.Append("\r\n");
                    }
                    break;
                case 9:
                    lst = bcp.CargaItau.ListaSalidaDirecciones(objSession.CodigoEmpresa, model.Pclid, desde, hasta);
                    foreach (string linea in lst)
                    {
                        s = linea.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                        sb.Append(s);
                        sb.Append("\r\n");
                    }
                    break;
                case 10:
                    lst = bcp.CargaItau.ListaSalidaTelefonos(objSession.CodigoEmpresa, model.Pclid, desde, hasta);
                    foreach (string linea in lst)
                    {
                        s = linea.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                        sb.Append(s);
                        sb.Append("\r\n");
                    }
                    break;
                case 11:
                    lst = bcp.CargaItau.ListaSalidaEmail(objSession.CodigoEmpresa, model.Pclid, desde, hasta);
                    foreach(string linea in lst){
                        s = linea.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                        sb.Append(s);
                        sb.Append("\r\n");
                    }
                    break;

            }
            text = sb.ToString();
            Response.Clear();
            Response.ClearHeaders();

            //Response.AddHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            Response.AppendHeader("content-disposition", "attachment;filename=\"" + ruta + "\"");

            Response.Write(text);
            Response.End();
        }

        public void ListaSalidaDirecciones(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
             //dao.CargaItau.ListaSalidaDirecciones(codemp, pclid, desde, hasta);
        }

        public void ListaSalidaTelefonos(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
            //dao.CargaItau.ListaSalidaTelefonos(codemp, pclid, desde, hasta);
        }

        public void ListaSalidaEmail(int codemp, int pclid, DateTime desde, DateTime hasta)
        {
             //dao.CargaItau.ListaSalidaEmail(codemp, pclid, desde, hasta);
        }

        #endregion

        #region "Recordatorios"

        public ActionResult GuardarRecordatorio(AgregarRecordatorioModel model)
        {
            string error = "";


            dto.Recordatorio obj = new dto.Recordatorio();
            obj.Codemp = objSession.CodigoEmpresa;
            obj.Ctcid = Int32.Parse(model.Ctcid);
            obj.Telefono = model.TelefonoRecordatorio;
            obj.Email = model.EmailRecordatorio;
            obj.FechaEnvio = model.FechaEnvioRecordatorio;
            obj.Usrid = objSession.UserId;
            obj.Estado = "V";
            obj.Tipo = model.TipoForm;

            bcp.Recordatorio.GrabarRecordatorio(obj);

            return Json(error);
        }

        public JsonResult GetSMSPreDeudor(GridSettings gridSettings, int ctcid)
        {
            // create json data 
            int totalRecords = bcp.Recordatorio.ListarSMSPreDeudorCount(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Recordatorio> lst = bcp.Recordatorio.ListarSMSPreDeudor(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Recordatorio item in lst
                    select new
                    {
                        id = item.Codemp + "|" + item.Ctcid + "|" + item.Telefono,
                        cell = new object[]
                        {
                            item.Telefono,
                            item.FechaEnvio,
                            item.Estado,
                            item.FechaModificacion,
                            item.UsuarioModificacion
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmailPreDeudor(GridSettings gridSettings, int ctcid)
        {
            // create json data 
            int totalRecords = bcp.Recordatorio.ListarEmailPreDeudorCount(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Recordatorio> lst = bcp.Recordatorio.ListarEmailPreDeudor(objSession.CodigoEmpresa, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Recordatorio item in lst
                    select new
                    {
                        id = item.Codemp + "|" + item.Ctcid + "|" + item.Email,
                        cell = new object[]
                        {
                            item.Email,
                            item.FechaEnvio,
                            item.Estado,
                            item.FechaModificacion,
                            item.UsuarioModificacion
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarSMSPre(string id)
        {
            int salida = 0;
            string[] ids = id.Split('|');
            try
            {
                dto.Recordatorio obj = new dto.Recordatorio();
                obj.Codemp = objSession.CodigoEmpresa;
                obj.Ctcid = Int32.Parse(ids[0]);
                obj.Telefono = ids[1];
                obj.Email = null;
                obj.Usrid = objSession.UserId;
                obj.Estado = "V";
                obj.Tipo = "SMS";

                bcp.Recordatorio.EliminarRecordatorio(obj);
                return Json("Telefono eliminado.");
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }

        public JsonResult EliminarEmailPre(string id)
        {
            int salida = 0;
            string[] ids = id.Split('|');
            try
            {
                dto.Recordatorio obj = new dto.Recordatorio();
                obj.Codemp = objSession.CodigoEmpresa;
                obj.Ctcid = Int32.Parse(ids[0]);
                obj.Telefono = null;
                obj.Email = ids[1];
                obj.Usrid = objSession.UserId;
                obj.Estado = "V";
                obj.Tipo = "E";

                bcp.Recordatorio.EliminarRecordatorio(obj);
                return Json("Email eliminado.");
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }

        #endregion

        public JsonResult EditDummy()
        {
            return Json("Ok");
        }

        public ActionResult GrabarCategoria(int ctcid, string categoria)
        {
            if (objSession.PrfId != 4 && objSession.PrfId != 9 && objSession.PrfId != 6)
            {
                if(objSession.UserId == 378)
                {
                    bcp.Deudor.InsertarCategoriaDeudor(objSession.CodigoEmpresa, ctcid, categoria, objSession.UserId);
                }
            }
            else
            {
                bcp.Deudor.InsertarCategoriaDeudor(objSession.CodigoEmpresa, ctcid, categoria, objSession.UserId);
            }
            
            return Json("", JsonRequestBehavior.AllowGet);
        }

        #region "Reversa Judicial"

        public ActionResult GetMoverCartera(GridSettings gridSettings, MoverCarteraModel model)
        {
            int pclid = 0, ctcid = 0;
            Int32.TryParse(model.Pclid, out pclid);
            Int32.TryParse(model.Ctcid, out ctcid);
            int totalRecords = bcp.Deudor.ListarDocumentosMoverGrillaCount(objSession.CodigoEmpresa, pclid, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoMover> lstReversa = bcp.Deudor.ListarDocumentosMoverGrilla(objSession.CodigoEmpresa, pclid, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoMover item in lstReversa
                    select new
                    {

                        id = item.Ccbid,
                        cell = new object[]
                        {
                           item.Tipo,
                           item.Numero,
                           item.Monto ,
                           item.Saldo,
                           item.UltimoEstado,
                           item.Estado,
                           item.FechaVencimiento
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GrabarMoverDocumentos(MoverCarteraModel model)
        {
            //string salida = Dimol.Carteras.bcp.CargaMasiva.AprobarCarga(ids,objSession);
            string[] splitted;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<int> lstIds = serializer.Deserialize<List<int>>(model.Ids); 
            bool error = false;
            using (TransactionScope scope = new TransactionScope())
            {
                error = bcp.Deudor.MoverCartera(objSession.CodigoEmpresa,Int32.Parse( model.Pclid),Int32.Parse( model.Ctcid),lstIds ,model.Comentario,model.Estado, Int32.Parse( model.EstadoDocumento));
                if (!error)
                {
                    scope.Complete();
                }
            }

            return Json(error?"Error al mover los documentos.":"");
        }

        public ActionResult BuscarEstadoCartera(string term)
        {
            return Json(bcp.Deudor.ListarAutoEstadoCartera(term), JsonRequestBehavior.AllowGet);
        }

        #endregion 

        #region "Cartigos y devoluciones"
        public ActionResult GetCastigoDevolucion(GridSettings gridSettings, CastigoDevolucionModel model)
        {
            int pclid = model.Pclid , ctcid = model.Ctcid;

            int totalRecords = bcp.Deudor.ListarDocumentosCastDevGrillaCount(objSession.CodigoEmpresa, pclid, ctcid, model.Estado, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoCastDev> lstReversa = bcp.Deudor.ListarDocumentosCastDevGrilla(objSession.CodigoEmpresa, pclid, ctcid, model.Estado, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoCastDev item in lstReversa
                    select new
                    {

                        id = item.Ccbid + "|" + item.Monto + "|" + item.Saldo + "|" + item.Asignado + "|" + item.EstadoCpbt,
                        cell = new object[]
                        {
                           item.Tipo,
                           item.Moneda,
                           item.Numero,
                           item.FechaAsignacion,
                           item.Monto ,
                           item.Saldo,
                           item.Estado,
                           item.EstadoCpbt
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProcesarCastigoDevolucion(CastigoDevolucionModel model)
        {
            dto.CabeceraComprobante obj = new dto.CabeceraComprobante();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<string> lstIds = serializer.Deserialize<List<string>>(model.Ids);
            //Motivos
            List<string> lstMotivo = new List<string>();
            if (!string.IsNullOrEmpty(model.IdMotivos))
            {
                lstMotivo = serializer.Deserialize<List<string>>(model.IdMotivos);
            }
           
            obj.Aplica = model.Aplica;
            obj.CabeceraId = model.CabeceraId;
            obj.Cambiodoc = model.Cambiodoc;
            obj.Cancela = model.Cancela;
            obj.Cartcli = model.Cartcli;
            obj.Contable = model.Contable;
            obj.Costos = model.Costos;
            obj.Cptoctbl = model.Cptoctbl;
            obj.Ctcid = model.Ctcid;
            obj.Ccbid = model.Ccbid;
            obj.Estado = model.Estado;
            obj.FechaContabilizacion = model.FechaContabilizacion;
            obj.FechaDocumento = model.FechaDocumento;
            obj.FechaEntrega = model.FechaEntrega;
            obj.FechaIngreso = model.FechaIngreso;
            obj.FechaOrdenCompra = model.FechaOrdenCompra;
            obj.FechaVencimiento = model.FechaVencimiento;
            obj.Findeuda = model.Findeuda;
            obj.FormaPago = model.FormaPago;
            obj.Forpag = model.Forpag;
            obj.GastoJudicial = model.Cartera == "J" ? "S" : "N";
            obj.Glosa = model.Glosa;
            obj.Libcompra = model.Libcompra;
            obj.Moneda = model.Moneda;
            obj.MotivoCobranza = model.MotivoCobranza;
            obj.NombreCliente = model.NombreCliente;
            obj.Numero = model.Numero;
            obj.NumeroOC = model.NumeroOC;
            obj.Ordcomp = model.Ordcomp;
            obj.Pclid = model.Pclid;
            obj.Remesa = model.Remesa;
            obj.RutCliente = model.RutCliente;
            obj.Selapl = model.Selapl;
            obj.Selcpbt = model.Selcpbt;
            obj.Sucursal = model.Sucursal == "null" ? null : model.Sucursal;
            obj.Tipcpbtdoc = model.Tipcpbtdoc;
            obj.Tipdig = model.Tipdig;
            obj.TipoCambio = model.TipoCambio;
            obj.TipoComprobante = model.TipoComprobante;
            obj.TipoComprobanteDesc = model.TipoComprobanteDesc;
            obj.TipoGasto = model.TipoGasto;
            obj.Tipprod = model.Tipprod;
            obj.Clbid = model.Clbid;
            obj.Sinimp = model.Sinimp;

            obj.Codemp = objSession.CodigoEmpresa;
            obj.Codsuc = objSession.CodigoSucursal;
            obj.Tipo = model.Tipo;
            obj.PJ = model.PJ;
            obj.Pag = model.Pag;
            obj.Descuento = decimal.Parse(model.Descuento ?? "0");
            obj.Glosa = model.Glosa;
            obj.Rolid = model.Rolid;
            obj.Glosa = model.Glosa;

            var tuple = bcp.Comprobante.GrabarCastigoDevolucion(obj, lstIds, objSession, lstMotivo);

            return Json(new { success = tuple.Item1, mensaje = tuple.Item2}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GrabarCastigoDevolucion(CastigoDevolucionModel model)
        {
            //string salida = Dimol.Carteras.bcp.CargaMasiva.AprobarCarga(ids,objSession);
            string[] splitted;
            dto.CabeceraComprobante obj = new dto.CabeceraComprobante();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<string> lstIds = serializer.Deserialize<List<string>>(model.Ids);
            string[] salida;
            int ccbid=0;
            decimal monto=0,saldo=0,asignado=0;
            bool error = false;
            using (TransactionScope scope = new TransactionScope())
            {
                obj.Aplica = model.Aplica;
                obj.CabeceraId = model.CabeceraId;
                obj.Cambiodoc = model.Cambiodoc;
                obj.Cancela = model.Cancela;
                obj.Cartcli = model.Cartcli;
                obj.Contable = model.Contable;
                obj.Costos = model.Costos;
                obj.Cptoctbl = model.Cptoctbl;
                obj.Ctcid = model.Ctcid;
                obj.Ccbid = model.Ccbid;
                obj.Estado = model.Estado;
                obj.FechaContabilizacion = model.FechaContabilizacion;
                obj.FechaDocumento = model.FechaDocumento;
                obj.FechaEntrega = model.FechaEntrega;
                obj.FechaIngreso = model.FechaIngreso;
                obj.FechaOrdenCompra = model.FechaOrdenCompra;
                obj.FechaVencimiento = model.FechaVencimiento;
                obj.Findeuda = model.Findeuda;
                obj.FormaPago = model.FormaPago;
                obj.Forpag = model.Forpag;
                obj.GastoJudicial = model.Cartera == "J" ? "S" : "N";
                obj.Glosa = model.Glosa;
                obj.Libcompra = model.Libcompra;
                obj.Moneda = model.Moneda;
                obj.MotivoCobranza = model.MotivoCobranza;
                obj.NombreCliente = model.NombreCliente;
                obj.Numero = model.Numero;
                obj.NumeroOC = model.NumeroOC;
                obj.Ordcomp = model.Ordcomp;
                obj.Pclid = model.Pclid;
                obj.Remesa = model.Remesa;
                obj.RutCliente = model.RutCliente;
                obj.Selapl = model.Selapl;
                obj.Selcpbt = model.Selcpbt;
                obj.Sucursal = model.Sucursal == "null" ? null : model.Sucursal;
                obj.Tipcpbtdoc = model.Tipcpbtdoc;
                obj.Tipdig = model.Tipdig;
                obj.TipoCambio = model.TipoCambio;
                obj.TipoComprobante = model.TipoComprobante;
                obj.TipoComprobanteDesc = model.TipoComprobanteDesc;
                obj.TipoGasto = model.TipoGasto;
                obj.Tipprod = model.Tipprod;
                obj.Clbid = model.Clbid;
                obj.Sinimp = model.Sinimp;

                obj.Codemp = objSession.CodigoEmpresa;
                obj.Codsuc = objSession.CodigoSucursal;
                obj.Tipo = model.Tipo;
                obj.PJ = model.PJ;
                obj.Pag = model.Pag;
                obj.Descuento = decimal.Parse(model.Descuento ?? "0");
                obj.Glosa = model.Glosa;
                obj.Rolid = model.Rolid;
                obj.Glosa = model.Glosa;

                //obj.DetalleC.Add(new dto.DetalleCabeceraComprobante
                //{
                //    Nombre = model.Nombre
                //});

                salida = bcp.Comprobante.GrabarCastigoDevolucion(obj, objSession);

                foreach (string id in lstIds)
                {
                    splitted = id.Split('|');
                    ccbid = Int32.Parse( splitted[0]);
                    monto = decimal.Parse(splitted[1]);
                    saldo = decimal.Parse(splitted[2]);
                    asignado = decimal.Parse(splitted[3]);

                    obj.DetalleC.Add(new dto.DetalleCabeceraComprobante
                    {
                        Pclid= model.Pclid,
                        Ctcid= model.Ctcid,
                        Ccbid = ccbid,
                        Item = model.Item,
                        Insid = 0,
                        Precio = saldo == 0 ? asignado : saldo,
                        PrecioReal = monto,
                        Cantidad =  1 ,
                        Saldo = saldo == 0 ? asignado : saldo,
                        Neto = saldo == 0 ? asignado : saldo,
                        Impuesto=0,
                        Retenido = "N",
                        Total = saldo == 0 ? asignado : saldo
                    });

                        salida = bcp.Comprobante.GrabarDetalleCastigo(obj, objSession);
                        if (salida.Length>0)
                        {
                            if (salida[1].Equals("OK")){
                                error = false;
                            }else{
                                error = true;
                            }
                        }
                }

                //salida = bcp.Comprobante.GrabarDetalleComprobante(obj, objSession);

                // bcp.Deudor.MoverCartera(objSession.CodigoEmpresa, Int32.Parse(model.Pclid), Int32.Parse(model.Ctcid), lstIds, model.Comentario, model.Estado, Int32.Parse(model.EstadoDocumento));
                if (!error)
                {
                    scope.Complete();
                }
            }

            //return Json(error ? "Error al mover los documentos." : "");

            //dto.CabeceraComprobante obj = new dto.CabeceraComprobante();
            //string[] salida;

            //obj.Aplica = model.Aplica;
            //obj.CabeceraId = model.CabeceraId;
            //obj.Cambiodoc = model.Cambiodoc;
            //obj.Cancela = model.Cancela;
            //obj.Cartcli = model.Cartcli;
            //obj.Contable = model.Contable;
            //obj.Costos = model.Costos;
            //obj.Cptoctbl = model.Cptoctbl;
            //obj.Ctcid = model.Ctcid;
            //obj.Ccbid = model.Ccbid;
            //obj.Estado = model.Estado;
            //obj.FechaContabilizacion = model.FechaContabilizacion;
            //obj.FechaDocumento = model.FechaDocumento;
            //obj.FechaEntrega = model.FechaEntrega;
            //obj.FechaIngreso = model.FechaIngreso;
            //obj.FechaOrdenCompra = model.FechaOrdenCompra;
            //obj.FechaVencimiento = model.FechaVencimiento;
            //obj.Findeuda = model.Findeuda;
            //obj.FormaPago = model.FormaPago;
            //obj.Forpag = model.Forpag;
            //obj.Glosa = model.Glosa;
            //obj.Libcompra = model.Libcompra;
            //obj.Moneda = model.Moneda;
            //obj.MotivoCobranza = model.MotivoCobranza;
            //obj.NombreCliente = model.NombreCliente;
            //obj.Numero = model.Numero;
            //obj.NumeroOC = model.NumeroOC;
            //obj.Ordcomp = model.Ordcomp;
            //obj.Pclid = model.Pclid;
            //obj.Remesa = model.Remesa;
            //obj.RutCliente = model.RutCliente;
            //obj.Selapl = model.Selapl;
            //obj.Selcpbt = model.Selcpbt;
            //obj.Sucursal = model.Sucursal == "null" ? null : model.Sucursal;
            //obj.Tipcpbtdoc = model.Tipcpbtdoc;
            //obj.Tipdig = model.Tipdig;
            //obj.TipoCambio = model.TipoCambio;
            //obj.TipoComprobante = model.TipoComprobante;
            //obj.TipoComprobanteDesc = model.TipoComprobanteDesc;
            //obj.TipoGasto = model.TipoGasto;
            //obj.Tipprod = model.Tipprod;
            //obj.Clbid = model.Clbid;
            //obj.Sinimp = model.Sinimp;

            //obj.Codemp = objSession.CodigoEmpresa;
            //obj.Codsuc = objSession.CodigoSucursal;
            //obj.Tipo = model.Tipo;
            //obj.PJ = model.PJ;
            //obj.Pag = model.Pag;
            //obj.Descuento = decimal.Parse(model.Descuento ?? "0");
            //obj.Glosa = model.Glosa;
            //obj.Rolid = model.Rolid;

            //obj.DetalleC.Add(new dto.DetalleCabeceraComprobante
            //{
            //    Nombre = model.Nombre
            //});

            //using (TransactionScope scope = new TransactionScope())
            //{
            //    salida = bcp.Comprobante.GrabarCastigoDevolucion(obj, objSession);
            //    if (string.IsNullOrEmpty(salida[1]))
            //    {
            //        scope.Complete();
            //    }
            //}
            var jsonData = new
            {
                //id = salida[0],
                //glosa = obj.Glosa,
                //mensaje = salida[1]
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarTipoComprobante(string cartera)
        {
            List<Combobox> lstTipoComprobante = bcp.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId, "G", "-- Seleccione --");
            if (cartera == "V")
            {
                lstTipoComprobante.RemoveAt(1);
            }
            else
            {
                if (cartera == "J")
                {
                    lstTipoComprobante.RemoveAt(2);
                }
                
            }
            return Json(new SelectList(lstTipoComprobante, "Value", "Text"));
        }
        public ActionResult GetDocumentosDeudorSelect(GridSettings gridSettings, CastigoDevolucionModel model)
        {
            int pclid = model.Pclid, ctcid = model.Ctcid;

            int totalRecords = bcp.Deudor.ListarDocumentosCastDevGrillaCount(objSession.CodigoEmpresa, pclid, ctcid, model.Estado, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoCastDev> lstDocs = bcp.Deudor.ListarDocumentosCastDevGrilla(objSession.CodigoEmpresa, pclid, ctcid, model.Estado, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoCastDev item in lstDocs
                    select new
                    {
                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid + "|" + item.Monto + "|" + item.Saldo + "|" + 
                        item.Tipo + "|" + item.Moneda + "|" + item.Numero + "|" + item.FechaAsignacion + "|" + item.Estado + "|" + 
                        item.Asignado + "|" + item.EstadoCpbt + "|" + item.UltimoEstado + "|" + item.Deudor + "|" +
                        item.FechaVencimiento + "|" + item.Asegurado + "|" + item.RolId + "|" + item.RolNumero,
                        cell = new object[]
                        {
                           item.Tipo,
                           item.Moneda,
                           item.Numero,
                           item.FechaAsignacion,
                           item.FechaVencimiento,
                           item.Monto ,
                           item.Saldo,
                           item.Estado,
                           item.EstadoCpbt,
                           item.Asegurado,
                           item.RolNumero,
                           item.RolId
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDocsSelectedCastigoDevolucion(GridSettings gridSettings, CastigoDevolucionModel model)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<string> lstIds = new  List<string>();
           
            if (!string.IsNullOrEmpty(model.DocumentosSeleccionados)){
                model.DocumentosSeleccionados = model.DocumentosSeleccionados;
                lstIds = serializer.Deserialize<List<string>>(model.DocumentosSeleccionados);
            }

            List<dto.DocumentoCastDev> lst = new List<dto.DocumentoCastDev>();
            lst = bcp.Deudor.ListarDocsSelectedCastigoDevolucion(lstIds);
            int totalRecords = lst.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
           

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,
               
                rows =
                (
                    from dto.DocumentoCastDev item in lst
                    select new
                    {

                        id = item.Ccbid + "|" + item.Monto + "|" + item.Saldo + "|" + item.Asignado + "|" + item.EstadoCpbt,
                        cell = new object[]
                        {
                           item.Tipo,
                           item.Moneda,
                           item.Numero,
                           item.FechaAsignacion,
                           item.FechaVencimiento,
                           item.Monto ,
                           item.Saldo,
                           item.Estado,
                           item.EstadoCpbt,
                           item.Asegurado
                        }
                    }
                ).ToArray()
            };



            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Control Procesos"

        public ActionResult GetProcesos(GridSettings gridSettings, DocumentoDeudorModel model)
        {
            Funciones objFunc = new Funciones();
            // create json data 
            int totalRecords = bcp.Proceso.ListarProcesoCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Proceso> lst = bcp.Proceso.ListarProceso(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Proceso item in lst
                    select new
                    {
                        id = item.IdProceso,
                        cell = new object[]
                        {
                            item.IdProceso,
                            item.Nombre,
                            item.Servidor,
                            item.Running
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InterruptorProceso(string Action)
        {
            string[] data = Action.Split(',');
            string ServiceName = data[1];
            string MachineName = data[2];
            string status = data[3];

            ImpersonateUser iu = new ImpersonateUser();
            iu.Impersonate();
            Dimol.WindowsService.bcp.Service s = new Dimol.WindowsService.bcp.Service(ServiceName, MachineName);
            iu.Undo();
            


            if (s.Running)
                s.StopService();
            else
                s.StartService();
           
            return Json(s.Running);
        }


        #endregion
        
        #region "Castigo Masivo"

        public JsonResult ProcesoCastigoMasivo(CastigoMasivoModel model, GridSettings gridSettings)
        {
            //bool error = false;
            //string[] rut = model.RutCliente.Split('-');
            if (model.Complementaria)
            {
                return ProcesoCastigoMasivoComplementaria(model, gridSettings);
            }
            dto.CastigoMasivo objCarga = new dto.CastigoMasivo();
            objCarga.Archivo = model.Archivo;
            objCarga.Pclid = model.Pclid;
            objCarga.Codemp = objSession.CodigoEmpresa;
            objCarga.Sucid = objSession.CodigoSucursal;
            objCarga.Tpcid = 31; // castigo prejudicial
            objCarga.Pagina = model.Pagina;
            string ubicacion = ConfigurationManager.AppSettings["RutaCastigoMasivoOut"]+ objSession.UserId +"_"+ DateTime.Now.ToString("yyyyMMddhhmmss");
            if (!System.IO.Directory.Exists(ubicacion))
            {
                System.IO.Directory.CreateDirectory(ubicacion);
            }
            if (model.Archivo != "")
            {
 
            }
            // cargo lista de castigos
            List<Reportes.dto.CastigoMasivo> lst = Reportes.bcp.CastigoMasivo.ListarRutHtml();
            List<Reportes.dto.CastigoMasivo> lst100 = lst.GetRange(0, 1);
            //Reportes.bcp.CastigoMasivo.GeneraPDFporRutHiQPdf2(lst100, model.Pagina, ubicacion);
            int  intervalo = 50,i = 0;
            lst100 = lst.GetRange(i, intervalo);
            Reportes.bcp.CastigoMasivo.GeneraPDFporRutHiQPdf2(lst100, model.Pagina, ubicacion);
            while (i + intervalo <= lst.Count) 
            {
                i = i + intervalo;
                if (i + intervalo < lst.Count)
                {
                    lst100 = lst.GetRange(i, intervalo);
                }
                else
                {
                    lst100 = lst.GetRange(i, lst.Count - i);
                }

                Reportes.bcp.CastigoMasivo.GeneraPDFporRutHiQPdf2(lst100, model.Pagina, ubicacion);

            }

                //Reportes.bcp.CastigoMasivo.GeneraPDFporRutHiQPdf2(lst, model.Pagina, ubicacion);

            int totalPages = (int)Math.Ceiling((float)objCarga.ListaErrores.Count / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = objCarga.ListaErrores.Count,
                rows =
                (
                    from dto.ErrorCarga item in objCarga.ListaErrores
                    select new
                    {
                        id = 1,
                        cell = new object[]
                        {
                            item.Rut+"-"+item.Dv,
                            item.Nombre,
                            item.Numero,
                            item.TipoDocumento,
                            item.TipoError
                        }
                    }
                ).ToArray()
            };
            return Json(objCarga.ListaErrores, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProcesoCastigoMasivoComplementaria(CastigoMasivoModel model, GridSettings gridSettings)
        {
            //bool error = false;
            //string[] rut = model.RutCliente.Split('-');
            dto.CastigoMasivo objCarga = new dto.CastigoMasivo();
            objCarga.Archivo = model.Archivo;
            objCarga.Pclid = model.Pclid;
            objCarga.Codemp = objSession.CodigoEmpresa;
            objCarga.Sucid = objSession.CodigoSucursal;
            objCarga.Tpcid = 31; // castigo prejudicial
            objCarga.Pagina = model.Pagina;
            string ubicacion = @"\\10.0.1.238\Usuarios\castigos\"; //ConfigurationManager.AppSettings["RutaCastigoMasivoOut"] + objSession.UserId + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            if (!System.IO.Directory.Exists(ubicacion))
            {
                System.IO.Directory.CreateDirectory(ubicacion);
            }
            if (model.Archivo != "")
            {

            }
            // cargo lista de castigos
            List<Reportes.dto.CastigoMasivo> lst = Reportes.bcp.CastigoMasivo.ListarRutHtmlComplementaria();
            List<Reportes.dto.CastigoMasivo> lst100 = lst.GetRange(0, 1);
            //Reportes.bcp.CastigoMasivo.GeneraPDFporRutHiQPdf2(lst100, model.Pagina, ubicacion);
            int intervalo = 50, i = 49;
            lst100 = lst;//.GetRange(i, intervalo > lst.Count ? lst.Count : intervalo);
            Reportes.bcp.CastigoMasivo.GeneraPDFporRutComplementaria(lst100, model.Pagina, ubicacion);
            while (i + intervalo <= lst.Count)
            {
                i = i + intervalo;
                if (i + intervalo < lst.Count)
                {
                    lst100 = lst.GetRange(i, intervalo);
                }
                else
                {
                    lst100 = lst.GetRange(i, lst.Count - i);
                }

                Reportes.bcp.CastigoMasivo.GeneraPDFporRutComplementaria(lst100, model.Pagina, ubicacion);

            }

            //Reportes.bcp.CastigoMasivo.GeneraPDFporRutHiQPdf2(lst, model.Pagina, ubicacion);

            int totalPages = (int)Math.Ceiling((float)objCarga.ListaErrores.Count / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = objCarga.ListaErrores.Count,
                rows =
                (
                    from dto.ErrorCarga item in objCarga.ListaErrores
                    select new
                    {
                        id = 1,
                        cell = new object[]
                        {
                            item.Rut+"-"+item.Dv,
                            item.Nombre,
                            item.Numero,
                            item.TipoDocumento,
                            item.TipoError
                        }
                    }
                ).ToArray()
            };
            return Json(objCarga.ListaErrores, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Visita Terreno"
        public JsonResult ListarVisitasTerreno(GridSettings gridSettings, int ctcid)
        {
            //bcp.CargaVisitaTerreno.CrearVisitaTerreno();
            List<dto.VisitaTerrenoDetalle> lst = bcp.VisitaTerreno.ListarVisitaTerreno(ctcid);

            int totalRecords = lst.Count;

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.VisitaTerrenoDetalle item in lst
                    select new
                    {
                        id = item.IdVisita + "|" + item.IdVisitaDetalle,
                        cell = new object[]
                        {
                            item.IdVisita,
                            item.IdVisitaDetalle,
                            item.Ctcid,
                            item.Direccion,
                            item.EstadoVisita,
                            item.Visita,
                            item.Comentarios,
                            item.Latitud,
                            item.Longitud,
                            item.Comuna,
                            item.FechaEnvio,
                            item.DireccionEnvio,
                            item.PosicionEnvio,
                            "Mapa",
                            "MapaVisita"

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarVisitasTerrenoGPS(GridSettings gridSettings, int idVisita, int idVisitaDetalle)
        {
            List<dto.VisitaTerrenoGPS> lst = bcp.VisitaTerreno.ListarVisitaTerrenoGPS(idVisita, idVisitaDetalle);

            int totalRecords = lst.Count;

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.VisitaTerrenoGPS item in lst
                    select new
                    {
                        id = item.Latitud + "|" + item.Longitud,
                        cell = new object[]
                        {
                            item.Latitud,
                            item.Longitud,
                            item.Altitud,
                            item.Direccion,
                            item.Comuna,
                            item.Ciudad,
                            "Mapa"
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarVisitasTerrenoFotos(int idVisita, int idVisitaDetalle)
        {
            return Json(bcp.VisitaTerreno.ListarVisitasTerrenoFotos(idVisita, idVisitaDetalle));
        }

        public JsonResult ListarVisitaTerrenoTelefonos(GridSettings gridSettings, int idVisita, int idVisitaDetalle)
        {
            List<dto.VisitaTerrenoTelefono> lst = bcp.VisitaTerreno.ListarVisitaTerrenoTelefonos(idVisita, idVisitaDetalle);

            int totalRecords = lst.Count;

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.VisitaTerrenoTelefono item in lst
                    select new
                    {
                        id = 1,
                        cell = new object[]
                        {
                            item.Numero
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarVisitaTerrenoSolicitud(int ctcid, string direccion,
                                                        int idRegion, int idCiudad, int idComuna,
                                                        string comuna)
        {
            int result = bcp.VisitaTerreno.InsertarVisitaTerrenoSolicitud(objSession.CodigoEmpresa, ctcid, direccion, idRegion, idCiudad, idComuna, comuna, objSession.UserId, objSession);


            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarVisitaTerrenoSolicitudCoordenadas(string coordenadas, bool respuesta, int SolicitudId)
        {
            int result = 0;
            if (respuesta)
            {
                string[] splitted;
                splitted = coordenadas.Split('|');
                result = bcp.VisitaTerreno.InsertarVisitaTerrenoSolicitudCoordenadas(SolicitudId, splitted[0], splitted[1]);
            }
            else
            {
                result = bcp.VisitaTerreno.InsertarVisitaTerrenoSolicitudCoordenadas(SolicitudId, "0", "0");
                Dimol.dao.Funciones.InsertarError(coordenadas + "Solicitud" + SolicitudId, "Cartera/InsertarVisitaTerrenoSolicitudCoordenadas", "Cartera/ProcesoCargaMasiva", 151);
            }
                        
            return Json(result);
        }
        public ActionResult VisitaTerrenoAprobar()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Direccion objDireccion = new Direccion();

            ViewBag.TipoDocumento = new SelectList(objDeudor.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma, "Todos"), "Value", "Text", "");
            ViewBag.Pais = new SelectList(objDireccion.ListarPais(), "Value", "Text", objSession.CodPais);
            ViewBag.Region = new SelectList(objDireccion.ListarRegion(objSession.CodPais), "Value", "Text");
            ViewBag.Ciudad = new SelectList(objDireccion.ListarCiudad(0), "Value", "Text");
            ViewBag.Comuna = new SelectList(objDireccion.ListarComuna(0), "Value", "Text");
            
            Gestor objGestor = new Gestor();
            ViewBag.lstGestoresTerreno = new SelectList(objGestor.ListarGestoresVisitaTerreno(objSession.CodigoEmpresa, objSession.CodigoSucursal), "Value", "Text");
            return View();
        }


        public ActionResult ListarVisitaTerrenoSolicitudAprobar(GridSettings gridSettings, VisitaTerrenoAprobarModel model)
        {
            // create json data 
            int totalRecords = bcp.VisitaTerreno.ListarVisitaTerrenoSolicitudCount(objSession.CodigoEmpresa, string.IsNullOrEmpty(model.Pclid) ? 0 : Int32.Parse(model.Pclid),
                                                            string.IsNullOrEmpty(model.Region) ? 0 : Int32.Parse(model.Region), string.IsNullOrEmpty(model.Ciudad) ? 0 : Int32.Parse(model.Ciudad),
                                                    string.IsNullOrEmpty(model.Comuna) ? 0 : Int32.Parse(model.Comuna), model.Monto, model.Quiebra, model.Solicitud, model.PreQuiebra, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.VisitaTerrenoSolicitudAceptar> lst = bcp.VisitaTerreno.ListarVisitaTerrenoSolicitudAprobar(objSession.CodigoEmpresa, string.IsNullOrEmpty(model.Pclid) ? 0 : Int32.Parse(model.Pclid),
                                                    string.IsNullOrEmpty(model.Region) ? 0 : Int32.Parse(model.Region), string.IsNullOrEmpty(model.Ciudad) ? 0 : Int32.Parse(model.Ciudad),
                                                    string.IsNullOrEmpty(model.Comuna) ? 0 : Int32.Parse(model.Comuna), model.Monto, model.Quiebra, model.Solicitud, model.PreQuiebra, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.VisitaTerrenoSolicitudAceptar item in lst
                    select new
                    {
                        id = item.pclid + "|" + item.ctcid + "|" + item.direccion + "|" + item.regId + "|" + item.ciuId + "|" + item.comId + "|" + item.comuna + "|" + item.solicitudId,
                        cell = new object[]
                        {
                            item.ctcid,
                            item.rutDeudor,
                            item.deudor,
                            item.direccion,
                            item.comuna,
                            item.ciudad,
                            item.deuda,
                            item.cliente,
                            item.gestor,
                            item.Solicitante,
                            item.ultimaGestion
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GrabarAceptarVisitas(List<String> ids, string gestorId, string gestorNombre)
        {
            
            string[] splitted;
            List<dto.VisitaTerrenoSolicitudAceptar> lst = new List<dto.VisitaTerrenoSolicitudAceptar>();
            foreach (string s in ids)
            {
                splitted = s.Split('|');
                lst.Add(new dto.VisitaTerrenoSolicitudAceptar()
                {
                    solicitudId = Int32.Parse(splitted[7]),
                    pclid = Int32.Parse(splitted[0]),
                    ctcid = Int32.Parse(splitted[1]),
                    direccion =splitted[2],
                    regId = Int32.Parse(splitted[3]),
                    ciuId = Int32.Parse(splitted[4]),
                    comId = Int32.Parse(splitted[5]),
                    comuna = splitted[6]
               
                });

            }
            List<dto.VisitaTerrenoSolicitudAceptar> salida = new List<dto.VisitaTerrenoSolicitudAceptar>();
            int gesId = Int32.Parse(gestorId.Split('|')[0]);
            string imei = gestorId.Split('|')[1];
            string gestor = gestorNombre.Split('-')[0];
            string telf = gestorNombre.Split('-')[1];
            salida = bcp.VisitaTerreno.GrabarAceptarVisitas(lst, objSession.CodigoEmpresa, objSession.UserId, 2, gesId, gestor.Trim(), imei, telf.Trim(), "N", objSession);


            return Json(salida);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GrabarRechazarVisitas(List<String> ids)
        {

            string[] splitted;
            List<dto.VisitaTerrenoSolicitudAceptar> lst = new List<dto.VisitaTerrenoSolicitudAceptar>();
            foreach (string s in ids)
            {
                splitted = s.Split('|');
                lst.Add(new dto.VisitaTerrenoSolicitudAceptar()
                {
                    solicitudId = Int32.Parse(splitted[7]),
                    pclid = Int32.Parse(splitted[0]),
                    ctcid = Int32.Parse(splitted[1]),
                    direccion = splitted[2],
                    regId = Int32.Parse(splitted[3]),
                    ciuId = Int32.Parse(splitted[4]),
                    comId = Int32.Parse(splitted[5]),
                    comuna = splitted[6]

                });

            }

            List<Combobox> salida = bcp.VisitaTerreno.GrabarRechazarVisitas(lst, objSession.CodigoEmpresa, objSession.UserId, 3, "N");


            return Json(salida);
        }

        public ActionResult VisitaTerrenoGenerar()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Gestor objGestor = new Gestor();
            ViewBag.lstGestoresTerreno = new SelectList(objGestor.ListarGestoresVisitaTerreno(objSession.CodigoEmpresa, objSession.CodigoSucursal), "Value", "Text");
            ViewBag.userGeoGestion = bcp.VisitaTerreno.getUserGeoGestion();
            ViewBag.passGeoGestion = bcp.VisitaTerreno.getPassGeoGestion();
            //ViewBag.lstCarteraGestoresTerreno = new SelectList(objGestor.ListarGestoresVisitaTerreno(objSession.CodigoEmpresa, objSession.CodigoSucursal), "Value", "Text");            
            return View();
        }
        public ActionResult ListarVisitaTerrenoGenerar(GridSettings gridSettings, VisitaTerrenoGenerarModel model)
        {
            // create json data 
            int totalRecords = bcp.VisitaTerreno.ListarVisitaTerrenoGenerarCount(Int32.Parse(model.lstGestoresTerreno), gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.VisitaTerrenoGenerar> lst = bcp.VisitaTerreno.ListarVisitaTerrenoGenerar(Int32.Parse(model.lstGestoresTerreno), gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.VisitaTerrenoGenerar item in lst
                    select new
                    {
                        id = item.RutDeudor + "-" + item.Deudor + "|" + item.Latitud + "|" + item.Longitud + "|" + item.Direccion + "|" + item.SolicitudId
                                + "|" + item.Comuna + "|" + item.Ciudad + "|" + item.RutDeudor,
                        cell = new object[]
                        {
                            item.SolicitudId,
                            item.RutDeudor,
                            item.Deudor,
                            item.Direccion,
                            item.Comuna,
                            item.Ciudad,
                            item.Latitud,
                            item.Longitud,
                            item.Deuda,
                            item.Gestor
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GenerarVisitasTerreno(List<String> ids, string gestorId, string gestorNombre)
        {
           try
            {
                //List<dto.VisitaTerrenoGenerar> lst = new List<dto.VisitaTerrenoGenerar>();
                string error = string.Empty;
                int gesId = Int32.Parse(gestorId.Split('|')[0]);
                string imei = gestorId.Split('|')[1];
                string gestor = gestorNombre.Split('-')[0];
                string telf = gestorNombre.Split('-')[1];
                string[] splitted;
               
                Funciones objFunc = new Funciones();
                string fileName = "";
                string path = "";

                var csv = new StringBuilder();
                var firstLine = "NOMBRE,LATITUD,LONGITUD,RADIO(metros),ADDRESS,DESCRIPCION,COLOR,PHONE,MAIL,Nota: Esta linea no se debera modificar ya que el alogritmo no la procesara. Cualquier dato aqui contenido sera ignorado.";
                csv.AppendLine(firstLine);
                int radio = 100;
                foreach (string s in ids)
                {
                    splitted = s.Split('|');
                    if (splitted[1] != "0")
                    {

                        int salida = bcp.VisitaTerreno.VisitaTerrenoSolicitudGenerar(Int32.Parse(splitted[4]), 4, objSession.UserId, string.Empty, 0, string.Empty, "N");
                        if (salida > 0)
                        {
                            var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6}", splitted[0] + "-" + splitted[4], splitted[1].Replace(",", "."), splitted[2].Replace(",", "."), radio, splitted[3], splitted[4], "green,,,");
                            csv.AppendLine(newLine);
                        }
                    }
                    else
                    {
                        error = "Verifique la latitud y longitud de las direcciones que intenta generar";
                    }
                }
                fileName = gestor.Trim() + "_" + objSession.UserId + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                path = ConfigurationManager.AppSettings["RutaArchivosTerreno"];
                objFunc.CreaCarpetas(path);
                //To save file
                FileInfo fi = new FileInfo(path + fileName);

                if (!fi.Exists)
                {
                    fi.Create().Dispose();
                }
               
                System.IO.File.AppendAllText(path + fileName, csv.ToString());
                //var jsonCsv = JsonConvert.SerializeObject(csv,
                //                    new JsonSerializerSettings());
                var csvData = new
                {
                    filename = fileName,
                    filepath = path,
                    jcsv = csv
                };
                return Json(new { messageError = error, data = csvData }, JsonRequestBehavior.AllowGet);
                                     
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Cartera/GenerarVisitasTerreno", 120);
                return Json(new { messageError = ex.Message, data = string.Empty }, JsonRequestBehavior.AllowGet);
                             
            }
           
        }
        public JsonResult EnviarVisitaTerreno(string idClienteGeoGestion, string idvisita, string gestorId, string telefonoImei)
        {

            int salida = bcp.VisitaTerreno.VisitaTerrenoSolicitudGenerar(Int32.Parse(idvisita), 4, objSession.UserId,
                                                                        idClienteGeoGestion, Int32.Parse(gestorId), telefonoImei, "N");

            return Json(salida, JsonRequestBehavior.AllowGet);
        }
        public FileResult Download(string filePath, string file)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = file;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult NotificarTerrenoColor(int ctcid)
        {
            List<string> resultados = new List<string>();
            resultados = bcp.VisitaTerreno.NotificarTerrenoColor(ctcid);
            var result = string.Join(",", resultados.ToArray());

            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult VisitaTerrenoCarteraGeoGestion()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Gestor objGestor = new Gestor();
            ViewBag.LstGestor = new SelectList(objGestor.ListarVisitaTerrenoGestores(objSession.CodigoEmpresa, objSession.CodigoSucursal), "Value", "Text");
            ViewBag.userGeoGestion = bcp.VisitaTerreno.getUserGeoGestion();
            ViewBag.passGeoGestion = bcp.VisitaTerreno.getPassGeoGestion();
            return View();
        }
        public ActionResult ListarVisitaTerrenoCarteraGeoGestionGestor(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = bcp.VisitaTerreno.ListarVisitaTerrenoCarteraGeoGestionGestorCount(gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.VisitaTerrenoCarteraGestorGeoGestion> lst = bcp.VisitaTerreno.ListarVisitaTerrenoCarteraGeoGestionGestor(gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.VisitaTerrenoCarteraGestorGeoGestion item in lst
                    select new
                    {
                        id = item.CarteraId,
                        cell = new object[]
                        {
                            item.Cartera_Nombre,
                            item.Descripcion,
                            item.Ges_Nombre,
                            item.TelefonoTerreno,
                            item.TelefonoImei,
                            item.CarteraId,
                            item.GesId
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GuardarVisitaTerrenoCarteraGestor(string carteraId, string gestorId, string carteraNombre,
                                                            string carteraDescripcion)
        {
            int salida = -1;
            salida = bcp.VisitaTerreno.InsertarVisitaTerrenoCarteraGestor(carteraId, Int32.Parse(gestorId), carteraNombre, carteraDescripcion,objSession.UserId);

            return Json(salida, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CountVisitaTerrenoCarteraGestor(string gestorId)
        {
            int salida = -1;
            salida = bcp.VisitaTerreno.CountVisitaTerrenoCarteraGestor(Int32.Parse(gestorId));

            return Json(salida, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult DescargarVisitaTerreno(string Archivo, GridSettings gridSettings)
        {
            try
            {
                int totalRecords = 0;
                List<dto.DatosCargaVisitaTerreno> lstResult = new List<dto.DatosCargaVisitaTerreno>();
                if (Archivo != "")
                {
                    List<dto.DatosCargaVisitaTerreno> lst = bcp.CargaVisitaTerreno.CargarDatosVisitaTerreno(Archivo, objSession.UserId);
                    lstResult = bcp.CargaVisitaTerreno.ProcesoCargaTerreno(lst, objSession);
                    totalRecords = lstResult.Count;
                }

                int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
                                
                    var jsonData = new
                    {
                        total = totalPages,
                        page = gridSettings.pageIndex,
                        records = totalRecords,

                        rows =
                        (
                            from dto.DatosCargaVisitaTerreno item in lstResult
                            select new
                            {
                                id = item.FormularioId,
                                cell = new object[]
                                {
                                    item.Deudor,
                                    item.FechaVisita,
                                    item.EstadoVisita,
                                    item.Visita,
                                    item.Gestor,
                                    item.Procesado,
                                    item.mensaje
                                }
                            }
                        ).ToArray()
                    };

                    return Json(new { success = true, data = lstResult }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Cartera/ProcesoCargaMasiva", 150);
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CrearEnviarVisitasTerreno(List<String> ids, string user, string pass, string gestorId, string gestorNombre)
        {
            var tuple = bcp.VisitaTerreno.CrearEnviarVisitasTerreno(ids, user, pass, gestorId, gestorNombre, objSession.UserId);
            return Json(new { messageError = tuple.Item1, messagesuccess = tuple.Item2 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CrearClientLayerVisitaTerrenoCarteraGestor(string gestorId, string imei, string carteraNombre, string carteraDescripcion, string userGeo, string passGeo)
        {
            string salida = string.Empty;
            salida = bcp.VisitaTerreno.crearClientLayerCarteraTerrenoVisitaGestor(Int32.Parse(gestorId), imei, carteraNombre, carteraDescripcion, userGeo, passGeo, objSession.UserId);

            return Json(salida, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Gestor"
        public ActionResult Gestor()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            Gestor objGestor = new Gestor();

            ViewBag.LstTipoCartera = new SelectList(objGestor.TraeListaDe("TipCart", objSession.Idioma), "Value", "Text");
            ViewBag.LstGrupoCobranza = new SelectList(objGestor.ListarGrupoCobranza(objSession.CodigoEmpresa, objSession.CodigoSucursal), "Value", "Text");
            ViewBag.LstEmpleado = new SelectList(objGestor.ListarEmpleados(objSession.CodigoEmpresa), "Value", "Text");
            
            return View();
        }
        public ActionResult ListarGestorGrilla(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = bcp.Gestor.ListarGestorGrillaCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Gestor> lst = bcp.Gestor.ListarGestorGrilla(objSession.CodigoEmpresa, objSession.CodigoSucursal, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Gestor item in lst
                    select new
                    {
                        id = item.GesId,
                        cell = new object[]
                        {
                            item.Nombre,
                            item.Telefono,
                            item.Email,
                            item.Estado,
                            item.GesId,
                            item.IdTipoCartera,
                            item.IdGrupo,
                            item.IndRemoto,
                            item.IndTerreno,
                            item.TelefonoTerreno,
                            item.TelefonoImei,
                            item.IdEmpleado
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GuardarGestor(GestorModel model)
        {
            try
            {
                int respuesta = -1;

                dto.Gestor objGestor = new dto.Gestor();
                objGestor.GesId = model.GesId;
                objGestor.Nombre = model.Nombre;
                objGestor.Telefono = model.Telefono;
                objGestor.Email = model.Email;
                objGestor.Estado = ((model.GestorActivo) ? "A" : "N");
                objGestor.IdTipoCartera = Int32.Parse(model.LstTipoCartera);
                objGestor.IdGrupo = Int32.Parse(model.LstGrupoCobranza);
                objGestor.IndRemoto = ((model.GestorRemoto) ? "S" : "N");
                objGestor.IndTerreno = ((model.GestorTerreno) ? "S" : "N");
                objGestor.TelefonoTerreno = model.TelefonoTerreno;
                objGestor.TelefonoImei = model.TelefonoImei;
                objGestor.IdEmpleado = model.LstEmpleado;
                if (string.IsNullOrEmpty(model.TelefonoTerreno))
                {
                    model.TelefonoTerreno = string.Empty;
                }
                if (string.IsNullOrEmpty(model.TelefonoImei))
                {
                    model.TelefonoImei = string.Empty;
                }
                respuesta = bcp.Gestor.GuardarGestor(objGestor, objSession.CodigoEmpresa, objSession.CodigoSucursal);
                return Json(new { success = true, data = respuesta }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Cartera/GuardarGestor", 15);
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult VisitaTerrenoDescarga()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
         #endregion

        #region "PerfilEstadosCobranzas"

        public ActionResult PerfilesEstadoCobranza()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            EstadosCobranza objEstadoCobranza = new EstadosCobranza();

            ViewBag.lstTipoEstado = new SelectList(objEstadoCobranza.TraeListaTipoEstados("TipAgru", objSession.Idioma), "Value", "Text");
            ViewBag.lstPerfil = new SelectList(objEstadoCobranza.ListarPerfiles(objSession.CodigoEmpresa, objSession.Idioma, objSession.PrfId), "Value", "Text");

            return View();
        }

        public ActionResult ListarEstadosCarteraPerfil(GridSettings gridSettings, PerfilEstadoCobranzaModel model)
        {
            // create json data 
            int totalRecords = bcp.EstadosCobranza.ListarEstadosCarteraPerfilCount(objSession.CodigoEmpresa, objSession.Idioma, Int32.Parse(model.lstPerfil), Int32.Parse(model.lstTipoEstado), gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PerfilEstadoCobranza> lst = bcp.EstadosCobranza.ListarEstadosCarteraPerfil(objSession.CodigoEmpresa, objSession.Idioma, Int32.Parse(model.lstPerfil), Int32.Parse(model.lstTipoEstado), gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.PerfilEstadoCobranza item in lst
                    select new
                    {
                        id = item.Estid,
                        cell = new object[]
                        {
                            item.IsSelected,
                            item.Estid,
                            item.Nombre
                        }
                    }
                ).ToArray()
            };
            
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarPerfilEstado(int perfil, int estid, int accion)
        {
            int result = 0;
            result = bcp.EstadosCobranza.InsertarPerfilEstado(objSession.CodigoEmpresa, perfil, estid, accion, objSession.UserId);
            
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EliminarPerfilEstado(int perfil, int estid, int accion)
        {
            int result = 0;
            result = bcp.EstadosCobranza.EliminarPerfilEstado(objSession.CodigoEmpresa, perfil, estid, accion, objSession.UserId);

            return Json(result);
        }
        #endregion

        #region "ClienteEstadosCobranzas"
        public ActionResult ClienteEstadoCobranza()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            EstadosCobranza objEstadoCobranza = new EstadosCobranza();

            ViewBag.lstTipoEstado = new SelectList(objEstadoCobranza.TraeListaTipoEstados("TipAgru", objSession.Idioma), "Value", "Text");
           
            return View();
        }
        public ActionResult BuscarRutNombreClienteTipoCliente(string term)
        {
            bcp.Cliente objCliente = new Cliente();
            return Json(objCliente.BuscarRutNombreClienteTipoCliente(term, objSession.CodigoEmpresa), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarEstadosCarteraCliente(GridSettings gridSettings, ClienteEstadoCobranzaModel model)
        {
            // create json data 
            int totalRecords = bcp.EstadosCobranza.ListarEstadosCarteraClienteCount(objSession.CodigoEmpresa, objSession.Idioma, Int32.Parse(model.Pclid), Int32.Parse(model.lstTipoEstado), gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PerfilEstadoCobranza> lst = bcp.EstadosCobranza.ListarEstadosCarteraCliente(objSession.CodigoEmpresa, objSession.Idioma, Int32.Parse(model.Pclid), Int32.Parse(model.lstTipoEstado), gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.PerfilEstadoCobranza item in lst
                    select new
                    {
                        id = item.Estid,
                        cell = new object[]
                        {
                            item.IsSelected,
                            item.Estid,
                            item.Nombre
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarClienteEstado(int pclid, int estid, int accion)
        {
            int result = 0;
            result = bcp.EstadosCobranza.InsertarClienteEstado(objSession.CodigoEmpresa, pclid, estid, accion, objSession.UserId);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EliminarClienteEstado(int pclid, int estid, int accion)
        {
            int result = 0;
            result = bcp.EstadosCobranza.EliminarClienteEstado(objSession.CodigoEmpresa, pclid, estid, accion, objSession.UserId);

            return Json(result);
        }
        #endregion

        #region "Bloquear Rol"

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult BloquearRol(int Rolid, bool BloquearRol)
        {
            int exito = -1;
            if (objSession.PrfId == 1 || objSession.PrfId == 6 || objSession.PrfId == 9 || objSession.PrfId == 13)
            {
                exito = Dimol.Judicial.Mantenedores.bcp.Rol.BloquearRol(objSession.CodigoEmpresa, Rolid, BloquearRol ? "S" : "N", objSession.UserId);
            }
            return Json(exito, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region "Terceros"
        public ActionResult ListarTerceros(GridSettings gridSettings, int Pclid, int Ctcid)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TerceroDocumento> lst = bcp.Comprobante.ListarTercerosDocumentos(objSession.CodigoEmpresa, Pclid, Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);
            int totalPages = (int)Math.Ceiling((float)lst.Count / (float)gridSettings.pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = lst.Count,

                rows =
                (
                    from dto.TerceroDocumento item in lst
                    select new
                    {
                        id = item.TerceroId,
                        cell = new object[]
                        {
                            item.TerceroId,
                            item.Rut,
                            item.Nombre
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Motivo Castigo Devolucion"
        public ActionResult ListarMotivoCastigoDevolucion(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.MotivoCastigoDevolucion> lst = bcp.Comprobante.ListarMotivoCastigoDevolucion(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);
            int totalPages = (int)Math.Ceiling((float)lst.Count / (float)gridSettings.pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = lst.Count,

                rows =
                (
                    from dto.MotivoCastigoDevolucion item in lst
                    select new
                    {
                        id = item.TipoId,
                        cell = new object[]
                        {
                            item.TipoId,
                            item.Nombre
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Panel Aprobacion Comprobantes"
        public ActionResult CastigoDevolucionAprobar()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public JsonResult ListarPanelAprobarCastigoDevolucionGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.ComprobanteCabecera> lst = bcp.Comprobante.ListarComprobantesAprobarGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                
                    from dto.ComprobanteCabecera item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.Pclid + "|" + item.Tpcid + "|" + item.Folio,
                        cell = new object[]
                        {
                           item.Pclid,
                           item.Tpcid,
                           item.TipoComprobante,
                           item.Folio,
                           item.Cliente,
                           item.FecEmision ,
                           item.Saldo,
                           item.Estado
                           
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarPanelAprobarCastigoDevolucionDetalleGrilla(GridSettings gridSettings, int tipoComprobante, int folio, int pclid)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.ComprobanteCabeceraDetalle> lst = bcp.Comprobante.ListarComprobantesAprobarDetalleGrilla(objSession.CodigoEmpresa, tipoComprobante, folio, pclid,gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.ComprobanteCabeceraDetalle item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid,
                        cell = new object[]
                        {
                           item.Pclid,
                           item.Ctcid,
                           item.Ccbid,
                           item.RutDeudor,
                           item.Deudor,
                           item.Tipo,
                           item.Numero,
                           item.FechaAsignacion,
                           item.FechaVencimiento,
                           item.Monto ,
                           item.Saldo,
                           item.Asignado,
                           item.UltimoEstado,
                           item.Estado,
                           item.Asegurado
                           
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RechazarComprobanteCastigoDevolucion(int comprobanteId, int folio, int pclid, string motivo)
        {
            int salida = 1;
            salida = bcp.Comprobante.RechazarCastigoDevolucion(objSession.CodigoEmpresa, folio, comprobanteId, pclid, motivo, objSession.UserId);
            return Json(salida, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AprobarComprobanteCastigoDevolucion(int comprobanteId, int folio, int pclid, string nombreComprobante, string reportsIs)
        {
            int salida = -1;
            //comprobanteId = 32;
            //folio = 1829;
            //pclid = 90;
            List<string> lstReportes = new List<string>();
            if (!string.IsNullOrEmpty(reportsIs))
            {
                lstReportes = JsonConvert.DeserializeObject<List<string>>(reportsIs);
            }
                        
            List<dto.ComprobanteCabeceraDetalle> lst = bcp.Comprobante.ListarComprobantesAprobarDetalleGrilla(objSession.CodigoEmpresa, comprobanteId, folio, pclid, "", "Deudor", "asc", 0, 10000);
            salida = bcp.Comprobante.AprobarCastigoDevolucion(lst, objSession.CodigoEmpresa, folio, comprobanteId, pclid, nombreComprobante, objSession.UserId, objSession.IpRed, objSession.IpPc);
             List<string> lstArchivos = new List<string>();
            if (lstReportes.Count > 0)
            {
                if (salida > 0)
                {
                    lstArchivos = bcp.Comprobante.AprobarCastigoDevolucionReporte(lst, lstReportes, objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.Idioma, comprobanteId, folio, pclid);
                }
            }

            return Json(new { resultado = salida, zipList = lstArchivos }, JsonRequestBehavior.AllowGet);
            
        }
        #endregion

        #region MANEJO DE ROLES PREVISIONALES
        public JsonResult VerificarEsClientePrevisional(int codemp, int IdCliente = 0, string RutCliente = "") {
            var EsPrevisional = bcp.Cliente.VerificarEsClientePrevisional(codemp, IdCliente, RutCliente);

            return Json(new { esPrevisional = EsPrevisional }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Bienes"
        public ActionResult ListarBienesRaicesGrilla(GridSettings gridSettings, int ctcid)
        {
            // create json data 
            int totalRecords = bcp.BienPropiedad.ListarBienesRaicesGrillaCount(ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BienPropiedad> lst = bcp.BienPropiedad.ListarBienesRaicesGrilla(ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BienPropiedad item in lst
                    select new
                    {
                        id = item.BienesRaicesId,
                        cell = new object[]
                        {
                            item.BienesRaicesId,
                            item.ConservadorId,
                            item.Conservador,
                            item.Rol,
                            item.Foja,
                            item.Anio,
                            item.Direccion,
                            item.Propietario,
                            item.EvaluoFiscal,
                            item.Verificado,
                            item.Hipotecado,
                            item.Embargo,
                            System.Configuration.ConfigurationManager.AppSettings["UrlArchivos"] + "Certificados/" + item.ArchivoCertificado
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarBienesVehiculosGrilla(GridSettings gridSettings, int ctcid)
        {
            
            // create json data 
            int totalRecords = bcp.BienVehiculo.ListarBienesVehiculosGrillaCount(ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.BienVehiculo> lst = bcp.BienVehiculo.ListarBienesVehiculosGrilla(ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.BienVehiculo item in lst
                    select new
                    {
                        id = item.VehiculoId,
                        cell = new object[]
                        {
                            item.VehiculoId,
                            item.MarcaId,
                            item.ModeloId,
                            item.Patente,
                            item.Marca,
                            item.Modelo,
                            item.Anio,
                            item.Propietario,
                            item.Prenda,
                            item.Embargo,
                            System.Configuration.ConfigurationManager.AppSettings["UrlArchivos"] + "Certificados/" + item.ArchivoComprobante
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DetalleBienes(int ctcid)
        {
            var objeto = new dto.BienDetalle();
            objeto = bcp.BienPropiedad.DetalleBienes(ctcid);
           
            return Json(objeto, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarModeloVehiculo(int marcaId)
        {
            List<Combobox> lstModeloVehiculos = bcp.BienVehiculo.ListarModelosVehiculo(marcaId);
           
            return Json(new SelectList(lstModeloVehiculos, "Value", "Text"));
        }

        public JsonResult GuardarBienVehiculo(dto.BienVehiculo model, int ctcid)
        {
            try
            {
                int respuesta = -1;
                              
                respuesta = bcp.BienVehiculo.InsertUpdateBienVehiculo(ctcid, model, objSession.UserId);
                return Json(new { success = true, data = respuesta }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "GuardarBienVehiculo", 66);
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult GuardarBienPropiedad(dto.BienPropiedad model, int ctcid)
        {
            try
            {
                int respuesta = -1;

                respuesta = bcp.BienPropiedad.InsertUpdateBienPropiedad(ctcid, model, objSession.UserId);
                return Json(new { success = true, data = respuesta }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "GuardarBienPropiedad", 66);
                return Json(new { success = false, data = ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }
        #endregion
    }
}