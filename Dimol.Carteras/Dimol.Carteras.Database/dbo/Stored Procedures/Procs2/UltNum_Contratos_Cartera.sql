

Create Procedure UltNum_Contratos_Cartera(@cct_codemp integer) as
  SELECT IsNull(Max(cct_cctid)+1, 1) 
    FROM contratos_cartera  
   WHERE ( contratos_cartera.cct_codemp = @cct_codemp )
