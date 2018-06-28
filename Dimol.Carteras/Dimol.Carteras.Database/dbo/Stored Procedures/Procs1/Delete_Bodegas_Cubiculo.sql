

Create Procedure Delete_Bodegas_Cubiculo(@bsc_codemp integer, @bsc_bodid integer, @bsc_bdsid integer, @bsc_bscid varchar (10)) as  
  DELETE FROM bodegas_sector_cubiculo  
   WHERE ( bodegas_sector_cubiculo.bsc_codemp = @bsc_codemp ) AND  
         ( bodegas_sector_cubiculo.bsc_bodid = @bsc_bodid ) AND  
         ( bodegas_sector_cubiculo.bsc_bdsid = @bsc_bdsid ) AND  
         ( bodegas_sector_cubiculo.bsc_bscid = @bsc_bscid )
