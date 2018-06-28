

Create Procedure Delete_Estados_Cartera(@ect_codemp integer, @ect_estid smallint) as  

  DELETE FROM estados_cartera_idiomas  
   WHERE ( estados_cartera_idiomas.eci_codemp = @ect_codemp ) AND  
         ( estados_cartera_idiomas.eci_estid = @ect_estid )   


  DELETE FROM estados_cartera  
   WHERE ( estados_cartera.ect_codemp = @ect_codemp ) AND  
         ( estados_cartera.ect_estid = @ect_estid )
