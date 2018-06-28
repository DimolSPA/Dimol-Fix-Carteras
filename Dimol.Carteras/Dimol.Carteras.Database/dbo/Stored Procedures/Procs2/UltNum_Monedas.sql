

Create Procedure UltNum_Monedas(@mon_codemp integer) as
  SELECT IsNull(Max(monedas.mon_codmon)+1, 1)  
    FROM monedas  
   WHERE monedas.mon_codemp = @mon_codemp
