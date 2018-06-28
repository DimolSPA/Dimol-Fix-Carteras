using Dimol.Carteras.dto;
using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dimol.Carteras.dao
{
    public class Aplicaciones
    {
        public static int InsertarAplicaciones(dto.Aplicaciones obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Aplicaciones");
                sp.AgregarParametro("apl_codemp", obj.CodigoEmpresa);
                    sp.AgregarParametro("apl_sucid", obj.CodigoSucursal);
                    sp.AgregarParametro("apl_anio", obj.Anio);
                    sp.AgregarParametro("apl_mes", obj.Mes);
                    sp.AgregarParametro("apl_tipo", obj.Tipo);
                    sp.AgregarParametro("apl_fecing", (object)obj.FechaIngreso ?? DBNull.Value);
                    sp.AgregarParametro("apl_fecapl", (object)obj.FechaAplicacion ?? DBNull.Value);
                    sp.AgregarParametro("apl_accion", obj.Accion);
                    sp.AgregarParametro("apl_usrid", obj.IdUsuario);


                ds = sp.EjecutarProcedimiento(); ;

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
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

        public static int InsertarAplicacionesItems(dto.AplicaionesItems obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Aplicaciones_Items");
                sp.AgregarParametro("api_codemp", obj.CodigoEmpresa);
                sp.AgregarParametro("api_sucid", obj.CodigoSucursal);
                sp.AgregarParametro("api_anio", obj.Anio);
                sp.AgregarParametro("api_numapl", obj.NumeroAplicacion);
                sp.AgregarParametro("api_item", obj.Item);
                sp.AgregarParametro("api_aniodoc", (object)obj.AnioDocumento ?? DBNull.Value);
                sp.AgregarParametro("api_numdoc", (object)obj.NumeroDocumento ?? DBNull.Value);
                sp.AgregarParametro("api_numdoc2", (object)obj.NumeroDocumento2 ?? DBNull.Value);
                sp.AgregarParametro("api_aniodoc2", (object)obj.AnioDocumento2 ?? DBNull.Value);
                sp.AgregarParametro("api_tpcid", (object)obj.TipoDocumento ?? DBNull.Value);
                sp.AgregarParametro("api_numero", (object)obj.Numero ?? DBNull.Value);
                sp.AgregarParametro("api_tpcid2", (object)obj.TipoDocumento2 ?? DBNull.Value);
                sp.AgregarParametro("api_numero2", (object)obj.Numero2 ?? DBNull.Value);
                sp.AgregarParametro("api_pclid", (object)obj.IdCliente ?? DBNull.Value);
                sp.AgregarParametro("api_ctcid", (object)obj.IdDeudor ?? DBNull.Value);
                sp.AgregarParametro("api_ccbid", (object)obj.Ccbid ?? DBNull.Value);
                sp.AgregarParametro("api_capital", obj.Capital);
                sp.AgregarParametro("api_interes", obj.Interes);
                sp.AgregarParametro("api_honorario", obj.Honorario);
                sp.AgregarParametro("api_gastpre", obj.GastoPrejudicial);
                sp.AgregarParametro("api_gastjud", obj.GastoJudicial);
                sp.AgregarParametro("api_gesid", obj.IdGestor == 0 ? DBNull.Value : (object)obj.IdGestor);
                sp.AgregarParametro("api_vdeid", (object)obj.IdVendedor ?? DBNull.Value);
                sp.AgregarParametro("api_remesa", obj.Remesa ? "S" : "N");

                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int BuscarIdGestor(int codemp,int sucid, int pclid, int ctcid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Id_Gestor");

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("sucid", sucid);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);

                int error = sp.EjecutarProcedimientoTrans();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
