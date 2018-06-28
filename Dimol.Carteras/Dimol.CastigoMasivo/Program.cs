using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Reportes.bcp;
using Dimol.Reportes.dto;
using System.Configuration;

namespace Dimol.CastigoMasivo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //bool error = false;
                //string[] rut = model.RutCliente.Split('-');
                Dimol.Carteras.dto.CastigoMasivo objCarga = new Dimol.Carteras.dto.CastigoMasivo();
                objCarga.Archivo = "test";
                objCarga.Pclid = 78;
                objCarga.Codemp = 1;
                objCarga.Sucid = 1;
                objCarga.Tpcid = 31; // castigo prejudicial
                objCarga.Pagina = 359;
                string ubicacion = @"\\10.0.1.238\Usuarios\castigos";//ConfigurationManager.AppSettings["RutaCastigoMasivoOut"] + "309_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                if (!System.IO.Directory.Exists(ubicacion))
                {
                    System.IO.Directory.CreateDirectory(ubicacion);
                }

                // cargo lista de castigos
                List<Reportes.dto.CastigoMasivo> lst = Reportes.bcp.CastigoMasivo.ListarRutHtmlComplementariaMarzo2018();
                List<Reportes.dto.CastigoMasivo> lst100 = lst;
                //Reportes.bcp.CastigoMasivo.GeneraPDFporRutHiQPdf2(lst100, model.Pagina, ubicacion);
                int intervalo = 200, i = 0;
                lst100 = lst.GetRange(i, intervalo > lst.Count ? lst.Count : intervalo);
                Reportes.bcp.CastigoMasivo.GeneraPDFPrevisionalMarzo2018(lst100, 359, ubicacion);
                //Reportes.bcp.CastigoMasivo.GeneraPDFporRutComplementaria(lst100, 359, ubicacion);

                while (i + intervalo <= lst.Count)
                {
                    i = i + intervalo;
                    if (i + intervalo < lst.Count)
                    {
                        lst100 = lst.GetRange(i, intervalo);
                    }
                    else
                    {
                        lst100 = lst.GetRange(i, lst.Count - i);
                    }

                    Reportes.bcp.CastigoMasivo.GeneraPDFPrevisionalMarzo2018(lst100, 359, ubicacion);

                }




                //// cargo lista de castigos
                //List<Reportes.dto.CastigoMasivo> lst = Reportes.bcp.CastigoMasivo.ListarRutHtmlComplementaria();
                //List<Reportes.dto.CastigoMasivo> lst100 = lst;
                ////Reportes.bcp.CastigoMasivo.GeneraPDFporRutHiQPdf2(lst100, model.Pagina, ubicacion);
                //int intervalo = 260, i = 0;
                //lst100 = lst.GetRange(i, intervalo > lst.Count ? lst.Count : intervalo);
                //Reportes.bcp.CastigoMasivo.GeneraPDFporRutSinCertificado(lst100, 359, ubicacion);
                ////Reportes.bcp.CastigoMasivo.GeneraPDFporRutComplementaria(lst100, 359, ubicacion);
                //while (i + intervalo <= lst.Count)
                //{
                //    i = i + intervalo;
                //    if (i + intervalo < lst.Count)
                //    {
                //        lst100 = lst.GetRange(i, intervalo);
                //    }
                //    else
                //    {
                //        lst100 = lst.GetRange(i, lst.Count - i);
                //    }

                //    Reportes.bcp.CastigoMasivo.GeneraPDFPrevisionalMarzo2018(lst100, 359, ubicacion);

                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Dimol.bcp.Funciones.InsertarError(ex.Message, ex.StackTrace, "GeneraPDFporRutComplementaria: ", 309);
            }
        }
    }
}
