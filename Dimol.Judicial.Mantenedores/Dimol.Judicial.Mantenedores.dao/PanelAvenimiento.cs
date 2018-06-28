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
    public class PanelAvenimiento
    {
        public static int InsertarPanelAvenimiento(int codemp, int rolId, string rolNumero,int pclid, int ctcid, int tribunalId, int userId)
        {
            int result = -1;
            try
            {
                
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Panel_Traspasos_Avenimiento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolId", rolId);
                sp.AgregarParametro("rolNumero", rolNumero);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("trbId", tribunalId);
                sp.AgregarParametro("user", userId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    result = Int32.Parse(ds.Tables[0].Rows[0]["rolId"].ToString());
                }

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelAvenimiento.InsertarPanelAvenimiento", userId);
                return -1;
            }
            return result;
        }

        public static List<dto.PanelAvenimiento> ListarPanelAvenimientoGrilla(int codemp, string where, string sidx, string sord)
        {
            List<dto.PanelAvenimiento> lst = new List<dto.PanelAvenimiento>();
            DateTime fechaTraspasoAvenimiento = new DateTime();
           
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Traspasos_Avenimiento_Grilla");
                sp.AgregarParametro("codemp", codemp);
              
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["FechaTraspasoAvenimiento"].ToString(), out fechaTraspasoAvenimiento);

                        lst.Add(new dto.PanelAvenimiento()
                        {
                            RolId = Int32.Parse(ds.Tables[0].Rows[i]["ROLID"].ToString()),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            TribunalId = Int32.Parse(ds.Tables[0].Rows[i]["TribunalId"].ToString()),
                            Rol= ds.Tables[0].Rows[i]["ROL"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["Cliente"].ToString(),
                            Deudor = ds.Tables[0].Rows[i]["Deudor"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString(),
                            FechaTraspasoAvenimiento = fechaTraspasoAvenimiento == new DateTime() ? (DateTime?)null : fechaTraspasoAvenimiento,
                            Row = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString())

                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelAvenimiento.ListarPanelAvenimientoGrilla", 0);
                return lst;
            }
        }
        public static List<Dimol.Carteras.dto.Comprobante> ListarPanelAvenimientoNuevosDocumentos(int codemp, int pclid, int ctcid, int ccbid, string numDocumento,
                                                                                    string fechaCuota, int numCuotas, string montoCuota)
        {
            List<Dimol.Carteras.dto.Comprobante> lst = new List<Dimol.Carteras.dto.Comprobante>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Panel_Avenimiento_Nuevos_Documentos");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("numeroDocumento", numDocumento);
                sp.AgregarParametro("fechaCuota", string.IsNullOrEmpty(fechaCuota) ? DBNull.Value : (object)DateTime.Parse(fechaCuota));
                sp.AgregarParametro("numCuotas", numCuotas);
                sp.AgregarParametro("montoCuota", string.IsNullOrEmpty(montoCuota) ? DBNull.Value : (object)Decimal.Parse(montoCuota));
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.Carteras.dto.Comprobante()
                        {
                            Pclid = pclid,
                            Ctcid = ctcid,
                            CalculoHonorarios = "S",
                            TipoDocumento = ds.Tables[0].Rows[i]["Tpcid"].ToString(),
                            TipoCartera = Int32.Parse(ds.Tables[0].Rows[i]["TipCart"].ToString()),
                            NumeroCpbt = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            FechaDocumento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaDocumento"].ToString()),
                            EstadoCartera = ds.Tables[0].Rows[i]["Estid"].ToString(),
                            EstadoCpbt = ds.Tables[0].Rows[i]["Estcpbt"].ToString(),
                            CodigoMoneda = Int32.Parse(ds.Tables[0].Rows[i]["CodMon"].ToString()),
                            TipoCambio = decimal.Parse(ds.Tables[0].Rows[i]["TipCambio"].ToString()),
                            MontoAsignado = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            GastoJudicial = decimal.Parse(ds.Tables[0].Rows[i]["GastJud"].ToString()),
                            GastoOtros = decimal.Parse(ds.Tables[0].Rows[i]["GastOtro"].ToString()),
                            //NombreBanco = ds.Tables[0].Rows[i]["BcoId"].ToString(),
                            //RutGirador = ds.Tables[0].Rows[i]["RutGir"].ToString(),
                            //NombreGirador = ds.Tables[0].Rows[i]["NomGir"].ToString(),
                            MotivoCobranza = ds.Tables[0].Rows[i]["MtcId"].ToString(),
                            CodigoCarga = ds.Tables[0].Rows[i]["CodId"].ToString(),
                            //NumeroEspecial = ds.Tables[0].Rows[i]["NumEsp"].ToString(),
                            //NumeroAgrupa = ds.Tables[0].Rows[i]["NumAgrupa"].ToString(),
                            Contrato = Int32.Parse(ds.Tables[0].Rows[i]["Cctid"].ToString()),
                            Sbcid = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SBCID"].ToString()) ? 0: Int32.Parse(ds.Tables[0].Rows[i]["SBCID"].ToString()),
                            Originales = ds.Tables[0].Rows[i]["DocOri"].ToString(),
                            Antecedentes = ds.Tables[0].Rows[i]["DocAnt"].ToString(),
                            TerceroId = Int32.Parse(ds.Tables[0].Rows[i]["TERCEROID"].ToString()),
                            IdCuenta = ds.Tables[0].Rows[i]["IdCuenta"].ToString(),
                            DescripcionCuenta = ds.Tables[0].Rows[i]["DescCuenta"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.PanelAvenimiento.ListarPanelAvenimientoNuevosDocumentos", 0);
                return lst;
            }
        }
        public static int FinalizarDocumentoAvenimiento(int codemp, int pclid, int ctcid, int ccbid, decimal monto, string estcpbt, string comentario)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Panel_Avenimiento_Finalizar_Documento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("monto", monto);
                sp.AgregarParametro("estcpbt", estcpbt);
                sp.AgregarParametro("nuevo_estcpbt", "F");
                sp.AgregarParametro("comentario", comentario);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.Avenimiento.FinalizarDocumentoAvenimiento", 0);
                throw ex;
            }
        }
        public static int ActualizarEstatusPanelAvenimiento(int codemp,int rolid, int pclid, int ctcid, string nuevoestado)
        {
            try
            {
                StoredProcedure sp = new StoredProcedure("_Panel_Avenimiento_Actualizar_Estatus");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("nuevo_estado", nuevoestado);

                int error = sp.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.Mantenedores.dao.Avenimiento.ActualizarEstatusPanelAvenimiento", 0);
                throw ex;
            }
        }
    }
}
