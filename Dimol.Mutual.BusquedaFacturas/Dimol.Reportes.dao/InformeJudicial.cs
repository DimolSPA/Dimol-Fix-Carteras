using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class InformeJudicial
    {
        public static void TraeTitulo(dto.InformeJudicial obj)
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
                            RutDeudor = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["RutDeudor"].ToString()),
                            RutCliente = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["RutCliente"].ToString())
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarDocumentos(dto.InformeJudicial obj)
        {
            try
            {
                decimal capital = 0, gasto = 0, total = 0;
                int cantidad = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Informe_Judicial");
                sp.AgregarParametro("rol_codemp", obj.Codemp);
                sp.AgregarParametro("rol_pclid", obj.Pclid);
                sp.AgregarParametro("eci_idid", obj.Idioma);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                        obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");
                        obj.CodigoCarga = ds.Tables[0].Rows[0]["pcc_nombre"].ToString();

                        Indicadores objIndicadores = new Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);
                        obj.CambioDolar = objIndicadores.DolarObservado;
                        obj.CambioUF = objIndicadores.UF;

                        decimal gastoAseg = 0, totalAseg = 0;
                        bool insertaAsegurado = false, insertaCodigoCarga = false, insertaDeudor = false, insertaCausa = false;
                        dto.InformeJudicialCodigoCarga objCodigoCarga = new dto.InformeJudicialCodigoCarga();
                        foreach (DataRow detalle in ds.Tables[0].Rows)
                        {
                            insertaAsegurado = false; 
                            insertaCodigoCarga = false;
                            insertaDeudor = false;
                            insertaCausa = false;
                            gastoAseg = decimal.Parse(detalle["ccb_gastjud"].ToString());
                            totalAseg = decimal.Parse(detalle["ccb_saldo"].ToString()) + gastoAseg;
                            objCodigoCarga = obj.lstCodigoCarga.Find(x => x.CodigoCarga == detalle["pcc_nombre"].ToString());
                            if (objCodigoCarga == null)
                            {
                                objCodigoCarga = new dto.InformeJudicialCodigoCarga() { CodigoCarga = detalle["pcc_nombre"].ToString() };
                                insertaCodigoCarga = true;
                            }
                            dto.InformeJudicialAsegurado objAsegurado = objCodigoCarga.lstAsegurados.Find(x => x.RutSubCartera == detalle["sbc_rut"].ToString());
                            if (objAsegurado == null)
                            {
                                objAsegurado = new dto.InformeJudicialAsegurado()
                                {
                                    SubCartera = detalle["sbc_nombre"].ToString(),
                                    RutSubCartera = detalle["sbc_rut"].ToString()
                                };
                                insertaAsegurado = true;
                            }
                            dto.InformeJudicialDeudor deudor = objAsegurado.lstDeudores.Find(x => x.RutDeudor == detalle["ctc_numero"].ToString());
                            if (deudor == null)
                            {
                                deudor = new dto.InformeJudicialDeudor()
                                {
                                    RutDeudor = detalle["ctc_numero"].ToString(),
                                    DvDeudor = detalle["ctc_digito"].ToString(),
                                    RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(detalle["ctc_numero"].ToString() + detalle["ctc_digito"].ToString()),
                                    NombreFantasia = detalle["ctc_nomfant"].ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"),
                                };
                                insertaDeudor = true;
                            }
                            dto.InformeJudicialCausa causa = deudor.lstCausas.Find(x => x.NumeroRol == detalle["rol_numero"].ToString());
                            if (causa == null)
                            {
                                causa = new dto.InformeJudicialCausa()
                                {
                                    Ciudad = detalle["ciu_nombre"].ToString(),
                                    Region = detalle["reg_nombre"].ToString(),
                                    Causa = detalle["tci_nombre"].ToString(),
                                    Juzgado = detalle["trb_nombre"].ToString(),
                                    NumeroRol = detalle["rol_numero"].ToString(),
                                    Materia = detalle["mji_nombre"].ToString(),
                                    Estado = detalle["eci_nombre"].ToString(),
                                    Resumen = detalle["rle_comentario"].ToString(),
                                    UltimaGestion = DateTime.Parse(detalle["rle_fecha"].ToString())
                                };
                                insertaCausa = true;
                            }

                            switch (detalle["mon_nombre"].ToString())
                            {
                                case "PESOS":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()),
                                        Total = totalAseg,
                                        TipoCambio = 1,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "DOLAR":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.DolarObservado,
                                        TipoCambio = objIndicadores.DolarObservado,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()) ,
                                        Total = totalAseg * objIndicadores.DolarObservado,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "UF":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.UF,
                                        TipoCambio = objIndicadores.UF,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()),
                                        Total = totalAseg * objIndicadores.UF,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;

                            }
                            if (insertaCausa)
                            {
                                deudor.lstCausas.Add(causa);
                            }
                            if (insertaDeudor)
                            {
                                objAsegurado.lstDeudores.Add(deudor);
                            }
                            if (insertaAsegurado)
                            {
                                objCodigoCarga.lstAsegurados.Add(objAsegurado);
                            }
                            if (insertaCodigoCarga)
                            {
                                obj.lstCodigoCarga.Add(objCodigoCarga);  ///fin bucle
                            }
                        }
                        foreach (dto.InformeJudicialCodigoCarga cc in obj.lstCodigoCarga)
                        {
                            foreach(dto.InformeJudicialAsegurado a in cc.lstAsegurados)
                            {
                                foreach (dto.InformeJudicialDeudor d in a.lstDeudores)
                                {
                                    foreach (dto.InformeJudicialCausa c in d.lstCausas)
                                    {
                                        c.TotalesPesos.Capital = (from od in c.lstDetallesPesos
                                                                  select od.Capital).Sum();
                                        c.TotalesPesos.Gasto = (from od in c.lstDetallesPesos
                                                                select od.Gasto).Sum();
                                        c.TotalesPesos.Total = (from od in c.lstDetallesPesos
                                                                select od.Total).Sum();
                                        c.TotalesPesos.Cantidad = c.lstDetallesPesos.Count;

                                        c.Totales.Capital = c.TotalesPesos.Capital;// +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                        c.Totales.Gasto = c.TotalesPesos.Gasto;// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                        c.Totales.Total = c.TotalesPesos.Total;// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                        c.Totales.Cantidad = c.lstDetallesPesos.Count;
                                    }
                                    d.Totales.Capital = (from od in d.lstCausas
                                                         select od.Totales.Capital).Sum();
                                    d.Totales.Gasto = (from od in d.lstCausas
                                                       select od.Totales.Gasto).Sum();
                                    d.Totales.Total = (from od in d.lstCausas
                                                       select od.Totales.Total).Sum();
                                    d.Totales.Cantidad = (from od in d.lstCausas
                                                          select od.Totales.Cantidad).Sum();
                                    d.Totales.NroHijos = d.lstCausas.Count;
                                }
                                a.Totales.Capital = (from od in a.lstDeudores
                                                     select od.Totales.Capital).Sum(); //d.TotalesPesos.Capital; +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                a.Totales.Gasto = (from od in a.lstDeudores
                                                   select od.Totales.Gasto).Sum();// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                a.Totales.Total = (from od in a.lstDeudores
                                                   select od.Totales.Total).Sum();// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                a.Totales.Cantidad = (from od in a.lstDeudores
                                                      select od.Totales.Cantidad).Sum();
                                a.Totales.NroHijos = a.lstDeudores.Count;
                                cantidad += a.Totales.Cantidad;
                                capital += a.Totales.Capital;
                                gasto += a.Totales.Gasto;
                                total += a.Totales.Total;
                            }

                            cc.Totales.Capital = (from od in cc.lstAsegurados
                                                  select od.Totales.Capital).Sum();
                            cc.Totales.Gasto = (from od in cc.lstAsegurados
                                                select od.Totales.Gasto).Sum();
                            cc.Totales.Total = (from od in cc.lstAsegurados
                                                select od.Totales.Total).Sum();
                            cc.Totales.Cantidad = (from od in cc.lstAsegurados
                                                   select od.Totales.Cantidad).Sum();
                            cc.Totales.NroHijos = cc.lstAsegurados.Count;
                        }

                        obj.Totales.Capital = capital;
                        obj.Totales.Gasto = gasto;
                        obj.Totales.Total = total;
                        obj.Totales.Cantidad = cantidad;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarDocumentosInformeJudicial(dto.InformeJudicial obj)
        {
            try
            {
                decimal capital = 0, gasto = 0, total = 0;
                int cantidad = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Informe_Judicial");
                sp.AgregarParametro("rol_codemp", obj.Codemp);
                sp.AgregarParametro("rol_pclid", obj.Pclid);
                sp.AgregarParametro("eci_idid", obj.Idioma);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                        obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");
                        obj.CodigoCarga = ds.Tables[0].Rows[0]["pcc_nombre"].ToString();

                        Indicadores objIndicadores = new Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);
                        obj.CambioDolar = objIndicadores.DolarObservado;
                        obj.CambioUF = objIndicadores.UF;

                        decimal gastoAseg = 0, totalAseg = 0;
                        bool insertaAsegurado = false, insertaCodigoCarga = false, insertaDeudor = false, insertaCausa = false;
                        dto.InformeJudicialCodigoCarga objCodigoCarga = new dto.InformeJudicialCodigoCarga();
                        foreach (DataRow detalle in ds.Tables[0].Rows)
                        {
                            insertaAsegurado = false;
                            insertaCodigoCarga = false;
                            insertaDeudor = false;
                            insertaCausa = false;
                            gastoAseg = decimal.Parse(detalle["ccb_gastjud"].ToString());
                            totalAseg = decimal.Parse(detalle["ccb_saldo"].ToString()) + gastoAseg;
                            objCodigoCarga = obj.lstCodigoCarga.Find(x => x.CodigoCarga == "");
                            if (objCodigoCarga == null)
                            {
                                objCodigoCarga = new dto.InformeJudicialCodigoCarga() { CodigoCarga = "" };
                                insertaCodigoCarga = true;
                            }
                            dto.InformeJudicialAsegurado objAsegurado = objCodigoCarga.lstAsegurados.Find(x => x.RutSubCartera == "");
                            if (objAsegurado == null)
                            {
                                objAsegurado = new dto.InformeJudicialAsegurado()
                                {
                                    SubCartera = "",
                                    RutSubCartera =""
                                };
                                insertaAsegurado = true;
                            }
                            dto.InformeJudicialDeudor deudor = objAsegurado.lstDeudores.Find(x => x.RutDeudor == detalle["ctc_numero"].ToString());
                            if (deudor == null)
                            {
                                deudor = new dto.InformeJudicialDeudor()
                                {
                                    RutDeudor = detalle["ctc_numero"].ToString(),
                                    DvDeudor = detalle["ctc_digito"].ToString(),
                                    RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(detalle["ctc_numero"].ToString() + detalle["ctc_digito"].ToString()),
                                    NombreFantasia = detalle["ctc_nomfant"].ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"),
                                };
                                insertaDeudor = true;
                            }
                            dto.InformeJudicialCausa causa = deudor.lstCausas.Find(x => x.NumeroRol == detalle["rol_numero"].ToString());
                            if (causa == null)
                            {
                                causa = new dto.InformeJudicialCausa()
                                {
                                    Ciudad = detalle["ciu_nombre"].ToString(),
                                    Region = detalle["reg_nombre"].ToString(),
                                    Causa = detalle["tci_nombre"].ToString(),
                                    Juzgado = detalle["trb_nombre"].ToString(),
                                    NumeroRol = detalle["rol_numero"].ToString(),
                                    Materia = detalle["mji_nombre"].ToString(),
                                    Estado = detalle["eci_nombre"].ToString(),
                                    Resumen = detalle["rle_comentario"].ToString(),
                                    UltimaGestion = DateTime.Parse(detalle["rle_fecha"].ToString())
                                };
                                insertaCausa = true;
                            }

                            switch (detalle["mon_nombre"].ToString())
                            {
                                case "PESOS":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()),
                                        Total = totalAseg,
                                        TipoCambio = 1,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "DOLAR":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.DolarObservado,
                                        TipoCambio = objIndicadores.DolarObservado,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()),
                                        Total = totalAseg * objIndicadores.DolarObservado,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "UF":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.UF,
                                        TipoCambio = objIndicadores.UF,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()),
                                        Total = totalAseg * objIndicadores.UF,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;

                            }
                            if (insertaCausa)
                            {
                                deudor.lstCausas.Add(causa);
                            }
                            if (insertaDeudor)
                            {
                                objAsegurado.lstDeudores.Add(deudor);
                            }
                            if (insertaAsegurado)
                            {
                                objCodigoCarga.lstAsegurados.Add(objAsegurado);
                            }
                            if (insertaCodigoCarga)
                            {
                                obj.lstCodigoCarga.Add(objCodigoCarga);  ///fin bucle
                            }
                        }
                        foreach (dto.InformeJudicialCodigoCarga cc in obj.lstCodigoCarga)
                        {
                            foreach (dto.InformeJudicialAsegurado a in cc.lstAsegurados)
                            {
                                foreach (dto.InformeJudicialDeudor d in a.lstDeudores)
                                {
                                    foreach (dto.InformeJudicialCausa c in d.lstCausas)
                                    {
                                        c.TotalesPesos.Capital = (from od in c.lstDetallesPesos
                                                                  select od.Capital).Sum();
                                        c.TotalesPesos.Gasto = (from od in c.lstDetallesPesos
                                                                select od.Gasto).Sum();
                                        c.TotalesPesos.Total = (from od in c.lstDetallesPesos
                                                                select od.Total).Sum();
                                        c.TotalesPesos.Cantidad = c.lstDetallesPesos.Count;

                                        c.Totales.Capital = c.TotalesPesos.Capital;// +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                        c.Totales.Gasto = c.TotalesPesos.Gasto;// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                        c.Totales.Total = c.TotalesPesos.Total;// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                        c.Totales.Cantidad = c.lstDetallesPesos.Count;
                                    }
                                    d.Totales.Capital = (from od in d.lstCausas
                                                         select od.Totales.Capital).Sum();
                                    d.Totales.Gasto = (from od in d.lstCausas
                                                       select od.Totales.Gasto).Sum();
                                    d.Totales.Total = (from od in d.lstCausas
                                                       select od.Totales.Total).Sum();
                                    d.Totales.Cantidad = (from od in d.lstCausas
                                                          select od.Totales.Cantidad).Sum();
                                    d.Totales.NroHijos = d.lstCausas.Count;
                                }
                                a.Totales.Capital = (from od in a.lstDeudores
                                                     select od.Totales.Capital).Sum(); //d.TotalesPesos.Capital; +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                a.Totales.Gasto = (from od in a.lstDeudores
                                                   select od.Totales.Gasto).Sum();// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                a.Totales.Total = (from od in a.lstDeudores
                                                   select od.Totales.Total).Sum();// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                a.Totales.Cantidad = (from od in a.lstDeudores
                                                      select od.Totales.Cantidad).Sum();
                                a.Totales.NroHijos = a.lstDeudores.Count;
                                cantidad += a.Totales.Cantidad;
                                capital += a.Totales.Capital;
                                gasto += a.Totales.Gasto;
                                total += a.Totales.Total;
                            }

                            cc.Totales.Capital = (from od in cc.lstAsegurados
                                                  select od.Totales.Capital).Sum();
                            cc.Totales.Gasto = (from od in cc.lstAsegurados
                                                select od.Totales.Gasto).Sum();
                            cc.Totales.Total = (from od in cc.lstAsegurados
                                                select od.Totales.Total).Sum();
                            cc.Totales.Cantidad = (from od in cc.lstAsegurados
                                                   select od.Totales.Cantidad).Sum();
                            cc.Totales.NroHijos = cc.lstAsegurados.Count;

                        }

                        obj.Totales.Capital = capital;
                        obj.Totales.Gasto = gasto;
                        obj.Totales.Total = total;
                        obj.Totales.Cantidad = cantidad;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarDocumentosCodigoCarga(dto.InformeJudicial obj)
        {
            try
            {
                decimal capital = 0, gasto = 0, total = 0;
                int cantidad = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Informe_Judicial_Codigo_Carga");
                sp.AgregarParametro("rol_codemp", obj.Codemp);
                sp.AgregarParametro("rol_pclid", obj.Pclid);
                sp.AgregarParametro("eci_idid", obj.Idioma);
                sp.AgregarParametro("pcc_codid", obj.Codid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                        obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");
                        obj.CodigoCarga = ds.Tables[0].Rows[0]["pcc_nombre"].ToString();

                        Indicadores objIndicadores = new Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);
                        obj.CambioDolar = objIndicadores.DolarObservado;
                        obj.CambioUF = objIndicadores.UF;

                        decimal gastoAseg = 0, totalAseg = 0;
                        bool insertaAsegurado = false, insertaCodigoCarga = false, insertaDeudor = false, insertaCausa = false;
                        dto.InformeJudicialCodigoCarga objCodigoCarga = new dto.InformeJudicialCodigoCarga();
                        foreach (DataRow detalle in ds.Tables[0].Rows)
                        {
                            insertaAsegurado = false;
                            insertaCodigoCarga = false;
                            insertaDeudor = false;
                            insertaCausa = false;
                            gastoAseg = decimal.Parse(detalle["ccb_gastjud"].ToString());
                            totalAseg = decimal.Parse(detalle["ccb_saldo"].ToString()) + gastoAseg;
                            objCodigoCarga = obj.lstCodigoCarga.Find(x => x.CodigoCarga == detalle["pcc_nombre"].ToString());
                            if (objCodigoCarga == null)
                            {
                                objCodigoCarga = new dto.InformeJudicialCodigoCarga() { CodigoCarga = detalle["pcc_nombre"].ToString() };
                                insertaCodigoCarga = true;
                            }
                            dto.InformeJudicialAsegurado objAsegurado = objCodigoCarga.lstAsegurados.Find(x => x.RutSubCartera == detalle["sbc_rut"].ToString());
                            if (objAsegurado == null)
                            {
                                objAsegurado = new dto.InformeJudicialAsegurado()
                                {
                                    SubCartera = detalle["sbc_nombre"].ToString(),
                                    RutSubCartera = detalle["sbc_rut"].ToString()
                                };
                                insertaAsegurado = true;
                            }
                            dto.InformeJudicialDeudor deudor = objAsegurado.lstDeudores.Find(x => x.RutDeudor == detalle["ctc_numero"].ToString());
                            if (deudor == null)
                            {
                                deudor = new dto.InformeJudicialDeudor()
                                {
                                    RutDeudor = detalle["ctc_numero"].ToString(),
                                    DvDeudor = detalle["ctc_digito"].ToString(),
                                    RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(detalle["ctc_numero"].ToString() + detalle["ctc_digito"].ToString()),
                                    NombreFantasia = detalle["ctc_nomfant"].ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"),
                                };
                                insertaDeudor = true;
                            }
                            dto.InformeJudicialCausa causa = deudor.lstCausas.Find(x => x.NumeroRol == detalle["rol_numero"].ToString());
                            if (causa == null)
                            {
                                causa = new dto.InformeJudicialCausa()
                                {
                                    Ciudad = detalle["ciu_nombre"].ToString(),
                                    Region = detalle["reg_nombre"].ToString(),
                                    Causa = detalle["tci_nombre"].ToString(),
                                    Juzgado = detalle["trb_nombre"].ToString(),
                                    NumeroRol = detalle["rol_numero"].ToString(),
                                    Materia = detalle["mji_nombre"].ToString(),
                                    Estado = detalle["eci_nombre"].ToString(),
                                    Resumen = detalle["rle_comentario"].ToString(),
                                    UltimaGestion = DateTime.Parse(detalle["rle_fecha"].ToString())
                                };
                                insertaCausa = true;
                            }

                            switch (detalle["mon_nombre"].ToString())
                            {
                                case "PESOS":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()),
                                        Total = totalAseg,
                                        TipoCambio = 1,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "DOLAR":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.DolarObservado,
                                        TipoCambio = objIndicadores.DolarObservado,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()) ,
                                        Total = totalAseg * objIndicadores.DolarObservado,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "UF":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.UF,
                                        TipoCambio = objIndicadores.UF,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()) ,
                                        Total = totalAseg * objIndicadores.UF,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;

                            }
                            if (insertaCausa)
                            {
                                deudor.lstCausas.Add(causa);
                            }
                            if (insertaDeudor)
                            {
                                objAsegurado.lstDeudores.Add(deudor);
                            }
                            if (insertaAsegurado)
                            {
                                objCodigoCarga.lstAsegurados.Add(objAsegurado);
                            }
                            if (insertaCodigoCarga)
                            {
                                obj.lstCodigoCarga.Add(objCodigoCarga);  ///fin bucle
                            }
                        }
                        foreach (dto.InformeJudicialCodigoCarga cc in obj.lstCodigoCarga)
                        {
                            foreach (dto.InformeJudicialAsegurado a in cc.lstAsegurados)
                            {
                                foreach (dto.InformeJudicialDeudor d in a.lstDeudores)
                                {
                                    foreach (dto.InformeJudicialCausa c in d.lstCausas)
                                    {
                                        c.TotalesPesos.Capital = (from od in c.lstDetallesPesos
                                                                  select od.Capital).Sum();
                                        c.TotalesPesos.Gasto = (from od in c.lstDetallesPesos
                                                                select od.Gasto).Sum();
                                        c.TotalesPesos.Total = (from od in c.lstDetallesPesos
                                                                select od.Total).Sum();
                                        c.TotalesPesos.Cantidad = c.lstDetallesPesos.Count;

                                        c.Totales.Capital = c.TotalesPesos.Capital;// +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                        c.Totales.Gasto = c.TotalesPesos.Gasto;// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                        c.Totales.Total = c.TotalesPesos.Total;// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                        c.Totales.Cantidad = c.lstDetallesPesos.Count;
                                    }
                                    d.Totales.Capital = (from od in d.lstCausas
                                                         select od.Totales.Capital).Sum();
                                    d.Totales.Gasto = (from od in d.lstCausas
                                                       select od.Totales.Gasto).Sum();
                                    d.Totales.Total = (from od in d.lstCausas
                                                       select od.Totales.Total).Sum();
                                    d.Totales.Cantidad = (from od in d.lstCausas
                                                          select od.Totales.Cantidad).Sum();
                                    d.Totales.NroHijos = d.lstCausas.Count;
                                }
                                a.Totales.Capital = (from od in a.lstDeudores
                                                     select od.Totales.Capital).Sum(); //d.TotalesPesos.Capital; +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                a.Totales.Gasto = (from od in a.lstDeudores
                                                   select od.Totales.Gasto).Sum();// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                a.Totales.Total = (from od in a.lstDeudores
                                                   select od.Totales.Total).Sum();// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                a.Totales.Cantidad = (from od in a.lstDeudores
                                                      select od.Totales.Cantidad).Sum();
                                a.Totales.NroHijos = a.lstDeudores.Count;
                                cantidad += a.Totales.Cantidad;
                                capital += a.Totales.Capital;
                                gasto += a.Totales.Gasto;
                                total += a.Totales.Total;
                            }

                            cc.Totales.Capital = (from od in cc.lstAsegurados
                                                  select od.Totales.Capital).Sum();
                            cc.Totales.Gasto = (from od in cc.lstAsegurados
                                                select od.Totales.Gasto).Sum();
                            cc.Totales.Total = (from od in cc.lstAsegurados
                                                select od.Totales.Total).Sum();
                            cc.Totales.Cantidad = (from od in cc.lstAsegurados
                                                   select od.Totales.Cantidad).Sum();
                            cc.Totales.NroHijos = cc.lstAsegurados.Count;

                        }

                        obj.Totales.Capital = capital;
                        obj.Totales.Gasto = gasto;
                        obj.Totales.Total = total;
                        obj.Totales.Cantidad = cantidad;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarDocumentosIncluyeAsegurado(dto.InformeJudicial obj)
        {
            try
            {
                decimal capital = 0, gasto = 0, total = 0;
                int cantidad = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Informe_Judicial");
                sp.AgregarParametro("rol_codemp", obj.Codemp);
                sp.AgregarParametro("rol_pclid", obj.Pclid);
                sp.AgregarParametro("eci_idid", obj.Idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                        obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");
                        obj.CodigoCarga = ds.Tables[0].Rows[0]["pcc_nombre"].ToString();

                        Indicadores objIndicadores = new Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);
                        obj.CambioDolar = objIndicadores.DolarObservado;
                        obj.CambioUF = objIndicadores.UF;

                        decimal gastoAseg = 0, totalAseg = 0;
                        bool insertaAsegurado = false, insertaCodigoCarga = false, insertaDeudor = false, insertaCausa = false;
                        dto.InformeJudicialCodigoCarga objCodigoCarga = new dto.InformeJudicialCodigoCarga();
                        foreach (DataRow detalle in ds.Tables[0].Rows)
                        {
                            insertaAsegurado = false;
                            insertaCodigoCarga = false;
                            insertaDeudor = false;
                            insertaCausa = false;
                            gastoAseg = decimal.Parse(detalle["ccb_gastjud"].ToString());
                            totalAseg = decimal.Parse(detalle["ccb_saldo"].ToString()) + gastoAseg;
                            objCodigoCarga = obj.lstCodigoCarga.Find(x => x.CodigoCarga == detalle["pcc_nombre"].ToString());
                            if (objCodigoCarga == null)
                            {
                                objCodigoCarga = new dto.InformeJudicialCodigoCarga() { CodigoCarga = detalle["pcc_nombre"].ToString() };
                                insertaCodigoCarga = true;
                            }
                            dto.InformeJudicialDeudor deudor = objCodigoCarga.lstDeudores.Find(x => x.RutDeudor == detalle["ctc_numero"].ToString());
                            if (deudor == null)
                            {
                                deudor = new dto.InformeJudicialDeudor()
                                {
                                    RutDeudor = detalle["ctc_numero"].ToString(),
                                    DvDeudor = detalle["ctc_digito"].ToString(),
                                    RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(detalle["ctc_numero"].ToString() + detalle["ctc_digito"].ToString()),
                                    NombreFantasia = detalle["ctc_nomfant"].ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"),
                                };
                                insertaDeudor = true;
                            }
                            dto.InformeJudicialAsegurado objAsegurado = deudor.lstAsegurados.Find(x => x.RutSubCartera == detalle["sbc_rut"].ToString());
                            if (objAsegurado == null)
                            {
                                objAsegurado = new dto.InformeJudicialAsegurado()
                                {
                                    SubCartera = detalle["sbc_nombre"].ToString(),
                                    RutSubCartera = detalle["sbc_rut"].ToString()
                                };
                                insertaAsegurado = true;
                            }
                            dto.InformeJudicialCausa causa = objAsegurado.lstCausas.Find(x => x.NumeroRol == detalle["rol_numero"].ToString());
                            if (causa == null)
                            {
                                causa = new dto.InformeJudicialCausa()
                                {
                                    Ciudad = detalle["ciu_nombre"].ToString(),
                                    Region = detalle["reg_nombre"].ToString(),
                                    Causa = detalle["tci_nombre"].ToString(),
                                    Juzgado = detalle["trb_nombre"].ToString(),
                                    NumeroRol = detalle["rol_numero"].ToString(),
                                    Materia = detalle["mji_nombre"].ToString(),
                                    Estado = detalle["eci_nombre"].ToString(),
                                    Resumen = detalle["rle_comentario"].ToString(),
                                    UltimaGestion = DateTime.Parse(detalle["rle_fecha"].ToString())
                                };
                                insertaCausa = true;
                            }

                            switch (detalle["mon_nombre"].ToString())
                            {
                                case "PESOS":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()),
                                        Total = totalAseg,
                                        TipoCambio = 1,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "DOLAR":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.DolarObservado,
                                        TipoCambio = objIndicadores.DolarObservado,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()) ,
                                        Total = totalAseg * objIndicadores.DolarObservado,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "UF":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.UF,
                                        TipoCambio = objIndicadores.UF,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()) ,
                                        Total = totalAseg * objIndicadores.UF,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;

                            }
                            if (insertaCausa)
                            {
                                objAsegurado.lstCausas.Add(causa);
                            }
                            if (insertaAsegurado)
                            {
                                deudor.lstAsegurados.Add(objAsegurado);
                            }
                            if (insertaDeudor)
                            {
                                objCodigoCarga.lstDeudores.Add(deudor);
                            }
                            if (insertaCodigoCarga)
                            {
                                obj.lstCodigoCarga.Add(objCodigoCarga);  ///fin bucle
                            }
                        }
                        foreach (dto.InformeJudicialCodigoCarga cc in obj.lstCodigoCarga)
                        {
                            foreach (dto.InformeJudicialDeudor d in cc.lstDeudores)
                            {
                                foreach (dto.InformeJudicialAsegurado a in d.lstAsegurados)
                                {
                                    foreach (dto.InformeJudicialCausa c in a.lstCausas)
                                    {
                                        c.TotalesPesos.Capital = (from od in c.lstDetallesPesos
                                                                  select od.Capital).Sum();
                                        c.TotalesPesos.Gasto = (from od in c.lstDetallesPesos
                                                                select od.Gasto).Sum();
                                        c.TotalesPesos.Total = (from od in c.lstDetallesPesos
                                                                select od.Total).Sum();
                                        c.TotalesPesos.Cantidad = c.lstDetallesPesos.Count;

                                        c.Totales.Capital = c.TotalesPesos.Capital;// +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                        c.Totales.Gasto = c.TotalesPesos.Gasto;// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                        c.Totales.Total = c.TotalesPesos.Total;// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                        c.Totales.Cantidad = c.lstDetallesPesos.Count;
                                    }
                                    a.Totales.Capital = (from od in a.lstCausas
                                                         select od.Totales.Capital).Sum();
                                    a.Totales.Gasto = (from od in a.lstCausas
                                                       select od.Totales.Gasto).Sum();
                                    a.Totales.Total = (from od in a.lstCausas
                                                       select od.Totales.Total).Sum();
                                    a.Totales.Cantidad = (from od in a.lstCausas
                                                          select od.Totales.Cantidad).Sum();
                                    a.Totales.NroHijos = a.lstCausas.Count;
                                }
                                d.Totales.Capital = (from od in d.lstAsegurados
                                                     select od.Totales.Capital).Sum(); //d.TotalesPesos.Capital; +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                d.Totales.Gasto = (from od in d.lstAsegurados
                                                   select od.Totales.Gasto).Sum();// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                d.Totales.Total = (from od in d.lstAsegurados
                                                   select od.Totales.Total).Sum();// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                d.Totales.Cantidad = (from od in d.lstAsegurados
                                                      select od.Totales.Cantidad).Sum();
                                d.Totales.NroHijos = d.lstAsegurados.Count;
                                cantidad += d.Totales.Cantidad;
                                capital += d.Totales.Capital;
                                gasto += d.Totales.Gasto;
                                total += d.Totales.Total;
                            }

                            cc.Totales.Capital = (from od in cc.lstDeudores
                                                  select od.Totales.Capital).Sum();
                            cc.Totales.Gasto = (from od in cc.lstDeudores
                                                select od.Totales.Gasto).Sum();
                            cc.Totales.Total = (from od in cc.lstDeudores
                                                select od.Totales.Total).Sum();
                            cc.Totales.Cantidad = (from od in cc.lstDeudores
                                                   select od.Totales.Cantidad).Sum();
                            cc.Totales.NroHijos = cc.lstDeudores.Count;

                        }

                        obj.Totales.Capital = capital;
                        obj.Totales.Gasto = gasto;
                        obj.Totales.Total = total;
                        obj.Totales.Cantidad = cantidad;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarDocumentosQuiebra(dto.InformeJudicial obj)
        {
            try
            {
                decimal capital = 0, gasto = 0, total = 0;
                int cantidad = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Informe_Judicial_Quiebra");
                sp.AgregarParametro("rol_codemp", obj.Codemp);
                sp.AgregarParametro("rol_pclid", obj.Pclid);
                sp.AgregarParametro("eci_idid", obj.Idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                        obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");
                        obj.CodigoCarga = ds.Tables[0].Rows[0]["pcc_nombre"].ToString();

                        Indicadores objIndicadores = new Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);
                        obj.CambioDolar = objIndicadores.DolarObservado;
                        obj.CambioUF = objIndicadores.UF;

                        decimal gastoAseg = 0, totalAseg = 0;
                        bool insertaAsegurado = false, insertaCodigoCarga = false, insertaDeudor = false, insertaCausa = false;
                        dto.InformeJudicialCodigoCarga objCodigoCarga = new dto.InformeJudicialCodigoCarga();
                        foreach (DataRow detalle in ds.Tables[0].Rows)
                        {
                            insertaAsegurado = false;
                            insertaCodigoCarga = false;
                            insertaDeudor = false;
                            insertaCausa = false;
                            gastoAseg = decimal.Parse(detalle["ccb_gastjud"].ToString());
                            totalAseg = decimal.Parse(detalle["ccb_saldo"].ToString()) + gastoAseg;
                            objCodigoCarga = obj.lstCodigoCarga.Find(x => x.CodigoCarga == detalle["pcc_nombre"].ToString());
                            if (objCodigoCarga == null)
                            {
                                objCodigoCarga = new dto.InformeJudicialCodigoCarga() { CodigoCarga = detalle["pcc_nombre"].ToString() };
                                insertaCodigoCarga = true;
                            }
                            dto.InformeJudicialDeudor deudor = objCodigoCarga.lstDeudores.Find(x => x.RutDeudor == detalle["ctc_numero"].ToString());
                            if (deudor == null)
                            {
                                deudor = new dto.InformeJudicialDeudor()
                                {
                                    RutDeudor = detalle["ctc_numero"].ToString(),
                                    DvDeudor = detalle["ctc_digito"].ToString(),
                                    RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(detalle["ctc_numero"].ToString() + detalle["ctc_digito"].ToString()),
                                    NombreFantasia = detalle["ctc_nomfant"].ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"),
                                };
                                insertaDeudor = true;
                            }
                            dto.InformeJudicialAsegurado objAsegurado = deudor.lstAsegurados.Find(x => x.RutSubCartera == detalle["sbc_rut"].ToString());
                            if (objAsegurado == null)
                            {
                                objAsegurado = new dto.InformeJudicialAsegurado()
                                {
                                    SubCartera = detalle["sbc_nombre"].ToString(),
                                    RutSubCartera = detalle["sbc_rut"].ToString()
                                };
                                insertaAsegurado = true;
                            }
                            dto.InformeJudicialCausa causa = objAsegurado.lstCausas.Find(x => x.NumeroRol == detalle["rol_numero"].ToString());
                            if (causa == null)
                            {
                                causa = new dto.InformeJudicialCausa()
                                {
                                    Ciudad = detalle["ciu_nombre"].ToString(),
                                    Region = detalle["reg_nombre"].ToString(),
                                    Causa = detalle["tci_nombre"].ToString(),
                                    Juzgado = detalle["trb_nombre"].ToString(),
                                    NumeroRol = detalle["rol_numero"].ToString(),
                                    Materia = detalle["mji_nombre"].ToString(),
                                    Estado = detalle["eci_nombre"].ToString(),
                                    Resumen = detalle["rle_comentario"].ToString(),
                                    UltimaGestion = DateTime.Parse(detalle["rle_fecha"].ToString())
                                };
                                insertaCausa = true;
                            }

                            switch (detalle["mon_nombre"].ToString())
                            {
                                case "PESOS":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()),
                                        Total = totalAseg,
                                        TipoCambio = 1,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "DOLAR":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.DolarObservado,
                                        TipoCambio = objIndicadores.DolarObservado,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()) ,
                                        Total = totalAseg * objIndicadores.DolarObservado,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;
                                case "UF":
                                    causa.lstDetallesPesos.Add(new dto.InformeJudicialDetalle
                                    {
                                        TipoDocumento = detalle["tipdoc"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        Capital = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.UF,
                                        TipoCambio = objIndicadores.UF,
                                        Gasto = decimal.Parse(detalle["ccb_gastjud"].ToString()) ,
                                        Total = totalAseg * objIndicadores.UF,
                                        Moneda = detalle["mon_nombre"].ToString()
                                    });
                                    break;

                            }
                            if (insertaCausa)
                            {
                                objAsegurado.lstCausas.Add(causa);
                            }
                            if (insertaAsegurado)
                            {
                                deudor.lstAsegurados.Add(objAsegurado);
                            }
                            if (insertaDeudor)
                            {
                                objCodigoCarga.lstDeudores.Add(deudor);
                            }
                            if (insertaCodigoCarga)
                            {
                                obj.lstCodigoCarga.Add(objCodigoCarga);  ///fin bucle
                            }
                        }
                        foreach (dto.InformeJudicialCodigoCarga cc in obj.lstCodigoCarga)
                        {
                            foreach (dto.InformeJudicialDeudor d in cc.lstDeudores)
                            {
                                foreach (dto.InformeJudicialAsegurado a in d.lstAsegurados)
                                {
                                    foreach (dto.InformeJudicialCausa c in a.lstCausas)
                                    {
                                        c.TotalesPesos.Capital = (from od in c.lstDetallesPesos
                                                                  select od.Capital).Sum();
                                        c.TotalesPesos.Gasto = (from od in c.lstDetallesPesos
                                                                select od.Gasto).Sum();
                                        c.TotalesPesos.Total = (from od in c.lstDetallesPesos
                                                                select od.Total).Sum();
                                        c.TotalesPesos.Cantidad = c.lstDetallesPesos.Count;

                                        c.Totales.Capital = c.TotalesPesos.Capital;// +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                        c.Totales.Gasto = c.TotalesPesos.Gasto;// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                        c.Totales.Total = c.TotalesPesos.Total;// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                        c.Totales.Cantidad = c.lstDetallesPesos.Count;
                                    }
                                    a.Totales.Capital = (from od in a.lstCausas
                                                         select od.Totales.Capital).Sum();
                                    a.Totales.Gasto = (from od in a.lstCausas
                                                       select od.Totales.Gasto).Sum();
                                    a.Totales.Total = (from od in a.lstCausas
                                                       select od.Totales.Total).Sum();
                                    a.Totales.Cantidad = (from od in a.lstCausas
                                                          select od.Totales.Cantidad).Sum();
                                    a.Totales.NroHijos = a.lstCausas.Count;
                                }
                                d.Totales.Capital = (from od in d.lstAsegurados
                                                     select od.Totales.Capital).Sum(); //d.TotalesPesos.Capital; +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                                d.Totales.Gasto = (from od in d.lstAsegurados
                                                   select od.Totales.Gasto).Sum();// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                                d.Totales.Total = (from od in d.lstAsegurados
                                                   select od.Totales.Total).Sum();// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;
                                d.Totales.Cantidad = (from od in d.lstAsegurados
                                                      select od.Totales.Cantidad).Sum();
                                d.Totales.NroHijos = d.lstAsegurados.Count;
                                cantidad += d.Totales.Cantidad;
                                capital += d.Totales.Capital;
                                gasto += d.Totales.Gasto;
                                total += d.Totales.Total;
                            }

                            cc.Totales.Capital = (from od in cc.lstDeudores
                                                  select od.Totales.Capital).Sum();
                            cc.Totales.Gasto = (from od in cc.lstDeudores
                                                select od.Totales.Gasto).Sum();
                            cc.Totales.Total = (from od in cc.lstDeudores
                                                select od.Totales.Total).Sum();
                            cc.Totales.Cantidad = (from od in cc.lstDeudores
                                                   select od.Totales.Cantidad).Sum();
                            cc.Totales.NroHijos = cc.lstDeudores.Count;

                        }

                        obj.Totales.Capital = capital;
                        obj.Totales.Gasto = gasto;
                        obj.Totales.Total = total;
                        obj.Totales.Cantidad = cantidad;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void TraeUltimaGestion(dto.InformePrejudicialDeudor obj, int codemp, int idioma)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Historial_Gestiones_Ultima");
                sp.AgregarParametro("cea_codemp", codemp);
                sp.AgregarParametro("cea_pclid", obj.Pclid);
                sp.AgregarParametro("cea_ctcid", obj.Ctcid);
                sp.AgregarParametro("aci_idid", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Gestion = ds.Tables[0].Rows[0]["aci_nombre"].ToString();
                        obj.UltimaGestion = DateTime.Parse(ds.Tables[0].Rows[0]["Fecha"].ToString());
                        obj.Resumen = ds.Tables[0].Rows[0]["comentario"].ToString();
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
