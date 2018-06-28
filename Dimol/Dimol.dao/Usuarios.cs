using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using Dimol.dto;
using CYPH;


namespace Dimol.dao
{
    public class Usuarios
    {
        #region "Variables"
        public int Idioma { get; set; }
        #endregion

        public Usuarios(int idioma)
        {
            this.Idioma = idioma;
        }

        public string Encripta(string password)
        {
            string encrip = "";

            Ucode objUcode = new Ucode();

            encrip = objUcode.Encripta(password);

            return encrip;

        }

        public string Desencripta(string psw_encriptada)
        {
            string result = "";

            Ucode objUcode = new Ucode();

            result = objUcode.Desencripta(psw_encriptada);

            return result;

        }

        public string ValidaUsuario(string usuario, string password)
        {
            string pass = "";
            string user = "";
            bool BolUsr = false;
            string Message = "";
            Funciones func = new Funciones();
            int idioma = 0;

            pass = Encripta(password);
            user = Encripta(usuario);
            idioma = Convert.ToInt32(func.ConfiguracionNum(1));
            this.Idioma = idioma;
            //----------Reviso si esta en la base de Datos----------------

            BolUsr = Comprobar(user);

            if (BolUsr == true)
            {
                Message = Comprobar(user, pass);
            }
            else
            {
                Message = func.TraeError("UsNotFound", idioma);
            }
            return Message;
        }

        public bool Comprobar(string usuario)
        {
            int Find = 0;
            Funciones func = new Funciones();
            int idioma = this.Idioma;
            string estado = null;

            //--------Busco el Idioma default del sistema---------------
            //MsgErr = "";
            //idioma = Convert.ToInt32(func.Configuracion_Num(1).ToString()) ;


            //---------Busco si el Usuario esta bien Ingresado----
            DataSet dsFindUsu = new DataSet();
            StoredProcedure sp1 = new StoredProcedure("Find_Usuario_Empresa");
            sp1.AgregarParametro("usuario", usuario);
            dsFindUsu = sp1.EjecutarProcedimiento();

            Find = Int32.Parse(dsFindUsu.Tables[0].Rows[0][0].ToString());

            if (Find == 0)
            {
                return false;
            }
            else
            {
                //----------Busco los datos del Usuario-------
                DataSet dsDatUsu = new DataSet();
                StoredProcedure sp4 = new StoredProcedure("Trae_Usuario_Usu");
                sp4.AgregarParametro("usuario", usuario);
                dsDatUsu = sp4.EjecutarProcedimiento();

                estado = dsDatUsu.Tables[0].Rows[0][15].ToString();
            }

            if (estado == "H")
            {
                return true;
            }

            if (estado == "B")
            {
                return false;
            }

            return true;



        }

