

Create Procedure UltNum_Tipos_Vehiculos(@tvc_codemp integer) as
  SELECT IsNull(Max(tvc_tvcid)+1, 1)
    FROM tipos_vehiculos  
   WHERE ( tipos_vehiculos.tvc_codemp = @tvc_codemp )
