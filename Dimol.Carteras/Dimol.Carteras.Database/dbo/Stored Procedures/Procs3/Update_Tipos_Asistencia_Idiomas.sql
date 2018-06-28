

Create Procedure Update_Tipos_Asistencia_Idiomas(@tai_codemp integer, @tai_tipoid integer, @tai_idid integer, @tai_nombre varchar(50)) as
  UPDATE tipos_asistencia_idiomas  
     SET tai_nombre = @tai_nombre  
   WHERE ( tipos_asistencia_idiomas.tai_codemp = @tai_codemp ) AND  
         ( tipos_asistencia_idiomas.tai_tipoid = @tai_tipoid ) AND  
         ( tipos_asistencia_idiomas.tai_idid = @tai_idid )
