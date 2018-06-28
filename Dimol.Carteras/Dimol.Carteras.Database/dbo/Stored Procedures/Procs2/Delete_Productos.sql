

Create Procedure Delete_Productos(@pdt_codemp integer, @pdt_prodid numeric (15)) as

  DELETE FROM productos_stock  
   WHERE ( productos_stock.pst_codemp = @pdt_codemp ) AND  
         ( productos_stock.pst_prodid = @pdt_prodid ) 

  DELETE FROM productos_moneda  
   WHERE ( productos_moneda.pdm_codemp = @pdt_codemp ) AND  
         ( productos_moneda.pdm_prodid = @pdt_prodid )   

  DELETE FROM productos_impuestos  
   WHERE ( productos_impuestos.pdi_codemp = @pdt_codemp ) AND  
         ( productos_impuestos.pdi_prodid = @pdt_prodid ) 

  DELETE FROM productos_imagenes  
   WHERE ( productos_imagenes.pdi_codemp = @pdt_codemp ) AND  
         ( productos_imagenes.pdi_prodid = @pdt_prodid )   


  DELETE FROM productos_documentos_idiomas  
   WHERE ( productos_documentos_idiomas.pdi_codemp = @pdt_codemp ) AND  
         ( productos_documentos_idiomas.pdi_prodid = @pdt_prodid )   

  DELETE FROM productos_documentos  
   WHERE ( productos_documentos.pdc_codemp = @pdt_codemp ) AND  
         ( productos_documentos.pdc_prodid = @pdt_prodid )   


   DELETE FROM productos  
   WHERE ( productos.pdt_codemp = @pdt_codemp ) AND  
         ( productos.pdt_prodid = @pdt_prodid )  

  DELETE insumos  
    FROM insumos,   
         productos  
   WHERE ( productos.pdt_codemp = insumos.ins_codemp ) and  
         ( productos.pdt_insid = insumos.ins_insid ) and  
         ( ( productos.pdt_codemp = @pdt_codemp ) AND  
         ( productos.pdt_prodid = @pdt_prodid )   
         )
