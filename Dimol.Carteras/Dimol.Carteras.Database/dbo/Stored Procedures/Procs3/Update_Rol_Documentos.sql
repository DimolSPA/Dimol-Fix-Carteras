

Create Procedure Update_Rol_Documentos(@rdc_codemp integer, @rdc_rolid integer, @rdc_pclid numeric (15), @rdc_ctcid numeric (15),
													@rdc_ccbid integer, @rdc_monto decimal (15,2), @rdc_saldo decimal (15,2)) as  
  UPDATE rol_documentos  
     SET rdc_codemp = @rdc_codemp,   
         rdc_rolid = @rdc_rolid,   
         rdc_pclid = @rdc_pclid,   
         rdc_ctcid = @rdc_ctcid,   
         rdc_ccbid = @rdc_ccbid,   
         rdc_monto = @rdc_monto,   
         rdc_saldo = @rdc_saldo  
   WHERE ( rol_documentos.rdc_codemp = @rdc_codemp ) AND  
         ( rol_documentos.rdc_rolid = @rdc_rolid ) AND  
         ( rol_documentos.rdc_pclid = @rdc_pclid ) AND  
         ( rol_documentos.rdc_ctcid = @rdc_ctcid ) AND  
         ( rol_documentos.rdc_ccbid = @rdc_ccbid )
