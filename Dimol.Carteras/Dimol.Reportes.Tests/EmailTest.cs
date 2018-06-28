using Dimol.PDF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Email;


namespace Dimol.Reportes.Tests
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void TestSendEmail()
        {
            //try
            //{
            //    Email.bcp.Email objEmail = new Email.bcp.Email();
            //    Email.dto.Email email = new Email.dto.Email();

            //    email.Codemp = 1;
            //    email.To.Add("famunoz@gmail.com");
            //    email.Subject = "Test Email Dimol";
            //    email.Body = "Hola";
            //    email.Html = false;

            //    objEmail.EnviarEmail(email);

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }

        [TestMethod]
        public void TestResumenGestiones()
        {
            try
            {
                dto.ResumenGestiones obj = new dto.ResumenGestiones();
                obj.Codemp = 1;
                obj.Sucid = 1;
                obj.Pclid = 22;
                obj.Ctcid = 9787;
                obj.FechaReporte = DateTime.Now;

                //string ruta = bcp.Cartera.TraeResumenGestiones(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestInformeRemesa()
        {
            try
            {
                dto.InformeRemesa obj = new dto.InformeRemesa();
                obj.Codemp = 1;
                obj.Sucid = 1;
                obj.Idioma = 1;
                obj.TipoDocumento = 42;
                obj.Numero = 1317;
                obj.FechaReporte = DateTime.Now;

                string ruta = bcp.Cartera.TraeInformeRemesa(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestGenerico()
        {
            try
            {
                dto.ResumenGestiones obj = new dto.ResumenGestiones();
                obj.Codemp = 1;
                obj.Sucid = 1;
                obj.Pclid = 22;
                obj.Ctcid = 9787;
                obj.FechaReporte = DateTime.Now;

                //string ruta = bcp.Cartera.TraeResumenGestiones(obj);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void TestEmailMutual()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
