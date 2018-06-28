

Create Procedure Delete_Productos_Imagenes(@pdi_codemp integer, @pdi_prodid numeric (15), @pdi_pdiid integer) as
  DELETE FROM productos_imagenes  
   WHERE ( productos_imagenes.pdi_codemp = @pdi_codemp ) AND  
         ( productos_imagenes.pdi_prodid = @pdi_prodid ) AND  
         ( productos_imagenes.pdi_pdiid = @pdi_pdiid )
