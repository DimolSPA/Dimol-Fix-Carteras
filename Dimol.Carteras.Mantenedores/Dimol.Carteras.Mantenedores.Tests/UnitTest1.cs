using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dimol.PDF;
using Dimol.dao;
using System.Data.SqlClient;

namespace Dimol.Carteras.Mantenedores.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPDF()
        {
            Generador objGenerador = new Generador("c:\\alignment.fo", "c:\\alignment.pdf");
            objGenerador.XSLToPDF();
            //objGenerador.EjecutaFOP("alignment.fo", "alignment.pdf");
        }

        [TestMethod]
        public void Encripta()
        {
            CYPH.Ucode sdf = new CYPH.Ucode();
            string user =sdf.Encripta("sa");
            string password = sdf.Encripta("password");

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
    }
}
