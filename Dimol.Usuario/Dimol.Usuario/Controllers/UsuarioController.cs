using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Diagnostics;
using Dimol.Usuario.Models;
using Dimol.bcp;

namespace Dimol.Usuario.Controllers
{
    public class UsuarioController : Dimol.Controllers.BaseController
    {
        #region Usuarios
        public ActionResult Usuario(int? IdUsuario)
        {
            //Validación de sesión del usuario
            if (!SettingAccount()) {
                return RedirectToAction("Login", "Account");
            }

            //Selección de valor seleccionado de las SelectList
            var SelectedTipoPreguntaSecreta = "";
            var SelectedPerfil = "";
            var SelectedPermisos = "";
            var SelectedEstado = "";

            //Verificación si es edición
            UsuarioModel UsuarioVM = null;
            ViewBag.EsEdicion = false;

            if (IdUsuario != null)
            {
                ViewBag.EsEdicion = true;

                dto.Usuario Usuario = bcp.Usuario.BuscarUsuarioPorIdUsuario((int)IdUsuario);
                UsuarioVM = new UsuarioModel(Usuario);

                //Decodificación
                var func = new Funciones();
                UsuarioVM.Usuario = func.Desencripta(UsuarioVM.Usuario);
                UsuarioVM.Clave = func.Desencripta(UsuarioVM.Clave);

                SelectedTipoPreguntaSecreta = UsuarioVM.TipoPreguntaSecreta;
                SelectedPerfil = UsuarioVM.Perfil;
                SelectedPermisos = UsuarioVM.Permisos;
                SelectedEstado = (UsuarioVM.Estado == "H" ? "1" : "2");
            }

            //Daclaración de SelectList
            ViewBag.idUsuario = IdUsuario;
            ViewBag.TipoPreguntaSecreta = new SelectList(bcp.Usuario.ListarPreguntasSecretas(objSession.Idioma, "Seleccione"), "Value", "Text", SelectedTipoPreguntaSecreta);
            ViewBag.Perfil = new SelectList(bcp.Usuario.ListarPerfiles(objSession.Idioma, "Seleccione"), "Value", "Text", SelectedPerfil);
            ViewBag.Permisos = new SelectList(bcp.Usuario.ListarPermisos(objSession.Idioma, "Seleccione"), "Value", "Text", SelectedPermisos);
            ViewBag.Estado = new SelectList(bcp.Usuario.ListarEstados(objSession.Idioma, "Seleccione"), "Value", "Text", SelectedEstado);

            if (UsuarioVM != null)
            {
                return View(UsuarioVM);
            }

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetSinAsignar(GridSettings gridSettings, string idUsuario)
        {
            int id = 0;
            Debug.WriteLine("ID USUARIN SIN ASIG" + idUsuario);
            if (idUsuario != null)
            {
                id = Int32.Parse(idUsuario);
            }
            List<dto.Sucursal> lstAccion = bcp.Usuario.ListarSucursalesSinAsignar(objSession.CodigoEmpresa, id, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());


            var jsonData = new
            {
                //total = totalPages,
                page = gridSettings.pageIndex,
                //records = totalRecords,

                rows =
                (
                    from dto.Sucursal item in lstAccion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre,
                            item.sel

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetAsignados(GridSettings gridSettings, string idUsuario)
        {

            int id = 0;
            Debug.WriteLine("ID USUARIN ASIG" + idUsuario);
            if (idUsuario != null)
            {
                id = Int32.Parse(idUsuario);
            }

            List<dto.Sucursal> lstAccion = bcp.Usuario.ListarSucursalesAsignadas(objSession.CodigoEmpresa, id, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString());


            var jsonData = new
            {
                //total = totalPages,
                page = gridSettings.pageIndex,
                //records = totalRecords,

                rows =
                (
                    from dto.Sucursal item in lstAccion
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre,
                            item.sel

                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarUsuario(int? idUsuario, string rut, string nombre, string usuario, string clave, string email, 
            string tipoPregunta, string respuesta, string perfil, string permiso, string estado, string cambiaPassword, 
            string fechaCambioPassword, string fechaIngreso, string ingresosOK, string fechaUltimoIngreso,
            string ingresosMalos, string fechaBloqueo)
        {
            #region Carga del objeto
            var func = new Funciones();

            dto.Usuario user = new dto.Usuario();
            user.IdUsuario = idUsuario != null ? (int)idUsuario : 0;
            user.Rut = rut;
            user.Nombre = nombre;
            user.Login = func.Encripta(usuario);
            user.Clave = func.Encripta(clave);
            user.Mail = email;
            user.TipoPregunta = tipoPregunta;
            user.Respuesta = respuesta;
            user.Perfil = perfil;
            user.Permiso = permiso;
            user.Estado = estado == "1" ? "H" : "B"; //estado
            user.CambiPassword = cambiaPassword.Equals("true");
            user.FechaCambioPassword = fechaCambioPassword;
            user.FechaIngreso = fechaIngreso;
            user.FechaUltimoIngreso = fechaUltimoIngreso;
            user.FechaBloqueo = fechaBloqueo;

            if (ingresosOK != null)
            {
                user.IngresosOK = int.Parse(ingresosOK);
            }

            if (ingresosMalos != null)
            {
                user.IngresosMalos = int.Parse(ingresosMalos);
            }
            #endregion

            #region Llamado a base de datos
            int val = 0;

            if (idUsuario != null) {
                val = bcp.Usuario.Actualizar(user);
            } else {
                val = bcp.Usuario.Insertar(user, objSession.CodigoEmpresa, objSession.CodigoSucursal);
            }
            #endregion

            return Json(val);
        }
        #endregion

        #region "BuscarUsuario"
        public ActionResult BuscarUsuario(int? IdUsuario)
        {
            //Validación de sesión del usuario
            if (!SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetUsuario(GridSettings gridSettings, string nombre, string usuario)
        {
            int totalRecords = bcp.Usuario.ListarUsuariosGrillaCount(objSession.CodigoEmpresa, nombre, usuario, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder);
            int totalPages   = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow     = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow       = startRow + gridSettings.pageSize;

            List<dto.Usuario> lst = bcp.Usuario.ListarUsuariosGrilla(objSession.CodigoEmpresa, nombre, usuario, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Usuario item in lst
                    select new
                    {
                        id = item.IdUsuario,
                        cell = new object[]
                        {
                            item.IdUsuario,
                            item.Nombre,
                            item.Estado,
                            item.FechaUltimoIngreso,
                            item.FechaBloqueo
                            
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDummy(GridSettings gridSettings)
        {
            // create json data 
            bcp.Usuario bcpDeudor = new bcp.Usuario();

            int totalRecords = 0;


            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Usuario> lst = new List<dto.Usuario>();


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Usuario item in lst
                    select new
                    {
                        id = 1,
                        cell = new object[]
                        {
                            item.IdUsuario,
                            item.Nombre,
                            //item.NombreCliente,
                            //item.Ctcid,
                            //item.Rut,
                            //item.NombreFantasia,
                            //item.Gestor,
                            //item.Rol,
                            //item.Gesid
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}

