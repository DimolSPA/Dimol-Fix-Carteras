using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;
using System.Data;
using Dimol.dao;

namespace Dimol.Email.dao
{
    public class Vista
    {
        public static List<Combobox> ListarEstados(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Email_Grilla");
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
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["Id"].ToString(),
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarEstados", 0);
                return lst;
            }
        }

        public static int ListarEstadosCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Email_Grilla_count");
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
                    return Int32.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarEstadosCount", 0);
                return 0; 
            }
        }

        public static int ListarEstadosClienteCount(int codemp, int pclid, int idioma)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Email_Cliente_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["RESULTS"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarEstadosCount", 0);
                return 0;
            }
        }

        public static List<Combobox> ListarGestores(int codemp, int sucid, int tipoCartera, int gestor, int grupo, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestores_Email_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo_cartera", tipoCartera);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("gestor", gestor);
                sp.AgregarParametro("grupo", grupo);
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
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["Id"].ToString(),
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarGestores", 0);
                return lst;
            }
        }

        public static List<Combobox> ListarGestoresEmailMasivo(int codemp, int sucid, int tipoCartera, string sidx, string sord, int inicio, int limite)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestores_Email_Masivo_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo_cartera", tipoCartera);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["Id"].ToString(),
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarGestores", 0);
                return lst;
            }
        }

        public static int ListarGestoresEmailMasivoCount(int codemp, int sucid, int tipoCartera, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestores_Email_Masivo_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo_cartera", tipoCartera);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarGestoresCount", 0);
                return 0; ;
            }
        }

        public static int ListarGestoresCount(int codemp, int sucid, int tipoCartera, int gestor, int grupo, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestores_Email_Grilla_count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo_cartera", tipoCartera);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("gestor", gestor);
                sp.AgregarParametro("grupo", grupo);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarGestoresCount", 0);
                return 0; ;
            }
        }

        public static List<Combobox> ListarGrupoCobranza(int codemp, int sucdid, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Grupo_Cobranza");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", sucdid);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = first
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["Id"].ToString(),
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString()
                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.dao.Vista.ListarGrupoCobranza", 0);
                return lst;
            }

        }
    }
}
