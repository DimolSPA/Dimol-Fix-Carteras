

Create Procedure Insertar_Ejercicios_Idiomas(@eji_codemp integer, @eji_ejcid integer, @eji_idid integer, @eji_nombre varchar(200)) as
     INSERT INTO ejercicios_idiomas  
         ( eji_codemp,   
           eji_ejcid,   
           eji_idid,   
           eji_nombre )  
  VALUES ( @eji_codemp,   
           @eji_ejcid,   
           @eji_idid,   
           @eji_nombre )
