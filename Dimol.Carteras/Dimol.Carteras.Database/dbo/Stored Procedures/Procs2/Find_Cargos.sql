

Create Procedure Find_Cargos(@car_codemp integer, @car_carid integer) as
  SELECT count(cargos.car_carid)
    FROM cargos  
   WHERE cargos.car_codemp = @car_codemp   and car_carid = @car_carid
