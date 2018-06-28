using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dimol.Open.dao
{
    public class ConsultaPJ
    {
        public static int ActPass(string usrid, string passAct, string newPass)
        {
            int error = 0;
            Funciones func = new Funciones();

            if (ValidaUsrPass(func.Encripta(usrid), func.Encripta(passAct)) == "") { 

                try
                {
                    DataSet ds = new DataSet();
                    StoredProcedure sp = new StoredProcedure("_Update_Pass_PJ");
                    sp.AgregarParametro("user", func.Encripta(usrid));
                    sp.AgregarParametro("pass", func.Encripta(newPass));
                    error = sp.EjecutarProcedimientoTrans();

                    return error;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public static int GuardarUserPJ(int iduser, string nombre, string username, string pass, int activa, int pclid, string adm)
        {
            int error = 0;
            Funciones func = new Funciones();

                try
                {
                    DataSet ds = new DataSet();
                    StoredProcedure sp = new StoredProcedure("_Insert_User_PJ");
                    sp.AgregarParametro("iduser", iduser);
                    sp.AgregarParametro("nombre", nombre);
                    sp.AgregarParametro("username", func.Encripta(username));
                    sp.AgregarParametro("pass", func.Encripta(pass));
                    sp.AgregarParametro("activa", activa);
                    sp.AgregarParametro("pclid", pclid);
                    sp.AgregarParametro("adm", adm);
                    error = sp.EjecutarProcedimientoTrans();

                    return error;
                }
                catch (Exception ex)
                {
                    return -1;
                }
          
        }

        public static int InsertarRutaLogoPJ(int iduser, string pclid, string ruta)
        {
            int error = 0;
            Funciones func = new Funciones();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insert_Ruta_Logo_PJ");
                sp.AgregarParametro("id", iduser);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ruta", ruta);
                error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static List<dto.ConsultaPJ> ConsultarPorRut(string rut)
        {
            List<dto.ConsultaPJ> lst = new List<dto.ConsultaPJ>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Causa_Rut_PJ");
                sp.AgregarParametro("rut", rut);
                ds = sp.EjecutarProcedimiento();
                Random rnd = new Random();
                int day = rnd.Next(1, 29);
                int month = rnd.Next(1, 13);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.ConsultaPJ()
                        {
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["Anio"].ToString()),
                            Demandado = ds.Tables[0].Rows[i]["Demandado"].ToString(),
                            Demandante = ds.Tables[0].Rows[i]["Demandante"].ToString(),
                            FechaIngreso = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()) ? new DateTime(Int32.Parse(ds.Tables[0].Rows[i]["Anio"].ToString()), month, day) : DateTime.Parse(ds.Tables[0].Rows[i]["FechaIngreso"].ToString()),
                            Numero = Int32.Parse(ds.Tables[0].Rows[i]["Numero"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString().Trim(),
                            RutaDemanda = ds.Tables[0].Rows[i]["RutaDemanda"].ToString(),
                            Url = ds.Tables[0].Rows[i]["Url"].ToString(),
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

        public static List<dto.UserPJ> TraeUserPJ(int userid, string button)
        {
            List<dto.UserPJ> lst = new List<dto.UserPJ>();
            Funciones fnc = new Funciones();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_User_PJ");
                sp.AgregarParametro("id", userid);
                sp.AgregarParametro("tipo", button);
                ds = sp.EjecutarProcedimiento();
  

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.UserPJ()
                        {
                            Userid = Int32.Parse(ds.Tables[0].Rows[i]["USRID"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            Login = fnc.Desencripta(ds.Tables[0].Rows[i]["LOGIN"].ToString()),
                            Password = fnc.Desencripta(ds.Tables[0].Rows[i]["PASSWORD"].ToString()),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["RUTCLIENTE"].ToString(),
                            Activa = Int32.Parse(ds.Tables[0].Rows[i]["ACTIVA"].ToString()),
                            Admin = ds.Tables[0].Rows[i]["ADM"].ToString().Trim(),
                            Left = Int32.Parse(ds.Tables[0].Rows[i]["L"].ToString()),
                            Right = Int32.Parse(ds.Tables[0].Rows[i]["R"].ToString())
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

        public static List<dto.UserPJ> TraeRutaLogoEmpresaPJ(int userid, string button)
        {
            List<dto.UserPJ> lst = new List<dto.UserPJ>();
            Funciones fnc = new Funciones();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Ruta_Logo_Empresa_PJ");
                sp.AgregarParametro("id", userid);
                sp.AgregarParametro("tipo", button);
                ds = sp.EjecutarProcedimiento();


                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.UserPJ()
                        {
                            Userid = Int32.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),                    
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["PCLID"].ToString()),
                            RutCliente = ds.Tables[0].Rows[i]["PCL_NOMFANT"].ToString(),
                            Nombre = ConfigurationManager.AppSettings["UrlImagenesEmpresa"] + ds.Tables[0].Rows[i]["RUTA"].ToString(),                            
                            Left = Int32.Parse(ds.Tables[0].Rows[i]["L"].ToString()),
                            Right = Int32.Parse(ds.Tables[0].Rows[i]["R"].ToString())
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

        public static string ValidaUsuario(string usuario, string password, string red, string local)
        {
            string pass = "";
            string user = "";
            bool BolUsr = false;
            string Message = "";
            Funciones func = new Funciones();

            pass = func.Encripta(password);
            user = func.Encripta(usuario);
            //----------Reviso si esta en la base de Datos----------------

            BolUsr = Comprobar(user, pass, red, local);

            if (BolUsr == true)
            {
                Message = ValidaUsrPass(user, pass);               
            }
            else
            {
                Message = "Usuario no encontrado"; //func.TraeError("UsNotFound", idioma);
            }
            return Message;
        }        

        public static bool Comprobar(string usuario, string pass, string red, string local)
        {
            int Find = 0;
            Funciones func = new Funciones();
            string estado = null;
            string password = "";
            //--------Busco el Idioma default del sistema---------------
            //MsgErr = "";
            //idioma = Convert.ToInt32(func.Configuracion_Num(1).ToString()) ;
            
            //---------Busco si el Usuario esta bien Ingresado----
            DataSet dsFindUsu = new DataSet();
            StoredProcedure sp1 = new StoredProcedure("_Find_Usuario_PJ");
            sp1.AgregarParametro("usuario", usuario);
            sp1.AgregarParametro("red", red);
            sp1.AgregarParametro("local", local);
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
                StoredProcedure sp4 = new StoredProcedure("_Trae_Usuario_PJ");
                sp4.AgregarParametro("usuario", usuario);
                sp4.AgregarParametro("red", red);
                sp4.AgregarParametro("local", local);
                dsDatUsu = sp4.EjecutarProcedimiento();

                estado = dsDatUsu.Tables[0].Rows[0][7].ToString();
                password = dsDatUsu.Tables[0].Rows[0][3].ToString();
            }

            if (estado == "0")
            {
                return true;
            }

            else //if (estado == "1")
            {
                return false;
            }

            //return true;
            
        }

        public static string TraeLoginPJ(string usuario, string red, string local)
        {
            string user = "";
            Funciones func = new Funciones();
            user = func.Encripta(usuario);
           
            try
            {
                DataSet dsDatUsu = new DataSet();
                StoredProcedure sp4 = new StoredProcedure("_Trae_Usuario_PJ");
                sp4.AgregarParametro("usuario", user);
                sp4.AgregarParametro("red", red);
                sp4.AgregarParametro("local", local);
                dsDatUsu = sp4.EjecutarProcedimiento();

                return dsDatUsu.Tables[0].Rows[0][2].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string TraeRutaLogo(string usuario)
        {
            string user = "";
            Funciones func = new Funciones();
            user = func.Encripta(usuario);

            try
            {
                DataSet dsDatUsu = new DataSet();
                StoredProcedure sp4 = new StoredProcedure("_Trae_Ruta_Logo_PJ");
                sp4.AgregarParametro("usuario", user);
                dsDatUsu = sp4.EjecutarProcedimiento();

                return ConfigurationManager.AppSettings["UrlImagenesEmpresa"] + dsDatUsu.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                return ConfigurationManager.AppSettings["UrlImagenesEmpresa"] + "logo.png";
                //return "~/images/logo.png";
            }
        }

        public static string TraePrf(string usuario, string red, string local)
        {

            string user = "";
            Funciones func = new Funciones();
            user = func.Encripta(usuario);

            DataSet dsDatUsu = new DataSet();
            StoredProcedure sp4 = new StoredProcedure("_Trae_Usuario_PJ");
            sp4.AgregarParametro("usuario", user);
            sp4.AgregarParametro("red", red);
            sp4.AgregarParametro("local", local);
            dsDatUsu = sp4.EjecutarProcedimiento();

            return dsDatUsu.Tables[0].Rows[0][10].ToString();
        }

        public static void LogOffUsrByIp(string ip)
        {
            DataSet dsDatUsu = new DataSet();
            StoredProcedure sp = new StoredProcedure("_LogOff_Usr");
            sp.AgregarParametro("ip", ip);
            sp.EjecutarProcedimiento();
        }

        public static string ValidaUsrPass(string usuario, string password)
        {
            DataSet dsDatUsu = new DataSet();
            StoredProcedure sp4 = new StoredProcedure("_Valida_Usuario_PJ");
            sp4.AgregarParametro("usuario", usuario);
            sp4.AgregarParametro("password", password);
            dsDatUsu = sp4.EjecutarProcedimiento();

            if (Int32.Parse(dsDatUsu.Tables[0].Rows[0][0].ToString()) <= 0)
            {
                return "Usuario o Password Inválida";
            }
            else
            {
                return "";
            }

        }

        public static int ValidaEmpresaRutaPJ(string Pclid)
        {
            
            try { 
                DataSet dsDatUsu = new DataSet();
                StoredProcedure sp4 = new StoredProcedure("_Valida_Empresa_Ruta_PJ");
                sp4.AgregarParametro("pclid", Int32.Parse(Pclid));
                dsDatUsu = sp4.EjecutarProcedimiento();

                if (dsDatUsu.Tables.Count > 0)
                {
                    return Int32.Parse(dsDatUsu.Tables[0].Rows[0]["PCLID"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        /*  public static string Comprobar(string usuario, string password)
          {
              int Find = 0;
              string MsgErr = null;
              Funciones func = new Funciones();
              string estado = null;
              int LogTot = 0;
              int LogBad = 0;
              int usuid = 0;
              int codemp = 0;
              string pass = null;
              string user = func.Desencripta(usuario);

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



          }*/

    }
}
