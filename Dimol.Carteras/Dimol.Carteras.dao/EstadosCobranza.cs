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
    public class EstadosCobranza
    {
        public static List<Combobox> ListarPerfiles(int codemp, int idioma, int perfilId)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_perfiles");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("perfilid", perfilId);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobranza.ListarPerfiles", 1);
            }
            return lst;
        }

        public static List<Combobox> TraeListaTipoEstados(string EtiClave, int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 2; i < 5; i++)
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Gestor.TraeListaTipoEstados", 1);
                return lst;
            }

        }
        public static int ListarEstadosCarteraPerfilCount(int codemp, int idioma, int perfilSelected, int agrupaSelected, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Estados_Cartera_Perfil_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("prfid", perfilSelected);
                sp.AgregarParametro("agrupaid", agrupaSelected);
                sp.AgregarParametro("idioma", idioma);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobraza.ListarEstadosCarteraPerfilCount", 0);
                return count;
            }
        }
        public static List<dto.PerfilEstadoCobranza> ListarEstadosCarteraPerfil(int codemp, int idioma, int perfilSelected, int agrupaSelected, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PerfilEstadoCobranza> lst = new List<dto.PerfilEstadoCobranza>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Estados_Cartera_Perfil");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("prfid", perfilSelected);
                sp.AgregarParametro("agrupaid", agrupaSelected);
                sp.AgregarParametro("idioma", idioma);
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
                        lst.Add(new dto.PerfilEstadoCobranza()
                        {
                            Estid = ds.Tables[0].Rows[i]["ESTID"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            IsSelected = bool.Parse(ds.Tables[0].Rows[i]["ISSELECTED"].ToString())

                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobraza.ListarEstadosCarteraPerfil", 0);
                return lst;
            }
        }

        public static int InsertarPerfilEstado(int codemp, int perfil, int estid)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Perfiles_Estados");
                sp.AgregarParametro("pfe_codemp", codemp);
                sp.AgregarParametro("pfe_prfid", perfil);
                sp.AgregarParametro("pfe_estid", estid);


                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobranza.InsertarPerfilEstado", 0);
                return -1;
            }
            return result;
        }

        public static int EliminarPerfilEstado(int codemp, int perfil, int estid)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Perfiles_Estados");
                sp.AgregarParametro("pfe_codemp", codemp);
                sp.AgregarParametro("pfe_prfid", perfil);
                sp.AgregarParametro("pfe_estid", estid);


                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobranza.EliminarPerfilEstado", 0);
                return -1;
            }
            return result;
        }

        public static int InsertarPerfilEstadoHistorial(int perfil, int estid, int accion, int user)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Perfiles_Estados_Historial");
                sp.AgregarParametro("perfil", perfil);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("accion", accion);
                sp.AgregarParametro("usrid", user);

                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobranza.InsertarPerfilEstadoHistorial", 0);
                return -1;
            }
            return result;
        }

        //Clientes Estados
        public static int ListarEstadosCarteraClienteCount(int codemp, int idioma, int clienteSelected, int agrupaSelected, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Estados_Cartera_Cliente_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", clienteSelected);
                sp.AgregarParametro("agrupaid", agrupaSelected);
                sp.AgregarParametro("idioma", idioma);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobraza.ListarEstadosCarteraClienteCount", 0);
                return count;
            }
        }
        public static List<dto.PerfilEstadoCobranza> ListarEstadosCarteraCliente(int codemp, int idioma, int clienteSelected, int agrupaSelected, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PerfilEstadoCobranza> lst = new List<dto.PerfilEstadoCobranza>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Estados_Cartera_Cliente");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", clienteSelected);
                sp.AgregarParametro("agrupaid", agrupaSelected);
                sp.AgregarParametro("idioma", idioma);
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
                        lst.Add(new dto.PerfilEstadoCobranza()
                        {
                            Estid = ds.Tables[0].Rows[i]["ESTID"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            IsSelected = bool.Parse(ds.Tables[0].Rows[i]["ISSELECTED"].ToString())

                        });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobraza.ListarEstadosCarteraCliente", 0);
                return lst;
            }
        }

        public static int InsertarClienteEstado(int codemp, int pclid, int estid)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Cliente_Estado");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("estid", estid);


                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobranza.InsertarClienteEstado", 0);
                return -1;
            }
            return result;
        }

        public static int EliminarClienteEstado(int codemp, int pclid, int estid)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Delete_Cliente_Estado");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("estid", estid);


                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobranza.EliminarClienteEstado", 0);
                return -1;
            }
            return result;
        }

        public static int InsertarClienteEstadoHistorial(int pclid, int estid, int accion, int user)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Cliente_Estado_Historial");
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("accion", accion);
                sp.AgregarParametro("usrid", user);

                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EstadosCobranza.InsertarClienteEstadoHistorial", user);
                return -1;
            }
            return result;
        }
    }
}
