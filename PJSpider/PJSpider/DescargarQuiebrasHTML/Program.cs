using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescargarQuiebrasHTML
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[] { DateTime.Today.Year.ToString() };
            }
            int anio = 0;
            try
            {
                anio = Int32.Parse(args[0]);
                PJSpider.bcp.Quiebra.DescargarQuiebrasHTML(anio);//Causa.ActualizarPoderJudicialParticion(1, 1, "-1", particion, cantidad);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el proceso. Particion: " + "" + "Mensaje: " + ex.Message);
            }
        }
    }
}
