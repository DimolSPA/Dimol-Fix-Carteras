

Create Procedure Insertar_Bodegas_Sector_Insumos(@bsi_codemp integer, @bsi_bodid integer, @bsi_bdsid integer, @bsi_insid numeric (15),
																    @bsi_bscid integer, @bsi_posicion smallint) as  
  INSERT INTO bodegas_sector_insumos  
         ( bsi_codemp,   
           bsi_bodid,   
           bsi_bdsid,   
           bsi_insid,   
           bsi_posicion,   
           bsi_stock,   
           bsi_merma,   
           bsi_reservado,   
           bsi_transito,   
           bsi_bscid )  
  VALUES ( @bsi_codemp,   
           @bsi_bodid,   
           @bsi_bdsid,   
           @bsi_insid,   
           @bsi_posicion,   
           0,   
           0,   
           0,   
           0,   
           @bsi_bscid )
