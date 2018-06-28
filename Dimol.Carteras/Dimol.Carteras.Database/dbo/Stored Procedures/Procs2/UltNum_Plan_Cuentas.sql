

Create Procedure UltNum_Plan_Cuentas(@pct_codemp integer) as
  SELECT IsNull(Max(pct_pctid)+1, 1)
    FROM plan_cuentas  
   WHERE ( plan_cuentas.pct_codemp = @pct_codemp )
