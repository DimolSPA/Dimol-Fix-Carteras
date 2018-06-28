

Create Procedure Delete_Productos_Documentos_Idiomas(@pdi_codemp integer, @pdi_prodid numeric (15), @pdi_tpdid integer, @pdi_idid integer) as
  DELETE FROM productos_documentos_idiomas  
   WHERE ( productos_documentos_idiomas.pdi_codemp = @pdi_codemp ) AND  
         ( productos_documentos_idiomas.pdi_prodid = @pdi_prodid ) AND  
         ( productos_documentos_idiomas.pdi_tpdid = @pdi_tpdid ) AND  
         ( productos_documentos_idiomas.pdi_idid = @pdi_idid )
