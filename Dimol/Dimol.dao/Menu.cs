using System;
using System.Collections.Generic;
using System.Data;

namespace Dimol.dao
{
    public class Menu
    {
        Funciones objFunc = new Funciones();

        public string[] TraeMenuEncriptado(int codemp)
        {
            try
            {
                DataSet ds = new DataSet();
                string[] salida =  new string[2]{"",""};
                StoredProcedure sp = new StoredProcedure("_Trae_Menu_Encriptado");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                salida[0] = objFunc.Desencripta( ds.Tables[0].Rows[0]["emp_menu"].ToString());
                salida[1] = ds.Tables[0].Rows[0]["emp_nombre"].ToString();

                return salida;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.dao.Menu.TraeMenuEncriptado", 0);
                throw ex;
            }
            
        }

        public List<dto.Menu> TraeMenuUsuario(int usuario, int idioma, int codemp, string menu, string dominio)
        {
            try
            {
                DataSet ds = new DataSet();
                List<dto.Menu> lstMenu = new List<dto.Menu>();
                StoredProcedure sp = new StoredProcedure("_Trae_Usuarios_Menu");
                sp.AgregarParametro("usuario", usuario);
                sp.AgregarParametro("idiomas", idioma);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("menu", menu);
                ds = sp.EjecutarProcedimiento();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstMenu.Add(new dto.Menu()
                    {
                        Id = ds.Tables[0].Rows[i]["trv_trvid"].ToString(),
                        ParentId = ds.Tables[0].Rows[i]["trv_padid"].ToString(),
                        Text = ds.Tables[0].Rows[i]["tvi_idiomas"].ToString(),
                        Url= dominio + ds.Tables[0].Rows[i]["Ventana"].ToString(),
                        Tooltip = ds.Tables[0].Rows[i]["trv_imagen"].ToString()
                    });
                }
                return lstMenu;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.dao.Menu.TraeMenuUsuario", usuario);
                throw ex;
            }
        }
    }
}
 