using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dao
{
    public class Empleado
    {
        public static List<Combobox> ListarEstadosEmpleado(int codemp)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Estados_Empleados");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Combobox()
                {
                    Value = "",
                    Text = ""
                });

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Value = ds.Tables[0].Rows[i]["Id"].ToString(),
                            Text = ds.Tables[0].Rows[i]["Descripcion"].ToString()
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


        public List<dto.Empleado> ListarEmpleadoGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite,
                                                        string nombre, string paterno, string materno, string rut, string estado)
        {
            List<dto.Empleado> lstEmpleado = new List<dto.Empleado>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Busqueda_Empleado_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("paterno",(object)paterno ?? DBNull.Value);
                sp.AgregarParametro("materno", (object)materno ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("estado", (object)estado ?? DBNull.Value);
               
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstEmpleado.Add(new dto.Empleado()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString().ToUpper(),
                            ApellidoPaterno = ds.Tables[0].Rows[i]["ApellidoPaterno"].ToString().ToUpper(),
                            ApellidoMaterno = ds.Tables[0].Rows[i]["ApellidoMaterno"].ToString().ToUpper(),
                            Foto = ds.Tables[0].Rows[i]["Foto"].ToString(),
                            Estado = Int16.Parse(ds.Tables[0].Rows[i]["Estado"].ToString()),
                            DescripcionEstado = ds.Tables[0].Rows[i]["DescripcionEstado"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEmpleado;
        }

        public static int ListarEmpleadoGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite,
                                                        string nombre, string paterno, string materno, string rut, string estado)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Busqueda_Empleado_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("paterno", (object)paterno ?? DBNull.Value);
                sp.AgregarParametro("materno", (object)materno ?? DBNull.Value);
                sp.AgregarParametro("rut", (object)rut ?? DBNull.Value);
                sp.AgregarParametro("estado", (object)estado ?? DBNull.Value);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }


    }
}
