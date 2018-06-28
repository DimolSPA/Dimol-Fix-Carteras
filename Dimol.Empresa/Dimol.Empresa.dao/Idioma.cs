using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dimol.Empresa.dao
{
    public class Idioma
    {
        public List<dto.Idioma> ListarIdiomaGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Idioma> lstIdioma = new List<dto.Idioma>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Idiomas_Grilla");
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
                        lstIdioma.Add(new dto.Idioma()
                        {
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Recurso = ds.Tables[0].Rows[i]["Recurso"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstIdioma;
        }

        public List<dto.Idioma> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Idioma> lstIdioma = new List<dto.Idioma>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Idiomas_Grilla");
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
                        lstIdioma.Add(new dto.Idioma()
                        {
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Recurso = ds.Tables[0].Rows[i]["Recurso"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstIdioma;
        }

        public static int ListarIdiomaGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Idiomas_Grilla_Count");
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

        public void InsertarIdioma(dto.Idioma objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Idiomas");
                sp.AgregarParametro("idi_nombre", objAccion.Nombre);
                sp.AgregarParametro("idi_idisrc", objAccion.Recurso);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarIdioma(dto.Idioma objAccion, int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Idiomas");
                sp.AgregarParametro("idi_idid", id);
                sp.AgregarParametro("idi_nombre", objAccion.Nombre);
                sp.AgregarParametro("idi_idisrc", objAccion.Recurso);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarIdioma( int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Idiomas");
                sp.AgregarParametro("idi_idid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
