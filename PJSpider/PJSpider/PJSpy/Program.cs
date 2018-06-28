using PJSpider.bcp;
using PJSpider.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSpy
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dimol.dto.Combobox> lstTribunales = Causa.ListarTribunalesScanner();
            ScannerCausas obj = new ScannerCausas();
            int[] anios = { 2013, 2014, 2015, 2016 };
            Console.WriteLine("Iniciando Explorador Poder Judicial");
            List<int> lst= new List<int>();
            int max = 0,cambiar = 0,i = 0, j = 0;
            #if DEBUG
            string[] argss =  {"2013","2014"};
            #endif
            //argss = args;
            foreach (string anio in args)
            {
                Console.WriteLine("Iniciando anio: "+ anio);
                foreach (Dimol.dto.Combobox tribunal in lstTribunales)
                {
                    cambiar = 0;
                    i = 1;
                    Console.WriteLine("Iniciando tribunal: " + tribunal.Text.Trim()+", cambiar: "+ cambiar);
                    lst= Causa.ListarRolesScanner(Int32.Parse( anio), Int32.Parse(tribunal.Value));
                    if (lst.Count > 0)
                    {
                        for (i = 0; i < lst.Count; i++)
                        {
                            if (i > 1 && lst[i] - lst[i - 1] > 1)
                            {
                                for (j = lst[i - 1] + 1; j < lst[i]; j++)
                                {
                                    obj.RolAnio = Int32.Parse(anio);
                                    obj.CodigoTribunal = Int32.Parse(tribunal.Value);
                                    obj.NombreTribunal = tribunal.Text;
                                    obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                                    obj.TipoCausa = "C";
                                    obj.RolCausa = j;
                                    obj.IdCausa = 0;
                                    obj.IdCuaderno = 0;
                                    try
                                    {
                                        Causa.ExplorarCausaConFecha(obj);
                                        Console.WriteLine("Causa: " + obj.TipoCausa + "-" + obj.RolCausa + "-" + obj.RolAnio);
                                        cambiar = 0;
                                    }
                                    catch (Exception ex)
                                    {
                                        cambiar++;
                                        Console.WriteLine("Rol no encontrado: " + obj.TipoCausa + "-" + obj.RolCausa.ToString() + "-" + obj.RolAnio.ToString() + ", Tribunal: " + obj.NombreTribunal);
                                    }
                                }
                            }
                        }
                        max = (int)lst[lst.Count - 1] + 1;//(from l in ()lst select l).Max().FirstorDefault();
                    }
                    else
                    {
                        max = 1;
                    }
                    while (cambiar < 5)
                    {
                        obj.RolAnio = Int32.Parse(anio);
                        obj.CodigoTribunal = Int32.Parse(tribunal.Value);
                        obj.NombreTribunal =  tribunal.Text;
                        obj.Url = "http://civil.poderjudicial.cl/CIVILPORWEB/";
                        obj.TipoCausa = "C";
                        obj.RolCausa = max;
                        obj.IdCausa = 0;
                        obj.IdCuaderno = 0;
                        try
                        {
                            Causa.ExplorarCausaConFecha(obj);
                            Console.WriteLine("Causa: " + obj.TipoCausa + "-" + obj.RolCausa + "-" + obj.RolAnio);
                            cambiar = 0;
                        }
                        catch (Exception ex)
                        {
                            cambiar++;
                            Console.WriteLine("Rol no encontrado: " + obj.TipoCausa + "-" + obj.RolCausa.ToString() + "-" + obj.RolAnio.ToString() + ", Tribunal: " + obj.NombreTribunal);
                        }

                        max++;
                    }
                    Console.WriteLine("Cambio tribunal: " + tribunal.Text.Trim() + ", cambiar: " + cambiar);
                }
                Console.WriteLine("Cambio anio: " + anio);
            }
            Console.WriteLine("Finalizando Explorador Poder Judicial");
            Console.ReadLine();
        }
    }
}
