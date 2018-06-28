

Create Procedure Trae_Perfil_Menu_Asignado(@codemp integer, @perfil integer, @idiomas integer) as
SELECT treeview.trv_trvid,   
         treeview.trv_padid,   
         treeview_idiomas.tvi_idiomas, 
        CASE   trv_ventana when '' THEN '' ELSE '' + trv_ventana END as Ventana,  
         treeview.trv_imagen  
    FROM menu,   
         treeview,   
         treeview_idiomas,   
         perfiles_menu
   WHERE ( treeview.trv_menid = menu.men_menid and tvi_idid = @idiomas ) and  
         ( treeview_idiomas.tvi_trvid = treeview.trv_trvid ) and  
         ( perfiles_menu.pfm_trvid = treeview.trv_trvid ) and  
         ( perfiles_menu.pfm_prfid = @perfil and pfm_codemp = @codemp)   
ORDER BY menu.men_orden ASC,   
         treeview.trv_orden ASC
