

Create Procedure Delete_Productos_Documentos(@pdc_codemp integer, @pdc_prodid numeric (15), @pdc_tpdid integer) as
  DELETE FROM productos_documentos  
   WHERE ( productos_documentos.pdc_codemp = @pdc_codemp ) AND  
         ( productos_documentos.pdc_prodid = @pdc_prodid ) AND  
         ( productos_documentos.pdc_tpdid = @pdc_tpdid )
