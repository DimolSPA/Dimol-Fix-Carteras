

Create Procedure Insertar_Bodegas_Sector_Productos(@bsp_codemp integer, @bsp_bodid integer, @bsp_bdsid integer, @bsp_prodid numeric (15), 
																	@bsp_posicion smallint, @bsp_bscid varchar(20)) as  
  INSERT INTO bodegas_sector_productos  
         ( bsp_codemp,   
           bsp_bodid,   
           bsp_bdsid,   
           bsp_prodid,   
           bsp_posicion,   
           bsp_stock,   
           bsp_merma,   
           bsp_reservado,   
           bsp_transito,   
           bsp_bscid )  
  VALUES ( @bsp_codemp,   
           @bsp_bodid,   
           @bsp_bdsid,   
           @bsp_prodid,   
           @bsp_posicion,   
           0,   
           0,   
           0,   
           0,   
           @bsp_bscid )
