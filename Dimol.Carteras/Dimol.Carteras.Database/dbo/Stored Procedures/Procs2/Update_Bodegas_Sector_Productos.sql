

Create Procedure Update_Bodegas_Sector_Productos(@bsp_codemp integer, @bsp_bodid integer, @bsp_bdsid integer, @bsp_prodid numeric (15), 
																	@bsp_posicion smallint, @bsp_stock decimal (15,2), @bsp_merma decimal (15,2),
																	@bsp_reservado decimal (15,2), @bsp_transito decimal (15,2), @bsp_bscid varchar (10)) as  
  UPDATE bodegas_sector_productos  
     SET bsp_codemp = @bsp_codemp,   
         bsp_bodid = @bsp_bodid,   
         bsp_bdsid = @bsp_bdsid,   
         bsp_prodid = @bsp_prodid,   
         bsp_posicion = @bsp_posicion,   
         bsp_stock = @bsp_stock,   
         bsp_merma = @bsp_merma,   
         bsp_reservado = @bsp_reservado,   
         bsp_transito = @bsp_transito,   
         bsp_bscid = @bsp_bscid  
   WHERE ( bodegas_sector_productos.bsp_codemp = @bsp_codemp ) AND  
         ( bodegas_sector_productos.bsp_bodid = @bsp_bodid ) AND  
         ( bodegas_sector_productos.bsp_bdsid = @bsp_bdsid ) AND  
         ( bodegas_sector_productos.bsp_prodid = @bsp_prodid ) AND  
         ( bodegas_sector_productos.bsp_posicion = @bsp_posicion )
