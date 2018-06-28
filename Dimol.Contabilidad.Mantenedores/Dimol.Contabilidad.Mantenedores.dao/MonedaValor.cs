    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dimol.Contabilidad.Mantenedores.dto;
    using System.Data;
    using Dimol.dao;
    using System.Data.SqlClient;
    using System.Diagnostics;


    namespace Dimol.Contabilidad.Mantenedores.dao
    {
        public class MonedaValor
        {
            public List<dto.MonedaValor> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
            {
                List<dto.MonedaValor> lstPeriodos = new List<dto.MonedaValor>();
                try
                {

                    DataSet ds = new DataSet();
                    StoredProcedure sp = new StoredProcedure("_Listar_MonedasValores_Grilla");
                    //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                    sp.AgregarParametro("codemp", codemp);
                    sp.AgregarParametro("where", where);
                    sp.AgregarParametro("sidx", sidx);
                    sp.AgregarParametro("sord", sord);
                    sp.AgregarParametro("inicio", inicio);
                    sp.AgregarParametro("limite", limite);
                    //Debug.WriteLine("PARAMETROS SP " + codemp + " " + where + " " + sidx + " " + sord + " " + inicio + " " + limite);
                    ds = sp.EjecutarProcedimiento();
                    Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                    if (ds.Tables.Count > 0)
                    {
                        //Debug.WriteLine("HAY DATOS");
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            //Debug.WriteLine("ENTRO AL FOR");
                            lstPeriodos.Add(new dto.MonedaValor()
                            {

                                Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                                Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                                Fecha = DateTime.Parse(ds.Tables[1].Rows[i]["fecha"].ToString()),
                                Valor = Double.Parse(ds.Tables[1].Rows[i]["valor"].ToString()),
                                codMoneda = Int32.Parse(ds.Tables[1].Rows[i]["codMoneda"].ToString()),
                                Id = Int32.Parse(ds.Tables[1].Rows[i]["Id"].ToString())
                            });
                        }
                    }
                    Debug.WriteLine("lstPeriodos" + lstPeriodos);
                    return lstPeriodos;
                }
                catch (Exception ex)
                {
                    return lstPeriodos;
                }

            }

            public bool convertirTrueFalse(string val)
            {
                bool returnval = true;
                if (val.Equals("S"))
                {
                    returnval = true;
                }
                else
                {
                    returnval = false;
                }
                return returnval;
            }


            public List<dto.MonedaValor> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
            {
                List<dto.MonedaValor> lstPeriodos = new List<dto.MonedaValor>();
                try
                {

                    DataSet ds = new DataSet();
                    StoredProcedure sp = new StoredProcedure("_Listar_MonedasValores_Grilla");
                    sp.AgregarParametro("codemp", codemp);
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
                            lstPeriodos.Add(new dto.MonedaValor()
                            {
                                Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                                Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                                Fecha = DateTime.Parse(ds.Tables[1].Rows[i]["fecha"].ToString()),
                                Valor = Double.Parse(ds.Tables[1].Rows[i]["valor"].ToString()),
                                codMoneda = Int32.Parse(ds.Tables[1].Rows[i]["codMoneda"].ToString()),
                                Id = Int32.Parse(ds.Tables[1].Rows[i]["Id"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                return lstPeriodos;
            }


            public void Insertar(dto.MonedaValor objAccion, int codemp)
            {
                try
                {
                    StoredProcedure sp = new StoredProcedure("Insertar_Monedas_Valores");
                    sp.AgregarParametro("mnv_codemp", codemp);
                    sp.AgregarParametro("mnv_codmon", objAccion.Nombre);
                    sp.AgregarParametro("mnv_fecha", objAccion.Fecha);
                    sp.AgregarParametro("mnv_valor", objAccion.Valor);
                   
                    int error = sp.EjecutarProcedimientoTrans();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            public void Borrar(dto.MonedaValor objAccion, int codemp, int? idmon)
            {
                DateTime fecha;
                string id = idmon.ToString();
                string _idmon = id.Substring(1, 1);
                string _idfecha = id.Substring(4, 4);

                string anio = id.Substring(2, 4);
                string mes = id.Substring(6, 2);
                string dia = id.Substring(8);
                int _anio = Int32.Parse(anio);
                int _mes = Int32.Parse(mes);
                int _dia = Int32.Parse(dia);
                try
                {
                    
                    fecha = new DateTime(_anio, _mes, _dia, 0, 0, 0, 000);
                    Debug.WriteLine("DATOS A BORRAR : " + codemp + "-" + fecha + "-" + _idmon);
                    StoredProcedure sp = new StoredProcedure("Delete_Monedas_Valores");
                    sp.AgregarParametro("mnv_codemp", codemp);
                    sp.AgregarParametro("mnv_codmon", _idmon);
                    sp.AgregarParametro("mnv_fecha", fecha);
                    int error = sp.EjecutarProcedimientoTrans();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void Editar(dto.MonedaValor objAccion, int codemp, int idmon)
            {
                DateTime fecha;
                try
                {
                    StoredProcedure sp = new StoredProcedure("Update_Monedas_Valores");
                    
                    
                    string id = idmon.ToString();
                    string _idmon = id.Substring(1, 1);
                    string _idfecha = id.Substring(4,4);
                    
                    
                    string anio = id.Substring(2, 4);
                    string mes = id.Substring(6,2);
                    string dia = id.Substring(8);
                    int _anio = Int32.Parse(anio);
                    int _mes = Int32.Parse(mes);
                    int _dia = Int32.Parse(dia);
                    fecha = new DateTime(_anio,_mes,_dia, 0, 0, 0, 000);
                    string _fechaFinal = String.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", fecha);

                    Debug.WriteLine("DATOS A EDITAR  " + codemp + "-" + _idmon + "-" + objAccion.Nombre + "-" + _fechaFinal + "-" + objAccion.Valor + "-" + fecha);
                    sp.AgregarParametro("mnv_codemp", codemp);
                    sp.AgregarParametro("mnv_codmon", objAccion.Nombre);
                    sp.AgregarParametro("mnv_fecha", fecha);
                    sp.AgregarParametro("mnv_valor", objAccion.Valor);
                    
                    int error = sp.EjecutarProcedimientoTrans();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public int ListarMonedasValoresCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
            {
                try
                {
                    DataSet ds = new DataSet();
                    StoredProcedure sp = new StoredProcedure("_Listar_MonedasValores_Grilla_Count");
                    sp.AgregarParametro("codemp", codemp);
                    sp.AgregarParametro("where", where);
                    sp.AgregarParametro("sidx", sidx);
                    sp.AgregarParametro("sord", sord);
                    //sp.AgregarParametro("inicio", inicio);
                    //sp.AgregarParametro("limite", limite);
                    ds = sp.EjecutarProcedimiento();
                    Debug.WriteLine("NRO DATOS COUNT" + ds.Tables.Count);
                    if (ds.Tables.Count > 0)
                    {
                        
                            return Int32.Parse(ds.Tables[1].Rows[0]["count"].ToString());
                        
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    return 0; ;
                }
            }

     public string ListarMonedas(int codemp)
            {
                string salida = ":" + "Seleccione";
                try
                {

                    DataSet ds = new DataSet();
                    DataSet ds2 = new DataSet();
                    StoredProcedure sp = new StoredProcedure("_Trae_Lista_Monedas");
                    sp.AgregarParametro("codemp", codemp);
                    
                    ds = sp.EjecutarProcedimiento();
                    Debug.WriteLine("TAMAÑO LISTA MONEDAS" + ds.Tables.Count);
                    //salida += ";" + "" + ":" + "Seleccione";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoredProcedure sp2 = new StoredProcedure("_Trae_Nombre_Monedas");

                        sp2.AgregarParametro("codemp", codemp);
                        sp2.AgregarParametro("id", ds.Tables[0].Rows[i][0].ToString());
                        ds2 = sp2.EjecutarProcedimiento();
                        Debug.WriteLine("SALIDA " + salida);

                        salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds2.Tables[0].Rows[0][0].ToString();
                        /*if (salida.Equals("undefined"))
                        {
                            salida = "Seleccione";
                        }*/
                    }

                    return salida;
                }
                catch (Exception ex)
                {
                    return salida;
                }

            }
        }
    }


