using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dimol.dao;
using System.Data.SqlClient;
using System.Diagnostics;
using Dimol.dto;

namespace Dimol.Finanzas.dao
{
    public class Comision
    {
        public static List<dto.Comision> ListarComisionesGrilla(int codemp, int codsuc, int anio, int mes, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Comision> lstAcciones = new List<dto.Comision>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comisiones_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("mes", mes);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS DATA:" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        lstAcciones.Add(new dto.Comision()
                        {
                            cms_anio = Int16.Parse(ds.Tables[1].Rows[i]["cms_anio"].ToString()),
                            cms_mes = Int16.Parse(ds.Tables[1].Rows[i]["cms_mes"].ToString()),
                            pcl_nomfant = ds.Tables[1].Rows[i]["pcl_nomfant"].ToString(),
                            tci_nombre = ds.Tables[1].Rows[i]["tci_nombre"].ToString(),
                            ddi_numcta = ds.Tables[1].Rows[i]["ddi_numcta"].ToString(),
                            FecCanc = ds.Tables[1].Rows[i]["FecCanc"].ToString(),
                            Capital = Decimal.Parse(ds.Tables[1].Rows[i]["Capital"].ToString()),
                            Interes = Decimal.Parse(ds.Tables[1].Rows[i]["Interes"].ToString()),
                            Honorario = Decimal.Parse(ds.Tables[1].Rows[i]["Honorario"].ToString()),
                            Total = Decimal.Parse(ds.Tables[1].Rows[i]["Total"].ToString()),
                            PorFact = Decimal.Parse(ds.Tables[1].Rows[i]["PorFact"].ToString()),
                            ctc_rut = ds.Tables[1].Rows[i]["ctc_rut"].ToString(),
                            ctc_nomfant = ds.Tables[1].Rows[i]["ctc_nomfant"].ToString(),
                            ges_nombre = ds.Tables[1].Rows[i]["ges_nombre"].ToString(),
                            ComTotal = Decimal.Parse(ds.Tables[1].Rows[i]["ComTotal"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstAcciones;
        }

        public static int ListarComisionesGrillaCount(int codemp, int codsuc, int anio, int mes, string where, string sidx, string sord)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comisiones_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("mes", mes);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS :" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[1].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<Combobox> ListarAniosComision(int codemp, int codsuc, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Anios_Comision");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["cms_anio"].ToString(),
                            Value = ds.Tables[0].Rows[i]["cms_anio"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static List<Combobox> ListarMesesComision(int idioma, string first)
        {
            //string salida = "";
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first))
            {
                lst.Add(new Combobox()
                {
                    Text = first,
                    Value = ""
                });
            }
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 13; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "Mes" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    lst.Add(new Dimol.dto.Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static int GrabarComision(int codemp, int idioma, int codsuc, string desde, string hasta)
        {
            decimal PcKi = 0;
            decimal PcH = 0;
            decimal PcJKi = 0;
            decimal PcJH = 0;
            decimal totCK = 0;
            decimal totCI = 0;
            decimal totCH = 0;
            decimal ComKi = 0;
            decimal ComH = 0;
            int item = 0;
            decimal cap = 0;
            decimal hon = 0;
            decimal interes = 0;
            decimal gpr = 0;
            decimal gju = 0;
            decimal tipCam = 0;
            double valFac = 0.0;
            int Empresa = 0;
            int Cliente = 0;
            int Deudor = 0;
            int Documento = 0;
            string valFij = "N";
            int vPagDir = 0;
            int gesid = 0;
            try
            {
                DataSet ds = new DataSet();
                DataSet dsGest = new DataSet();
                DataSet dsRem = new DataSet();
                DataSet dsJud = new DataSet();
                DataSet dsGA = new DataSet();
                string anio = desde.Substring(0, 2);
                string mes = desde.Substring(2, 1);

                Debug.WriteLine(" ANIO :" + anio + " MES :" + mes);
                //Elimino las Comisiones
                StoredProcedure spDel = new StoredProcedure("Delete_Comisiones");
                spDel.AgregarParametro("cms_codemp", codemp);
                spDel.AgregarParametro("cms_sucid", codsuc);
                spDel.AgregarParametro("cms_anio", anio);
                spDel.AgregarParametro("cms_mes", mes);

                int err = spDel.EjecutarProcedimientoTrans();
                /*
                Debug.WriteLine("DATOS A INGRESAR :" + " codemp:" + codemp + " codigo:" + obj.Codigo + " nombre:" + obj.Nombre +
                    " arancel:" + obj.Arancel + " porcArancel:" + obj.PorcentajeArancel + " exento:" + obj.Impuesto +
                    " tipo:" + obj.Tipo + " producto final:" + obj.ProductoFinal + " tipoInsumo:" + obj.TiposInsumo +
                    " categoria:" + obj.CategoriaId + " perecible:" + obj.Perecible + " cuenta:" + obj.Cuenta +
                    " pack:" + obj.Pack + " packInterno: " + obj.PackInterno + " supercat: " + obj.SuperCategoriaId +
                    " medida entrada:" + obj.MedidasEntrada + " valo entrada:" + obj.ValorEntrada + " idioma:" + idioma);*/
                StoredProcedure sp = new StoredProcedure("_Listar_CalculoComisiones");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("desde", desde);
                sp.AgregarParametro("hasta", hasta);
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS CALCULO COMISIONES:" + ds.Tables.Count);

                
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        item = item + 1;
                        cap = Decimal.Parse(ds.Tables[1].Rows[i]["api_capital"].ToString());
                        interes = Decimal.Parse(ds.Tables[1].Rows[i]["api_interes"].ToString());
                        hon = Decimal.Parse(ds.Tables[1].Rows[i]["api_honorario"].ToString());
                        gpr = Decimal.Parse(ds.Tables[1].Rows[i]["api_gastpre"].ToString());
                        gju = Decimal.Parse(ds.Tables[1].Rows[i]["api_gastjud"].ToString());

                        valFac = 0;

                        //Reviso el contrato del ciente
                        Empresa = codemp;
                        Cliente = Int32.Parse(ds.Tables[1].Rows[i]["ccb_pclid"].ToString());
                        Deudor = Int32.Parse(ds.Tables[1].Rows[i]["ccb_ctcid"].ToString());
                        Documento = Int32.Parse(ds.Tables[1].Rows[i]["ccb_ccbid"].ToString());

                        //Reviso si es pago directo
                        if(ds.Tables[1].Rows[i]["ddi_pagdir"].ToString().Equals("S")){
                            
                            vPagDir = 1;
                            valFij = "S";
                        }
                            
                        if(ds.Tables[1].Rows[i]["tci_nombre"].ToString().Equals("NOTA DEBITO")){
                            valFac = 0.05;
                        }

                        if(valFij.Equals("S")){
                            valFac = vPagDir;
                        }
                        else{
                            valFac = valFac + vPagDir;
                        }

                        //Busco los % de las comisiones del gestor
                        if (ds.Tables[1].Rows[i]["api_gesid"].ToString() != null)
                        {
                            gesid = Int32.Parse(ds.Tables[1].Rows[i]["api_gesid"].ToString());
                        }
                        else
                        {
                            gesid = 0;
                        }
                        StoredProcedure spGest = new StoredProcedure("_Listar_Gestores");
                        spGest.AgregarParametro("codemp", codemp);
                        spGest.AgregarParametro("codsuc", codsuc);
                        spGest.AgregarParametro("gesid", gesid);
                        dsGest = spGest.EjecutarProcedimiento();

                        if (dsGest.Tables.Count > 0)
                        {
                            PcKi = Decimal.Parse(dsGest.Tables[0].Rows[0]["ges_comki"].ToString());
                            PcH = Decimal.Parse(dsGest.Tables[0].Rows[0]["ges_comhon"].ToString());
                            PcJKi = Decimal.Parse(dsGest.Tables[0].Rows[0]["ges_comJki"].ToString());
                            PcJH = Decimal.Parse(dsGest.Tables[0].Rows[0]["ges_comJhon"].ToString());
                        }
                        else { 
                            
                            PcKi = 0;
                            PcH = 0;
                            PcJKi = 0;
                            PcJH = 0;
                        }

                        totCK = 0;
                        totCI = 0;
                        totCH = 0;

                        //Reviso si esta remesado
                        StoredProcedure spRem = new StoredProcedure("_Listar_Remesas");
                        spRem.AgregarParametro("codemp", codemp);
                        spRem.AgregarParametro("codsuc", codsuc);
                        spRem.AgregarParametro("apl_anio", ds.Tables[0].Rows[i]["apl_anio"].ToString());
                        spRem.AgregarParametro("apl_numapl", ds.Tables[0].Rows[i]["apl_numapl"].ToString());
                        spRem.AgregarParametro("api_item", ds.Tables[0].Rows[i]["api_item"].ToString());
                        dsRem = spRem.EjecutarProcedimiento();

                        if (dsRem.Tables.Count > 0)
                        {
                            valFac = Double.Parse(dsRem.Tables[0].Rows[0][0].ToString());
                        }

                        //COMIENZO A GRABAR
                        //Calculo los totales por el % facturacion
                        tipCam = Decimal.Parse(ds.Tables[0].Rows[i]["ddi_tipcambio"].ToString());
                        totCK = totCK * tipCam;
                        totCI = totCI * tipCam;
                        totCH = totCH * tipCam;
                        totCK = (totCK * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1;
                        totCI = (totCI * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1;
                        totCH = (totCH * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1;

                        //Reviso si el caso esta o no en judicial
                        StoredProcedure spJud = new StoredProcedure("_Listar_CasoJudicial");
                        spJud.AgregarParametro("codemp", codemp);
                        spJud.AgregarParametro("ccb_pclid", ds.Tables[0].Rows[i]["ccb_pclid"].ToString());
                        spJud.AgregarParametro("ccb_ctcid", ds.Tables[0].Rows[i]["ccb_ctcid"].ToString());
                        spJud.AgregarParametro("ccb_ccbid", ds.Tables[0].Rows[i]["ccb_ccbid"].ToString());
                        dsJud = spJud.EjecutarProcedimiento();

                        //Calculo los totales por el % Honorario
                        ComKi = 0;
                        ComH = 0;

                        if(dsJud.Tables.Count > 0){
                            ComKi = totCK * PcJKi;
                            ComH = totCH * PcJH;
                        }
                        else{
                            ComKi = totCK * PcKi;
                            ComH = totCH * PcH;
                        }

                        //Reviso si el gestor tiene algun anexo
                        StoredProcedure spGA = new StoredProcedure("_Listar_GestorAnexo");
                        spGA.AgregarParametro("codemp", codemp);
                        spGA.AgregarParametro("codsuc", codsuc);
                        spGA.AgregarParametro("gesid", gesid);
                        spGA.AgregarParametro("ccb_ctcid", ds.Tables[0].Rows[i]["ccb_ctcid"].ToString());
                        dsGA = spGA.EjecutarProcedimiento();

                        //Inserto la Comision
                        if (dsGA.Tables.Count == 0)
                        {
                            StoredProcedure spIns = new StoredProcedure("Insertar_Comisiones");
                            spIns.AgregarParametro("cms_codemp", codemp);
                            spIns.AgregarParametro("cms_sucid", codsuc);
                            spIns.AgregarParametro("cms_anio", anio);
                            spIns.AgregarParametro("cms_mes", mes);
                            spIns.AgregarParametro("cms_item", item);
                            if (gesid > 0)
                            {
                                spIns.AgregarParametro("cms_gesid", gesid);
                            }
                            else
                            {
                                spIns.AgregarParametro("cms_gesid", DBNull.Value);
                            }
                            spIns.AgregarParametro("cms_vdeid", DBNull.Value);
                            spIns.AgregarParametro("cms_anioapl", ds.Tables[0].Rows[i]["apl_anio"].ToString());
                            spIns.AgregarParametro("cms_numapl", ds.Tables[0].Rows[i]["apl_numapl"].ToString());
                            spIns.AgregarParametro("cms_itemapl", ds.Tables[0].Rows[i]["api_item"].ToString());
                            spIns.AgregarParametro("cms_tpcid", DBNull.Value);
                            spIns.AgregarParametro("cms_numero", DBNull.Value);
                            spIns.AgregarParametro("cms_totcpbt", 0);
                            spIns.AgregarParametro("cms_capital", ((cap) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                            spIns.AgregarParametro("cms_honorario", ((hon) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                            spIns.AgregarParametro("cms_interes", ((interes) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                            spIns.AgregarParametro("cms_gastpre", ((gpr) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                            spIns.AgregarParametro("cms_gastjud", ((gju) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                            spIns.AgregarParametro("cms_porcfcob", valFac);
                            spIns.AgregarParametro("cms_porcki", PcKi);
                            spIns.AgregarParametro("cms_porch", PcH);
                            spIns.AgregarParametro("cms_comki", ComKi);
                            spIns.AgregarParametro("cms_comh", ComH);
                            spIns.AgregarParametro("cms_porcjki", PcJKi);
                            spIns.AgregarParametro("cms_porcjh", PcJH);

                            int error = spIns.EjecutarProcedimientoTrans();
                        }
                        else
                        {
                            for (int g = 0; g < dsGA.Tables[1].Rows.Count; i++)
                            { 
                                PcKi = Decimal.Parse(dsGA.Tables[0].Rows[g]["gsa_porcom"].ToString());
                                PcH = PcKi;

                                ComKi = totCK * PcKi;
                                ComH = totCH * PcH;

                                StoredProcedure spIns = new StoredProcedure("Insertar_Comisiones");
                                spIns.AgregarParametro("cms_codemp", codemp);
                                spIns.AgregarParametro("cms_sucid", codsuc);
                                spIns.AgregarParametro("cms_anio", anio);
                                spIns.AgregarParametro("cms_mes", mes);
                                spIns.AgregarParametro("cms_item", item);
                                if (gesid > 0)
                                {
                                    spIns.AgregarParametro("cms_gesid", gesid);
                                }
                                else
                                {
                                    spIns.AgregarParametro("cms_gesid", DBNull.Value);
                                }
                                spIns.AgregarParametro("cms_vdeid", DBNull.Value);
                                spIns.AgregarParametro("cms_anioapl", ds.Tables[0].Rows[i]["apl_anio"].ToString());
                                spIns.AgregarParametro("cms_numapl", ds.Tables[0].Rows[i]["apl_numapl"].ToString());
                                spIns.AgregarParametro("cms_itemapl", ds.Tables[0].Rows[i]["api_item"].ToString());
                                spIns.AgregarParametro("cms_tpcid", DBNull.Value);
                                spIns.AgregarParametro("cms_numero", DBNull.Value);
                                spIns.AgregarParametro("cms_totcpbt", 0);
                                spIns.AgregarParametro("cms_capital", ((cap) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spIns.AgregarParametro("cms_honorario", ((hon) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spIns.AgregarParametro("cms_interes", ((interes) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spIns.AgregarParametro("cms_gastpre", ((gpr) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spIns.AgregarParametro("cms_gastjud", ((gju) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spIns.AgregarParametro("cms_porcfcob", valFac);
                                spIns.AgregarParametro("cms_porcki", PcKi);
                                spIns.AgregarParametro("cms_porch", PcH);
                                spIns.AgregarParametro("cms_comki", ComKi);
                                spIns.AgregarParametro("cms_comh", ComH);
                                
                                int error = spIns.EjecutarProcedimientoTrans();

                                PcKi = Decimal.Parse(dsGA.Tables[0].Rows[g]["gsa_porcomgp"].ToString());
                                PcH = PcKi;
                                item = item + 1;

                                StoredProcedure spEsp = new StoredProcedure("Insertar_Comisiones");
                                spEsp.AgregarParametro("cms_codemp", codemp);
                                spEsp.AgregarParametro("cms_sucid", codsuc);
                                spEsp.AgregarParametro("cms_anio", anio);
                                spEsp.AgregarParametro("cms_mes", mes);
                                spEsp.AgregarParametro("cms_item", item);
                                if (gesid > 0)
                                {
                                    spEsp.AgregarParametro("cms_gesid", gesid);
                                }
                                else
                                {
                                    spEsp.AgregarParametro("cms_gesid", DBNull.Value);
                                }
                                spEsp.AgregarParametro("cms_vdeid", DBNull.Value);
                                spEsp.AgregarParametro("cms_anioapl", ds.Tables[0].Rows[i]["apl_anio"].ToString());
                                spEsp.AgregarParametro("cms_numapl", ds.Tables[0].Rows[i]["apl_numapl"].ToString());
                                spEsp.AgregarParametro("cms_itemapl", ds.Tables[0].Rows[i]["api_item"].ToString());
                                spEsp.AgregarParametro("cms_tpcid", DBNull.Value);
                                spEsp.AgregarParametro("cms_numero", DBNull.Value);
                                spEsp.AgregarParametro("cms_totcpbt", 0);
                                spEsp.AgregarParametro("cms_capital", ((cap) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spEsp.AgregarParametro("cms_honorario", ((hon) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spEsp.AgregarParametro("cms_interes", ((interes) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spEsp.AgregarParametro("cms_gastpre", ((gpr) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spEsp.AgregarParametro("cms_gastjud", ((gju) * Decimal.Parse(ds.Tables[0].Rows[i]["apl_accion"].ToString())) * -1);
                                spEsp.AgregarParametro("cms_porcfcob", valFac);
                                spEsp.AgregarParametro("cms_porcki", PcKi);
                                spEsp.AgregarParametro("cms_porch", PcH);
                                spEsp.AgregarParametro("cms_comki", ComKi);
                                spEsp.AgregarParametro("cms_comh", ComH);
                                
                                int error2 = spEsp.EjecutarProcedimientoTrans();

                                item = item + 1;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }
    }
}
