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
    public class FormasDePago
    {
        public List<dto.FormasDePago> ListarGrilla(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.FormasDePago> lstPeriodos = new List<dto.FormasDePago>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_FormasDePago_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                
                ds = sp.EjecutarProcedimiento();
                //

                
                if (ds.Tables.Count > 0)
                {
                    
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        
                        lstPeriodos.Add(new dto.FormasDePago()
                        {

                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Idioma = Int16.Parse(ds.Tables[1].Rows[i]["Idioma"].ToString()),
                            IdFP = Int16.Parse(ds.Tables[1].Rows[i]["IdFP"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["Nombre"].ToString(),
                            DiasVenc = Int16.Parse(ds.Tables[1].Rows[i]["DiasVenc"].ToString()),
                            IngFV = convertirTrueFalse(ds.Tables[1].Rows[i]["IngFV"].ToString()),
                            IngCuotas = convertirTrueFalse(ds.Tables[1].Rows[i]["IngCuotas"].ToString()),
                            Tipo = validaNULL(ds.Tables[1].Rows[i]["Tipo"].ToString())

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

        public string convertirTrueFalse(bool val)
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

        public string validaNULL(string val)
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


        public List<dto.FormasDePago> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.FormasDePago> lstPeriodos = new List<dto.FormasDePago>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_FormasDePago_Grilla");
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
                        lstPeriodos.Add(new dto.FormasDePago()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Idioma = Int16.Parse(ds.Tables[1].Rows[i]["Idioma"].ToString()),
                            IdFP = Int16.Parse(ds.Tables[1].Rows[i]["IdFP"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["Nombre"].ToString(),
                            DiasVenc = Int16.Parse(ds.Tables[1].Rows[i]["DiasVenc"].ToString()),
                            IngFV = convertirTrueFalse(ds.Tables[1].Rows[i]["IngFV"].ToString()),
                            IngCuotas = convertirTrueFalse(ds.Tables[1].Rows[i]["IngCuotas"].ToString()),
                            Tipo = validaNULL(ds.Tables[1].Rows[i]["Tipo"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPeriodos;
        }


        public void Insertar(dto.FormasDePago objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Formas_Pago");
                sp.AgregarParametro("frp_codemp", codemp);
                sp.AgregarParametro("frp_idioma", idioma);
                sp.AgregarParametro("frp_nombre", objAccion.Nombre);
                sp.AgregarParametro("frp_diasvenc", objAccion.DiasVenc);
                
                sp.AgregarParametro("frp_fecesp", convertirTrueFalse(objAccion.IngFV));
                sp.AgregarParametro("frp_cuotas", convertirTrueFalse(objAccion.IngCuotas));
                sp.AgregarParametro("frp_tipcpbt", objAccion.Tipo);
                Debug.WriteLine("INGFV " + objAccion.IngFV + " - " + "TIPO" + objAccion.Tipo);
                //Debug.WriteLine("DATOS A INSERTAR " + codemp + idioma + objAccion.Nombre + objAccion.DiasVenc + objAccion.IngFV + objAccion.IngCuotas + objAccion.Tipo);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Borrar(int codemp, int? id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Formas_Pago");
                //Debug.WriteLine("ID FORMA PAGO " + id);
                sp.AgregarParametro("frp_codemp", codemp);
                sp.AgregarParametro("frp_frpid", id);
                int error = sp.EjecutarProcedimientoTrans();
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.FormasDePago objAccion, int codemp, int id, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Formas_Pago");
                sp.AgregarParametro("frp_codemp", codemp);
                sp.AgregarParametro("frp_frpid", id);
                sp.AgregarParametro("frp_nombre", objAccion.Nombre);
                sp.AgregarParametro("frp_diasvenc", objAccion.DiasVenc);
                sp.AgregarParametro("frp_fecesp", convertirTrueFalse(objAccion.IngFV));
                sp.AgregarParametro("frp_cuotas", convertirTrueFalse(objAccion.IngCuotas));
                sp.AgregarParametro("frp_tipcpbt", objAccion.Tipo);
                int error = sp.EjecutarProcedimientoTrans();

                StoredProcedure sp2 = new StoredProcedure("Update_Formas_Pago_Idiomas");
                sp2.AgregarParametro("fpi_codemp", codemp);
                sp2.AgregarParametro("fpi_frpid", id);
                sp2.AgregarParametro("fpi_idid", idioma);
                sp2.AgregarParametro("fpi_nombre", objAccion.Nombre);
                int error2 = sp2.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ListarFormasDePagoCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_FormasDePago_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
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

        public List<string> ListarTiposDocCaja(int codemp)
        {
            List<string> lst = new List<string>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Agrupa_TiposDocumentosCaja");
                Debug.WriteLine("INICIA Trae_Agrupa_TiposDocCaja" + codemp );
                sp.AgregarParametro("codemp", codemp);
                
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i][0].ToString());

                        
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public string ListarTiposDocCaja2(int codemp)
        {
            string salida = ":" + "Seleccione";
            try
            {

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Lista_TiposDocumentosCaja");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS" + ds.Tables.Count);
                
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        StoredProcedure sp2 = new StoredProcedure("_Trae_Nombre_TiposDocumentosCaja");

                        sp2.AgregarParametro("codemp", codemp);
                        sp2.AgregarParametro("id", ds.Tables[0].Rows[i][0].ToString());
                        ds2 = sp2.EjecutarProcedimiento();

                        salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds2.Tables[0].Rows[0][0].ToString();
                
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


