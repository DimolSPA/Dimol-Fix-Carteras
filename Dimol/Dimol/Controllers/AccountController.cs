using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Dimol.Models;
using System.Data;
using System.Data.SqlClient;
using Dimol.bcp;
using Dimol.dto;
using System.Globalization;
using System.Threading;

namespace Dimol.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        
        bcp.Usuarios usu = new bcp.Usuarios();
        Funciones func = new Funciones();
        UserSession objSession = new UserSession();
        #region "logica login"
        
        private bool Parametros(LoginModel model)
        {
            DataSet dsUsr = new DataSet();
            try
            {
                dsUsr = usu.Trae_DatUsuario(model.UserName);
                objSession.CodigoEmpresa = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["usr_codemp"]);
                objSession.CodigoSucursal = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["uss_sucid"]);
                objSession.UserId = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["usr_usrid"]);
                objSession.Nombre = dsUsr.Tables[0].Rows[0]["usr_nombre"].ToString();
                objSession.PrfId = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["usr_prfid"]);
                objSession.Permisos = Convert.ToInt16(dsUsr.Tables[0].Rows[0]["usr_permisos"]);
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
                
                

                //Session["Usuario"] = dsUsr.Tables[0].Rows[0][0].ToString() + ";" + dsUsr.Tables[0].Rows[0][28].ToString() + ";" + dsUsr.Tables[0].Rows[0][1].ToString() + ";" + dsUsr.Tables[0].Rows[0][2].ToString() + ";" + dsUsr.Tables[0].Rows[0][14].ToString() + ";" + dsUsr.Tables[0].Rows[0][15].ToString();

                if (dsUsr.Tables[0].Rows[0][29].ToString() == "S")
                {
                    //if (dsUsr.Tables[0].Rows[0][30] <= DateTime.Today)
                    //{
                    //    PanIng.Visible = false;
                    //    PanPass.Visible = true;
                        return false;
                    //}
                }



                //QSsec["codemp"] = dsUsr.Tables[0].Rows[0]["usr_codemp"];
                //QSsec("codsuc") = dsUsr.Tables[0].Rows[0]("uss_sucid");
                //QSsec("usrid") = dsUsr.Tables[0].Rows[0]("usr_usrid");
                //QSsec("nombre") = dsUsr.Tables[0].Rows[0]("usr_nombre");
                //QSsec("prfid") = dsUsr.Tables[0].Rows[0]("usr_prfid");
                //QSsec("permisos") = dsUsr.Tables[0].Rows[0]("usr_permisos");
                //if (IsDBNull(dsUsr.Tables[0].Rows[0]("epl_emplid")))
                //{
                //    QSsec("emplid") = "";
                //    Session("Usuario") = Session("Usuario") + ";0";
                //}
                //else
                //{
                //    QSsec("emplid") = dsUsr.Tables[0].Rows[0]("epl_emplid");
                //    Session("Usuario") = Session("Usuario") + ";" + dsUsr.Tables[0].Rows[0]("epl_emplid").ToString();
                //}
                //if (IsDBNull(dsUsr.Tables[0].Rows[0]("pcl_pclid")))
                //{
                //    QSsec("pclid") = "";
                //    Session("Usuario") = Session("Usuario") + ";0";
                //}
                //else
                //{
                //    QSsec("pclid") = dsUsr.Tables[0].Rows[0]("pcl_pclid");
                //    Session("Usuario") = Session("Usuario") + ";" + dsUsr.Tables[0].Rows[0](22).ToString();
                //}
                //QSsec("idioma") = func.Configuracion_Num(1).ToString();
                //Session("Usuario") = Session("Usuario") + ";" + func.Configuracion_Num(1).ToString();
                //Session("Permiso") = dsUsr.Tables[0].Rows[0]("usr_permisos");
                //Response.Redirect("horus.aspx?datos=" + HttpUtility.UrlEncode(QSsec.ToString()));

                //Response.Redirect("horus2.aspx")
                Session["Usuario"] = objSession;
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


            //Create Procedure Trae_Usuarios_Empleado_Provcli(@usuario varchar(20)) as
            //  SELECT usuarios.usr_codemp,  0
            //         usuarios.usr_usrid,   1
            //         usuarios.usr_nombre,   2
            //         usuarios.usr_login,   3
            //         usuarios.usr_password, 4
            //         usuarios.usr_fecing,   5
            //         usuarios.usr_godlog,   6
            //         usuarios.usr_badlog,   7
            //         usuarios.usr_fecultlog, 8
            //         usuarios.usr_fecblock,   9
            //         usuarios.usr_mail,   10
            //         usuarios.usr_tipquest, 11
            //         usuarios.usr_answer,   12
            //         usuarios.usr_sucid,   13
            //         usuarios.usr_prfid,   14
            //         usuarios.usr_permisos,  15
            //         usuarios.usr_estado,   16
            //         empleados.epl_emplid,   17
            //         empleados.epl_rut,   18
            //         empleados.epl_nombre,  19
            //         empleados.epl_apepat,   20
            //         empleados.epl_apemat,   21
            //         provcli.pcl_pclid,   22
            //         provcli.pcl_rut,   23
            //         provcli.pcl_nombre,  24
            //         provcli.pcl_apepat,   25
            //         provcli.pcl_apemat,   26
            //         provcli.pcl_nomfant,   27
            //         usuarios_sucursal.uss_sucid  28

        }
        
        #endregion

        //
        // GET: /Account/Login

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
            Dimol.bcp.Menu objMenu = new bcp.Menu();
            string err = "";
            //Session.LCID = 13322;
            try
            {
                usu.Usuario =   model.UserName;
                usu.Password = model.Password;
                err = usu.ValidaUsuario();

                if (err != "")
                {
                    ModelState.AddModelError("", "El nombre de usuario o la contraseña especificados son incorrectos.");
                    return View(model);
                }

                Session["Usuario"] = new UserSession();
                dsUsr = usu.Trae_DatUsuario(model.UserName);
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
                    objSession.ClienteAsociado = Int32.Parse( clienteAsociado[0] );
                    objSession.EstadosClienteAsociado = clienteAsociado[1] ;
                    objSession.RutClienteAsociado = clienteAsociado[2];
                    objSession.NombreClienteAsociado = clienteAsociado[3];
                }
                else
                {
                    objSession.Menu= objMenu.Cargar(objSession.UserId, objSession.Idioma, objSession.CodigoEmpresa, objSession.Domain);
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


                
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
                ModelState.AddModelError("", "El nombre de usuario o la contraseña especificados son incorrectos. Error: "+ex.Message);
                return View(model);
            }            
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult CambiarContrasena()
        {
            this.SettingAccount();
            Dimol.Models.RegisterModel model = new RegisterModel();
            model.UserName = objSession.User;
           
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CambiarContrasena(RegisterModel model)
        {
            

            model.UserName = objSession.User;

            return View(model);
        }


       

        #region Aplicaciones auxiliares
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Vaya a http://go.microsoft.com/fwlink/?LinkID=177550 para
            // obtener una lista completa de códigos de estado.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario ya existe. Escriba un nombre de usuario diferente.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Ya existe un nombre de usuario para esa dirección de correo electrónico. Escriba una dirección de correo electrónico diferente.";

                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña especificada no es válida. Escriba un valor de contraseña válido.";

                case MembershipCreateStatus.InvalidEmail:
                    return "La dirección de correo electrónico especificada no es válida. Compruebe el valor e inténtelo de nuevo.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "La respuesta de recuperación de la contraseña especificada no es válida. Compruebe el valor e inténtelo de nuevo.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "La pregunta de recuperación de la contraseña especificada no es válida. Compruebe el valor e inténtelo de nuevo.";

                case MembershipCreateStatus.InvalidUserName:
                    return "El nombre de usuario especificado no es válido. Compruebe el valor e inténtelo de nuevo.";

                case MembershipCreateStatus.ProviderError:
                    return "El proveedor de autenticación devolvió un error. Compruebe los datos especificados e inténtelo de nuevo. Si el problema continúa, póngase en contacto con el administrador del sistema.";

                case MembershipCreateStatus.UserRejected:
                    return "La solicitud de creación de usuario se ha cancelado. Compruebe los datos especificados e inténtelo de nuevo. Si el problema continúa, póngase en contacto con el administrador del sistema.";

                default:
                    return "Error desconocido. Compruebe los datos especificados e inténtelo de nuevo. Si el problema continúa, póngase en contacto con el administrador del sistema.";
            }
        }
        #endregion
    }
}
