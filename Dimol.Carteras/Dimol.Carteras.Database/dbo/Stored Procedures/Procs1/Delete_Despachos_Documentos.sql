

Create Procedure Delete_Despachos_Documentos(@dcd_codemp integer, @dcd_sucid integer, @dcd_dpcid numeric (15),
															@dcd_tpcid integer, @dcd_numero numeric (15)) as

  DELETE FROM despachos_documentos_estados  
   WHERE ( despachos_documentos_estados.dde_codemp = @dcd_codemp ) AND  
         ( despachos_documentos_estados.dde_sucid = @dcd_sucid ) AND  
         ( despachos_documentos_estados.dde_dpcid = @dcd_dpcid ) AND  
         ( despachos_documentos_estados.dde_tpcid = @dcd_tpcid ) AND  
         ( despachos_documentos_estados.dde_numero = @dcd_numero ) 

  DELETE FROM despachos_documentos  
   WHERE ( despachos_documentos.dcd_codemp = @dcd_codemp ) AND  
         ( despachos_documentos.dcd_sucid = @dcd_sucid ) AND  
         ( despachos_documentos.dcd_dpcid = @dcd_dpcid ) AND  
         ( despachos_documentos.dcd_tpcid = @dcd_tpcid ) AND  
         ( despachos_documentos.dcd_numero = @dcd_numero )
