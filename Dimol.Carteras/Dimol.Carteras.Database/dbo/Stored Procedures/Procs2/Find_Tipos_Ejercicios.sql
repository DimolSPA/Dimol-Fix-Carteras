

Create Procedure Find_Tipos_Ejercicios(@tej_codemp integer, @tej_tejid integer) as
  SELECT count(tipos_ejercicios.tej_tejid)  
    FROM tipos_ejercicios  
   WHERE ( tipos_ejercicios.tej_codemp = @tej_codemp ) AND  
         ( tipos_ejercicios.tej_tejid = @tej_tejid )
