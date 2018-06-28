

Create Procedure Update_Productos_Documentos(@pdc_codemp integer,  @pdc_prodid numeric (15), @pdc_tpdid integer, @pdc_orden smallint) as
  UPDATE productos_documentos  
     SET pdc_codemp = @pdc_codemp,   
         pdc_prodid = @pdc_prodid,   
         pdc_tpdid = @pdc_tpdid,   
         pdc_orden = @pdc_orden  
   WHERE ( productos_documentos.pdc_codemp = @pdc_codemp ) AND  
         ( productos_documentos.pdc_prodid = @pdc_prodid ) AND  
         ( productos_documentos.pdc_tpdid = @pdc_tpdid )
