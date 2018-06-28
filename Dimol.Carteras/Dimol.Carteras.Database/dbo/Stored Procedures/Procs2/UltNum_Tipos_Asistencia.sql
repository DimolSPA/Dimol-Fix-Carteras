

Create Procedure UltNum_Tipos_Asistencia(@tia_codemp integer) as
  SELECT IsNull(Max(tia_tipoid)+1, 1)
    FROM tipos_asistencia  
   WHERE ( tipos_asistencia.tia_codemp = @tia_codemp )
