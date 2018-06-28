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
    public class Impuesto
    {
        public List<dto.Impuesto> ListarGrilla(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Impuesto> lstPeriodos = new List<dto.Impuesto>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Impuestos");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                //Debug.WriteLine("PARAMETROS SP " + codemp + " " + where + " " + sidx + " " + sord + " " + inicio + " " + limite);
                ds = sp.EjecutarProcedimiento();
                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    //Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.Impuesto()
                        {

                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            NombreCorto = ds.Tables[1].Rows[i]["nombreCorto"].ToString(),
                            idPlanDeCuentas = Int32.Parse(ds.Tables[1].Rows[i]["idPlanDeCuentas"].ToString()),
                            NombreCC = ds.Tables[1].Rows[i]["nombrePlanDeCuentas"].ToString(),
                            Retenido = convertirTrueFalse(ds.Tables[1].Rows[i]["retenido"].ToString()),
                            Monto = Decimal.Parse(ds.Tables[1].Rows[i]["monto"].ToString())
                            
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


        public List<dto.Impuesto> ExportarExcel(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Impuesto> lstPeriodos = new List<dto.Impuesto>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Impuestos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
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
                        lstPeriodos.Add(new dto.Impuesto()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            NombreCorto = ds.Tables[1].Rows[i]["nombreCorto"].ToString(),
                            idPlanDeCuentas = Int32.Parse(ds.Tables[1].Rows[i]["IdImpuesto"].ToString()),
                            NombreCC = ds.Tables[1].Rows[i]["nombrePlanDeCuentas"].ToString(),
                            Retenido = convertirTrueFalse(ds.Tables[1].Rows[i]["retenido"].ToString()),
                            Monto = Decimal.Parse(ds.Tables[1].Rows[i]["monto"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPeriodos;
        }


        public void Insertar(dto.Impuesto objAccion, int codemp)
        {
            
            DataSet ds = new DataSet();
            StoredProcedure sp = new StoredProcedure("Insertar_Impuestos");
            StoredProcedure sp7 = new StoredProcedure("UltNum_Impuestos");
            int dsnum = 0;
            try
            {
                sp7.AgregarParametro("ipt_codemp", codemp);
                ds = sp7.EjecutarProcedimiento();
                Debug.WriteLine("NRO DATOS SP7" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    dsnum = Int16.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                sp.AgregarParametro("ipt_codemp", codemp);
                sp.AgregarParametro("ipt_iptid", dsnum);
                sp.AgregarParametro("ipt_nombre", objAccion.Nombre);
                sp.AgregarParametro("ipt_nomcort", objAccion.NombreCorto);

                //PENDIENTE...AGREGAR COMBOBOX EN CUENTA CONTABLE
                sp.AgregarParametro("ipt_pctid", 50);
                sp.AgregarParametro("ipt_retenido", this.convertirTrueFalse(objAccion.Retenido));
                sp.AgregarParametro("ipt_monto", objAccion.Monto);
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
                StoredProcedure sp = new StoredProcedure("Delete_Impuestos");
                sp.AgregarParametro("ipt_codemp", codemp);
                sp.AgregarParametro("ipt_iptid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.Impuesto objAccion, int codemp, int id, int idCC)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Impuestos");
                sp.AgregarParametro("ipt_codemp", codemp);
                sp.AgregarParametro("ipt_iptid", id);
                sp.AgregarParametro("ipt_nomcort", objAccion.NombreCorto);
                sp.AgregarParametro("ipt_nombre", objAccion.Nombre);
                //PENDIENTE POR EL MOMENTO
                sp.AgregarParametro("ipt_pctid", 68);
                sp.AgregarParametro("ipt_retenido", this.convertirTrueFalse(objAccion.Retenido));
                sp.AgregarParametro("ipt_monto", objAccion.Monto);
                Debug.WriteLine("DATOS EDITAR IMPUESTO : " + codemp + "-" + id + "-" + objAccion.NombreCorto + "-" + objAccion.Nombre + "-" + idCC + "-" + objAccion.Retenido + "-" + objAccion.Monto);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ListarCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Impuestos_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                //sp.AgregarParametro("inicio", inicio);
                //sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                //Debug.WriteLine("NRO DATOS COUNT" + ds.Tables.Count);
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

        public string ListarCuentasContables(int codemp)
        {
            string salida = ":" + "Seleccione";
            try
            {

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Lista_CuentaContable");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO LISTA CUENTAS CONTABLES" + ds.Tables.Count);
                //salida += ";" + "" + ":" + "Seleccione";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StoredProcedure sp2 = new StoredProcedure("_Trae_Nombre_CuentaContable");

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
