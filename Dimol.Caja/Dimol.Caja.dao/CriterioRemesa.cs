using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.dao
{
    public class CriterioRemesa
    {
        public static List<dto.CriterioRemesa> ListarCriterioRemesaClienteGrilla(int codemp, int pclid, string where, string sidx, string sord)
        {
            List<dto.CriterioRemesa> lst = new List<dto.CriterioRemesa>();
           
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_CriteriosRemesa_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.CriterioRemesa()
                        {
                            Id = Int32.Parse(ds.Tables[0].Rows[i]["Id"].ToString()),
                            DesdeDiasVencido = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["DesdeDiasVencido"].ToString()) ? (int?)null: int.Parse(ds.Tables[0].Rows[i]["DesdeDiasVencido"].ToString()),
                            HastaDiasVencido = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["HastaDiasVencido"].ToString()) ? (int?)null : int.Parse(ds.Tables[0].Rows[i]["HastaDiasVencido"].ToString()),
                            RegionMetropolitana = ds.Tables[0].Rows[i]["RegionMetropolitana"].ToString(),
                            CodigoCarga = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["codigocarga"].ToString()) ? (int?)null : int.Parse(ds.Tables[0].Rows[i]["codigocarga"].ToString()),
                            CodigoDeCarga = ds.Tables[0].Rows[i]["CodigoDeCarga"].ToString(),
                            TipoCambioCapital = ds.Tables[0].Rows[i]["TipoCambioCapital"].ToString(),
                            SimboloId = int.Parse(ds.Tables[0].Rows[i]["SimboloId"].ToString()),
                            Capital = int.Parse(ds.Tables[0].Rows[i]["Capital"].ToString()),
                            Interes = int.Parse(ds.Tables[0].Rows[i]["Interes"].ToString()),
                            Honorario = int.Parse(ds.Tables[0].Rows[i]["Honorario"].ToString()),
                            TipoConciliacionId = int.Parse(ds.Tables[0].Rows[i]["TipoConciliacionId"].ToString()),
                            TipoConciliacion = ds.Tables[0].Rows[i]["TipoConciliacion"].ToString(),
                            ConcicionId = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["CondicionId"].ToString()) ? (int?)null : int.Parse(ds.Tables[0].Rows[i]["CondicionId"].ToString()),
                            CondicionAnticipo = ds.Tables[0].Rows[i]["CondicionAnticipo"].ToString(),
                            IsAnticipo = ds.Tables[0].Rows[i]["IsAnticipo"].ToString(),
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.CriterioRemesa.ListarCriterioRemesaClienteGrilla", 0);
                return lst;
            }
        }

        public static int InsertarCriterio(int codemp, int pclid, string desdeDiasVencido, string hastaDiasVencido,string codigoRegion,
                                                    string codigoCarga, string capital, string interes, string honorario,
                                                    string tipoCambioCapital, int simboloId, int tipoConciliacion, string condicionId, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_CriterioRemesa");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("desdeDiasVencido", string.IsNullOrEmpty(desdeDiasVencido) ? DBNull.Value : (object)int.Parse(desdeDiasVencido));
                sp.AgregarParametro("hastaDiasVencido", string.IsNullOrEmpty(hastaDiasVencido) ? DBNull.Value : (object)int.Parse(hastaDiasVencido));
                sp.AgregarParametro("codigoRegion", string.IsNullOrEmpty(codigoRegion) ? DBNull.Value : (object)int.Parse(codigoRegion));
                sp.AgregarParametro("codigoCarga", string.IsNullOrEmpty(codigoCarga) ? DBNull.Value : (object)int.Parse(codigoCarga));
                sp.AgregarParametro("capital", string.IsNullOrEmpty(capital) ? 0 : (object)int.Parse(capital));
                sp.AgregarParametro("interes", string.IsNullOrEmpty(interes) ? 0 : (object)int.Parse(interes));
                sp.AgregarParametro("honorario", string.IsNullOrEmpty(honorario) ? 0 : (object)int.Parse(honorario));
                sp.AgregarParametro("tipoCambioCapital", tipoCambioCapital);
                sp.AgregarParametro("simboloId", simboloId);
                sp.AgregarParametro("conciliacionTipoId", tipoConciliacion);
                sp.AgregarParametro("condicionId", string.IsNullOrEmpty(condicionId) ? DBNull.Value : (object)int.Parse(condicionId));
                sp.AgregarParametro("userId", user);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["CriterioId"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.CriterioRemesa.InsertarCriterio", user);

                return id;
            }
            return id;
        }

        public static int ActualizarCriterio(int criterioId, int codemp, int pclid, string desdeDiasVencido, string hastaDiasVencido, string codigoRegion,
                                            string codigoCarga, string capital, string interes, string honorario,
                                            string tipoCambioCapital, int simboloId, int tipoConciliacion, string condicionId, int user)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Actualizar_CriterioRemesa");
                sp.AgregarParametro("Id", criterioId);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("desdeDiasVencido", string.IsNullOrEmpty(desdeDiasVencido) ? DBNull.Value : (object)int.Parse(desdeDiasVencido));
                sp.AgregarParametro("hastaDiasVencido", string.IsNullOrEmpty(hastaDiasVencido) ? DBNull.Value : (object)int.Parse(hastaDiasVencido));
                sp.AgregarParametro("codigoRegion", string.IsNullOrEmpty(codigoRegion) ? DBNull.Value : (object)int.Parse(codigoRegion));
                sp.AgregarParametro("codigoCarga", string.IsNullOrEmpty(codigoCarga) ? DBNull.Value : (object)int.Parse(codigoCarga));
                sp.AgregarParametro("capital", string.IsNullOrEmpty(capital) ? 0 : (object)int.Parse(capital));
                sp.AgregarParametro("interes", string.IsNullOrEmpty(interes) ? 0 : (object)int.Parse(interes));
                sp.AgregarParametro("honorario", string.IsNullOrEmpty(honorario) ? 0 : (object)int.Parse(honorario));
                sp.AgregarParametro("tipoCambioCapital", tipoCambioCapital);
                sp.AgregarParametro("simboloId", simboloId);
                sp.AgregarParametro("conciliacionTipoId", tipoConciliacion);
                sp.AgregarParametro("condicionId", string.IsNullOrEmpty(condicionId) ? DBNull.Value : (object)int.Parse(condicionId));
                sp.AgregarParametro("userId", user);

                id = sp.EjecutarProcedimientoTrans();
                                   

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.CriterioRemesa.InsertarCriterio", user);

                return id;
            }
            return id;
        }

        public static int ExisteCriterio(int criterioId)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Exist_CriterioRemesa");
                sp.AgregarParametro("Id", criterioId);
               
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = Int32.Parse(ds.Tables[0].Rows[0]["criterioId"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.CriterioRemesa.ExisteCriterio", 0);

                return id;
            }
            return id;
        }

        public static string EmpresaFactura(int codemp, int pclid)
        {
            string respuesta = string.Empty;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Empresa_SeEncuentraFacturando");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        respuesta = ds.Tables[0].Rows[0]["Respuesta"].ToString();
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.CriterioRemesa.EmpresaFactura", 0);

                return respuesta;
            }
            return respuesta;
        }

        public static int ExisteDefinicionCriterio(int codemp, int pclid, string desdeDiasVencido, string hastaDiasVencido, string codigoRegion,
                                                    string codigoCarga, int tipoConciliacion, string condicionId)
        {
            int id = -1;

            try
            {

                Funciones func = new Funciones();
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Exist_CriterioRemesa_Definicion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("desdeDiasVencido", string.IsNullOrEmpty(desdeDiasVencido) ? DBNull.Value : (object)int.Parse(desdeDiasVencido));
                sp.AgregarParametro("hastaDiasVencido", string.IsNullOrEmpty(hastaDiasVencido) ? DBNull.Value : (object)int.Parse(hastaDiasVencido));
                sp.AgregarParametro("codigoRegion", string.IsNullOrEmpty(codigoRegion) ? DBNull.Value : (object)int.Parse(codigoRegion));
                sp.AgregarParametro("codigoCarga", string.IsNullOrEmpty(codigoCarga) ? DBNull.Value : (object)int.Parse(codigoCarga));
                sp.AgregarParametro("conciliacionTipoId", tipoConciliacion);
                sp.AgregarParametro("condicionId", string.IsNullOrEmpty(condicionId) ? DBNull.Value : (object)int.Parse(condicionId));

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        id = int.Parse(ds.Tables[0].Rows[0]["existe"].ToString());
                    }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Caja.dao.CriterioRemesa.ExisteCriterio", 0);

                return id;
            }
            return id;
        }
    }
}
