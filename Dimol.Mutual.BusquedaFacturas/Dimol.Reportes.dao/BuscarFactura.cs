using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class BuscarFactura
    {
        public static void TraeTitulo(dto.ResumenGestiones obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Historial_Gestiones_Titulo");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.Titulo = new dto.TituloReporte
                        {
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutDeudor =Dimol.bcp.Funciones.formatearRut(  ds.Tables[0].Rows[i]["RutDeudor"].ToString())
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ListarDocumentosDetalle(dto.BuscarFactura obj)
        {
            try
            {
                int c = 0;
                string ruta1 = "";
                string ruta2 = "";
                string busqueda = "";
                string[] archivos;
                string archivo = "";
                string rutFormato = "";
                
                List<dto.BuscarFacturaBruto> lstBruto = new List<dto.BuscarFacturaBruto>();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Buscar_Facturas");
                
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstBruto.Add(new dto.BuscarFacturaBruto
                        {
                            Rut = decimal.Parse(ds.Tables[0].Rows[i]["rut"].ToString()),
                            Dv = ds.Tables[0].Rows[i]["dv"].ToString(),
                            Facturas = ds.Tables[0].Rows[i]["Facturas"].ToString(),
                            Cruce = ds.Tables[0].Rows[i]["cruce"].ToString(),
                            SS = ds.Tables[0].Rows[i]["SS"].ToString(),
                            MILANO = ds.Tables[0].Rows[i]["MILANO"].ToString(),
                            Nombre1 = ds.Tables[0].Rows[i]["Nombre1"].ToString(),
                            Div = decimal.Parse(ds.Tables[0].Rows[i]["Div"].ToString()),
                            Referencia = decimal.Parse(ds.Tables[0].Rows[i]["Referencia"].ToString()),
                            M = decimal.Parse(ds.Tables[0].Rows[i]["M"].ToString()),
                            Ano = decimal.Parse(ds.Tables[0].Rows[i]["Ano"].ToString())
                        });
                    }
                                       
                    

                    if (!Directory.Exists(obj.CarpetaRaiz))
                    {
                        DirectoryInfo raiz = Directory.CreateDirectory(obj.CarpetaRaiz);
                    }
                         
                    System.IO.StreamWriter file = new System.IO.StreamWriter(obj.CarpetaRaiz + "Listado.txt");
                    file.WriteLine("Rut|Dv|Referencia|Generado|Ubicacion");

                    foreach (var item in lstBruto)
                    {
                        c = 0;
                        rutFormato = item.Rut + item.Dv; //item.Rut.ToString("0000000000");

                         if (item.MILANO == "")
                         {
                            if (item.M < 10)
                            {
                                ruta1 = @"W:\magnolia\" + item.Ano + @"\0" + item.M + @"\EMITIDOS\PDF\";
                            }
                            else
                            {
                                ruta1 = @"W:\magnolia\" + item.Ano + @"\" + item.M + @"\EMITIDOS\PDF\";
                            }

                            //Quitar
                            ruta1 = @"W:\magnolia\";

                            busqueda = "*000" + item.Referencia + ".pdf";

                            if (Directory.Exists(ruta1)) {

                            // DirectoryInfo di = new DirectoryInfo(ruta1);
                            // DirectoryInfo[] directories = di.GetDirectories();

                            //foreach (DirectoryInfo dir in directories)

                            string[] direct = { @"W:\magnolia\2011\04\EMITIDOS\PDF\",
                                                @"W:\magnolia\2011\05\EMITIDOS\PDF\",
                                                @"W:\magnolia\2011\06\EMITIDOS\PDF\",
                                                @"W:\magnolia\2011\07\EMITIDOS\PDF\",
                                                @"W:\magnolia\2011\08\EMITIDOS\PDF\",
                                                @"W:\magnolia\2011\09\EMITIDOS\PDF\",
                                                @"W:\magnolia\2011\10\EMITIDOS\PDF\",
                                                @"W:\magnolia\2011\11\EMITIDOS\PDF\",
                                                @"W:\magnolia\2011\12\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\01\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\02\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\03\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\04\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\05\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\06\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\07\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\08\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\09\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\10\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\11\EMITIDOS\PDF\",
                                                @"W:\magnolia\2012\12\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\01\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\02\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\03\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\04\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\05\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\06\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\07\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\08\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\09\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\10\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\11\EMITIDOS\PDF\",
                                                @"W:\magnolia\2013\12\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\01\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\02\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\03\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\04\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\05\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\06\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\07\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\08\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\09\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\10\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\11\EMITIDOS\PDF\",
                                                @"W:\magnolia\2014\12\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\01\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\02\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\03\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\04\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\05\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\06\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\07\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\08\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\09\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\10\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\11\EMITIDOS\PDF\",
                                                @"W:\magnolia\2015\12\EMITIDOS\PDF\" };

                                foreach (string dir in direct)
                                {
                                    archivos = Directory.GetFiles(dir, busqueda);
                                
                                    if (archivos.Length!=0)
                                    {
                                        archivo = archivos[0];
                                        c = 1;
                                    }                              
                                                                        
                                }

                                    if (c == 0)
                                    {
                                        file.WriteLine(item.Rut + "|" + item.Dv + "|" + item.Referencia + "|No|" + ruta1);

                                    }
                                    else
                                    {
                                        file.WriteLine(item.Rut + "|" + item.Dv + "|" + item.Referencia + "|Si|" + ruta1);
                                        DirectoryInfo direc = Directory.CreateDirectory(obj.CarpetaRaiz + rutFormato);
                                        System.IO.File.Copy(archivo, obj.CarpetaRaiz + rutFormato + @"\" + item.Referencia + ".pdf", true);
                                    }

                            }
                            else
                            {
                                file.WriteLine(item.Rut + "|" + item.Dv + "|" + item.Referencia + "|No|" + ruta1);
                            }
                         }
                         else
                         {
                             ruta2 = @"W:\RESPALDOS - RECEPCION CONFORME - PDF\MILANO - PDF  .... 2015\" + item.MILANO.Replace("/", @"\");
                             ruta1 = @"W:\Facturas  PDF\Facturas 2014 y anteriores recepcionadas\" + item.MILANO.Replace("/", @"\");

                             if (File.Exists(ruta1))
                             {
                                 file.WriteLine(item.Rut + "|" + item.Dv + "|" + item.Referencia + "|Si|" + ruta1);
                                 DirectoryInfo di = Directory.CreateDirectory(obj.CarpetaRaiz + rutFormato);
                                 System.IO.File.Copy(ruta1, obj.CarpetaRaiz + rutFormato + @"\" + item.Referencia + ".pdf", true);
                             }
                             else if (File.Exists(ruta2))
                             {
                                 file.WriteLine(item.Rut + "|" + item.Dv + "|" + item.Referencia + "|Si|" + ruta2);
                                 DirectoryInfo di = Directory.CreateDirectory(obj.CarpetaRaiz + rutFormato);
                                 System.IO.File.Copy(ruta2, obj.CarpetaRaiz + rutFormato + @"\" + item.Referencia + ".pdf", true);
                             }
                             else
                             {
                                 file.WriteLine(item.Rut + "|" + item.Dv + "|" + item.Referencia + "|No|?");
                             }

                         }

                        DataSet ds2 = new DataSet();
                        StoredProcedure sp2 = new StoredProcedure("_Insert_Log_Buscar_Facturas");

                        sp2.AgregarParametro("rut", item.Rut.ToString() + item.Dv);
                        sp2.AgregarParametro("cliente", item.Nombre1);
                        sp2.AgregarParametro("factura", item.Referencia.ToString());
                        sp2.AgregarParametro("ruta", string.IsNullOrEmpty(ruta1)? ruta2:ruta1);

                        ds2 = sp2.EjecutarProcedimiento();
                    }

                    file.Close();

                    /* List<string> clientes = lstBruto.Select(o => o.Rut).Distinct().ToList();

                     foreach (var item in clientes)
                     {
                         dto.BuscarFacturaCliente objCli = new dto.BuscarFacturaCliente();

                         foreach (var det in lstBruto.Where(o => o.Rut == item))
                         {
                             objCli.lstDetalle.Add(new dto.BuscarFacturaDetalle
                             {
                                 Factura = det.Factura,
                                 Fecha = det.Fecha,
                                 Prestacion = det.Prestacion,
                                 Agencia = det.Agencia,
                                 Monto = det.Monto
                             });
                             objCli.Rut = det.Rut;
                             objCli.Nombre = det.Nombre;
                         }
                         objCli.Totales.Total = lstBruto.Where(o => o.Rut == item).Select(o => o.Monto).Sum();
                         obj.lstCli.Add(objCli);
                     }
                     */
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
