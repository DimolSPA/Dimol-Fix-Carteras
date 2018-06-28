using Dimol.dao;
using PJSpider.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSpider.dao
{
    public class Interno
    {
        public static List<RolActualizar> ListarRolesInternos()
        {
            //string[] numero;
            //int rol = 0;
            //int anio = 0;
            //bool tieneLetras = false;
            //DateTime fechaHistorial = new DateTime();
            //DateTime fechaReceptor = new DateTime();
            List<RolActualizar> lst = new List<RolActualizar>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Interno_Listar_Roles");
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        try
                        {
                            //numero = ds.Tables[0].Rows[i]["rol_numero"].ToString().Split('-');
                            //rol = Int32.Parse(numero[0].Replace(".", ""));
                            //anio = Int32.Parse(numero[1].Replace(".", ""));
                            lst.Add(new RolActualizar()
                            {
                                Anio = Int32.Parse(ds.Tables[0].Rows[i]["anio"].ToString()),
                                //Codemp = Int32.Parse(ds.Tables[0].Rows[i]["rol_codemp"].ToString()),
                                Numero = ds.Tables[0].Rows[i]["numero"].ToString(),
                                Rol = Int32.Parse(ds.Tables[0].Rows[i]["numero"].ToString()),
                                //Rolid = Int32.Parse(ds.Tables[0].Rows[i]["rolid"].ToString()),
                                TipoCausa = ds.Tables[0].Rows[i]["tipo"].ToString(),
                                //Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                                IdCausa = Int32.Parse(ds.Tables[0].Rows[i]["id_causa"].ToString()),
                                IdCuaderno = Int32.Parse(ds.Tables[0].Rows[i]["id_cuaderno"].ToString()),
                                IdTribunal = Int32.Parse(ds.Tables[0].Rows[i]["tribunal"].ToString()),
                                HTML = ds.Tables[0].Rows[i]["Html"].ToString()
                            });
                        }
                        catch (Exception e)
                        {
                            Dimol.dao.Funciones.InsertarError("Numero de rol con formato incorrecto", "Rol: " + ds.Tables[0].Rows[i]["rol_numero"].ToString(), "Bot Poder Judicial", 0);
                        }

                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Roles Internos Poder Judicial", 0);
                return lst;
            }
        }
    }
}
