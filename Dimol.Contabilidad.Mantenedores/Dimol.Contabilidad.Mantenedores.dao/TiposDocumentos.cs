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
    public class TiposDocumentos
    {
        public List<dto.TiposDocumentos> ListarGrilla(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposDocumentos> lstPeriodos = new List<dto.TiposDocumentos>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposDocumentos_Grilla");
                //Debug.WriteLine("INICIA SP" + sp.NombreProcedimiento);
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                Debug.WriteLine("PARAMETROS SP " + codemp + " " + where + " " + sidx + " " + sord + " " + inicio + " " + limite);
                ds = sp.EjecutarProcedimiento();
                //

                //Debug.WriteLine("NRO DATOS" + ds.Tables.Count);
                if (ds.Tables.Count > 0)
                {
                    //Debug.WriteLine("HAY DATOS");
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //Debug.WriteLine("ENTRO AL FOR");
                        lstPeriodos.Add(new dto.TiposDocumentos()
                        {

                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["Id"].ToString()),
                            Codigo = ds.Tables[1].Rows[i]["Codigo"].ToString(),
                            CodigoNumero = ds.Tables[1].Rows[i]["CodigoNumero"].ToString(),
                            Tipo = ds.Tables[1].Rows[i]["Tipo"].ToString(),
                            Nombre = ds.Tables[1].Rows[i]["Nombre"].ToString(),
                            TipoComprobante = ds.Tables[1].Rows[i]["TipoComprobante"].ToString(),
                            Talonario = convertirTrueFalse(ds.Tables[1].Rows[i]["Talonario"].ToString()),
                            UltimoNumero = Int16.Parse(ds.Tables[1].Rows[i]["UltimoNumero"].ToString()),
                            LineasXReporte = Int16.Parse(ds.Tables[1].Rows[i]["LineasXReporte"].ToString()),
                            TipoDocDigital = ds.Tables[1].Rows[i]["TipoDocDigital"].ToString()

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

        public List<dto.TiposDocumentos> ExportarExcel(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.TiposDocumentos> lstPeriodos = new List<dto.TiposDocumentos>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposDocumentos_Grilla");
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
                        lstPeriodos.Add(new dto.TiposDocumentos()
                        {
                            Codemp = Int16.Parse(ds.Tables[1].Rows[i]["Codemp"].ToString()),
                            Id = Int16.Parse(ds.Tables[1].Rows[i]["Id"].ToString()),
                            Codigo = ds.Tables[1].Rows[i]["Codigo"].ToString(),
                            CodigoNumero = ds.Tables[1].Rows[i]["CodigoNumero"].ToString(),
                            Tipo = ds.Tables[1].Rows[i]["Tipo"].ToString(),
                            Nombre = ds.Tables[1].Rows[i]["Nombre"].ToString(),
                            TipoComprobante = ds.Tables[1].Rows[i]["TipoComprobante"].ToString(),
                            Talonario = convertirTrueFalse(ds.Tables[1].Rows[i]["Talonario"].ToString()),
                            UltimoNumero = Int16.Parse(ds.Tables[1].Rows[i]["UltimoNumero"].ToString()),
                            LineasXReporte = Int16.Parse(ds.Tables[1].Rows[i]["LineasXReporte"].ToString()),
                            TipoDocDigital = ds.Tables[1].Rows[i]["TipoDocDigital"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstPeriodos;
        }


        public void Insertar(dto.TiposDocumentos objAccion, int codemp, int idioma)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Insertar_Tipos_CpbtDoc");
                sp.AgregarParametro("tpc_codemp", codemp);
                sp.AgregarParametro("tci_tpcid", idioma);
                sp.AgregarParametro("tpc_clbid", objAccion.TipoComprobante);
                sp.AgregarParametro("tpc_nombre", objAccion.Nombre);
                sp.AgregarParametro("tpc_talonario", convertirTrueFalse(objAccion.Talonario));
                sp.AgregarParametro("tpc_ultnum", objAccion.UltimoNumero);
                sp.AgregarParametro("tpc_lineas", objAccion.LineasXReporte);
                sp.AgregarParametro("tpc_codigo", objAccion.CodigoNumero);
                sp.AgregarParametro("tpc_tipdig", objAccion.TipoDocDigital);
                Debug.WriteLine("INSERTAR TIPOS DOCUMENTO DATOS : " + codemp + idioma + objAccion.TipoComprobante + objAccion.Nombre + objAccion.Talonario + objAccion.UltimoNumero + objAccion.LineasXReporte + objAccion.CodigoNumero + objAccion.TipoDocDigital);

                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Borrar(int codemp, int? id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Tipos_CpbtDoc");
                sp.AgregarParametro("tpc_codemp", codemp);
                sp.AgregarParametro("tpc_tpcid", id);
                int error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(dto.TiposDocumentos objAccion, int codemp, int idioma, int id)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("Update_Tipos_CpbtDoc");
                sp.AgregarParametro("tpc_codemp", codemp);
                sp.AgregarParametro("tpc_tpcid", id);
                sp.AgregarParametro("tpc_clbid", objAccion.TipoComprobante);
                sp.AgregarParametro("tpc_nombre", objAccion.Nombre);
                sp.AgregarParametro("tpc_talonario", convertirTrueFalse(objAccion.Talonario));
                sp.AgregarParametro("tpc_ultnum", objAccion.UltimoNumero);
                sp.AgregarParametro("tpc_lineas", objAccion.LineasXReporte);
                sp.AgregarParametro("tpc_codigo", objAccion.CodigoNumero);
                sp.AgregarParametro("tpc_tipdig", objAccion.TipoDocDigital);

                int error = sp.EjecutarProcedimientoTrans();

                StoredProcedure sp2 = new StoredProcedure("Update_Tipos_CpbtDoc_Idiomas");
                sp2.AgregarParametro("tci_codemp", codemp);
                sp2.AgregarParametro("tci_tpcid", id);
                sp2.AgregarParametro("tci_idid", idioma);
                sp2.AgregarParametro("tci_nombre", objAccion.Nombre);

                int err = sp2.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ListarTiposDocumentosCount(int codemp, int idid, string where, string sidx, string sord, int inicio, int limite)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_TiposDocumentos_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idid);
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

        public string ListarTipoComprobante(int codemp)
        {
            string salida = ":" + "Seleccione";
            try
            {

                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Lista_TiposComprobante");
                sp.AgregarParametro("codemp", codemp);

                ds = sp.EjecutarProcedimiento();
                Debug.WriteLine("TAMAÑO DS" + ds.Tables.Count);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    StoredProcedure sp2 = new StoredProcedure("_Trae_Nombre_TiposComprobante");

                    sp2.AgregarParametro("codemp", codemp);
                    sp2.AgregarParametro("id", ds.Tables[0].Rows[i][0].ToString());
                    ds2 = sp2.EjecutarProcedimiento();

                    salida += ";" + ds.Tables[0].Rows[i][0].ToString() + ":" + ds2.Tables[0].Rows[0][0].ToString();

                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }

        }

        public string ListarTipoDocCaja(int codemp, int idid)
        {
            string salida = "";

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
                for (int i = 1; i < 5; i++)
                {
                    sp = new StoredProcedure("Trae_Etiquetas");

                    sp.AgregarParametro("codigo", "TiDig" + i);
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

    }
}


