using Dimol.Carteras.dto;
using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;
namespace Dimol.Carteras.dao
{
    public class Gestor
    {
        public static List<dto.RestriccionGestor> ListarRestriccionesGestor(int codemp, int codsuc, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.RestriccionGestor> lst = new List<dto.RestriccionGestor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestor_Restriccion_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
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
                        lst.Add(new dto.RestriccionGestor()
                        {
                            Usrid = Int32.Parse(ds.Tables[0].Rows[i]["Usrid"].ToString()),
                            Gesid = Int32.Parse(ds.Tables[0].Rows[i]["Gesid"].ToString()),
                            NombreUsuario = ds.Tables[0].Rows[i]["NombreUsuario"].ToString(),
                            NombreGestor = ds.Tables[0].Rows[i]["NombreGestor"].ToString(),
                            FechaDesde = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDesde"].ToString()),
                            FechaHasta = DateTime.Parse(ds.Tables[0].Rows[i]["FechaHasta"].ToString())
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

        public static int ListarRestriccionesGestorCount(int codemp, int codsuc, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestor_Restriccion_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
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

        public static int InsertarRestriccionesGestor(RestriccionGestor obj)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Gestor_Restriccion_Nula");
                sp.AgregarParametro("grn_codemp", obj.Codemp);
                sp.AgregarParametro("grn_usrid", obj.Usrid);
                sp.AgregarParametro("grn_sucid", obj.Sucid);
                sp.AgregarParametro("grn_gesid", obj.Gesid);
                sp.AgregarParametro("grn_desde", obj.FechaDesde);
                sp.AgregarParametro("grn_hasta", obj.FechaHasta);
                error = sp.EjecutarProcedimientoTrans();
         

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ModificarRestriccionesGestor(RestriccionGestor obj)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Update_Gestor_Restriccion_Nula");
                sp.AgregarParametro("grn_codemp", obj.Codemp);
                sp.AgregarParametro("grn_usrid", obj.Usrid);
                sp.AgregarParametro("grn_sucid", obj.Sucid);
                sp.AgregarParametro("grn_gesid", obj.Gesid);
                sp.AgregarParametro("grn_desde", obj.FechaDesde);
                sp.AgregarParametro("grn_hasta", obj.FechaHasta);
                error = sp.EjecutarProcedimientoTrans();


                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int BorrarRestriccionesGestor(RestriccionGestor obj)
        {
            int error = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Delete_Gestor_Restriccion_Nula");
                sp.AgregarParametro("grn_codemp", obj.Codemp);
                sp.AgregarParametro("grn_usrid", obj.Usrid);
                sp.AgregarParametro("grn_sucid", obj.Sucid);
                sp.AgregarParametro("grn_gesid", obj.Gesid);
                error = sp.EjecutarProcedimientoTrans();


                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static string ListarUsuarios(int codemp)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Usuarios");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 1)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string ListarGestores(int codemp, int codsuc)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestores");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 1)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }


        }

        public static List<Dimol.dto.Combobox> ListarGestoresCombo(int codemp, int codsuc)
        {
            List<Dimol.dto.Combobox> salida = new List<Dimol.dto.Combobox>(); ;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestores");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                ds = sp.EjecutarProcedimiento();

                salida.Add(new Dimol.dto.Combobox() { Text = "-- Seleccione --", Value = "" });

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    salida.Add(new Dimol.dto.Combobox() { Text = ds.Tables[0].Rows[i][1].ToString(), Value = ds.Tables[0].Rows[i][0].ToString() });
                }
                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }


        }

