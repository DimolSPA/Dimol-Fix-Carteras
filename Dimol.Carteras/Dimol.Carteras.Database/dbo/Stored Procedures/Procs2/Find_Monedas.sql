

Create Procedure Find_Monedas(@mon_codemp integer, @mon_codmon integer) as
  SELECT count(monedas.mon_codmon  )
    FROM monedas  
   WHERE monedas.mon_codemp = @mon_codemp   and mon_codmon = @mon_codmon
