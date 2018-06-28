

Create Procedure Insertar_Grupo_Cobranza_Gestor(@gcg_codemp integer, @gcg_sucid integer, @gcg_grcid integer, @gcg_gesid integer) as
  INSERT INTO grupo_cobranza_gestor  
         ( gcg_codemp,   
           gcg_sucid,   
           gcg_grcid,   
           gcg_gesid )  
  VALUES ( @gcg_codemp,   
           @gcg_sucid,   
           @gcg_grcid,   
           @gcg_gesid )
