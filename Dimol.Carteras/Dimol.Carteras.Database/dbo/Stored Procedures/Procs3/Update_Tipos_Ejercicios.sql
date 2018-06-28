

Create Procedure Update_Tipos_Ejercicios(@tej_codemp integer, @tej_tejid integer, @tej_nombre varchar(200), @tej_tipo smallint) as
   UPDATE tipos_ejercicios  
     SET tej_nombre = @tej_nombre,   
         tej_tipo = @tej_tipo  
   WHERE ( tipos_ejercicios.tej_codemp = @tej_codemp ) AND  
         ( tipos_ejercicios.tej_tejid = @tej_tejid )
