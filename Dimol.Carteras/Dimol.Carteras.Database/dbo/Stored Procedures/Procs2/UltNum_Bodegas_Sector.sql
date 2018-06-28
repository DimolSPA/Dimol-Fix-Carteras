

Create Procedure UltNum_Bodegas_Sector(@bds_codemp integer, @bds_bodid integer) as
  SELECT IsNull(Max(bds_bdsid)+1, 1)
    FROM bodegas_sector  
   WHERE ( bodegas_sector.bds_codemp = @bds_codemp ) AND  
         ( bodegas_sector.bds_bodid = @bds_bodid )
