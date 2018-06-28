using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class TrekkingCartera
    {
        public static void TraeTitulo(dto.TrekkingCartera obj)
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
        public static void ListarCarteraDetalle(dto.TrekkingCartera obj)
        {
            try
            {
                decimal saldoGral = 0;
                decimal saldo1 = 0;
                decimal saldo2 = 0;
                decimal saldo3 = 0;
                decimal saldo4 = 0;
                decimal act = 0;
                int flag = 0;
                string categoria = "";

                List<dto.TrekkingCarteraBruto> lstBruto = new List<dto.TrekkingCarteraBruto>();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Trekking_Cartera");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("sucid", obj.Sucid);
                sp.AgregarParametro("tipocartera", obj.TipoCartera);
                sp.AgregarParametro("gestor", obj.CodGestor);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstBruto.Add(new dto.TrekkingCarteraBruto
                        {
                            CodGestor = Int32.Parse(ds.Tables[0].Rows[i]["ges_gesid"].ToString()),
                            NombreGestor = ds.Tables[0].Rows[i]["ges_nombre"].ToString(),
                            CodCliente = Int32.Parse(ds.Tables[0].Rows[i]["ccb_pclid"].ToString()),
                            RutCliente = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rut"].ToString()),
                            NombreCliente = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString(),
                            CodDeudor = Int32.Parse(ds.Tables[0].Rows[i]["ctc_ctcid"].ToString()),
                            RutDeudor = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["ctc_rut"].ToString()),
                            NombreDeudor = ds.Tables[0].Rows[i]["ctc_nombre"].ToString(),
                            CodigoMoneda = Int32.Parse(ds.Tables[0].Rows[i]["ccb_codmon"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["ccb_saldo"].ToString()),
                            NumDias = Int32.Parse(ds.Tables[0].Rows[i]["NumDias"].ToString())
                        });
                    }

                    Dimol.dto.Indicadores objInd = new Dimol.dto.Indicadores();
                    Funciones.TraeDolarUFHoy(obj.Codemp, objInd);
                    saldoGral = 0;
                    saldo1 = 0;
                    saldo2 = 0;
                    saldo3 = 0;
                    saldo4 = 0;
                    act = 0;

                    foreach (var gestor in lstBruto.Select(o => new { CodGestor = o.CodGestor, NombreGestor = o.NombreGestor }).Distinct())
                    {
                        dto.TrekkingCarteraGestor objGestor = new dto.TrekkingCarteraGestor();
                        
                        //Crear la consulta con un group by y asignar
                        foreach (var cliente in lstBruto.Where(o => o.CodGestor == gestor.CodGestor).Select(o => new { CodCliente = o.CodCliente, RutCliente = o.RutCliente, NombreCliente = o.NombreCliente }).Distinct())
                        {
                            //dto.TrekkingCarteraCliente objCli = new dto.TrekkingCarteraCliente();

                            saldoGral = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 1).Select(o => o.Saldo).Sum();
                            saldoGral += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 2).Select(o => o.Saldo * objInd.UF).Sum();
                            saldoGral += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 3).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                            saldo1 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 1 && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.Saldo).Sum();
                            saldo1 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 2 && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.Saldo * objInd.UF).Sum();
                            saldo1 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 3 && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                            saldo2 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 1 && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.Saldo).Sum();
                            saldo2 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 2 && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.Saldo * objInd.UF).Sum();
                            saldo2 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 3 && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                            saldo3 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 1 && o.NumDias >= 16 && o.NumDias <= 30).Select(o => o.Saldo).Sum();
                            saldo3 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 2 && o.NumDias >= 16 && o.NumDias <= 30).Select(o => o.Saldo * objInd.UF).Sum();
                            saldo3 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 3 && o.NumDias >= 16 && o.NumDias <= 30).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                            saldo4 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 1 && o.NumDias > 30).Select(o => o.Saldo).Sum();
                            saldo4 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 2 && o.NumDias > 30).Select(o => o.Saldo * objInd.UF).Sum();
                            saldo4 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.CodigoMoneda == 3 && o.NumDias > 30).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                            if(lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente).Select(o => o.CodDeudor).Distinct().Count() == 0)
                            {
                                act = 0;
                            }
                            else
                            {
                                act = ((lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.CodDeudor).Distinct().Count() + lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.CodDeudor).Distinct().Count()) / lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente).Select(o => o.CodDeudor).Distinct().Count()) * 100;
                            }

                            if (act < 60)
                            {
                                flag = 1;
                            }
                            else if(act >= 60 && act <= 80)
                            {
                                flag = 2;
                            }
                            else
                            {
                                flag = 3;
                            }

                            objGestor.lstClientes.Add(new dto.TrekkingCarteraCliente {
                                CodCliente = cliente.CodCliente,
                                RutCliente = cliente.RutCliente,
                                NombreCliente = cliente.NombreCliente,
                                CasosBloque1 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.CodDeudor).Distinct().Count(),
                                SaldoBloque1 = saldo1,
                                CasosBloque2 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.CodDeudor).Distinct().Count(),
                                SaldoBloque2 = saldo2,
                                CasosBloque3 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.NumDias >= 16 && o.NumDias <= 30).Select(o => o.CodDeudor).Distinct().Count(),
                                SaldoBloque3 = saldo3,
                                CasosBloque4 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente && o.NumDias > 30).Select(o => o.CodDeudor).Distinct().Count(),
                                SaldoBloque4 = saldo4,
                                TotalCasos = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodCliente == cliente.CodCliente).Select(o => o.CodDeudor).Distinct().Count(),
                                TotalSaldo = saldoGral,
                                Actualiza = act,
                                Flag = flag
                            });
                                                        
                        }

                        objGestor.CodGestor = gestor.CodGestor;
                        objGestor.NombreGestor = gestor.NombreGestor;

                        saldoGral = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 1).Select(o => o.Saldo).Sum();
                        saldoGral += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 2).Select(o => o.Saldo * objInd.UF).Sum();
                        saldoGral += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 3).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                        saldo1 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 1 && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.Saldo).Sum();
                        saldo1 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 2 && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.Saldo * objInd.UF).Sum();
                        saldo1 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 3 && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                        saldo2 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 1 && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.Saldo).Sum();
                        saldo2 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 2 && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.Saldo * objInd.UF).Sum();
                        saldo2 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 3 && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                        saldo3 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 1 && o.NumDias >= 16 && o.NumDias <= 30).Select(o => o.Saldo).Sum();
                        saldo3 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 2 && o.NumDias >= 16 && o.NumDias <= 30).Select(o => o.Saldo * objInd.UF).Sum();
                        saldo3 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 3 && o.NumDias >= 16 && o.NumDias <= 30).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                        saldo4 = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 1 && o.NumDias > 30).Select(o => o.Saldo).Sum();
                        saldo4 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 2 && o.NumDias > 30).Select(o => o.Saldo * objInd.UF).Sum();
                        saldo4 += lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.CodigoMoneda == 3 && o.NumDias > 30).Select(o => o.Saldo * objInd.DolarObservado).Sum();

                        objGestor.Caso1.NumCasos = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.CodDeudor).Distinct().Count();
                        objGestor.Caso1.Saldo = saldo1;

                        objGestor.Caso2.NumCasos = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.CodDeudor).Distinct().Count();
                        objGestor.Caso2.Saldo = saldo2;

                        objGestor.Caso3.NumCasos = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.NumDias >= 16 && o.NumDias <= 30).Select(o => o.CodDeudor).Distinct().Count();
                        objGestor.Caso3.Saldo = saldo3;

                        objGestor.Caso4.NumCasos = lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.NumDias > 30).Select(o => o.CodDeudor).Distinct().Count();
                        objGestor.Caso4.Saldo = saldo4;

                        objGestor.TotalGestor.NumCasos = lstBruto.Where(o => o.CodGestor == gestor.CodGestor).Select(o => o.CodDeudor).Distinct().Count();
                        objGestor.TotalGestor.Saldo = saldoGral;

                        if(lstBruto.Where(o => o.CodGestor == gestor.CodGestor).Select(o => o.CodDeudor).Distinct().Count() == 0)
                        {
                            objGestor.TotalGestor.PorcCasos = 0;
                        }
                        else
                        {
                            objGestor.TotalGestor.PorcCasos = ((lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.NumDias >= 0 && o.NumDias <= 7).Select(o => o.CodDeudor).Distinct().Count() + lstBruto.Where(o => o.CodGestor == gestor.CodGestor && o.NumDias >= 8 && o.NumDias <= 15).Select(o => o.CodDeudor).Distinct().Count()) / lstBruto.Where(o => o.CodGestor == gestor.CodGestor).Select(o => o.CodDeudor).Distinct().Count()) * 100;
                        }

                        if(saldoGral == 0)
                        {
                            objGestor.TotalGestor.PorcSaldo = 0;
                        }
                        else
                        {
                            objGestor.TotalGestor.PorcSaldo = ((saldo1 + saldo2) / saldoGral) * 100;
                        }
                        
                        obj.lstGestores.Add(objGestor);
                    }
                    
                    if (obj.CodGestor != 0)
                    {
                        dto.TrekkingCarteraRanking objRank = new dto.TrekkingCarteraRanking();

                        foreach (var deudor in lstBruto.Where(o => o.CodGestor == obj.CodGestor).Select(o => new { CodGestor = o.CodGestor, NombreGestor = o.NombreGestor, CodCliente = o.CodCliente, RutCliente = o.RutCliente, NombreCliente = o.NombreCliente, CodDeudor = o.CodDeudor, RutDeudor = o.RutDeudor, NombreDeudor = o.NombreDeudor, NumDias = o.NumDias }).Distinct())
                        {
                            saldoGral = lstBruto.Where(o => o.CodGestor == deudor.CodGestor && o.CodCliente == deudor.CodCliente && o.CodDeudor == deudor.CodDeudor && o.CodigoMoneda == 1).Select(o => o.Saldo).Sum();
                            saldoGral += lstBruto.Where(o => o.CodGestor == deudor.CodGestor && o.CodCliente == deudor.CodCliente && o.CodDeudor == deudor.CodDeudor && o.CodigoMoneda == 2).Select(o => o.Saldo * objInd.UF).Sum();
                            saldoGral += lstBruto.Where(o => o.CodGestor == deudor.CodGestor && o.CodCliente == deudor.CodCliente && o.CodDeudor == deudor.CodDeudor && o.CodigoMoneda == 3).Select(o => o.Saldo * objInd.DolarObservado).Sum();
                                                        
                            if (deudor.NumDias >= 0 && deudor.NumDias <= 7)
                            {
                                categoria = "0 - 7 Dias";
                            }
                            else if(deudor.NumDias >= 8 && deudor.NumDias <= 15)
                            {
                                categoria = "8 - 15 Dias";
                            }
                            else if (deudor.NumDias >= 16 && deudor.NumDias <= 30)
                            {
                                categoria = "16 - 30 Dias";
                            }
                            else if (deudor.NumDias > 30)
                            {
                                categoria = "Sobre 30 Dias";
                            }

                            objRank.lstDeudores.Add(new dto.TrekkingCarteraDeudor {
                                CodDeudor = deudor.CodDeudor,
                                RutDeudor = deudor.RutDeudor,
                                NombreDeudor = deudor.NombreDeudor,
                                Saldo = saldoGral,
                                Categoria = categoria,
                                NumDocs = lstBruto.Where(o => o.CodGestor == deudor.CodGestor && o.CodCliente == deudor.CodCliente && o.CodDeudor == deudor.CodDeudor).Select(o => o.CodDeudor).Count()
                            });

                            objRank.CodGestor = deudor.CodGestor;
                            objRank.NombreGestor = deudor.NombreGestor;
                            objRank.CodCliente = deudor.CodCliente;
                            objRank.RutCliente = deudor.RutCliente;
                            objRank.NombreCliente = deudor.NombreCliente;
                                                        
                        }

                        objRank.lstDeudores = objRank.lstDeudores.OrderByDescending(o => o.Saldo).ToList();

                        obj.lstRanking.Add(objRank);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
