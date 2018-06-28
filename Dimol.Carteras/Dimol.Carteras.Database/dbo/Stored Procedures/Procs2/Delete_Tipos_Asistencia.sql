

Create Procedure Delete_Tipos_Asistencia(@tia_codemp integer, @tia_tipoid integer) as
  DELETE FROM tipos_asistencia_idiomas  
   WHERE ( tipos_asistencia_idiomas.tai_codemp = @tia_codemp ) AND  
         ( tipos_asistencia_idiomas.tai_tipoid = @tia_tipoid )   
           

  DELETE FROM tipos_asistencia  
   WHERE ( tipos_asistencia.tia_codemp = @tia_codemp ) AND  
         ( tipos_asistencia.tia_tipoid = @tia_tipoid )
