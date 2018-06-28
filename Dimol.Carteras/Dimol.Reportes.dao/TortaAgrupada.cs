using Dimol;
using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class TortaAgrupada
    {
        public static void TraeTitulo(dto.TortaAgrupada obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Historial_Gestiones_Titulo");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("ctcid", DBNull.Value);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.Titulo = new dto.TituloReporte
                        {
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutDeudor =Dimol.bcp.Funciones.formatearRut(  ds.Tables[0].Rows[i]["RutDeudor"].ToString()),
                            RutCliente =Dimol.bcp.Funciones.formatearRut(  ds.Tables[0].Rows[i]["RutCliente"].ToString())
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        //Following function will return Distinct records for Name, City and State column.
        public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
        {
            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = dt.DefaultView.ToTable(true, Columns);
            return dtUniqRecords;
        }

        public static void ListarDocumentosDetalleTodo(dto.TortaAgrupada obj)
        {
            try
            {
                string agrupa = "";
                int deudores = 0;
                int documentos = 0;
                decimal monto = 0;
                decimal regularizado = 0;
                decimal compromiso = 0;
                decimal saldo = 0;
                decimal sumaMonto = 0;
                decimal sumaRegularizado = 0;
                decimal sumaCompromiso = 0;
                decimal sumaSaldo = 0;
                decimal totalMonto = 0;
                decimal totalRegularizado = 0;
                decimal totalCompromiso = 0;
                decimal totalSaldo = 0;


                //int totalDeudores = 0;
                int totalDocumentos = 0;
                List<dto.TortaEstadoBruto> lstBruto = new List<dto.TortaEstadoBruto>();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Torta_Generica");
                sp.AgregarParametro("ccb_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("ccb_estcpbt", obj.EstadoCpbt);
                sp.AgregarParametro("idi_idid", obj.Idioma);              
                
                sp.AgregarParametro("cod_ges", obj.CodGestor);
                sp.AgregarParametro("cod_carga", obj.CodigoCarga);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj.NombreCarga = dr["pcc_nombre"].ToString();
                            obj.NombreGestor = dr["nom_gestor"].ToString();

                            lstBruto.Add(new dto.TortaEstadoBruto()
                            {
                                Agrupa = Int32.Parse(dr["ect_agrupa"].ToString()),
                                Asignado = decimal.Parse(dr["ccb_asignado"].ToString()),
                                Ccbid = Int32.Parse(dr["ccb_ccbid"].ToString()),
                                CodigoMoneda = Int32.Parse(dr["ccb_codmon"].ToString()),
                                Compromiso = decimal.Parse(dr["ccb_compromiso"].ToString()),
                                Ctcid = Int32.Parse(dr["ccb_ctcid"].ToString()),
                                Estado = dr["eci_nombre"].ToString(),
                                EstadoCpbt = dr["ccb_estcpbt"].ToString(),
                                Monto = decimal.Parse(dr["ccb_monto"].ToString()),
                                Nombre = dr["pcl_nombre"].ToString(),
                                NombreMoneda = dr["mon_nombre"].ToString(),
                                Pclid = Int32.Parse(dr["ccb_pclid"].ToString()),
                                Prejudicial = dr["ect_prejud"].ToString(),
                                Rut = dr["pcl_rut"].ToString(),
                                Saldo = decimal.Parse(dr["ccb_saldo"].ToString()),
                                TipoCambio = decimal.Parse(dr["ccb_tipcambio"].ToString()),
                                TipoCartera = Int32.Parse(dr["ccb_tipcart"].ToString()),
                                TotalDeuda = decimal.Parse(dr["TotDeu"].ToString())

                            });

                        }

                        List<int> agrupaEstado = lstBruto.Select(o => o.Agrupa).Distinct().ToList();

                        foreach (int dr in agrupaEstado)
                        {
                            switch (dr)
                            {
                                case 1:
                                    agrupa = "NUEVA ASIGNACION";
                                    break;
                                case 2:
                                    agrupa = "BUENOS";
                                    break;
                                case 3:
                                    agrupa = "REGULARES";
                                    break;
                                case 4:
                                    agrupa = "MALOS";
                                    break;


                                /*case 5:
                                    agrupa = "FINALIZADO";
                                    break;
                                case 6:
                                    agrupa = "CASTIGOS";
                                    break;
                                case 7:
                                    agrupa = "FINALIZADO POR CLIENTE";


                                    break;*/
                                default:
                                    agrupa = "OTRO";
                                    break;
                            }
                            dto.TortaCliente objTC = new dto.TortaCliente();
                            objTC.Agrupa = agrupa;
                            objTC.IdAgrupa = dr;

                            List<string> lstEstados = lstBruto.Where(e => e.Agrupa == dr).Select(o => o.Estado).Distinct().ToList();
                            lstEstados = lstEstados.OrderBy(x => x).ToList();
                            Dimol.dto.Indicadores objInd = new Dimol.dto.Indicadores();
                            Funciones.TraeDolarUFHoy(obj.Codemp, objInd);
                            monto = 0;
                            regularizado = 0;
                            compromiso = 0;
                            saldo = 0;
                            sumaMonto = 0;
                            sumaRegularizado = 0;
                            sumaCompromiso = 0;
                            sumaSaldo = 0;
                            foreach (string teb in lstEstados)
                            {

                                deudores = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb).Select(p => p.Ctcid).Distinct().Count();
                                //deudores = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb).Select(p => p.Ctcid).Except(lstBruto.Where(x => x.Agrupa < dr).Select(p => p.Ctcid).Distinct()).Distinct().Count();
                                documentos = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb).Select(p => p.Ccbid).Count();
                                monto = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 1).Select(p => p.Asignado).Sum();
                                monto += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 2).Select(p => p.Asignado * objInd.UF).Sum();
                                monto += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 3).Select(p => p.Asignado * objInd.DolarObservado).Sum();
                                sumaMonto += monto;
                                regularizado = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 1).Select(p => p.Asignado - p.Saldo).Sum();
                                regularizado += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 2).Select(p => (p.Asignado - p.Saldo) * objInd.UF).Sum();
                                regularizado += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 3).Select(p => (p.Asignado - p.Saldo) * objInd.DolarObservado).Sum();
                                sumaRegularizado += regularizado;
                                compromiso = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 1).Select(p => p.Compromiso).Sum();
                                compromiso += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 2).Select(p => (p.Compromiso) * objInd.UF).Sum();
                                compromiso += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 3).Select(p => (p.Compromiso) * objInd.DolarObservado).Sum();
                                sumaCompromiso += compromiso;
                                saldo = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 1).Select(p => p.Saldo).Sum();
                                saldo += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 2).Select(p => (p.Saldo) * objInd.UF).Sum();
                                saldo += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 3).Select(p => (p.Saldo) * objInd.DolarObservado).Sum();
                                sumaSaldo += saldo;
                                objTC.lstEstados.Add(new dto.TortaEstado()
                                {
                                    Estado = teb,
                                    Deudores = deudores,
                                    Documentos = documentos,
                                    Monto = monto,
                                    Regularizado = regularizado,
                                    Compromiso = compromiso,
                                    Saldo = saldo
                                });



                                //totalDeudores += deudores;
                                //totalDeudores += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb).Select(p => p.Ctcid).Except(lstBruto.Where(x => x.Agrupa < dr).Select(p => p.Ctcid).Distinct()).Distinct().Count();
                                totalDocumentos += documentos;
                            }
                            objTC.SubTotal.Monto = sumaMonto;
                            /* foreach (var porc in objTC.lstEstados)
                             {
                                 if (objTC.SubTotal.Monto != 0)
                                 {
                                     porc.Regularizado = (porc.Monto / objTC.SubTotal.Monto) * 100;
                                 }
                                 else
                                 {
                                     porc.Regularizado = 0;
                                 }

                             }
                             objTC.lstEstados = objTC.lstEstados.OrderByDescending(x => x.Regularizado).ToList();*/
                            objTC.SubTotal.Regularizado = sumaRegularizado;
                            objTC.SubTotal.Compromiso = sumaCompromiso;
                            objTC.SubTotal.Saldo = sumaSaldo;
                            totalMonto += sumaMonto;
                            totalRegularizado += sumaRegularizado;
                            totalCompromiso += sumaCompromiso;
                            totalSaldo += sumaSaldo;
                            obj.lstDocumentos.Add(objTC);
                           
                        }


                    }
                    obj.lstDocumentos = obj.lstDocumentos.OrderBy(x => x.IdAgrupa).ToList();
                    obj.Totales.Monto = totalMonto;
                    obj.Totales.Regularizado = totalRegularizado;
                    obj.Totales.Compromiso = totalCompromiso;
                    obj.Totales.Saldo = totalSaldo;



                    foreach(var tot in obj.lstDocumentos)
                    {
                        sumaRegularizado = 0;

                        foreach (var est in tot.lstEstados){

                            if (obj.Totales.Saldo != 0)
                            {
                                est.Regularizado = (est.Saldo / obj.Totales.Saldo) * 100;
                                sumaRegularizado += est.Regularizado;
                            }
                            else
                            {
                                est.Regularizado = 0;
                            }
                        }

                        tot.SubTotal.Regularizado = sumaRegularizado;
                        tot.lstEstados = tot.lstEstados.OrderByDescending(x => x.Regularizado).ToList();



                    }
                    //obj.Totales.Deudores = totalDeudores;
                    obj.Totales.Deudores = lstBruto.Select(p => p.Ctcid).Distinct().Count();
                    obj.Totales.Documentos = totalDocumentos;

                    //dto.TortaRanking objTR = new dto.TortaRanking();

                    var rnk = lstBruto.GroupBy(o => o.Ctcid).Select(r => new { Ctcid = r.Key, Saldo = r.Sum(rk => rk.Saldo), Docs = r.Count() }).OrderByDescending(r=>r.Saldo).Take(25);


                    foreach (var rk in rnk)
                    {

                        //dto.TortaCliente objTC = new dto.TortaCliente();
                        dto.TortaRanking objTR = new dto.TortaRanking();

                        objTR.Ctcid = rk.Ctcid;
                        objTR.Documentos = rk.Docs;
                        objTR.Saldo = rk.Saldo;
                        if (obj.Totales.Saldo != 0)
                        { 
                            objTR.PorcSaldo = (rk.Saldo / obj.Totales.Saldo)*100;
                        }
                        else
                        {
                            objTR.PorcSaldo = 0;
                        }

                        try
                        {
                            DataSet dst = new DataSet();
                            StoredProcedure spr = new StoredProcedure("_rpt_Trae_Deudores_Id");
                            spr.AgregarParametro("codemp", obj.Codemp);
                            spr.AgregarParametro("ctcid", objTR.Ctcid);
                            dst = spr.EjecutarProcedimiento();

                            if (dst.Tables.Count > 0)
                            {
                                for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                                {
                                    objTR.Rut = Dimol.bcp.Funciones.formatearRut(dst.Tables[0].Rows[i]["CTC_RUT"].ToString());
                                    objTR.Nombre = dst.Tables[0].Rows[i]["CTC_NOMBRE"].ToString();                                   
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        //obj.lstDocumentos.Add(objTC);
                        obj.lstRanking.Add(objTR);


                    }

                }
                obj.NombreArchivo = "";
                obj.NombreCliente = "";
                obj.NombreUsuario = "";
                obj.RutCliente = "";
                obj.RutUsuario = "";

                /*
                obj.Porcentajes.Add(new dto.TortaPorcentaje()
                {
                    Titulo = "1.- TOTAL CARTERA",
                    Porcentaje = 0,
                    Monto = obj.Totales.Monto
                });
                if (obj.lstDocumentos.Find(x => x.IdAgrupa == 1) != null)
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "2.- TOTAL SIN GESTIONAR",
                        Porcentaje = (obj.lstDocumentos.Find(x => x.IdAgrupa == 1).SubTotal.Monto / obj.Totales.Monto) * 100,
                        Monto = obj.lstDocumentos.Find(x => x.IdAgrupa == 1).SubTotal.Monto
                    });
                }
                else
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "2.- TOTAL SIN GESTIONAR",
                        Porcentaje =0,
                        Monto = 0
                    });
                }
                if (obj.lstDocumentos.Find(x => x.IdAgrupa == 2) != null)
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "3.- TOTAL BUENOS",
                        Porcentaje = (obj.lstDocumentos.Find(x => x.IdAgrupa == 2).SubTotal.Monto / obj.Totales.Monto) * 100,
                        Monto = obj.lstDocumentos.Find(x => x.IdAgrupa == 2).SubTotal.Monto
                    });
                }
                else
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "3.- TOTAL BUENOS",
                        Porcentaje = 0,
                        Monto =0
                    });
                }
                if (obj.lstDocumentos.Find(x => x.IdAgrupa == 3) != null)
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "4.- TOTAL REGULARES",
                        Porcentaje = (obj.lstDocumentos.Find(x => x.IdAgrupa == 3).SubTotal.Monto / obj.Totales.Monto) *100,
                        Monto = obj.lstDocumentos.Find(x => x.IdAgrupa == 3).SubTotal.Monto
                    });
                }
                else
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "4.- TOTAL REGULARES",
                        Porcentaje = 0,
                        Monto = 0
                    });
                }
                if (obj.lstDocumentos.Find(x => x.IdAgrupa == 4) != null)
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "5.- TOTAL MALOS",
                        Porcentaje = (obj.lstDocumentos.Find(x => x.IdAgrupa == 4).SubTotal.Monto / obj.Totales.Monto) * 100,
                        Monto = obj.lstDocumentos.Find(x => x.IdAgrupa == 4).SubTotal.Monto
                    });
                }
                else
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "5.- TOTAL MALOS",
                        Porcentaje = 0,
                        Monto = 0
                    });
                }
                if (obj.lstDocumentos.Find(x => x.IdAgrupa == 5) != null)
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "6.- TOTAL PAGADO",
                        Porcentaje = (obj.lstDocumentos.Find(x => x.IdAgrupa == 5).SubTotal.Monto / obj.Totales.Monto) * 100,
                        Monto = obj.lstDocumentos.Find(x => x.IdAgrupa == 5).SubTotal.Monto
                    });
                }
                else
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "6.- TOTAL PAGADO",
                        Porcentaje = 0,
                        Monto = 0
                    });
                }
                if (obj.lstDocumentos.Find(x => x.IdAgrupa == 6) != null)
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                        {
                            Titulo = "7.- TOTAL CASTIGOS",
                            Porcentaje = (obj.lstDocumentos.Find(x => x.IdAgrupa == 6).SubTotal.Monto / obj.Totales.Monto) * 100,
                            Monto = obj.lstDocumentos.Find(x => x.IdAgrupa == 6).SubTotal.Monto
                        });
                }
                else
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "7.- TOTAL CASTIGOS",
                        Porcentaje = 0,
                        Monto = 0
                    });
                }
                if (obj.lstDocumentos.Find(x => x.IdAgrupa == 7) != null)
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                        {
                            Titulo = "8.- TOTAL REGULARIZADO CLIENTE",
                            Porcentaje = (obj.lstDocumentos.Find(x => x.IdAgrupa == 7).SubTotal.Monto / obj.Totales.Monto) * 100,
                            Monto = obj.lstDocumentos.Find(x => x.IdAgrupa == 7).SubTotal.Monto
                        });
                }
                else
                {
                    obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "8.- TOTAL REGULARIZADO CLIENTE",
                        Porcentaje = 0,
                        Monto = 0
                    });
                }
                obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "9.- TOTAL REGULARIZACION",
                        Porcentaje = ((obj.Porcentajes.Find(x => x.Titulo == "8.- TOTAL REGULARIZADO CLIENTE").Monto + obj.Porcentajes.Find(x => x.Titulo == "6.- TOTAL PAGADO").Monto + obj.Porcentajes.Find(x => x.Titulo == "7.- TOTAL CASTIGOS").Monto) / obj.Totales.Monto) * 100,
                        Monto = (obj.Porcentajes.Find(x => x.Titulo == "8.- TOTAL REGULARIZADO CLIENTE").Monto + obj.Porcentajes.Find(x => x.Titulo == "6.- TOTAL PAGADO").Monto + obj.Porcentajes.Find(x => x.Titulo == "7.- TOTAL CASTIGOS").Monto)
                    
                    });
                obj.Porcentajes.Add(new dto.TortaPorcentaje()
                    {
                        Titulo = "TOTAL SALDO CARTERA (1-9)",
                        Porcentaje = ((obj.Totales.Monto - (obj.Porcentajes.Find(x => x.Titulo == "8.- TOTAL REGULARIZADO CLIENTE").Monto + obj.Porcentajes.Find(x => x.Titulo == "6.- TOTAL PAGADO").Monto + obj.Porcentajes.Find(x => x.Titulo == "7.- TOTAL CASTIGOS").Monto)) / obj.Totales.Monto) * 100,
                        Monto = obj.Totales.Monto - (obj.Porcentajes.Find(x => x.Titulo == "8.- TOTAL REGULARIZADO CLIENTE").Monto + obj.Porcentajes.Find(x => x.Titulo == "6.- TOTAL PAGADO").Monto + obj.Porcentajes.Find(x => x.Titulo == "7.- TOTAL CASTIGOS").Monto)
                    }); */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarRankingDetalle(dto.TortaAgrupada obj)
        {
            try
            {
                string agrupa = "";
                int deudores = 0;
                int documentos = 0;
                decimal monto = 0;
                decimal regularizado = 0;
                decimal compromiso = 0;
                decimal saldo = 0;
                decimal sumaMonto = 0;
                decimal sumaRegularizado = 0;
                decimal sumaCompromiso = 0;
                decimal sumaSaldo = 0;
                decimal totalMonto = 0;
                decimal totalRegularizado = 0;
                decimal totalCompromiso = 0;
                decimal totalSaldo = 0;
                int totalDeudores = 0;
                int totalDocumentos = 0;
                List<dto.TortaEstadoBruto> lstBruto = new List<dto.TortaEstadoBruto>();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Torta_Ranking");
                sp.AgregarParametro("ccb_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("ect_prejud", obj.EstadoCpbt);
                sp.AgregarParametro("idi_idid", obj.Idioma);
                if (obj.RutBusca == null)
                {
                    sp.AgregarParametro("rut_cli", DBNull.Value);
                }
                else
                {
                    sp.AgregarParametro("rut_cli", obj.RutBusca);
                }

                sp.AgregarParametro("cod_ges", obj.CodGestor);

                if (obj.EstadosCartera == null)
                {
                    sp.AgregarParametro("estid", DBNull.Value);
                }
                else
                {
                    sp.AgregarParametro("estid", obj.EstadosCartera);
                }
                

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            lstBruto.Add(new dto.TortaEstadoBruto()
                            {
                                Agrupa = Int32.Parse(dr["ect_agrupa"].ToString()),
                                Asignado = decimal.Parse(dr["ccb_asignado"].ToString()),
                                Ccbid = Int32.Parse(dr["ccb_ccbid"].ToString()),
                                CodigoMoneda = Int32.Parse(dr["ccb_codmon"].ToString()),
                                Compromiso = decimal.Parse(dr["ccb_compromiso"].ToString()),
                                Ctcid = Int32.Parse(dr["ccb_ctcid"].ToString()),
                                Estado = dr["eci_nombre"].ToString(),
                                EstadoCpbt = dr["ccb_estcpbt"].ToString(),
                                Monto = decimal.Parse(dr["ccb_monto"].ToString()),
                                Nombre = dr["pcl_nombre"].ToString(),
                                NombreMoneda = dr["mon_nombre"].ToString(),
                                Pclid = Int32.Parse(dr["ccb_pclid"].ToString()),
                                Prejudicial = dr["ect_prejud"].ToString(),
                                Rut = dr["pcl_rut"].ToString(),
                                Saldo = decimal.Parse(dr["ccb_saldo"].ToString()),
                                TipoCambio = decimal.Parse(dr["ccb_tipcambio"].ToString()),
                                TipoCartera = Int32.Parse(dr["ccb_tipcart"].ToString()),
                                TotalDeuda = decimal.Parse(dr["TotDeu"].ToString())

                            });

                        }

                        List<int> agrupaEstado = lstBruto.Select(o => o.Agrupa).Distinct().ToList();

                        foreach (int dr in agrupaEstado)
                        {
                            switch (dr)
                            {
                                case 1:
                                    agrupa = "NUEVA ASIGNACION";
                                    break;
                                case 2:
                                    agrupa = "BUENOS";
                                    break;
                                case 3:
                                    agrupa = "REGULARES";
                                    break;
                                case 4:
                                    agrupa = "MALOS";
                                    break;
                                /*case 5:
                                    agrupa = "FINALIZADO";
                                    break;
                                case 6:
                                    agrupa = "CASTIGOS";
                                    break;
                                case 7:
                                    agrupa = "FINALIZADO POR CLIENTE";
                                    break;*/
                                default:
                                    agrupa = "OTRO";
                                    break;
                            }
                            dto.TortaCliente objTC = new dto.TortaCliente();
                            objTC.Agrupa = agrupa;
                            objTC.IdAgrupa = dr;

                            List<string> lstEstados = lstBruto.Where(e => e.Agrupa == dr).Select(o => o.Estado).Distinct().ToList();
                            lstEstados = lstEstados.OrderBy(x => x).ToList();
                            Dimol.dto.Indicadores objInd = new Dimol.dto.Indicadores();
                            Funciones.TraeDolarUFHoy(obj.Codemp, objInd);
                            monto = 0;
                            regularizado = 0;
                            compromiso = 0;
                            saldo = 0;
                            sumaMonto = 0;
                            sumaRegularizado = 0;
                            sumaCompromiso = 0;
                            sumaSaldo = 0;
                            foreach (string teb in lstEstados)
                            {                                
                                deudores = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb).Select(p => p.Ctcid).Except(lstBruto.Where(x => x.Agrupa < dr).Select(p => p.Ctcid).Distinct()).Distinct().Count();
                                documentos = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb).Select(p => p.Ccbid).Count();
                                monto = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 1).Select(p => p.Asignado).Sum();
                                monto += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 2).Select(p => p.Asignado * objInd.UF).Sum();
                                monto += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 3).Select(p => p.Asignado * objInd.DolarObservado).Sum();
                                sumaMonto += monto;
                                regularizado = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 1).Select(p => p.Asignado - p.Saldo).Sum();
                                regularizado += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 2).Select(p => (p.Asignado - p.Saldo) * objInd.UF).Sum();
                                regularizado += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 3).Select(p => (p.Asignado - p.Saldo) * objInd.DolarObservado).Sum();
                                sumaRegularizado += regularizado;
                                compromiso = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 1).Select(p => p.Compromiso).Sum();
                                compromiso += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 2).Select(p => (p.Compromiso) * objInd.UF).Sum();
                                compromiso += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 3).Select(p => (p.Compromiso) * objInd.DolarObservado).Sum();
                                sumaCompromiso += compromiso;
                                saldo = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 1).Select(p => p.Saldo).Sum();
                                saldo += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 2).Select(p => (p.Saldo) * objInd.UF).Sum();
                                saldo += lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb && x.CodigoMoneda == 3).Select(p => (p.Saldo) * objInd.DolarObservado).Sum();
                                sumaSaldo += saldo;
                                objTC.lstEstados.Add(new dto.TortaEstado()
                                {
                                    Estado = teb,
                                    Deudores = deudores,
                                    Documentos = documentos,
                                    Monto = monto,
                                    Regularizado = regularizado,
                                    Compromiso = compromiso,
                                    Saldo = saldo
                                });

                                totalDeudores += deudores;
                                totalDocumentos += documentos;
                            }
                            objTC.SubTotal.Monto = sumaMonto;
                            
                            objTC.SubTotal.Regularizado = sumaRegularizado;
                            objTC.SubTotal.Compromiso = sumaCompromiso;
                            objTC.SubTotal.Saldo = sumaSaldo;
                            totalMonto += sumaMonto;
                            totalRegularizado += sumaRegularizado;
                            totalCompromiso += sumaCompromiso;
                            totalSaldo += sumaSaldo;
                            obj.lstDocumentos.Add(objTC);

                        }


                    }
                    obj.lstDocumentos = obj.lstDocumentos.OrderBy(x => x.IdAgrupa).ToList();
                    obj.Totales.Monto = totalMonto;
                    obj.Totales.Regularizado = totalRegularizado;
                    obj.Totales.Compromiso = totalCompromiso;
                    obj.Totales.Saldo = totalSaldo;

                    foreach (var tot in obj.lstDocumentos)
                    {
                        sumaRegularizado = 0;

                        foreach (var est in tot.lstEstados)
                        {

                            if (obj.Totales.Saldo != 0)
                            {
                                est.Regularizado = (est.Saldo / obj.Totales.Saldo) * 100;
                                sumaRegularizado += est.Regularizado;
                            }
                            else
                            {
                                est.Regularizado = 0;
                            }
                        }

                        tot.SubTotal.Regularizado = sumaRegularizado;
                        tot.lstEstados = tot.lstEstados.OrderByDescending(x => x.Regularizado).ToList();



                    }
                    obj.Totales.Deudores = totalDeudores;
                    obj.Totales.Documentos = totalDocumentos;
                    
                    var rnk = lstBruto.GroupBy(o => o.Ctcid).Select(r => new { Ctcid = r.Key, Saldo = r.Sum(rk => rk.Saldo), Docs = r.Count() }).OrderByDescending(r => r.Saldo);


                    foreach (var rk in rnk)
                    {
                        
                        dto.TortaRanking objTR = new dto.TortaRanking();

                        objTR.Ctcid = rk.Ctcid;
                        objTR.Documentos = rk.Docs;
                        objTR.Saldo = rk.Saldo;
                        if (obj.Totales.Saldo != 0)
                        {
                            objTR.PorcSaldo = (rk.Saldo / obj.Totales.Saldo) * 100;
                        }
                        else
                        {
                            objTR.PorcSaldo = 0;
                        }

                        try
                        {
                            DataSet dst = new DataSet();
                            StoredProcedure spr = new StoredProcedure("_rpt_Trae_Deudores_Id");
                            spr.AgregarParametro("codemp", obj.Codemp);
                            spr.AgregarParametro("ctcid", objTR.Ctcid);
                            dst = spr.EjecutarProcedimiento();

                            if (dst.Tables.Count > 0)
                            {
                                for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                                {
                                    objTR.Rut = Dimol.bcp.Funciones.formatearRut(dst.Tables[0].Rows[i]["CTC_RUT"].ToString());
                                    objTR.Nombre = dst.Tables[0].Rows[i]["CTC_NOMBRE"].ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                                                
                        obj.lstRanking.Add(objTR);


                    }

                }
                obj.NombreArchivo = "";
                obj.NombreCliente = "";
                obj.NombreUsuario = "";
                obj.RutCliente = "";
                obj.RutUsuario = "";
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static void ListarDocumentosDetalle(dto.TortaAgrupada obj)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        StoredProcedure sp = new StoredProcedure("Trae_Reporte_Liquidacion_DocVenc");
        //        sp.AgregarParametro("ccb_codemp", obj.Codemp);
        //        sp.AgregarParametro("ccb_pclid", obj.Pclid);
        //        sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
        //        sp.AgregarParametro("ccb_estcpbt ", obj.EstadoCpbt);
        //        sp.AgregarParametro("idioma", obj.Idioma);
        //        sp.AgregarParametro("gsc_sucid", obj.Sucid);

        //        ds = sp.EjecutarProcedimiento();

        //        if (ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {

        //                obj.lstDocumentos.Add(new dto.TortaDeudor
        //                {
        //                    RutDeudor = 0,
        //                    DvDeudor = ds.Tables[0].Rows[0]["ctc_rut"].ToString(),
        //                    RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[0]["ctc_rut"].ToString()),
        //                    NombreFantasia = ds.Tables[0].Rows[0]["ctc_nomfant"].ToString(),
        //                    Ciudad = ds.Tables[0].Rows[0]["ciu_nombre"].ToString(),
        //                    Comuna = ds.Tables[0].Rows[0]["com_nombre"].ToString(),
        //                    Region = ds.Tables[0].Rows[0]["reg_nombre"].ToString(),
        //                    CodigoPostal = ds.Tables[0].Rows[0]["com_codpost"].ToString(),
        //                    Direccion = ds.Tables[0].Rows[0]["ctc_direccion"].ToString(),
        //                    Gestor = ds.Tables[0].Rows[0]["ges_nombre"].ToString(),

        //                    SubCartera = ds.Tables[0].Rows[0]["sbc_nombre"].ToString(),
        //                    RutSubCartera = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[0]["sbc_rut"].ToString())
        //                });

        //                foreach (DataRow detalle in ds.Tables[0].Rows)
        //                {
        //                    switch (detalle["mon_nombre"].ToString())
        //                    {
        //                        case "PESOS":
        //                            obj.lstDocumentos[0].lstDetallesPesos.Add(new dto.TortaDetalle
        //                            {
        //                                TipoDocumento = detalle["tci_nombre"].ToString(),
        //                                Numero = Int32.Parse(detalle["ccb_numero"].ToString()),
        //                                FechaEmision = DateTime.Parse(detalle["ccb_fecdoc"].ToString()),
        //                                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
        //                                DiasVencido = (DateTime.Today - DateTime.Parse(detalle["ccb_fecvenc"].ToString())).Days,
        //                                Monto = decimal.Parse(detalle["ccb_monto"].ToString()),
        //                                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
        //                                Negocio = detalle["ccb_numesp"].ToString(),
        //                                Moneda = detalle["mon_nombre"].ToString()
        //                            });
        //                            break;
        //                        case "DOLAR":
        //                            obj.lstDocumentos[0].lstDetallesDolares.Add(new dto.TortaDetalle
        //                            {
        //                                TipoDocumento = detalle["tci_nombre"].ToString(),
        //                                Numero = Int32.Parse(detalle["ccb_numero"].ToString()),
        //                                FechaEmision = DateTime.Parse(detalle["ccb_fecdoc"].ToString()),
        //                                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
        //                                DiasVencido = (DateTime.Today - DateTime.Parse(detalle["ccb_fecvenc"].ToString())).Days,
        //                                Monto = decimal.Parse(detalle["ccb_monto"].ToString()),
        //                                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
        //                                Negocio = detalle["ccb_numesp"].ToString(),
        //                                Moneda = detalle["mon_nombre"].ToString()
        //                            });
        //                            break;
        //                        case "UF":
        //                            obj.lstDocumentos[0].lstDetallesUF.Add(new dto.TortaDetalle
        //                            {
        //                                TipoDocumento = detalle["tci_nombre"].ToString(),
        //                                Numero = Int32.Parse(detalle["ccb_numero"].ToString()),
        //                                FechaEmision = DateTime.Parse(detalle["ccb_fecdoc"].ToString()),
        //                                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
        //                                DiasVencido = (DateTime.Today - DateTime.Parse(detalle["ccb_fecvenc"].ToString())).Days,
        //                                Monto = decimal.Parse(detalle["ccb_monto"].ToString()),
        //                                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
        //                                Negocio = detalle["ccb_numesp"].ToString(),
        //                                Moneda = detalle["mon_nombre"].ToString()
        //                            });
        //                            break;

        //                    }
        //                }
        //                obj.lstDocumentos[0].TotalesPesos.Monto = (from od in obj.lstDocumentos[0].lstDetallesPesos
        //                                                     select od.Monto).Sum();
        //                obj.lstDocumentos[0].TotalesPesos.Saldo = (from od in obj.lstDocumentos[0].lstDetallesPesos
        //                                                     select od.Saldo).Sum();
        //                obj.lstDocumentos[0].TotalesPesos.CantidadDocumentos = obj.lstDocumentos[0].lstDetallesPesos.Count;

        //                obj.lstDocumentos[0].TotalesDolar.Monto = (from od in obj.lstDocumentos[0].lstDetallesDolares
        //                                                      select od.Monto).Sum();
        //                obj.lstDocumentos[0].TotalesDolar.Saldo = (from od in obj.lstDocumentos[0].lstDetallesDolares
        //                                                      select od.Saldo).Sum();
        //                obj.lstDocumentos[0].TotalesDolar.CantidadDocumentos = obj.lstDocumentos[0].lstDetallesDolares.Count;

        //                obj.lstDocumentos[0].TotalesUF.Monto = (from od in obj.lstDocumentos[0].lstDetallesUF
        //                                                      select od.Monto).Sum();
        //                obj.lstDocumentos[0].TotalesUF.Saldo = (from od in obj.lstDocumentos[0].lstDetallesUF
        //                                                      select od.Saldo).Sum();
        //                obj.lstDocumentos[0].TotalesUF.CantidadDocumentos = obj.lstDocumentos[0].lstDetallesUF.Count;

        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
