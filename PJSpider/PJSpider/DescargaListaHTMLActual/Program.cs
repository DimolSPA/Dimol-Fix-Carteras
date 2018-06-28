using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescargaListaHTMLActual
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[] { System.Configuration.ConfigurationManager.AppSettings["ServiceYear"], System.Configuration.ConfigurationManager.AppSettings["ServiceNumber"] };
            }
            int anio = 0, inicio =1;
            try
            {
                anio = Int32.Parse(args[0]);
                inicio = Int32.Parse(args[1]);
                PJSpider.bcp.Causa.DescargarListaHTML(anio, inicio);//Causa.ActualizarPoderJudicialParticion(1, 1, "-1", particion, cantidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el proceso. Particion: "+"" + "Mensaje: " + ex.Message);
            }
        }
    }
}
