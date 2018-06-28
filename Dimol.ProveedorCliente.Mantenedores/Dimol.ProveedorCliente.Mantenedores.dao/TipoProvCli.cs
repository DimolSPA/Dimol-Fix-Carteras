using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.ProveedorCliente.Mantenedores.dto;
using System.Data;
using Dimol.dao;
using System.Data.SqlClient;

namespace Dimol.ProveedorCliente.Mantenedores.dao
{
    public class TipoProvCli
    {
        public List<dto.TipoProvCli> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoProvCli> lst = new List<dto.TipoProvCli>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TipoProvCli_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        lst.Add(new dto.TipoProvCli()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            Agrupa = ds.Tables[1].Rows[i]["agrupa"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public List<dto.TipoProvCli> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoProvCli> lst = new List<dto.TipoProvCli>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TipoProvCli_Grilla");
                sp.AgregarParametro("codemp", codemp);
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
                        lst.Add(new dto.TipoProvCli()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            Agrupa = ds.Tables[1].Rows[i]["agrupa"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }



        public void Insertar(dto.TipoProvCli objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_TipoProvCli");
                sp.AgregarParametro("tpc_codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("tpc_nombre", objAccion.Nombre);
                sp.AgregarParametro("tpc_agrupa", objAccion.Agrupa);
                
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Borrar(int codemp, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Tipos_ProvCli");
                sp.AgregarParametro("tpc_codemp", codemp);
                sp.AgregarParametro("tpc_tpcid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.TipoProvCli objAccion, int codemp, int idioma, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Tipos_Provcli");
                sp.AgregarParametro("tpc_codemp", codemp);
                sp.AgregarParametro("tpc_tpcid", id);
                sp.AgregarParametro("tpc_nombre", objAccion.Nombre);
                sp.AgregarParametro("tpc_agrupa", objAccion.Agrupa);
                int error = sp.EjecutarProcedimientoTrans();

                StoredProcedure sp2 = new StoredProcedure("Update_Tipos_ProvCli_Idiomas");
                sp2.AgregarParametro("tpi_codemp", codemp);
                sp2.AgregarParametro("tpi_tpcid", id);
                sp2.AgregarParametro("tpi_idid", idioma);
                sp2.AgregarParametro("tpi_nombre", objAccion.Nombre);

                int error2 = sp2.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ListarCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TipoProvCli_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idid", idid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                //sp.AgregarParametro("inicio", inicio);
                //sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {

                    return Int32.Parse(ds.Tables[1].Rows[0]["count"].ToString());

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0; ;
            }
        }

        public string ListarTipos(int idid)
        {
            string salida = ":" + "Seleccione";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                sp.AgregarParametro("codigo", "Venta");
                sp.AgregarParametro("idioma", idid);
                ds = sp.EjecutarProcedimiento();
                salida += ";" + "V" + ":" + ds.Tables[0].Rows[0][0].ToString();

                StoredProcedure sp2 = new StoredProcedure("Trae_Etiquetas");
                sp2.AgregarParametro("codigo", "Compra");
                sp2.AgregarParametro("idioma", idid);
                ds = sp2.EjecutarProcedimiento();
                salida += ";" + "C" + ":" + ds.Tables[0].Rows[0][0].ToString();

                StoredProcedure sp3 = new StoredProcedure("Trae_Etiquetas");
                sp3.AgregarParametro("codigo", "Ambos");
                sp3.AgregarParametro("idioma", idid);
                ds = sp3.EjecutarProcedimiento();
                salida += ";" + "A" + ":" + ds.Tables[0].Rows[0][0].ToString();

                StoredProcedure sp4 = new StoredProcedure("Trae_Etiquetas");
                sp4.AgregarParametro("codigo", "Deudor");
                sp4.AgregarParametro("idioma", idid);
                ds = sp4.EjecutarProcedimiento();
                salida += ";" + "D" + ":" + ds.Tables[0].Rows[0][0].ToString();

                StoredProcedure sp5 = new StoredProcedure("Trae_Etiquetas");
                sp5.AgregarParametro("codigo", "Otro");
                sp5.AgregarParametro("idioma", idid);
                ds = sp5.EjecutarProcedimiento();
                salida += ";" + "O" + ":" + ds.Tables[0].Rows[0][0].ToString();

                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

    }
}
