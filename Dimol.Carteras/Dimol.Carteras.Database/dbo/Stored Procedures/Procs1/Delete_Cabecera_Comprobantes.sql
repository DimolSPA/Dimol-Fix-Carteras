

Create Procedure Delete_Cabecera_Comprobantes(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero numeric (15)) as  

  DELETE FROM detalle_comprobantes_provcli  
   WHERE ( detalle_comprobantes_provcli.dbp_codemp = @cbc_codemp ) AND  
         ( detalle_comprobantes_provcli.dbp_sucid = @cbc_sucid ) AND  
         ( detalle_comprobantes_provcli.dbp_tpcid = @cbc_tpcid ) AND  
         ( detalle_comprobantes_provcli.dbp_numero = @cbc_numero )   
         

  DELETE FROM Cabacera_Comprobantes_Envio_Doc  
   WHERE ( Cabacera_Comprobantes_Envio_Doc.cbv_codemp = @cbc_codemp ) AND  
         ( Cabacera_Comprobantes_Envio_Doc.cbv_sucid = @cbc_sucid ) AND  
         ( Cabacera_Comprobantes_Envio_Doc.cbv_tpcid = @cbc_tpcid ) AND  
         ( Cabacera_Comprobantes_Envio_Doc.cbv_numero = @cbc_numero ) 


  DELETE FROM detalle_comprobantes_rol  
   WHERE ( detalle_comprobantes_rol.dcr_codemp = @cbc_codemp ) AND  
         ( detalle_comprobantes_rol.dcr_sucid = @cbc_sucid ) AND  
         ( detalle_comprobantes_rol.dcr_tpcid = @cbc_tpcid ) AND  
         ( detalle_comprobantes_rol.dcr_numero = @cbc_numero ) 


  DELETE FROM cabacera_comprobantes_estados  
   WHERE ( cabacera_comprobantes_estados.cbe_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes_estados.cbe_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_estados.cbe_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes_estados.cbe_numero = @cbc_numero ) 

  DELETE FROM cabacera_comprobantes_motivos_castigo  
   WHERE ( cabacera_comprobantes_motivos_castigo.cbm_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_numero = @cbc_numero ) 



  DELETE FROM detalle_comprobantes  
   WHERE ( detalle_comprobantes.dcc_codemp = @cbc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @cbc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @cbc_tpcid ) AND  
         ( detalle_comprobantes.dcc_numero = @cbc_numero ) 


  DELETE FROM detalle_comprobantes_rol  
   WHERE ( detalle_comprobantes_rol.dcr_codemp = @cbc_codemp ) AND  
         ( detalle_comprobantes_rol.dcr_sucid = @cbc_sucid ) AND  
         ( detalle_comprobantes_rol.dcr_tpcid = @cbc_tpcid ) AND  
         ( detalle_comprobantes_rol.dcr_numero = @cbc_numero ) 


  DELETE FROM cabacera_comprobantes_estados  
   WHERE ( cabacera_comprobantes_estados.cbe_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes_estados.cbe_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_estados.cbe_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes_estados.cbe_numero = @cbc_numero ) 

  DELETE FROM cabacera_comprobantes_motivos_castigo  
   WHERE ( cabacera_comprobantes_motivos_castigo.cbm_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes_motivos_castigo.cbm_numero = @cbc_numero ) 


  DELETE FROM cabacera_comprobantes_op  
   WHERE ( cabacera_comprobantes_op.cbo_codemp = @cbc_codemp  ) AND  
         ( cabacera_comprobantes_op.cbo_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes_op.cbo_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes_op.cbo_numero = @cbc_numero )   


DELETE FROM cabacera_comprobantes  
   WHERE ( cabacera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes.cbc_numero = @cbc_numero )
