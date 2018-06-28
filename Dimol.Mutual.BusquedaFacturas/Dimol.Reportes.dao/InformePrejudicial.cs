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
    public class InformePrejudicial
    {
        public static void TraeTitulo(dto.InformePrejudicial obj)
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

        public static void ListarDocumentosDetalle(dto.InformePrejudicial obj)
        {
            try
            {
                decimal monto = 0, saldo = 0, interes = 0, honorarios = 0, gasto = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Informe_PreJudicial");
                sp.AgregarParametro("ccb_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("idi_idid", obj.Idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                        obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");
                        obj.CodigoCarga = ds.Tables[0].Rows[0]["pcc_nombre"].ToString();
                        var lstAsegurados = ds.Tables[0].AsEnumerable()
                        .Select(row => new
                        {
                            RutDeudor = row.Field<decimal>("ctc_numero"),
                            DvDeudor = row.Field<string>("ctc_digito"),
                            SubCartera = "",
                            RutSubCartera ="",
                            RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(row.Field<decimal>("ctc_numero").ToString() + row.Field<string>("ctc_digito").ToString()),
                            NombreFantasia = row.Field<string>("ctc_nomfant").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"),
                            Ciudad = row.Field<string>("ciu_nombre"),
                            Comuna = row.Field<string>("com_nombre"),
                            Region = row.Field<string>("reg_nombre"),
                            Pais = row.Field<string>("pai_nombre"),
                            //CodigoPostal = ds.Tables[0].Rows[0]["com_codpost"].ToString(),
                            Direccion = row.Field<string>("ctc_direccion"),
                            Ctcid = row.Field<decimal>("ccb_ctcid"),
                            Pclid = row.Field<decimal>("ccb_pclid"),
                            Negocio = row.Field<string>("ccb_numesp")

                        })
                        .Distinct().ToList();
                        Indicadores objIndicadores = new Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);
                        obj.CambioDolar = objIndicadores.DolarObservado;
                        obj.CambioUF = objIndicadores.UF;
                        bool insertaAsegurado = false, insertaDeudor = false, insertaNegocio = false;
                        foreach (var asegurado in lstAsegurados)
                        {
                            insertaDeudor = false;
                            insertaNegocio = false;
                            dto.InformePrejudicialDeudor deudor = obj.lstDocumentos.Find(x => x.RutDeudor == asegurado.RutDeudor.ToString());// new dto.InformePrejudicialDeudor();
                            if (deudor == null)
                            {
                                deudor = new dto.InformePrejudicialDeudor
                                {
                                    RutDeudor = asegurado.RutDeudor.ToString(),
                                    DvDeudor = asegurado.DvDeudor,
                                    RutDeudorFormateado = asegurado.RutDeudorFormateado,
                                    NombreFantasia = asegurado.NombreFantasia,
                                    Ciudad = asegurado.Ciudad,
                                    Comuna = asegurado.Comuna,
                                    Region = asegurado.Region,
                                    Pais = asegurado.Pais,
                                    Ctcid = asegurado.Ctcid,
                                    Direccion = asegurado.Direccion,
                                    Pclid = asegurado.Pclid
                                };
                                insertaDeudor = true;
                            }

                            dao.InformePrejudicial.TraeUltimaGestion(deudor, obj.Codemp, obj.Idioma);

                            dto.InformePrejudicialNegocio negocio = deudor.lstNegocios.Find(x => x.Negocio == asegurado.Negocio.ToString());// new dto.InformePrejudicialDeudor();
                            if (negocio == null)
                            {
                                negocio = new dto.InformePrejudicialNegocio
                                {
                                    Negocio = asegurado.Negocio.ToString()
                                };
                                insertaNegocio = true;
                            }
                            if (insertaNegocio)
                            {
                                deudor.lstNegocios.Add(negocio);
                            }
                            if (insertaDeudor)
                            {
                                obj.lstDocumentos.Add(deudor);
                            }
                        }
                        foreach (DataRow detalle in ds.Tables[0].Rows)
                        {
                            dto.InformePrejudicialDeudor deudor = obj.lstDocumentos.Find(x => x.RutDeudor == detalle["ctc_numero"].ToString());
                            if (deudor != null)
                            {
                                dto.InformePrejudicialNegocio objNegocio = deudor.lstNegocios.Find(x => x.Negocio == detalle["ccb_numesp"].ToString());
                                if (objNegocio != null)
                                {
                                    switch (detalle["mon_nombre"].ToString())
                                    {
                                        case "PESOS":
                                            objNegocio.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
                                            {
                                                TipoDocumento = detalle["tci_nombre"].ToString(),
                                                Numero = detalle["ccb_numero"].ToString(),
                                                FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
                                                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                                Capital = decimal.Parse(detalle["ccb_monto"].ToString()),
                                                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                                Abono = decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString()),
                                                Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()),
                                                Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()),
                                                //Total = totalAseg,
                                                //Negocio = detalle["ccb_numesp"].ToString(),
                                                Moneda = detalle["mon_nombre"].ToString(),
                                                UltimoEstado = detalle["eci_nombre"].ToString()
                                            });
                                            break;
                                        case "DOLAR":
                                            objNegocio.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
                                            {
                                                TipoDocumento = detalle["tci_nombre"].ToString(),
                                                Numero = detalle["ccb_numero"].ToString(),
                                                FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
                                                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                                Capital = decimal.Parse(detalle["ccb_monto"].ToString()) * objIndicadores.DolarObservado,
                                                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.DolarObservado,
                                                Abono = (decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString())) * objIndicadores.DolarObservado,
                                                Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()) * objIndicadores.DolarObservado,
                                                Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()) * objIndicadores.DolarObservado,
                                                //Total = totalAseg,
                                                //Negocio = detalle["ccb_numesp"].ToString(),
                                                Moneda = detalle["mon_nombre"].ToString(),
                                                UltimoEstado = detalle["eci_nombre"].ToString()
                                            });
                                            break;
                                        case "UF":
                                            objNegocio.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
                                            {
                                                TipoDocumento = detalle["tci_nombre"].ToString(),
                                                Numero = detalle["ccb_numero"].ToString(),
                                                FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
                                                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                                Capital = decimal.Parse(detalle["ccb_monto"].ToString()) * objIndicadores.UF,
                                                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.UF,
                                                Abono = (decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString())) * objIndicadores.UF,
                                                Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()) * objIndicadores.UF,
                                                Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()) * objIndicadores.UF,
                                                //Total = totalAseg,
                                                //Negocio = detalle["ccb_numesp"].ToString(),
                                                Moneda = detalle["mon_nombre"].ToString(),
                                                UltimoEstado = detalle["eci_nombre"].ToString()
                                            });
                                            break;

                                    }
                                }
                                else
                                {
                                    string ddd = detalle["ctc_numero"].ToString();
                                }
                            }
                            else
                            {
                                string e = detalle["ctc_numero"].ToString();
                            }
                        }
                        foreach (dto.InformePrejudicialDeudor d in obj.lstDocumentos)
                        {
                            foreach (dto.InformePrejudicialNegocio neg in d.lstNegocios)
                            {
                                neg.TotalesPesos.Capital = (from od in neg.lstDetallesPesos
                                                            select od.Capital).Sum();
                                neg.TotalesPesos.Saldo = (from od in neg.lstDetallesPesos
                                                          select od.Saldo).Sum();
                                neg.TotalesPesos.Abono = (from od in neg.lstDetallesPesos
                                                          select od.Abono).Sum();
                                neg.TotalesPesos.Asignado = (from od in neg.lstDetallesPesos
                                                             select od.Asignado).Sum();
                                neg.TotalesPesos.Gasto = (from od in neg.lstDetallesPesos
                                                          select od.Gasto).Sum();

                                neg.Totales.Capital = neg.TotalesPesos.Capital;
                                neg.Totales.Saldo = neg.TotalesPesos.Saldo;
                                neg.Totales.Asignado = neg.TotalesPesos.Asignado;
                                neg.Totales.Abono = neg.TotalesPesos.Abono;
                                neg.Totales.Gasto = neg.TotalesPesos.Gasto;

                                monto += neg.Totales.Capital;
                                saldo += neg.Totales.Saldo;
                                interes += neg.Totales.Abono;
                                honorarios += neg.Totales.Asignado;
                                gasto += neg.Totales.Gasto;
                            }
                            d.Totales.Capital = (from od in d.lstNegocios
                                                 select od.Totales.Capital).Sum();
                            d.Totales.Saldo = (from od in d.lstNegocios
                                               select od.Totales.Saldo).Sum();
                            d.Totales.Abono = (from od in d.lstNegocios
                                               select od.Totales.Abono).Sum();
                            d.Totales.Asignado = (from od in d.lstNegocios
                                                  select od.Totales.Asignado).Sum();
                            d.Totales.Gasto = (from od in d.lstNegocios
                                               select od.Totales.Gasto).Sum();
                        }
                        obj.Totales.Capital = monto;
                        obj.Totales.Saldo = saldo;
                        obj.Totales.Abono = interes;
                        obj.Totales.Asignado = honorarios;
                        obj.Totales.Gasto = gasto;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarDocumentosDetalleAsegurado(dto.InformePrejudicial obj)
        {
            try
            {
                decimal monto = 0, saldo = 0, interes = 0, honorarios = 0, gasto = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Informe_PreJudicial");
                sp.AgregarParametro("ccb_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("idi_idid", obj.Idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                        obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");
                        obj.CodigoCarga = ds.Tables[0].Rows[0]["pcc_nombre"].ToString();
                        var lstAsegurados = ds.Tables[0].AsEnumerable()
                        .Select(row => new
                        {
                            RutDeudor = row.Field<decimal>("ctc_numero"),
                            DvDeudor = row.Field<string>("ctc_digito"),
                            SubCartera = row.Field<string>("sbc_nombre") ?? "",
                            RutSubCartera = row.Field<string>("sbc_rut") ?? "",
                            RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(row.Field<decimal>("ctc_numero").ToString() + row.Field<string>("ctc_digito").ToString()),
                            NombreFantasia = row.Field<string>("ctc_nomfant").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"),
                            Ciudad = row.Field<string>("ciu_nombre"),
                            Comuna = row.Field<string>("com_nombre"),
                            Region = row.Field<string>("reg_nombre"),
                            Pais = row.Field<string>("pai_nombre"),
                            //CodigoPostal = ds.Tables[0].Rows[0]["com_codpost"].ToString(),
                            Direccion = row.Field<string>("ctc_direccion"),
                            Ctcid = row.Field<decimal>("ccb_ctcid"),
                            Pclid = row.Field<decimal>("ccb_pclid"),
                            Negocio = row.Field<string>("ccb_numesp")

                        })
                        .Distinct().ToList();
                        Indicadores objIndicadores = new Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);
                        obj.CambioDolar = objIndicadores.DolarObservado;
                        obj.CambioUF = objIndicadores.UF;
                        bool insertaAsegurado = false, insertaDeudor = false, insertaNegocio = false;
                        foreach (var asegurado in lstAsegurados)
                        {
                            insertaDeudor = false;
                            insertaNegocio = false;
                            insertaAsegurado = false;
                            dto.InformePrejudicialAsegurado objAseg = obj.lstAsegurados.Find(x => x.RutSubCartera == asegurado.RutSubCartera.ToString());
                            if (objAseg == null)
                            {
                                objAseg = new dto.InformePrejudicialAsegurado
                                {
                                    SubCartera = asegurado.SubCartera ?? "",
                                    RutSubCartera = asegurado.RutSubCartera == null ? "" : Dimol.bcp.Funciones.formatearRut(asegurado.RutSubCartera)
                                };
                                insertaAsegurado = true;
                            }
                            dto.InformePrejudicialDeudor deudor = objAseg.lstDocumentos.Find(x => x.RutDeudor == asegurado.RutDeudor.ToString());// new dto.InformePrejudicialDeudor();
                            if (deudor == null)
                            {
                                deudor = new dto.InformePrejudicialDeudor
                                {
                                    RutDeudor = asegurado.RutDeudor.ToString(),
                                    DvDeudor = asegurado.DvDeudor,
                                    RutDeudorFormateado = asegurado.RutDeudorFormateado,
                                    NombreFantasia = asegurado.NombreFantasia,
                                    Ciudad = asegurado.Ciudad,
                                    Comuna = asegurado.Comuna,
                                    Region = asegurado.Region,
                                    Pais = asegurado.Pais,
                                    Ctcid = asegurado.Ctcid,
                                    Direccion = asegurado.Direccion,
                                    Pclid = asegurado.Pclid
                                };
                                insertaDeudor = true;
                            }

                            dao.InformePrejudicial.TraeUltimaGestion(deudor, obj.Codemp, obj.Idioma);

                            dto.InformePrejudicialNegocio negocio = deudor.lstNegocios.Find(x => x.Negocio == asegurado.Negocio.ToString());// new dto.InformePrejudicialDeudor();
                            if (negocio == null)
                            {
                                negocio = new dto.InformePrejudicialNegocio
                                {
                                    Negocio = asegurado.Negocio.ToString()
                                };
                                insertaNegocio = true;
                            }
                            if (insertaNegocio)
                            {
                                deudor.lstNegocios.Add(negocio);
                            }
                            if (insertaDeudor)
                            {
                                objAseg.lstDocumentos.Add(deudor);
                            }
                            if (insertaAsegurado)
                            {
                                obj.lstAsegurados.Add(objAseg);
                            }
                        }
                        foreach (DataRow detalle in ds.Tables[0].Rows)
                        {
                            dto.InformePrejudicialAsegurado asegurado = obj.lstAsegurados.Find(x => x.RutSubCartera == Dimol.bcp.Funciones.formatearRut(detalle["sbc_rut"].ToString()));
                            if (asegurado != null)
                            {
                                dto.InformePrejudicialDeudor deudor = asegurado.lstDocumentos.Find(x => x.RutDeudor == detalle["ctc_numero"].ToString());
                                if (deudor != null)
                                {
                                    dto.InformePrejudicialNegocio objNegocio = deudor.lstNegocios.Find(x => x.Negocio == detalle["ccb_numesp"].ToString());
                                    if (objNegocio != null)
                                    {
                                        switch (detalle["mon_nombre"].ToString())
                                        {
                                            case "PESOS":
                                                objNegocio.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
                                                {
                                                    TipoDocumento = detalle["tci_nombre"].ToString(),
                                                    Numero = detalle["ccb_numero"].ToString(),
                                                    FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
                                                    FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                                    Capital = decimal.Parse(detalle["ccb_monto"].ToString()),
                                                    Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                                    Abono = decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString()),
                                                    Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()),
                                                    Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()),
                                                    //Total = totalAseg,
                                                    //Negocio = detalle["ccb_numesp"].ToString(),
                                                    Moneda = detalle["mon_nombre"].ToString(),
                                                    UltimoEstado = detalle["eci_nombre"].ToString()
                                                });
                                                break;
                                            case "DOLAR":
                                                objNegocio.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
                                                {
                                                    TipoDocumento = detalle["tci_nombre"].ToString(),
                                                    Numero = detalle["ccb_numero"].ToString(),
                                                    FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
                                                    FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                                    Capital = decimal.Parse(detalle["ccb_monto"].ToString()) * objIndicadores.DolarObservado,
                                                    Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.DolarObservado,
                                                    Abono = (decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString())) * objIndicadores.DolarObservado,
                                                    Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()) * objIndicadores.DolarObservado,
                                                    Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()) * objIndicadores.DolarObservado,
                                                    //Total = totalAseg,
                                                    //Negocio = detalle["ccb_numesp"].ToString(),
                                                    Moneda = detalle["mon_nombre"].ToString(),
                                                    UltimoEstado = detalle["eci_nombre"].ToString()
                                                });
                                                break;
                                            case "UF":
                                                objNegocio.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
                                                {
                                                    TipoDocumento = detalle["tci_nombre"].ToString(),
                                                    Numero = detalle["ccb_numero"].ToString(),
                                                    FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
                                                    FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                                    Capital = decimal.Parse(detalle["ccb_monto"].ToString()) * objIndicadores.UF,
                                                    Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.UF,
                                                    Abono = (decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString())) * objIndicadores.UF,
                                                    Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()) * objIndicadores.UF,
                                                    Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()) * objIndicadores.UF,
                                                    //Total = totalAseg,
                                                    //Negocio = detalle["ccb_numesp"].ToString(),
                                                    Moneda = detalle["mon_nombre"].ToString(),
                                                    UltimoEstado = detalle["eci_nombre"].ToString()
                                                });
                                                break;

                                        }
                                    }
                                    else
                                    {
                                        string ddd = detalle["ctc_numero"].ToString();
                                    }
                                }
                                else
                                {
                                    string e = detalle["ctc_numero"].ToString();
                                }
                            }
                        }
                        foreach (dto.InformePrejudicialAsegurado a in obj.lstAsegurados)
                        {
                            foreach (dto.InformePrejudicialDeudor d in a.lstDocumentos)
                            {
                                foreach (dto.InformePrejudicialNegocio neg in d.lstNegocios)
                                {
                                    neg.TotalesPesos.Capital = (from od in neg.lstDetallesPesos
                                                                select od.Capital).Sum();
                                    neg.TotalesPesos.Saldo = (from od in neg.lstDetallesPesos
                                                              select od.Saldo).Sum();
                                    neg.TotalesPesos.Abono = (from od in neg.lstDetallesPesos
                                                              select od.Abono).Sum();
                                    neg.TotalesPesos.Asignado = (from od in neg.lstDetallesPesos
                                                                 select od.Asignado).Sum();
                                    neg.TotalesPesos.Gasto = (from od in neg.lstDetallesPesos
                                                              select od.Gasto).Sum();

                                    neg.Totales.Capital = neg.TotalesPesos.Capital;
                                    neg.Totales.Saldo = neg.TotalesPesos.Saldo;
                                    neg.Totales.Asignado = neg.TotalesPesos.Asignado;
                                    neg.Totales.Abono = neg.TotalesPesos.Abono;
                                    neg.Totales.Gasto = neg.TotalesPesos.Gasto;
                                }
                                d.Totales.Capital = (from od in d.lstNegocios
                                                     select od.Totales.Capital).Sum();
                                d.Totales.Saldo = (from od in d.lstNegocios
                                                   select od.Totales.Saldo).Sum();
                                d.Totales.Abono = (from od in d.lstNegocios
                                                   select od.Totales.Abono).Sum();
                                d.Totales.Asignado = (from od in d.lstNegocios
                                                      select od.Totales.Asignado).Sum();
                                d.Totales.Gasto = (from od in d.lstNegocios
                                                   select od.Totales.Gasto).Sum();
                            }
                        a.Totales.Capital = (from od in a.lstDocumentos
                                             select od.Totales.Capital).Sum();
                        a.Totales.Saldo = (from od in a.lstDocumentos
                                           select od.Totales.Saldo).Sum();
                        a.Totales.Abono = (from od in a.lstDocumentos
                                           select od.Totales.Abono).Sum();
                        a.Totales.Asignado = (from od in a.lstDocumentos
                                              select od.Totales.Asignado).Sum();
                        a.Totales.Gasto = (from od in a.lstDocumentos
                                           select od.Totales.Gasto).Sum();
                        monto += a.Totales.Capital;
                        saldo += a.Totales.Saldo;
                        interes += a.Totales.Abono;
                        honorarios += a.Totales.Asignado;
                        gasto += a.Totales.Gasto;
                        }
                        
                    }
                    obj.Totales.Capital = monto;
                    obj.Totales.Saldo = saldo;
                    obj.Totales.Abono = interes;
                    obj.Totales.Asignado = honorarios;
                    obj.Totales.Gasto = gasto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //try
            //{
            //    decimal monto = 0, saldo = 0, interes = 0, honorarios = 0, gasto = 0, total = 0;
            //    DataSet ds = new DataSet();
            //    StoredProcedure sp = new StoredProcedure("Trae_Reporte_Informe_PreJudicial");
            //    sp.AgregarParametro("ccb_codemp", obj.Codemp);
            //    sp.AgregarParametro("ccb_pclid", obj.Pclid);
            //    sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
            //    sp.AgregarParametro("idi_idid", obj.Idioma);
            //    ds = sp.EjecutarProcedimiento();

            //    if (ds.Tables.Count > 0)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            //            obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");

            //            var lstAsegurados = ds.Tables[0].AsEnumerable()
            //            .Select(row => new
            //            {
            //                RutDeudor = row.Field<decimal>("ctc_numero"),
            //                DvDeudor = row.Field<string>("ctc_digito"),
            //                SubCartera = row.Field<string>("sbc_nombre")??"",
            //                RutSubCartera = row.Field<string>("sbc_rut")??"",
            //                RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(row.Field<decimal>("ctc_numero").ToString() + row.Field<string>("ctc_digito").ToString()),
            //                NombreFantasia = row.Field<string>("ctc_nomfant").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"),
            //                Ciudad = row.Field<string>("ciu_nombre"),
            //                Comuna =row.Field<string>("com_nombre"),
            //                Region = row.Field<string>("reg_nombre"),
            //                Pais =row.Field<string>("pai_nombre"),
            //                //CodigoPostal = ds.Tables[0].Rows[0]["com_codpost"].ToString(),
            //                Direccion = row.Field<string>("ctc_direccion"),
            //                Ctcid = row.Field<decimal>("ccb_ctcid"),
            //                Pclid = row.Field<decimal>("ccb_pclid")

            //            })
            //            .Distinct().ToList();
            //            Indicadores objIndicadores = new Indicadores();
            //            Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);
            //            obj.CambioDolar = objIndicadores.DolarObservado;
            //            obj.CambioUF = objIndicadores.UF;
            //            bool insertaDeudor = false;
            //            foreach (var asegurado in lstAsegurados)
            //            {
            //                insertaDeudor = false;
            //                dto.InformePrejudicialDeudor deudor = obj.lstDocumentos.Find(x => x.RutDeudor == asegurado.RutDeudor.ToString());// new dto.InformePrejudicialDeudor();
            //                if (deudor == null)
            //                {
            //                    deudor = new dto.InformePrejudicialDeudor
            //                    {
            //                        RutDeudor = asegurado.RutDeudor.ToString(),
            //                        DvDeudor = asegurado.DvDeudor,
            //                        RutDeudorFormateado =asegurado.RutDeudorFormateado,
            //                        NombreFantasia = asegurado.NombreFantasia,
            //                        Ciudad = asegurado.Ciudad,
            //                        Comuna = asegurado.Comuna,
            //                        Region = asegurado.Region,
            //                        Pais = asegurado.Pais,
            //                        Ctcid = asegurado.Ctcid,
            //                        Direccion =asegurado.Direccion,
            //                        Pclid = asegurado.Pclid,

            //                    };
            //                    insertaDeudor = true;
            //                }

            //                dao.InformePrejudicial.TraeUltimaGestion(deudor, obj.Codemp, obj.Idioma);

            //                deudor.lstAsegurados.Add(new dto.InformePrejudicialAsegurado
            //                {
            //                    SubCartera = asegurado.SubCartera ?? "",
            //                    RutSubCartera = asegurado.RutSubCartera == null ? "" : Dimol.bcp.Funciones.formatearRut(asegurado.RutSubCartera)
            //                });
            //                if (insertaDeudor)
            //                {
            //                    obj.lstDocumentos.Add(deudor);
            //                }
                            
            //            }
            //            decimal gastoAseg = 0, totalAseg = 0;
            //            foreach (DataRow detalle in ds.Tables[0].Rows)
            //            {
            //                dto.InformePrejudicialDeudor deudor = obj.lstDocumentos.Find(x => x.RutDeudor==detalle["ctc_numero"].ToString());
            //                if (deudor != null)
            //                {
            //                    dto.InformePrejudicialAsegurado objAsegurado = deudor.lstAsegurados.Find(x => x.SubCartera == detalle["sbc_nombre"].ToString());
            //                    //gastoAseg = decimal.Parse(obj.EstadoCpbt == "V" ? detalle["ccb_gastotro"].ToString() : detalle["ccb_gastjud"].ToString());
            //                    //totalAseg = decimal.Parse(detalle["ccb_saldo"].ToString()) + decimal.Parse(detalle["ccb_intereses"].ToString()) + decimal.Parse(detalle["ccb_honorarios"].ToString()) + gastoAseg;
            //                    if (objAsegurado != null)
            //                    {
            //                        switch (detalle["mon_nombre"].ToString())
            //                        {
            //                            case "PESOS":
            //                                objAsegurado.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
            //                                {
            //                                    TipoDocumento = detalle["tci_nombre"].ToString(),
            //                                    Numero = detalle["ccb_numero"].ToString(),
            //                                    FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
            //                                    FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
            //                                    Capital = decimal.Parse(detalle["ccb_monto"].ToString()),
            //                                    Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
            //                                    Abono = decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString()),
            //                                    Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()),
            //                                    Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()),
            //                                    //Total = totalAseg,
            //                                   // Negocio = detalle["ccb_numesp"].ToString(),
            //                                    Moneda = detalle["mon_nombre"].ToString(),
            //                                    UltimoEstado = detalle["eci_nombre"].ToString()
            //                                });
            //                                break;
            //                            case "DOLAR":
            //                                objAsegurado.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
            //                                {
            //                                    TipoDocumento = detalle["tci_nombre"].ToString(),
            //                                    Numero = detalle["ccb_numero"].ToString(),
            //                                    FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
            //                                    FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
            //                                    Capital = decimal.Parse(detalle["ccb_monto"].ToString()) * objIndicadores.DolarObservado,
            //                                    Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.DolarObservado,
            //                                    Abono = (decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString())) * objIndicadores.DolarObservado,
            //                                    Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()) * objIndicadores.DolarObservado,
            //                                    Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()) * objIndicadores.DolarObservado,
            //                                    //Total = totalAseg,
            //                                    //Negocio = detalle["ccb_numesp"].ToString(),
            //                                    Moneda = detalle["mon_nombre"].ToString(),
            //                                    UltimoEstado = detalle["eci_nombre"].ToString()
            //                                });
            //                                break;
            //                            case "UF":
            //                                objAsegurado.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
            //                                {
            //                                    TipoDocumento = detalle["tci_nombre"].ToString(),
            //                                    Numero = detalle["ccb_numero"].ToString(),
            //                                    FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
            //                                    FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
            //                                    Capital = decimal.Parse(detalle["ccb_monto"].ToString()) * objIndicadores.UF,
            //                                    Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.UF,
            //                                    Abono = (decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString())) * objIndicadores.UF,
            //                                    Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()) * objIndicadores.UF,
            //                                    Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()) * objIndicadores.UF,
            //                                    //Total = totalAseg,
            //                                   // Negocio = detalle["ccb_numesp"].ToString(),
            //                                    Moneda = detalle["mon_nombre"].ToString(),
            //                                    UltimoEstado = detalle["eci_nombre"].ToString()
            //                                });
            //                                break;

            //                        }
            //                    }
            //                    else
            //                    {
            //                        string ddd = detalle["ctc_numero"].ToString();
            //                    }
            //                }
            //                else
            //                {
            //                    string e = detalle["ctc_numero"].ToString();
            //                }
            //            }
            //            foreach (dto.InformePrejudicialDeudor d in obj.lstDocumentos)
            //            {
            //                foreach (dto.InformePrejudicialAsegurado aseg in d.lstAsegurados)
            //                {
            //                    aseg.TotalesPesos.Capital = (from od in aseg.lstDetallesPesos
            //                                                 select od.Capital).Sum();
            //                    aseg.TotalesPesos.Saldo = (from od in aseg.lstDetallesPesos
            //                                               select od.Saldo).Sum();
            //                    aseg.TotalesPesos.Abono = (from od in aseg.lstDetallesPesos
            //                                               select od.Abono).Sum();
            //                    aseg.TotalesPesos.Asignado = (from od in aseg.lstDetallesPesos
            //                                                  select od.Asignado).Sum();
            //                    aseg.TotalesPesos.Gasto = (from od in aseg.lstDetallesPesos
            //                                               select od.Gasto).Sum();
            //                    //aseg.TotalesPesos.Total = (from od in aseg.lstDetallesPesos
            //                    //                           select od.Total).Sum();

            //                    //aseg.TotalesDolar.Monto = (from od in aseg.lstDetallesDolares
            //                    //                           select od.Monto).Sum();
            //                    //aseg.TotalesDolar.Saldo = (from od in aseg.lstDetallesDolares
            //                    //                           select od.Saldo).Sum();
            //                    //aseg.TotalesDolar.Intereses = (from od in aseg.lstDetallesDolares
            //                    //                               select od.Intereses).Sum();
            //                    //aseg.TotalesDolar.Honorarios = (from od in aseg.lstDetallesDolares
            //                    //                                select od.Honorarios).Sum();
            //                    //aseg.TotalesDolar.Gasto = (from od in aseg.lstDetallesDolares
            //                    //                           select od.Gasto).Sum();
            //                    //aseg.TotalesDolar.Total = (from od in aseg.lstDetallesDolares
            //                    //                           select od.Total).Sum();

            //                    //aseg.TotalesUF.Monto = (from od in aseg.lstDetallesUF
            //                    //                        select od.Monto).Sum();
            //                    //aseg.TotalesUF.Saldo = (from od in aseg.lstDetallesUF
            //                    //                        select od.Saldo).Sum();
            //                    //aseg.TotalesUF.Intereses = (from od in aseg.lstDetallesUF
            //                    //                            select od.Intereses).Sum();
            //                    //aseg.TotalesUF.Honorarios = (from od in aseg.lstDetallesUF
            //                    //                             select od.Honorarios).Sum();
            //                    //aseg.TotalesUF.Gasto = (from od in aseg.lstDetallesUF
            //                    //                        select od.Gasto).Sum();
            //                    //aseg.TotalesUF.Total = (from od in aseg.lstDetallesUF
            //                    //                        select od.Total).Sum();

            //                    aseg.Totales.Capital = aseg.TotalesPesos.Capital;// +aseg.TotalesDolar.Monto * objIndicadores.DolarObservado + aseg.TotalesUF.Monto * objIndicadores.UF;
            //                    aseg.Totales.Saldo = aseg.TotalesPesos.Saldo;// +aseg.TotalesDolar.Saldo * objIndicadores.DolarObservado + aseg.TotalesUF.Saldo * objIndicadores.UF;
            //                    aseg.Totales.Asignado = aseg.TotalesPesos.Asignado;// +aseg.TotalesDolar.Intereses * objIndicadores.DolarObservado + aseg.TotalesUF.Intereses * objIndicadores.UF;
            //                    aseg.Totales.Abono = aseg.TotalesPesos.Abono;// +aseg.TotalesDolar.Honorarios * objIndicadores.DolarObservado + aseg.TotalesUF.Honorarios * objIndicadores.UF;
            //                    aseg.Totales.Gasto = aseg.TotalesPesos.Gasto;// +aseg.TotalesDolar.Gasto * objIndicadores.DolarObservado + aseg.TotalesUF.Gasto * objIndicadores.UF;
            //                    //aseg.Totales.Total = aseg.TotalesPesos.Total;// +aseg.TotalesDolar.Total * objIndicadores.DolarObservado + aseg.TotalesUF.Total * objIndicadores.UF;

            //                    monto += aseg.Totales.Capital;
            //                    saldo += aseg.Totales.Saldo;
            //                    interes += aseg.Totales.Abono;
            //                    honorarios += aseg.Totales.Asignado;
            //                    gasto += aseg.Totales.Gasto;
            //                    //total += aseg.Totales.Total;
            //                }
            //            }
            //            obj.Totales.Capital = monto;
            //            obj.Totales.Saldo = saldo;
            //            obj.Totales.Abono = interes;
            //            obj.Totales.Asignado = honorarios;
            //            obj.Totales.Gasto = gasto;
            //            //obj.Totales.Total = total;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
                    if(ds.Tables[0].Rows.Count> 0)
                    {
                        obj.Gestion = ds.Tables[0].Rows[0]["aci_nombre"].ToString();
                        obj.UltimaGestion = DateTime.Parse( ds.Tables[0].Rows[0]["Fecha"].ToString());
                        obj.Resumen = ds.Tables[0].Rows[0]["comentario"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarDocumentosDetalleAseguradoCodigoCarga(dto.InformePrejudicial obj)
        {
            try
            {
                decimal monto = 0, saldo = 0, interes = 0, honorarios = 0, gasto = 0;
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Informe_PreJudicial_CodCarg");
                sp.AgregarParametro("ccb_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", obj.Pclid);
                sp.AgregarParametro("ccb_tipcart", obj.TipoCartera);
                sp.AgregarParametro("idi_idid", obj.Idioma);
                sp.AgregarParametro("pcc_codid", obj.Codid); 
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string[] meses = { "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
                        obj.FechaLarga = DateTime.Today.Day.ToString() + " de " + meses[DateTime.Today.Month] + ", " + DateTime.Today.ToString("yyyy");
                        obj.CodigoCarga = ds.Tables[0].Rows[0]["pcc_nombre"].ToString();
                        var lstAsegurados = ds.Tables[0].AsEnumerable()
                        .Select(row => new
                        {
                            RutDeudor = row.Field<decimal>("ctc_numero"),
                            DvDeudor = row.Field<string>("ctc_digito"),
                            //SubCartera = row.Field<string>("sbc_nombre") ?? "",
                            //RutSubCartera = row.Field<string>("sbc_rut") ?? "",
                            RutDeudorFormateado = Dimol.bcp.Funciones.formatearRut(row.Field<decimal>("ctc_numero").ToString() + row.Field<string>("ctc_digito").ToString()),
                            NombreFantasia = row.Field<string>("ctc_nomfant").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"),
                            Ciudad = row.Field<string>("ciu_nombre"),
                            Comuna = row.Field<string>("com_nombre"),
                            Region = row.Field<string>("reg_nombre"),
                            Pais = row.Field<string>("pai_nombre"),
                            //CodigoPostal = ds.Tables[0].Rows[0]["com_codpost"].ToString(),
                            Direccion = row.Field<string>("ctc_direccion"),
                            Ctcid = row.Field<decimal>("ccb_ctcid"),
                            Pclid = row.Field<decimal>("ccb_pclid"),
                            Negocio = row.Field<string>("ccb_numesp")
                            
                        })
                        .Distinct().ToList();
                        Indicadores objIndicadores = new Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objIndicadores);
                        obj.CambioDolar = objIndicadores.DolarObservado;
                        obj.CambioUF = objIndicadores.UF;
                        bool insertaDeudor = false, insertaNegocio = false;
                        foreach (var asegurado in lstAsegurados)
                        {
                            insertaDeudor = false;
                            insertaNegocio = false;
                            dto.InformePrejudicialDeudor deudor = obj.lstDocumentos.Find(x => x.RutDeudor == asegurado.RutDeudor.ToString());// new dto.InformePrejudicialDeudor();
                            if (deudor == null)
                            {
                                deudor = new dto.InformePrejudicialDeudor
                                {
                                    RutDeudor = asegurado.RutDeudor.ToString(),
                                    DvDeudor = asegurado.DvDeudor,
                                    RutDeudorFormateado = asegurado.RutDeudorFormateado,
                                    NombreFantasia = asegurado.NombreFantasia,
                                    Ciudad = asegurado.Ciudad,
                                    Comuna = asegurado.Comuna,
                                    Region = asegurado.Region,
                                    Pais = asegurado.Pais,
                                    Ctcid = asegurado.Ctcid,
                                    Direccion = asegurado.Direccion,
                                    Pclid = asegurado.Pclid
                                };
                                insertaDeudor = true;
                            }

                            dao.InformePrejudicial.TraeUltimaGestion(deudor, obj.Codemp, obj.Idioma);

                            dto.InformePrejudicialNegocio negocio = deudor.lstNegocios.Find(x => x.Negocio == asegurado.Negocio.ToString());// new dto.InformePrejudicialDeudor();
                            if (negocio == null)
                            {
                                negocio = new dto.InformePrejudicialNegocio
                                {
                                    Negocio = asegurado.Negocio.ToString()
                                };
                                insertaNegocio=  true;
                            }
                            if(insertaNegocio )
                            {
                                deudor.lstNegocios.Add(negocio);
                            }
                            if (insertaDeudor)
                            {
                                obj.lstDocumentos.Add(deudor);
                            }
                            //deudor.lstAsegurados.Add(new dto.InformePrejudicialAsegurado
                            //{
                            //    SubCartera = asegurado.SubCartera ?? "",
                            //    RutSubCartera = asegurado.RutSubCartera == null ? "" : Dimol.bcp.Funciones.formatearRut(asegurado.RutSubCartera)
                            //});
                            //obj.lstDocumentos.Add(deudor);
                        }
                        foreach (DataRow detalle in ds.Tables[0].Rows)
                        {
                            dto.InformePrejudicialDeudor deudor = obj.lstDocumentos.Find(x => x.RutDeudor == detalle["ctc_numero"].ToString());
                            if (deudor != null)
                            {
                                dto.InformePrejudicialNegocio objNegocio = deudor.lstNegocios.Find(x => x.Negocio == detalle["ccb_numesp"].ToString());
                                if (objNegocio != null)
                                {
                                    switch (detalle["mon_nombre"].ToString())
                                    {
                                        case "PESOS":
                                            objNegocio.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
                                            {
                                                TipoDocumento = detalle["tci_nombre"].ToString(),
                                                Numero = detalle["ccb_numero"].ToString(),
                                                FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
                                                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                                Capital = decimal.Parse(detalle["ccb_monto"].ToString()),
                                                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()),
                                                Abono = decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString()),
                                                Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()),
                                                Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()),
                                                //Total = totalAseg,
                                                //Negocio = detalle["ccb_numesp"].ToString(),
                                                Moneda = detalle["mon_nombre"].ToString(),
                                                UltimoEstado = detalle["eci_nombre"].ToString()
                                            });
                                            break;
                                        case "DOLAR":
                                            objNegocio.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
                                            {
                                                TipoDocumento = detalle["tci_nombre"].ToString(),
                                                Numero = detalle["ccb_numero"].ToString(),
                                                FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
                                                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                                Capital = decimal.Parse(detalle["ccb_monto"].ToString()) * objIndicadores.DolarObservado,
                                                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.DolarObservado,
                                                Abono = (decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString())) * objIndicadores.DolarObservado,
                                                Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()) * objIndicadores.DolarObservado,
                                                Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()) * objIndicadores.DolarObservado,
                                                //Total = totalAseg,
                                                //Negocio = detalle["ccb_numesp"].ToString(),
                                                Moneda = detalle["mon_nombre"].ToString(),
                                                UltimoEstado = detalle["eci_nombre"].ToString()
                                            });
                                            break;
                                        case "UF":
                                            objNegocio.lstDetallesPesos.Add(new dto.InformePrejudicialDetalle
                                            {
                                                TipoDocumento = detalle["tci_nombre"].ToString(),
                                                Numero = detalle["ccb_numero"].ToString(),
                                                FechaIngreso = DateTime.Parse(detalle["ccb_fecing"].ToString()),
                                                FechaVencimiento = DateTime.Parse(detalle["ccb_fecvenc"].ToString()),
                                                Capital = decimal.Parse(detalle["ccb_monto"].ToString()) * objIndicadores.UF,
                                                Saldo = decimal.Parse(detalle["ccb_saldo"].ToString()) * objIndicadores.UF,
                                                Abono = (decimal.Parse(detalle["ccb_asignado"].ToString()) - decimal.Parse(detalle["ccb_saldo"].ToString())) * objIndicadores.UF,
                                                Asignado = decimal.Parse(detalle["ccb_asignado"].ToString()) * objIndicadores.UF,
                                                Gasto = decimal.Parse(detalle["ccb_gastotro"].ToString()) * objIndicadores.UF,
                                                //Total = totalAseg,
                                                //Negocio = detalle["ccb_numesp"].ToString(),
                                                Moneda = detalle["mon_nombre"].ToString(),
                                                UltimoEstado = detalle["eci_nombre"].ToString()
                                            });
                                            break;

                                    }
                                }
                                else
                                {
                                    string ddd = detalle["ctc_numero"].ToString();
                                }
                            }
                            else
                            {
                                string e = detalle["ctc_numero"].ToString();
                            }
                        }
                        foreach (dto.InformePrejudicialDeudor d in obj.lstDocumentos)
                        {
                            foreach (dto.InformePrejudicialNegocio neg in d.lstNegocios)
                            {
                                neg.TotalesPesos.Capital = (from od in neg.lstDetallesPesos
                                                             select od.Capital).Sum();
                                neg.TotalesPesos.Saldo = (from od in neg.lstDetallesPesos
                                                           select od.Saldo).Sum();
                                neg.TotalesPesos.Abono = (from od in neg.lstDetallesPesos
                                                           select od.Abono).Sum();
                                neg.TotalesPesos.Asignado = (from od in neg.lstDetallesPesos
                                                              select od.Asignado).Sum();
                                neg.TotalesPesos.Gasto = (from od in neg.lstDetallesPesos
                                                           select od.Gasto).Sum();

                                neg.Totales.Capital = neg.TotalesPesos.Capital;
                                neg.Totales.Saldo = neg.TotalesPesos.Saldo;
                                neg.Totales.Asignado = neg.TotalesPesos.Asignado;
                                neg.Totales.Abono = neg.TotalesPesos.Abono;
                                neg.Totales.Gasto = neg.TotalesPesos.Gasto;

                                monto += neg.Totales.Capital;
                                saldo += neg.Totales.Saldo;
                                interes += neg.Totales.Abono;
                                honorarios += neg.Totales.Asignado;
                                gasto += neg.Totales.Gasto;
                            }
                            d.Totales.Capital = (from od in d.lstNegocios
                                                        select od.Totales.Capital).Sum();
                            d.Totales.Saldo = (from od in d.lstNegocios
                                               select od.Totales.Saldo).Sum();
                            d.Totales.Abono = (from od in d.lstNegocios
                                               select od.Totales.Abono).Sum();
                            d.Totales.Asignado = (from od in d.lstNegocios
                                                  select od.Totales.Asignado).Sum();
                            d.Totales.Gasto = (from od in d.lstNegocios
                                               select od.Totales.Gasto).Sum();
                        }
                        obj.Totales.Capital = monto;
                        obj.Totales.Saldo = saldo;
                        obj.Totales.Abono = interes;
                        obj.Totales.Asignado = honorarios;
                        obj.Totales.Gasto = gasto;
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
