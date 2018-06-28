

Create Procedure Delete_Detalle_Comprobantes(@dcc_codemp integer, @dcc_sucid integer, @dcc_tpcid integer, 
															@dcc_numero numeric (15), @dcc_item smallint) as  
 

  DELETE FROM detalle_comprobantes_provcli  
   WHERE ( detalle_comprobantes_provcli.dbp_codemp = @dcc_codemp ) AND  
         ( detalle_comprobantes_provcli.dbp_sucid = @dcc_sucid ) AND  
         ( detalle_comprobantes_provcli.dbp_tpcid = @dcc_tpcid ) AND  
         ( detalle_comprobantes_provcli.dbp_numero = @dcc_numero ) AND  
         ( detalle_comprobantes_provcli.dbp_item = @dcc_item )   
           

  DELETE FROM detalle_comprobantes_rol  
   WHERE ( detalle_comprobantes_rol.dcr_codemp = @dcc_codemp ) AND  
         ( detalle_comprobantes_rol.dcr_sucid = @dcc_sucid ) AND  
         ( detalle_comprobantes_rol.dcr_tpcid = @dcc_tpcid ) AND  
         ( detalle_comprobantes_rol.dcr_numero = @dcc_numero ) AND  
         ( detalle_comprobantes_rol.dcr_item = @dcc_item)   
           

 DELETE FROM detalle_comprobantes  
   WHERE ( detalle_comprobantes.dcc_codemp = @dcc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @dcc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @dcc_tpcid ) AND  
         ( detalle_comprobantes.dcc_numero = @dcc_numero ) AND  
         ( detalle_comprobantes.dcc_item = @dcc_item )
