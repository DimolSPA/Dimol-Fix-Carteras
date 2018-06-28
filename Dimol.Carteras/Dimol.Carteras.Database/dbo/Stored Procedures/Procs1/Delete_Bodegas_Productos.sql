

Create Procedure Delete_Bodegas_Productos(@bsp_codemp integer, @bsp_bodid integer, @bsp_bdsid integer, @bsp_prodid numeric (15), @bsp_posicion smallint, @bsp_bscid varchar(20)) as  
  DELETE FROM bodegas_sector_productos  
   WHERE ( bodegas_sector_productos.bsp_codemp = @bsp_codemp ) AND  
         ( bodegas_sector_productos.bsp_bodid = @bsp_bodid ) AND  
         ( bodegas_sector_productos.bsp_bdsid = @bsp_bdsid ) AND  
         ( bodegas_sector_productos.bsp_prodid = @bsp_prodid ) AND  
         ( bodegas_sector_productos.bsp_posicion = @bsp_posicion )  AND
         ( bodegas_sector_productos.bsp_bscid = @bsp_bscid )
