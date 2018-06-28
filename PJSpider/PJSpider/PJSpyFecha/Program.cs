using PJSpider.bcp;
using PJSpider.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSpyFecha
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScanner();
            ScannerCausas obj = new ScannerCausas();
            int[] anios = { 2013, 2014, 2015, 2016 };
            Console.WriteLine("Iniciando Explorador Poder Judicial");
            #if DEBUG
                        string[] argss =  {"2013","2014"};
            #endif
            //argss = args;

            List<IndiceScanner> lstRoles = Causa.ListarIndiceScannerFecha();

            foreach (IndiceScanner rol in lstRoles)
            {
                Console.WriteLine("Iniciando Scanner Fecha");

                obj.RolAnio = rol.Anio;
                obj.CodigoTribunal = rol.Tribunal;
                obj.NombreTribunal = lstTribunales.Find(x => x.Value == rol.Tribunal.ToString()).Text;
                obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                obj.TipoCausa = rol.TipoCausa;
                obj.RolCausa = rol.Rol;
                obj.IdCausa = rol.IdCausa;
                obj.IdCuaderno = 0;
                try
                {
                    Causa.ExplorarCausaActualizaFecha(obj);
                    Console.WriteLine("Causa: " + obj.TipoCausa + "-" + obj.RolCausa + "-" + obj.RolAnio);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Rol no encontrado: " + obj.TipoCausa + "-" + obj.RolCausa.ToString() + "-" + obj.RolAnio.ToString() + ", Tribunal: " + obj.NombreTribunal);
                }

            }
            Console.WriteLine("Finalizando Explorador Poder Judicial");
            Console.ReadLine();
        }
    }
}
