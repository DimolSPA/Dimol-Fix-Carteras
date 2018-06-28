using CYPH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dimol.Slider.dao
{
    public class Util
    {
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

        public int ConfigParamNum(int paramid)
        {
            ConexionSgd cng = new ConexionSgd();
            DataSet ds = new DataSet();

            cng.Parametros.Add(new SqlParameter() { ParameterName = "@paramid", SqlDbType = SqlDbType.Int, Value = paramid });            
            ds = cng.Procedimiento("_Trae_Params");

            return (int)(decimal.Parse(ds.Tables[0].Rows[0][0].ToString()));
        }

        public string ConfigParamStr(int paramid)
        {
            ConexionSgd cng = new ConexionSgd();
            DataSet ds = new DataSet();

            cng.Parametros.Add(new SqlParameter() { ParameterName = "@paramid", SqlDbType = SqlDbType.Int, Value = paramid });
            ds = cng.Procedimiento("_Trae_Params");

            return ds.Tables[0].Rows[0][1].ToString();
        }
    }
}