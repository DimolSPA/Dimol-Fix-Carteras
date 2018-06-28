using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Dimol.Slider.Models
{
    public class ConexionElastic
    {
        
            Util func = new Util();

            public DataSet Registros(string query)
            {
            
                string constr = "Data Source=" + ConfigurationManager.AppSettings["ServidorMySql"] + ";port=" + ConfigurationManager.AppSettings["PuertoMySql"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["BaseDatosMySql"] + ";User Id=" + func.Desencripta(ConfigurationManager.AppSettings["UsuarioMySql"]) + ";password=" + func.Desencripta(ConfigurationManager.AppSettings["PasswordMySql"]);//ConfigurationManager.ConnectionStrings["Elastic"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataSet ds = new DataSet())
                            {
                                sda.Fill(ds);
                                //ViewBag.Total = ds.Tables[0].Rows.Count;
                                return ds;
                            }
                        }
                    }
                }
            }
           
            



    }
    
}