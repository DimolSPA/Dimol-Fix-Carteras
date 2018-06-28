

Create Procedure Find_Bodegas_Sector(@bds_codemp integer, @bds_bodid integer, @bds_bdsid integer) as
  SELECT count(bodegas_sector.bds_bdsid)  
    FROM bodegas_sector  
   WHERE ( bodegas_sector.bds_codemp = @bds_codemp ) AND  
         ( bodegas_sector.bds_bodid = @bds_bodid )   AND
         ( bodegas_sector.bds_bdsid = @bds_bdsid )
