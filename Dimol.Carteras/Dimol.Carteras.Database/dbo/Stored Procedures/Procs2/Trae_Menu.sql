

Create Procedure Trae_Menu(@idiomas integer) as
  SELECT menu_idiomas.mni_nombre,   
         treeview_idiomas.tvi_idiomas,   
         '~/' + men_directorio + '/' + trv_ventana as Ventana,   
         menu.men_menid,   
         treeview.trv_trvid,   
         treeview.trv_padid,   
         menu.men_imagen,   
         treeview.trv_imagen  
    FROM menu,   
         menu_idiomas,   
         treeview,   
         treeview_idiomas  
   WHERE ( menu_idiomas.mni_menid = menu.men_menid ) and  
         ( treeview.trv_menid = menu.men_menid and mni_idid = @idiomas) and  
         ( treeview_idiomas.tvi_trvid = treeview.trv_trvid and tvi_idid = @idiomas)   
ORDER BY menu.men_orden ASC,   
         treeview.trv_orden ASC
