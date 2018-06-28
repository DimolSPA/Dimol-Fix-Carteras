

Create Procedure UltNum_Estados_Despachos(@edp_codemp integer) as
  SELECT IsNull(Max(edp_edpid)+1, 1) 
    FROM estados_despachos  
   WHERE ( estados_despachos.edp_codemp = @edp_codemp )
