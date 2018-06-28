

Create Procedure UltNum_Producto_Area(@pta_codemp integer) as
  SELECT IsNull(Max(pta_ptaid)+1, 1)
    FROM producto_area  
   WHERE ( producto_area.pta_codemp = @pta_codemp )
