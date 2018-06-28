using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Dimol.dao
{
    public class Conexion
    {
        #region "Variables"
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool ConSSPI { get; set; }
        public string Servidor { get; set; }
        public string BaseDatos { get; set; }
        public SqlConnection  SQLConn { get; set; }
        public bool Nativo { get; set; }
        public string Proveedor { get; set; }
        public string Date { get; set; }
        public string Datetime { get; set; }
        public string DisableBind { get; set; }
        public string CallEscape { get; set; }

        #endregion

        public Conexion()
        {
            try
            {
                Funciones func = new Funciones();
                //Esta seccion de variables deberia ser leida 
                //de un archivo de configuracion .Config
                this.Servidor =  ConfigurationManager.AppSettings["Servidor"];
                this.BaseDatos = ConfigurationManager.AppSettings["BaseDatos"];
                this.Usuario = func.Desencripta(ConfigurationManager.AppSettings["Usuario"]);
                this.Password = func.Desencripta(ConfigurationManager.AppSettings["Password"]);
                this.Proveedor = ConfigurationManager.AppSettings["Proveedor"];
                this.DisableBind = ConfigurationManager.AppSettings["DisableBind"];
                this.CallEscape = ConfigurationManager.AppSettings["CallEscape"];
                this.ConSSPI = Boolean.Parse(ConfigurationManager.AppSettings["SSPI"]);

                //System.Configuration.ConfigurationSettings.AppSettings("Servidor")

                //Me.BaseDatos = System.Configuration.ConfigurationSettings.AppSettings("BaseDatos")
                //Me.Usuario = System.Configuration.ConfigurationSettings.AppSettings("Usuario")
                //Me.Password = System.Configuration.ConfigurationSettings.AppSettings("Password")
                //Me.mProveedor = System.Configuration.ConfigurationSettings.AppSettings("Proveedor")

                SQLConn = new SqlConnection(this.StrConexion());

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.dao.Conexion", 0);
            }
        }

        private string StrConexion()
        {
            try
            {
                string strCon = "";
                strCon = "Server=" + Servidor + ";Database=" + BaseDatos;
                if (!ConSSPI)
                {
                    strCon = strCon + ";user=" + Usuario + ";password=" + Password;
                }
                else
                {
                    strCon = strCon + ";Integrated Security=true";
                }

                strCon = strCon + ";Connect Timeout=24000; pooling='true'; Max Pool Size=24000";
                //if (!Nativo)
                //{
                //    strCon = strCon + ";Provider=" + Proveedor + ";";
                //}

                return strCon;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.dao.StrConexion", 0);
                return "";
            }
        }
 



//        Public Sub New()
//            Dim func As New Horus.Usuarios
//            Try
//                'Esta seccion de variables deberia ser leida 
//                'de un archivo de configuracion .Config
//                Me.Servidor = System.Configuration.ConfigurationManager.AppSettings("Servidor")
//                Me.BaseDatos = System.Configuration.ConfigurationManager.AppSettings("BaseDatos")
//                Me.Usuario = func.Desencripta(System.Configuration.ConfigurationManager.AppSettings("Usuario"))
//                Me.Password = func.Desencripta(System.Configuration.ConfigurationManager.AppSettings("Password"))
//                Me.mProveedor = System.Configuration.ConfigurationManager.AppSettings("Proveedor")
//                Me.mDisableBind = System.Configuration.ConfigurationManager.AppSettings("DisableBind")
//                Me.mCallEscape = System.Configuration.ConfigurationManager.AppSettings("CallEscape")
//                Me.ConSSPI = System.Configuration.ConfigurationManager.AppSettings("SSPI")

//                'System.Configuration.ConfigurationSettings.AppSettings("Servidor")

//                'Me.BaseDatos = System.Configuration.ConfigurationSettings.AppSettings("BaseDatos")
//                'Me.Usuario = System.Configuration.ConfigurationSettings.AppSettings("Usuario")
//                'Me.Password = System.Configuration.ConfigurationSettings.AppSettings("Password")
//                'Me.mProveedor = System.Configuration.ConfigurationSettings.AppSettings("Proveedor")

//                SQLConn = New SqlConnection(Me.StrConexion)
//            Catch ex As Exception
//                'Throw ex
//            End Try
//        End Sub
    }
}
