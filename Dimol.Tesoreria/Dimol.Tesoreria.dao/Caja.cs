using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Tesoreria.dto;

namespace Dimol.Tesoreria.dao
{
    public class Caja
    {
        public static List<Combobox> ListarTipo(int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            string value = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                lst.Add(new Combobox() { Text = first, Value = "" });
                for (int i = 2; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipAsi" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    switch (i)
                    {
                        case 2:
                            value = "I";
                            break;
                        case 3:
                            value = "E";
                            break;
                    }

                    lst.Add(new Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = value
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static string ListarTipoString(int idioma)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                string value = "";
                for (int i = 2; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipAsi" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    switch (i)
                    {
                        case 2:
                            value = "I";
                            break;
                        case 3:
                            value = "E";
                            break;
                    }


                    if (i == 1)
                    {
                        salida += value + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + value + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static List<Combobox> ListarEmpleados(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Empleados");
                sp.AgregarParametro("epl_codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["epl_emplid"].ToString(),
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString()
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

        public static List<Combobox> ListarTipoCpbt(int codemp,string tipoCpbtDoc, int perfil, int idioma, string carteraCliente, string tipo, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Tipos_Cpbt_Perfil_Tipo_Documentos");
                sp.AgregarParametro("clb_codemp", codemp);
                sp.AgregarParametro("clb_tipcpbtdoc", tipoCpbtDoc);
                sp.AgregarParametro("pfc_prfid", perfil);
                sp.AgregarParametro("tci_idid", idioma);
                sp.AgregarParametro("clb_cartcli", carteraCliente);
                sp.AgregarParametro("clb_cptoctbl", tipo);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["tpc_tpcid"].ToString(),
                            Text = ds.Tables[0].Rows[i]["tci_nombre"].ToString()
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

        public static List<BuscarDocumentoCaja> ListarDocumentosCaja(int codemp, int idioma, string pclid, string ctcid, string tipoMovimiento, string tipoDocumento, string emplid, string numeroCuenta, string montoDesde, string montoHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarDocumentoCaja> lst = new List<dto.BuscarDocumentoCaja>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Caja_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("ctcid", (object)ctcid ?? DBNull.Value);
                sp.AgregarParametro("tipo_movimiento", (object)tipoMovimiento ?? DBNull.Value);
                sp.AgregarParametro("tipo_documento", (object)tipoDocumento ?? DBNull.Value);
                sp.AgregarParametro("emplid", emplid);
                sp.AgregarParametro("num_cta", (object)numeroCuenta ?? DBNull.Value);
                sp.AgregarParametro("monto_desde", (object)montoDesde ?? DBNull.Value);
                sp.AgregarParametro("monto_hasta", (object)montoHasta ?? DBNull.Value);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.BuscarDocumentoCaja()
                        {
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["Anio"].ToString()),
                            NumeroDocumento = ds.Tables[0].Rows[i]["NumeroDocumento"].ToString(),
                            TipoMovimiento = ds.Tables[0].Rows[i]["TipoMovimiento"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Monto = decimal.Parse( ds.Tables[0].Rows[i]["Monto"].ToString()),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            NombreDeudor = ds.Tables[0].Rows[i]["NombreDeudor"].ToString(),
                            NumeroCuenta = ds.Tables[0].Rows[i]["NumeroCuenta"].ToString(),
                            Empleado = ds.Tables[0].Rows[i]["Empleado"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString()
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

        public static int ListarDocumentosCajaCount(int codemp, int idioma, string pclid, string ctcid, string tipoMovimiento, string tipoDocumento, string emplid, string numeroCuenta, string montoDesde, string montoHasta, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Caja_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("ctcid",  (object)ctcid ?? DBNull.Value);
                sp.AgregarParametro("tipo_movimiento", (object)tipoMovimiento ?? DBNull.Value);
                sp.AgregarParametro("tipo_documento", (object)tipoDocumento ?? DBNull.Value);
                sp.AgregarParametro("emplid", emplid);
                sp.AgregarParametro("num_cta", (object)numeroCuenta ?? DBNull.Value);
                sp.AgregarParametro("monto_desde", (object)montoDesde ?? DBNull.Value);
                sp.AgregarParametro("monto_hasta", (object)montoHasta ?? DBNull.Value);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<BuscarAnularPago> ListarAnularPago(int codemp, int sucid, string pclid, string ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarAnularPago> lst = new List<dto.BuscarAnularPago>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Anula_Pagos_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("ctcid", (object)ctcid ?? DBNull.Value);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.BuscarAnularPago()
                        {
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["Anio"].ToString()),
                            NumeroAplicacion = Int32.Parse(ds.Tables[0].Rows[i]["NumeroAplicacion"].ToString()),
                            Item = Int32.Parse(ds.Tables[0].Rows[i]["Item"].ToString()),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            NombreDeudor = ds.Tables[0].Rows[i]["NombreDeudor"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            NumeroCuenta = Int32.Parse(ds.Tables[0].Rows[i]["NumeroCuenta"].ToString()),
                            NumeroDocumento = Int32.Parse(ds.Tables[0].Rows[i]["NumeroDocumento"].ToString()),
                            Fecha = DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString()),
                            Capital = decimal.Parse(ds.Tables[0].Rows[i]["Capital"].ToString()),
                            Interes = decimal.Parse(ds.Tables[0].Rows[i]["Interes"].ToString()),
                            Honorario = decimal.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString()),
                            GastoPrejudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoPrejudicial"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastoJudicial"].ToString()),
                            Total = decimal.Parse(ds.Tables[0].Rows[i]["Total"].ToString()),
                            Gestor = ds.Tables[0].Rows[i]["Gestor"].ToString(),
                            Vendedor = ds.Tables[0].Rows[i]["Vendedor"].ToString()
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

        public static int ListarAnularPagoCount(int codemp, int sucid, string pclid, string ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Anula_Pagos_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("ctcid", (object)ctcid ?? DBNull.Value);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }


        public static List<Combobox> ListarEstadosCaja(int codemp,  int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Caja");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["edi_edcid"].ToString(),
                            Text = ds.Tables[0].Rows[i]["edi_nombre"].ToString()
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

        public static List<Combobox> ListarNegociacionesCaja(int codemp, int ctcid, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Negociacion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["neg_anio"].ToString() + "|" +ds.Tables[0].Rows[i]["neg_negid"].ToString(),
                            Text = ds.Tables[0].Rows[i]["neg_anio"].ToString() + "," + ds.Tables[0].Rows[i]["neg_negid"].ToString()
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
    }
}
