

Create Procedure UltNum_Marcas_Vehiculos(@mkv_codemp integer) as
  SELECT IsNull(Max(mkv_mkvid)+1, 1)
    FROM marcas_vehiculos  
   WHERE ( marcas_vehiculos.mkv_codemp = @mkv_codemp )
