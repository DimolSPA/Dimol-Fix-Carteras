

Create Procedure Update_Ejercicios_Idiomas(@eji_codemp integer, @eji_ejcid integer, @eji_idid integer, @eji_nombre varchar(200)) as
      UPDATE ejercicios_idiomas  
     SET eji_nombre = @eji_nombre  
   WHERE ( ejercicios_idiomas.eji_codemp = @eji_codemp ) AND  
         ( ejercicios_idiomas.eji_ejcid = @eji_ejcid ) AND  
         ( ejercicios_idiomas.eji_idid = @eji_idid )
