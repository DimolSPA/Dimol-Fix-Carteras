using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class EnteAsignado
    {
        public static List<Combobox> ListarTiposEnte(int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipos_Ente");
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = ""
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["Id"].ToString(),
                            Text = ds.Tables[0].Rows[i]["Descripcion"].ToString()
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

        public List<dto.EnteAsignado> ListarEnteAsignadoGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, string tipo)
        {
            List<dto.EnteAsignado> lstEnteAsignado = new List<dto.EnteAsignado>();
            try
            {
              
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_EnteAsignado_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("tipo", tipo);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstEnteAsignado.Add(new dto.EnteAsignado()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            Sindico = ds.Tables[0].Rows[i]["Sindico"].ToString(),
                            Abogado = ds.Tables[0].Rows[i]["Abogado"].ToString(),
                            Procurador = ds.Tables[0].Rows[i]["Procurador"].ToString(),
                            Receptor = ds.Tables[0].Rows[i]["Receptor"].ToString(),
                            AbogadoEncargado = ds.Tables[0].Rows[i]["AbogadoEncargado"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEnteAsignado;
        }

        public static int ListarEnteAsignadoGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, string tipo)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_EnteAsignado_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("tipo", tipo);
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

        public List<dto.EnteAsignado> ListarEnteParaAsignarGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite, string tipo)
        {
            List<dto.EnteAsignado> lstEnteAsignado = new List<dto.EnteAsignado>();
            try
            {
              
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_EnteParaAsignar_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("tipo", tipo );
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstEnteAsignado.Add(new dto.EnteAsignado()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["CODEMP"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            Sindico = ds.Tables[0].Rows[i]["Sindico"].ToString(),
                            Abogado = ds.Tables[0].Rows[i]["Abogado"].ToString(),
                            Procurador = ds.Tables[0].Rows[i]["Procurador"].ToString(),
                            Receptor = ds.Tables[0].Rows[i]["Receptor"].ToString(),
                            AbogadoEncargado = ds.Tables[0].Rows[i]["AbogadoEncargado"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEnteAsignado;
        }

        public static int ListarEnteParaAsignarGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite,string tipo)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_EnteParaAsignar_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("tipo", tipo);
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


        public static void  GrabarEnte(int codemp, int etjId, int rolId)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_EnteJud_Rol_Especial");
                sp.AgregarParametro("ejr_codemp", codemp);
                sp.AgregarParametro("ejr_etjid", etjId);
                sp.AgregarParametro("ejr_etjidN", rolId);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     
    }
}
