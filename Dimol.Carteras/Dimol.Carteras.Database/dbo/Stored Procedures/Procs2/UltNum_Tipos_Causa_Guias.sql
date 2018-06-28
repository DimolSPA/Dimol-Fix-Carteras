

Create Procedure UltNum_Tipos_Causa_Guias(@tgd_codemp integer) as
   
  SELECT IsNull(Max(tgd_tgdid)+1, 1)
    FROM tipos_causa_guias  
   WHERE ( tipos_causa_guias.tgd_codemp = @tgd_codemp )
