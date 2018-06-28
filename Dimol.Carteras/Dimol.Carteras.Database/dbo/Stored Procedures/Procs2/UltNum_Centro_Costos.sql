

Create Procedure UltNum_Centro_Costos(@ccs_codemp integer) as
  SELECT IsNull(Max(ccs_ccsid)+1, 1)
    FROM centro_costos  
   WHERE ( centro_costos.ccs_codemp = @ccs_codemp )
