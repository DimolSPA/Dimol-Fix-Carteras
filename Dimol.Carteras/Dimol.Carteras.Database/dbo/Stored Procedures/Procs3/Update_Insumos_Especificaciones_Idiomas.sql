

Create Procedure Update_Insumos_Especificaciones_Idiomas(@iei_codemp integer, @iei_insid numeric(15), @iei_iseid integer, @iei_idid integer,  @iei_descripcion text) as    
  UPDATE insumo_especificaciones_idiomas  
     SET iei_descripcion = @iei_descripcion  
   WHERE ( insumo_especificaciones_idiomas.iei_codemp = @iei_codemp ) AND  
         ( insumo_especificaciones_idiomas.iei_insid = @iei_insid ) AND  
         ( insumo_especificaciones_idiomas.iei_iseid = @iei_iseid ) AND  
         ( insumo_especificaciones_idiomas.iei_idid = @iei_idid )
