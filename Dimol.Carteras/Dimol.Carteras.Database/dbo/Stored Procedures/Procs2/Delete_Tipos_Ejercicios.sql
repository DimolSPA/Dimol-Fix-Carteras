

Create Procedure Delete_Tipos_Ejercicios(@tej_codemp integer, @tej_tejid integer) as
   DELETE FROM tipos_ejercicios_idiomas  
   WHERE ( tipos_ejercicios_idiomas.tji_codemp = @tej_codemp ) AND  
         ( tipos_ejercicios_idiomas.tji_tejid = @tej_tejid )   
           

  DELETE FROM tipos_ejercicios  
   WHERE ( tipos_ejercicios.tej_codemp = @tej_codemp ) AND  
         ( tipos_ejercicios.tej_tejid = @tej_tejid )
