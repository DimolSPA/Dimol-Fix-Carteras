

Create Procedure Update_Productos_Documentos_Idiomas(@pdi_codemp integer, @pdi_prodid numeric (15), @pdi_tpdid integer,
																		@pdi_idid integer, @pdi_documento bit) as
  UPDATE productos_documentos_idiomas  
     SET pdi_codemp = @pdi_codemp,   
         pdi_prodid = @pdi_prodid,   
         pdi_tpdid = @pdi_tpdid,   
         pdi_idid = @pdi_idid,   
         pdi_documento = @pdi_documento  
   WHERE ( productos_documentos_idiomas.pdi_codemp = @pdi_codemp ) AND  
         ( productos_documentos_idiomas.pdi_prodid = @pdi_prodid ) AND  
         ( productos_documentos_idiomas.pdi_tpdid = @pdi_tpdid ) AND  
         ( productos_documentos_idiomas.pdi_idid = @pdi_idid )
