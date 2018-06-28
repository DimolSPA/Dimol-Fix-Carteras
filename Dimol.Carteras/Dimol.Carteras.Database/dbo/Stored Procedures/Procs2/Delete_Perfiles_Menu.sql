

Create Procedure Delete_Perfiles_Menu(@pfm_codemp integer, @pfm_prfid integer) as  
  DELETE FROM perfiles_menu  
   WHERE ( perfiles_menu.pfm_codemp = @pfm_codemp ) AND  
         ( perfiles_menu.pfm_prfid = @pfm_prfid )
