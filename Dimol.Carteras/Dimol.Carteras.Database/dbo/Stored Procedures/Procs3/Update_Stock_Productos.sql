

Create Procedure Update_Stock_Productos(@bsp_codemp integer, @bsp_bodid integer, @bsp_bdsid integer, @bsp_bscid varchar(10),
													 @bsp_posicion smallint, @bsp_prodid numeric(15), @bsp_stock decimal(15,2),
													 @bsp_merma decimal(15,2), @bsp_reservado decimal(15,2), @bsp_transito decimal(15,2)) as



  UPDATE productos_stock  
     SET pst_stock_total = pst_stock_total  +@bsp_stock,   
         pst_stock_reservado = pst_stock_reservado +@bsp_reservado,   
         pst_stock_transito = pst_stock_transito +@bsp_transito,   
         pst_stock_merma = pst_stock_merma + @bsp_merma  
   WHERE ( pst_codemp =  @bsp_codemp ) AND  
         ( pst_prodid = @bsp_prodid )   


  UPDATE bodegas_sector_cubiculo  
     SET bsc_stock = bsc_stock + @bsp_stock,   
         bsc_merma = bsc_merma  + @bsp_merma,   
         bsc_reservado = bsc_reservado + @bsp_reservado,   
         bsc_transito = bsc_transito + @bsp_transito  
   WHERE ( bodegas_sector_cubiculo.bsc_codemp = @bsp_codemp ) AND  
         ( bodegas_sector_cubiculo.bsc_bodid = @bsp_bodid ) AND  
         ( bodegas_sector_cubiculo.bsc_bdsid = @bsp_bdsid ) AND  
         ( bodegas_sector_cubiculo.bsc_bscid = @bsp_bscid )   
           

  UPDATE bodegas_sector_productos
     SET bsp_stock = bsp_stock + @bsp_stock,   
         bsp_merma = bsp_merma  + @bsp_merma,   
         bsp_reservado = bsp_reservado + @bsp_reservado,   
         bsp_transito = bsp_transito  + @bsp_transito  
   WHERE ( bsp_codemp = @bsp_codemp ) AND  
         ( bsp_bodid = @bsp_bodid ) AND  
         ( bsp_bdsid = @bsp_bdsid ) AND  
         ( bsp_bscid = bsp_bscid ) AND  
         ( bsp_posicion = @bsp_posicion ) AND  
         ( bsp_prodid = @bsp_prodid )
