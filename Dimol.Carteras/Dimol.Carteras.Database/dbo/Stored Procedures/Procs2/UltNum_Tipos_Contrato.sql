

Create Procedure UltNum_Tipos_Contrato(@tic_codemp integer) as
  SELECT IsNull(Max(tipos_contrato.tic_ticid)+1, 1)
    FROM tipos_contrato  
   WHERE ( tipos_contrato.tic_codemp = @tic_codemp )
