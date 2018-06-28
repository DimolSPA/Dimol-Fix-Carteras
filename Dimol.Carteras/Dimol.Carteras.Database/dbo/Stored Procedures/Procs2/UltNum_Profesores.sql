

Create Procedure UltNum_Profesores(@prf_codemp integer) as
  SELECT IsNull(Max(prf_prfid)+1, 1)
    FROM profesores  
   WHERE ( prf_codemp = @prf_codemp )
