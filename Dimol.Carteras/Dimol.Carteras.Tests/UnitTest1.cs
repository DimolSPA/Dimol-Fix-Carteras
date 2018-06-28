using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dimol.PDF;
using Dimol.dao;
using Dimol.dto;
using System.Data.SqlClient;
using System.Data;
using Dimol.ConvertidorImagenes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Dimol.Carteras.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestImage()
        {
            try
            {
                string ruta;
                int inicio;
                int cantidad;
                ruta = @"c:\imagenes\" ;
                inicio = 1;
                cantidad = 20;
                Convertidor objConvertidor = new Convertidor();
                objConvertidor.Convertir(ruta,inicio,cantidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestImageEmpleados()
        {
            try
            {
                string ruta;
                int inicio;
                int cantidad;
                ruta = @"/Images/Empleados/";
                inicio = 1;
                cantidad = 400;
                Convertidor objConvertidor = new Convertidor();
                objConvertidor.ConvertirEmpleados(ruta, inicio, cantidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestPDF()
        {
            Generador objGenerador = new Generador("d:\\LIQUIDACION.fop", "d:\\LIQUIDACION.pdf");
            objGenerador.XSLToPDF();
            //objGenerador.EjecutaFOP("alignment.fo", "alignment.pdf");
        }
        
        [TestMethod]
        public void TestXSLPDF()
        {
            Transformador objTransformador = new Transformador();
            objTransformador.TransformXml(null, @"d:\SD2FO-DOC.xsl");
            Generador objGenerador = new Generador(@"D:\path.fo",@"D:\path.pdf");
            objGenerador.XSLToPDF();
            //objGenerador.EjecutaFOP("alignment.fo", "alignment.pdf");
        }

        [TestMethod]
        public void TestInsert()
        {
            try
            {
                Conexion conn = new Conexion();
                conn.SQLConn.Open();
                SqlTransaction trans = conn.SQLConn.BeginTransaction();
                
                StoredProcedure sp = new StoredProcedure("_Insertar_Acciones");
                sp.AgregarParametro("codemp", 1);
                sp.AgregarParametro("nombre", "lala");
                sp.AgregarParametro("agrupa", "1");
                sp.AgregarParametro("idid",1);
                int error = sp.EjecutarProcedimiento(conn, trans);

                if (error > 0)
                {
                    trans.Commit();
                    conn.SQLConn.Close();
                }
                else
                {
                    trans.Rollback();
                    conn.SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
 
        }

        [TestMethod]
        public void TestDelete()
        {
            try
            {
                Conexion conn = new Conexion();
                conn.SQLConn.Open();
                SqlTransaction trans = conn.SQLConn.BeginTransaction();

                StoredProcedure sp = new StoredProcedure("Delete_Acciones");
                sp.AgregarParametro("acc_codemp", 1);
                sp.AgregarParametro("acc_accid", 11);
                int error = sp.EjecutarProcedimiento(conn, trans);

                if (error > 0)
                {
                    trans.Commit();
                    conn.SQLConn.Close();
                }
                else
                {
                    trans.Rollback();
                    conn.SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestRutValidator()
        {
            bool res = bcp.Funciones.ValidaRut("135644994");
        }

        [TestMethod]
        public void TestDescargaPagina()
        {
             Dimol.dto.Indicadores obj = bcp.Funciones.DescargaContenidoPagina("http://www.bancoestado.cl/bancoestado/indiceseconomicos/indicadores.asp");

        }


    }
}
