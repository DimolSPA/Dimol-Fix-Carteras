

Create Procedure Update_Bodegas_Sector(@bds_codemp integer, @bds_bodid integer, @bds_bdsid integer, @bds_nombre varchar (200),
													@bds_bodegaje char (1), @bds_cubicaje decimal (15,2), @bds_columnas varchar (10), 
													@bds_filas integer, @bds_tipoalma smallint, @bds_largo decimal(15,2), @bds_ancho decimal(15,2), @bds_alto decimal(15,2)) as  
  UPDATE bodegas_sector  
     SET bds_codemp = @bds_codemp,   
         bds_bodid = @bds_bodid,   
         bds_bdsid = @bds_bdsid,   
         bds_nombre = @bds_nombre,   
         bds_bodegaje = @bds_bodegaje,   
         bds_cubicaje = @bds_cubicaje,   
         bds_columnas = @bds_columnas,   
         bds_filas = @bds_filas,   
         bds_tipoalma = @bds_tipoalma,  
         bds_largo = @bds_largo,
         bds_ancho = @bds_ancho,
         bds_alto = @bds_alto
   WHERE ( bodegas_sector.bds_codemp = @bds_codemp ) AND  
         ( bodegas_sector.bds_bodid = @bds_bodid ) AND  
         ( bodegas_sector.bds_bdsid = @bds_bdsid )
