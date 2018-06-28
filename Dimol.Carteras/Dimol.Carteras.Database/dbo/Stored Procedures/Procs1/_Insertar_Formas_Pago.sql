CREATE Procedure [dbo].[_Insertar_Formas_Pago](@frp_codemp integer, @frp_idioma integer, @frp_nombre varchar(800), @frp_diasvenc integer, @frp_fecesp char(1), @frp_cuotas char(1), @frp_tipcpbt integer) as
declare @frpid int

set @frpid = (select IsNull(Max(frp_frpid)+1, 1) from formas_pago where frp_codemp = @frp_codemp)

  INSERT INTO formas_pago  
         ( frp_codemp,   
           frp_frpid,   
           frp_nombre,   
           frp_diasvenc,
		   frp_fecesp,
		   frp_cuotas,
		   frp_tipcpbt )  
  VALUES ( @frp_codemp,
           @frpid,
		   @frp_nombre,
           @frp_diasvenc,   
           @frp_fecesp,
		   @frp_cuotas,
		   @frp_tipcpbt )
  
  INSERT INTO formas_pago_idiomas  
         ( fpi_codemp,   
           fpi_frpid,   
           fpi_idid,   
           fpi_nombre )  
  VALUES ( @frp_codemp,
		   @frpid,
           @frp_idioma,   
           @frp_nombre )
