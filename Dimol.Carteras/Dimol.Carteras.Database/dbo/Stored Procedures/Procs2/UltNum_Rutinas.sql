

Create Procedure UltNum_Rutinas(@rtn_codemp integer) as
  SELECT IsNull(Max(rtn_rtnid)+1, 1) 
    FROM rutinas  
   WHERE ( rutinas.rtn_codemp = @rtn_codemp )
