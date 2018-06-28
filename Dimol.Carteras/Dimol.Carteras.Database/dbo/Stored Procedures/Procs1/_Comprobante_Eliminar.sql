CREATE Procedure [dbo].[_Comprobante_Eliminar]( @codemp integer,  @tpcid integer, @numero numeric(15)) as


declare @count int=0

delete from DETALLE_COMPROBANTES_ROL 
where DCr_NUMERO = @numero 
and DCr_TPCID = @tpcid
and dcr_codemp = @codemp

delete FROM [CABACERA_COMPROBANTES_ESTADOS] 
where CBe_NUMERO = @numero
and CBe_TPCID   = @tpcid
and CBe_codemp = @codemp

delete FROM [CABACERA_COMPROBANTES_OP] 
where CBo_NUMERO =@numero
and CBo_TPCID = @tpcid
and CBO_CODEMP = @codemp

delete from DETALLE_COMPROBANTES 
where DCC_NUMERO =@numero
and dcc_TPCID = @tpcid
and dcc_CODEMP = @codemp

--delete FROM ASIENTOS_CONTABLES_CPBTDOC_APL
--where  ADA_NUMAPL= @numero
--and ADA_TPCID = @tpcid
--and ADA_CODEMP = @codemp

delete from CABACERA_COMPROBANTES
where CBC_NUMERO =@numero
and CBc_TPCID = @tpcid
and CBc_CODEMP = @codemp
