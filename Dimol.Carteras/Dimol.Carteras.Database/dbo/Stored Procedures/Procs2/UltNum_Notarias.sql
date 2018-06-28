

Create Procedure UltNum_Notarias(@not_codemp integer) as
  SELECT IsNull(Max(not_notid)+1, 1) 
    FROM notarias  
   WHERE notarias.not_codemp = @not_codemp
