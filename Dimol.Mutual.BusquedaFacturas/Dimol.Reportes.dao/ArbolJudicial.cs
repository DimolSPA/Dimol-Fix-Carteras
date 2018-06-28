using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Reportes.dao
{
    public class ArbolJudicial
    {
        public static void TraeTitulo(dto.ArbolJudicial obj)
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
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                

        public static void ListarArbolJudicialDetalle(dto.ArbolJudicial obj)
        {
            try
            {
                
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Arbol_Judicial");
                sp.AgregarParametro("rol_codemp", obj.Codemp);
                //sp.AgregarParametro("desde", obj.FechaDesde);
                //sp.AgregarParametro("hasta", obj.FechaHasta);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    { 
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {                                                    

                            obj.lstArbol.Add(new dto.ArbolJudicialBruto
                            {
                                RutCliente = Dimol.bcp.Funciones.formatearRut(dr["RutCli"].ToString()),
                                NombreCliente = dr["NomCli"].ToString(),
                                RutDeudor = Dimol.bcp.Funciones.formatearRut(dr["RutDeu"].ToString()),
                                NombreDeudor = dr["NomDeu"].ToString(),
                                Numero = dr["Numero"].ToString(),
                                Demandado = decimal.Parse(dr["Demandado"].ToString()),
                                Saldo = decimal.Parse(dr["Saldo"].ToString()),
                                MateriaJudicial = dr["MatJud"].ToString(),
                                EstadoRol = dr["EstRol"].ToString(),
                                Rol = dr["Rol"].ToString(),
                                FechaJudicial = DateTime.Parse(dr["FecJud"].ToString()),
                                Tribunal = dr["Tribunal"].ToString(),
                                CodigoCarga = dr["CodCarg"].ToString(),
                                FechaAsignacion = DateTime.Parse(dr["FecAsig"].ToString()),
                                Moneda = dr["Moneda"].ToString(),
                                Valido = Int32.Parse(dr["Valido"].ToString()),
                                Abogado = dr["Abogado"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                Cuenta = Int32.Parse(dr["Cuenta"].ToString()),
                                Logica = dr["Logica"].ToString(),
                                Asegurado = dr["Asegurado"].ToString(),
                                TipoCausa = dr["TipCausa"].ToString(),
                                DiasSinGestion = Int32.Parse(dr["Dias"].ToString())
                            });                        
                        }
                        
                        List<string> lstCategorias = obj.lstArbol.Select(o => o.Categoria).Distinct().ToList();

                        int cuenta = 0;
                        decimal total = 0;
                        decimal porcTotal = 0;

                        foreach (string categoria in lstCategorias)
                        {
                            dto.ArbolJudicialCategoria objCategoria = new dto.ArbolJudicialCategoria();

                            //foreach (var item in obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado)).GroupBy(o => new { o.RutCliente, o.NombreCliente, o.RutDeudor, o.NombreDeudor, o.Rol, o.Tribunal, o.Logica, o.Cuenta, o.Categoria, o.Abogado }).Select(o => new dto.ArbolJudicialDetalle { RutCliente = o.Key.RutCliente, NombreCliente = o.Key.NombreCliente, RutDeudor = o.Key.RutDeudor, NombreDeudor = o.Key.NombreDeudor, Rol = o.Key.Rol, Tribunal = o.Key.Tribunal, Logica = o.Key.Logica, Saldo = o.Sum(s => s.Saldo), Cuenta = o.Key.Cuenta, Categoria = o.Key.Categoria, Abogado = o.Key.Abogado }).ToList())
                            foreach (var item in obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado)).GroupBy(o => new { o.NombreCliente, o.RutDeudor, o.NombreDeudor, o.Asegurado, o.Rol, o.Tribunal, o.TipoCausa, o.MateriaJudicial, o.EstadoRol, o.FechaJudicial, o.DiasSinGestion, o.Logica, o.Cuenta, o.Categoria, o.Abogado }).Select(o => new dto.ArbolJudicialDetalle { NombreCliente = o.Key.NombreCliente, RutDeudor = o.Key.RutDeudor, NombreDeudor = o.Key.NombreDeudor, Asegurado = o.Key.Asegurado, Rol = o.Key.Rol, Tribunal = o.Key.Tribunal, TipoCausa = o.Key.TipoCausa, MateriaJudicial = o.Key.MateriaJudicial, EstadoRol = o.Key.EstadoRol, FechaJudicial = o.Key.FechaJudicial, DiasSinGestion = o.Key.DiasSinGestion, Logica = o.Key.Logica, Saldo = o.Sum(s => s.Saldo), Cuenta = o.Key.Cuenta, Categoria = o.Key.Categoria, Abogado = o.Key.Abogado }).ToList())
                            {
                                objCategoria.lstDetalle.Add(new dto.ArbolJudicialDetalle
                                {
                                    NombreCliente = item.NombreCliente,
                                    RutDeudor = item.RutDeudor,
                                    NombreDeudor = item.NombreDeudor,
                                    Asegurado = item.Asegurado,
                                    Rol = item.Rol,
                                    Tribunal = item.Tribunal,
                                    TipoCausa = item.TipoCausa,
                                    MateriaJudicial = item.MateriaJudicial,
                                    EstadoRol = item.EstadoRol,
                                    FechaJudicial = item.FechaJudicial,
                                    DiasSinGestion = item.DiasSinGestion,
                                    Logica = item.Logica,                              
                                    Saldo = item.Saldo,
                                    Cuenta = item.Cuenta,
                                    Categoria = item.Categoria,
                                    Abogado = item.Abogado
                                });
                                
                            }

                            objCategoria.Nombre = categoria;

                            objCategoria.Totales.CuentaSinMov = obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado) && o.Logica == "SIN MOVIMIENTO").GroupBy(o => new { o.NombreCliente, o.RutDeudor, o.NombreDeudor, o.Rol, o.Tribunal, o.TipoCausa, o.MateriaJudicial, o.EstadoRol, o.FechaJudicial, o.DiasSinGestion, o.Logica, o.Cuenta, o.Categoria, o.Abogado }).Select(o => new dto.ArbolJudicialDetalle { NombreCliente = o.Key.NombreCliente, RutDeudor = o.Key.RutDeudor, NombreDeudor = o.Key.NombreDeudor, Rol = o.Key.Rol, Tribunal = o.Key.Tribunal, TipoCausa = o.Key.TipoCausa, MateriaJudicial = o.Key.MateriaJudicial, EstadoRol = o.Key.EstadoRol, FechaJudicial = o.Key.FechaJudicial, DiasSinGestion = o.Key.DiasSinGestion, Logica = o.Key.Logica, Saldo = o.Sum(s => s.Saldo), Cuenta = o.Key.Cuenta, Categoria = o.Key.Categoria, Abogado = o.Key.Abogado }).Count();
                            objCategoria.Totales.TotalSinMov = obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado) && o.Logica == "SIN MOVIMIENTO").Select(s => s.Saldo).Sum();
                            objCategoria.Totales.CuentaConMov = obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado) && o.Logica == "CON MOVIMIENTO").GroupBy(o => new { o.NombreCliente, o.RutDeudor, o.NombreDeudor, o.Rol, o.Tribunal, o.TipoCausa, o.MateriaJudicial, o.EstadoRol, o.FechaJudicial, o.DiasSinGestion, o.Logica, o.Cuenta, o.Categoria, o.Abogado }).Select(o => new dto.ArbolJudicialDetalle { NombreCliente = o.Key.NombreCliente, RutDeudor = o.Key.RutDeudor, NombreDeudor = o.Key.NombreDeudor, Rol = o.Key.Rol, Tribunal = o.Key.Tribunal, TipoCausa = o.Key.TipoCausa, MateriaJudicial = o.Key.MateriaJudicial, EstadoRol = o.Key.EstadoRol, FechaJudicial = o.Key.FechaJudicial, DiasSinGestion = o.Key.DiasSinGestion, Logica = o.Key.Logica, Saldo = o.Sum(s => s.Saldo), Cuenta = o.Key.Cuenta, Categoria = o.Key.Categoria, Abogado = o.Key.Abogado }).Count();
                            objCategoria.Totales.TotalConMov = obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado) && o.Logica == "CON MOVIMIENTO").Select(s => s.Saldo).Sum();
                            objCategoria.Totales.CuentaTotal = obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado)).GroupBy(o => new { o.NombreCliente, o.RutDeudor, o.NombreDeudor, o.Rol, o.Tribunal, o.TipoCausa, o.MateriaJudicial, o.EstadoRol, o.FechaJudicial, o.DiasSinGestion, o.Logica, o.Cuenta, o.Categoria, o.Abogado }).Select(o => new dto.ArbolJudicialDetalle { NombreCliente = o.Key.NombreCliente, RutDeudor = o.Key.RutDeudor, NombreDeudor = o.Key.NombreDeudor, Rol = o.Key.Rol, Tribunal = o.Key.Tribunal, TipoCausa = o.Key.TipoCausa, MateriaJudicial = o.Key.MateriaJudicial, EstadoRol = o.Key.EstadoRol, FechaJudicial = o.Key.FechaJudicial, DiasSinGestion = o.Key.DiasSinGestion, Logica = o.Key.Logica, Saldo = o.Sum(s => s.Saldo), Cuenta = o.Key.Cuenta, Categoria = o.Key.Categoria, Abogado = o.Key.Abogado }).Count();
                            objCategoria.Totales.Total = obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado)).Select(s => s.Saldo).Sum();
                            //  objCategoria.Totales.CuentaSinMov = objCategoria.lstDetalle.Where(o => o.Logica == "SIN MOVIMIENTO").Select(o => o.Saldo).Count();
                            //  objCategoria.Totales.TotalSinMov = objCategoria.lstDetalle.Where(o => o.Logica == "SIN MOVIMIENTO").Select(o => o.Saldo).Sum();
                            //  objCategoria.Totales.CuentaConMov = objCategoria.lstDetalle.Where(o => o.Logica == "CON MOVIMIENTO").Select(o => o.Saldo).Count();
                            //  objCategoria.Totales.TotalConMov = objCategoria.lstDetalle.Where(o => o.Logica == "CON MOVIMIENTO").Select(o => o.Saldo).Sum();
                            //  objCategoria.Totales.CuentaTotal = objCategoria.lstDetalle.Select(o => o.Saldo).Count();
                            //  objCategoria.Totales.Total = objCategoria.lstDetalle.Select(o => o.Saldo).Sum();

                            obj.lstCategoria.Add(objCategoria);

                            cuenta += obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado)).GroupBy(o => new { o.NombreCliente, o.RutDeudor, o.NombreDeudor, o.Rol, o.Tribunal, o.TipoCausa, o.MateriaJudicial, o.EstadoRol, o.FechaJudicial, o.DiasSinGestion, o.Logica, o.Cuenta, o.Categoria, o.Abogado }).Select(o => new dto.ArbolJudicialDetalle { NombreCliente = o.Key.NombreCliente, RutDeudor = o.Key.RutDeudor, NombreDeudor = o.Key.NombreDeudor, Rol = o.Key.Rol, Tribunal = o.Key.Tribunal, TipoCausa = o.Key.TipoCausa, MateriaJudicial = o.Key.MateriaJudicial, EstadoRol = o.Key.EstadoRol, FechaJudicial = o.Key.FechaJudicial, DiasSinGestion = o.Key.DiasSinGestion, Logica = o.Key.Logica, Saldo = o.Sum(s => s.Saldo), Cuenta = o.Key.Cuenta, Categoria = o.Key.Categoria, Abogado = o.Key.Abogado }).Count();
                            total += obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado)).Select(s => s.Saldo).Sum();
                            porcTotal += obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && obj.Abogados.Contains(o.Abogado) && o.Logica == "CON MOVIMIENTO").GroupBy(o => new { o.NombreCliente, o.RutDeudor, o.NombreDeudor, o.Rol, o.Tribunal, o.TipoCausa, o.MateriaJudicial, o.EstadoRol, o.FechaJudicial, o.DiasSinGestion, o.Logica, o.Cuenta, o.Categoria, o.Abogado }).Select(o => new dto.ArbolJudicialDetalle { NombreCliente = o.Key.NombreCliente, RutDeudor = o.Key.RutDeudor, NombreDeudor = o.Key.NombreDeudor, Rol = o.Key.Rol, Tribunal = o.Key.Tribunal, TipoCausa = o.Key.TipoCausa, MateriaJudicial = o.Key.MateriaJudicial, EstadoRol = o.Key.EstadoRol, FechaJudicial = o.Key.FechaJudicial, DiasSinGestion = o.Key.DiasSinGestion, Logica = o.Key.Logica, Saldo = o.Sum(s => s.Saldo), Cuenta = o.Key.Cuenta, Categoria = o.Key.Categoria, Abogado = o.Key.Abogado }).Count(); 
                            //  cuenta += objCategoria.lstDetalle.Select(o => o.Saldo).Count();
                            //  total += objCategoria.lstDetalle.Select(o => o.Saldo).Sum();
                        }

                        if (total == 0)
                        {
                            obj.Total.PorcTotalConMov = 0;
                        }
                        else
                        {
                            obj.Total.PorcTotalConMov = (porcTotal / cuenta) *100;
                        }
                        
                        obj.Total.CuentaTotal = cuenta;
                        obj.Total.Total = total;
                                                
                        foreach(string abogado in obj.Abogados)
                        {

                            cuenta = 0;
                            total = 0;
                            porcTotal = 0;

                            dto.ArbolJudicialAbogado objAbo = new dto.ArbolJudicialAbogado();

                            objAbo.Nombre = abogado;

                            foreach (string categoria in lstCategorias)
                            {
                                dto.ArbolJudicialCategoria objCat = new dto.ArbolJudicialCategoria();

                                objCat.lstDetalle = obj.lstArbol.Where(o => o.Categoria == categoria && o.Valido == 1 && o.Abogado == abogado).GroupBy(o => new { o.RutCliente, o.NombreCliente, o.RutDeudor, o.NombreDeudor, o.Rol, o.Tribunal, o.Logica, o.Cuenta, o.Categoria, o.Abogado }).Select(o => new dto.ArbolJudicialDetalle { RutCliente = o.Key.RutCliente, NombreCliente = o.Key.NombreCliente, RutDeudor = o.Key.RutDeudor, NombreDeudor = o.Key.NombreDeudor, Rol = o.Key.Rol, Tribunal = o.Key.Tribunal, Logica = o.Key.Logica, Saldo = o.Sum(s => s.Saldo), Cuenta = o.Key.Cuenta, Categoria = o.Key.Categoria, Abogado = o.Key.Abogado }).ToList();
                                
                                objCat.Nombre = categoria;

                                objCat.Totales.CuentaSinMov = objCat.lstDetalle.Where(o => o.Logica == "SIN MOVIMIENTO").Select(o => o.Saldo).Count();
                                objCat.Totales.TotalSinMov = objCat.lstDetalle.Where(o => o.Logica == "SIN MOVIMIENTO").Select(o => o.Saldo).Sum();
                                objCat.Totales.CuentaConMov = objCat.lstDetalle.Where(o => o.Logica == "CON MOVIMIENTO").Select(o => o.Saldo).Count();
                                objCat.Totales.TotalConMov = objCat.lstDetalle.Where(o => o.Logica == "CON MOVIMIENTO").Select(o => o.Saldo).Sum();
                                objCat.Totales.CuentaTotal = objCat.lstDetalle.Select(o => o.Saldo).Count();
                                objCat.Totales.Total = objCat.lstDetalle.Select(o => o.Saldo).Sum();

                                porcTotal += objCat.lstDetalle.Where(o => o.Logica == "CON MOVIMIENTO").Select(o => o.Saldo).Count();
                                cuenta += objCat.lstDetalle.Select(o => o.Saldo).Count();
                                total += objCat.lstDetalle.Select(o => o.Saldo).Sum();

                                objCat.lstDetalle = null;
                                objAbo.lstCateg.Add(objCat);
                                
                            }

                            if (total == 0)
                            {
                                objAbo.Tot.PorcTotalConMov = 0;
                            }
                            else
                            {
                                objAbo.Tot.PorcTotalConMov = (porcTotal / cuenta) * 100;
                            }
                            objAbo.Tot.CuentaTotal = cuenta;
                            objAbo.Tot.Total = total;

                            obj.lstArbolAbogado.Add(objAbo);
                        }
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
