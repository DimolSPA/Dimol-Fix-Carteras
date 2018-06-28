

Create Procedure Find_Estados_Cartera(@ect_codemp integer, @ect_estid integer) as
  SELECT count(estados_cartera.ect_estid  )
    FROM estados_cartera  
   WHERE ( estados_cartera.ect_codemp = @ect_codemp ) AND  
         ( estados_cartera.ect_estid = @ect_estid )
