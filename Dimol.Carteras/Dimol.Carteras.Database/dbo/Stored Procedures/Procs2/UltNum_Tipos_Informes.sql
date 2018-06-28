

Create procedure UltNum_Tipos_Informes(@tif_codemp integer) as
  SELECT  IsNull(Max(tif_tifid)+1, 1)
    FROM tipos_informes  
   WHERE ( tipos_informes.tif_codemp = @tif_codemp )
