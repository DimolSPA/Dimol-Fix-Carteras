

Create Procedure Insertar_Insumos_Especificaciones_Idiomas(@iei_codemp integer, @iei_insid numeric(15), @iei_iseid integer, @iei_idid integer,  @iei_descripcion text) as    
  INSERT INTO insumo_especificaciones_idiomas  
         ( iei_codemp,   
           iei_insid,   
           iei_iseid,   
           iei_idid,   
           iei_descripcion )  
  VALUES ( @iei_codemp,   
           @iei_insid,   
           @iei_iseid,   
           @iei_idid,   
           @iei_descripcion )
