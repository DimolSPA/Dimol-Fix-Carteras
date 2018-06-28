

Create Procedure UltNum_Region as
  SELECT IsNull(Max(region.reg_regid)+1, 1)
    FROM region
