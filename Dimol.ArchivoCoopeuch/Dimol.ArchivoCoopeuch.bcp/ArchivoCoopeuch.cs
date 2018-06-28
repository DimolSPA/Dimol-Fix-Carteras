using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dao;
using Dimol.bcp;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Text.RegularExpressions;
namespace Dimol.ArchivoCoopeuch.bcp
{
    public class ArchivoCoopeuch
    {
        public static void ListarGestionesCobralex()
        {

            try
            {
                Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();
                string fileName = "";
                string path = "";
                Regex re = new Regex("[;\\\\/:*?\"<>|&']|\t|\n");

                List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch> lst = new List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch>();
                List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch> lstPoderJudicial = new List<Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch>();
                
                lst = Dimol.ArchivoCoopeuch.dao.ArchivoCoopeuch.ListarGestionesCobralex(279);
                var csv = new StringBuilder();
                var firstLine = "RUT_DEUDOR;CODIGO_TRIBUNAL;ROL_TRIBUNAL;FECHA_GESTION;CODIGO_GESTION;COMENTARIO";
                csv.AppendLine(firstLine);
                foreach (Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch a in lst)
                {
                    //Construir csv, con gestiones internas
                    if (!string.IsNullOrEmpty(a.CodigoGestion))
                    {
                        var newLine = string.Format("{0};{1};{2};{3};{4};{5}", a.RutDeudor, a.CodigoTribunal, a.RolTribunal, a.FechaGestion, a.CodigoGestion, re.Replace(a.Comentario, " "));
                        csv.AppendLine(newLine);
                    }

                }

                lstPoderJudicial = Dimol.ArchivoCoopeuch.dao.ArchivoCoopeuch.ListarGestionesCobralexPoderJudicial(279);
                foreach (Dimol.ArchivoCoopeuch.dto.ArchivoCoopeuch poderJudicial in lstPoderJudicial)
                {
                    //Construir csv, con datos de poder Judicial
                    if (!string.IsNullOrEmpty(poderJudicial.CodigoGestion))
                    {
                        var newLine = string.Format("{0};{1};{2};{3};{4};{5}", poderJudicial.RutDeudor, poderJudicial.CodigoTribunal, poderJudicial.RolTribunal, poderJudicial.FechaGestion, poderJudicial.CodigoGestion, re.Replace(poderJudicial.Comentario, " "));
                        csv.AppendLine(newLine);
                    }

                }

                fileName = "Coopeuch_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                path = ConfigurationManager.AppSettings["RutaArchivosCoopeuch"];
                objFunc.CreaCarpetas(path);
                //To save file
                FileInfo fi = new FileInfo(path + fileName);

                if (!fi.Exists)
                {
                    fi.Create().Dispose();
                }

                File.AppendAllText(path + fileName, csv.ToString(), Encoding.UTF8);

                //using
                //    (
                //        var sw = new StreamWriter(
                //                                new FileStream(path + fileName, FileMode.Open, FileAccess.ReadWrite),
                //                                Encoding.UTF8
                //                                )
                //    )
                //{
                //    sw.Write(csv.ToString());
                //}
   
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarGestionesCobralex", 0);
            }
        }
    }
}
