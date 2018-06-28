

Create Procedure Update_Cabecera_Comprobantes_Estados(@cbe_codemp integer, @cbe_sucid integer, @cbe_tpcid integer, @cbe_numero numeric (15),
																			@cbe_estado char (1), @cbe_fecha datetime, @cbe_usrid integer, @cbe_ippc varchar (20),
																			@cbe_ipred varchar (20), @cbe_comentario text) as
  UPDATE cabacera_comprobantes_estados  
     SET cbe_codemp = @cbe_codemp,   
         cbe_sucid = @cbe_sucid,   
         cbe_tpcid = @cbe_tpcid,   
         cbe_numero = @cbe_numero,   
         cbe_estado = @cbe_estado,   
         cbe_fecha = @cbe_fecha,   
         cbe_usrid = @cbe_usrid,   
         cbe_ippc = @cbe_ippc,   
         cbe_ipred = @cbe_ipred,   
         cbe_comentario = @cbe_comentario  
   WHERE ( cabacera_comprobantes_estados.cbe_codemp = @cbe_codemp ) AND  
         ( cabacera_comprobantes_estados.cbe_sucid = @cbe_sucid ) AND  
         ( cabacera_comprobantes_estados.cbe_tpcid = @cbe_tpcid ) AND  
         ( cabacera_comprobantes_estados.cbe_numero = @cbe_numero ) AND  
         ( cabacera_comprobantes_estados.cbe_estado = @cbe_estado ) AND  
         ( cabacera_comprobantes_estados.cbe_fecha = @cbe_fecha )
