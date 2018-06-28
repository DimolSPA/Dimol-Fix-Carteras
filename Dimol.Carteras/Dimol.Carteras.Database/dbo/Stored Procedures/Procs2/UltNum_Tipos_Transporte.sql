

Create Procedure UltNum_Tipos_Transporte(@tpt_codemp integer) as
  SELECT IsNull(Max(tpt_tptid)+1, 1)
    FROM tipos_transporte  
   WHERE ( tipos_transporte.tpt_codemp = @tpt_codemp )
