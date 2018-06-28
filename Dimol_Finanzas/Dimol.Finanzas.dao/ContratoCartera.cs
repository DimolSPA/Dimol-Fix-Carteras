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
    public class ContratoCartera
    {
        public static List<dto.ContratoCartera> ListarContratoCarteraGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ContratoCartera> lst = new List<dto.ContratoCartera>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ContratoCartera_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
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
                        lst.Add(new dto.ContratoCartera()
                        {
                            cct_codemp = Int16.Parse(ds.Tables[1].Rows[i]["cct_codemp"].ToString()),
                            cct_cctid = Int16.Parse(ds.Tables[1].Rows[i]["cct_cctid"].ToString()),
                            cct_nombre = ds.Tables[1].Rows[i]["cct_nombre"].ToString(),
                            cct_tipo = ds.Tables[1].Rows[i]["cct_tipo"].ToString(),
                            tipo = ds.Tables[1].Rows[i]["Tipo"].ToString()
                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static int ListarContratoCarteraGrillaCount(int codemp, int idioma, string where, string sidx, string sord)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ContratoCartera_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
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

                for (int i = 1; i < 3; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipCart" + i);
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

        /*
        public static List<dto.Clausula> ListarClausulasGrilla(int codemp, int idioma, int id)
        {
            List<dto.Clausula> lst = new List<dto.Clausula>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Clausulas_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("ccl_cctid", id);
                
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS DATA:" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Clausula()
                        {

                            id = Int16.Parse(ds.Tables[0].Rows[i]["ccl_clcid"].ToString()),
                            cli_nombre = ds.Tables[0].Rows[i]["cli_nombre"].ToString()
                            

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }
        */
        public static int ListarClausulasGrillaCount(int codemp, int idioma, int id, string where, string sidx, string sord)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Clausulas_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("ccl_cctid", id);
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

        public static void BorrarContratoCartera(int codemp, int id)
        {
            try
            {
                Debug.WriteLine("ENTRO A BORRAR CONTRATO CARTERA " + codemp + "-" + id);
                StoredProcedure sp = new StoredProcedure("Delete_Contratos_Cartera");
                sp.AgregarParametro("cct_codemp", codemp);
                sp.AgregarParametro("cct_cctid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void GuardarTodoClausulas(int codemp, string nom, string tipo)
        {
            int num = 0;
            try
            {
                
                    StoredProcedure sp2 = new StoredProcedure("Insertar_Contratos_Cartera");
                    StoredProcedure sp7 = new StoredProcedure("UltNum_Contratos_Cartera");
                    sp7.AgregarParametro("cct_codemp", codemp);
                    DataSet dsnum = new DataSet();
                    dsnum = sp7.EjecutarProcedimiento();

                    num = Int32.Parse(dsnum.Tables[0].Rows[0][0].ToString());

                    
                    sp2.AgregarParametro("cct_codemp", codemp);
                    sp2.AgregarParametro("cct_cctid", num);
                    sp2.AgregarParametro("cct_nombre", nom);
                    sp2.AgregarParametro("cct_tipo", tipo);
                    int error = sp2.EjecutarProcedimientoTrans();
                     
                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
