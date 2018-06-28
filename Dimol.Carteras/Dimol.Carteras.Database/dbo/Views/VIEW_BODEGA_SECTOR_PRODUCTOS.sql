
/*==============================================================*/
/* View: VIEW_BODEGA_SECTOR_PRODUCTOS                           */
/*==============================================================*/
create view VIEW_BODEGA_SECTOR_PRODUCTOS as
   SELECT bodegas.bod_codemp,   
         bodegas.bod_bodid,   
         bodegas.bod_nombre,   
         bodegas_sector.bds_bdsid,   
         bodegas_sector.bds_nombre,   
         bodegas_sector_cubiculo.bsc_bscid,   
         bodegas_sector_productos.bsp_prodid,   
         bodegas_sector_productos.bsp_posicion,   
         bodegas_sector_productos.bsp_stock,   
         bodegas_sector_productos.bsp_merma,   
         bodegas_sector_productos.bsp_reservado,   
         bodegas_sector_productos.bsp_transito  
    FROM bodegas,   
         bodegas_sector,   
         bodegas_sector_cubiculo,   
         bodegas_sector_productos  
   WHERE ( bodegas_sector.bds_codemp = bodegas.bod_codemp ) and  
         ( bodegas_sector.bds_bodid = bodegas.bod_bodid ) and  
         ( bodegas_sector_cubiculo.bsc_codemp = bodegas_sector.bds_codemp ) and  
         ( bodegas_sector_cubiculo.bsc_bodid = bodegas_sector.bds_bodid ) and  
         ( bodegas_sector_cubiculo.bsc_bdsid = bodegas_sector.bds_bdsid ) and  
         ( bodegas_sector_productos.bsp_codemp = bodegas_sector_cubiculo.bsc_codemp ) and  
         ( bodegas_sector_productos.bsp_bodid = bodegas_sector_cubiculo.bsc_bodid ) and  
         ( bodegas_sector_productos.bsp_bdsid = bodegas_sector_cubiculo.bsc_bdsid ) and  
         ( bodegas_sector_productos.bsp_bscid = bodegas_sector_cubiculo.bsc_bscid )
