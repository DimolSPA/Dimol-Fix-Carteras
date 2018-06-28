

Create Procedure Find_Producto_Area(@pta_codemp integer, @pta_ptaid integer) as
  SELECT count(producto_area.pta_ptaid)  
    FROM producto_area  
   WHERE ( producto_area.pta_codemp = @pta_codemp ) AND  
         ( producto_area.pta_ptaid = @pta_ptaid )
