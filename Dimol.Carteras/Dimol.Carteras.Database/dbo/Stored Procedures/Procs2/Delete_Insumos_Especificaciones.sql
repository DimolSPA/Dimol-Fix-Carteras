

Create Procedure Delete_Insumos_Especificaciones(@iei_codemp integer, @iei_insid numeric(15), @iei_iseid integer, @iei_idid integer) as    
  DELETE FROM insumo_especificaciones_idiomas  
   WHERE ( insumo_especificaciones_idiomas.iei_codemp = @iei_codemp ) AND  
         ( insumo_especificaciones_idiomas.iei_insid = @iei_insid ) AND  
         ( insumo_especificaciones_idiomas.iei_iseid = @iei_iseid ) 

  DELETE FROM insumo_especificaciones  
   WHERE ( insumo_especificaciones.ise_codemp = @iei_codemp ) AND  
         ( insumo_especificaciones.ise_insid = @iei_insid ) AND  
         ( insumo_especificaciones.ise_iseid = @iei_iseid )
