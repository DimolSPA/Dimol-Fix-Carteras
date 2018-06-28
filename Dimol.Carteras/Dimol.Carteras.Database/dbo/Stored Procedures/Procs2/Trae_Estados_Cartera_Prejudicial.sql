

Create Procedure Trae_Estados_Cartera_Prejudicial(@ect_codemp integer, @ect_agrupa integer, @eci_idid integer, @ect_utiliza char(1)) as
  SELECT estados_cartera.ect_estid,   
         estados_cartera_idiomas.eci_nombre  
    FROM estados_cartera,   
         estados_cartera_idiomas  
   WHERE ( estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp ) and  
         ( estados_cartera_idiomas.eci_estid = estados_cartera.ect_estid ) and  
         ( ( estados_cartera.ect_codemp = @ect_codemp ) AND  
         ( estados_cartera.ect_agrupa > 1 ) AND  
         ( estados_cartera.ect_agrupa <= @ect_agrupa ) AND  
         ( estados_cartera_idiomas.eci_idid = @eci_idid ) AND  
         ( estados_cartera.ect_prejud = 'P' ) AND  
         ( estados_cartera.ect_utiliza = @ect_utiliza )   
         )   
order by eci_nombre
