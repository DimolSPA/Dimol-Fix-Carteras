

Create Procedure Insertar_Tipos_Ejercicios_Idiomas(@tji_codemp integer, @tji_tejid integer, @tji_idid integer, @tji_nombre varchar(200)) as
    INSERT INTO tipos_ejercicios_idiomas  
         ( tji_codemp,   
           tji_tejid,   
           tji_idid,   
           tji_nombre )  
  VALUES ( @tji_codemp,   
           @tji_tejid,   
           @tji_idid,   
           @tji_nombre )
