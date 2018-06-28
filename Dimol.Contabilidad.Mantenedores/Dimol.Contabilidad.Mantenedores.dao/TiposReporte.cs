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
    public class TiposReporte
    {
        public List<dto.TiposReporte> ListarGrilla(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposReporte> lstPeriodos = new List<dto.TiposReporte>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposReporte_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                //Debug.WriteLine("PARAMETROS SP " + codemp + " " + where + " " + sidx + " " + sord + " " + inicio + " " + limite);
                ds = sp.EjecutarProcedimiento();
                //

                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    //Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.TiposReporte()
                        {

                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Idioma = Int16.Parse(ds.Tables[1].Rows[i]["Idioma"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["Id"].ToString()),
                            Codigo = ds.Tables[1].Rows[i]["Codigo"].ToString(),
                            Tipo = ds.Tables[1].Rows[i]["Tipo"].ToString(),
                            Nombre = ds.Tables[1].Rows[i]["Nombre"].ToString(),
                            Reporte = ds.Tables[1].Rows[i]["Reporte"].ToString(),
                            Agrupa = ds.Tables[1].Rows[i]["Agrupa"].ToString(),
                            ReportePadre = ds.Tables[1].Rows[i]["ReportePadre"].ToString(),
                            IdTiposReporte = Int16.Parse(ds.Tables[1].Rows[i]["IdTiposReporte"].ToString())

                        });
                    }
                }
                
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


        public List<dto.TiposReporte> ExportarExcel(int codemp, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposReporte> lstPeriodos = new List<dto.TiposReporte>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposReporte_Grilla");
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
                        lstPeriodos.Add(new dto.TiposReporte()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Idioma = Int16.Parse(ds.Tables[1].Rows[i]["Idioma"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["Id"].ToString()),
                            Codigo = ds.Tables[1].Rows[i]["Codigo"].ToString(),
                            Tipo = ds.Tables[1].Rows[i]["Tipo"].ToString(),
                            Nombre = ds.Tables[1].Rows[i]["Nombre"].ToString(),
                            Reporte = ds.Tables[1].Rows[i]["Reporte"].ToString(),
                            Agrupa = ds.Tables[1].Rows[i]["Agrupa"].ToString(),
                            ReportePadre = ds.Tables[1].Rows[i]["ReportePadre"].ToString(),
                            IdTiposReporte = Int16.Parse(ds.Tables[1].Rows[i]["IdTiposReporte"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPeriodos;
        }


        public void Insertar(dto.TiposReporte objAccion, int codemp)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Tipos_CpbtDoc_Report");
                Debug.WriteLine("DATOS INSERCION TIPOS REPORTE :" + objAccion.Agrupa + "-" + objAccion.Reporte + "-" + objAccion.Nombre + "-" + objAccion.ReportePadre);
                sp.AgregarParametro("trc_codemp", codemp);
                sp.AgregarParametro("trc_tpcid", objAccion.Agrupa);
                sp.AgregarParametro("trc_reporte", objAccion.Reporte);
                sp.AgregarParametro("trc_nombre", objAccion.Nombre);
                sp.AgregarParametro("trc_reppad", this.devuelveReportePadre(Int16.Parse(objAccion.ReportePadre.ToString())));

                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Borrar(int codemp, int? id, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Tipos_CpbtDoc_Report");
                sp.AgregarParametro("trc_codemp", codemp);
                
                //Debug.WriteLine("DATOS BORRAR TIPOS REPORTE--- ID : " + id );
                string _id = id.ToString();
                string id1 = _id.Substring(0,1);
                //Debug.WriteLine("DATOS BORRAR TIPOS REPORTE--- ID1 : " + id1);
                string id2 = _id.Substring(1);
                //Debug.WriteLine("DATOS BORRAR TIPOS REPORTE--- ID2 : " + id2);
                sp.AgregarParametro("trc_tpcid", id2);
                sp.AgregarParametro("trc_trcid", id1);
                
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.TiposReporte objAccion, int codemp, int id, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Tipos_CpbtDoc_Report");
                sp.AgregarParametro("trc_codemp", codemp);
                sp.AgregarParametro("trc_tpcid", id);
                sp.AgregarParametro("trc_trcid", objAccion.Idioma);
                sp.AgregarParametro("trc_reporte", objAccion.Reporte);
                sp.AgregarParametro("trc_nombre", objAccion.Nombre);
                sp.AgregarParametro("trc_reppad", this.devuelveReportePadre(Int16.Parse(objAccion.ReportePadre.ToString())));

                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ListarTiposReporteCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposReporte_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
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

        public List<dto.TiposComprobante> ListarTiposComprobante(int codemp, int idid)
        {
            List<dto.TiposComprobante> lst = new List<dto.TiposComprobante>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Desplegables_TiposComprobante");
                Debug.WriteLine("INICIA DESPLEGABLES TIPOS COMPROBANTE" + codemp + "-" + idid);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
                          
                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        lst.Add(new dto.TiposComprobante()
                        {

                            Id = Int16.Parse(ds.Tables[1].Rows[i]["Id"].ToString()),
                            Nombre = ds.Tables[1].Rows[i]["Nombre"].ToString()

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

        public string ListarTiposComprobante2(int codemp, int idid)
        {
            string salida = "";
            
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 11; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");
                    
                    sp.AgregarParametro("codigo", "TiCOMP"+i);
                    sp.AgregarParametro("idioma", idid);
                    ds = sp.EjecutarProcedimiento();
                    Debug.WriteLine("TAMAÑO DS " +ds.Tables.Count + "-" + idid);
                    if (i == 1)
                    {
                        salida += i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
          
                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string ListarTiposComprobante3(int codemp, int idioma)
        {
            string salida = ":" + "Seleccione";
            try
            {

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Lista_TipoComprobante");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                
                ds = sp.EjecutarProcedimiento();
                //Debug.WriteLine("TAMAÑO DS" + ds.Tables.Count);
                //salida += ";" + "" + ":" + "Seleccione";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StoredProcedure sp2 = new StoredProcedure("_Trae_Nombre_TipoComprobante");

                    sp2.AgregarParametro("codemp", codemp);
                    sp2.AgregarParametro("idioma", idioma);
                    sp2.AgregarParametro("id", ds.Tables[0].Rows[i][0].ToString());
                    ds2 = sp2.EjecutarProcedimiento();
                    Debug.WriteLine("SALIDA " + salida);
                    
                    salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds2.Tables[0].Rows[0][0].ToString();
                    /*if (salida.Equals("undefined"))
                    {
                        salida = "Seleccione";
                    }*/
                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }

        }

        public string ListarReportePadre(int codemp, int idid)
        {
            string salida = "";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 13; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");

                    sp.AgregarParametro("codigo", "RepPad" + i);
                    sp.AgregarParametro("idioma", idid);
                    ds = sp.EjecutarProcedimiento();
                    Debug.WriteLine("TAMAÑO REPORTE PADRE " + ds.Tables.Count + "-" + idid);
                    if (i == 1)
                    {
                        salida += i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        salida += ";" + i.ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                    }

                }
                return salida;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public int devulveCodigoPorCorrelativo(int val)
        {
            int salida = 0;
            if (val == 1)
            {
                salida = 32;
            }
            if (val == 2)
            {
                salida = 31;
            }
            if (val == 3)
            {
                salida = 34;
            }
            if (val == 4)
            {
                salida = 33;
            }
            if (val == 5)
            {
                salida = 71;
            }
            if (val == 6)
            {
                salida = 41;
            }
            if (val == 7)
            {
                salida = 75;
            }
            if (val == 8)
            {
                salida = 55;
            }
            if (val == 9)
            {
                salida = 42;
            }
            if (val == 10)
            {
                salida = 46;
            }
            return salida;
        }

        public string devuelveReportePadre(int val)
        {
            string salida = "";
            if (val == 1)
            {
                salida = "factura.rpt";
            }
            if (val == 2)
            {
                salida = "factura_comentario.rpt";
            }
            if (val == 3)
            {
                salida = "factura_x_producto.rpt";
            }
            if (val == 4)
            {
                salida = "informe_remesas.rpt";
            }
            if (val == 5)
            {
                salida = "nota_credito.rpt";
            }
            if (val == 6)
            {
                salida = "notacredito_comentario.rpt";
            }
            if (val == 7)
            {
                salida = "notacredito_x_producto.rpt";
            }
            if (val == 8)
            {
                salida = "orden_compra.rpt";
            }
            if (val == 9)
            {
                salida = "devolucion_documentos.rpt";
            }
            if (val == 10)
            {
                salida = "castigo_cliente_preContabilidad.rpt";
            }
            if (val == 11)
            {
                salida = "castigo_subdeuda_preContabilidad.rpt";
            }
            if (val == 12)
            {
                salida = "castigo_cliente_Contabilidad.rpt";
            }

            return salida;
        }


    }
}


