using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.Contabilidad.Mantenedores.dto;
using System.Data;
using Dimol.dao;
using System.Data.SqlClient;
using System.Diagnostics;


namespace Dimol.Contabilidad.Mantenedores.dao
{
    public class PeriodoContable
    {
        public List<dto.PeriodoContable> ListarPeriodosGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PeriodoContable> lstPeriodos = new List<dto.PeriodoContable>();
            try
            {
                
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Periodos_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                //Debug.WriteLine("PARAMETROS SP " + codemp + " " + where + " " + sidx + " " + sord + " " + inicio + " " + limite);
                ds = sp.EjecutarProcedimiento();
                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    //Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.PeriodoContable()
                        {
                            
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Ano = Int16.Parse(ds.Tables[1].Rows[i]["ano"].ToString()),
                            Habilitado = convertirTrueFalse(ds.Tables[1].Rows[i]["habilitado"].ToString()),
                            Finalizado = convertirTrueFalse(ds.Tables[1].Rows[i]["finalizado"].ToString()),
                            IdPeriodo = Int16.Parse(ds.Tables[1].Rows[i]["IdPeriodo"].ToString())
                        });
                    }
                }
                Debug.WriteLine("lstPeriodos" + lstPeriodos);
                return lstPeriodos;
            }
            catch (Exception ex)
            {
                return lstPeriodos;
            }
            
        }

        public bool convertirTrueFalse(string val){
            bool returnval = true;
            if (val.Equals("S"))
            {
                returnval = true;
            }
            else
            {
                returnval = false;
            }
            return returnval;
        }

        public string convertirTrueFalse(bool val)
        {
            string returnval = "S";
            if (val == true)
            {
                returnval = "S";
            }
            else
            {
                returnval = "N";
            }
            return returnval;
        }


        public List<dto.PeriodoContable> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PeriodoContable> lstPeriodos = new List<dto.PeriodoContable>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Periodos_Grilla");
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
                        lstPeriodos.Add(new dto.PeriodoContable()
                        {
                            Codemp = Int16.Parse(ds.Tables[0].Rows[i]["codemp"].ToString()),
                            Ano = Int16.Parse(ds.Tables[0].Rows[i]["ano"].ToString()),
                            Habilitado = Boolean.Parse(ds.Tables[0].Rows[i]["habilitado"].ToString()),
                            Finalizado = Boolean.Parse(ds.Tables[0].Rows[i]["finalizado"].ToString()),
                            IdPeriodo = Int16.Parse(ds.Tables[0].Rows[i]["idPeriodo"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPeriodos;
        }

       
        public void InsertarPeriodo(dto.PeriodoContable objAccion, int codemp)
        {
            
            DateTime fecFin; 
            try
            {
                Debug.WriteLine("INSERTAR SP1");
                StoredProcedure sp = new StoredProcedure("Insertar_Periodos_Contables");
                sp.AgregarParametro("pec_codemp", codemp);
                sp.AgregarParametro("pec_anio", objAccion.Ano);
                Debug.WriteLine("HABILITADO" + objAccion.Habilitado);
                string hab = this.convertirTrueFalse(objAccion.Habilitado);
                string fin = this.convertirTrueFalse(objAccion.Finalizado);
                sp.AgregarParametro("pec_habilitado", "S");
                sp.AgregarParametro("pec_finalizado", "N");
                int error = sp.EjecutarProcedimientoTrans();

                for (int i = 1; i <= 12; i++)
                {
                    StoredProcedure sp3 = new StoredProcedure("Insertar_Periodos_Contables_Meses");
                    sp3.AgregarParametro("pcm_codemp", codemp);
                    sp3.AgregarParametro("pcm_anio", objAccion.Ano);
                    sp3.AgregarParametro("pcm_mes", i);

                    fecFin = new DateTime(objAccion.Ano, i, 1, 0, 0, 0, 123);

                    String.Format("{0:d/M/yyyy HH:mm:ss}", fecFin); 
                    sp3.AgregarParametro("pcm_inicio", fecFin);
                    Debug.WriteLine("INSERTAR SP3");
                    fecFin = fecFin.AddMonths(1);
                    fecFin = fecFin.AddDays(-1);
                    sp3.AgregarParametro("pcm_fin", fecFin);
                    sp3.AgregarParametro("pcm_apeini", 0);
                    sp3.AgregarParametro("pcm_apefin", 0);
                    sp3.AgregarParametro("pcm_ingini", 0);
                    sp3.AgregarParametro("pcm_ingfin", 0);
                    sp3.AgregarParametro("pcm_egreini", 0);
                    sp3.AgregarParametro("pcm_egrefin", 0);
                    sp3.AgregarParametro("pcm_trasini", 0);
                    sp3.AgregarParametro("pcm_trasfin", 0);
                    sp3.AgregarParametro("pcm_habilitado", "S");
                    sp3.AgregarParametro("pcm_finalizado", "N");
                    int error3 = sp3.EjecutarProcedimientoTrans();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void BorrarPeriodo(int codemp, int? id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Periodos_Contables");
                sp.AgregarParametro("pec_codemp", codemp);
                string _id = id.ToString();
                string anio = _id.Substring(1,4);
                Debug.WriteLine("ANIO " + anio);
                sp.AgregarParametro("pec_anio", anio);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarPeriodo(dto.PeriodoContable objAccion, int codemp, int user)
        {
            string[] tipos = new string[4] {"I", "E", "T", "A"};
            DataSet ds = new DataSet();
            try
            {
                //Debug.WriteLine("SP EDITARPERIODO  " + objAccion.Ano + " - " + codemp + " - " + objAccion.Habilitado);
                if (objAccion.Finalizado == true)
                {
                    StoredProcedure sp2 = new StoredProcedure("Update_Periodo_Contable_Cierre");
                    sp2.AgregarParametro("ast_codemp", codemp);
                    sp2.AgregarParametro("ast_anio", objAccion.Ano);
                    sp2.AgregarParametro("usrid", user);
                    int error = sp2.EjecutarProcedimientoTrans();
                }
                else
                {
                    StoredProcedure sp = new StoredProcedure("Update_Periodos_Contables");
                    sp.AgregarParametro("pec_codemp", codemp);
                    sp.AgregarParametro("pec_anio", objAccion.Ano);
                    sp.AgregarParametro("pec_habilitado", this.convertirTrueFalse(objAccion.Habilitado));
                    sp.AgregarParametro("pec_finalizado", this.convertirTrueFalse(objAccion.Finalizado));
                    //sp.AgregarParametro("IdPeriodo", objAccion.IdPeriodo);
                    int error = sp.EjecutarProcedimientoTrans();
                }

                for (int i = 0; i < tipos.Length; i++)
                {
                    StoredProcedure sp = new StoredProcedure("_Trae_Numero_Asiento_Contable");
                    sp.AgregarParametro("codemp", codemp);
                    sp.AgregarParametro("anio", objAccion.Ano);
                    sp.AgregarParametro("tipo", tipos.GetValue(i));
                    ds = sp.EjecutarProcedimiento();
                    if (ds.Tables.Count > 0)
                    {
                        //Debug.WriteLine("HAY DATOS");
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            StoredProcedure sp2 = new StoredProcedure("Update_Asientos_Contables_NumFin");
                            sp2.AgregarParametro("ast_codemp", codemp);
                            sp2.AgregarParametro("ast_anio", objAccion.Ano);
                            sp2.AgregarParametro("ast_tipo", tipos.GetValue(i));
                            sp2.AgregarParametro("ast_numero", ds.Tables[0].Rows[j][0]);
                            sp2.AgregarParametro("ast_numfin", j+1);
                            int error = sp2.EjecutarProcedimientoTrans();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ListarPeriodoContableCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Periodos_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                //sp.AgregarParametro("inicio", inicio);
                //sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("NRO DATOS COUNT" + ds.Tables.Count);
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
