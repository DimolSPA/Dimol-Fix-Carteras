

Create Procedure Delete_Perfiles_Menu_Item(@pfm_codemp integer, @pfm_prfid integer, @pfm_trvid integer) as  
  DELETE FROM perfiles_menu  
   WHERE ( perfiles_menu.pfm_codemp = @pfm_codemp ) AND  
         ( perfiles_menu.pfm_prfid = @pfm_prfid and pfm_trvid = @pfm_trvid)
