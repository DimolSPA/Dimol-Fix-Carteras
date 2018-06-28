using Dimol.bcp;
using Dimol.Carteras.bcp;
using Dimol.dto;
using Dimol.Judicial.Mantenedores.bcp;
using Dimol.Judicial.Mantenedores.Models;
using Dimol.Judicial.Mantenedores.Models.Globals;
using Mvc.HtmlHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dimol.Judicial.Mantenedores.Controllers
{
    public class JudicialController : Dimol.Controllers.BaseController
    {
        #region "Rol"
        public ActionResult Rol(string idd, string tipoComp)
        {
            #region VALIDACIONES DE SESIÓN Y PARÁMETROS DE URL
            //Validación de la sesión del usuario
            if (!SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            //Validación del tipo de competencia
            if (idd == null || idd == "")
            {
                if (tipoComp == null || tipoComp == "" || (tipoComp != "C" && tipoComp != "P"))
                {
                    return RedirectToAction("Rol", "Judicial", new { tipoComp = "C" });
                }
            }
            #endregion

            ViewBag.Permiso = objSession.Permisos;
            ViewBag.EstadoHistorial = new SelectList(bcp.MateriaEstado.ListarEstadosCombo(objSession.CodigoEmpresa, objSession.Idioma, 0), "Value", "Text", "");
            ViewBag.MateriaHistorial = new SelectList(bcp.MateriaEstado.ListarMateriasCombo(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            ViewBag.EnteJudicial = new SelectList(bcp.EnteJudicial.ListarEntes(objSession.CodigoEmpresa, "Seleccione Ente"), "Value", "Text", "");
            var AreaBorrador = "JUD";
            int idCompetencia = Competencia.obtenerCompetenciaIdPorTipoCompetencia(tipoComp);

            #region LISTA TIPO DE ROL
            List<Combobox> lstTipo = ComboTipoRol.CargarListaTipoRol(tipoComp);

            switch (tipoComp)
            {
                case "P":
                    ViewBag.Title = "Rol Previsionales";
                    ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", "P"); //Combo de TipoRol para roles previsionales
                    ViewBag.UrlBuscarListaClientes = Url.Action("BuscarRutNombreCliente", new { TipoCliente = "P" });
                    ViewBag.EsPrevisional = true;
                    AreaBorrador = "PRE";
                    break;
                default:
                    ViewBag.Title = "Rol Civiles";
                    ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", "C"); //Combo de TipoRol para roles civiles
                    ViewBag.UrlBuscarListaClientes = Url.Action("BuscarRutNombreCliente", new { TipoCliente = "C" });
                    ViewBag.EsPrevisional = false;
                    AreaBorrador = "JUD";
                    break;
            }
            #endregion

            ViewBag.Borradores = new SelectList(Dimol.CKEditor.bcp.Documento.ListarDocumentos(objSession.CodigoEmpresa, AreaBorrador, "Seleccione Borrador"), "Value", "Text", "");

            List<Combobox> lstQuiebra = new List<Combobox>();
            lstQuiebra.Add(new Combobox { Text = "No", Value = "N" });
            lstQuiebra.Add(new Combobox { Text = "En Proceso", Value = "P" });
            lstQuiebra.Add(new Combobox { Text = "Si", Value = "S" });
            ViewBag.ComboQuiebra = new SelectList(lstQuiebra, "Value", "Text", "N");

            ViewBag.Del = false;
            if (objSession.PrfId == 6 || objSession.PrfId == 9 || objSession.PrfId == 10 || objSession.PrfId == 13 || objSession.PrfId == 1)
            {
                ViewBag.Del = true;
            }

            ViewBag.Prf = objSession.PrfId;

            int rolid = 0;
            dto.RolCarga obj = new dto.RolCarga();
            RolModel model = new RolModel();
            if (Int32.TryParse(idd, out rolid))
            {
                obj = bcp.Rol.BuscarRol(objSession.CodigoEmpresa, rolid, objSession.Idioma);

                //Redirecciona para corregir parámetro tipoComp
                string tipoCompActual = Competencia.ParametroCompetenciaPorIdCompetencia(obj.IdCompetencia);
                if (tipoCompActual != tipoComp)
                {
                    return RedirectToAction("Rol", "Judicial", new { idd, tipoComp = tipoCompActual });
                }

                bcp.Rol.BuscarRolDemandaAvenimiento(objSession.CodigoEmpresa, rolid, obj);
                model.BloquearRol = obj.BloquearRol;
                model.Comentario = obj.Comentario;
                model.NombreRutDeudor = obj.RutDeudor + " - " + obj.NombreDeudor;
                model.NombreRutCliente = obj.RutCliente + " - " + obj.Cliente;
                model.TipoRol = obj.TipoRol;
                ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", obj.TipoRol ?? "C");
                model.Rol = obj.Rol;
                ViewBag.TipoCausa = new SelectList(bcp.Rol.ListarTiposCausa(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", obj.IdTipoCausa);
                ViewBag.Tribunal = new SelectList(bcp.Rol.ListarTribunales(objSession.CodigoEmpresa, idCompetencia, "Seleccione"), "Value", "Text", obj.IdTribunal);
                model.FechaIngreso = obj.FechaIngreso == new DateTime() ? "" : obj.FechaIngreso.ToShortDateString();
                model.FechaRol = obj.FechaRol == new DateTime() ? "" : obj.FechaRol.ToShortDateString();
                model.FechaDemanda = obj.FechaDemanda == new DateTime() ? "" : obj.FechaDemanda.ToShortDateString();
                model.MateriaJudicial = obj.MateriaJudicial;
                model.Estado = obj.Estado;
                model.ProcesoQuiebra = obj.ProcesoQuiebra;
                model.Rolid = obj.Rolid;
                model.Quiebra = obj.Quiebra;
                model.Pclid = obj.Pclid;
                model.Ctcid = obj.Ctcid;
                //Avenimiento 
                model.FechaAvenimiento = obj.Avenimiento.Fecha == new DateTime() ? "" : obj.Avenimiento.Fecha.ToShortDateString();
                model.MontoAvenimiento = obj.Avenimiento.Monto.ToString("N2");
                model.CuotasAvenimiento = obj.Avenimiento.Cuotas;
                model.MontoCuotaAvenimiento = obj.Avenimiento.MontoCuota.ToString("N2");
                model.MontoUltimaCuotaAvenimiento = obj.Avenimiento.MontoUltimaCuota.ToString("N2");
                model.FechaPrimeraCuotaAvenimiento = obj.Avenimiento.FechaPrimeraCuota == new DateTime() ? "" : obj.Avenimiento.FechaPrimeraCuota.ToShortDateString();
                model.FechaUltimaCuotaAvenimiento = obj.Avenimiento.FechaUltimaCuota == new DateTime() ? "" : obj.Avenimiento.FechaUltimaCuota.ToShortDateString();
                model.InteresAvenimiento = obj.Avenimiento.Interes.ToString("N2");
                //Demanda
                model.FechaDemandaAve = obj.Demanda.Fecha == new DateTime() ? "" : obj.Demanda.Fecha.ToShortDateString();
                model.MontoDemanda = obj.Demanda.Monto.ToString("N2");
                model.CuotasDemanda = obj.Demanda.Cuotas;
                model.MontoCuotaDemanda = obj.Demanda.MontoCuota.ToString("N2");
                model.MontoUltimaCuotaDemanda = obj.Demanda.MontoUltimaCuota.ToString("N2");
                model.FechaPrimeraCuotaDemanda = obj.Demanda.FechaPrimeraCuota == new DateTime() ? "" : obj.Demanda.FechaPrimeraCuota.ToShortDateString();
                model.FechaUltimaCuotaDemanda = obj.Demanda.FechaUltimaCuota == new DateTime() ? "" : obj.Demanda.FechaUltimaCuota.ToShortDateString();
                model.InteresDemanda = obj.Demanda.Interes.ToString("N2");
                if (model.ProcesoQuiebra)
                {
                    ViewBag.ComboQuiebra = new SelectList(lstQuiebra, "Value", "Text", "P");
                }
                else if (model.Quiebra)
                {
                    ViewBag.ComboQuiebra = new SelectList(lstQuiebra, "Value", "Text", "S");
                }
                else
                {
                    ViewBag.ComboQuiebra = new SelectList(lstQuiebra, "Value", "Text", "N");
                }

                List<dto.DocumentoRol> lstDocsAsig = bcp.Rol.ListarDocumentosAsignadosGrilla(objSession.CodigoEmpresa, objSession.Idioma, model.Rolid, "", "Tipo", "asc", 0, 1111111);
                decimal montoAsig = lstDocsAsig.Sum(x => x.Monto);
                decimal saldoAsig = lstDocsAsig.Sum(x => x.Saldo);

                List<dto.DocumentoRol> lstDocsPorAsig = bcp.Rol.ListarDocumentosPorAsignarGrilla(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, "", "Tipo", "asc", 0, 1111111);
                decimal montoPorAsig = lstDocsPorAsig.Sum(x => x.Monto);
                decimal saldoPorAsig = lstDocsPorAsig.Sum(x => x.Saldo);

                model.TotalMontoAsignado = montoAsig.ToString("N2");
                model.TotalSaldoAsignado = saldoAsig.ToString("N2");
                model.TotalMontoPorAsignar = montoPorAsig.ToString("N2");
                model.TotalSaldoPorAsignar = saldoPorAsig.ToString("N2");

                model.Sindico = bcp.PanelQuiebra.getPanelQuiebraSindico(rolid);
                model.Veedor = bcp.PanelQuiebra.getPanelQuiebraVeedor(rolid);
                model.Interventor = bcp.PanelQuiebra.getPanelQuiebraInterventor(rolid);

                model.ActualizarRolPoderJudicial = bcp.Rol.TraeActualizarPoderJudicial(objSession.CodigoEmpresa, rolid);
                model.EstAdm = bcp.Rol.TraeEstAdmPoderJudicial(rolid);

                if (obj.IdMateriaJudicial == 3 || obj.IdMateriaJudicial == 9 || obj.IdMateriaJudicial == 10)
                {
                    ViewBag.IsLiquidacionRol = -1;
                }
                else
                {
                    ViewBag.IsLiquidacionRol = 1;
                }

                return View(model);
            }
            else
            {
                ViewBag.TipoCausa = new SelectList(bcp.Rol.ListarTiposCausa(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
                ViewBag.Tribunal = new SelectList(bcp.Rol.ListarTribunales(objSession.CodigoEmpresa, idCompetencia, "Seleccione"), "Value", "Text", "");
                ViewBag.Rolid = 0;
                ViewBag.IsLiquidacionRol = 1;
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Upload(string tipo, string rut, string Ctcid, string Pclid, string TipoDocumento, string Rolid, string FecJud)
        {
            string fileName = "";
            string path = "";
            string ext = "";
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
                    System.IO.Stream fileContent = file.InputStream;
                    switch (tipo)
                    {
                        case "Estampe":
                            ////id = 15;
                            fileName = "estampe_" + objSession.CodigoEmpresa + "_" + objSession.UserId + "_" + Pclid + "_" + Ctcid + "_" + Rolid + "_";
                            //path = ConfigurationManager.AppSettings["RutaArchivosEstampes"] + Pclid + "\\\\" + Ctcid + "\\\\" + Rolid + "\\\\";
                            path = ConfigurationManager.AppSettings["RutaArchivos"] + "Documentos\\Estampes\\" + Pclid + "\\" + Ctcid + "\\" + Rolid + "\\";
                            ext = Path.GetExtension(file.FileName).ToLower();
                            //Use the following properties to get file's name, size and MIMEType
                            ////fileName = fileName + file.FileName;
                            if (ext != ".pdf" && ext != ".odf" && ext != ".doc" && ext != ".docx")
                            {
                                return Json(-1);
                            }

                            id = bcp.Rol.GuardarDocumentoEstampe(objSession.CodigoEmpresa, Int32.Parse(Pclid), Int32.Parse(Ctcid), Int32.Parse(Rolid), path, fileName, ext, FecJud, objSession.UserId);
                            if (id > 0)
                            {
                                objFunc.CreaCarpetas(path);
                                fileName = fileName + id + ext;
                                //To save file, use SaveAs method
                                file.SaveAs(path + fileName); //File will be saved in application root

                            }
                            else
                            {
                                return Json("");
                            }
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.JudicialController.Carga.Upload", 0);
                fileName = "";
            }

            return Json(fileName);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarRol(RolModel model)
        {
            int rolid = 0;
            string quiebraDeudor = "N";

            dto.Rol rol = new dto.Rol();

            if (model.ComboQuiebra == "P")
            {
                rol.rol_prequiebra = "S";
                quiebraDeudor = "N";
            }
            else if (model.ComboQuiebra == "S")
            {
                rol.rol_prequiebra = "N";
                quiebraDeudor = "S";
            }
            else
            {
                rol.rol_prequiebra = "N";
                quiebraDeudor = "N";
            }

            if (model.Rolid == 0)
            {
                rol.rol_tipo_rol = model.TipoRol;
                rol.rol_numero = model.Rol;
                rol.rol_trbid = Int32.Parse(model.Tribunal);
                rol.rol_tcaid = Int32.Parse(model.TipoCausa);
                rol.rol_comentario = model.Comentario;
                rol.rol_fecrol = model.FechaRol;
                rol.rol_pclid = model.Pclid;
                rol.rol_ctcid = model.Ctcid;
                rol.rol_bloqueo = model.BloquearRol ? "S" : "N";

                rolid = bcp.Rol.InsertarRol(rol, objSession);

                if (rolid > 0)
                {
                    bcp.Rol.ActualizarParaPoderJudicial(objSession.CodigoEmpresa, rolid, model.ActualizarRolPoderJudicial);
                    bcp.Rol.ActualizarParaPoderJudicialHistorial(objSession.CodigoEmpresa, rolid, model.ActualizarRolPoderJudicial, objSession.UserId);
                }
                bcp.Rol.ActualilzarQuiebraDeudor(objSession.CodigoEmpresa, model.Ctcid, quiebraDeudor);
            }
            else
            {
                rol.rol_rolid = model.Rolid;
                rol.rol_tipo_rol = model.TipoRol;
                rol.rol_numero = model.Rol;
                rol.rol_trbid = Int32.Parse(model.Tribunal);
                rol.rol_tcaid = Int32.Parse(model.TipoCausa);
                rol.rol_fecdem = model.FechaDemanda;
                rol.rol_fecrol = model.FechaRol;
                rol.rol_comentario = model.Comentario;
                rol.rol_fecrol = model.FechaRol;
                rol.rol_pclid = model.Pclid;
                rol.rol_ctcid = model.Ctcid;
                rol.rol_bloqueo = model.BloquearRol ? "S" : "N";

                rolid = bcp.Rol.EditarRol(rol, objSession);
                bcp.Rol.ActualilzarQuiebraDeudor(objSession.CodigoEmpresa, rol.rol_ctcid, quiebraDeudor);
                bcp.Rol.ActualizarParaPoderJudicial(objSession.CodigoEmpresa, model.Rolid, model.ActualizarRolPoderJudicial);
                bcp.Rol.ActualizarParaPoderJudicialHistorial(objSession.CodigoEmpresa, model.Rolid, model.ActualizarRolPoderJudicial, objSession.UserId);

                if (rolid > 0)
                {
                    rolid = rol.rol_rolid;
                }
            }

            dto.DemandaAvenimiento demanda = new dto.DemandaAvenimiento();
            dto.DemandaAvenimiento avenimiento = new dto.DemandaAvenimiento();

            //Avenimiento 
            avenimiento.Fecha = string.IsNullOrEmpty(model.FechaAvenimiento) ? new DateTime() : DateTime.Parse(model.FechaAvenimiento);
            avenimiento.Monto = !string.IsNullOrEmpty(model.MontoAvenimiento) ? decimal.Parse(model.MontoAvenimiento, NumberStyles.Currency, CultureInfo.CurrentCulture) : 0;
            avenimiento.Cuotas = model.CuotasAvenimiento;
            avenimiento.MontoCuota = !string.IsNullOrEmpty(model.MontoAvenimiento) ? decimal.Parse(model.MontoCuotaAvenimiento, NumberStyles.Currency, CultureInfo.CurrentCulture) : 0;
            avenimiento.MontoUltimaCuota = !string.IsNullOrEmpty(model.MontoAvenimiento) ? decimal.Parse(model.MontoUltimaCuotaAvenimiento, NumberStyles.Currency, CultureInfo.CurrentCulture) : 0;
            avenimiento.FechaPrimeraCuota = string.IsNullOrEmpty(model.FechaPrimeraCuotaAvenimiento) ? new DateTime() : DateTime.Parse(model.FechaPrimeraCuotaAvenimiento);
            avenimiento.FechaUltimaCuota = string.IsNullOrEmpty(model.FechaUltimaCuotaAvenimiento) ? new DateTime() : DateTime.Parse(model.FechaUltimaCuotaAvenimiento);
            avenimiento.Interes = !string.IsNullOrEmpty(model.MontoAvenimiento) ? decimal.Parse(model.InteresAvenimiento, NumberStyles.Currency, CultureInfo.CurrentCulture) : 0;

            //Demanda
            demanda.Fecha = string.IsNullOrEmpty(model.FechaDemandaAve) ? new DateTime() : DateTime.Parse(model.FechaDemandaAve);
            demanda.Monto = !string.IsNullOrEmpty(model.MontoDemanda) ? decimal.Parse(model.MontoDemanda, NumberStyles.Currency, CultureInfo.CurrentCulture) : 0;
            demanda.Cuotas = model.CuotasDemanda;
            demanda.MontoCuota = !string.IsNullOrEmpty(model.MontoCuotaDemanda) ? decimal.Parse(model.MontoCuotaDemanda, NumberStyles.Currency, CultureInfo.CurrentCulture) : 0;
            demanda.MontoUltimaCuota = !string.IsNullOrEmpty(model.MontoUltimaCuotaDemanda) ? decimal.Parse(model.MontoUltimaCuotaDemanda, NumberStyles.Currency, CultureInfo.CurrentCulture) : 0;
            demanda.FechaPrimeraCuota = string.IsNullOrEmpty(model.FechaPrimeraCuotaDemanda) ? new DateTime() : DateTime.Parse(model.FechaPrimeraCuotaDemanda);
            demanda.FechaUltimaCuota = string.IsNullOrEmpty(model.FechaUltimaCuotaDemanda) ? new DateTime() : DateTime.Parse(model.FechaUltimaCuotaDemanda);
            demanda.Interes = !string.IsNullOrEmpty(model.InteresDemanda) ? decimal.Parse(model.InteresDemanda, NumberStyles.Currency, CultureInfo.CurrentCulture) : 0;

            bcp.Rol.GrabarMontosDemanda(objSession.CodigoEmpresa, rolid, demanda, avenimiento);

            return Json(rolid);
        }
        #endregion

        public ActionResult BuscarRol(string tipoComp)
        {
            Rol objComprobante = new Rol();

            //Validación de la sesión de usuario
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            //Validación del parámetro tipo de competencia
            if (tipoComp == null || tipoComp == "" || (tipoComp != "C" && tipoComp != "P"))
            {
                return RedirectToAction("BuscarRol", "Judicial", new { tipoComp = "C" });
            }

            switch (tipoComp)
            {
                case "P":
                    ViewBag.Title = "Buscar Rol Previsional";
                    ViewBag.UrlBuscarListaClientes = Url.Action("BuscarRutNombreCliente", new { TipoCliente = "P" });
                    break;
                default:
                    ViewBag.Title = "Buscar Rol Civil";
                    ViewBag.UrlBuscarListaClientes = Url.Action("BuscarRutNombreCliente", new { TipoCliente = "C" });
                    break;
            }

            int idCompetencia = Competencia.obtenerCompetenciaIdPorTipoCompetencia(tipoComp);
            ViewBag.TipoCausa = new SelectList(bcp.Rol.ListarTiposCausa(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Tribunal = new SelectList(bcp.Rol.ListarTribunales(objSession.CodigoEmpresa, idCompetencia, "Seleccione"), "Value", "Text", "");
            ViewBag.Cliente = new SelectList(bcp.Rol.ListarClientes(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");

            return View();
        }

        #region "BuscarRol"
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetRoles(GridSettings gridSettings, string tipoComp, string cliente, string rol, string tribunal, string tipoCausa, string rutDeudor, string nombreDeudor, int? Ctcid, int? Pclid)
        {
            //Create json data 
            bcp.Rol bcp = new Rol();
            int _cliente = 0;
            int _tribunal = 0;
            int _tipoCausa = 0;
            int pclid = Pclid == null ? 0 : (int)Pclid;
            int ctcid = Ctcid == null ? 0 : (int)Ctcid;

            if (pclid != 0)
            {
                _cliente = pclid;
            }

            if (tribunal != null && tribunal != "")
            {
                _tribunal = Int32.Parse(tribunal.ToString());
            }

            if (tipoCausa != null && tipoCausa != "")
            {
                _tipoCausa = Int32.Parse(tipoCausa.ToString());
            }

            int idCompetencia = Competencia.obtenerCompetenciaIdPorTipoCompetencia(tipoComp);

            int totalRecords = bcp.ListarRolesGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, idCompetencia, rol, _tribunal, _tipoCausa, _cliente, rutDeudor, nombreDeudor, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Rol> lst = bcp.ListarRolesGrilla(objSession.CodigoEmpresa, objSession.Idioma, idCompetencia, rol, _tribunal, _tipoCausa, _cliente, rutDeudor, nombreDeudor, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

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
                        id = item.rol_rolid,
                        cell = new object[]
                        {
                            item.rol_rolid,
                            item.rol_numero,
                            item.trb_nombre,
                            item.pcl_nomfant,
                            item.ctc_rut,
                            item.ctc_nomfant

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Materia judicial

        public ActionResult MateriaJudicial()
        {
            //ActivarSession();
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "MateriaJudicial";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMateriaJudicial(GridSettings gridSettings)
        {
            int totalRecords = bcp.MateriaJudicial.ListarMateriaJudicialGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.MateriaJudicial bcpMateriaJudicial = new MateriaJudicial();
            List<dto.MateriaJudicial> lstMateriaJudicial = bcpMateriaJudicial.ListarMateriaJudicialGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);




            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MateriaJudicial item in lstMateriaJudicial
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Id,
                            item.Nombre,
                            item.Orden
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperMateriaJudicial(bcp.MateriaJudicial model, string oper, int? id)
        {
            model.OperMateriaJudicial(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelMateriaJudicial(GridSettings gridSettings)
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

            bcp.MateriaJudicial objMateriaJudicial = new MateriaJudicial();

            var grid = new GridView();
            grid.DataSource = objMateriaJudicial.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Materia Estado

        public ActionResult MateriaEstado()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "MateriaEstado";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Estado = bcp.MateriaEstado.ListarEstados(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.Materia = bcp.MateriaEstado.ListarMaterias(objSession.CodigoEmpresa, objSession.Idioma);
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMateriaEstado(GridSettings gridSettings)
        {
            int totalRecords = bcp.MateriaEstado.ListarMateriaEstadoGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.MateriaEstado bcpMateriaEstado = new MateriaEstado();
            List<dto.MateriaEstado> lstMateriaEstado = bcpMateriaEstado.ListarMateriaEstadoGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);




            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MateriaEstado item in lstMateriaEstado
                    select new
                    {
                        id = item.IdEstado + "|" + item.IdMateria,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.IdEstado,
                            item.NombreEstado,
                            item.IdMateria,
                            item.NombreMateria
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperMateriaEstado(bcp.MateriaEstado model, string oper, string id)
        {
            model.OperMateriaEstado(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelMateriaEstado(GridSettings gridSettings)
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

            bcp.MateriaEstado objAccion = new MateriaEstado();


            var grid = new GridView();
            grid.DataSource = objAccion.ExportarExcel(1, 1, gridSettings.where.groupOp, "nombreestado,nombremateria", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Tipos Causas

        public ActionResult TiposCausa()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "TiposCausa";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTiposCausa(GridSettings gridSettings)
        {
            int totalRecords = bcp.TiposCausa.ListarTiposCausaGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.TiposCausa bcpTiposCausa = new TiposCausa();
            List<dto.TiposCausa> lstTiposCausa = bcpTiposCausa.ListarTiposCausaGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);




            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TiposCausa item in lstTiposCausa
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Id,
                            item.Nombre,

                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTiposCausa(bcp.TiposCausa model, string oper, int? id)
        {
            model.OperTiposCausa(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelTiposCausa(GridSettings gridSettings)
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

            bcp.TiposCausa objAccion = new TiposCausa();


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

        #region Tipos Tribunal

        public ActionResult TiposTribunal()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "TiposTribunal";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTiposTribunal(GridSettings gridSettings)
        {
            int totalRecords = bcp.TiposTribunal.ListarTiposTribunalGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.TiposTribunal bcpTiposTribunal = new TiposTribunal();
            List<dto.TiposTribunal> lstTiposTribunal = bcpTiposTribunal.ListarTiposTribunalGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);




            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TiposTribunal item in lstTiposTribunal
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Id,
                            item.Nombre,

                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTiposTribunal(bcp.TiposTribunal model, string oper, int? id)
        {
            model.OperTiposTribunal(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelTiposTribunal(GridSettings gridSettings)
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

            bcp.TiposTribunal objAccion = new TiposTribunal();


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

        #region Mandatos

        public ActionResult Mandatos()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Mandatos";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Notarias = bcp.Mandatos.ListarNotarias(objSession.CodigoEmpresa);
            ViewBag.Clientes = bcp.Mandatos.ListarClientes(objSession.CodigoEmpresa);
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMandatos(GridSettings gridSettings)
        {
            int totalRecords = bcp.Mandatos.ListarMandatosGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.Mandatos bcpMandatos = new Mandatos();
            List<dto.Mandatos> lstMandatos = bcpMandatos.ListarMandatosGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);




            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Mandatos item in lstMandatos
                    select new
                    {

                        id = item.IdCliente + "|" + item.IdNotaria + "|" + item.NumeroRepertorio,
                        cell = new object[]
                        {
                           item.Codemp,
                           item.IdCliente,
                           item.IdNotaria ,
                           item.NombreNotaria ,
                           item.NumeroRepertorio,
                           item.RutCliente,
                           item.NombreCliente,
                           item.FechaAsignacion ,
                           item.FechaVencimiento ,
                           item.Indefinido
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperMandatos(bcp.Mandatos model, string oper, string id)
        {
            model.OperMandatos(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelMandatos(GridSettings gridSettings)
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

            bcp.Mandatos objMandatos = new Mandatos();


            var grid = new GridView();
            grid.DataSource = objMandatos.ExportarExcel(1, 1, gridSettings.where.groupOp, "NombreCliente,NombreNotaria  ", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Tribunal

        public ActionResult Tribunal()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Tribunal";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.TiposTribunal = bcp.Tribunal.ListarTiposTribunal(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.Bancos = bcp.Tribunal.ListarBancos(objSession.CodigoEmpresa);
            return View(model);
        }

        public JsonResult ListarRegion(int pais)
        {
            Direccion objDireccion = new Direccion();
            return Json(new SelectList(objDireccion.ListarRegion(pais), "Value", "Text"));
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTribunal(GridSettings gridSettings)
        {
            int totalRecords = bcp.Tribunal.ListarTribunalGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.Tribunal bcpTribunal = new Tribunal();
            List<dto.Tribunal> lstTribunal = bcpTribunal.ListarTribunalGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);




            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Tribunal item in lstTribunal
                    select new
                    {

                        id = item.Id,
                        cell = new object[]
                        {
                           item.Codemp,
                           item.Id,
                           item.Rut ,
                           item.Nombre ,
                           item.IdTipo,
                           item.TipoTribunal,
                           item.IdComuna,
                           item.Direccion,
                           item.Telefono1 ,
                           item.Telefono2 ,
                           item.Fax,
                           item.Email,
                           item.IdBanco,
                           item.Banco,
                           item.CuentaCorriente

                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTribunal(bcp.Tribunal model, string oper, int? id)
        {
            model.OperTribunal(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelTribunal(GridSettings gridSettings)
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

            bcp.Tribunal objTribunal = new Tribunal();


            var grid = new GridView();
            grid.DataSource = objTribunal.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Notaria
        public ActionResult Notaria()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Notaria";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetNotaria(GridSettings gridSettings)
        {
            int totalRecords = bcp.Notaria.ListarNotariaGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.Notaria bcpNotaria = new Notaria();
            List<dto.Notaria> lstNotaria = bcpNotaria.ListarNotariaGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Notaria item in lstNotaria
                    select new
                    {

                        id = item.Id,
                        cell = new object[]
                        {
                           item.Codemp,
                           item.Id,
                           item.Rut ,
                           item.Nombre ,
                           item.NombreNotaria,
                           item.IdComuna,
                           item.Direccion,
                           item.Telefono1 ,
                           item.Telefono2 ,
                           item.Fax,
                           item.Celular,
                           item.Email

                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperNotaria(bcp.Notaria model, string oper, int? id)
        {
            model.OperNotaria(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelNotaria(GridSettings gridSettings)
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

            bcp.Notaria objNotaria = new Notaria();


            var grid = new GridView();
            grid.DataSource = objNotaria.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Ente Judicial
        public ActionResult EnteJudicial()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "EnteJudicial";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Provcli = bcp.EnteJudicial.ListarProvcli(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.Empleados = bcp.EnteJudicial.ListarEmpleados(objSession.CodigoEmpresa, objSession.Idioma);

            return View(model);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEnteJudicial(GridSettings gridSettings)
        {
            int totalRecords = bcp.EnteJudicial.ListarEnteJudicialGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.EnteJudicial bcpEnteJudicial = new EnteJudicial();
            List<dto.EnteJudicial> lstEnteJudicial = bcpEnteJudicial.ListarEnteJudicialGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EnteJudicial item in lstEnteJudicial
                    select new
                    {

                        id = item.Id,
                        cell = new object[]
                        {
                           item.Codemp,
                           item.Id,
                           item.Rut ,
                           item.Nombre ,
                           item.NombreEmpleado,
                           item.Sindico,
                           item.Abogado,
                           item.Procurador,
                           item.Receptor ,


                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperEnteJudicial(bcp.EnteJudicial model, string oper, int? id)
        {
            model.OperEnteJudicial(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelEnteJudicial(GridSettings gridSettings)
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

            bcp.EnteJudicial objEnteJudicial = new EnteJudicial();


            var grid = new GridView();
            grid.DataSource = objEnteJudicial.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region Ente Asignado

        public ActionResult EnteAsignado()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "EnteAsignado";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            //ViewBag.Provcli = bcp.EnteJudicial.ListarProvcli(objSession.CodigoEmpresa, objSession.Idioma);
            //ViewBag.Empleados = bcp.EnteJudicial.ListarEmpleados(objSession.CodigoEmpresa, objSession.Idioma);
            ViewBag.TipoEnte = new SelectList(bcp.EnteAsignado.ListarTiposEnte(objSession.Idioma), "Value", "Text");
            return View(model);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEnteAsignado(GridSettings gridSettings, string id)
        {
            int totalRecords = bcp.EnteAsignado.ListarEnteAsignadoGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize, id);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.EnteAsignado bcpEnteAsignado = new EnteAsignado();
            List<dto.EnteAsignado> lstEnteAsignado = bcpEnteAsignado.ListarEnteParaAsignarGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow, id);




            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EnteAsignado item in lstEnteAsignado
                    select new
                    {

                        id = item.Id,
                        cell = new object[]
                        {
                           item.Codemp,
                           item.Id,
                           item.Nombre ,
                           item.Sindico,
                           item.Abogado,
                           item.Procurador,
                           item.Receptor ,
                           item.AbogadoEncargado

                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEnteReasignar(GridSettings gridSettings)
        {
            int totalRecords = bcp.EnteAsignado.ListarEnteParaAsignarGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize, "");
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            bcp.EnteAsignado bcpEnteAsignado = new EnteAsignado();
            List<dto.EnteAsignado> lstEnteAsignado = bcpEnteAsignado.ListarEnteParaAsignarGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow, "");




            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EnteAsignado item in lstEnteAsignado
                    select new
                    {

                        id = item.Id,
                        cell = new object[]
                        {
                           item.Codemp,
                           item.Id,
                           item.Nombre ,
                           item.Sindico,
                           item.Abogado,
                           item.Procurador,
                           item.Receptor ,

                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GrabarEnte(List<String> ids, List<String> id2)
        {
            bcp.EnteAsignado.GrabarEnte(objSession.CodigoEmpresa, ids, -1);
            return Json("OK");
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperEnteAsignado(bcp.EnteJudicial model, string oper, int? id)
        {
            model.OperEnteJudicial(oper, id, objSession);
            return Json("OK");
        }

        public ActionResult ExportToExcelEnteAsignado(GridSettings gridSettings)
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

            bcp.EnteJudicial objEnteJudicial = new EnteJudicial();


            var grid = new GridView();
            grid.DataSource = objEnteJudicial.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
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

        #region "ROL"
        public ActionResult BuscarRutNombreCliente(string term, string TipoCliente = "")
        {
            Dimol.Carteras.bcp.Cliente objCliente = new Dimol.Carteras.bcp.Cliente();

            bool esPrevisional;
            switch (TipoCliente)
            {
                case "P":
                    esPrevisional = true;
                    break;
                default:
                    esPrevisional = false;
                    break;
            }

            return Json(objCliente.ListarRutNombreClienteFiltradoEsPrevisional(term, esPrevisional), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutNombreDeudor(string term)
        {
            return Json(bcp.Rol.ListarRutNombreDeudor(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarEstadoHistorial(string term, int esjid)
        {
            return Json(bcp.Rol.ListarEstadoJudicial(objSession.CodigoEmpresa, objSession.Idioma, term, esjid), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult GetDummy(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = 0;

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.RolCarga> lst = new List<dto.RolCarga>();

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.RolCarga item in lst
                    select new
                    {
                        id = item.Pclid,
                        cell = new object[]
                        {
                            item.Pclid,
                            item.Ctcid,
                            item.Rol
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #region "Rol Documentos"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDocumentosAsignados(GridSettings gridSettings, RolModel model)
        {
            int totalRecords = bcp.Rol.ListarDocumentosAsignadosGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoRol> lstDocsAsig = bcp.Rol.ListarDocumentosAsignadosGrilla(objSession.CodigoEmpresa, objSession.Idioma, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoRol item in lstDocsAsig
                    select new
                    {

                        id = item.Ccbid,
                        cell = new object[]
                        {
                            item.Tipo,
                            item.Numero,
                            item.Monto,
                            item.FechaVencimiento,
                            item.Moneda,
                            item.Saldo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDocumentosAsignadosPrevisional(GridSettings gridSettings, RolModel model)
        {
            int totalRecords = bcp.Rol.ListarDocumentosAsignadosGrillaCountPrevisional(objSession.CodigoEmpresa, objSession.Idioma, model.Rolid, gridSettings.where.groupOp, "FechaResolucion", gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoRol> lstDocsAsig = bcp.Rol.ListarDocumentosAsignadosGrillaPrevisional(objSession.CodigoEmpresa, objSession.Idioma, model.Rolid, gridSettings.where.groupOp, "FechaResolucion", gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoRol item in lstDocsAsig
                    select new
                    {
                        id = item.Resolucion,
                        cell = new object[]
                        {
                            item.Resolucion,
                            item.FechaResolucion,
                            item.Monto,
                            item.Saldo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEstampes(GridSettings gridSettings, RolModel model)
        {
            int totalRecords = bcp.Rol.ListarDocumentosEstampesGrillaCount(objSession.CodigoEmpresa, model.Pclid, model.Ctcid, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoEstampe> lstDocsEst = bcp.Rol.ListarDocumentosEstampesGrilla(objSession.CodigoEmpresa, model.Pclid, model.Ctcid, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoEstampe item in lstDocsEst
                    select new
                    {
                        id = item.Codemp + "|" + item.Ddeid + "|" + item.Pclid + "|" + item.Ctcid + "|" + item.Rolid,
                        cell = new object[]
                        {
                           item.Ddeid,
                           item.Pclid,
                           item.Ctcid,
                           item.Rolid,
                           item.Nombre,
                           item.FechaJudicial,
                           item.NombreInsumo,
                           @"<a href='\Documentos\Estampes\" + model.Pclid + "\\" + model.Ctcid + "\\" + model.Rolid + "\\" + item.Nombre + "' target='_blank'>" + item.Nombre + "</a>",
                           item.Usuario
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarEstampe(string id)
        {
            int salida = 0;
            string[] ids = id.Split('|');
            try
            {
                salida = bcp.Rol.EliminarEstampe(objSession.CodigoEmpresa, ids);

                if (salida > 0)
                {
                    System.IO.File.Delete(ConfigurationManager.AppSettings["RutaArchivos"] + "Documentos\\Estampes\\" + ids[1] + "\\" + ids[2] + "\\" + ids[3] + "\\" + ids[4]);
                }

                return Json("Archivo eliminado.");
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }

        public ActionResult GetTotalDocsAsignados(RolModel model)
        {
            List<dto.DocumentoRol> lstDocsAsig = bcp.Rol.ListarDocumentosAsignadosGrilla(objSession.CodigoEmpresa, objSession.Idioma, model.Rolid, "", "Tipo", "asc", 0, 1111111);
            decimal monto = lstDocsAsig.Sum(x => x.Monto);
            decimal saldo = lstDocsAsig.Sum(x => x.Saldo);

            var jsonData = new
            {
                monto = monto,
                saldo = saldo
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalDocsAsignadosPrevisional(RolModel model)
        {
            List<dto.DocumentoRol> lstDocsAsig = bcp.Rol.ListarDocumentosAsignadosGrillaPrevisional(objSession.CodigoEmpresa, objSession.Idioma, model.Rolid, "", "FechaResolucion", "asc", 0, 1111111);
            decimal monto = lstDocsAsig.Sum(x => x.Monto);
            decimal saldo = lstDocsAsig.Sum(x => x.Saldo);

            var jsonData = new
            {
                monto = monto,
                saldo = saldo
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDocumentosPorAsignar(GridSettings gridSettings, RolModel model)
        {
            int totalRecords = bcp.Rol.ListarDocumentosPorAsignarGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoRol> lstDocsAsig = bcp.Rol.ListarDocumentosPorAsignarGrilla(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoRol item in lstDocsAsig
                    select new
                    {
                        id = item.Ccbid + "|" + item.Monto + "|" + item.Saldo,
                        cell = new object[]
                        {
                            item.Tipo,
                            item.Numero,
                            item.Monto ,
                            item.FechaVencimiento,
                            item.Moneda,
                            item.Saldo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDocumentosPorAsignarPrevisional(GridSettings gridSettings, RolModel model)
        {
            int totalRecords = bcp.Rol.ListarDocumentosPorAsignarGrillaCountPrevisional(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, "FechaResolucion", gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoRol> lstDocsAsig = bcp.Rol.ListarDocumentosPorAsignarGrillaPrevisonal(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, gridSettings.where.groupOp, "FechaResolucion", gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoRol item in lstDocsAsig
                    select new
                    {
                        id = item.Resolucion,
                        cell = new object[]
                        {
                            item.Resolucion,
                            item.FechaResolucion,
                            item.Monto,
                            item.Saldo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalDocsPorAsignar(RolModel model)
        {
            List<dto.DocumentoRol> lstDocsAsig = bcp.Rol.ListarDocumentosPorAsignarGrilla(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, "", "Tipo", "asc", 0, 1111111);
            decimal monto = lstDocsAsig.Sum(x => x.Monto);
            decimal saldo = lstDocsAsig.Sum(x => x.Saldo);

            var jsonData = new
            {
                monto = monto,
                saldo = saldo
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalDocsPorAsignarPrevisional(RolModel model)
        {
            List<dto.DocumentoRol> lstDocsPorAsig = bcp.Rol.ListarDocumentosPorAsignarGrillaPrevisonal(objSession.CodigoEmpresa, objSession.Idioma, model.Pclid, model.Ctcid, "", "FechaResolucion", "asc", 0, 1111111);
            decimal monto = lstDocsPorAsig.Sum(x => x.Monto);
            decimal saldo = lstDocsPorAsig.Sum(x => x.Saldo);

            var jsonData = new
            {
                monto = monto,
                saldo = saldo
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarDocumentosRol(RolModel model)
        {
            int grabar = 0;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string[] agregar = serializer.Deserialize<string[]>(model.DocumentosAsignar);
            string[] eliminar = serializer.Deserialize<string[]>(model.DocumentosEliminar);

            if (agregar.Length > 0 && grabar >= 0)
            {
                foreach (string a in agregar)
                {
                    string[] ids = a.Split('|');
                    grabar = bcp.Rol.InsertarDocumentosRol(objSession.CodigoEmpresa, model.Rolid, model.Pclid, model.Ctcid, Int32.Parse(ids[0]), decimal.Parse(ids[1]), decimal.Parse(ids[2]));
                }
            }

            if (eliminar.Length > 0 && grabar >= 0)
            {
                foreach (string e in eliminar)
                {
                    grabar = bcp.Rol.EliminarDocumentosRol(objSession.CodigoEmpresa, model.Rolid, model.Pclid, model.Ctcid, Int32.Parse(e));
                }
            }

            return Json(grabar);
        }

        public JsonResult ActualizarDocumentosRolPrevisional(RolModel model)
        {
            int grabar = 0;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string[] agregar = serializer.Deserialize<string[]>(model.DocumentosAsignar);
            string[] eliminar = serializer.Deserialize<string[]>(model.DocumentosEliminar);

            //Agregado de documentos
            if (agregar.Length > 0 && grabar >= 0)
            {
                foreach (string NumeroResolucion in agregar)
                {
                    //Buscar documentos de la resolucion
                    List<dto.DocumentoRol> lstDocsPorResolucion = bcp.Rol.ListarDocumentosPorNumeroResolucion(NumeroResolucion);

                    for (int i = 0; i < lstDocsPorResolucion.Count(); i++)
                    {
                        var Doc = lstDocsPorResolucion[i];

                        grabar = bcp.Rol.InsertarDocumentosRol(objSession.CodigoEmpresa, model.Rolid, model.Pclid, model.Ctcid, Doc.Ccbid, Doc.Monto, Doc.Saldo);

                        if (grabar <= 0)
                        {
                            return Json(grabar);
                        }
                    }
                }
            }

            //Eliminación de documentos
            if (eliminar.Length > 0 && grabar >= 0)
            {
                foreach (string NumeroResolucion in eliminar)
                {
                    //Buscar documentos de la resolucion
                    List<dto.DocumentoRol> lstDocsPorResolucion = bcp.Rol.ListarDocumentosPorNumeroResolucion(NumeroResolucion);

                    for (int i = 0; i < lstDocsPorResolucion.Count(); i++)
                    {
                        var Doc = lstDocsPorResolucion[i];

                        grabar = bcp.Rol.EliminarDocumentosRol(objSession.CodigoEmpresa, model.Rolid, model.Pclid, model.Ctcid, Doc.Ccbid);

                        if (grabar <= 0)
                        {
                            return Json(grabar);
                        }
                    }
                }
            }

            return Json(grabar);
        }
        #endregion

        #region "Rol Estados"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEstados(GridSettings gridSettings, RolModel model)
        {
            int totalRecords = bcp.Rol.ListarEstadosRolGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.EstadosRol> lstEstados = bcp.Rol.ListarEstadosRolGrilla(objSession.CodigoEmpresa, objSession.Idioma, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EstadosRol item in lstEstados
                    select new
                    {

                        id = item.Id + "|" + item.Rolid + "|" + item.IdEstado + "|" + item.IdMateria + "|" + item.Fecha.ToString("MMddyyyyHHmmssfff"),
                        cell = new object[]
                        {
                           objSession.PrfId == 6 || objSession.PrfId == 9 || objSession.PrfId == 10 || objSession.PrfId == 13|| objSession.PrfId == 1? item.Rolid + "|" + item.IdEstado + "|" + item.IdMateria + "|" + item.Fecha.ToString("MMddyyyyHHmmssfff"): "0",
                           item.Materia,
                           item.Cuaderno,
                           item.Estado ,
                           item.FechaJudicial ,
                           item.Comentario,
                           item.Usuario    ,
                           item.Archivo
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarEstadosHistorial(int esjid)
        {
            return Json(new SelectList(bcp.MateriaEstado.ListarEstadosCombo(objSession.CodigoEmpresa, objSession.Idioma, esjid), "Value", "Text"));
        }

        public JsonResult GuardarEstadoRol(RolModel model)
        {
            int grabar = 0;

            grabar = bcp.Rol.InsertarEstadoRol(objSession.CodigoEmpresa, model.Rolid, model.EstadoHistorial, model.MateriaHistorial, objSession.UserId, objSession.IpRed, objSession.IpPc, model.ComentarioHistorial, (DateTime)model.FechaHistorial, objSession.CodigoSucursal, objSession.Gestor);
            if (grabar >= 0)
            {
                return Json(new { success = true, message = "OK", Materia = model.MateriaHistorial, Estado = model.EstadoHistorial, RolId = model.Rolid }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "No fue posible ingresar la gestion. Revise que tiene documentos asociados al rol y que este tipo de gestion no existe para la misma fecha.", Materia = model.MateriaHistorial, Estado = model.EstadoHistorial, RolId = model.Rolid }, JsonRequestBehavior.AllowGet);
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperRol(bcp.Rol model, string oper, string id)
        {
            string[] ids = id.Split('|');
            if (objSession.PrfId == 6 || objSession.PrfId == 9 || objSession.PrfId == 10 || objSession.PrfId == 13 || objSession.PrfId == 1)
            {
                if (ids[1] != "0" && ids[2] != "0")
                {
                    bcp.Rol.OperRol(oper, id, objSession);
                    return Json("OK");
                }
                else
                {
                    return Json("Las gestiones del poder judicial no se pueden eliminar.");
                }
            }
            else
            {
                return Json("No tiene permisos suficientes para eliminar una gestión.");
            }

        }
        #endregion

        #region "Rol Entes"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetRolEntes(GridSettings gridSettings, RolModel model)
        {
            int totalRecords = bcp.EnteJudicial.ListarRolEnteJudicialGrillaCount(objSession.CodigoEmpresa, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.EnteJudicial> lstEntes = bcp.EnteJudicial.ListarRolEnteJudicialGrilla(objSession.CodigoEmpresa, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.EnteJudicial item in lstEntes
                    select new
                    {

                        id = item.Id,
                        cell = new object[]
                        {
                           item.Nombre,
                           item.Sindico,
                           item.Abogado ,
                           item.Procurador ,
                           item.Receptor
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarEntesRol(RolModel model)
        {
            int grabar = 0;
            if (model.NuevoEnte > 0)
            {
                grabar = bcp.EnteJudicial.InsertarEnteJudicialRol(objSession.CodigoEmpresa, model.NuevoEnte, model.Rolid);
            }
            if (!string.IsNullOrEmpty(model.ListaEntes) && grabar >= 0)
            {
                string[] entes = model.ListaEntes.Split(',');
                foreach (string s in entes)
                {
                    grabar = bcp.EnteJudicial.EliminarEnteJudicialRol(objSession.CodigoEmpresa, Int32.Parse(s), model.Rolid);
                }

            }
            return Json(grabar);
        }
        #endregion

        #region "Rol Demandados"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetDemandados(GridSettings gridSettings, RolModel model)
        {
            int totalRecords = bcp.Rol.ListarRolDemandadosGrillaCount(objSession.CodigoEmpresa, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Demandado> lstDemandados = bcp.Rol.ListarRolDemandadosGrilla(objSession.CodigoEmpresa, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Demandado item in lstDemandados
                    select new
                    {

                        id = item.Rut,
                        cell = new object[]
                        {
                           Dimol.bcp.Funciones.formatearRut( item.Rut) ,
                           item.Nombre,
                           item.RepresentanteLegal
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActualizarDemandadosRol(RolModel model)
        {
            int grabar = 0;

            if (!string.IsNullOrEmpty(model.DemandadoRut) && !string.IsNullOrEmpty(model.DemandadoNombre))
            {
                model.DemandadoRut = model.DemandadoRut.Replace(".", "").Replace("-", "");
                grabar = bcp.Rol.InsertarDemandadoRol(objSession.CodigoEmpresa, model.Rolid, model.DemandadoRut.Replace(".", "").Replace("-", ""), model.DemandadoNombre, model.DemandadoRepresentanteLegal);
            }
            if (!string.IsNullOrEmpty(model.ListaDemandados) && grabar >= 0)
            {
                string[] demandados = model.ListaDemandados.Split(',');
                foreach (string s in demandados)
                {
                    grabar = bcp.Rol.EliminarDemandadoRol(objSession.CodigoEmpresa, model.Rolid, s);
                }

            }
            return Json(grabar);
        }
        #endregion

        #region "Asociados"

        public JsonResult ListarAsociados(int ctcid)
        {
            return Json(bcp.Rol.ListarAsociados(objSession.CodigoEmpresa, ctcid), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Asegurados"

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAsegurados(GridSettings gridSettings, RolModel model)
        {
            int totalRecords = bcp.Rol.ListarRolAseguradosGrillaCount(objSession.CodigoEmpresa, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Asegurado> lstAsegurados = bcp.Rol.ListarRolAseguradosGrilla(objSession.CodigoEmpresa, model.Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Asegurado item in lstAsegurados
                    select new
                    {

                        id = item.Rut,
                        cell = new object[]
                        {
                           Dimol.bcp.Funciones.formatearRut( item.Rut) ,
                           item.Nombre,
                           item.Numero
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion 

        #region "Borradores"
        public JsonResult GetBorrador(RolModel model)
        {
            string html = CKEditor.bcp.Documento.GeneraDocumento(objSession.CodigoEmpresa, model.Rolid, model.Borradores);

            return Json(html);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult GuardarBorrador(RolModel model)
        {
            int grabar = 0;

            grabar = CKEditor.bcp.Documento.InsertarRolBorrador(objSession.CodigoEmpresa, model.Rolid, model.Borradores, model.HTMLBorrador, objSession.UserId);

            return Json(grabar);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult GuardarBorradorDemandasMasivas(int PanelDemandaId, int TipoBorradorId, string HtmlBorrador)
        {
            int grabar = 0;

            grabar = CKEditor.bcp.Documento.InsertarBorradorDemandasMasivas(objSession.CodigoEmpresa, PanelDemandaId, TipoBorradorId, HtmlBorrador, objSession.UserId);

            return Json(grabar);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult GuardarBorradorDemandasPrevisional(int PanelDemandaId, int TipoBorradorId, string HtmlBorrador)
        {
            int grabar = 0;

            grabar = CKEditor.bcp.Documento.InsertarBorradorDemandasPrevisional(objSession.CodigoEmpresa, PanelDemandaId, TipoBorradorId, HtmlBorrador, objSession.UserId);

            return Json(grabar);
        }

        public JsonResult GetHistoriaBorrador(RolModel model)
        {
            Dimol.dto.Combobox html = bcp.Rol.HistoriaBorrador(objSession.CodigoEmpresa, model.Rolid, model.Borradores);
            var jsonData = new
            {
                creacion = html.Text,
                ultimo = html.Value
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Borradores PanelDemandasMasivas"
        public JsonResult GetBorradorDemandasMasivas(int IdDM, int TipoBorrador)
        {
            string html = CKEditor.bcp.Documento.GeneraDocumentoDemandaMasiva(objSession.CodigoEmpresa, IdDM, TipoBorrador);

            return Json(html);
        }

        public JsonResult GetHistoriaBorradorDemandasMasivas(int IdDM, int TipoBorrador)
        {
            Dimol.dto.Combobox html = bcp.PanelDemandaMasiva.HistoriaBorradorDemandasMasivas(objSession.CodigoEmpresa, IdDM, TipoBorrador);

            var jsonData = new
            {
                creacion = html.Text,
                ultimo = html.Value
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Borradores PanelDemandasPrevisional"
        public JsonResult GetBorradorDemandasPrevisional(int IdDP, int TipoBorrador)
        {
            string html = CKEditor.bcp.Documento.GeneraDocumentoDemandaPrevisional(objSession.CodigoEmpresa, IdDP, TipoBorrador);

            return Json(html);
        }

        public JsonResult GetHistoriaBorradorDemandasPrevisional(int IdDP, int TipoBorrador)
        {
            Dimol.dto.Combobox html = bcp.PanelDemandaPrevisional.HistoriaBorradorDemandasPrevisional(objSession.CodigoEmpresa, IdDP, TipoBorrador);

            var jsonData = new
            {
                creacion = html.Text,
                ultimo = html.Value
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Traspaso Judicial"
        public ActionResult TraspasoJudicial()
        {
            if (!SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public JsonResult GetTraspasoJudicial(GridSettings gridSettings, TraspasoJudicialModel model)
        {
            int totalRecords = bcp.TraspasoJudicial.ListarTraspasosGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, objSession.Permisos, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TraspasoJudicialCandidato> lstTraspasos = bcp.TraspasoJudicial.ListarTraspasosGrilla(objSession.CodigoEmpresa, objSession.Idioma, objSession.Permisos, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TraspasoJudicialCandidato item in lstTraspasos
                    select new
                    {

                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid + "|" + item.Monto + "|" + item.Saldo + "|" + item.Numero,
                        cell = new object[]
                        {
                           item.Cliente,
                           Dimol.bcp.Funciones.formatearRut(item.RutDeudor),
                           item.Deudor,
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



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarTraspasos(string ids)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
            traspasos = bcp.TraspasoJudicial.TraspasoDeudores(lst, objSession);

            return Json(traspasos);
        }

        public JsonResult GetTraspasoJudicialPendiente(GridSettings gridSettings, string fechaDesde, string fechaHasta)
        {
            DateTime desde, hasta;
            try
            {
                desde = DateTime.ParseExact(fechaDesde, "dd-MM-yyyy", null);
            }
            catch (Exception ex)
            {
                desde = DateTime.Today;
            }
            try
            {
                hasta = DateTime.ParseExact(fechaHasta, "dd-MM-yyyy", null).AddDays(1);
            }
            catch (Exception ex)
            {
                hasta = DateTime.Today.AddDays(1);
            }

            int totalRecords = bcp.TraspasoJudicial.ListarTraspasosPendientesGrillaCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.Idioma, desde, hasta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TraspasoJudicialPendiente> lstTraspasos = bcp.TraspasoJudicial.ListarTraspasosPendientesGrilla(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.Idioma, desde, hasta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TraspasoJudicialPendiente item in lstTraspasos
                    select new
                    {

                        id = item.Tpcid + "|" + item.Numero,
                        cell = new object[]
                        {
                           item.Cliente,
                           item.Tipo,
                           item.Numero ,
                           item.Fecha
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTraspasoJudicialHecho(GridSettings gridSettings, string fechaDesde, string fechaHasta)
        {
            DateTime desde, hasta;
            try
            {
                desde = DateTime.ParseExact(fechaDesde, "dd-MM-yyyy", null);
            }
            catch (Exception ex)
            {
                desde = DateTime.Today;
            }
            try
            {
                hasta = DateTime.ParseExact(fechaHasta, "dd-MM-yyyy", null).AddDays(1);
            }
            catch (Exception ex)
            {
                hasta = DateTime.Today.AddDays(1);
            }

            int totalRecords = bcp.TraspasoJudicial.ListarTraspasosHechosGrillaCount(objSession.CodigoEmpresa, desde, hasta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TraspasoJudicialHecho> lstTraspasos = bcp.TraspasoJudicial.ListarTraspasosHechosGrilla(objSession.CodigoEmpresa, desde, hasta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TraspasoJudicialHecho item in lstTraspasos
                    select new
                    {

                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid + "|" + item.Monto + "|" + item.Saldo,
                        cell = new object[]
                        {
                           item.Cliente,
                           Dimol.bcp.Funciones.formatearRut(item.RutDeudor),
                           item.Deudor,
                           item.Tipo,
                           item.Numero,
                           item.FechaAsignacion,
                           item.FechaVencimiento,
                           item.Monto ,
                           item.Saldo,
                           item.Estado,
                           item.EstadoCpbt,
                           item.Fecha
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTraspasoJudicialHechoDeudor(GridSettings gridSettings, int Ctcid)
        {

            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TraspasoJudicialHecho> lstTraspasos = bcp.TraspasoJudicial.ListarTraspasosHechosDeudorGrilla(objSession.CodigoEmpresa, Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());
            int totalRecords = lstTraspasos.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TraspasoJudicialHecho item in lstTraspasos
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid + "|" + item.Monto + "|" + item.Saldo,
                        cell = new object[]
                        {
                           item.Cliente,
                           Dimol.bcp.Funciones.formatearRut(item.RutDeudor),
                           item.Deudor,
                           item.Tipo,
                           item.Numero,
                           item.FechaAsignacion,
                           item.FechaVencimiento,
                           item.Monto ,
                           item.Saldo,
                           item.Estado,
                           item.EstadoCpbt,
                           item.Fecha
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExcelTraspasosPendientes(GridSettings gridSettings, string fechaDesde, string fechaHasta)
        {
            DateTime desde, hasta;
            try
            {
                desde = DateTime.ParseExact(fechaDesde, "dd-MM-yyyy", null);
            }
            catch (Exception ex)
            {
                desde = DateTime.Parse("2014-01-01");//DateTime.Today;
            }
            try
            {
                hasta = DateTime.ParseExact(fechaHasta, "dd-MM-yyyy", null).AddDays(1);
            }
            catch (Exception ex)
            {
                hasta = DateTime.Today.AddDays(1);
            }
            var grid = new GridView();
            grid.DataSource = bcp.TraspasoJudicial.ListarTraspasosPendientesGrilla(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.Idioma, desde, hasta, gridSettings.where.groupOp, "Numero", gridSettings.sortOrder.ToString(), 1, 100000000); ;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=TraspasosPendientes.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("TraspasoJudicial");
        }

        public ActionResult ExcelTraspasosHechos(GridSettings gridSettings, string fechaDesde, string fechaHasta)
        {
            DateTime desde, hasta;
            try
            {
                desde = DateTime.ParseExact(fechaDesde, "dd-MM-yyyy", null);
            }
            catch (Exception ex)
            {
                desde = DateTime.Parse("2014-01-01");//DateTime.Today;
            }
            try
            {
                hasta = DateTime.ParseExact(fechaHasta, "dd-MM-yyyy", null).AddDays(1);
            }
            catch (Exception ex)
            {
                hasta = DateTime.Today.AddDays(1);
            }
            var grid = new GridView();
            grid.DataSource = bcp.TraspasoJudicial.ListarTraspasosHechosGrilla(objSession.CodigoEmpresa, desde, hasta, gridSettings.where.groupOp, "Cliente,Deudor,Fecha", gridSettings.sortOrder.ToString(), 0, 100000000); ;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=TraspasosRealizados.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("TraspasoJudicial");
        }

        public JsonResult DocumentosNoDemandables(string ids, string motivo)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
            traspasos = bcp.TraspasoJudicial.DocumentosNoDemandables(lst, objSession, motivo);

            return Json(traspasos);
        }
        #endregion


        #region "Traspaso Judicial Previsional"
        public ActionResult TraspasoJudicialPrevisional()
        {
            if (!SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public JsonResult GetTraspasoJudicialPrevisional(GridSettings gridSettings, TraspasoJudicialModel model)
        {
            int totalRecords = bcp.TraspasoJudicial.ListarTraspasosGrillaCountPrevisional(objSession.CodigoEmpresa, objSession.Idioma, objSession.Permisos, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TraspasoJudicialCandidato> lstTraspasos = bcp.TraspasoJudicial.ListarTraspasosGrillaPrevisional(objSession.CodigoEmpresa, objSession.Idioma, objSession.Permisos, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TraspasoJudicialCandidato item in lstTraspasos
                    select new
                    {
                        //id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid + "|" + item.Monto + "|" + item.Saldo + "|" + item.Numero,
                        id = item.Pclid + "|" + item.Ctcid + "|" + item.NumResolucion + "|" + item.Monto + "|" + item.Saldo,
                        cell = new object[]
                        {
                           item.Cliente,
                           Dimol.bcp.Funciones.formatearRut(item.RutDeudor),
                           item.Deudor,
                           item.NumResolucion,
                           item.FecResolucion,
                           //item.Numero,
                           //item.FechaAsignacion,
                           //item.FechaVencimiento,
                           item.Monto ,
                           item.Saldo,
                           item.Estado,
                           item.EstadoCpbt,
                           //item.Asegurado
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTraspasoJudicialHechoPrevisional(GridSettings gridSettings, string fechaDesde, string fechaHasta)
        {
            DateTime desde, hasta;

            try
            {
                desde = DateTime.ParseExact(fechaDesde, "dd-MM-yyyy", null);
            }
            catch (Exception ex)
            {
                desde = DateTime.Today;
            }

            try
            {
                hasta = DateTime.ParseExact(fechaHasta, "dd-MM-yyyy", null).AddDays(1);
            }
            catch (Exception ex)
            {
                hasta = DateTime.Today.AddDays(1);
            }

            int totalRecords = bcp.TraspasoJudicial.ListarTraspasosHechosGrillaCountPrevisional(objSession.CodigoEmpresa, desde, hasta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TraspasoJudicialHecho> lstTraspasos = bcp.TraspasoJudicial.ListarTraspasosHechosGrillaPrevisional(objSession.CodigoEmpresa, desde, hasta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TraspasoJudicialHecho item in lstTraspasos
                    select new
                    {

                        //id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid + "|" + item.Monto + "|" + item.Saldo,
                        id = item.Pclid + "|" + item.Ctcid + "|" + item.NumResolucion + "|" + item.Monto + "|" + item.Saldo,
                        cell = new object[]
                        {
                           item.Cliente,
                           Dimol.bcp.Funciones.formatearRut(item.RutDeudor),
                           item.Deudor,
                           item.NumResolucion,
                           item.FecResolucion,
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

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarTraspasosPrevisional(string ids)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);

            traspasos = bcp.TraspasoJudicial.TraspasoDeudoresPrevisional(lst, objSession);

            return Json(traspasos);
        }

        public ActionResult ExcelTraspasosHechosPrevisional(GridSettings gridSettings, string fechaDesde, string fechaHasta)
        {
            DateTime desde, hasta;
            try
            {
                desde = DateTime.ParseExact(fechaDesde, "dd-MM-yyyy", null);
            }
            catch (Exception ex)
            {
                desde = DateTime.Parse("2014-01-01");
            }
            try
            {
                hasta = DateTime.ParseExact(fechaHasta, "dd-MM-yyyy", null).AddDays(1);
            }
            catch (Exception ex)
            {
                hasta = DateTime.Today.AddDays(1);
            }
            var grid = new GridView();
            grid.DataSource = bcp.TraspasoJudicial.ListarTraspasosHechosGrillaPrevisional(objSession.CodigoEmpresa, desde, hasta, gridSettings.where.groupOp, "Cliente,Deudor,FecResolucion", gridSettings.sortOrder.ToString(), 0, 100000000); ;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=TraspasosPrevisionalesRealizados.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("TraspasoJudicialPrevisional");
        }

        //public JsonResult GetTraspasoJudicialPendiente(GridSettings gridSettings, string fechaDesde, string fechaHasta)
        //{
        //    DateTime desde, hasta;
        //    try
        //    {
        //        desde = DateTime.ParseExact(fechaDesde, "dd-MM-yyyy", null);
        //    }
        //    catch (Exception ex)
        //    {
        //        desde = DateTime.Today;
        //    }
        //    try
        //    {
        //        hasta = DateTime.ParseExact(fechaHasta, "dd-MM-yyyy", null).AddDays(1);
        //    }
        //    catch (Exception ex)
        //    {
        //        hasta = DateTime.Today.AddDays(1);
        //    }

        //    int totalRecords = bcp.TraspasoJudicial.ListarTraspasosPendientesGrillaCount(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.Idioma, desde, hasta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
        //    int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
        //    int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
        //    int endRow = startRow + gridSettings.pageSize;

        //    List<dto.TraspasoJudicialPendiente> lstTraspasos = bcp.TraspasoJudicial.ListarTraspasosPendientesGrilla(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.Idioma, desde, hasta, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


        //    var jsonData = new
        //    {
        //        total = totalPages,
        //        page = gridSettings.pageIndex,
        //        records = totalRecords,

        //        rows =
        //        (
        //            from dto.TraspasoJudicialPendiente item in lstTraspasos
        //            select new
        //            {

        //                id = item.Tpcid + "|" + item.Numero,
        //                cell = new object[]
        //                {
        //                   item.Cliente,
        //                   item.Tipo,
        //                   item.Numero ,
        //                   item.Fecha
        //                }
        //            }
        //        ).ToArray()
        //    };



        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetTraspasoJudicialHechoDeudor(GridSettings gridSettings, int Ctcid)
        //{

        //    int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
        //    int endRow = startRow + gridSettings.pageSize;

        //    List<dto.TraspasoJudicialHecho> lstTraspasos = bcp.TraspasoJudicial.ListarTraspasosHechosDeudorGrilla(objSession.CodigoEmpresa, Ctcid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());
        //    int totalRecords = lstTraspasos.Count;
        //    int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);

        //    var jsonData = new
        //    {
        //        total = totalPages,
        //        page = gridSettings.pageIndex,
        //        records = totalRecords,

        //        rows =
        //        (
        //            from dto.TraspasoJudicialHecho item in lstTraspasos
        //            where item.Row >= startRow && item.Row <= endRow
        //            select new
        //            {

        //                id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid + "|" + item.Monto + "|" + item.Saldo,
        //                cell = new object[]
        //                {
        //                   item.Cliente,
        //                   Dimol.bcp.Funciones.formatearRut(item.RutDeudor),
        //                   item.Deudor,
        //                   item.Tipo,
        //                   item.Numero,
        //                   item.FechaAsignacion,
        //                   item.FechaVencimiento,
        //                   item.Monto ,
        //                   item.Saldo,
        //                   item.Estado,
        //                   item.EstadoCpbt,
        //                   item.Fecha
        //                }
        //            }
        //        ).ToArray()
        //    };

        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult ExcelTraspasosPendientes(GridSettings gridSettings, string fechaDesde, string fechaHasta)
        //{
        //    DateTime desde, hasta;
        //    try
        //    {
        //        desde = DateTime.ParseExact(fechaDesde, "dd-MM-yyyy", null);
        //    }
        //    catch (Exception ex)
        //    {
        //        desde = DateTime.Parse("2014-01-01");//DateTime.Today;
        //    }
        //    try
        //    {
        //        hasta = DateTime.ParseExact(fechaHasta, "dd-MM-yyyy", null).AddDays(1);
        //    }
        //    catch (Exception ex)
        //    {
        //        hasta = DateTime.Today.AddDays(1);
        //    }
        //    var grid = new GridView();
        //    grid.DataSource = bcp.TraspasoJudicial.ListarTraspasosPendientesGrilla(objSession.CodigoEmpresa, objSession.CodigoSucursal, objSession.Idioma, desde, hasta, gridSettings.where.groupOp, "Numero", gridSettings.sortOrder.ToString(), 1, 100000000); ;
        //    grid.DataBind();

        //    Response.ClearContent();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment; filename=TraspasosPendientes.xls");
        //    Response.ContentType = "application/ms-excel";

        //    Response.Charset = "";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    grid.RenderControl(htw);

        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();

        //    return View("TraspasoJudicial");
        //}

        //public JsonResult DocumentosNoDemandables(string ids, string motivo)
        //{
        //    string traspasos = "";
        //    List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
        //    traspasos = bcp.TraspasoJudicial.DocumentosNoDemandables(lst, objSession, motivo);

        //    return Json(traspasos);
        //}
        #endregion


        #region "Reversa Traspaso Judicial"
        public ActionResult ReversaTraspasoJudicial()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Estado = new SelectList(bcp.TraspasoJudicial.ListarEstadosReversa(objSession.CodigoEmpresa, objSession.Idioma, "-- Seleccione --"), "Value", "Text", "");
            return View();
        }

        public JsonResult GetReversaTraspasoJudicial(GridSettings gridSettings, TraspasoJudicialModel model)
        {
            int totalRecords = bcp.TraspasoJudicial.ListarTraspasosGrillaCount(objSession.CodigoEmpresa, objSession.Idioma, objSession.Permisos, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TraspasoJudicialCandidato> lstTraspasos = bcp.TraspasoJudicial.ListarTraspasosGrilla(objSession.CodigoEmpresa, objSession.Idioma, objSession.Permisos, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TraspasoJudicialCandidato item in lstTraspasos
                    select new
                    {

                        id = item.Pclid + "|" + item.Ctcid,
                        cell = new object[]
                        {
                           item.Cliente,
                           Dimol.bcp.Funciones.formatearRut(item.RutDeudor),
                           item.Deudor ,
                           item.MontoTotal
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarReversaTraspasos(string ids, int estid, string comentario)
        {
            string traspasos = "";
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(ids);
            traspasos = bcp.TraspasoJudicial.ReversaDocumentoDeudores(lst, estid, comentario, objSession);

            return Json(traspasos);
        }

        public JsonResult GetDocumentosReversaTraspasoJudicial(GridSettings gridSettings, int pclid, int ctcid)
        {
            int totalRecords = bcp.TraspasoJudicial.ListarDocumentosReversaGrillaCount(objSession.CodigoEmpresa, pclid, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DocumentoReversar> lstReversa = bcp.TraspasoJudicial.ListarDocumentosReversaGrilla(objSession.CodigoEmpresa, pclid, ctcid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentoReversar item in lstReversa
                    select new
                    {

                        id = item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid + "|" + item.Monto + "|" + item.Saldo,
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

        public ActionResult ExcelReversaTraspasosHechos(GridSettings gridSettings, string fechaDesde, string fechaHasta)
        {
            DateTime desde, hasta;
            try
            {
                desde = DateTime.ParseExact(fechaDesde, "dd-MM-yyyy", null);
            }
            catch (Exception ex)
            {
                desde = DateTime.Parse("2014-01-01");//DateTime.Today;
            }
            try
            {
                hasta = DateTime.ParseExact(fechaHasta, "dd-MM-yyyy", null).AddDays(1);
            }
            catch (Exception ex)
            {
                hasta = DateTime.Today.AddDays(1);
            }
            var grid = new GridView();
            grid.DataSource = bcp.TraspasoJudicial.ListarTraspasosHechosGrilla(objSession.CodigoEmpresa, desde, hasta, gridSettings.where.groupOp, "Cliente,Deudor,Fecha", gridSettings.sortOrder.ToString(), 0, 100000000); ;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=TraspasosRealizados.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("TraspasoJudicial");
        }
        #endregion

        #region "Panel de Demandas"
        public ActionResult PanelDemandas()
        {
            //Validación de sesión
            if (!SettingAccount())
            {
                return RedirectToAction("Login", "Account");

            }

            List<Combobox> lstTipo = new List<Combobox>();
            lstTipo.Add(new Combobox { Text = "C", Value = "C" });
            lstTipo.Add(new Combobox { Text = "V", Value = "V" });
            lstTipo.Add(new Combobox { Text = "E", Value = "E" });
            lstTipo.Add(new Combobox { Text = "A", Value = "A" });
            lstTipo.Add(new Combobox { Text = "F", Value = "F" });
            lstTipo.Add(new Combobox { Text = "I", Value = "I" });
            ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", "C");

            ViewBag.TipoCausa = new SelectList(bcp.Rol.ListarTiposCausa(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Rolid = 0;
            ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", "C");
            ViewBag.MateriaJudicial = new SelectList(bcp.MateriaEstado.ListarMateriasCombo(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            List<Combobox> lstQuiebra = new List<Combobox>();
            lstQuiebra.Add(new Combobox { Text = "No", Value = "N" });
            lstQuiebra.Add(new Combobox { Text = "En Proceso", Value = "P" });
            lstQuiebra.Add(new Combobox { Text = "Si", Value = "S" });
            ViewBag.ComboQuiebra = new SelectList(lstQuiebra, "Value", "Text", "N");

            ViewBag.GetPerfil = objSession.PrfId;
            ViewBag.GetUsuario = 397; ////eliminar, para pruebas objSession.UserId;

            return View();
        }

        public ActionResult PanelDemandasPrevisional()
        {
            //Validación de sesión
            if (!SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            //List<Combobox> lstTipo = new List<Combobox>();
            //lstTipo.Add(new Combobox { Text = "C", Value = "C" });
            //lstTipo.Add(new Combobox { Text = "V", Value = "V" });
            //lstTipo.Add(new Combobox { Text = "E", Value = "E" });
            //lstTipo.Add(new Combobox { Text = "A", Value = "A" });
            //lstTipo.Add(new Combobox { Text = "F", Value = "F" });
            //lstTipo.Add(new Combobox { Text = "I", Value = "I" });
            //ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", "C");
            List<Combobox> lstTipo = ComboTipoRol.CargarListaTipoRol("P");

            ViewBag.TipoCausa = new SelectList(bcp.Rol.ListarTiposCausa(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Rolid = 0;
            ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", "P");
            ViewBag.MateriaJudicial = new SelectList(bcp.MateriaEstado.ListarMateriasCombo(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            List<Combobox> lstQuiebra = new List<Combobox>();
            lstQuiebra.Add(new Combobox { Text = "No", Value = "N" });
            lstQuiebra.Add(new Combobox { Text = "En Proceso", Value = "P" });
            lstQuiebra.Add(new Combobox { Text = "Si", Value = "S" });
            ViewBag.ComboQuiebra = new SelectList(lstQuiebra, "Value", "Text", "N");

            ViewBag.GetPerfil = objSession.PrfId;
            ViewBag.GetUsuario = objSession.UserId;

            //Modal de borradores
            ViewBag.TipoBorrador = new SelectList(Dimol.CKEditor.bcp.Documento.ListarDocumentos(objSession.CodigoEmpresa, "PRE", "Seleccione Borrador"), "Value", "Text", "");

            return View();
        }

        public JsonResult EliminarPanelDemanda(int IdPanelDemanda)
        {
            if (objSession.UserId != 334) //Solo se permite al usuario de Pedro
            {
                return Json("No posee privilegios para eliminar.");
            }

            try
            {
                if (PanelDemanda.EliminarPanelDemanda(IdPanelDemanda))
                {
                    return Json("Demanda eliminada.");
                }

                return Json("No se ha podido eliminar la demanda.");
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }
        public JsonResult EliminarPanelDemandaPrevisional(int IdPanelDemanda)
        {
            if (objSession.UserId != 334) //Solo se permite al usuario de Pedro
            {
                return Json("No posee privilegios para eliminar.");
            }

            try
            {
                if (PanelDemanda.EliminarPanelDemandaPrevisional(IdPanelDemanda))
                {
                    return Json("Demanda eliminada.");
                }

                return Json("No se ha podido eliminar la demanda.");
            }
            catch (Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }

        public ActionResult PanelDemandasMasivas()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            List<Combobox> lstTipo = new List<Combobox>();
            lstTipo.Add(new Combobox { Text = "C", Value = "C" });
            lstTipo.Add(new Combobox { Text = "V", Value = "V" });
            lstTipo.Add(new Combobox { Text = "E", Value = "E" });
            lstTipo.Add(new Combobox { Text = "A", Value = "A" });
            lstTipo.Add(new Combobox { Text = "F", Value = "F" });
            lstTipo.Add(new Combobox { Text = "I", Value = "I" });
            ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", "C");

            ViewBag.TipoCausa = new SelectList(bcp.Rol.ListarTiposCausa(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");

            ViewBag.Rolid = 0;
            ViewBag.TipoRol = new SelectList(lstTipo, "Value", "Text", "C");
            ViewBag.MateriaJudicial = new SelectList(bcp.MateriaEstado.ListarMateriasCombo(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");
            List<Combobox> lstQuiebra = new List<Combobox>();
            lstQuiebra.Add(new Combobox { Text = "No", Value = "N" });
            lstQuiebra.Add(new Combobox { Text = "En Proceso", Value = "P" });
            lstQuiebra.Add(new Combobox { Text = "Si", Value = "S" });
            ViewBag.ComboQuiebra = new SelectList(lstQuiebra, "Value", "Text", "N");

            ViewBag.GetPerfil = objSession.PrfId;
            ViewBag.GetUsuario = objSession.UserId;

            List<Combobox> lstTiposDemandaMasiva = new List<Combobox>();
            lstTiposDemandaMasiva.Add(new Combobox() { Text = "--Seleccione Tipo Demanda--", Value = "" });
            lstTiposDemandaMasiva.Add(new Combobox() { Text = "DEMANDA SIN MANDATO", Value = "1" });
            lstTiposDemandaMasiva.Add(new Combobox() { Text = "DEMANDA CON MANDATO", Value = "2" });

            ViewBag.TiposDemandaMasiva = new SelectList(lstTiposDemandaMasiva, "Value", "Text");

            //Modal de borradores
            ViewBag.TipoBorrador = new SelectList(Dimol.CKEditor.bcp.Documento.ListarDocumentos(objSession.CodigoEmpresa, "JUD", "Seleccione Borrador"), "Value", "Text", "");

            return View();
        }

        public void ExportToPDFDemandaMasiva(int IdDM, int IdTipoBorrador)
        {
            //Siempre se busca el template de "Demanda con Mandato"
            string Template = CKEditor.bcp.Documento.GeneraDocumentoDemandaMasiva(objSession.CodigoEmpresa, IdDM, IdTipoBorrador);

            byte[] ByteFile = bcp.PanelDemanda.ExportToPDFDemandaMasiva(objSession.CodigoEmpresa, IdDM, Template);

            Response.Clear();
            MemoryStream ms = new MemoryStream(ByteFile);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=test.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }

        public ActionResult ListarPanelDemandas(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = PanelDemanda.ListarPanelDemandasCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelDemandaGet> lst = PanelDemanda.ListarPanelDemandas(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.PanelDemandaGet item in lst
                    select new
                    {
                        id = item.PanelId,
                        cell = new object[]
                        {
                            item.PanelId,
                            item.Responsable,
                            item.FechaAsignacion,
                            item.FechaAprobacionTraspaso,
                            item.FechaIngresaJudicial,
                            item.Cliente,
                            item.RutDeudor,
                            item.Deudor,
                            item.Asegurado,
                            item.TipoDocumento,
                            item.Comuna,
                            item.Region,
                            item.EncargadoCofeccion,
                            item.FechaEnvioConfeccion,
                            item.FechaEntrega,
                            item.FechaIngresoTribunal,
                            item.Correcciones,
                            item.CountCorrecciones,
                            item.Comentarios,
                            item.Procesada,
                            "Avance",
                            item.RolNumero,
                            item.TribunalNombre,
                            item.UsridEncargado,
                            item.Pclid,
                            item.Ctcid,
                            item.RutCliente,
                            item.CountFechaEntrega,
                            item.Cursodemanda,
                            item.CountCursodemanda
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPanelDemandasPrevisional(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = PanelDemanda.ListarPanelDemandasPrevisionalCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelDemandaGet> lst = PanelDemanda.ListarPanelDemandasPrevisionales(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.PanelDemandaGet item in lst
                    select new
                    {
                        id = item.PanelId,
                        cell = new object[]
                        {
                            item.PanelId,
                            item.Responsable,
                            item.FechaAsignacion,
                            item.FechaAprobacionTraspaso,
                            item.FechaIngresaJudicial,
                            item.Cliente,
                            item.RutDeudor,
                            item.Deudor,
                            item.Comuna,
                            item.Region,
                            item.EncargadoCofeccion,
                            item.FechaEnvioConfeccion,
                            item.FechaEntrega,
                            item.FechaIngresoTribunal,
                            item.Correcciones,
                            item.CountCorrecciones,
                            item.Comentarios,
                            item.Procesada,
                            "Avance",
                            item.RolNumero,
                            item.TribunalNombre,
                            item.UsridEncargado,
                            item.Pclid,
                            item.Ctcid,
                            item.RutCliente,
                            item.CountFechaEntrega,
                            item.Cursodemanda,
                            item.CountCursodemanda
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPanelDemandasMasivas(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = bcp.PanelDemanda.ListarPanelDemandasMasivasCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelDemandaGet> lst = bcp.PanelDemanda.ListarPanelDemandasMasivas(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,
                rows =
                (
                    from dto.PanelDemandaGet item in lst
                    select new
                    {
                        id = item.PanelId,
                        cell = new object[]
                        {
                            item.PanelId,
                            //item.Responsable,
                            item.FechaAsignacion,
                            item.FechaAprobacionTraspaso,
                            item.FechaIngresaJudicial,
                            item.Cliente,
                            item.RutDeudor,
                            item.Deudor,
                            item.Asegurado,
                            item.TipoDocumento,
                            item.Comuna,
                            item.Region,
                            //item.EncargadoCofeccion,
                            item.FechaEnvioConfeccion,
                            item.FechaEntrega,
                            item.FechaIngresoTribunal,
                            item.Correcciones,
                            item.CountCorrecciones,
                            item.Comentarios,
                            item.Procesada,
                            "Avance",
                            item.UsridEncargado,
                            item.Pclid,
                            item.Ctcid,
                            item.RutCliente,
                            item.CountFechaEntrega,
                            item.RolNumero,
                            item.TribunalNombre,
                            item.Cursodemanda,
                            item.CountCursodemanda

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarNombreUsuario(string term)
        {
            return Json(bcp.PanelDemanda.BuscarNombreUsuario(term, objSession.CodigoEmpresa, objSession.CodigoSucursal), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarAvanceDemanda(int panelId, int userIdEncargado, DateTime fechaEnvio, bool flagEnviaFechaEntrega, DateTime? fechaEntrega, DateTime? fechaTribunales, string comentarios)
        {
            int grabar = 0;

            grabar = bcp.PanelDemanda.GrabarAvanceDemanda(panelId, userIdEncargado, fechaEnvio, fechaEntrega, fechaTribunales, comentarios, objSession.UserId, flagEnviaFechaEntrega); //bcp.Rol.InsertarEstadoRol(objSession.CodigoEmpresa, model.Rolid, model.EstadoHistorial, model.MateriaHistorial, objSession.UserId, objSession.IpRed, objSession.IpPc, model.ComentarioHistorial, (DateTime)model.FechaHistorial, objSession.CodigoSucursal, objSession.Gestor);
            if (grabar > 0)
            {
                return Json(grabar);
            }
            else
            {
                return Json("No fue posible ingresar el avance");
            }

        }

        public JsonResult GuardarAvanceDemandaPrevisional(int panelId, int userIdEncargado, DateTime fechaEnvio, bool flagEnviaFechaEntrega, DateTime? fechaEntrega, DateTime? fechaTribunales, string comentarios)
        {
            int grabar = 0;

            grabar = bcp.PanelDemanda.GrabarAvanceDemandaPrevisional(panelId, userIdEncargado, fechaEnvio, fechaEntrega, fechaTribunales, comentarios, objSession.UserId, flagEnviaFechaEntrega); //bcp.Rol.InsertarEstadoRol(objSession.CodigoEmpresa, model.Rolid, model.EstadoHistorial, model.MateriaHistorial, objSession.UserId, objSession.IpRed, objSession.IpPc, model.ComentarioHistorial, (DateTime)model.FechaHistorial, objSession.CodigoSucursal, objSession.Gestor);
            if (grabar > 0)
            {
                return Json(grabar);
            }
            else
            {
                return Json("No fue posible ingresar el avance");
            }
        }

        public JsonResult GuardarAvanceDemandaMasiva(int panelId, int userIdEncargado, DateTime fechaEnvio, bool flagEnviaFechaEntrega, DateTime? fechaEntrega, DateTime? fechaTribunales, string comentarios)
        {
            int grabar = 0;

            grabar = bcp.PanelDemanda.GrabarAvanceDemandaMasiva(panelId, userIdEncargado, fechaEnvio, fechaEntrega, fechaTribunales, comentarios, objSession.UserId, flagEnviaFechaEntrega); //bcp.Rol.InsertarEstadoRol(objSession.CodigoEmpresa, model.Rolid, model.EstadoHistorial, model.MateriaHistorial, objSession.UserId, objSession.IpRed, objSession.IpPc, model.ComentarioHistorial, (DateTime)model.FechaHistorial, objSession.CodigoSucursal, objSession.Gestor);
            if (grabar > 0)
            {
                return Json(grabar);
            }
            else
            {
                return Json("No fue posible ingresar el avance");
            }
        }

        public ActionResult BuscarTribunal(string term)
        {
            return Json(bcp.PanelDemanda.BuscarTribunal(term, objSession.CodigoEmpresa), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarAvanceDemandaRol(RolModel model, int panelId, DateTime? fechaTribunales, bool flagEnviaFechaEntrega, DateTime? fechaEntrega)
        {
            string quiebraDeudor = "N";
            dto.Rol rol = new dto.Rol();
            if (model.ComboQuiebra == "P")
            {
                rol.rol_prequiebra = "S";
                quiebraDeudor = "N";
            }
            else if (model.ComboQuiebra == "S")
            {
                rol.rol_prequiebra = "N";
                quiebraDeudor = "S";
            }
            else
            {
                rol.rol_prequiebra = "N";
                quiebraDeudor = "N";
            }

            rol.rol_tipo_rol = model.TipoRol;
            rol.rol_numero = model.Rol;
            rol.rol_trbid = Int32.Parse(model.Tribunal);
            rol.rol_tcaid = Int32.Parse(model.TipoCausa);
            rol.rol_comentario = model.Comentario;
            rol.rol_fecrol = model.FechaRol;
            rol.rol_pclid = model.Pclid;
            rol.rol_ctcid = model.Ctcid;
            rol.rol_bloqueo = "N";
            rol.ctc_rut = model.RutDeudor;

            var tuple = bcp.PanelDemanda.InsertarAvanceDemandaRol(rol, objSession, Int32.Parse(model.MateriaJudicial), panelId, fechaTribunales, quiebraDeudor, flagEnviaFechaEntrega, fechaEntrega);

            return Json(new { success = tuple.Item1, rolId = tuple.Item2, rol = rol.rol_numero }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarAvanceDemandaRolPrevisional(RolModel model, int panelId, DateTime? fechaTribunales, bool flagEnviaFechaEntrega, DateTime? fechaEntrega)
        {
            string quiebraDeudor = "N";
            dto.Rol rol = new dto.Rol();

            if (model.ComboQuiebra == "P")
            {
                rol.rol_prequiebra = "S";
                quiebraDeudor = "N";
            }
            else if (model.ComboQuiebra == "S")
            {
                rol.rol_prequiebra = "N";
                quiebraDeudor = "S";
            }
            else
            {
                rol.rol_prequiebra = "N";
                quiebraDeudor = "N";
            }

            rol.rol_tipo_rol = model.TipoRol;
            rol.rol_numero = model.Rol;
            rol.rol_trbid = Int32.Parse(model.Tribunal);
            rol.rol_tcaid = Int32.Parse(model.TipoCausa);
            rol.rol_comentario = model.Comentario;
            rol.rol_fecrol = model.FechaRol;
            rol.rol_pclid = model.Pclid;
            rol.rol_ctcid = model.Ctcid;
            rol.rol_bloqueo = "N";
            rol.ctc_rut = model.RutDeudor;

            var tuple = bcp.PanelDemanda.InsertarAvanceDemandaRolPrevisional(rol, objSession, Int32.Parse(model.MateriaJudicial), panelId, fechaTribunales, quiebraDeudor, flagEnviaFechaEntrega, fechaEntrega);

            return Json(new { success = tuple.Item1, rolId = tuple.Item2, rol = rol.rol_numero }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarAvanceDemandaMasivaRol(RolModel model, int panelId, DateTime? fechaTribunales, bool flagEnviaFechaEntrega, DateTime? fechaEntrega)
        {
            string quiebraDeudor = "N";
            dto.Rol rol = new dto.Rol();
            if (model.ComboQuiebra == "P")
            {
                rol.rol_prequiebra = "S";
                quiebraDeudor = "N";
            }
            else if (model.ComboQuiebra == "S")
            {
                rol.rol_prequiebra = "N";
                quiebraDeudor = "S";
            }
            else
            {
                rol.rol_prequiebra = "N";
                quiebraDeudor = "N";
            }

            rol.rol_tipo_rol = model.TipoRol;
            rol.rol_numero = model.Rol;
            rol.rol_trbid = Int32.Parse(model.Tribunal);
            rol.rol_tcaid = Int32.Parse(model.TipoCausa);
            rol.rol_comentario = model.Comentario;
            rol.rol_fecrol = model.FechaRol;
            rol.rol_pclid = model.Pclid;
            rol.rol_ctcid = model.Ctcid;
            rol.rol_bloqueo = "N";
            rol.ctc_rut = model.RutDeudor;

            var tuple = bcp.PanelDemanda.InsertarAvanceDemandaMasivaRol(rol, objSession, Int32.Parse(model.MateriaJudicial), panelId, fechaTribunales, quiebraDeudor, flagEnviaFechaEntrega, fechaEntrega);

            return Json(new { success = tuple.Item1, rolId = tuple.Item2, rol = rol.rol_numero }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPanelDemandaControlGestion()
        {
            return Json(bcp.PanelDemanda.ListarPanelDemandaControlGestion(objSession.CodigoEmpresa));
        }

        public JsonResult ListarPanelDemandaControlGestionMasivas()
        {
            return Json(PanelDemanda.ListarPanelDemandaControlGestionMasivas(objSession.CodigoEmpresa));
        }

        public ActionResult PanelDemandaOrgChart()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            List<dto.PanelAlerta> lst = bcp.PanelAlerta.ListarPanelAlerta(objSession.CodigoEmpresa);
            foreach (dto.PanelAlerta p in lst)
            {
                ViewBag.DemandasProceso = p.DemandasProceso;
                ViewBag.PromedioConfeccionDias = p.PromedioConfeccionDias;
                ViewBag.CantCorrecciones = p.CantCorrecciones;
                ViewBag.PromedioCorrecciones = p.PromedioCorrecciones;
                ViewBag.TraspasosAprobados = p.TraspasosAprobados;
            }

            return View();
        }

        public ActionResult PanelDemandaMasivaOrgChart()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            List<dto.PanelAlerta> lst = bcp.PanelAlerta.ListarPanelAlertaMasiva(objSession.CodigoEmpresa);
            foreach (dto.PanelAlerta p in lst)
            {
                ViewBag.DemandasProceso = p.DemandasProceso;
                ViewBag.PromedioConfeccionDias = p.PromedioConfeccionDias;
            }

            return View();
        }

        public ActionResult ListarPanelDemandaReporte(GridSettings gridSettings, int reporteId)
        {
            // create json data 
            int totalRecords = bcp.PanelDemanda.ListarPanelDemandaReporteCount(reporteId, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelDemandaReporte> lst = bcp.PanelDemanda.ListarPanelDemandaReporte(reporteId, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.PanelDemandaReporte item in lst
                    select new
                    {
                        id = item.PanelId,
                        cell = new object[]
                        {
                            item.PanelId,
                            item.Deudor,
                            item.Asegurado,
                            item.FechaAsignacion,
                            item.FechaAprobacionTraspaso,
                            item.IngresoJudicial,
                            item.FechaEnvio,
                            item.FechaIngresoTribunal,
                            item.FechaEntrega ,
                            item.Correcciones,
                            item.CountCorrecciones,
                            item.DiasTranscurso,
                            item.Encargado
                        }
                    }
                ).ToArray()
            };

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EnviarReporteId(int reporteId)
        {
            return Json(reporteId);
        }
        public ActionResult ListarPanelDemandaReporteOrgChartItem(GridSettings gridSettings, int reporteId)
        {
            // create json data 


            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelDemandaReporte> lst = bcp.PanelDemanda.ListarPanelDemandaReporteOrgChartItem(reporteId);
            int totalPages = (int)Math.Ceiling((float)lst.Count() / (float)gridSettings.pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = lst.Count(),

                rows =
                (
                    from dto.PanelDemandaReporte item in lst
                    select new
                    {
                        id = item.PanelId,
                        cell = new object[]
                        {
                            item.PanelId,
                            item.Cliente,
                            item.RutDeudor,
                            item.Deudor,
                            item.Asegurado,
                            item.FechaAsignacion,
                            item.FechaAprobacionTraspaso,
                            item.IngresoJudicial,
                            item.FechaEnvio,
                            item.FechaIngresoTribunal,
                            item.FechaEntrega ,
                            item.Correcciones,
                            item.CountCorrecciones,
                            item.DiasTranscurso,
                            item.Encargado,
                            item.DiasTranscurso2,
                            item.Comentarios,
                            item.TrackingDemanda
                        }
                    }
                ).ToArray()
            };

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPanelDemandaMasivaReporteOrgChartItem(GridSettings gridSettings, int reporteId)
        {
            // create json data 


            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelDemandaReporte> lst = bcp.PanelDemanda.ListarPanelDemandaMasivaReporteOrgChartItem(reporteId);
            int totalPages = (int)Math.Ceiling((float)lst.Count() / (float)gridSettings.pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = lst.Count(),

                rows =
                (
                    from dto.PanelDemandaReporte item in lst
                    select new
                    {
                        id = item.PanelId,
                        cell = new object[]
                        {
                            item.PanelId,
                            item.Cliente,
                            item.RutDeudor,
                            item.Deudor,
                            item.Asegurado,
                            item.FechaAsignacion,
                            item.FechaAprobacionTraspaso,
                            item.IngresoJudicial,
                            item.FechaEnvio,
                            item.FechaIngresoTribunal,
                            item.FechaEntrega ,
                            item.Correcciones,
                            item.CountCorrecciones,
                            item.DiasTranscurso,
                            item.Encargado,
                            item.DiasTranscurso2,
                            item.Comentarios,
                            item.TrackingDemanda
                        }
                    }
                ).ToArray()
            };

            return Json(lst, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ExportToExcelPanelDemandas(GridSettings gridSettings)
        {

            var grid = new GridView();
            List<dto.PanelDemandaExcel> lst = bcp.PanelDemanda.ListarPanelDemandasExcel(objSession.CodigoEmpresa);
            grid.DataSource = lst;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=PanelDemandas.xls");
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
        public ActionResult ExportToExcelPanelDemandasMasivas(GridSettings gridSettings)
        {

            var grid = new GridView();
            List<dto.PanelDemandaMasivaExcel> lst = bcp.PanelDemanda.ListarPanelDemandasExcelMasiva(objSession.CodigoEmpresa);
            grid.DataSource = lst;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=PanelDemandasMasivas.xls");
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
        public ActionResult ExportToExcelPanelDemandasPrevisional(GridSettings gridSettings)
        {
            var grid = new GridView();
            List<dto.PanelDemandaExcel> lst = bcp.PanelDemanda.ListarPanelDemandasExcelPrevisional(objSession.CodigoEmpresa);
            grid.DataSource = lst;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=PanelDemandasPrevisionales.xls");
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

        public JsonResult GuardarCursoDemanda(int panelId, int cursoDemanda, string motivo)
        {
            int grabar = 0;

            grabar = bcp.PanelDemanda.GrabarCursoDemanda(objSession.CodigoEmpresa, panelId, cursoDemanda, motivo, objSession.UserId);
            if (grabar > 0)
            {
                return Json(grabar);
            }
            else
            {
                return Json("No fue posible ingresar el avance");
            }

        }
        public JsonResult GuardarCursoDemandaPrevisional(int panelId, int cursoDemanda, string motivo)
        {
            int grabar = 0;

            grabar = bcp.PanelDemanda.GrabarCursoDemandaPrevisional(objSession.CodigoEmpresa, panelId, cursoDemanda, motivo, objSession.UserId);
            if (grabar > 0)
            {
                return Json(grabar);
            }
            else
            {
                return Json("No fue posible ingresar el avance");
            }

        }

        public JsonResult GuardarCursoDemandaMasiva(int panelId, int cursoDemanda, string motivo)
        {
            int grabar = 0;

            grabar = bcp.PanelDemanda.GrabarCursoDemandaMasiva(objSession.CodigoEmpresa, panelId, cursoDemanda, motivo, objSession.UserId);
            if (grabar > 0)
            {
                return Json(grabar);
            }
            else
            {
                return Json("No fue posible ingresar el avance");
            }

        }

        public JsonResult ListarDocumentosPanelId(GridSettings gridSettings, int panelId)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.DocumentosPanel> lst = bcp.PanelDemanda.ListarDocumentosPanelId(objSession.CodigoEmpresa, panelId, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentosPanel item in lst
                    select new
                    {

                        id = item.PanelId + "|" + item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid,
                        cell = new object[]
                        {
                           item.Numero,
                           item.TipoDocumento,
                           item.FechaAsignacion,
                           item.FechaVencimiento,
                           item.Monto ,
                           item.Saldo,
                           item.Estado,
                           item.Asegurado
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarDocumentosPanelPrevisionalId(GridSettings gridSettings, int panelId)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.DocumentosPanel> lst = bcp.PanelDemanda.ListarDocumentosPanelPrevisionalId(objSession.CodigoEmpresa, panelId, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentosPanel item in lst
                    select new
                    {

                        id = item.PanelId + "|" + item.Pclid + "|" + item.Ctcid,
                        cell = new object[]
                        {
                           item.NumResolucion,
                           item.FecResolucion,
                           item.Monto,
                           item.Saldo,
                           item.Estado
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarDocumentosMasivosPanelId(GridSettings gridSettings, int panelId)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.DocumentosPanel> lst = bcp.PanelDemanda.ListarDocumentosMasivosPanelId(objSession.CodigoEmpresa, panelId, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DocumentosPanel item in lst
                    select new
                    {

                        id = item.PanelId + "|" + item.Pclid + "|" + item.Ctcid + "|" + item.Ccbid,
                        cell = new object[]
                        {
                           item.Numero,
                           item.TipoDocumento,
                           item.FechaAsignacion,
                           item.FechaVencimiento,
                           item.Monto ,
                           item.Saldo,
                           item.Estado,
                           item.Asegurado
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Panel de Alerta"
        public ActionResult ListarPanelAlertaAnalisisCliente(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = bcp.PanelAlerta.ListarPanelAlertaAnalisisClienteCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelAlertaAnalisisCliente> lst = bcp.PanelAlerta.ListarPanelAlertaAnalisisCliente(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.PanelAlertaAnalisisCliente item in lst
                    select new
                    {
                        id = item.Pclid,
                        cell = new object[]
                        {
                            item.Pclid,
                            item.Cliente,
                            item.CantDemandas,
                            item.Saldo,
                            item.Porcentaje,
                            item.Percentage
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarPanelAlertaEncargado(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = bcp.PanelAlerta.ListarPanelAlertaEncargadoCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelAlertaEncargado> lst = bcp.PanelAlerta.ListarPanelAlertaEncargado(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.PanelAlertaEncargado item in lst
                    select new
                    {
                        id = item.UsrId,
                        cell = new object[]
                        {
                            item.UsrId,
                            item.Encargado,
                            item.CantDemandas,
                            item.Saldo,
                            item.CantCorrecciones,
                            item.TiempoEntrega
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPanelAlertaTipo(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelAlertaTipo> lst = bcp.PanelAlerta.ListarPanelAlertaTipo(objSession.CodigoEmpresa);
            int totalPages = (int)Math.Ceiling((float)lst.Count() / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = lst.Count(),

                rows =
                (
                    from dto.PanelAlertaTipo item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Item,
                            item.PromedioDias,
                            item.CantCasos,
                            item.Atraso
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPanelAlertaTipoMasiva(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelAlertaTipo> lst = bcp.PanelAlerta.ListarPanelAlertaTipoMasiva(objSession.CodigoEmpresa);
            int totalPages = (int)Math.Ceiling((float)lst.Count() / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = lst.Count(),

                rows =
                (
                    from dto.PanelAlertaTipo item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Item,
                            item.PromedioDias,
                            item.CantCasos,
                            item.Atraso
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPanelAlertaTipoReporte(GridSettings gridSettings, int TipoReporte)
        {


            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelAlertaTipoReporte> lst = bcp.PanelAlerta.ListarPanelAlertaTipoReporte(objSession.CodigoEmpresa, TipoReporte, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);
            int totalPages = (int)Math.Ceiling((float)lst.Count / (float)gridSettings.pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = lst.Count,

                rows =
                (
                    from dto.PanelAlertaTipoReporte item in lst
                    select new
                    {
                        id = item.PanelId,
                        cell = new object[]
                        {
                            item.PanelId,
                            item.Cliente,
                            item.Deudor,
                            item.Asegurado,
                            item.FechaAprobacionTraspaso,
                            item.IngresoJudicial,
                            item.FechaEnvio,
                            item.Encargado,
                            item.FechaEntrega,
                            item.FechaIngresoTribunal,
                            item.Correcciones,
                            item.CountCorrecciones,
                            item.Comentarios,
                            item.DiasTranscurso,
                            item.DiasAtraso
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarPanelAlertaReporteAnalisisCliente(GridSettings gridSettings, int Pclid)
        {


            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.PanelAlertaReporteAnalisisCliente> lst = bcp.PanelAlerta.ListarPanelAlertaReporteAnalisisCliente(objSession.CodigoEmpresa, Pclid, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);
            int totalPages = (int)Math.Ceiling((float)lst.Count / (float)gridSettings.pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = lst.Count,

                rows =
                (
                    from dto.PanelAlertaReporteAnalisisCliente item in lst
                    select new
                    {
                        id = item.PanelId,
                        cell = new object[]
                        {
                            item.PanelId,
                            item.Pclid,
                            item.Cliente,
                            item.Deudor,
                            item.Asegurado,
                            item.FechaAprobacionTraspaso,
                            item.IngresoJudicial,
                            item.FechaEnvio,
                            item.Encargado,
                            item.FechaEntrega,
                            item.FechaIngresoTribunal,
                            item.Correcciones,
                            item.CountCorrecciones,
                            item.Comentarios,
                            item.TipoDocumento,
                            item.Saldo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Bloqueo Rol "

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult BloquearRol(RolModel model)
        {
            int exito = -1;
            if (objSession.PrfId == 1 || objSession.PrfId == 6 || objSession.PrfId == 9 || objSession.PrfId == 13)
            {
                exito = bcp.Rol.BloquearRol(objSession.CodigoEmpresa, model.Rolid, model.BloquearRol ? "S" : "N", objSession.UserId);
            }
            return Json(exito, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region "Deudor Quiebra"
        public ActionResult DeudorQuiebra()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TipoCausa = new SelectList(bcp.Rol.ListarTiposCausa(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.MateriaJudicial = new SelectList(bcp.MateriaEstado.ListarMateriasCombo(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");

            return View();
        }
        public ActionResult ListarDeudoresQuiebra(GridSettings gridSettings)
        {
            // create json data 
            int totalRecords = bcp.DeudorQuiebra.ListarDeudoresQuiebraCount(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.DeudorQuiebra> lst = bcp.DeudorQuiebra.ListarDeudoresQuiebra(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.DeudorQuiebra item in lst
                    select new
                    {
                        id = item.Rut,
                        cell = new object[]
                        {
                            item.TribunalId,
                            item.TipoCausaId,
                            item.MateriaJodicialId,
                            item.Rut,
                            item.Deudor,
                            item.RolNumero,
                            item.Tribunal,
                            item.Causa,
                            item.Materia,
                            item.Rut
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarDeudorQuiebra(RolModel model)
        {
            int result = -1;
            string mensaje = string.Empty;
            result = bcp.DeudorQuiebra.InsertarDeudorQuiebra(objSession.CodigoEmpresa, model.RutDeudor, model.NombreDeudor, model.Rol, Int32.Parse(model.Tribunal), model.TipoCausa, model.MateriaJudicial, objSession.UserId);
            if (result > 1)
            {
                mensaje = "Registro Almacenado con exito";
            }
            else
            {
                mensaje = "Error al guardar el Deudor";
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult TraeDeudorQuiebra(string rutDeudor)
        {
            List<dto.DeudorQuiebra> lst = bcp.DeudorQuiebra.ListarDeudorQuiebra(objSession.CodigoEmpresa, rutDeudor);

            int TribunalId = 0;
            int TipoCausaId = 0;
            int MateriaJodicialId = 0;
            string RolNumero = string.Empty;
            string Tribunal = string.Empty;
            bool encontrado = false;

            foreach (dto.DeudorQuiebra datos in lst)
            {
                TribunalId = datos.TribunalId;
                TipoCausaId = datos.TipoCausaId;
                MateriaJodicialId = datos.MateriaJodicialId;
                RolNumero = datos.RolNumero;
                Tribunal = datos.Tribunal;
                encontrado = true;
                break;
            }

            return Json(new { success = encontrado, tribunalId = TribunalId, tipoCausaId = TipoCausaId, materiaJodicialId = MateriaJodicialId, rolNumero = RolNumero, tribunal = Tribunal }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Panel de quiebras"
        public JsonResult ListarRolLiquidacionGrilla(GridSettings gridSettings, int Rolid)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.LiquidacionRol> lst = bcp.PanelQuiebra.ListarRolLiquidacionGrilla(objSession.CodigoEmpresa, Rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.LiquidacionRol item in lst
                    where item.row >= startRow && item.row <= endRow
                    select new
                    {

                        id = item.Quiebra_Id,
                        cell = new object[]
                        {
                           item.Quiebra_Id,
                           item.Rol,
                           item.Nombre,
                           item.Cuantia,
                           item.Materia

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AvancePanelQuiebra(string rowId, int materia)
        {
            //string[] split = rowId.Split('_');
            //10	PROCEDIMENTO CONCURSAL DE REORGANIZACION
            //9	PROCEDIMIENTO CONCURSAL DE LIQUIDACION
            //3	QUIEBRA

            ViewData["RowId"] = rowId;
            dto.PanelQuiebraAvance obj = new dto.PanelQuiebraAvance();
            obj = bcp.PanelQuiebra.BuscarDetalleQuiebra(Int32.Parse(rowId));
            ViewData["QuiebraId"] = Int32.Parse(rowId);
            List<Combobox> lstSolicitante = new List<Combobox>();
            lstSolicitante.Add(new Combobox { Text = "No", Value = "N" });
            lstSolicitante.Add(new Combobox { Text = "Si", Value = "S" });



            List<Combobox> lstApruebaRechaza = new List<Combobox>();
            lstApruebaRechaza.Add(new Combobox { Text = "Aprobar", Value = "A" });
            lstApruebaRechaza.Add(new Combobox { Text = "Rechazar", Value = "R" });
            lstApruebaRechaza.Add(new Combobox { Text = "--", Value = "" });
            string apruebaRechaza = string.IsNullOrEmpty(obj.StatusAcuerdo) ? "" : obj.StatusAcuerdo;
            ViewBag.ComboApruebaRechaza = new SelectList(lstApruebaRechaza, "Value", "Text", apruebaRechaza);


            if (materia == 9 || materia == 3)
            {
                //si no trae valor el solicitante, por defecto es si
                string solicitante = string.IsNullOrEmpty(obj.Solicitante) ? "" : obj.Solicitante;
                if (string.IsNullOrEmpty(solicitante))
                {
                    ViewBag.ComboSolicitante = new SelectList(lstSolicitante, "Value", "Text", "S");
                }
                else
                {
                    ViewBag.ComboSolicitante = new SelectList(lstSolicitante, "Value", "Text", solicitante);
                }

                ViewBag.Display = "display:block";
                ViewBag.IsReorganizacion = "display:none";
            }
            else
            {
                lstSolicitante.Add(new Combobox { Text = "--", Value = "" });
                ViewBag.ComboSolicitante = new SelectList(lstSolicitante, "Value", "Text", "");
                ViewBag.Display = "display:none";
                ViewBag.IsReorganizacion = "display:block";
            }

            ViewBag.MtoCostasPersonales = Decimal.ToInt32(Math.Round(obj.MtoCostasPersonales));
            ViewBag.fecCostasPersonales = obj.FecCostasPersonales == new DateTime() ? "" : obj.FecCostasPersonales.ToShortDateString();
            ViewBag.fecIngresoSolicitud = obj.FecIngresoSolicitud == new DateTime() ? "" : obj.FecIngresoSolicitud.ToShortDateString();
            ViewBag.fecNotificacionSolicitud = obj.FecNotificacionSolicitud == new DateTime() ? "" : obj.FecNotificacionSolicitud.ToShortDateString();
            ViewBag.fecAudienciaInicial = obj.FecAudienciaInicial == new DateTime() ? "" : obj.FecAudienciaInicial.ToShortDateString();
            ViewBag.fecAudienciaPrueba = obj.FecAudienciaPrueba == new DateTime() ? "" : obj.FecAudienciaPrueba.ToShortDateString();
            ViewBag.fecAudienciaFallo = obj.FecAudienciaFallo == new DateTime() ? "" : obj.FecAudienciaFallo.ToShortDateString();
            ViewBag.fecResolucionLiquidacion = obj.FecResolucionLiquidacion == new DateTime() ? "" : obj.FecResolucionLiquidacion.ToShortDateString();
            ViewBag.fecResolucionLiquidacionBC = obj.FecResolucionLiquidacionBC == new DateTime() ? "" : obj.FecResolucionLiquidacionBC.ToShortDateString();
            ViewBag.fecResolucionReorganizacionBC = obj.FecResolucionReorganizacionBC == new DateTime() ? "" : obj.FecResolucionReorganizacionBC.ToShortDateString();
            ViewBag.fecVerificacion = obj.FecVerificacion == new DateTime() ? "" : obj.FecVerificacion.ToShortDateString();
            ViewBag.fecAcreditacionPoder = obj.FecAcreditacionPoder == new DateTime() ? "" : obj.FecAcreditacionPoder.ToShortDateString();
            ViewBag.fecJuntaConstitutiva = obj.FecJuntaConstitutiva == new DateTime() ? "" : obj.FecJuntaConstitutiva.ToShortDateString();
            ViewBag.fecJuntaDeliberativa = obj.FecJuntaDeliberativa == new DateTime() ? "" : obj.FecJuntaDeliberativa.ToShortDateString();

            ViewBag.fecAcuerdo = obj.FecAcuerdo == new DateTime() ? "" : obj.FecAcuerdo.ToShortDateString();
            ViewBag.fecVerificadoAcreditado = obj.FecVerificadoAcreditado == new DateTime() ? "" : obj.FecVerificadoAcreditado.ToShortDateString();
            ViewBag.fecNomCreditoVerificado = obj.FecNomCreditoVerificado == new DateTime() ? "" : obj.FecNomCreditoVerificado.ToShortDateString();
            ViewBag.fecImpugnacion = obj.FecImpugnacion == new DateTime() ? "" : obj.FecImpugnacion.ToShortDateString();
            ViewBag.fecNomCreditoReconocido = obj.FecNomCreditoReconocido == new DateTime() ? "" : obj.FecNomCreditoReconocido.ToShortDateString();
            ViewBag.fecSolAntecedente = obj.FecSolicitudAntecedente == new DateTime() ? "" : obj.FecSolicitudAntecedente.ToShortDateString();
            ViewBag.fecRecepcionAntecedente = obj.FecRecepcionAntecedente == new DateTime() ? "" : obj.FecRecepcionAntecedente.ToShortDateString();
            ViewBag.fecEnvioAntecedente = obj.FecEnvioAntecedente == new DateTime() ? "" : obj.FecEnvioAntecedente.ToShortDateString();
            ViewBag.fecEmisionND = obj.FecEmisionND == new DateTime() ? "" : obj.FecEmisionND.ToShortDateString();
            ViewBag.MtoEmision = Decimal.ToInt32(Math.Round(obj.MtoEmision));
            ViewBag.fecRepartos = obj.FecRepartos == new DateTime() ? "" : obj.FecRepartos.ToShortDateString();
            ViewBag.MtoRepartos = Decimal.ToInt32(Math.Round(obj.MtoRepartos));
            ViewBag.fecDevolucion = obj.FecDevolucion == new DateTime() ? "" : obj.FecDevolucion.ToShortDateString();
            ViewBag.PgoCostasPersonales = Decimal.ToInt32(Math.Round(obj.PgoCostasPersonales));
            ViewBag.fecPgoCostasPersonales = obj.FecPgoCostasPersonales == new DateTime() ? "" : obj.FecPgoCostasPersonales.ToShortDateString();
            ViewBag.fecAprobacionCtaFinal = obj.FecAprobacionCtaFinal == new DateTime() ? "" : obj.FecAprobacionCtaFinal.ToShortDateString();
            ViewBag.fecCertificadoIncobrable = obj.FecCertificadoIncobrable == new DateTime() ? "" : obj.FecCertificadoIncobrable.ToShortDateString();

            return View("_AgregarAvancePanelQuiebra");

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarAvancePanelQuiebra(int rolId, string quiebraId, string solicitante, string mtoCostasPersonales, string fecCostasPersonales, string fecIngrSolicitud, string fecNotSolicitud,
                                                        string fecAudienciaIni, string fecAudienciaPrueba, string fecAudienciaFallo, string fecResolLiqui, string fecResolLiquiBC,
                                                        string fecResolReorgBC, string fecVerificacion, string fecAcreditaPoder, string fecJuntaConsti, string fecJuntaDelibe,
                                                        string statusAcuerdo, string fecAcuerdo, string fecVerificaAcredita, string fecNomCreditoVeri, string fecImpugnacion, string fecNomCreditoRec,
                                                        string fecSolAntecedente, string fecRecepAntecedente, string fecEnvAntecedente, string fecEmisionND, string mtoEmisionND,
                                                        string fecRepartos, string MtoRepartos, string fecDevolucion, string pgoCostPersonales, string fecpgoCostPersonales,
                                                        string fecAprobCtaFinal, string fecCertiIncobrable)
        {
            int result = -1;
            result = bcp.PanelQuiebra.InsertUpdateAvancePanelQuiebra(rolId, Int32.Parse(quiebraId), solicitante, mtoCostasPersonales, fecCostasPersonales, fecIngrSolicitud, fecNotSolicitud,
                                                        fecAudienciaIni, fecAudienciaPrueba, fecAudienciaFallo, fecResolLiqui, fecResolLiquiBC,
                                                        fecResolReorgBC, fecVerificacion, fecAcreditaPoder, fecJuntaConsti, fecJuntaDelibe,
                                                        statusAcuerdo, fecAcuerdo, fecVerificaAcredita, fecNomCreditoVeri, fecImpugnacion, fecNomCreditoRec,
                                                        fecSolAntecedente, fecRecepAntecedente, fecEnvAntecedente, fecEmisionND, mtoEmisionND,
                                                        fecRepartos, MtoRepartos, fecDevolucion, pgoCostPersonales, fecpgoCostPersonales,
                                                        fecAprobCtaFinal, fecCertiIncobrable, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPanelQuiebraGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.PanelQuiebra> lst = bcp.PanelQuiebra.ListarPanelQuiebraGrilla(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.PanelQuiebra item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.QuiebraId,
                        cell = new object[]
                        {
                           item.RolNumero,
                           item.Cliente,
                            item.RutDeudor,
                           item.Deudor,
                           item.Asegurado,
                           item.Cuantia,
                           item.FechaIngresoQuiebra,
                           item.Materia,
                           item.Solicitante,
                           item.MtoCostasPersonales,
                           item.FecCostasPersonales,
                           item.FecIngresoSolicitud,
                           item.FecNotificacionSolicitud ,
                           item.FecAudienciaInicial,
                           item.FecAudienciaPrueba,
                           item.FecAudienciaFallo,
                           item.FecResolucionLiquidacion,
                           item.FecResolucionLiquidacionBC,
                           item.FecResolucionReorganizacionBC,
                           item.FecVerificacion,
                           item.FecAcreditacionPoder,
                           item.FecJuntaConstitutiva,
                           item.FecJuntaDeliberativa,
                           item.StatusAcuerdo,
                           item.FecAcuerdo,
                           item.FecVerificadoAcreditado ,
                           item.FecNomCreditoVerificado,
                           item.FecImpugnacion,
                           item.FecNomCreditoReconocido,
                           item.FecSolicitudAntecedente,
                           item.FecRecepcionAntecedente,
                           item.FecEnvioAntecedente,
                           item.FecEmisionND,
                           item.MtoEmision,
                           item.FecRepartos,
                           item.MtoRepartos,
                           item.FecDevolucion,
                           item.PgoCostasPersonales,
                           item.FecPgoCostasPersonales,
                           item.FecAprobacionCtaFinal,
                           item.FecCertificadoIncobrable,
                           item.NombreTribunal,


                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PanelQuiebra()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        public ActionResult ExportToExcelPanelQuiebras(GridSettings gridSettings)
        {

            var grid = new GridView();

            List<dto.PanelQuiebra> lst = bcp.PanelQuiebra.ListarPanelQuiebraGrilla(objSession.CodigoEmpresa, objSession.Idioma, "", "QuiebraId", "asc");
            grid.DataSource = lst;
            grid.DataBind();

            //Enacabezado de tribunal
            var Tribunal = grid.HeaderRow.Cells[7];
            grid.HeaderRow.Cells.RemoveAt(7);
            grid.HeaderRow.Cells.AddAt(4, Tribunal);

            var RutDeudor = grid.HeaderRow.Cells[7];
            grid.HeaderRow.Cells.RemoveAt(7);
            grid.HeaderRow.Cells.AddAt(6, RutDeudor);

            foreach (GridViewRow r in grid.Rows)
            {
                //Tribunal
                var cel = r.Cells[7];
                r.Cells.RemoveAt(7);
                r.Cells.AddAt(4, cel);

                //Deudor
                var deu = r.Cells[7];
                r.Cells.RemoveAt(7);
                r.Cells.AddAt(6, deu);
            }

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=PanelQuiebras.xls");
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

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertUpdatePanelQuiebraSindico(int rolId, string sindico, string veedor, string interventor)
        {
            int result = -1;
            result = bcp.PanelQuiebra.InsertUpdatePanelQuiebraSindico(objSession.CodigoEmpresa, rolId, sindico, veedor, interventor, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDatosRol(string idd)
        {
            dto.RolCarga obj = new dto.RolCarga();
            int rolid = 0;
            if (Int32.TryParse(idd, out rolid))
            {
                obj = bcp.Rol.BuscarRol(objSession.CodigoEmpresa, rolid, objSession.Idioma);
                ViewBag.MateriaJudicial = obj.MateriaJudicial;
                ViewBag.Estado = obj.Estado;
            }
            ViewBag.EstadoHistorial = new SelectList(bcp.MateriaEstado.ListarEstadosCombo(objSession.CodigoEmpresa, objSession.Idioma, 0), "Value", "Text", "");
            ViewBag.MateriaHistorial = new SelectList(bcp.MateriaEstado.ListarMateriasCombo(objSession.CodigoEmpresa, objSession.Idioma), "Value", "Text", "");


            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarPanelQuiebraRepartos(GridSettings gridSettings, int quiebraId)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.PanelQuiebraReparto> lst = bcp.PanelQuiebra.ListarPanelQuiebraRepartos(quiebraId, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.PanelQuiebraReparto item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.RepartoId,
                        cell = new object[]
                        {
                           item.RepartoId,
                           item.FecReparto,
                           item.MtoReparto

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertPanelQuiebraReparto(int quiebraId, string fecReparto, string MtoReparto)
        {
            int result = -1;
            result = bcp.PanelQuiebra.InsertPanelQuiebraReparto(quiebraId, fecReparto, MtoReparto, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDatosPanelQuiebraItem(int quiebraId)
        {
            dto.PanelQuiebraAvance obj = new dto.PanelQuiebraAvance();
            obj = bcp.PanelQuiebra.BuscarDetalleQuiebra(quiebraId);
            ViewBag.fecRepartos = obj.FecRepartos == new DateTime() ? "" : obj.FecRepartos.ToShortDateString();
            ViewBag.MtoRepartos = Decimal.ToInt32(Math.Round(obj.MtoRepartos));

            return Json(new
            {
                FechaReparto = ViewBag.fecRepartos,
                MontoReparto = ViewBag.MtoRepartos
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "TraspasarAvenimiento"
        public ActionResult GetDatosAvenimiento(string idd)
        {
            dto.RolCarga obj = new dto.RolCarga();
            int rolid = 0;
            if (Int32.TryParse(idd, out rolid))
            {
                obj = bcp.Rol.BuscarRol(objSession.CodigoEmpresa, rolid, objSession.Idioma);
                bcp.Rol.BuscarRolDemandaAvenimiento(objSession.CodigoEmpresa, rolid, obj);
                ViewBag.FechaAvenimiento = obj.Avenimiento.Fecha == new DateTime() ? "" : obj.Avenimiento.Fecha.ToShortDateString();
                ViewBag.MontoAvenimiento = obj.Avenimiento.Monto.ToString("N2");
                ViewBag.CuotasAvenimiento = obj.Avenimiento.Cuotas;
                ViewBag.MontoCuotaAvenimiento = obj.Avenimiento.MontoCuota.ToString("N2");
                ViewBag.MontoUltimaCuotaAvenimiento = obj.Avenimiento.MontoUltimaCuota.ToString("N2");
                ViewBag.FechaPrimeraCuotaAvenimiento = obj.Avenimiento.FechaPrimeraCuota == new DateTime() ? "" : obj.Avenimiento.FechaPrimeraCuota.ToShortDateString();
                ViewBag.FechaUltimaCuotaAvenimiento = obj.Avenimiento.FechaUltimaCuota == new DateTime() ? "" : obj.Avenimiento.FechaUltimaCuota.ToShortDateString();
                ViewBag.InteresAvenimiento = obj.Avenimiento.Interes.ToString("N2");
            }

            return Json(new
            {
                FechaAvenimiento = ViewBag.FechaAvenimiento,
                MontoAvenimiento = ViewBag.MontoAvenimiento,
                CuotasAvenimiento = ViewBag.CuotasAvenimiento,
                MontoCuotaAvenimiento = ViewBag.MontoCuotaAvenimiento,
                MontoUltimaCuotaAvenimiento = ViewBag.MontoUltimaCuotaAvenimiento,
                FechaPrimeraCuotaAvenimiento = ViewBag.FechaPrimeraCuotaAvenimiento,
                FechaUltimaCuotaAvenimiento = ViewBag.FechaUltimaCuotaAvenimiento,
                InteresAvenimiento = ViewBag.InteresAvenimiento
            }, JsonRequestBehavior.AllowGet);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult InsertarPanelAvenimiento(int rolId, string rolNumero, int pclid, int ctcid, int tribunalId)
        {
            int result = -1;
            result = bcp.PanelAvenimiento.InsertarPanelAvenimiento(objSession.CodigoEmpresa, rolId, rolNumero, pclid, ctcid, tribunalId, objSession.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AprobarAvenimiento(int rolId, int pclid, int ctcid, string fechaAvenimiento, string montoAvenimiento, string cuotasAvenimiento,
                                                string montoCuotaAvenimiento, string montoUltimaCuotaAvenimiento, string fechaPrimeraCuotaAvenimiento,
                                                string fechaUltimaCuotaAvenimiento, string interesAvenimiento)
        {
            int result = -1;
            result = bcp.PanelAvenimiento.AprobarAvenimiento(objSession.CodigoEmpresa, objSession.Idioma, objSession.CodigoSucursal, rolId, pclid, ctcid, fechaAvenimiento,
                                                            montoAvenimiento, cuotasAvenimiento, montoCuotaAvenimiento, montoUltimaCuotaAvenimiento,
                                                            fechaPrimeraCuotaAvenimiento, fechaUltimaCuotaAvenimiento, interesAvenimiento, objSession.UserId,
                                                            objSession.IpRed, objSession.IpPc);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PanelAvenimiento()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        public JsonResult ListarPanelAvenimientoGrilla(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.PanelAvenimiento> lst = bcp.PanelAvenimiento.ListarPanelAvenimientoGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.PanelAvenimiento item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.RolId,
                        cell = new object[]
                        {
                           item.Rol,
                           item.Cliente,
                           item.Deudor,
                           item.Tribunal,
                           item.FechaTraspasoAvenimiento,
                           item.Pclid,
                           item.Ctcid,
                           item.RolId

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region "Panel Demonio"
        public ActionResult MonitoreoDemonioExterno()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.ZonaTribunales = new SelectList(bcp.PanelMonitoreoDemonio.ListarZonasTribunales(objSession.CodigoEmpresa, ""), "Value", "Text", "");
            List<dto.MonitoreoExternoCabecera> lst = bcp.PanelMonitoreoDemonio.ListarPanelMonitoreoExternoCabecera();
            foreach (dto.MonitoreoExternoCabecera p in lst)
            {
                ViewBag.Recolecto = p.Recolecto;
                ViewBag.CantCausas = p.CantCausas;
                ViewBag.CantMesActual = p.CantMesActual;

            }

            return View();
        }

        public ActionResult ListarPanelMonitoreoExternoDemandas(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.MonitoreoExternoDemanda> lst = bcp.PanelMonitoreoDemonio.ListarPanelMonitoreoExternoDemandas(gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());
            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MonitoreoExternoDemanda item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {
                        id = item.Pclid,
                        cell = new object[]
                        {
                            item.Cliente,
                            item.SaldoCartera,
                            item.SaldoSinDemanda,
                            item.PorSaldoSinDemanda,
                            item.SaldoDemandado,
                            item.PorSaldoDemandado,
                            item.SaldoDemandadoDosAnios,
                            item.PorSaldoDemandadoDosAnios
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarPanelMonitoreoExternoRol(GridSettings gridSettings, int zonaId)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.MonitoreoExternoRolBuscado> lst = bcp.PanelMonitoreoDemonio.ListarPanelMonitoreoExternoRol(objSession.CodigoEmpresa, zonaId, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());
            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MonitoreoExternoRolBuscado item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {
                        id = item.TribunalId,
                        cell = new object[]
                        {
                            item.Tribunal,
                            item.Anio,
                            item.MinRol,
                            item.MaxRol,
                            item.Encontrados,
                            item.NoEncontrados,
                            item.Porcentaje
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MonitoreoDemonioSii()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            List<dto.MonitoreoSiiCabecera> lst = bcp.PanelMonitoreoDemonio.ListarPanelMonitoreoSiiCabecera();
            foreach (dto.MonitoreoSiiCabecera p in lst)
            {
                ViewBag.Recolecto = p.Recolecto;
                ViewBag.CantRut = p.CantRut;
                ViewBag.CantMesActual = p.CantMesActual;
                ViewBag.Acumulativas = p.Acumulativas;
                ViewBag.FecUtimaActualizacion = p.FecUtimaActualizacion == new DateTime() ? "" : p.FecUtimaActualizacion.ToShortDateString(); ;
                ViewBag.FecProximaActualizacion = p.FecProximaActualizacion == new DateTime() ? "" : p.FecProximaActualizacion.ToShortDateString();
            }

            return View();
        }

        public ActionResult ListarPanelMonitoreoSiiClientes(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.MonitoreoSiiCliente> lst = bcp.PanelMonitoreoDemonio.ListarPanelMonitoreoSiiClientes(gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());
            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MonitoreoSiiCliente item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {
                        id = item.Pclid,
                        cell = new object[]
                        {
                            item.Cliente,
                            item.SaldoCartera,
                            item.SaldoVerde,
                            item.PorSaldoVerde,
                            item.SaldoAmarillo,
                            item.PorSaldoAmarillo,
                            item.SaldoRojo,
                            item.PorSaldoRojo
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MonitoreoDemonioInterno()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            List<dto.MonitoreoInternoCabecera> lst = bcp.PanelMonitoreoDemonio.ListarPanelMonitoreoInternoCabecera();
            foreach (dto.MonitoreoInternoCabecera p in lst)
            {
                ViewBag.CantCausasDiaAnterior = p.CantCausasDiaAnterior;
                ViewBag.CantDeudoresDiaAnterior = p.CantDeudoresDiaAnterior;
                ViewBag.SaldoDiaAnterior = p.SaldoDiaAnterior.ToString("N2");

                ViewBag.CantDeudoresJudicializado = p.CantDeudoresJudicializado;
                ViewBag.CantCausasJudicializadas = p.CantCausasJudicializadas;
                ViewBag.SaldoJudicializado = p.SaldoJudicializado.ToString("N2");

                ViewBag.CantDeudoresCausasActivas = p.CantDeudoresCausasActivas;
                ViewBag.CantCausasActivas = p.CantCausasActivas;
                ViewBag.SaldoCausaActiva = p.SaldoCausaActiva.ToString("N2");

                ViewBag.CantDeudoresCausasArchivadas = p.CantDeudoresCausasArchivadas;
                ViewBag.CantCausasArchivadas = p.CantCausasArchivadas;
                ViewBag.SaldoCausaArchivada = p.SaldoCausaArchivada.ToString("N2");

                ViewBag.CantDeudoresCausasArchivadas7dias = p.CantDeudoresCausasArchivadas7dias;
                ViewBag.CantCausaArchivada7Dias = p.CantCausaArchivada7Dias;
                ViewBag.SaldoCausaArchivada7Dias = p.SaldoCausaArchivada7Dias.ToString("N2");

                ViewBag.FecUtimaActualizacion = p.FecUtimaActualizacion == new DateTime() ? "" : p.FecUtimaActualizacion.ToShortDateString(); ;
            }

            return View();
        }

        public ActionResult ListarPanelMonitoreoInternoClientes(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.MonitoreoInternoCliente> lst = bcp.PanelMonitoreoDemonio.ListarPanelMonitoreoInternoClientes(gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());
            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.MonitoreoInternoCliente item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {
                        id = item.Pclid,
                        cell = new object[]
                        {
                            item.Cliente,
                            item.TotalCausas,
                            item.ActualizadasCount,
                            item.NoActualizadasCount,
                            item.Porcentaje,
                            item.ACount,
                            item.BCount,
                            item.CCount,
                            item.DCount
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Alerta Panel Quiebra"
        public ActionResult PanelQuiebraDiagrama()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }
        public JsonResult ListarPanelQuiebraGrafico()
        {
            return Json(bcp.PanelQuiebra.ListarPanelQuiebraGrafico(objSession.CodigoEmpresa));
        }
        public JsonResult ListarPanelQuiebraReporteLiquidaciones(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.PanelQuiebraPublicados> lst = bcp.PanelQuiebra.ListarPanelQuiebraReporteLiquidaciones(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.PanelQuiebraPublicados item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.QuiebraId,
                        cell = new object[]
                        {
                           item.Cliente,
                           item.Rut,
                           item.Deudor,
                           item.Asegurado,
                           item.FecPublicacion,
                           item.Dias

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarPanelQuiebraReporteReorganizaciones(GridSettings gridSettings)
        {
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            List<dto.PanelQuiebraPublicados> lst = bcp.PanelQuiebra.ListarPanelQuiebraReporteReorganizaciones(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());

            int totalRecords = lst.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (

                    from dto.PanelQuiebraPublicados item in lst
                    where item.Row >= startRow && item.Row <= endRow
                    select new
                    {

                        id = item.QuiebraId,
                        cell = new object[]
                        {
                           item.Cliente,
                           item.Rut,
                           item.Deudor,
                           item.Asegurado,
                           item.FecPublicacion,
                           item.Dias

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarPanelQuiebraProyeccion()
        {
            return Json(bcp.PanelQuiebra.ListarPanelQuiebraProyeccion(objSession.CodigoEmpresa));
        }
        #endregion
    }
}