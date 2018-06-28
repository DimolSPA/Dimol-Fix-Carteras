

Create Procedure Update_Detalle_Comprobantes_ProvCli(@dbp_codemp integer, @dbp_sucid integer, @dbp_tpcid integer, @dbp_numero numeric(15), @dbp_item integer, @dbp_monto decimal(15,2)) as
  UPDATE detalle_comprobantes_provcli  
     SET dbp_monto = @dbp_monto  
   WHERE ( detalle_comprobantes_provcli.dbp_codemp = @dbp_codemp ) AND  
         ( detalle_comprobantes_provcli.dbp_sucid = @dbp_sucid ) AND  
         ( detalle_comprobantes_provcli.dbp_tpcid = @dbp_tpcid ) AND  
         ( detalle_comprobantes_provcli.dbp_numero = @dbp_numero ) AND  
         ( detalle_comprobantes_provcli.dbp_item = @dbp_item )
