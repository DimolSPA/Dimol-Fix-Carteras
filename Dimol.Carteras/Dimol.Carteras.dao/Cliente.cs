using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dimol.Carteras.dao
{
    public class Cliente
    {
        public static List<Autocomplete> ListarRutCliente(string numero)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Cliente");
                sp.AgregarParametro("rut", numero);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Autocomplete> ListarNombreCliente(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Nombre_Cliente");
                sp.AgregarParametro("nombre", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Autocomplete> ListarRutNombreCliente(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Nombre_Cliente");
                sp.AgregarParametro("texto", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Autocomplete> ListarRutNombreClienteFiltradoEsPrevisional(string nombre, bool esPrevisional)
        {
            List<Autocomplete> lst = new List<Autocomplete>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Nombre_Cliente_FiltradoEsPrevisional");
                sp.AgregarParametro("texto", nombre);

                if (esPrevisional)
                {
                    sp.AgregarParametro("esPrevisional", esPrevisional);
                }

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return lst;
        }

        public static List<Autocomplete> BuscarRutNombreClienteTipoCliente(string nombre, int codemp)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Nombre_Tipo_Cliente");
                sp.AgregarParametro("texto", nombre);
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static string BuscarNombreSucursalCliente(int codemp, int pclid, int pcsid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Nombre_Sucursal");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("pcsid", pcsid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string BuscarNombreProductoCliente(int codemp, int pclid, int pdtid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Nombre_Producto");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("pdtid", pdtid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                } else {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static bool VerificarEsClientePrevisional(int codemp, int IdCliente, string RutCliente)
        {
            try
            {
                #region VALIDACION DE PARAMETROS
                //Tiene que hacerse la búsqueda por lo menos por alguno de estos parametros
                if (IdCliente == 0 && RutCliente == "")
                {
                    throw new ArgumentNullException();
                }
                #endregion

                #region LLAMADA AL SP
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_ProvCliPorIdOrRut");
                sp.AgregarParametro("codemp", codemp);

                if (IdCliente > 0)
                {
                    sp.AgregarParametro("pclid", IdCliente);
                }

                if (RutCliente != "")
                {
                    sp.AgregarParametro("pclrut", RutCliente);
                }
                
                ds = sp.EjecutarProcedimiento();
                #endregion

                #region CARGA DE OBJETO CLIENTE
                string TipoCliente = "";
                if (ds.Tables.Count > 0)
                {
                    TipoCliente = ds.Tables[0].Rows[0]["PCL_TIPCLI"].ToString();
                }
                #endregion

                if (TipoCliente == "P")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}