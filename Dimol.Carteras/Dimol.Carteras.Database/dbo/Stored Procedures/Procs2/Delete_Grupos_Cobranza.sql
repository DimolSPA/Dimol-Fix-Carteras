

Create Procedure Delete_Grupos_Cobranza(@grc_codemp integer, @grc_sucid integer, @grc_grcid integer) as

 DELETE FROM grupo_cobranza_gestor  
   WHERE ( grupo_cobranza_gestor.gcg_codemp = @grc_codemp ) AND  
         ( grupo_cobranza_gestor.gcg_sucid = @grc_sucid ) AND  
         ( grupo_cobranza_gestor.gcg_grcid = @grc_grcid ) 

  DELETE FROM grupos_cobranza  
   WHERE ( grupos_cobranza.grc_codemp = @grc_codemp ) AND  
         ( grupos_cobranza.grc_sucid = @grc_sucid ) AND  
         ( grupos_cobranza.grc_grcid = @grc_grcid )
