

Create Procedure Update_Tipos_Asistencia(@tia_codemp integer, @tia_tipoid integer, @tia_nombre varchar(50), @tia_tipo smallint) as
  UPDATE tipos_asistencia  
     SET tia_nombre = @tia_nombre,   
         tia_tipo = @tia_tipo  
   WHERE ( tipos_asistencia.tia_codemp = @tia_codemp ) AND  
         ( tipos_asistencia.tia_tipoid = @tia_tipoid )
