

Create Procedure Update_Caja_Compensacion(@cjc_codemp integer, @cjc_cjcid integer, @cjc_rut varchar (20), 
                                                                             @cjc_nombre varchar (150), @cjc_pctid integer) as  
  UPDATE caja_compensacion  
     SET cjc_rut = @cjc_rut,   
         cjc_nombre = @cjc_nombre,   
         cjc_pctid = @cjc_pctid  
   WHERE ( caja_compensacion.cjc_codemp = @cjc_codemp ) AND  
         ( caja_compensacion.cjc_cjcid = @cjc_cjcid )
