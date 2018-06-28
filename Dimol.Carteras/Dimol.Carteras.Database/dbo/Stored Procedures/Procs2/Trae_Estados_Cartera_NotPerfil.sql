

Create Procedure Trae_Estados_Cartera_NotPerfil(@pfe_codemp integer, @pfe_prfid integer, @ect_agrupa char(1), @eci_idid integer) as
  SELECT estados_cartera.ect_estid,   
         estados_cartera_idiomas.eci_nombre  
    FROM estados_cartera,   
         estados_cartera_idiomas  
   WHERE ( estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp ) and  
         ( estados_cartera_idiomas.eci_estid = estados_cartera.ect_estid ) and  
         ( ( estados_cartera.ect_codemp =@pfe_codemp ) AND  
         ( estados_cartera_idiomas.eci_idid = @eci_idid ) AND  
         ( estados_cartera.ect_agrupa = @ect_agrupa ) AND  
         ( estados_cartera.ect_prejud = 'P' ) AND  
         ( estados_cartera.ect_estid not in (  SELECT perfiles_estados.pfe_estid  
                                                 FROM perfiles_estados  
                                                WHERE ( perfiles_estados.pfe_codemp =@pfe_codemp ) AND  
                                                      ( perfiles_estados.pfe_prfid =@pfe_prfid )   
                                                       )) )   


union

 SELECT estados_cartera.ect_estid,   
         estados_cartera_idiomas.eci_nombre  
    FROM estados_cartera,   
         estados_cartera_idiomas  
   WHERE ( estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp ) and  
         ( estados_cartera_idiomas.eci_estid = estados_cartera.ect_estid ) and  
         ( ( estados_cartera.ect_codemp =@pfe_codemp ) AND  
         ( estados_cartera_idiomas.eci_idid = @eci_idid ) AND  
         ( estados_cartera.ect_agrupa = @ect_agrupa ) AND  
         ( estados_cartera.ect_prejud = 'J' ) AND  
         ( estados_cartera.ect_estid not in ( select mej_estid from materia_estados where mej_codemp = @pfe_codemp)) and
         ( estados_cartera.ect_estid not in (  SELECT perfiles_estados.pfe_estid  
                                                 FROM perfiles_estados  
                                                WHERE ( perfiles_estados.pfe_codemp =@pfe_codemp ) AND  
                                                      ( perfiles_estados.pfe_prfid =@pfe_prfid )   
                                                       )) )   

ORDER BY estados_cartera_idiomas.eci_nombre ASC
