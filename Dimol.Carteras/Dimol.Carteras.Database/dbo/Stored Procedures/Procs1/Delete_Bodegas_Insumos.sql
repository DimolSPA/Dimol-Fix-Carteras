

Create Procedure Delete_Bodegas_Insumos(@bsi_codemp integer, @bsi_bodid integer, @bsi_bdsid integer, @bsi_bscid varchar(10), @bsi_posicion smallint, @bsi_insid numeric (15)) as  
  DELETE FROM bodegas_sector_insumos  
   WHERE ( bodegas_sector_insumos.bsi_codemp = @bsi_codemp ) AND  
         ( bodegas_sector_insumos.bsi_bodid = @bsi_bodid) AND  
         ( bodegas_sector_insumos.bsi_bdsid = @bsi_bdsid ) AND  
         ( bodegas_sector_insumos.bsi_bscid = @bsi_bscid ) AND  
         ( bodegas_sector_insumos.bsi_posicion = @bsi_posicion ) AND  
         ( bodegas_sector_insumos.bsi_insid = @bsi_insid )
