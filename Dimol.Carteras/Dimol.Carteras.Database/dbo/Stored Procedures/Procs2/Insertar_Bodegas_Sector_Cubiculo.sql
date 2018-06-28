

Create Procedure Insertar_Bodegas_Sector_Cubiculo(@bsc_codemp integer, @bsc_bodid integer, @bsc_bdsid integer, @bsc_bscid varchar (10), 
																@bsc_cubicaje decimal (15,2), @bsc_posiciones smallint, 
															      @bsc_largo decimal(15,2), @bsc_ancho decimal(15,2), @bsc_alto decimal(15,2)) as  
  INSERT INTO bodegas_sector_cubiculo  
         ( bsc_codemp,   
           bsc_bodid,   
           bsc_bdsid,   
           bsc_bscid,   
           bsc_cubicaje,   
           bsc_posiciones,   
           bsc_stock,   
           bsc_merma,   
           bsc_reservado,   
           bsc_transito,
		bsc_largo,
		bsc_ancho,
		bsc_alto )  
  VALUES ( @bsc_codemp,   
           @bsc_bodid,   
           @bsc_bdsid,   
           @bsc_bscid,   
           @bsc_cubicaje,   
           @bsc_posiciones,   
           0,   
           0,   
           0,   
           0,
           @bsc_largo,
           @bsc_ancho,
           @bsc_alto )
