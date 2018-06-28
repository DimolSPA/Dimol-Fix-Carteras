

Create Procedure UltNum_Caja_Compensacion(@cjc_codemp integer) as
  SELECT IsNull(Max(caja_compensacion.cjc_cjcid)+1, 1) 
    FROM caja_compensacion  
   WHERE ( caja_compensacion.cjc_codemp = @cjc_codemp )
