

Create Procedure Insertar_Importacion(@imp_codemp integer, @imp_impid integer, @imp_tptid integer, @imp_numero varchar (15),
												@imp_nombre varchar (200), @imp_pclid numeric (15),  @imp_fecarribo datetime) as
  INSERT INTO importacion  
         ( imp_codemp,   
           imp_impid,   
           imp_tptid,   
           imp_numero,   
           imp_nombre,   
           imp_pclid,   
           imp_fecing,   
           imp_fecarribo )  
  VALUES ( @imp_codemp,   
           @imp_impid,   
           @imp_tptid,   
           @imp_numero,   
           @imp_nombre,   
           @imp_pclid,   
           getdate(),   
           @imp_fecarribo )
