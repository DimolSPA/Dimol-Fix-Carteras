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
    public class PeriodoContableMes
    {
        public List<dto.PeriodoContableMes> ListarPeriodosGrilla(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PeriodoContableMes> lstPeriodos = new List<dto.PeriodoContableMes>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Periodos_Mensuales_Grilla");
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
                        lstPeriodos.Add(new dto.PeriodoContableMes()
                        {

                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Ano = Int16.Parse(ds.Tables[1].Rows[i]["ano"].ToString()),
                            Mes = getNombreMes(Int16.Parse(ds.Tables[1].Rows[i]["mes"].ToString())),
                            Inicio = DateTime.Parse(ds.Tables[1].Rows[i]["inicio"].ToString()),
                            Fin = DateTime.Parse(ds.Tables[1].Rows[i]["fin"].ToString()),
                            Habilitado = convertirTrueFalse(ds.Tables[1].Rows[i]["habilitado"].ToString()),
                            Finalizado = convertirTrueFalse(ds.Tables[1].Rows[i]["finalizado"].ToString()),
                            IdPeriodoMensual = Int32.Parse(ds.Tables[1].Rows[i]["IdPeriodoMensual"].ToString())
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

        public bool convertirTrueFalse(string val)
        {
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

        public string getNombreMes(int val)
        {
            string nom = "";
            if (val == 1)
            {
                nom = "Enero";
            }
            else if (val == 2)
            {
                nom = "Febrero";
            }
            else if (val == 3)
            {
                nom = "Marzo";
            }
            else if (val == 4)
            {
                nom = "Abril";
            }
            else if (val == 5)
            {
                nom = "Mayo";
            }
            else if (val == 6)
            {
                nom = "Junio";
            }
            else if (val == 7)
            {
                nom = "Julio";
            }
            else if (val == 8)
            {
                nom = "Agosto";
            }
            else if (val == 9)
            {
                nom = "Septiembre";
            }
            else if (val == 10)
            {
                nom = "Octubre";
            }
            else if (val == 11)
            {
                nom = "Noviembre";
            }
            else if (val == 12)
            {
                nom = "Diciembre";
            }
            return nom;
        }


        public List<dto.PeriodoContableMes> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.PeriodoContableMes> lstPeriodos = new List<dto.PeriodoContableMes>();
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
                        lstPeriodos.Add(new dto.PeriodoContableMes()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Ano = Int16.Parse(ds.Tables[1].Rows[i]["ano"].ToString()),
                            Mes = getNombreMes(Int16.Parse(ds.Tables[1].Rows[i]["mes"].ToString())),
                            Inicio = DateTime.Parse(ds.Tables[1].Rows[i]["inicio"].ToString()),
                            Fin = DateTime.Parse(ds.Tables[1].Rows[i]["fin"].ToString()),
                            Habilitado = convertirTrueFalse(ds.Tables[1].Rows[i]["habilitado"].ToString()),
                            Finalizado = convertirTrueFalse(ds.Tables[1].Rows[i]["finalizado"].ToString()),
                            IdPeriodoMensual = Int32.Parse(ds.Tables[1].Rows[i]["IdPeriodoMensual"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPeriodos;
        }


        /*public void InsertarPeriodo(dto.PeriodoContableMes objAccion, int codemp)
        {
            DateTime fecInicio, fecFin;
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Periodos_Contables_Meses");
                sp.AgregarParametro("pcm_codemp", codemp);
                sp.AgregarParametro("pcm_anio", objAccion.Ano);
                sp.AgregarParametro("pcm_mes", objAccion.Mes);

                fecFin = new DateTime(objAccion.Ano, Int16.Parse(objAccion.Mes.ToString()), 1, 0, 0, 0, 123);

                String.Format("{0:d/M/yyyy HH:mm:ss}", fecFin);
                sp.AgregarParametro("pcm_inicio", fecFin);
                Debug.WriteLine("INSERTAR SP3");
                fecFin = fecFin.AddMonths(1);
                fecFin = fecFin.AddDays(-1);
                sp.AgregarParametro("pcm_fin", fecFin);
                sp.AgregarParametro("pcm_apeini", 0);
                sp.AgregarParametro("pcm_apefin", 0);
                sp.AgregarParametro("pcm_ingini", 0);
                sp.AgregarParametro("pcm_ingfin", 0);
                sp.AgregarParametro("pcm_egreini", 0);
                sp.AgregarParametro("pcm_egrefin", 0);
                sp.AgregarParametro("pcm_trasini", 0);
                sp.AgregarParametro("pcm_trasfin", 0);
                sp.AgregarParametro("pcm_habilitado", "S");
                sp.AgregarParametro("pcm_finalizado", "N");
                int error3 = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }*/

        public void BorrarPeriodo(int codemp, int? id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Periodos_Contables_Meses");
                sp.AgregarParametro("pec_codemp", codemp);
                string _id = id.ToString();
                string anio = _id.Substring(1, 4);
                sp.AgregarParametro("pc_anio", anio);
                string mes = _id.Substring(5, 6);
                sp.AgregarParametro("pc_mes", mes);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarPeriodo(dto.PeriodoContableMes objAccion, int codemp, int id)
        {
            //DateTime fecInicio, fecFin;
            try
            {
                Debug.WriteLine("ID " + id);
                StoredProcedure sp = new StoredProcedure("Update_Periodos_Contables_Meses");
                sp.AgregarParametro("pcm_codemp", codemp);
                string _id = id.ToString();
                string anio = _id.Substring(1, 4);
                sp.AgregarParametro("pcm_anio", anio);
                string mes = _id.Substring(5);
                sp.AgregarParametro("pcm_mes", mes);
               
                //fecFin = new DateTime(objAccion.Ano, Int16.Parse(objAccion.Mes.ToString()), 1, 0, 0, 0, 123);
                Debug.WriteLine("DATOS AL EDITAR " + codemp + "-" + anio + "-" + mes + "-" + objAccion.Inicio + "-" + objAccion.Fin);
                String.Format("{0:d/M/yyyy HH:mm:ss}", objAccion.Inicio);
                String.Format("{0:d/M/yyyy HH:mm:ss}", objAccion.Fin);
                sp.AgregarParametro("pcm_inicio", objAccion.Inicio);
                sp.AgregarParametro("pcm_fin", objAccion.Fin);
                
                sp.AgregarParametro("pcm_habilitado", convertirTrueFalse(objAccion.Habilitado));
                sp.AgregarParametro("pcm_finalizado", convertirTrueFalse(objAccion.Finalizado));
                int error3 = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ListarPeriodoContableMesCount(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Periodos_Mensuales_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                //sp.AgregarParametro("inicio", inicio);
                //sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
               // Debug.WriteLine("NRO DATOS COUNT" + ds.Tables.Count);
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
