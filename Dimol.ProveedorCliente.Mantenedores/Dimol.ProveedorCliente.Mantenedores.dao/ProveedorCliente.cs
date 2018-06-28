using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Dimol.ProveedorCliente.Mantenedores.dao
{
    public class ProveedorCliente
    {
        public static List<dto.ProveedorCliente> ListarProveedorClienteGrilla(int codemp, string tipo, string nombre, string apellidoPaterno, string apellidoMaterno, string rut, string nombreFantasia, string estado, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ProveedorCliente> lst = new List<dto.ProveedorCliente>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ProveedorCliente_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("nombre", nombre);
                sp.AgregarParametro("apellidoPaterno", apellidoPaterno);
                sp.AgregarParametro("apellidoMaterno", apellidoMaterno);
                sp.AgregarParametro("rut", rut);
                sp.AgregarParametro("nombreFantasia", nombreFantasia);
                sp.AgregarParametro("estado", estado);
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
                        lst.Add(new dto.ProveedorCliente()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Rut = ds.Tables[1].Rows[i]["rut"].ToString(),
                            TipoCliente = ds.Tables[1].Rows[i]["TIPOCLIENTE"].ToString(),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            NombreFantasia = ds.Tables[1].Rows[i]["nombreFantasia"].ToString(),
                            ApellidoPaterno = ds.Tables[1].Rows[i]["apellidoPaterno"].ToString(),
                            ApellidoMaterno = ds.Tables[1].Rows[i]["apellidoMaterno"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return lst;
        }

        public static int ListarProveedorClienteGrillaCount(int codemp, string tipo, string nombre, string apellidoPaterno, string apellidoMaterno, string rut, string nombreFantasia, string estado, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ProveedorCliente_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("nombre", nombre);
                sp.AgregarParametro("apellidoPaterno", apellidoPaterno);
                sp.AgregarParametro("apellidoMaterno", apellidoMaterno);
                sp.AgregarParametro("rut", rut);
                sp.AgregarParametro("nombreFantasia", nombreFantasia);
                sp.AgregarParametro("estado", estado);
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
        public static List<dto.ProveedorCliente> ListarReceptoresGrilla(int codemp, string tipo, string nombre, string apellidoPaterno, string apellidoMaterno, string rut, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.ProveedorCliente> lst = new List<dto.ProveedorCliente>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ProveedorClienteReceptor_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("nombre", nombre);
                sp.AgregarParametro("apellidoPaterno", apellidoPaterno);
                sp.AgregarParametro("apellidoMaterno", apellidoMaterno);
                sp.AgregarParametro("rut", rut);
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
                        lst.Add(new dto.ProveedorCliente()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Rut = ds.Tables[1].Rows[i]["rut"].ToString(),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            ApellidoPaterno = ds.Tables[1].Rows[i]["apellidoPaterno"].ToString(),
                            ApellidoMaterno = ds.Tables[1].Rows[i]["apellidoMaterno"].ToString(),
                            NombreFantasia = ds.Tables[1].Rows[i]["nombreFantasia"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }
        public static int ListarReceptoresGrillaCount(int codemp, string tipo, string nombre, string apellidoPaterno, string apellidoMaterno, string rut, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_ProveedorClienteReceptor_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("tipo", tipo);
                sp.AgregarParametro("nombre", nombre);
                sp.AgregarParametro("apellidoPaterno", apellidoPaterno);
                sp.AgregarParametro("apellidoMaterno", apellidoMaterno);
                sp.AgregarParametro("rut", rut);
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
        public static List<Combobox> ListarEstados(int idioma, string first)
        {
            //string salida = "";
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first))
            {
                lst.Add(new Combobox()
                {
                    Text = first,
                    Value = ""
                });
            }
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "EstPC" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    lst.Add(new Dimol.dto.Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }


        public static List<Combobox> ListarTiposProvCli(int codemp, int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Tipos_ProvCli");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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


        public static List<Combobox> ListarNacionalidad(int codemp, int idioma, string first)
        {
            //string salida = "";
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first))
            {
                lst.Add(new Combobox()
                {
                    Text = first,
                    Value = ""
                });
            }
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                    sp.AgregarParametro("codigo", "Nac");
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    lst.Add(new Dimol.dto.Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = "1"
                    });

                    sp.AgregarParametro("codigo", "Ext");
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    lst.Add(new Dimol.dto.Combobox()
                    {
                        Text = ds.Tables[0].Rows[1][0].ToString(),
                        Value = "2"
                    });

                
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<Combobox> ListarGiros(int codemp, int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();

            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Giros");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", DBNull.Value);
                sp.AgregarParametro("sidx", "nombre");
                sp.AgregarParametro("sord", "asc");
                sp.AgregarParametro("inicio", 0);
                sp.AgregarParametro("limite", 99999);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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

        public static List<Combobox> ListarEstado(int idioma, string first)
        {
            //string salida = "";
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first))
            {
                lst.Add(new Combobox()
                {
                    Text = first,
                    Value = ""
                });
            }
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "EstPC" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    lst.Add(new Dimol.dto.Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<Combobox> ListarTipoCartera(int idioma, string first)
        {
            //string salida = "";
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first))
            {
                lst.Add(new Combobox()
                {
                    Text = first,
                    Value = ""
                });
            }
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipCart" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    lst.Add(new Dimol.dto.Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }


        public static List<Combobox> ListarUsuarios(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Usuarios_Asignar");
                sp.AgregarParametro("usr_codemp", codemp);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    Debug.WriteLine("USUARIOS " + ds.Tables.Count);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["usr_nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["usr_usrid"].ToString()
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

        public static List<Combobox> ListarPais()
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Pais");
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][1].ToString(),
                            Value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Combobox> ListarRegion(int pais)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Region");
                sp.AgregarParametro("codpais", pais);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][1].ToString(),
                            Value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Combobox> ListarCiudad(int region)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Ciudad");
                sp.AgregarParametro("regid", region);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][1].ToString(),
                            Value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Combobox> ListarComuna(int ciudad)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Comuna");
                sp.AgregarParametro("ciuid", ciudad);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Combobox()
                        {
                            Text = ds.Tables[0].Rows[i][1].ToString(),
                            Value = ds.Tables[0].Rows[i][0].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<Combobox> ListarBancos(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Bancos");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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

        public static List<Combobox> ListarTiposCuentas(int idioma, string first)
        {
            //string salida = "";
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first))
            {
                lst.Add(new Combobox()
                {
                    Text = first,
                    Value = ""
                });
            }
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "TipCCte" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    lst.Add(new Dimol.dto.Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

                }
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public static List<Combobox> ListarImpuestosProvCli(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Impuestos_ProvCli");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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

        public static List<Combobox> ListarTiposContactoProvCli(int codemp, int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposContacto_ProvCli");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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

        public static List<Combobox> ListarFormasDePago(int codemp, int idioma, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Formas_De_Pago");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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

        public static List<Combobox> ListarContratosCartera(int codemp, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Contratos_Cartera");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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

        public static List<Combobox> ListarSucursalesProvCli(int codemp, int idsuc, string first)
        {
            List<Combobox> lst = new List<Combobox>();
            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Sucursales_ProvCli");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idsuc", idsuc);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
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

        public static int GrabarCliente(dto.ProveedorCliente obj, int codemp, int idioma, string tipoCliente)
        {
            int num = 0;
           
            try
            {
                //Se evalúa si existe
                DataSet dsExist = new DataSet();
                StoredProcedure spExist = new StoredProcedure("_Existe_ProvCli");
                spExist.AgregarParametro("pcl_rut", obj.Rut);
                dsExist = spExist.EjecutarProcedimiento();

                if (dsExist.Tables[0].Rows.Count > 0)
                    num = Int32.Parse(dsExist.Tables[0].Rows[0][0].ToString()); //Si devuelve el pclid existe
                else
                    num = 0;

                if (num == 0)//Si no existe se inserta
                {
                    DataSet dsnum = new DataSet();
                    StoredProcedure spUlt = new StoredProcedure("UltNum_ProvCli");
                    spUlt.AgregarParametro("pcl_codemp", codemp);
                    dsnum = spUlt.EjecutarProcedimiento();
                    num = Int32.Parse(dsnum.Tables[0].Rows[0][0].ToString());

                    StoredProcedure sp = new StoredProcedure("_Insertar_ProvCli");
                    sp.AgregarParametro("pcl_codemp", codemp);
                    sp.AgregarParametro("pcl_pclid", num);
                    sp.AgregarParametro("pcl_tpcid", obj.Tipo);
                    sp.AgregarParametro("pcl_rut", obj.Rut);
                    sp.AgregarParametro("pcl_nombre", obj.Nombre);
                    sp.AgregarParametro("pcl_apepat", obj.ApellidoPaterno);
                    sp.AgregarParametro("pcl_apemat", obj.ApellidoMaterno);
                    sp.AgregarParametro("pcl_nomfant", obj.NombreFantasia);
                    sp.AgregarParametro("pcl_girid", obj.Giro);

                    if (obj.Estados.Equals("1"))
                    {
                        sp.AgregarParametro("pcl_estado", "V");
                    }
                    else if (obj.Estados.Equals("2"))
                    {
                        sp.AgregarParametro("pcl_estado", "E");
                    }
                    else if (obj.Estados.Equals("3"))
                    {
                        sp.AgregarParametro("pcl_estado", "B");
                    }
                    else if (obj.Estados.Equals("4"))
                    {
                        sp.AgregarParametro("pcl_estado", "P");
                    }

                    sp.AgregarParametro("pcl_rutlegal", obj.RutRepLegal);
                    sp.AgregarParametro("pcl_replegal", obj.NombreRepLegal);

                    //Tipo de usuarios
                    if (obj.EsPrevisional)
                    {
                        sp.AgregarParametro("pcl_tipcli", "P");
                    }
                    else
                    {
                        switch (obj.Nacionalidad)
                        {
                            case "1":
                                sp.AgregarParametro("pcl_tipcli", "N");
                                break;
                            case "2":
                                sp.AgregarParametro("pcl_tipcli", "E");
                                break;
                        }
                    }

                    if (obj.Mostrar == true)
                    {
                        sp.AgregarParametro("pcl_web", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("pcl_web", "N");
                    }
                    sp.AgregarParametro("pcl_comentario", obj.Comentario);

                    if (tipoCliente.Equals("C"))
                    {
                        sp.AgregarParametro("pcl_tipcart", DBNull.Value);
                    }
                    else
                    {
                        sp.AgregarParametro("pcl_tipcart", obj.TipoCartera);
                    }

                    if (obj.Transportista == true)
                    {
                        sp.AgregarParametro("pcl_transportista", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("pcl_transportista", "N");
                    }
                    if (obj.Usuario != "0")
                    {
                        sp.AgregarParametro("pcl_usrid", obj.Usuario);
                    }
                    else
                    {
                        sp.AgregarParametro("pcl_usrid", DBNull.Value);
                    }
                    if (obj.Naviera == true)
                    {
                        sp.AgregarParametro("pcl_naviera", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("pcl_naviera", "N");
                    }
                    sp.AgregarParametro("pcl_codigo", obj.CodigoSAP);

                    if (tipoCliente.Equals("C") || tipoCliente.Equals("A"))
                    {
                        sp.AgregarParametro("pci_iptid", idioma);
                    }
                    else
                    {
                        sp.AgregarParametro("pci_iptid", idioma);
                    }

                    int error = sp.EjecutarProcedimientoTrans();
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            return num;
        }

        public static int InsertarEnteJudicial(int codemp, int idCliente)
        {
            int exist = 0;
            try
            {
                exist = ExisteEnteJudicial(idCliente);
                if (exist != -1)
                {
                    if (exist == 1)
                    {
                        int err = ActualizarEnteJudicial(idCliente);
                    }
                    else
                    {
                        StoredProcedure sp = new StoredProcedure("_Insertar_Entes_Judicial");
                        sp.AgregarParametro("etj_codemp", codemp);
                        sp.AgregarParametro("etj_pclid", idCliente);
                        sp.AgregarParametro("etj_emplid", DBNull.Value);
                        sp.AgregarParametro("etj_sindico", "N");
                        sp.AgregarParametro("etj_abogado", "N");
                        sp.AgregarParametro("etj_procurador", "N");
                        sp.AgregarParametro("etj_receptor", "S");

                        int error = sp.EjecutarProcedimientoTrans();
                    }
                }
  
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;

        }
        public static int ActualizarEnteJudicial(int idCliente)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Update_Entes_Judicial");
                sp.AgregarParametro("etj_pclid", idCliente);
                sp.AgregarParametro("etj_sindico", "N");
                sp.AgregarParametro("etj_abogado", "N");
                sp.AgregarParametro("etj_procurador", "N");
                sp.AgregarParametro("etj_receptor", "S");

                int error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;

        }
        public static int ExisteEnteJudicial(int idCliente)
        {
            int exist = 0;
            try
            {
               
                //Se evalúa si existe
                DataSet dsExist = new DataSet();
                StoredProcedure spExist = new StoredProcedure("_Existe_EnteJudicial");
                spExist.AgregarParametro("etj_pclid", idCliente);
                dsExist = spExist.EjecutarProcedimiento();
                if (dsExist.Tables[0].Rows.Count > 0)
                    exist = Int32.Parse(dsExist.Tables[0].Rows[0][0].ToString());//Si devuelve 1 existe
                else
                    exist = 0;

            }
            catch (Exception ex)
            {
                return -1;
            }

            return exist;

        }
        public static int GrabarClienteSucursal(dto.ProveedorCliente obj, int codemp, int idioma, int idCliente)
        {
            try
            {
                int exist = 0;
                //Se evalúa si existe
                DataSet dsExist = new DataSet();
                StoredProcedure spExist = new StoredProcedure("_Existe_ProvCli_Sucursal");
                spExist.AgregarParametro("pcs_pclid", idCliente);
                dsExist = spExist.EjecutarProcedimiento();
                if (dsExist.Tables[0].Rows.Count > 0)
                    exist = Int32.Parse(dsExist.Tables[0].Rows[0][0].ToString());//Si devuelve 1 existe

                else
                    exist = 0;
                if (exist != 1)
                {
                    StoredProcedure sp = new StoredProcedure("_Insertar_ProvCli_Sucursal");
                    /*Debug.WriteLine("DATOS A INSERTAR : " + "codemp:" + codemp + " tipoCliente:" + tipoCliente + " tipo:" + obj.Tipo
                        + " rut:" + obj.Rut + " Nombre:" + obj.Nombre + " ApellidoPat:" + obj.ApellidoPaterno + " ApellidoMat:" + obj.ApellidoMaterno
                        + " nombrefant:" + obj.NombreFantasia + " giro:" + obj.Giro + " ESTADO: " + obj.Estados
                        + " rutlegal:" + obj.RutRepLegal + " replegal:" + obj.NombreRepLegal + " nacionalidad:" + obj.Nacionalidad
                        + " web:" + obj.Mostrar + " comentario:" + obj.Comentario + " tipocartera:" + obj.TipoCartera +
                        " transportista:" + obj.Transportista + " usuario:" + obj.Usuario + " naviera:" + obj.Naviera
                        + " codigoSAP: " + obj.CodigoSAP + " idioma:" + idioma);*/
                    sp.AgregarParametro("pcs_codemp", codemp);
                    sp.AgregarParametro("pcs_pclid", idCliente);
                    //sp.AgregarParametro("pcs_pcsid", num)
                    sp.AgregarParametro("pcs_nombre", obj.NombreSucursal);
                    sp.AgregarParametro("pcs_comid", obj.Comuna);
                    sp.AgregarParametro("pcs_direccion", obj.Direccion);
                    sp.AgregarParametro("pcs_telefono", obj.Telefono);
                    sp.AgregarParametro("pcs_fax", obj.Fax);
                    sp.AgregarParametro("pcs_mail", obj.Correo);
                    if (obj.CasaMatriz == true)
                    {
                        sp.AgregarParametro("pcs_casamatriz", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("pcs_casamatriz", "N");
                    }

                    if (obj.Banco != "0")
                    {
                        sp.AgregarParametro("pcs_bcoid", obj.Banco);
                        sp.AgregarParametro("pcs_tipcta", obj.TipoCuenta);
                        sp.AgregarParametro("pcs_numcta", obj.Numero);
                    }
                    else
                    {
                        sp.AgregarParametro("pcs_bcoid", DBNull.Value);
                        sp.AgregarParametro("pcs_tipcta", DBNull.Value);
                        sp.AgregarParametro("pcs_numcta", "");
                    }

                    sp.AgregarParametro("pcs_codigo", obj.CodigoSucursal);

                    int err = sp.EjecutarProcedimientoTrans();
                }
                

            }

            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public static int GrabarClienteImpuesto(dto.ProveedorCliente obj, int codemp, int idCliente)
        {
            try
            {
                int exist = 0;
                //Se evalúa si existe
                DataSet dsExist = new DataSet();
                StoredProcedure spExist = new StoredProcedure("_Existe_ProvCli_Impuestos");
                spExist.AgregarParametro("pci_pclid", idCliente);
                dsExist = spExist.EjecutarProcedimiento();
                if (dsExist.Tables[0].Rows.Count > 0)
                    exist = Int32.Parse(dsExist.Tables[0].Rows[0][0].ToString());//Si devuelve 1 existe

                else
                    exist = 0;
                if (exist != 1)
                {
                    StoredProcedure sp = new StoredProcedure("Insertar_ProvCli_Impuestos");
                    sp.AgregarParametro("pci_codemp", codemp);
                    sp.AgregarParametro("pci_pclid", idCliente);
                    sp.AgregarParametro("pci_iptid", obj.Impuesto);

                    int err = sp.EjecutarProcedimientoTrans();
                }
               

            }

            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public static int GrabarClienteContacto(dto.ProveedorCliente obj, int codemp, int idCliente)
        {
            try
            {
                int exist = 0;
                //Se evalúa si existe
                DataSet dsExist = new DataSet();
                StoredProcedure spExist = new StoredProcedure("_Existe_ProvCli_Contacto");
                spExist.AgregarParametro("psc_pclid", idCliente);
                dsExist = spExist.EjecutarProcedimiento();
                if (dsExist.Tables[0].Rows.Count > 0)
                    exist = Int32.Parse(dsExist.Tables[0].Rows[0][0].ToString());//Si devuelve 1 existe

                else
                    exist = 0;

                if (exist != 1)
                {
                    StoredProcedure sp = new StoredProcedure("_Insertar_ProvCli_Sucursal_Contacto");
                    /*Debug.WriteLine("DATOS A INSERTAR : " + "codemp:" + codemp + " tipoCliente:" + tipoCliente + " tipo:" + obj.Tipo
                        + " rut:" + obj.Rut + " Nombre:" + obj.Nombre + " ApellidoPat:" + obj.ApellidoPaterno + " ApellidoMat:" + obj.ApellidoMaterno
                        + " nombrefant:" + obj.NombreFantasia + " giro:" + obj.Giro + " ESTADO: " + obj.Estados
                        + " rutlegal:" + obj.RutRepLegal + " replegal:" + obj.NombreRepLegal + " nacionalidad:" + obj.Nacionalidad
                        + " web:" + obj.Mostrar + " comentario:" + obj.Comentario + " tipocartera:" + obj.TipoCartera +
                        " transportista:" + obj.Transportista + " usuario:" + obj.Usuario + " naviera:" + obj.Naviera
                        + " codigoSAP: " + obj.CodigoSAP + " idioma:" + idioma);*/
                    sp.AgregarParametro("psc_codemp", codemp);
                    sp.AgregarParametro("psc_pclid", idCliente);
                    sp.AgregarParametro("psc_pcsid", obj.Sucursal);
                    //sp.AgregarParametro("psc_pscid", num)
                    sp.AgregarParametro("psc_ticid", obj.TipoContacto);
                    sp.AgregarParametro("psc_nombre", obj.NombreContacto);
                    sp.AgregarParametro("psc_telefono", obj.TelefonoContacto);
                    sp.AgregarParametro("psc_anexo", obj.AnexoContacto);
                    sp.AgregarParametro("psc_fax", obj.FaxContacto);
                    sp.AgregarParametro("psc_celular", obj.CelularContacto);
                    sp.AgregarParametro("psc_mail", obj.CorreoContacto);

                    int err = sp.EjecutarProcedimientoTrans();
                }
               

            }

            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public static int GrabarClienteCuentaCorriente(dto.ProveedorCliente obj, int codemp, int idCliente)
        {
            try
            {
                int exist = 0;
                //Se evalúa si existe
                DataSet dsExist = new DataSet();
                StoredProcedure spExist = new StoredProcedure("_Existe_ProvCli_CtaCte");
                spExist.AgregarParametro("pct_pclid", idCliente);
                dsExist = spExist.EjecutarProcedimiento();
                if (dsExist.Tables[0].Rows.Count > 0)
                    exist = Int32.Parse(dsExist.Tables[0].Rows[0][0].ToString());//Si devuelve 1 existe

                else
                    exist = 0;

                if (exist != 1)
                {
                    StoredProcedure sp = new StoredProcedure("Insertar_ProvCli_CtaCte");
                    /*Debug.WriteLine("DATOS A INSERTAR : " + "codemp:" + codemp + " tipoCliente:" + tipoCliente + " tipo:" + obj.Tipo
                        + " rut:" + obj.Rut + " Nombre:" + obj.Nombre + " ApellidoPat:" + obj.ApellidoPaterno + " ApellidoMat:" + obj.ApellidoMaterno
                        + " nombrefant:" + obj.NombreFantasia + " giro:" + obj.Giro + " ESTADO: " + obj.Estados
                        + " rutlegal:" + obj.RutRepLegal + " replegal:" + obj.NombreRepLegal + " nacionalidad:" + obj.Nacionalidad
                        + " web:" + obj.Mostrar + " comentario:" + obj.Comentario + " tipocartera:" + obj.TipoCartera +
                        " transportista:" + obj.Transportista + " usuario:" + obj.Usuario + " naviera:" + obj.Naviera
                        + " codigoSAP: " + obj.CodigoSAP + " idioma:" + idioma);*/
                    sp.AgregarParametro("pct_codemp", codemp);
                    sp.AgregarParametro("pct_tpcid", obj.Tipo);
                    sp.AgregarParametro("pct_pclid", idCliente);
                    sp.AgregarParametro("pct_frpid", obj.FormaDePago);
                    if (obj.UtilizaCredito == true)
                    {
                        sp.AgregarParametro("pct_credito", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("pct_credito", "N");
                    }
                    sp.AgregarParametro("pct_limite_credito", obj.LimiteCredito);
                    sp.AgregarParametro("pct_credito_consumido", obj.CreditoConsumido);
                    if (obj.EstadoCredito.Equals("1"))
                    {
                        sp.AgregarParametro("pct_estado", "A");
                    }
                    else if (obj.EstadoCredito.Equals("2"))
                    {
                        sp.AgregarParametro("pct_estado", "M");
                    }
                    else if (obj.EstadoCredito.Equals("3"))
                    {
                        sp.AgregarParametro("pct_estado", "B");
                    }
                    else if (obj.EstadoCredito.Equals("4"))
                    {
                        sp.AgregarParametro("pct_estado", "P");
                    }

                    sp.AgregarParametro("pct_comentarios", obj.ComentarioCuentaCorriente);

                    int err = sp.EjecutarProcedimientoTrans();
                }
                
            }

            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public static int GrabarClienteContrato(dto.ProveedorCliente obj, int codemp, int idCliente)
        {
            DateTime now = DateTime.Now;
            try
            {
                int exist = 0;
                //Se evalúa si existe
                DataSet dsExist = new DataSet();
                StoredProcedure spExist = new StoredProcedure("_Existe_ProvCli_ContratosClientes");
                spExist.AgregarParametro("ctc_pclid", idCliente);
                dsExist = spExist.EjecutarProcedimiento();
                if (dsExist.Tables[0].Rows.Count > 0)
                    exist = Int32.Parse(dsExist.Tables[0].Rows[0][0].ToString());//Si devuelve 1 existe

                else
                    exist = 0;
                if (exist != 1)
                {
                    StoredProcedure sp = new StoredProcedure("Insertar_Contratos_Clientes");
                    /*Debug.WriteLine("DATOS A INSERTAR : " + "codemp:" + codemp + " tipoCliente:" + tipoCliente + " tipo:" + obj.Tipo
                        + " rut:" + obj.Rut + " Nombre:" + obj.Nombre + " ApellidoPat:" + obj.ApellidoPaterno + " ApellidoMat:" + obj.ApellidoMaterno
                        + " nombrefant:" + obj.NombreFantasia + " giro:" + obj.Giro + " ESTADO: " + obj.Estados
                        + " rutlegal:" + obj.RutRepLegal + " replegal:" + obj.NombreRepLegal + " nacionalidad:" + obj.Nacionalidad
                        + " web:" + obj.Mostrar + " comentario:" + obj.Comentario + " tipocartera:" + obj.TipoCartera +
                        " transportista:" + obj.Transportista + " usuario:" + obj.Usuario + " naviera:" + obj.Naviera
                        + " codigoSAP: " + obj.CodigoSAP + " idioma:" + idioma);*/
                    sp.AgregarParametro("ctc_codemp", codemp);
                    sp.AgregarParametro("ctc_cctid", obj.ContratoCartera);
                    sp.AgregarParametro("ctc_pclid", idCliente);
                    if (obj.FechaInicioContrato != "")
                    {
                        sp.AgregarParametro("ctc_fecini", obj.FechaInicioContrato);
                    }
                    else
                    {
                        sp.AgregarParametro("ctc_fecini", String.Format("{0:MM/dd/yyyy}", now));
                    }
                    if (obj.FechaInicioContrato != "")
                    {
                        sp.AgregarParametro("ctc_fecfin", obj.FechaFinContrato);
                    }
                    else
                    {
                        sp.AgregarParametro("ctc_fecfin", String.Format("{0:MM/dd/yyyy}", now));
                    }
                    if (obj.Indefinido == true)
                    {
                        sp.AgregarParametro("ctc_indefinido", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("ctc_indefinido", "N");
                    }
                    sp.AgregarParametro("ctc_rut", obj.RutContrato);
                    sp.AgregarParametro("ctc_nombre", obj.NombreContrato);

                    if (obj.InteresClientes == true)
                    {
                        sp.AgregarParametro("ctc_intcli", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("ctc_intcli", "N");
                    }

                    if (obj.HonorariosClientes == true)
                    {
                        sp.AgregarParametro("ctc_honcli", "S");
                    }
                    else
                    {
                        sp.AgregarParametro("ctc_honcli", "N");
                    }

                    int err = sp.EjecutarProcedimientoTrans();
                }
               
            }

            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public static List<Combobox> ListarEstadosCredito(int idioma, string first)
        {
            //string salida = "";
            List<Combobox> lst = new List<Combobox>();
            if (!string.IsNullOrEmpty(first))
            {
                lst.Add(new Combobox()
                {
                    Text = first,
                    Value = ""
                });
            }
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");

                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    sp.AgregarParametro("codigo", "EstC0" + i);
                    sp.AgregarParametro("idioma", idioma);
                    ds = sp.EjecutarProcedimiento();
                    lst.Add(new Dimol.dto.Combobox()
                    {
                        Text = ds.Tables[0].Rows[0][0].ToString(),
                        Value = i.ToString()
                    });

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
