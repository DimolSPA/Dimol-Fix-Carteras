using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PJSpider.dto;
using PJSpider.dao;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Transactions;
using System.Threading;
using System.Globalization;
using OpenQA.Selenium.PhantomJS;
using HtmlAgilityPack;
using System.Collections.Specialized;
using System.Configuration;

namespace PJSpider.bcp
{
    public class Causa
    {
        public static void ActualizarPoderJudicial(int codemp, int idioma, string estados)
        {
            ConsultaCausa obj = new ConsultaCausa();
            List<RolActualizar> lstRoles = new List<RolActualizar>();
            Dimol.dao.Funciones.InsertarError("Generando lista de roles para actualizar", "Estados: " + estados, "Bot Poder Judicial", 0);
            lstRoles = ListarRolesActualizar(codemp,idioma, estados);
            //Dimol.dao.Funciones.InsertarError("Lista de roles para actualizar", "Cantidad: " + lstRoles.Count, "Bot Poder Judicial", 0);
            
                Dimol.dao.Funciones.InsertarError("Lista de roles para actualizar", "Cantidad: " + lstRoles.Count, "Bot Poder Judicial", 0);
                // Get all the ids of interest.
                // I assume you mark successfully updated rows in some way
                // in the update transaction.
                //List<int> ids = lstRoles.Select(item => item.Rolid).ToList();



                // Either allow the TaskParallel library to select what it considers
                // as the optimum degree of parallelism by omitting the 
                // ParallelOptions parameter, or specify what you want.
                //Parallel.ForEach(lstRoles, new ParallelOptions { MaxDegreeOfParallelism = 4 }, rol => ConsultarCausaParalela(rol));
            

            foreach (RolActualizar r in lstRoles)
            {
                Dimol.dao.Funciones.InsertarError("Actualizando", "Rol: " + r.TipoCausa + "-" + r.Numero + ", Tribunal: " + r.Tribunal, "Bot Poder Judicial", 0);
                obj.RolAnio = r.Anio;
                obj.CodigoTribunal = 0;
                obj.NombreTribunal = r.Tribunal;
                obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                obj.TipoCausa = r.TipoCausa;
                obj.RolCausa = r.Rol;
                obj.Codemp = r.Codemp;
                obj.Rolid = r.Rolid;
                obj.IdCausa = r.IdCausa;
                obj.IdCuaderno = 0;
                obj.FechaUltHistorial = r.FechaUltHistorial;
                obj.FechaUltReceptor = r.FechaUltReceptor;
                ConsultarCausa(obj,0);
            }

            Dimol.dao.Funciones.InsertarError("Fin de roles para actualizar", "Estados: " + estados, "Bot Poder Judicial", 0);
        }

        public static void ActualizarPoderJudicialParticion(int codemp, int idioma, string estados, int particion, int cantidad)
        {
            int inicio = 0;
            inicio = (particion - 1) * cantidad;
            ConsultaCausa obj = new ConsultaCausa();
            List<RolActualizar> lstRoles = new List<RolActualizar>();
            Dimol.dao.Funciones.InsertarError("Generando lista de roles para actualizar", "Estados: " + estados, "Bot Poder Judicial", particion);
            lstRoles = ListarRolesActualizar(codemp, idioma, estados);
            
            if (lstRoles.Count - inicio < cantidad)
            {
                cantidad = lstRoles.Count - inicio;
                if(cantidad <= 0){
                    Dimol.dao.Funciones.InsertarError("Esta particion no es necesaria. Favor cerrar la aplicacion.", "Cantidad: " + lstRoles.Count, "Bot Poder Judicial", particion);
                    throw new Exception("Esta particion no es necesaria. Favor cerrar la aplicacion.");
                }
            }
            List<RolActualizar> lst =lstRoles.GetRange(inicio, cantidad);
            Dimol.dao.Funciones.InsertarError("Lista de roles para actualizar", "Cantidad: " + lst.Count, "Bot Poder Judicial", particion);
            foreach (RolActualizar r in lst)
            {
                Dimol.dao.Funciones.InsertarError("Actualizando", "Rol: " + r.TipoCausa + "-" + r.Numero + ", Tribunal: " + r.Tribunal, "Bot Poder Judicial", particion);
                obj.RolAnio = r.Anio;
                obj.CodigoTribunal = 0;
                obj.NombreTribunal = r.Tribunal;
                obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                obj.TipoCausa = r.TipoCausa;
                obj.RolCausa = r.Rol;
                obj.Codemp = r.Codemp;
                obj.Rolid = r.Rolid;
                obj.IdCausa = r.IdCausa;
                obj.IdCuaderno = 0;
                obj.FechaUltHistorial = r.FechaUltHistorial;
                obj.FechaUltReceptor = r.FechaUltReceptor;
                int retry = 0;
                try
                {
                    ConsultarCausa(obj, particion);
                }
                catch (WebDriverException ex)
                {
                    if (retry < 6)
                    {
                        ConsultarCausa(obj,particion);
                        retry++;
                    }
                    else
                    {
                        Dimol.dao.Funciones.InsertarError("Rol no se pudo actualizar", "Rol: " + obj.RolCausa+"-"+obj.RolAnio, "Bot Poder Judicial", particion);
                    }
                }
                catch (Exception ex)
                {
                    Dimol.dao.Funciones.InsertarError("Rol no se pudo actualizar. Rol: " + obj.RolCausa+"-"+obj.RolAnio+ ". Mensaje: " + ex.Message, ex .StackTrace, "Bot Poder Judicial", particion);
             
                }

                
            }

            Dimol.dao.Funciones.InsertarError("Fin de roles para actualizar", "Estados: " + estados, "Bot Poder Judicial",particion);
        }

        private static void ConsultarCausaParalela(RolActualizar r)
        {
            try
            {
                ConsultaCausa obj = new ConsultaCausa();
                Dimol.dao.Funciones.InsertarError("Actualizando", "Rol: " + r.TipoCausa + "-" + r.Numero + ", Tribunal: " + r.Tribunal, "Bot Poder Judicial", 0);
                obj.RolAnio = r.Anio;
                obj.CodigoTribunal = 0;
                obj.NombreTribunal = r.Tribunal;
                obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                obj.TipoCausa = r.TipoCausa;
                obj.RolCausa = r.Rol;
                obj.Codemp = r.Codemp;
                obj.Rolid = r.Rolid;
                obj.IdCausa = r.IdCausa;
                obj.IdCuaderno = 0;
                obj.FechaUltHistorial = r.FechaUltHistorial;
                obj.FechaUltReceptor = r.FechaUltReceptor;
               
                // Handle deadlocks
                //DeadlockRetryHelper.Execute(() => ConsultarCausa(obj));
                ConsultarCausa(obj,0);
            }
            catch (Exception e)
            {
                // Too many deadlock retries (or other exception). 
                // Record so we can diagnose problem or retry later
                //problematicIds.Add(new ErrorType(id, e));
                Dimol.dao.Funciones.InsertarError("DeadLock", "Rolid: " + r.Rolid, "Bot Poder Judicial", 0);
            }
        }

        public static void ActualizarPoderJudicialCliente(int codemp, int idioma, string estados, int pclid)
        {
            ConsultaCausa obj = new ConsultaCausa();
            List<RolActualizar> lstRoles = new List<RolActualizar>();
            Dimol.dao.Funciones.InsertarError("Generando lista de roles para actualizar", "Estados: " + estados, "Bot Poder Judicial", 0);
            lstRoles = ListarRolesActualizarCliente(codemp, idioma, estados,pclid);
            Dimol.dao.Funciones.InsertarError("Lista de roles para actualizar", "Cantidad: " + lstRoles.Count, "Bot Poder Judicial", 0);

            foreach (RolActualizar r in lstRoles)
            {

                Dimol.dao.Funciones.InsertarError("Actualizando", "Rol: " + r.TipoCausa + "-" + r.Numero + ", Tribunal: " + r.Tribunal, "Bot Poder Judicial", 0);
                obj.RolAnio = r.Anio;
                obj.CodigoTribunal = 0;
                obj.NombreTribunal = r.Tribunal;
                obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                obj.TipoCausa = r.TipoCausa;
                obj.RolCausa = r.Rol;
                obj.Codemp = r.Codemp;
                obj.Rolid = r.Rolid;
                obj.IdCausa = r.IdCausa;
                obj.IdCuaderno = 0;
                obj.FechaUltHistorial = r.FechaUltHistorial;
                obj.FechaUltReceptor = r.FechaUltReceptor;
                ConsultarCausa(obj,0);
            }

            Dimol.dao.Funciones.InsertarError("Fin de roles para actualizar", "Estados: " + estados, "Bot Poder Judicial", 0);
        }

        public static void ActualizarPoderJudicialRol(int codemp, int idioma, string estados, string tipo, int rol, int anio, string tribunal, int rolid)
        {
            ConsultaCausa obj = new ConsultaCausa();


        
                obj.RolAnio = anio;
                obj.CodigoTribunal = 0;
                obj.NombreTribunal = tribunal;
                obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                obj.TipoCausa = tipo;
                obj.RolCausa = rol;
                obj.Codemp = codemp;
                obj.Rolid = rolid;
                obj.IdCausa = 0;
                obj.IdCuaderno = 0;
                obj.FechaUltHistorial = new DateTime();
                obj.FechaUltReceptor = new DateTime();
                ConsultarCausa(obj, 0);
            
        }


