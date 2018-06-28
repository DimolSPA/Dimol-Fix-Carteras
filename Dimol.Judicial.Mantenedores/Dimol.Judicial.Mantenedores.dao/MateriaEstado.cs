using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class MateriaEstado
    {
        public string ListarEstados(int codemp, int idioma )
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Estados");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
              
                ds = sp.EjecutarProcedimiento();
                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    //salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();

                    if (i == 1)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                }
                salida = salida.Replace("\"", "'");
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string ListarMaterias(int codemp, int idioma)
        {
            string salida = "";
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Materias");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);

                ds = sp.EjecutarProcedimiento();
                for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                {
                    //salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();

                    if (i == 1)
                    {
                        salida += ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                    else
                    {
                        salida += ";" + ds.Tables[0].Rows[i][0].ToString().Trim() + ":" + ds.Tables[0].Rows[i][1].ToString().Trim();
                    }
                }
                salida = salida.Replace("\"", "'");
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public List<Dimol.dto.Combobox> ListarEstadosCombo(int codemp, int idioma,int esjid)
        {
            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Estados_Historial_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("esjid", esjid);
                ds = sp.EjecutarProcedimiento();

                lst.Add(new Dimol.dto.Combobox
                {
                    Value = "-1",
                    Text ="Seleccione Estado"
                });

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lst.Add(new Dimol.dto.Combobox
                    {
                        Value = ds.Tables[0].Rows[i][0].ToString().Trim(),
                        Text = ds.Tables[0].Rows[i][1].ToString().Trim()
                    });
                }



                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public List<Dimol.dto.Combobox> ListarMateriasCombo(int codemp, int idioma)
        {
            List<Dimol.dto.Combobox> lst = new List<Dimol.dto.Combobox>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Materias_Historial_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);

                ds = sp.EjecutarProcedimiento();
                lst.Add(new Dimol.dto.Combobox
                {
                    Value = "-1",
                    Text = "Seleccione Materia"
                });
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    lst.Add(new Dimol.dto.Combobox
                    {
                        Value = ds.Tables[0].Rows[i][0].ToString().Trim(),
                        Text = ds.Tables[0].Rows[i][1].ToString().Trim()
                    });
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }

        }

        public List<dto.MateriaEstado> ListarMateriaEstadoGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MateriaEstado> lstMateriaEstado = new List<dto.MateriaEstado>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Materia_Estado_Grilla");
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
                        lstMateriaEstado.Add(new dto.MateriaEstado()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            IdEstado = Int16.Parse(ds.Tables[0].Rows[i]["idestado"].ToString()),
                            NombreEstado = ds.Tables[0].Rows[i]["nombreestado"].ToString(),
                            IdMateria = Int16.Parse(ds.Tables[0].Rows[i]["idmateria"].ToString()),
                            NombreMateria = ds.Tables[0].Rows[i]["nombremateria"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMateriaEstado;
        }

        public List<dto.MateriaEstado> ExportarExcel(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.MateriaEstado> lstMateriaEstado = new List<dto.MateriaEstado>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Materia_Estado_Grilla");
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
                        lstMateriaEstado.Add(new dto.MateriaEstado()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            IdEstado = Int16.Parse(ds.Tables[0].Rows[i]["idestado"].ToString()),
                            NombreEstado = ds.Tables[0].Rows[i]["nombreestado"].ToString(),
                            IdMateria = Int16.Parse(ds.Tables[0].Rows[i]["idmateria"].ToString()),
                            NombreMateria = ds.Tables[0].Rows[i]["nombremateria"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstMateriaEstado;
        }

        public static int ListarMateriaEstadoGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Materia_Estado_Grilla_Count");
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
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public void InsertarMateriaEstado(dto.MateriaEstado objAccion, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Materia_Estados");
                sp.AgregarParametro("mej_codemp", codemp);
                sp.AgregarParametro("mej_esjid", objAccion.NombreMateria);
                sp.AgregarParametro("mej_estid", objAccion.NombreEstado);
             
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void BorrarMateriaEstado(int codemp, int idEstado,int idMateria)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Materia_Estados");
                sp.AgregarParametro("mej_codemp", codemp);
                sp.AgregarParametro("mej_esjid", idMateria);
                sp.AgregarParametro("mej_estid", idEstado);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
