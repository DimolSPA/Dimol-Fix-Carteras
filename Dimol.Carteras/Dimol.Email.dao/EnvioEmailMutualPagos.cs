using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;
using System.Data;
using Dimol.dao;
using Dimol.Email.dto;

namespace Dimol.Email.dao
{
    public class EnvioEmailMutualPagos
    {
        public static void TraeCuentaBancoEjecutivos(Dimol.Email.dto.DocumentoMutualPagos docEmailMutualPagos)
        {
            dto.CochaCpbt doc = new CochaCpbt();
            
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Datos_Ejecutivo_Mutual");
                sp.AgregarParametro("cuenta", Int32.Parse(docEmailMutualPagos.Cuenta));

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    docEmailMutualPagos.Cuenta = ds.Tables[0].Rows[0]["CUENTA"].ToString();
                    docEmailMutualPagos.Banco = ds.Tables[0].Rows[0]["BANCO"].ToString();
                    docEmailMutualPagos.Numero = ds.Tables[0].Rows[0]["EMAIL_DESTINO"].ToString();                                     
                }

                
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Email.dao.EnvioEmailMutualPagos.TraeCuentaBancoEjecutivos", Int32.Parse(docEmailMutualPagos.Cuenta));
                
            }
        }

        public static int InsertarBajasCpbtDoc(int pclid, int ctcid, int ccbid, dto.CochaCpbt doc, DateTime fechaPago, int usrid, int cuenta, int banco, string obs, int estid, DateTime fechaActual)
        {
            try
            {
                StoredProcedure spAc = new StoredProcedure("_Insert_Bajas_Cpbt_Doc");
                spAc.AgregarParametro("pclid", pclid);
                spAc.AgregarParametro("ctcid", ctcid);
                spAc.AgregarParametro("ccbid", ccbid);
                spAc.AgregarParametro("numero", doc.Numero);
                spAc.AgregarParametro("fecha_reclamo", fechaActual.AddDays(5 - (int)fechaActual.DayOfWeek));
                spAc.AgregarParametro("saldo", doc.Saldo);
                spAc.AgregarParametro("usrid", usrid);
                spAc.AgregarParametro("fecha_pago", fechaPago);
                spAc.AgregarParametro("tipo_banco", banco);
                spAc.AgregarParametro("id_cuenta", cuenta);
                spAc.AgregarParametro("observaciones", (string.IsNullOrEmpty(obs)) ? "":obs);
                spAc.AgregarParametro("codmon", (doc.TipoMoneda == "CLP") ? 1:3);
                spAc.AgregarParametro("codid", doc.CodigoCarga);
                spAc.AgregarParametro("estid", estid);
                spAc.AgregarParametro("fecha", fechaActual); 
                
                int error = spAc.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Email.dao.EnvioEmailMutualPagos.InsertarBajasCpbtDoc: " + pclid + "-" + ctcid + "-" + ccbid, usrid);
                throw ex;
            }

        }

        public static int ActualizarHistorialBajasCpbtDoc(int pclid, int ctcid, int ccbid, string historial, int usrid)
        {
            try
            {
                StoredProcedure spAc = new StoredProcedure("_Update_Historial_Bajas_Cpbt_Doc");
                spAc.AgregarParametro("pclid", pclid);
                spAc.AgregarParametro("ctcid", ctcid);
                spAc.AgregarParametro("ccbid", ccbid);
                spAc.AgregarParametro("historial", historial);                

                int error = spAc.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Email.dao.EnvioEmailMutualPagos.ActualizarHistorialBajasCpbtDoc: " + pclid + "-" + ctcid + "-" + ccbid, usrid);
                throw ex;
            }

        }

    }
}
