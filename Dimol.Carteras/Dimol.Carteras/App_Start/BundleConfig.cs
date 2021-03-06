﻿using System.Web;
using System.Web.Optimization;

namespace Dimol.Carteras
{
    public class BundleConfig
    {
        // Para obtener más información acerca de Bundling, consulte http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/site.js",
                        "~/Scripts/caja.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery-ui.unobtrusive-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/menu").Include(
                        "~/Scripts/menu_jquery.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de creación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/menu").Include("~/Content/menu_styles.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/slider").Include(
                        "~/Content/slider/demo.css",
                        "~/Content/slider/elastislide.css",
                        "~/Content/slider/reset.css",
                        "~/Content/slider/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/slider").Include(
                        "~/Scripts/slider/gallery.js",
                        "~/Scripts/slider/jquery.easing.1.3.js",
                        "~/Scripts/slider/jquery.elastislide.js",
                        "~/Scripts/slider/jquery.tmpl.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jszip").Include(
                        "~/Scripts/jszip.js",
                        "~/Scripts/FileSaver.js",
                        "~/Scripts/jszip-utils.js"));
            bundles.Add(new StyleBundle("~/Content/caja").Include("~/Content/Caja.css"));
        }
    }
}