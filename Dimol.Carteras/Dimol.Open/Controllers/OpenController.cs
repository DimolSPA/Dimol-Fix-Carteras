using Dimol.bcp;
using Dimol.dto;
using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dimol.Models;

namespace Dimol.Open.Controllers
{
    public class OpenController : Controller
    {
        Dimol.bcp.Usuarios usu = new Dimol.bcp.Usuarios();
        Funciones func = new Funciones();
        UserSession objSession = new UserSession();

        //Deudor objDeudor = new Deudor();
        public ActionResult Index()
        {
           if (Session["Usuario"] == null)
           {
                return RedirectToAction("Login", "Open");
           }
            objSession = (UserSession) Session["Usuario"];
            ViewBag.UsrName= func.Desencripta(objSession.Nombre);
            ViewBag.RutaLogo = objSession.UrlFoto;
            ViewBag.Prf = objSession.Cargo;

            List<Combobox> lstTipo = new List<Combobox>();
            lstTipo.Add(new Combobox { Text = "ACTIVO", Value = "0" });
            lstTipo.Add(new Combobox { Text = "INACTIVO", Value = "1" });
            ViewBag.Activo = new SelectList(lstTipo, "Value", "Text", "0");

            List<Combobox> lstPrf = new List<Combobox>();
            lstPrf.Add(new Combobox { Text = "SI", Value = "S" });
            lstPrf.Add(new Combobox { Text = "NO", Value = "N" });
            ViewBag.Perfil = new SelectList(lstPrf, "Value", "Text", "S");

            return View();
        }
        public ActionResult BuscarRutNombreDeudor(string term)
        {
            Dimol.Carteras.bcp.Deudor obj = new Dimol.Carteras.bcp.Deudor();
            return Json(obj.ListarRutNombreDeudor(term), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuscarRutNombreDeudorPJ(string term)
        {
            Dimol.Carteras.bcp.Deudor obj = new Dimol.Carteras.bcp.Deudor();
            return Json(obj.ListarRutNombreDeudorPJ(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarNombreDeudorPJ(string term)
        {
            Dimol.Carteras.bcp.Deudor obj = new Dimol.Carteras.bcp.Deudor();
            return Json(obj.ListarNombreDeudorPJ(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarRutDeudor(string term)
        {
            //bcp.Deudor objDeudor = new Deudor();
            Dimol.Carteras.bcp.Deudor objDeudor = new Dimol.Carteras.bcp.Deudor();
            return Json(objDeudor.ListarRutDeudor(term), JsonRequestBehavior.AllowGet);
        }


        public ActionResult BuscarRutNombreCliente(string term)
        {
            Dimol.Carteras.bcp.Cliente objCliente = new Dimol.Carteras.bcp.Cliente();
            return Json(objCliente.ListarRutNombreCliente(term), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Buscar(int ctcid)
        {
            Dimol.Carteras.bcp.Deudor objDeudor = new Dimol.Carteras.bcp.Deudor();
            objDeudor.CodigoEmpresa = 1;
            objDeudor.CodigoDeudor = ctcid;
            objDeudor.BuscarDeudor();
           
            return Json(objDeudor, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCausaRut(GridSettings gridSettings, string rut)
        {
            // create json data 

            List<dto.ConsultaPJ> lst = bcp.ConsultaPJ.ConsultarPorRut(rut.Trim());

            int totalRecords = lst.Count;

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
            
            lst = lst.Skip(startRow).Take(gridSettings.pageSize).ToList();

             var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.ConsultaPJ item in lst
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

        public ActionResult GetUserPJ(int userid, string button)
        {
            List<dto.UserPJ> lstUserPJ = bcp.ConsultaPJ.TraeUserPJ(userid, button);                      

            return Json(lstUserPJ, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TraeRutaLogoEmpresaPJ(int userid, string button)
        {
            List<dto.UserPJ> lstRutaLogoPJ = bcp.ConsultaPJ.TraeRutaLogoEmpresaPJ(userid, button);

            return Json(lstRutaLogoPJ, JsonRequestBehavior.AllowGet);
        }

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

        public JsonResult GetRol(GridSettings gridSettings, int ctcid)
        {
            // create json data 
            Dimol.Carteras.bcp.Deudor objDeudor = new Dimol.Carteras.bcp.Deudor();
            int totalRecords = objDeudor.ListarRolCount(1, ctcid, 1, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<Dimol.Carteras.dto.Rol> lst = objDeudor.ListarRol(1, ctcid, 1, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.Carteras.dto.Rol item in lst
                    select new
                    {
                        id = ctcid + "|" + item.Rolid,
                        cell = new object[]
                        {
                            item.Rolid,
                            item.Cliente,
                            item.Deudor,
                            item.NumeroRol,
                            item.Causa,
                            item.Tribunal,
                            item.Materia,
                            item.Estado
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalDocRol(int rolid)
        {
            Dimol.Carteras.bcp.Deudor objDeudor = new Dimol.Carteras.bcp.Deudor();
            List<Dimol.Carteras.dto.DocumentoRol> lst = objDeudor.ListarDocRol(1, rolid, 1, "", "Ccbid", "asc", 0, 111111111);
            decimal monto = lst.Sum(x => x.Monto);
            decimal saldo = lst.Sum(x => x.Saldo);

            var jsonData = new
            {
                monto = monto,//.ToString("N2"),
                saldo = saldo//.ToString("N2")
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocRol(GridSettings gridSettings, int rolid)
        {
            // create json data 
            Dimol.Carteras.bcp.Deudor objDeudor = new Dimol.Carteras.bcp.Deudor();
            int totalRecords = objDeudor.ListarDocRolCount(1, rolid, 1, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<Dimol.Carteras.dto.DocumentoRol> lst = objDeudor.ListarDocRol(1, rolid, 1, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.Carteras.dto.DocumentoRol item in lst
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

        public JsonResult GetArchivosRol(GridSettings gridSettings, int rolid, int ctcid)
        {
            // create json data 
            int totalRecords = 0;

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;


            List<Dimol.Carteras.dto.EstadosRol> lst = new List<Dimol.Carteras.dto.EstadosRol>();
            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.Carteras.dto.EstadosRol item in lst
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
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetEstados(GridSettings gridSettings, int rolid)
        {
            int totalRecords = Dimol.Judicial.Mantenedores.bcp.Rol.ListarEstadosRolGrillaCount(1, 1, rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<Dimol.Judicial.Mantenedores.dto.EstadosRol> lstEstados = Dimol.Judicial.Mantenedores.bcp.Rol.ListarEstadosRolGrilla(1, 1, rolid, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from Dimol.Judicial.Mantenedores.dto.EstadosRol item in lstEstados
                    select new
                    {

                        id = item.Id + "|" + item.Rolid + "|" + item.IdEstado + "|" + item.IdMateria + "|" + item.Fecha.ToString("MMddyyyyHHmmssfff"),
                        cell = new object[]
                        {
                           item.Rolid + "|" + item.IdEstado + "|" + item.IdMateria + "|" + item.Fecha.ToString("MMddyyyyHHmmssfff"),
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

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LogOff();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            DataSet dsUsr = new DataSet();
            Dimol.bcp.Menu objMenu = new Dimol.bcp.Menu();
            string err = "";
            //Session.LCID = 13322;
            try
            {
                err = bcp.ConsultaPJ.ValidaUsuario(model.UserName, model.Password, Request.ServerVariables["REMOTE_HOST"], Request.ServerVariables["REMOTE_ADDR"]);

                if (err != "")
                {
                    ModelState.AddModelError("", "El nombre de usuario o la contraseña especificados son incorrectos o la cuenta ya se encuentra en uso.");
                    return View(model);
                }

                //Session["Usuario"] = new UserSession();

                
                //ViewBag.NombreUsr = bcp.ConsultaPJ.TraeLoginPJ(model.UserName, Request.ServerVariables["REMOTE_HOST"], Request.ServerVariables["REMOTE_ADDR"]);                         
                objSession.Nombre = bcp.ConsultaPJ.TraeLoginPJ(model.UserName, Request.ServerVariables["REMOTE_HOST"], Request.ServerVariables["REMOTE_ADDR"]);
                objSession.UrlFoto = bcp.ConsultaPJ.TraeRutaLogo(model.UserName);
                objSession.Cargo = bcp.ConsultaPJ.TraePrf(model.UserName, Request.ServerVariables["REMOTE_HOST"], Request.ServerVariables["REMOTE_ADDR"]);
                Session["Usuario"] = objSession;

                /*   dsUsr = usu.Trae_DatUsuario(model.UserName);
                   objSession.CodigoEmpresa = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["usr_codemp"]);
                   objSession.NombreEmpresa = dsUsr.Tables[0].Rows[0]["nombre_empresa"].ToString();
                   objSession.CodigoSucursal = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["uss_sucid"]);
                   objSession.UserId = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["usr_usrid"]);
                   objSession.Nombre = dsUsr.Tables[0].Rows[0]["usr_nombre"].ToString();
                   objSession.Rut = Funciones.formatearRut(dsUsr.Tables[0].Rows[0]["epl_rut"].ToString());
                   objSession.PrfId = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["usr_prfid"]);
                   objSession.Permisos = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["usr_permisos"]);
                   objSession.Email = dsUsr.Tables[0].Rows[0]["usr_mail"].ToString();
                   objSession.IpRed = Request.ServerVariables["LOCAL_ADDR"];
                   objSession.IpPc = Request.ServerVariables["REMOTE_HOST"];
                   if (!string.IsNullOrEmpty(dsUsr.Tables[0].Rows[0]["gestor"].ToString()))
                   {
                       objSession.Gestor = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["gestor"].ToString());
                   }
                   else
                   {
                       objSession.Gestor = 0;
                   }
                   if (dsUsr.Tables[0].Rows[0]["epl_emplid"].ToString() == "")
                   {
                       objSession.EmplId = 0;
                   }
                   else
                   {
                       objSession.EmplId = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["epl_emplid"]);
                   }
                   if (dsUsr.Tables[0].Rows[0]["pcl_pclid"].ToString() == "")
                   {
                       objSession.PclId = 0;
                   }
                   else
                   {
                       objSession.PclId = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["pcl_pclid"]);
                   }
                   if (!string.IsNullOrEmpty(dsUsr.Tables[0].Rows[0]["epl_url_foto"].ToString()))
                   {
                       objSession.UrlFoto = dsUsr.Tables[0].Rows[0]["epl_url_foto"].ToString();
                   }
                   else
                   {
                       objSession.UrlFoto = "/Images/blank_avatar.jpg";
                   }
                   Session["Usuario"] = objSession;
                   if (dsUsr.Tables[0].Rows[0][30].ToString() == "S")
                   {
                       DateTime fechaCambioPassword = new DateTime(3000, 12, 30);
                       if (DateTime.TryParse(dsUsr.Tables[0].Rows[0][31].ToString(), out fechaCambioPassword))
                       {
                           if (fechaCambioPassword <= DateTime.Today)
                           {
                               //objSession.Menu = objMenu.Cargar(objSession.UserId, objSession.Idioma, objSession.CodigoEmpresa);

                               Session["Usuario"] = objSession;
                               //return RedirectToAction("CambiarContrasena","Account");//Redirect("Register"); // configurar cambio clave
                           }
                       }
                   }
                   objSession.Domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                   if (objSession.PrfId.ToString() == System.Configuration.ConfigurationManager.AppSettings["perfilCliente"])
                   {
                       objSession.Menu = "<div id='cssmenu' style='position: absolute;z-index: 9;'><ul><li class='has-sub'><a href='#' title='s'><span>Gestionar Cartera</span></a><ul><li class='last'><a href='" + objSession.Domain + System.Configuration.ConfigurationManager.AppSettings["UrlGestionClientes"] + "' title='s'><span>ABM</span></a></li></ul></li></ul></div>";
                       List<string> clienteAsociado = usu.TraeClienteAsociadoUsuario(objSession.CodigoEmpresa, objSession.UserId);
                       objSession.ClienteAsociado = Int32.Parse(clienteAsociado[0]);
                       objSession.EstadosClienteAsociado = clienteAsociado[1];
                       objSession.RutClienteAsociado = clienteAsociado[2];
                       objSession.NombreClienteAsociado = clienteAsociado[3];
                   }
                   else
                   {
                       objSession.Menu = objMenu.Cargar(objSession.UserId, objSession.Idioma, objSession.CodigoEmpresa, objSession.Domain);
                   }

                   if (objSession.PrfId.ToString() == "21" && objSession.UserId == 403)
                   {
                       List<string> clienteAsociado = usu.TraeClienteAsociadoUsuario(objSession.CodigoEmpresa, objSession.UserId);
                       objSession.ClienteAsociado = Int32.Parse(clienteAsociado[0]);
                       objSession.EstadosClienteAsociado = clienteAsociado[1];
                       objSession.RutClienteAsociado = clienteAsociado[2];
                       objSession.NombreClienteAsociado = clienteAsociado[3];
                       objSession.Menu = "<div id='cssmenu' style='position: absolute;z-index: 9;'><ul><li class='has-sub'><a href='#' title='s'><span>Gestion</span></a><ul><li class='last'>";
                       objSession.Menu += "<a href='" + objSession.Domain + "/Cartera/Index?tipo=V&pag=353" + "' title='s'><span>Cartera</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Reportes</span></a><ul><li>";
                       objSession.Menu += "<a href='" + objSession.Domain + "/Reportes/Cartera" + "' title='s'><span>Dinamicos</span></a></li><li class='last'>";
                       objSession.Menu += "<a href='" + objSession.Domain + "/Reportes/Predefinidos/?ctr=P&pag=354" + "' title='s'><span>Predefinidos</span></a></li></ul></li></ul></div>";
                       //objMenu.Cargar(objSession.UserId, objSession.Idioma, objSession.CodigoEmpresa, objSession.Domain);
                   }

       */

                return RedirectToAction("Index", "Open", new { });
            }
            catch (Exception ex)
            {
                // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
                ModelState.AddModelError("", "El nombre de usuario o la contraseña especificados son incorrectos. Error: " + ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {

            if(Session["Usuario"] != null)
            {
                bcp.ConsultaPJ.LogOffUsrByIp(Request.ServerVariables["REMOTE_HOST"] + Request.ServerVariables["REMOTE_ADDR"]);   //bcp.ConsultaPJ.ValidaUsuario(model.UserName);
            }

            Session["Usuario"] = null;

            return RedirectToAction("Login", "Open");
        }

        [HttpPost]
        public JsonResult Upload(int idUser, string tipo, string Pclid)
        {
            string fileName = "";
            string path = "";
            int id = 0;
            int idLogo = 0;
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

                        case "CargaLogoEmpresa":

                            string ext = "";

                            ext = Path.GetExtension(file.FileName).ToLower();

                            //Use the following properties to get file's name, size and MIMEType
                            ////fileName = fileName + file.FileName;
                            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                            {
                                return Json(-1);
                            }

                            //Guarda el documento actual (estampe)
                            id = bcp.ConsultaPJ.ValidaEmpresaRutaPJ(Pclid);

                            if (id == 0)
                            {
                                //idLogo = bcp.ConsultaPJ.InsertarRutaLogoPJ(idUser, Pclid, "~/images/" + file.FileName);
                                idLogo = bcp.ConsultaPJ.InsertarRutaLogoPJ(idUser, Pclid, file.FileName);
                                if (idLogo > 0) { 
                                    objFunc.CreaCarpetas(ConfigurationManager.AppSettings["RutaImagenesEmpresa"]);
                                    //fileName = fileName + id + ext;
                                    file.SaveAs(ConfigurationManager.AppSettings["RutaImagenesEmpresa"] + file.FileName);
                                    //file.SaveAs(path + fileName); //File will be saved in application root
                                    return Json(idLogo);
                                }
                                else
                                {
                                    return Json("");
                                }
                            }
                            else if (id > 0)
                            {
                                return Json(0);
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
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.OpenController.Carga.Upload", 0);
                fileName = "";
            }

            return Json("");

        }

        public ActionResult ActPass(string usrid, string passAct, string newPass)
        {            
            return Json(bcp.ConsultaPJ.ActPass(usrid, passAct, newPass), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GuardarUserPJ(int iduser, string nombre, string username, string pass, int activa, int pclid, string adm)
        {
            return Json(bcp.ConsultaPJ.GuardarUserPJ(iduser, nombre, username, pass, activa, pclid, adm), JsonRequestBehavior.AllowGet);
        }
    }
}