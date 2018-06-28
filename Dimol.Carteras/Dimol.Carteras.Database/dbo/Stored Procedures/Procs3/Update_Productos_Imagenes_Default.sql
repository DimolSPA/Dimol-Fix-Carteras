

Create Procedure Update_Productos_Imagenes_Default(@pdi_codemp integer, @pdi_prodid integer, @pdi_pdiid integer, @pdi_default char(1)) as
  UPDATE productos_imagenes  
     SET pdi_default = @pdi_default
   WHERE ( productos_imagenes.pdi_codemp = @pdi_codemp ) AND  
         ( productos_imagenes.pdi_prodid = @pdi_prodid and pdi_pdiid = @pdi_pdiid )
