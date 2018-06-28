using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class MateriaJudicial
    {
        public List<dto.MateriaJudicial> ListarMateriaJudicialGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MateriaJudicial> lstMateriaJudicial = new List<dto.MateriaJudicial>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Materia_Judicial_Grilla");
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
                        lstMateriaJudicial.Add(new dto.MateriaJudicial()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            Orden = Int16.Parse(ds.Tables[0].Rows[i]["ORDEN"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMateriaJudicial;
        }

        public static int ListarMateriaJudicialGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Materia_Judicial_Grilla_Count");
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

        public List<dto.MateriaJudicial> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MateriaJudicial> lstMateriaJudicial = new List<dto.MateriaJudicial>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Materia_Judicial_Grilla");
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
                        lstMateriaJudicial.Add(new dto.MateriaJudicial()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            Orden = Int16.Parse(ds.Tables[0].Rows[i]["ORDEN"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMateriaJudicial;
        }

        public void InsertarMateriaJudicial(dto.MateriaJudicial objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_materia_judicial");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("orden", objAccion.Orden);
                sp.AgregarParametro("idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditarMateriaJudicial(dto.MateriaJudicial objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Materia_Judicial");
                sp.AgregarParametro("esj_codemp", codemp);
                sp.AgregarParametro("esj_esjid", objAccion.Id);
                sp.AgregarParametro("esj_nombre", objAccion.Nombre.ToUpper());
                sp.AgregarParametro("esj_orden", objAccion.Orden);
                sp.AgregarParametro("idid", idioma);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BorrarMateriaJudicial(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Materia_Judicial");
                sp.AgregarParametro("esj_codemp", codemp);
                sp.AgregarParametro("esj_esjid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
