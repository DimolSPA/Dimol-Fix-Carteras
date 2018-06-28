
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;
using Dimol.dao;
using System.Data;
using System.Diagnostics;
using System.IO;


namespace Dimol.Contabilidad.Mantenedores.dao
{
    public class Talonario
    {
        public static List<Combobox> ListarTalonarios(int codemp, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_Talonarios");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["tac_nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["tac_tacid"].ToString()
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

        public static dto.Talonario getTalonarioPorId(int codemp, int id)
        {
            dto.Talonario talonario = new dto.Talonario();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Talonario_PorId");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id", id);
                               
                ds = sp.EjecutarProcedimiento();
                
                if (ds.Tables.Count > 0)
                {

                    talonario.tac_nombre = ds.Tables[0].Rows[0]["nombre"].ToString();
                    talonario.tac_numero = Int32.Parse(ds.Tables[0].Rows[0]["numero"].ToString());
                     
                }

                return talonario;
            }
            catch (Exception ex)
            {
                return talonario;
            }

        }

        public static List<dto.Talonario> ListarTalonariosSinAsignar(int codemp, int id, int suc, string where, string sidx, string sord)
        {
            List<dto.Talonario> lstPeriodos = new List<dto.Talonario>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Talonarios_SinAsignar_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idTal", id);
                sp.AgregarParametro("idSuc", suc);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                //sp.AgregarParametro("inicio", inicio);
                //sp.AgregarParametro("limite", limite);
                //Debug.WriteLine("PARAMETROS SP " + codemp + " " + where + " " + sidx + " " + sord + " " + inicio + " " + limite);
                ds = sp.EjecutarProcedimiento();
                //

                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.Talonario()
                        {

                            tac_tacid = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            tac_nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            tpc_talonario = convertirTrueFalse(ds.Tables[1].Rows[i]["tipo"].ToString())
                            

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

        public static List<dto.Talonario> ListarTalonariosAsignados(int codemp, int id, int suc, string where, string sidx, string sord)
        {
            List<dto.Talonario> lstPeriodos = new List<dto.Talonario>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Talonarios_Asignados_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idTal", id);
                sp.AgregarParametro("idSuc", suc);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                //sp.AgregarParametro("inicio", inicio);
                //sp.AgregarParametro("limite", limite);
                //Debug.WriteLine("PARAMETROS SP " + codemp + " " + where + " " + sidx + " " + sord + " " + inicio + " " + limite);
                ds = sp.EjecutarProcedimiento();
                //

                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.Talonario()
                        {

                            tac_tacid = Int16.Parse(ds.Tables[1].Rows[i]["id"].ToString()),
                            tac_nombre = ds.Tables[1].Rows[i]["nombre"].ToString(),
                            tpc_talonario = convertirTrueFalse(ds.Tables[1].Rows[i]["tipo"].ToString())


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

        public static bool convertirTrueFalse(string val)
        {
            bool returnval = true;
            if (val.Equals("N") && val != null)
            {
                returnval = true;
            }
            else
            {
                returnval = false;
            }
            return returnval;
        }

        public static int Insertar(dto.Talonario objAccion, int codemp, int sucursal)
        {
            int num = 0;
            int error = 0;
            
            try
            {
                DataSet dsn = new DataSet();
                StoredProcedure spn = new StoredProcedure("UltNum_Talonario_CpbtDoc");
                spn.AgregarParametro("tac_codemp", codemp);
                dsn = spn.EjecutarProcedimiento();

                num = Int32.Parse(dsn.Tables[0].Rows[0][0].ToString());

                if (objAccion.tac_tacid == 0)
                {
                    Debug.WriteLine("INGRESO NUEVO TALONARIO");
                    StoredProcedure sp2 = new StoredProcedure("Insertar_Talonario_CpbtDoc");
                    sp2.AgregarParametro("tac_codemp", codemp);
                    sp2.AgregarParametro("tac_tacid", num);
                    sp2.AgregarParametro("tac_nombre", objAccion.tac_nombre);
                    sp2.AgregarParametro("tac_numero", objAccion.tac_numero);

                    error = sp2.EjecutarProcedimientoTrans();
                    

                }
                else
                {
                    Debug.WriteLine("ACTUALIZO TALONARIO");
                    StoredProcedure sp3 = new StoredProcedure("Update_Talonario_CpbtDoc");
                    sp3.AgregarParametro("tac_codemp", codemp);
                    sp3.AgregarParametro("tac_tacid", objAccion.tac_tacid);
                    sp3.AgregarParametro("tac_nombre", objAccion.tac_nombre);
                    sp3.AgregarParametro("tac_numero", objAccion.tac_numero);

                    error = sp3.EjecutarProcedimientoTrans();
                }

                //Debug.WriteLine("ERRRRRRRRRRRROR " + error);
                if (error > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
                /*
                if(error > 0){
                    
                    For i = 0 To RaG_NoAsig.Items.Count - 1
                        If RaG_NoAsig.Items(i).Selected = True Then
                            Dim spTal As New Horus.StoredProcedure("Insertar_Tipos_CpbtDoc_Talonario")
                            spTal.AgregarParametro("tct_codemp", codemp)
                            spTal.AgregarParametro("tct_tacid", num)
                            spTal.AgregarParametro("tct_tpcid", RaG_NoAsig.Items(i).Cells(2).Text)
                            spTal.AgregarParametro("tct_sucid", codsuc)

                            err = spTal.EjecutarProcedimiento(conn, myTrans)

                            If err < 0 Then
                                Exit For
                            End If
                        End If
                    Next
                     
                }
                * */
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
