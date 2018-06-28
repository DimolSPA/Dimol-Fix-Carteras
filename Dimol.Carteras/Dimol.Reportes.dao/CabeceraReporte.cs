using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Reportes.dto;
using System.Data;
using Dimol.dao;
using Dimol.ConvertidorImagenes;

namespace Dimol.Reportes.dao
{
    public class CabeceraReporte
    {
        public static void TraeEmpresa(dto.CabeceraReporte obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Empresa_Datos_Todos");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("codsuc", obj.Codsuc);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.Rut = Dimol.bcp.Funciones.formatearRut( ds.Tables[0].Rows[i]["emp_rut"].ToString());
                        obj.Nombre = ds.Tables[0].Rows[i]["emp_nombre"].ToString();
                        obj.RutRepresentanteLegal = Dimol.bcp.Funciones.formatearRut( ds.Tables[0].Rows[i]["emp_rutrepleg"].ToString());
                        obj.NombreRepresentanteLegal = ds.Tables[0].Rows[i]["emp_replegal"].ToString();
                        obj.Giro = ds.Tables[0].Rows[i]["emp_giro"].ToString();
                        obj.LogoArray = (byte[])ds.Tables[0].Rows[i]["emp_logo"];
                        obj.Logo = Convertidor.byteArrayToImage((byte[])ds.Tables[0].Rows[i]["emp_logo"]);
                        obj.Sucursal = ds.Tables[0].Rows[i]["esu_nombre"].ToString();
                        obj.Pais = ds.Tables[0].Rows[i]["pai_nombre"].ToString();
                        obj.Region = ds.Tables[0].Rows[i]["reg_nombre"].ToString();
                        obj.Ciudad = ds.Tables[0].Rows[i]["ciu_nombre"].ToString();
                        obj.Comuna = ds.Tables[0].Rows[i]["com_nombre"].ToString();
                        obj.CodigoPais = Int32.Parse(ds.Tables[0].Rows[i]["pai_codtel"].ToString());
                        obj.CodigoArea = Int32.Parse(ds.Tables[0].Rows[i]["ciu_codarea"].ToString());
                        obj.CodigoPostal = Int32.Parse(ds.Tables[0].Rows[i]["com_codpost"].ToString());
                        obj.Direccion = ds.Tables[0].Rows[i]["esu_direccion"].ToString();
                        obj.Telefono = Int32.Parse(ds.Tables[0].Rows[i]["esu_telefono"].ToString());
                        obj.Fax = Int32.Parse(ds.Tables[0].Rows[i]["esu_fax"].ToString());
                        obj.Email = ds.Tables[0].Rows[i]["esu_mail"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
