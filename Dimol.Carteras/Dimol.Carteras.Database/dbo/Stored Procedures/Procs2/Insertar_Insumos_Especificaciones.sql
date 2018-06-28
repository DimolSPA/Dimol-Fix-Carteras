

Create Procedure Insertar_Insumos_Especificaciones(@ise_codemp integer, @ise_insid numeric(15), @ise_iseid integer, @ise_tpdid integer) as
  INSERT INTO insumo_especificaciones  
         ( ise_codemp,   
           ise_insid,   
           ise_iseid,   
           ise_tpdid )  
  VALUES ( @ise_codemp,   
           @ise_insid,   
           @ise_iseid,   
           @ise_tpdid )
