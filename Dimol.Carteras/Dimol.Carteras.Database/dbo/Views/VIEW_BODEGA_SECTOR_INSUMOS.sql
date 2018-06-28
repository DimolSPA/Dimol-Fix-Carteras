
/*==============================================================*/
/* View: VIEW_BODEGA_SECTOR_INSUMOS                             */
/*==============================================================*/
create view VIEW_BODEGA_SECTOR_INSUMOS as
  SELECT bodegas.bod_codemp,   
         bodegas.bod_bodid,   
         bodegas.bod_nombre,   
         bodegas_sector.bds_bdsid,   
         bodegas_sector.bds_nombre,   
         bodegas_sector_cubiculo.bsc_bscid,   
         bodegas_sector_insumos.bsi_posicion, 
         bodegas_sector_insumos.bsi_insid,   
         bodegas_sector_insumos.bsi_stock,   
         bodegas_sector_insumos.bsi_merma,   
         bodegas_sector_insumos.bsi_reservado,   
         bodegas_sector_insumos.bsi_transito  
    FROM bodegas,   
         bodegas_sector,   
         bodegas_sector_cubiculo,   
         bodegas_sector_insumos  
   WHERE ( bodegas_sector.bds_codemp = bodegas.bod_codemp ) and  
         ( bodegas_sector.bds_bodid = bodegas.bod_bodid ) and  
         ( bodegas_sector_cubiculo.bsc_codemp = bodegas_sector.bds_codemp ) and  
         ( bodegas_sector_cubiculo.bsc_bodid = bodegas_sector.bds_bodid ) and  
         ( bodegas_sector_cubiculo.bsc_bdsid = bodegas_sector.bds_bdsid ) and  
         ( bodegas_sector_insumos.bsi_codemp = bodegas_sector_cubiculo.bsc_codemp ) and  
         ( bodegas_sector_insumos.bsi_bodid = bodegas_sector_cubiculo.bsc_bodid ) and  
         ( bodegas_sector_insumos.bsi_bdsid = bodegas_sector_cubiculo.bsc_bdsid ) and  
         ( bodegas_sector_insumos.bsi_bscid = bodegas_sector_cubiculo.bsc_bscid )
