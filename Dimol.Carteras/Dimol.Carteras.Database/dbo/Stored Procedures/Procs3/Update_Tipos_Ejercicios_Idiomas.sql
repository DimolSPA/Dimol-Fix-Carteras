

Create Procedure Update_Tipos_Ejercicios_Idiomas(@tji_codemp integer, @tji_tejid integer, @tji_idid integer, @tji_nombre varchar(200)) as
  UPDATE tipos_ejercicios_idiomas  
     SET tji_nombre = @tji_nombre  
   WHERE ( tipos_ejercicios_idiomas.tji_codemp = @tji_codemp ) AND  
         ( tipos_ejercicios_idiomas.tji_tejid = @tji_tejid ) AND  
         ( tipos_ejercicios_idiomas.tji_idid = @tji_idid )
