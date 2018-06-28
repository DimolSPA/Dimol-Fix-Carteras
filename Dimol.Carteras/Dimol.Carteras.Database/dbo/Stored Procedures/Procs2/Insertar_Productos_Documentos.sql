

Create Procedure Insertar_Productos_Documentos(@pdc_codemp integer,  @pdc_prodid numeric (15), @pdc_tpdid integer, @pdc_orden smallint) as
  INSERT INTO productos_documentos  
         ( pdc_codemp,   
           pdc_prodid,   
           pdc_tpdid,   
           pdc_orden )  
  VALUES ( @pdc_codemp,   
           @pdc_prodid,   
           @pdc_tpdid,   
           @pdc_orden )
