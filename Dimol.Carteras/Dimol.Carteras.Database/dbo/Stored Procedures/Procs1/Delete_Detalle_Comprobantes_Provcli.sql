

Create Procedure Delete_Detalle_Comprobantes_Provcli(@dbp_codemp integer, @dbp_sucid integer, @dbp_tpcid integer, @dbp_numero numeric(15),
																		 @dbp_item integer, @dbp_subitem integer) as
    DELETE FROM detalle_comprobantes_provcli  
   WHERE ( detalle_comprobantes_provcli.dbp_codemp = @dbp_codemp ) AND  
         ( detalle_comprobantes_provcli.dbp_sucid = @dbp_sucid ) AND  
         ( detalle_comprobantes_provcli.dbp_tpcid = @dbp_tpcid ) AND  
         ( detalle_comprobantes_provcli.dbp_numero = @dbp_numero ) AND  
         ( detalle_comprobantes_provcli.dbp_item = @dbp_item )   AND
         ( detalle_comprobantes_provcli.dbp_subitem = @dbp_subitem )
