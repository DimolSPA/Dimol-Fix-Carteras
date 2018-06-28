

Create Procedure Update_Despachos_Documentos_Estados(@dde_codemp integer, @dde_sucid integer, @dde_dpcid numeric (15),
																			@dde_tpcid integer, @dde_numero numeric (15), @dde_edpid integer,
																			@dde_fecha datetime, @dde_comentario text, @dde_usrid integer) as
  UPDATE despachos_documentos_estados  
     SET dde_codemp = @dde_sucid,   
         dde_sucid = @dde_sucid,   
         dde_dpcid = @dde_dpcid,   
         dde_tpcid = @dde_tpcid,   
         dde_numero = @dde_numero,   
         dde_edpid = @dde_edpid,   
         dde_fecha = @dde_fecha,   
         dde_comentario = @dde_comentario,   
         dde_usrid = @dde_usrid  
   WHERE ( despachos_documentos_estados.dde_codemp = @dde_codemp ) AND  
         ( despachos_documentos_estados.dde_sucid = @dde_sucid ) AND  
         ( despachos_documentos_estados.dde_dpcid = @dde_dpcid ) AND  
         ( despachos_documentos_estados.dde_tpcid = @dde_tpcid ) AND  
         ( despachos_documentos_estados.dde_numero = @dde_numero ) AND  
         ( despachos_documentos_estados.dde_edpid = @dde_edpid ) AND  
         ( despachos_documentos_estados.dde_fecha = @dde_fecha )
