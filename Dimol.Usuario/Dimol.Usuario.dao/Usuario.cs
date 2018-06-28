using System;
using System.Collections.Generic;
using System.Data;
using Dimol.dao;
using System.Diagnostics;
using Dimol.dto;

namespace Dimol.Usuario.dao
{
    public class Usuario
    {
        public static List<dto.Usuario> ListarUsuariosGrilla(int codemp, string nombre, string usuario, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Usuario> lst = new List<dto.Usuario>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Usuarios_Grilla");
                sp.AgregarParametro("codemp", codemp);
                if (nombre != null && nombre != "")
                {
                    sp.AgregarParametro("usr_nombre", nombre);
                }
                else
                {
                    sp.AgregarParametro("usr_nombre", DBNull.Value);
                }
                if (usuario != null && usuario != "")
                {
                    sp.AgregarParametro("usr_login", usuario);
                }
                else
                {
                    sp.AgregarParametro("usr_login", DBNull.Value);
                }
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("NRO DATOS COUNT" + ds.Tables.Count + "-" + sidx);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        lst.Add(new dto.Usuario()
                        {
                            IdUsuario = int.Parse(ds.Tables[1].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["Nombre"].ToString(),
                            Estado = ds.Tables[1].Rows[i]["Estado"].ToString(),
                            FechaUltimoIngreso = validaNULL(ds.Tables[1].Rows[i]["FechaUltimoIngreso"].ToString()),
                            FechaBloqueo = validaNULL(ds.Tables[1].Rows[i]["FechaBloqueo"].ToString())
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

        public static int ListarUsuariosGrillaCount(int codemp, string nombre, string usuario, string where, string sidx, string sord)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Usuarios_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                if (nombre != null && nombre != "")
                {
                    sp.AgregarParametro("usr_nombre", nombre);
                }
                else
                {
                    sp.AgregarParametro("usr_nombre", DBNull.Value);
                }
                if (usuario != null && usuario != "")
                {
                    sp.AgregarParametro("usr_login", usuario);
                }
                else
                {
                    sp.AgregarParametro("usr_login", DBNull.Value);
                }

                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine(codemp + nombre + usuario + where + sidx + sord);
                Debug.WriteLine("TAMAÑO DS :" + ds.Tables.Count + ds.Tables[0].Rows[0][0]);
                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[1].Rows[0][0].ToString());
                }
                Debug.WriteLine("COUNT :" + count);
                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static string validaNULL(string val)
        {
            if (val != null && val != "") {
                return val;
            } else {
                return "0";
            }
        }

        public static List<Combobox> ListarPreguntasSecretas(int idioma, string first)
        {
            //string salida = "";
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first))
            {
                lst.Add(new Combobox()
                {
                    Text = first,
                    Value = ""
                });
            }
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 6; i++)
                {
                    if (i == 1)
                    {

                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "Mascota");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
                        });
                    }
                    else if (i == 2)
                    {
                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "NomPad");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
                        });
                    }
                    else if (i == 3)
                    {
                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "FecNac");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
                        });
                    }
                    else if (i == 4)
                    {
                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "PelFav");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
                        });
                    }
                    else if (i == 5)
                    {
                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "LibFav");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
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

        public static List<Combobox> ListarEstados(int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first)) {
                lst.Add(new Combobox() {
                    Text = first,
                    Value = ""
                });
            }

            try {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 3; i++)
                {
                    if (i == 1)
                    {
                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "Hab");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
                        });
                    } else if (i == 2) {
                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "Bloq");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
                        });
                    }
                }

                return lst;
            } catch (Exception ex) {
                return lst;
            }
        }

        public static List<Combobox> ListarPermisos(int idioma, string first)
        {
            //string salida = "";
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first))
            {
                lst.Add(new Combobox()
                {
                    Text = first,
                    Value = ""
                });
            }
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 5; i++)
                {
                    if (i == 1)
                    {

                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "Lec");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
                        });
                    }
                    else if (i == 2)
                    {
                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "LeS");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
                        });
                    }
                    else if (i == 3)
                    {
                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "LSE");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
                        });
                    }
                    else if (i == 4)
                    {
                        sp = new StoredProcedure("Trae_Etiquetas");
                        sp.AgregarParametro("codigo", "CreUsu");
                        sp.AgregarParametro("idioma", idioma);
                        ds = sp.EjecutarProcedimiento();
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[0][0].ToString(),
                            Value = i.ToString()
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

        public static List<Combobox> ListarPerfiles(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Perfiles");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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

        public static List<dto.Sucursal> ListarSucursalesSinAsignar(int codemp, int id, string where, string sidx, string sord)
        {
            List<dto.Sucursal> lstPeriodos = new List<dto.Sucursal>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Sucursales_SinAsignar_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idUsuario", id);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                ds = sp.EjecutarProcedimiento();
                //

                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.Sucursal()
                        {

                            Id = Int16.Parse(ds.Tables[1].Rows[i]["esu_sucid"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["esu_nombre"].ToString(),
                            sel = convertirTrueFalse(ds.Tables[1].Rows[i]["sel"].ToString())
                            
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

        public static List<dto.Sucursal> ListarSucursalesAsignadas(int codemp, int id, string where, string sidx, string sord)
        {
            List<dto.Sucursal> lstPeriodos = new List<dto.Sucursal>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Sucursales_Asignados_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idUsuario", id);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                ds = sp.EjecutarProcedimiento();
                //

                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.Sucursal()
                        {

                            Id = Int16.Parse(ds.Tables[1].Rows[i]["esu_sucid"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["esu_nombre"].ToString(),
                            sel = convertirTrueFalse(ds.Tables[1].Rows[i]["sel"].ToString())

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

        public static bool convertirTrueFalse(string val)
        {
            bool returnval = true;
            if (val.Equals("S") && val != null) {
                returnval = true;
            } else {
                returnval = false;
            }

            return returnval;
        }

        public static int Insertar(dto.Usuario objAccion, int codemp, int suc)
        {
            try {
                #region Insertar Usuario
                int error1 = 0;
                int error2 = 0;
                int usrId = 0;

                DataSet dsn = new DataSet();

                StoredProcedure spn = new StoredProcedure("UltNum_Usuarios");
                spn.AgregarParametro("usr_codemp", codemp);
                dsn = spn.EjecutarProcedimiento();

                usrId = int.Parse(dsn.Tables[0].Rows[0][0].ToString());

                //Inserta Usuario
                if (objAccion.IdUsuario == 0) {
                    error1 = InsertarUsuario(objAccion, codemp, suc, usrId);

                    //Inserta Usuario Sucursal
                    if (error1 > 0)
                    {
                        error2 = InsertarUsuarioSucursal(codemp, suc, usrId);
                    }
                }
                #endregion

                #region Insertar Empleado
                int error3 = 0;
                int Emplid = 0;
                DataSet dsn2 = new DataSet();

                StoredProcedure spn2 = new StoredProcedure("UltNum_Empleados");
                spn2.AgregarParametro("epl_codemp", codemp);
                dsn2 = spn2.EjecutarProcedimiento();

                Emplid = int.Parse(dsn2.Tables[0].Rows[0][0].ToString());

                //Inserta Empleado
                if (objAccion.IdUsuario == 0)
                {
                    error3 = InsertarEmpleado(objAccion, codemp, suc, usrId, Emplid);
                }
                #endregion

                //Retorno
                if (error1 > 0 && error2 > 0 && error3 > 0) {
                    return 1;
                } else {
                    return -1; //Error
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        protected static int InsertarUsuario(dto.Usuario objAccion, int codemp, int suc, int usrId) {
            StoredProcedure sp1 = new StoredProcedure("Insertar_Usuarios");

            sp1.AgregarParametro("usr_codemp", codemp);
            sp1.AgregarParametro("usr_usrid", usrId);
            sp1.AgregarParametro("usr_nombre", objAccion.Nombre);
            sp1.AgregarParametro("usr_login", objAccion.Login);
            sp1.AgregarParametro("usr_password", objAccion.Clave);
            sp1.AgregarParametro("usr_mail", objAccion.Mail);
            //sp1.AgregarParametro("usr_tipquest", int.Parse(objAccion.TipoPregunta));
            //sp1.AgregarParametro("usr_answer", objAccion.Respuesta);
            sp1.AgregarParametro("usr_tipquest", 1);
            sp1.AgregarParametro("usr_answer", "");
            sp1.AgregarParametro("usr_sucid", suc);
            sp1.AgregarParametro("usr_prfid", int.Parse(objAccion.Perfil));
            sp1.AgregarParametro("usr_permisos", int.Parse(objAccion.Permiso));
            sp1.AgregarParametro("usr_feccambio", DBNull.Value);

            if (objAccion.CambiPassword == true)
            {
                sp1.AgregarParametro("usr_campass", "S");
            }
            else
            {
                sp1.AgregarParametro("usr_campass", "N");
            }

            return sp1.EjecutarProcedimientoTrans();
        }

        protected static int InsertarUsuarioSucursal(int codemp, int suc, int usrId)
        {
            StoredProcedure sp = new StoredProcedure("Insertar_Usuarios_Sucursal");

            sp.AgregarParametro("uss_codemp", codemp);
            sp.AgregarParametro("uss_usrid", usrId);
            sp.AgregarParametro("uss_sucid", suc);
            sp.AgregarParametro("uss_default", "N");

            return sp.EjecutarProcedimientoTrans();
        }

        protected static int InsertarEmpleado(dto.Usuario objAccion, int codemp, int suc, int usrId, int Emplid)
        {
            StoredProcedure sp2 = new StoredProcedure("Insertar_Empleados");

            sp2.AgregarParametro("epl_codemp", codemp);
            sp2.AgregarParametro("epl_emplid", Emplid);
            sp2.AgregarParametro("epl_rut", objAccion.Rut);
            sp2.AgregarParametro("epl_nombre", objAccion.Nombre);
            sp2.AgregarParametro("epl_mail", objAccion.Mail);
            sp2.AgregarParametro("epl_sucid", suc);
            sp2.AgregarParametro("epl_usrid", usrId);
            sp2.AgregarParametro("epl_digito", 0);

            sp2.AgregarParametro("epl_apepat", "");
            sp2.AgregarParametro("epl_apemat", "");
            sp2.AgregarParametro("epl_eemid", 1); //Siempre pertenece a "Empresa: Dimol"
            sp2.AgregarParametro("epl_comid", 0); //No es FK
            sp2.AgregarParametro("epl_direccion", "");
            sp2.AgregarParametro("epl_telefono", "");
            sp2.AgregarParametro("epl_celular", "");
            sp2.AgregarParametro("epl_huella", "");

            return sp2.EjecutarProcedimientoTrans();
        }

        public static int Actualizar(dto.Usuario objUsuario)
        {
            try
            {
                #region Actualizar los datos del usuario
                int error1 = 0;
                StoredProcedure sp1 = new StoredProcedure("_Actualizar_Usuario");
                sp1.AgregarParametro("USR_USRID", objUsuario.IdUsuario);
                sp1.AgregarParametro("USR_NOMBRE", objUsuario.Nombre);
                sp1.AgregarParametro("USR_LOGIN", objUsuario.Login);
                sp1.AgregarParametro("USR_PASSWORD", objUsuario.Clave);
                sp1.AgregarParametro("USR_GODLOG", objUsuario.IngresosOK);
                sp1.AgregarParametro("USR_BADLOG", objUsuario.IngresosMalos);
                sp1.AgregarParametro("USR_FECULTLOG", StringToDatetime(objUsuario.FechaUltimoIngreso));
                sp1.AgregarParametro("USR_FECBLOCK", StringToDatetime(objUsuario.FechaBloqueo));
                sp1.AgregarParametro("USR_MAIL", objUsuario.Mail);
                //sp1.AgregarParametro("USR_TIPQUEST", int.Parse(objUsuario.TipoPregunta));
                sp1.AgregarParametro("USR_TIPQUEST", 1);
                //sp1.AgregarParametro("USR_ANSWER", objUsuario.Respuesta);
                sp1.AgregarParametro("USR_ANSWER", "");
                sp1.AgregarParametro("USR_PRFID", int.Parse(objUsuario.Perfil));
                sp1.AgregarParametro("USR_PERMISOS", int.Parse(objUsuario.Permiso));
                sp1.AgregarParametro("USR_ESTADO", objUsuario.Estado);
                sp1.AgregarParametro("USR_CAMPASS", objUsuario.CambiPassword ? "S" : "N");
                sp1.AgregarParametro("USR_FECCAMBIO", StringToDatetime(objUsuario.FechaCambioPassword));

                error1 = sp1.EjecutarProcedimientoTrans();
                #endregion

                #region Actualizar los datos del empleado
                //Obtener empleado
                DataSet ds2 = new DataSet();
                StoredProcedure sp2 = new StoredProcedure("Trae_Usuario_Empleado");
                sp2.AgregarParametro("codemp", 1);
                sp2.AgregarParametro("usuario", objUsuario.IdUsuario);

                ds2 = sp2.EjecutarProcedimiento();

                Empleado.dto.Empleado Empleado = new Empleado.dto.Empleado(ds2.Tables[0].Rows[0].ItemArray);

                //Actualizar empleado
                int error2 = 0;
                StoredProcedure sp3 = new StoredProcedure("Update_Empleados");
                sp3.AgregarParametro("epl_codemp", Empleado.CodigoEmpleado);
                sp3.AgregarParametro("epl_emplid", Empleado.EmpleadoId);
                sp3.AgregarParametro("epl_rut", objUsuario.Rut);
                sp3.AgregarParametro("epl_nombre", objUsuario.Nombre);
                sp3.AgregarParametro("epl_apepat", Empleado.ApellidoPaterno);
                sp3.AgregarParametro("epl_apemat", Empleado.ApellidoMaterno);
                sp3.AgregarParametro("epl_eemid", Empleado.EstadoEmpleadoId);
                sp3.AgregarParametro("epl_comid", Empleado.ComId);
                sp3.AgregarParametro("epl_direccion", Empleado.Direccion);
                sp3.AgregarParametro("epl_telefono", Empleado.Telefono);
                sp3.AgregarParametro("epl_celular", Empleado.Celular);
                sp3.AgregarParametro("epl_mail", objUsuario.Mail);

                if (Empleado.FechaFin != null)
                {
                    sp3.AgregarParametro("epl_fecfin", Empleado.FechaFin);
                }

                sp3.AgregarParametro("epl_sucid", Empleado.SucursalId);
                sp3.AgregarParametro("epl_usrid", objUsuario.IdUsuario);
                sp3.AgregarParametro("epl_digito", Empleado.Digito);
                
                error2 = sp3.EjecutarProcedimientoTrans();
                #endregion

                //Retorno
                if (error1 > 0 && error2 > 0) {
                    return 1;
                } else {
                    return -1; //Error
                }
            } catch (Exception ex) {
                throw ex;
            }
        }

        public static dto.Usuario BuscarUsuarioPorIdUsuario(int IdUsuario)
        {
            try {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_UsuarioPorIdUsuario");
                sp.AgregarParametro("USRID", IdUsuario);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    var Usuario = new dto.Usuario()
                    {
                        Rut = ds.Tables[0].Rows[0]["EPL_RUT"].ToString(),
                        IdUsuario = int.Parse(ds.Tables[0].Rows[0]["USR_USRID"].ToString()),
                        IngresosOK = int.Parse(ds.Tables[0].Rows[0]["USR_GODLOG"].ToString()),
                        IngresosMalos = int.Parse(ds.Tables[0].Rows[0]["USR_BADLOG"].ToString()),

                        Nombre = ds.Tables[0].Rows[0]["USR_NOMBRE"].ToString(),
                        Estado = ds.Tables[0].Rows[0]["USR_ESTADO"].ToString(),
                        FechaIngreso = ds.Tables[0].Rows[0]["USR_FECING"].ToString(),
                        FechaUltimoIngreso = ds.Tables[0].Rows[0]["USR_FECULTLOG"].ToString(),
                        FechaBloqueo = ds.Tables[0].Rows[0]["USR_FECBLOCK"].ToString(),
                        FechaCambioPassword = ds.Tables[0].Rows[0]["USR_FECCAMBIO"].ToString(),
                        Login = ds.Tables[0].Rows[0]["USR_LOGIN"].ToString(),
                        Clave = ds.Tables[0].Rows[0]["USR_PASSWORD"].ToString(),
                        Mail = ds.Tables[0].Rows[0]["USR_MAIL"].ToString(),
                        TipoPregunta = ds.Tables[0].Rows[0]["USR_TIPQUEST"].ToString(),
                        Respuesta = ds.Tables[0].Rows[0]["USR_ANSWER"].ToString(),
                        Perfil = ds.Tables[0].Rows[0]["USR_PRFID"].ToString(),
                        Permiso = ds.Tables[0].Rows[0]["USR_PERMISOS"].ToString(),

                        CambiPassword = convertirTrueFalse(ds.Tables[0].Rows[0]["USR_CAMPASS"].ToString())
                    };

                    return Usuario;
                }
            } catch (Exception ex) {
                return null;
            }

            return null;
        }

        protected static DateTime StringToDatetime(string FechaString) {
            //2018-01-04 09:35:26.207
            //FechaString = "2018-01-04 09:35:26.207";
            //return DateTime.ParseExact(FechaString, "yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture);
            //return DateTime.ParseExact(FechaString, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            return DateTime.Now;
        }
    }
}