

Create Procedure Insertar_Despachos_Documentos_Estados(@dde_codemp integer, @dde_sucid integer, @dde_dpcid numeric (15),
																			@dde_tpcid integer, @dde_numero numeric (15), @dde_edpid integer,
																			@dde_comentario text, @dde_usrid integer) as
  INSERT INTO despachos_documentos_estados  
         ( dde_codemp,   
           dde_sucid,   
           dde_dpcid,   
           dde_tpcid,   
           dde_numero,   
           dde_edpid,   
           dde_fecha,   
           dde_comentario,   
           dde_usrid )  
  VALUES ( @dde_codemp,   
           @dde_sucid,   
           @dde_dpcid,   
           @dde_tpcid,   
           @dde_numero,   
           @dde_edpid,   
            getdate(),   
           @dde_comentario,   
           @dde_usrid )
