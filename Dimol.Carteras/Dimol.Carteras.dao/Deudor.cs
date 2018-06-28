using Dimol.Carteras.dto;
using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dao
{
    public class Deudor
    {
        #region "DatosDeudor"
        public static void BuscarDeudor(dto.Deudor objDeudor)
        {
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Deudor");
                sp.AgregarParametro("codemp", objDeudor.CodigoEmpresa);
                sp.AgregarParametro("ctcid", objDeudor.CodigoDeudor);
                ds = sp.EjecutarProcedimiento();
                DateTime fecha;
                int sociedad = 0;

                if (ds.Tables.Count > 0)
                {
                    objDeudor.CodigoDeudor = Int32.Parse(ds.Tables[0].Rows[0]["CTC_CTCID"].ToString());
                    objDeudor.CodigoEmpresa = Int32.Parse(ds.Tables[0].Rows[0]["CTC_CODEMP"].ToString());
                    objDeudor.Direccion = ds.Tables[0].Rows[0]["CTC_DIRECCION"].ToString();
                    objDeudor.Dv = ds.Tables[0].Rows[0]["CTC_DIGITO"].ToString();
                    objDeudor.EstadoDireccion = Int32.Parse(ds.Tables[0].Rows[0]["CTC_ESTDIR"].ToString());
                    if (DateTime.TryParse(ds.Tables[0].Rows[0]["CTC_FECING"].ToString(), out fecha))
                    {
                        objDeudor.FechaIngreso = fecha;
                    }
                    else
                    {
                        objDeudor.FechaIngreso = new DateTime();
                    }
                    objDeudor.IdComuna = Int32.Parse(ds.Tables[0].Rows[0]["CTC_COMID"].ToString());
                    if (Int32.TryParse(ds.Tables[0].Rows[0]["CTC_SOCID"].ToString(), out sociedad))
                    {
                        objDeudor.IdSociedad = sociedad;
                    }
                    else
                    {
                        objDeudor.IdSociedad = 0;
                    }
                    objDeudor.Sociedad = ds.Tables[0].Rows[0]["SOCIEDAD"].ToString();
                    objDeudor.Materno = ds.Tables[0].Rows[0]["CTC_APEMAT"].ToString();
                    objDeudor.NacionalExtranjero = ds.Tables[0].Rows[0]["CTC_NACEXT"].ToString();
                    objDeudor.Nombre = ds.Tables[0].Rows[0]["CTC_NOMBRE"].ToString();
                    objDeudor.NombreFantasia = ds.Tables[0].Rows[0]["CTC_NOMFANT"].ToString();
                    objDeudor.Numero = Int32.Parse(ds.Tables[0].Rows[0]["CTC_NUMERO"].ToString());
                    objDeudor.ParticularEmpresa = ds.Tables[0].Rows[0]["CTC_PARTEMP"].ToString();
                    objDeudor.Paterno = ds.Tables[0].Rows[0]["CTC_APEPAT"].ToString();
                    objDeudor.Quiebra = ds.Tables[0].Rows[0]["CTC_QUIEBRA"].ToString();
                    objDeudor.Rut = ds.Tables[0].Rows[0]["CTC_RUT"].ToString();
                    objDeudor.IdCiudad = Int32.Parse(ds.Tables[0].Rows[0]["COM_CIUID"].ToString());
                    objDeudor.IdRegion = Int32.Parse(ds.Tables[0].Rows[0]["CIU_REGID"].ToString());
                    objDeudor.IdPais = Int32.Parse(ds.Tables[0].Rows[0]["REG_PAIID"].ToString());
                    objDeudor.PreQuiebra = ds.Tables[0].Rows[0]["Prequiebra"].ToString();
                    objDeudor.Categoria = ds.Tables[0].Rows[0]["Categoria"].ToString();
                    objDeudor.SolicitaQuiebra = ds.Tables[0].Rows[0]["CTC_SOLICITA_QUIEBRA"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static int GuardarDocumentoEstampe(int codemp, int pclid, int ctcid, string[] param, string path, string nombre, string ext, int usrid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudores_Estampes_Insumos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("rolid", param[0]);
                sp.AgregarParametro("path", (object)path ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("ext", (object)ext ?? DBNull.Value);
                sp.AgregarParametro("insid", param[1]);
                sp.AgregarParametro("item", param[2]);
                sp.AgregarParametro("tpcid", param[3]);
                sp.AgregarParametro("numero", param[4]);
                sp.AgregarParametro("fecjud", (object) DateTime.Parse(param[5]) ?? DBNull.Value);
                sp.AgregarParametro("usrid", (object) usrid ?? DBNull.Value);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["DDE_DDEID"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static List<string> ListarRutaEstampes(int codemp, int pclid, int ctcid, string[] param)
        {
            List<string> lst = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Ruta_Estampes");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("rolid", param[0]);
                sp.AgregarParametro("insid", param[1]);
                sp.AgregarParametro("item", param[2]);
                sp.AgregarParametro("tpcid", param[3]);
                sp.AgregarParametro("numero", param[4]);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i]["Estampe"].ToString());
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<string> ListarRutaEstampesDeudor(int codemp, int pclid, int ctcid)
        {
            List<string> lst = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Ruta_Estampes_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);                
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i]["ARCHIVO"].ToString());
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static int EliminarDatosEstampes(int codemp, int pclid, int ctcid, string[] param)
        {
            int id = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Eliminar_Datos_Estampes");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("rolid", param[0]);
                sp.AgregarParametro("insid", param[1]);
                sp.AgregarParametro("item", param[2]);
                sp.AgregarParametro("tpcid", param[3]);
                sp.AgregarParametro("numero", param[4]);

                id = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Deudor.EliminarDatosEstampes: " + param[4], pclid);
                return -1;
            }

            return id;
        }

        public static BuscarDeudor BuscarDeudorCliente(int codemp, int pclid, int ctcid)
        {
            BuscarDeudor datos = new BuscarDeudor();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudor_Cliente");
                sp.AgregarParametro("codemp",codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();
                
                if (ds.Tables.Count > 0)
                {
                    datos.Pclid = ds.Tables[0].Rows[0]["Pclid"].ToString();
                    datos.RutCliente = ds.Tables[0].Rows[0]["RutCliente"].ToString();
                    datos.NombreCliente = ds.Tables[0].Rows[0]["NombreCliente"].ToString();
                    datos.Ctcid = ds.Tables[0].Rows[0]["Ctcid"].ToString();
                    datos.Rut = ds.Tables[0].Rows[0]["Rut"].ToString();
                    datos.NombreFantasia = ds.Tables[0].Rows[0]["NombreFantasia"].ToString();
                }

                return datos;
            }
            catch (Exception ex)
            {
                return datos;
            }
        }

        #region "Telefono"
        public static List<Telefono> ListarTelefonos(int codemp, int ctcid, string telefono, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Telefono> lst = new List<Telefono>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Telefonos_Contacto_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
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
                        lst.Add(new Telefono()
                        {
                            TipoContacto = ds.Tables[0].Rows[i]["TipoContacto"].ToString(),
                            CodigoArea = ds.Tables[0].Rows[i]["CodigoArea"].ToString(),
                            Comuna = Int32.Parse(ds.Tables[0].Rows[i]["Comuna"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Ddcid = Int32.Parse(ds.Tables[0].Rows[i]["ddcid"].ToString()),
                            DescEstado = ds.Tables[0].Rows[i]["DescEstado"].ToString(),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            EstadoDireccion = Int32.Parse(ds.Tables[0].Rows[i]["EstadoDireccion"].ToString()),
                            IdEstado = ds.Tables[0].Rows[i]["IdEstado"].ToString(),
                            NombreContacto = ds.Tables[0].Rows[i]["NombreContacto"].ToString(),
                            Numero = Int64.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            Ticid = Int32.Parse(ds.Tables[0].Rows[i]["ticid"].ToString()),
                            TipoTelefono = ds.Tables[0].Rows[i]["TipoTelefono"].ToString(),
                            Ciudad = Int32.Parse(ds.Tables[0].Rows[i]["Ciudad"].ToString()),
                            Region = Int32.Parse(ds.Tables[0].Rows[i]["Region"].ToString()),
                            Pais = Int32.Parse(ds.Tables[0].Rows[i]["Pais"].ToString())
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

        public static int ListarTelefonosCount(int codemp, int ctcid, string telefono, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Telefonos_Contacto_Deudor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        public static List<TelefonoDeudor> ListarTelefonosDeudor(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<TelefonoDeudor> lst = new List<TelefonoDeudor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Telefonos_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new TelefonoDeudor()
                        {
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            IdTipoTelefono = ds.Tables[0].Rows[i]["IdTipoTelefono"].ToString(),
                            TipoTelefono = ds.Tables[0].Rows[i]["TipoTelefono"].ToString(),
                            EstadoTelefono = ds.Tables[0].Rows[i]["EstadoTelefono"].ToString(),
                            IdEstadoTelefono = ds.Tables[0].Rows[i]["IdEstadoTelefono"].ToString() 
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

        public static int ListarTelefonosDeudorCount(int codemp, int ctcid,  string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Telefonos_Deudor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        public static void InsertarTelefono(dto.TelefonoDeudor objTelefono, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Deudores_Telefonos");
                sp.AgregarParametro("ddt_codemp", codemp);
                sp.AgregarParametro("ddt_ctcid", objTelefono.Ctcid);
                sp.AgregarParametro("ddt_numero", objTelefono.Numero);
                sp.AgregarParametro("ddt_tipo", objTelefono.TipoTelefono);
                sp.AgregarParametro("ddt_estado", objTelefono.EstadoTelefono);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarTelefono: " + objTelefono.Numero, objTelefono.Ctcid);
                throw ex;
            }

        }

        public static void BorrarTelefono(int codemp, string ctcid, string telefono)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Deudores_Telefonos_Todos");
                sp.AgregarParametro("ddt_codemp", codemp);
                sp.AgregarParametro("ddt_ctcid", ctcid);
                sp.AgregarParametro("ddt_numero", telefono);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void EditarTelefono(dto.TelefonoDeudor objTelefono, int codemp, string ctcid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Deudores_Telefonos");
                sp.AgregarParametro("ddt_codemp", codemp);
                sp.AgregarParametro("ddt_ctcid", objTelefono.Ctcid);
                sp.AgregarParametro("ddt_numero", objTelefono.Numero);
                sp.AgregarParametro("ddt_tipo", objTelefono.TipoTelefono);
                sp.AgregarParametro("ddt_estado", objTelefono.EstadoTelefono);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetCuentaProvcliBanco(int Pclid, int Tipo, int codemp)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Cuentas_Provcli_Banco");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", Pclid);
                sp.AgregarParametro("tipo", Tipo);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        salida = ds.Tables[0].Rows[i]["CUENTA"].ToString();
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static int GetDeudorCodigoCargaCount(int Pclid, int Ctcid, string EstCpbt, int CodCarga, int codemp)
        {
            int salida = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Deudor_Codigo_Carga_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", Pclid);
                sp.AgregarParametro("ctcid", Ctcid);
                sp.AgregarParametro("estcpbt", EstCpbt);
                sp.AgregarParametro("codcarga", CodCarga);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        salida = Int32.Parse(ds.Tables[0].Rows[i]["CUENTA"].ToString());
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static string ListarTipoTelefono(int idioma)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                string value="";
                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipTel" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    switch (i)
                    {
                        case 1:
                            value = "C";
                            break;
                        case 2:
                            value = "M";
                            break;
                        case 3:
                            value = "O";
                            break;
                        case 4:
                            value = "F";
                            break;
                    }


                    if (i == 1)
                    {
                        salida += value + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + value + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string ListarEstadoTelefono(int idioma)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                string value = "";
                for (int i = 1; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "EstTel" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    switch (i)
                    {
                        case 1:
                            value = "A";
                            break;
                        case 2:
                            value = "C";
                            break;
                        case 3:
                            value = "M";
                            break;
                    }


                    if (i == 1)
                    {
                        salida += value + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + value + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static List<Combobox> ListarTipoTelefonoC(int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                string value = "";
                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipTel" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    switch (i)
                    {
                        case 1:
                            value = "C";
                            break;
                        case 2:
                            value = "M";
                            break;
                        case 3:
                            value = "O";
                            break;
                        case 4:
                            value = "F";
                            break;
                    }
                    lst.Add(new Combobox { Text = ds.Tables[0].Rows[0][0].ToString(), Value = value });
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<Combobox> ListarEstadoTelefonoC(int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                string value = "";
                for (int i = 1; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "EstTel" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    switch (i)
                    {
                        case 1:
                            value = "A";
                            break;
                        case 2:
                            value = "C";
                            break;
                        case 3:
                            value = "M";
                            break;
                    }
                    lst.Add(new Combobox { Text = ds.Tables[0].Rows[0][0].ToString(), Value = value });
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        #endregion
        #region "Email"
        public static List<Email> ListarEmail(int codemp, int ctcid, string email, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Email> lst = new List<Email>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Email_Contacto_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
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
                        lst.Add(new Email()
                        {
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            TipoContacto = ds.Tables[0].Rows[i]["TipoContacto"].ToString(),
                            NombreContacto = ds.Tables[0].Rows[i]["NombreContacto"].ToString(),
                            Mail = ds.Tables[0].Rows[i]["Mail"].ToString(),
                            Masivo = ds.Tables[0].Rows[i]["Masivo"].ToString(),
                            DescTipo = ds.Tables[0].Rows[i]["DescTipo"].ToString(),
                            TipoEmail = ds.Tables[0].Rows[i]["TipoEmail"].ToString(),
                            IdEstado = ds.Tables[0].Rows[i]["IdEstado"].ToString(),
                            Ddcid = Int32.Parse(ds.Tables[0].Rows[i]["ddcid"].ToString()),
                            EstadoDireccion = Int32.Parse(ds.Tables[0].Rows[i]["EstadoDireccion"].ToString()),
                            Comuna = Int32.Parse(ds.Tables[0].Rows[i]["Comuna"].ToString()),
                            Ticid = Int32.Parse(ds.Tables[0].Rows[i]["ticid"].ToString()),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            Ciudad = Int32.Parse(ds.Tables[0].Rows[i]["Ciudad"].ToString()),
                            Region = Int32.Parse(ds.Tables[0].Rows[i]["Region"].ToString()),
                            Pais = Int32.Parse(ds.Tables[0].Rows[i]["Pais"].ToString())
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

        public static List<Email> ListarEmailProv(int codemp, int ctcid, string email, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Email> lst = new List<Email>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Email_Contacto_Deudor_Grilla_Prov");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
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
                        lst.Add(new Email()
                        {
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            TipoContacto = ds.Tables[0].Rows[i]["TipoContacto"].ToString(),
                            NombreContacto = ds.Tables[0].Rows[i]["NombreContacto"].ToString(),
                            Mail = ds.Tables[0].Rows[i]["Mail"].ToString(),
                            Masivo = ds.Tables[0].Rows[i]["Masivo"].ToString(),
                            DescTipo = ds.Tables[0].Rows[i]["DescTipo"].ToString(),
                            TipoEmail = ds.Tables[0].Rows[i]["TipoEmail"].ToString(),
                            IdEstado = ds.Tables[0].Rows[i]["IdEstado"].ToString(),
                            Ddcid = Int32.Parse(ds.Tables[0].Rows[i]["ddcid"].ToString()),
                            EstadoDireccion = Int32.Parse(ds.Tables[0].Rows[i]["EstadoDireccion"].ToString()),
                            Comuna = Int32.Parse(ds.Tables[0].Rows[i]["Comuna"].ToString()),
                            Ticid = Int32.Parse(ds.Tables[0].Rows[i]["ticid"].ToString()),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString(),
                            Ciudad = Int32.Parse(ds.Tables[0].Rows[i]["Ciudad"].ToString()),
                            Region = Int32.Parse(ds.Tables[0].Rows[i]["Region"].ToString()),
                            Pais = Int32.Parse(ds.Tables[0].Rows[i]["Pais"].ToString())
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

        public static int ListarEmailCount(int codemp, int ctcid, string email, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Email_Contacto_Deudor_Grilla_count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        public static List<EmailDeudor> ListarEmailDeudor(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<EmailDeudor> lst = new List<EmailDeudor>();
            string masivo = "false";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Email_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
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
                        if (ds.Tables[0].Rows[i]["Masivo"].ToString() == "S")
                        {
                            masivo = "true";
                        }
                        else
                        {
                            masivo = "false";
                        }
                        lst.Add(new EmailDeudor()
                        {
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            TipoMail = ds.Tables[0].Rows[i]["TipoMail"].ToString(),
                            IdTipoMail = ds.Tables[0].Rows[i]["IdTipoMail"].ToString(),
                            Mail = ds.Tables[0].Rows[i]["Mail"].ToString(),
                            Masivo = masivo,
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString())
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

        public static int ListarEmailDeudorCount(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Email_Deudor_Grilla_count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        public static void InsertarEmail(dto.EmailDeudor obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Deudores_Mail");
                sp.AgregarParametro("ddm_codemp", obj.Codemp);
                sp.AgregarParametro("ddm_ctcid", obj.Ctcid);
                sp.AgregarParametro("ddm_mail", obj.Mail);
                sp.AgregarParametro("ddm_masivo", obj.Masivo);
                sp.AgregarParametro("ddm_tipo", obj.TipoMail);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarEmail: "+ obj.Mail , obj.Ctcid);
                throw ex;
            }

        }

        public static void EditarEmail(dto.EmailDeudor obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Deudores_Mail");
                sp.AgregarParametro("ddm_codemp", obj.Codemp);
                sp.AgregarParametro("ddm_ctcid", obj.Ctcid);
                sp.AgregarParametro("ddm_mail", obj.Mail);
                sp.AgregarParametro("ddm_masivo", obj.Masivo);
                sp.AgregarParametro("ddm_tipo", obj.TipoMail);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void BorrarEmail(string codemp, string ctcid, string mail)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Deudores_Mail");
                sp.AgregarParametro("ddm_codemp", codemp);
                sp.AgregarParametro("ddm_ctcid", ctcid);
                sp.AgregarParametro("ddm_mail", mail);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void BorrarEmailTodos(string codemp, string ctcid, string mail)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Deudores_Mail_Todos");
                sp.AgregarParametro("ddm_codemp", codemp);
                sp.AgregarParametro("ddm_ctcid", ctcid);
                sp.AgregarParametro("ddm_mail", mail);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void BorrarEmailProv(string codemp, string ctcid, string mail)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Deudores_Mail_Prov");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("mail", mail);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ListarTipoMail(int idioma)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                string value = "";
                for (int i = 1; i < 3; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipMail" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    switch (i)
                    {
                        case 1:
                            value = "P";
                            break;
                        case 2:
                            value = "E";
                            break;
                    }


                    if (i == 1)
                    {
                        salida += value + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + value + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        #endregion
        #region "Historial"
        public static List<Historial> ListarHstorial(int codemp, int pclid, int ctcid, int idioma,string tipo, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Historial> lst = new List<Historial>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Historial_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("tipo", tipo);
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
                        lst.Add(new Historial()
                        {
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString() ?? "",
                            Fecha = DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString()),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString() ?? "",
                            NombreUsuario = ds.Tables[0].Rows[i]["NombreUsuario"].ToString() ?? "",
                            TipoContacto = ds.Tables[0].Rows[i]["TipoContacto"].ToString() ?? "",
                            NombreContacto = ds.Tables[0].Rows[i]["NombreContacto"].ToString() ?? "",
                            Accion = ds.Tables[0].Rows[i]["Accion"].ToString() ?? "0",
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString() ?? "0",
                            Agrupa = ds.Tables[0].Rows[i]["Agrupa"].ToString() ?? "0",
                            Utiliza = ds.Tables[0].Rows[i]["Utiliza"].ToString() ?? "",
                            Ticid = ds.Tables[0].Rows[i]["Ticid"].ToString() ?? "0",
                            Contacto = ds.Tables[0].Rows[i]["Contacto"].ToString() ?? "0",
                            Telefono = ds.Tables[0].Rows[i]["Telefono"].ToString() ?? "0"
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

        public static int ListarHistorialCount(int codemp, int pclid, int ctcid, int idioma, string tipo, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Historial_Deudor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        #endregion
        #region "Documentos Cliente"
        public static List<Documento> ListarDocCliente(int codemp, int pclid, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Documento> lst = new List<Documento>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Doc_Cliente_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new Documento()
                        {
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Dcdid = Int32.Parse(ds.Tables[0].Rows[i]["Dcdid"].ToString()),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString() ?? "",
                            NombreArchivo = ds.Tables[0].Rows[i]["NombreArchivo"].ToString() ?? "",
                            UrlArchivo = ds.Tables[0].Rows[i]["UrlArchivo"].ToString() ?? ""
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

        public static int ListarDocClienteCount(int codemp, int pclid, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Doc_Cliente_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        public static List<Documento> ListarDocDeudor(int codemp, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Documento> lst = new List<Documento>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Doc_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new Documento()
                        {
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Dcdid = Int32.Parse(ds.Tables[0].Rows[i]["Dcdid"].ToString()),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString() ?? "",
                            NombreArchivo = ds.Tables[0].Rows[i]["NombreArchivo"].ToString() ?? "",
                            UrlArchivo = ds.Tables[0].Rows[i]["UrlArchivo"].ToString() ?? ""
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

        public static int ListarDocDeudorCount(int codemp, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Doc_Deudor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        public static List<Autocomplete> ListarRutNombreDeudor(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Nombre_Deudor");
                sp.AgregarParametro("texto", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Autocomplete> ListarRutNombreDeudorPJ(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Nombre_Deudor_PJ");
                sp.AgregarParametro("texto", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static string ListarNombreDeudorPJ(string nombre)
        {
            string lst = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Nombre_Deudor_PJ");
                sp.AgregarParametro("texto", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {                   
                    lst = ds.Tables[0].Rows[0][0].ToString();                   
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        #endregion
        #region "Numero Rol"
        public static List<Rol> ListarRol(int codemp, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Rol> lst = new List<Rol>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new Rol()
                        {
                            Rolid = Int32.Parse(ds.Tables[0].Rows[i]["Rolid"].ToString()),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString() ?? "",
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString() ?? "",
                            NumeroRol = ds.Tables[0].Rows[i]["NumeroRol"].ToString() ?? "",
                            Causa = ds.Tables[0].Rows[i]["Causa"].ToString() ?? "",
                            Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString() ?? "",
                            Materia = ds.Tables[0].Rows[i]["Materia"].ToString() ?? "",
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString() ?? "",
                            Bloqueo = ds.Tables[0].Rows[i]["Bloqueo"].ToString() ?? "",
                            EstAdm =   ds.Tables[0].Rows[i]["EstAdm"].ToString() ?? "",
                            FechaAccion = ds.Tables[0].Rows[i]["FechaAccion"].ToString() ?? "",
                            AccionJudicial = ds.Tables[0].Rows[i]["AccionJudicial"].ToString() ?? ""
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

        public static int ListarRolCount(int codemp, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        public static List<DocumentoRol> ListarDocRol(int codemp, int rolid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<DocumentoRol> lst = new List<DocumentoRol>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Doc_Rol_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
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
                        lst.Add(new DocumentoRol()
                        {
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()?? "0"),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString() ?? "",
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString() ?? "",
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString())
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

        public static int ListarDocRolCount(int codemp, int rolid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Doc_Rol_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        public static List<EstadosRol> ListarEstadoRol(int codemp, int rolid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<EstadosRol> lst = new List<EstadosRol>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Rol_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
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
                        lst.Add(new EstadosRol()
                        {
                            IdEstado = ds.Tables[0].Rows[i]["IdEstado"].ToString() ?? "",
                            Materia = ds.Tables[0].Rows[i]["Materia"].ToString() ?? "",
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString() ?? "",
                            Fecha = DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString()),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            Usuario = ds.Tables[0].Rows[i]["Usuario"].ToString()
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

        public static int ListarEstadoRolCount(int codemp, int rolid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Estados_Rol_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        #endregion
        #region "Contactos"
        public static List<Contacto> ListarContactos(int codemp, int ctcid,  int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Contacto> lst = new List<Contacto>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Contacto_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new Contacto()
                        {
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                            Ddcid = Int32.Parse(ds.Tables[0].Rows[i]["ddcid"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoContacto = ds.Tables[0].Rows[i]["EstadoContacto"].ToString(),
                            Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                            Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString()
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
        public static int ListarContactosCount(int codemp, int ctcid,  int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Contacto_Deudor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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
        public static void InsertarContacto(dto.Contacto obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudores_Contactos");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("ticid", obj.Tipo);
                sp.AgregarParametro("nombre", obj.Nombre);
                sp.AgregarParametro("comid", string.IsNullOrEmpty(obj.Comuna) ? DBNull.Value : (object)obj.Comuna);
                sp.AgregarParametro("direccion",string.IsNullOrEmpty(obj.Direccion) ? DBNull.Value : (object)obj.Direccion);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void EditarContacto(dto.Contacto obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Deudores_Contactos");
                sp.AgregarParametro("ddc_codemp", obj.Codemp);
                sp.AgregarParametro("ddc_ctcid", obj.Ctcid);
                sp.AgregarParametro("ddc_ddcid", obj.Ddcid);
                sp.AgregarParametro("ddc_ticid", obj.Tipo);
                sp.AgregarParametro("ddc_nombre", obj.Nombre);
                sp.AgregarParametro("ddc_comid", obj.Comuna);
                sp.AgregarParametro("ddc_direccion", obj.Direccion);
                sp.AgregarParametro("ddc_estdir", 1);
                sp.AgregarParametro("ddc_estado", obj.EstadoContacto);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void BorrarContacto(string codemp, string ctcid, string ddcid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Deudores_Contactos");
                sp.AgregarParametro("ddc_codemp", codemp);
                sp.AgregarParametro("ddc_ctcid", ctcid);
                sp.AgregarParametro("ddc_ddcid", ddcid);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ListarTipoContacto(int codemp,int idioma)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipo_Contacto");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            salida += ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                        }
                        else
                        {
                            salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                        }
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static List<Combobox> ListarTipoContactoC(int codemp, int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipo_Contacto");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox { Text = ds.Tables[0].Rows[i][1].ToString(), Value = ds.Tables[0].Rows[i][0].ToString() });
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static string ListarComunaGrilla()
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Comuna_Grilla");
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            salida += ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                        }
                        else
                        {
                            salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds.Tables[0].Rows[i][1].ToString();
                        }
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        #endregion
        #region "Observaciones"
        public static List<Historial> ListarObservacion(int codemp, int pclid, int ctcid, int idioma, string tipo, string where, string sidx, string sord, int inicio, int limite)
        {
            List<Historial> lst = new List<Historial>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Observacion_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("tipo", tipo);
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
                        lst.Add(new Historial()
                        {
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString() ?? "",
                            Fecha = DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString()),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString() ?? "",
                            NombreUsuario = ds.Tables[0].Rows[i]["NombreUsuario"].ToString() ?? "",
                            TipoContacto = ds.Tables[0].Rows[i]["TipoContacto"].ToString() ?? "",
                            NombreContacto = ds.Tables[0].Rows[i]["NombreContacto"].ToString() ?? "",
                            Accion = ds.Tables[0].Rows[i]["Accion"].ToString() ?? "0",
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString() ?? "0",
                            Agrupa = ds.Tables[0].Rows[i]["Agrupa"].ToString() ?? "0",
                            Utiliza = ds.Tables[0].Rows[i]["Utiliza"].ToString() ?? "",
                            Ticid = ds.Tables[0].Rows[i]["Ticid"].ToString() ?? "0",
                            Contacto = ds.Tables[0].Rows[i]["Contacto"].ToString() ?? "0",
                            Telefono = ds.Tables[0].Rows[i]["Telefono"].ToString() ?? "0"
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

        public static int ListarObservacionCount(int codemp, int pclid, int ctcid, int idioma, string tipo, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Observacion_Deudor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
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

        #endregion
        #endregion


        #region "Buscar Deudores"
        public static List<Autocomplete> ListarRutDeudor(string numero)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Deudor");
                sp.AgregarParametro("rut", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Combobox> ListarEstado(int idioma)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                lst.Add(new Combobox() { Text = "Seleccione", Value = "" });
                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "EstCpbt" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();

                    lst.Add(new Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = IdEstado(i)
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static string IdEstado(int id)
        {
            switch (id)
            {
                case 1:
                    return "V";
                case 2:
                    return "F";
                case 3:
                    return "N";
                case 4:
                    return "J";
                default:
                    return "";
            }
        }

        public static List<BuscarDeudor> ListarDeudores(int codemp, int sucid, int usrid, string pclid, string nombre, string paterno, string materno, string rut, string nomFant, string telefono, string email, string direccion, string gestor, string rol, string estado, string numCPBT, string where, string sidx, string sord, int inicio, int limite)
        {
            List<BuscarDeudor> lst = new List<BuscarDeudor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("usrid", usrid);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)nombre?? DBNull.Value);
                sp.AgregarParametro("paterno", (object)paterno ?? DBNull.Value);
                sp.AgregarParametro("materno", (object)materno ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("nom_fant", (object)nomFant ?? DBNull.Value);
                sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
                sp.AgregarParametro("direccion", (object)direccion ?? DBNull.Value);
                sp.AgregarParametro("gestor", (object)gestor ?? DBNull.Value);
                sp.AgregarParametro("rol", (object)rol ?? DBNull.Value);
                sp.AgregarParametro("estado", (object)estado ?? DBNull.Value);
                sp.AgregarParametro("num_CPBT", (object)numCPBT ?? DBNull.Value);
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
                        lst.Add(new dto.BuscarDeudor()
                        {
                            Pclid = ds.Tables[0].Rows[i]["pclid"].ToString(),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["ctcid"].ToString(),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            Gestor = ds.Tables[0].Rows[i]["Gestor"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["Rol"].ToString(),
                            Gesid = ds.Tables[0].Rows[i]["gesid"].ToString(),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["row"].ToString()),
                            TipoCliente = ds.Tables[0].Rows[i]["TipoCliente"].ToString()
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

        public static List<BuscarDeudor> ListarDeudor(int codemp, int sucid, int usrid,  string rut)
        {
            List<BuscarDeudor> lst = new List<BuscarDeudor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("usrid", usrid);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.BuscarDeudor()
                        {
                            Pclid = ds.Tables[0].Rows[i]["pclid"].ToString(),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["ctcid"].ToString(),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            Gestor = ds.Tables[0].Rows[i]["Gestor"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["Rol"].ToString(),
                            Gesid = ds.Tables[0].Rows[i]["gesid"].ToString()
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

        public static List<BuscarDeudor> ListarDeudorCli(int codemp, int sucid, int usrid, string rut, int cli)
        {
            List<BuscarDeudor> lst = new List<BuscarDeudor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudor_Cli");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("usrid", usrid);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("cli", cli);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.BuscarDeudor()
                        {
                            Pclid = ds.Tables[0].Rows[i]["pclid"].ToString(),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["ctcid"].ToString(),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            Gestor = ds.Tables[0].Rows[i]["Gestor"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["Rol"].ToString(),
                            Gesid = ds.Tables[0].Rows[i]["gesid"].ToString()
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

        public static int ListarDeudoresCount(int codemp, int sucid, int usrid, string pclid, string nombre, string paterno, string materno, string rut, string nomFant, string telefono, string email, string direccion, string gestor, string rol, string estado, string numCPBT, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("usrid", usrid);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("paterno", (object)paterno ?? DBNull.Value);
                sp.AgregarParametro("materno", (object)materno ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("nom_fant", (object)nomFant ?? DBNull.Value);
                sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
                sp.AgregarParametro("direccion", (object)direccion ?? DBNull.Value);
                sp.AgregarParametro("gestor", (object)gestor ?? DBNull.Value);
                sp.AgregarParametro("rol", (object)rol ?? DBNull.Value);
                sp.AgregarParametro("estado", (object)estado ?? DBNull.Value);
                sp.AgregarParametro("num_CPBT", (object)numCPBT ?? DBNull.Value);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["ctcid"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        #endregion

        #region "Deudor"
        public static List<BuscarDeudor> ListarBuscarDeudoresGrilla(int codemp, int usrid,  string nombre, string paterno, string materno, string rut, string nomFant, string telefono, string email, string direccion, string where, string sidx, string sord, int inicio, int limite)
        {
            List<BuscarDeudor> lst = new List<BuscarDeudor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudores_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("usrid", usrid);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("paterno", (object)paterno ?? DBNull.Value);
                sp.AgregarParametro("materno", (object)materno ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("nom_fant", (object)nomFant ?? DBNull.Value);
                sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
                sp.AgregarParametro("direccion", (object)direccion ?? DBNull.Value);
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
                        lst.Add(new dto.BuscarDeudor()
                        {
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            ApellidoPaterno = ds.Tables[0].Rows[i]["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = ds.Tables[0].Rows[i]["ApellidoMaterno"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString()
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

        public static int ListarBuscarDeudoresCount(int codemp, int usrid, string nombre, string paterno, string materno, string rut, string nomFant, string telefono, string email, string direccion,string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudores_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("usrid", usrid);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("paterno", (object)paterno ?? DBNull.Value);
                sp.AgregarParametro("materno", (object)materno ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("nom_fant", (object)nomFant ?? DBNull.Value);
                sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
                sp.AgregarParametro("direccion", (object)direccion ?? DBNull.Value);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static int BuscarIdDeudor(string rut, int codemp)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Id_Deudor");
                sp.AgregarParametro("rut", (object)rut.TrimStart('0') ?? DBNull.Value);
                sp.AgregarParametro("codemp", codemp );
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0 )
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Int32.Parse(ds.Tables[0].Rows[0]["ctcid"].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
       
        public static int GuardarDeudor(int codemp, int ctcid, string nombre, string paterno, string materno, string rut, 
                                        string nomFant, int idComuna, string partEmp, string direccion, string idSociedad,
                                        string quiebra, string nacional, int estadoDireccion, bool solicitaQuiebra)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Guardar_Deudor");
                sp.AgregarParametro("ctc_codemp", codemp);
                sp.AgregarParametro("ctc_ctcid", ctcid);
                sp.AgregarParametro("ctc_rut", rut);
                sp.AgregarParametro("ctc_numero", rut.Substring(0,rut.Length-1));
                sp.AgregarParametro("ctc_digito", rut.Substring(rut.Length-1,1));
                sp.AgregarParametro("ctc_nombre", nombre.TrimEnd());
                sp.AgregarParametro("ctc_apepat", paterno.TrimEnd());
                sp.AgregarParametro("ctc_apemat", string.IsNullOrEmpty(materno.TrimEnd()) ? DBNull.Value : (object)materno.TrimEnd());
                sp.AgregarParametro("ctc_nomfant", nomFant == null ? nombre.TrimEnd() + " " + paterno.TrimEnd() + " " + materno.TrimEnd() : nomFant.TrimEnd());
                sp.AgregarParametro("ctc_comid", idComuna);
                sp.AgregarParametro("ctc_direccion", direccion);
                sp.AgregarParametro("ctc_partemp", partEmp);
                sp.AgregarParametro("ctc_socid", idSociedad == "0" || string.IsNullOrEmpty(idSociedad) ? DBNull.Value : (object)idSociedad);
                sp.AgregarParametro("ctc_quiebra", quiebra);
                sp.AgregarParametro("ctc_nacext", nacional);
                sp.AgregarParametro("ctc_estdir", estadoDireccion);
                sp.AgregarParametro("ctc_solicita_quiebra", (solicitaQuiebra) ? "S" : "N");
                ds = sp.EjecutarProcedimiento();
                //int error = sp.EjecutarProcedimientoTrans();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["ctcid"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int EditarDeudorParcial(int codemp, int ctcid, string nombre, string paterno, string materno, string nomFant, int idComuna, string direccion, int estadoDireccion)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Editar_Deudor_Parcial");
                sp.AgregarParametro("ctc_codemp", codemp);
                sp.AgregarParametro("ctc_ctcid", ctcid);
                sp.AgregarParametro("ctc_nombre", nombre);
                sp.AgregarParametro("ctc_apepat", paterno);
                sp.AgregarParametro("ctc_apemat", string.IsNullOrEmpty(materno) ? DBNull.Value : (object)materno);
                sp.AgregarParametro("ctc_nomfant", nomFant == null ? nombre + " " + paterno + " " + materno : nomFant);
                sp.AgregarParametro("ctc_comid", idComuna);
                sp.AgregarParametro("ctc_direccion", direccion);
                sp.AgregarParametro("ctc_estdir", estadoDireccion);
                ds = sp.EjecutarProcedimiento();
                //int error = sp.EjecutarProcedimientoTrans();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["ctcid"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int BuscarContacto( int codemp, int ctcid, int ticid ,string nombre)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Contacto");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ticid", ticid);
                sp.AgregarParametro("nombre", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Int32.Parse(ds.Tables[0].Rows[0]["ddcid"].ToString());
                    }
                    else 
                    {
                        return 0;
                    }

                    
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static List<Contacto> ListarContactos(int codemp, int ctcid)
        {
            List<Contacto> lst = new List<Contacto>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Contactos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            lst.Add(new Contacto()
                            {
                                Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                                Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ctcid"].ToString()),
                                Ddcid = Int32.Parse(ds.Tables[0].Rows[i]["ddcid"].ToString()),
                                Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                                Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                                Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                                EstadoContacto = ds.Tables[0].Rows[i]["EstadoContacto"].ToString(),
                                Comuna = ds.Tables[0].Rows[i]["Comuna"].ToString(),
                                Direccion = ds.Tables[0].Rows[i]["Direccion"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.Deudor.ListarContactos", ctcid);
            }
            return lst;
        }

        #endregion

        #region "Deudor Cpbt"

        public static List<Combobox> ListarTipoDocumento(int codemp, int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Tipos_Documentos_Deudor");
                sp.AgregarParametro("clb_codemp", codemp);
                sp.AgregarParametro("tci_idid", idioma);
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
                            Value = ds.Tables[0].Rows[i]["tci_tpcid"].ToString(),
                            Text = ds.Tables[0].Rows[i]["tci_nombre"].ToString()
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

        public static List<DeudorDocumento> ListarDeudoresCpbt(int codemp, int idioma, string pclid, string nombre, string paterno, string materno, string rut, string nomFant, string tipoDocumento, string numero, string sbcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<DeudorDocumento> lst = new List<DeudorDocumento>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudor_Cpbt_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("sbcid", (object)sbcid ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("paterno", (object)paterno ?? DBNull.Value);
                sp.AgregarParametro("materno", (object)materno ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("nom_fant", (object)nomFant ?? DBNull.Value);
                sp.AgregarParametro("tipoDocumento", (object)tipoDocumento ?? DBNull.Value);
                sp.AgregarParametro("numero", (object)numero ?? DBNull.Value);
                // sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
                // sp.AgregarParametro("email", (object)email ?? DBNull.Value);
                // sp.AgregarParametro("direccion", (object)direccion ?? DBNull.Value);
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
                        lst.Add(new dto.DeudorDocumento()
                        {
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            Ccbid = ds.Tables[0].Rows[i]["Ccbid"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            FechaDocumento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString())
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

        public static int ListarDeudoresCpbtCount(int codemp, int idioma, string pclid, string nombre, string paterno, string materno, string rut, string nomFant, string tipoDocumento, string numero, string telefono, string email, string direccion, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudor_Cpbt_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("paterno", (object)paterno ?? DBNull.Value);
                sp.AgregarParametro("materno", (object)materno ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("nom_fant", (object)nomFant ?? DBNull.Value);
                sp.AgregarParametro("tipoDocumento", (object)tipoDocumento ?? DBNull.Value);
                sp.AgregarParametro("numero", (object)numero ?? DBNull.Value);
                sp.AgregarParametro("telefono", (object)telefono ?? DBNull.Value);
                sp.AgregarParametro("email", (object)email ?? DBNull.Value);
                sp.AgregarParametro("direccion", (object)direccion ?? DBNull.Value);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static List<dto.DeudorDocumento> ListarDeudoresMailCochaCpbt(int codemp, int idioma, string pclid, string ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DeudorDocumento> lst = new List<dto.DeudorDocumento>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudor_Cpbt_Mail_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("ctcid", (object)ctcid ?? DBNull.Value);
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
                        lst.Add(new dto.DeudorDocumento()
                        {
                            RutCliente = ds.Tables[0].Rows[i]["RutCliente"].ToString(),
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Ctcid = ds.Tables[0].Rows[i]["Ctcid"].ToString(),
                            Ccbid = ds.Tables[0].Rows[i]["Ccbid"].ToString(),
                            NombreFantasia = ds.Tables[0].Rows[i]["NombreFantasia"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            FechaDocumento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Moneda = ds.Tables[0].Rows[i]["ccb_codmon"].ToString(),
                            Carga = ds.Tables[0].Rows[i]["Carga"].ToString(),
                            Negocio = ds.Tables[0].Rows[i]["Negocio"].ToString()
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

        public static int ListarDeudoresMailCochaCpbtCount(int codemp, int idioma, string pclid, string ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Deudor_Cpbt_Mail_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("ctcid", (object)ctcid ?? DBNull.Value);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static List<Combobox> ListarBancos(int codemp)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Bancos_Enviar_Email");
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
            }
            return lst;
        }

        public static List<Combobox> ListarBancos()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Bancos");
                
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
            }
            return lst;
        }

        public static List<Combobox> ListarTipoBancos(int pclid)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Banco_Mutual");
                sp.AgregarParametro("pclid", pclid);

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
            }
            return lst;
        }

        public static List<Combobox> ListarEjecutivosMutual(int pclid)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Ejecutivos_Mutual");
                sp.AgregarParametro("pclid", pclid);

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
            }
            return lst;
        }

        public static int InsertarCuentaEjecutivo(string cuenta, int idEjecutivo, int idBanco)
        {            
            int id = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Cuenta_Ejecutivo");
                sp.AgregarParametro("idEjecutivo", idEjecutivo);
                sp.AgregarParametro("idBanco", idBanco);
                sp.AgregarParametro("cuenta", string.IsNullOrEmpty(cuenta) ? DBNull.Value : (object)cuenta);
                id = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarCuentaEjecutivo: " + idEjecutivo, idBanco);
                return -1;
            }
            return id;
        }

        public static int InsertarEjecutivoMutual(int pclid, string ejecutivo, string email, string oficina, int idEjecutivo)
        {
            int id = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Ejecutivo_Mutual");
                sp.AgregarParametro("cliente", pclid);
                sp.AgregarParametro("ejecutivo", string.IsNullOrEmpty(ejecutivo) ? DBNull.Value : (object)ejecutivo);
                sp.AgregarParametro("email", string.IsNullOrEmpty(email) ? DBNull.Value : (object)email);
                sp.AgregarParametro("oficina", string.IsNullOrEmpty(oficina) ? DBNull.Value : (object)oficina);
                sp.AgregarParametro("idejecutivo", idEjecutivo);
                id = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.InsertarEjecutivoMutual: " + idEjecutivo + "-" + pclid, idEjecutivo);
                return -1;
            }
            return id;
        }

        public static int EliminarCuentaEjecutivo(int cuenta)
        {
            int id = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Eliminar_Cuenta_Ejecutivo");
                sp.AgregarParametro("cuenta", cuenta);
                id = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EliminarCuentaEjecutivo: " + cuenta, cuenta);
                return -1;
            }
            return id;
        }

        public static int EliminarEjecutivoMutual(int idejecutivo)
        {
            int id = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Eliminar_Ejecutivo_Mutual");
                sp.AgregarParametro("idejecutivo", idejecutivo);
                id = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.EliminarEjecutivoMutual: " + idejecutivo, idejecutivo);
                return -1;
            }
            return id;
        }

        public static List<dto.EjecutivoMutual> ListarEjecutivoMutual(int ejecutivo, int cuenta, int pclid)
        {
            List<dto.EjecutivoMutual> lst = new List<dto.EjecutivoMutual>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Cuenta_O_Ejecutivo");
                sp.AgregarParametro("ejecutivo", ejecutivo);
                sp.AgregarParametro("cuenta", cuenta);
                sp.AgregarParametro("pclid", pclid);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.EjecutivoMutual()
                        {
                            IdTipoBanco = Int32.Parse(string.IsNullOrEmpty(ds.Tables[0].Rows[i]["ID_TIPO_BANCO"].ToString()) ? "0": ds.Tables[0].Rows[i]["ID_TIPO_BANCO"].ToString()),
                            NombreBanco = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            IdCuentaEjecutivo = Int32.Parse(string.IsNullOrEmpty(ds.Tables[0].Rows[i]["ID_CUENTA_EJECUTIVO"].ToString()) ? "0": ds.Tables[0].Rows[i]["ID_CUENTA_EJECUTIVO"].ToString()),
                            Cuenta = ds.Tables[0].Rows[i]["CUENTA"].ToString(),
                            Email = ds.Tables[0].Rows[i]["EMAIL"].ToString(),
                            Oficina = ds.Tables[0].Rows[i]["OFICINA"].ToString()
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
        #endregion

        #region "Subcartera"

        public static int BuscarSubcartera(string rut, int codemp)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Subcartera");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["sbcid"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static int GuardarSubcartera(int codemp, string rut,  string nombre, int idComuna, string direccion, int telefono)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_SubCarteras");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rut", rut.ToUpper());
                sp.AgregarParametro("nombre", nombre.ToUpper());
                sp.AgregarParametro("comid", idComuna);
                sp.AgregarParametro("direccion", direccion.ToUpper());
                sp.AgregarParametro("telefono", telefono);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Int32.Parse(ds.Tables[0].Rows[0]["sbcid"].ToString());
                    }
                    else
                    {
                        return -1;
                    }
                    
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        

        #endregion

        #region "Tercero"

        public static int BuscarTercero(int codemp, string rut)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tercero");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rutTercero", (object)rut ?? DBNull.Value);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["terceroid"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static int GuardarTercero(int codemp, string rut, string nombre)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Guardar_Tercero");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rutTercero", rut.ToUpper());
                sp.AgregarParametro("nombreTercero", nombre.ToUpper());
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return Int32.Parse(ds.Tables[0].Rows[0]["terceroid"].ToString());
                    } else {
                        return -1;
                    }
                } else {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static KeyValuePair<string, string> BuscarTerceroRutNombre(int codemp, int terceroId)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_TerceroRutNombre");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("terceroId", terceroId);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return new KeyValuePair<string, string>(ds.Tables[0].Rows[0]["nombreTercero"].ToString(), ds.Tables[0].Rows[0]["rutTercero"].ToString());
                }
                else
                {
                    return new KeyValuePair<string, string>(string.Empty,string.Empty);
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<string, string>(string.Empty, string.Empty);
            }

        }

        #endregion

        #region "Carga Masiva"

        public static int DeudorEnJudicial(int codemp, int pclid, int ctcid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Deudor_Judicial");

                sp.AgregarParametro("ccb_codemp", codemp);
                sp.AgregarParametro("ccb_pclid", pclid);
                sp.AgregarParametro("ccb_ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        return Int32.Parse(ds.Tables[0].Rows[0]["judicial"].ToString());
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int QuiebrarDeudor(int codemp, int ctcid, string quiebra)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Update_Deudores_Quiebra");
                sp.AgregarParametro("ctc_codemp", codemp);
                sp.AgregarParametro("ctc_ctcid", ctcid);
                sp.AgregarParametro("ctc_quiebra", quiebra);
                int error = sp.EjecutarProcedimientoTrans();

                return error;

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        #endregion

        #region "Documentos Deudor"

        public static List<Combobox> ListarTipoDocumentosDeudor(int codemp, int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipos_Documentos_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
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
                            Value = ds.Tables[0].Rows[i]["tdi_tddid"].ToString(),
                            Text = ds.Tables[0].Rows[i]["tdi_nombre"].ToString()
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

        public static List<DocumentoDeudor> ListarDocumentosDeudor(int codemp, int idioma, int ctcid, string pclid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<DocumentoDeudor> lst = new List<DocumentoDeudor>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Deudor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
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
                        lst.Add(new dto.DocumentoDeudor()
                        {
                            Ctcid = Int32.Parse( ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Pclid = ds.Tables[0].Rows[i]["Pclid"].ToString(),
                            Dcdid =  Int32.Parse(ds.Tables[0].Rows[i]["Dcdid"].ToString()),
                            NombreCliente = ds.Tables[0].Rows[i]["NombreCliente"].ToString(),
                            TipoDocumento = ds.Tables[0].Rows[i]["TipoDocumento"].ToString(),
                            Archivo = ds.Tables[0].Rows[i]["Archivo"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString()
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

        public static int ListarDocumentosDeudorCount(int codemp, int idioma, int ctcid, string pclid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Deudor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public static string TraeTipoTipoDocumento(int codemp, int tipo)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Tipo_Tipo_Documento");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo", tipo);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["Tipo"].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static int GuardarDocumentoDeudor(int codemp, int ctcid,int tipo, string nombre,  string pclid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudores_Documentos");
                //@codemp integer, @ctcid integer, @tddid integer, @nombre varchar(800), @pclid integer
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("tddid", tipo);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("pclid", (object)pclid ?? DBNull.Value);
                ds = sp.EjecutarProcedimiento();
                //int error = sp.EjecutarProcedimientoTrans();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["dcdid"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        #endregion

        #region "Agregar Gestiones"

        public static List<Combobox> ListarTelefonosContactos(int codemp, int ctcid)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Telefonos_Contactos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Text = ds.Tables[0].Rows[i]["Numero"].ToString()
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

        public static int EditarTelefonoPrioridad(int codemp, string ctcid, long numero, string estado, int prioridad)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Deudores_Telefonos_Prioridad");
                sp.AgregarParametro("ddt_codemp", codemp);
                sp.AgregarParametro("ddt_ctcid", ctcid);
                sp.AgregarParametro("ddt_numero", numero);
                sp.AgregarParametro("ddt_estado", estado);
                sp.AgregarParametro("ddt_prioridad", prioridad);
                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int InsertarContacto(dto.Telefono obj)
        {
            DataSet ds = new DataSet();
            int id = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudores_Contactos");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("ticid", obj.TipoContacto);
                sp.AgregarParametro("nombre", obj.NombreContacto);
                sp.AgregarParametro("comid", obj.Comuna == 0 ? DBNull.Value : (object)obj.Comuna);
                sp.AgregarParametro("direccion", string.IsNullOrEmpty(obj.Direccion) ? DBNull.Value : (object)obj.Direccion);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return id;
        }

        public static int EditarContacto(dto.Telefono obj)
        {
            int id = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Deudores_Contactos");
                sp.AgregarParametro("ddc_codemp", obj.Codemp);
                sp.AgregarParametro("ddc_ctcid", obj.Ctcid);
                sp.AgregarParametro("ddc_ddcid", obj.Ddcid);
                sp.AgregarParametro("ddc_ticid", obj.TipoContacto);
                sp.AgregarParametro("ddc_nombre", obj.NombreContacto);
                sp.AgregarParametro("ddc_comid", obj.Comuna == 0 ? DBNull.Value : (object)obj.Comuna);
                sp.AgregarParametro("ddc_direccion", string.IsNullOrEmpty(obj.Direccion) ? DBNull.Value : (object)obj.Direccion);
                sp.AgregarParametro("ddc_estdir", obj.EstadoDireccion);
                sp.AgregarParametro("ddc_estado", obj.IdEstado);

                id = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return id;
        }

        public static int InsertarContacto(dto.Email obj)
        {
            DataSet ds = new DataSet();
            int id = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudores_Contactos");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("ticid", obj.TipoContacto);
                sp.AgregarParametro("nombre", obj.NombreContacto);
                sp.AgregarParametro("comid", obj.Comuna ==0 ? DBNull.Value : (object)obj.Comuna);
                sp.AgregarParametro("direccion", string.IsNullOrEmpty(obj.Direccion) ? DBNull.Value : (object)obj.Direccion);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return id;
        }

        public static int EditarContacto(dto.Email obj)
        {
            int id = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Deudores_Contactos");
                sp.AgregarParametro("ddc_codemp", obj.Codemp);
                sp.AgregarParametro("ddc_ctcid", obj.Ctcid);
                sp.AgregarParametro("ddc_ddcid", obj.Ddcid);
                sp.AgregarParametro("ddc_ticid", obj.TipoContacto);
                sp.AgregarParametro("ddc_nombre", obj.NombreContacto);
                sp.AgregarParametro("ddc_comid", obj.Comuna == 0 ? DBNull.Value : (object)obj.Comuna);
                sp.AgregarParametro("ddc_direccion", string.IsNullOrEmpty(obj.Direccion) ? DBNull.Value : (object)obj.Direccion);
                sp.AgregarParametro("ddc_estdir", obj.EstadoDireccion);
                sp.AgregarParametro("ddc_estado", obj.IdEstado);

                id = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return id;
        }

        public static int InsertarTelefonoContacto(dto.Telefono objTelefono)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudores_Contactos_Telefonos");
                sp.AgregarParametro("dct_codemp", objTelefono.Codemp);
                sp.AgregarParametro("dct_ctcid", objTelefono.Ctcid);
                sp.AgregarParametro("dct_ddcid", objTelefono.Ddcid);
                sp.AgregarParametro("dct_numero", objTelefono.Numero);
                sp.AgregarParametro("dct_tipo", objTelefono.TipoTelefono);
                sp.AgregarParametro("dct_estado", objTelefono.IdEstado);
                return sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static int InsertarEmailContacto(dto.Email obj)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudores_Contactos_Mail");
                sp.AgregarParametro("dcm_codemp", obj.Codemp);
                sp.AgregarParametro("dcm_ctcid", obj.Ctcid);
                sp.AgregarParametro("dcm_ddcid", obj.Ddcid);
                sp.AgregarParametro("dcm_mail", obj.Mail);
                sp.AgregarParametro("dcm_masivo", obj.Masivo);
                sp.AgregarParametro("dcm_tipo", obj.TipoEmail);
                return sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        
        public static int InsertarEmailContactoProvider(dto.Email obj)
        {
            //Insert to new ContactsMail
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudores_Contactos_Mail_Prov");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("ddcid", obj.Ddcid);
                sp.AgregarParametro("mail", obj.Mail);
                sp.AgregarParametro("tipo", obj.TipoEmail);
                sp.AgregarParametro("masivo", obj.Masivo);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("userid", obj.UserId);
                sp.AgregarParametro("fecha_creacion", obj.FechaCreacion);
                return sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region "Mover Cartera"

        public static List<dto.DocumentoMover> ListarDocumentosMoverGrilla(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentoMover> lst = new List<dto.DocumentoMover>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Reversa_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
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
                        lst.Add(new dto.DocumentoMover()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            UltimoEstado = ds.Tables[0].Rows[i]["UltimoEstado"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString())
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosReversaGrilla", 0);
                return lst;
            }
        }

        public static int ListarDocumentosMoverGrillaCount(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Reversa_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosReversaGrillaCount", 0);
                return count;
            }
        }

        public static List<Autocomplete> ListarAutoEstadoCartera(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Auto_Estados_Cartera");
                sp.AgregarParametro("texto", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static int MoverCartera(int codemp, int pclid, int ctcid, int ccbid, string comentario, string estCpbt, int estid)
        {
            int salida =0;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Mover_Documento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("comentario", comentario);
                sp.AgregarParametro("estCpbt", estCpbt);
                sp.AgregarParametro("estid", estid);
                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return salida;
        }

        #endregion

        #region "Castigo y devolucion"

        public static List<dto.DocumentoCastDev> ListarDocumentosCastDevGrilla(int codemp, int pclid, int ctcid, string estcpbt, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentoCastDev> lst = new List<dto.DocumentoCastDev>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Castigo_Devolucion_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("estcpbt", estcpbt);
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
                        lst.Add(new dto.DocumentoCastDev()
                        {
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Asignado = decimal.Parse(ds.Tables[0].Rows[i]["Asignado"].ToString()),
                            UltimoEstado = ds.Tables[0].Rows[i]["UltimoEstado"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["EstadoCpbt"].ToString(),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            FechaAsignacion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaAsignacion"].ToString()),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Asegurado = ds.Tables[0].Rows[i]["Asegurado"].ToString(),
                            RolNumero = ds.Tables[0].Rows[i]["RolNumero"].ToString(),
                            RolId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["RolId"].ToString()) ? default(int) : Int32.Parse(ds.Tables[0].Rows[i]["RolId"].ToString())
                            
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosCastigoDevolucionGrilla", 0);
                return lst;
            }
        }

        public static int ListarDocumentosCastDevGrillaCount(int codemp, int pclid, int ctcid, string estcpbt, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Documentos_Castigo_Devolucion_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("estcpbt", estcpbt);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.ListarDocumentosCastigoDevolucionGrillaCount", 0);
                return count;
            }
        }
        #endregion

        public static void InsertarCategoriaDeudor(int codemp, int ctcid, string categoria, int usrid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Cartera_Categoria");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("categoria", categoria);
                sp.AgregarParametro("usrid", usrid);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<Combobox> ListarCpbt(int codEmp, int ctcId, string pclid, string estadoCPBT)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Documentos_cpbt_doc");
                sp.AgregarParametro("codemp", codEmp);
                sp.AgregarParametro("ctcid", ctcId);
                sp.AgregarParametro("pclid", Int32.Parse(pclid));
                sp.AgregarParametro("estcpbt", estadoCPBT);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][0].ToString(),
                            Value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Combobox> ListarTiposImagenesCpbt(int codEmp)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipos_Imagenes_Cpbtdoc");
                sp.AgregarParametro("codemp", codEmp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][0].ToString(),
                            Value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Combobox> ListarCuentaTipoBanco(int tipoBanco, int pclid)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Cuenta_Tipo_Banco");
                sp.AgregarParametro("tipobanco", tipoBanco);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i][0].ToString(),
                            Text = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }
        
        public static int NumCarteraClientesCpbtDocImagenes(int codemp, string pclid, int ctcid, int ccbid)
        {
            int num = 0;
            try
            {
                 
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("UltNum_Cartera_Clientes_Cpbt_Doc_Imagenes");
                sp.AgregarParametro("cdi_codemp", codemp);
                sp.AgregarParametro("cdi_pclid", Int32.Parse(pclid));
                sp.AgregarParametro("cdi_ctcid", ctcid);
                sp.AgregarParametro("cdi_ccbid", ccbid);
                ds = sp.EjecutarProcedimiento();
                num = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());

            }
            catch (Exception ex)
            {
                return -1;
            }

            return num;

        }

        public static int GrabarImagenesCpbt(int codemp, int pclid, int ctcid, int ccbid, int cdid, int tpcid, string rutaImagen)
        {
            int result = 0;
            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            System.Data.SqlClient.SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
            try
            {
               
                //---------------------Hago el cambio de gestor------------------
                StoredProcedure sp = new StoredProcedure("_Insertar_Cartera_Clientes_Cpbt_Doc_Imagenes");
                sp.AgregarParametro("cdi_codemp", codemp);
                sp.AgregarParametro("cdi_pclid", pclid);
                sp.AgregarParametro("cdi_ctcid", ctcid);
                sp.AgregarParametro("cdi_ccbid", ccbid);
                sp.AgregarParametro("cdi_cdid", cdid);
                sp.AgregarParametro("cdi_tpcid", tpcid);
                sp.AgregarParametro("cdi_ruta_archivo", rutaImagen);
              

                result = sp.EjecutarProcedimiento(conn, myTrans);

                myTrans.Commit();
                conn.SQLConn.Close();

                return result;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                conn.SQLConn.Close();
                return -1;
            }

        }

        public static int MarcarQuiebraDeudor(int codemp, int ctcid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Update_Deudor_Quiebra_Panel");
                sp.AgregarParametro("ctc_codemp", codemp);
                sp.AgregarParametro("ctc_ctcid", ctcid);
                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
    }
}
