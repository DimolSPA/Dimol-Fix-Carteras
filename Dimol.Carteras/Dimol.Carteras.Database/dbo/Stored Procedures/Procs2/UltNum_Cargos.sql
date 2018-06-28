

Create Procedure UltNum_Cargos(@car_codemp integer) as
  SELECT IsNull(Max(cargos.car_carid)+1, 1) 
    FROM cargos  
   WHERE cargos.car_codemp = @car_codemp
