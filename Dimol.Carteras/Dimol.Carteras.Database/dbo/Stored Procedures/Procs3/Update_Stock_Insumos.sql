

Create Procedure Update_Stock_Insumos(@bsi_codemp integer, @bsi_bodid integer, @bsi_bdsid integer, @bsi_bscid varchar(10),
													 @bsi_posicion smallint, @bsi_insid numeric(15), @bsi_stock decimal(15,2),
													 @bsi_merma decimal(15,2), @bsi_reservado decimal(15,2), @bsi_transito decimal(15,2)) as



  UPDATE insumos  
     SET ins_stock_total = ins_stock_total  +@bsi_stock,   
         ins_stock_reservado = ins_stock_reservado +@bsi_reservado,   
         ins_stock_transito = ins_stock_transito +@bsi_transito,   
         ins_stock_merma = ins_stock_merma + @bsi_merma  
   WHERE ( insumos.ins_codemp =  @bsi_codemp ) AND  
         ( insumos.ins_insid = @bsi_insid )   


  UPDATE bodegas_sector_cubiculo  
     SET bsc_stock = bsc_stock + @bsi_stock,   
         bsc_merma = bsc_merma  + @bsi_merma,   
         bsc_reservado = bsc_reservado + @bsi_reservado,   
         bsc_transito = bsc_transito + @bsi_transito  
   WHERE ( bodegas_sector_cubiculo.bsc_codemp = @bsi_codemp ) AND  
         ( bodegas_sector_cubiculo.bsc_bodid = @bsi_bodid ) AND  
         ( bodegas_sector_cubiculo.bsc_bdsid = @bsi_bdsid ) AND  
         ( bodegas_sector_cubiculo.bsc_bscid = @bsi_bscid )   
           

  UPDATE bodegas_sector_insumos  
     SET bsi_stock = bsi_stock + @bsi_stock,   
         bsi_merma = bsi_merma  + @bsi_merma,   
         bsi_reservado = bsi_reservado + @bsi_reservado,   
         bsi_transito = bsi_transito  + @bsi_transito  
   WHERE ( bodegas_sector_insumos.bsi_codemp = @bsi_codemp ) AND  
         ( bodegas_sector_insumos.bsi_bodid = @bsi_bodid ) AND  
         ( bodegas_sector_insumos.bsi_bdsid = @bsi_bdsid ) AND  
         ( bodegas_sector_insumos.bsi_bscid = bsi_bscid ) AND  
         ( bodegas_sector_insumos.bsi_posicion = @bsi_posicion ) AND  
         ( bodegas_sector_insumos.bsi_insid = @bsi_insid )
