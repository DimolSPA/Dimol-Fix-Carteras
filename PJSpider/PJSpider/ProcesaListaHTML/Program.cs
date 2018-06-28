using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesaListaHTML
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                PJSpider.bcp.Causa.ProcesarListaHTML(Int32.Parse(args[0]));
            }
            else
            {
                PJSpider.bcp.Causa.ProcesarListaHTML(2016);
            }
        }
    }
}
