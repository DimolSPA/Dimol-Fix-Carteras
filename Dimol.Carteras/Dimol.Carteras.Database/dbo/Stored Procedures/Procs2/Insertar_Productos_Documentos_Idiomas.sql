

Create Procedure Insertar_Productos_Documentos_Idiomas(@pdi_codemp integer, @pdi_prodid numeric (15), @pdi_tpdid integer,
																		@pdi_idid integer, @pdi_documento varchar(300)) as
  INSERT INTO productos_documentos_idiomas  
         ( pdi_codemp,   
           pdi_prodid,   
           pdi_tpdid,   
           pdi_idid,   
           pdi_documento )  
  VALUES ( @pdi_codemp,   
           @pdi_prodid,   
           @pdi_tpdid,   
           @pdi_idid,   
           @pdi_documento )
