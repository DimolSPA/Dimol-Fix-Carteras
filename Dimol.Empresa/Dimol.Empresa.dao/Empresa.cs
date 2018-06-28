using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.dao
{
    public class Empresa
    {

        public List<dto.Empresa> ListarEmpresa(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Empresa> lstEmpresa = new List<dto.Empresa>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Empresas");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
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
                        lstEmpresa.Add(new dto.Empresa()
                        {
                            CodEmp = Int16.Parse(ds.Tables[0].Rows[i]["CodEmp"].ToString()),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            RutRepresentanteLegal = ds.Tables[0].Rows[i]["RutRepresentanteLegal "].ToString(),
                            NombreRepresentanteLegal = ds.Tables[0].Rows[i]["NombreRepresentanteLegal "].ToString(),
                            Giro = ds.Tables[0].Rows[i]["Giro"].ToString(),
                            Logo = ds.Tables[0].Rows[i]["Logo"].ToString()
                        
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEmpresa;
        }

        public static dto.Empresa DatosEmpresa(int codemp)
        {
            dto.Empresa empresa = new  dto.Empresa();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Empresas");
                sp.AgregarParametro("codemp", codemp);
             
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    empresa.CodEmp = Int16.Parse(ds.Tables[0].Rows[0]["CodEmp"].ToString());
                    empresa.Rut = ds.Tables[0].Rows[0]["Rut"].ToString();
                    empresa.Nombre = ds.Tables[0].Rows[0]["Nombre"].ToString();
                    empresa.RutRepresentanteLegal = ds.Tables[0].Rows[0]["RutRepresentanteLegal"].ToString();
                    empresa.NombreRepresentanteLegal = ds.Tables[0].Rows[0]["NombreRepresentanteLegal"].ToString();
                    empresa.Giro = ds.Tables[0].Rows[0]["Giro"].ToString();
                    empresa.Logo = ds.Tables[0].Rows[0]["Logo"].ToString();

                }
            }
            catch (Exception ex)
            {
            }
            return empresa;
        }

        public static void EditarDatosEmpresa(dto.Empresa objAccion, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Empresa");
                sp.AgregarParametro("emp_codemp", codemp);
                sp.AgregarParametro("emp_rut", (object)objAccion.Rut.ToUpper());
                sp.AgregarParametro("emp_nombre", (object)objAccion.Nombre.ToUpper());
                sp.AgregarParametro("emp_rutrepleg", (object)objAccion.RutRepresentanteLegal );
                sp.AgregarParametro("emp_replegal", (object)objAccion.NombreRepresentanteLegal );
                sp.AgregarParametro("emp_giro", (object)objAccion.Giro ?? DBNull.Value);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
