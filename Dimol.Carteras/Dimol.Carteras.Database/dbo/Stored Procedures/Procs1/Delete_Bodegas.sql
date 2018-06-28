

Create Procedure Delete_Bodegas(@bod_codemp integer, @bod_bodid integer) as  

 DELETE FROM bodegas_sector  
   WHERE ( bodegas_sector.bds_codemp = @bod_codemp ) AND  
         ( bodegas_sector.bds_bodid = @bod_bodid ) 

  DELETE FROM bodegas_sector_cubiculo  
   WHERE ( bodegas_sector_cubiculo.bsc_codemp = @bod_codemp ) AND  
         ( bodegas_sector_cubiculo.bsc_bodid = @bod_bodid ) 


  DELETE FROM bodegas  
   WHERE ( bodegas.bod_codemp = @bod_codemp ) AND  
         ( bodegas.bod_bodid = @bod_bodid )
