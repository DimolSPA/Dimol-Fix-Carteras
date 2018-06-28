

Create Procedure Update_Bodegas_Sector_Insumos(@bsi_codemp integer, @bsi_bodid integer, @bsi_bdsid integer, @bsi_insid numeric (15),
																@bsi_posicion smallint, @bsi_stock decimal (15,2), @bsi_merma decimal (15,2),
																@bsi_reservado decimal (15,2), @bsi_transito decimal (15,2), @bsi_bscid varchar (10)) as  
  UPDATE bodegas_sector_insumos  
     SET bsi_codemp = @bsi_codemp,   
         bsi_bodid = @bsi_bodid,   
         bsi_bdsid = @bsi_bdsid,   
         bsi_insid = @bsi_insid,   
         bsi_posicion = @bsi_posicion,   
         bsi_stock = @bsi_stock,   
         bsi_merma = @bsi_merma,   
         bsi_reservado = @bsi_reservado,   
         bsi_transito = @bsi_transito,   
         bsi_bscid = @bsi_bscid  
   WHERE ( bodegas_sector_insumos.bsi_codemp = @bsi_codemp ) AND  
         ( bodegas_sector_insumos.bsi_bodid = @bsi_bodid ) AND  
         ( bodegas_sector_insumos.bsi_bdsid = @bsi_bdsid ) AND  
         ( bodegas_sector_insumos.bsi_insid = @bsi_insid ) AND  
         ( bodegas_sector_insumos.bsi_posicion = @bsi_posicion )
