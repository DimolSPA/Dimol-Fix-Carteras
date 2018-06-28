

 Create Procedure Update_Perfiles_Menu(@pfm_codemp integer, @pfm_prfid integer, @pfm_trvid integer) as
  UPDATE perfiles_menu  
     SET pfm_trvid = @pfm_trvid  
   WHERE ( perfiles_menu.pfm_codemp = @pfm_codemp ) AND  
         ( perfiles_menu.pfm_prfid = @pfm_prfid ) AND  
         ( perfiles_menu.pfm_trvid = @pfm_trvid )
