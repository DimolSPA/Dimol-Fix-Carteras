using Dimol;
using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dao
{
    public class TortaAgrupada
    {
        public static string formatearRut(string rut)
        {
            int cont = 0;
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {
                    format = rut.Substring(i, 1) + format;
                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
        }

        public static void ListarDocumentosDetalleTodo(dto.Torta obj)
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
                int totalDocumentos = 0;
                List<dto.TortaEstadoBruto> lstBruto = new List<dto.TortaEstadoBruto>();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Torta_Dinamica");
                sp.AgregarParametro("ccb_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("ccb_estcpbt", (object)obj.EstadoCpbt ?? DBNull.Value);
                sp.AgregarParametro("idi_idid", obj.Idioma);
                sp.AgregarParametro("cod_ges", obj.CodGestor);
                sp.AgregarParametro("cod_carga", obj.CodigoCarga);

                if (obj.DocsVencidos != null)
                {
                    sp.AgregarParametro("dias_vencidos", (int)obj.DocsVencidos);
                } else {
                    sp.AgregarParametro("dias_vencidos", DBNull.Value);
                }

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
                                RutDeudor = dr["ctc_rut"].ToString(),
                                NombreDeudor = dr["ctc_nomfant"].ToString(),
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
                                TotalDeuda = decimal.Parse(dr["TotDeu"].ToString()),
                                Acciones = Int32.Parse(dr["acciones"].ToString()),
                                Historial = Int32.Parse(dr["historial"].ToString())
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
                                    Saldo = saldo,
                                    lstDeudores = lstBruto.Where(x => x.Agrupa == dr && x.Estado == teb).GroupBy(o => new  { o.Ctcid, o.RutDeudor, o.NombreDeudor, o.Acciones, o.Historial }).Select(p => new dto.TortaDeudores() { Ctcid = p.Key.Ctcid, Rut = p.Key.RutDeudor, Nombre = p.Key.NombreDeudor, Acciones = p.Key.Acciones, Historial = p.Key.Historial, Saldo = p.Sum(rk => rk.Saldo) }).OrderByDescending(o=>o.Saldo).ToList()
                                });

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
                    
                    obj.Totales.Deudores = lstBruto.Select(p => p.Ctcid).Distinct().Count();
                    obj.Totales.Documentos = totalDocumentos;

                    var rnk = lstBruto.GroupBy(o => o.Ctcid).Select(r => new { Ctcid = r.Key, Saldo = r.Sum(rk => rk.Saldo), Docs = r.Count() }).OrderByDescending(r=>r.Saldo).Take(25);

                    foreach (var rk in rnk)
                    {
                        dto.TortaRanking objTR = new dto.TortaRanking();
                        objTR.Ctcid = rk.Ctcid;
                        objTR.Documentos = rk.Docs;
                        objTR.Saldo = rk.Saldo;

                        if (obj.Totales.Saldo != 0)
                        { 
                            objTR.PorcSaldo = (rk.Saldo / obj.Totales.Saldo)*100;
                        } else {
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
                                    objTR.Rut = formatearRut(dst.Tables[0].Rows[i]["CTC_RUT"].ToString());
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

        public static void ListarRankingDetalle(dto.Torta obj)
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
                                    objTR.Rut = formatearRut(dst.Tables[0].Rows[i]["CTC_RUT"].ToString());
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

        
    }
}
