using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dimol.Slider.Models
{
    public class ConexionSgd
    {
        Util func = new Util();
        public List<SqlParameter> Parametros = new List<SqlParameter>();

        public DataSet Registros(string query)
        {
            string constr = "Data Source=" + ConfigurationManager.AppSettings["Servidor"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["BaseDatos"] + ";User Id=" + func.Desencripta(ConfigurationManager.AppSettings["Usuario"]) + ";password=" + func.Desencripta(ConfigurationManager.AppSettings["Password"]); //ConfigurationManager.ConnectionStrings["Sgd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        //cmd.CommandTimeout = 900000;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
        }

        public DataSet Procedimiento(string sp)
        {
            string constr = "Data Source=" + ConfigurationManager.AppSettings["Servidor"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["BaseDatos"] + ";User Id=" + func.Desencripta(ConfigurationManager.AppSettings["Usuario"]) + ";password=" + func.Desencripta(ConfigurationManager.AppSettings["Password"]); //ConfigurationManager.ConnectionStrings["Sgd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(sp))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter item in Parametros)
                    {
                        cmd.Parameters.Add(item);
                    }
                    
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        //cmd.CommandTimeout = 900000;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
        }

        public int ProcedimientoTran(string sp)
        {
            string constr = "Data Source=" + ConfigurationManager.AppSettings["Servidor"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["BaseDatos"] + ";User Id=" + func.Desencripta(ConfigurationManager.AppSettings["Usuario"]) + ";password=" + func.Desencripta(ConfigurationManager.AppSettings["Password"]); //ConfigurationManager.ConnectionStrings["Sgd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                int error;
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                using (SqlCommand cmd = new SqlCommand(sp, con, tran))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter item in Parametros)
                    {
                        cmd.Parameters.Add(item);
                    }

                    error = cmd.ExecuteNonQuery();                                      

                }

                if (error >= 0)
                {
                    tran.Commit();
                    con.Close();
                }
                else
                {
                    tran.Rollback();
                    con.Close();
                }

                return error;
            }
        }

    }
}