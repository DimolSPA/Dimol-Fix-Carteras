using Dimol.Reportes.Models;
using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dimol.Carteras.bcp;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Globalization;
using Dimol.Email.bcp.Services;
using Dimol.Email.dto;
using Dimol.Email.dto.MailModels;
using System.Threading.Tasks;

namespace Dimol.Reportes.Controllers
{
    public class EmailController : Dimol.Controllers.BaseController
    {
        IEnvioEmailService _emailService;
        //
        // GET: /ReportesCartera/
        public EmailController()
        {
            this._emailService = new EnvioEmailService();
        }

        public ActionResult Cartera(string ctr, string pag)
        {
            this.SettingAccount();
            if (string.IsNullOrEmpty(pag))
            {
                pag = "1";
            }
            ViewBag.Codemp = objSession.CodigoEmpresa;
            ViewBag.Reporte = new SelectList(bcp.Cartera.ListarReportes(Int32.Parse(pag), objSession.Idioma, "Seleccione"), "Value", "Text", "");
            Comprobante objComprobante = new Comprobante();
            Deudor objDeudor = new Deudor();
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "3");
            ViewBag.CodigoCarga = new SelectList(objComprobante.ListarCodigoCarga(objSession.CodigoEmpresa, 1, "Seleccione"), "Value", "Text", "");
            ViewBag.GrupoCobranza = new SelectList(Email.bcp.Vista.ListarGrupoCobranza(objSession.CodigoEmpresa, objSession.CodigoSucursal, "Seleccione"), "Value", "Text", "");
            ViewBag.Estado = new SelectList(objDeudor.ListarEstado(objSession.Idioma), "Value", "Text", "");
            ViewBag.EstadoCartera = new SelectList(objComprobante.ListarEstadosCartera(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            return View();
        }

        [HttpPost]
        public ActionResult Cartera(EmailModel obj)
        {

            return Json("");
        }

        public ActionResult MailCocha(string ctr, string pag)
        {
            this.SettingAccount();
            if (string.IsNullOrEmpty(pag))
            {
                pag = "1";
            }
            ViewBag.Codemp = objSession.CodigoEmpresa;
            ViewBag.Reporte = new SelectList(bcp.Cartera.ListarReportes(Int32.Parse(pag), objSession.Idioma, "Seleccione"), "Value", "Text", "");
            Comprobante objComprobante = new Comprobante();
            Deudor objDeudor = new Deudor();
            ViewBag.TipoReporte = new SelectList(Dimol.Email.bcp.EnvioEmailCocha.ListarTipoReporte(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.Moneda = Email.bcp.EnvioEmailCocha.ListarMonedas(objSession.CodigoEmpresa);
            ViewBag.GrupoCobranza = new SelectList(Email.bcp.Vista.ListarGrupoCobranza(objSession.CodigoEmpresa, objSession.CodigoSucursal, "Seleccione"), "Value", "Text", "");
            ViewBag.Estado = new SelectList(objDeudor.ListarEstado(objSession.Idioma), "Value", "Text", "");
            ViewBag.EstadoCartera = new SelectList(objComprobante.ListarEstadosCartera(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            return View();
        }

        public ActionResult MailMutual(string ctr, string pag)
        {
            this.SettingAccount();
            if (string.IsNullOrEmpty(pag))
            {
                pag = "1";
            }
            ViewBag.Codemp = objSession.CodigoEmpresa;
            ViewBag.Reporte = new SelectList(bcp.Cartera.ListarReportes(Int32.Parse(pag), objSession.Idioma, "Seleccione"), "Value", "Text", "");
            Comprobante objComprobante = new Comprobante();
            Deudor objDeudor = new Deudor();
            ViewBag.TipoReporte = new SelectList(Dimol.Email.bcp.EnvioEmailCocha.ListarTipoReporte(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.Moneda = Email.bcp.EnvioEmailCocha.ListarMonedas(objSession.CodigoEmpresa);
            ViewBag.GrupoCobranza = new SelectList(Email.bcp.Vista.ListarGrupoCobranza(objSession.CodigoEmpresa, objSession.CodigoSucursal, "Seleccione"), "Value", "Text", "");
            ViewBag.Estado = new SelectList(objDeudor.ListarEstado(objSession.Idioma), "Value", "Text", "");
            ViewBag.EstadoCartera = new SelectList(objComprobante.ListarEstadosCartera(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            return View();
        }
        /*   [HttpPost]
           public ActionResult MailCocha(EmailCochaModel obj)
           {
               return Json("");
           }
           */
        public ActionResult GetDummy(GridSettings gridSettings)
        {
            // create json data 
            Dimol.dto.Combobox bcpDeudor = new Dimol.dto.Combobox();

            int totalRecords = 0;


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.dto.Combobox item in lst
                    select new
                    {
                        id = item.Value,
                        cell = new object[]
                        {
                            item.Value,
                            item.Text
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreCliente(string term)
        {
            Cliente objCliente = new Cliente();
            return Json(objCliente.ListarRutNombreCliente(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreDeudor(string term)
        {
            Deudor obj = new Deudor();
            return Json(obj.ListarRutNombreDeudor(term), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGestores(GridSettings gridSettings)
        {
            return GetGestor(gridSettings, 0, "0");
        }

        public JsonResult GetGestor(GridSettings gridSettings, int TipoCartera, string grupo)
        {
            // create json data 
            int grupoCobranza = 0;
            int gestor = 0;
            if (!string.IsNullOrEmpty(grupo))
            {
                grupoCobranza = Int32.Parse(grupo);
            }
            int totalRecords = Email.bcp.Vista.ListarGestoresCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, TipoCartera, objSession.Permisos > 3 ? 0 : objSession.Gestor, grupoCobranza, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.dto.Combobox> lst = Email.bcp.Vista.ListarGestores(objSession.CodigoEmpresa, objSession.CodigoSucursal, TipoCartera, objSession.Permisos > 3 ? 0 : objSession.Gestor, grupoCobranza, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.dto.Combobox item in lst
                    select new
                    {
                        id = item.Value,
                        cell = new object[]
                        {
                            item.Value,
                            item.Text
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGestorEmailMasivo(GridSettings gridSettings, int TipoCartera)
        {
            int totalRecords = Email.bcp.Vista.ListarGestoresEmailMasivoCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, TipoCartera, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.dto.Combobox> lst = Email.bcp.Vista.ListarGestoresEmailMasivo(objSession.CodigoEmpresa, objSession.CodigoSucursal, TipoCartera, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.dto.Combobox item in lst
                    select new
                    {
                        id = item.Value,
                        cell = new object[]
                        {
                            item.Value,
                            item.Text
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEstados(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = Email.bcp.Vista.ListarEstadosCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.dto.Combobox> lst = Email.bcp.Vista.ListarEstados(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.dto.Combobox item in lst
                    select new
                    {
                        id = item.Value,
                        cell = new object[]
                        {
                            item.Value,
                            item.Text
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEstadosCliente(GridSettings gridSettings, int Pclid)
        {

            // create json data 
            int totalRecords = Email.bcp.Vista.ListarEstadosClienteCount(objSession.CodigoEmpresa, Pclid, objSession.Idioma);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            var lst = Email.bcp.Vista.ListarEstadosCliente(objSession.CodigoEmpresa, Pclid, objSession.Idioma, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.dto.Combobox item in lst
                    select new
                    {
                        id = item.Value,
                        cell = new object[]
                        {
                            item.Value,
                            item.Text,
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EnviarEmail(EmailModel model)
        {
            string lstEstados = "", lstGestores = "";
            var objects = JArray.Parse(model.Estados); // parse as array  
            foreach (string root in objects)
            {
                lstEstados += root + ",";
            }
            objects = JArray.Parse(model.Gestores); // parse as array  
            foreach (string root in objects)
            {
                lstGestores += root + ",";
            }
            lstEstados = lstEstados.Substring(0, lstEstados.Length - 1);
            lstGestores = lstGestores.Substring(0, lstGestores.Length - 1);

            bool s = Email.bcp.EnvioEmail.Enviar(Int32.Parse(model.Pclid), lstEstados, Int32.Parse(model.TipoCartera), lstGestores, model.EmailTodos, model.EmailContacto, objSession, ConfigurationManager.AppSettings["RutaReportesEmail"], model.Email);
            return Json(s);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EnviarEmailCocha(EmailCochaModel model)
        {
            string[] lstDocs;
            bool s = false;
            List<string[]> Documentos = new List<string[]>();

            Dimol.Email.dto.DocumentoCocha docEmailCocha = new Email.dto.DocumentoCocha();

            docEmailCocha.Cliente = Dimol.Email.bcp.EnvioEmailCocha.TraeNombreCliente(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Pclid) ? "0" : model.Pclid));
            docEmailCocha.Deudor = Dimol.Email.bcp.EnvioEmailCocha.TraeRutDeudor(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid)) + " " + Dimol.Email.bcp.EnvioEmailCocha.TraeNombreDeudor(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid));
            //docEmailCocha.Numero = model.Numero;
            docEmailCocha.Cuenta = model.Cuenta;
            docEmailCocha.Banco = model.Banco;
            docEmailCocha.Saldo = model.Monto; //model.Saldo;
            docEmailCocha.SaldoDolar = model.SaldoDolar;
            docEmailCocha.FechaMail = DateTime.ParseExact(model.FechaMail, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var objects = JArray.Parse(model.Documentos); // parse as array  

            foreach (string root in objects)
            {
                Dimol.Email.dto.CochaCpbt doc = new Email.dto.CochaCpbt();
                lstDocs = root.Split('|');
                Documentos.Add(lstDocs);

                doc = Email.bcp.EnvioEmailCocha.TraeDocumento(lstDocs);

                if (doc.TipoMoneda == "CLP")
                {
                    docEmailCocha.DocPesos.Add(doc);
                }
                else if (doc.TipoMoneda == "DOLAR")
                {
                    docEmailCocha.DocDolares.Add(new Dimol.Email.dto.CochaCpbtDolar
                    {
                        Numero = doc.Numero,
                        TipoMoneda = doc.TipoMoneda,
                        Saldo = doc.Saldo,
                        CodigoCarga = doc.CodigoCarga
                    });
                }
                docEmailCocha.CodigoCarga = doc.CodigoCarga;
            }

            if (model.CheckNotaCredito)
            {
                docEmailCocha.DocPesos.Add(new Dimol.Email.dto.CochaCpbt
                {
                    Numero = "N/C",
                    TipoMoneda = "CLP",
                    Saldo = model.NotaCredito
                });
                docEmailCocha.Comentario = model.ComentarioMail;
            }

            //docEmailCocha.Moneda = docEmailCocha.DocPesos.Select(o => o.TipoMoneda).First();

            if (model.ValidaNotaCredito == true && model.MontoDolar == model.SaldoDolar)
            {
                s = Email.bcp.EnvioEmailCocha.Enviar(docEmailCocha, Int32.Parse(string.IsNullOrEmpty(model.Pclid) ? "0" : model.Pclid), Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid), objSession, Documentos, model.TipoReporte);
            }

            return Json(s);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EnviarEmailMutual(EmailMutualModel model)
        {
            string[] lstDocs;
            List<string[]> Documentos = new List<string[]>();

            Dimol.Email.dto.DocumentoCocha docEmailCocha = new Email.dto.DocumentoCocha();

            docEmailCocha.Cliente = Dimol.Email.bcp.EnvioEmailCocha.TraeNombreCliente(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Pclid) ? "0" : model.Pclid));
            docEmailCocha.Deudor = Dimol.Email.bcp.EnvioEmailCocha.TraeNombreDeudor(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid));
            docEmailCocha.RutDeudor = Dimol.Email.bcp.EnvioEmailCocha.TraeRutDeudor(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid));

            if (!string.IsNullOrEmpty(model.FechaMail))
            {
                docEmailCocha.FechaMail = DateTime.ParseExact(model.FechaMail, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                docEmailCocha.FechaMail = DateTime.Now;
            }

            DateTime fechaFin = new DateTime(docEmailCocha.FechaMail.Year, docEmailCocha.FechaMail.Month, 1);
            docEmailCocha.FechaMail = fechaFin.AddMonths(1).AddDays(-1);

            var objects = JArray.Parse(model.Documentos);

            foreach (string root in objects)
            {
                Dimol.Email.dto.CochaCpbt doc = new Email.dto.CochaCpbt();
                lstDocs = root.Split('|');
                Documentos.Add(lstDocs);

                doc = Email.bcp.EnvioEmailCocha.TraeDocumento(lstDocs);
                docEmailCocha.DocPesos.Add(doc);
                docEmailCocha.Saldo += doc.Saldo;

                docEmailCocha.Comentario += doc.Numero + ";";
            }

            docEmailCocha.Comentario = "Se informa deuda a los siguientes destinatarios: " + model.Email + ", facturas Nº " + docEmailCocha.Comentario;

            bool s = Email.bcp.EnvioEmailMutual.Enviar(Int32.Parse(string.IsNullOrEmpty(model.Pclid) ? "0" : model.Pclid), Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid), objSession, ConfigurationManager.AppSettings["RutaReportesEmail"], model.Email, docEmailCocha, Documentos, model.TipoReporte);

            return Json(!s);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EnviarEmailMutualPagos(EmailMutualPagosModel model)
        {
            string[] lstDocs;
            string comentarioHistorial = "";
            bool s = false;
            DateTime fechaActual = DateTime.Now;
            List<string[]> Documentos = new List<string[]>();

            Dimol.Email.dto.DocumentoMutualPagos docEmailMutualPagos = new Email.dto.DocumentoMutualPagos();

            docEmailMutualPagos.Cliente = Dimol.Email.bcp.EnvioEmailCocha.TraeNombreCliente(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Pclid) ? "0" : model.Pclid));
            docEmailMutualPagos.Deudor = Dimol.Email.bcp.EnvioEmailCocha.TraeNombreDeudor(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid));
            docEmailMutualPagos.RutDeudor = Dimol.Email.bcp.EnvioEmailCocha.TraeRutDeudor(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid));

            docEmailMutualPagos.Saldo = model.Saldo;
            docEmailMutualPagos.FechaMail = DateTime.ParseExact(model.FechaMail, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            docEmailMutualPagos.Comentario = model.ComentarioMail;

            var objects = JArray.Parse(model.Documentos); // parse as array  

            foreach (string root in objects)
            {
                Dimol.Email.dto.CochaCpbt doc = new Email.dto.CochaCpbt();
                lstDocs = root.Split('|');
                Documentos.Add(lstDocs);

                doc = Email.bcp.EnvioEmailCocha.TraeDocumento(lstDocs);

                Dimol.Email.bcp.EnvioEmailMutualPagos.InsertarBajasCpbtDoc(Int32.Parse(lstDocs[0]), Int32.Parse(lstDocs[1]), Int32.Parse(lstDocs[2]), doc, docEmailMutualPagos.FechaMail, objSession.UserId, Int32.Parse(model.Cuenta), Int32.Parse(model.Banco), model.ComentarioMail, Int32.Parse(model.Gestion), fechaActual);

                docEmailMutualPagos.DocPesos.Add(doc);

                comentarioHistorial += doc.Numero + ",";
            }

            //docEmailMutualPagos.Numero = model.Numero; //Email Destinatario
            docEmailMutualPagos.Cuenta = model.Cuenta;
            docEmailMutualPagos.Banco = model.Banco;

            Dimol.Email.bcp.EnvioEmailMutualPagos.TraeCuentaBancoEjecutivos(docEmailMutualPagos);

            comentarioHistorial = comentarioHistorial.Substring(0, comentarioHistorial.Length - 1);

            comentarioHistorial = "Se envía respaldo de pagos asociados a las facturas N°: " + comentarioHistorial + ", por el monto de " + docEmailMutualPagos.SaldoStr + " a " + docEmailMutualPagos.Numero;

            foreach (var item in Documentos)
            {
                Dimol.Email.bcp.EnvioEmailMutualPagos.ActualizarHistorialBajasCpbtDoc(Int32.Parse(item[0]), Int32.Parse(item[1]), Int32.Parse(item[2]), comentarioHistorial, objSession.UserId);
            }

            if (model.Monto == model.Saldo)
            {
                s = Email.bcp.EnvioEmailMutualPagos.Enviar(Int32.Parse(string.IsNullOrEmpty(model.Pclid) ? "0" : model.Pclid), Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid), objSession, docEmailMutualPagos, Documentos, model.TipoReporte, comentarioHistorial, model.Gestion);
            }

            return Json(s);
        }

        public ActionResult GetDeudoresMailMutualCpbt(GridSettings gridSettings, EmailCochaModel model)
        {
            // create json data 
            Dimol.Carteras.bcp.Deudor bcpDeudor = new Dimol.Carteras.bcp.Deudor();

            int totalRecords = bcpDeudor.ListarDeudoresMailCochaCpbtCount(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.Carteras.dto.DeudorDocumento> lst = bcpDeudor.ListarDeudoresMailCochaCpbt(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EnviarEmailCoopeuch(EmailCoopeuchModel model)
        {
            string[] lstDocs;
            List<string[]> Documentos = new List<string[]>();

            Dimol.Email.dto.DocumentoCocha docEmailCocha = new Email.dto.DocumentoCocha();

            docEmailCocha.Cliente = Dimol.Email.bcp.EnvioEmailCocha.TraeNombreCliente(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.PclidCoopeuch) ? "0" : model.PclidCoopeuch));
            docEmailCocha.Deudor = Dimol.Email.bcp.EnvioEmailCocha.TraeNombreDeudor(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid));
            docEmailCocha.RutDeudor = Dimol.Email.bcp.EnvioEmailCocha.TraeRutDeudor(objSession.CodigoEmpresa, Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid));
            docEmailCocha.FechaMail = DateTime.Now;

            if (model.TipoReporte == "4")
            {
                docEmailCocha.Comentario = "Se envia correo a socio informando morosidad.";
            }
            else
            {
                docEmailCocha.Comentario = "Se envia correo a socio informando vencimiento de cuota.";
            }

            bool s = Email.bcp.EnvioEmailCoopeuch.Enviar(Int32.Parse(string.IsNullOrEmpty(model.PclidCoopeuch) ? "0" : model.PclidCoopeuch), Int32.Parse(string.IsNullOrEmpty(model.Ctcid) ? "0" : model.Ctcid), objSession, ConfigurationManager.AppSettings["RutaReportesEmail"], model.Email, docEmailCocha, model.TipoReporte);

            return Json(s);
        }

        //public ActionResult SendEmailTemplate(int pclid, string filename)
        public async Task<ActionResult> SendMassiveEmail(EmailMasivoModel Model)
        {
            var PathReportes = ConfigurationManager.AppSettings["RutaReportesEmail"];
            var TestResult = await Email.bcp.EnvioEmail.EnvioMasivo(new BuscarCarteraMasivaModel()
            {
                Estados = FormatEstados(Model.Estados),
                Ctcid = Model.Ctcid != null ? int.Parse(Model.Ctcid) : 0,
                Codemp = objSession.CodigoEmpresa,
                Gestores = FormatEstados(Model.Gestores),
                Pclid = Model.Pclid != null ? int.Parse(Model.Pclid) : 0,
                TipoCartera = Model.TipoCartera,
                FechaVencimiento = Model.FechaVencimiento,
                FechaTipo = Model.FechaOperador,
                Liquidacion = Model.Liquidacion,
                LiquidacionTipo = Model.TipoLiquidacion,
                MontoDesde = Model.MontoDesde,
                MontoHasta = Model.MontoHasta,
                Template = !String.IsNullOrEmpty(Model.Template) ? Model.Template : "GENERAL",
                Path = PathReportes
            }, objSession, Model.Test);
            //var Result = _emailService.Send(int.Parse(Model.Pclid), Model.Template);
            return Json("Se enviaron: " + TestResult + " emails", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApplyEmailFilters(GridSettings gridSettings, EmailMasivoModel Model)
        {
            var Listado = Email.bcp.EnvioEmail.ListarCarteraEmailMasivo(new BuscarCarteraMasivaModel()
            {
                Estados = FormatEstados(Model.Estados),
                Gestores = FormatEstados(Model.Gestores),
                Ctcid = Model.Ctcid != null ? int.Parse(Model.Ctcid) : 0,
                Codemp = 1,
                Pclid = Model.Pclid != null ? int.Parse(Model.Pclid) : 0,
                TipoCartera = Model.TipoCartera,
                FechaVencimiento = Model.FechaVencimiento,
                FechaTipo = Model.FechaOperador,
                Liquidacion = Model.Liquidacion,
                LiquidacionTipo = Model.TipoLiquidacion,
                MontoDesde = Model.MontoDesde,
                MontoHasta = Model.MontoHasta
            }, objSession);

            // create json data 
            int totalRecords = Listado.Count;
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
                    from EmailBody item in Listado
                    select new
                    {
                        id = item.Ctcid,
                        cell = new object[]
                        {
                            item.Ctcid,
                            item.Rut,
                            item.NombreDeudor,
                            item.Contactos,
                            item.DatosGestor.Nombre
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        private static string FormatEstados(string Estados)
        {
            string lstEstados = "";
            var objects = JArray.Parse(Estados); // parse as array  
            foreach (string root in objects)
            {
                lstEstados += root + ",";
            }
            if (objects.Count > 0)
                lstEstados = lstEstados.Substring(0, lstEstados.Length - 1);
            return lstEstados;
        }

        public ActionResult PreviewTemplate(int pclid, string filename)
        {
            var Result = _emailService.Render(pclid, filename);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MailMasivo(string ctr, string pag)
        {
            this.SettingAccount();
            if (string.IsNullOrEmpty(pag))
            {
                pag = "1";
            }
            ViewBag.Codemp = objSession.CodigoEmpresa;
            Comprobante objComprobante = new Comprobante();
            ViewBag.FechaTipos = new SelectList(new List<SelectListItem>()
            {
                new SelectListItem{ Value = "1", Text = "Hasta", Selected = true },
                new SelectListItem{ Value = "2", Text = "Sin vencer", Selected = false },
                new SelectListItem{ Value = "3", Text = "Vencidas", Selected = false}
            }, "Value", "Text");
            ViewBag.TipoCartera = new SelectList(objComprobante.ListarTipoCartera(objSession.Idioma), "Value", "Text", "3");
            //ViewBag.Reporte = new SelectList(bcp.Cartera.ListarReportes(Int32.Parse(pag), objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Templates = new SelectList(_emailService.ListTemplates());
            //ViewBag.TipoReporte = new SelectList(Dimol.Email.bcp.EnvioEmailCocha.ListarTipoReporte(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            return View();
        }


        [HttpGet]
        public ActionResult ListarEmailCliente(int id)
        {
            return Json(_emailService.ListarEmailTemplatesCliente(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarLiquidacionesEnvioMasivo()
        {
            return Json(Email.bcp.EnvioEmail.GetLiquidaciones(), JsonRequestBehavior.AllowGet);
        }
    }
}
