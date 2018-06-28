using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dao
{
    public class Moneda
    {

        public List<dto.Moneda> ListarMonedaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Moneda> lstMoneda = new List<dto.Moneda>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Monedas_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
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
                        lstMoneda.Add(new dto.Moneda()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Simbolo = ds.Tables[0].Rows[i]["Simbolo"].ToString(),
                            MonedaDefault = ds.Tables[0].Rows[i]["MonedaDefault"].ToString().ToUpper() == "S" ? "ON" : "OFF",
                            Porcentaje = ds.Tables[0].Rows[i]["Porcentaje"].ToString(),
                            Decimales = Int16.Parse(ds.Tables[0].Rows[i]["Decimales"].ToString()),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMoneda;
        }

        public List<dto.Moneda> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Moneda> lstMoneda = new List<dto.Moneda>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Monedas_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
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
                        lstMoneda.Add(new dto.Moneda()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Simbolo = ds.Tables[0].Rows[i]["Simbolo"].ToString(),
                            MonedaDefault = ds.Tables[0].Rows[i]["MonedaDefault"].ToString(),
                            Porcentaje = ds.Tables[0].Rows[i]["Porcentaje"].ToString(),
                            Decimales = Int16.Parse(ds.Tables[0].Rows[i]["Decimales"].ToString()),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMoneda;
        }

        public static int ListarMonedaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Monedas_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
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

        public void InsertarMoneda(dto.Moneda objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Monedas");
                sp.AgregarParametro("mon_codemp", codemp);
                sp.AgregarParametro("mon_nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("mon_simbolo", objAccion.Simbolo);
                sp.AgregarParametro("mon_default", objAccion.MonedaDefault.ToUpper() == "ON" || objAccion.MonedaDefault.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("mon_porcint",  string.IsNullOrEmpty(objAccion.Porcentaje) ? "0" : objAccion.Porcentaje.Replace(",", "."));
                sp.AgregarParametro("mon_decimales", objAccion.Decimales);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarMoneda(dto.Moneda objAccion, int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Monedas");
                sp.AgregarParametro("mon_codemp", codemp);
                sp.AgregarParametro("mon_codmon", id);
                sp.AgregarParametro("mon_nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("mon_simbolo", objAccion.Simbolo);
                sp.AgregarParametro("mon_default", objAccion.MonedaDefault.ToUpper() == "ON" || objAccion.MonedaDefault.ToUpper() == "YES" ? "S" : "N");
                sp.AgregarParametro("mon_porcint", string.IsNullOrEmpty(objAccion.Porcentaje) ? "0" : objAccion.Porcentaje.Replace(",", "."));
                sp.AgregarParametro("mon_decimales", objAccion.Decimales);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarMoneda(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Monedas");
                sp.AgregarParametro("mon_codemp", codemp);
                sp.AgregarParametro("mon_codmon", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
