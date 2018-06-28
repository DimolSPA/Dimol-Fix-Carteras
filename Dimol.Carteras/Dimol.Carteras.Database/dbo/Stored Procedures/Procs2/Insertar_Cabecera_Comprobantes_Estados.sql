

Create Procedure Insertar_Cabecera_Comprobantes_Estados(@cbe_codemp integer, @cbe_sucid integer, @cbe_tpcid integer, @cbe_numero numeric (15),
																			@cbe_estado char (1), @cbe_fecha datetime, @cbe_usrid integer, @cbe_ippc varchar (20),
																			@cbe_ipred varchar (20), @cbe_comentario text) as
  INSERT INTO cabacera_comprobantes_estados  
         ( cbe_codemp,   
           cbe_sucid,   
           cbe_tpcid,   
           cbe_numero,   
           cbe_estado,   
           cbe_fecha,   
           cbe_usrid,   
           cbe_ippc,   
           cbe_ipred,   
           cbe_comentario )  
  VALUES ( @cbe_codemp,   
           @cbe_sucid,   
           @cbe_tpcid,   
           @cbe_numero,   
           @cbe_estado,   
           @cbe_fecha,   
           @cbe_usrid,   
           @cbe_ippc,   
           @cbe_ipred,   
           @cbe_comentario )
