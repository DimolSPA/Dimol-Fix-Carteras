

Create Procedure Delete_Bodegas_Sector(@bds_codemp integer, @bds_bodid integer, @bds_bdsid integer) as  

  DELETE FROM bodegas_sector_cubiculo  
   WHERE ( bodegas_sector_cubiculo.bsc_codemp = @bds_codemp ) AND  
         ( bodegas_sector_cubiculo.bsc_bodid = @bds_bodid ) AND  
         ( bodegas_sector_cubiculo.bsc_bdsid = @bds_bdsid ) 


  DELETE FROM bodegas_sector  
   WHERE ( bodegas_sector.bds_codemp = @bds_codemp ) AND  
         ( bodegas_sector.bds_bodid = @bds_bodid ) AND  
         ( bodegas_sector.bds_bdsid = @bds_bdsid )
