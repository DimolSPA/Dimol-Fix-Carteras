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
    public class MaximaConvencional
    {
        public static List<dto.MaximaConvencional> ListarMaximaConvencionalGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MaximaConvencional> lstAcciones = new List<dto.MaximaConvencional>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_MaximaConvencional_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
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
                        lstAcciones.Add(new dto.MaximaConvencional()
                        {
                            MXC_CODEMP = Int16.Parse(ds.Tables[1].Rows[i]["MXC_CODEMP"].ToString()),
                            MXC_MXCID = Int16.Parse(ds.Tables[1].Rows[i]["mxc_mxcid"].ToString()),
                            MXC_TPCID = Int16.Parse(ds.Tables[1].Rows[i]["MXC_TPCID"].ToString()),
                            MXC_TIPO = ds.Tables[1].Rows[i]["MXC_TIPO"].ToString(),
                            MXC_APLICA = ds.Tables[1].Rows[i]["MXC_APLICA"].ToString(),
                            MXC_CODMON = Int16.Parse(ds.Tables[1].Rows[i]["MXC_CODMON"].ToString()),
                            MXC_DESDE = Decimal.Parse(ds.Tables[1].Rows[i]["MXC_DESDE"].ToString()),
                            MXC_HASTA = Decimal.Parse(ds.Tables[1].Rows[i]["MXC_HASTA"].ToString()),
                            MXC_VALOR = Decimal.Parse(ds.Tables[1].Rows[i]["MXC_VALOR"].ToString()),
                            TCI_NOMBRE = ds.Tables[1].Rows[i]["tci_nombre"].ToString()
                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstAcciones;
        }

        public static int ListarMaximaConvencionalGrillaCount(int codemp, int idioma, string where, string sidx, string sord)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_MaximaConvencional_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
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

        public static bool convertirTrueFalse(string val)
        {
            bool returnval = true;
            if (val.Equals("S") && val != null)
            {
                returnval = true;
            }
            else
            {
                returnval = false;
            }
            return returnval;
        }

        public static string convertirTrueFalse(bool val)
        {
            string returnval = "S";
            if (val == true)
            {
                returnval = "S";
            }
            else
            {
                returnval = "N";
            }
            return returnval;
        }

        public static List<Combobox> ListarTiposDocumentos(int codemp, int idioma, string first)
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
                StoredProcedure sp = new StoredProcedure("Trae_Tipos_Documentos_Deudor");
                sp.AgregarParametro("clb_codemp", codemp);
                sp.AgregarParametro("tci_idid", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["tci_nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["tci_tpcid"].ToString()
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

        public static string ListarTiposDocumentos(int codemp, int idioma)
        {
            string salida = ":" + "Seleccione";
            try
            {

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Lista_Tipos_Documentos_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);

                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS LIS" + ds.Tables.Count);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StoredProcedure sp2 = new StoredProcedure("_Trae_Nombre_Tipos_Documentos_Deudor");

                    sp2.AgregarParametro("codemp", codemp);
                    sp2.AgregarParametro("idioma", idioma);
                    sp2.AgregarParametro("id", ds.Tables[0].Rows[i][0].ToString());
                    ds2 = sp2.EjecutarProcedimiento();
                    Debug.WriteLine("TAMAÑO DS LIS2" + ds2.Tables.Count);

                    salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds2.Tables[0].Rows[0][0].ToString();

                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }

        }

        public static string ListarTipos(int codemp, int idid)
        {
            string salida = "";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 3; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");

                    sp.AgregarParametro("codigo", "TipMax" + i);
                    sp.AgregarParametro("idioma", idid);
                    ds = sp.EjecutarProcedimiento();
                    //Debug.WriteLine("TAMAÑO REPORTE PADRE " + ds.Tables.Count + "-" + idid);
                    if (i == 1)
                    {
                        salida += i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }

                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string ListarMonedas(int codemp)
        {
            string salida = ":" + "Seleccione";
            try
            {

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Lista_Monedas");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idioma", idioma);

                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS LIS" + ds.Tables.Count);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StoredProcedure sp2 = new StoredProcedure("_Trae_Nombre_Monedas");

                    sp2.AgregarParametro("codemp", codemp);
                    //sp2.AgregarParametro("idioma", idioma);
                    sp2.AgregarParametro("id", ds.Tables[0].Rows[i][0].ToString());
                    ds2 = sp2.EjecutarProcedimiento();
                    Debug.WriteLine("TAMAÑO DS LIS2" + ds2.Tables.Count);

                    salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds2.Tables[0].Rows[0][0].ToString();

                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }

        }

        public static string ListarAplica(int codemp, int idid)
        {
            string salida = "";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 3; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");

                    sp.AgregarParametro("codigo", "AplMx" + i);
                    sp.AgregarParametro("idioma", idid);
                    ds = sp.EjecutarProcedimiento();
                    //Debug.WriteLine("TAMAÑO REPORTE PADRE " + ds.Tables.Count + "-" + idid);
                    if (i == 1)
                    {
                        salida += i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }

                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public void Borrar(int codemp, int? id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Maxima_Convencional");
                sp.AgregarParametro("mxc_codemp", codemp);
                sp.AgregarParametro("mxc_mxcid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.MaximaConvencional obj, int codemp, int id)
        {
            try
            {
                string tipo = "";
                string aplica = "";
               
                StoredProcedure sp = new StoredProcedure("Update_Maxima_Convencional");
                sp.AgregarParametro("mxc_codemp", codemp);
                sp.AgregarParametro("mxc_mxcid", id);
                sp.AgregarParametro("mxc_tpcid", obj.TCI_NOMBRE);
                if (obj.MXC_TIPO.Equals("1"))
                {
                    tipo = "M";
                }
                else 
                {
                    tipo = "C";
                }
                sp.AgregarParametro("mxc_tipo", tipo);
                if (obj.MXC_APLICA.Equals("1"))
                {
                    aplica = "H";
                }
                else
                {
                    aplica = "I";
                }
                sp.AgregarParametro("mxc_aplica", aplica);
                sp.AgregarParametro("mxc_codmon", obj.MXC_CODMON);
                sp.AgregarParametro("mxc_desde", obj.MXC_DESDE);
                sp.AgregarParametro("mxc_hasta", obj.MXC_HASTA);
                sp.AgregarParametro("mxc_valor", obj.MXC_VALOR);

                Debug.WriteLine("mxc_mxcid:" + id + " MXC_TIPO:" + tipo +
                   " MXC_APLICA:" + aplica + " MXC_CODMON:" + obj.MXC_CODMON + " TCI_NOMBRE:" + obj.TCI_NOMBRE +
                   " DESDE:" + obj.MXC_DESDE + " HASTA:" + obj.MXC_HASTA + " VALOR:" + obj.MXC_VALOR);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insertar(dto.MaximaConvencional obj, int codemp)
        {
            string tipo = "";
            string aplica = "";
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Maxima_Convencional");

                sp.AgregarParametro("mxc_codemp", codemp);
                sp.AgregarParametro("mxc_tpcid", obj.TCI_NOMBRE);
                if (obj.MXC_TIPO.Equals("1"))
                {
                    tipo = "M";
                }
                else
                {
                    tipo = "C";
                }
                sp.AgregarParametro("mxc_tipo", tipo);
                if (obj.MXC_APLICA.Equals("1"))
                {
                    aplica = "H";
                }
                else
                {
                    aplica = "I";
                }
                
                sp.AgregarParametro("mxc_aplica", aplica);
                sp.AgregarParametro("mxc_codmon", obj.MXC_CODMON);
                sp.AgregarParametro("mxc_desde", obj.MXC_DESDE);
                sp.AgregarParametro("mxc_hasta", obj.MXC_HASTA);
                sp.AgregarParametro("mxc_valor", obj.MXC_VALOR);
                Debug.WriteLine(" MXC_TIPO:" + tipo +
                   " MXC_APLICA:" + aplica + " MXC_CODMON:" + obj.MXC_CODMON + " TCI_NOMBRE:" + obj.TCI_NOMBRE +
                   " DESDE:" + obj.MXC_DESDE + " HASTA:" + obj.MXC_HASTA + " VALOR:" + obj.MXC_VALOR);
                
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
