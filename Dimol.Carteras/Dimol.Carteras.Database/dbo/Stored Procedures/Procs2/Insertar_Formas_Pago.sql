

Create Procedure Insertar_Formas_Pago(@frp_codemp integer, @frp_frpid integer, @frp_nombre varchar(200), @frp_diasvenc smallint, @frp_fecesp char(1), @frp_cuotas char(1), @frp_tipcpbt integer) as
  INSERT INTO formas_pago  
         ( frp_codemp,   
           frp_frpid,   
           frp_nombre,   
           frp_diasvenc,   
           frp_fecesp,   
           frp_cuotas,
           frp_tipcpbt )  
  VALUES ( @frp_codemp,   
           @frp_frpid,   
           @frp_nombre,   
           @frp_diasvenc,   
           @frp_fecesp,   
           @frp_cuotas,
           @frp_tipcpbt )
