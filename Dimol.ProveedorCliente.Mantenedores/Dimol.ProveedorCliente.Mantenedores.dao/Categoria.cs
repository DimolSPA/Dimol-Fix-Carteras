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
    public class Categoria
    {
        public List<dto.Categoria> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Categoria> lst = new List<dto.Categoria>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Categorias");
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
                        lst.Add(new dto.Categoria()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            Utilizacion = ds.Tables[1].Rows[i]["utilizacion"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public List<dto.Categoria> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Categoria> lst = new List<dto.Categoria>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Categorias");
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
                        lst.Add(new dto.Categoria()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["nombre"].ToString(),
                            Utilizacion = ds.Tables[0].Rows[i]["utilizacion"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public string ListarUtilizacion(int idioma)
        {
            string salida = ":" + "Seleccione";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 4; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "Uti" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    if (i == 1)
                    {
                        salida += i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public void Insertar(dto.Categoria objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Categoria");
                sp.AgregarParametro("cat_codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("cat_nombre", objAccion.Nombre);
                sp.AgregarParametro("cat_utilizacion", objAccion.Utilizacion);
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
                StoredProcedure sp = new StoredProcedure("Delete_Categorias");
                sp.AgregarParametro("cai_codemp", codemp);
                sp.AgregarParametro("cai_catid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.Categoria objAccion, int codemp, int idioma, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Categorias");
                sp.AgregarParametro("cat_codemp", codemp);
                sp.AgregarParametro("cat_catid", id);
                sp.AgregarParametro("cat_nombre", objAccion.Nombre);
                sp.AgregarParametro("cat_utilizacion", objAccion.Utilizacion);
                int error = sp.EjecutarProcedimientoTrans();

                StoredProcedure sp2 = new StoredProcedure("Update_Categorias_Idiomas");
                sp2.AgregarParametro("cai_codemp", codemp);
                sp2.AgregarParametro("cai_catid", id);
                sp2.AgregarParametro("cai_idiid", idioma);
                sp2.AgregarParametro("cai_nombre", objAccion.Nombre);
                
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
                StoredProcedure sp = new StoredProcedure("_Listar_Categorias_Grilla_Count");
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

    }
}
