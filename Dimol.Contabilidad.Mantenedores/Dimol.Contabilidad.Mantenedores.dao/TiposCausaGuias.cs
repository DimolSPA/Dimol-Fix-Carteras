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
    public class TiposCausaGuias
    {
        public List<dto.TiposCausaGuias> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposCausaGuias> lstPeriodos = new List<dto.TiposCausaGuias>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposCausaGuias_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                Debug.WriteLine("PARAMETROS SP " + codemp + " " + where + " " + sidx + " " + sord + " " + inicio + " " + limite);
                ds = sp.EjecutarProcedimiento();
                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.TiposCausaGuias()
                        {

                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            Codigo = Int16.Parse(ds.Tables[1].Rows[i]["codigo"].ToString())
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


        public List<dto.TiposCausaGuias> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposCausaGuias> lstPeriodos = new List<dto.TiposCausaGuias>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposCausaGuias_Grilla");
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
                        lstPeriodos.Add(new dto.TiposCausaGuias()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            Codigo = Int16.Parse(ds.Tables[1].Rows[i]["codigo"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPeriodos;
        }


        public void Insertar(dto.TiposCausaGuias objAccion, int codemp, int idioma)
        {
            try
            {
                Debug.WriteLine("insertar tipos causa guias");
                StoredProcedure sp = new StoredProcedure("_Insertar_Tipos_Causa_Guias");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("nombre", objAccion.Nombre);
                sp.AgregarParametro("codigo", objAccion.Codigo);
                sp.AgregarParametro("idioma", idioma);

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
                StoredProcedure sp = new StoredProcedure("Delete_Tipos_Causa_Guias");
                sp.AgregarParametro("tgd_codemp", codemp);
                sp.AgregarParametro("tgd_tgdid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.TiposCausaGuias objAccion, int codemp, int id, int idioma)
        {
            int error = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Tipos_Causa_Guias");
                sp.AgregarParametro("tgd_codemp", codemp);
                sp.AgregarParametro("tgd_tgdid", id);
                sp.AgregarParametro("tgd_nombre", objAccion.Nombre);
                sp.AgregarParametro("tgd_codigo", objAccion.Codigo);

                error = sp.EjecutarProcedimientoTrans();

                StoredProcedure sp2 = new StoredProcedure("Update_Tipos_Causa_Guias_Idiomas");
                sp2.AgregarParametro("tgi_codemp", codemp);
                sp2.AgregarParametro("tgi_tgdid", id);
                sp2.AgregarParametro("tgi_idid", idioma);
                sp2.AgregarParametro("tgi_nombre", objAccion.Nombre);

                error = sp2.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ListarTiposCausaGuiasCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposCausaGuias_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
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


    }
}


