using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.dao
{
    public class Utilidades
    {
        #region " Propiedades "

        public int Empresa { get; set; }
        public int Sucursal { get; set; }
        public int Usuario { get; set; }
        public string IpRed { get; set; }
        public string IpMaquina { get; set; }
        public DatSet Hds = new DatSet();
        public Funciones func = new Funciones();
        public DataSet ds = new DataSet();
        

        #endregion

        public Utilidades(int emp, int suc, int usuario, string ipRed, string ipMaquina)
        {
            this.Empresa= emp;
            this.Sucursal= suc;
            this.Usuario =usuario;
            this.IpMaquina=ipMaquina;
            this.IpRed = ipRed;
        }

        public DateTime TraeFechaUltEmpresa(int codemp)
        {
            try
            {
                StoredProcedure spFechaUtl = new StoredProcedure("_Trae_Fecha_Utl_Empresa");
                spFechaUtl.AgregarParametro("codemp", Empresa);
                ds = spFechaUtl.EjecutarProcedimiento();
                return DateTime.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                return new DateTime();
            }
        }

        public int UpdateEmpresaFechaUtl(int codemp)
        {
            int err;
            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
            StoredProcedure sp = new StoredProcedure("Update_Empresa_FechaUtl");
            sp.AgregarParametro("emp_codemp", Empresa);
            err = sp.EjecutarProcedimiento(conn, myTrans);
            if (err < 0)
            {
                myTrans.Rollback();
                conn.SQLConn.Close();
            }
            myTrans.Commit();
            conn.SQLConn.Close();

            return err;
        }

        public bool Revisiones()
        {
            DateTime hoy;
            DateTime fechaServer;
            bool ok = true;
            int err=0;

            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();


            try
            {
                hoy = DateTime.Parse( func.FechaServer());
                fechaServer = TraeFechaUltEmpresa(Empresa);

                if (fechaServer == new DateTime())
                {
                    if (RevCompromisos())
                    {
                        if (CreaSupCamp())
                        {
                            err=UpdateEmpresaFechaUtl(Empresa);
                        }
                    }

                    if (RevVencidos())
                    {
                        err = -1;
                    }

                    RevisaAplicacionesEstados();
                    //RevisaFechasJudicialesRol();

                }
                else
                {
                    if (DateTime.Compare(DateTime.Parse( ds.Tables[0].Rows[0][0].ToString()), hoy) != 0)
                    {
                        if (RevCompromisos())
                        {
                            if (CreaSupCamp())
                            {
                                err=UpdateEmpresaFechaUtl(Empresa);
                            }
                        }

                        if (RevVencidos())
                        {
                            err = -1;
                        }

                        RevisaAplicacionesEstados();
                        //RevisaFechasJudicialesRol();
                    }
                }


                if (err < 0)
                {
                    myTrans.Rollback();
                    conn.SQLConn.Close();
                    return false;
                }
                myTrans.Commit();
                conn.SQLConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                conn.SQLConn.Close();
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: Revisiones", 0);
                return false;
            }
        }

        public DataSet TraeCasosCompromiso(int codemp, int estid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Trae_Casos_Compromiso");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
                ds = sp.EjecutarProcedimiento();
                return ds;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: TraeCasosCompromiso", 0);
                throw ex;
            }
        }

        public DataSet TraeCasosVencidos(int codemp, int estid, string pclid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Trae_Casos_Vencidos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TraeGestorCartera(int codemp, int sucid, int ctcid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Trae_Gestor_Cartera");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();
                return Int32.Parse( ds.Tables[0].Rows[0]["gsc_gesid"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TraeCantidadCompromisosMesDeudor(int codemp, int pclid, int ctcid, int ccbid, int estid, int gesid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Trae_Cantidad_Compromisos_Mes_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("gesid", gesid);
                ds = sp.EjecutarProcedimiento();
                return Int32.Parse( ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TraeCantidadCompromisosAnioDeudor(int codemp, int pclid, int ctcid, int ccbid, int estid, int gesid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Trae_Cantidad_Compromisos_Anio_Deudor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("gesid", gesid);
                ds = sp.EjecutarProcedimiento();
                return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TraeSupervisor(int codemp, int sucid, int gesid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Trae_Supervisor");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("gesid", gesid);
                ds = sp.EjecutarProcedimiento();
                return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int CambiaGestor(int gesid, int ctcid, int pclid)
        {
            int result=0;
            Conexion conn = new Conexion();
                conn.SQLConn.Open();
                SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
            try
            {
                
                //---------------------Hago el cambio de gestor------------------
                StoredProcedure sp = new StoredProcedure("Update_Gestor_Cartera");
                sp.AgregarParametro("gsc_codemp", Empresa);
                sp.AgregarParametro("gsc_sucid", Sucursal);
                sp.AgregarParametro("gsc_gesid", gesid);
                sp.AgregarParametro("gsc_ctcid", ctcid);
                sp.AgregarParametro("gsc_pclid", pclid);

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

        public DataSet TraeCpbtSinFechaPlazo(int codemp, int estid, int pclid, int ctcid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Trae_Cpbt_Sin_Fecha_Plazo");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet TraeCpbtFechaVencimiento(int codemp, int estid, int pclid, int ctcid, DateTime fechaVencimiento)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Trae_Cpbt_Fecha_Vencimiento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("fechaVencimiento", fechaVencimiento);
                ds = sp.EjecutarProcedimiento();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ActualizaCarteraEstados(int codemp, int pclid, int ctcid, int ccbid, int estid, string estcpbt)
        {
            int result = 0;
            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
            try
            {

                //---------------------Hago el cambio de gestor------------------
                StoredProcedure sp3 = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Estados");
                sp3.AgregarParametro("ccb_codemp", Empresa);
                sp3.AgregarParametro("ccb_pclid", pclid);
                sp3.AgregarParametro("ccb_ctcid", ctcid);
                sp3.AgregarParametro("ccb_ccbid", ccbid);
                sp3.AgregarParametro("ccb_estid", estid);
                sp3.AgregarParametro("ccb_estcpbt", estcpbt);

                result = sp3.EjecutarProcedimiento(conn, myTrans);

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

        public int InsertarHistorialCartera(int codemp,int suc, int pclid, int ctcid, int ccbid, int estid, string monto, string saldo, int idUsuario, string comentario)
        {
            int result = 0;
            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
            try
            {

                //---------------------Hago el cambio de gestor------------------
                StoredProcedure sp4 = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial");
                sp4.AgregarParametro("ceh_codemp", codemp);
                sp4.AgregarParametro("ceh_pclid", pclid);
                sp4.AgregarParametro("ceh_ctcid", ctcid);
                sp4.AgregarParametro("ceh_ccbid", ccbid);
                sp4.AgregarParametro("ceh_estid", estid);
                sp4.AgregarParametro("ceh_sucid", suc);
                sp4.AgregarParametro("ceh_gesid", DBNull.Value);
                sp4.AgregarParametro("ceh_ipred", IpRed);
                sp4.AgregarParametro("ceh_ipmaquina", IpMaquina);
                sp4.AgregarParametro("ceh_comentario", comentario);
                sp4.AgregarParametro("ceh_monto", monto.ToString().Replace(',','.'));
                sp4.AgregarParametro("ceh_saldo", saldo.ToString().Replace(',', '.'));
                sp4.AgregarParametro("ceh_usrid", idUsuario);

                result = sp4.EjecutarProcedimiento(conn, myTrans);

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

        public bool RevCompromisos()
        {
            int estid = func.ConfiguracionEmpNum(Empresa, 82);
            int i;
            DataSet dsEst = new DataSet();
            int CantCp = func.ConfiguracionEmpNum(Empresa, 84);
            DataSet dsGest = new DataSet();
            Conexion conn = new Conexion();
            int err=0;
            DataSet dsSup = new DataSet();
            DataSet dsCpbt = new DataSet();
            int d;
            int estRoto = func.ConfiguracionEmpNum(Empresa, 83);
            DateTime hoy;
            int pclid;
            int ctcid;
            int ccbid;
            int gesid;

            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
            
            try
            {
                hoy = DateTime.Parse(func.FechaServer());
                //------------------Busco los casos que estan con compromisos---------------------

                //StrSql = "SELECT DISTINCT cartera_clientes_cpbt_doc.ccb_pclid,   ";
                //StrSql = StrSql + " cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid";
                //StrSql = StrSql + " FROM cartera_clientes_cpbt_doc";
                //StrSql = StrSql + " WHERE  cartera_clientes_cpbt_doc.ccb_codemp = " + Empresa.ToString();
                //StrSql = StrSql + " and cartera_clientes_cpbt_doc.ccb_estid = " + estid.ToString();
                ////                StrSql = StrSql + " and cartera_clientes_cpbt_doc.ccb_fecplazo <= getdate() and ccb_tipcart =2"
                //StrSql = StrSql + " and cartera_clientes_cpbt_doc.ccb_fecplazo < getdate()";

                ds = TraeCasosCompromiso(Empresa, estid);

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pclid = Int32.Parse(ds.Tables[0].Rows[i]["ccb_pclid"].ToString());
                    ctcid = Int32.Parse(ds.Tables[0].Rows[i]["ccb_ctcid"].ToString());
                    ccbid = Int32.Parse(ds.Tables[0].Rows[i]["ccb_ccbid"].ToString());


                    //-------Se busca el Gestor---------------
                    //StrSql = "Select  gestor_cartera.gsc_gesid";
                    //StrSql = StrSql + " FROM gestor_cartera";
                    //StrSql = StrSql + " WHERE  gestor_cartera.gsc_codemp = " + Empresa.ToString();
                    //StrSql = StrSql + " and gestor_cartera.gsc_sucid = " + Sucursal.ToString();
                    //StrSql = StrSql + " and gestor_cartera.gsc_ctcid = " + ctcid.ToString();


                    //dsGest = Hds.ConsultaBD(conn, myTrans, StrSql);

                    try
                    {
                        gesid = TraeGestorCartera(Empresa, Sucursal, ctcid);
                    }
                    catch (Exception ex)
                    {
                        gesid = 0;
                    }

                    //------- Trae Cantidad Compromisos Pago Anio Deudor
                    //StrSql = "SELECT count(ceh_ctcid) as Tot  ";
                    //StrSql = StrSql + " FROM cartera_clientes_estados_historial";
                    //StrSql = StrSql + " WHERE  cartera_clientes_estados_historial.ceh_codemp = " + Empresa.ToString();
                    //StrSql = StrSql + " and cartera_clientes_estados_historial.ceh_pclid = " + pclid.ToString();
                    //StrSql = StrSql + " and cartera_clientes_estados_historial.ceh_ctcid = " + ctcid.ToString();
                    //StrSql = StrSql + " and cartera_clientes_estados_historial.ceh_ccbid = " + ccbid.ToString();
                    //StrSql = StrSql + " and cartera_clientes_estados_historial.ceh_estid = " + estid.ToString();
                    //StrSql = StrSql + " and cartera_clientes_estados_historial.ceh_gesid = " + gesid.ToString();
                    //StrSql = StrSql + " and datepart(year,cartera_clientes_estados_historial.ceh_fecha) = datepart(year, getdate())    ";
                    //// StrSql = StrSql + " DatePart(Month, cartera_clientes_estados_historial.ceh_fecha) = DatePart(Month, getdate())"

                    //dsEst = Hds.ConsultaBD(conn, myTrans, StrSql);

                    if (TraeCantidadCompromisosAnioDeudor(Empresa, pclid, ctcid, ccbid, estid, gesid) > CantCp)
                    {
                        //------------------Se reasigna el caso-------------------------
                        //StrSql = "Select  gestor_cartera.gsc_gesid";
                        //StrSql = StrSql + " FROM gestor_cartera";
                        //StrSql = StrSql + " WHERE  gestor_cartera.gsc_codemp = " + Empresa.ToString();
                        //StrSql = StrSql + " and gestor_cartera.gsc_sucid = " + Sucursal.ToString();
                        //StrSql = StrSql + " and gestor_cartera.gsc_ctcid = " + ctcid.ToString();

                        ////dsGest = Hds.ConsultaBD(conn, myTrans, StrSql);

                        //try
                        //{
                        //    gesid = Int32.Parse(dsGest.Tables[0].Rows[0]["gsc_gesid"].ToString());
                        //}
                        //catch (Exception ex)
                        //{
                        //    gesid = 0;
                        //}

                        //-----------------Busco el Supervisor------------------------
                        //StrSql = "SELECT DISTINCT gestor.ges_gesid  ";
                        //StrSql = StrSql + " FROM grupos_cobranza,   ";
                        //StrSql = StrSql + " gestor,   ";
                        //StrSql = StrSql + " grupo_cobranza_gestor";
                        //StrSql = StrSql + " WHERE  grupos_cobranza.grc_codemp = gestor.ges_codemp  and  ";
                        //StrSql = StrSql + " grupos_cobranza.grc_sucid = gestor.ges_sucid  and  ";
                        //StrSql = StrSql + " grupos_cobranza.grc_emplid = gestor.ges_emplid  and  ";
                        //StrSql = StrSql + " grupo_cobranza_gestor.gcg_codemp = grupos_cobranza.grc_codemp  and  ";
                        //StrSql = StrSql + " grupo_cobranza_gestor.gcg_sucid = grupos_cobranza.grc_sucid  and  ";
                        //StrSql = StrSql + " grupo_cobranza_gestor.gcg_grcid = grupos_cobranza.grc_grcid   ";
                        //StrSql = StrSql + " and grupos_cobranza.grc_codemp =" + Empresa.ToString();
                        //StrSql = StrSql + " and grupos_cobranza.grc_sucid = " + Sucursal.ToString();
                        //StrSql = StrSql + " and grupo_cobranza_gestor.gcg_gesid = " + gesid.ToString();

                        //dsSup = Hds.ConsultaBD(conn, myTrans, StrSql);
                        int gesidSupervisor = TraeSupervisor(Empresa,Sucursal,gesid);

                        if ( gesidSupervisor > 0)
                        {
                            //---------------------Hago el cambio de gestor------------------
                            //StoredProcedure sp = new StoredProcedure("Update_Gestor_Cartera");
                            //sp.AgregarParametro("gsc_codemp", Empresa);
                            //sp.AgregarParametro("gsc_sucid", Sucursal);
                            //sp.AgregarParametro("gsc_gesid", dsSup.Tables[0].Rows[0]["ges_gesid"]);
                            //sp.AgregarParametro("gsc_ctcid", ctcid);
                            //sp.AgregarParametro("gsc_pclid", pclid);

                            //err = sp.EjecutarProcedimiento(conn, myTrans);

                            err = CambiaGestor(gesidSupervisor, ctcid, pclid);

                            if (err < 0)
                            {
                                break;
                            }

                        }

                        //--------------------Cambio los estados-------------------
                        //StrSql = "SELECT cartera_clientes_cpbt_doc.ccb_pclid,   ";
                        //StrSql = StrSql + " cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid, ccb_estcpbt, ccb_fecplazo, ccb_monto, ccb_saldo";
                        //StrSql = StrSql + " FROM cartera_clientes_cpbt_doc";
                        //StrSql = StrSql + " WHERE  cartera_clientes_cpbt_doc.ccb_codemp = " + Empresa.ToString();
                        //StrSql = StrSql + " and cartera_clientes_cpbt_doc.ccb_estid = " + estid.ToString();
                        //StrSql = StrSql + " and ccb_pclid = " + pclid.ToString();
                        //StrSql = StrSql + " and ccb_ctcid = " + ctcid.ToString();
                        //StrSql = StrSql + " and ccb_fecplazo is not null";

                        dsCpbt = TraeCpbtSinFechaPlazo(Empresa, estid, pclid, ctcid);//Hds.ConsultaBD(conn, myTrans, StrSql);

                        for (d = 0; d < dsCpbt.Tables[0].Rows.Count; d++)
                        {
                            if (DateTime.Parse( dsCpbt.Tables[0].Rows[d]["ccb_fecplazo"].ToString())<hoy)
                            {
                                pclid = Int32.Parse( dsCpbt.Tables[0].Rows[d]["ccb_pclid"].ToString());
                                ctcid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_ctcid"].ToString());
                                ccbid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_ccbid"].ToString());

                                //StoredProcedure sp3 = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Estados");
                                //sp3.AgregarParametro("ccb_codemp", Empresa);
                                //sp3.AgregarParametro("ccb_pclid", pclid);
                                //sp3.AgregarParametro("ccb_ctcid", ctcid);
                                //sp3.AgregarParametro("ccb_ccbid", ccbid);
                                //sp3.AgregarParametro("ccb_estid", estRoto);
                                //sp3.AgregarParametro("ccb_estcpbt", dsCpbt.Tables[0].Rows[d]["ccb_estcpbt"]);

                                err = ActualizaCarteraEstados(Empresa, pclid, ctcid, ccbid, estRoto, dsCpbt.Tables[0].Rows[d]["ccb_estcpbt"].ToString());//sp3.EjecutarProcedimiento(conn, myTrans);

                                if (err < 0)
                                {
                                    break;
                                }


                                //---------------------Agrego el Historial--------------------------
                                //StoredProcedure sp4 = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial");
                                //sp4.AgregarParametro("ceh_codemp", Empresa);
                                //sp4.AgregarParametro("ceh_pclid", pclid);
                                //sp4.AgregarParametro("ceh_ctcid", ctcid);
                                //sp4.AgregarParametro("ceh_ccbid", ccbid);
                                //sp4.AgregarParametro("ceh_estid", estRoto);
                                //sp4.AgregarParametro("ceh_sucid", Sucursal);
                                //sp4.AgregarParametro("ceh_gesid", DBNull.Value);
                                //sp4.AgregarParametro("ceh_ipred", IpRed);
                                //sp4.AgregarParametro("ceh_ipmaquina", IpMaquina);
                                //sp4.AgregarParametro("ceh_comentario", "");
                                //sp4.AgregarParametro("ceh_monto", dsCpbt.Tables[0].Rows[d]["ccb_monto"]);
                                //sp4.AgregarParametro("ceh_saldo", dsCpbt.Tables[0].Rows[d]["ccb_saldo"]);
                                //sp4.AgregarParametro("ceh_usrid", 1);

                                err = InsertarHistorialCartera(Empresa, Sucursal, pclid, ctcid, ccbid, estRoto, dsCpbt.Tables[0].Rows[d]["ccb_monto"].ToString(), dsCpbt.Tables[0].Rows[d]["ccb_saldo"].ToString(), 1, "");//sp4.EjecutarProcedimiento(conn, myTrans);

                                if (err < 0)
                                {
                                    break;
                                }


                            }

                        }

                        if (err < 0)
                        {
                            break;
                        }


                    }
                    else
                    {

                        //--------------------Cambio los estados-------------------
                        //StrSql = "SELECT cartera_clientes_cpbt_doc.ccb_pclid,   ";
                        //StrSql = StrSql + " cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid, ccb_estcpbt, ccb_fecplazo, ccb_monto, ccb_saldo";
                        //StrSql = StrSql + " FROM cartera_clientes_cpbt_doc";
                        //StrSql = StrSql + " WHERE  cartera_clientes_cpbt_doc.ccb_codemp = " + Empresa.ToString();
                        //StrSql = StrSql + " and cartera_clientes_cpbt_doc.ccb_estid = " + estid.ToString();
                        //StrSql = StrSql + " and ccb_pclid = " + pclid.ToString();
                        //StrSql = StrSql + " and ccb_ctcid = " + ctcid.ToString();
                        //StrSql = StrSql + " and ccb_fecplazo is not null";

                        //dsCpbt = Hds.ConsultaBD(conn, myTrans, StrSql);
                        dsCpbt = TraeCpbtSinFechaPlazo(Empresa, estid, pclid, ctcid);

                        for (d = 0; d < dsCpbt.Tables[0].Rows.Count; d++)
                        {
                            if (DateTime.Parse(dsCpbt.Tables[0].Rows[d]["ccb_fecplazo"].ToString()) < hoy)
                            {
                                pclid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_pclid"].ToString());
                                ctcid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_ctcid"].ToString());
                                ccbid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_ccbid"].ToString());

                                //StoredProcedure sp3 = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Estados");
                                //sp3.AgregarParametro("ccb_codemp", Empresa);
                                //sp3.AgregarParametro("ccb_pclid", pclid);
                                //sp3.AgregarParametro("ccb_ctcid", ctcid);
                                //sp3.AgregarParametro("ccb_ccbid", ccbid);
                                //sp3.AgregarParametro("ccb_estid", estRoto);
                                //sp3.AgregarParametro("ccb_estcpbt", dsCpbt.Tables[0].Rows[d]["ccb_estcpbt"]);

                                err = err = ActualizaCarteraEstados(Empresa, pclid, ctcid, ccbid, estRoto, dsCpbt.Tables[0].Rows[d]["ccb_estcpbt"].ToString()); //sp3.EjecutarProcedimiento(conn, myTrans);

                                if (err < 0)
                                {
                                    break;
                                }


                                //---------------------Agrego el Historial--------------------------
                                //StoredProcedure sp4 = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial");
                                //sp4.AgregarParametro("ceh_codemp", Empresa);
                                //sp4.AgregarParametro("ceh_pclid", pclid);
                                //sp4.AgregarParametro("ceh_ctcid", ctcid);
                                //sp4.AgregarParametro("ceh_ccbid", ccbid);
                                //sp4.AgregarParametro("ceh_estid", estRoto);
                                //sp4.AgregarParametro("ceh_sucid", Sucursal);
                                //sp4.AgregarParametro("ceh_gesid", DBNull.Value);
                                //sp4.AgregarParametro("ceh_ipred", IpRed);
                                //sp4.AgregarParametro("ceh_ipmaquina", IpMaquina);
                                //sp4.AgregarParametro("ceh_comentario", "");
                                //sp4.AgregarParametro("ceh_monto", dsCpbt.Tables[0].Rows[d]["ccb_monto"]);
                                //sp4.AgregarParametro("ceh_saldo", dsCpbt.Tables[0].Rows[d]["ccb_saldo"]);
                                //sp4.AgregarParametro("ceh_usrid", 1);

                                err = InsertarHistorialCartera(Empresa, Sucursal, pclid, ctcid, ccbid, estRoto, dsCpbt.Tables[0].Rows[d]["ccb_monto"].ToString(), dsCpbt.Tables[0].Rows[d]["ccb_saldo"].ToString(),1,"");//sp4.EjecutarProcedimiento(conn, myTrans);

                                if (err < 0)
                                {
                                    break;
                                }


                            }

                        }

                        if (err < 0)
                        {
                            break;
                        }

                    }

                }

                if (err < 0)
                {
                    myTrans.Rollback();
                    conn.SQLConn.Close();
                    return false;
                }
                myTrans.Commit();
                conn.SQLConn.Close();


            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                conn.SQLConn.Close();

                return false;
            }

            return true;
            
        }

        public bool RevVencidos()
        {
            int estid = func.ConfiguracionEmpNum(Empresa, 100);
            int i;
            DataSet dsEst = new DataSet();
            int CantCp = func.ConfiguracionEmpNum(Empresa, 84);
            DataSet dsGest = new DataSet();
            Conexion conn = new Conexion();
            int err = 0;
            DataSet dsSup = new DataSet();
            DataSet dsCpbt = new DataSet();
            int d = 0;
            int estVenc = func.ConfiguracionEmpNum(Empresa, 102);
            DateTime hoy;
            int pclid;
            int ctcid;
            int ccbid;

            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();

            //hoy = Left(hoy.ToString(), 10) + " 00:00:00";

            try
            {
                hoy = DateTime.Parse(func.FechaServer());
                hoy = new DateTime(hoy.Year, hoy.Month, hoy.Day);
                //------------------Busco los casos que estan con compromisos---------------------

                //StrSql = "SELECT DISTINCT cartera_clientes_cpbt_doc.ccb_pclid,   ";
                //StrSql = StrSql + " cartera_clientes_cpbt_doc.ccb_ctcid";
                //StrSql = StrSql + " FROM cartera_clientes_cpbt_doc";
                //StrSql = StrSql + " WHERE  cartera_clientes_cpbt_doc.ccb_codemp = " + Empresa.ToString();
                //StrSql = StrSql + " and cartera_clientes_cpbt_doc.ccb_estid = " + estid.ToString();
                //StrSql = StrSql + " and ccb_pclid in (" + func.ConfiguracionEmpStr(Empresa, 103) + ")";
                //StrSql = StrSql + " and cartera_clientes_cpbt_doc.ccb_fecvenc < getdate()";

                //ds = Hds.ConsultaBD(conn, myTrans, StrSql);
                ds = TraeCasosVencidos(Empresa, estid, func.ConfiguracionEmpStr(Empresa, 103));

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pclid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_pclid"].ToString());
                    ctcid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_ctcid"].ToString());

                    //--------------------Cambio los estados-------------------
                    //StrSql = "SELECT cartera_clientes_cpbt_doc.ccb_pclid,   ";
                    //StrSql = StrSql + " cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid, ccb_estcpbt, ccb_fecplazo, ccb_monto, ccb_saldo, ccb_fecvenc";
                    //StrSql = StrSql + " FROM cartera_clientes_cpbt_doc";
                    //StrSql = StrSql + " WHERE  cartera_clientes_cpbt_doc.ccb_codemp = " + Empresa.ToString();
                    //StrSql = StrSql + " and cartera_clientes_cpbt_doc.ccb_estid = " + estid.ToString();
                    //StrSql = StrSql + " and ccb_pclid = " + pclid.ToString();
                    //StrSql = StrSql + " and ccb_ctcid = " + ctcid.ToString();
                    //StrSql = StrSql + " and ccb_fecvenc < '" + hoy.ToString() + "'";


                    dsCpbt = TraeCpbtFechaVencimiento(Empresa, estid, pclid, ctcid, hoy);
                    //dsCpbt = Hds.ConsultaBD(conn, myTrans, StrSql);

                    for (d = 0; d < dsCpbt.Tables[0].Rows.Count; d++)
                    {

                        pclid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_pclid"].ToString());
                        ctcid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_ctcid"].ToString());
                        ccbid = Int32.Parse(dsCpbt.Tables[0].Rows[d]["ccb_ccbid"].ToString());

                        //StoredProcedure sp3 = new StoredProcedure("Update_Cartera_Clientes_Cpbt_Doc_Estados");
                        //sp3.AgregarParametro("ccb_codemp", Empresa);
                        //sp3.AgregarParametro("ccb_pclid", pclid);
                        //sp3.AgregarParametro("ccb_ctcid", ctcid);
                        //sp3.AgregarParametro("ccb_ccbid", ccbid);
                        //sp3.AgregarParametro("ccb_estid", estVenc);
                        //sp3.AgregarParametro("ccb_estcpbt", dsCpbt.Tables[0].Rows[d]["ccb_estcpbt"]);

                        err = ActualizaCarteraEstados(Empresa, pclid, ctcid, ccbid, estVenc, dsCpbt.Tables[0].Rows[d]["ccb_estcpbt"].ToString());//sp3.EjecutarProcedimiento(conn, myTrans);

                        if (err < 0)
                        {
                            break;
                        }

                        //---------------------Agrego el Historial--------------------------
                        //StoredProcedure sp4 = new StoredProcedure("Insertar_Cartera_Clientes_Estados_Historial");
                        //sp4.AgregarParametro("ceh_codemp", Empresa);
                        //sp4.AgregarParametro("ceh_pclid", pclid);
                        //sp4.AgregarParametro("ceh_ctcid", ctcid);
                        //sp4.AgregarParametro("ceh_ccbid", ccbid);
                        //sp4.AgregarParametro("ceh_estid", estVenc);
                        //sp4.AgregarParametro("ceh_sucid", Sucursal);
                        //sp4.AgregarParametro("ceh_gesid", DBNull.Value);
                        //sp4.AgregarParametro("ceh_ipred", IpRed);
                        //sp4.AgregarParametro("ceh_ipmaquina", IpMaquina);
                        //sp4.AgregarParametro("ceh_comentario", "");
                        //sp4.AgregarParametro("ceh_monto", dsCpbt.Tables[0].Rows[d]["ccb_monto"]);
                        //sp4.AgregarParametro("ceh_saldo", dsCpbt.Tables[0].Rows[d]["ccb_saldo"]);
                        //sp4.AgregarParametro("ceh_usrid", 1);

                        err = InsertarHistorialCartera(Empresa, Sucursal, pclid, ctcid, ccbid, estVenc, dsCpbt.Tables[0].Rows[d]["ccb_monto"].ToString(), dsCpbt.Tables[0].Rows[d]["ccb_saldo"].ToString(), 1, "");//sp4.EjecutarProcedimiento(conn, myTrans);

                        if (err < 0)
                        {
                            break;
                        }

                    }

                }


                if (err < 0)
                {
                    myTrans.Rollback();
                    conn.SQLConn.Close();
                    return false;
                }
                myTrans.Commit();
                conn.SQLConn.Close();
                return true;

            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                conn.SQLConn.Close();

                return false;
            }



        }

        public int TraeUltNumCampanaCartera(int codemp)
        {
            try
            {
                DataSet dsnum = new DataSet();
                StoredProcedure sp7 = new StoredProcedure("Ultnum_Cartera_Clientes_Campana");
                sp7.AgregarParametro("ccc_codemp", codemp);
                dsnum = sp7.EjecutarProcedimiento();

                return Int32.Parse(dsnum.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int InsertarCarteraCampana(int codemp, int suc, int num, string nombre, DateTime fecha , int usuario)
        {
            int result = 0;
            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
            try
            {

                //---------------------Hago el cambio de gestor------------------
                StoredProcedure sp2 = new StoredProcedure("Insertar_Cartera_Clientes_Campana");
                sp2.AgregarParametro("ccc_codemp", codemp);
                sp2.AgregarParametro("ccc_sucid", suc);
                sp2.AgregarParametro("ccc_cccid", num);
                sp2.AgregarParametro("ccc_nombre", nombre);
                sp2.AgregarParametro("ccc_prioridad", 1);
                sp2.AgregarParametro("ccc_fecini", fecha);
                sp2.AgregarParametro("ccc_fecfin", fecha.AddHours(4));
                sp2.AgregarParametro("ccc_descripcion", nombre);
                sp2.AgregarParametro("ccc_usrid", usuario);
                sp2.AgregarParametro("ccc_gesid", DBNull.Value);
                sp2.AgregarParametro("ccc_supgest", "S");
                sp2.AgregarParametro("ccc_predictivo", "S");

                result = sp2.EjecutarProcedimiento(conn, myTrans);

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

        public DataSet TraeCpbtCampana(int codemp, string estid)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Trae_Cpbt_Campana");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("estid", estid);
                ds = sp.EjecutarProcedimiento();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExisteDocumentoCampana(int codemp, int sucid, int cccid, int pclid, int ctcid, int ccbid)
        {
            try
            {
                DataSet dsnum = new DataSet();
                StoredProcedure sp7 = new StoredProcedure("_Existe_Documento");
                sp7.AgregarParametro("codemp", codemp);
                sp7.AgregarParametro("sucid", sucid);
                sp7.AgregarParametro("cccid", cccid);
                sp7.AgregarParametro("pclid", pclid);
                sp7.AgregarParametro("ctcid", ctcid);
                sp7.AgregarParametro("ccbid", ccbid);
                dsnum = sp7.EjecutarProcedimiento();

                return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int InsertarCarteraCampanaCpbt(int codemp, int suc, int num, int pclid, int ctcid, int ccbid, int estid)
        {
            int result = 0;
            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
            try
            {
                StoredProcedure sp3 = new StoredProcedure("Insertar_Cartera_Clientes_Campana_CpbtDoc");
                sp3.AgregarParametro("ccd_codemp", codemp);
                sp3.AgregarParametro("ccd_sucid", suc);
                sp3.AgregarParametro("ccd_cccid", num);
                sp3.AgregarParametro("ccd_pclid", pclid);
                sp3.AgregarParametro("ccd_ctcid", ctcid);
                sp3.AgregarParametro("ccd_ccbid", ccbid);
                sp3.AgregarParametro("ccd_estid", estid);

                result = sp3.EjecutarProcedimiento(conn, myTrans);

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

        public bool CreaSupCamp()
        {
            int err;
            //DataSet ds2 = new DataSet();
            //StoredProcedure sp2 = new StoredProcedure("Insertar_Cartera_Clientes_Campana");
            StoredProcedure sp4 = new StoredProcedure("Update_Cartera_Clientes_Campana");
            
            int num;
            string nombre = func.ConfiguracionEmpStr(Empresa, 85);
            DateTime fec;
            int estid = func.ConfiguracionEmpNum(Empresa, 82);
            int estRoto = func.ConfiguracionEmpNum(Empresa, 83);
            DataSet dsCart = new DataSet();
            int i;
            string StaRev = func.ConfiguracionEmpStr(Empresa, 86);
            int pclid;
            int ctcid;
            int ccbid;
            int estidRow;

            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();

            try
            {
                fec = DateTime.Parse(func.FechaServer());
                
                num =TraeUltNumCampanaCartera(Empresa);

                //sp2.AgregarParametro("ccc_codemp", Empresa);
                //sp2.AgregarParametro("ccc_sucid", Sucursal);
                //sp2.AgregarParametro("ccc_cccid", num);
                //sp2.AgregarParametro("ccc_nombre", nombre);
                //sp2.AgregarParametro("ccc_prioridad", 1);
                //sp2.AgregarParametro("ccc_fecini", fec);
                //sp2.AgregarParametro("ccc_fecfin", DateAdd(DateInterval.Hour, 4, fec));
                //sp2.AgregarParametro("ccc_descripcion", nombre);
                //sp2.AgregarParametro("ccc_usrid", Usuario);
                //sp2.AgregarParametro("ccc_gesid", DBNull.Value);
                //sp2.AgregarParametro("ccc_supgest", "S");
                //sp2.AgregarParametro("ccc_predictivo", "S");

                err = InsertarCarteraCampana(Empresa, Sucursal, num, nombre, fec, Usuario);//sp2.EjecutarProcedimiento(conn, myTrans);

                if (err >= 0)
                {
                    //------------------Agrego el Detalle----
                    //strSql = "SELECT cartera_clientes_cpbt_doc.ccb_pclid,   ";
                    //strSql = strSql + " cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid, ccb_estcpbt, ccb_fecplazo, ccb_monto, ccb_saldo, ccb_estid";
                    //strSql = strSql + " FROM cartera_clientes_cpbt_doc";
                    //strSql = strSql + " WHERE  cartera_clientes_cpbt_doc.ccb_codemp = " + Empresa.ToString();
                    //strSql = strSql + " and cartera_clientes_cpbt_doc.ccb_estid in (" + StaRev + ")";
                    //strSql = strSql + " and ccb_fecplazo <= getdate() and cartera_clientes_cpbt_doc.ccb_estcpbt in ('V','J')  ";

                    //-------------Reviso si tiene Negociaciones-------------------
                    //strSql = strSql + " union ";
                    //strSql = strSql + " SELECT distinct cartera_clientes_cpbt_doc.ccb_pclid,   ";
                    //strSql = strSql + " cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid, ccb_estcpbt, ccb_fecplazo, ccb_monto, ccb_saldo, ccb_estid";
                    //strSql = strSql + "  FROM cartera_clientes_cpbt_doc,   ";
                    //strSql = strSql + " negociacion_cpbtdoc,   ";
                    //strSql = strSql + " negociacion,   ";
                    //strSql = strSql + " negociacion_pagos";
                    //strSql = strSql + " WHERE  negociacion_cpbtdoc.ngd_codemp = cartera_clientes_cpbt_doc.ccb_codemp  and  ";
                    //strSql = strSql + " negociacion_cpbtdoc.ngd_pclid = cartera_clientes_cpbt_doc.ccb_pclid  and  ";
                    //strSql = strSql + " negociacion_cpbtdoc.ngd_ctcid = cartera_clientes_cpbt_doc.ccb_ctcid  and  ";
                    //strSql = strSql + " negociacion_cpbtdoc.ngd_ccbid = cartera_clientes_cpbt_doc.ccb_ccbid  and  ";
                    //strSql = strSql + " cartera_clientes_cpbt_doc.ccb_estcpbt in ('V','J') and  ";
                    //strSql = strSql + " negociacion.neg_codemp = negociacion_cpbtdoc.ngd_codemp  and  ";
                    //strSql = strSql + " negociacion.neg_anio = negociacion_cpbtdoc.ngd_anio  and  ";
                    //strSql = strSql + " negociacion.neg_negid = negociacion_cpbtdoc.ngd_negid  and  ";
                    //strSql = strSql + " negociacion_pagos.ngp_codemp = negociacion.neg_codemp  and  ";
                    //strSql = strSql + "           negociacion_pagos.ngp_anio = negociacion.neg_anio  and  ";
                    //strSql = strSql + " negociacion_pagos.ngp_negid = negociacion.neg_negid  and  ";
                    //strSql = strSql + " cartera_clientes_cpbt_doc.ccb_codemp = " + Empresa.ToString();
                    ////strSql = strSql + " and cartera_clientes_cpbt_doc.ccb_estid = " + CInt(func.Configuracion_Emp_Num(codemp, 22)).ToString
                    //strSql = strSql + " and negociacion.neg_estado = 'A'";
                    //strSql = strSql + " and negociacion_pagos.ngp_fechas <= getdate()";
                    //strSql = strSql + " and convert(varchar,ngp_anio) + '_' + convert(varchar,ngp_negid) + '_' + CONVERT (char(10), ngp_fechas, 112)  in (";
                    //strSql = strSql + "  SELECT convert(varchar, ddi_anioneg) + '_' + convert(varchar, ddi_negid) + '_' + CONVERT (char(10), ddi_fecvenc, 112)  ";
                    //strSql = strSql + " FROM documentos_diarios,   ";
                    //strSql = strSql + " estados_documentos_diarios";
                    //strSql = strSql + " WHERE  estados_documentos_diarios.edc_codemp = documentos_diarios.ddi_codemp  and  ";
                    //strSql = strSql + " estados_documentos_diarios.edc_edcid = documentos_diarios.ddi_edcid  and  ";
                    //strSql = strSql + " documentos_diarios.ddi_codemp = " + Empresa.ToString();
                    //strSql = strSql + " and documentos_diarios.ddi_negid is not null  AND  ";
                    //strSql = strSql + " estados_documentos_diarios.edc_estado <> 4)";


                    //dsCart = Hds.ConsultaBD(strSql);
                    dsCart = TraeCpbtCampana(Empresa, StaRev);


                    for (i = 0; i < dsCart.Tables[0].Rows.Count; i++)
                    {
                        //-------------Busco si esta ya ingresado el documento-------------------------
                        //strSql = "select count(ccd_codemp) from cartera_clientes_campana_cpbtdoc";
                        //strSql = strSql + " where ccd_codemp =" + Empresa.ToString();
                        //strSql = strSql + " and ccd_sucid =" + Sucursal.ToString();
                        //strSql = strSql + " and ccd_cccid =" + num.ToString();
                        //strSql = strSql + " and ccd_pclid =" + dsCart.Tables[0].Rows[i]["ccb_pclid"].ToString();
                        //strSql = strSql + " and ccd_ctcid =" + dsCart.Tables[0].Rows[i]["ccb_ctcid"].ToString();
                        //strSql = strSql + " and ccd_ccbid =" + dsCart.Tables[0].Rows[i]["ccb_ccbid"].ToString();

                        //ds = Hds.ConsultaBD(conn, myTrans, strSql);

                        pclid = Int32.Parse(dsCart.Tables[0].Rows[i]["ccb_pclid"].ToString());
                        ctcid = Int32.Parse(dsCart.Tables[0].Rows[i]["ccb_ctcid"].ToString());
                        ccbid = Int32.Parse(dsCart.Tables[0].Rows[i]["ccb_ccbid"].ToString());
                        estidRow = Int32.Parse(dsCart.Tables[0].Rows[i]["ccb_estid"].ToString());

                        if (ExisteDocumentoCampana(Empresa, Sucursal, num, pclid, ctcid, ccbid) == 0)
                        {
                            //----------------------------Ingreso el documento-----------------------
                            //StoredProcedure sp3 = new StoredProcedure("Insertar_Cartera_Clientes_Campana_CpbtDoc");
                            //sp3.AgregarParametro("ccd_codemp", Empresa);
                            //sp3.AgregarParametro("ccd_sucid", Sucursal);
                            //sp3.AgregarParametro("ccd_cccid", num);
                            //sp3.AgregarParametro("ccd_pclid", dsCart.Tables[0].Rows[i]["ccb_pclid"]);
                            //sp3.AgregarParametro("ccd_ctcid", dsCart.Tables[0].Rows[i]["ccb_ctcid"]);
                            //sp3.AgregarParametro("ccd_ccbid", dsCart.Tables[0].Rows[i]["ccb_ccbid"]);
                            //sp3.AgregarParametro("ccd_estid", dsCart.Tables[0].Rows[i]["ccb_estid"]);

                            err = InsertarCarteraCampanaCpbt(Empresa, Sucursal, num, pclid, ctcid, ccbid, estidRow);//sp3.EjecutarProcedimiento(conn, myTrans);

                            if (err < 0)
                            {
                                break;
                            }

                        }

                    }


                }

                if (err < 0)
                {
                    myTrans.Rollback();
                    conn.SQLConn.Close();
                    return false;
                }
                myTrans.Commit();
                conn.SQLConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                myTrans.Commit();
                conn.SQLConn.Close();
                return false;

            }
        }

        public bool RevisaAplicacionesEstados()
        {
            int err=0;
            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();

            try
            {
                StoredProcedure sp2 = new StoredProcedure("Update_Revision_CasosRemear");
                err = sp2.EjecutarProcedimiento(conn, myTrans);

                if (err < 0)
                {
                    myTrans.Rollback();
                    conn.SQLConn.Close();
                    return false;
                }
                myTrans.Commit();
                conn.SQLConn.Close();


            }
            catch (Exception ex)
            {
                myTrans.Commit();
                conn.SQLConn.Close();
                return false;
            }

            return true;
        }

        public bool RevisaFechasJudicialesRol()
        {
            int err=0;


            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction myTrans = conn.SQLConn.BeginTransaction();

            try
            {

                StoredProcedure sp2 = new StoredProcedure("Update_Rol_Repara_Fechas");
                err = sp2.EjecutarProcedimiento(conn, myTrans);

                if (err < 0)
                {
                    myTrans.Rollback();
                    conn.SQLConn.Close();
                    return false;
                }
                myTrans.Commit();
                conn.SQLConn.Close();


            }
            catch (Exception ex)
            {
                myTrans.Commit();
                conn.SQLConn.Close();
                return false;
            }

            return true;
        }
    }
}
