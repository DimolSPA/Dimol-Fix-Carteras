using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.ProveedorCliente.Mantenedores.dto;
using System.Data;
using Dimol.dao;
using System.Data.SqlClient;

namespace Dimol.ProveedorCliente.Mantenedores.dao
{
    public class Giro
    {
        public List<dto.Giro> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Giro> lst = new List<dto.Giro>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Giros");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        lst.Add(new dto.Giro()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public List<dto.Giro> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Giro> lst = new List<dto.Giro>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Giros");
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
                        lst.Add(new dto.Giro()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }



        public void Insertar(dto.Giro objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Giro");
                sp.AgregarParametro("gir_codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("gir_nombre", objAccion.Nombre);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Borrar(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Giros");
                sp.AgregarParametro("gir_codemp", codemp);
                sp.AgregarParametro("gir_girid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.Giro objAccion, int codemp, int idioma, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Giros");
                sp.AgregarParametro("gir_codemp", codemp);
                sp.AgregarParametro("gir_girid", id);
                sp.AgregarParametro("gir_nombre", objAccion.Nombre);
                int error = sp.EjecutarProcedimientoTrans();

                StoredProcedure sp2 = new StoredProcedure("Update_Giros_Idiomas");
                sp2.AgregarParametro("gii_codemp", codemp);
                sp2.AgregarParametro("gii_girid", id);
                sp2.AgregarParametro("gii_idid", idioma);
                sp2.AgregarParametro("gii_nombre", objAccion.Nombre);

                int error2 = sp2.EjecutarProcedimientoTrans();

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
                StoredProcedure sp = new StoredProcedure("_Listar_Giros_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idid", idid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                //sp.AgregarParametro("inicio", inicio);
                //sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
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

    }
}