        public static List<Dimol.dto.Combobox> ListarGestorCombo(int codemp, int codsuc, string email)
        {
            List<Dimol.dto.Combobox> salida = new List<Dimol.dto.Combobox>(); ;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestor_Por_Email");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("email", email);
                ds = sp.EjecutarProcedimiento();

                salida.Add(new Dimol.dto.Combobox() { Text = "-- Seleccione --", Value = "" });

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    salida.Add(new Dimol.dto.Combobox() { Text = ds.Tables[0].Rows[i][1].ToString(), Value = ds.Tables[0].Rows[i][0].ToString() });
                }
                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }


        }

        public static List<dto.Gestor> ListarGestorGrilla(int codemp, int codsucursal, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Gestor> lst = new List<dto.Gestor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestores_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucursal", codsucursal);
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
                        lst.Add(new dto.Gestor()
                        {
                            GesId = ds.Tables[0].Rows[i]["ges_gesid"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                            Telefono = ds.Tables[0].Rows[i]["ges_telefono"].ToString(),
                            Email = ds.Tables[0].Rows[i]["ges_email"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["estado"].ToString(),
                            IdTipoCartera =Int32.Parse(ds.Tables[0].Rows[i]["ges_tipcart"].ToString()),
                            IdGrupo = Int32.Parse(ds.Tables[0].Rows[i]["grupoid"].ToString()),
                            IndTerreno = ds.Tables[0].Rows[i]["ges_visita_terreno"].ToString(),
                            IndRemoto = ds.Tables[0].Rows[i]["ges_remoto"].ToString(),
                            TelefonoTerreno = ds.Tables[0].Rows[i]["ges_telefono_terreno"].ToString(),
                            TelefonoImei = ds.Tables[0].Rows[i]["ges_imei"].ToString(),
                            IdEmpleado = ds.Tables[0].Rows[i]["ges_emplid"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Gestor.ListarGestorGrilla", 1);
                return lst;
            }
        }

        public static int ListarGestorGrillaCount(int codemp, int codsucursal, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Gestores_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucursal", codsucursal);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Gestor.ListarGestorGrillaCount", 1);
                return count;
            }
        }

        public static List<Combobox> TraeListaDe(string EtiClave,int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 3; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", EtiClave + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();

                    lst.Add(new Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Gestor.TraeListaDe", 1);
                return lst;
            }

        }

        public static List<Combobox> ListarGrupoCobranza(int codemp, int codsucursal)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_GrupoCobranza");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucursal", codsucursal);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][1].ToString(),
                            Value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Gestor.ListarGrupoCobranza", 1);
            }
            return lst;
        }

        public static int GuardarGestor(dto.Gestor obj, int codemp, int codsucursal)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insert_Update_Gestor");
                sp.AgregarParametro("ges_codemp", codemp);
                sp.AgregarParametro("ges_sucid", codsucursal);
                sp.AgregarParametro("ges_gesid", (String.IsNullOrEmpty(obj.GesId) ? 0: Int32.Parse(obj.GesId)));
                sp.AgregarParametro("ges_nombre", obj.Nombre);
                sp.AgregarParametro("ges_telefono", obj.Telefono);
                sp.AgregarParametro("ges_email", obj.Email);
                sp.AgregarParametro("ges_tipcart", obj.IdTipoCartera);
                sp.AgregarParametro("ges_emplid", obj.IdEmpleado);
                sp.AgregarParametro("ges_remoto", obj.IndRemoto);
                sp.AgregarParametro("ges_estado", obj.Estado);
                sp.AgregarParametro("ges_grupoid", obj.IdGrupo);
                sp.AgregarParametro("ges_visita_terreno", obj.IndTerreno);
                sp.AgregarParametro("ges_telefono_terreno", string.IsNullOrEmpty(obj.TelefonoTerreno) ? DBNull.Value :(object) obj.TelefonoTerreno );
                sp.AgregarParametro("ges_imei", string.IsNullOrEmpty(obj.TelefonoImei) ? DBNull.Value : (object)obj.TelefonoImei);
                int respuesta = sp.EjecutarProcedimientoTrans();
                return respuesta;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Gestor.GuardarGestor", 1);
                return -1;
            }
        }

        public static List<Combobox> ListarEmpleados(int codemp)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Empleados");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][1].ToString(),
                            Value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Gestor.ListarEmpleados", 1);
            }
            return lst;
        }

        public static List<Combobox> ListarGestoresVisitaTerreno(int codemp, int codsucursal)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_listar_VisitaTerreno_Gestores_Aprobar");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", codsucursal);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][1].ToString(),
                            Value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Gestor.ListarGestoresVisitaTerreno", 1);
            }
            return lst;
        }

        public static List<Combobox> ListarVisitaTerrenoGestores(int codemp, int codsucursal)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_VisitaTerreno_Gestores");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", codsucursal);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][1].ToString(),
                            Value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Gestor.ListarVisitaTerrenoGestores", 1);
            }
            return lst;
        }
    }
}
