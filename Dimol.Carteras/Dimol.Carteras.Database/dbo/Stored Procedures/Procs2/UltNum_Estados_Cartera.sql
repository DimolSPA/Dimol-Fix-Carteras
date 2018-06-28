

Create Procedure UltNum_Estados_Cartera(@ect_codemp integer) as
  SELECT IsNull(Max(ect_estid)+1, 1) 
    FROM estados_cartera  
   WHERE ( estados_cartera.ect_codemp = @ect_codemp )
