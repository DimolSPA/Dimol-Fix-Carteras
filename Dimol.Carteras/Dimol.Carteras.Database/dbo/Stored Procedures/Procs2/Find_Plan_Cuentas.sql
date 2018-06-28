

Create Procedure Find_Plan_Cuentas(@pct_codemp integer, @pct_pctid integer) as
  SELECT count(plan_cuentas.pct_pctid)  
    FROM plan_cuentas  
   WHERE ( plan_cuentas.pct_codemp = @pct_codemp ) AND  
         ( plan_cuentas.pct_pctid = @pct_pctid )
