using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.ConvertidorImagenes;
using System.Drawing.Imaging;

namespace Dimol.ConvertidorExe
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Iniciando conversion desde la fila numero " + args[0] + " a " + args[1]);
                string ruta;
                int inicio;
                int cantidad;
                ruta = @"d:\imagenes\";
                inicio = Int32.Parse( args[0]);
                cantidad = Int32.Parse( args[1]);
                Convertidor objConvertidor = new Convertidor();
                Console.WriteLine("Generando lista de imagenes"); 
                objConvertidor.GenerarListaImagenes(inicio, cantidad);
                Console.WriteLine("Lista de imagenes cargada, total: "+ objConvertidor.ListaImagenes.Count); 

                string extension = "";
                foreach (ConverterEntity obj in objConvertidor.ListaImagenes)
                {
                    ConverterEntity objImagen = objConvertidor.RecuperarImagen(obj.Codemp, obj.Pclid, obj.Ctcid, obj.Ccbid, obj.Cdid);
                    extension = "";
                    if (ImageFormat.Jpeg.Equals(objImagen.Imagen.RawFormat))
                    {
                        extension = ".jpg";// JPEG
                    }
                    else if (ImageFormat.Png.Equals(objImagen.Imagen.RawFormat))
                    {
                        extension = ".png";// PNG
                    }
                    else if (ImageFormat.Gif.Equals(objImagen.Imagen.RawFormat))
                    {
                        extension = ".gif";// GIF
                    }
                    obj.NombreArchivoImagen = ruta + obj.NombreArchivoImagen + extension;
                    Console.WriteLine(obj.NombreArchivoImagen);
                    objImagen.Imagen.Save(obj.NombreArchivoImagen);
                    Console.WriteLine("Archivo creado");
                    objConvertidor.ActualizarListaImagenes(obj);
                    Console.WriteLine("Tabla actualizada");
                }
            }
            catch (Exception ex)
            {
                Console.Write( ex.StackTrace);
            }

        }
    }
}