        public string Comprobar(string usuario, string password)
        {
            int Find = 0;
            string MsgErr = null;
            Funciones func = new Funciones();
            int idioma = this.Idioma;
            string estado = null;
            int LogTot = 0;
            int LogBad = 0;
            int usuid = 0;
            int codemp = 0;
            string pass = null;
            string user = Desencripta(usuario);

            //--------Busco el Idioma default de la empresa---------------
            MsgErr = "";

            LogTot = Convert.ToInt32(func.ConfiguracionNum(2));



            //----------Busco los datos del Usuario-------
            DataSet dsDatUsu = new DataSet();
            StoredProcedure sp4 = new StoredProcedure("Trae_Usuario_Usu");
            sp4.AgregarParametro("usuario", usuario);
            dsDatUsu = sp4.EjecutarProcedimiento();

            LogBad = Int16.Parse(dsDatUsu.Tables[0].Rows[0][6].ToString());
            estado = dsDatUsu.Tables[0].Rows[0][15].ToString();
            usuid = Int16.Parse(dsDatUsu.Tables[0].Rows[0][2].ToString());
            codemp = Int16.Parse(dsDatUsu.Tables[0].Rows[0][0].ToString());
            pass = Desencripta(dsDatUsu.Tables[0].Rows[0][3].ToString());

            //------------Reviso si la empresa, esta con el codigo OK-----------------
            //If CodBarra(codemp) = False Then
            //    MsgErr = func.TraeError("ErrEmpCod", idioma)
            //    Return MsgErr
            //End If



            //---Reviso si el usuario tiene asignada alguna sucursal-----------
            DataSet dsUsuSuc = new DataSet();
            StoredProcedure sp7 = new StoredProcedure("Trae_Usuario_Sucursal");
            sp7.AgregarParametro("codemp", codemp);
            sp7.AgregarParametro("usuario", usuid);
            dsUsuSuc = sp7.EjecutarProcedimiento();

            if (dsUsuSuc.Tables[0].Rows.Count == 0)
            {
                MsgErr = func.TraeError("UsuNotSuc", idioma);
                return MsgErr;
            }

            //------------Reviso si el Usuario tiene asignado un empleado--------------
            DataSet dsFindUsuEmp = new DataSet();
            StoredProcedure sp8 = new StoredProcedure("Trae_Usuario_Empleado");
            sp8.AgregarParametro("codemp", codemp);
            sp8.AgregarParametro("usuario", usuid);
            dsFindUsuEmp = sp8.EjecutarProcedimiento();

            Find = dsFindUsuEmp.Tables[0].Rows.Count;

            if (Find > 1)
            {
                MsgErr = func.TraeError("UsuAsigTwoOrMoreTime", idioma);
                return MsgErr;
            }
            else
            {
                if (Find == 1)
                {
                    if (dsFindUsuEmp.Tables[0].Rows[0][15].ToString() == "D")
                    {
                        MsgErr = func.TraeError("UsuEmpBloq", idioma);
                        return MsgErr;
                    }
                }
            }



            //-------------------Reviso si el Usuario tiene asignado algun cliente-----
            DataSet dsFindUsuCli = new DataSet();
            StoredProcedure sp19 = new StoredProcedure("Trae_Usuario_Provcli");
            sp19.AgregarParametro("codemp", codemp);
            sp19.AgregarParametro("usuario", usuid);
            dsFindUsuCli = sp19.EjecutarProcedimiento();

            Find = dsFindUsuCli.Tables[0].Rows.Count;

            if (Find > 1)
            {
                MsgErr = func.TraeError("UsuAsigTwoOrMoreTime", idioma);
                return MsgErr;
            }
            else
            {
                if (Find == 1)
                {

                    if (dsFindUsuCli.Tables[0].Rows[0][9].ToString() != "V")
                    {
                        MsgErr = func.TraeError("UsuProvCliBloq", idioma);
                        return MsgErr;

                    }
                }
            }




            if (estado == "H")
            {
                if (Encripta(pass) != password)
                {
                    MsgErr = func.TraeError("PaswNotFound", idioma);
                    LogTot = LogTot - LogBad;
                    MsgErr = MsgErr + ", " + func.TraeError("Intentos", idioma) + " : " + LogTot.ToString();

                    //-----------Hago el Update en los LogBad----
                    StoredProcedure sp5 = new StoredProcedure("Update_Usuarios_BadLog");
                    sp5.AgregarParametro("codemp", codemp);
                    sp5.AgregarParametro("usuario", usuid);
                    sp5.EjecutarProcedimiento(2);

                    if (LogTot == 0)
                    {
                        //-----------Hago el Bloqueo y coloco la fecha en que sucedio----
                        StoredProcedure sp6 = new StoredProcedure("Update_Usuarios_FecBlock");
                        sp6.AgregarParametro("codemp", codemp);
                        sp6.AgregarParametro("usuario", usuid);
                        sp6.EjecutarProcedimiento(2);

                        MsgErr = func.TraeError("UsuCtaBloq", idioma);



                    }
                    else if (LogTot < 0)
                    {
                        MsgErr = func.TraeError("UsuCtaBloq", idioma);

                    }

                }
                else
                {
                    //-----------Hago el Desbloqueo y coloco la fecha en que sucedio----
                    StoredProcedure sp6 = new StoredProcedure("Update_Usuarios_GodLog");
                    sp6.AgregarParametro("codemp", codemp);
                    sp6.AgregarParametro("usuario", usuid);
                    sp6.EjecutarProcedimiento(2);

                }

            }


            if (estado == "B")
            {
                MsgErr = func.TraeError("UsuCtaBloq", idioma);
                return MsgErr;
            }

            //-----------Hago el Update en los LogOk----
            StoredProcedure sp9 = new StoredProcedure("Update_Usuarios_FecLog");
            sp9.AgregarParametro("codemp", codemp);
            sp9.AgregarParametro("usuario", usuid);
            sp9.EjecutarProcedimiento(2);

            return MsgErr;



        }

        public DataSet Trae_DatUsuario(string usuario)
        {
            DataSet dsUsu = new DataSet();

            DataSet dsDatUsu = new DataSet();
            StoredProcedure sp4 = new StoredProcedure("_Trae_Usuarios_Empleado_Provcli");
            sp4.AgregarParametro("usuario", Encripta(usuario));
            dsUsu = sp4.EjecutarProcedimiento();

            return dsUsu;



        }


        public List<string> TraeClienteAsociadoUsuario(int codemp, int usrid)
        {
            try
            {
                DataSet ds = new DataSet();
                List<string> obj = new List<string>();
                StoredProcedure sp = new StoredProcedure("_Trae_Cliente_Consulta_Asociado");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("usrid", usrid);
                ds = sp.EjecutarProcedimiento();
                obj.Add(ds.Tables[0].Rows[0]["PCLID"].ToString());
                obj.Add( ds.Tables[0].Rows[0]["ESTADOS"].ToString());
                obj.Add(ds.Tables[0].Rows[0]["RUT"].ToString());
                obj.Add(ds.Tables[0].Rows[0]["NOMBRE"].ToString());
                return obj;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: Trae_Cliente_Consulta_Asociado" , usrid);
                throw ex;
            }
        }

