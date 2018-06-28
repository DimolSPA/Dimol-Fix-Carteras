using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Word;

namespace Dimol.Reportes.dao
{
    public class CastigoJudicial
    {
        public static void TraeTitulo(dto.CastigoJudicialCliente obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Historial_Gestiones_Titulo");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("ctcid", DBNull.Value);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.Titulo = new dto.TituloReporte
                        {
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            RutDeudor = ds.Tables[0].Rows[i]["RutDeudor"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void TraeSucursalCliente(dto.CastigoJudicialCliente obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_ProvCli_Sucursal");
                sp.AgregarParametro("pcs_codemp", obj.Codemp);
                sp.AgregarParametro("pcs_pclid", obj.Pclid);
                sp.AgregarParametro("pcs_pcsid", obj.Pcsid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj.SucursalCliente = new dto.CabeceraSucursalCliente
                        {
                            Direccion = ds.Tables[0].Rows[i]["pcs_direccion"].ToString(),
                            Comuna = ds.Tables[0].Rows[i]["com_nombre"].ToString(),
                            Ciudad = ds.Tables[0].Rows[i]["ciu_nombre"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarCastigosDetalle(dto.CastigoJudicialCliente obj, List<dto.CastigoJudicialCliente> lstDevDocs)
        {
            try
            {
                List<dto.CastigoJudicialBruto> lstBruto = new List<dto.CastigoJudicialBruto>();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Castigo_Judicial");
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                sp.AgregarParametro("cbc_tpcid", obj.Tpcid);
                sp.AgregarParametro("cbc_desde", obj.CbcDesde);
                sp.AgregarParametro("cbc_hasta", obj.CbcHasta);
                sp.AgregarParametro("idi_idid", obj.Idioma);


                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    { 
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj.FechaComprobante = DateTime.Parse(dr["cbc_feccpbt"].ToString());
                            obj.RutUsuario = Dimol.bcp.Funciones.formatearRut(dr["pcl_rut"].ToString());
                            obj.NombreUsuario = dr["pcl_nomfant"].ToString();
                            obj.Pclid = Int32.Parse(dr["cbc_pclid"].ToString());
                            obj.Pcsid = Int32.Parse(dr["cbc_pcsid"].ToString());

                            lstBruto.Add(new dto.CastigoJudicialBruto
                            {
                                CtcRut = Dimol.bcp.Funciones.formatearRut(dr["ctc_numero"].ToString() + dr["ctc_digito"].ToString()),
                                CtcNombre = dr["ctc_nomfant"].ToString(),
                                TipoDoc = dr["tip_doc"].ToString(),
                                NroDoc = dr["ccb_numero"].ToString(),
                                Monto = dr["ccb_monto"].ToString(),
                                Saldo = dr["dcc_saldo"].ToString(),
                                FechaEmision = DateTime.Parse(dr["ccb_fecdoc"].ToString()),
                                FechaVcto = DateTime.Parse(dr["ccb_fecvenc"].ToString()),
                                Negocio = dr["ccb_numesp"].ToString(),
                                Moneda = dr["mon_nombre"].ToString(),
                                DetalleCtcId = Int32.Parse(dr["dcc_ctcid"].ToString()),
                                Ori = dr["ccb_docori"].ToString(),
                                Ant = dr["ccb_docant"].ToString(),
                                Direccion = dr["ctc_direccion"].ToString() + ", " + dr["ciudad_deudor"].ToString() + ", " + dr["comuna_deudor"].ToString(),
                                Tribunal = dr["trb_nombre"].ToString(),
                                Rol = dr["rol_numero"].ToString(),
                                CbcNumProvCli = Int32.Parse(dr["cbc_numprovcli"].ToString()),
                                NombreAsegurado = dr["sbc_nombre"].ToString()
                            });                        
                        }

                        TraeSucursalCliente(obj);

                        List<string> lstdeudores = lstBruto.Select(o => o.CtcRut).Distinct().ToList();
                        int ctcid = 0;
                        int numReg = 0;

                        foreach (string rut in lstdeudores)
                        {
                            dto.CastigoJudicialDeudor objDeu = new dto.CastigoJudicialDeudor();
                            
                            numReg++;         
                                          
                            foreach (var item in lstBruto.Where(o =>o.CtcRut == rut))
                            {
                                objDeu.lstDocs.Add(new dto.CastigoJudicialDetalle
                                {
                                    TipoDocumento = item.TipoDoc,
                                    NroDocumento = item.NroDoc,
                                    DeudorCtcId = item.DetalleCtcId,
                                    FechaEmision = item.FechaEmision,
                                    FechaVcto = item.FechaVcto,
                                    Moneda = item.Moneda,
                                    Monto = decimal.Parse(item.Monto),
                                    Saldo = decimal.Parse(item.Saldo),
                                    Negocio = item.Negocio,
                                    DocOrigen = item.Ori,
                                    DocCantidad = item.Ant
                                });
                                objDeu.NombreDeudor = item.CtcNombre;
                                ctcid = item.DetalleCtcId;
                                objDeu.Tribunal = item.Tribunal;
                                objDeu.Rol = item.Rol;
                                objDeu.Direccion = item.Direccion;
                                objDeu.CbcNumProvCli = item.CbcNumProvCli;
                                objDeu.NombreAsegurado = item.NombreAsegurado;
                            }
                            
                            objDeu.RutDeudor = Dimol.bcp.Funciones.formatearRut(rut);
                            
                            objDeu.Totales.Total = lstBruto.Where(o => o.CtcRut == rut).Select(o => decimal.Parse(o.Saldo)).Sum();
                            objDeu.Totales.Cantidad = lstBruto.Where(o => o.CtcRut == rut).Count();
                            
                            try
                            {
                                DataSet dst = new DataSet();
                                StoredProcedure spr = new StoredProcedure("Trae_Reporte_Cabecera_Comprobantes_Motivos");
                                spr.AgregarParametro("cbm_codemp", obj.Codemp);
                                spr.AgregarParametro("cbm_sucid", obj.Codsuc);
                                spr.AgregarParametro("cbm_tpcid", obj.Tpcid);
                                spr.AgregarParametro("cbm_numero", obj.CbcDesde);
                                spr.AgregarParametro("tmi_idid", obj.Idioma);
                                spr.AgregarParametro("cbm_ctcid", ctcid);
                                dst = spr.EjecutarProcedimiento();

                                if (dst.Tables.Count > 0)
                                {
                                    for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                                    {
                                        objDeu.lstMotivos.Add(new dto.CabeceraMotivoCastigo
                                        {
                                            Motivo = dst.Tables[0].Rows[i]["tmi_nombre"].ToString()
                                        });

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                           
                            lstDevDocs.Add(new dto.CastigoJudicialCliente
                            {
                                Deudor = objDeu,
                                FechaComprobante = obj.FechaComprobante,
                                RutUsuario = obj.RutUsuario,
                                NombreUsuario = obj.NombreUsuario,
                                Pclid = obj.Pclid,
                                Pcsid = obj.Pcsid,
                                NumRegistro = numReg,
                                RutAbogado = obj.RutAbogado,
                                NombreAbogado = obj.NombreAbogado,
                                Empresa = obj.Empresa,                                
                                Codemp = obj.Codemp,
                                Codsuc = obj.Codsuc,
                                Tpcid = obj.Tpcid,
                                CbcDesde = obj.CbcDesde,
                                CbcHasta = obj.CbcHasta,
                                Idioma = obj.Idioma,
                                FechaReporte = obj.FechaReporte,
                                Encabezado = obj.Encabezado,
                                Titulo = obj.Titulo,
                                SucursalCliente = obj.SucursalCliente
                            });

                        }                                                
                       
                    }
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarCastigosDetalleManual(dto.CastigoJudicialCliente obj, List<dto.CastigoJudicialCliente> lstDevDocs)
        {
            try
            {
                List<dto.CastigoJudicialBruto> lstBruto = new List<dto.CastigoJudicialBruto>();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Castigo_Judicial_Manual");
                /*sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                sp.AgregarParametro("cbc_tpcid", obj.Tpcid);
                sp.AgregarParametro("cbc_desde", obj.CbcDesde);
                sp.AgregarParametro("cbc_hasta", obj.CbcHasta);
                sp.AgregarParametro("idi_idid", obj.Idioma);*/
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("ccb_pclid", 86);
                sp.AgregarParametro("ccb_estcpbt", "V");
                sp.AgregarParametro("pcc_codigo", "2");
                sp.AgregarParametro("idi_idid", 1);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj.FechaComprobante = DateTime.Now;//DateTime.Parse(dr["cbc_feccpbt"].ToString());
                            obj.RutUsuario = Dimol.bcp.Funciones.formatearRut(dr["pcl_rut"].ToString());
                            obj.NombreUsuario = dr["pcl_nomfant"].ToString();
                            obj.Pclid = Int32.Parse(dr["ccb_pclid"].ToString());
                            obj.Pcsid = 1; //Int32.Parse(dr["cbc_pcsid"].ToString());

                            lstBruto.Add(new dto.CastigoJudicialBruto
                            {
                                CtcRut = Dimol.bcp.Funciones.formatearRut(dr["ctc_numero"].ToString() + dr["ctc_digito"].ToString()),
                                CtcNombre = dr["ctc_nomfant"].ToString(),
                                TipoDoc = dr["tip_doc"].ToString(),
                                NroDoc = dr["ccb_numero"].ToString(),
                                Monto = dr["ccb_monto"].ToString(),
                                Saldo = dr["ccb_saldo"].ToString(),
                                FechaEmision = DateTime.Parse(dr["ccb_fecdoc"].ToString()),
                                FechaVcto = DateTime.Parse(dr["ccb_fecvenc"].ToString()),
                                Negocio = dr["ccb_numesp"].ToString(),
                                Moneda = dr["mon_nombre"].ToString(),
                                DetalleCtcId = Int32.Parse(dr["ccb_ctcid"].ToString()),
                                Ori = dr["ccb_docori"].ToString(),
                                Ant = dr["ccb_docant"].ToString(),
                                Direccion = dr["ctc_direccion"].ToString() + ", " + dr["ciudad_deudor"].ToString() + ", " + dr["comuna_deudor"].ToString(),
                                Tribunal = dr["trb_nombre"].ToString(),
                                Rol = dr["rol_numero"].ToString(),
                                CbcNumProvCli = 1804, //Int32.Parse(dr["cbc_numprovcli"].ToString()),
                                NombreAsegurado = dr["sbc_nombre"].ToString()
                            });
                        }

                        TraeSucursalCliente(obj);

                        List<string> lstdeudores = lstBruto.Select(o => o.CtcRut).Distinct().ToList();
                        int ctcid = 0;
                        int numReg = 0;

                        foreach (string rut in lstdeudores)
                        {
                            dto.CastigoJudicialDeudor objDeu = new dto.CastigoJudicialDeudor();

                            numReg++;

                            foreach (var item in lstBruto.Where(o => o.CtcRut == rut))
                            {
                                objDeu.lstDocs.Add(new dto.CastigoJudicialDetalle
                                {
                                    TipoDocumento = item.TipoDoc,
                                    NroDocumento = item.NroDoc,
                                    DeudorCtcId = item.DetalleCtcId,
                                    FechaEmision = item.FechaEmision,
                                    FechaVcto = item.FechaVcto,
                                    Moneda = item.Moneda,
                                    Monto = decimal.Parse(item.Monto),
                                    Saldo = decimal.Parse(item.Saldo),
                                    Negocio = item.Negocio,
                                    DocOrigen = item.Ori,
                                    DocCantidad = item.Ant
                                });
                                objDeu.NombreDeudor = item.CtcNombre;
                                ctcid = item.DetalleCtcId;
                                objDeu.Tribunal = item.Tribunal;
                                objDeu.Rol = item.Rol;
                                objDeu.Direccion = item.Direccion;
                                objDeu.CbcNumProvCli = item.CbcNumProvCli;
                                objDeu.NombreAsegurado = item.NombreAsegurado;
                            }

                            objDeu.RutDeudor = Dimol.bcp.Funciones.formatearRut(rut);

                            objDeu.Totales.Total = lstBruto.Where(o => o.CtcRut == rut).Select(o => decimal.Parse(o.Saldo)).Sum();
                            objDeu.Totales.Cantidad = lstBruto.Where(o => o.CtcRut == rut).Count();

                            try
                            {
                                DataSet dst = new DataSet();
                                StoredProcedure spr = new StoredProcedure("_Trae_Motivos_Castigos");
                                spr.AgregarParametro("rut", rut.Replace(".","").Replace("-",""));
                                /*StoredProcedure spr = new StoredProcedure("Trae_Reporte_Cabecera_Comprobantes_Motivos");

                                spr.AgregarParametro("cbm_codemp", obj.Codemp);
                                spr.AgregarParametro("cbm_sucid", obj.Codsuc);
                                spr.AgregarParametro("cbm_tpcid", obj.Tpcid);
                                spr.AgregarParametro("cbm_numero", obj.CbcDesde);
                                spr.AgregarParametro("tmi_idid", obj.Idioma);
                                spr.AgregarParametro("cbm_ctcid", ctcid);*/
                                dst = spr.EjecutarProcedimiento();

                                if (dst.Tables.Count > 0)
                                {
                                    for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
                                    {
                                        objDeu.lstMotivos.Add(new dto.CabeceraMotivoCastigo
                                        {
                                            //Motivo = dst.Tables[0].Rows[i]["tmi_nombre"].ToString()
                                            Motivo = dst.Tables[0].Rows[i]["motivo"].ToString()
                                        });

                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }


                            lstDevDocs.Add(new dto.CastigoJudicialCliente
                            {
                                Deudor = objDeu,
                                FechaComprobante = obj.FechaComprobante,
                                RutUsuario = obj.RutUsuario,
                                NombreUsuario = obj.NombreUsuario,
                                Pclid = obj.Pclid,
                                Pcsid = obj.Pcsid,
                                NumRegistro = numReg,
                                RutAbogado = obj.RutAbogado,
                                NombreAbogado = obj.NombreAbogado,
                                Empresa = obj.Empresa,
                                Codemp = obj.Codemp,
                                Codsuc = obj.Codsuc,
                                Tpcid = obj.Tpcid,
                                CbcDesde = obj.CbcDesde,
                                CbcHasta = obj.CbcHasta,
                                Idioma = obj.Idioma,
                                FechaReporte = obj.FechaReporte,
                                Encabezado = obj.Encabezado,
                                Titulo = obj.Titulo,
                                SucursalCliente = obj.SucursalCliente
                            });

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarCastigosAseguradoDetalle(dto.CastigoJudicialAsegurado obj)
        {
            try
            {
                List<dto.CastigoJudicialAseguradoBruto> lstBruto = new List<dto.CastigoJudicialAseguradoBruto>();

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Reporte_Castigo_Judicial_Asegurado");
                sp.AgregarParametro("cbc_codemp", obj.Codemp);
                sp.AgregarParametro("cbc_sucid", obj.Codsuc);
                sp.AgregarParametro("cbc_tpcid", obj.Tpcid);
                sp.AgregarParametro("ctc_rut", obj.RutDeudor);
                sp.AgregarParametro("sbc_rut", obj.RutAsegurado);
                sp.AgregarParametro("idi_idid", obj.Idioma);


                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            obj.NombreDeudor = dr["ctc_nomfant"].ToString();
                            obj.NombreAsegurado = dr["sbc_nombre"].ToString();

                            lstBruto.Add(new dto.CastigoJudicialAseguradoBruto
                            {                                
                                TipoCausa = dr["Tipo_Causa"].ToString(),
                                NroComprobante = dr["ccb_numero"].ToString(),
                                NombreJuzgado = dr["trb_nombre"].ToString(),
                                Rol = dr["rol_numero"].ToString(),
                                TipoDoc = dr["tip_doc"].ToString(),
                                Saldo = decimal.Parse(dr["dcc_saldo"].ToString()),
                                CodMoneda = Int32.Parse(dr["ccb_codmon"].ToString())
                            });
                        }

                        
                        List<string> lstTipCausa = lstBruto.Select(o => o.TipoCausa).Distinct().ToList();

                        Dimol.dto.Indicadores objInd = new Dimol.dto.Indicadores();
                        Funciones.TraeDolarUFHoy(obj.Codemp, objInd);

                        foreach (string tipoCausa in lstTipCausa)
                        {
                            dto.CastigoJudicialAseguradoDetalle objDetalle = new dto.CastigoJudicialAseguradoDetalle();

                            int numTipDoc = 0;
                            string cpbt ="";
                                                     
                            
                            foreach (var item in lstBruto.Where(o => o.TipoCausa == tipoCausa))
                            {
                                numTipDoc++;

                                cpbt = cpbt + item.NroComprobante + " - ";

                                objDetalle.Rol = item.Rol;
                                objDetalle.TipoDoc = item.TipoDoc;
                                objDetalle.TipoCausa = item.TipoCausa;
                                objDetalle.NombreJuzgado = item.NombreJuzgado;
                                objDetalle.CodMoneda = item.CodMoneda;
                            }

                            if (numTipDoc == 1)
                            {
                                objDetalle.Rol = objDetalle.Rol + ", el documento objeto de esta gestión ";
                                objDetalle.NroComprobante = "es: " + objDetalle.TipoDoc.ToLower() + " serie Nº " + cpbt.Substring(0, cpbt.Length - 3);
                            }
                            else
                            {
                                objDetalle.Rol = objDetalle.Rol + ", los documentos objetos de esta gestión ";
                                objDetalle.NroComprobante = "son: " + objDetalle.TipoDoc.ToLower() + "s series Nº " + cpbt.Substring(0, cpbt.Length - 3);
                            }

                            switch (objDetalle.CodMoneda)
                            {
                                case 1:
                                    objDetalle.SubTotalCastigo = lstBruto.Where(o => o.TipoCausa == tipoCausa).Select(o => decimal.Parse(o.Saldo.ToString())).Sum();
                                    break;
                                case 2:
                                    objDetalle.SubTotalCastigo = lstBruto.Where(o => o.TipoCausa == tipoCausa).Select(o => decimal.Parse(o.Saldo.ToString())).Sum() * objInd.UF;
                                    break;
                                case 3:
                                    objDetalle.SubTotalCastigo = lstBruto.Where(o => o.TipoCausa == tipoCausa).Select(o => decimal.Parse(o.Saldo.ToString())).Sum() * objInd.DolarObservado;
                                    break;
                                default:
                                    break;
                            }
                            
                                                                                                         
                            obj.lstDetalle.Add(objDetalle);

                        }
                        obj.TotalCastigo = lstBruto.Select(o => decimal.Parse(o.Saldo.ToString())).Sum();
                    }
                }

                ListarCastigosDoc(obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ListarCastigosDoc(dto.CastigoJudicialAsegurado obj)
        {   
            
            Application MSWord = new Application();
            Document Documento = new Document();
            
            /*if (File.Exists(Destino))
            {
                File.Delete(Destino);
            }*/

            File.Copy (obj.RutaOrigen, obj.RutaDestino);

            Documento = MSWord.Documents.Open(obj.RutaDestino);
            object oEndOfDoc = "Parrafos"; //"\\endofdoc";
            string parrafo;
            int numReg = obj.lstDetalle.Count();

            Documento.Bookmarks["Fecha_Aceptacion"].Range.Text = obj.FechaReporteStr;
            Documento.Bookmarks["Nombre_Asegurado"].Range.Text = obj.NombreAsegurado;
            Documento.Bookmarks["Comuna_Asegurado"].Range.Text = "Santiago";
            Documento.Bookmarks["Deudor"].Range.Text = obj.NombreDeudor + ", Rut " + Dimol.bcp.Funciones.formatearRut(obj.RutDeudor);
            Documento.Bookmarks["Deudor2"].Range.Text = obj.NombreDeudor + ", Rut " + Dimol.bcp.Funciones.formatearRut(obj.RutDeudor);
            Documento.Bookmarks["Asegurado"].Range.Text = obj.NombreAsegurado + ", Rut Nº " + Dimol.bcp.Funciones.formatearRut(obj.RutAsegurado);
            Documento.Bookmarks["Aseg_sin_Rut"].Range.Text = obj.NombreAsegurado;
            Documento.Bookmarks["Monto_Global"].Range.Text = obj.TotalCastigoStr;

            foreach (var item in obj.lstDetalle.OrderBy(o => o.SubTotalCastigo))
            {
                
                parrafo = "";

                object oRng = Documento.Bookmarks.get_Item(ref oEndOfDoc).Range;
                Documento.Bookmarks["Parrafos"].Range.ParagraphFormat.LeftIndent = 32;
                Documento.Bookmarks["Parrafos"].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;

                parrafo +=  numReg + ". ";
                parrafo += item.TipoCausa + " por la suma de $";
                parrafo += item.SubTotalCastigoStr + ".- (";
                parrafo += enletras(item.SubTotalCastigoStr) + ") ante el ";
                parrafo += item.NombreJuzgado.Trim() + ", en causa Rol Nº ";
                parrafo += item.Rol;
                parrafo += item.NroComprobante + "." + Convert.ToString((char)13);
                                
                Documento.Bookmarks["Parrafos"].Range.Text = parrafo;
                //Convert.ToString((char)9) + 

                Documento.Bookmarks.Add("Parrafos", oRng);
                numReg--;
            }

            MSWord.Visible = true;

        }

        public static string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }

    }
}
