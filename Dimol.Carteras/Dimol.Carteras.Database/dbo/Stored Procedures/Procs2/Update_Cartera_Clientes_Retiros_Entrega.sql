

Create Procedure Update_Cartera_Clientes_Retiros_Entrega(@cre_codemp integer, @cre_pclid numeric (15), @cre_ctcid numeric (15), @cre_ccbid integer, 
																			@cre_fecha datetime, @cre_treid integer, @cre_horini datetime, @cre_horfin datetime,
																			@cre_comid integer, @cre_direccion varchar (400), @cre_telefono varchar (80), @cre_contacto varchar (200),
																			@cre_comentario text, @cre_tipo char (1), @cre_copia char (1)) as
  UPDATE cartera_clientes_retiros_entrega  
     SET cre_treid = @cre_treid,   
         cre_horini = @cre_horini,   
         cre_horfin = @cre_horfin,   
         cre_comid = @cre_comid,   
         cre_direccion = @cre_direccion,   
         cre_telefono = @cre_telefono,   
         cre_contacto = @cre_contacto,   
         cre_comentario = @cre_comentario,   
         cre_tipo = @cre_tipo,   
         cre_copia = @cre_copia  
   WHERE ( cartera_clientes_retiros_entrega.cre_codemp = @cre_codemp ) AND  
         ( cartera_clientes_retiros_entrega.cre_pclid = @cre_pclid ) AND  
         ( cartera_clientes_retiros_entrega.cre_ctcid = @cre_ctcid ) AND  
         ( cartera_clientes_retiros_entrega.cre_ccbid = @cre_ccbid ) AND  
         ( cartera_clientes_retiros_entrega.cre_fecha = @cre_fecha )
