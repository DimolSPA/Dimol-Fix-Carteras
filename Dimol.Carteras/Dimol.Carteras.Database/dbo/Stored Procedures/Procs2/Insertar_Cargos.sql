

Create Procedure Insertar_Cargos(@car_codemp integer, @car_carid integer, @car_nombre varchar(50)) as
  INSERT INTO cargos  
         ( car_codemp,   
           car_carid,   
           car_nombre )  
  VALUES ( @car_codemp,   
           @car_carid,   
           @car_nombre )
