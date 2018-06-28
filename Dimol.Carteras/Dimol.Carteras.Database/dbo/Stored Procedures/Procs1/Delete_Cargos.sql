

Create Procedure Delete_Cargos(@car_codemp integer, @car_carid integer) as  

 DELETE FROM cargos_idiomas  
   WHERE ( cargos_idiomas.cai_codemp = @car_codemp ) AND  
         ( cargos_idiomas.cai_carid = @car_carid ) 


  DELETE FROM cargos  
   WHERE ( cargos.car_codemp = @car_codemp ) AND  
         ( cargos.car_carid = @car_carid )
