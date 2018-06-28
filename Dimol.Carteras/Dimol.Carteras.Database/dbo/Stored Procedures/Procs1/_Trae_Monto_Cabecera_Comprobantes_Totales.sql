create Procedure [dbo].[_Trae_Monto_Cabecera_Comprobantes_Totales](@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero numeric(15)) as
  

  
  select sum(dcc_neto) monto from detalle_comprobantes where dcc_codemp= @cbc_codemp
                and dcc_sucid=@cbc_sucid
                and dcc_tpcid=@cbc_tpcid
                and dcc_numero=@cbc_numero
