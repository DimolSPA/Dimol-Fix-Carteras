

Create Procedure Insertar_Tipos_Ejercicios(@tej_codemp integer, @tej_tejid integer, @tej_nombre varchar(200), @tej_tipo smallint) as
  INSERT INTO tipos_ejercicios  
         ( tej_codemp,   
           tej_tejid,   
           tej_nombre,   
           tej_tipo )  
  VALUES ( @tej_codemp,   
           @tej_tejid,   
           @tej_nombre,   
           @tej_tipo )
