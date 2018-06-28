using Dimol.ProveedorCliente.Mantenedores.bcp;
using Dimol.ProveedorCliente.Mantenedores.Models;
using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dimol.ProveedorCliente.Mantenedores.Controllers
{
    public class ProvCliController : Dimol.Controllers.BaseController
    {
        string tipoCliente = "V";

        public ActionResult Categorias()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Categorias";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Utilizacion = bcp.Categoria.ListarUtilizacion(objSession.Idioma);

            return View(model);
        }

        public ActionResult Giros()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "Giros";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            
            return View(model);
        }

        public ActionResult SuperCategorias()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "SuperCategorias";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Utilizacion = bcp.SuperCategoria.ListarUtilizacion(objSession.Idioma);

            return View(model);
        }

        public ActionResult TiposContacto()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "TiposContacto";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            
            return View(model);
        }

        public ActionResult TiposProvCli()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            GridModel model = new GridModel();
            model.GridSelect = "TiposProvCli";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Agrupa = bcp.TipoProvCli.ListarTipos(objSession.Idioma);

            return View(model);
        }

        public void setTipoCliente(string tipoC)
        {
            this.tipoCliente = tipoC;
        }

        #region "ProveedorCliente"
        public ActionResult Cliente(string tipoCliente)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            this.tipoCliente = tipoCliente;
            Debug.WriteLine("TIPO CLIENTE: " + this.tipoCliente);
            ViewBag.Tipo = new SelectList(bcp.ProveedorCliente.ListarTiposProvCli(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Nacionalidad = new SelectList(bcp.ProveedorCliente.ListarNacionalidad(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Giro = new SelectList(bcp.ProveedorCliente.ListarGiros(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Estados = new SelectList(bcp.ProveedorCliente.ListarEstado(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoCartera = new SelectList(bcp.ProveedorCliente.ListarTipoCartera(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Usuario = new SelectList(bcp.ProveedorCliente.ListarUsuarios(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.Pais = new SelectList(bcp.ProveedorCliente.ListarPais(), "Value", "Text", objSession.CodPais);
            ViewBag.Region = new SelectList(bcp.ProveedorCliente.ListarRegion(objSession.CodPais), "Value", "Text");
            ViewBag.Ciudad = new SelectList(bcp.ProveedorCliente.ListarCiudad(0), "Value", "Text");
            ViewBag.Comuna = new SelectList(bcp.ProveedorCliente.ListarComuna(0), "Value", "Text");
            ViewBag.Banco = new SelectList(bcp.ProveedorCliente.ListarBancos(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoCuenta = new SelectList(bcp.ProveedorCliente.ListarTiposCuenta(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Impuesto = new SelectList(bcp.ProveedorCliente.ListarImpuestosProvCli(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoContacto = new SelectList(bcp.ProveedorCliente.ListarTiposContactoProvCli(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.FormaDePago = new SelectList(bcp.ProveedorCliente.ListarFormasDePago(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.ContratoCartera = new SelectList(bcp.ProveedorCliente.ListarContratosCartera(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.Sucursal = new SelectList(bcp.ProveedorCliente.ListarSucursalesProvCli(objSession.CodigoEmpresa, objSession.CodigoSucursal, "Seleccione"), "Value", "Text", "");
            ViewBag.EstadoCredito = new SelectList(bcp.ProveedorCliente.ListarEstadosCredito(objSession.Idioma, "Seleccione"), "Value", "Text", "");

            return View();
        }

        public ActionResult Receptores(string tipoCliente)
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }
            this.tipoCliente = tipoCliente;
            
            ViewBag.Tipo = new SelectList(bcp.ProveedorCliente.ListarTiposProvCli(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Nacionalidad = new SelectList(bcp.ProveedorCliente.ListarNacionalidad(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Giro = new SelectList(bcp.ProveedorCliente.ListarGiros(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Estado = new SelectList(bcp.ProveedorCliente.ListarEstado(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoCartera = new SelectList(bcp.ProveedorCliente.ListarTipoCartera(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Usuario = new SelectList(bcp.ProveedorCliente.ListarUsuarios(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.Pais = new SelectList(bcp.ProveedorCliente.ListarPais(), "Value", "Text", objSession.CodPais);
            ViewBag.Region = new SelectList(bcp.ProveedorCliente.ListarRegion(objSession.CodPais), "Value", "Text");
            ViewBag.Ciudad = new SelectList(bcp.ProveedorCliente.ListarCiudad(0), "Value", "Text");
            ViewBag.Comuna = new SelectList(bcp.ProveedorCliente.ListarComuna(0), "Value", "Text");
            ViewBag.Banco = new SelectList(bcp.ProveedorCliente.ListarBancos(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoCuenta = new SelectList(bcp.ProveedorCliente.ListarTiposCuenta(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.Impuesto = new SelectList(bcp.ProveedorCliente.ListarImpuestosProvCli(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.TipoContacto = new SelectList(bcp.ProveedorCliente.ListarTiposContactoProvCli(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.FormaDePago = new SelectList(bcp.ProveedorCliente.ListarFormasDePago(objSession.CodigoEmpresa, objSession.Idioma, "Seleccione"), "Value", "Text", "");
            ViewBag.ContratoCartera = new SelectList(bcp.ProveedorCliente.ListarContratosCartera(objSession.CodigoEmpresa, "Seleccione"), "Value", "Text", "");
            ViewBag.Sucursal = new SelectList(bcp.ProveedorCliente.ListarSucursalesProvCli(objSession.CodigoEmpresa, objSession.CodigoSucursal, "Seleccione"), "Value", "Text", "");
            ViewBag.EstadoCredito = new SelectList(bcp.ProveedorCliente.ListarEstadosCredito(objSession.Idioma, "Seleccione"), "Value", "Text", "");

            return View();
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult GrabarProveedorCliente(string Tipo, string Nacionalidad, string Rut, string Nombre, string ApellidoPaterno, string ApellidoMaterno,
        //    string NombreFantasia, string Giro, string FechaIngreso, string Estado, string FechaFin, bool Transportista,
        //    bool Naviera, string Comentario, string RutRepLegal, string NombreRepLegal, bool Mostrar, string TipoCartera,
        //    string CodigoSAP, string Usuario, string NombreSucursal, string Comuna, string Direccion, string Telefono,
        //    string Fax, string Correo, bool CasaMatriz, string Banco, string TipoCuenta, string Numero, string CodigoSucursal,
        //    string Impuesto, string Sucursal, string TipoContacto, string NombreContacto, string TelefonoContacto,
        //    string AnexoContacto, string FaxContacto, string CelularContacto, string CorreoContacto,
        //    string FormaDePago, bool UtilizaCredito, string LimiteCredito, string CreditoConsumido,
        //    string EstadoCredito, string ComentarioCuentaCorriente, string ContratoCartera, string FechaInicioContrato,
        //    string FechaFinContrato, bool Indefinido, string RutContrato, string NombreContrato,
        //    bool InteresClientes, bool HonorariosClientes)
        //{
        //    dto.ProveedorCliente pc = new dto.ProveedorCliente();
        //    pc.Tipo = Tipo;
        //    pc.Nacionalidad = Nacionalidad;
        //    pc.Rut = Rut;
        //    pc.Nombre = Nombre;
        //    pc.ApellidoPaterno = ApellidoPaterno;
        //    pc.ApellidoMaterno = ApellidoMaterno;
        //    pc.NombreFantasia = NombreFantasia;
        //    pc.Giro = Giro;
        //    pc.FechaIngreso = FechaIngreso;
        //    pc.Estados = Estado;
        //    pc.FechaFin = FechaFin;
        //    pc.Transportista = Transportista;
        //    pc.Naviera = Naviera;
        //    pc.Comentario = Comentario;
        //    pc.RutRepLegal = RutRepLegal;
        //    pc.NombreRepLegal = NombreRepLegal;
        //    pc.Mostrar = Mostrar;
        //    pc.TipoCartera = TipoCartera;
        //    pc.CodigoSAP = CodigoSAP;
        //    pc.Usuario = Usuario;
        //    pc.NombreSucursal = NombreSucursal;
        //    pc.Comuna = Comuna;
        //    pc.Direccion = Direccion;
        //    pc.Telefono = Telefono;
        //    pc.Fax = Fax;
        //    pc.Correo = Correo;
        //    pc.CasaMatriz = CasaMatriz;
        //    pc.Banco = Banco;
        //    pc.TipoCuenta = TipoCuenta;
        //    pc.Numero = Numero;
        //    pc.CodigoSucursal = CodigoSucursal;
        //    pc.Impuesto = Impuesto;
        //    pc.Sucursal = Sucursal;
        //    pc.TipoContacto = TipoContacto;
        //    pc.NombreContacto = NombreContacto;
        //    pc.TelefonoContacto = TelefonoContacto;
        //    pc.AnexoContacto = AnexoContacto;
        //    pc.FaxContacto = FaxContacto;
        //    pc.CelularContacto = CelularContacto;
        //    pc.CorreoContacto = CorreoContacto;
        //    pc.FormaDePago = FormaDePago;
        //    pc.UtilizaCredito = UtilizaCredito;
        //    pc.LimiteCredito = LimiteCredito;
        //    pc.CreditoConsumido = CreditoConsumido;
        //    pc.EstadoCredito = EstadoCredito;
        //    pc.ComentarioCuentaCorriente = ComentarioCuentaCorriente;
        //    pc.ContratoCartera = ContratoCartera;
        //    pc.FechaInicioContrato = FechaInicioContrato;
        //    pc.FechaFinContrato = FechaFinContrato;
        //    pc.Indefinido = Indefinido;
        //    pc.RutContrato = RutContrato;
        //    pc.NombreContrato = NombreContrato;
        //    pc.InteresClientes = InteresClientes;
        //    pc.HonorariosClientes = HonorariosClientes;

        //    int idCliente = bcp.ProveedorCliente.GrabarCliente(pc, objSession.CodigoEmpresa, objSession.Idioma, this.tipoCliente);
        //    int ingSucursal = bcp.ProveedorCliente.GrabarClienteSucursal(pc, objSession.CodigoEmpresa, objSession.Idioma, idCliente);
        //    int ingImpuesto = bcp.ProveedorCliente.GrabarClienteImpuesto(pc, objSession.CodigoEmpresa, idCliente);
        //    int ingContacto = bcp.ProveedorCliente.GrabarClienteContacto(pc, objSession.CodigoEmpresa, idCliente);
        //    int ingCtaCte = bcp.ProveedorCliente.GrabarClienteCuentaCorriente(pc, objSession.CodigoEmpresa, idCliente);
        //    int ingContrato = bcp.ProveedorCliente.GrabarClienteContrato(pc, objSession.CodigoEmpresa, idCliente);
        //    if (idCliente != -1 && ingSucursal != -1 && ingImpuesto != -1 && ingContacto != -1 && ingCtaCte != -1
        //        && ingContrato != -1)
        //    {
        //        Debug.WriteLine("INGRESO OK");
        //    }

        //    return Json(idCliente);
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GrabarProveedorCliente(dto.ProveedorCliente pc)
        {
            int idCliente   = bcp.ProveedorCliente.GrabarCliente(pc, objSession.CodigoEmpresa, objSession.Idioma, this.tipoCliente);
            int ingSucursal = bcp.ProveedorCliente.GrabarClienteSucursal(pc, objSession.CodigoEmpresa, objSession.Idioma, idCliente);
            int ingImpuesto = bcp.ProveedorCliente.GrabarClienteImpuesto(pc, objSession.CodigoEmpresa, idCliente);
            int ingContacto = bcp.ProveedorCliente.GrabarClienteContacto(pc, objSession.CodigoEmpresa, idCliente);
            int ingCtaCte   = bcp.ProveedorCliente.GrabarClienteCuentaCorriente(pc, objSession.CodigoEmpresa, idCliente);
            int ingContrato = bcp.ProveedorCliente.GrabarClienteContrato(pc, objSession.CodigoEmpresa, idCliente);

            return Json(idCliente);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GrabarReceptores(string Tipo, string Nacionalidad, string Rut, string Nombre, string ApellidoPaterno, string ApellidoMaterno,
            string NombreFantasia, string Giro, string FechaIngreso, string Estado, string FechaFin, 
            string Comentario, string RutRepLegal, string NombreRepLegal, string TipoCartera,
            string CodigoSAP, string Usuario, string NombreSucursal, string Comuna, string Direccion, string Telefono,
            string Fax, string Correo, string Banco, string TipoCuenta, string Numero, string CodigoSucursal,
            string Impuesto, string Sucursal, string TipoContacto, string NombreContacto, string TelefonoContacto,
            string AnexoContacto, string FaxContacto, string CelularContacto, string CorreoContacto,
            string FormaDePago, string LimiteCredito, string CreditoConsumido,
            string EstadoCredito, string ComentarioCuentaCorriente, string ContratoCartera, string FechaInicioContrato,
            string FechaFinContrato, string RutContrato, string NombreContrato)
        {
            dto.ProveedorCliente pc = new dto.ProveedorCliente();
            pc.Tipo = "1";//Tipo;
            pc.Nacionalidad = Nacionalidad;
            pc.Rut = Rut;
            pc.Nombre = Nombre;
            pc.ApellidoPaterno = ApellidoPaterno;
            pc.ApellidoMaterno = ApellidoMaterno;
            pc.NombreFantasia = string.Empty; // NombreFantasia;
            pc.Giro = "48";//Giro;
            pc.FechaIngreso = FechaIngreso;
            pc.Estados = "1";//Estado;
            pc.FechaFin = FechaFin;
            pc.Transportista = false;
            pc.Naviera = false;
            pc.Comentario = Comentario;
            pc.RutRepLegal = RutRepLegal;
            pc.NombreRepLegal = NombreRepLegal;
            pc.Mostrar = false;
            pc.TipoCartera = TipoCartera;
            pc.CodigoSAP = CodigoSAP;
            pc.Usuario = "0";//Usuario;
            pc.NombreSucursal = NombreSucursal;
            pc.Comuna = "112"; // Comuna;
            pc.Direccion = Direccion;
            pc.Telefono = Telefono;
            pc.Fax = Fax;
            pc.Correo = Correo;
            pc.CasaMatriz = true;
            pc.Banco = "0";//Banco;
            pc.TipoCuenta = TipoCuenta;
            pc.Numero = Numero;
            pc.CodigoSucursal = CodigoSucursal;
            pc.Impuesto = "0";//Impuesto;
            pc.Sucursal = "1"; //Sucursal;
            pc.TipoContacto = "4"; //TipoContacto;
            pc.NombreContacto = NombreContacto;
            pc.TelefonoContacto = TelefonoContacto;
            pc.AnexoContacto = AnexoContacto;
            pc.FaxContacto = FaxContacto;
            pc.CelularContacto = CelularContacto;
            pc.CorreoContacto = CorreoContacto;
            pc.FormaDePago = "7"; //FormaDePago;
            pc.UtilizaCredito = false;
            pc.LimiteCredito = "0"; //LimiteCredito;
            pc.CreditoConsumido = "0"; //CreditoConsumido;
            pc.EstadoCredito = "1"; //EstadoCredito;
            pc.ComentarioCuentaCorriente = "Receptor"; //ComentarioCuentaCorriente;
            pc.ContratoCartera = "1"; //ContratoCartera;
            pc.FechaInicioContrato = FechaInicioContrato;
            pc.FechaFinContrato = FechaFinContrato;
            pc.Indefinido = false;
            pc.RutContrato = RutContrato;
            pc.NombreContrato = NombreContrato;
            pc.InteresClientes = false;
            pc.HonorariosClientes = false;

            //Validar que el rut no se encuentre creado para insertar, si ya existe, entonces actualizar como ente Judicial receptor
            int idCliente = bcp.ProveedorCliente.GrabarCliente(pc, objSession.CodigoEmpresa, objSession.Idioma, "C");//this.tipoCliente);
            if ((idCliente != 0) && (idCliente != -1) )
            {
                int ingSucursal = bcp.ProveedorCliente.GrabarClienteSucursal(pc, objSession.CodigoEmpresa, objSession.Idioma, idCliente);
                int ingImpuesto = bcp.ProveedorCliente.GrabarClienteImpuesto(pc, objSession.CodigoEmpresa, idCliente);
                int ingContacto = bcp.ProveedorCliente.GrabarClienteContacto(pc, objSession.CodigoEmpresa, idCliente);
                int ingCtaCte = bcp.ProveedorCliente.GrabarClienteCuentaCorriente(pc, objSession.CodigoEmpresa, idCliente);
                int ingContrato = bcp.ProveedorCliente.GrabarClienteContrato(pc, objSession.CodigoEmpresa, idCliente);
                //Valida si el idCliente se encuentra creado como ente judicial
                int idEnte = bcp.ProveedorCliente.InsertarEnteJudicial(objSession.CodigoEmpresa, idCliente);
                if (idCliente != -1 && ingSucursal != -1 && ingImpuesto != -1 && ingContacto != -1 && ingCtaCte != -1
                    && ingContrato != -1)
                {
                }
            }
           
            return Json(idCliente);
        }
        #endregion

        public ActionResult BuscarProveedorCliente()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Estados = new SelectList(bcp.ProveedorCliente.ListarEstados(objSession.Idioma, "Seleccione"), "Value", "Text", "");
            
            return View();
        }

        public ActionResult BuscarReceptores()
        {
            if (!this.SettingAccount())
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        #region "BuscarProveedorCliente"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetProveedorCliente(GridSettings gridSettings, string rut, string nombre, string apellidoPaterno, string apellidoMaterno, string nombreFantasia, string estados)
        {
            // create json data 
            bcp.ProveedorCliente bcpProvCli = new bcp.ProveedorCliente();
            string tipo = "V";
            int totalRecords = bcpProvCli.ListarProveedorClienteGrillaCount(objSession.CodigoEmpresa, tipo, nombre, apellidoPaterno, apellidoMaterno, rut, nombreFantasia, estados, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL " + totalRecords);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.ProveedorCliente> lst = bcpProvCli.ListarProveedorClienteGrilla(objSession.CodigoEmpresa, tipo, nombre, apellidoPaterno, apellidoMaterno, rut, nombreFantasia, estados, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.ProveedorCliente item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Rut,
                            item.TipoCliente == "P" ? "Previsional" : "Civil",
                            item.Nombre,
                            item.NombreFantasia,
                            item.ApellidoPaterno
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetReceptores(GridSettings gridSettings, string rut, string nombre, string apellidoPaterno, string apellidoMaterno)
        {
            // create json data 
            bcp.ProveedorCliente bcpProvCli = new bcp.ProveedorCliente();
            string tipo = "V";
            int totalRecords = bcpProvCli.ListarReceptoresGrillaCount(objSession.CodigoEmpresa, tipo, nombre, apellidoPaterno, apellidoMaterno, rut, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            Debug.WriteLine("TOTAL " + totalRecords);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.ProveedorCliente> lst = bcpProvCli.ListarReceptoresGrilla(objSession.CodigoEmpresa, tipo, nombre, apellidoPaterno, apellidoMaterno, rut, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.ProveedorCliente item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Rut,
                            item.Nombre,
                            item.NombreFantasia,
                            item.ApellidoPaterno
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "Motivo Cobranza"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMotivoCobranza(GridSettings gridSettings)
        {
            // create json data
            int totalRecords = 1000;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<MotivoCobranzaModel> list = new List<MotivoCobranzaModel>();
            for (int iX = startRow; iX < endRow; iX++)
            {
                MotivoCobranzaModel motivo = new MotivoCobranzaModel();
                motivo.Id = iX;
                motivo.Nombre = "Line# " + iX.ToString();
                list.Add(motivo);
            }

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from MotivoCobranzaModel item in list
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult OperMotivoCobranza(bcp.MotivoCobranza model, string oper, int? id)
        //{
        //    model.OperMotivoCobranza(oper, id, objSession);
        //    return Json("OK");
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditMotivoCobranza(FormCollection form)
        {
            string lll = form.ToString();
            return Json(lll);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelMotivoCobranza(FormCollection form)
        {
            string lll = form.ToString();
            return Json(lll);
        }
        #endregion

        #region "Acciones"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAcciones(GridSettings gridSettings)
        {
            // create json data 
            bcp.Accion bcpAccion = new Accion();
            List<dto.Accion> lstAccion = bcpAccion.ListarAccionesGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            
            int totalRecords = lstAccion.Count;
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
                    from dto.Accion item in lstAccion
                    select new
                    {
                        id = item.IdAccion,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Idioma,
                            item.IdAccion,
                            item.Nombre,
                            item.Agrupa
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperAccion(bcp.Accion model, string oper, int? id)
        {
            model.OperAccion( oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "Categorias"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCategorias(GridSettings gridSettings, bcp.Categoria model)
        {
            // create json data 
            bcp.Categoria bcp = new Categoria();

            int totalRecords = bcp.ListarCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);
            
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Categoria> lst = bcp.ListarGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Categoria item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Id,
                            item.Nombre,
                            item.Utilizacion
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperCategorias(bcp.Categoria model, string oper, int? id)
        {
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "Giros"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetGiros(GridSettings gridSettings, bcp.Giro model)
        {
            // create json data 
            bcp.Giro bcp = new Giro();

            int totalRecords = bcp.ListarCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.Giro> lst = bcp.ListarGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Giro item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Id,
                            item.Nombre
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperGiros(bcp.Giro model, string oper, int? id)
        {
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "SuperCategorias"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetSuperCategorias(GridSettings gridSettings, bcp.SuperCategoria model)
        {
            // create json data 
            bcp.SuperCategoria bcp = new SuperCategoria();

            int totalRecords = bcp.ListarCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.SuperCategoria> lst = bcp.ListarGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.SuperCategoria item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Id,
                            item.Nombre,
                            item.Orden,
                            item.Utilizacion
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperSuperCategorias(bcp.SuperCategoria model, string oper, int? id)
        {
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "TiposContacto"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTiposContacto(GridSettings gridSettings, bcp.TipoContacto model)
        {
            // create json data 
            bcp.TipoContacto bcp = new TipoContacto();

            int totalRecords = bcp.ListarCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TipoContacto> lst = bcp.ListarGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TipoContacto item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Id,
                            item.Nombre
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTiposContacto(bcp.TipoContacto model, string oper, int? id)
        {
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "TiposProvCli"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTiposProvCli(GridSettings gridSettings, bcp.TipoProvCli model)
        {
            // create json data 
            bcp.TipoProvCli bcp = new TipoProvCli();

            int totalRecords = bcp.ListarCount(objSession.CodigoEmpresa, objSession.Idioma, gridSettings.where.groupOp, gridSettings.sortColumn, gridSettings.sortOrder, gridSettings.pageIndex, gridSettings.pageSize);

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<dto.TipoProvCli> lst = bcp.ListarGrilla(objSession.CodigoEmpresa, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), startRow, endRow);


            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.TipoProvCli item in lst
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Id,
                            item.Nombre,
                            item.Agrupa
                            
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperTiposProvCli(bcp.TipoProvCli model, string oper, int? id)
        {
            model.OperAccion(oper, id, objSession);
            return Json("OK");
        }
        #endregion

        public ActionResult ExportToExcel(GridSettings gridSettings)
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

            bcp.Accion objAccion = new Accion();


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

        public JsonResult ListarRegion(int pais)
        {
            //Direccion objDireccion = new Direccion();
            return Json(new SelectList(bcp.ProveedorCliente.ListarRegion(pais), "Value", "Text"));
        }

        public JsonResult ListarCiudad(int region)
        {
            //Direccion objDireccion = new Direccion();
            return Json(new SelectList(bcp.ProveedorCliente.ListarCiudad(region), "Value", "Text"));
        }

        public JsonResult ListarComuna(int ciudad)
        {
            //Direccion objDireccion = new Direccion();
            return Json(new SelectList(bcp.ProveedorCliente.ListarComuna(ciudad), "Value", "Text"));
        }
    }
}