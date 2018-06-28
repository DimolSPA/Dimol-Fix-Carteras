

Create Procedure UltNum_AFP(@afp_codemp integer) as
  SELECT IsNull(Max(afp.afp_afpid)+1, 1)
    FROM afp  
   WHERE ( afp.afp_codemp = @afp_codemp )
