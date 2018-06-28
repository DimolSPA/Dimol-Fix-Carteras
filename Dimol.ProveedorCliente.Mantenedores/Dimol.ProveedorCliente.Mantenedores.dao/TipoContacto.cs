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
    public class TipoContacto
    {
        public List<dto.TipoContacto> ListarGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoContacto> lst = new List<dto.TipoContacto>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposContacto_Grilla");
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
                        lst.Add(new dto.TipoContacto()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public List<dto.TipoContacto> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TipoContacto> lst = new List<dto.TipoContacto>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposContacto_Grilla");
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
                        lst.Add(new dto.TipoContacto()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[0].Rows[i]["id"].ToString()),
                            Nombre = ds.Tables[0].Rows[i]["nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }



        public void Insertar(dto.TipoContacto objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_TipoContacto");
                sp.AgregarParametro("tic_codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("tic_nombre", objAccion.Nombre);
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
                StoredProcedure sp = new StoredProcedure("Delete_Tipos_Contacto");
                sp.AgregarParametro("tic_codemp", codemp);
                sp.AgregarParametro("tic_ticid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.TipoContacto objAccion, int codemp, int idioma, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Tipos_Contacto");
                sp.AgregarParametro("tic_codemp", codemp);
                sp.AgregarParametro("tic_ticid", id);
                sp.AgregarParametro("tic_nombre", objAccion.Nombre);
                int error = sp.EjecutarProcedimientoTrans();

                StoredProcedure sp2 = new StoredProcedure("Update_Tipos_Contacto_Idiomas");
                sp2.AgregarParametro("tci_codemp", codemp);
                sp2.AgregarParametro("tci_ticid", id);
                sp2.AgregarParametro("tci_idid", idioma);
                sp2.AgregarParametro("tci_nombre", objAccion.Nombre);

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
                StoredProcedure sp = new StoredProcedure("_Listar_TiposContacto_Grilla_Count");
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
