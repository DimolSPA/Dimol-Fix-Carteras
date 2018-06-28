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
    public class ClausulaContratoCartera
    {

        public static List<dto.ClausulaContratoCartera> ListarClausulaContratoCarteraGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ClausulaContratoCartera> lst = new List<dto.ClausulaContratoCartera>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ClausulasContratoCartera_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                //Debug.WriteLine("TAMAÑO DS DATA:" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        lst.Add(new dto.ClausulaContratoCartera()
                        {
                            clc_clcid = Int16.Parse(ds.Tables[1].Rows[i]["clc_clcid"].ToString()),
                            clc_nombre = ds.Tables[1].Rows[i]["clc_nombre"].ToString(),
                            TipoPorId = ListarTipoPorId(idioma, Int16.Parse(ds.Tables[1].Rows[i]["clc_tipo"].ToString())),
                            TipoAplicacionPorId = ListarTipoAplicacionPorId(idioma, ds.Tables[1].Rows[i]["clc_porcmon"].ToString()),
                            tipo = ds.Tables[1].Rows[i]["tipo"].ToString(),
                            clc_porcmon = ds.Tables[1].Rows[i]["clc_porcmon"].ToString(),
                            clc_tipo = Int16.Parse(validaNULL(ds.Tables[1].Rows[i]["clc_tipo"].ToString())),
                            Area = validaNULL(ds.Tables[1].Rows[i]["Area"].ToString()),
                            clc_valor = Decimal.Parse(ds.Tables[1].Rows[i]["clc_valor"].ToString()),
                            clc_rango = convertirTrueFalse(ds.Tables[1].Rows[i]["clc_rango"].ToString()),
                            ValorFijo = convertirTrueFalse(ds.Tables[1].Rows[i]["clc_fija"].ToString()),
                            Capital = convertirTrueFalse(ds.Tables[1].Rows[i]["CLC_FACCAP"].ToString()),
                            Interes = convertirTrueFalse(ds.Tables[1].Rows[i]["CLC_FACINT"].ToString()),
                            Honorario = convertirTrueFalse(ds.Tables[1].Rows[i]["CLC_FACHON"].ToString()),
                            GastoPrejudicial = convertirTrueFalse(ds.Tables[1].Rows[i]["CLC_FACGPRE"].ToString()),
                            GastoJudicial = convertirTrueFalse(ds.Tables[1].Rows[i]["CLC_FACGJUD"].ToString()),
                            AnulaMaximaConvencional = convertirTrueFalse(ds.Tables[1].Rows[i]["CLC_ANUMAX"].ToString()),
                            tip_rango = ListarTipoRangoPorId(idioma, ds.Tables[1].Rows[i]["CLC_TIPRANGO"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static string validaNULL(string val)
        {

            if (val != null && val != "")
            {
                return val;
            }
            else
            {
                return "0";
            }
        }

        public static bool convertirTrueFalse(string val)
        {
            bool returnval = true;
            if (val.Equals("S") && val != null)
            {
                //Debug.WriteLine("ENTRO AL TRUE");
                returnval = true;
            }
            else
            {
                //Debug.WriteLine("ENTRO AL FALSO");
                returnval = false;
            }
            return returnval;
        }

        
        public static int ListarClausulaContratoCarteraGrillaCount(int codemp, string where, string sidx, string sord)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ClausulasContratoCartera_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                ds = sp.EjecutarProcedimiento();
                //Debug.WriteLine("TAMAÑO DS :" + ds.Tables.Count);
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

        public static List<Combobox> ListarTipos(int idioma, string first)
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

                for (int i = 1; i < 11; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipCla" + i);
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

        public static List<Combobox> ListarTiposAplicacion(int idioma, string first)
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

                for (int i = 1; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipApl" + i);
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

        public static List<Combobox> ListarAreas(int idioma, string first)
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

                for (int i = 1; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipArea" + i);
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

        public static List<Combobox> ListarTiposRango(int idioma, string first)
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

                for (int i = 1; i < 8; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipRang" + i);
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

        public static List<dto.ClausulaContratoCartera> ListarClausulaContratoCarteraPorID(int codemp, int idioma, int id)
        {
            List<dto.ClausulaContratoCartera> lst = new List<dto.ClausulaContratoCartera>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ClausulasContratoCartera_PorId");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id", id);
               
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS DATA:" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.ClausulaContratoCartera()
                        {
                            clc_clcid = Int16.Parse(ds.Tables[0].Rows[i]["clc_clcid"].ToString()),
                            clc_nombre = ds.Tables[0].Rows[i]["clc_nombre"].ToString(),
                            tipo = ds.Tables[0].Rows[i]["tipo"].ToString(),
                            TipoPorId = ListarTipoPorId(idioma,Int16.Parse(ds.Tables[0].Rows[i]["clc_tipo"].ToString())), 
                            clc_tipo = Int16.Parse(ds.Tables[0].Rows[i]["clc_tipo"].ToString()),
                            Area = ds.Tables[0].Rows[i]["Area"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static string ListarTipoPorId(int idioma, int id)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                
                
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipCla" + id);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    salida = ds.Tables[0].Rows[0][0].ToString();

                    //Debug.WriteLine("SALIDA" + salida + " ID" +id);
                             
                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }

        }

        public static string ListarTipoRangoPorId(int idioma, string id)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

               
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipRang" + id);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    salida = ds.Tables[0].Rows[0][0].ToString();

                    return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }

        }

        public static string ListarTipoAplicacionPorId(int idioma, string id)
        {
            string salida = "";
            int val = 0;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");


                sp = new StoredProcedure("Trae_Etiquetas");
                if(id.Equals("P"))
                {
                    val = 1;
                }
                else if (id.Equals("M"))
                {
                    val = 2;
                }
                else if (id.Equals("O"))
                {
                    val = 3;
                }
                sp.AgregarParametro("codigo", "TipApl" + val);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();
                salida = ds.Tables[0].Rows[0][0].ToString();

                Debug.WriteLine("SALIDA" + salida + " ID" + id);

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }

        }

        public static int GrabarClausulaContratoCartera(int codemp, int idioma, dto.ClausulaContratoCartera obj)
        {
            int num = 0;
            
            try
            {
                DataSet ds = new DataSet();
                DataSet dsnum = new DataSet();
                Debug.WriteLine("clc_codemp:" + codemp);
                Debug.WriteLine("idioma:" + idioma);
                Debug.WriteLine(" DATOS A INSERTAR :" + " ID :" + obj.clc_clcid);
                Debug.WriteLine(" NOMBRE:" + obj.clc_nombre);
                Debug.WriteLine(" TIPO:" + obj.clc_tipo);
                Debug.WriteLine(" clc_porcmon:" + obj.clc_porcmon);
                Debug.WriteLine(" MONEDA:" + obj.clc_codmon);
                Debug.WriteLine(" VALOR:" + obj.clc_valor);
                Debug.WriteLine(" RANGO:" + obj.clc_rango);
                Debug.WriteLine(" TIPORANGO:" + obj.TipoRango);
                Debug.WriteLine(" AREA:" + obj.Area);
                Debug.WriteLine(" CAPITAL:" + obj.Capital);
                Debug.WriteLine(" INTERES:" + obj.Interes);
                Debug.WriteLine(" HONORARIO:" + obj.Honorario);
                Debug.WriteLine(" PREJUDICIAL:" + obj.GastoPrejudicial);
                Debug.WriteLine(" JUDICIAL:" + obj.GastoJudicial);
                Debug.WriteLine(" FIJO:" + obj.ValorFijo); 
                Debug.WriteLine(" ANULAMAX:" + obj.AnulaMaximaConvencional);

                StoredProcedure sp = new StoredProcedure("_Insertar_Clausulas_ContCart");
                StoredProcedure spclonar = new StoredProcedure("Insertar_Clausulas_ContCart_Clon");
                StoredProcedure sp7 = new StoredProcedure("UltNum_Clausulas_ContCart");
                StoredProcedure sp4 = new StoredProcedure("Update_Clausulas_ContCart");
                StoredProcedure sp5 = new StoredProcedure("Update_Clausulas_ContCart_Idiomas");

                sp7.AgregarParametro("clc_codemp", codemp);
                dsnum = sp7.EjecutarProcedimiento();
                num = Int32.Parse(dsnum.Tables[0].Rows[0][0].ToString());

                if (obj.clc_clcid != 0 && obj.Clonar == true)
                {
                    Debug.WriteLine("UPDATE Y CLONAR");
                    spclonar.AgregarParametro("clc_codemp", codemp);
                    spclonar.AgregarParametro("clc_clcid", obj.clc_clcid);
                    spclonar.AgregarParametro("clc_clcidnew", num);
                    if(obj.clc_nombre.Equals("")){
                        spclonar.AgregarParametro("clc_nombre", obj.clc_nombre + "_Clon");
                    }
                    else{
                        spclonar.AgregarParametro("clc_nombre", obj.NombreClonar);
                    }

                }
                else if (obj.clc_clcid == 0 || obj.clc_clcid.Equals(""))
                {
                    Debug.WriteLine("INSERTAR");
                    sp.AgregarParametro("clc_codemp", codemp);
                    sp.AgregarParametro("idioma", idioma);
                    sp.AgregarParametro("clc_nombre", obj.clc_nombre);
                    sp.AgregarParametro("clc_tipo", obj.clc_tipo);
                    sp.AgregarParametro("clc_porcmon", obj.clc_porcmon);
                    if (obj.clc_codmon != null)
                    {
                        sp.AgregarParametro("clc_codmon", 2);
                    }
                    else
                    {
                        sp.AgregarParametro("clc_codmon", DBNull.Value);
                    }
                    if (obj.clc_rango == false)
                    {
                        sp.AgregarParametro("clc_valor", obj.clc_valor);
                    }
                    else
                    {
                        sp.AgregarParametro("clc_valor", 0);
                    }
                    if (obj.clc_rango == true)
                    {
                        sp.AgregarParametro("clc_rango", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("clc_rango", "N");
                    }
                    sp.AgregarParametro("clc_tiprango", obj.TipoRango);
                    sp.AgregarParametro("clc_prejud", obj.Area);

                    if (obj.Capital == true)
                    {
                        sp.AgregarParametro("clc_faccap", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("clc_faccap", "N");
                    }

                    if (obj.Interes == true)
                    {
                        sp.AgregarParametro("clc_facint", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("clc_facint", "N");
                    }

                    if (obj.Honorario == true)
                    {
                        sp.AgregarParametro("clc_fachon", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("clc_fachon", "N");
                    }

                    if (obj.GastoPrejudicial == true)
                    {
                        sp.AgregarParametro("clc_facgpre", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("clc_facgpre", "N");
                    }

                    if (obj.GastoJudicial == true)
                    {
                        sp.AgregarParametro("clc_facgjud", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("clc_facgjud", "N");
                    }

                    if (obj.ValorFijo == true)
                    {
                        sp.AgregarParametro("clc_fija", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("clc_fija", "N");
                    }

                    if (obj.AnulaMaximaConvencional == true)
                    {
                        sp.AgregarParametro("clc_anumax", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("clc_anumax", "N");
                    }

                    int error = sp.EjecutarProcedimientoTrans();
                }
                else
                {
                    Debug.WriteLine("UPDATE SIN CLONAR");
                    sp4.AgregarParametro("clc_codemp", codemp);
                    sp4.AgregarParametro("clc_clcid", obj.clc_clcid);
                    sp4.AgregarParametro("clc_nombre", obj.clc_nombre);
                    sp4.AgregarParametro("clc_tipo", obj.clc_tipo);
                    sp4.AgregarParametro("clc_porcmon", obj.clc_porcmon);
                    if (obj.clc_codmon != null)
                    {
                        sp4.AgregarParametro("clc_codmon", 2);
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_codmon", DBNull.Value);
                    }
                    if (obj.clc_rango == false)
                    {
                        sp4.AgregarParametro("clc_valor", obj.clc_valor);
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_valor", 0);
                    }
                    if (obj.clc_rango == true)
                    {
                        sp4.AgregarParametro("clc_rango", "S");
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_rango", "N");
                    }
                    sp4.AgregarParametro("clc_tiprango", obj.TipoRango);
                    sp4.AgregarParametro("clc_prejud", obj.Area);

                    if (obj.Capital == true)
                    {
                        sp4.AgregarParametro("clc_faccap", "S");
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_faccap", "N");
                    }

                    if (obj.Interes == true)
                    {
                        sp4.AgregarParametro("clc_facint", "S");
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_facint", "N");
                    }

                    if (obj.Honorario == true)
                    {
                        sp4.AgregarParametro("clc_fachon", "S");
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_fachon", "N");
                    }

                    if (obj.GastoPrejudicial == true)
                    {
                        sp4.AgregarParametro("clc_facgpre", "S");
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_facgpre", "N");
                    }

                    if (obj.GastoJudicial == true)
                    {
                        sp4.AgregarParametro("clc_facgjud", "S");
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_facgjud", "N");
                    }

                    if (obj.ValorFijo == true)
                    {
                        sp4.AgregarParametro("clc_fija", "S");
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_fija", "N");
                    }

                    if (obj.AnulaMaximaConvencional == true)
                    {
                        sp4.AgregarParametro("clc_anumax", "S");
                    }
                    else
                    {
                        sp4.AgregarParametro("clc_anumax", "N");
                    }

                    int error2 = sp4.EjecutarProcedimientoTrans();

                    sp5.AgregarParametro("cli_codemp", codemp);
                    sp5.AgregarParametro("cli_clcid", obj.clc_clcid);
                    sp5.AgregarParametro("cli_idid", idioma);
                    sp5.AgregarParametro("cli_nombre", obj.clc_nombre);

                    int error3 = sp5.EjecutarProcedimientoTrans();
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
