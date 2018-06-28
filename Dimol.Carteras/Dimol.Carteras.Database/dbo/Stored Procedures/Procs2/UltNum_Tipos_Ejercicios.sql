

Create Procedure UltNum_Tipos_Ejercicios(@tej_codemp integer) as
  SELECT IsNull(Max(tej_tejid)+1, 1)  
    FROM tipos_ejercicios  
   WHERE ( tipos_ejercicios.tej_codemp = @tej_codemp )