        public static void ConsultarCausa(ConsultaCausa objCausa, int particion)
        {
            //using (var scope = new TransactionScope(
            //        TransactionScopeOption.Required,
            //        new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            //{
                //Dimol.dao.Funciones.InsertarError("Descargando datos del PJ.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString(), "Bot Poder Judicial", 0);
                // Create a new instance of the Firefox driver.
                IWebDriver driver = new FirefoxDriver();// PhantomJSDriver(@"c:\");
                driver.Navigate().GoToUrl(objCausa.Url);
                try
                {
                    
                    string format = "dd/MM/yyyy";

                    while (!driver.Title.Contains("CONSULTA CAUSAS"))
                    {
                        Dimol.dao.Funciones.InsertarError("Pagina de consulta de Causas del Poder Judicial offline.", "", "Bot Poder Judicial", particion);
                        Thread.Sleep(10);
                        driver.Navigate().GoToUrl(objCausa.Url);
                    }

                    if (driver.Title.Contains("CONSULTA CAUSAS"))
                    {
                        driver.SwitchTo().Frame(driver.FindElement(By.Name("body")));

                        // Find the text input element by its name
                        IWebElement tipoCausa = driver.FindElement(By.Name("TIP_Causa"));
                        IWebElement rol = driver.FindElement(By.Name("ROL_Causa"));
                        IWebElement anio = driver.FindElement(By.Name("ERA_Causa"));
                        IWebElement tribunal = driver.FindElement(By.Name("COD_Tribunal"));

                        // Enter something to search for
                        rol.SendKeys(objCausa.RolCausa.ToString());
                        anio.SendKeys(objCausa.RolAnio.ToString());
                        tipoCausa.SendKeys(objCausa.TipoCausa);
                        tribunal.SendKeys(objCausa.NombreTribunal.Trim());

                        IWebElement btnBuscar = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));

                        // Now submit the form. WebDriver will find the form for us from the element
                        btnBuscar.Click();

                        if (driver.FindElements(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]")).Count == 0)
                        {
                            Dimol.dao.Funciones.InsertarError("Rol no encontrado.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString() + ", Tribunal: " + objCausa.NombreTribunal, "Bot Poder Judicial", particion);
                            throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
                        }
                        IWebElement urlCausa = driver.FindElement(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]"));

                        if (objCausa.IdCausa == 0)
                        {
                            string url = urlCausa.GetAttribute("href");
                            string queryString = url.Remove(0, url.IndexOf('?') + 1);
                            string[] parametros = queryString.Split('&');
                            string[] parametro;
                            foreach (string s in parametros)
                            {
                                parametro = s.Split('=');
                                switch (parametro[0])
                                {
                                    case "TIP_Consulta":
                                        objCausa.TipoConsulta = Int32.Parse(parametro[1]);
                                        break;
                                    case "TIP_Cuaderno":
                                        objCausa.TipoCuaderno = Int32.Parse(parametro[1]);
                                        break;
                                    case "CRR_IdCuaderno":
                                        objCausa.IdCuaderno = Int32.Parse(parametro[1]);
                                        break;
                                    case "CRR_IdCausa":
                                        objCausa.IdCausa = Int32.Parse(parametro[1]);
                                        break;
                                    case "TIP_Informe":
                                        objCausa.TipoInforme = Int32.Parse(parametro[1]);
                                        break;
                                    case "COD_Tribunal":
                                        objCausa.CodigoTribunal = Int32.Parse(parametro[1]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        InsertarRolPoderJudicial(objCausa.Codemp, objCausa.Rolid, objCausa.TipoCausa, objCausa.IdCausa, objCausa.CodigoTribunal);

                        urlCausa.Click();

                        IWebElement tipoCuaderno = driver.FindElement(By.Name("CRR_Cuaderno"));
                        //string cuadernoSeleccionado = tipoCuaderno.GetAttribute("value");
                        SelectElement selectList = new SelectElement(tipoCuaderno);
                        IList<IWebElement> options = selectList.Options;
                        IWebElement btnCuaderno = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));
                        List<string> lstCuadernos = new List<string>();
                        foreach (IWebElement e in options)
                        {
                            lstCuadernos.Add(e.Text);
                        }

                        foreach (string e in lstCuadernos)
                        {
                            tipoCuaderno.SendKeys(e);
                            btnCuaderno = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));
                            btnCuaderno.Click();
                            tipoCuaderno = driver.FindElement(By.Name("CRR_Cuaderno"));
                            InsertarTipoCuaderno(objCausa.IdCausa, Int32.Parse(tipoCuaderno.GetAttribute("value")), e, "N");
                            int idCuaderno = Int32.Parse(tipoCuaderno.GetAttribute("value"));

                            IWebElement divHistorial = driver.FindElement(By.Id("Historia"));
                            IWebElement divLitigantes = driver.FindElement(By.Id("Litigantes"));
                            IWebElement divNotificaciones = driver.FindElement(By.Id("Notificaciones"));
                            IWebElement divEscritos = driver.FindElement(By.Id("Escritos"));

                            IWebElement tablaHistorial = divHistorial.FindElement(By.XPath("//table[@bgcolor='white']"));
                            //IReadOnlyCollection<IWebElement> tablaLitigantes = divLitigantes.FindElements(By.TagName("table"));
                            //IReadOnlyCollection<IWebElement> tablaNotificaciones = divNotificaciones.FindElements(By.TagName("table"));
                            IWebElement tablaEscritos = divEscritos.FindElement(By.XPath("//table[@width='620']"));
                            IReadOnlyCollection<IWebElement> rowsEscritos = tablaEscritos.FindElements(By.TagName("tr"));

                            if (rowsEscritos.Count > 0)
                            {
                               InsertarTipoCuaderno(objCausa.IdCausa, Int32.Parse(tipoCuaderno.GetAttribute("value")), e, "S");
                            }


                            IReadOnlyCollection<IWebElement> rows = tablaHistorial.FindElements(By.TagName("tr"));
                            foreach (IWebElement row in rows)
                            {
                                IReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                                int i = 1;
                                TablaHistorial tbl = new TablaHistorial();
                                foreach (IWebElement cell in cells)
                                {
                                    switch (i)
                                    {
                                        case 1:
                                            tbl.Folio = cell.Text;
                                            break;
                                        case 2:
                                            string[] ruta = new string[] { "", "" };
                                            if (cell.FindElements(By.XPath("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']")).Count > 0)
                                            {
                                                IWebElement btnPDF = cell.FindElement(By.XPath("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']"));
                                                ruta = btnPDF.GetAttribute("onclick").Split('\'');
                                            }
                                            else if (cell.FindElements(By.XPath("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']")).Count > 0)
                                            {
                                                IWebElement btnWord = cell.FindElement(By.XPath("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']"));
                                                ruta = btnWord.GetAttribute("onclick").Split('\'');
                                            }
                                            if (!string.IsNullOrEmpty(ruta[1]))
                                            {
                                                tbl.RutaDocumento = "http://civil.poderjudicial.cl" + ruta[1];
                                            }
                                            break;
                                        case 3:
                                            tbl.Etapa = cell.Text;
                                            break;
                                        case 4:
                                            tbl.Tramite = cell.Text;
                                            break;
                                        case 5:
                                            tbl.DescTramite = cell.Text;
                                            break;
                                        case 6:
                                            tbl.FechaTramite = DateTime.ParseExact(cell.Text, format, CultureInfo.InvariantCulture);
                                            break;
                                        case 7:
                                            tbl.Foja = cell.Text;
                                            break;
                                    }
                                    i++;
                                }
                                if (objCausa.FechaUltHistorial < tbl.FechaTramite)
                                {
                                    objCausa.TablaHistorial.Add(tbl);
                                    InsertarHistorialPoderJudicial(objCausa.IdCausa, idCuaderno, tbl.Folio, tbl.RutaDocumento, tbl.Etapa, tbl.Tramite, tbl.DescTramite, tbl.FechaTramite, Int32.Parse(tbl.Foja));
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        IWebElement btnReceptor = driver.FindElement(By.XPath("//img[@alt='Información de Receptor']"));
                        btnReceptor.Click();
                        IWebElement informacionReceptor = driver.FindElement(By.Id("ReceptorDIV"));
                        IWebElement tablaReceptores = informacionReceptor.FindElement(By.XPath("//table[@style='width: 635px; background-color: #e0ecfa; border-color: #6891AB; border-style:solid; border-width:1px; padding:1px;']"));

                        IReadOnlyCollection<IWebElement> rowsReceptor = tablaReceptores.FindElements(By.TagName("tr"));
                        foreach (IWebElement row in rowsReceptor)
                        {
                            IReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                            int i = 1;
                            TablaReceptores tbl = new TablaReceptores();
                            foreach (IWebElement cell in cells)
                            {
                                switch (i)
                                {
                                    case 1:
                                        tbl.Cuaderno = cell.Text;
                                        break;
                                    case 2:
                                        string[] receptor = cell.Text.Split('-');
                                        tbl.Receptor = receptor[0];
                                        tbl.Fecha = DateTime.ParseExact((receptor[1].Trim()).Replace(".", ""), format, CultureInfo.InvariantCulture);
                                        break;
                                    case 3:
                                        tbl.Estado = cell.Text;
                                        break;
                                }
                                i++;
                            }
                            if (objCausa.FechaUltReceptor < tbl.Fecha)
                            {
                                objCausa.TablaReceptor.Add(tbl);
                                InsertarReceptorPoderJudicial(objCausa.IdCausa, tbl.Cuaderno, tbl.Estado, tbl.Receptor, tbl.Fecha);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        Dimol.dao.Funciones.InsertarError("Pagina de consulta de Causas del Poder Judicial offline.", "", "Bot Poder Judicial", 0);
                    }

                    //Close the browser
                    driver.Quit();
                }
                catch (Exception ex)
                {
                    //Close the browser
                    driver.Quit();
                    Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Bot Poder Judicial", 0);
                }
                //Dimol.dao.Funciones.InsertarError("Datos descargados del PJ.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString(), "Bot Poder Judicial", particion);
                
                
                //dc.SubmitChanges();
            //    scope.Complete();
            //}
        }

        public static void InsertarTipoCuaderno(int idCausa, int tipoCuaderno, string descCuaderno, string escritosPendientes)
        {
            dao.Causa.InsertarTipoCuaderno(idCausa, tipoCuaderno, descCuaderno, escritosPendientes);
        }

        public static void InsertarRolPoderJudicial(int codemp, int rolid, string tipo, int idCausa,int idTribunal)
        {
            dao.Causa.InsertarRolPoderJudicial(codemp, rolid, tipo, idCausa,idTribunal);
        }

        public static void InsertarHistorialPoderJudicial(int idCausa, int idCuaderno, string folio, string rutaDocumento, string etapa, string tramite, string descTramite, DateTime fechaTramite, int foja)
        {
            dao.Causa.InsertarHistorialPoderJudicial(idCausa, idCuaderno, folio, rutaDocumento, etapa, tramite, descTramite, fechaTramite, foja);
        }

        public static void InsertarReceptorPoderJudicial(int idCausa, string cuaderno, string estado, string receptor, DateTime fecha)
        {
            dao.Causa.InsertarReceptorPoderJudicial(idCausa, cuaderno, estado, receptor, fecha);
        }

        public static List<RolActualizar> ListarRolesActualizar(int codemp, int idioma, string estados)
        {
            return dao.Causa.ListarRolesActualizar( codemp, idioma, estados);
        }

        public static List<RolActualizar> ListarRolesActualizarCliente(int codemp, int idioma, string estados, int pclid)
        {
            return dao.Causa.ListarRolesActualizarCliente(codemp, idioma, estados,pclid);
        }

        #region "Scanner Poder Judicial"

        public static void ExplorarCausa(ScannerCausas objCausa)
        {
            //using (var scope = new TransactionScope(
            //        TransactionScopeOption.Required,
            //        new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            //{
            //Dimol.dao.Funciones.InsertarError("Descargando datos del PJ.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString(), "Bot Poder Judicial", 0);
            // Create a new instance of the Firefox driver.
            IWebDriver driver = new FirefoxDriver();// PhantomJSDriver(@"c:\");
            driver.Navigate().GoToUrl(objCausa.Url);
            try
            {
                while (!driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    Dimol.dao.Funciones.InsertarError("Explorador de Causas del Poder Judicial offline.", "", "Explorador Poder Judicial", objCausa.RolAnio);
                    Thread.Sleep(600000);
                    driver.Navigate().GoToUrl(objCausa.Url);
                }

                if (driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    driver.SwitchTo().Frame(driver.FindElement(By.Name("body")));

                    // Find the text input element by its name
                    IWebElement tipoCausa = driver.FindElement(By.Name("TIP_Causa"));
                    IWebElement rol = driver.FindElement(By.Name("ROL_Causa"));
                    IWebElement anio = driver.FindElement(By.Name("ERA_Causa"));
                    IWebElement tribunal = driver.FindElement(By.Name("COD_Tribunal"));

                    // Enter something to search for
                    rol.SendKeys(objCausa.RolCausa.ToString());
                    anio.SendKeys(objCausa.RolAnio.ToString());
                    tipoCausa.SendKeys(objCausa.TipoCausa);
                    tribunal.SendKeys(objCausa.NombreTribunal.Trim());

                    IWebElement btnBuscar = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));

                    // Now submit the form. WebDriver will find the form for us from the element
                    btnBuscar.Click();

                    if (driver.FindElements(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]")).Count == 0)
                    {
                        Dimol.dao.Funciones.InsertarError("Rol no encontrado.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString() + ", Tribunal: " + objCausa.NombreTribunal, "Explorador Poder Judicial", objCausa.RolAnio);
                        throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
                    }
                    IWebElement urlCausa = driver.FindElement(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]"));

                    if (objCausa.IdCausa == 0)
                    {
                        string url = urlCausa.GetAttribute("href");
                        string queryString = url.Remove(0, url.IndexOf('?') + 1);
                        string[] parametros = queryString.Split('&');
                        string[] parametro;
                        foreach (string s in parametros)
                        {
                            parametro = s.Split('=');
                            switch (parametro[0])
                            {
                                case "TIP_Consulta":
                                    objCausa.TipoConsulta = Int32.Parse(parametro[1]);
                                    break;
                                case "TIP_Cuaderno":
                                    objCausa.TipoCuaderno = Int32.Parse(parametro[1]);
                                    break;
                                case "CRR_IdCuaderno":
                                    objCausa.IdCuaderno = Int32.Parse(parametro[1]);
                                    break;
                                case "CRR_IdCausa":
                                    objCausa.IdCausa = Int32.Parse(parametro[1]);
                                    break;
                                case "TIP_Informe":
                                    objCausa.TipoInforme = Int32.Parse(parametro[1]);
                                    break;
                                case "COD_Tribunal":
                                    objCausa.CodigoTribunal = Int32.Parse(parametro[1]);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    urlCausa.Click();      
                    string[] ruta = new string[] { "", "" };
                    if (driver.FindElements(By.XPath("//td[contains(text(),'Texto Demanda')]")).Count > 0)
                    {
                        IWebElement cell = driver.FindElement(By.XPath("//td[contains(text(),'Texto Demanda')]"));
                        IWebElement demanda = cell.FindElement(By.TagName("img"));
                        if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
                        {
                            ruta = new string[] { "", "" };
                        }
                        else if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
                        {
                            ruta = demanda.GetAttribute("onclick").Split('\'');
                        }
                        else if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
                        {
                            ruta = demanda.GetAttribute("onclick").Split('\'');
                        }
                    }

                    if (!string.IsNullOrEmpty(ruta[1]))
                    {
                        objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
                    }
                    else
                    {
                        objCausa.RutaDemanda = null;
                    }

                    InsertarPoderJudicialRol(objCausa.IdCausa, objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal, objCausa.RutaDemanda,objCausa.FechaIngreso);

                    IWebElement tipoCuaderno = driver.FindElement(By.Name("CRR_Cuaderno"));
                    string cuadernoSeleccionado = tipoCuaderno.Text;
                    IWebElement btnCuaderno = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));
                    btnCuaderno.Click();
                    tipoCuaderno = driver.FindElement(By.Name("CRR_Cuaderno"));
                    InsertarPoderJudicialCuaderno(objCausa.IdCausa, Int32.Parse(tipoCuaderno.GetAttribute("value")), cuadernoSeleccionado, "N");
                    int idCuaderno = Int32.Parse(tipoCuaderno.GetAttribute("value"));

                    IWebElement btntdDos = driver.FindElement(By.XPath("//td[@id='tdDos']"));
                    btntdDos.Click();

                    IWebElement divLitigantes = driver.FindElement(By.Id("Litigantes"));

                    IReadOnlyCollection<IWebElement> tablaLitigantes = divLitigantes.FindElements(By.TagName("table"));

                    foreach (IWebElement tabla in tablaLitigantes)
                    {
                        IReadOnlyCollection<IWebElement> rows = tabla.FindElements(By.TagName("tr"));
                        foreach (IWebElement row in rows)
                        {
                            IReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                            int i = 1;
                            TablaLitigantes tbl = new TablaLitigantes();
                            foreach (IWebElement cell in cells)
                            {
                                switch (i)
                                {
                                    case 1:
                                        tbl.Participante = cell.Text;
                                        break;
                                    case 2:
                                        tbl.Rut = cell.Text;
                                        break;
                                    case 3:
                                        tbl.Persona = cell.Text;
                                        break;
                                    case 4:
                                        tbl.NombreRazonSocial = cell.Text;
                                        break;
                                }
                                i++;
                            }
                            if (tbl.Participante != "Participante")
                            {
                                InsertarPoderJudicialLitigante(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Participante, tbl.Rut, tbl.Persona, tbl.NombreRazonSocial);
                            }
                        }
                    }
                    InsertarPoderJudicialIndice(objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal);

                }
                else
                {
                    Dimol.dao.Funciones.InsertarError("Pagina de consulta de Causas del Poder Judicial offline.", "", "Explorador Poder Judicial", objCausa.RolAnio);
                }

                //Close the browser
                driver.Quit();
            }
            catch (Exception ex)
            {
                //Close the browser
                driver.Quit();
                throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
            }

        }

        public static void InsertarPoderJudicialRol(int idCausa, string tipo, int numero, int anio, int tribunal, string ruta, DateTime fechaIngreso)
        {
            dao.Causa.InsertarPoderJudicialRol(idCausa, tipo, numero, anio, tribunal, ruta,fechaIngreso);
        }

        public static void InsertarPoderJudicialCuaderno(int idCausa, int idCuaderno, string descCuaderno, string escritosPendientes)
        {
            dao.Causa.InsertarPoderJudicialCuaderno(idCausa, idCuaderno, descCuaderno, escritosPendientes);
        }

        public static void InsertarPoderJudicialIndice(string tipo, int numero, int anio, int tribunal)
        {
            dao.Causa.InsertarPoderJudicialIndice(tipo, numero, anio, tribunal);
        }

        public static void InsertarPoderJudicialLitigante(int idCausa, int idCuaderno, string participante, string rut, string tipoPersona, string nombre)
        {
            dao.Causa.InsertarPoderJudicialLitigante(idCausa, idCuaderno, participante, rut, tipoPersona, nombre);
        }

        public static List<Dimol.dto.Combobox> ListarTribunalesScanner()
        {
            return dao.Causa.ListarTribunalesScanner();
        }

        public static List<Dimol.dto.Combobox> ListarTribunalesScannerRango(int inicio, int termino)
        {
            return dao.Causa.ListarTribunalesScannerRango(inicio, termino);
        }

        public static List<IndiceScanner> ListarIndiceScanner()
        {
            return dao.Causa.ListarIndiceScanner();
        }

        public static List<IndiceScanner> ListarRolesScanner(int anio)
        {
            return dao.Causa.ListarRolesScanner(anio);
        }

        public static List<IndiceScanner> ListarIndiceScannerFecha()
        {
            return dao.Causa.ListarIndiceScannerFecha();
        }

        public static List<int> ListarRolesScanner(int anio, int tribunal)
        {
            return dao.Causa.ListarRolesScanner(anio, tribunal);
        }

        public static void ExplorarCausaConFecha(ScannerCausas objCausa)
        {
            //using (var scope = new TransactionScope(
            //        TransactionScopeOption.Required,
            //        new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            //{
            //Dimol.dao.Funciones.InsertarError("Descargando datos del PJ.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString(), "Bot Poder Judicial", 0);
            // Create a new instance of the Firefox driver.
            IWebDriver driver = new FirefoxDriver();// PhantomJSDriver(@"c:\");
            driver.Navigate().GoToUrl(objCausa.Url);
            try
            {

                string format = "dd/MM/yyyy";
                while (!driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    Dimol.dao.Funciones.InsertarError("Explorador de Causas del Poder Judicial offline.", "", "Explorador Poder Judicial", objCausa.RolAnio);
                    Thread.Sleep(600000);
                    driver.Navigate().GoToUrl(objCausa.Url);
                }

                if (driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    driver.SwitchTo().Frame(driver.FindElement(By.Name("body")));

                    // Find the text input element by its name
                    IWebElement tipoCausa = driver.FindElement(By.Name("TIP_Causa"));
                    IWebElement rol = driver.FindElement(By.Name("ROL_Causa"));
                    IWebElement anio = driver.FindElement(By.Name("ERA_Causa"));
                    IWebElement tribunal = driver.FindElement(By.Name("COD_Tribunal"));

                    // Enter something to search for
                    rol.SendKeys(objCausa.RolCausa.ToString());
                    anio.SendKeys(objCausa.RolAnio.ToString());
                    tipoCausa.SendKeys(objCausa.TipoCausa);
                    tribunal.SendKeys(objCausa.NombreTribunal.Trim());

                    IWebElement btnBuscar = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));

                    // Now submit the form. WebDriver will find the form for us from the element
                    btnBuscar.Click();

                    if (driver.FindElements(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]")).Count == 0)
                    {
                        Dimol.dao.Funciones.InsertarError("Rol no encontrado.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString() + ", Tribunal: " + objCausa.NombreTribunal, "Explorador Poder Judicial", objCausa.RolAnio);
                        throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
                    }
                    IWebElement urlCausa = driver.FindElement(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]"));

                    if (objCausa.IdCausa == 0)
                    {
                        string url = urlCausa.GetAttribute("href");
                        string queryString = url.Remove(0, url.IndexOf('?') + 1);
                        string[] parametros = queryString.Split('&');
                        string[] parametro;
                        foreach (string s in parametros)
                        {
                            parametro = s.Split('=');
                            switch (parametro[0])
                            {
                                case "TIP_Consulta":
                                    objCausa.TipoConsulta = Int32.Parse(parametro[1]);
                                    break;
                                case "TIP_Cuaderno":
                                    objCausa.TipoCuaderno = Int32.Parse(parametro[1]);
                                    break;
                                case "CRR_IdCuaderno":
                                    objCausa.IdCuaderno = Int32.Parse(parametro[1]);
                                    break;
                                case "CRR_IdCausa":
                                    objCausa.IdCausa = Int32.Parse(parametro[1]);
                                    break;
                                case "TIP_Informe":
                                    objCausa.TipoInforme = Int32.Parse(parametro[1]);
                                    break;
                                case "COD_Tribunal":
                                    objCausa.CodigoTribunal = Int32.Parse(parametro[1]);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    urlCausa.Click();
                    string[] ruta = new string[] { "", "" };
                    if (driver.FindElements(By.XPath("//td[contains(text(),'Texto Demanda')]")).Count > 0)
                    {
                        IWebElement cell = driver.FindElement(By.XPath("//td[contains(text(),'Texto Demanda')]"));
                        IWebElement demanda = cell.FindElement(By.TagName("img"));
                        if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
                        {
                            ruta = new string[] { "", "" };
                        }
                        else if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
                        {
                            ruta = demanda.GetAttribute("onclick").Split('\'');
                        }
                        else if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
                        {
                            ruta = demanda.GetAttribute("onclick").Split('\'');
                        }
                    }
                    string fecha = "";
                    if (driver.FindElements(By.XPath("//td[contains(text(),'F. Ing :')]")).Count > 0)
                    {
                        IWebElement cell = driver.FindElement(By.XPath("//td[contains(text(),'F. Ing :')]"));
                        fecha = cell.Text.Replace("F. Ing :", "");
                    }
                    if (!string.IsNullOrEmpty(fecha))
                    {
                        objCausa.FechaIngreso =  DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        objCausa.FechaIngreso = new DateTime();
                    }
                    if (!string.IsNullOrEmpty(ruta[1]))
                    {
                        objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
                    }
                    else
                    {
                        objCausa.RutaDemanda = null;
                    }

                    InsertarPoderJudicialRol(objCausa.IdCausa, objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal, objCausa.RutaDemanda,objCausa.FechaIngreso);

                    IWebElement tipoCuaderno = driver.FindElement(By.Name("CRR_Cuaderno"));
                    string cuadernoSeleccionado = tipoCuaderno.Text;
                    IWebElement btnCuaderno = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));
                    btnCuaderno.Click();
                    tipoCuaderno = driver.FindElement(By.Name("CRR_Cuaderno"));
                    InsertarPoderJudicialCuaderno(objCausa.IdCausa, Int32.Parse(tipoCuaderno.GetAttribute("value")), cuadernoSeleccionado, "N");
                    int idCuaderno = Int32.Parse(tipoCuaderno.GetAttribute("value"));

                    IWebElement btntdDos = driver.FindElement(By.XPath("//td[@id='tdDos']"));
                    btntdDos.Click();

                    IWebElement divLitigantes = driver.FindElement(By.Id("Litigantes"));

                    IReadOnlyCollection<IWebElement> tablaLitigantes = divLitigantes.FindElements(By.TagName("table"));

                    foreach (IWebElement tabla in tablaLitigantes)
                    {
                        IReadOnlyCollection<IWebElement> rows = tabla.FindElements(By.TagName("tr"));
                        foreach (IWebElement row in rows)
                        {
                            IReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                            int i = 1;
                            TablaLitigantes tbl = new TablaLitigantes();
                            foreach (IWebElement cell in cells)
                            {
                                switch (i)
                                {
                                    case 1:
                                        tbl.Participante = cell.Text;
                                        break;
                                    case 2:
                                        tbl.Rut = cell.Text;
                                        break;
                                    case 3:
                                        tbl.Persona = cell.Text;
                                        break;
                                    case 4:
                                        tbl.NombreRazonSocial = cell.Text;
                                        break;
                                }
                                i++;
                            }
                            if (tbl.Participante != "Participante")
                            {
                                InsertarPoderJudicialLitigante(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Participante, tbl.Rut, tbl.Persona, tbl.NombreRazonSocial);
                            }
                        }
                    }
                    InsertarPoderJudicialIndice(objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal);

                }
                else
                {
                    Dimol.dao.Funciones.InsertarError("Pagina de consulta de Causas del Poder Judicial offline.", "", "Explorador Poder Judicial", objCausa.RolAnio);
                }

                //Close the browser
                driver.Quit();
            }
            catch (Exception ex)
            {
                //Close the browser
                driver.Quit();
                throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
            }

        }

        public static void ExplorarCausaActualizaFecha(ScannerCausas objCausa)
        {
            IWebDriver driver = new FirefoxDriver();// PhantomJSDriver(@"c:\");
            driver.Navigate().GoToUrl(objCausa.Url);
            try
            {

                string format = "dd/MM/yyyy";
                while (!driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    Dimol.dao.Funciones.InsertarError("Explorador de Causas del Poder Judicial offline.", "", "Explorador Poder Judicial", objCausa.RolAnio);
                    Thread.Sleep(600000);
                    driver.Navigate().GoToUrl(objCausa.Url);
                }

                if (driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    driver.SwitchTo().Frame(driver.FindElement(By.Name("body")));

                    // Find the text input element by its name
                    IWebElement tipoCausa = driver.FindElement(By.Name("TIP_Causa"));
                    IWebElement rol = driver.FindElement(By.Name("ROL_Causa"));
                    IWebElement anio = driver.FindElement(By.Name("ERA_Causa"));
                    IWebElement tribunal = driver.FindElement(By.Name("COD_Tribunal"));

                    // Enter something to search for
                    rol.SendKeys(objCausa.RolCausa.ToString());
                    anio.SendKeys(objCausa.RolAnio.ToString());
                    tipoCausa.SendKeys(objCausa.TipoCausa);
                    tribunal.SendKeys(objCausa.NombreTribunal.Trim());

                    IWebElement btnBuscar = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));

                    // Now submit the form. WebDriver will find the form for us from the element
                    btnBuscar.Click();

                    if (driver.FindElements(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]")).Count == 0)
                    {
                        Dimol.dao.Funciones.InsertarError("Rol no encontrado.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString() + ", Tribunal: " + objCausa.NombreTribunal, "Explorador Poder Judicial", objCausa.RolAnio);
                        throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
                    }
                    IWebElement urlCausa = driver.FindElement(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]"));

                    if (objCausa.IdCausa == 0)
                    {
                        string url = urlCausa.GetAttribute("href");
                        string queryString = url.Remove(0, url.IndexOf('?') + 1);
                        string[] parametros = queryString.Split('&');
                        string[] parametro;
                        foreach (string s in parametros)
                        {
                            parametro = s.Split('=');
                            switch (parametro[0])
                            {
                                case "TIP_Consulta":
                                    objCausa.TipoConsulta = Int32.Parse(parametro[1]);
                                    break;
                                case "TIP_Cuaderno":
                                    objCausa.TipoCuaderno = Int32.Parse(parametro[1]);
                                    break;
                                case "CRR_IdCuaderno":
                                    objCausa.IdCuaderno = Int32.Parse(parametro[1]);
                                    break;
                                case "CRR_IdCausa":
                                    objCausa.IdCausa = Int32.Parse(parametro[1]);
                                    break;
                                case "TIP_Informe":
                                    objCausa.TipoInforme = Int32.Parse(parametro[1]);
                                    break;
                                case "COD_Tribunal":
                                    objCausa.CodigoTribunal = Int32.Parse(parametro[1]);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    urlCausa.Click();
                    //string[] ruta = new string[] { "", "" };
                    //if (driver.FindElements(By.XPath("//td[contains(text(),'Texto Demanda')]")).Count > 0)
                    //{
                    //    IWebElement cell = driver.FindElement(By.XPath("//td[contains(text(),'Texto Demanda')]"));
                    //    IWebElement demanda = cell.FindElement(By.TagName("img"));
                    //    if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
                    //    {
                    //        ruta = new string[] { "", "" };
                    //    }
                    //    else if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
                    //    {
                    //        ruta = demanda.GetAttribute("onclick").Split('\'');
                    //    }
                    //    else if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
                    //    {
                    //        ruta = demanda.GetAttribute("onclick").Split('\'');
                    //    }
                    //}
                    string fecha = "";
                    if (driver.FindElements(By.XPath("//td[contains(text(),'F. Ing :')]")).Count > 0)
                    {
                        IWebElement cell = driver.FindElement(By.XPath("//td[contains(text(),'F. Ing :')]"));
                        fecha = cell.Text.Replace("F. Ing :", "");
                    }
                    if (!string.IsNullOrEmpty(fecha))
                    {
                        objCausa.FechaIngreso = DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        objCausa.FechaIngreso = new DateTime();
                    }
                    //if (!string.IsNullOrEmpty(ruta[1]))
                    //{
                    //    objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
                    //}
                    //else
                    //{
                    //    objCausa.RutaDemanda = null;
                    //}

                    ActualizarPoderJudicialRolFecha(objCausa.IdCausa, objCausa.FechaIngreso);

                }
                //Close the browser
                driver.Quit();
            }
            catch (Exception ex)
            {
                //Close the browser
                driver.Quit();
                throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
            }

        }

        public static void ActualizarPoderJudicialRolFecha(int idCausa, DateTime fechaIngreso)
        {
            dao.Causa.ActualizarPoderJudicialRolFecha(idCausa, fechaIngreso);
        }

        #endregion

        #region "Nuevo Scanner sin cliente"

        public static List<ScannerHTML> ListarRolesHTML()
        {
            return dao.Causa.ListarRolesHTML();
        }

        public static List<ScannerHTML> ListarRolesHTMLOrden(string orden)
        {
            return dao.Causa.ListarRolesHTMLOrden(orden);
        }

        public static void InsertarPoderJudicialRolHTML(int idCausa, int idCuaderno, string tipo, int numero, int anio, int tribunal, string html)
        {
            dao.Causa.InsertarPoderJudicialRolHTML( idCausa, idCuaderno, tipo,  numero, anio, tribunal,  html);
        }

        public static List<ScannerHTML> ListarCausasHTML()
        {
            return dao.Causa.ListarCausaHTML();
        }

        public static void ActualizaFechaIngresoCausa()
        {
            List<ScannerHTML> lst = PJSpider.bcp.Causa.ListarCausasHTML();
            HtmlDocument html = new HtmlDocument();
            string fecha= "";
            string format = "dd/MM/yyyy";
            while (lst.Count > 0)
            {
                foreach (ScannerHTML s in lst)
                {
                    fecha = "";
                    try
                    {
                        // Example of loading your HTML into an HtmlDocument object

                        html.LoadHtml(s.HTML);
                    }
                    catch (Exception ex)
                    {
                        Exception exx = new Exception("No se pudo crear el documento HTML.", ex);
                        throw exx;
                    }

                    try
                    {
                        foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'F. Ing :')]]"))
                        {
                            fecha = td.InnerText.Replace("F. Ing :", "").Replace(" \r\n\t\t","");
                            if (!string.IsNullOrEmpty(fecha))
                            {
                                dao.Causa.ActualizarPoderJudicialRolFecha(s.IdCausa, DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture));
                                dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno,"F");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                    }

                }
                lst = PJSpider.bcp.Causa.ListarCausasHTML();
            }
            //IWebDriver driver = new FirefoxDriver();// PhantomJSDriver(@"c:\");
            //driver.Navigate().GoToUrl("");
            //try
            //{

            //    string format = "dd/MM/yyyy";
            //    while (!driver.Title.Contains("CONSULTA CAUSAS"))
            //    {
            //        Dimol.dao.Funciones.InsertarError("Explorador de Causas del Poder Judicial offline.", "", "Explorador Poder Judicial", objCausa.RolAnio);
            //        Thread.Sleep(600000);
            //        driver.Navigate().GoToUrl(objCausa.Url);
            //    }

            //    if (driver.Title.Contains("CONSULTA CAUSAS"))
            //    {
            //        driver.SwitchTo().Frame(driver.FindElement(By.Name("body")));

            //        // Find the text input element by its name
            //        IWebElement tipoCausa = driver.FindElement(By.Name("TIP_Causa"));
            //        IWebElement rol = driver.FindElement(By.Name("ROL_Causa"));
            //        IWebElement anio = driver.FindElement(By.Name("ERA_Causa"));
            //        IWebElement tribunal = driver.FindElement(By.Name("COD_Tribunal"));

            //        // Enter something to search for
            //        rol.SendKeys(objCausa.RolCausa.ToString());
            //        anio.SendKeys(objCausa.RolAnio.ToString());
            //        tipoCausa.SendKeys(objCausa.TipoCausa);
            //        tribunal.SendKeys(objCausa.NombreTribunal.Trim());

            //        IWebElement btnBuscar = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));

            //        // Now submit the form. WebDriver will find the form for us from the element
            //        btnBuscar.Click();

            //        if (driver.FindElements(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]")).Count == 0)
            //        {
            //            Dimol.dao.Funciones.InsertarError("Rol no encontrado.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString() + ", Tribunal: " + objCausa.NombreTribunal, "Explorador Poder Judicial", objCausa.RolAnio);
            //            throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
            //        }
            //        IWebElement urlCausa = driver.FindElement(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]"));

            //        if (objCausa.IdCausa == 0)
            //        {
            //            string url = urlCausa.GetAttribute("href");
            //            string queryString = url.Remove(0, url.IndexOf('?') + 1);
            //            string[] parametros = queryString.Split('&');
            //            string[] parametro;
            //            foreach (string s in parametros)
            //            {
            //                parametro = s.Split('=');
            //                switch (parametro[0])
            //                {
            //                    case "TIP_Consulta":
            //                        objCausa.TipoConsulta = Int32.Parse(parametro[1]);
            //                        break;
            //                    case "TIP_Cuaderno":
            //                        objCausa.TipoCuaderno = Int32.Parse(parametro[1]);
            //                        break;
            //                    case "CRR_IdCuaderno":
            //                        objCausa.IdCuaderno = Int32.Parse(parametro[1]);
            //                        break;
            //                    case "CRR_IdCausa":
            //                        objCausa.IdCausa = Int32.Parse(parametro[1]);
            //                        break;
            //                    case "TIP_Informe":
            //                        objCausa.TipoInforme = Int32.Parse(parametro[1]);
            //                        break;
            //                    case "COD_Tribunal":
            //                        objCausa.CodigoTribunal = Int32.Parse(parametro[1]);
            //                        break;
            //                    default:
            //                        break;
            //                }
            //            }
            //        }

            //        urlCausa.Click();
            //        //string[] ruta = new string[] { "", "" };
            //        //if (driver.FindElements(By.XPath("//td[contains(text(),'Texto Demanda')]")).Count > 0)
            //        //{
            //        //    IWebElement cell = driver.FindElement(By.XPath("//td[contains(text(),'Texto Demanda')]"));
            //        //    IWebElement demanda = cell.FindElement(By.TagName("img"));
            //        //    if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
            //        //    {
            //        //        ruta = new string[] { "", "" };
            //        //    }
            //        //    else if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
            //        //    {
            //        //        ruta = demanda.GetAttribute("onclick").Split('\'');
            //        //    }
            //        //    else if (demanda.GetAttribute("src").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
            //        //    {
            //        //        ruta = demanda.GetAttribute("onclick").Split('\'');
            //        //    }
            //        //}
            //        string fecha = "";
            //        if (driver.FindElements(By.XPath("//td[contains(text(),'F. Ing :')]")).Count > 0)
            //        {
            //            IWebElement cell = driver.FindElement(By.XPath("//td[contains(text(),'F. Ing :')]"));
            //            fecha = cell.Text.Replace("F. Ing :", "");
            //        }
            //        if (!string.IsNullOrEmpty(fecha))
            //        {
            //            objCausa.FechaIngreso = DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture);
            //        }
            //        else
            //        {
            //            objCausa.FechaIngreso = new DateTime();
            //        }
            //        //if (!string.IsNullOrEmpty(ruta[1]))
            //        //{
            //        //    objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
            //        //}
            //        //else
            //        //{
            //        //    objCausa.RutaDemanda = null;
            //        //}

            //        ActualizarPoderJudicialRolFecha(objCausa.IdCausa, objCausa.FechaIngreso);

            //    }
            //    //Close the browser
            //    driver.Quit();
            //}
            //catch (Exception ex)
            //{
            //    //Close the browser
            //    driver.Quit();
            //    throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
            //}

        }

        #endregion

        #region "Ultra Demon"

        public static void InsertarListaRolHTML(string tipo, int numero, int anio,string html)
        {
            dao.Causa.InsertarListaRolHTML(tipo, numero, anio, html);
        }

        public static void ProcesarListaHTML(int anio)
        {
            Console.WriteLine("Iniciando descarga anio: " + anio.ToString());
            HtmlDocument html = new HtmlDocument();
            List<ScannerHTML> lst = dao.Causa.ListarListaRolesHTML(anio);
            Console.WriteLine("Roles a descargar: " + lst.Count);
            while (anio >= 2013)
            {
                while (lst.Count != 0)
                {
                    foreach (ScannerHTML s in lst)
                    {
                        Console.WriteLine("Descargando HTML " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString());
                        //indicadores = DescargaPagina(s.HTML);
                        if (s.HTML.Contains("textoC"))
                        {
                            try
                            {
                                html.LoadHtml(s.HTML);

                                var myTable = html.DocumentNode
                                                .Descendants()
                                                .Where(e => e.Attributes.Contains("id"))
                                                .SingleOrDefault(e => e.Attributes["id"].Value == "contentCellsAddTabla");
                                int columna = 1;
                                string linea = "", htmlRol = "", attributeValue = "", url = "";
                                foreach (HtmlNode row in myTable.SelectNodes("tbody//tr"))
                                {
                                    columna = 1;
                                    linea = "";
                                    foreach (HtmlNode cell in row.SelectNodes("td"))
                                    {
                                        switch (columna)
                                        {
                                            case 1:
                                                foreach (HtmlNode node in cell.SelectNodes("a"))
                                                {
                                                    attributeValue = "http://civil.poderjudicial.cl" + node.GetAttributeValue("href", "");
                                                    if (!string.IsNullOrEmpty(attributeValue))
                                                    {
                                                        url = attributeValue;
                                                        string queryString = url.Remove(0, url.IndexOf('?') + 1);
                                                        string[] parametros = queryString.Split('&');
                                                        string[] parametro;
                                                        foreach (string st in parametros)
                                                        {
                                                            parametro = st.Split('=');
                                                            switch (parametro[0])
                                                            {
                                                                case "CRR_IdCausa":
                                                                    s.IdCausa = Int32.Parse(parametro[1]);
                                                                    break;
                                                                case "COD_Tribunal":
                                                                    s.Tribunal = Int32.Parse(parametro[1]);
                                                                    break;
                                                                case "CRR_IdCuaderno":
                                                                    s.IdCuaderno = Int32.Parse(parametro[1]);
                                                                    break;
                                                                default:
                                                                    break;
                                                            }
                                                        }
                                                    }
                                                    linea = linea + attributeValue + "|";
                                                }
                                                break;
                                        }
                                        columna++;
                                    }
                                    try
                                    {
                                        htmlRol = DescargaPagina(url);
                                        if (htmlRol.Contains("textoC"))
                                        {
                                            PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, htmlRol, "L", url);
                                        }
                                        else
                                        {
                                            PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, "", "M", url);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, "", "M", url);
                                        Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Procesa Lista HTML Poder Judicial", s.Anio);
                                        throw ex;
                                    }
                                    Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                                }
                                dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "P");
                            }
                            catch (Exception ex)
                            {
                                dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "M");
                                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Error cargar Lista HTML Poder Judicial", s.Anio);
                                //throw ex;
                            }
                            
                        }
                        else
                        {
                            Dimol.dao.Funciones.InsertarError("Lista HTML Poder Judicial Vacia","Rol: "+s.TipoCausa + "-"+s.Rol.ToString()+"-"+s.Anio.ToString(), "Lista HTML Poder Judicial Vacia", s.Anio);
                            dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "M");
                            //cambiar++;
                        }
                    }
                    lst = dao.Causa.ListarListaRolesHTML(anio);
                }
                anio--;
                Console.WriteLine("Cambio anio " + anio.ToString());
            }
            Console.WriteLine("Fin descarga");
            Console.ReadLine();
        }

        public static void ProcesarListaHTMLVacio(int anio)
        {
            Console.WriteLine("Iniciando descarga anio: " + anio.ToString());
            HtmlDocument html = new HtmlDocument();
            List<ScannerHTML> lst = dao.Causa.ListarListaRolesHTML(anio);
            Console.WriteLine("Roles a descargar: " + lst.Count);
            while (anio >= 2013)
            {
                while (lst.Count != 0)
                {
                    foreach (ScannerHTML s in lst)
                    {
                        Console.WriteLine("Descargando HTML " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString());
                        //indicadores = DescargaPagina(s.HTML);
                        if (s.HTML.Contains("textoC"))
                        {
                            try
                            {
                                html.LoadHtml(s.HTML);

                                var myTable = html.DocumentNode
                                                .Descendants()
                                                .Where(e => e.Attributes.Contains("id"))
                                                .SingleOrDefault(e => e.Attributes["id"].Value == "contentCellsAddTabla");
                                int columna = 1;
                                string linea = "", htmlRol = "", attributeValue = "", url = "";
                                foreach (HtmlNode row in myTable.SelectNodes("tbody//tr"))
                                {
                                    columna = 1;
                                    linea = "";
                                    foreach (HtmlNode cell in row.SelectNodes("td"))
                                    {
                                        switch (columna)
                                        {
                                            case 1:
                                                foreach (HtmlNode node in cell.SelectNodes("a"))
                                                {
                                                    attributeValue = "http://civil.poderjudicial.cl" + node.GetAttributeValue("href", "");
                                                    if (!string.IsNullOrEmpty(attributeValue))
                                                    {
                                                        url = attributeValue;
                                                        string queryString = url.Remove(0, url.IndexOf('?') + 1);
                                                        string[] parametros = queryString.Split('&');
                                                        string[] parametro;
                                                        foreach (string st in parametros)
                                                        {
                                                            parametro = st.Split('=');
                                                            switch (parametro[0])
                                                            {
                                                                case "CRR_IdCausa":
                                                                    s.IdCausa = Int32.Parse(parametro[1]);
                                                                    break;
                                                                case "COD_Tribunal":
                                                                    s.Tribunal = Int32.Parse(parametro[1]);
                                                                    break;
                                                                case "CRR_IdCuaderno":
                                                                    s.IdCuaderno = Int32.Parse(parametro[1]);
                                                                    break;
                                                                case "TIP_Cuaderno":
                                                                    //s.TipoCuaderno = Int32.Parse(parametro[1]);
                                                                    break;
                                                                default:
                                                                    break;
                                                            }
                                                        }
                                                    }
                                                    linea = linea + attributeValue + "|";
                                                }
                                                break;
                                        }
                                        columna++;
                                    }
                                    try
                                    {
                                        //htmlRol = DescargaPagina(url);
                                        //if (htmlRol.Contains("textoC"))
                                        //{
                                        //    PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, htmlRol, "L", url);
                                        //}
                                        //else
                                        //{
                                            PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, "", "M", url);
                                        //}
                                    }
                                    catch (Exception ex)
                                    {
                                        PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, "", "M", url);
                                        Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Procesa Lista HTML Poder Judicial", s.Anio);
                                        throw ex;
                                    }
                                    Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                                }
                                dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "P");
                            }
                            catch (Exception ex)
                            {
                                dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "M");
                                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Error cargar Lista HTML Poder Judicial", s.Anio);
                                //throw ex;
                            }

                        }
                        else
                        {
                            Dimol.dao.Funciones.InsertarError("Lista HTML Poder Judicial Vacia", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Lista HTML Poder Judicial Vacia", s.Anio);
                            dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "M");
                            //cambiar++;
                        }
                    }
                    lst = dao.Causa.ListarListaRolesHTML(anio);
                }
                anio--;
                Console.WriteLine("Cambio anio " + anio.ToString());
            }
            Console.WriteLine("Fin descarga");
            Console.ReadLine();
        }

        public static ScannerHTML UltimaListaHTML()
        {
            return  dao.Causa.UltimaListaHTML();
        }

        public static ScannerHTML UltimaListaHTML(int anio)
        {
            return dao.Causa.UltimaListaHTML(anio);
        }

        public static void InsertarRolHTMLURL(int idCausa, int idCuaderno, string tipo, int numero, int anio, int tribunal, string html, string estado, string url)
        {
            dao.Causa.InsertarRolHTMLURL(idCausa, idCuaderno, tipo, numero, anio, tribunal, html, estado, url);
        }

        public static string DescargaPagina(string fileName)
        {

            string sContents = string.Empty;
            if (fileName.ToLower().IndexOf("http:") > -1)
            {
                // URL
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Encoding = Encoding.Default;
                return wc.DownloadString(fileName);
                //byte[] response = wc.DownloadData(fileName);
                //sContents = System.Text.Encoding.UTF8.GetString(response);
            }
            else
            {
                // Regular Filename
                System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                sContents = sr.ReadToEnd();
                sr.Close();
            }
            return sContents;
        }

        #endregion

        #region "Procesar Rol HTML"

        public static void ProcesarRolHTML()
        {
            Console.WriteLine("Inicio carga roles desde HTML");
            List<ScannerHTML> lst = PJSpider.dao.Causa.ListarCargaRolHTML();
            Console.WriteLine("Lista de roles: "+ lst.Count.ToString());
            ScannerCausas objCausa = new ScannerCausas();
            HtmlDocument html = new HtmlDocument();
            string fecha = "";
            string format = "dd/MM/yyyy";
            while (lst.Count > 0)
            {
                foreach (ScannerHTML s in lst)
                {
                    fecha = "";
                    if (s.HTML.Contains("Participante"))
                    try
                    {
                        html.LoadHtml(s.HTML);
                        try
                        {
                            // Grabar rol
                            objCausa.IdCausa = s.IdCausa;
                            objCausa.IdCuaderno = s.IdCuaderno;
                            objCausa.TipoCausa = s.TipoCausa;
                            objCausa.RolCausa = s.Rol;
                            objCausa.RolAnio = s.Anio;
                            objCausa.CodigoTribunal = s.Tribunal;
                            string[] ruta = new string[] { "", "" };
                            foreach (HtmlNode td in html.DocumentNode.SelectNodes("//td[contains(text(),'Texto Demanda')]"))
                            {
                                foreach (HtmlNode node in td.SelectNodes("img"))
                                {
                                    if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
                                    {
                                        ruta = new string[] { "", "" };
                                    }
                                    else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
                                    {
                                        ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                    }
                                    else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
                                    {
                                        ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                    }
                                }
                            }
                            foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'F. Ing :')]]"))
                            {
                                fecha = td.InnerText.Replace("F. Ing :", "").Replace(" \r\n\t\t", "");
                                if (!string.IsNullOrEmpty(fecha))
                                {
                                    objCausa.FechaIngreso = DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    objCausa.FechaIngreso = new DateTime();
                                }
                            }
                            if (!string.IsNullOrEmpty(ruta[1]))
                            {
                                objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
                            }
                            else
                            {
                                objCausa.RutaDemanda = null;
                            }

                            dao.Causa.InsertarRolHTML(objCausa.IdCausa, objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal, objCausa.RutaDemanda, objCausa.FechaIngreso);

                            // Grabar cuadernos
                            HtmlNode.ElementsFlags.Remove("option");
                            string idCuaderno="",descCuaderno="";
                            long test=0;
                            foreach (HtmlNode cuaderno in html.DocumentNode.SelectNodes("//select[@name='CRR_Cuaderno']"))
                            {
                                foreach (HtmlNode option in cuaderno.SelectNodes("option"))
                                {
                                    idCuaderno = option.GetAttributeValue("value", "");
                                    if (string.IsNullOrEmpty(option.InnerHtml))
                                    {
                                        descCuaderno = option.NextSibling.InnerHtml;
                                    }
                                    else
                                    {
                                        descCuaderno = option.InnerHtml;
                                    }
                                    
                                    if (Int64.TryParse(idCuaderno, out test) && !string.IsNullOrEmpty(descCuaderno))
                                    {
                                        dao.Causa.InsertarCuadernoHTML(objCausa.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString()? "N": "S");
                                    }
                                    
                                }
                            }

                            // Grabar Litigantes
                            int i = 1;
                            TablaLitigantes tbl = new TablaLitigantes();
                            foreach (HtmlNode litigantes in html.DocumentNode.SelectNodes("//div[@id='Litigantes']"))
                            {
                                foreach (HtmlNode table in litigantes.SelectNodes("table"))
                                {
                                    foreach (HtmlNode row in table.SelectNodes("tbody/tr"))
                                    {
                                        i=1;
                                        tbl = new TablaLitigantes();
                                        foreach (HtmlNode cell in row.SelectNodes("td"))
                                        {
                                            switch (i)
                                            {
                                                case 1:
                                                    tbl.Participante = cell.InnerText;
                                                    break;
                                                case 2:
                                                    tbl.Rut = cell.InnerText;
                                                    break;
                                                case 3:
                                                    tbl.Persona = cell.InnerText;
                                                    break;
                                                case 4:
                                                    tbl.NombreRazonSocial = cell.InnerText;
                                                    break;
                                            }
                                            if (tbl.Participante == "Participante")
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                i++;
                                            }
                                            
                                        }
                                        if (tbl.Participante != "Participante")
                                        {
                                            dao.Causa.InsertarLitiganteHTML(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Participante, tbl.Rut, tbl.Persona, tbl.NombreRazonSocial);
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                            dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "P");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                            Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                            dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                        Dimol.dao.Funciones.InsertarError("HTML con error", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                        dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                    }
                }
                lst = PJSpider.dao.Causa.ListarCargaRolHTML();
            }
            Console.WriteLine("Fin carga roles desde HTML");
            Console.ReadLine();
        }

        public static void DescargarListaHTML(int anio)
        {
            FirefoxBinary binary = new FirefoxBinary(System.Configuration.ConfigurationManager.AppSettings["PathFirefox"]);
            FirefoxProfile profile = new FirefoxProfile();
            IWebDriver driver = new FirefoxDriver(binary,profile);
            driver.Navigate().GoToUrl( System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
            try
            {
                while (!driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    Dimol.dao.Funciones.InsertarError("Explorador de Causas del Poder Judicial offline.", "", "Explorador Poder Judicial", anio);
                    Thread.Sleep(600000);
                    driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
                }

                if (driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    string cookies = GetCookies(driver);
                    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                    string indicadores = "";
                    string fileName = ConfigurationManager.AppSettings["UrlLista"];//"http://civil.poderjudicial.cl/CIVILPORWEB/AtPublicoDAction.do";
                    NameValueCollection parametros = new NameValueCollection();

                    string fechaConsulta = DateTime.Today.ToString("dd/MM/yyyy");
                    int cambiar = 0, rol = 1, limiteCambio = 6;
                    ScannerHTML obj = PJSpider.dao.Causa.UltimaListaNumeroHTML(anio);
                    if (obj.Rol != 0)
                    {
                        rol = obj.Rol + 1;
                    }
                    else
                    {
                        rol = 1;
                    }
                    string tipo = "C";
                    bool error = true;
                    Console.WriteLine("Iniciando scanner lista de roles, anio: " + anio.ToString());
                    rol = 133687;
                    while (cambiar < limiteCambio)
                    {
                        error = true;
                        try
                        {
                            parametros = new NameValueCollection() {
                                { "APE_Materno", "" },
                                { "APE_Paterno", "" },
                                { "COD_Tribunal", "0" },
                                { "ERA_Causa" ,anio.ToString()},
                                { "FEC_Desde", "20/04/2016" },
                                { "FEC_Hasta", "20/04/2016" },
                                { "NOM_Consulta", "" },
                                { "ROL_Causa", rol.ToString()},
                                { "RUC_Dv", "" },
                                { "RUC_Era", "" },
                                { "RUC_Numero", "" },
                                { "RUC_Tribunal", "3" },
                                { "RUT_Consulta", "" },
                                { "RUT_DvConsulta", "" },
                                { "SEL_Litigantes", "0" },
                                { "SeleccionL", "0" },
                                { "TIP_Causa", tipo },
                                { "TIP_Consulta", "1" },
                                { "TIP_Lengueta", "tdUno" },
                                { "irAccionAtPublico", "Consulta" },
                            };
                            Console.WriteLine("Descargando HTML " + tipo + "-" + rol.ToString() + "-" + anio.ToString());
                            while (error)
                            {
                                try
                                {
                                    indicadores = ConsultarRol(fileName, parametros, cookies);
                                    error = false;
                                }
                                catch (Exception ex)
                                {
                                    Thread.Sleep(600000);
                                    error = true;
                                }
                            }

                            if (indicadores.Contains("textoC"))
                            {
                                Console.WriteLine("Grabando HTML " + tipo + "-" + rol.ToString() + "-" + anio.ToString());
                                PJSpider.bcp.Causa.InsertarListaRolHTML(tipo, rol, anio, indicadores);
                                cambiar = 0;
                            }
                            else
                            {
                                cambiar++;
                            }
                            rol++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    Console.WriteLine("Fin anio " + anio.ToString());
                }
            }
            catch (Exception ex)
            {

            }
 
                
            Console.WriteLine("Fin descarga lista de roles");
        }

        public static void DescargarListaHTML(int anio, int inicio)
        {
            FirefoxBinary binary = new FirefoxBinary(System.Configuration.ConfigurationManager.AppSettings["PathFirefox"]);
            FirefoxProfile profile = new FirefoxProfile();
            IWebDriver driver = new FirefoxDriver(binary, profile);
            driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
            try
            {
                while (!driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    Dimol.dao.Funciones.InsertarError("Explorador de Causas del Poder Judicial offline.", "", "Explorador Poder Judicial", anio);
                    Thread.Sleep(600000);
                    driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
                }

                if (driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    string cookies = GetCookies(driver);
                    CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                    string indicadores = "";
                    string fileName = ConfigurationManager.AppSettings["UrlLista"];//"http://civil.poderjudicial.cl/CIVILPORWEB/AtPublicoDAction.do";
                    NameValueCollection parametros = new NameValueCollection();

                    string fechaConsulta = DateTime.Today.ToString("dd/MM/yyyy");
                    int cambiar = 0, rol = 1, limiteCambio = 6;
                    //ScannerHTML obj = PJSpider.dao.Causa.UltimaListaNumeroHTML(anio);
                    //if (obj.Rol != 0)
                    //{
                    //    rol = obj.Rol + 1;
                    //}
                    //else
                    //{
                    //    rol = 1;
                    //}
                    string tipo = "C";
                    bool error = true;
                    Console.WriteLine("Iniciando scanner lista de roles, anio: " + anio.ToString());
                    rol = inicio;
                    while (cambiar < limiteCambio)
                    {
                        error = true;
                        try
                        {
                            parametros = new NameValueCollection() {
                                { "APE_Materno", "" },
                                { "APE_Paterno", "" },
                                { "COD_Tribunal", "0" },
                                { "ERA_Causa" ,anio.ToString()},
                                { "FEC_Desde", "20/04/2016" },
                                { "FEC_Hasta", "20/04/2016" },
                                { "NOM_Consulta", "" },
                                { "ROL_Causa", rol.ToString()},
                                { "RUC_Dv", "" },
                                { "RUC_Era", "" },
                                { "RUC_Numero", "" },
                                { "RUC_Tribunal", "3" },
                                { "RUT_Consulta", "" },
                                { "RUT_DvConsulta", "" },
                                { "SEL_Litigantes", "0" },
                                { "SeleccionL", "0" },
                                { "TIP_Causa", tipo },
                                { "TIP_Consulta", "1" },
                                { "TIP_Lengueta", "tdUno" },
                                { "irAccionAtPublico", "Consulta" },
                            };
                            Console.WriteLine("Descargando HTML " + tipo + "-" + rol.ToString() + "-" + anio.ToString());
                            while (error)
                            {
                                try
                                {
                                    indicadores = ConsultarRol(fileName, parametros, cookies);
                                    error = false;
                                }
                                catch (Exception ex)
                                {
                                    Thread.Sleep(600000);
                                    error = true;
                                }
                            }

                            if (indicadores.Contains("textoC"))
                            {
                                Console.WriteLine("Grabando HTML " + tipo + "-" + rol.ToString() + "-" + anio.ToString());
                                PJSpider.bcp.Causa.InsertarListaRolHTML(tipo, rol, anio, indicadores);
                                cambiar = 0;
                            }
                            else
                            {
                                cambiar++;
                            }
                            rol++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    Console.WriteLine("Fin anio " + anio.ToString());
                }
            }
            catch (Exception ex)
            {

            }


            Console.WriteLine("Fin descarga lista de roles");
        }

        public static string ConsultarRol(string fileName, NameValueCollection parametros, string cookie)
        {

            string sContents = string.Empty;
            if (fileName.ToLower().IndexOf("http:") > -1)
            {
                // URL
                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                wc.Headers.Add("Accept-Encoding", "gzip, deflate");
                wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                wc.Headers.Add("Cookie",cookie);
                wc.Headers.Add("Host", "civil.poderjudicial.cl");
                wc.Headers.Add("Referer", "http://civil.poderjudicial.cl/CIVILPORWEB/AtPublicoViewAccion.do?tipoMenuATP=1");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0");
                wc.Encoding = Encoding.Default;
                byte[] response = wc.UploadValues(fileName, parametros);
                sContents = System.Text.Encoding.Default.GetString(response);
            }
            else
            {
                // Regular Filename
                System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                sContents = sr.ReadToEnd();
                sr.Close();
            }
            return sContents;
        }

        public static string GetCookies(IWebDriver driver)
        {
            var _cookies = driver.Manage().Cookies.AllCookies;
            string cookies = "";
            foreach (var cookie in _cookies)
            {
                cookies = cookies + cookie.Name + "=" + cookie.Value + ";";
            }
            return cookies;
        }

        #endregion

        #region "Actualizar Roles internos"

        public static void ActualizarRolesInternos(int codemp, int idioma, string estados, int particion, int cantidad)
        {
            int inicio = 0;
            string htmlRol = "";
            inicio = (particion - 1) * cantidad;
            //ConsultaCausa obj = new ConsultaCausa();
            List<RolActualizar> lstRoles = new List<RolActualizar>();
            List<DatoTipo> lstTipoCuaderno = dao.Causa.ListarTipoCuaderno();
            Dimol.dao.Funciones.InsertarError("Generando lista de roles para actualizar", "Estados: " + estados, "Actualizar Roles Internos", particion);
            // Genero la lista de roles a actualizar
            lstRoles = dao.Causa.ListarRolesInternos(codemp, idioma, estados);

            if (lstRoles.Count - inicio < cantidad)
            {
                cantidad = lstRoles.Count - inicio;
                if (cantidad <= 0)
                {
                    Dimol.dao.Funciones.InsertarError("Esta particion no es necesaria. Favor cerrar la aplicacion.", "Cantidad: " + lstRoles.Count, "Bot Poder Judicial", particion);
                    throw new Exception("Esta particion no es necesaria. Favor cerrar la aplicacion.");
                }
            }
            List<RolActualizar> lst = lstRoles.GetRange(inicio, cantidad);
            Dimol.dao.Funciones.InsertarError("Lista de roles para actualizar", "Cantidad: " + lst.Count, "Actualizar Roles Internosl", particion);

            //Levanto una ventana del navegador para obtener las cookies si es necesario
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
            try
            {
                while (!driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    Dimol.dao.Funciones.InsertarError("Explorador de Causas del Poder Judicial offline.", "", "Actualizar Roles Internos", particion);
                    Thread.Sleep(600000);
                    driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
                }

                if (driver.Title.Contains("CONSULTA CAUSAS"))
                {
                }
            }
            catch (Exception ex)
            {

            }


            foreach (RolActualizar obj in lst)
            {
                Dimol.dao.Funciones.InsertarError("Actualizando", "Rol: " + obj.TipoCausa + "-" + obj.Numero + ", Tribunal: " + obj.Tribunal, "Bot Poder Judicial", particion);
                //obj.RolAnio = r.Anio;
                //obj.CodigoTribunal = r.IdTribunal;
                //obj.NombreTribunal = r.Tribunal;
                obj.Url = ConfigurationManager.AppSettings["UrlPJ"]; //"http://civil.poderjudicial.cl/CIVILPORWEB/";
                //obj.TipoCausa = r.TipoCausa;
                //obj.RolCausa = r.Rol;
                //obj.Codemp = r.Codemp;
                //obj.Rolid = r.Rolid;
                //obj.IdCausa = r.IdCausa;
                //obj.IdCuaderno = r.;
                //obj.FechaUltHistorial = r.FechaUltHistorial;
                //obj.FechaUltReceptor = r.FechaUltReceptor;
                int retry = 0;
                if (!string.IsNullOrEmpty(obj.UrlHTML))
                {
                    try
                    {
                        htmlRol = DescargaPagina(obj.UrlHTML);
                        if (htmlRol.Contains("textoC"))
                        {
                            //Insertar Rol
                            obj.HTML = htmlRol;
                            if (obj.IdCausa == 0)
                            {
                                string url = obj.UrlHTML;
                                string queryString = url.Remove(0, url.IndexOf('?') + 1);
                                string[] parametros = queryString.Split('&');
                                string[] parametro;
                                foreach (string s in parametros)
                                {
                                    parametro = s.Split('=');
                                    switch (parametro[0])
                                    {
                                        //case "TIP_Consulta":
                                        //    obj.TipoConsulta = Int32.Parse(parametro[1]);
                                        //    break;
                                        case "TIP_Cuaderno":
                                            obj.TipoCuaderno = Int32.Parse(parametro[1]);
                                            break;
                                        case "CRR_IdCuaderno":
                                            obj.IdCuaderno = Int32.Parse(parametro[1]);
                                            break;
                                        case "CRR_IdCausa":
                                            obj.IdCausa = Int32.Parse(parametro[1]);
                                            break;
                                        //case "TIP_Informe":
                                        //    obj.TipoInforme = Int32.Parse(parametro[1]);
                                        //    break;
                                        //case "COD_Tribunal":
                                        //    obj.CodigoTribunal = Int32.Parse(parametro[1]);
                                        //    break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            InsertarRolPoderJudicial(obj.Codemp, obj.Rolid, obj.TipoCausa, obj.IdCausa, obj.IdTribunal);
                            PJSpider.bcp.Causa.InsertarRolHTMLURL(obj.IdCausa, obj.IdCuaderno, obj.TipoCausa, obj.Rol, obj.Anio, obj.IdTribunal, htmlRol, "I", obj.UrlHTML);

                            ProcesarRolInternoHTML(obj, lstTipoCuaderno);
                        }
                        else
                        {
                            Dimol.dao.Funciones.InsertarError("Error bot roles internos", "No se encuentra Rol HTML: " + obj.TipoCausa + "-" + obj.Numero + "-" + obj.Anio, "Actualizar Roles Internos", obj.Anio);
                        }
                    }
                    catch (Exception ex)
                    {
                        //PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, "", "M", r.UrlHTML);
                        Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Procesa Lista HTML Poder Judicial", obj.Anio);
                        throw ex;
                    }
                    //Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                }
                else
                {
                    dao.Causa.BuscarListaRolesHTML(obj);
                    if (string.IsNullOrEmpty(obj.HTML) || obj.Estado == "M")
                    {
                        DescargarListaHTML(obj, driver);
                    }

                    DescargarRolListaHTML(obj, lstTipoCuaderno);
                    
                    //htmlRol = DescargaPagina(r.UrlHTML);
                    //"http://civil.poderjudicial.cl/CIVILPORWEB/ConsultaDetalleAtPublicoAccion.do?TIP_Consulta=1&TIP_Cuaderno=1&CRR_IdCuaderno=564494&ROL_Causa=1&TIP_Causa=C&ERA_Causa=2009&CRR_IdCausa=512883&COD_Tribunal=161&TIP_Informe=1&"
                }
            }
            Dimol.dao.Funciones.InsertarError("Fin de roles internos para actualizar", "Estados: " + estados, "Actualizar Roles Internos", particion);
        }

        public static void DescargarListaHTML(RolActualizar s, IWebDriver driver)
        {

            string cookies = GetCookies(driver);
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            string indicadores = "";
            string fileName = ConfigurationManager.AppSettings["UrlLista"];//"http://civil.poderjudicial.cl/CIVILPORWEB/AtPublicoDAction.do";
            NameValueCollection parametros = new NameValueCollection();

            string fechaConsulta = DateTime.Today.ToString("dd/MM/yyyy");
            bool error = true;
            Console.WriteLine("Descargando roles, numero: " + s.Rol.ToString() + ", anio: " + s.Anio.ToString());
            error = true;
            try
            {
                parametros = new NameValueCollection() {
                                { "APE_Materno", "" },
                                { "APE_Paterno", "" },
                                { "COD_Tribunal", "0" },
                                { "ERA_Causa" , s.Anio.ToString()},
                                { "FEC_Desde", "20/04/2016" },
                                { "FEC_Hasta", "20/04/2016" },
                                { "NOM_Consulta", "" },
                                { "ROL_Causa", s.Rol.ToString()},
                                { "RUC_Dv", "" },
                                { "RUC_Era", "" },
                                { "RUC_Numero", "" },
                                { "RUC_Tribunal", "3" },
                                { "RUT_Consulta", "" },
                                { "RUT_DvConsulta", "" },
                                { "SEL_Litigantes", "0" },
                                { "SeleccionL", "0" },
                                { "TIP_Causa", s.TipoCausa ?? "C"},
                                { "TIP_Consulta", "1" },
                                { "TIP_Lengueta", "tdUno" },
                                { "irAccionAtPublico", "Consulta" },
                            };
                Console.WriteLine("Descargando HTML " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString());
                while (error)
                {
                    try
                    {
                        indicadores = ConsultarRol(fileName, parametros, cookies);
                        s.HTML = indicadores;
                        if (indicadores.Contains("textoC"))
                        {
                            Console.WriteLine("Grabando HTML " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString());
                            dao.Causa.InsertarListaRolHTML(s.TipoCausa, s.Rol, s.Anio, indicadores);
  
                        }
                        error = false;
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(20000); //un minuto de espera
                        error = true;
                    }
                }

                if (indicadores.Contains("textoC"))
                {
                    Console.WriteLine("Grabando HTML " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString());
                    PJSpider.bcp.Causa.InsertarListaRolHTML(s.TipoCausa, s.Rol, s.Anio, indicadores);
                }
            }
            catch (Exception ex)
            {

            }

            //Console.WriteLine("Fin roles, numero: " + numero.ToString() + ", anio: " + anio.ToString());
            //Console.WriteLine("Fin descarga lista de roles");
        }

        public static void DescargarRolListaHTML(RolActualizar s, List<DatoTipo> lstTipoCuaderno)
        {
            HtmlDocument html = new HtmlDocument();
            HtmlDocument htmlR = new HtmlDocument();
            string htmlCuaderno = "",urlCuaderno="";
            int idTribunal = 0;
            int tipoCuaderno = 0;
            try
            {
                html.LoadHtml(s.HTML);

                var myTable = html.DocumentNode
                                .Descendants()
                                .Where(e => e.Attributes.Contains("id"))
                                .SingleOrDefault(e => e.Attributes["id"].Value == "contentCellsAddTabla");
                int columna = 1;
                string linea = "", htmlRol = "", attributeValue = "", url = "";
                foreach (HtmlNode row in myTable.SelectNodes("tbody//tr"))
                {
                    columna = 1;
                    linea = "";
                    idTribunal = 0;
                    foreach (HtmlNode cell in row.SelectNodes("td"))
                    {
                        switch (columna)
                        {
                            case 1:
                                foreach (HtmlNode node in cell.SelectNodes("a"))
                                {
                                    attributeValue = "http://civil.poderjudicial.cl" + node.GetAttributeValue("href", "");
                                    if (!string.IsNullOrEmpty(attributeValue))
                                    {
                                        url = attributeValue;
                                        string queryString = url.Remove(0, url.IndexOf('?') + 1);
                                        string[] parametros = queryString.Split('&');
                                        string[] parametro;
                                        foreach (string st in parametros)
                                        {
                                            parametro = st.Split('=');
                                            switch (parametro[0])
                                            {
                                                case "CRR_IdCausa":
                                                    s.IdCausa = Int32.Parse(parametro[1]);
                                                    break;
                                                case "COD_Tribunal":
                                                    idTribunal = Int32.Parse(parametro[1]);
                                                    break;
                                                case "CRR_IdCuaderno":
                                                    s.IdCuaderno = Int32.Parse(parametro[1]);
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                    linea = linea + attributeValue + "|";
                                }
                                break;
                        }
                        columna++;
                    }
                    try
                    {
                        if (s.IdTribunal == idTribunal)
                        {
                            htmlRol = DescargaPagina(url);
                            if (htmlRol.Contains("textoC"))
                            {
                                dao.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.IdTribunal, htmlRol, "L", url);
                                htmlR.LoadHtml(htmlRol);
                                foreach (HtmlNode cuaderno in htmlR.DocumentNode.SelectNodes("//select[@name='CRR_Cuaderno']"))
                                {
                                    tipoCuaderno = 0;
                                    string idCuaderno = "";
                                    string descCuaderno = "";
                                    long test = 0;
                                    foreach (HtmlNode option in cuaderno.SelectNodes("option"))
                                    {
                                        idCuaderno = option.GetAttributeValue("value", "");
                                        if (string.IsNullOrEmpty(option.InnerHtml))
                                        {
                                            descCuaderno = option.NextSibling.InnerHtml;
                                        }
                                        else
                                        {
                                            descCuaderno = option.InnerHtml;
                                        }

                                        if (Int64.TryParse(idCuaderno, out test) && !string.IsNullOrEmpty(descCuaderno))
                                        {

                                            if (lstTipoCuaderno.Find(x => x.Nombre == descCuaderno) == null)
                                            {
                                                dao.Causa.InsertarCuadernoHTML(s.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S");
                                            }
                                            else
                                            {
                                                tipoCuaderno = lstTipoCuaderno.Find(x => x.Nombre == descCuaderno).Id;
                                                dao.Causa.InsertarCuadernoHTMLFull(s.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S", tipoCuaderno);
                                                urlCuaderno = "http://civil.poderjudicial.cl/CIVILPORWEB/ConsultaDetalleAtPublicoAccion.do?TIP_Consulta=1&TIP_Cuaderno=" + tipoCuaderno + "&CRR_IdCuaderno=" + idCuaderno + "&ROL_Causa=" + s.Rol + "&TIP_Causa=" + s.TipoCausa + "&ERA_Causa=" + s.Anio + "&CRR_IdCausa=" + s.IdCausa + "&COD_Tribunal=" + s.IdTribunal + "&TIP_Informe=1&";
                                                htmlCuaderno = DescargaPagina(urlCuaderno);
                                                if (htmlRol.Contains("textoC"))
                                                {
                                                    dao.Causa.InsertarRolHTMLURL(s.IdCausa, Int32.Parse(idCuaderno), s.TipoCausa, s.Rol, s.Anio, s.IdTribunal, htmlCuaderno, "L", urlCuaderno);
                                                }
                                            }

                                        }

                                    }
                                }
                            }
                            
                            //else
                            //{
                            //    PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, "", "M", url);
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        dao.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.IdTribunal, "", "M", url);
                        Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Procesa Lista HTML Poder Judicial", s.Anio);
                        throw ex;
                    }
                    //Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                }
                //dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "P");
            }
            catch (Exception ex)
            {
                dao.Causa.MarcarLeidaListaRolHTML(s.TipoCausa, s.Rol, s.Anio, "M");
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Error cargar Lista HTML Poder Judicial", s.Anio);
                //throw ex;
            }
        }

        public static void ProcesarRolInternoHTML(RolActualizar s, List<DatoTipo> lstTipoCuaderno)
        {
            HtmlDocument html = new HtmlDocument();
            ScannerCausas objCausa = new ScannerCausas();
            string format = "dd/MM/yyyy";
            string fecha ;
            try
            {
                html.LoadHtml(s.HTML);
                try
                {
                    // Grabar rol
                    objCausa.IdCausa = s.IdCausa;
                    objCausa.IdCuaderno = s.IdCuaderno;
                    objCausa.TipoCausa = s.TipoCausa;
                    objCausa.RolCausa = s.Rol;
                    objCausa.RolAnio = s.Anio;
                    objCausa.CodigoTribunal = s.IdTribunal;
                    string[] ruta = new string[] { "", "" };
                    foreach (HtmlNode td in html.DocumentNode.SelectNodes("//td[contains(text(),'Texto Demanda')]"))
                    {
                        foreach (HtmlNode node in td.SelectNodes("img"))
                        {
                            if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
                            {
                                ruta = new string[] { "", "" };
                            }
                            else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
                            {
                                ruta = node.GetAttributeValue("onclick", "").Split('\'');
                            }
                            else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
                            {
                                ruta = node.GetAttributeValue("onclick", "").Split('\'');
                            }
                        }
                    }
                    foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'F. Ing :')]]"))
                    {
                        fecha = td.InnerText.Replace("F. Ing :", "").Replace(" \r\n\t\t", "");
                        if (!string.IsNullOrEmpty(fecha))
                        {
                            objCausa.FechaIngreso = DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            objCausa.FechaIngreso = new DateTime();
                        }
                    }
                    if (!string.IsNullOrEmpty(ruta[1]))
                    {
                        objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
                    }
                    else
                    {
                        objCausa.RutaDemanda = null;
                    }

                    dao.Causa.InsertarRolHTML(objCausa.IdCausa, objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal, objCausa.RutaDemanda, objCausa.FechaIngreso);

                    // Grabar cuadernos
                    HtmlNode.ElementsFlags.Remove("option");
                    string idCuaderno = "", descCuaderno = "";
                    long test = 0;
                    int tipoCuaderno = 0;
                    foreach (HtmlNode cuaderno in html.DocumentNode.SelectNodes("//select[@name='CRR_Cuaderno']"))
                    {
                        foreach (HtmlNode option in cuaderno.SelectNodes("option"))
                        {
                            idCuaderno = option.GetAttributeValue("value", "");
                            if (string.IsNullOrEmpty(option.InnerHtml))
                            {
                                descCuaderno = option.NextSibling.InnerHtml;
                            }
                            else
                            {
                                descCuaderno = option.InnerHtml;
                            }

                            if (Int64.TryParse(idCuaderno, out test) && !string.IsNullOrEmpty(descCuaderno))
                            {

                                if (lstTipoCuaderno.Find(x => x.Nombre == descCuaderno) == null) 
                                {
                                    dao.Causa.InsertarCuadernoHTML(objCausa.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S");
                                }
                                else
                                {
                                    tipoCuaderno = lstTipoCuaderno.Find(x => x.Nombre == descCuaderno).Id;
                                    dao.Causa.InsertarCuadernoHTMLFull(objCausa.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S",tipoCuaderno);
                                }
                                
                            }

                        }
                    }

                    // Grabar historial
                    HtmlNode divHistorial = html.DocumentNode.SelectSingleNode("//div[@id='Historia']");
                    HtmlNode tablaHistorial = divHistorial;
                    foreach (HtmlNode chNode in divHistorial.ChildNodes)
                    {
                        if (chNode.Name == "table")
                        {
                            if (chNode.Attributes["bgcolor"] != null && chNode.Attributes["bgcolor"].Value == "white")
                            {
                                tablaHistorial = chNode;
                            }
                        }
                    }
                    HtmlNode divEscritos = html.DocumentNode.SelectSingleNode("//div[@id='Escritos']");
                    HtmlNode tablaEscritos = divEscritos;
                    foreach (HtmlNode chNode in divEscritos.ChildNodes)
                    {
                        if (chNode.Name == "table")
                        {
                            if (chNode.Attributes["width"] != null && chNode.Attributes["width"].Value == "620")
                            {
                                tablaEscritos = chNode;
                            }
                        }
                    }

                    //if (tablaEscritos.ChildNodes.Count > 0)
                    //{
                    //    //InsertarTipoCuaderno(objCausa.IdCausa, objCausa.IdCuaderno,, "S");
                    //}
                    int i = 1;
                    HtmlNodeCollection rows = tablaHistorial.SelectNodes("tbody/tr");
                    foreach (HtmlNode row in rows)
                    {
                        HtmlNodeCollection cells = row.SelectNodes("td");
                        TablaHistorial tbl = new TablaHistorial();
                        foreach (HtmlNode cell in cells)
                        {
                            switch (i)
                            {
                                case 1:
                                    tbl.Folio = cell.InnerText;
                                    break;
                                case 2:
                                    ruta = new string[] { "", "" };
                                    if (cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']") != null)
                                    {
                                        HtmlNode btnPDF = cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']");
                                        ruta = btnPDF.Attributes["onclick"].Value.Split('\'');
                                    }
                                    else if (cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']") != null)
                                    {
                                        HtmlNode btnWord = cell.SelectSingleNode("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']");
                                        ruta = btnWord.Attributes["onclick"].Value.Split('\'');
                                    }
                                    if (!string.IsNullOrEmpty(ruta[1]))
                                    {
                                        tbl.RutaDocumento = "http://civil.poderjudicial.cl" + ruta[1];
                                    }
                                    break;
                                case 3:
                                    tbl.Etapa = cell.InnerText;
                                    break;
                                case 4:
                                    tbl.Tramite = cell.InnerText;
                                    break;
                                case 5:
                                    tbl.DescTramite = cell.InnerText;
                                    break;
                                case 6:
                                    tbl.FechaTramite = DateTime.ParseExact(cell.InnerText, format, CultureInfo.InvariantCulture);
                                    break;
                                case 7:
                                    tbl.Foja = cell.InnerText;
                                    break;
                            }
                            i++;
                        }

                        int fer = 5;

                        dao.Causa.InsertarHistorialPoderJudicial(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Folio, tbl.RutaDocumento, tbl.Etapa, tbl.Tramite, tbl.DescTramite, tbl.FechaTramite, Int32.Parse(tbl.Foja));
                        //if (objCausa.FechaUltHistorial < tbl.FechaTramite)
                        //{
                            //objCausa.TablaHistorial.Add(tbl);
                            //InsertarHistorialPoderJudicial(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Folio, tbl.RutaDocumento, tbl.Etapa, tbl.Tramite, tbl.DescTramite, tbl.FechaTramite, Int32.Parse(tbl.Foja));
                        //}
                        //else
                        //{
                        //    break;
                        //}
                    }

                    // Grabar Litigantes
                    //int i = 1;
                    TablaLitigantes tblLitigantes = new TablaLitigantes();
                    foreach (HtmlNode litigantes in html.DocumentNode.SelectNodes("//div[@id='Litigantes']"))
                    {
                        foreach (HtmlNode table in litigantes.SelectNodes("table"))
                        {
                            foreach (HtmlNode row in table.SelectNodes("tbody/tr"))
                            {
                                i = 1;
                                tblLitigantes = new TablaLitigantes();
                                foreach (HtmlNode cell in row.SelectNodes("td"))
                                {
                                    switch (i)
                                    {
                                        case 1:
                                            tblLitigantes.Participante = cell.InnerText;
                                            break;
                                        case 2:
                                            tblLitigantes.Rut = cell.InnerText;
                                            break;
                                        case 3:
                                            tblLitigantes.Persona = cell.InnerText;
                                            break;
                                        case 4:
                                            tblLitigantes.NombreRazonSocial = cell.InnerText;
                                            break;
                                    }
                                    if (tblLitigantes.Participante == "Participante")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        i++;
                                    }

                                }
                                if (tblLitigantes.Participante != "Participante")
                                {
                                    dao.Causa.InsertarLitiganteHTML(objCausa.IdCausa, objCausa.IdCuaderno, tblLitigantes.Participante, tblLitigantes.Rut, tblLitigantes.Persona, tblLitigantes.NombreRazonSocial);
                                }
                            }
                        }
                    }

                    Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                    dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "P");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                    Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                    dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                Dimol.dao.Funciones.InsertarError("HTML con error", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
            }
        }

        public static void ConsultarCausaHTML(ConsultaCausa objCausa, int particion)
        {
            //using (var scope = new TransactionScope(
            //        TransactionScopeOption.Required,
            //        new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            //{
            //Dimol.dao.Funciones.InsertarError("Descargando datos del PJ.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString(), "Bot Poder Judicial", 0);
            // Create a new instance of the Firefox driver.
            IWebDriver driver = new FirefoxDriver();// PhantomJSDriver(@"c:\");
            driver.Navigate().GoToUrl(objCausa.Url);
            try
            {

                string format = "dd/MM/yyyy";

                while (!driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    Dimol.dao.Funciones.InsertarError("Pagina de consulta de Causas del Poder Judicial offline.", "", "Bot Poder Judicial", particion);
                    Thread.Sleep(10);
                    driver.Navigate().GoToUrl(objCausa.Url);
                }

                if (driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    driver.SwitchTo().Frame(driver.FindElement(By.Name("body")));

                    // Find the text input element by its name
                    IWebElement tipoCausa = driver.FindElement(By.Name("TIP_Causa"));
                    IWebElement rol = driver.FindElement(By.Name("ROL_Causa"));
                    IWebElement anio = driver.FindElement(By.Name("ERA_Causa"));
                    IWebElement tribunal = driver.FindElement(By.Name("COD_Tribunal"));

                    // Enter something to search for
                    rol.SendKeys(objCausa.RolCausa.ToString());
                    anio.SendKeys(objCausa.RolAnio.ToString());
                    tipoCausa.SendKeys(objCausa.TipoCausa);
                    tribunal.SendKeys(objCausa.NombreTribunal.Trim());

                    IWebElement btnBuscar = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));

                    // Now submit the form. WebDriver will find the form for us from the element
                    btnBuscar.Click();

                    if (driver.FindElements(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]")).Count == 0)
                    {
                        Dimol.dao.Funciones.InsertarError("Rol no encontrado.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString() + ", Tribunal: " + objCausa.NombreTribunal, "Bot Poder Judicial", particion);
                        throw new Exception("Rol no encontrado: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString());
                    }
                    IWebElement urlCausa = driver.FindElement(By.XPath("//a[ contains(@href,'ConsultaDetalleAtPublicoAccion.do')]"));

                    if (objCausa.IdCausa == 0)
                    {
                        string url = urlCausa.GetAttribute("href");
                        string queryString = url.Remove(0, url.IndexOf('?') + 1);
                        string[] parametros = queryString.Split('&');
                        string[] parametro;
                        foreach (string s in parametros)
                        {
                            parametro = s.Split('=');
                            switch (parametro[0])
                            {
                                case "TIP_Consulta":
                                    objCausa.TipoConsulta = Int32.Parse(parametro[1]);
                                    break;
                                case "TIP_Cuaderno":
                                    objCausa.TipoCuaderno = Int32.Parse(parametro[1]);
                                    break;
                                case "CRR_IdCuaderno":
                                    objCausa.IdCuaderno = Int32.Parse(parametro[1]);
                                    break;
                                case "CRR_IdCausa":
                                    objCausa.IdCausa = Int32.Parse(parametro[1]);
                                    break;
                                case "TIP_Informe":
                                    objCausa.TipoInforme = Int32.Parse(parametro[1]);
                                    break;
                                case "COD_Tribunal":
                                    objCausa.CodigoTribunal = Int32.Parse(parametro[1]);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    InsertarRolPoderJudicial(objCausa.Codemp, objCausa.Rolid, objCausa.TipoCausa, objCausa.IdCausa, objCausa.CodigoTribunal);

                    urlCausa.Click();

                    IWebElement tipoCuaderno = driver.FindElement(By.Name("CRR_Cuaderno"));
                    //string cuadernoSeleccionado = tipoCuaderno.GetAttribute("value");
                    SelectElement selectList = new SelectElement(tipoCuaderno);
                    IList<IWebElement> options = selectList.Options;
                    IWebElement btnCuaderno = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));
                    List<string> lstCuadernos = new List<string>();
                    foreach (IWebElement e in options)
                    {
                        lstCuadernos.Add(e.Text);
                    }

                    foreach (string e in lstCuadernos)
                    {
                        tipoCuaderno.SendKeys(e);
                        btnCuaderno = driver.FindElement(By.XPath("//a[@onclick='AtPublicoPpalForm.irAccionAtPublico.click();']"));
                        btnCuaderno.Click();
                        tipoCuaderno = driver.FindElement(By.Name("CRR_Cuaderno"));
                        InsertarTipoCuaderno(objCausa.IdCausa, Int32.Parse(tipoCuaderno.GetAttribute("value")), e, "N");
                        int idCuaderno = Int32.Parse(tipoCuaderno.GetAttribute("value"));

                        IWebElement divHistorial = driver.FindElement(By.Id("Historia"));
                        IWebElement divLitigantes = driver.FindElement(By.Id("Litigantes"));
                        IWebElement divNotificaciones = driver.FindElement(By.Id("Notificaciones"));
                        IWebElement divEscritos = driver.FindElement(By.Id("Escritos"));

                        IWebElement tablaHistorial = divHistorial.FindElement(By.XPath("//table[@bgcolor='white']"));
                        //IReadOnlyCollection<IWebElement> tablaLitigantes = divLitigantes.FindElements(By.TagName("table"));
                        //IReadOnlyCollection<IWebElement> tablaNotificaciones = divNotificaciones.FindElements(By.TagName("table"));
                        IWebElement tablaEscritos = divEscritos.FindElement(By.XPath("//table[@width='620']"));
                        IReadOnlyCollection<IWebElement> rowsEscritos = tablaEscritos.FindElements(By.TagName("tr"));

                        if (rowsEscritos.Count > 0)
                        {
                            InsertarTipoCuaderno(objCausa.IdCausa, Int32.Parse(tipoCuaderno.GetAttribute("value")), e, "S");
                        }


                        IReadOnlyCollection<IWebElement> rows = tablaHistorial.FindElements(By.TagName("tr"));
                        foreach (IWebElement row in rows)
                        {
                            IReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                            int i = 1;
                            TablaHistorial tbl = new TablaHistorial();
                            foreach (IWebElement cell in cells)
                            {
                                switch (i)
                                {
                                    case 1:
                                        tbl.Folio = cell.Text;
                                        break;
                                    case 2:
                                        string[] ruta = new string[] { "", "" };
                                        if (cell.FindElements(By.XPath("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']")).Count > 0)
                                        {
                                            IWebElement btnPDF = cell.FindElement(By.XPath("img[@src='/CIVILPORWEB/img/Comun/generarpdf.gif']"));
                                            ruta = btnPDF.GetAttribute("onclick").Split('\'');
                                        }
                                        else if (cell.FindElements(By.XPath("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']")).Count > 0)
                                        {
                                            IWebElement btnWord = cell.FindElement(By.XPath("img[@src='/CIVILPORWEB/img/Comun/edit.jpg']"));
                                            ruta = btnWord.GetAttribute("onclick").Split('\'');
                                        }
                                        if (!string.IsNullOrEmpty(ruta[1]))
                                        {
                                            tbl.RutaDocumento = "http://civil.poderjudicial.cl" + ruta[1];
                                        }
                                        break;
                                    case 3:
                                        tbl.Etapa = cell.Text;
                                        break;
                                    case 4:
                                        tbl.Tramite = cell.Text;
                                        break;
                                    case 5:
                                        tbl.DescTramite = cell.Text;
                                        break;
                                    case 6:
                                        tbl.FechaTramite = DateTime.ParseExact(cell.Text, format, CultureInfo.InvariantCulture);
                                        break;
                                    case 7:
                                        tbl.Foja = cell.Text;
                                        break;
                                }
                                i++;
                            }
                            if (objCausa.FechaUltHistorial < tbl.FechaTramite)
                            {
                                objCausa.TablaHistorial.Add(tbl);
                                InsertarHistorialPoderJudicial(objCausa.IdCausa, idCuaderno, tbl.Folio, tbl.RutaDocumento, tbl.Etapa, tbl.Tramite, tbl.DescTramite, tbl.FechaTramite, Int32.Parse(tbl.Foja));
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    IWebElement btnReceptor = driver.FindElement(By.XPath("//img[@alt='Información de Receptor']"));
                    btnReceptor.Click();
                    IWebElement informacionReceptor = driver.FindElement(By.Id("ReceptorDIV"));
                    IWebElement tablaReceptores = informacionReceptor.FindElement(By.XPath("//table[@style='width: 635px; background-color: #e0ecfa; border-color: #6891AB; border-style:solid; border-width:1px; padding:1px;']"));

                    IReadOnlyCollection<IWebElement> rowsReceptor = tablaReceptores.FindElements(By.TagName("tr"));
                    foreach (IWebElement row in rowsReceptor)
                    {
                        IReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                        int i = 1;
                        TablaReceptores tbl = new TablaReceptores();
                        foreach (IWebElement cell in cells)
                        {
                            switch (i)
                            {
                                case 1:
                                    tbl.Cuaderno = cell.Text;
                                    break;
                                case 2:
                                    string[] receptor = cell.Text.Split('-');
                                    tbl.Receptor = receptor[0];
                                    tbl.Fecha = DateTime.ParseExact((receptor[1].Trim()).Replace(".", ""), format, CultureInfo.InvariantCulture);
                                    break;
                                case 3:
                                    tbl.Estado = cell.Text;
                                    break;
                            }
                            i++;
                        }
                        if (objCausa.FechaUltReceptor < tbl.Fecha)
                        {
                            objCausa.TablaReceptor.Add(tbl);
                            InsertarReceptorPoderJudicial(objCausa.IdCausa, tbl.Cuaderno, tbl.Estado, tbl.Receptor, tbl.Fecha);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Dimol.dao.Funciones.InsertarError("Pagina de consulta de Causas del Poder Judicial offline.", "", "Bot Poder Judicial", 0);
                }

                //Close the browser
                driver.Quit();
            }
            catch (Exception ex)
            {
                //Close the browser
                driver.Quit();
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Bot Poder Judicial", 0);
            }
            //Dimol.dao.Funciones.InsertarError("Datos descargados del PJ.", "Rol: " + objCausa.TipoCausa + "-" + objCausa.RolCausa.ToString() + "-" + objCausa.RolAnio.ToString(), "Bot Poder Judicial", particion);


            //dc.SubmitChanges();
            //    scope.Complete();
            //}
        }


        #endregion

        #region "Procesar Rol HTML Demonio"

        public static void ProcesarRolHTMLDemonio(string anio, string estado)
        {
            Console.WriteLine("Inicio carga roles desde HTML");
            List<ScannerHTML> lst = PJSpider.dao.Causa.ListarCargaRolHTMLDemonio(anio, estado);
            Console.WriteLine("Lista de roles: " + lst.Count.ToString());
            ScannerCausas objCausa = new ScannerCausas();
            HtmlDocument html = new HtmlDocument();
            string fecha = "";
            string format = "dd/MM/yyyy";
            while (lst.Count > 0)
            {
                foreach (ScannerHTML s in lst)
                {
                    fecha = "";
                    if (s.HTML.Contains("Participante"))
                        try
                        {
                            html.LoadHtml(s.HTML);
                            try
                            {
                                // Grabar rol
                                objCausa.IdCausa = s.IdCausa;
                                objCausa.IdCuaderno = s.IdCuaderno;
                                objCausa.TipoCausa = s.TipoCausa;
                                objCausa.RolCausa = s.Rol;
                                objCausa.RolAnio = s.Anio;
                                objCausa.CodigoTribunal = s.Tribunal;
                                string[] ruta = new string[] { "", "" };
                                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//td[contains(text(),'Texto Demanda')]"))
                                {
                                    foreach (HtmlNode node in td.SelectNodes("img"))
                                    {
                                        if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/DocVacio.JPG"))
                                        {
                                            ruta = new string[] { "", "" };
                                        }
                                        else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/edit.jpg"))
                                        {
                                            ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                        }
                                        else if (node.GetAttributeValue("src", "").Contains("/CIVILPORWEB/img/Comun/generarpdf.gif"))
                                        {
                                            ruta = node.GetAttributeValue("onclick", "").Split('\'');
                                        }
                                    }
                                }
                                foreach (HtmlNode td in html.DocumentNode.SelectNodes("//*[text()[contains(., 'F. Ing :')]]"))
                                {
                                    fecha = td.InnerText.Replace("F. Ing :", "").Replace(" \r\n\t\t", "");
                                    if (!string.IsNullOrEmpty(fecha))
                                    {
                                        objCausa.FechaIngreso = DateTime.ParseExact(fecha.Trim(), format, CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        objCausa.FechaIngreso = new DateTime();
                                    }
                                }
                                if (!string.IsNullOrEmpty(ruta[1]))
                                {
                                    objCausa.RutaDemanda = "http://civil.poderjudicial.cl" + ruta[1];
                                }
                                else
                                {
                                    objCausa.RutaDemanda = null;
                                }

                                dao.Causa.InsertarRolHTML(objCausa.IdCausa, objCausa.TipoCausa, objCausa.RolCausa, objCausa.RolAnio, objCausa.CodigoTribunal, objCausa.RutaDemanda, objCausa.FechaIngreso);

                                // Grabar cuadernos
                                HtmlNode.ElementsFlags.Remove("option");
                                string idCuaderno = "", descCuaderno = "";
                                long test = 0;
                                foreach (HtmlNode cuaderno in html.DocumentNode.SelectNodes("//select[@name='CRR_Cuaderno']"))
                                {
                                    foreach (HtmlNode option in cuaderno.SelectNodes("option"))
                                    {
                                        idCuaderno = option.GetAttributeValue("value", "");
                                        if (string.IsNullOrEmpty(option.InnerHtml))
                                        {
                                            descCuaderno = option.NextSibling.InnerHtml;
                                        }
                                        else
                                        {
                                            descCuaderno = option.InnerHtml;
                                        }

                                        if (Int64.TryParse(idCuaderno, out test) && !string.IsNullOrEmpty(descCuaderno))
                                        {
                                            dao.Causa.InsertarCuadernoHTML(objCausa.IdCausa, Int32.Parse(idCuaderno), descCuaderno, idCuaderno == s.IdCuaderno.ToString() ? "N" : "S");
                                        }

                                    }
                                }

                                // Grabar Litigantes
                                int i = 1;
                                TablaLitigantes tbl = new TablaLitigantes();
                                foreach (HtmlNode litigantes in html.DocumentNode.SelectNodes("//div[@id='Litigantes']"))
                                {
                                    foreach (HtmlNode table in litigantes.SelectNodes("table"))
                                    {
                                        foreach (HtmlNode row in table.SelectNodes("tbody/tr"))
                                        {
                                            i = 1;
                                            tbl = new TablaLitigantes();
                                            foreach (HtmlNode cell in row.SelectNodes("td"))
                                            {
                                                switch (i)
                                                {
                                                    case 1:
                                                        tbl.Participante = cell.InnerText;
                                                        break;
                                                    case 2:
                                                        tbl.Rut = cell.InnerText;
                                                        break;
                                                    case 3:
                                                        tbl.Persona = cell.InnerText;
                                                        break;
                                                    case 4:
                                                        tbl.NombreRazonSocial = cell.InnerText;
                                                        break;
                                                }
                                                if (tbl.Participante == "Participante")
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    i++;
                                                }

                                            }
                                            if (tbl.Participante != "Participante")
                                            {
                                                dao.Causa.InsertarLitiganteHTML(objCausa.IdCausa, objCausa.IdCuaderno, tbl.Participante, tbl.Rut, tbl.Persona, tbl.NombreRazonSocial);
                                            }
                                        }
                                    }
                                }
                                Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                                dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "P");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                                Dimol.dao.Funciones.InsertarError("HTML con error al extraer los datos", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                                dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                            Dimol.dao.Funciones.InsertarError("HTML con error", "Rol: " + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString(), "Procesa Rol HTML", s.Anio);
                            dao.Causa.ActualizarRolHTMLFecha(s.IdCausa, s.IdCuaderno, "M");
                        }
                }
                lst = PJSpider.dao.Causa.ListarCargaRolHTMLDemonio(anio,estado);
            }
            Console.WriteLine("Fin carga roles desde HTML");
            Console.ReadLine();
        }


        public static int DetenerCargaRolHTMLDemonio(string anio, string estado)
        {
            return dao.Causa.DetenerCargaRolHTMLDemonio(anio, estado);
        }

        #endregion

        #region "Descargar Roles Externos Al dia"

        public static void DescargarRolesExternos(int anio, int codemp, int idioma, string estados, int particion, int cantidad)
        {
            int indice = 0;
            List<RolActualizar> lstRoles = new List<RolActualizar>();
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScanner();
            foreach (Dimol.dto.Combobox trib in lstTribunales)
            {
                lstRoles = dao.Causa.ListarRolesExternos(anio, Int32.Parse(trib.Value));
                foreach (RolActualizar obj in lstRoles)
                {
                    Console.WriteLine(obj.Rol.ToString() + "-" + obj.IdTribunal);
                    if (indice + 1 < obj.Rol)
                    {
                        Console.WriteLine("Pendiente: " + obj.Rol.ToString() + "-" + obj.IdTribunal);
                    }

                }


            }

            int inicio = 0;
            string htmlRol = "";
            inicio = (particion - 1) * cantidad;
            //ConsultaCausa obj = new ConsultaCausa();
            //List<RolActualizar> lstRoles = new List<RolActualizar>();
            List<DatoTipo> lstTipoCuaderno = dao.Causa.ListarTipoCuaderno();
            Dimol.dao.Funciones.InsertarError("Generando lista de roles para actualizar", "Estados: " + estados, "Actualizar Roles Internos", particion);
            // Genero la lista de roles a actualizar
            lstRoles = dao.Causa.ListarRolesInternos(codemp, idioma, estados);

            if (lstRoles.Count - inicio < cantidad)
            {
                cantidad = lstRoles.Count - inicio;
                if (cantidad <= 0)
                {
                    Dimol.dao.Funciones.InsertarError("Esta particion no es necesaria. Favor cerrar la aplicacion.", "Cantidad: " + lstRoles.Count, "Bot Poder Judicial", particion);
                    throw new Exception("Esta particion no es necesaria. Favor cerrar la aplicacion.");
                }
            }
            List<RolActualizar> lst = lstRoles.GetRange(inicio, cantidad);
            Dimol.dao.Funciones.InsertarError("Lista de roles para actualizar", "Cantidad: " + lst.Count, "Actualizar Roles Internosl", particion);

            //Levanto una ventana del navegador para obtener las cookies si es necesario
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
            try
            {
                while (!driver.Title.Contains("CONSULTA CAUSAS"))
                {
                    Dimol.dao.Funciones.InsertarError("Explorador de Causas del Poder Judicial offline.", "", "Actualizar Roles Internos", particion);
                    Thread.Sleep(600000);
                    driver.Navigate().GoToUrl(System.Configuration.ConfigurationManager.AppSettings["UrlPJ"]);
                }

                if (driver.Title.Contains("CONSULTA CAUSAS"))
                {
                }
            }
            catch (Exception ex)
            {

            }


            foreach (RolActualizar obj in lst)
            {
                Dimol.dao.Funciones.InsertarError("Actualizando", "Rol: " + obj.TipoCausa + "-" + obj.Numero + ", Tribunal: " + obj.Tribunal, "Bot Poder Judicial", particion);
                //obj.RolAnio = r.Anio;
                //obj.CodigoTribunal = r.IdTribunal;
                //obj.NombreTribunal = r.Tribunal;
                obj.Url = ConfigurationManager.AppSettings["UrlPJ"]; //"http://civil.poderjudicial.cl/CIVILPORWEB/";
                //obj.TipoCausa = r.TipoCausa;
                //obj.RolCausa = r.Rol;
                //obj.Codemp = r.Codemp;
                //obj.Rolid = r.Rolid;
                //obj.IdCausa = r.IdCausa;
                //obj.IdCuaderno = r.;
                //obj.FechaUltHistorial = r.FechaUltHistorial;
                //obj.FechaUltReceptor = r.FechaUltReceptor;
                int retry = 0;
                if (!string.IsNullOrEmpty(obj.UrlHTML))
                {
                    try
                    {
                        htmlRol = DescargaPagina(obj.UrlHTML);
                        if (htmlRol.Contains("textoC"))
                        {
                            //Insertar Rol
                            obj.HTML = htmlRol;
                            if (obj.IdCausa == 0)
                            {
                                string url = obj.UrlHTML;
                                string queryString = url.Remove(0, url.IndexOf('?') + 1);
                                string[] parametros = queryString.Split('&');
                                string[] parametro;
                                foreach (string s in parametros)
                                {
                                    parametro = s.Split('=');
                                    switch (parametro[0])
                                    {
                                        //case "TIP_Consulta":
                                        //    obj.TipoConsulta = Int32.Parse(parametro[1]);
                                        //    break;
                                        case "TIP_Cuaderno":
                                            obj.TipoCuaderno = Int32.Parse(parametro[1]);
                                            break;
                                        case "CRR_IdCuaderno":
                                            obj.IdCuaderno = Int32.Parse(parametro[1]);
                                            break;
                                        case "CRR_IdCausa":
                                            obj.IdCausa = Int32.Parse(parametro[1]);
                                            break;
                                        //case "TIP_Informe":
                                        //    obj.TipoInforme = Int32.Parse(parametro[1]);
                                        //    break;
                                        //case "COD_Tribunal":
                                        //    obj.CodigoTribunal = Int32.Parse(parametro[1]);
                                        //    break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            InsertarRolPoderJudicial(obj.Codemp, obj.Rolid, obj.TipoCausa, obj.IdCausa, obj.IdTribunal);
                            PJSpider.bcp.Causa.InsertarRolHTMLURL(obj.IdCausa, obj.IdCuaderno, obj.TipoCausa, obj.Rol, obj.Anio, obj.IdTribunal, htmlRol, "I", obj.UrlHTML);

                            ProcesarRolInternoHTML(obj, lstTipoCuaderno);
                        }
                        else
                        {
                            Dimol.dao.Funciones.InsertarError("Error bot roles internos", "No se encuentra Rol HTML: " + obj.TipoCausa + "-" + obj.Numero + "-" + obj.Anio, "Actualizar Roles Internos", obj.Anio);
                        }
                    }
                    catch (Exception ex)
                    {
                        //PJSpider.bcp.Causa.InsertarRolHTMLURL(s.IdCausa, s.IdCuaderno, s.TipoCausa, s.Rol, s.Anio, s.Tribunal, "", "M", r.UrlHTML);
                        Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Procesa Lista HTML Poder Judicial", obj.Anio);
                        throw ex;
                    }
                    //Console.WriteLine("Rol :" + s.TipoCausa + "-" + s.Rol.ToString() + "-" + s.Anio.ToString() + ", ID Tribunal: " + s.Tribunal.ToString());
                }
                else
                {
                    dao.Causa.BuscarListaRolesHTML(obj);
                    if (string.IsNullOrEmpty(obj.HTML) || obj.Estado == "M")
                    {
                        DescargarListaHTML(obj, driver);
                    }

                    DescargarRolListaHTML(obj, lstTipoCuaderno);

                    //htmlRol = DescargaPagina(r.UrlHTML);
                    //"http://civil.poderjudicial.cl/CIVILPORWEB/ConsultaDetalleAtPublicoAccion.do?TIP_Consulta=1&TIP_Cuaderno=1&CRR_IdCuaderno=564494&ROL_Causa=1&TIP_Causa=C&ERA_Causa=2009&CRR_IdCausa=512883&COD_Tribunal=161&TIP_Informe=1&"
                }
            }
            Dimol.dao.Funciones.InsertarError("Fin de roles internos para actualizar", "Estados: " + estados, "Actualizar Roles Internos", particion);
        }

        #endregion
    }
}
