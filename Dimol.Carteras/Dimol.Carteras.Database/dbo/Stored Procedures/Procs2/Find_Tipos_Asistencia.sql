

Create Procedure Find_Tipos_Asistencia(@tia_codemp integer, @tia_tipoid integer) as
  SELECT count(tipos_asistencia.tia_tipoid)  
    FROM tipos_asistencia  
   WHERE ( tipos_asistencia.tia_codemp = @tia_codemp ) AND  
         ( tipos_asistencia.tia_tipoid = @tia_tipoid )
