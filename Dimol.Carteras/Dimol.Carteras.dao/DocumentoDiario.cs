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
    public class DocumentoDiario
    {
        public static int InsertarDocumentoDiario(dto.DocumentoDiario obj)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Documentos_Diarios");
                 sp.AgregarParametro("ddi_codemp", obj.CodigoEmpresa);
                sp.AgregarParametro("ddi_sucid", obj.CodigoSucursal);
                sp.AgregarParametro("ddi_anio", obj.Anio);
                //sp.AgregarParametro("ddi_numdoc", obj.NumeroDocumento);
                sp.AgregarParametro("ddi_tpcid", obj.TipoDocumento);
                sp.AgregarParametro("ddi_tipmov", obj.TipoMovimiento);
                sp.AgregarParametro("ddi_pclid", (object)obj.IdCliente ?? DBNull.Value);
                sp.AgregarParametro("ddi_propio", obj.Propio);
                sp.AgregarParametro("ddi_bcoid", (object)obj.IdBanco ??DBNull.Value);
                sp.AgregarParametro("ddi_ctacte",obj.CuentaCorriente);
                sp.AgregarParametro("ddi_fecing", obj.FechaIngreso);
                sp.AgregarParametro("ddi_fecdoc", obj.FechaDocumento);
                sp.AgregarParametro("ddi_fecvenc", obj.FechaVencimiento);
                sp.AgregarParametro("ddi_edcid", obj.EstadoDocumento);
                sp.AgregarParametro("ddi_numcta",obj.NumeroCuenta);
                sp.AgregarParametro("ddi_monto", obj.Monto);
                sp.AgregarParametro("ddi_saldo", obj.Saldo);
                sp.AgregarParametro("ddi_codmon", obj.CodigoMoneda);
                sp.AgregarParametro("ddi_tipcambio", obj.TipoCambio);
                sp.AgregarParametro("ddi_titular", obj.Titular);
                sp.AgregarParametro("ddi_rutpag", (object)obj.RutPagador ??DBNull.Value);
                sp.AgregarParametro("ddi_nompag", (object)obj.NombrePagador ??DBNull.Value);
                sp.AgregarParametro("ddi_ctcid", (object)obj.IdDeudor ??DBNull.Value);
                sp.AgregarParametro("ddi_empleado", obj.Empleado);
                sp.AgregarParametro("ddi_emplid",(object)obj.IdEmpleado ?? DBNull.Value);
                sp.AgregarParametro("ddi_custodia", obj.Custodia);
                sp.AgregarParametro("ddi_docemp", obj.DocumentoEmpresa);
                sp.AgregarParametro("ddi_pagdir", obj.PagDir);
                sp.AgregarParametro("ddi_vecesdep", obj.VecesDepositado);
                sp.AgregarParametro("ddi_fechadep", (object)obj.FechaDeposito ??DBNull.Value);
                sp.AgregarParametro("ddi_depositar", obj.Depositar? 'S': 'N');
                sp.AgregarParametro("ddi_rutdep", (object)obj.RutDeposito ?? DBNull.Value);
                sp.AgregarParametro("ddi_nomdep", (object)obj.NombreDeposito ??DBNull.Value);
                sp.AgregarParametro("ddi_nrodep", (object)obj.NumeroDeposito ??DBNull.Value);
                sp.AgregarParametro("ddi_fecdep", (object)obj.FecDep ??DBNull.Value);
                sp.AgregarParametro("ddi_pendiente", obj.Pendiente ? 'S' : 'N');
                sp.AgregarParametro("ddi_anioneg", (object)obj.AnioNegociacion ??DBNull.Value);
                sp.AgregarParametro("ddi_negid", (object)obj.IdNegociacion ??DBNull.Value);
                sp.AgregarParametro("ddi_comentario", (object)obj.Comentario ?? "");


                ds = sp.EjecutarProcedimiento();;

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

        public static int InsertarDocumentoDiarioEstados(dto.DocumentoDiario obj, int idUsuario)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Documentos_Diarios_Estados");
                sp.AgregarParametro("dde_codemp", obj.CodigoEmpresa);
                sp.AgregarParametro("dde_sucid", obj.CodigoSucursal);
                sp.AgregarParametro("dde_anio", obj.Anio);
                sp.AgregarParametro("dde_numdoc", obj.NumeroDocumento);
                sp.AgregarParametro("dde_edcid", obj.EstadoDocumento);
                sp.AgregarParametro("dde_usrid", idUsuario);
                sp.AgregarParametro("dde_comentario", (object)obj.Comentario ?? "");
                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        
    }
}
