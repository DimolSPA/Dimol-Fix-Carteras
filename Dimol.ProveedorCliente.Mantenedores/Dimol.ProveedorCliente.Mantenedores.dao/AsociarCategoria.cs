using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dimol.dao;
using System.Data.SqlClient;

namespace Dimol.ProveedorCliente.Mantenedores.dao
{
    public class AsociarCategoria
    {

        public List<dto.Categoria> Trae_Categorias_NotAsociada(dto.SuperCategoria objAccion, int codemp, int idioma)
        {
            List<dto.Categoria> lst = new List<dto.Categoria>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Categorias_NotAsociada");
                sp.AgregarParametro("scc_codemp", codemp);
                sp.AgregarParametro("scc_spcid", objAccion.Id);
                sp.AgregarParametro("cai_idiid", idioma);
                sp.AgregarParametro("cat_utilizacion", objAccion.Utilizacion);
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

        public List<dto.Categoria> Trae_Categorias_Asociada(dto.SuperCategoria objAccion, int codemp, int idioma)
        {
            List<dto.Categoria> lst = new List<dto.Categoria>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Categorias_Asociada");
                sp.AgregarParametro("scc_codemp", codemp);
                sp.AgregarParametro("scc_spcid", objAccion.Id);
                sp.AgregarParametro("cai_idiid", idioma);
                sp.AgregarParametro("cat_utilizacion", objAccion.Utilizacion);
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

        public string ListarSuperCategorias(int codemp)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Lista_SuperCategorias");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();
                //Debug.WriteLine("TAMAÑO DS" + ds.Tables.Count);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StoredProcedure sp2 = new StoredProcedure("_Trae_Nombre_SuperCategorias");

                    sp2.AgregarParametro("codemp", codemp);
                    sp2.AgregarParametro("id", ds.Tables[0].Rows[i][0].ToString());
                    ds2 = sp2.EjecutarProcedimiento();

                    salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds2.Tables[0].Rows[0][0].ToString();

                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }


        }

        public List<dto.SuperCategoria> ListarSuperCategorias2(int codemp)
        {
            List<dto.SuperCategoria> lst = new List<dto.SuperCategoria>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Lista_SuperCategorias2");
                //Debug.WriteLine("INICIA DESPLEGABLES TIPOS COMPROBANTE" + codemp + "-" + idid);
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idid", idid);

                ds = sp.EjecutarProcedimiento();
                //Debug.WriteLine("TAMAÑO DS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.SuperCategoria()
                        {

                            Id = Int16.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString()

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

    }
}
