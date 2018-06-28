

Create Procedure Update_Formas_Pago(@frp_codemp integer, @frp_frpid integer, @frp_nombre varchar(200), @frp_diasvenc smallint, @frp_fecesp char(1), @frp_cuotas char(1), @frp_tipcpbt integer) as
   UPDATE formas_pago  
     SET frp_nombre = @frp_nombre,   
         frp_diasvenc = @frp_diasvenc,   
         frp_fecesp = @frp_fecesp,   
         frp_cuotas = @frp_cuotas,
         frp_tipcpbt = @frp_tipcpbt  
   WHERE ( formas_pago.frp_codemp = @frp_codemp ) AND  
         ( formas_pago.frp_frpid = @frp_frpid )
