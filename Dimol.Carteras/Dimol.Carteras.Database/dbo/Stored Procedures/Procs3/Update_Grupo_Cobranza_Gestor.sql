

Create Procedure Update_Grupo_Cobranza_Gestor(@gcg_codemp integer, @gcg_sucid integer, @gcg_grcid integer, @gcg_gesid integer) as
    UPDATE grupo_cobranza_gestor  
     SET  gcg_gesid = @gcg_gesid
   WHERE ( grupo_cobranza_gestor.gcg_codemp = @gcg_codemp ) AND  
         ( grupo_cobranza_gestor.gcg_sucid = @gcg_sucid ) AND  
         ( grupo_cobranza_gestor.gcg_grcid = @gcg_grcid ) AND  
         ( grupo_cobranza_gestor.gcg_gesid = @gcg_gesid )
