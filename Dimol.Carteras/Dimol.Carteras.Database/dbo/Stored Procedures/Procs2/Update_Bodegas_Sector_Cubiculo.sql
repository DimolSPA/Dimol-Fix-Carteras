

Create Procedure Update_Bodegas_Sector_Cubiculo(@bsc_codemp integer, @bsc_bodid integer, @bsc_bdsid integer, @bsc_bscid varchar (10), 
																@bsc_cubicaje decimal (15,2), @bsc_posiciones smallint, 
															      @bsc_largo decimal(15,2), @bsc_ancho decimal(15,2), @bsc_alto decimal(15,2)) as   


  UPDATE bodegas_sector_cubiculo  
     SET  bsc_cubicaje = @bsc_cubicaje,   
         bsc_posiciones = @bsc_posiciones,   
         bsc_largo = @bsc_largo,
         bsc_ancho = @bsc_ancho,
         bsc_alto = @bsc_alto
   WHERE ( bodegas_sector_cubiculo.bsc_codemp = @bsc_codemp ) AND  
         ( bodegas_sector_cubiculo.bsc_bodid = @bsc_bodid ) AND  
         ( bodegas_sector_cubiculo.bsc_bdsid = @bsc_bdsid ) AND  
         ( bodegas_sector_cubiculo.bsc_bscid = @bsc_bscid )
