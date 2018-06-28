using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class DeudorQuiebra
    {
        public static int ListarDeudoresQuiebraCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Deudores_Quiebra_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
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
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.DeudorQuiebra.ListarDeudoresQuiebraCount", 0);
                return count;
            }
        }
        public static List<dto.DeudorQuiebra> ListarDeudoresQuiebra(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DeudorQuiebra> lst = new List<dto.DeudorQuiebra>();
            try
            {
              
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Deudores_Quiebra_Grilla");
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
                   
                        lst.Add(
                            new dto.DeudorQuiebra()
                            {
                                TribunalId = Int32.Parse(ds.Tables[0].Rows[i]["TRIBUNALID"].ToString()),
                                TipoCausaId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["TIPOCAUSAID"].ToString()) ? default(int) : Int32.Parse(ds.Tables[0].Rows[i]["TIPOCAUSAID"].ToString()),
                                MateriaJodicialId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MATERIAJODICIALID"].ToString()) ? default(int) : Int32.Parse(ds.Tables[0].Rows[i]["MATERIAJODICIALID"].ToString()),
                                Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                                Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                                RolNumero = ds.Tables[0].Rows[i]["ROLNUMERO"].ToString(),
                                Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString(),
                                Causa = ds.Tables[0].Rows[i]["Causa"].ToString(),
                                Materia = ds.Tables[0].Rows[i]["Materia"].ToString()
                            });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.DeudorQuiebra.ListarDeudoresQuiebra", 0);
                return lst;
            }
        }
        public static int InsertarDeudorQuiebra(int codemp, string rut, string nombre, string rolNumero, int tribumalId, string tipoCausa, string materia, int userId)
        {
            int result = -1;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudor_Quiebra");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rut", rut);
                sp.AgregarParametro("nombre", nombre);
                sp.AgregarParametro("rolNumero", rolNumero);
                sp.AgregarParametro("tribunalId", tribumalId);
                sp.AgregarParametro("tipoCausaId", string.IsNullOrEmpty(tipoCausa) ? DBNull.Value : (object)tipoCausa);
                sp.AgregarParametro("materiaId", string.IsNullOrEmpty(materia) ? DBNull.Value : (object)materia);
                sp.AgregarParametro("user", userId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    result = Int32.Parse(ds.Tables[0].Rows[0]["rut"].ToString());
                }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.DeudorQuiebra.InsertarDeudorQuiebra", userId);
                return -1;
            }
            return result;
        }
        public static List<dto.DeudorQuiebra> ListarDeudorQuiebra(int codemp, string rut)
        {
            List<dto.DeudorQuiebra> lst = new List<dto.DeudorQuiebra>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Deudor_Quiebra");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rut", rut);
              
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        lst.Add(
                            new dto.DeudorQuiebra()
                            {
                                TribunalId = Int32.Parse(ds.Tables[0].Rows[i]["TRIBUNALID"].ToString()),
                                TipoCausaId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["TIPOCAUSAID"].ToString()) ? default(int) : Int32.Parse(ds.Tables[0].Rows[i]["TIPOCAUSAID"].ToString()),
                                MateriaJodicialId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["MATERIAJODICIALID"].ToString()) ? default(int) : Int32.Parse(ds.Tables[0].Rows[i]["MATERIAJODICIALID"].ToString()),
                                RolNumero = ds.Tables[0].Rows[i]["ROLNUMERO"].ToString(),
                                Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString()
                               
                            });

                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.DeudorQuiebra.ListarDeudoresQuiebra", 0);
                return lst;
            }
        }
        public static int ActualizarDeudorQuiebra(int codemp, string rut, string tipoCausaId, string materiaId)
        {
            int id = -1;
           
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Actualizar_Deudor_Quiebra");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rut", rut);//deudor cliente
                sp.AgregarParametro("tipoCausaId", tipoCausaId);
                sp.AgregarParametro("materiaId", materiaId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    id = Int32.Parse(ds.Tables[0].Rows[0]["rut"].ToString());
                }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.DeudorQuiebra.ActualizarDeudorQuiebra", 0);
                return -1;
            }
            return id;
        }

        public static string TraeDeudorQuiebraRolNumero(int codemp, string rut)
        {
            string rolNumero = "0";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Deudor_Quiebra_RolNumero");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rut", rut);//deudor cliente
               

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    rolNumero = ds.Tables[0].Rows[0]["rolNumero"].ToString();
                }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.DeudorQuiebra.TraeDeudorQuiebraRolNumero", 0);
                return "0";
            }
            return rolNumero;
        }

        public static int TraeDeudorQuiebraIdRol(int codemp, int pclid, int ctcid, string RolNumero)
        {
            int idRol = 0;

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Deudor_Quiebra_RolId");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("rolNumero", RolNumero);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    idRol = Int32.Parse(ds.Tables[0].Rows[0]["rolId"].ToString());
                }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.DeudorQuiebra.TraeDeudorQuiebraIdRol", 0);
                return 0;
            }
            return idRol;
        }
    }
}
