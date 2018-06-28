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
    public class Clausula
    {
        public static List<dto.Clausula> ListarClausulasGrilla(int codemp, int idioma, int _id)
        {
            List<dto.Clausula> lst = new List<dto.Clausula>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Clausulas_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("ccl_cctid", _id);

                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS DATA:" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Clausula()
                        {
                            id = Int32.Parse(ds.Tables[0].Rows[i]["ccl_clcid"].ToString()),
                            cli_nombre = ds.Tables[0].Rows[i]["cli_nombre"].ToString(),
                            idCCT = Int32.Parse(ds.Tables[0].Rows[i]["idCCT"].ToString())

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

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

        public static string ListarClausulasTodas(int codemp, int idioma)
        {
            string salida = ":" + "Seleccione";
            try
            {

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ClausulasID_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);

                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS LISSSSSSSSSSSSSSSSSSSSSSSSSS" + ds.Tables.Count);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StoredProcedure sp2 = new StoredProcedure("_Listar_ClausulasNombre_Grilla");

                    sp2.AgregarParametro("codemp", codemp);
                    sp2.AgregarParametro("idioma", idioma);
                    sp2.AgregarParametro("ccl_clcid", ds.Tables[0].Rows[i][0].ToString());
                    ds2 = sp2.EjecutarProcedimiento();
                    Debug.WriteLine("TAMAÑO DS LIS2" + ds2.Tables.Count);

                    salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds2.Tables[0].Rows[0][0].ToString() ;

                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }

        }

        public void Borrar(int codemp, int id, int idCCT)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Contratos_Cartera_Clausulas");
                sp.AgregarParametro("ccl_codemp", codemp);
                sp.AgregarParametro("ccl_cctid", idCCT);
                sp.AgregarParametro("ccl_clcid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public void Editar(dto.Clausula obj, int codemp, string id)
        {
            
        }

        public void Insertar(int codemp, int id, int idCCT)
        {
            int num = 0;
            try
            {
                if (idCCT > 0)
                {

                    StoredProcedure sp = new StoredProcedure("Insertar_Contratos_Cartera_Clausulas");
                    sp.AgregarParametro("ccl_codemp", codemp);
                    sp.AgregarParametro("ccl_cctid", idCCT);
                    sp.AgregarParametro("ccl_clcid", id);
                    int error = sp.EjecutarProcedimientoTrans();
                }
                else
                {
                    
                    //StoredProcedure sp2 = new StoredProcedure("Insertar_Contratos_Cartera");
                    StoredProcedure sp7 = new StoredProcedure("UltNum_Contratos_Cartera");
                    sp7.AgregarParametro("cct_codemp", codemp);
                    DataSet dsnum = new DataSet();
                    dsnum = sp7.EjecutarProcedimiento();

                    num = Int32.Parse(dsnum.Tables[0].Rows[0][0].ToString());
                    Debug.WriteLine("AGREGAR CON CCT MAYOR" + num + "-" + id);
                    /*
                    sp2.AgregarParametro("cct_codemp", codemp);
                    sp2.AgregarParametro("cct_cctid", num);
                    sp2.AgregarParametro("cct_nombre", nom);
                    sp2.AgregarParametro("cct_tipo", tipo);
                    int error = sp2.EjecutarProcedimientoTrans();
                     */
                    StoredProcedure sp = new StoredProcedure("Insertar_Contratos_Cartera_Clausulas");
                    sp.AgregarParametro("ccl_codemp", codemp);
                    sp.AgregarParametro("ccl_cctid", num);
                    sp.AgregarParametro("ccl_clcid", id);
                    int error = sp.EjecutarProcedimientoTrans();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<Combobox> ListarClausulasTodas2(int codemp, int idioma, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_Clausulas_Todas");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["cli_nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ccl_clcid"].ToString()
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