        public string CambiaPassword(int codemp, int usuario, string password)
        {
            StoredProcedure sp2 = new StoredProcedure("Update_Usuarios_Cambia_Password");
            Funciones func = new Funciones();
            int err = 0;
            try
            {
                sp2.AgregarParametro("usr_codemp", codemp);
                sp2.AgregarParametro("usr_usrid", usuario);
                sp2.AgregarParametro("usr_password", Encripta(password));
                sp2.AgregarParametro("dias",  func.ConfiguracionEmpNum(codemp, 14));

                err = sp2.EjecutarProcedimiento(1);

                if (err < 0)
                {
                    return "Error al guardar nueva clave.";
                }
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: CambiaPassword", usuario);
                return ex.Message;
            }
            return "";
        }

        /*
              public string Administrador(int codemp, int perfil)
              {
                  DataSet dsPerfil = new DataSet();
                  string strSql = "";
                  Horus.DatSet Hds = new Horus.DatSet();

                  //----------Busco el perfil-------
                  strSql = "Select perfiles.prf_administrador";
                  strSql = strSql + " FROM perfiles";
                  strSql = strSql + "  WHERE ( perfiles.prf_codemp = " + codemp.ToString + " ) AND  ";
                  strSql = strSql + "  ( perfiles.prf_prfid = " + perfil.ToString + " )   ";
                  dsPerfil = Hds.ConsultaBD(strSql);

                  return dsPerfil.Tables(0).Rows(0)(0);



              }

              public string UsuarioNiv(int codemp, int usuario)
              {
                  DataSet dsPerfil = new DataSet();
                  string strSql = "";
                  Horus.DatSet Hds = new Horus.DatSet();
                  string Usr = "0;N;0";


                  try
                  {
                      //----------Busco el perfil-------
                      strSql = "  SELECT usuarios.usr_usrid,   ";
                      strSql = strSql + "  perfiles.prf_administrador, usr_permisos";
                      strSql = strSql + "  FROM usuarios,   ";
                      strSql = strSql + "      perfiles";
                      strSql = strSql + "  WHERE ( perfiles.prf_codemp = usuarios.usr_codemp ) and  ";
                      strSql = strSql + "   ( perfiles.prf_prfid = usuarios.usr_prfid ) and  ";
                      strSql = strSql + "   ( ( usuarios.usr_codemp = " + codemp.ToString + " ) AND  ";
                      strSql = strSql + "   ( usuarios.usr_usrid = " + usuario.ToString + " ) )  ";

                      dsPerfil = Hds.ConsultaBD(strSql);

                      if (dsPerfil.Tables(0).Rows.Count > 0)
                      {
                          Usr = dsPerfil.Tables(0).Rows(0)(0).ToString + ";" + dsPerfil.Tables(0).Rows(0)(1).ToString + ";" + dsPerfil.Tables(0).Rows(0)(2).ToString;

                      }


                  }
                  catch (Exception ex)
                  {
                  }

                  return Usr;

              }

              public string UsuarioSuc(int codemp, int usuario)
              {
                  DataSet dsPerfil = new DataSet();
                  string strSql = "";
                  Horus.DatSet Hds = new Horus.DatSet();
                  string ArrSuc = "";
                  int i = 0;



                  try
                  {
                      //----------Busco el perfil-------
                      strSql = " Select usuarios_sucursal.uss_sucid";
                      strSql = strSql + "  FROM usuarios_sucursal";
                      strSql = strSql + "  WHERE  usuarios_sucursal.uss_codemp = " + codemp.ToString;
                      strSql = strSql + "  and usuarios_sucursal.uss_usrid = " + usuario.ToString;

                      dsPerfil = Hds.ConsultaBD(strSql);

                      if (dsPerfil.Tables(0).Rows.Count > 0)
                      {
                          for (i = 0; i <= dsPerfil.Tables(0).Rows.Count - 1; i++)
                          {
                              ArrSuc = dsPerfil.Tables(0).Rows(i)(0).ToString + ",";
                          }
                      }

                      ArrSuc = Strings.Left(ArrSuc, Strings.Len(ArrSuc) - 1);



                  }
                  catch (Exception ex)
                  {
                  }

                  return ArrSuc;

              }

         

              private bool CodBarra(int empresa)
              {
                  string strSql = null;
                  string Codigo = "SoftLogic Permiso Rut:";
                  DataSet dsEmp = new DataSet();
                  Horus.DatSet Hds = new Horus.DatSet();
                  byte[] img1 = null;
                  byte[] img2 = null;
                  Horus.Funciones func = new Horus.Funciones();

                  try
                  {
                      strSql = "select emp_rut, emp_codbarr from empresa where emp_codemp=" + empresa.ToString;
                      dsEmp = Hds.ConsultaBD(strSql);

                      Codigo = Codigo + dsEmp.Tables(0).Rows(0)(0).ToString;

                      img1 = func.CodBarImagenBitEsp(Codigo, 4);

                      if (Information.IsDBNull(dsEmp.Tables(0).Rows(0)("emp_codbarr")) == true)
                      {
                          return false;
                      }

                      img2 = dsEmp.Tables(0).Rows(0)("emp_codbarr");

                      if (img1.Length != img2.Length)
                      {
                          return false;
                      }




                  }
                  catch (Exception ex)
                  {
                      return false;

                  }

                  return true;
              }
              */
    }
}


