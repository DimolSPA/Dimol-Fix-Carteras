using Dimol.dao;
using PJSpider.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSpider.dao
{
    public class Causa
    {
        public static void InsertarTipoCuaderno(int idCausa, int idCuaderno, string descCuaderno, string escritosPendientes)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Tipos_Cuaderno");
                sp.AgregarParametro("ID_causa", idCausa);
                sp.AgregarParametro("ID_cuaderno", idCuaderno);
                sp.AgregarParametro("desc_cuaderno", descCuaderno);
                sp.AgregarParametro("esc_pend", escritosPendientes); 
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message,ex.StackTrace, "Bot Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarRolPoderJudicial(int codemp,int rolid, string tipo, int idCausa, int idTribunal)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Rol_Poder_Judicial");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("tribunal", idTribunal);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Bot Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarHistorialPoderJudicial(int idCausa, int idCuaderno, string folio, string rutaDocumento, string etapa, string tramite, string descTramite, DateTime fechaTramite, int foja )
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Historial_Poder_Judicial");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("id_cuaderno", idCuaderno);
                sp.AgregarParametro("folio", string.IsNullOrEmpty(folio) ? DBNull.Value : (object)folio );
                sp.AgregarParametro("ruta_documento", string.IsNullOrEmpty(rutaDocumento) ? DBNull.Value : (object)rutaDocumento);
                sp.AgregarParametro("etapa", string.IsNullOrEmpty( etapa) ? DBNull.Value : (object)etapa);
                sp.AgregarParametro("tramite", string.IsNullOrEmpty(tramite) ? DBNull.Value : (object)tramite);
                sp.AgregarParametro("desc_tramite", string.IsNullOrEmpty(descTramite) ? DBNull.Value : (object)descTramite);
                //sp.AgregarParametro("fecha_tramite", fechaTramite);
                //Se evalua la fecha de tramite cuando viene vacía, aun cuando en la tabla ese dato no acepta null
                // por lo que generará una excepción que se guardará indicandola, y éste registro no se guardará
                sp.AgregarParametro("fecha_tramite", fechaTramite == new DateTime() ? DBNull.Value : (object)fechaTramite);
                sp.AgregarParametro("foja", foja);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Bot Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarReceptorPoderJudicial(int idCausa, string cuaderno, string estado, string receptor, DateTime fecha)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Receptor_Poder_Judicial");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("cuaderno", cuaderno);
                sp.AgregarParametro("estado", estado);
                sp.AgregarParametro("receptor", receptor);
                sp.AgregarParametro("fecha", fecha);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Bot Poder Judicial", 0);
                throw ex;
            }

        }

        public static List<RolActualizar> ListarRolesActualizar(int codemp, int idioma, string estados)
        {
            string[] numero ;
            int rol = 0;
            int anio =0;
            bool tieneLetras = false;
            DateTime fechaHistorial = new DateTime();
            DateTime fechaReceptor = new DateTime();
            List<RolActualizar> lst = new List<RolActualizar>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Roles_Spider");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("estados", estados);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaHistorial = new DateTime();
                        fechaReceptor = new DateTime();
                        try
                        {
                            numero = ds.Tables[0].Rows[i]["rol_numero"].ToString().Split('-');
                            rol = Int32.Parse(numero[0].Replace(".", ""));
                            anio = Int32.Parse(numero[1].Replace(".", ""));
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_historial"].ToString(),out fechaHistorial);
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_receptor"].ToString(), out fechaReceptor);
                            lst.Add(new RolActualizar()
                                                        {
                                                            Anio = anio,
                                                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["rol_codemp"].ToString()),
                                                            Numero = ds.Tables[0].Rows[i]["rol_numero"].ToString(),
                                                            Rol = rol,
                                                            Rolid = Int32.Parse(ds.Tables[0].Rows[i]["rol_rolid"].ToString()),
                                                            TipoCausa = ds.Tables[0].Rows[i]["rol_tipo_rol"].ToString(),
                                                            Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                                                            IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["id_causa"].ToString()),
                                                            FechaUltHistorial = fechaHistorial,
                                                            FechaUltReceptor = fechaReceptor
                                                        });
                        }
                        catch (Exception e)
                        {
                            Dimol.dao.Funciones.InsertarError("Numero de rol con formato incorrecto", "Rol: " + ds.Tables[0].Rows[i]["rol_numero"].ToString(), "Bot Poder Judicial", 0);
                        }
                       
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Bot Poder Judicial", 0);
                return lst;
            }
        }

        public static List<RolActualizar> ListarRolesActualizarDemonio(int codemp, int idioma, string estados, int inicio, int termino)
        {
            string[] numero;
            int rol = 0;
            int anio = 0;
            DateTime fechaHistorial = new DateTime();
            DateTime fechaReceptor = new DateTime();
            List<RolActualizar> lst = new List<RolActualizar>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Roles_Demonio");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("estados", estados);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("termino", termino);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaHistorial = new DateTime();
                        fechaReceptor = new DateTime();
                        try
                        {
                            numero = ds.Tables[0].Rows[i]["rol_numero"].ToString().Split('-');
                            rol = Int32.Parse(numero[0].Replace(".", ""));
                            anio = Int32.Parse(numero[1].Replace(".", ""));
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_historial"].ToString(), out fechaHistorial);
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_receptor"].ToString(), out fechaReceptor);
                            lst.Add(new RolActualizar()
                            {
                                Anio = anio,
                                Codemp = Int32.Parse(ds.Tables[0].Rows[i]["rol_codemp"].ToString()),
                                Numero = ds.Tables[0].Rows[i]["rol_numero"].ToString(),
                                Rol = rol,
                                Rolid = Int32.Parse(ds.Tables[0].Rows[i]["rol_rolid"].ToString()),
                                TipoCausa = ds.Tables[0].Rows[i]["rol_tipo_rol"].ToString(),
                                Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                                IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["id_causa"].ToString()),
                                FechaUltHistorial = fechaHistorial,
                                FechaUltReceptor = fechaReceptor,
                                IdTribunal = string.IsNullOrEmpty( ds.Tables[0].Rows[i]["id_tribunal"].ToString()) ? 0 : Int32.Parse(ds.Tables[0].Rows[i]["id_tribunal"].ToString())
                            });
                        }
                        catch (Exception e)
                        {
                            Dimol.dao.Funciones.InsertarError("Numero de rol con formato incorrecto", "Rol: " + ds.Tables[0].Rows[i]["rol_numero"].ToString(), "Bot Poder Judicial", 0);
                        }

                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Bot Poder Judicial", 0);
                return lst;
            }
        }

        public static List<RolActualizar> ListarRolesActualizarCliente(int codemp, int idioma, string estados, int pclid)
        {
            string[] numero;
            int rol = 0;
            int anio = 0;
            DateTime fechaHistorial = new DateTime();
            DateTime fechaReceptor = new DateTime();
            List<RolActualizar> lst = new List<RolActualizar>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Roles_Spider_Cliente");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("estados", estados);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaHistorial = new DateTime();
                        fechaReceptor = new DateTime();
                        try
                        {
                            numero = ds.Tables[0].Rows[i]["rol_numero"].ToString().Split('-');
                            rol = Int32.Parse(numero[0].Replace(".", ""));
                            anio = Int32.Parse(numero[1].Replace(".", ""));
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_historial"].ToString(), out fechaHistorial);
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_receptor"].ToString(), out fechaReceptor);
                            lst.Add(new RolActualizar()
                            {
                                Anio = anio,
                                Codemp = Int32.Parse(ds.Tables[0].Rows[i]["rol_codemp"].ToString()),
                                Numero = ds.Tables[0].Rows[i]["rol_numero"].ToString(),
                                Rol = rol,
                                Rolid = Int32.Parse(ds.Tables[0].Rows[i]["rol_rolid"].ToString()),
                                TipoCausa = ds.Tables[0].Rows[i]["rol_tipo_rol"].ToString(),
                                Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                                IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["id_causa"].ToString()),
                                FechaUltHistorial = fechaHistorial,
                                FechaUltReceptor = fechaReceptor
                            });
                        }
                        catch (Exception e)
                        {
                            Dimol.dao.Funciones.InsertarError("Numero de rol con formato incorrecto", "Rol: " + ds.Tables[0].Rows[i]["rol_numero"].ToString(), "Bot Poder Judicial", 0);
                        }

                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Bot Poder Judicial", 0);
                return lst;
            }
        }

        public static List<RolActualizar> ListarRolesActualizarDeudor(int codemp, int idioma, int ctcid, int pclid)
        {
            string[] numero;
            int rol = 0;
            int anio = 0;
            DateTime fechaHistorial = new DateTime();
            DateTime fechaReceptor = new DateTime();
            List<RolActualizar> lst = new List<RolActualizar>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Roles_Spider_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaHistorial = new DateTime();
                        fechaReceptor = new DateTime();
                        try
                        {
                            numero = ds.Tables[0].Rows[i]["rol_numero"].ToString().Split('-');
                            rol = Int32.Parse(numero[0].Replace(".", ""));
                            anio = Int32.Parse(numero[1].Replace(".", ""));
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_historial"].ToString(), out fechaHistorial);
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_receptor"].ToString(), out fechaReceptor);
                            lst.Add(new RolActualizar()
                            {
                                Anio = anio,
                                Codemp = Int32.Parse(ds.Tables[0].Rows[i]["rol_codemp"].ToString()),
                                Numero = ds.Tables[0].Rows[i]["rol_numero"].ToString(),
                                Rol = rol,
                                Rolid = Int32.Parse(ds.Tables[0].Rows[i]["rol_rolid"].ToString()),
                                TipoCausa = ds.Tables[0].Rows[i]["rol_tipo_rol"].ToString(),
                                Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                                IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["id_causa"].ToString()),
                                FechaUltHistorial = fechaHistorial,
                                FechaUltReceptor = fechaReceptor
                            });
                        }
                        catch (Exception e)
                        {
                            Dimol.dao.Funciones.InsertarError("Numero de rol con formato incorrecto", "Rol: " + ds.Tables[0].Rows[i]["rol_numero"].ToString(), "Bot Poder Judicial", 0);
                        }

                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Bot Poder Judicial", 0);
                return lst;
            }
        }

        #region "Scanner Poder Judicial"

        public static void InsertarPoderJudicialRol(int idCausa, string tipo, int numero, int anio, int tribunal, string ruta, DateTime fechaIngreso)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Poder_Judicial_Rol");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("tribunal", tribunal);
                sp.AgregarParametro("ruta_demanda", (object)ruta?? DBNull.Value);
                sp.AgregarParametro("fecha_ingreso", fechaIngreso == new DateTime() ? DBNull.Value : (object)fechaIngreso);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarRolHTML(int idCausa, string tipo, int numero, int anio, int tribunal, string ruta, DateTime fechaIngreso)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Rol_HTML");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("tribunal", tribunal);
                sp.AgregarParametro("ruta_demanda", (object)ruta ?? DBNull.Value);
                sp.AgregarParametro("fecha_ingreso", fechaIngreso == new DateTime() ? DBNull.Value : (object)fechaIngreso);
                int error = sp.EjecutarProcedimientoNoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarRolHTMLURL(int idCausa, string tipo, int numero, int anio, int tribunal, string ruta, DateTime fechaIngreso)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Rol_HTML");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("tribunal", tribunal);
                sp.AgregarParametro("ruta_demanda", (object)ruta ?? DBNull.Value);
                sp.AgregarParametro("fecha_ingreso", fechaIngreso == new DateTime() ? DBNull.Value : (object)fechaIngreso);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarPoderJudicialCuaderno(int idCausa, int idCuaderno, string descCuaderno, string escritosPendientes)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Tipos_Cuaderno");
                sp.AgregarParametro("ID_causa", idCausa);
                sp.AgregarParametro("ID_cuaderno", idCuaderno);
                sp.AgregarParametro("desc_cuaderno", descCuaderno);
                sp.AgregarParametro("esc_pend", escritosPendientes);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarPoderJudicialTipoCuaderno( int idCuaderno, string descCuaderno)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Tipo_Cuaderno_HTML");
                sp.AgregarParametro("ID_cuaderno", idCuaderno);
                sp.AgregarParametro("desc_cuaderno", descCuaderno);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarCuadernoHTML(int idCausa, int idCuaderno, string descCuaderno, string escritosPendientes)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Tipos_Cuaderno_HTML");
                sp.AgregarParametro("ID_causa", idCausa);
                sp.AgregarParametro("ID_cuaderno", idCuaderno);
                sp.AgregarParametro("desc_cuaderno", descCuaderno);
                sp.AgregarParametro("esc_pend", escritosPendientes);
                int error = sp.EjecutarProcedimientoNoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarCuadernoHTMLFull(int idCausa, int idCuaderno, string descCuaderno, string escritosPendientes, int tipoCuaderno)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Tipos_Cuaderno_HTML_Full");
                sp.AgregarParametro("ID_causa", idCausa);
                sp.AgregarParametro("ID_cuaderno", idCuaderno);
                sp.AgregarParametro("desc_cuaderno", descCuaderno);
                sp.AgregarParametro("esc_pend", escritosPendientes);
                sp.AgregarParametro("tipo_cuaderno", tipoCuaderno);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarPoderJudicialIndice( string tipo, int numero, int anio, int tribunal)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Poder_Judicial_Indice");
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("tribunal", tribunal);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarPoderJudicialLitigante(int idCausa, int idCuaderno, string participante, string rut, string tipoPersona, string nombre)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Poder_Judicial_Litigante");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("id_cuaderno", idCuaderno);
                sp.AgregarParametro("participante", participante);
                sp.AgregarParametro("rut", rut.Replace("-","").Replace(".",""));
                sp.AgregarParametro("tipo_persona", tipoPersona);
                sp.AgregarParametro("nombre", nombre);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarLitiganteHTML(int idCausa, int idCuaderno, string participante, string rut, string tipoPersona, string nombre)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Litigante_HTML");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("id_cuaderno", idCuaderno);
                sp.AgregarParametro("participante", participante);
                sp.AgregarParametro("rut", rut.Replace("-", "").Replace(".", ""));
                sp.AgregarParametro("tipo_persona", tipoPersona);
                sp.AgregarParametro("nombre", nombre);
                int error = sp.EjecutarProcedimientoNoTrans();
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static List<Dimol.dto.Combobox> ListarTribunalesScanner()
        {
            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Poder_Judicial_Tribunales");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox
                        {
                            Value = ds.Tables[0].Rows[i][0].ToString(),
                            Text = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static List<Dimol.dto.Combobox> ListarTribunalesScannerRango(int inicio, int termino)
        {
            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Poder_Judicial_Tribunales_Rango");
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("termino", termino);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox
                        {
                            Value = ds.Tables[0].Rows[i][0].ToString(),
                            Text = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static List<IndiceScanner> ListarIndiceScanner()
        {
            List<IndiceScanner> lst = new List<IndiceScanner>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Poder_Judicial_Indice");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new IndiceScanner
                        {
                            TipoCausa= ds.Tables[0].Rows[i][0].ToString(),
                            Rol=Int32.Parse( ds.Tables[0].Rows[i][1].ToString()),
                            Anio=Int32.Parse(ds.Tables[0].Rows[i][2].ToString()),
                            Tribunal=Int32.Parse(ds.Tables[0].Rows[i][3].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static List<IndiceScanner> ListarRolesScanner(int anio)
        {
            List<IndiceScanner> lst = new List<IndiceScanner>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Poder_Judicial_Indice_Anio");
                sp.AgregarParametro("anio", anio);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new IndiceScanner
                        {
                            Rol = Int32.Parse(ds.Tables[0].Rows[i][1].ToString()),
                            Anio =anio,
                            Tribunal = Int32.Parse(ds.Tables[0].Rows[i][3].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static List<int> ListarRolesScanner(int anio, int tribunal)
        {
            List<int> lst = new List<int>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Poder_Judicial_Tribunal_Anio");
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("tribunal", tribunal);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add( Int32.Parse(ds.Tables[0].Rows[i][0].ToString()));
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static List<IndiceScanner> ListarIndiceScannerFecha()
        {
            List<IndiceScanner> lst = new List<IndiceScanner>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Poder_Judicial_Fecha");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new IndiceScanner
                        {

                            IdCausa = Int32.Parse(ds.Tables[0].Rows[i][0].ToString()),
                            TipoCausa = ds.Tables[0].Rows[i][1].ToString(),
                            Rol = Int32.Parse(ds.Tables[0].Rows[i][2].ToString()),
                            Anio = Int32.Parse(ds.Tables[0].Rows[i][3].ToString()),
                            Tribunal = Int32.Parse(ds.Tables[0].Rows[i][4].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static void ActualizarPoderJudicialRolFecha(int idCausa, DateTime fechaIngreso)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Actualizar_Poder_Judicial_Rol_Fecha");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("fecha_ingreso", fechaIngreso == new DateTime() ? DBNull.Value : (object)fechaIngreso);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

      
        #endregion

        #region "Nuevo Scanner sin cliente"

        public static List<ScannerHTML> ListarRolesHTML()
        {
            List<ScannerHTML> lst = new List<ScannerHTML>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Causas_Actualizar_Fecha");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new ScannerHTML
                        {

                            TipoCausa = ds.Tables[0].Rows[i]["TIPO"].ToString(),
                            Rol = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString()),
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString()),
                            NombreTribunal = ds.Tables[0].Rows[i]["NOMBRE_TRIBUNAL"].ToString(),
                            IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["ID_CAUSA"].ToString()),
                            Tribunal = Int32.Parse(ds.Tables[0].Rows[i]["TRIBUNAL"].ToString()),
                            IdCuaderno = Int32.Parse(ds.Tables[0].Rows[i]["CUADERNO"].ToString()),
                            Url = ds.Tables[0].Rows[i]["Url"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static List<ScannerHTML> ListarRolesHTMLOrden(string orden)
        {
            List<ScannerHTML> lst = new List<ScannerHTML>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Causas_Actualizar_Fecha_Orden");
                sp.AgregarParametro("order", orden);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new ScannerHTML
                        {

                            TipoCausa = ds.Tables[0].Rows[i]["TIPO"].ToString(),
                            Rol = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString()),
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString()),
                            NombreTribunal = ds.Tables[0].Rows[i]["NOMBRE_TRIBUNAL"].ToString(),
                            IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["ID_CAUSA"].ToString()),
                            Tribunal = Int32.Parse(ds.Tables[0].Rows[i]["TRIBUNAL"].ToString()),
                            IdCuaderno = Int32.Parse(ds.Tables[0].Rows[i]["CUADERNO"].ToString()),
                            Url = ds.Tables[0].Rows[i]["Url"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static void InsertarPoderJudicialRolHTML(int idCausa, int idCuaderno, string tipo, int numero, int anio, int tribunal, string html)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Poder_Judicial_Rol_HTML");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("id_cuaderno", idCuaderno);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("tribunal", tribunal);
                sp.AgregarParametro("html", (object)html ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static void InsertarRolHTMLURL(int idCausa, int idCuaderno, string tipo, int numero, int anio, int tribunal, string html, string estado, string url)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Rol_HTML_URL");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("id_cuaderno", idCuaderno);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("tribunal", tribunal);
                sp.AgregarParametro("html", (object)html ?? DBNull.Value);
                sp.AgregarParametro("estado", (object)estado ?? "L");
                sp.AgregarParametro("ruta_demanda", (object)url ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static List<ScannerHTML> ListarCausaHTML()
        {
            List<ScannerHTML> lst = new List<ScannerHTML>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Causa_HTML");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new ScannerHTML
                        {
                            IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["ID_CAUSA"].ToString()),
                            IdCuaderno = Int32.Parse(ds.Tables[0].Rows[i]["id_CUADERNO"].ToString()),
                            HTML = ds.Tables[0].Rows[i]["HTML"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static void ActualizarRolHTMLFecha(int idCausa, int idCuaderno, string estado)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("[Actualizar_Rol_HTML_Fecha]");
                sp.AgregarParametro("id_causa", idCausa);
                sp.AgregarParametro("id_cuaderno", idCuaderno);
                sp.AgregarParametro("estado", estado);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        #endregion

        #region "Ultra Demon"

        public static void InsertarListaRolHTML(string tipo, int numero, int anio, string html)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Lista_Rol_HTML");
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("html", (object)html ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static ScannerHTML UltimaListaHTML()
        {
            ScannerHTML obj = new ScannerHTML();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Ultima_Lista_Rol_HTML");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        obj.Rol = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString());
                        obj.Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString());

                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return obj;
            }
        }

        public static ScannerHTML UltimaListaHTML(int anio)
        {
            ScannerHTML obj = new ScannerHTML();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Ultima_Lista_Rol_Anio_HTML");
                sp.AgregarParametro("anio", anio);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        obj.Rol = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString());
                        obj.Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString());

                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return obj;
            }
        }

        public static ScannerHTML UltimaListaNumeroHTML(int anio)
        {
            ScannerHTML obj = new ScannerHTML();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Ultima_Lista_Rol_Numero_Anio_HTML");
                sp.AgregarParametro("anio", anio);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        obj.Rol = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString());
                        obj.Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString());

                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return obj;
            }
        }

        #endregion

        public static List<ScannerHTML> ListarListaRolesHTML(int anio)
        {
            List<ScannerHTML> lst = new List<ScannerHTML>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Lista_Roles_HTML");
                sp.AgregarParametro("anio", anio);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new ScannerHTML
                        {

                            TipoCausa = ds.Tables[0].Rows[i]["TIPO"].ToString(),
                            Rol = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString()),
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString()),
                            HTML = ds.Tables[0].Rows[i]["HTML"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static void MarcarLeidaListaRolHTML(string tipo, int numero, int anio, string estado)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Lista_Rol_HTML_Leida");
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("numero", numero);
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("estado", estado);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                throw ex;
            }

        }

        public static List<ScannerHTML> ListarCargaRolHTML()
        {
            List<ScannerHTML> lst = new List<ScannerHTML>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Carga_Rol_HTML");

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new ScannerHTML
                        {
                            IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["ID_CAUSA"].ToString()),
                            IdCuaderno = Int32.Parse(ds.Tables[0].Rows[i]["id_CUADERNO"].ToString()),
                            TipoCausa = ds.Tables[0].Rows[i]["TIPO"].ToString(),
                            Rol = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString()),
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString()),
                            Tribunal = Int32.Parse(ds.Tables[0].Rows[i]["TRIBUNAL"].ToString()),
                            HTML = ds.Tables[0].Rows[i]["HTML"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        #region "Actualizar Roles internos"

        public static List<RolActualizar> ListarRolesInternos(int codemp, int idioma, string estados)
        {
            string[] numero;
            int rol = 0;
            int anio = 0;
            bool tieneLetras = false;
            DateTime fechaHistorial = new DateTime();
            DateTime fechaReceptor = new DateTime();
            List<RolActualizar> lst = new List<RolActualizar>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Roles_Internos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("estados", estados);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        fechaHistorial = new DateTime();
                        fechaReceptor = new DateTime();
                        try
                        {
                            numero = ds.Tables[0].Rows[i]["rol_numero"].ToString().Split('-');
                            rol = Int32.Parse(numero[0].Replace(".", ""));
                            anio = Int32.Parse(numero[1].Replace(".", ""));
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_historial"].ToString(), out fechaHistorial);
                            DateTime.TryParse(ds.Tables[0].Rows[i]["ult_receptor"].ToString(), out fechaReceptor);
                            lst.Add(new RolActualizar()
                            {
                                Anio = anio,
                                Codemp = Int32.Parse(ds.Tables[0].Rows[i]["rol_codemp"].ToString()),
                                Numero = ds.Tables[0].Rows[i]["rol_numero"].ToString(),
                                Rol = rol,
                                Rolid = Int32.Parse(ds.Tables[0].Rows[i]["rol_rolid"].ToString()),
                                TipoCausa = ds.Tables[0].Rows[i]["rol_tipo_rol"].ToString(),
                                Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                                IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["id_causa"].ToString()),
                                FechaUltHistorial = fechaHistorial,
                                FechaUltReceptor = fechaReceptor,
                                IdTribunal = Int32.Parse(ds.Tables[0].Rows[i]["id_tribunal"].ToString()),
                                UrlHTML = ds.Tables[0].Rows[i]["UrlHtml"].ToString()
                            });
                        }
                        catch (Exception e)
                        {
                            Dimol.dao.Funciones.InsertarError("Numero de rol con formato incorrecto", "Rol: " + ds.Tables[0].Rows[i]["rol_numero"].ToString(), "Bot Poder Judicial", 0);
                        }

                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Roles Internos Poder Judicial", 0);
                return lst;
            }
        }



        public static void BuscarListaRolesHTML(RolActualizar rol)// vstring tipo, int numero, int anio)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Buscar_Lista_Roles_HTML");
                sp.AgregarParametro("tipo", rol.TipoCausa);
                sp.AgregarParametro("numero", rol.Rol);
                sp.AgregarParametro("anio", rol.Anio);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        rol.HTML = ds.Tables[0].Rows[i]["HTML"].ToString();
                        rol.Estado = ds.Tables[0].Rows[i]["ESTADO"].ToString();
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Roles Internos Poder Judicial", 0);
            }
        }

        public static List<DatoTipo> ListarTipoCuaderno()
        {
            List<DatoTipo> lst = new List<DatoTipo>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Poder_Judicial_Tipo_Cuaderno");
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add( new DatoTipo{
                            Id=Int32.Parse(ds.Tables[0].Rows[i]["tipo_cuaderno"].ToString()),
                            Nombre= ds.Tables[0].Rows[i]["cuaderno"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Roles Internos Poder Judicial", 0);
                return lst;
            }
        }

        public static void InsertarTipoCuaderno(int idCuaderno, string cuaderno)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Tipo_Cuaderno");
                sp.AgregarParametro("ID_cuaderno", idCuaderno);
                sp.AgregarParametro("cuaderno", cuaderno);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Roles Internos Poder Judicial", 0);
                throw ex;
            }

        }

        #endregion

        #region "Procesar Rol HTML Demonio"

        public static List<ScannerHTML> ListarCargaRolHTMLDemonio(string anio, string estado)
        {
            List<ScannerHTML> lst = new List<ScannerHTML>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Carga_Rol_HTML_Demonio");
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("estado", estado);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new ScannerHTML
                        {
                            IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["ID_CAUSA"].ToString()),
                            IdCuaderno = Int32.Parse(ds.Tables[0].Rows[i]["id_CUADERNO"].ToString()),
                            TipoCausa = ds.Tables[0].Rows[i]["TIPO"].ToString(),
                            Rol = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString()),
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString()),
                            Tribunal = Int32.Parse(ds.Tables[0].Rows[i]["TRIBUNAL"].ToString()),
                            HTML = ds.Tables[0].Rows[i]["HTML"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        public static int DetenerCargaRolHTMLDemonio(string anio, string estado)
        {
            int lst = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Detener_Rol_HTML_Demonio");
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("estado", estado);
                lst  = sp.EjecutarProcedimientoTrans();

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        #endregion

        #region "Descargar Roles externos"

        public static List<RolActualizar> ListarRolesExternos(int anio, int tribunal)
        {
            List<RolActualizar> lst = new List<RolActualizar>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Listar_Rol_Externo_Anio_Tribunal");
                sp.AgregarParametro("anio", anio);
                sp.AgregarParametro("tribunal", tribunal);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new RolActualizar
                        {
                            TipoCausa = ds.Tables[0].Rows[i]["TIPO"].ToString(),
                            Rol = Int32.Parse(ds.Tables[0].Rows[i]["NUMERO"].ToString()),
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString()),
                            IdTribunal = Int32.Parse(ds.Tables[0].Rows[i]["TRIBUNAL"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Scanner Poder Judicial", 0);
                return lst;
            }
        }

        #endregion
    }
}
