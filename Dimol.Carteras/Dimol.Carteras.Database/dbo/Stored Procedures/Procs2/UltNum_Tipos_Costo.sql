

Create Procedure UltNum_Tipos_Costo(@tco_codemp integer) as
  SELECT IsNull(Max(tco_tcoid)+1, 1) 
    FROM tipos_costo  
   WHERE ( tipos_costo.tco_codemp = @tco_codemp )
