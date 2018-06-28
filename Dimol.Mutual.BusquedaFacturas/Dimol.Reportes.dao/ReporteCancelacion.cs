using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Reportes.dto;

namespace Dimol.Reportes.dao
{
    public class ReporteCancelacion
    {
        public static void TraeTitulo(dto.ReporteCancelacion obj)
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

        public static void ListarDocumentosReporteCancelacion(dto.ReporteCancelacion obj)
        {
            try
            {
                decimal capital = 0, gastoPrejudicial = 0, interes = 0, honorarios = 0, gastoJudicial = 0, total = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Cancelaciones_Deudor");
                sp.AgregarParametro("apl_codemp", obj.Codemp);
                sp.AgregarParametro("apl_sucid", obj.Sucid);
                sp.AgregarParametro("api_ctcid", obj.Ctcid);
                sp.AgregarParametro("idi_idid", obj.Idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        obj.RutDeudor = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[0]["ctc_rut"].ToString());
                        obj.NombreDeudor = ds.Tables[0].Rows[0]["ctc_nomfant"].ToString();


                        var lstCliente = ds.Tables[0].AsEnumerable()
                        .Select(row => new
                        {
                            NombreCliente = row.Field<string>("pcl_nomfant")
                        })
                        .Distinct();
                        foreach (var cliente in lstCliente)
                        {
                            CancelacionCliente objCliente= new CancelacionCliente()
                            {
                                NombreCliente = cliente.NombreCliente
                            };

                            var lstFecha = ds.Tables[0].AsEnumerable()
                                 .Select(row => new
                                 {
                                     FechaCancelacion = row.Field<DateTime>("apl_fecapl").Date,
                                     NombreCliente = row.Field<string>("pcl_nomfant")
                                 })
                                 .Distinct();
                            foreach (var fecha in lstFecha)
                            {
                                if (fecha.NombreCliente == cliente.NombreCliente)
                                {
                                    CancelacionClienteFecha objFecha = new CancelacionClienteFecha(){
                                        FechaCancelacion = fecha.FechaCancelacion
                                    };
                                }
                            }
                        }

                        //obj.ListaCliente.Add(

                        //obj.lstDocumentos.Add(new dto.LiquidacionDuraDeudor
                        //{
                        //    RutDeudor = 0,
                        //    DvDeudor = ds.Tables[0].Rows[0]["ctc_rut"].ToString(),
                        //    RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[0]["ctc_rut"].ToString()),
                        //    NombreFantasia = ds.Tables[0].Rows[0]["ctc_nomfant"].ToString(),
                        //    Ciudad = ds.Tables[0].Rows[0]["ciu_nombre"].ToString(),
                        //    Comuna = ds.Tables[0].Rows[0]["com_nombre"].ToString(),
                        //    Region = ds.Tables[0].Rows[0]["reg_nombre"].ToString(),
                        //    CodigoPostal = ds.Tables[0].Rows[0]["com_codpost"].ToString(),
                        //    Direccion = ds.Tables[0].Rows[0]["ctc_direccion"].ToString(),
                        //    Gestor = ds.Tables[0].Rows[0]["ges_nombre"].ToString(),
                        //    CambioDolar = objIndicadores.DolarObservado,
                        //    CambioUF = objIndicadores.UF
                        //});



                        //foreach (var asegurado in lstAsegurados)
                        //{
                        //    obj.lstDocumentos[0].lstAsegurados.Add(new dto.LiquidacionDuraAsegurado
                        //    {
                        //        SubCartera = asegurado.SubCartera ?? "",
                        //        RutSubCartera = asegurado.RutSubCartera == null ? "" : Dimol.bcp.Funciones.formatearRut(asegurado.RutSubCartera)
                        //    });
                        //}
                        //decimal gastoAseg = 0, totalAseg = 0;
                        //foreach (DataRow detalle in ds.Tables[0].Rows)
                        //{
                        //    dto.LiquidacionDuraAsegurado objAsegurado = obj.lstDocumentos[0].lstAsegurados.Find(x => x.SubCartera == detalle["sbc_nombre"].ToString());
                        //    gastoAseg = decimal.Parse(obj.EstadoCpbt == "V" ? detalle["ccb_gastotro"].ToString() : detalle["ccb_gastjud"].ToString());
                        //    totalAseg = decimal.Parse(detalle["ccb_saldo"].ToString()) + decimal.Parse(detalle["ccb_intereses"].ToString()) + decimal.Parse(detalle["ccb_honorarios"].ToString()) + gastoAseg;
                        //    switch (detalle["mon_nombre"].ToString())
                        //    {
                        //        case "PESOS":
                        //            objAsegurado.lstDetallesPesos.Add(new dto.LiquidacionDuraDetalle
                        //            {
                        //                TipoDocumento = detalle["tci_nombre"].ToString(),
                        //                Numero = detalle["ccb_numero"].ToString(),
                        //                FechaEmision = DateTime.Parse(detalle["ccb_fecdoc"].ToString()),
                        //                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                        //                DiasVencido = (DateTime.Today - DateTime.Parse(detalle["ccb_fecvenc"].ToString())).Days,
                        //                Monto = decimal.Parse(detalle["ccb_monto"].ToString()),
                        //                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
                        //                Intereses = decimal.Parse(detalle["ccb_intereses"].ToString()),
                        //                Honorarios = decimal.Parse(detalle["ccb_honorarios"].ToString()),
                        //                Gasto = gastoAseg,
                        //                Total = totalAseg,
                        //                Negocio = detalle["ccb_numesp"].ToString(),
                        //                Moneda = detalle["mon_nombre"].ToString(),
                        //                Estado = detalle["eci_nombre"].ToString(),
                        //                Motivo = detalle["mci_nombre"].ToString()
                        //            });
                        //            break;
                        //        case "DOLAR":
                        //            objAsegurado.lstDetallesDolares.Add(new dto.LiquidacionDuraDetalle
                        //            {
                        //                TipoDocumento = detalle["tci_nombre"].ToString(),
                        //                Numero = detalle["ccb_numero"].ToString(),
                        //                FechaEmision = DateTime.Parse(detalle["ccb_fecdoc"].ToString()),
                        //                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                        //                DiasVencido = (DateTime.Today - DateTime.Parse(detalle["ccb_fecvenc"].ToString())).Days,
                        //                Monto = decimal.Parse(detalle["ccb_monto"].ToString()),
                        //                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
                        //                Intereses = decimal.Parse(detalle["ccb_intereses"].ToString()),
                        //                Honorarios = decimal.Parse(detalle["ccb_honorarios"].ToString()),
                        //                Gasto = gastoAseg,
                        //                Total = totalAseg,
                        //                Negocio = detalle["ccb_numesp"].ToString(),
                        //                Moneda = detalle["mon_nombre"].ToString(),
                        //                Estado = detalle["eci_nombre"].ToString(),
                        //                Motivo = detalle["mci_nombre"].ToString()
                        //            });
                        //            break;
                        //        case "UF":
                        //            objAsegurado.lstDetallesUF.Add(new dto.LiquidacionDuraDetalle
                        //            {
                        //                TipoDocumento = detalle["tci_nombre"].ToString(),
                        //                Numero = detalle["ccb_numero"].ToString(),
                        //                FechaEmision = DateTime.Parse(detalle["ccb_fecdoc"].ToString()),
                        //                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                        //                DiasVencido = (DateTime.Today - DateTime.Parse(detalle["ccb_fecvenc"].ToString())).Days,
                        //                Monto = decimal.Parse(detalle["ccb_monto"].ToString()),
                        //                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
                        //                Intereses = decimal.Parse(detalle["ccb_intereses"].ToString()),
                        //                Honorarios = decimal.Parse(detalle["ccb_honorarios"].ToString()),
                        //                Gasto = gastoAseg,
                        //                Total = totalAseg,
                        //                Negocio = detalle["ccb_numesp"].ToString(),
                        //                Moneda = detalle["mon_nombre"].ToString(),
                        //                Estado = detalle["eci_nombre"].ToString(),
                        //                Motivo = detalle["mci_nombre"].ToString()
                        //            });
                        //            break;

                        //    }
                        //}

                        //foreach (dto.LiquidacionDuraAsegurado aseg in obj.lstDocumentos[0].lstAsegurados)
                        //{
                        //    aseg.TotalesPesos.Monto = (from od in aseg.lstDetallesPesos
                        //                               select od.Monto).Sum();
                        //    aseg.TotalesPesos.Saldo = (from od in aseg.lstDetallesPesos
                        //                               select od.Saldo).Sum();
                        //    aseg.TotalesPesos.Intereses = (from od in aseg.lstDetallesPesos
                        //                                   select od.Intereses).Sum();
                        //    aseg.TotalesPesos.Honorarios = (from od in aseg.lstDetallesPesos
                        //                                    select od.Honorarios).Sum();
                        //    aseg.TotalesPesos.Gasto = (from od in aseg.lstDetallesPesos
                        //                               select od.Gasto).Sum();
                        //    aseg.TotalesPesos.Total = (from od in aseg.lstDetallesPesos
                        //                               select od.Total).Sum();

                        //    aseg.TotalesDolar.Monto = (from od in aseg.lstDetallesDolares
                        //                               select od.Monto).Sum();
                        //    aseg.TotalesDolar.Saldo = (from od in aseg.lstDetallesDolares
                        //                               select od.Saldo).Sum();
                        //    aseg.TotalesDolar.Intereses = (from od in aseg.lstDetallesDolares
                        //                                   select od.Intereses).Sum();
                        //    aseg.TotalesDolar.Honorarios = (from od in aseg.lstDetallesDolares
                        //                                    select od.Honorarios).Sum();
                        //    aseg.TotalesDolar.Gasto = (from od in aseg.lstDetallesDolares
                        //                               select od.Gasto).Sum();
                        //    aseg.TotalesDolar.Total = (from od in aseg.lstDetallesDolares
                        //                               select od.Total).Sum();

                        //    aseg.TotalesUF.Monto = (from od in aseg.lstDetallesUF
                        //                            select od.Monto).Sum();
                        //    aseg.TotalesUF.Saldo = (from od in aseg.lstDetallesUF
                        //                            select od.Saldo).Sum();
                        //    aseg.TotalesUF.Intereses = (from od in aseg.lstDetallesUF
                        //                                select od.Intereses).Sum();
                        //    aseg.TotalesUF.Honorarios = (from od in aseg.lstDetallesUF
                        //                                 select od.Honorarios).Sum();
                        //    aseg.TotalesUF.Gasto = (from od in aseg.lstDetallesUF
                        //                            select od.Gasto).Sum();
                        //    aseg.TotalesUF.Total = (from od in aseg.lstDetallesUF
                        //                            select od.Total).Sum();

                        //    aseg.Totales.Monto = aseg.TotalesPesos.Monto + aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                        //    aseg.Totales.Saldo = aseg.TotalesPesos.Saldo + aseg.TotalesDolar.Saldo * objIndicadores.DolarObservado + aseg.TotalesUF.Saldo * objIndicadores.UF;
                        //    aseg.Totales.Intereses = aseg.TotalesPesos.Intereses + aseg.TotalesDolar.Intereses * objIndicadores.DolarObservado + aseg.TotalesUF.Intereses * objIndicadores.UF;
                        //    aseg.Totales.Honorarios = aseg.TotalesPesos.Honorarios + aseg.TotalesDolar.Honorarios * objIndicadores.DolarObservado + aseg.TotalesUF.Honorarios * objIndicadores.UF;
                        //    aseg.Totales.Gasto = aseg.TotalesPesos.Gasto + aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                        //    aseg.Totales.Total = aseg.TotalesPesos.Total + aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;

                        //    monto += aseg.Totales.Monto;
                        //    saldo += aseg.Totales.Saldo;
                        //    interes += aseg.Totales.Intereses;
                        //    honorarios += aseg.Totales.Honorarios;
                        //    gasto += aseg.Totales.Gasto;
                        //    total += aseg.Totales.Total;
                        //}

                        //obj.Totales.Monto = monto;
                        //obj.Totales.Saldo = saldo;
                        //obj.Totales.Intereses = interes;
                        //obj.Totales.Honorarios = honorarios;
                        //obj.Totales.Gasto = gasto;
                        //if (obj.EstadoCpbt == "V")
                        //{
                        //    obj.Totales.GastoPrejudicial = gasto;
                        //    obj.Totales.GastoJudicial = 0;
                        //}
                        //else
                        //{
                        //    obj.Totales.GastoPrejudicial = 0;
                        //    obj.Totales.GastoJudicial = gasto;
                        //}
                        //obj.Totales.Total = total;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarDocumentosReporteCancelacion(dto.LiquidacionDura obj)
        {
            try
            {
                decimal monto = 0, saldo = 0, interes = 0, honorarios = 0, gasto = 0, total = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Liquidacion_DocVenc");
                sp.AgregarParametro("ccb_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_ctcid", obj.Ctcid);
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("ccb_estcpbt ", obj.EstadoCpbt);
                sp.AgregarParametro("idioma", obj.Idioma);
                sp.AgregarParametro("gsc_sucid", obj.Sucid);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var lstAsegurados = ds.Tables[0].AsEnumerable()
                        .Select(row => new
                        {
                            DvDeudor = row.Field<string>("ctc_rut"),
                            SubCartera = row.Field<string>("sbc_nombre"),
                            RutSubCartera = row.Field<string>("sbc_rut")

                        })
                        .Distinct();
                        Indicadores objIndicadores = new Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);

                        obj.lstDocumentos.Add(new dto.LiquidacionDuraDeudor
                        {
                            RutDeudor = 0,
                            DvDeudor = ds.Tables[0].Rows[0]["ctc_rut"].ToString(),
                            RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[0]["ctc_rut"].ToString()),
                            NombreFantasia = ds.Tables[0].Rows[0]["ctc_nomfant"].ToString(),
                            Ciudad = ds.Tables[0].Rows[0]["ciu_nombre"].ToString(),
                            Comuna = ds.Tables[0].Rows[0]["com_nombre"].ToString(),
                            Region = ds.Tables[0].Rows[0]["reg_nombre"].ToString(),
                            CodigoPostal = ds.Tables[0].Rows[0]["com_codpost"].ToString(),
                            Direccion = ds.Tables[0].Rows[0]["ctc_direccion"].ToString(),
                            Gestor = ds.Tables[0].Rows[0]["ges_nombre"].ToString(),
                            CambioDolar = objIndicadores.DolarObservado,
                            CambioUF = objIndicadores.UF
                        });



                        foreach (var asegurado in lstAsegurados)
                        {
                            obj.lstDocumentos[0].lstAsegurados.Add(new dto.LiquidacionDuraAsegurado
                            {
                                SubCartera = asegurado.SubCartera ?? "",
                                RutSubCartera = asegurado.RutSubCartera == null ? "" : Dimol.bcp.Funciones.formatearRut(asegurado.RutSubCartera)
                            });
                        }
                        decimal gastoAseg = 0, totalAseg = 0;
                        foreach (DataRow detalle in ds.Tables[0].Rows)
                        {
                            dto.LiquidacionDuraAsegurado objAsegurado = obj.lstDocumentos[0].lstAsegurados.Find(x => x.SubCartera == detalle["sbc_nombre"].ToString());
                            gastoAseg = decimal.Parse(obj.EstadoCpbt == "V" ? detalle["ccb_gastotro"].ToString() : detalle["ccb_gastjud"].ToString());
                            totalAseg = decimal.Parse(detalle["ccb_saldo"].ToString()) + decimal.Parse(detalle["ccb_intereses"].ToString()) + decimal.Parse(detalle["ccb_honorarios"].ToString()) + gastoAseg;
                            switch (detalle["mon_nombre"].ToString())
                            {
                                case "PESOS":
                                    objAsegurado.lstDetallesPesos.Add(new dto.LiquidacionDuraDetalle
                                    {
                                        TipoDocumento = detalle["tci_nombre"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaEmision = DateTime.Parse(detalle["ccb_fecdoc"].ToString()),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        DiasVencido = (DateTime.Today - DateTime.Parse(detalle["ccb_fecvenc"].ToString())).Days,
                                        Monto = decimal.Parse(detalle["ccb_monto"].ToString()),
                                        Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                        Intereses = decimal.Parse(detalle["ccb_intereses"].ToString()),
                                        Honorarios = decimal.Parse(detalle["ccb_honorarios"].ToString()),
                                        Gasto = gastoAseg,
                                        Total = totalAseg,
                                        Negocio = detalle["ccb_numesp"].ToString(),
                                        Moneda = detalle["mon_nombre"].ToString(),
                                        Estado = detalle["eci_nombre"].ToString(),
                                        Motivo = detalle["mci_nombre"].ToString()
                                    });
                                    break;
                                case "DOLAR":
                                    objAsegurado.lstDetallesDolares.Add(new dto.LiquidacionDuraDetalle
                                    {
                                        TipoDocumento = detalle["tci_nombre"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaEmision = DateTime.Parse(detalle["ccb_fecdoc"].ToString()),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        DiasVencido = (DateTime.Today - DateTime.Parse(detalle["ccb_fecvenc"].ToString())).Days,
                                        Monto = decimal.Parse(detalle["ccb_monto"].ToString()),
                                        Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                        Intereses = decimal.Parse(detalle["ccb_intereses"].ToString()),
                                        Honorarios = decimal.Parse(detalle["ccb_honorarios"].ToString()),
                                        Gasto = gastoAseg,
                                        Total = totalAseg,
                                        Negocio = detalle["ccb_numesp"].ToString(),
                                        Moneda = detalle["mon_nombre"].ToString(),
                                        Estado = detalle["eci_nombre"].ToString(),
                                        Motivo = detalle["mci_nombre"].ToString()
                                    });
                                    break;
                                case "UF":
                                    objAsegurado.lstDetallesUF.Add(new dto.LiquidacionDuraDetalle
                                    {
                                        TipoDocumento = detalle["tci_nombre"].ToString(),
                                        Numero = detalle["ccb_numero"].ToString(),
                                        FechaEmision = DateTime.Parse(detalle["ccb_fecdoc"].ToString()),
                                        FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                        DiasVencido = (DateTime.Today - DateTime.Parse(detalle["ccb_fecvenc"].ToString())).Days,
                                        Monto = decimal.Parse(detalle["ccb_monto"].ToString()),
                                        Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                        Intereses = decimal.Parse(detalle["ccb_intereses"].ToString()),
                                        Honorarios = decimal.Parse(detalle["ccb_honorarios"].ToString()),
                                        Gasto = gastoAseg,
                                        Total = totalAseg,
                                        Negocio = detalle["ccb_numesp"].ToString(),
                                        Moneda = detalle["mon_nombre"].ToString(),
                                        Estado = detalle["eci_nombre"].ToString(),
                                        Motivo = detalle["mci_nombre"].ToString()
                                    });
                                    break;

                            }
                        }

                        foreach (dto.LiquidacionDuraAsegurado aseg in obj.lstDocumentos[0].lstAsegurados)
                        {
                            aseg.TotalesPesos.Monto = (from od in aseg.lstDetallesPesos
                                                       select od.Monto).Sum();
                            aseg.TotalesPesos.Saldo = (from od in aseg.lstDetallesPesos
                                                       select od.Saldo).Sum();
                            aseg.TotalesPesos.Intereses = (from od in aseg.lstDetallesPesos
                                                           select od.Intereses).Sum();
                            aseg.TotalesPesos.Honorarios = (from od in aseg.lstDetallesPesos
                                                            select od.Honorarios).Sum();
                            aseg.TotalesPesos.Gasto = (from od in aseg.lstDetallesPesos
                                                       select od.Gasto).Sum();
                            aseg.TotalesPesos.Total = (from od in aseg.lstDetallesPesos
                                                       select od.Total).Sum();

                            aseg.TotalesDolar.Monto = (from od in aseg.lstDetallesDolares
                                                       select od.Monto).Sum();
                            aseg.TotalesDolar.Saldo = (from od in aseg.lstDetallesDolares
                                                       select od.Saldo).Sum();
                            aseg.TotalesDolar.Intereses = (from od in aseg.lstDetallesDolares
                                                           select od.Intereses).Sum();
                            aseg.TotalesDolar.Honorarios = (from od in aseg.lstDetallesDolares
                                                            select od.Honorarios).Sum();
                            aseg.TotalesDolar.Gasto = (from od in aseg.lstDetallesDolares
                                                       select od.Gasto).Sum();
                            aseg.TotalesDolar.Total = (from od in aseg.lstDetallesDolares
                                                       select od.Total).Sum();

                            aseg.TotalesUF.Monto = (from od in aseg.lstDetallesUF
                                                    select od.Monto).Sum();
                            aseg.TotalesUF.Saldo = (from od in aseg.lstDetallesUF
                                                    select od.Saldo).Sum();
                            aseg.TotalesUF.Intereses = (from od in aseg.lstDetallesUF
                                                        select od.Intereses).Sum();
                            aseg.TotalesUF.Honorarios = (from od in aseg.lstDetallesUF
                                                         select od.Honorarios).Sum();
                            aseg.TotalesUF.Gasto = (from od in aseg.lstDetallesUF
                                                    select od.Gasto).Sum();
                            aseg.TotalesUF.Total = (from od in aseg.lstDetallesUF
                                                    select od.Total).Sum();

                            aseg.Totales.Monto = aseg.TotalesPesos.Monto + aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
                            aseg.Totales.Saldo = aseg.TotalesPesos.Saldo + aseg.TotalesDolar.Saldo * objIndicadores.DolarObservado + aseg.TotalesUF.Saldo * objIndicadores.UF;
                            aseg.Totales.Intereses = aseg.TotalesPesos.Intereses + aseg.TotalesDolar.Intereses * objIndicadores.DolarObservado + aseg.TotalesUF.Intereses * objIndicadores.UF;
                            aseg.Totales.Honorarios = aseg.TotalesPesos.Honorarios + aseg.TotalesDolar.Honorarios * objIndicadores.DolarObservado + aseg.TotalesUF.Honorarios * objIndicadores.UF;
                            aseg.Totales.Gasto = aseg.TotalesPesos.Gasto + aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
                            aseg.Totales.Total = aseg.TotalesPesos.Total + aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;

                            monto += aseg.Totales.Monto;
                            saldo += aseg.Totales.Saldo;
                            interes += aseg.Totales.Intereses;
                            honorarios += aseg.Totales.Honorarios;
                            gasto += aseg.Totales.Gasto;
                            total += aseg.Totales.Total;
                        }

                        obj.Totales.Monto = monto;
                        obj.Totales.Saldo = saldo;
                        obj.Totales.Intereses = interes;
                        obj.Totales.Honorarios = honorarios;
                        obj.Totales.Gasto = gasto;
                        if (obj.EstadoCpbt == "V")
                        {
                            obj.Totales.GastoPreJudicial = gasto;
                            obj.Totales.GastoJudicial = 0;
                        }
                        else
                        {
                            obj.Totales.GastoPreJudicial = 0;
                            obj.Totales.GastoJudicial = gasto;
                        }
                        obj.Totales.Total = total;
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
