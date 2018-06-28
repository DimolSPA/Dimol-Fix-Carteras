using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class Notaria
    {

        public List<dto.Notaria> ListarNotariaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Notaria> lstNotaria = new List<dto.Notaria>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Notarias_Grilla");
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
                        lstNotaria.Add(new dto.Notaria()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            NombreNotaria = ds.Tables[0].Rows[i]["NombreNotaria"].ToString(),
                            IdComuna = Int16.Parse(ds.Tables[0].Rows[i]["IdComuna"].ToString()),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            Telefono1 = ds.Tables[0].Rows[i]["Telefono1"].ToString(),
                            Telefono2 = ds.Tables[0].Rows[i]["Telefono2"].ToString(),
                            Fax = ds.Tables[0].Rows[i]["Fax"].ToString(),
                            Celular = ds.Tables[0].Rows[i]["Celular"].ToString(),
                            Email = ds.Tables[0].Rows[i]["Mail"].ToString(),
                         
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstNotaria;
        }

        public List<dto.Notaria> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Notaria> lstNotaria = new List<dto.Notaria>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Notarias_Grilla");
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
                        lstNotaria.Add(new dto.Notaria()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            NombreNotaria = ds.Tables[0].Rows[i]["NombreNotaria"].ToString(),
                            IdComuna = Int16.Parse(ds.Tables[0].Rows[i]["IdComuna"].ToString()),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            Telefono1 = ds.Tables[0].Rows[i]["Telefono1"].ToString(),
                            Telefono2 = ds.Tables[0].Rows[i]["Telefono2"].ToString(),
                            Fax = ds.Tables[0].Rows[i]["Fax"].ToString(),
                            Celular = ds.Tables[0].Rows[i]["Celular"].ToString(),
                            Email = ds.Tables[0].Rows[i]["Mail"].ToString(),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstNotaria;
        }

        public static int ListarNotariaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Notaria_Grilla_Count");
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

        public void InsertarNotaria(dto.Notaria objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Notarias");
                sp.AgregarParametro("not_codemp", codemp);
                sp.AgregarParametro("not_rut", (object)objAccion.Rut.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("not_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("not_nomnot", (object)objAccion.NombreNotaria ?? DBNull.Value);
                sp.AgregarParametro("not_comid", (object)objAccion.IdComuna ?? DBNull.Value);
                sp.AgregarParametro("not_direccion", (object)objAccion.Direccion.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("not_telefono1", (object)objAccion.Telefono1 ?? DBNull.Value);
                sp.AgregarParametro("not_telefono2", (object)objAccion.Telefono2 ?? DBNull.Value);
                sp.AgregarParametro("not_fax", (object)objAccion.Fax ?? DBNull.Value);
                sp.AgregarParametro("not_celular", (object)objAccion.Celular ?? DBNull.Value);
                sp.AgregarParametro("not_mail", (object)objAccion.Email.ToLower() ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarNotaria(dto.Notaria objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Notarias");
                sp.AgregarParametro("not_codemp", codemp);
                sp.AgregarParametro("not_notid", objAccion.Id);
                sp.AgregarParametro("not_rut", (object)objAccion.Rut.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("not_nombre", (object)objAccion.Nombre.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("not_nomnot", (object)objAccion.NombreNotaria.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("not_comid", (object)objAccion.IdComuna ?? DBNull.Value);
                sp.AgregarParametro("not_direccion", (object)objAccion.Direccion.ToUpper() ?? DBNull.Value);
                sp.AgregarParametro("not_telefono1", (object)objAccion.Telefono1 ?? DBNull.Value);
                sp.AgregarParametro("not_telefono2", (object)objAccion.Telefono2 ?? DBNull.Value);
                sp.AgregarParametro("not_fax", (object)objAccion.Fax ?? DBNull.Value);
                sp.AgregarParametro("not_celular", (object)objAccion.Celular ?? DBNull.Value);
                sp.AgregarParametro("not_mail", (object)objAccion.Email.ToLower() ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarNotaria(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Notarias");
                sp.AgregarParametro("not_codemp", codemp);
                sp.AgregarParametro("not_notid", id);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
