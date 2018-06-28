CREATE Procedure [dbo].[_Trae_Estados]      
(      
  @codemp int,                    
  @idioma int             
  )      
     
as        
         
    
BEGIN      
    SELECT	estados_cartera.ect_estid,   
             estados_cartera_idiomas.eci_nombre
               FROM estados_cartera,   
             estados_cartera_idiomas
             WHERE  estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp  and  
             estados_cartera_idiomas.eci_estid = estados_cartera.ect_estid  and  
             estados_cartera.ect_codemp = @codemp
             and estados_cartera.ect_prejud = 'J'
             and estados_cartera.ect_agrupa in (1,2,3)
             and eci_idid = @idioma
             order by eci_nombre
END
