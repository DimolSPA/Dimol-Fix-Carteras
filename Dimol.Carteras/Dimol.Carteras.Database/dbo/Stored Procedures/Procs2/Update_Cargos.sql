

Create Procedure Update_Cargos(@car_codemp integer, @car_carid integer, @car_nombre varchar (50)) as  
  UPDATE cargos  
     SET car_nombre = @car_nombre  
   WHERE ( cargos.car_codemp = @car_codemp ) AND  
         ( cargos.car_carid = @car_carid )
