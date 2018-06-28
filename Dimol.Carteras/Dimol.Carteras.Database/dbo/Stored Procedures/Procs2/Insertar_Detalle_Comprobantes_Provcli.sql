

Create Procedure Insertar_Detalle_Comprobantes_Provcli(@dbp_codemp integer, @dbp_sucid integer, @dbp_tpcid integer, @dbp_numero numeric(15),
																		 @dbp_item integer, @dbp_subitem integer, @dbp_pclid integer, @dbp_ctcid integer, @dbp_monto decimal(15,2)) as
   INSERT INTO detalle_comprobantes_provcli  
         ( dbp_codemp,   
           dbp_sucid,   
           dbp_tpcid,   
           dbp_numero,   
           dbp_item,   
           dbp_subitem,   
           dbp_pclid,   
           dbp_ctcid,   
           dbp_monto )  
  VALUES ( @dbp_codemp,   
           @dbp_sucid,   
           @dbp_tpcid,   
           @dbp_numero,   
           @dbp_item,   
           @dbp_subitem, 
           @dbp_pclid,   
           @dbp_ctcid,   
           @dbp_monto )
