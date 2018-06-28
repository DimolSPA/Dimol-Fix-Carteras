

Create Procedure Insertar_Perfiles_Menu(@pfm_codemp integer, @pfm_prfid integer, @pfm_trvid integer) as  
  INSERT INTO perfiles_menu  
         ( pfm_codemp,   
           pfm_prfid,   
           pfm_trvid )  
  VALUES ( @pfm_codemp,   
           @pfm_prfid,   
           @pfm_trvid )
