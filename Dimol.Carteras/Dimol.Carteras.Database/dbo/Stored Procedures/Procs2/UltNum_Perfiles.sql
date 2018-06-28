

Create Procedure UltNum_Perfiles(@prf_codemp integer) as
   SELECT IsNull(Max(perfiles.prf_prfid)+1, 1)  
    FROM perfiles  
   WHERE perfiles.prf_codemp = @prf_codemp
