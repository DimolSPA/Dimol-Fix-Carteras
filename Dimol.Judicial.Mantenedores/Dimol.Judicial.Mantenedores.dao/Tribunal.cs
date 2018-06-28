using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class Tribunal
    {
        public string ListarTiposTribunal(int codemp, int idioma)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_TipoTribunal");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);

                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();

                    if (i == 0)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                }
                salida = salida.Replace("\"", "'");
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string ListarBancos(int codemp)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Bancos");
                sp.AgregarParametro("codemp", codemp);
              
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();

                    if (i == 0)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                }
                salida = salida.Replace("\"", "'");
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public List<dto.Tribunal> ListarTribunalGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Tribunal> lstTribunal = new List<dto.Tribunal>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tribunales_Grilla");
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
                        lstTribunal.Add(new dto.Tribunal()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            IdTipo=Int16.Parse(ds.Tables[0].Rows[i]["IdTipo"].ToString()),
                            TipoTribunal = ds.Tables[0].Rows[i]["TipoTribunal"].ToString(),
                            IdComuna=Int16.Parse(ds.Tables[0].Rows[i]["IdComuna"].ToString()),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            Telefono1 = ds.Tables[0].Rows[i]["Telefono1"].ToString(),
                            Telefono2 = ds.Tables[0].Rows[i]["Telefono2"].ToString(),
                            Fax = ds.Tables[0].Rows[i]["Fax"].ToString(),
                            Email = ds.Tables[0].Rows[i]["Email"].ToString(),
                            IdBanco=Int16.Parse(ds.Tables[0].Rows[i]["IdBanco"].ToString()),
                            Banco = ds.Tables[0].Rows[i]["Banco"].ToString(),
                            CuentaCorriente= ds.Tables[0].Rows[i]["CuentaCorriente"].ToString()
            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstTribunal;
        }

        public List<dto.Tribunal> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Tribunal> lstTribunal = new List<dto.Tribunal>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tribunales_Grilla");
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
                        lstTribunal.Add(new dto.Tribunal()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            IdTipo = Int16.Parse(ds.Tables[0].Rows[i]["IdTipo"].ToString()),
                            TipoTribunal = ds.Tables[0].Rows[i]["TipoTribunal"].ToString(),
                            IdComuna = Int16.Parse(ds.Tables[0].Rows[i]["IdComuna"].ToString()),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            Telefono1 = ds.Tables[0].Rows[i]["Telefono1"].ToString(),
                            Telefono2 = ds.Tables[0].Rows[i]["Telefono2"].ToString(),
                            Fax = ds.Tables[0].Rows[i]["Fax"].ToString(),
                            Email = ds.Tables[0].Rows[i]["Email"].ToString(),
                            IdBanco = Int16.Parse(ds.Tables[0].Rows[i]["IdBanco"].ToString()),
                            Banco = ds.Tables[0].Rows[i]["Banco"].ToString(),
                            CuentaCorriente = ds.Tables[0].Rows[i]["CuentaCorriente"].ToString()

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstTribunal;
        }

        public static int ListarTribunalGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tribunales_Grilla_Count");
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

        public void InsertarTribunal(dto.Tribunal objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Tribunales");
                sp.AgregarParametro("trb_codemp", codemp);
                sp.AgregarParametro("trb_rut", (object)objAccion.Rut.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("trb_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("trb_ttbid", (object)objAccion.IdTipo ?? DBNull.Value);
                sp.AgregarParametro("trb_comid", (object)objAccion.IdComuna ?? DBNull.Value);
                sp.AgregarParametro("trb_direccion", (object)objAccion.Direccion.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("trb_telefono1", (object)objAccion.Telefono1 ?? DBNull.Value);
                sp.AgregarParametro("trb_telefono2", (object)objAccion.Telefono2 ?? DBNull.Value);
                sp.AgregarParametro("trb_fax", (object)objAccion.Fax ?? DBNull.Value);
                sp.AgregarParametro("trb_email", (object)objAccion.Email.ToLower() ?? DBNull.Value);
                sp.AgregarParametro("trb_bcoid", (object)objAccion.IdBanco ?? DBNull.Value);
                sp.AgregarParametro("trb_ctacte", (object)objAccion.CuentaCorriente ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarTribunal(dto.Tribunal objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Tribunales");
                sp.AgregarParametro("trb_codemp", codemp);
                sp.AgregarParametro("trb_trbid", objAccion.Id);
                sp.AgregarParametro("trb_rut", (object)objAccion.Rut.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("trb_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("trb_ttbid", (object)objAccion.IdTipo ?? DBNull.Value);
                sp.AgregarParametro("trb_comid", (object)objAccion.IdComuna ?? DBNull.Value);
                sp.AgregarParametro("trb_direccion", (object)objAccion.Direccion.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("trb_telefono1", (object)objAccion.Telefono1 ?? DBNull.Value);
                sp.AgregarParametro("trb_telefono2", (object)objAccion.Telefono2 ?? DBNull.Value);
                sp.AgregarParametro("trb_fax", (object)objAccion.Fax ?? DBNull.Value);
                sp.AgregarParametro("trb_email", (object)objAccion.Email.ToLower() ?? DBNull.Value);
                sp.AgregarParametro("trb_bcoid", (object)objAccion.IdBanco ?? DBNull.Value);
                sp.AgregarParametro("trb_ctacte", (object)objAccion.CuentaCorriente ?? DBNull.Value);

                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarTribunal(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Tribunales");
                sp.AgregarParametro("trb_codemp", codemp);
                sp.AgregarParametro("trb_trbid", id);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
