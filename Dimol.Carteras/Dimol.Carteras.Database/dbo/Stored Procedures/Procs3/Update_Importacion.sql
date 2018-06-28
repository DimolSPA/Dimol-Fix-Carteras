

Create Procedure Update_Importacion(@imp_codemp integer, @imp_impid integer, @imp_tptid integer, @imp_numero varchar (15),
												@imp_nombre varchar (200), @imp_pclid numeric (15),  
                                                @imp_fecarribo datetime, @imp_estado char(1)) as
  UPDATE importacion  
     SET   imp_tptid = @imp_tptid,   
         imp_numero = @imp_numero,   
         imp_nombre = @imp_nombre,   
         imp_pclid = @imp_pclid,   
         imp_fecarribo = @imp_fecarribo,
         imp_estado = @imp_estado  
   WHERE ( importacion.imp_codemp = @imp_codemp ) AND  
         ( importacion.imp_impid = @imp_impid )
