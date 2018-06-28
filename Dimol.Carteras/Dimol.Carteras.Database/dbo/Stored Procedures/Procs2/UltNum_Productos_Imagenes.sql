

Create Procedure UltNum_Productos_Imagenes(@pdi_codemp integer, @pdi_prodid integer) as
  SELECT IsNull(Max(pdi_pdiid)+1, 1)
    FROM productos_imagenes  
   WHERE ( productos_imagenes.pdi_codemp = @pdi_codemp ) AND  
         ( productos_imagenes.pdi_prodid = @pdi_prodid )
