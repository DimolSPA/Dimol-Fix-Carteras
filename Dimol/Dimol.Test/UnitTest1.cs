using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dimol.bcp;
using Dimol.dao;
using System.Collections.Generic;
using Dimol.dto;

namespace Dimol.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Encripta()
        {
            Dimol.bcp.Usuarios objUsuario = new Dimol.bcp.Usuarios();

            string usuario = objUsuario.Encripta("reporte");//°°¸¶¾´  "°¸Á»½É"
            string password = objUsuario.Encripta("417500");
        }



        [TestMethod]
        public void Desencripta()
        {

            Dimol.bcp.Usuarios objUsuario = new Dimol.bcp.Usuarios();

            string copec = objUsuario.Desencripta("°°¸¶¾´");//  Ž´¹}º€‰ˆ‹");//felipe
            string copecStgo = objUsuario.Desencripta("½°¼ÂºÅµµ³…„†‹");//  Ž´¹}º€‰ˆ‹");//felipe
            string copecPass = objUsuario.Desencripta("®´¹¼º");//  Ž´¹}º€‰ˆ‹");//felipe
            string copecStgoPass = objUsuario.Desencripta("­­­®´¹¼º");//  Ž´¹}º€‰ˆ‹");//felipe
            string mutual = objUsuario.Desencripta("·ÀÀÂ¯»");//  Ž´¹}º€‰ˆ‹");//felipe
            string mutualPass = objUsuario.Desencripta("·ÀÀÂ¯»ƒ…‡");//  Ž´¹}º€‰ˆ‹");//felipe
            string cocha = objUsuario.Desencripta("¾À¾¶Á¼¿´Á¶¼¶");//  Ž´¹}º€‰ˆ‹");//felipe
            string cochaPass = objUsuario.Desencripta("­º¯µ¯€‚„†");//  Ž´¹}º€‰ˆ‹");//felipe
            string oriencoop = objUsuario.Desencripta("¹½µ²¼²¿ÀÂ");//  Ž´¹}º€‰ˆ‹");//felipe
            string oriencoopPass = objUsuario.Desencripta("¹½µ²¼²¿ÀÂ…„†Š");//  Ž´¹}º€‰ˆ‹");//felipe
            string miPass = objUsuario.Desencripta("°°¸¶¾´‚ƒˆ");
            string continental = objUsuario.Desencripta("­ººÁ·½µ¿Æ´À");//  Ž´¹}º€‰ˆ‹");//felipe
            string continentalPass = objUsuario.Desencripta("­ººÁ·½µ¿Æ´À‡†ˆ");//  Ž´¹}º€‰ˆ‹");//felipe

            string userIVR = objUsuario.Desencripta("½¬");//  Ž´¹}º€‰ˆ‹");//felipe
            string passIVR = objUsuario.Desencripta("~|ƒ‚~");//  Ž´¹}º€‰ˆ‹");//felipe

        }

        [TestMethod]
        public void AppPath()
        {

            Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();

            string eee = objFunc.AppPath();
        }

        [TestMethod]
        public void Configuracion_Num()
        {

            Dimol.dao.Funciones objFunc = new Dimol.dao.Funciones();

            decimal eee = objFunc.ConfiguracionNum(1);
        }

        [TestMethod]
        public void Conexion()
        {

            Conexion objFunc = new Conexion();

            string eee = objFunc.BaseDatos;
        }

        [TestMethod]
        public void MEnu1()
        {

            dao.Menu objFunc = new dao.Menu();

            string[] eee = objFunc.TraeMenuEncriptado(1);
        }

        [TestMethod]
        public void MEnuCompleto()
        {

            bcp.Menu objFunc = new bcp.Menu();

            //objFunc.Cargar(223,1,1);
            //objFunc.OrdenarMenu();
        }

        [TestMethod]
        public void Split()
        {

            string lala = "\\dada\ad\asdsads\adsadadsa.pdf";
            string[] lele = lala.Split(',');
            
        }

        [TestMethod]
        public void IndicadoresBC()
        {

            Dimol.bcp.Funciones func = new Dimol.bcp.Funciones();
            Indicadores objIndicadores = new Indicadores();
            bool salida = true;
            try
            {
                dao.Funciones.TraeDolarUFHoy(1, objIndicadores);
                objIndicadores = bcp.Funciones.DescargaContenidoPaginaBC(System.Configuration.ConfigurationManager.AppSettings["UrlIndicadoresBC"]);
                objIndicadores.UTM = bcp.Funciones.DescargaUTMBC(System.Configuration.ConfigurationManager.AppSettings["UrlUTMBC"]);
            }
            catch (Exception ex)
            {
                salida = false;
                //    objIndicadores.DolarObservado = 0,0;
                //    objIndicadores.IPC = 0.0;
                //    objIndicadores.UF=0
            }

        }

        [TestMethod]
        public void Meses()
        {
            int mes = DateTime.Now.Month;
            string dss = bcp.Funciones.nombreMeses(mes);

        }

    }
}

